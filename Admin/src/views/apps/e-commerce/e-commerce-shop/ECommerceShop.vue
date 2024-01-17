<template>
  <b-overlay
      :show="products === null"
      rounded="sm"
  >
    <div v-if="products !== null" style="height: inherit">

    <!-- ECommerce Header -->
    <section id="ecommerce-header">
      <div class="row">
        <div class="col-sm-12">
          <div class="ecommerce-header-items">
            <div class="result-toggler">
              <feather-icon
                icon="MenuIcon"
                class="d-block d-lg-none"
                size="21"
                @click="mqShallShowLeftSidebar = true"
              />
              <div class="search-results">
                {{ totalCount }} محصول یافت شد
              </div>
            </div>
            <div class="view-options d-flex">

              <!-- Sort Button -->
<!--              <b-dropdown-->
<!--                v-ripple.400="'rgba(113, 102, 240, 0.15)'"-->
<!--                :text="sortBy.text"-->
<!--                right-->
<!--                variant="outline-primary"-->
<!--              >-->
<!--                <b-dropdown-item-->
<!--                  v-for="sortOption in sortByOptions"-->
<!--                  :key="sortOption.value"-->
<!--                  @click="sortBy=sortOption"-->
<!--                >-->
<!--                  {{ sortOption.text }}-->
<!--                </b-dropdown-item>-->
<!--              </b-dropdown>-->

              <!-- Item View Radio Button Group  -->
<!--              <b-form-radio-group-->
<!--                v-model="itemView"-->
<!--                class="ml-1 list item-view-radio-group"-->
<!--                buttons-->
<!--                size="sm"-->
<!--                button-variant="outline-primary"-->
<!--              >-->
<!--                <b-form-radio-->
<!--                  v-for="option in itemViewOptions"-->
<!--                  :key="option.value"-->
<!--                  :value="option.value"-->
<!--                >-->
<!--                  <feather-icon-->
<!--                    :icon="option.icon"-->
<!--                    size="18"-->
<!--                  />-->
<!--                </b-form-radio>-->
<!--              </b-form-radio-group>-->
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- Overlay -->
    <div class="body-content-overlay" />

    <!-- Searchbar -->
    <div class="ecommerce-searchbar mt-1">
      <b-row>
        <b-col cols="12">
          <b-input-group class="input-group-merge">
            <b-form-input
              v-model="searchQuery"
              placeholder="جستجوی محصولات"
              class="search-product"
            />
            <b-input-group-append is-text>
              <feather-icon
                icon="SearchIcon"
                class="text-muted"
              />
            </b-input-group-append>
          </b-input-group>
        </b-col>
      </b-row>
    </div>

    <!-- Prodcuts -->
    <section :class="itemView">
      <b-card
        v-for="product in products"
        :key="product.productId"
        class="ecommerce-card"
        no-body
      >
        <div class="item-img text-center pt-0">
          <b-link class="w-100 position-relative" :to="{ name: 'apps-e-commerce-product-details', params: { id: product.productId } }">
            <b-img
              :alt="`${product.name}-${product.productId}`"
              fluid
              class="card-img-top"
              :src="'https://api.halamkhoobee.com/'+product.imageFile"
            />
            <b-badge
                v-if="product.fileData !== ''"
                class="product-badge p-1"
                variant="light-primary"
            >
              دارای مدیا
            </b-badge>
          </b-link>
        </div>

        <!-- Product Details -->
        <b-card-body>
          <div class="item-wrapper">
