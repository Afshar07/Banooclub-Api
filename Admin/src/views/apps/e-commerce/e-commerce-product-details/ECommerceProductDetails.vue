<template>
  <b-overlay
      :show="product === null || showOverlay"
      rounded="sm"
  >
    <component :is="product === null ? 'div' : 'b-card'">
    <b-tabs
        v-if="product"

    >

      <!-- Tab: Account -->
      <b-tab active>
        <template #title>
          <feather-icon
              icon="ClipboardIcon"
              size="16"
              class="mr-0 mr-sm-50"
          />
          <span class="d-none d-sm-inline">اطلاعات محصول</span>
        </template>

        <!-- Media -->
        <b-media class="my-2">
          <template >
            <b-avatar
                ref="previewEl"
                :src="base64ImageSrc"
                size="300px"
                rounded
            />
          </template>
          <div class="d-flex flex-wrap mt-1">
            <b-button
                variant="primary"
                @click="$refs.refInputEl.click()"
            >
              <input
                  ref="refInputEl"
                  type="file"
                  class="d-none"
                  @input="makeBase64Pic"
              >
              <span class="d-none d-sm-inline">+ عکس</span>
              <feather-icon
                  icon="EditIcon"
                  class="d-inline d-sm-none"
              />
            </b-button>
            <b-button
                variant="outline-secondary"
                class="ml-1"
                @click="deleteProductPic()"
            >
              <span class="d-none d-sm-inline">حذف</span>
              <feather-icon
                  icon="TrashIcon"
                  class="d-inline d-sm-none"
              />
            </b-button>
          </div>
        </b-media>

        <div>

          <b-form>
            <b-row>

              <!-- Field:  Title -->
              <b-col
                  cols="12"
                  md="4"
              >
                <b-form-group
                    label="Title"
                    label-for="Title"
                >
                  <b-form-input
                      id="Title"
                      v-model="product.title"
                  />
                </b-form-group>
              </b-col>

              <!-- Field: isFree -->
              <b-col
                  cols="12"
                  md="4"
              >
                <b-form-group
                    label="Status"
                    label-for="user-status"
                >
                  <v-select
                      v-model="product.isFree"
                      :options="isFreeOptions"
                      :reduce="val => val.value"
                      :clearable="false"
                      input-id="user-status"
                  />
                </b-form-group>
              </b-col>

              <!-- Field:  price -->
              <b-col
                  v-if="!product.isFree"
                  cols="12"
                  md="4"
              >
                <b-form-group
                    label="price"
                    label-for="price"
                >
                  <b-form-input
                      id="price"
                      v-model="product.price"
                  />
                </b-form-group>
              </b-col>

              <!-- Field:  description -->
              <b-col
                  cols="12"
              >
                <b-form-group
                    label="description"
                    label-for="description"
                >
                  <b-form-textarea
                      id="description"
                      v-model="product.description"
                      rows="6"
                  />
                </b-form-group>
              </b-col>

              <!-- Field: productCategoryId -->
              <b-col v-if="categories !== null"
                     cols="12"
                     md="4"
              >
                <b-form-group
                    label="دسته بندی"
                    label-for="productCategoryId"
                >
                  <v-select
                      v-model="product.productCategoryId"
                      :options="categories"
                      label="name"
                      :reduce="name => name.productCategoryId"
                      :clearable="false"
                      input-id="productCategoryId"
                      disabled
                  />
                </b-form-group>
              </b-col>

              <!-- Field: type -->
              <b-col
                  cols="12"
                  md="4"
              >
                <b-form-group
                    label="انتخاب نوع محصول"
                    label-for="type"
                >
                  <v-select
                      v-model="product.type"
                      :options="types"
                      :reduce="val => val.value"
                      :clearable="false"
                      input-id="productCategoryId"
                      disabled
                  />
                </b-form-group>
              </b-col>

              <!-- Field: trailerFile -->
              <b-col
                  cols="12"
                  md="4"
              >

                <b-button
                    class="mt-2 col-12"
                    variant="warning"
                    @click="$refs.refInputEl2.click()"
                >
                  <input
                      ref="refInputEl2"
                      type="file"
                      class="d-none"
                      @change="makeBase64"
                  >
                  <span class="">تریلر</span>
                </b-button>

              </b-col>


            </b-row>
          </b-form>

          <b-button
              variant="primary"
              class="mt-1 mb-sm-0 mr-0 mr-sm-1 col-12 col-md-2"
              :block="$store.getters['app/currentBreakPoint'] === 'xs'"
              @click="updateProduct"
          >
            ثبت
          </b-button>

        </div>

      </b-tab>

    </b-tabs>
  </component>
  </b-overlay>
