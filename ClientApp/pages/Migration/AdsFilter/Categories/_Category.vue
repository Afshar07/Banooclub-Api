<template>
  <div :class="{'loading-skeleton':$fetchState.pending}" class=" mcontainer tw-overflow-x-hidden p-3">
    <div class="row">
      <div class="col-12 ">
        <input
          class="SearchStyle "
          type="text"
          placeholder="جستجو"
          v-model="searchKey"
        />
        <div class="row">
          <LazySpinner v-if="$fetchState.pending"></LazySpinner>
          <div v-if="categories.length>0"  class="col-xl-3 my-3 col-sm-12 my-2 col-md-4" v-for="(category,idx) in categories" :key="idx">
            <LazyadsCategoryItem :category_details="category"/>
          </div>
          <div v-else class="tw-flex tw-items-center tw-justify-center">
            <span class="text-secondary">داده ای برای نمایش وجود ندارد</span>
          </div>
          <div v-if="AllAds.length>0"  class="col-xl-3 my-2  col-sm-12 col-md-4" v-for="item in AllAds">
            <LazyAdItem :AdsDetail="item" />
          </div>
          <div v-else class="tw-flex tw-items-center tw-justify-center">
            <span class="text-secondary">داده ای برای نمایش وجود ندارد</span>
          </div>
          <!--          <CustomPagination v-if="totalPages.length>1"  :pages="totalPages" @PageChanged="changePage($event)"/>-->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Categories",
  layout:'PoshtebamPlusLayout',
  data(){
    return{
      categories:[],
      AllAds:[],
      searchKey:'',
      totalPages:[],
      SelectedPageId:1
    }
  },

  async fetch(){
    if(!this.$route.params.Category){
      await this.GetAllCategories()
    }else{
      await this.GetAllAds()
    }

  },

  watch:{
    searchKey:function (val){

      if(!this.$route.params.Category){

          this.GetAllCategories()

      }else if(this.$route.params.Category){

          this.GetAllAds()

      }

    }
  },

  methods:{
    async GetAllAds(){
      try {
        const res = await this.$repositories.GetAllAds.GetAllAds({
          priceFrom:null,
          priceTo:null,
          title:this.searchKey,
          tag:null,
          city:null,
          state:null,
          firstSearchadsId:null,
          count:10,
          categoryId:parseInt(this.$route.params.Category)
        })
        this.AllAds = res.data.ads
        this.categories = []
        this.totalPages = []
        const result = Math.ceil( res.data.adsCount / 10)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
        }

      }
      catch (error){
        console.log(error)
      }
    },
    async   GetAllCategories(){
      try {
        const allCategories = await this.$repositories.getAllAdsCategory.getAllAdsCategory({
          pageNumber:1,
          count:100,
          search:this.searchKey
        });
        this.AllServices = []
        this.categories = allCategories.data.adsCategories;
      }
      catch (error){
        console.log(error)
      }

    }
  }
}
</script>

<style scoped>

</style>
