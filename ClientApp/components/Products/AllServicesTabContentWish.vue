<template>
  <div class="row">
    <div class="d-flex flex-row gap-1 flex-wrap">

      <ProductItemWish class="my-3" v-for="(service,idx) in services" :key="idx" :service_details="service" :show_buttons="false"/>
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
      type:Array,
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
