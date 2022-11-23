<template>
  <b-overlay
      :show="AllComments === null"
      rounded="sm"
  >
    <div v-if="AllComments !== null">
      <b-modal
          id="modal-delete"
          centered
          ok-title="بله"

          cancelTitle="خیر"
          @ok="ChangeCommentStatus"
      >
        <span>  آیا از انجام این عملیات اطمینان دارید؟</span>
      </b-modal>
      <!-- Table Container Card -->
      <b-card
          no-body
          class="mb-0"
      >
        <b-col lg="12" md="12" class="my-2">
          <b-row>
            <b-col lg="10" md="12" class="my-2">
              <b-form-input
                  id="basicInput"
                  v-model="search"
                  placeholder="جستجو بر اساس نام و نام خانوادگی"
              />

            </b-col>
            <b-col lg="2" md="12" class="my-2">
              <select v-model="IsActive" class="form-control">
                <option :value="0" selected>دسته بندی بر اساس</option>
                <option :value="1">تایید شده</option>
                <option :value="2">تایید نشده</option>
              </select>
            </b-col>

          </b-row>

        </b-col>

            <div class="row">
              <div v-for="(item,idx) in AllComments" :key="idx" class="col-lg-3 col-md-6 my-1">
                <div class="d-flex flex-lg-row flex-md-column align-items-center justify-content-between   gap-2  BoxShadow p-3"  style="border-radius: 15px">
                  <div class="d-flex align-items-center">
                    <img class="mx-2 rounded-circle shadow" v-if="item.userInfo && item.userInfo.displayName!==null" :src="'https://banooclubapi.oregonserver.com/' + item.userInfo.selfieFileData" alt="Comment" width="40px" height="40px">
                    <img class="mx-2 rounded-circle shadow"  v-else src="/nopicture.jpg" alt="Comment" width="40px" height="40px">
                    <div class="d-flex flex-column align-items-start gap-1">
                      <strong v-if="item.userInfo">{{ item.userInfo.displayName!==null?item.userInfo.displayName:'بدون نام' }}</strong>
                      <small class="text-secondary">{{item.message}}</small>
                    </div>
                  </div>

                  <div class="d-flex align-items-center">
                    <a    v-b-modal.modal-delete class="StandardLinkTag" @click="SetComment(1,item)" v-if="!item.isActive">
                      <feather-icon icon="CheckIcon" size="20"></feather-icon>
                    </a>
                    <a    v-b-modal.modal-delete class="StandardLinkTag" @click="SetComment(2,item)" v-if="item.isActive">
                      <feather-icon class="text-danger" icon="MinusIcon" size="20"></feather-icon>
                    </a>
                    <a    v-b-modal.modal-delete class="StandardLinkTag" @click="SetComment(3,item)">
                      <feather-icon  class="text-danger" icon="TrashIcon" size="20"></feather-icon>
                    </a>



                  </div>

                </div>

              </div>
            </div>

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

import BlogCommentGetAllRequest from '@/libs/Api/Blog/BlogCommentGetAllRequest'
import CommentChangeStatusRequest from '@/libs/Api/Blog/BlogCommentChangeStatusRequest'
import BlogCommentDeleteRequest from '@/libs/Api/Blog/BlogCommentDeleteRequest'
import ToastificationContent from '@core/components/toastification/ToastificationContent'


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
  title: 'دیدگاه مقالات  - پنل ادمین بانو کلاب',
  name: 'UsersList',
  data() {
    return {
      AllComments: null,
      totalCount: null,
      showDeleteModal: false,
      IsActive:null,
      currentPage: 1,
      deleteItem: null,
      perPage: 5,
      perPageOptions: [10, 20, 30, 40, 50],
      pageNumber: 1,
      count: 10,
      search: '',
      tagId:0,
      adminId:0,
      Actiontype:null,
      SelectedComment:null,

    }
  },
  async created() {
    await this.GetAllBlogComment()
    let admin = JSON.parse(localStorage.getItem('userData'))
    this.adminId = admin.data.user.userId
  },

  watch: {
    search: function () {
      this.GetAllBlogComment()
    },
    currentPage: function () {
      this.GetAllBlogComment()
    },
    IsActive:function (val,ildVal){
      this.GetAllBlogComment()
    }
  },
  methods: {

    SetComment(type,data){
      this.Actiontype = type
      this.SelectedComment = data
    },
    async ChangeCommentStatus(){
      let _this = this
      if (this.Actiontype===1||this.Actiontype===2){
            let commentChangeStatusRequest = new CommentChangeStatusRequest(_this)
        commentChangeStatusRequest.setId(this.SelectedComment.blogCommentId)
        await  commentChangeStatusRequest.fetch(()=>{
          _this.$toast({
            component: ToastificationContent,
            position: 'bottom-center',
            props: {
              title: `عملیات موفق`,
              icon: 'CheckIcon',
              variant: 'success',
              text: `وضعیت نظر با موفقیت تغییر یافت`,
            },
          })
          _this.GetAllBlogComment();
        },(e)=>{
          console.log(e)
        })

      }else if(this.Actiontype===3){
        let blogCommentDeleteRequest = new BlogCommentDeleteRequest(_this)
        blogCommentDeleteRequest.setId(this.SelectedComment.blogCommentId)
        await  blogCommentDeleteRequest.fetch(()=>{
          _this.$toast({
            component: ToastificationContent,
            position: 'bottom-center',
            props: {
              title: `عملیات موفق`,
              icon: 'CheckIcon',
              variant: 'success',
              text: `نظر با موفقیت حذف شد`,
            },
          })
          _this.GetAllBlogComment();
        },(e)=>{
          console.log(e)
        })
      }
    },

    async GetAllBlogComment() {
      let _this = this
      let blogCommentGetAllRequest = new BlogCommentGetAllRequest(_this)
      let data = {
        pageNumber: _this.currentPage,
        count: _this.count,
        search: _this.search,
        IsActive:_this.IsActive===0 ? '' : _this.IsActive === 1
      }
      blogCommentGetAllRequest.setParams(data)
      await blogCommentGetAllRequest.fetch(function (content) {
        _this.AllComments = content.comments
        _this.totalCount = content.commentsCount
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
.BoxShadow {
  box-shadow: 0 5px 20px rgb(0 0 0 / 12%);
}

</style>
