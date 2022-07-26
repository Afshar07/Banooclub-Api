<template>
  <div>
    <div class="row" v-if="!$fetchState.pending && my_services.length === 0">
      <div class="col-12 text-warning fw-bold text-center">
        هیچ محتوای برای نمایش وجود ندارد
      </div>
    </div>
    <div class="row boxMainContent mx-auto" v-else>
      <div class="col-lg-3" v-for="(service,idx) in my_services" :key="idx">
        <ProductItem style="width: 220px !important;" class="my-3" :service_details="service" :show_buttons="true"/>
      </div>
    </div>
  </div>
</template>

<script>
import ProductItem from "./ProductItem";
export default {
  name: "MyServicesTabContents",
  components:{ProductItem},
  data(){
    return{
      my_services:null,
    }
  },
  async fetch(){
    try {
      const services = await this.$repositories.getMyServices.getMyServices({pageNumber:0, count:0, searchCommand:null})
      this.my_services = services.data.services
    }
    catch (error){
      console.log(error)
    }
  }
}
</script>

<style scoped>
.friendBox:hover {
  box-shadow: 0 8px 8px #e1e8ec;
  border-color: transparent;
}

</style>
