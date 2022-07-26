<template>
  <b-overlay
      :show="overlay"
      rounded="sm"
  >
   <div>
    <b-row>
      <b-col cols="12">
        <b-card>

          <h4>اطلاعات کلی</h4>

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
                  label="عنوان"
                  label-for="Title"
              >
                <b-form-input
                    id="Title"
                    v-model="defaultData.title"
                />
              </b-form-group>
            </b-col>

            <!-- Field: isFree -->
            <b-col
                cols="12"
                md="4"
            >
              <b-form-group
                  label="وضعیت"
                  label-for="user-status"
              >
                <v-select
                    v-model="defaultData.isFree"
                    :options="isFreeOptions"
                    :reduce="val => val.value"
                    :clearable="false"
                    input-id="user-status"
                />
              </b-form-group>
            </b-col>

            <!-- Field:  price -->
            <b-col
                v-if="!defaultData.isFree"
                cols="12"
                md="4"
            >
              <b-form-group
                  label="قیمت (تومان)"
                  label-for="price"
              >
                <b-form-input
                    id="price"
                    v-model="defaultData.price"
                    :aria-disabled="!defaultData.isFree"
                />
              </b-form-group>
            </b-col>

            <!-- Field:  description -->
            <b-col
                cols="12"
            >
              <b-form-group
                  label="توضیحات"
                  label-for="description"
              >
                <b-form-textarea
                    id="description"
                    v-model="defaultData.description"
                    rows="6"
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
                    v-model="defaultData.type"
                    :options="types"
                    :reduce="val => val.value"
                    :clearable="false"
                    input-id="productCategoryId"
                />
              </b-form-group>
            </b-col>

            <!-- Field: productCategoryId -->
            <b-col v-if="productCategories !== null && defaultData.type !== 0 && defaultData.type !== 1 "
                   cols="12"
                   md="4"
            >
              <b-form-group
                  label="دسته بندی"
                  label-for="productCategoryId"
              >
                <v-select
                    v-model="defaultData.productCategoryId"
                    :options="productCategories"
                    label="name"
                    :reduce="name => name.productCategoryId"
                    :clearable="false"
                    input-id="productCategoryId"
                />
              </b-form-group>
            </b-col>

            <!-- Field: productCategoryId -->
            <b-col v-if="courseCategories !== null && defaultData.type !== 0 && defaultData.type === 1 "
                   cols="12"
                   md="4"
            >
              <b-form-group
                  label="دسته بندی"
                  label-for="productCategoryId"
              >
                <v-select
                    v-model="defaultData.productCategoryId"
                    :options="courseCategories"
                    label="name"
                    :reduce="name => name.productCategoryId"
                    :clearable="false"
                    input-id="productCategoryId"
                />
              </b-form-group>
            </b-col>

            <!-- Field: courseId -->
            <b-col
                v-if="defaultData.type === 1"
                cols="12"
                md="4"
            >
              <b-form-group
                  label="انتخاب دوره"
                  label-for="type"
              >
                <v-select
                    v-model="defaultData.courseId"
                    :options="courses"
                    label="name"
                    :reduce="name => name.courseId"
                    :clearable="false"
                    input-id="productCategoryId"
                />
              </b-form-group>
            </b-col>

          </b-row>
        </b-form>

        <b-button
            variant="primary"
            class="mt-1 mb-sm-0 mr-0 mr-sm-1 col-12 col-md-2"
            :block="$store.getters['app/currentBreakPoint'] === 'xs'"
            @click="createProduct"
        >
          ثبت
        </b-button>

      </div>
    </b-card>

    <b-card>

      <h4 class="">تریلر</h4>

      <b-row>
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
            <span class="">بارگذاری</span>
          </b-button>

        </b-col>

        <b-col
            cols="12"
            md="4"
            class="pt-2"
        >
            <span class="text-danger">فایل مورد نظر باید کمتر از 25 مگابایت باشد.</span>
        </b-col>
      </b-row>

    </b-card>

        <!--  Category Properties  -->
        <b-card ref="categoryProperties" v-if="defaultData.productCategoryId !== null">

          <h4 class="text-primary">خصوصیات پیشفرض</h4>

          <hr class="my-2">

         <b-row>
           <b-col
               v-for="i in defaultData.properties"
               v-if="i.categoryPropertyId > 0"
               cols="8"
           >
             <b-form-group
                 label-cols="4"
                 label-size="lg"
                 :label="i.name"
                 :label-for="i.name"
             >
               <b-form-input
                   :id="i.name"
                   v-model="i.value"
               />
             </b-form-group>
           </b-col>

         </b-row>

          <b-row>

            <b-col cols="12">
              <h4 class="text-primary">خصوصیات اضافی</h4>

              <hr class="my-2">
            </b-col>

            <NewCategoryProperty
                v-for="i in defaultData.properties"
                :key="i.id"
                :propertyData="i"
            />

