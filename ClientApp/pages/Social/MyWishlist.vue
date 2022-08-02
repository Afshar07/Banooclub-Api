<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="container mcontainer">
    <div class="row">
      <h1 class="tw-text-2xl tw-font-semibold">علاقه مندیها</h1>
      <ul class="nav nav-pills align-items-end profile_tabs py-3" id="pills-tab" role="tablist">
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link active" id="products-pills-home-tab" data-bs-toggle="pill"
                  data-bs-target="#products-pills-home" type="button" role="tab" aria-controls="products-pills-home" aria-selected="true">
            خدمات مورد علاقه

          </button>

        </li>
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">
            آگهی های مورد علاقه
          </button>
        </li>
      </ul>
    </div>
    <div class="tab-content" id="pills-tabContent" v-if="!$fetchState.pending">
      <div class="tab-pane fade show active" id="products-pills-home" role="tabpanel" aria-labelledby="products-pills-home-tab">
        <div class="row boxMainContent mx-auto">
          <div class="col-12 text-center px-0">
            <div class="row">
              <div v-if="ServicesWishList.length>0" class="d-flex flex-row ">
                <AllServicesTabContentWish :services="ServicesWishList" :categories="categories"/>
              </div>
              <span v-else>شما خدمت مورد علاقه ندارید</span>
            </div>

          </div>
        </div>
      </div>
      <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
        <div class="row">
          <div v-if="AdsWishList.length>0" class="d-flex flex-row flex-wrap">
            <AllAdsTabContentWish :Ads="AdsWishList" :categories="categories"/>

          </div>
          <span v-else>شما آگهی مورد علاقه ندارید</span>
        </div>
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
import AllServicesTabContentWish from "../../components/Products/AllServicesTabContentWish";
import AllAdsTabContentWish from "../../components/Ads/AllAdsTabContentWish";

export default {

  name: "index",
  layout: "PoshtebamPlusLayout",
  components:{
    AllServicesTabContentWish,
    AllAdsTabContentWish,
    FirstTabContent,
    LeftChevronIcon,
    RightChevronIcon,
    ChevronLeftIcon,
    ChevronRightIcon,
    ProductItem,
  },
  head() {
    return {
      title: 'علاقه مندی ها',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'علاقه مندی ها',
        },
      ],
    }
  },
  data(){
    return{
      ServicesWishList:null,
      AdsWishList:null,
      categories:[]
    }
  },
  async fetch(){
    try {
   const res =  await this.$repositories.GetWishlist.GetWishlist({
      userId:this.$auth.user.userInfo.userId
    })
      this.ServicesWishList = res.data.serviceWishes
      this.AdsWishList = res.data.adsWishes

    }
    catch (error){
      console.log(error)
    }
  }
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
