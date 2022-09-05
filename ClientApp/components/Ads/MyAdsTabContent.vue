<template>
  <div>
    <div v-if="Ads.length>0" class="tw-grid tw-grid-cols-1 md:tw-grid-cols-5 tw-justify-center tw-mx-auto  ">
      <div @click="$router.push('/Migration/CreateAdvertise/')" class=" tw-cursor-pointer tw-my-2.5">
        <div class="card h-100 w-100  d-flex align-items-center justify-content-center tw-bg-stone-700">
          <div class="d-flex align-items-center justify-content-center flex-column">
            <i class="fas fa-plus text-secondary"></i>
            <span class="text-secondary">ثبت آگهی</span>
          </div>
        </div>
      </div>
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
