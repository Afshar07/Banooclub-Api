<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class=" mcontainer px-2">
    <div class="tw-w-full bg-white p-3 d-flex align-items-center justify-content-between gap-3  rounded ">
      <div class="d-flex align-items-center gap-2">
        <img src="/girl-icon-khadamat.png" class="tw-w-[7rem] tw-h-20" alt="">
        <div class="d-flex align-items-center flex-column">
          <h1 class="text-purple h6"> خدمات من</h1>
          <strong class="text-pink">My Services</strong>
        </div>
      </div>

<!--      <button v-tooltip="{content:'ثبت خدمت'}" @click="$router.push('/Products/AddProduct')" class="btn AddReplyBtn text-white">-->
<!--        <PlusIcon fill="#ff6f9e" style="width: 30px; height: 30px;"/>-->
<!--      </button>-->
    </div>
    <div class="row">
      <div class="col-md-4 col-lg-3" v-for="(service,idx) in my_services" :key="idx">
        <ProductItem @updateServiceDetails="updateServiceDetails" class="my-3" :service_details="service" :show_buttons="true"/>
      </div>
    </div>
<!--    <div class="row py-3">-->
<!--      <div class="d-flex flex-row flex-wrap">-->
<!--        <ProductItem class="mx-1 my-3" v-for="service in my_services" :service_details="service" :show_buttons="true"/>-->
<!--      </div>-->
<!--    </div>-->
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
  head() {
    return {
      title: 'خدمات من',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'خدمات من',
        },
      ],
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
  },
  methods:{
    updateServiceDetails(){
      this.$fetch();
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
