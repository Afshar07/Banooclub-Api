<template>
  <div class="container mcontainer">
    <h1 class="tw-text-2xl tw-font-semibold">خدمات من</h1>
    <div class="row py-3">
      <div class="d-flex flex-row flex-wrap">
        <ProductItem class="mx-1 my-3" v-for="service in my_services" :service_details="service" :show_buttons="true"/>
      </div>
    </div>
  </div>



</template>

<script>
import ProductItem from "../../components/Products/ProductItem";

export default {
  name: "MyServices",
  layout: "PoshtebamPlusLayout",
  components: {
    ProductItem,
  },
  head(){
    return{
      title: 'خدمات من'
    }
  },
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
@media (min-width: 1024px) {
  .mcontainer {
    max-width: 1000px;
    padding: 30px 0px;
    margin-left: auto;
    margin-right: auto;
  }
}
@media (max-width: 1024px) {
  .mcontainer {
    max-width: 1000px;
    padding: 25px 0px;
    margin-left: auto;
    margin-right: auto;
  }
}
</style>
