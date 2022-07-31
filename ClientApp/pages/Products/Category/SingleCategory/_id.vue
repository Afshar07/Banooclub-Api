<template>
  <div class=" mcontainer tw-overflow-x-hidden">
    <div class="row">
      <div class="col-12">
        <input
          class="searchFriend"
          type="text"
          placeholder="جست‌وجو خدمت"
          v-model="searchKey"
        />
        <div class="row">
          <div v-if=" filterServices.length>0" class="col-xl-3 col-sm-12 col-md-4" v-for="service in filterServices">
            <ProductItem class="my-3" :service_details="service" :show_buttons="false"/>
          </div>
          <div v-if=" filterServices.length===0"  class="col-xl-3 col-sm-12 col-md-4" v-for="service in AllServices">
            <ProductItem class="my-3" :service_details="service" :show_buttons="false"/>
          </div>
<!--          <CustomPagination v-if="totalPages.length>1" :pages="totalPages" @PageChanged="changePage($event)"/>-->
        </div>
      </div>
    </div>
  </div>


</template>

<script>
import CustomPagination from "../../../../components/utilities/CustomPagination"
import ProductItem from "../../../../components/Products/ProductItem"

export default {
  name: "SingleCategory",
  layout: "PoshtebamPlusLayout",
  components: {
    ProductItem,
    CustomPagination
  },
  // mounted(){
  //   this.filter()
  // },
  data(){
    return{
      AllServices:[],
      pageNumber:0,
      searchKey:'',
      totalPages:[],
      filterServices:[],

    }
  },
  methods:{
    changePage(id){
      this.pageNumber = id
      this.GetServices()
    },
  async  GetServices(){
      try {
        const res = await this.$repositories.getAllServices.getAllServices(
          {
            pageNumber:this.pageNumber,
            count:12,
            searchCommand:this.searchKey,
            status:1
          }
        )
        this.AllServices = res.data.services
        this.filterServices =   res.data.services.filter((element)=>{
          return element.serviceCategoryId === (parseInt(this.$route.params.id))
        })

        this.totalPages = []
        const result = Math.ceil( this.AllServices.length / 12)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
        }
      }
      catch (error){
        console.log(error)
      }

    },
  },
  watch:{
    searchKey(){
        this.GetServices()
    }
  },



  async fetch(){

    try {
      const res = await this.$repositories.getAllServices.getAllServices(
        {
          pageNumber:this.pageNumber,
          count:12,
          searchCommand:this.searchKey,
        }
      )
      this.AllServices = res.data.services
      this.filterServices =   res.data.services.filter((element)=>{
        return element.serviceCategoryId === (parseInt(this.$route.params.id))
      })

      this.totalPages = []
      const result = Math.ceil( this.AllServices.length / 12)
      for (let i = 1; i <= result; i++) {
        this.totalPages.push(i);
      }
    }
    catch (error){
      console.log(error)
    }

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
