<template>
  <div class="row">
    <input
      class="searchFriend"
      type="text"
      placeholder="جست‌وجو خدمت"
      v-model="searchKey"
    />
    <div class="col-xl-3 col-sm-12 col-md-4" v-for="(service,idx) in allServices" :key="idx">
      <ProductItem class="my-3" :service_details="service" :show_buttons="false"/>
    </div>
    <div class="row mb-3" v-if="allServices && allServices.length === 0">
      <div class="col-12 text-warning fw-bold text-center">
        هیچ خدمتی برای نمایش وجود ندارد
      </div>
    </div>
    <CustomPagination v-if="totalPages.length>1" :pages="totalPages" @PageChanged="changePage($event)"/>

  </div>

</template>

<script>
import ProductItem from "../Products/ProductItem";
import CustomPagination from "../../components/utilities/CustomPagination"


export default {
  name: "AllServicesTabContent",
  components: {
    ProductItem,
    CustomPagination
  },
  mounted(){
    this.getAllServices()
  },
  data(){
    return{
      allServices:null,
      searchKey:'',
      pageNumber:0,
      totalPages:[]

    }
  },
  watch:{
    searchKey(){
      if(this.searchKey.length>=3){

        this.getAllServices()
      }
      else if (this.searchKey.length === 0){

        this.getAllServices()
      }
    }
  },
  methods:{
    async getAllServices(){
      try {
        const services = await this.$repositories.getAllServices.getAllServices(
          {
            pageNumber:this.pageNumber,
            count:12,
            searchCommand:this.searchKey,
            status:1,

          }
        )
        this.allServices = services.data.services
        this.totalPages = []
        const result = Math.ceil( this.allServices.length / 12)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
        }
      }
      catch (error){
        console.log(error)
      }
    },
    changePage(id){
      this.pageNumber = id
      this.$fetch()
    }
  },
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
