<template>
  <div class="tw-flex-column">
    <div v-if="FilterAdsSuggestion.length>0" class="tw-flex-column tw-relative">
      <button
        class="tw-absolute tw-bg-white tw-bottom-1/2 tw-flex tw-items-center tw-justify-center tw-p-2 tw--right-4 tw-rounded-full tw-shadow-md tw-text-xl tw-w-9 tw-z-10"
        @click="$refs.product_carousel.next()">
        <RightChevronIcon/>
      </button>
      <button
        class="tw-absolute tw-bg-white tw-bottom-1/2 tw-flex tw-items-center tw-justify-center tw-p-2 tw--left-4 tw-rounded-full tw-shadow-md tw-text-xltw-w-9 tw-z-10"
        @click="$refs.product_carousel.previous()">
        <LeftChevronIcon/>
      </button>
      <vueper-slides
        fixed-height="250px"
        ref="product_carousel"
        class="no-shadow"
        :visible-slides="5"
        slide-multiple
        :rtl="true"
        :gap="5"
        :bullets="false"
        :touchable="false"
        :arrows="false"
        :infinite="false"
        :breakpoints="{ 426: { visibleSlides: 2,  slideMultiple: 2 } , 769: { visibleSlides: 3,  slideMultiple: 2,  gap:0 }}"
      >
        <vueper-slide v-for="ad in FilterAdsSuggestion ">
          <template #content>
            <AdItem :AdsDetail="ad" :category_details="categories"/>
          </template>
        </vueper-slide>
      </vueper-slides>
    </div>
    <div v-else class="tw-flex tw-items-center tw-justify-center">
      <span class="text-secondary">داده ای برای نمایش وجود ندارد</span>
    </div>
    <div class="tw-flex-column">
      <div class="tw-flex tw-justify-between tw-items-center tw-my-6">
        <div class="d-flex flex-column align-items-start">
          <h2 class="tw-text-2xl tw-font-semibold">دسته بندی ها</h2>
          <p class="tw-font-medium tw-text-gray-500 tw-leading-6">
            بر اساس دسته بندی ها محصول خود را پیدا کنید
          </p>
        </div>
        <nuxt-link to="/Migration/AdsFilter/Categories/" class="tw-text-blue-500 sm:tw-block tw-hidden text-decoration-none"> مشاهده همه</nuxt-link>
      </div>
      <div class="tw-relative">
        <button
          class="tw-absolute tw-bg-white -tw-translate-y-1/2 tw-top-1/2 tw-flex tw-items-center tw-justify-center tw-p-2 tw--right-4 tw-rounded-full tw-shadow-md tw-text-xl tw-w-9 tw-z-10"
          @click="$refs.category_carousel.next()">
          <RightChevronIcon/>
        </button>
        <button
          class="tw-absolute tw-bg-white -tw-translate-y-1/2 tw-top-1/2 tw-flex tw-items-center tw-justify-center tw-p-2 tw--left-4 tw-rounded-full tw-shadow-md tw-text-xltw-w-9 tw-z-10"
          @click="$refs.category_carousel.previous()">
          <LeftChevronIcon/>
        </button>
        <vueper-slides
          fixed-height="170px"
          ref="category_carousel"
          class="no-shadow tw-relative"
          :visible-slides="5"
          :gap="1"
          slide-multiple
          :rtl="true"
          :bullets="false"
          :arrows="false"
          :touchable="false"
          :infinite="false"
          :breakpoints="{ 426: { visibleSlides: 2,  slideMultiple: 2 } , 590: { visibleSlides: 3,  slideMultiple: 2,  gap:0 }}"
        >
          <vueper-slide v-for="category in categories">
            <template #content>

              <adsCategoryItem :category_details="category"/>
            </template>
          </vueper-slide>
        </vueper-slides>

      </div>
    </div>

    <div  class="tw-flex-column">
      <div class="tw-flex tw-justify-between tw-items-center tw-my-6">
        <h2 class="tw-text-2xl tw-font-semibold">جدیدترین ها</h2>
