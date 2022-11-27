<template>
  <div class="widget custom_card p-3">
    <h4 class="tw-text-lg tw-font-semibold"> دوستان </h4>
    <p class="tw-text-sm">{{ $auth.user.baseData.followersCount }} نفر</p>
    <div class="tw-grid tw-grid-cols-3 tw-gap-3 tw-text-gray-600 tw-font-semibold">
      <nuxt-link class="text-decoration-none" v-for="(friend,idx) in followingList" :key="idx" to="#">
        <div class="d-flex flex-column justify-content-center align-items-center">
          <img style="width: 89px; height: 96px;" :src="`https://subapi.banoclub.com/${friend.userInfo.selfieFileData}`" alt=""
               class="tw-w-full tw-h-full tw-object-cover tw-rounded-md">
          <div style="color: #4b5563" class="tw-text-sm tw-truncate mt-2 text-primary tw-cursor-pointer" @click="goToUserProfile(friend.userInfo)"> {{ friend.userInfo.name + ' ' + friend.userInfo.familyName }}</div>
        </div>
      </nuxt-link>
    </div>
    <button style="background-color: #f0f2f5;color: #666;"  class="tw-rounded-lg tw-mt-3 tw-w-full py-2"> دیدن همه </button>
  </div>

</template>

<script>
export default {
  name: "MyFriends",
  data(){
    return{
      followerList:[],
      followingList:[],
      followRequestList:[]
    }
  },
  async mounted(){
    await this.getFriends()
  },
  methods:{
    async goToUserProfile(user){
      try {
        this.$router.push({path: `/user/${user.userName}/posts`});
      }catch (e){
        console.log(e)
      }
    },
    async getFriends(){
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

  }

}
</script>

<style scoped>

</style>
