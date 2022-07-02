<template>
  <div class="row mt-3">
    <div class="col-md-12 col-lg-7 add_post px-0 custom_padding_for_posts">
      <ProductDetail :service_details="service_details"/>
    </div>
  </div>

</template>

<script>
import ProductDetail from "../../components/Products/PDetails";
export default {
  name: "PreviewProductTab",
  components:{
    ProductDetail,
  },
  props:{
    service_details:{
      type: Object,
      required: true
    }
  }


}
</script>

<style scoped>

</style>
