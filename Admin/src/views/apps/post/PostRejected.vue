<template>
  <b-overlay
      :show="posts === null"
      rounded="sm"
  >
    <b-modal
        id="modal-delete"
        centered
        ok-title="بله"

        cancelTitle="خیر"
        @ok="DeletePost"
    >
      <span>آیا از حذف کردن این پست اطمینان دارید ؟ </span>
    </b-modal>


    <b-modal
        id="modal-view"
        ok-title="تغییر وضعیت"
        cancelTitle="بستن"
        centered
        size="lg"
        @ok="OpenStatusModal"
        title="مشاهده پست"
    >
      <postModal v-if="SelectedPost!==null" :postData="SelectedPost"></postModal>
      <span ref="statusBtn" v-b-modal.modal-status style="display: none"></span>
    </b-modal>
    <b-modal
        id="modal-status"
        centered
        ok-title="بروزرسانی"
        cancelTitle="بستن"
        @ok="ChangeStatus"
    >
      <div class="row">
        <div class="col-md-12">
          <span>تغییر وضعیت به: </span>
        </div>
        <div class="col-md-12 my-2">
          <v-select
              v-model="SelectedPostStatus"
              :options="PostStatus"
              label="name"
              :reduce="name => name.status"
              :clearable="false"
              class="per-page-selector d-inline-block w-100"
          />
        </div>
      </div>
    </b-modal>
    <b-row>
      <!-- post -->
      <b-col lg="12" md="12" class="my-2">
        <b-form-input
            id="basicInput"
            v-model="search"
            placeholder="جستجو بر اساس موضوع پست"
        />
      </b-col>
      <b-col cols="12" class="mx-auto">
        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="posts"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >
          <template #cell(createDate)="data">

            <span v-if="data.item.createDate">{{new Date(data.item.createDate).toLocaleDateString('fa-IR')}}</span>
          </template>
          <template #cell(reporterName)="data">

            <span v-if="data.item.reporterUserInfo">{{data.item.reporterUserInfo.userName}}@</span>
          </template>
          <template #cell(delete)="data">

            <div class="cursor-pointer d-flex flex-row"
                 v-b-modal.modal-delete
                 @click="SetSelectedPost(data.item)"
            >
              <feather-icon icon="TrashIcon" size="20" class="text-danger" />
            </div>

          </template>

          <template #cell(view)="data">


            <feather-icon @click="GetSelectedPost(data.item)" v-b-modal.modal-view icon="EyeIcon" size="20" class="text-primary cursor-pointer" />


          </template>

        </b-table>
      </b-col>
      <!-- post -->
      <b-col
          cols="12"
          sm="6"
          class="d-flex align-items-center my-2 justify-content-center justify-content-sm-end"
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

  </b-overlay>
</template>

<script>
import {
  BCard, BRow, BCol, BFormInput, BButton, BTable, BMedia, BAvatar, BLink,
  BBadge, BDropdown, BDropdownItem, BPagination,BOverlay,
} from 'bootstrap-vue'
import ProfilePost from "@/views/pages/profile/ProfilePost";
import PostDeleteRequest from "@/libs/Api/Post/PostDeleteRequest";
import ChangePostStatus from "@/libs/Api/Post/ChangePostStatus";
import postModal from "@/views/components/post/postModal";
import vSelect from 'vue-select'
import ToastificationContent from "@core/components/toastification/ToastificationContent";
import PostGetRejected from "@/libs/Api/Post/PostGetRejected";
import PostGetRequest from "@/libs/Api/Post/PostGetRequest";
export default {
  title: 'لیست پست های گزارش شده - پنل ادمین پشت بام پلاس',
  name: "PostList",
  data(){
    return{
      posts:null,
      totalCount:null,
      currentPage:1,
      SelectedPostStatus:1,
      perPage:10,
      SelectedPost:null,
      search:'',
      PostStatus:[
        {
          name:'فعال',
          status:1
        },
        {
          name:'غیرفعال',
          status:2
        }
      ],
      myTableColumns : [
        { key: 'postReportId',label:'شناسه گزارش'},
        { key: 'reason',label:'دلیل'},
        { key: 'createDate',label:'تاریخ'},
        { key: 'reporterName',label:'توسط'},
        { key: 'delete',label:'حذف'},
        { key: 'view',label:'مشاهده'},

      ],

    }
  },
  watch:{
    currentPage:function (){
      this.getPosts()
    },
    search:function (){
      this.getPosts()
    }
  },
  async created(){
    await this.getPosts();
  },
  computed:{
    FilteredPosts(){
      const data = this.posts
      if(this.search===''){
        return data
      }  else{
        return data.filter((element)=>{
          return element.Reason.match(this.search) || element.PostId === parseInt(this.search)
        })
      }
    }
  },
  methods:{
   async GetSelectedPost(item){

      let _this = this;
      let postGetRequest = new PostGetRequest(_this)
      postGetRequest.setId(item.postId);
      await postGetRequest.fetch(function (content){
       _this.SelectedPost = content

      },function (error){
        console.log(error)
      })
    },
    OpenStatusModal(){
      this.$refs.statusBtn.click();
    },
    async DeletePost(){
      let _this = this
      let postDeleteRequest = new PostDeleteRequest(_this)
      postDeleteRequest.setId(this.SelectedPost.PostId)
      await postDeleteRequest.fetch(()=>{
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `پست با موفقیت حذف شد`,
          },
        })
        _this.getPosts()
      },()=>{

      })
    },
    async ChangeStatus () {
      let _this = this
      let changePostStatus = new ChangePostStatus(_this)

      let data ={
        status:this.SelectedPostStatus,
        postId: this.SelectedPost.postId,
      }
      changePostStatus.setParams(data)
      await changePostStatus.fetch(()=>{
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `وضعیت پست با موفقیت تغییر یافت`,
          },
        })

        this.getPosts();
      },()=>{

      })
    },
    SetSelectedPost(item){
      this.SelectedPost = item
    },
    async getPosts(){
      let _this = this;
      let data={

        pageNumber:this.currentPage,
        search:this.search
      }

      let postGetRejected = new PostGetRejected(_this)
      postGetRejected.setParams(data);
      await postGetRejected.fetch(function (content){
        _this.posts = content.reports;
        _this.totalCount = content.reportsCount;
      },function (error){
        console.log(error)
      })
    },

  },
  components:{
    BCard,
    BRow,
    BCol,
    BFormInput,
    BButton,
    BTable,
    BMedia,
    postModal,
    BAvatar,
    BLink,
    BBadge,
    vSelect,
    BDropdown,
    BDropdownItem,
    BPagination,
    BOverlay,
    ProfilePost,

  },
}
</script>

<style scoped>

</style>
