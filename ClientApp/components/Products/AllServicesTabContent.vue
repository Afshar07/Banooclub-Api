<template>
  <div class="row">
    <div class="d-flex flex-row flex-wrap">
      <ProductItem class="mx-1 my-3" v-for="service in services" :service_details="service" :category_details="categories"/>
    </div>
  </div>

</template>

<script>
import ProductItem from "../Products/ProductItem";


export default {
  name: "AllServicesTabContent",
  components: {
    ProductItem,
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
