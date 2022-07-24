<template>

  <div v-if="loading" class="loading-page d-flex flex-column align-items-center justify-content-center"  >
    <div class="sk-grid justify-content-center ">
      <div class="sk-grid-cube"></div>
      <div class="sk-grid-cube"></div>
      <div class="sk-grid-cube"></div>
      <div class="sk-grid-cube"></div>
      <div class="sk-grid-cube"></div>
      <div class="sk-grid-cube"></div>
      <div class="sk-grid-cube"></div>
      <div class="sk-grid-cube"></div>
      <div class="sk-grid-cube"></div>
    </div>
    <div class="LoadingText d-flex">

      <p class="text-white mb-0 align-self-center">BanooClub.com</p>
    </div>





  </div>




</template>

<script>
export default {
  data(){
    return{
      loading:false
    }
  },

  mounted(){


    this.$nextTick(()=>{
      this.loading = true
      this.$nuxt.$loading.finish() ;
    })




  },
  methods: {
    start() {
      this.loading = true
    },
    finish() {
      this.loading = false
    }
  }
}
</script>

<style>

.LoadingText{
  font-size: 15px;
}
.loading-page {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100vh;
  z-index: 9999999999999999999;
  background: rgba(44,62,80,0.8);

  text-align: center;

  font-size: 50px;
  font-family: sans-serif;
}
p{
  opacity: 1!important;
}
.sk-grid-cube{
  background-color: white !important;

}

.sk-grid{
  width: 70px!important;
  height: 70px!important;
}
</style>
