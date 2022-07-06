<template>
  <div class="container mcontainer tw-h-full">
    <div class="row">
      <h1 class="tw-text-2xl tw-font-semibold">ارتقاء خدمت</h1>
      <ul class="nav nav-pills align-items-end profile_tabs pt-3 nav-fill tw-overflow-x-scroll" id="pills-tab" role="tablist">
        <li class="nav-item " role="presentation m-0" style="margin: 0 !important;">
          <button :class="[$route.query.active_tab==='edit'?'active':'']" class="nav-link" id="products-pills-home-tab" data-bs-toggle="pill"
                  data-bs-target="#products-pills-home" type="button" role="tab" aria-controls="products-pills-home" aria-selected="true">
            ویرایش خدمت
          </button>
        </li>
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button :class="[$route.query.active_tab==='upgrade'?'active':'']" class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">
            ارتقاء خدمت
          </button>
        </li>
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link" id="pills-contact-tab" data-bs-toggle="pill" data-bs-target="#pills-contact" type="button" role="tab" aria-controls="pills-contact" aria-selected="false">
            پیش نمایش و آمار
          </button>
        </li>

      </ul>
    </div>
    <div class="tab-content tw-h-full" id="pills-tabContent" v-if="!$fetchState.pending">
      <div class="tab-pane fade show" :class="[$route.query.active_tab==='edit'?'active':'']"  id="products-pills-home" role="tabpanel" aria-labelledby="products-pills-home-tab">
        <EditProductTab :service_details="ServiceDetails"/>
      </div>
      <div class="tab-pane fade show tw-h-full" :class="[$route.query.active_tab==='upgrade'?'active':'']" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
        <UpgradeProductTab/>
      </div>
      <div class="tab-pane fade" id="pills-contact" role="tabpanel" aria-labelledby="pills-contact-tab">
        <PreviewProductTab :service_details="ServiceDetails"/>
      </div>
    </div>


  </div>

</template>

<script>
import EditProductTab from "../../../../components/ProductTabs/EditProductTab";
import UpgradeProductTab from "../../../../components/ProductTabs/UpgradeProductTab";
import PreviewProductTab from "../../../../components/ProductTabs/PreviewProductTab";
export default {
  name: "index",
  layout: "BanooClubLayout",
  components: {PreviewProductTab, UpgradeProductTab, EditProductTab},
  data(){
    return{
      ServiceDetails:null
    }
  },
  head(){
    return{
      title: 'ارتقاء خدمت'
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
</style>
