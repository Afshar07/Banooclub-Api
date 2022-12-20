<template>
  <div :class="{'loading-skeleton':$fetchState.pending}" class=" mcontainer tw-overflow-x-hidden p-3">
    <div class="row">
      <div class="col-12">
        <input
          class="SearchStyle"
          type="text"
          placeholder="جستجو خدمت"
          v-model="searchKey"
        />
        <div class="row">
          <LazySpinner v-if="$fetchState.pending"></LazySpinner>
          <div v-if=" AllServices.length>0" class="col-xl-3 col-sm-12 col-md-4" v-for="service in AllServices.filter(e=> e.planTypes.includes(parseInt($route.params.FilterId)))">
            <LazyProductItem @GetServices="GetAllServices()" class="my-3" :service_details="service"/>
          </div>
          <div v-else class="tw-flex tw-items-center tw-justify-center">
            <span class="text-secondary">داده ای برای نمایش وجود ندارد</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "FilterId",
  layout:'PoshtebamPlusLayout',
  data(){
    return{
      searchKey:'',
      AllServices:[],
      SelectedPageId:1
    }
  },
  watch:{
    searchKey:function (val){
        this.GetAllServices()
    }
  },
  async fetch(){
    try {
      const res = await this.$repositories.getAllServices.getAllServices(
        {
          pageNumber:this.SelectedPageId,
          count:0,
          searchCommand:this.searchKey,
          status:1,

        }
      )
      this.AllServices = res.data.services
      this.totalPages = []
      const result = Math.ceil( res.data.servicesCount / 1)
      for (let i = 1; i <= result; i++) {
        this.totalPages.push(i);
      }

    }
    catch (error){
      console.log(error)
    }
  },





  methods:{
    async GetAllServices(){
      try {
        const res = await this.$repositories.getAllServices.getAllServices(
          {
            pageNumber:this.SelectedPageId,
            count:0,
            searchCommand:this.Search,
            status:1,

          }
        )
        this.AllServices = res.data.services
        this.totalPages = []
        const result = Math.ceil( res.data.servicesCount / 1)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
        }

      }
      catch (error){
        console.log(error)
      }
    },
  }
}
</script>

<style scoped>

</style>
