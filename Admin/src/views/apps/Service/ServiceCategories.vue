<template>
  <b-overlay
      :show="Categories === null"
      rounded="sm"
  >
    <div v-if="Categories !== null">
      <b-modal
          id="modal-delete"
          centered
          ok-title="بله"

          cancelTitle="خیر"
          @ok="DeleteCategory"
      >
        <span>آیا از حذف کردن این دسته بندی اطمینان دارید ؟ </span>
      </b-modal>


      <b-modal
          id="modal-edit"
          centered
          ok-title="بروزرسانی دسته بندی"
          ok-only
          @ok.preventDefault()="UpdateCategory"
      >
        <b-row v-if="SelectedCategory!==null">
          <b-col cols="12">
            <b-form-group>
              <label >نام دسته بندی</label>
              <b-form-input
                  type="text"
                  placeholder="نام دسته بندی"
                  v-model="SelectedCategory.title"
              />
            </b-form-group>
          </b-col>
          <b-col cols="12">
            <b-form-group>
              <label >توضیحات دسته بندی</label>
              <b-form-input
                  type="text"
                  placeholder="توضیحات دسته بندی"
                  v-model="SelectedCategory.description"
              />
            </b-form-group>
          </b-col>
          <b-col cols="12">
            <div class="d-flex align-items-center gap-3 my-25">
              <b-form-checkbox
                  v-model="SelectedCategory.isFunTime"

              >
              </b-form-checkbox>
              <label>دسته بندی شادی تایم</label>
            </div>
          </b-col>
          <b-col md="12">
            <b-form-group
                label="عکس دسته بندی"
                label-for="mc-last-name"
            >
              <b-form-file
                  ref="FileUpdate"
                  @input="HandleFileUpdate"
                  placeholder="Choose a file or drop it here..."
                  drop-placeholder="Drop file here..."
              />
            </b-form-group>
            <div class="position-relative" v-if="SelectedCategory.fileData!==''">

              <img v-if="BaseImgUrl===''" :src="`https://subapi.banoclub.com/media/gallery/serviceCategory/${SelectedCategory.fileData}`" width="100px" height="100px" alt="">
              <img v-else :src="BaseImgUrl" width="100px" height="100px" alt="">
            </div>


          </b-col>
        </b-row>




      </b-modal>

      <b-modal
          id="modal-Add"
          centered
          ok-title="افزودن دسته بندی"
          ok-only

          @ok.preventDefault()="AddCategory"
      >
        <b-row>
          <b-col cols="12">
            <b-form-group>
              <label for="email">نام دسته بندی</label>
              <b-form-input
                  type="text"
                  placeholder="نام دسته بندی"
                  v-model="CategoryName"
              />
            </b-form-group>
          </b-col>
          <b-col cols="12">
              <div class="d-flex align-items-center gap-3 my-25">
                <b-form-checkbox
                    v-model="isFunTime"
                >
                </b-form-checkbox>
                <label>دسته بندی شادی تایم</label>
              </div>
          </b-col>
          <b-col cols="12">
            <b-form-group>
              <label for="email">توضیحات دسته بندی</label>
              <b-form-input
                  type="text"
                  placeholder="توضیحات دسته بندی"
                  v-model="CategoryDescription"
              />
            </b-form-group>
          </b-col>
          <b-col md="12">
            <b-form-group
                label="عکس دسته بندی"
                label-for="mc-last-name"
            >
              <b-form-file
                  ref="File"
                  @input="HandleFile"
                  placeholder="Choose a file or drop it here..."
                  drop-placeholder="Drop file here..."
              />
            </b-form-group>
            <div class="position-relative" v-if="BaseImgUrl!==''">
              <feather-icon icon="TrashIcon" size="20" class="text-danger position-absolute" @click="RemovePic"></feather-icon>
              <img :src="BaseImgUrl" width="100px" height="100px" alt="">
            </div>


          </b-col>
        </b-row>




      </b-modal>
      <!-- Table Container Card -->
      <b-card
          no-body
          class="mb-0"
      >
        <b-col lg="12" md="12" class="my-2">
          <b-row>
            <b-col lg="10" md="12" class="my-2">
              <b-form-input
                  id="basicInput"
                  v-model="search"
                  placeholder="جستجو بر اساس نام دسته بندی"
              />
            </b-col>
            <b-col lg="2" md="12" class="my-2">
              <b-button

                  variant="success"
                  v-b-modal.modal-Add

              >
                افزودن دسته بندی جدید
              </b-button>
            </b-col>
          </b-row>
        </b-col>


        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="Categories"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >

          <!-- Column: delete -->

          <template #cell(Delete)="data">

            <div class="cursor-pointer d-flex flex-row"
                 v-b-modal.modal-delete
                 @click="SetSelectedCategory(data.item)"
            >
              <feather-icon icon="TrashIcon" size="20" class="text-danger" />
            </div>

          </template>
          <template #cell(fileData)="data">

            <img v-if="data.item.fileData" :src="`https://subapi.banoclub.com/media/gallery/serviceCategory/${data.item.fileData}`" width="50px" height="50px" alt="">

          </template>
          <template #cell(isFunTime)="data">

           <feather-icon v-if="data.item.isFunTime" icon="CheckIcon" class="text-success"></feather-icon>
            <feather-icon v-else icon="XIcon" class="text-danger"></feather-icon>
          </template>
          <template #cell(Edit)="data">

            <div class="cursor-pointer d-flex flex-row"
                 v-b-modal.modal-edit
                 @click="SetSelectedCategory(data.item)"
            >
              <feather-icon class="text-primary" icon="EditIcon" size="20" />
            </div>

          </template>

        </b-table>
        <div class="mx-2 mb-2">
          <b-row>
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


  </b-overlay>
