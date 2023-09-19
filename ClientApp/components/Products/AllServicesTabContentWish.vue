<template>
    <div class="tw-grid lg:tw-grid-cols-4 tw-gap-3 md:tw-grid-cols-3 tw-grid-cols-2 ">
      <ProductItemWish class="my-3 lg:tw-col-span-2  md:tw-col-span-1 tw-col-span-1" v-for="(service,idx) in services" :key="idx" :service_details="service" :show_buttons="false"/>
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
