<template>
  <div class="overflow-scroll">
    <div class=" tab_content_back tw-bg-white tw-rounded-xl p-md-4 p-0" v-if="!$fetchState.pending">
      <!--      <div-->
      <!--        class="row boxMainContent col-12 mx-auto rounded main-shadow"-->
      <!--        v-if="!$fetchState.pending"-->
      <!--      >-->
      <div class="col-12">
        <multiple-tabs
          @changeTab="toggleTab"
          :in_my_profile="true"
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
        :is_follower="false"
      ></follow-card>

      <!-- For following tab -->
      <follow-card
        v-if="isRenderingComponent('following')"
        :followList="followingList"
        @unfollow="unfollowASingleFollowing"
        :is_follower="false"
      ></follow-card>

      <!-- For follow requests tab -->
      <follow-card
        v-if="isRenderingComponent('friendReqs')"
        :followList="followRequestList"
        :isARequestCard="true"
        @acceptReq="acceptFriendReq"
        @rejectReq="rejectFriendReq"
        :is_follower="true"

      ></follow-card>
      <div @click="$router.push(`/Social/Contacts`)" class="col-12">
        <div class="d-flex flex-row justify-content-center gap-5 w-100 overflow-scroll text-nowrap">
          <span class="text-purple">اینجا دوست جدید پیدا کن</span>
        </div>
      </div>
    </div>



    <!--    </div>-->


  </div>

</template>

<script>
import MultipleTabs from "../Friends/MultipleTabs";
import FollowCard from "../Friends/FollowCard";
export default {
  name: "FriendsTabContent",
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
        this.activeTab = 'followers'
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
}
</script>

<style scoped>

</style>
