<template>
  <b-overlay
      :show="Blogs === null"
      rounded="sm"
  >
    <div v-if="Blogs !== null">
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



          <template #cell(Edit)="data">

              <feather-icon icon="EyeIcon" size="20" class="text-primary" @click="SetBlogData(data.item)" />


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
        <div v-if="SelectedBlog!==null" class="mx-2 mb-2">
          <b-row>
            <div class="col-md-6">
              <div  class="row">
                <div class="col-md-6">
                  <small class="text-secondary my-1">سئو url</small>
                  <input v-model="SelectedBlog.seourl" type="text" placeholder="url سئو" maxlength="50" class="form-control">
                  <span :class="{'text-lightPink':SelectedBlog.seourl.length===60}"><small>{{SelectedBlog.seourl.length}}</small><small class="text-secondary">/</small><small class="text-secondary">50</small></span>
                </div>
                <div class="col-md-6 my-lg-0 my-md-0 my-3">
                  <small class="text-secondary my-1">تیتر سئو </small>
                  <input v-model="SelectedBlog.seoTitle" type="text" placeholder="تیتر سئو" class="form-control" maxlength="60">
                  <span :class="{'text-lightPink':SelectedBlog.seoTitle.length===60}"><small>{{SelectedBlog.seoTitle.length}}</small><small class="text-secondary">/</small><small class="text-secondary">60</small></span>
                </div>
                <div class="col-md-12 my-3">
                  <small class="text-secondary my-1">توضیحات سئو </small>
                  <textarea v-model="SelectedBlog.seoDescription" class="form-control" placeholder="توضیحات سئو" maxlength="160"></textarea>
                  <span :class="{'text-lightPink':SelectedBlog.seoDescription.length===160}"><small>{{SelectedBlog.seoDescription.length}}</small><small class="text-secondary">/</small><small class="text-secondary">160</small></span>
                </div>
                <div class="col-md-3">
                  <button class="btn btn-sm btn-success" @click="UpdateSeo">
                    <small>ذخیره تغییرات</small>
                  </button>
                </div>
              </div>
            </div>
            <div class="col-md-6 my-3">
              <!--- serp Desktop --->
              <div  class="serp-preview d-lg-flex p-3 d-none flex-column align-items-end">
                <div class="serp-title">
                  <h4>
                    {{SelectedBlog.seoTitle}}
                  </h4>
                </div>
                <div class="serp-url">
                  <h5 class="d-flex align-items-center flex-wrap">
                    <span style="overflow-wrap: anywhere">{{SelectedBlog.seourl.replaceAll(' ','-')}}/</span>
                    <span>
                             https://banooclub.com/BlogDetail
                        <span class="serp-arrow"></span>
                      </span>

                  </h5>

                </div>
                <div class="serp-description">
                  <h6 style="overflow-wrap: anywhere">
                    {{ SelectedBlog.seoDescription }}
                  </h6>
                </div>
              </div>
              <!--- serp Mobile --->
              <div class="serp-preview d-lg-none  p-3 d-flex flex-column align-items-end">
                <div class="serp-title">
                  <h6>
                    {{SelectedBlog.seoTitle}}
                  </h6>
                </div>
                <div class="serp-url d-flex align-items-center flex-wrap" >

                  <small >{{SelectedBlog.seourl.replaceAll(' ','-')}}/</small>
                  <small>
                    https://banooclub.com/BlogDetail
                    <small class="serp-arrow"></small>
                  </small>




                </div>
                <div class="serp-description">
                  <small>
                    {{ SelectedBlog.seoDescription }}
                  </small>
                </div>
              </div>
            </div>
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

import BlogGetAllRequest from '@/libs/Api/Blog/BlogGetAllReqeust'
import BlogUpdateSeoRequest from '@/libs/Api/Blog/BlogSeoUpdateRequest'

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
  title: 'سئو مقالات  - پنل ادمین بانو کلاب',
  name: 'UsersList',
  data() {
    return {
      Blogs: null,
      totalCount: null,
      showDeleteModal: false,
      currentPage: 1,
      deleteItem: null,
      perPage: 5,
      seoUrl:'',
      seoTitle:'',
      seoDescription:'',
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
          key: 'Edit',
          label: 'ویرایش'
        },
        // { key: 'parentId'},
      ],
      SelectedCategoryId:0,
      pageNumber: 1,
      count: 10,
      search: '',
      SelectedBlog: null,
      tagId:0,
      adminId:0

    }
  },
  async created() {
    await this.GetAllBlogs()
    let admin = JSON.parse(localStorage.getItem('userData'))
    this.adminId = admin.data.user.userId
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

    SetBlogData(item) {
      this.SelectedBlog = item
    },
    async UpdateSeo(){
      let _this = this
      let blogUpdateSeoRequest = new BlogUpdateSeoRequest(_this)
let data ={
  userId: this.adminId,
  blogId:this.SelectedBlog.blogId ,
  description:this.SelectedBlog.description,
  summary:this.SelectedBlog.summary,
  title:this.SelectedBlog.title,
  seoTitle: this.SelectedBlog.seoTitle,
  seoDescription: this.SelectedBlog.seoDescription,
  seourl: this.SelectedBlog.seourl.replaceAll(' ','-'),
}
      blogUpdateSeoRequest.setParams(data)
    await  blogUpdateSeoRequest.fetch(()=>{
      _this.$toast({
        component: ToastificationContent,
        position: 'bottom-center',
        props: {
          title: `عملیات موفق`,
          icon: 'CheckIcon',
          variant: 'success',
          text: `سئو مقاله با موفقیت بروزرسانی شد.`,
        },
      })
    _this.GetAllBlogs();
    _this.SelectedBlog = null
    },()=>{

    })
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


  },
}
</script>

<style scoped>
label {
  display: inline-block;
  width: 180px;
  margin-bottom: .625em;
}


.serp-arrow {
  border-bottom-color: rgb(0, 102, 33);
  border-bottom-style: solid;
  border-bottom-width: 0px;
  border-left-color: rgba(0, 0, 0, 0);
  border-left-style: solid;
  border-left-width: 4px;
  border-right-color: rgba(0, 0, 0, 0);
  border-right-style: solid;
  border-right-width: 4px;
  border-top-color: rgb(0, 102, 33);
  border-top-style: solid;
  border-top-width: 5px;
  color: rgb(128, 128, 128);
  cursor: default;
  font-family: arial, sans-serif;
  font-size: 11px;
  font-weight: bold;
  height: 0px;
  position: absolute;
  line-height: 27px;
  margin-left: 3px;
  margin-top: 6px;
  text-align: center;
  user-select: none;
  visibility: visible;
  white-space: nowrap;
  width: 0px;
}

.serp-title {
  color: rgb(26, 13, 171);
  cursor: pointer;
  font-family: arial, sans-serif;
  font-size: 18px;
  font-weight: normal;
  line-height: 21.6px;
  text-align: left;
  text-decoration: none;
  visibility: visible;
  white-space: nowrap;
}

.serp-url {
  color: rgb(0, 102, 33);
  font-family: arial, sans-serif;
  font-size: 14px;
  font-style: normal;
  font-weight: normal;
  line-height: 16px;
  text-align: left;
  visibility: visible;
  white-space: nowrap;
}

.serp-description {
  color: rgb(84, 84, 84);
  font-family: arial, sans-serif;
  font-size: 13px;
  font-weight: normal;
  line-height: 18.2px;
  text-align: left;
  visibility: visible;
  word-wrap: break-word;
}
</style>
