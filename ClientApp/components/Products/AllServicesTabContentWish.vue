<template>
  <div class="row">
    <div class="d-flex flex-row flex-wrap">

      <ProductItemWish class="tw-ml-1 my-3" v-for="service in services" :service_details="service" :show_buttons="false"/>
    </div>
  </div>
</template>

<script>
import ProductItemWish from "../Products/ProductItemWish";


export default {
  name: "AllServicesTabContent",
  components: {
    ProductItemWish,
  },

  props:{
    services:{
      type:Object,
      required: true
    },
    categories:{
      type: Array,
      required: true
    }
  }

}
</script>


<style scoped>

</style>