<!--            <b-col-->
<!--                v-for="(i,index) in defaultData.properties"-->
<!--                v-if="i.name === ''"-->
<!--                cols="12"-->
<!--            >-->
<!--              <b-row>-->

<!--                <b-form-group-->
<!--                    class="col-3"-->
<!--                    label="name"-->
<!--                    label-for=""-->
<!--                >-->
<!--                  <b-form-input-->

<!--                      v-model="i.name"-->
<!--                  />-->
<!--                </b-form-group>-->

<!--                <b-form-group-->
<!--                    class="col-5"-->
<!--                    label="value"-->
<!--                    label-for=""-->
<!--                >-->
<!--                  <b-form-input-->

<!--                      v-model="i.value"-->
<!--                  />-->
<!--                </b-form-group>-->

<!--              </b-row>-->
<!--            </b-col>-->
          </b-row>

          <b-button
              variant="primary"
              @click="defaultData.properties.push({'name':'','value':'','categoryPropertyId':0})"
          >
            <span class="text-nowrap">+ افزودن خصوصیت</span>
          </b-button>

        </b-card>
      </b-col>
    </b-row>
  </div>
  </b-overlay>
</template>

<script>
import {
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
  BOverlay
} from 'bootstrap-vue'
import vSelect from "vue-select";
import ProductCategoryGetAllRequest from "@/libs/Api/Product/ProductCategoryGetAllRequest";
import ProductCategoryPropertyGetByCategoryIdRequest
  from "@/libs/Api/Product/ProductCategoryPropertyGetByCategoryIdRequest";
import ProductCreateRequest from "@/libs/Api/Product/ProductCreateRequest";
import ProductCourseGetAllRequest from "@/libs/Api/Product/ProductCourseGetAllRequest";
import NewCategoryProperty from "@/views/apps/e-commerce/e-commerce-add/NewCategoryProperty";
import ToastificationContent from "@core/components/toastification/ToastificationContent";
import ProductCategoryGetAllProductCategoriesRequest
  from "@/libs/Api/Product/ProductCategoryGetAllProductCategoriesRequest";
import ProductCategoryGetAllCourseCategoriesRequest
  from "@/libs/Api/Product/ProductCategoryGetAllCourseCategoriesRequest";

