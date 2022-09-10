<template>

    <div class="tw-grid lg:tw-grid-cols-4 md:tw-grid-cols-3 tw-grid-cols-1 ">
      <AdItemWish class="my-3 mx-auto" v-for="item in Ads" :AdsDetail="item" :show_buttons="false"/>
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
