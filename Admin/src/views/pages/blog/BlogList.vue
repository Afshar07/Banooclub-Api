<template>
  <b-overlay
      :show="blogList === null"
      rounded="sm"
  >
   <content-with-sidebar class="blog-wrapper">

    <!-- blogs -->
    <b-row class="blog-list-wrapper">
      <b-col
        v-for="blog in blogList"
        :key="blog.blogId"
        md="6"
      >
        <b-card
          tag="article"
          no-body
        >
          <b-link :to="{ name: 'pages-blog-edit', params: { id: blog.blogId } }">
            <b-img
              :src="'https://api.halamkhoobee.com/'+blog.fileData"
              class="card-img-top"
            />
          </b-link>
          <b-card-body>
            <b-card-title>
              <b-link
                :to="{ name: 'pages-blog-edit', params: { id: blog.blogId } }"
                class="blog-title-truncate text-body-heading"
              >
                {{ blog.title }}
              </b-link>
            </b-card-title>
            <b-media no-body>
              <b-media-body>
                <feather-icon
                    icon="CalendarIcon"
                    class="mr-50"
                />
                <small class="text-muted">
                  {{blog.createDate.slice(11,16)}}
                  -
                  {{createJalaliDate(blog.createDate.slice(0,10))}}
                </small>
                <span class="text-muted ml-75 mr-50">|</span>
                <b-badge
                    pill
                    class="mr-75"
                    variant="primary"
                >
                  {{ blog.categoryName }}
                </b-badge>
              </b-media-body>
            </b-media>
            <b-card-text class=" mt-1 item-description">
              {{ blog.summary }}
            </b-card-text>
            <hr>
            <div class="d-flex justify-content-between align-items-center">
              <span >
                <div class="d-flex align-items-center text-body">
                  <feather-icon
                    icon="MessageSquareIcon"
                    class="mr-50"
                  />
                  <span class="font-weight-bold">{{ blog.commentsCount }} نظرات</span>
                  <span class="text-muted ml-75 mr-50">|</span>
                  <feather-icon
                      icon="EyeIcon"
                      class="mr-50"
                  />
                  <span class="font-weight-bold">{{ blog.viewCounts }} بازدید ها</span>
                </div>
              </span>
              <div>
                <b-link
                    class="font-weight-bold text-danger mr-1"
                    v-b-modal.modal-delete
                    @click="showModal(blog.blogId)"
                >
                  حذف
                </b-link>
                <b-link
                    :to="{ name: 'pages-blog-edit', params: { id: blog.blogId } }"
                    class="font-weight-bold"
                >
                  ویرایش
                </b-link>
              </div>
            </div>
          </b-card-body>
        </b-card>
      </b-col>
      <b-col cols="12">
        <!-- pagination -->
        <div class="my-2">
          <b-pagination
              v-model="currentPage"
              :total-rows="totalCount"
              :per-page="8"
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
        </div>
      </b-col>
    </b-row>

    <!--/ blogs -->

    <!-- sidebar -->
    <div
      slot="sidebar"
      class="blog-sidebar py-2 py-lg-0"
    >
      <!-- input search -->
      <b-form-group class="blog-search">
        <b-input-group class="input-group-merge">
          <b-form-input
            id="search-input"
            v-model="searchQuery"
            placeholder="جستجو"
          />
          <b-input-group-append
            class="cursor-pointer"
            is-text
          >
            <feather-icon
              icon="SearchIcon"
            />
          </b-input-group-append>
        </b-input-group>
      </b-form-group>
      <!--/ input search -->

      <!-- categories -->
      <div class="blog-categories mt-3">
        <h6 class="section-label mb-1">
          دسته بندی ها
        </h6>

        <b-form-radio
            v-model="categoryId"
            stacked
            class="mt-1"
            value="0"
        >همه</b-form-radio>
        <b-form-radio
            v-for="item in blogCategories"
            v-model="categoryId"
            stacked
            class="mt-1"
            :name="item.name"
            :value="item.blogCategoryId"
        >
          {{ item.name }}
        </b-form-radio>
      </div>
      <!--/ categories -->
    </div>
    <!--/ sidebar -->
  </content-with-sidebar>
    <b-modal
        id="modal-delete"
        centered
        ok-title="حذف"
        cancelTitle="انصراف"
        @ok="deleteBlog(deleteItem)"
    >
      <span>حذف شود؟</span>
    </b-modal>
  </b-overlay>
</template>

