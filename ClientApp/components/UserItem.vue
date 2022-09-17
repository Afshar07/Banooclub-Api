<template>
  <div class="d-flex flex-column align-items-center justify-content-between">
    <div class="Card-Img tw-w-full tw-h-[10rem] tw-rounded relative" :style="`background-color:${SelectRandomBg}`">
      <img v-if="userDetails.PictureUrl" :src="BaseUrl + 'media/gallery/profile/'+userDetails.PictureUrl"
           class="userImage custom-image tw-absolute -tw-translate-y-1/4  tw-top-1/4  -tw-translate-x-1/2 tw-left-1/2 "
           alt=""/>
      <img v-else src="/defaultUser.png" class="userImage custom-image tw-absolute -tw-translate-y-1/4  tw-top-1/4  -tw-translate-x-1/2 tw-left-1/2" alt=""/>
      <!--      <img alt="User Avatar" :src="BaseUrl + '/media/gallery/banner/' + userDetails.bannerFileData" class="tw-w-full tw-h-full tw-object-cover  "/>-->
    </div>
    <div class="d-flex flex-column align-items-center my-3">
      <h5 @click="goToUserProfile(userDetails)" class="mb-0 tw-cursor-pointer tw-text-[16px]">
        <strong>{{ userDetails.Name + " " + userDetails.FamilyName }}</strong>
      </h5>
      <h6 class="mb-0 tw-text-[15px]">
        <small class="text-primary tw-cursor-pointer" @click="goToUserProfile(userDetails)">{{
            userDetails.UserName
          }}@</small>
      </h6>
    </div>
    <div class="d-flex flex-column gap-3 my-3   justify-content-center align-items-center ">
      <div class="d-flex   align-items-center gap-3">
        <div class="d-flex">
          <div class="px-2">{{ userDetails.FollowersCount }}</div>
          <FollowerIcon style="fill: #0d6efd;width: 24px;height: 24px;"/>
        </div>
        <div class="d-flex">
          <FollowingIcon style="fill: #0d6efd;width: 24px;height: 24px;"/>
          <div class="px-2">{{ userDetails.FollowingsCount }}</div>

        </div>
      </div>
      <button v-if="userDetails.IsFollowing === null && userDetails.Requested === null"
              class=" bg-pink p-2 rounded tw-cursor-pointer text-white w-100"
              @click.stop="FollowReq(userDetails.UserId)">دنبال کردن
      </button>
      <button v-if="userDetails.IsFollowing === null && userDetails.Requested !== null"
              class=" bg-purple p-2  rounded tw-cursor-pointer text-white  w-100">درخواست شده
      </button>
      <button v-if="userDetails.IsFollowing !== null" class=" bg-purple p-2  rounded tw-cursor-pointer text-white  w-100">
        دنبال شده
      </button>
    </div>
  </div>

</template>

<script>
import FollowerIcon from "../components/Icons/FollowerIcon"
import FollowingIcon from "../components/Icons/FollowingIcon"

export default {
  name: "UserItem",
  components: {
    FollowerIcon,
    FollowingIcon
  },
  computed: {
    BaseUrl() {
      return process.env.pic;
    },
    SelectRandomBg(){

      const Id =  Math.round(Math.random() * (5 - 1) + 1);
     const Bg =  this.RandomBg.filter(e=> e.bgId === Id)
      console.log(Bg[0].bg)
      return Bg[0].bg


    },
  },

  props: {
    userDetails: {
      type: Object,
      required: true
    }
  },
  data(){
    return{
    RandomBg:[
      {
        bgId:1,
        bg:'#5a5af0'
      },
      {
        bgId:2,
        bg:'#89ffde'
      },
      {
        bgId:3,
        bg:'#44ecf9'
      },
      {
        bgId:4,
        bg:'#d6baff'
      },
      {
        bgId:5,
        bg:'#fb8dbc'
      },
    ]
    }
  },
  methods: {


    async goToUserProfile(user) {
      try {
        this.$router.push({path: `/user/${user.UserName}/posts`});
      } catch (e) {
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
        this.$nuxt.refresh()
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
.userImage {
  object-fit: cover;
  border-radius: 50%;
  width: 60px;
  height: 60px;
}

</style>
