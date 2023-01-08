<template>
  <div>
    <div v-if="Ads.length>0" class="tw-grid tw-grid-cols-1 md:tw-grid-cols-5 tw-justify-center tw-mx-auto  ">


      <AdItem  class="tw-my-3 tw-mx-auto"  v-for="item in Ads" :AdsDetail="item" />

    </div>
    <div v-else class="tw-flex tw-col-span-5 tw-items-center tw-justify-center" @click="$router.push('/Migration/CreateAdvertise/')">
      <div  class="row my-3" >
        <div class="col-12 text-warning fw-bold text-center tw-cursor-pointer">
          <img src="/ads.jpg" class="tw-w-full tw-h-auto tw-rounded-xl" alt="">
        </div>
      </div>
    </div>
    <CustomPagination  :activePage="activePage" :totalPages="totalPages" @PageChanged="changePage($event)"/>
  </div>

</template>

<script>
import AdItem from "../Ads/AdItem";
import CustomPagination from "@/components/utilities/CustomPagination";

export default {
  name: "AllAdsTabContent",
  data(){
    return{
      searchKey:''
    }
  },
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
