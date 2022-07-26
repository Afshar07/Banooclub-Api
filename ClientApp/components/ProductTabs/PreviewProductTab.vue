<template>
  <div class="row mt-3">
    <div class="col-md-12 col-lg-7 add_post px-0 custom_padding_for_posts">
      <ProductDetail :service_details="service_details"/>
    </div>
    <div class="col-md-12 col-lg-5 add_post px-0 custom_padding_for_posts">
      <Chart/>
    </div>
  </div>

</template>

<script>
import ProductDetail from "../../components/Products/PDetails";
import Chart from "../../components/utilities/Chart"
export default {
  name: "PreviewProductTab",
  components:{
    ProductDetail,
    Chart
  },

  props:{
    service_details:{
      type: Object,
      required: true
    },
  },


}
</script>

<style scoped>

</style>
