<template>
  <div class="container-fluid">
    <div class="row bg-white">
      <div class="col-md-12 p-5">
        <div class="row p-3 SingleFollow" style="cursor: pointer" v-for="item in Following" :key="item.userId" @click="goToUserProfile(item.userId)">

          <div class="col-10  d-flex justify-content-center flex-column align-items-end">
            <strong>{{   item.name + ' ' + item.familyName}}</strong>
            <small class="text-muted">{{ item.userName }}</small>
          </div>
          <div class="col-2  d-flex justify-content-center">
            <img v-if="item" :src="BaseUrl + item.selfieFileData" width="50px" height="50px" class=" rounded-circle" alt="">
          </div>

        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  layout:'PoshtebamPlusLayout',
  name: "UserFollowings",
  data(){
    return{
      Following:[]

    }
  },
  mounted(){
    this.getFollowing()
  },

  computed:{
    BaseUrl(){
      return  process.env.pic
    }
  },
  methods:{

    goToUserProfile(userId) {
      this.$router.push({path: '/Social/AccountSetting/Posts', query: {userId:userId}})
      this.$store.commit('SetSearchedUserId',userId)
      this.$axios.post(`Common/GetUserIndex?userId=${userId}`,null,{
       
      }).then((res)=>{

        this.$store.commit('SetUserData',res.data)
      })

      this.SearchUsers =''

    },

    getFollowing() {
      this.$axios.post(`Following/GetByUserId?userId=${this.$route.query.userId}`, null, ).then((response) => {
        if (response.status === 200) {

          response.data.forEach((item)=>{
            this.Following.push(item.userInfo)
          })


        }
      }).catch((error) => {
        console.log(error)
      })
    },

  }
}
</script>

<style >
.SingleFollow{
  transition: .2s ease!important;

}

.SingleFollow:hover{
  box-shadow: 0 2px 4px rgb(0 0 0 / 40%);
  transform: scale(1.01);
  transition: .2s ease;

}

</style>
