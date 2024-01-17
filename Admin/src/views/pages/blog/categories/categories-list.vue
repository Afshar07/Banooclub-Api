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
                 v-b-modal.modal-delete
                 @click="showModal(data.item.blogCategoryId)"
            >
              <feather-icon size="20" icon="TrashIcon" class="text-danger" />
            </div>

          </template>

          <template #cell(edit)="data">

            <router-link :to="{ name: 'pages-blog-categories-edit', params: { id: data.item.blogCategoryId } }" class="cursor-pointer d-flex flex-row">
              <feather-icon size="20" icon="EditIcon" />
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
            </b-col>
          </b-row>
        </div>
      </b-card>
    </div>
    <b-modal
        id="modal-delete"
        centered
        ok-title="حذف"
        cancelTitle="انصراف"
        @ok="deleteCategory(deleteItem)"
    >
      <span>حذف شود؟</span>
    </b-modal>
  </b-overlay>
</template>

<script>
import {
  BCard, BRow, BCol, BFormInput, BButton, BTable, BMedia, BAvatar, BLink,
  BBadge, BDropdown, BDropdownItem, BPagination,BOverlay,BModal,
} from 'bootstrap-vue'
import vSelect from 'vue-select'
import ToastificationContent from "@core/components/toastification/ToastificationContent";
import BlogCategoryGetAllRequest from "@/libs/Api/Blog/BlogCategoryGetAllRequest";
import BlogCategoryCreateRequest from "@/libs/Api/Blog/ProductCategoryCreateRequest";
import BlogCategoryDeleteRequest from "@/libs/Api/Blog/BlogCategoryDeleteRequest";

export default {
  name: "categories-list",
  title:"دسته بندی های بلاگ - پنل ادمین بانو کلاب",
  data(){
    return{
      categories:null,
      totalCount:null,
      addNewCategory:false,
      deleteItem:null,
      currentPage:1,
      perPage:10,
      myTableColumns : [
        { key: 'blogCategoryId',label:'شناسه'},
        { key: 'name',label:'نام'},
        { key: 'delete',label:'حذف'},
        { key: 'edit',label:'ویرایش'},
      ],
      defaultCreateData:{
        isDeleted:false,
        blogCategoryId:0,
        name:''
      },
    }
  },
  async created(){
    await this.getBlogCategories()
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
  methods:{
    async getBlogCategories(){
      let _this = this;

      let blogCategoryGetAllRequest = new BlogCategoryGetAllRequest(_this);
      await blogCategoryGetAllRequest.fetch(function (content){
        _this.categories = content
      } , function (content){
        console.log(content)
      });
    },
    async createProductCategory(){
      let _this = this;

      let blogCategoryCreateRequest = new BlogCategoryCreateRequest(_this);
      blogCategoryCreateRequest.setParams(_this.defaultCreateData);
      await blogCategoryCreateRequest.fetch(function (content){
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
        _this.getBlogCategories()
      },function (error){
        console.log(error)
      })
    },
    async deleteCategory(param){
      let _this = this;

      let blogCategoryDeleteRequest = new BlogCategoryDeleteRequest(_this);
      blogCategoryDeleteRequest.setId(param);
      await blogCategoryDeleteRequest.fetch(function (content){
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
        _this.getBlogCategories()
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
