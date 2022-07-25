<template>
  <div class=" mcontainer tw-overflow-x-hidden">
    <div class="row">
      <div class="col-12">
        <input
          class="searchFriend "
          type="text"
          placeholder="جست‌وجو دسته بندی"
          v-model="searchKey"
        />
        <div class="row">
          <div class="row" v-if="!$fetchState.pending && categories && categories.length === 0">
            <div class="col-12 text-warning fw-bold text-center">
              هیچ دسته بندی برای نمایش وجود ندارد
            </div>
          </div>
          <div v-else class="col-xl-3 col-sm-12 col-md-4" v-for="(category,idx) in categories" :key="idx">
            <CategoryItem class="m-2" :category_details="category"/>
          </div>
<!--          <CustomPagination v-if="totalPages.length>1"  :pages="totalPages" @PageChanged="changePage($event)"/>-->
        </div>
      </div>
    </div>
  </div>


</template>

<script>
import CustomPagination from "../../../components/utilities/CustomPagination"
import CategoryItem from "../../../components/Categories/CategoryItem"
export default {
  name: "index",
  layout: "PoshtebamPlusLayout",
  components:{
    CategoryItem,
    CustomPagination
  },
  async fetch(){
    try {
      const allCategories = await this.$repositories.getAllServicesCategory.getAllServicesCategory({
        pageNumber:this.pageNumber,
        count:12,
        searchCommand:this.searchKey,
      });
      this.categories = allCategories.data.serviceCategories;
      this.totalPages = []
      const result = Math.ceil( allCategories.data.serviceCategoriesCount / 12)
      for (let i = 1; i <= result; i++) {
        this.totalPages.push(i);
      }
    }
    catch (error){
      console.log(error)
    }
  },

  data(){
    return{
      categories:[],
      searchKey:'',
      totalPages:[],
      pageNumber:1,
      allServices:[]
    }
  },
  watch:{
    searchKey(){
        this.getAllCats()
    }
  },

  methods:{
    async getAllServices(){
      try {
        const services = await this.$repositories.getAllServices.getAllServices(
          {
            pageNumber:0,
            count:0,
            searchCommand:'',
            status:1,
          }
        )
        this.allServices = services.data.services
      }
      catch (error){
        console.log(error)
      }
    },

    async getAllCats(){
      try {
        const allCategories = await this.$repositories.getAllServicesCategory.getAllServicesCategory(
          {
            pageNumber:this.pageNumber,
            count:12,
            searchCommand:this.searchKey,
          }
        )
        this.categories = allCategories.data.serviceCategories;
        this.totalPages = []
        const result = Math.ceil( allCategories.data.serviceCategoriesCount / 12)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
          console.log(this.totalPages)
        }
      }
      catch (error){
        console.log(error)
      }
    },
    filterServices(id){
      this.allServices.filter(el=>{

      })


    },

    changePage(id){
      this.pageNumber = id
      this.getAllCats()
    },
  },



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
.searchFriend {
  border: 1px solid #eaeaea;
  color: #575757;
  font-size: 14px;
  padding: 5px 10px;
  width: 100%;
  margin-bottom: 20px;
}
</style>
