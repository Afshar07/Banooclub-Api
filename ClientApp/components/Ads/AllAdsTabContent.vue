<template>
  <div class="row">
    <div class="d-flex flex-row flex-wrap">
      <AdItem class="tw-ml-1 my-3" v-for="item in Ads" :AdsDetail="item" :show_buttons="false"/>
    </div>
  </div>

</template>

<script>
import AdItem from "../Ads/AdItem";


export default {
  name: "AllServicesTabContent",
  components: {
    AdItem,
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
