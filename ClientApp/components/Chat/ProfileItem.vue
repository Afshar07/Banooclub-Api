<template>
  <div class="d-flex align-items-center profile_item p-2 " style="cursor: pointer!important;">
    <img v-if="UserData.userPhoto!==null && UserData.userId!==0" style="width: 50px; height: 50px;" class="rounded-circle" :src="`https://subapi.banoclub.com/media/gallery/profile/${UserData.userPhoto}`"/>
    <img v-else-if="UserData.userPhoto!==null && UserData.groupId!==0" style="width: 50px; height: 50px;" class="rounded-circle" :src="`https://subapi.banoclub.com/media/gallery/group/${UserData.userPhoto}`"/>
    <img v-else style="width: 50px; height: 50px;" class="rounded-circle" src="~/assets/images/defaultUser.png"/>
    <div class="d-flex justify-content-between w-100 px-3">
      <div class="d-flex flex-column">
        <div class="tw-text-[#333] tw-text-[15px] tw-font-bold tw-cursor-pointer"  @click="goToUserProfile" >{{ UserData.fullName }}</div>
        <div style="color: #888;font-size: 14px;" class="=ShortDescriptionIndex">{{ UserData.subject }}</div>
      </div>
      <div class="d-flex flex-column justify-content-center align-items-center">
        <div v-if="UserData.unReadCount!==0" class="d-flex align-items-center justify-content-center  rounded-circle tw-bg-blue-600" style="width: 20px;height: 20px">
          <small class="text-white">{{UserData.unReadCount}}</small>
        </div>
        <small style="color: #888;">{{ new Date(UserData.createDate).toLocaleTimeString('fa-IR') }}</small>

      </div>

    </div>


  </div>
</template>

<script>
export default {
  props:{
    UserData:{
      // Object That Handles Both Group And User Data In SideBar
      type:Object,
      required:true
    }
  },
  name: "ProfileItem",
  methods:{
    // Method That Routes To User Profile Page
    async goToUserProfile(user) {
      if(user.groupId===0){
        try {
          this.$router.push({path: `/user/${user.userName}/posts`});
        } catch (e) {
          console.log(e)
        }
      }

    },
  }
}
</script>

<style scoped>


.ShortTitle{
  display: -webkit-box;
  -webkit-line-clamp: 1;
  -webkit-box-orient: vertical;
  overflow: hidden;
}
</style>
