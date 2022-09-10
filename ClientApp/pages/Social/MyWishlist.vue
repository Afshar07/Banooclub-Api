<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="container NewBg p-3 mcontainer">
    <div class="row">
      <div class="tw-w-full tw-shadow bg-white p-3 d-flex align-items-center justify-content-between gap-3  rounded ">
        <div class="d-flex align-items-center gap-2">
          <img src="/girl-icon-alaghemandi.png" class="tw-w-[7rem] tw-h-[7rem] tw-object-contain" alt="">
          <div class="d-flex align-items-center flex-column">
            <h1 class="text-purple h6">علاقه مندی من</h1>
            <strong class="text-pink">My Favorites</strong>
          </div>
        </div>

        <button v-tooltip="{content:'ثبت آگهی'}" @click="$router.push('/Migration/CreateAdvertise')" class="btn AddReplyBtn text-white">
          <PlusIcon fill="#ff6f9e" style="width: 30px; height: 30px;"/>
        </button>
      </div>

    </div>
    <div class="tab-content bg-white tw-shadow my-3 tw-rounded p-3" id="pills-tabContent" v-if="!$fetchState.pending">
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
      <div class="tab-pane fade show active" id="products-pills-home" role="tabpanel" aria-labelledby="products-pills-home-tab">
        <div class="row boxMainContent mx-auto">
          <div class="col-12 text-center px-0">
                <AllServicesTabContentWish v-if="ServicesWishList.length>0" :services="ServicesWishList" :categories="categories"/>
              <span v-else>شما خدمت مورد علاقه ندارید</span>
          </div>
        </div>
      </div>
      <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
        <div class="row boxMainContent mx-auto">
          <div class="col-12 text-center px-0">
            <AllAdsTabContentWish v-if="AdsWishList.length>0" :Ads="AdsWishList" :categories="categories"/>
            <span v-else>شما آگهی مورد علاقه ندارید</span>
          </div>
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
      ServicesWishList:[],
      AdsWishList:[],
      categories:[]
    }
  },
  async fetch(){
    try {
   const res =  await this.$repositories.GetMyWishList.GetMyWishList()
      res.data.forEach((item)=>{
        if(item.type===1){
          this.ServicesWishList.push(item)
        }else if(item.type===3){
          this.AdsWishList.push(item)
        }
      })


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
