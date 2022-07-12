<template>
  <div class="row">
    <div class="d-flex flex-row flex-wrap">
      <AdItemWish class="tw-ml-1 my-3" v-for="item in Ads" :AdsDetail="item" :show_buttons="false"/>
    </div>
  </div>

</template>

<script>
import AdItemWish from "../Ads/AdItemWish";


export default {
  name: "AllServicesTabContent",
  components: {
    AdItemWish,
  },

  props:{
    Ads:{
      type:Array,
      required: true
    },
  }

}
</script>

<style scoped>

</style>
