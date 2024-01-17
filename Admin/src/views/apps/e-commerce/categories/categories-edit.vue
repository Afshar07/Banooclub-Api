<template>
  <div v-if="category !== null">
    <b-card>

      <b-form>
        <b-row>

          <b-col
              cols="12"
              class="mb-1"
          >
            <h3>نام دسته بندی</h3>
          </b-col>

          <!-- Field: name -->
          <b-col
              cols="12"
              md="4"
          >
            <b-form-group
                label-for="name"
            >
              <b-form-input
                  id="name"
                  v-model="category.name"
              />
            </b-form-group>
          </b-col>
        </b-row>
      </b-form>

      <!-- Action Buttons -->
      <b-button
          variant="primary"
          class="mt-1 mb-sm-0 mr-0 mr-sm-1"
          :block="$store.getters['app/currentBreakPoint'] === 'xs'"
          @click="updateCategory"
      >
        ذخیره تغییرات
      </b-button>

    </b-card>

    <b-card>

      <b-row>
        <b-col
            cols="12"
            class="mb-1"
        >
          <h3>خصوصیات دسته بندی</h3>
        </b-col>

        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="categoryProperties"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="خصوصیتی یافت نشد."
        >

          <!-- Column: delete -->

          <template #cell(delete)="data">

            <div class="cursor-pointer d-flex flex-row w-25"
                 v-b-modal.modal-delete
                 @click="showModal(data.item.categoryPropertyId)"
            >
              <feather-icon icon="TrashIcon" class="text-danger" size="20"/>
            </div>

          </template>

          <template #cell(edit)="data">

            <b-button v-b-modal.modal-center @click="setUpdateCategoryPropertyData(data.item)" class="cursor-pointer d-flex flex-row">
              <feather-icon icon="EditIcon" size="20"/>
            </b-button>

          </template>

        </b-table>

        <b-col
            cols="12"
            class="d-flex align-items-center justify-content-start mb-1 mb-md-0"
        >
          <b-button
              variant="primary"
              @click="addNewCategoryProperty = !addNewCategoryProperty"
          >
            <span class="text-nowrap">+ ساخت خصوصیت جدید</span>
          </b-button>
        </b-col>
        <!--   new Category name  -->
        <transition name="fade">
          <b-col
              v-if="addNewCategoryProperty"
              cols="12"
              class="d-flex align-items-center justify-content-start mb-1 mb-md-0 mt-1"
          >
            <b-col md="8">
              <div class="form-label-group">
                <b-form-input
                    id="propertyName"
                    placeholder="نام خصوصیت"
                    v-model="defaultCreateCategoryPropertyData.name"
                />
              </div>
            </b-col>
            <b-col md="2" class="mb-1">
              <b-button
                  @click="createProductCategoryProperty"
                  variant="success"
              >
                <span class="text-nowrap">ساخت</span>
              </b-button>
            </b-col>
          </b-col>
        </transition>

      </b-row>

    </b-card>

    <!-- modal vertical center -->
    <b-modal
        v-if="updateCategoryPropertyData !== null"
        id="modal-center"
        centered
        title="ویرایش خصوصیت"
        ok-only
        ok-title="ثبت"
        @ok="updateCategoryProperty"

    >
      <b-form-group
          label="Name"
          label-for="name-input"
          invalid-feedback="Name is required"
      >
        <b-form-input
            id="name-input"
            v-model="updateCategoryPropertyData.name"
            required
        />
      </b-form-group>
    </b-modal>
    <!-- modal vertical center -->
    <b-modal
        id="modal-delete"
        centered
        ok-title="حذف"
        cancelTitle="انصراف"
        @cancel="showDeleteModal = !showDeleteModal"
        @ok="deleteCategoryProperty(deleteItem)"
    >
      <span>حذف شود؟</span>
    </b-modal>
  </div>
</template>

<script>
import ProductCategoryGetRequest from "@/libs/Api/Product/ProductCategoryGetRequest";
import ProductCategoryUpdateRequest from "@/libs/Api/Product/ProductCategoryUpdateRequest";
import {
  BCard, BRow, BCol, BFormInput, BButton, BTable, BMedia, BAvatar, BLink,
  BBadge, BDropdown, BDropdownItem, BPagination, BFormGroup, BForm,BModal,
} from 'bootstrap-vue'
import vSelect from "vue-select";
import ToastificationContent from "@core/components/toastification/ToastificationContent";
import ProductCategoryPropertyCreateRequest from "@/libs/Api/Product/ProductCategoryPropertyCreateRequest";
import ProductCategoryPropertyGetByCategoryIdRequest
  from "@/libs/Api/Product/ProductCategoryPropertyGetByCategoryIdRequest";
