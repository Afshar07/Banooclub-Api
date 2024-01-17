<template>
  <div class="overflow-scroll">
    <div class="bg-white tab_content_back" v-if="!$fetchState.pending">
      <ul class="nav nav-pills align-items-end profile_tabs tw-overflow-x-auto px-3" id="pills-tab" role="tablist" style="z-index: 99;">
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link active" id="services-tab" data-bs-toggle="pill" data-bs-target="#services"
                  type="button" role="tab" aria-controls="services" aria-selected="true">
            <span>دنبال کنندگان</span>
          </button>
        </li>
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link" id="posts-home-tab" data-bs-toggle="pill" data-bs-target="#posts-home"
                  type="button" role="tab" aria-controls="posts-home" aria-selected="true">
            <span>دنبال شوندگان</span>
          </button>
        </li>
      </ul>
      <div class="tab-content" id="pills-tabContent">
        <div class="tab-pane fade show active" id="services" role="tabpanel" aria-labelledby="services-tab">
        </div>
        <div class="tab-pane fade" id="posts-home" role="tabpanel" aria-labelledby="posts-home-tab">
        </div>
      </div>


    </div>
<!--    <div class="bg-white tab_content_back" v-if="!$fetchState.pending">-->
<!--&lt;!&ndash;            <div&ndash;&gt;-->
<!--&lt;!&ndash;              class="row boxMainContent col-12 mx-auto rounded main-shadow"&ndash;&gt;-->
<!--&lt;!&ndash;              v-if="!$fetchState.pending"&ndash;&gt;-->
<!--&lt;!&ndash;            >&ndash;&gt;-->
<!--      <div class="col-12">-->
<!--        <multiple-tabs-->
<!--          @changeTab="toggleTab"-->
<!--          :followerCount="followerList.length"-->
<!--          :followingCount="followingList.length"-->
<!--          :followReqsCount="0"-->
<!--          :in_my_profile="false"-->
<!--        ></multiple-tabs>-->
<!--      </div>-->

<!--      &lt;!&ndash; For followers tab &ndash;&gt;-->
<!--      <follow-card-->
<!--        v-if="isRenderingComponent('followers')"-->
<!--        :followList="followerList"-->
<!--        @unfollow="unfollowASingleFollower"-->
<!--        :is_follower="false"-->
<!--      ></follow-card>-->

<!--      &lt;!&ndash; For following tab &ndash;&gt;-->
<!--      <follow-card-->
<!--        v-if="isRenderingComponent('following')"-->
<!--        :followList="followingList"-->
<!--        @unfollow="unfollowASingleFollowing"-->
<!--        :is_follower="false"-->
<!--      ></follow-card>-->

<!--&lt;!&ndash;       For follow requests tab&ndash;&gt;-->
<!--&lt;!&ndash;      <follow-card&ndash;&gt;-->
<!--&lt;!&ndash;        v-if="isRenderingComponent('friendReqs')"&ndash;&gt;-->
<!--&lt;!&ndash;        :followList="followRequestList"&ndash;&gt;-->
<!--&lt;!&ndash;        :isARequestCard="true"&ndash;&gt;-->
<!--&lt;!&ndash;        @acceptReq="acceptFriendReq"&ndash;&gt;-->
<!--&lt;!&ndash;        @rejectReq="rejectFriendReq"&ndash;&gt;-->
<!--&lt;!&ndash;        :is_follower="true"&ndash;&gt;-->

<!--&lt;!&ndash;      ></follow-card>&ndash;&gt;-->
<!--    </div>-->



    <!--    </div>-->


  </div>


</template>

<script>
import MultipleTabs from "../Friends/MultipleTabs";
import FollowCard from "../Friends/FollowCard";
export default {
  name: "UserFriends",
  components: {
    MultipleTabs,
    FollowCard,
  },
  fetchOnServer() {
    return true;
  },
  async fetch() {
    // Get followers
    const followers = await this.$repositories.FollowerGetByUserId.FollowerGetByUserId({
      userId:this.$route.params.id
    });
    this.followerList = followers.data;


    // Get following
    const following = await this.$repositories.FollowingGetByUserId.FollowingGetByUserId({
      userId:this.$route.params.id
    });
    this.followingList = following.data.data.followings;



  },
  data() {
    return {
      activeTab: "followers",
      followerList: null,
      followingList: null,
      followRequestList: null,
      UserId:0
    };
  },

  methods: {
    setUserId(id){

      this.UserId = id
    },
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
}
</script>

<style scoped>

</style>
