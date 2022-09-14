<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="container  NewBg p-3 mcontainer">
    <div class="row my-3">
        <div class="tw-w-full bg-white tw-shadow p-3 d-flex align-items-center justify-content-between gap-3  rounded ">
          <div class="d-flex align-items-center gap-2">
            <img src="/girl-icon-ads.png" class="tw-w-[7rem] tw-h-[7rem] tw-object-contain" alt="">
            <div class="d-flex align-items-center flex-column">
              <h1 class="text-purple h6" style="font-weight: bolder!important;">آگهی ها</h1>
              <strong class="text-pink">AdvertiseMents</strong>
            </div>
          </div>

          <button v-tooltip="{content:'ثبت آگهی'}" @click="$router.push('/Migration/CreateAdvertise')" class="btn AddReplyBtn text-white">
            <PlusIcon fill="#ff6f9e" style="width: 30px; height: 30px;"/>
          </button>
        </div>
    </div>
    <div class="tab-content bg-white rounded tw-shadow p-3 my-3" id="pills-tabContent">

      <div class="row boxMainContent mx-auto">
        <div class="col-12  px-0">
          <div class="row mx-auto">
            <div class="col-md-12">
              <ul class="nav nav-pills align-items-end profile_tabs py-3" id="pills-tab" role="tablist">
                <li class="nav-item" role="presentation m-0" style="margin: 0 !important;" @click="ActiveTab=1">
                  <button class="nav-link active" id="products-pills-home-tab" data-bs-toggle="pill"
                          type="button" role="tab" aria-controls="products-pills-home" aria-selected="true">
                    همه آگهی ها
                  </button>
                </li>
                <li class="nav-item" role="presentation m-0" style="margin: 0 !important;" @click="ActiveTab=2">
                  <button class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" type="button" role="tab"
                          aria-controls="pills-profile" aria-selected="false">
                    پیشنهادات

                  </button>
                </li>
              </ul>
            </div>
            <div v-if="ActiveTab===1" class="tab-pane fade show active row" id="products-pills-home" role="tabpanel"
                 aria-labelledby="products-pills-home-tab">
              <div class="col-md-12">
                <div class="row">
                  <div class="col-md-3  mt-3">
                    <div class="row">
                      <div class="col-md-12">
                        <div class="accordion " id="accordionPanelsStayOpenExample">
                          <div class="accordion-item">
                            <h2 class="accordion-header" id="panelsStayOpen-headingFour">
                              <button
                                class="accordion-button p-2 w-100   "
                                type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseOne"
                                aria-expanded="true" aria-controls="panelsStayOpen-collapseOne">
                                <div class="tw-w-full tw-flex tw-items-center tw-justify-between">

                                <small> فیلتر ها</small>
                                <FilterIcon class="tw-fill-white"></FilterIcon>
                                </div>
                              </button>
                            </h2>
                            <div id="panelsStayOpen-collapseOne" class="accordion-collapse collapse show"
                                 aria-labelledby="panelsStayOpen-headingFour">
                              <div class="accordion-body">
                                <div class="d-flex  gap-2">
                                  <SlideCheckBox v-model="Ladder" :UniqueId="1"></SlideCheckBox>
                                  <div class="d-flex align-items-center gap-2">
                                    <FireIcon class="tw-fill-red-500 tw-w-6 tw-h-6"></FireIcon>

                                    <small>آتیش زدم</small>
                                  </div>

                                </div>
                                <div class="d-flex align-items-end my-2  gap-2">
                                  <SlideCheckBox v-model="Exchange" :UniqueId="2"></SlideCheckBox>
                                  <div class="d-flex align-items-center gap-2">
                                    <ExChangeIcon class="tw-fill-sky-500 tw-w-6 tw-h-6"></ExChangeIcon>
                                    <small>قابل معاوضه</small>
                                  </div>

                                </div>


                              </div>
                            </div>

                          </div>
                          <div class="accordion-item">
                            <h2 class="accordion-header" id="panelsStayOpen-headingSeven">
                              <button
                                class="accordion-button p-2 w-100 d-flex align-items-center justify-content-between"
                                type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseSeven"
                                aria-expanded="true" aria-controls="panelsStayOpen-collapseSeven">
                                <small> قیمت</small>
                              </button>
                            </h2>
                            <div id="panelsStayOpen-collapseSeven" class="accordion-collapse collapse show"
                                 aria-labelledby="panelsStayOpen-headingSeven">
                              <div class="accordion-body">
                                <div class="d-flex align-items-center gap-2">
                                  <input type="tel" v-model="PriceFrom" placeholder="مثلا از 100000 تومان"
                                         class=" my-1 p-1 Form-Control border ">
                                  <small>-</small>
                                  <input type="tel" v-model="PriceTo" placeholder="مثلا تا 200000 تومان"
                                         class=" my-1 p-1 Form-Control border ">
                                </div>

                              </div>
                            </div>

                          </div>
                          <div class="accordion-item">
                            <h2 class="accordion-header" id="panelsStayOpen-headingTwo">
                              <button class="accordion-button p-2" type="button" data-bs-toggle="collapse"
                                      data-bs-target="#panelsStayOpen-collapseTwo" aria-expanded="true"
                                      aria-controls="panelsStayOpen-collapseOne">

                                <small> دسته بندی</small>
                              </button>
                            </h2>
                            <div id="panelsStayOpen-collapseTwo" class="accordion-collapse collapse show"
                                 aria-labelledby="panelsStayOpen-headingOne">
                              <div class="accordion-body">
                                <select v-model="SelectedCategoryId" class=" Form-Control border">
                                  <option :value="0" selected>
                                    همه دسته بندی ها
                                  </option>
                                  <option v-for="item in categories" :value="item.adsCategoryId">
                                    {{ item.name }}
                                  </option>
                                </select>
                              </div>
                            </div>
                          </div>
                          <div class="accordion-item">
                            <h2 class="accordion-header" id="panelsStayOpen-headingThree">
                              <button class="accordion-button p-2" type="button" data-bs-toggle="collapse"
                                      data-bs-target="#panelsStayOpen-collapseThree" aria-expanded="true"
                                      aria-controls="panelsStayOpen-collapseOne">
                                <small> شهر و استان</small>
                              </button>
                            </h2>
                            <div id="panelsStayOpen-collapseThree" class="accordion-collapse collapse show"
                                 aria-labelledby="panelsStayOpen-headingOne">
                              <div class="accordion-body">

                                <v-select
                                  @input="GetCity"
                                  :options="AllStates"
                                  label="name"
                                  dir="rtl"

                                  placeholder="استان"
                                  v-model="SelectedStateId"
                                  :reduce="(name) => name.stateId"

                                ></v-select>


                                <v-select

                                  :options="AllCities"
                                  label="name"
                                  dir="rtl"
                                  class="my-3"
                                  placeholder="شهر"
                                  v-model="SelectedCityId"
                                  :reduce="(name) => name.cityId"

                                ></v-select>
                              </div>
                            </div>
                          </div>

                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="col-md-9">
                    <div class="row">
                      <div class="col-md-12 mt-3">
                        <input type="search" v-model="Search" class="SearchStyle" placeholder="جستجو در میان آگهی ها">
                      </div>
                      <div class="col-md-12"  style="height: 1000px; overflow-y: scroll">
                        <AllAdsTabContent :Ads="AllAds" :categories="categories"/>