<!--            <div class="item-rating">-->
<!--              <ul class="unstyled-list list-inline">-->
<!--                <li-->
<!--                  v-for="star in 5"-->
<!--                  :key="star"-->
<!--                  class="ratings-list-item"-->
<!--                  :class="{'ml-25': star-1}"-->
<!--                >-->
<!--                  <feather-icon-->
<!--                    icon="StarIcon"-->
<!--                    size="16"-->
<!--                    :class="[{'fill-current': star <= product.rating}, star <= product.rating ? 'text-warning' : 'text-muted']"-->
<!--                  />-->
<!--                </li>-->
<!--              </ul>-->
<!--            </div>-->
            <h6 class="item-name">
              <b-link
                  class="text-body"
                  :to="{ name: 'apps-e-commerce-product-details', params: { id: product.productId } }"
              >
                {{ product.title }}
              </b-link>
            </h6>
            <div>
              <h6  v-if="!product.isFree" class="item-price text-info">
                {{ makePrettyPrice(product.price) }} تومان
              </h6>
            </div>
          </div>
          <b-card-text class="item-description">
            {{ product.description }}
          </b-card-text>
          <b-badge
              v-if="product.type === 1"
              variant="warning"
              class="d-inline-block"
          >
            دوره
          </b-badge>
          <b-badge
              v-else-if="product.type === 2"
              variant="warning"

          >
            فیلم
          </b-badge>
          <b-badge
              v-else-if="product.type === 3"
              variant="warning"

          >
            فایل
          </b-badge>
          <b-badge
              v-else-if="product.type === 4"
              variant="warning"

          >
            پادکست
          </b-badge>
        </b-card-body>

        <!-- Product Actions -->
        <div class="item-options text-center">
          <div v-if="!product.isFree" class="item-wrapper">
            <div class="item-cost">
              <h4 class="item-price">
                {{ makePrettyPrice(product.price) }} تومان
              </h4>
            </div>
          </div>
          <b-button
            variant="danger"
            tag="a"
            class="btn-wishlist"
            v-b-modal.modal-delete
            @click="showModal(product.productId)"
          >
            <feather-icon
              icon="TrashIcon"
              class="mr-50"
              :class="{'text-danger': product.isInWishlist}"
            />
            <span>حذف</span>
          </b-button>
          <b-button
            variant="light"
            tag="a"
            class="btn-cart"
            @click.prevent="$router.push({ name: 'apps-e-commerce-product-details', params: { id: product.productId } })"
          >
            <feather-icon
              icon="ShoppingCartIcon"
              class="mr-50"
            />
            <span>ویرایش</span>
          </b-button>
          <b-button
              variant="info"
              tag="a"
              class="btn-cart"
              @click.prevent="$router.push({ name: 'apps-e-commerce-add-product-media', params: { id: product.productId } , query : {productType : product.type} })"
          >
            <feather-icon
                icon="CastIcon"
                class="mr-50"
            />
            <span>مدیا</span>
          </b-button>
        </div>
      </b-card>
    </section>

    <!-- Pagination -->
    <section>
      <b-row>
        <b-col cols="12">
          <b-pagination
            v-model="currentPage"
            :total-rows="totalCount"
            per-page="9"
            first-number
            align="center"
            last-number
            prev-class="prev-item"
            next-class="next-item"
          >
            <template #prev-text >
              <feather-icon style="transform: rotate(180deg)"
                icon="ChevronLeftIcon"
                size="18"
              />
            </template>
            <template #next-text>
              <feather-icon
                icon="ChevronRightIcon"
                size="18"
              />
            </template>
          </b-pagination>
        </b-col>
      </b-row>
    </section>

    <!-- Sidebar -->
    <portal to="content-renderer-sidebar-detached-left">
      <shop-left-filter-sidebar
          :filters="filters"
          :filter-options="filterOptions"
          :categories="productCategories"
          :mq-shall-show-left-sidebar.sync="mqShallShowLeftSidebar"
          :types="types"
          @changeCategory="changeCategory"
          @changeType="changeType"
      />
    </portal>
  </div>
    <b-modal
        id="modal-delete"
        centered
        ok-title="حذف"
        cancelTitle="انصراف"
        @ok="deleteProduct(deleteItem)"
    >
      <span>حذف شود؟</span>
    </b-modal>
  </b-overlay>
</template>

<script>
import {
  BDropdown,
  BDropdownItem,
  BFormRadioGroup,
  BFormRadio,
  BRow,
  BCol,
  BInputGroup,
  BInputGroupAppend,
  BFormInput,
  BCard,
  BCardBody,
  BLink,
  BImg,
  BCardText,
  BButton,
  BPagination,
  BOverlay,BBadge,BModal
} from 'bootstrap-vue'
import Ripple from 'vue-ripple-directive'
import { watch } from '@vue/composition-api'
import { useResponsiveAppLeftSidebarVisibility } from '@core/comp-functions/ui/app'
import ShopLeftFilterSidebar from './ECommerceShopLeftFilterSidebar.vue'
import { useShopFiltersSortingAndPagination, useShopUi, useShopRemoteData } from './useECommerceShop'
import { useEcommerceUi } from '../useEcommerce'
import ProductGetAllRequest from "@/libs/Api/Product/ProductGetAllRequest";
import ProductDeleteProductRequest from "@/libs/Api/Product/ProductDeleteProductRequest";
import ToastificationContent from "@core/components/toastification/ToastificationContent";
import ProductCategoryGetAllRequest from "@/libs/Api/Product/ProductCategoryGetAllRequest";
import Helper from "@/libs/Helper";
import ProductCategoryGetAllProductCategoriesRequest
  from "@/libs/Api/Product/ProductCategoryGetAllProductCategoriesRequest";

