<template>
  <div class="container mcontainer">
    <h1 class="tw-text-xl tw-font-bold" style="font-family: iranSans !important;">دوستان من</h1>
    <div class="row px-0 col-12 mx-auto my-3" v-if="!$fetchState.pending">
        <div class="col-12">
          <multiple-tabs
            @changeTab="toggleTab"
            :followerCount="followerList.length"
            :followingCount="followingList.length"
            :followReqsCount="followRequestList.length"
          ></multiple-tabs>
        </div>

        <!-- For followers tab -->
        <follow-card
          v-if="isRenderingComponent('followers')"
          :followList="followerList"
          @unfollow="unfollowASingleFollower"
        ></follow-card>

        <!-- For following tab -->
        <follow-card
          v-if="isRenderingComponent('following')"
          :followList="followingList"
          @unfollow="unfollowASingleFollowing"
        ></follow-card>

        <!-- For follow requests tab -->
        <follow-card
          v-if="isRenderingComponent('friendReqs')"
          :followList="followRequestList"
          :isARequestCard="true"
          @acceptReq="acceptFriendReq"
          @rejectReq="rejectFriendReq"
        ></follow-card>
      </div>
    </div>
</template>

<script>
import MultipleTabs from "../../components/Friends/MultipleTabs.vue";
import FollowCard from "../../components/Friends/FollowCard.vue";
export default {
  name: "TimelineFriends",
  layout: "BanooClubLayout",
  components: {
    MultipleTabs,
    FollowCard,
  },
  fetchOnServer() {
    return true;
  },
  async fetch() {
    // Get followers
    const followers = await this.$repositories.getMyFollowers.getMyFollowers();
    this.followerList = followers.data;

    // Get following
    const following =
      await this.$repositories.getMyFollowings.getMyFollowings();
    this.followingList = following.data;

    // Get follow requests
    const followReqs =
      await this.$repositories.getMyFollowRequests.getMyFollowRequests();
    this.followRequestList = followReqs.data;
  },

  data() {
    return {
      activeTab: "followers",
      followerList: null,
      followingList: null,
      followRequestList: null,
    };
  },

  methods: {
    async acceptFriendReq(reqId) {
      try {
        await this.$repositories.followRequestRespond.followRequestRespond({
          followRequestId: reqId,
          Respond: true,
        });
      } catch (error) {
        console.log(error);
      } finally {
        this.$fetch();
      }
    },

    async rejectFriendReq(reqId) {
      try {
        await this.$repositories.followRequestRespond.followRequestRespond({
          followRequestId: reqId,
          Respond: false,
        });
      } catch (error) {
        console.log(error);
      } finally {
        this.$fetch();
      }
    },

    async unfollowASingleFollower(user) {
      try {
        await this.$repositories.deleteASingleFollower.deleteASingleFollower(
          user.followerUserId
        );
      } catch (error) {
        console.log(error);
      } finally {
        this.$fetch();
      }
    },

    async unfollowASingleFollowing(user) {
      try {
        await this.$repositories.deleteASingleFollowing.deleteASingleFollowing(
          user.followingUserId
        );
      } catch (error) {
        console.log(error);
      } finally {
        this.$fetch();
      }
    },

    isRenderingComponent(componentName) {
      if (this.activeTab == componentName) {
        return true;
      }
    },

    toggleTab(tab) {
      this.activeTab = tab;
    },
  },
};
</script>

<style scoped>
.boxMainContent {
  background: #fdfdfd none repeat scroll 0 0;
  border: 1px solid #ede9e9;
  border-radius: 3px;
  display: inline-block;
  /* width: 100%; */
  margin-bottom: 20px;
  padding: 25px;
}
@media (min-width: 1024px) {
  .mcontainer {
    max-width: 1000px;
    /*padding: 30px 120px;*/
    margin-left: auto;
    margin-right: auto;
  }
}
@media (max-width: 1024px) {
  .mcontainer {
    max-width: 1000px;
    padding: 25px 30px;
    margin-left: auto;
    margin-right: auto;
  }
}
</style>
