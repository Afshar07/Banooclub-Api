<template>
  <b-overlay
      :show="Blogs === null"
      rounded="sm"
  >
    <div v-if="Blogs !== null">
      <b-modal
          id="modal-delete"
          centered
          ok-title="بله"

          cancelTitle="خیر"
          @ok="DeleteBlog"
      >
        <span>آیا از حذف کردن این مقاله اطمینان دارید ؟ </span>

      </b-modal>
      <b-modal
          id="modal-View"
          centered
          ok-title="تغییر وضعیت"
          ok-only
          @ok="ChangeBlogStatus"
      >
        <span>تغییر وضعیت به </span>
        <v-select
            v-model="SelectedStatus"
            class="my-3"
            label="option"
            :options="BlogStatus"
            :reduce="option => option.value"
        />
      </b-modal>

      <!-- Table Container Card -->
      <b-card
          no-body
          class="mb-0"
      >
        <b-col lg="12" md="12" class="my-2">
          <b-row>
            <b-col lg="12" md="12" class="my-2">
              <b-form-input
                  id="basicInput"
                  v-model="search"
                  placeholder="جستجو بر اساس تیتر مقاله"
              />
            </b-col>

          </b-row>

        </b-col>


        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="Blogs"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >
          <template #cell(createDate)="data">

              <span>{{ new Date(data.item.createDate).toLocaleDateString('fa-IR') }}</span>


          </template>
          <!-- Column: delete -->

          <template #cell(Delete)="data">

            <div class="cursor-pointer d-flex flex-row"
                 v-b-modal.modal-delete
                 @click="SetSelectedBlog(data.item)"
            >
              <feather-icon icon="TrashIcon" size="20" class="text-danger" />
            </div>

          </template>
          <template #cell(status)="data">
            <b-badge v-if="data.item.status===1" variant="success">
             منتشر شده
            </b-badge>
            <b-badge v-if="data.item.status===2" variant="danger">
              آرشیو شده
            </b-badge>
          </template>

          <template #cell(View)="data">


              <feather-icon @click="SetSelectedBlog(data.item)" v-b-modal.modal-View icon="EyeIcon" size="20" class="text-primary" />


          </template>
          <template #cell(Edit)="data">

            <b-link class="cursor-pointer" :to="`/apps/Blog/EditBlog/${data.item.blogId}`">
              <feather-icon icon="EditIcon" size="20" class="text-primary" />
            </b-link>

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
import ChangeBlogStatus from "@/libs/Api/Blog/BlogChangeStatus";
import BlogGetAllRequest from '@/libs/Api/Blog/BlogGetAllReqeust'

import ToastificationContent from '@core/components/toastification/ToastificationContent'
import BlogDeleteRequest from '@/libs/Api/Blog/BlogDeleteRequest'

export default {
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
  title: 'لیست دسته بندی آگهی ها  - پنل ادمین بانو کلاب',
  name: 'UsersList',
  data() {
    return {
      Blogs: null,
      totalCount: null,
      showDeleteModal: false,
      currentPage: 1,
      deleteItem: null,
      perPage: 5,
      SelectedStatus:0,
      BlogStatus:[
        {
          option:'منتشر شده',
          value:1
        }  ,
        {
          option:'آرشیو شده',
          value:2
        }
      ],
      perPageOptions: [10, 20, 30, 40, 50],
      myTableColumns: [
        {
          key: 'blogId',
          label: 'شناسه'
        },
        {
          key: 'createDate',
          label: 'تاریخ ایجاد'
        },
        {
          key: 'title',
          label: 'نام'
        },
        {
          key: 'status',
          label: 'وضعیت'
        },
        {
          key: 'Delete',
          label: 'حذف'
        },
        {
          key: 'Edit',
          label: 'ویرایش'
        },
        {
          key: 'View',
          label: 'تغییر وضعیت'
        },
        // { key: 'parentId'},
      ],
      SelectedCategoryId:0,
      pageNumber: 1,
      count: 10,
      search: '',
      SelectedBlog: null,
      tagId:0

    }
  },
  async created() {
    await this.GetAllBlogs()
  },

  watch: {
    search: function () {
      this.GetAllBlogs()
    },
    currentPage: function () {
      this.GetAllBlogs()
    }
  },
  methods: {
    async ChangeBlogStatus(){
      let _this = this
      let changeBlogStatus = new ChangeBlogStatus(_this)
      let data ={
        status:this.SelectedStatus
      }
      changeBlogStatus.setParams(data)
      changeBlogStatus.setParamsWithBody([this.SelectedBlog.blogId])
      await changeBlogStatus.fetch(()=>{
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `وضعیت مقاله با موفقیت تغییر یافت`,
          },
        })
        _this.GetAllBlogs()
      },(e)=>{
        console.log(e)
      })
    },

    async  DeleteBlog(){
      let _this = this
      let deleteBlog = new BlogDeleteRequest(_this)

      deleteBlog.setId(this.SelectedBlog.blogId)
      await deleteBlog.fetch(function (content) {
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `مقاله حذف شد.`,
          },
        })
        _this.GetAllBlogs();

      }, function (error) {
        console.log(error)
      })
    },
    SetSelectedBlog(item) {
      this.SelectedBlog = item
    },

    async GetAllBlogs() {
      let _this = this
      let blogGetAllRequest = new BlogGetAllRequest(_this)
      let data = {
        pageNumber: _this.currentPage,
        count: _this.count,
        search: _this.search,
        tagId:_this.tagId
      }
      blogGetAllRequest.setParams(data)
      await blogGetAllRequest.fetch(function (content) {
        _this.Blogs = content.blogs
        _this.totalCount = content.blogsCount
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
