<template>
  <div class="tw-grid tw-grid-cols-1 md:tw-grid-cols-3 tw-justify-center tw-mx-auto  ">
    <AdItem class="tw-my-3 tw-mx-auto"  v-for="item in Ads" :AdsDetail="item" :show_buttons="false"/>
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