<script>
import {
  BRow,
  BCol,
  BCard,
  BFormInput,
  BCardText,
  BCardTitle,
  BMedia,
  BAvatar,
  BMediaAside,
  BMediaBody,
  BImg,
  BCardBody,
  BLink,
  BBadge,
  BFormGroup,
  BInputGroup,
  BInputGroupAppend,
  BPagination,
  BFormRadio,
  BOverlay,
    BModal,
} from 'bootstrap-vue'
import { kFormatter } from '@core/utils/filter'
import ContentWithSidebar from '@core/layouts/components/content-with-sidebar/ContentWithSidebar.vue'
import BlogGetAllReqeust from "@/libs/Api/Blog/BlogGetAllReqeust";
import BlogCategoryGetAllRequest from "@/libs/Api/Blog/BlogCategoryGetAllRequest";
import BlogDeleteRequest from "@/libs/Api/Blog/BlogDeleteRequest";
import ToastificationContent from "@core/components/toastification/ToastificationContent";

export default {
  title:"لیست بلاگ ها - پنل ادمین بانو کلاب",
  components: {
    BRow,
    BCol,
    BCard,
    BFormInput,
    BCardText,
    BCardBody,
    BCardTitle,
    BMedia,
    BAvatar,
    BMediaAside,
    BMediaBody,
    BLink,
    BBadge,
    BFormGroup,
    BInputGroup,
    BInputGroupAppend,
    BImg,
    BPagination,
    BFormRadio,
    BOverlay,
    BModal,
    ContentWithSidebar,
  },
  data() {
    return {
      searchQuery: '',
      blogList: null,
      blogSidebar: {},
      deleteItem:null,
      blogCategories:null,
      currentPage: 1,
      totalCount: null,
      categoryId:null,
      defaultGetProductData:{
        categoryId:0,
        searchByTitle:'',
        pageNumber:1,
        count:8,
        tagId:0,
      }
    }
  },
  async created() {
    await this.getBlogs(this.defaultGetProductData.categoryId,this.defaultGetProductData.searchByTitle,this.defaultGetProductData.pageNumber);
    await this.getBlogCategories();
  },
  methods: {
    async getBlogs(categoryId,search,pageNumber){
      let _this = this;
      let getBlogData={
        categoryId:categoryId,
        searchByTitle:search,
        pageNumber:pageNumber,
        count:8,
        tagId:0,
      }

      let blogGetAllRequest = new BlogGetAllReqeust(_this);
      blogGetAllRequest.setParams(getBlogData)
      await blogGetAllRequest.fetch(function (content){
        _this.blogList = content.blogs;
        _this.totalCount = content.blogsCount;
      } , function (content){
        console.log(content)
      });
    },
    async getBlogCategories(){
      let _this = this;

      let blogCategoryGetAllRequest = new BlogCategoryGetAllRequest(_this);
      await blogCategoryGetAllRequest.fetch(function (content){
        _this.blogCategories = content
      } , function (content){
        console.log(content)
      });
    },
    async deleteBlog(param){
      let _this = this;

      let blogDeleteRequest = new BlogDeleteRequest(_this);
      blogDeleteRequest.setId(param);
      await blogDeleteRequest.fetch(function (content){
            _this.$toast({
              component: ToastificationContent,
              position: 'bottom-center',
              props: {
                title: `عملیات موفق`,
                icon: 'CheckIcon',
                variant: 'success',
                text: `بلاگ حذف شد.`,
              },
            })
            _this.getBlogs(_this.defaultGetProductData.categoryId,_this.defaultGetProductData.searchByTitle,_this.defaultGetProductData.pageNumber)
          },
          function (error){
            console.log(error)
          })
    },
    createJalaliDate(param){
      return new Date(param).toLocaleDateString("fa-IR",);
    },
    showModal(param){
      let _this = this;
      _this.deleteItem = param
    },
  },
  watch:{
    currentPage:function (nv,ov) {
      this.getBlogs(this.defaultGetProductData.categoryId,this.defaultGetProductData.searchByTitle,nv);
    },
    searchQuery:function (nv,ov){
      this.getBlogs(this.defaultGetProductData.categoryId,nv,this.defaultGetProductData.pageNumber);
    },
    categoryId:function (nv,ov){
      this.getBlogs(nv,this.defaultGetProductData.searchByTitle,this.defaultGetProductData.pageNumber);
    },
  },
}
</script>

<style lang="scss">
@import '@core/scss/vue/pages/page-blog.scss';
.item-description{
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
}
</style>
