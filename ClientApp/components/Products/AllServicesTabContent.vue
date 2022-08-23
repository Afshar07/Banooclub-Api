<template>
  <div class="row">
    <input
      class="SearchStyle"
      type="text"
      placeholder="جستجو خدمت"
      v-model="searchKey"
    />
    <div class="col-xl-3 col-sm-12 col-md-4" v-for="(service,idx) in AllServices" :key="idx">
      <ProductItem @GetServices="Refresh()" class="my-3" :service_details="service" />
    </div>
    <div class="row mb-3" v-if="AllServices && AllServices.length === 0">
      <div class="col-12 text-warning fw-bold text-center">
        هیچ خدمتی برای نمایش وجود ندارد
      </div>
    </div>

    <CustomPagination v-if="totalpages.length>1" :activePage="1" :totalPages="totalpages" @PageChanged="changePage($event)"/>

  </div>

</template>

<script>
import ProductItem from "../Products/ProductItem";
import CustomPagination from "../../components/utilities/CustomPagination"


export default {
  props:['AllServices','totalpages'],
  name: "AllServicesTabContent",
  components: {
    ProductItem,
    CustomPagination
  },
  mounted(){

  },
  data(){
    return{
      allServices:null,
      searchKey:'',
      pageNumber:0,
      totalPages:[]

    }
  },
  watch:{
    searchKey:function (val){

      this.$emit('SearchCommandFired',val)
    }
  },
  methods: {
    Refresh(){
      this.$emit('RefetchServices')
    },
    changePage(id){
      this.$emit('PageChanged',id)
    }
  }
}
</script>

<style scoped>
.searchFriend {
  border: 1px solid #eaeaea;
  color: #575757;
  font-size: 14px;
  padding: 5px 10px;
  width: 100%;
  margin-bottom: 20px;
}
</style>
