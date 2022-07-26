<template>
  <b-overlay
      :show="comments === null"
      rounded="sm"
  >

    <div v-if="comments !== null">

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
                md="6"
                class="d-flex align-items-center justify-content-start mb-1 mb-md-0"
            >
              <label>نمایش</label>
              <v-select
                  v-model="perPage"
                  :options="perPageOptions"
                  :clearable="false"
                  class="per-page-selector d-inline-block mx-50"
              />
              <label>تعداد</label>
            </b-col>
          </b-row>

        </div>

        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="comments"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >

          <template #cell(userId)="data">
            <span class="d-block">{{data.item.name}}</span>
            <small class="text-muted">@{{ data.item.userId }}</small>
          </template>

<!--          &lt;!&ndash; Column: createDate &ndash;&gt;-->
<!--          <template #cell(createDate)="data">-->
<!--             <span>-->
<!--              {{data.item.createDate.slice(11,16)}}-->
<!--              - -->
<!--              {{createJalaliDate(data.item.createDate.slice(0,10))}}-->
<!--            </span>-->
<!--          </template>-->

          <!-- Column: edit -->
          <template #cell(edit)="data">
            <b-link :to="{ name: 'pages-blog-comments-edit', params: { id: data.item.blogCommentId } }">
              <feather-icon size="20" icon="EditIcon" />
            </b-link>
          </template>

          <!-- Column: delete -->
          <template #cell(delete)="data">
            <b-link class="text-danger" v-b-modal.modal-delete @click="showModal(data.item.blogCommentId)">
              <feather-icon size="20" icon="TrashIcon" />
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
                  :total-rows="totalCounts"
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
        @ok="deleteComment(deleteItem)"
    >
      <span>حذف شود؟</span>
    </b-modal>
  </b-overlay>
</template>

<script>
import {
  BCard, BRow, BCol, BFormInput, BButton, BTable, BMedia, BAvatar, BLink,
  BBadge, BDropdown, BDropdownItem, BPagination, BOverlay,BModal,
} from 'bootstrap-vue'
import vSelect from 'vue-select'
import UsersGetAllRequest from "@/libs/Api/Users/UsersGetAllRequest";
import PaymentGetAllRequest from "@/libs/Api/Payment/PaymentGetAllRequest";
import Helper from "@/libs/Helper";
import ProductCommentGetAllRequest from "@/libs/Api/Product/ProductCommentGetAllRequest";
import ProductCommentDeleteRequest from "@/libs/Api/Product/ProductCommentDeleteRequest";
import ToastificationContent from "@core/components/toastification/ToastificationContent";
import BlogCommentGetAllRequest from "@/libs/Api/Blog/BlogCommentGetAllRequest";
import BlogCommentDeleteRequest from "@/libs/Api/Blog/BlogCommentDeleteRequest";

export default {
  name: "BlogComments",
  title:"لیست نظرات بلاگ - پنل ادمین بانو کلاب",
  data(){
    return{
      comments:null,
      currentPage:1,
      totalCounts:null,
      deleteItem:null,
      perPage:10,
      perPageOptions:[10, 20, 30 , 40 , 50],
      myTableColumns : [
        { key: 'userId' ,label :'شناسه و نام کاربر'},
        { key: 'message' ,label :'متن'},
        // { key: 'createDate' ,label :'تاریخ ثبت'},
        { key: 'blogId' ,label :'شناسه بلاگ'},
        { key: 'edit',label:'ویرایش'},
        { key: 'delete',label:'حذف'},
      ],
    }
  },
  async created() {
    await this.getComments(this.perPage,this.currentPage);
  },
  methods:{
    refreshData(){
      this.getPayments(this.perPage,this.currentPage,this.searchQuery)
    },
    async getComments(count,pageNumber){
      let _this = this;
      let data = {
        pageNumber:pageNumber,
        count:count,
      }

      let blogCommentGetAllRequest = new BlogCommentGetAllRequest(_this);
      blogCommentGetAllRequest.setParams(data);
      await blogCommentGetAllRequest.fetch(function (content){
        _this.comments = content.blogComments
        _this.totalCounts = content.counts;
      },function (error){
        console.log(error);
      })
    },
    async deleteComment(param){
      let _this = this;

      let blogCommentDeleteRequest = new BlogCommentDeleteRequest(_this);
      blogCommentDeleteRequest.setId(param);
      await blogCommentDeleteRequest.fetch(function (content){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `حذف شد.`,
          },
        })
        _this.getComments(_this.perPage,_this.currentPage)
      },function (error){
        console.log(error)
      })
    },
    createJalaliDate(param){
      return new Date(param).toLocaleDateString("fa-IR",);
    },
    makePrettyPrice(param){
      return Helper.prettyPrice(param,0)
    },
    showModal(param){
      let _this = this;
      _this.deleteItem = param
    },
  },
  watch:{
    perPage:function (nv,ov){
      this.getComments(nv,this.currentPage);
    },
    currentPage:function (nv,ov) {
      this.getComments(this.perPage,nv);
    },
  },
  components: {

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
}
</script>

<style lang="scss" scoped>
.per-page-selector {
  width: 90px;
}
</style>

<style lang="scss">
@import '@core/scss/vue/libs/vue-select.scss';
</style>