<!--                      <div class="col-md-12 d-flex align-items-center justify-content-center">-->
<!--                        <Spinner></Spinner>-->
<!--                      </div>-->
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div v-if="ActiveTab===2" class="tab-pane show active row" id="pills-profile" role="tabpanel"
                 aria-labelledby="pills-profile-tab">
              <div class="col-md-12">
                <FirstTabContentAds :Ads="AllAds" :categories="categories"/>
              </div>
            </div>

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
import FirstTabContentAds from "../../components/Ads/FirstTabContentAds";
import CustomPagination from "@/components/utilities/CustomPagination";
import AllAdsTabContent from "../../components/Ads/AllAdsTabContent";
import PlusIcon from "@/components/Icons/PlusIcon";
import SlideCheckBox from "@/components/SlideCheckBox";
import FilterIcon from "@/components/Icons/FilterIcon";
import FireIcon from "@/components/Icons/FireIcon";
import ExChangeIcon from "@/components/Icons/ExChangeIcon";
import Spinner from "@/components/Spinner";
export default {

  name: "index",
  layout: "PoshtebamPlusLayout",
  components: {
    CustomPagination,
    AllAdsTabContent,
    FirstTabContentAds,
    Spinner,
    FireIcon,
    SlideCheckBox,
    ExChangeIcon,
    LeftChevronIcon,
    RightChevronIcon,
    ChevronLeftIcon,
    FilterIcon,
    ChevronRightIcon,
    ProductItem,
    PlusIcon,
  },
  head() {
    return {

      title: 'همه آگهی ها',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'همه آگهی ها',
        },
      ],
    };
  },
  data() {
    return {
      ActiveTab: 1,
      AllAds: null,
      categories: [],
      SelectedCategoryId: 0,
      PriceFrom: null,
      Ladder: false,
      PriceTo: null,
      CityId: 0,
      SelectedPageId: 1,
      SelectedStateId: null,
      Exchange: false,
      AllAdsCount: 0,
      SelectedCityId: null,
      totalPages: [],
      AllCities: [
        {
          name: 'همه شهر ها',
          cityId: 0
        }
      ],
      AllStates: [
        {

          name: 'همه استان ها',
          stateId: 0
        }
      ],
      Search: ''
    }
  },
  watch: {
    Search: function (val, oldVal) {
      this.GetAdd();
    },
    PriceFrom: function (val, oldVal) {
      this.GetAdd();
    },
    PriceTo: function (val, oldVal) {
      this.GetAdd();
    },
    SelectedCategoryId: function (val, oldVal) {
      this.GetAdd();
    },
    SelectedStateId: function (val, oldVal) {
      this.GetAdd();
    },
    SelectedCityId: function (val, oldVal) {
      this.GetAdd();
    },
    Exchange:function (){
      this.GetAdd()
    },
    Ladder:function (){
      this.GetAdd()
    }
  },
  methods: {
    changePage(id) {
      this.SelectedPageId = id
      this.GetAdd()
    },
    handleScroll(event){

      if (event.target.offsetHeight + event.target.scrollTop >= event.target.scrollHeight) {
        this.GetAddForInfinite()
      }
    },

    async GetAdd() {
      try {
        const res = await this.$repositories.GetAllAds.GetAllAds({
          priceFrom: this.PriceFrom,
          priceTo: this.PriceTo,
          title: this.Search,
          tag: null,
          city: this.SelectedCityId,
          state: this.SelectedStateId,
          lastItemFireDate: '',
          count: 10,
          categoryId: this.SelectedCategoryId,
          planType: this.Ladder ? 1 : null,
          exchangeability: this.Exchange
        })
        this.AllAds = res.data.ads
        this.AllAdsCount = res.data.adsCount
        this.$nuxt.$loading.finish();
        this.$nuxt.loading = false;
      } catch (error) {
        console.log(error)
      } finally {
        this.$nuxt.$loading.finish();
        this.$nuxt.loading = false;
      }
    },
    async GetAddForInfinite() {
      try {
        const res = await this.$repositories.GetAllAds.GetAllAds({
          priceFrom: this.PriceFrom,
          priceTo: this.PriceTo,
          title: this.Search,
          tag: null,
          city: this.SelectedCityId,
          state: this.SelectedStateId,
          lastItemFireDate: this.AllAds[this.AllAds.length - 1].fireDate,
          count: 10,
          categoryId: this.SelectedCategoryId,
          planType: this.Ladder ? 1 : null,
          exchangeability: this.Exchange
        })
        this.AllAds = res.data.ads
        this.AllAdsCount = res.data.adsCount
        this.$nuxt.$loading.finish();
        this.$nuxt.loading = false;
      } catch (error) {
        console.log(error)
      } finally {
        this.$nuxt.$loading.finish();
        this.$nuxt.loading = false;
      }
    },
    async GetCity() {

      this.$nextTick(() => {
        this.$nuxt.$loading.start()
      })
      try {
        const res = await this.$repositories.GetAllCities.GetAllCities({
          pageNumber: 1,
          count: 500,
          stateId: this.SelectedStateId

        })
        res.data.cities.forEach((item) => {
          this.AllCities.push(item)
        })
        this.$nuxt.$loading.finish()
        this.$nuxt.loading = false;
      } catch (e) {
        console.log(e)
      } finally {
        this.$nuxt.$loading.finish()
        this.$nuxt.loading = false;
      }
    }
  },
  async fetch() {
    try {
      const res = await this.$repositories.GetAllAds.GetAllAds({
        priceFrom: this.PriceFrom,
        priceTo: this.PriceTo,
        title: this.Search,
        tag: null,
        city: this.SelectedCityId,
        state: this.SelectedStateId,
        firstSearchadsId: null,
        count: 10,
        categoryId: this.SelectedCategoryId
      })
      this.AllAds = res.data.ads
      this.totalPages = []
      const result = Math.ceil(res.data.adsCount / 10)
      for (let i = 1; i <= result; i++) {
        this.totalPages.push(i);
      }

      const allCategories = await this.$repositories.getAllAdsCategory.getAllAdsCategory();
      this.categories = allCategories.data.adsCategories;

    } catch (error) {
      console.log(error)
    }

    try {
      const res = await this.$repositories.GetAllStates.GetAllStates()
      res.data.states.forEach((item) => {
        this.AllStates.push(item)
      })


    } catch (e) {
      console.log(e)
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
.nav-pills .nav-link {
  border-bottom: 3px solid transparent;
}

.nav-link {
  font-weight: 500;
  color: #8c8d90 !important;
  font-weight: 500;
  padding: 5px 9px 15px;
}

.nav-pills .nav-link.active, .nav-pills .show > .nav-link {
  color: #0d6efd !important;
  background-color: transparent;
  font-weight: 500;
  border-bottom: 3px solid #0d6efd !important;
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.accordion-button::after {
  content: '' !important;
  background-image: none !important;
}

.accordion-button:not(.collapsed) {
  color: white !important;
  background-color: #3b82f6;
  box-shadow: inset 0 -1px 0 rgb(0 0 0 / 13%);
}

.accordion-button{
  @apply tw-bg-[#fc5399] !important;
}

.Form-Control {
  height: 30px !important;
  font-size: 12px !important;
}

.vs__search {
  height: 20px !important;
  font-size: 12px !important;
  color: grey !important;

}
</style>
