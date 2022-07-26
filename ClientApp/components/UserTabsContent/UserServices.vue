<template>
  <div class="row py-3">
    <input
      class="searchFriend"
      type="text"
      placeholder="جست‌وجو خدمت"
      v-model="searchKey"
    />
    <div class="row">
      <div class="col-md-12" style="height: 600px;overflow-y: scroll;" @scroll="handleScroll">
        <div class="row">
          <div class="col-md-4 col-lg-3" v-for="(service,idx) in services" :key="idx">
            <ProductItem @updateServiceDetails="updateServiceDetails" class="my-3" :service_details="service" :show_buttons="false"/>
          </div>
          <Spinner v-if="services && services.length !== serviceCounts"/>
          <div class="row mb-3" v-if="!$fetchState.pending && services && services.length === 0">
            <div class="col-12 text-warning fw-bold text-center">
              هیچ خدمتی برای نمایش وجود ندارد
            </div>
          </div>
        </div>
      </div>
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

    }
  },
  async fetch(){
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
  methods:{
    async getPostsForInfiniteScroll(id){
      try {
        const response = await this.$repositories.getUserServices.getUserServices(
          {
            lastId:id,
            count:12
          }
        );
        const newServices = response.data.services;
        newServices.forEach((element) => {
          this.services.push(element)
        });
        // console.log('this.postData',this.postData)
      }catch (error) {
        console.log(error);
      }
    },

    handleScroll(event){

      if (event.target.offsetHeight + event.target.scrollTop >= event.target.scrollHeight) {
        const lastServiceId = Object.values(this.services).pop();
        this.getPostsForInfiniteScroll(lastServiceId.serviceId)
      }

    },


    updateServiceDetails(){
      this.$fetch();
    }
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