export default {
  title:"افزودن محصول - پنل ادمین بانو کلاب",
  name: "ECommerceAdd",
  components:{
    NewCategoryProperty,
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
  },
  data(){
    return{
      overlay:false,
      propertiesData:[],
      defaultData:{
        isDeleted: false,
        productId: 0,
        title: "",
        description: "",
        productCategoryId: null,
        courseScheduleId: 0,
        courseId: null,
        price: "",
        isFree: true,
        type: 0,
        imageFile: "",
        fileData: null,
        trailerFile:'',
        viewCounts: 0,
        properties: []
      },
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
      base64ImageSrc:null,
      productCategories:null,
      courseCategories:null,
      categoryProperties:null,
      courses:null,
    }
  },
  computed:{
    defaultDataProductCategoryId(){
      return this.defaultData.productCategoryId;
    },
    defaultDataProductType(){
      return this.defaultData.type;
    },
    defaultDataImageFile(){
      return this.defaultData.imageFile
    }
  },
  watch:{
    defaultDataProductCategoryId(nv,ov){
      this.getCategoryProperties(nv)
    },
    defaultDataProductType(nv,ov){

    },
  },
  async created(){
    await this.getAllProductCategories();
    await this.getAllCourseCategories();
    await this.getAllCourses();
  },
  methods:{
    async getAllProductCategories(){
      let  _this = this;
      let  defaultGetData={
        pageNumber:0,
        count:0,
      }

      let productCategoryGetAllProductCategoriesRequest = new ProductCategoryGetAllProductCategoriesRequest(_this);
      productCategoryGetAllProductCategoriesRequest.setParams(defaultGetData);
      await  productCategoryGetAllProductCategoriesRequest.fetch(function (content){
        _this.productCategories = content.productCategories;
        console.log(_this.categories)
      },function (error){
        console.log(error)
      })
    },
    async getAllCourseCategories(){
      let  _this = this;
      let  defaultGetData={
        pageNumber:0,
        count:0,
      }

      let productCategoryGetAllProductCategoriesRequest = new ProductCategoryGetAllCourseCategoriesRequest(_this);
      productCategoryGetAllProductCategoriesRequest.setParams(defaultGetData);
      await  productCategoryGetAllProductCategoriesRequest.fetch(function (content){
        _this.courseCategories = content.productCategories;
        console.log(_this.categories)
      },function (error){
        console.log(error)
      })
    },
    async createProduct(){
      let  _this = this;
      _this.overlay = true

      let productCreateRequest = new ProductCreateRequest(_this);
      productCreateRequest.setParams(_this.defaultData);
      await productCreateRequest.fetch(function (content){
        _this.overlay = false;
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
        _this.$router.push({name:'apps-e-commerce-shop'})
        }, function (error){
        _this.overlay = false;
        console.log(error)
      })
    },
    async getCategoryProperties(param){
      let _this = this;

      let productCategoryPropertyGetByCategoryIdRequest = new ProductCategoryPropertyGetByCategoryIdRequest(_this);
      productCategoryPropertyGetByCategoryIdRequest.setId(param);
      await productCategoryPropertyGetByCategoryIdRequest.fetch(function (content){
        _this.categoryProperties = content
        _this.defaultData.properties = [];
        _this.categoryProperties.forEach(element => _this.defaultData.properties.push({'name':element.name,'val':'','categoryPropertyId':element.categoryPropertyId}))
        _this.$refs.categoryProperties.scrollIntoView({ behavior: "smooth" });
      },function (error){
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
    async getAllCourses(){
      let _this = this;
      let courseData = {
        pageNumber:0,
        searchCommand : '',
        count: 0
      }

      let productCourseGetAllRequest = new ProductCourseGetAllRequest(_this);
      productCourseGetAllRequest.setParams(courseData);
      await productCourseGetAllRequest.fetch(function (content){
        _this.courses = content.result.data.courses;
      },function (error){
        console.log(error);
      })
    },
    async makeBase64Pic(e){
      const _this = this;
      let file = e.target.files[0]
      _this.base64ImageSrc = URL.createObjectURL(file)
      const result = await _this.readAsDataURL(file);
      _this.defaultData.imageFile = result;
    },
    async makeBase64(e){
      const _this = this;
      let file = e.target.files[0]
      const result = await _this.readAsDataURL(file);
      _this.defaultData.trailerFile = result;
    },
    deleteProductPic(){
      this.defaultData.imageFile = '';
      this.base64ImageSrc = null
    },
  },
}
</script>

<style scoped>

</style>
