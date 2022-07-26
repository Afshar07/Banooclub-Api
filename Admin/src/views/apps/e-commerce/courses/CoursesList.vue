<template>
  <b-overlay
      :show="courses === null"
      rounded="sm"
  >
    <div v-if="courses !== null">

      <!-- Table Container Card -->
      <b-card
          no-body
          class="mb-0"
      >

        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="courses"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >

          <!-- Column: inOnline -->

          <template #cell(isOnline)="data">

            <div class="cursor-pointer d-flex flex-row w-25">
              <span v-if="data.item.isOnline" class="text-primary">آنلاین</span>
              <span v-else class="text-danger">آفلاین</span>
            </div>

          </template>

          <!-- Column: delete -->

          <template #cell(delete)="data">

            <div class="cursor-pointer d-flex flex-row "
                 v-b-modal.modal-delete
                 @click="showModal(data.item.courseId)"
            >
              <feather-icon icon="TrashIcon" size="20" class="text-danger" />
            </div>

          </template>

          <template #cell(edit)="data">

            <router-link :to="{ name: 'apps-e-commerce-courses-edit', params: { id: data.item.courseId } }" class="cursor-pointer d-flex flex-row ">
              <feather-icon icon="EditIcon" size="20"/>
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
    <b-modal
        id="modal-delete"
        centered
        ok-title="حذف"
        cancelTitle="انصراف"
        @ok="deleteCourse(deleteItem)"
    >
      <span>حذف شود؟</span>
    </b-modal>
  </b-overlay>
</template>

<script>
import {
  BCard, BRow, BCol, BFormInput, BButton, BTable, BMedia, BAvatar, BLink,
  BBadge, BDropdown, BDropdownItem, BPagination,BOverlay,BModal
} from 'bootstrap-vue'
import vSelect from 'vue-select'
import ToastificationContent from "@core/components/toastification/ToastificationContent";
import ProductCourseGetAllRequest from "@/libs/Api/Product/ProductCourseGetAllRequest";
import ProductCourseDeleteRequest from "@/libs/Api/Product/ProductCourseDeleteRequest";


export default {
  title:"لیست دوره ها - پنل ادمین بانو کلاب",
  name: "CoursesList",
    data(){
      return{
        courses:null,
        totalCount:null,
        addNewCategory:false,
        deleteItem:null,
        currentPage:1,
        perPage:10,
        searchCommand:'',
        perPageOptions:[10, 20, 30 , 40 , 50],
        myTableColumns : [
          { key: 'courseId',label:'شناسه'},
          { key: 'name',label:'نام'},
          { key: 'summary',label:'خلاصه'},
          { key: 'isOnline',label:'نوع استریم'},
          { key: 'delete',label:'حذف'},
          { key: 'edit',label:'ویرایش'},
        ],
        defaultGetData:{
          pageNumber:1,
          count:10,
          searchCommand:''
        },
        defaultCreateData:{
          isDeleted:false,
          productCategoryId:0,
          parentId:0,
          name:''
        },
      }
    },
    async created(){
      await this.getAllCourses(this.defaultGetData.pageNumber,this.defaultGetData.count,this.defaultGetData.searchCommand)
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
      async getAllCourses(pageNumber,count,searchCommand){
        let  _this = this;
        let  defaultGetData={
          pageNumber:pageNumber,
          count:count,
          searchCommand:searchCommand,
        }

        let productCourseGetAllRequest = new ProductCourseGetAllRequest(_this);
        productCourseGetAllRequest.setParams(defaultGetData);
        await  productCourseGetAllRequest.fetch(function (content){
          _this.courses = content.result.data.courses;
          _this.totalCount = content.result.data.coursesCount;
        },function (error){
          console.log(error)
        })
      },
      async deleteCourse(param){
        let _this = this;

        let productCourseDeleteRequest = new ProductCourseDeleteRequest(_this);
        productCourseDeleteRequest.setId(param);
        await productCourseDeleteRequest.fetch(function (content){
          _this.$toast({
            component: ToastificationContent,
            position: 'bottom-center',
            props: {
              title: `عملیات موفق`,
              icon: 'CheckIcon',
              variant: 'success',
              text: `دوره حذف شد.`,
            },
          })
          _this.getAllCourses(_this.defaultGetData.pageNumber,_this.defaultGetData.count,_this.defaultGetData.searchCommand)
        },function (error){
          console.log(error)
        })
      },
      showModal(param){
        let _this = this;
        _this.deleteItem = param
      },
    },
  watch:{
    currentPage:function (nv,ov) {
      this.getAllCourses(nv,this.defaultGetData.count,this.defaultGetData.searchCommand);
    },
  },
}
</script>

<style scoped>

</style>
