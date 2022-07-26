<template>
  <b-overlay
      :show="categories === null"
      rounded="sm"
  >
    <div v-if="categories !== null">

      <!-- Table Container Card -->
      <b-card
          no-body
          class="mb-0"
      >

        <div class="m-2">

          <!-- Table Top -->
          <b-row>

            <b-col
                cols="12"
                class="d-flex align-items-center justify-content-start mb-1 mb-md-0"
            >
              <b-button
                  variant="primary"
                  @click="addNewCategory = !addNewCategory"
              >
                <span class="text-nowrap">+ ساخت دسته بندی جدید</span>
              </b-button>
            </b-col>
            <!--   new Category name  -->
            <transition name="fade">
              <b-col
                  v-if="addNewCategory"
                  cols="12"
                  class="d-flex align-items-center justify-content-start mb-1 mb-md-0 mt-1"
              >
                <b-col md="8">
                  <div class="form-label-group">
                    <b-form-input
                        id="name"
                        placeholder="نام دسته بندی"
                        v-model="defaultCreateData.name"
                    />
                  </div>
                </b-col>
                <b-col md="2" class="mb-1">
                  <b-button
                      @click="createProductCategory"
                      variant="success"
                  >
                    <span class="text-nowrap">ساخت</span>
                  </b-button>
                </b-col>
              </b-col>
            </transition>
          </b-row>

        </div>

        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="categories"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >

          <!-- Column: delete -->

          <template #cell(delete)="data">

            <div class="cursor-pointer d-flex flex-row"
                 v-b-modal.modal-center
                 @click="showModal(data.item.productCategoryId)"
            >
              <feather-icon icon="TrashIcon" size="20" class="text-danger" />
            </div>

          </template>

          <template #cell(edit)="data">

            <router-link :to="{ name: 'apps-e-commerce-categories-edit', params: { id: data.item.productCategoryId } }" class="cursor-pointer d-flex flex-row ">
              <feather-icon icon="EditIcon" size="20" />
            </router-link>

          </template>

        </b-table>
        <div class="mx-2 mb-2">
          <b-row>

            <b-col
                cols="12"
                sm="6"
                class="d-flex align-items-center justify-content-center justify-content-sm-start"
            >
              <!--            <span class="text-muted">Showing {{ dataMeta.from }} to {{ dataMeta.to }} of {{ dataMeta.of }} entries</span>-->
            </b-col>
            <!-- Pagination -->
            <b-col
                cols="12"
                sm="6"
                class="d-flex align-items-center justify-content-center justify-content-sm-end"
            >

              <b-pagination
                  v-model="currentPage"
                  :total-rows="totalCount"
                  :per-page="perPage"
                  first-number
                  last-number
                  class="mb-0 mt-1 mt-sm-0"
                  prev-class="prev-item"
                  next-class="next-item"
              >
                <template #prev-text>
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
        </div>
      </b-card>
    </div>
    <!-- modal vertical center -->
    <b-modal
        id="modal-center"
        centered
        ok-title="حذف"
        cancelTitle="انصراف"
        @cancel="showDeleteModal = !showDeleteModal"
        @ok="deleteCategory(deleteItem)"

    >
      <span>حذف شود؟</span>
    </b-modal>
  </b-overlay>
</template>

<script>
import ProductCategoryGetAllRequest from "@/libs/Api/Product/ProductCategoryGetAllRequest";
import {
  BCard, BRow, BCol, BFormInput, BButton, BTable, BMedia, BAvatar, BLink,
  BBadge, BDropdown, BDropdownItem, BPagination,BOverlay,BModal
} from 'bootstrap-vue'
import vSelect from 'vue-select'
import ProductCategoryCreateRequest from "@/libs/Api/Product/ProductCategoryCreateRequest";
import ProductCategoryDeleteRequest from "@/libs/Api/Product/ProductCategoryDeleteRequest";
import ToastificationContent from "@core/components/toastification/ToastificationContent";
import ProductCategoryGetAllProductCategoriesRequest
  from "@/libs/Api/Product/ProductCategoryGetAllProductCategoriesRequest";
import ProductCategoryGetAllCourseCategoriesRequest
  from "@/libs/Api/Product/ProductCategoryGetAllCourseCategoriesRequest";

export default {
  title: 'لیست دسته بندی های دوره ها - پنل ادمین بانو کلاب',
  name: "CourseCategories",
  data(){
    return{
      categories:null,
      totalCount:null,
      showDeleteModal:false,
      addNewCategory:false,
      currentPage:1,
      deleteItem : null,
      perPage:10,
      perPageOptions:[10, 20, 30 , 40 , 50],
      myTableColumns : [
        { key: 'productCategoryId',label:'شناسه'},
        { key: 'name',label:'نام'},
        { key: 'delete',label:'حذف'},
        { key: 'edit',label:'ویرایش'},
        // { key: 'parentId'},
      ],
      defaultGetData:{
        pageNumber:1,
        count:10,
      },
      defaultCreateData:{
        isDeleted:false,
        productCategoryId:0,
        parentId:0,
        name:'',
        isCourse:true,
      },
    }
  },
  async created(){
    await this.getAllProductCategories(this.defaultGetData.pageNumber,this.defaultGetData.count)
  },
  components:{

    BCard,
    BRow,
    BCol,
    BFormInput,
    BButton,
    BTable,
    BMedia,
    BAvatar,
    BLink,
    BBadge,
    BDropdown,
    BDropdownItem,
    BPagination,
    BOverlay,
    BModal,

    vSelect,
  },
  watch:{
    currentPage:function (nv,ov) {
      this.getAllProductCategories(nv,this.defaultGetData.count);
    },
  },
  methods:{
    async getAllProductCategories(pageNumber,count){
      let  _this = this;
      let  defaultGetData={
        pageNumber:pageNumber,
        count:count,
      }

      let productCategoryGetAllCourseCategoriesRequest = new ProductCategoryGetAllCourseCategoriesRequest(_this);
      productCategoryGetAllCourseCategoriesRequest.setParams(defaultGetData);
      await  productCategoryGetAllCourseCategoriesRequest.fetch(function (content){
        _this.categories = content.productCategories;
        _this.totalCount = content.productCategoriesCount;
      },function (error){
        console.log(error)
      })
    },
    async createProductCategory(){
      let _this = this;

      let productCategoryCreateRequest = new ProductCategoryCreateRequest(_this);
      productCategoryCreateRequest.setParams(_this.defaultCreateData);
      await productCategoryCreateRequest.fetch(function (content){
        _this.defaultCreateData.name = '';
        _this.addNewCategory =false;
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `دسته بندی جدید اضافه شد.`,
          },
        })
        _this.getAllProductCategories(_this.defaultGetData.pageNumber,_this.defaultGetData.count)
      },function (error){
        console.log(error)
      })
    },
    async deleteCategory(param){
      let _this = this;

      let productCategoryDeleteRequest = new ProductCategoryDeleteRequest(_this);
      productCategoryDeleteRequest.setId(param);
      await productCategoryDeleteRequest.fetch(function (content){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `دسته بندی جدید حذف شد.`,
          },
        })
        _this.getAllProductCategories(_this.defaultGetData.pageNumber,_this.defaultGetData.count)
      },function (error){
        console.log(error)
      })
    },
    showModal(param){
      let _this = this;

      _this.deleteItem = param
    },
  },
}
</script>

<style scoped>

</style>
