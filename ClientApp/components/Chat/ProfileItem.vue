<template>
  <div class="d-flex align-items-center profile_item p-2 " style="cursor: pointer!important;">
    <img v-if="UserData.userPhoto!==null" style="width: 50px; height: 50px;" class="rounded-circle" :src="`https://banooclubapi.simagar.com/${UserData.userPhoto}`"/>
    <img v-else style="width: 50px; height: 50px;" class="rounded-circle" src="~/assets/images/defaultUser.png"/>
    <div class="d-flex justify-content-between w-100 px-3">
      <div class="d-flex flex-column">
        <div style="color: #333;font-size: 15px;font-weight: 600;" >{{ UserData.userName }}</div>
        <div style="color: #888;font-size: 14px;" clas="=ShortTitle">{{ UserData.subject }}</div>
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
  props:['UserData'],
  name: "ProfileItem",
  methods:{
    async goToUserProfile(user){
      try {
        this.$router.push({path: `/user/${user.userName}/posts`});
      }catch (e){
        console.log(e)
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
