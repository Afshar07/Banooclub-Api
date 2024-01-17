<template>
  <div class="row mt-3">
    <div class="col-12 add_post px-3 custom_padding_for_posts">
      <Chart/>
      <ProductDetail :service_details="service_details"/>
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
async fetch(){


},
  data(){
    return{
      ServiceViews:null
    }
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
