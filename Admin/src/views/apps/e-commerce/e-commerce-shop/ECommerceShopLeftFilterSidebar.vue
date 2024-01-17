<template>
  <div class="sidebar-detached sidebar-left">
    <div class="sidebar">
      <div
        class="sidebar-shop"
        :class="{'show': mqShallShowLeftSidebar}"
      >
        <b-row>
          <b-col cols="12">
            <h6 class="filter-heading d-none d-lg-block">
              فیلترها
            </h6>
          </b-col>
        </b-row>

        <!-- Filters' Card -->
        <b-card>

          <!-- Price Slider -->
<!--          <div class="price-slider">-->
<!--            <h6 class="filter-title">-->
<!--              Price Range-->
<!--            </h6>-->
<!--            <vue-slider-->
<!--              v-model="filters.priceRange"-->
<!--              :direction="$store.state.appConfig.isRTL ? 'rtl' : 'ltr'"-->
<!--            />-->
<!--          </div>-->

          <!-- Categories -->
          <div class="product-categories">
            <h4 class="filter-title">
              دسته بندی ها
            </h4>
            <b-form-radio
                v-model="category"
                stacked
                class="mt-1"
                value="0"
                @change="changeCategory(0)"
            >همه</b-form-radio>
            <b-form-radio
              v-for="item in categories"
              v-model="category"
              stacked
              class="mt-1"
              :name="item.name"
              :value="item.productCategoryId"
              @change="changeCategory(item.productCategoryId)"
            >
              {{ item.name }}
            </b-form-radio>
          </div>

          <!-- Types -->
          <div class="product-categories">
            <h4 class="filter-title">
              نوع محصول
            </h4>
            <b-form-radio
                v-model="type"
                stacked
                class="mt-1"
                value="0"
                @change="changeType(0)"
            >همه</b-form-radio>
            <b-form-radio
                v-for="item in types"
                v-model="type"
                stacked
                class="mt-1"
                :name="item.label"
                :value="item.value"
                @change="changeType(item.value)"
            >
              {{ item.label }}
            </b-form-radio>
          </div>

          <!-- Ratings -->
<!--          <div class="ratings">-->
<!--            <h6 class="filter-title">-->
<!--              Ratings-->
<!--            </h6>-->
<!--            <div-->
<!--              v-for="rating in filterOptions.ratings"-->
<!--              :key="rating.rating"-->
<!--              class="ratings-list"-->
<!--            >-->
<!--              <b-link>-->
<!--                <div class="d-flex">-->
<!--                  <feather-icon-->
<!--                    v-for="star in 5"-->
<!--                    :key="star"-->
<!--                    icon="StarIcon"-->
<!--                    size="17"-->
<!--                    :class="[{'fill-current': star <= rating.rating}, star <= rating.rating ? 'text-warning' : 'text-muted']"-->
<!--                  />-->
<!--                  <span class="ml-25">&amp; up</span>-->
<!--                </div>-->
<!--              </b-link>-->
<!--              <div class="stars-received">-->
<!--                <span>{{ rating.count }}</span>-->
<!--              </div>-->
<!--            </div>-->
<!--          </div>-->
        </b-card>
      </div>
    </div>

    <div
      class="body-content-overlay"
      :class="{'show': mqShallShowLeftSidebar}"
      @click="$emit('update:mq-shall-show-left-sidebar', false)"
    />
  </div>
</template>

<script>
import {
  BRow, BCol, BCard, BFormRadioGroup, BLink,
} from 'bootstrap-vue'
import VueSlider from 'vue-slider-component'
import {BFormRadio} from "bootstrap-vue";

export default {
  components: {
    BRow,
    BCol,
    BFormRadioGroup,
    BLink,
    BCard,

    // 3rd Party
    VueSlider,
    BFormRadio
  },
  data(){
    return{
      category:0,
      type:0,
    }
  },
  methods:{
    changeCategory(param){
      this.$emit('changeCategory',param)
    },
    changeType(param){
      this.$emit('changeType',param)
    }
  },
  props: {
    filters: {
      type: Object,
      required: true,
    },
    filterOptions: {
      type: Object,
      required: true,
    },
    categories:{
      type : Array,
      required : true,
    },
    types:{
      type : Array,
      required : true,
    },
    mqShallShowLeftSidebar: {
      type: Boolean,
      required: true,
    },
  },
}
</script>

<style lang="scss">
@import '~@core/scss/vue/libs/vue-slider.scss';
</style>

<style lang="scss" scoped>
.categories-radio-group,
.brands-radio-group,
.price-range-defined-radio-group {
    ::v-deep > .custom-control {
    margin-bottom: 0.75rem;
  }
}
</style>