<!--        <nuxt-link to="#" class="tw-text-blue-500 sm:tw-block tw-hidden text-decoration-none"> مشاهده همه</nuxt-link>-->
      </div>
      <div v-if="FilterNewestAds.length>0" class="tw-relative">
        <button
          class="tw-absolute tw-bg-white -tw-translate-y-1/2 tw-top-1/2 tw-flex tw-items-center tw-justify-center tw-p-2 tw--right-4 tw-rounded-full tw-shadow-md tw-text-xl tw-w-9 tw-z-10"
          @click="$refs.new_carousel.next()">
          <RightChevronIcon/>
        </button>
        <button
          class="tw-absolute tw-bg-white -tw-translate-y-1/2 tw-top-1/2 tw-flex tw-items-center tw-justify-center tw-p-2 tw--left-4 tw-rounded-full tw-shadow-md tw-text-xltw-w-9 tw-z-10"
          @click="$refs.new_carousel.previous()">
          <LeftChevronIcon/>
        </button>
        <vueper-slides
          fixed-height="300px"
          ref="new_carousel"
          class="no-shadow"
          :visible-slides="5"
          :gap="5"
          slide-multiple
          :rtl="true"
          :bullets="false"
          :touchable="false"
          :arrows="false"
          :infinite="true"
          :breakpoints="{ 426: { visibleSlides: 1,  slideMultiple: 2 } , 769: { visibleSlides: 2,  slideMultiple: 2,  gap:0 }}"
        >
          <vueper-slide v-for="Ad in FilterNewestAds">
            <template #content>
              <AdItem class="" :AdsDetail="Ad" />
            </template>
          </vueper-slide>
        </vueper-slides>

      </div>
      <div v-else class="tw-flex tw-items-center tw-justify-center">
        <span class="text-secondary">داده ای برای نمایش وجود ندارد</span>
      </div>
    </div>

    <div class="tw-flex-column">
      <div class="tw-flex tw-justify-between tw-items-center tw-my-6 tw-border-b pb-2 my-4">
        <h2 class="tw-text-2xl tw-font-semibold">خدمات برتر</h2>
<!--        <nuxt-link to="#" class="tw-text-blue-500 sm:tw-block tw-hidden text-decoration-none"> مشاهده همه</nuxt-link>-->
      </div>
      <div v-if="FilterBestAds.length>0" class="tw-flex-column tw-relative">
        <button
          class="tw-absolute tw-bg-white tw-bottom-1/2 tw-flex tw-items-center tw-justify-center tw-p-2 tw--right-4 tw-rounded-full tw-shadow-md tw-text-xl tw-w-9 tw-z-10"
          @click="$refs.brands_carousel.next()">
          <RightChevronIcon/>
        </button>
        <button
          class="tw-absolute tw-bg-white tw-bottom-1/2 tw-flex tw-items-center tw-justify-center tw-p-2 tw--left-4 tw-rounded-full tw-shadow-md tw-text-xltw-w-9 tw-z-10"
          @click="$refs.brands_carousel.previous()">
          <LeftChevronIcon/>
        </button>
        <vueper-slides
          fixed-height="250px"
          ref="brands_carousel"
          class="no-shadow"
          :visible-slides="5"
          slide-multiple
          :rtl="true"
          :gap="5"
          :touchable="false"
          :bullets="false"
          :arrows="false"
          :infinite="true"
          :breakpoints="{ 426: { visibleSlides: 2,  slideMultiple: 2 } , 769: { visibleSlides: 3,  slideMultiple: 2,  gap:0 }}"
        >
          <vueper-slide v-for="Ad in FilterBestAds">
            <template #content>
              <AdItem  :AdsDetail="Ad"/>
            </template>
          </vueper-slide>
        </vueper-slides>
      </div>
      <div v-else class="tw-flex tw-items-center tw-justify-center">
        <span class="text-secondary">داده ای برای نمایش وجود ندارد</span>
      </div>
    </div>


  </div>

</template>

<script>
import ProductItem from "../Products/ProductItem";
import {VueperSlides, VueperSlide} from 'vueperslides'
import 'vueperslides/dist/vueperslides.css'
import RightChevronIcon from "../Icons/RightChevronIcon";
import LeftChevronIcon from "../Icons/LeftChevronIcon";
import adsCategoryItem from "../Categories/adsCategoryItem";
import  AdItem from '../../components/Ads/AdItem'

export default {
  name: "FirstTabContentAds",
  components: {

    LeftChevronIcon,
    RightChevronIcon,
    VueperSlides,
    VueperSlide,
    AdItem,
    adsCategoryItem
  },

  computed:{
    FilterAdsSuggestion(){
      const data = this.Ads
      return data.filter(e=> e.planTypes.includes(2))
    },
    FilterNewestAds(){
      const data = this.Ads
      return data.filter(e=> e.planTypes.includes(3))
    },
    FilterBestAds(){
      const data = this.Ads
      return data.filter(e=> e.planTypes.includes(4))
    }
  },
  props:{
    Ads:{
      type:Array,
      required: true
    },
    categories:{
      type: Array,
      required: true
    }
  }
}
</script>

<style scoped>

</style>