</template>

<script>
import { useRouter } from '@core/utils/utils'
import store from '@/store'
import { ref } from '@vue/composition-api'
import {
  BCard, BCardBody, BRow, BCol, BImg, BCardText, BLink, BButton, BDropdown, BDropdownItem, BAlert,BTab,BTabs,BForm,BFormGroup,BFormInput,BFormTextarea,BMedia,BAvatar, BOverlay,
} from 'bootstrap-vue'
import Ripple from 'vue-ripple-directive'
import ECommerceProductDetailsItemFeatures from './ECommerceProductDetailsItemFeatures.vue'
import ECommerceProductDetailsRelatedProducts from './ECommerceProductDetailsRelatedProducts.vue'
import vSelect from 'vue-select'
import ProductGetByIdRequest from "@/libs/Api/Product/ProductGetByIdRequest";
import ProductCategoryGetAllRequest from "@/libs/Api/Product/ProductCategoryGetAllRequest";
import ToastificationContent from "@core/components/toastification/ToastificationContent";
import ProductUpdateRequest from "@/libs/Api/Product/ProductUpdateRequest";

export default {
  title: 'اطلاعات محصول - پنل ادمین بانو کلاب',
  data(){
    return{
      product:null,
      showOverlay:false,
      productId:this.$route.params.id,
      base64ImageSrc:null,
      categories:null,
      isFreeOptions:[
        {label : 'رایگان' , value : true},
        {label : 'پولی' , value : false}
      ],
      types:[
        {label : 'دوره' , value : 1},
        {label : 'فیلم' , value : 2},
        {label : 'فایل' , value : 3},
        {label : 'پادکست' , value : 4},
      ],
    }
  },
  async created(){
    await this.getProduct();
    await this.getAllProductCategories();
  },
  methods:{
    async getProduct(){
      let _this = this;

      let productGetByIdRequest = new ProductGetByIdRequest(_this);
      productGetByIdRequest.setId(_this.productId);
      await productGetByIdRequest.fetch(function (content){
        _this.product = content;
        _this.base64ImageSrc = 'https://api.halamkhoobee.com/'+_this.product.imageFile;
        _this.product.imageFile = '';
      },function (error){
        console.log(error)
      })
    },
    async getAllProductCategories(){
      let  _this = this;
      let  defaultGetData={
        pageNumber:0,
        count:0,
      }

      let productCategoryGetAllRequest = new ProductCategoryGetAllRequest(_this);
      productCategoryGetAllRequest.setParams(defaultGetData);
      await productCategoryGetAllRequest.fetch(function (content){
        _this.categories = content.productCategories;
      },function (error){
        console.log(error)
      })
    },
    async updateProduct(){
      let  _this = this;
      _this.showOverlay = true

      let productUpdateRequest = new ProductUpdateRequest(_this);
      productUpdateRequest.setParams(_this.product);
      await productUpdateRequest.fetch(function (content){
        _this.showOverlay = false;
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `عملیات انجام شد.`,
          },
        })
        window.location.reload()
      }, function (error){
        _this.overlay = false;
        console.log(error)
      })
    },
    async readAsDataURL(file) {
      return new Promise((resolve, reject) => {
        const fr = new FileReader();
        fr.onerror = reject;
        fr.onload = () => {
          resolve(fr.result.split(",")[1]);
        }
        fr.readAsDataURL(file);
      });
    },
    async makeBase64Pic(e){
      const _this = this;
      let file = e.target.files[0]
      _this.base64ImageSrc = URL.createObjectURL(file)
      const result = await _this.readAsDataURL(file);
      _this.product.imageFile = result;
    },
    deleteProductPic(){
      this.product.imageFile = '';
      this.base64ImageSrc = null
    },
    async makeBase64(e){
      const _this = this;
      let file = e.target.files[0]
      const result = await _this.readAsDataURL(file);
      _this.product.trailerFile = result;
      console.log(_this.product.trailerFile)
    },
  },
  directives: {
    Ripple,
  },
  components: {
    // BSV
    BCard,
    BCardBody,
    BRow,
    BCol,
    BImg,
    BCardText,
    BLink,
    BButton,
    BDropdown,
    BDropdownItem,
    BAlert,
    BTab,
    BTabs,
    BForm,
    BFormGroup,
    BFormInput,
    BFormTextarea,
    BMedia,
    BAvatar,
    BOverlay,
    vSelect,

    // SFC
    ECommerceProductDetailsItemFeatures,
    ECommerceProductDetailsRelatedProducts,
  },

}
</script>

<style lang="scss">
@import "~@core/scss/base/pages/app-ecommerce-details.scss";
</style>
