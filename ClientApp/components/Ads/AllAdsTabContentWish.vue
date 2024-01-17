<template>

  <div class="tw-grid lg:tw-grid-cols-4 tw-gap-3 md:tw-grid-cols-3 tw-grid-cols-2 ">
    <AdItemWish v-for="item in Ads" :AdsDetail="item" :show_buttons="false"
                class="my-3 lg:tw-col-span-2  md:tw-col-span-1 tw-col-span-1"/>
  </div>


</template>

<script>
import AdItemWish from "../Ads/AdItemWish";

export default {
  name: "AllServicesTabContent",
  components: {
    AdItemWish,
  },

  props: {
    Ads: {
      type: Array,
      required: true
    },
  }

}
</script>

<style scoped>

</style>
