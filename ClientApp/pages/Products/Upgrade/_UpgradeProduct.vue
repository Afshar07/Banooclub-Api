<template>
  <div  :class="$fetchState.pending?'loading-skeleton':''" class="container mcontainer tw-h-full">
    <div class="row">
      <h1 class="tw-text-2xl tw-font-semibold">ارتقاء خدمت</h1>
      <ul class="nav nav-pills align-items-end profile_tabs pt-3 nav-fill tw-overflow-x-auto" id="pills-tab" role="tablist">
        <li class="nav-item " role="presentation m-0" style="margin: 0 !important;">
          <button @click="ActiveTab=1" :class="{' active':ActiveTab===1}" class="nav-link " id="products-pills-home-tab" data-bs-toggle="pill"
                  data-bs-target="#products-pills-home" type="button" role="tab" aria-controls="products-pills-home" aria-selected="true">
            ویرایش خدمت
          </button>
        </li>
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button  @click="ActiveTab=2" :class="{' active':ActiveTab===2}" class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">
            ارتقاء خدمت
          </button>
        </li>
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button  @click="ActiveTab=3" class="nav-link" id="pills-contact-tab" data-bs-toggle="pill" data-bs-target="#pills-contact" type="button" role="tab" aria-controls="pills-contact" aria-selected="false">
            پیش نمایش و آمار
          </button>
        </li>

      </ul>
    </div>
    <div class="tab-content tw-h-full" id="pills-tabContent" v-if="!$fetchState.pending">
      <div  class="tab-pane fade  " :class="{'show active':ActiveTab===1}" id="products-pills-home" role="tabpanel" aria-labelledby="products-pills-home-tab">
        <EditProductTab @DiscountChanged="GetService" v-if="ActiveTab===1"  :service_details="ServiceDetails"/>
      </div>
      <div class="tab-pane fade tw-h-full" :class="{'show active':ActiveTab===2}" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
        <UpgradeProductTab v-if="ActiveTab===2"/>
      </div>
      <div class="tab-pane fade" id="pills-contact" :class="{'show active':ActiveTab===3}" role="tabpanel" aria-labelledby="pills-contact-tab">
        <PreviewProductTab v-if="ActiveTab===3" :service_details="ServiceDetails"/>
      </div>
    </div>


  </div>


</template>

<script>
import EditProductTab from "../../../components/ProductTabs/EditProductTab";
import UpgradeProductTab from "../../../components/ProductTabs/UpgradeProductTab";
import PreviewProductTab from "../../../components/ProductTabs/PreviewProductTab";
export default {
  name: "UpgradeProduct",
  components: {PreviewProductTab, UpgradeProductTab, EditProductTab},
  layout: "PoshtebamPlusLayout",
  data(){
    return{
      ServiceDetails:null,
      ActiveTab:1
    }
  },
  head(){
    return{
      title: 'ارتقاء خدمت'
    }
  },
  mounted(){
    if(this.$route.query.active_tab){
      if(this.$route.query.active_tab==='edit'){

        this.ActiveTab = 1
      }else if (this.$route.query.active_tab==='upgrade'){

        this.ActiveTab = 2
      }
    }
  },
  async fetch(){



    try {
      const service_details = await this.$repositories.getAService.getAService(
        {
          servicePackId:this.$route.params.UpgradeProduct
        }
      )
      this.ServiceDetails = service_details.data
    }
    catch (error){
      console.log(error)
    }
  },
  methods:{
    async GetService(){

      try {
        const service_details = await this.$repositories.getAService.getAService(
          {
            servicePackId:this.$route.params.UpgradeProduct
          }
        )
        this.ServiceDetails = service_details.data
        this.$nuxt.refresh()
      }
      catch (error){
        console.log(error)
      }
    }
  }



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
.nav {
  flex-wrap: nowrap !important;
}
li.nav-item{
  white-space: nowrap !important;
}
.nav-link {
  font-weight: 500;
  color: #8c8d90 !important;
  font-weight: 500;
  padding: 5px 9px 15px;
  border-bottom: 3px solid transparent;
}
.nav-pills .nav-link.active, .nav-pills .show>.nav-link {
  color: #0d6efd !important;
  background-color: transparent;
  font-weight: 500;
  border-bottom: 3px solid #0d6efd;
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
