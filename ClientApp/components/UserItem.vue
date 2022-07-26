<template>
  <div class="d-flex align-items-center justify-content-between">
    <div class="d-flex align-items-center">
      <img
        v-if="userDetails.PictureUrl"
        :src="BaseUrl + 'media/gallery/profile/'+userDetails.PictureUrl"
        class="userImage"
        alt=""
      />
      <img
        v-else
        src="/defaultUser.png"
        class="userImage"
        alt=""
      />
      <div class="d-flex flex-column mx-4">
        <h5 @click="goToUserProfile(userDetails)" class="mb-0" style="cursor: pointer; font-size: 16px">
          <strong>{{ userDetails.Name + " " + userDetails.FamilyName }}</strong>
        </h5>
        <h6 class="mb-0" style="font-size: 15px">
          <small class="text-primary tw-cursor-pointer" @click="goToUserProfile(userDetails)">{{ userDetails.UserName }}@</small>
        </h6>
      </div>

    </div>
    <div class="d-flex mt-4 mt-md-0 justify-content-center align-items-center mx-2">
      <div class="d-flex flex-column flex-md-row  align-items-center tw-ml-3" >
        <div class="d-flex">
          <div class="px-2">{{userDetails.FollowersCount}}</div>
          <FollowerIcon style="fill: #0d6efd;width: 24px;height: 24px;" />
        </div>
       <div class="d-flex">
         <div class="px-2">{{userDetails.FollowingsCount}}</div>
         <FollowingIcon style="fill: #0d6efd;width: 24px;height: 24px;" />
       </div>
      </div>
      <button v-if="!userDetails.IsFollowing" class="btn btn-primary w-100" @click.stop="FollowReq(userDetails.UserId)">دنبال کردن</button>
      <p v-else class="w-100 mx-3">دنبال شده</p>
    </div>
  </div>

</template>

<script>
import FollowerIcon from "../components/Icons/FollowerIcon"
import FollowingIcon from "../components/Icons/FollowingIcon"
export default {
  name: "UserItem",
  components:{
    FollowerIcon,
    FollowingIcon
  },
  computed: {
    BaseUrl() {
      return process.env.pic;
    },
  },

  props:{
    userDetails:{
      type:Object,
      required: true
    }
  },
  methods:{
    async goToUserProfile(user){
      try {
        this.$router.push({path: `/user/${user.UserName}/posts`});
      }catch (e){
        console.log(e)
      }
    },
    async FollowReq(id) {
      try {
        const response =
          await this.$repositories.createAFollowRequest.createAFollowRequest(
            id
          );
        if (response.data) {
          this.$toast.success("درخواست دوستی شما ارسال شد");
        } else {
          this.$toast.error("عملیات قابل اجرا نیست");
        }
      } catch (error) {
        console.log(error);
      }
    },
    async ChangeRoute(item) {
      console.log(item);
      this.$store.commit("SetSearchedUserId", item.userId);
      // const response = await this.$repositories.getUserIndex.getUserIndex(
      //   item.UserId
      // );
      // this.$store.commit("SetUserData", response.data);
      this.$router.push({
        path: `/user/${item.userId}/posts`,
      });
      this.searchKey = null;
    },

  }
}
</script>

<style scoped>
.userImage{
  object-fit: cover;
  border-radius: 50%;
  width: 60px;
  height: 60px;
}
</style>
