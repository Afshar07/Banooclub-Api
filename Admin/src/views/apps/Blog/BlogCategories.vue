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
              type="text"
              placeholder="نام دسته بندی"
              v-model="SelectedCategory.name"
          />

        </b-form-group>

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

      </b-modal>
      <!-- Table Container Card -->
      <b-card
          no-body
          class="mb-0"
      >
        <b-col lg="12" md="12" class="my-2">
          <b-row>
<!--            <b-col lg="10" md="12" class="my-2">-->
<!--              <b-form-input-->
<!--                  id="basicInput"-->
<!--                  v-model="search"-->
<!--                  placeholder="جستجو بر اساس نام دسته بندی"-->
<!--              />-->
<!--            </b-col>-->
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
  BBadge, BDropdown, BDropdownItem, BPagination, BOverlay, BModal, BFormGroup,BFormSelect
} from 'bootstrap-vue'
import vSelect from 'vue-select'

import BlogCategoryGetAllRequest from '@/libs/Api/Blog/BlogCategoryGetAllRequest'
import BlogCategoryDeleteRequest from '@/libs/Api/Blog/BlogCategoryDeleteRequest'
import BlogCategoryCreateRequest from '@/libs/Api/Blog/BlogCategoryCreateRequest'
import BlogCategoryUpdateRequest from '@/libs/Api/Blog/BlogCategoryUpdateRequest'
import ToastificationContent from '@core/components/toastification/ToastificationContent'

export default {
  title: 'لیست دسته بندی مقالات  - پنل ادمین بانو کلاب',
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
          key: 'blogCategoryId',
          label: 'شناسه'
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
      SelectedCategory: null,

    }
  },
  async created() {
    await this.GetAllBlogCategory()
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
    BModal,
    BFormSelect,
    vSelect,
  },
  watch: {
    search: function () {
      this.GetAllBlogCategory()
    },
    currentPage: function () {
      this.GetAllBlogCategory()
    }
  },
  methods: {
    async UpdateCategory(){
      let _this = this
      let blogCategoryUpdateRequest = new BlogCategoryUpdateRequest(_this)
      let data = {
        blogCategoryId: _this.SelectedCategory.blogCategoryId,
        name: _this.SelectedCategory.name,
        seoTitle: _this.SelectedCategory.seoTitle,
        seoDescription: _this.SelectedCategory.seoDescription,
        seourl: _this.SelectedCategory.seourl
      }
      blogCategoryUpdateRequest.setData(data)
      await blogCategoryUpdateRequest.fetch(function (content) {
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
        _this.GetAllBlogCategory();
      }, function (error) {
        console.log(error)
      })
    },
    async AddCategory(){
      let _this = this
      if(_this.CategoryName!==''){
        let blogCategoryCreateRequest = new BlogCategoryCreateRequest(_this)
        let data = {
          name: this.CategoryName,
          seoDescription: this.CategoryName
        }
        blogCategoryCreateRequest.setRequestParamDataObj(data)
        await blogCategoryCreateRequest.fetch(function (content) {
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
          _this.GetAllBlogCategory();
          _this.CategoryName = ''
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
            text: `نام دسته بندی را وارد کنید`,
          },
        })
      }


    },
    async  DeleteCategory(){
      let _this = this
      let blogCategoryDeleteRequest = new BlogCategoryDeleteRequest(_this)
      blogCategoryDeleteRequest.setId(this.SelectedCategory.blogCategoryId)
      await blogCategoryDeleteRequest.fetch(function (content) {
        console.log(content)
        if(content ===0){
          alert(111)
          _this.$toast({
            component: ToastificationContent,
            position: 'bottom-center',
            props: {
              title: `عملیات ناموفق`,
              icon: 'CheckIcon',
              variant: 'success',
              text: `این دسته بندی مقاله فعال دارد`,
            },
          })
        }else{
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
          _this.GetAllBlogCategory();


        }


      }, function (error) {
        console.log(error)
      })
    },
    SetSelectedCategory(item) {
      this.SelectedCategory = item
    },

    async GetAllBlogCategory() {
      let _this = this
      let blogCategoryGetAllRequest = new BlogCategoryGetAllRequest(_this)

      await blogCategoryGetAllRequest.fetch(function (content) {
        _this.Categories = content

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
