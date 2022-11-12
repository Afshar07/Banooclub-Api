<template>
  <b-overlay
      :show="Roomate === null"
      rounded="sm"
  >
    <b-modal
        id="modal-delete"
        centered
        ok-title="بله"

        cancelTitle="خیر"
        @ok="DeletePost"
    >
      <span>آیا از حذف کردن این هم خانه اطمینان دارید ؟ </span>
    </b-modal>


<!--    <b-modal-->
<!--        id="modal-view"-->
<!--        ok-title="تغییر وضعیت"-->
<!--        cancelTitle="بستن"-->
<!--        centered-->
<!--        size="lg"-->
<!--        @ok="OpenStatusModal"-->
<!--        title="مشاهده پست"-->
<!--    >-->
<!--      <postModal v-if="SelectedPost!==null" :postData="SelectedPost"></postModal>-->
<!--      <span ref="statusBtn" v-b-modal.modal-status style="display: none"></span>-->
<!--    </b-modal>-->
<!--    <b-modal-->
<!--        id="modal-status"-->
<!--        centered-->
<!--        ok-title="بروزرسانی"-->
<!--        cancelTitle="بستن"-->
<!--        @ok="ChangeStatus"-->
<!--    >-->
<!--      <div class="row">-->
<!--        <div class="col-md-12">-->
<!--          <span>تغییر وضعیت به: </span>-->
<!--        </div>-->
<!--        <div class="col-md-12 my-2">-->
<!--          <v-select-->
<!--              v-model="SelectedPostStatus"-->
<!--              :options="PostStatus"-->
<!--              label="name"-->
<!--              :reduce="name => name.status"-->
<!--              :clearable="false"-->
<!--              class="per-page-selector d-inline-block w-100"-->
<!--          />-->
<!--        </div>-->
<!--      </div>-->
<!--    </b-modal>-->
    <b-row>
      <!-- post -->
      <b-col lg="12" md="12" class="my-2">
        <b-form-input
            id="basicInput"
            v-model="search"
            placeholder="جستجو"
        />
      </b-col>
      <b-col cols="12" class="mx-auto">
        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="Roomate"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >

          <!-- Column: delete -->
          <template #cell(medias)="data">
              <div v-for="el in data.item.photos" class="d-flex align-items-center gap-2">
                <img v-if="el.base64!==''" :src="`https://banooclubapi.oregonserver.com/${el.base64}`" style="object-fit: contain;object-position: center" width="50px" height="50px" alt="">

              </div>

          </template>
          <template #cell(receptionDate)="data">

            <span>{{new Date(data.item.receptionDate).toLocaleDateString('fa-IR')}}</span>
          </template>
<!--          <template #cell(userInfo)="data">-->

<!--            <span v-if="data.item.userInfo">{{data.item.userInfo.userName}}@</span>-->
<!--          </template>-->
          <template #cell(delete)="data">

            <div class="cursor-pointer d-flex flex-row"
                 v-b-modal.modal-delete
                 @click="SetSelectedPost(data.item)"
            >
              <feather-icon icon="TrashIcon" size="20" class="text-danger" />
            </div>

          </template>

<!--          <template #cell(view)="data">-->


<!--            <feather-icon @click="SetSelectedPost(data.item)" v-b-modal.modal-view icon="EyeIcon" size="20" class="text-primary cursor-pointer" />-->


<!--          </template>-->

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
import GetAllRoomate from "@/libs/Api/Roomate/GetAll";
import DeleteRequest from "@/libs/Api/Roomate/DeleteRequest";
import ChangePostStatus from "@/libs/Api/Post/ChangePostStatus";
import postModal from "@/views/components/post/postModal";
import vSelect from 'vue-select'
import ToastificationContent from "@core/components/toastification/ToastificationContent";
export default {
  title: 'لیست آگهی های هم خانه - پنل ادمین بانو کلاب',
  name: "PostList",
  data(){
    return{
      Roomate:null,
      totalCount:null,
      currentPage:1,
      search:'',
      perPage:10,
      SelectedPost:null,
      SelectedPostStatus:1,
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
        { key: 'roomateId',label:'شناسه'},
        { key: 'medias',label:'عکس ها'},
        { key: 'description',label:'توضیحات'},
        { key: 'address',label:'آدرس'},
        { key: 'receptionDate',label:'تاریخ پذیرش'},
        { key: 'delete',label:'حذف'},
        { key: 'view',label:'مشاهده'},

      ],

    }
  },
  watch:{
    currentPage:function (){
      this.GetAllRoomate()
    }
  },
  async created(){
    await this.GetAllRoomate();
  },

  computed:{
    FilteredPosts(){
      const data = this.posts
      if(this.search===''){
        return data
      } else{
        return data.filter((element)=>{
          return element.title.match(this.search) || element.postId === parseInt(this.search)
        })
      }
    }
  },
  methods:{
    OpenStatusModal(){
      this.$refs.statusBtn.click();
    },
    async DeletePost(){
      let _this = this
      let deleteRequest = new DeleteRequest(_this)
      deleteRequest.setId(this.SelectedPost.roomateId)
      await deleteRequest.fetch(()=>{
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `هم خانه  با موفقیت حذف شد`,
          },
        })
        _this.GetAllRoomate()
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
    async GetAllRoomate(){
      let _this = this;
      let data={
        pageNumber:this.currentPage,
        count:10
      }

      let getAllRoomate = new GetAllRoomate(_this)
      getAllRoomate.setParams(data);
      await getAllRoomate.fetch(function (content){
        console.log(_this.Roomate)
        _this.Roomate = content;
        // _this.totalCount = content.postCount;
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
