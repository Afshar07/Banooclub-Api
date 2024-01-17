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
          ok-title="ویرایش"
          ok-only

          @ok="UpdateCategory"
      >
        <b-form-group>
          <label for="email">نام دسته بندی</label>
          <b-form-input
              v-if="SelectedCategory!==null"
              id="email"
              class="mb-2 my-3"
              type="text"
              placeholder="نام دسته بندی"
              v-model="SelectedCategory.name"
          />

        </b-form-group>
        <b-form-group>
        <b-form-file
            ref="FileUpdate"
            @input="HandleFileUpdate2"
            placeholder="فایل خود را انتخاب کنید"
            drop-placeholder="Drop file here..."
        />
        </b-form-group>
        <div class="position-relative" v-if=" SelectedCategory!==null&&SelectedCategory.fileData!==null">
          <img v-if="BaseImgUrl===''" :src="`https://subapi.banoclub.com/media/gallery/adsCategory/${SelectedCategory.fileData}`" width="100px" height="100px" alt="">
          <img v-else :src="BaseImgUrl" width="100px" height="100px" alt="">
        </div>
      </b-modal>
      <b-modal
          id="modal-Add"
          centered
          ok-title="افزودن دسته بندی"
          ok-only

          @ok.preventDefault()="AddCategory"
      >
        <b-form-group>
          <label for="email">نام دسته بندی</label>
          <b-form-input
              type="text"
              placeholder="نام دسته بندی"
              v-model="CategoryName"
          />
        </b-form-group>

        <b-form-group
            label="عکس دسته بندی"

            label-for="mc-last-name"
        >
          <b-form-file
              ref="FileUpdate"
              @input="HandleFileUpdate"
              placeholder="فایل خود را انتخاب کنید"
              drop-placeholder="Drop file here..."
          />
        </b-form-group>
        <div v-if="BaseImgUrl!==''" class="position-relative">
          <feather-icon icon="TrashIcon" class="position-absolute text-danger"  @click="RemovePic"/>
          <img :src="BaseImgUrl" width="100px" height="100px" alt="">
        </div>
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
          <b-form-group

          >

          </b-form-group>
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
          <template #cell(fileData)="data">

            <img :src="`https://subapi.banoclub.com/media/gallery/adsCategory/${data.item.fileData}`" width="50px" height="50px" alt="">
          </template>
          <template #cell(Delete)="data">

            <div class="cursor-pointer d-flex flex-row"
                 v-b-modal.modal-delete
             @click="SetSelectedCategory(data.item)"
            >
              <feather-icon icon="TrashIcon" size="20" class="text-danger" />
            </div>

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


  </b-overlay>
</template>

<script>

import {
  BCard, BRow, BCol, BFormInput, BButton, BTable, BMedia, BAvatar, BLink,
  BBadge, BDropdown, BDropdownItem, BPagination, BOverlay, BModal, BFormGroup,BFormSelect,BFormFile
} from 'bootstrap-vue'
import vSelect from 'vue-select'

import GetAllAdsCategory from '@/libs/Api/ads/GetAllAdsCategory'
import DeleteAdCategory from '@/libs/Api/ads/DeleteAdCategory'
import AddAdCategory from '@/libs/Api/ads/AddAdCategory'
import UpdateAdCategory from '@/libs/Api/ads/UpdateAdCategory'
import ChangeStatus from '@/libs/Api/ads/ChangeStatus'
import ToastificationContent from '@core/components/toastification/ToastificationContent'

export default {
  title: 'لیست دسته بندی آگهی ها  - پنل ادمین بانو کلاب',
  name: 'UsersList',
  data() {
    return {
      Categories: null,
      totalCount: null,
      showDeleteModal: false,
      currentPage: 1,
      deleteItem: null,
      perPage: 5,
      CategoryName:'',
      perPageOptions: [10, 20, 30, 40, 50],
      myTableColumns: [
        {
          key: 'adsCategoryId',
          label: 'شناسه'
        },
        {
          key: 'fileData',
          label: 'عکس'
        },
        {
          key: 'name',
          label: 'نام'
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
      SelectedCategoryId:0,
      pageNumber: 1,
      count: 10,
      search: '',
      BaseImgUrl:'',
      images:'',
      SelectedCategory: null,
      selected: null,
      options: [
        { value: null, text: 'Please select an option' },
        { value: 'a', text: 'This is First option' },
        { value: 'b', text: 'Simple Option' },
        { value: { C: '3PO' }, text: 'This is an option with object value' },
        { value: 'd', text: 'This one is disabled', disabled: true },
      ],

    }
  },
  async created() {
    await this.GetAllAdsCategory()
  },
  components: {

    BCard,
    BRow,
    BCol,
    BFormInput,
    BFormGroup,
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
    BFormFile,
    BModal,
    BFormSelect,
    vSelect,
  },
  watch: {
    search: function () {
      this.GetAllAdsCategory()
    },
    currentPage: function () {
      this.GetAllAdsCategory()
    }
  },
  methods: {
    RemovePic(){
      this.BaseImgUrl = ''
      this.images = ''
    },

    HandleFileUpdate2(){
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
      let updateAdCategory = new UpdateAdCategory(_this)
      let data = {
        adsCategoryId: this.SelectedCategory.adsCategoryId,
        name: this.SelectedCategory.name,
        parentId: this.SelectedCategory.parentId,
        fileData:this.images
      }
      updateAdCategory.setRequestParamDataObj(data)
      await updateAdCategory.fetch(function (content) {
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
        _this.GetAllAdsCategory();
      }, function (error) {
        console.log(error)
      })
    },
    async AddCategory(){
      let _this = this
      let addAdCategory = new AddAdCategory(_this)
      let data = {
        parentId: 0,
        name: this.CategoryName,
        fileData:this.images
      }
      addAdCategory.setRequestParamDataObj(data)
      await addAdCategory.fetch(function (content) {
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `دسته بندی ساخته شد`,
          },
        })
        _this.images = ''
        _this.BaseImgUrl = ''
        _this.CategoryName = ''
        _this.GetAllAdsCategory();
      }, function (error) {
        console.log(error)
      })
    },
  async  DeleteCategory(){
      let _this = this
      let deleteAdCategory = new DeleteAdCategory(_this)
      let data = {
        id:this.SelectedCategory.adsCategoryId
      }
      deleteAdCategory.setParams(data)
      await deleteAdCategory.fetch(function (content) {
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
        _this.GetAllAdsCategory();

      }, function (error) {
        console.log(error)
      })
    },
    SetSelectedCategory(item) {
      this.SelectedCategory = item
    },

    async GetAllAdsCategory() {
      let _this = this
      let getAllAdsCategory = new GetAllAdsCategory(_this)
      let data = {
        pageNumber: _this.currentPage,
        count: _this.count,
        search: _this.search
      }
      getAllAdsCategory.setParams(data)
      await getAllAdsCategory.fetch(function (content) {
        _this.Categories = content.adsCategories
        _this.totalCount = content.adsCategoriesCount
      }, function (error) {
        console.log(error)
      })
    },

    showModal(param) {
      let _this = this

      _this.deleteItem = param
    },
  },
}
</script>

<style scoped>

</style>