import ProductCategoryPropertyDeleteRequest from "@/libs/Api/Product/ProductCategoryPropertyDeleteRequest";
import ProductCategoryPropertyUpdateRequest from "@/libs/Api/Product/ProductCategoryPropertyUpdateRequest";

export default {
  title:"ویرایش دسته بندی - پنل ادمین بانو کلاب",
  name: "categories-edit",
  data(){
    return{
      category:null,
      categoryProperties:null,
      deleteItem:null,
      categoryId:this.$route.params.id,
      addNewCategoryProperty:false,
      myTableColumns : [
        { key: 'categoryPropertyId',label:'شناسه'},
        { key: 'name',label:'نام'},
        { key: 'delete',label:'حذف'},
        { key: 'edit',label:'ویرایش'},
      ],
      defaultCreateCategoryPropertyData:{
        isDeleted: false,
        categoryPropertyId: 0,
        categoryId: this.$route.params.id,
        name: ""
      },
      updateCategoryPropertyData:null,
    }
  },
  async created(){
    await this.getCategory();
    await this.getCategoryProperty();
  },
  methods:{
    async getCategory(){
      let _this = this;

      let productCategoryGetRequest = new ProductCategoryGetRequest(_this);
      productCategoryGetRequest.setId(_this.categoryId);
      await productCategoryGetRequest.fetch(function (content){
        _this.category = content
      },function (error){
        console.log(error)
      })
    },
    async getCategoryProperty(){
      let _this = this;

      let productCategoryPropertyGetByCategoryIdRequest = new ProductCategoryPropertyGetByCategoryIdRequest(_this);
      productCategoryPropertyGetByCategoryIdRequest.setId(_this.categoryId);
      await productCategoryPropertyGetByCategoryIdRequest.fetch(function (content){
        _this.categoryProperties = content
      },function (error){
        console.log(error)
      })
    },
    async updateCategory(){
      let _this = this;

      let productCategoryUpdateRequest = new ProductCategoryUpdateRequest(_this);
      productCategoryUpdateRequest.setParams(_this.category);
      await productCategoryUpdateRequest.fetch(function (content){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `تغییرات اعمال شد.`,
          },
        })
        _this.getCategory()
      },function (error){
        console.log(error)
      })
    },
    async createProductCategoryProperty(){
      let _this = this;

      let productCategoryPropertyCreateRequest = new ProductCategoryPropertyCreateRequest(_this);
      productCategoryPropertyCreateRequest.setParams(_this.defaultCreateCategoryPropertyData);
      await productCategoryPropertyCreateRequest.fetch(function (content){
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
        _this.getCategoryProperty();
      },function (error){
        console.log(error)
      })
    },
    async deleteCategoryProperty(param){
      let _this = this;

      let productCategoryPropertyDeleteRequest = new ProductCategoryPropertyDeleteRequest(_this);
      productCategoryPropertyDeleteRequest.setId(param);
      await productCategoryPropertyDeleteRequest.fetch(function (content){
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
        _this.getCategoryProperty();
      },function (error){
        console.log(error)
      })
    },
    async updateCategoryProperty(){
      let _this = this;

      let productCategoryPropertyUpdateRequest = new ProductCategoryPropertyUpdateRequest(_this);
      productCategoryPropertyUpdateRequest.setParams(_this.updateCategoryPropertyData);
      await productCategoryPropertyUpdateRequest.fetch(function (content){
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
        _this.getCategoryProperty();
      },function (error){
        console.log(error)
      })
    },
    setUpdateCategoryPropertyData(param){
      this.updateCategoryPropertyData = param;
    },
    showModal(param){
      let _this = this;
      _this.deleteItem = param
    },
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
    BFormGroup,
    BBadge,
    BForm,
    BDropdown,
    BDropdownItem,
    BPagination,
    BModal,

    vSelect,
  }
}
</script>

<style scoped>

</style>
