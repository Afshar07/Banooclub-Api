<template>
  <div class="row py-3" style="height: 600px;overflow-y: scroll;">
    <div class="col-md-12">

    <input
      class="SearchStyle"
      type="text"
      placeholder="جستجو خدمت"
      v-model="searchKey"
    />
    </div>
    <div class="col-md-4 col-lg-3" v-for="(service,idx) in services" :key="idx">
      <ProductItem @GetServices="GetServices" @updateServiceDetails="GetServices" class="my-3" :service_details="service" :show_buttons="false"/>
    </div>
    <Spinner v-if="services && services.length !== serviceCounts"/>
    <div v-if="!$fetchState.pending && services && services.length === 0" @click="$router.push('/Products/AddProduct')" class="col-12 text-warning fw-bold text-center tw-cursor-pointer my-3">
      <img src="/service.jpg" class="tw-w-full tw-h-auto tw-rounded-xl" alt="">
    </div>

  </div>

</template>

<script>
import ProductItem from "../../components/Products/ProductItem"
import CustomPagination from "../../components/utilities/CustomPagination"
import Spinner from "../../components/Spinner"

export default {
  name: "UserServices",
  components: {ProductItem,CustomPagination, Spinner},
  watch:{
    searchKey(){
      if(this.searchKey.length>=3){

        this.$fetch()
      }
      else if (this.searchKey.length === 0){

        this.$fetch()
      }
    }
  },
  data(){
    return{
      services:null,
      pageNumber:0,
      totalPages:[],
      searchKey:'',
      serviceCounts: 0,
      lastId:0

    }
  },
  async fetch(){
    try {

      const services = await this.$repositories.getUserServicesByUserName.getUserServicesByUserName(
        {
          lastId:this.lastId,
          count:12,
          searchCommand:this.searchKey,
          userName:this.$route.params.slug
        }
      )
      this.services = services.data.services
      this.serviceCounts = services.data.servicesCount;

    }
    catch (error){
      console.log(error)
    }
  },
  methods:{
    async GetServices(){
      try {
        const services = await this.$repositories.getUserServicesByUserName.getUserServicesByUserName(
          {
            lastId:0,
            count:12,
            searchCommand:this.searchKey,
            userName:this.$route.params.slug
          }
        )
        this.services = services.data.services
        this.serviceCounts = services.data.servicesCount;

      }
      catch (error){
        console.log(error)
      }
    },
  }
}
</script>

<style scoped>
.searchFriend {
  border: 1px solid #eaeaea;
  color: #575757;
  font-size: 14px;
  padding: 5px 10px;
  width: 100%;
  margin-bottom: 20px;
}
</style>
