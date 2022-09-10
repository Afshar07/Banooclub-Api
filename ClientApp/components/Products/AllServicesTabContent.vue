<template>
  <div class="row">
    <div class="col-md-12">
      <div class="row">
        <div class="col-md-7 d-flex align-items-center ">
          <input
            class="SearchStyle"
            type="text"
            placeholder="جستجو خدمت"
            v-model="searchKey"
          />
        </div>
        <div class="col-md-5">
          <v-select
            v-model="SelectedCategoryId"
            :options="Categories"
            placeholder="جستجو بر اساس دسته بندی"
            label="title"
            :reduce="title=> title.serviceCategoryId"
          />
        </div>

      </div>
    </div>

    <div class="col-xl-3 col-sm-12 col-md-4" v-for="(service,idx) in AllServices" :key="idx">
      <ProductItem @GetServices="Refresh()" class="my-3" :service_details="service" />
    </div>
    <div class="row mb-3" v-if="AllServices && AllServices.length === 0">
      <div class="col-12 text-warning fw-bold text-center">
        هیچ خدمتی برای نمایش وجود ندارد
      </div>
    </div>
    <CustomPagination v-if="totalpages.length>1" :activePage="SelectedPageId" :totalPages="totalpages" @PageChanged="changePage($event)"/>
  </div>

</template>

<script>
import ProductItem from "../Products/ProductItem";
import CustomPagination from "../../components/utilities/CustomPagination"


export default {
  props:['AllServices','totalpages','SelectedPageId','Categories'],
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
      SelectedCategoryId:null,
      totalPages:[]

    }
  },
  watch:{
    searchKey:function (val){

      this.$emit('SearchCommandFired',val)
    },
    SelectedCategoryId:function (val){
      this.$emit('CategoryChanged',val)
    },
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