</template>

<script>

import {
  BCard, BRow, BCol, BFormInput, BButton, BTable, BMedia, BAvatar, BLink,BFormFile,
  BBadge, BDropdown, BDropdownItem, BPagination, BOverlay, BModal, BFormGroup,BFormSelect,BFormCheckbox
} from 'bootstrap-vue'
import vSelect from 'vue-select'

import ServiceCategoryGetAllRequest from '@/libs/Api/Service/ServiceCategoryGetAllRequest'
import ServiceCategoryDeleteRequest from '@/libs/Api/Service/ServiceCategoryDeleteRequest'
import ServiceCategoryCreateRequest from '@/libs/Api/Service/ServiceCategoryCreateRequest'
import ServiceCategoryUpdateRequest from '@/libs/Api/Service/ServiceCategoryUpdateRequest'
import ToastificationContent from '@core/components/toastification/ToastificationContent'

export default {
  title: 'لیست دسته بندی خدمات  - پنل ادمین بانو کلاب',
  name: 'UsersList',
  data() {
    return {
      Categories: null,
      isFunTime:false,
      totalCount: null,
      perPageOptions: [10, 20, 30, 40, 50],
      myTableColumns: [
        {
          key: 'serviceCategoryId',
          label: 'شناسه'
        },
        {
          key: 'title',
          label: 'نام'
        },
        {
          key: 'fileData',
          label: 'عکس'
        },
        {
          key: 'description',
          label: 'توضیحات'
        },
        {
          key: 'isFunTime',
          label: 'شادی تایم'
        },

        {
          key: 'Delete',
          label: 'حذف'
        },
        {
          key: 'Edit',
          label: 'ویرایش'
        },
        // { key: 'parentId'},
      ],
      showDeleteModal: false,
      currentPage: 1,
      deleteItem: null,
      perPage: 10,
      CategoryName:'',
      BaseImgUrl:'',
      images:'',
      CategoryDescription:'',
      search: '',
      SelectedCategory: null,

    }
  },
  async created() {
    await this.GetAllCategory()
  },
  components: {

    BCard,
    BRow,
    BCol,
    BFormInput,
    BFormGroup,
    BFormCheckbox,
    BFormFile,
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
    BFormSelect,
    vSelect,
  },
  watch: {
    search: function () {
      this.GetAllCategory()
    },
    currentPage: function () {
      this.GetAllCategory()
    }
  },
  methods: {
    RemovePic(){
      this.BaseImgUrl = ''
      this.images = ''
    },
    HandleFile(){
      const that = this
      let f = ''

      f = this.$refs.File.files[0]
      this.BaseImgUrl = URL.createObjectURL(f)
      const reader = new FileReader()
      reader.onload = (function (theFile) {
        return function () {
          const binaryData = reader.result
          that.images = window.btoa(binaryData)
        }
      })(f)
      reader.readAsBinaryString(f)


    },
    HandleFileUpdate(){
      const that = this
      let f = ''

      f = this.$refs.FileUpdate.files[0]
      this.BaseImgUrl = URL.createObjectURL(f)
      const reader = new FileReader()
      reader.onload = (function (theFile) {
        return function () {
          const binaryData = reader.result
          that.images = window.btoa(binaryData)
        }
      })(f)
      reader.readAsBinaryString(f)


    },
    async UpdateCategory(){
      let _this = this
      let serviceCategoryUpdateRequest = new ServiceCategoryUpdateRequest(_this)
      let data = {
        serviceCategoryId:this.SelectedCategory.serviceCategoryId,
        createDate:new Date(Date.now()),
        title: this.SelectedCategory.title,
        isFunTime:this.SelectedCategory.isFunTime,
        description: this.SelectedCategory.description,
        seoTitle: this.SelectedCategory.seoTitle,
        seoDescription: this.SelectedCategory.seoDescription,
        fileData: this.images
      }
      serviceCategoryUpdateRequest.setData(data)
      await serviceCategoryUpdateRequest.fetch(function (content) {
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `دسته بندی بروزرسانی شد`,
          },
        })
        _this.SelectedCategory = null
        _this.BaseImgUrl = ''
        _this.images = ''
        _this.GetAllCategory();
      }, function (error) {
        console.log(error)
      })
    },
    async AddCategory(){

      let _this = this
      if(_this.CategoryName!=='' && _this.CategoryDescription!==''){
        let serviceCategoryCreateRequest = new ServiceCategoryCreateRequest(_this)
        let data = {
          createDate:new Date(Date.now()),
          title: this.CategoryName,
          description: this.CategoryDescription,
          seoTitle: this.CategoryName,
          isFunTime:this.isFunTime,
          seoDescription: this.CategoryDescription,
          fileData: this.images
        }
        serviceCategoryCreateRequest.setParams(data)
        await serviceCategoryCreateRequest.fetch(function (content) {
          _this.$toast({
            component: ToastificationContent,
            position: 'bottom-center',
            props: {
              title: `عملیات موفق`,
              icon: 'CheckIcon',
              variant: 'success',
              text: `دسته بندی ساخته شد شد`,
            },
          })
          _this.GetAllCategory();
          _this.CategoryName = ''
          _this.CategoryDescription = ''
          _this.BaseImgUrl = ''
          _this.images = ''
        }, function (error) {
          console.log(error)
        })
      }else{
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات ناموفق`,
            icon: 'AlertCircleIcon',
            variant: 'danger',
            text: `لطفا نام و توضیحات دسته بندی را وارد کنید`,
          },
        })
      }


    },
    async  DeleteCategory(){
      let _this = this
      let serviceCategoryDeleteRequest = new ServiceCategoryDeleteRequest(_this)
      serviceCategoryDeleteRequest.setId(this.SelectedCategory.serviceCategoryId)
      await serviceCategoryDeleteRequest.fetch(function (content) {
        if(content===true){
          _this.$toast({
            component: ToastificationContent,
            position: 'bottom-center',
            props: {
              title: `عملیات موفق`,
              icon: 'CheckIcon',
              variant: 'success',
              text: `دسته بندی  حذف شد.`,
            },
          })
          _this.GetAllCategory();
        }

      }, function (error) {
        console.log(error)
      })
    },
    SetSelectedCategory(item) {
      this.SelectedCategory = item
    },

    async GetAllCategory() {
      let _this = this
      let serviceCategoryGetAllRequest = new ServiceCategoryGetAllRequest(_this)
      let data = {
        pageNumber:_this.currentPage,
        search:_this.search
      }
      serviceCategoryGetAllRequest.setParams(data)
      await serviceCategoryGetAllRequest.fetch(function (content) {
        _this.Categories = content.serviceCategories

      }, function (error) {
        console.log(error)
      })
    },
  },
}
</script>

<style scoped>

</style>
