<template>
  <b-overlay
      :show="Categories === null"
      rounded="sm"
  >
    <div v-if="Categories !== null">



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



          <template #cell(Edit)="data">

            <div class="cursor-pointer d-flex flex-row"

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
        <div v-if="SelectedCategory!==null" class="mx-2 mb-2">
          <b-row>
            <div class="col-md-6">
              <div  class="row">
                <div class="col-md-6">
                  <small class="text-secondary my-1">سئو url</small>
                  <input v-model="SelectedCategory.seourl" type="text" placeholder="url سئو" maxlength="50" class="form-control">
                  <span :class="{'text-lightPink':SelectedCategory.seourl.length===60}"><small>{{SelectedCategory.seourl.length}}</small><small class="text-secondary">/</small><small class="text-secondary">50</small></span>
                </div>
                <div class="col-md-6 my-lg-0 my-md-0 my-3">
                  <small class="text-secondary my-1">تیتر سئو </small>
                  <input v-model="SelectedCategory.seoTitle" type="text" placeholder="تیتر سئو" class="form-control" maxlength="60">
                  <span :class="{'text-lightPink':SelectedCategory.seoTitle.length===60}"><small>{{SelectedCategory.seoTitle.length}}</small><small class="text-secondary">/</small><small class="text-secondary">60</small></span>
                </div>
                <div v-if="SelectedCategory.seoDescription!==null" class="col-md-12 my-3">
                  <small class="text-secondary my-1">توضیحات سئو </small>
                  <textarea v-model="SelectedCategory.seoDescription" class="form-control" placeholder="توضیحات سئو" maxlength="160"></textarea>
                  <span :class="{'text-lightPink':SelectedCategory.seoDescription.length===160}"><small>{{SelectedCategory.seoDescription.length}}</small><small class="text-secondary">/</small><small class="text-secondary">160</small></span>
                </div>
                <div class="col-md-3">
                  <button class="btn btn-sm btn-success" @click="UpdateSeo">
                    <small>ذخیره تغییرات</small>
                  </button>
                </div>
              </div>
            </div>
            <div v-if="SelectedCategory!==null" class="col-md-6 my-3">
              <!--- serp Desktop --->
              <div  class="serp-preview d-lg-flex p-3 d-none flex-column align-items-end">
                <div class="serp-title">
                  <h4>
                    {{SelectedCategory.seoTitle}}
                  </h4>
                </div>
                <div class="serp-url">
                  <h5 class="d-flex align-items-center flex-wrap">
                    <span style="overflow-wrap: anywhere">{{SelectedCategory.seourl.replaceAll(' ','-')}}/</span>
                    <span>
                             https://banooclub.com/Blog
                        <span class="serp-arrow"></span>
                      </span>

                  </h5>

                </div>
                <div class="serp-description">
                  <h6 style="overflow-wrap: anywhere">
                    {{ SelectedCategory.seoDescription }}
                  </h6>
                </div>
              </div>
              <!--- serp Mobile --->
              <div v-if="SelectedCategory!==null" class="serp-preview d-lg-none  p-3 d-flex flex-column align-items-end">
                <div class="serp-title">
                  <h6>
                    {{SelectedCategory.seoTitle}}
                  </h6>
                </div>
                <div class="serp-url d-flex align-items-center flex-wrap" >

                  <small >{{SelectedCategory.seourl.replaceAll(' ','-')}}/</small>
                  <small>
                    https://banooclub.com/Blog
                    <small class="serp-arrow"></small>
                  </small>




                </div>
                <div class="serp-description">
                  <small>
                    {{ SelectedCategory.seoDescription }}
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

import BlogCategoryGetAllRequest from '@/libs/Api/Blog/BlogCategoryGetAllRequest'

import ToastificationContent from '@core/components/toastification/ToastificationContent'
import BlogCategoryUpdateSeoRequest from '@/libs/Api/Blog/BlogCategoryUpdateSeoRequest'

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
    async UpdateSeo(){
      let _this = this
      let blogCategoryUpdateSeoRequest = new BlogCategoryUpdateSeoRequest(_this)
      let data ={
        blogCategoryId: this.SelectedCategory.blogCategoryId,
        name: this.SelectedCategory.name,
        seoTitle: this.SelectedCategory.seoTitle,
        seoDescription: this.SelectedCategory.seoDescription,
        seourl: this.SelectedCategory.seourl.replaceAll(' ','-'),
      }
      blogCategoryUpdateSeoRequest.setParams(data)
      await  blogCategoryUpdateSeoRequest.fetch(()=>{
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `سئو دسته بندی مقاله با موفقیت بروزرسانی شد.`,
          },
        })
        _this.GetAllBlogCategory();
        _this.SelectedCategory = null
      },()=>{

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
