<template>
  <div class=" tw-grid tw-grid-cols-2 md:tw-grid-cols-2 tw-gap-3   lg:tw-grid-cols-3    ">
    <AdItem v-for="item in Ads" :AdsDetail="item" class="tw-my-3  tw-col-span-1 md:tw-col-span-1   lg:tw-col-span-1"/>
  </div>
</template>

<script>
import AdItem from "../Ads/AdItem";

export default {
  name: "AllAdsTabContent",
  methods: {
    changePage() {

    }
  },
  components: {
    AdItem,
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
