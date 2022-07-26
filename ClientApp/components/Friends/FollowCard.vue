<template>
  <div class="row" v-if="followList">
    <div class="col-md-6" v-for="(item, index) in followList" :key="index">
      <div class="friendBox">
        <div @click="profileLinkGenerator(item)" style="cursor: pointer">
          <div class="d-flex justify-content-around align-items-center flex-column flex-md-row">
            <div>
              <img
                alt="User Avatar"
                :src="isARequestCard ? requestUserAvatar(item) : userAvatar(item)"
                class=" custom-image"
              />
            </div>
            <div class="d-flex flex-column py-3 friendName">
              <p style="font-weight: 600;color: #666666;font-size: 1rem; margin-bottom: 0 !important;" class="text-primary tw-cursor-pointer" @click="goToUserProfile(item.userInfo)">
                {{
                  isARequestCard ? item.followerInfo.name : item.userInfo.name
                }}

                {{
                  isARequestCard
                    ? item.followerInfo.familyName
                    : item.userInfo.familyName
                }}
              </p>
              <div class="friendCaption" >
                <p class="mb-0" v-if="!isARequestCard && item.userInfo.bio" style="margin-bottom: 0 !important;">
                  {{ item.userInfo.bio }}
                </p>
              </div>
            </div>
            <div class="friendCaption" >
              <p class="mb-0" v-if="!isARequestCard && item.userInfo.followingsCount" style="margin-bottom: 0 !important;">
                {{ item.userInfo.followingsCount }}
                دنبال کننده
              </p>
              <p class="mb-0" v-if="!isARequestCard && item.userInfo.followingsCount" style="margin-bottom: 0 !important;">
                {{ item.userInfo.followersCount }}
                دنبال شونده
              </p>
            </div>
            <div
              class="d-flex align-items-center justify-content-center my-auto"
            >
              <button
                v-if="!isARequestCard"
                type="button"
                style="border-radius: .375rem"
                class="btn btn-danger tw-w-full"
                @click.stop="unfollowUser(item)"
              >
                حذف
              </button>
              <div class="w-100 d-flex flex-column" v-if="isARequestCard">
                <button
                  type="button"
                  class="btn btn-success w-100 my-1"
                  @click.stop="acceptFollowReq(item.followRequestId)"
                >
                  قبول درخواست
                </button>
                <button
                  type="button"
                  class="btn btn-danger w-100 my-1"
                  @click.stop="rejectFollowReq(item.followRequestId)"
                >
                  رد درخواست
                </button>
              </div>
            </div>
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
export default {
  emits: ["unfollow", "acceptReq", "rejectReq"],
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
    is_follower:{
      type: Boolean,
      default: false,
    }
  },
  methods: {
    async goToUserProfile(user){
      try {
        this.$router.push({path: `/user/${user.userName}/posts`});
      }catch (e){
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
        return `https://banooclubapi.simagar.com/media/gallery/profile/${user.userInfo.selfieFileData}`;
      } else {
        return "/defaultUser.png";
      }
    },
    requestUserAvatar(user) {
      if (user.followerInfo.selfieFileData) {
        return `https://banooclubapi.simagar.com/media/gallery/profile/${user.followerInfo.selfieFileData}`;
      } else {
        return "/defaultUser.png";
      }
    },
    profileLinkGenerator(user) {

      // TODO: Modify this to work with all cards
      if(this.is_follower){
        this.$router.push({ path: `/user/${user.followerUserId}/posts` });
      }
      else {
        this.$router.push({ path: `/user/${user.userInfo.userId}/posts` });

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
@media only screen and (max-width: 768px) {
  .friendBox {
    background: #fff none repeat scroll 0 0;
    border: 1px solid #eaf1f6;
    padding: 10px;
    /*width: 100%;*/
    transition: all 0.15s linear 0s;
    margin-bottom: 0;
    margin-top: 35px;
    border-radius: 8px;
  }
}
@media only screen and (min-width: 769px) {
  .friendBox {
    background: #fff none repeat scroll 0 0;
    border: 1px solid #eaf1f6;
    padding: 10px;
    /*width: 100%;*/
    transition: all 0.15s linear 0s;
    margin-bottom: 0;
    /*margin-top: 35px;*/
    border-radius: 8px;

  }
}

.friendBox:hover {
  box-shadow: 0 8px 8px #e1e8ec;
  border-color: transparent;
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

.friendCaption {
  color: #088dcd;
  font-size: 13px;
  text-transform: capitalize;
}
@media (max-width: 576px) {
  .friendBox {
    padding: 20px;
  }

  .friendName {
    font-size: 16px;
    margin-bottom: 0.25rem;
    line-height: 1.2;
  }

  .buttonList {
    padding: 10px;
    font-size: 14px;
  }
}
</style>
