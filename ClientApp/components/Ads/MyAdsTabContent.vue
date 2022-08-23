<template>
  <div>
    <div v-if="Ads.length>0" class="tw-grid tw-grid-cols-1 md:tw-grid-cols-5 tw-justify-center tw-mx-auto  ">
      <AdItem  class="tw-my-3 tw-mx-auto"  v-for="item in Ads" :AdsDetail="item" />

    </div>
    <div v-else class="tw-flex tw-items-center tw-justify-center">
      <span class="text-secondary">داده ای برای نمایش وجود ندارد</span>
    </div>
    <CustomPagination  :activePage="activePage" :totalPages="totalPages" @PageChanged="changePage($event)"/>
  </div>

</template>

<script>
import AdItem from "../Ads/AdItem";
import CustomPagination from "@/components/utilities/CustomPagination";

export default {
  name: "AllAdsTabContent",
  methods:{
    changePage(id){
    this.$emit('PageChanged',id)
    }
  },
  components: {
    AdItem,
    CustomPagination
  },

  props:{
    activePage:{
      type:Number,
      required:false
    },
    totalPages:{
      type:Array,
      required: false
    },
    Ads:{
      type:Array,
      required: true
    },
  }

}
</script>

<style scoped>

</style>
