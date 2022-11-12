<template>
  <div class="row "  v-if="followList">
    <div @click="goToUserProfile(item.userInfo)" class="col-md-3  tw-cursor-pointer my-2 p-3"
         v-for="(item, index) in followList" :key="index">
      <div class="rounded shadow border d-flex flex-column bg-white justify-content-center align-items-center ">
            <div class="Card-Img tw-w-full tw-h-[10rem] relative">
              <img :src="isARequestCard ? requestUserAvatar(item) : userAvatar(item)" class="custom-image tw-absolute -tw-translate-y-1/4  tw-top-1/4  -tw-translate-x-1/2 tw-left-1/2 " alt="">
              <img alt="User Avatar" :src="isARequestCard ? requestUserBanner(item) : userBanner(item)" class="tw-w-full tw-h-full tw-object-cover  "/>

            </div>
        <div class="d-flex align-items-center py-3 friendName">
          <strong class="text-primary tw-cursor-pointer">
            {{isARequestCard ? item.followerInfo.name : item.userInfo.name}}
            {{isARequestCard ? item.followerInfo.familyName : item.userInfo.familyName }}
          </strong>
        </div>
        <div class="d-flex align-items-center gap-2 my-3">
          <small v-if="!isARequestCard && item.userInfo.followingsCount"> {{ item.userInfo.followingsCount }}</small>
          <FollowerIcon style="fill: #0d6efd;width: 24px;height: 24px;"/>
          <FollowingIcon style="fill: #0d6efd;width: 24px;height: 24px;"/>
          <small v-if="!isARequestCard && item.userInfo.followingsCount" >     {{ item.userInfo.followersCount }}</small>
        </div>
        <div class="d-flex align-items-center justify-content-center mb-2">
          <button v-if="!isARequestCard" type="button" style="border-radius: .375rem" class="bg-pink rounded p-2 text-white" @click.stop="unfollowUser(item)">
            حذف
          </button>
          <div class="w-100 d-flex align-items-center gap-2 mb-2" v-if="isARequestCard">
            <button type="button" class="bg-pink rounded p-2 text-white" @click.stop="acceptFollowReq(item.followRequestId)">
              قبول درخواست
            </button>
            <button type="button" class="bg-purple rounded p-2 text-white" @click.stop="rejectFollowReq(item.followRequestId)">
              رد درخواست
            </button>
          </div>
        </div>

      </div>
    </div>
  </div>
  <div v-else>
    <p>اطلاعاتی جهت نمایش وجود ندارد</p>
  </div>
</template>

<script>
import FollowerIcon from "@/components/Icons/FollowerIcon";
import FollowingIcon from "@/components/Icons/FollowingIcon";
export default {
  emits: ["unfollow", "acceptReq", "rejectReq"],
  components:{FollowerIcon,FollowingIcon},
  props: {
    followList: {
      type: Array,
      required: false,
      default: null,
    },
    isARequestCard: {
      type: Boolean,
      default: false,
    },
    is_follower: {
      type: Boolean,
      default: false,
    }
  },
  methods: {
    async goToUserProfile(user) {
      try {
        this.$router.push({path: `/user/${user.userName}/posts`});
      } catch (e) {
        console.log(e)
      }
    },
    rejectFollowReq(reqId) {
      this.$emit("rejectReq", reqId);
    },
    acceptFollowReq(reqId) {
      this.$emit("acceptReq", reqId);
    },
    unfollowUser(user) {
      this.$emit("unfollow", user);
    },

    userAvatar(user) {
      if (user.userInfo.selfieFileData) {
        return `https://banooclubapi.oregonserver.com/${user.userInfo.selfieFileData}`;
      } else {
        return "/defaultUser.png";
      }
    },
    userBanner(user) {
      if (user.userInfo.bannerFileData) {
        return `https://banooclubapi.oregonserver.com/${user.userInfo.bannerFileData}`;
      } else {
        return "/defaultUser.png";
      }
    },
    requestUserBanner(user){
      if (user.followerInfo.bannerFileData) {
        return `https://banooclubapi.oregonserver.com/${user.followerInfo.bannerFileData}`;
      } else {
        return "/defaultUser.png";
      }
    },
    requestUserAvatar(user) {
      if (user.followerInfo.selfieFileData) {
        return `https://banooclubapi.oregonserver.com/${user.followerInfo.selfieFileData}`;
      } else {
        return "/defaultUser.png";
      }
    },

  },
};
</script>

<style scoped>
.custom-image {
  border-radius: 50%;
  width: 60px;
  height: 60px;
  /*min-height: 60px;*/
  /*max-height: 60px;*/
  /*min-width: 60px;*/
  /*max-width: 60px;*/
}

a {
  text-decoration: none;
  color: none;
}
.friendName {
  font-size: 16px;
  text-transform: capitalize;
  /*margin-bottom: 0.5rem;*/
  font-family: inherit;
  font-weight: 500;
  line-height: 1.2;
  color: inherit;
}


</style>
