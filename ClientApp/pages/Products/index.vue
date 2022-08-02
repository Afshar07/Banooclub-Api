<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="container bg-white mcontainer px-2">
    <div class="row">
      <h1 class="tw-text-2xl tw-font-semibold">خدمات</h1>
      <ul class="nav nav-pills align-items-end profile_tabs py-3" id="pills-tab" role="tablist">
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button  class="nav-link active" id="products-pills-home-tab" data-bs-toggle="pill"
                  data-bs-target="#products-pills-home" type="button" role="tab" aria-controls="products-pills-home" aria-selected="true">
            همه خدمات
          </button>


        </li>
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button @click="filter()" class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">
            پیشنهادات
          </button>
        </li>
      </ul>
    </div>
    <div class="tab-content" id="pills-tabContent">
      <div class="tab-pane fade show active" id="products-pills-home" role="tabpanel" aria-labelledby="products-pills-home-tab">
        <div class="row boxMainContent mx-auto">
          <div class="col-12 text-center ">
            <AllServicesTabContent/>
          </div>
        </div>
      </div>
      <div class="tab-pane fade " id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
        <FirstTabContent :best="best" :Services="AllServices" :newest="newest" :categories="categories" :suggestion="suggestion" />
      </div>
    </div>


  </div>


</template>

<script>
import ProductItem from "../../components/Products/ProductItem";
import ChevronRightIcon from "../../components/Icons/RightChevronIcon";
import ChevronLeftIcon from "../../components/Icons/LeftChevronIcon";
import RightChevronIcon from "../../components/Icons/RightChevronIcon";
import LeftChevronIcon from "../../components/Icons/LeftChevronIcon";
import FirstTabContent from "../../components/Products/FirstTabContent";


import AllServicesTabContent from "../../components/Products/AllServicesTabContent";
export default {
  name: "index",
  layout: "PoshtebamPlusLayout",
  components:{
    AllServicesTabContent,
    FirstTabContent,
    LeftChevronIcon,
    RightChevronIcon,
    ChevronLeftIcon,
    ChevronRightIcon,
    ProductItem,
  },
  head() {
    return {
      title: 'همه خدمات',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'همه خدمات',
        },
      ],
    }
  },
  data(){
    return{
      categories:[],
      AllServices:[],
      best:[],
      newest:[],
      suggestion:[],
      hasBest:false,
      hasNewest:false
    }
  },
  methods:{
    filter(){
      this.best=[]
      this.newest=[]
      this.suggestion=[]
        this.AllServices.forEach(el=>{
          if(el.planTypes.includes(5)){
            this.best.push(el)
          }
        })
        this.AllServices.forEach(el=>{
          if(el.planTypes.includes(4)){
            this.newest.push(el)
          }
        })
      this.AllServices.forEach(el=>{
        if(el.planTypes.includes(2)){
          this.suggestion.push(el)
        }
      })
    }



  },
  async fetch(){
    try {
      const allCategories = await this.$repositories.getAllServicesCategory.getAllServicesCategory();
      this.categories = allCategories.data.serviceCategories;
    }
    catch (error){
      console.log(error)
    }
    try {
      const res = await this.$repositories.getAllServices.getAllServices(
        {
          pageNumber:0,
          count:0,
          searchCommand:'',
          status:1,

        }
      )
      this.AllServices = res.data.services
    }
    catch (error){
      console.log(error)
    }

  },
}
</script>


<style>
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
/*@media (max-width: 375px) {*/
/*  .vueperslide.vueperslide--active.vueperslide--visible{*/
/*    margin-left: 3% !important;*/
/*  }*/
/*  .vueperslide.vueperslide--visible{*/
/*    margin-left: 3% !important;*/
/*  }*/
/*  .vueperslide{*/
/*    margin-left: 3% !important;*/
/*  }*/
/*}*/
.nav-pills .nav-link {
  border-bottom: 3px solid transparent ;
}
.nav-link {
  font-weight: 500;
  color: #8c8d90 !important;
  font-weight: 500;
  padding: 5px 9px 15px;
}
.nav-pills .nav-link.active, .nav-pills .show>.nav-link {
  color: #0d6efd !important;
  background-color: transparent;
  font-weight: 500;
  border-bottom: 3px solid #0d6efd !important;
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

</style>
