<template>
  <div class="tw-flex-column px-2">
    <div class="tw-flex-column">
      <div class="tw-flex tw-justify-between tw-items-center tw-my-6">
        <h2 class="tw-text-2xl tw-font-semibold">پیشنهادات</h2>

        <nuxt-link  v-if="FilterTypeSuggestion.length>0" :to="`/Products/ProductsFilter/FilterId/2`" class="tw-text-blue-500 sm:tw-block tw-hidden text-decoration-none"> مشاهده همه</nuxt-link>
      </div>
      <div   v-if="FilterTypeSuggestion.length>0" class="tw-relative">
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

          fixed-height="270px"
          ref="product_carousel"
          class="no-shadow"
          :visible-slides="5"
          slide-multiple
          :rtl="true"
          :gap="1"
          :bullets="false"
          :touchable="false"
          :arrows="false"
          :infinite="false"
          :breakpoints="{ 425:{visibleSlides: 1,  slideMultiple: 1}, 426: { visibleSlides: 2,  slideMultiple: 2 } , 769: { visibleSlides: 3,  slideMultiple: 2,  gap:0 }}"
        >
          <vueper-slide v-for="(item,idx) in FilterTypeSuggestion " v-if="FilterTypeSuggestion.length>0"  :key="idx">
            <template #content>
              <ProductItem   @GetServices="Refresh()" :service_details="item" />

            </template>
          </vueper-slide>
        </vueper-slides>

      </div>
      <div v-else class="tw-flex tw-items-center tw-justify-center">
        <span class="text-secondary">داده ای برای نمایش وجود ندارد</span>
      </div>
    </div>


    <div class="tw-flex-column">
      <div class="tw-flex tw-justify-between tw-items-center tw-my-6">
        <div class="d-flex flex-column align-items-start">
          <h2 class="tw-text-2xl tw-font-semibold">دسته بندی ها</h2>
          <p class="tw-font-medium tw-text-gray-500 tw-leading-6">
            بر اساس دسته بندی ها محصول خود را پیدا کنید
          </p>
        </div>
        <nuxt-link :to="`/Products/ProductsFilter/Categories/`" class="tw-text-blue-500 sm:tw-block tw-hidden text-decoration-none"> مشاهده همه</nuxt-link>
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
          v-if="categories !== null && categories.length>0"
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
          <vueper-slide v-for="(category,idx) in categories" :key="idx">
            <template #content>
              <CategoryItem :category_details="category"/>
            </template>
          </vueper-slide>
        </vueper-slides>

      </div>
    </div>

    <div class="tw-flex-column">
      <div class="tw-flex tw-justify-between tw-items-center tw-my-6">
        <h2 class="tw-text-2xl tw-font-semibold">جدیدترین ها</h2>
        <nuxt-link v-if="FilterNewestType" :to="`/Products/ProductsFilter/FilterId/1`" class="tw-text-blue-500 sm:tw-block tw-hidden text-decoration-none"> مشاهده همه</nuxt-link>
      </div>
      <div   v-if="FilterNewestType.length>0" class="tw-relative">
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

          fixed-height="120px"
          ref="new_carousel"
          class="no-shadow"
          :visible-slides="3"
          :gap="1"
          slide-multiple
          :rtl="true"
          :bullets="false"
          :touchable="false"
          :arrows="false"
          :infinite="true"
          :breakpoints="{ 425:{visibleSlides: 1,  slideMultiple: 1}, 426: { visibleSlides: 1,  slideMultiple: 2 } , 769: { visibleSlides: 2,  slideMultiple: 2,  gap:0 }}"
        >
          <vueper-slide v-for="(item,idx) in FilterNewestType" :key="idx">
            <template #content>
              <NewItem  @GetServices="Refresh()" :service_details="item" :category_details="categories"/>
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
        <nuxt-link v-if="FilterBestType" :to="`/Products/ProductsFilter/FilterId/3`" class="tw-text-blue-500 sm:tw-block tw-hidden text-decoration-none"> مشاهده همه</nuxt-link>
      </div>
      <div v-if="FilterBestType.length>0" class="tw-flex-column tw-relative">
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
          fixed-height="270px"
          ref="brands_carousel"
          class="no-shadow"
          :visible-slides="5"
          slide-multiple
          :rtl="true"
          :gap="1"
          :touchable="false"
          :bullets="false"
          :arrows="false"
          :infinite="true"
          :breakpoints="{ 425:{visibleSlides: 1,  slideMultiple: 1}, 426: { visibleSlides: 2,  slideMultiple: 2 } , 769: { visibleSlides: 3,  slideMultiple: 2,  gap:0 }}"
        >
          <vueper-slide v-for="(item,idx) in FilterBestType" :key="idx">
            <template #content>
              <ProductItem  @GetServices="Refresh()" :service_details="item"/>
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
import CategoryItem from "../Categories/CategoryItem";
import NewItem from "./NewItem";

export default {
  name: "FirstTabContent",
  components: {
    NewItem,
    LeftChevronIcon,
    RightChevronIcon,
    VueperSlides,
    VueperSlide,
    ProductItem,
    CategoryItem
  },

  methods:{
    Refresh(){
    this.$emit('RefetchServices')
    }
  },

  computed:{
    FilterTypeSuggestion(){
      const data = this.AllServices
          return data.filter((el)=>{
            return el.planTypes.includes(2)
          })

    },
    FilterNewestType(){
      const data = this.AllServices
      return data.filter((el)=>{
        return el.planTypes.includes(4)
      })
    },
    FilterBestType(){
      const data = this.AllServices
      return data.filter((el)=>{
        return el.planTypes.includes(3)
      })
    }
  },
  props:{
    AllServices:{
      type:Array,
      required: true
    },
    categories:{
      type: Array,
      required: true
    },
  }
}
</script>

<style scoped>

</style>