export default {
  title: 'لیست محصولات - پنل ادمین بانو کلاب',
  data(){
    return{
      products:null,
      totalCount:null,
      productCategories:null,
      deleteItem:null,
      currentPage:1,
      searchQuery:'',
      categoryId:0,
      type:0,
      types:[
        {label : 'دوره' , value : 1},
        {label : 'فیلم' , value : 2},
        {label : 'فایل' , value : 3},
        {label : 'پادکست' , value : 4},
      ],
      defaultGetProductData:{
        categoryId:0,
        searchByTitle:'',
        pageNumber:1,
        count:9,
        type:0
      }
    }
  },
  async created(){
    await this.getProducts(this.defaultGetProductData.pageNumber,this.defaultGetProductData.searchByTitle,this.defaultGetProductData.categoryId,this.defaultGetProductData.type);
    await this.getCategories();
  },
  methods:{
    async getProducts(pageNumber,search,categoryId,type){
      let _this = this;
      let getProductData={
        categoryId:categoryId,
        searchByTitle:search,
        pageNumber:pageNumber,
        count:9,
        type:type,
        isFree:''
      }

      let productGetAllRequest = new ProductGetAllRequest(_this);
      productGetAllRequest.setParams(getProductData)
      await productGetAllRequest.fetch(function (content){
        _this.products = content.products;
        _this.totalCount = content.productsCount;
      } , function (content){
        console.log(content)
      });
    },
    async getCategories(){
      let _this = this;
      let getCategoriesData = {
        pageNumber : 0,
        count : 0
      }

      let productCategoryGetAllRequest = new ProductCategoryGetAllProductCategoriesRequest(_this);
      productCategoryGetAllRequest.setParams(getCategoriesData);
      await productCategoryGetAllRequest.fetch(function (content){
        _this.productCategories = content.productCategories;
      },function (error){
        console.log(error)
      })
    },
    async deleteProduct(param){
      let _this = this;

      let productDeleteProductRequest = new ProductDeleteProductRequest(_this);
      productDeleteProductRequest.setId(param)
      await productDeleteProductRequest.fetch(function (content){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `حذف شد.`,
          },
        })
        _this.getProducts(_this.defaultGetProductData.pageNumber,_this.defaultGetProductData.searchByTitle,_this.defaultGetProductData.categoryId,_this.defaultGetProductData.type);
      } , function (error){
        console.log(error)
      });
    },
    changeCategory(param){
      this.categoryId = param;
    },
    changeType(param){
      this.type = param;
    },
    makePrettyPrice(param){
      return Helper.prettyPrice(param,0)
    },
    showModal(param){
      let _this = this;
      _this.deleteItem = param
    },
  },
  watch:{
    currentPage:function (nv,ov) {
      this.getProducts(nv,this.defaultGetProductData.searchByTitle,this.defaultGetProductData.categoryId,this.defaultGetProductData.type);
    },
    searchQuery:function (nv,ov){
      this.getProducts(this.defaultGetProductData.pageNumber,nv,this.defaultGetProductData.categoryId,this.defaultGetProductData.type);
    },
    categoryId:function (nv,ov){
      this.getProducts(this.defaultGetProductData.pageNumber,this.defaultGetProductData.searchByTitle,nv,this.defaultGetProductData.type);
    },
    type:function (nv,ov){
      this.getProducts(this.defaultGetProductData.pageNumber,this.defaultGetProductData.searchByTitle,this.defaultGetProductData.categoryId,nv);
    },
  },
  directives: {
    Ripple,
  },
  components: {
    // BSV
    BDropdown,
    BDropdownItem,
    BFormRadioGroup,
    BFormRadio,
    BRow,
    BCol,
    BInputGroup,
    BInputGroupAppend,
    BFormInput,
    BCard,
    BCardBody,
    BLink,
    BImg,
    BCardText,
    BButton,
    BPagination,
    BOverlay,
    BBadge,
    BModal,

    // SFC
    ShopLeftFilterSidebar,
  },
  setup() {
    const {
      filters, filterOptions, sortBy, sortByOptions,
    } = useShopFiltersSortingAndPagination()

    const { handleCartActionClick, toggleProductInWishlist } = useEcommerceUi()

    const {
      itemView, itemViewOptions, totalProducts,
    } = useShopUi()

    const { products, fetchProducts } = useShopRemoteData()

    const { mqShallShowLeftSidebar } = useResponsiveAppLeftSidebarVisibility()

    // Wrapper Function for `fetchProducts` which can be triggered initially and upon changes of filters
    const fetchShopProducts = () => {
      fetchProducts({
        q: filters.value.q,
        sortBy: sortBy.value.value,
        page: filters.value.page,
        perPage: filters.value.perPage,
      })
        .then(response => {
          products.value = response.data.products
          totalProducts.value = response.data.total
        })
    }

    fetchShopProducts()

    watch([filters, sortBy], () => {
      fetchShopProducts()
    }, {
      deep: true,
    })

    return {
      // useShopFiltersSortingAndPagination
      filters,
      filterOptions,
      sortBy,
      sortByOptions,

      // useShopUi
      itemView,
      itemViewOptions,
      totalProducts,
      toggleProductInWishlist,
      handleCartActionClick,

      // useShopRemoteData
      products,

      // mqShallShowLeftSidebar
      mqShallShowLeftSidebar,
    }
  },
}
</script>

<style lang="scss">
@import "~@core/scss/base/pages/app-ecommerce.scss";
</style>

<style lang="scss" scoped>
.item-view-radio-group ::v-deep {
  .btn {
    display: flex;
    align-items: center;
  }
}
img{
  height: 25rem!important;
}
</style>
