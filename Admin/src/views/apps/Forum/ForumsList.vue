<template>
  <b-overlay
      :show="Forums === null"
      rounded="sm"
  >
    <div v-if="Forums !== null">
      <b-modal
          id="modal-delete"
          centered
          ok-title="بله"

          cancelTitle="خیر"
          @ok="DeleteForum"
      >
        <span>آیا از حذف کردن این تالار اطمینان دارید ؟ </span>
      </b-modal>
      <b-modal
          id="modal-View"
          centered
          ok-title="تغییر وضعیت"
          cancelTitle="بستن"
          @ok="OpenChangeStatus"
      >
        <div v-if="SelectedForum!==null" class="row">
          <div class="col-md-12">
            <div class="d-flex align-items-center gap-2">
              <span class="text-secondary">نام تالار : </span>
              <span>{{SelectedForum.title}}</span>
            </div>
          </div>
          <div class="col-md-12 my-2">
            <div class="text-secondary" style="overflow-wrap: anywhere">
              {{SelectedForum.description}}
            </div>
          </div>
        </div>
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
               v-model="SelectedForumStatus"
               :options="ForumStatus.filter(e=> e.status!== status)"
               label="name"
               :reduce="name => name.status"
               :clearable="false"
               class="per-page-selector d-inline-block w-100"
           />
         </div>
       </div>
      </b-modal>




      <span  v-b-modal.modal-status ref="OpenStatusBtn"  style="display: none"></span>
      <!-- Table Container Card -->
      <b-card
          no-body
          class="mb-0"
      >
        <b-tabs>
          <b-tab>
            <template active #title>
              <div @click="status =1">
                <feather-icon icon="CheckIcon"  />
                <span>تالار های فعال</span>
              </div>

            </template>
            <b-col lg="12" md="12" class="my-2">
              <b-row>
                <b-col lg="12" md="12" class="my-2">
                  <b-form-input
                      id="basicInput"
                      v-model="search"
                      placeholder="جستجو بر اساس تیتر تالار"
                  />
                </b-col>

              </b-row>

            </b-col>


            <b-table
                ref="refUserListTable"
                class="position-relative"
                :items="Forums"
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
                     @click="SetSelectedForum(data.item)"
                >
                  <feather-icon icon="TrashIcon" size="20" class="text-danger" />
                </div>

              </template>
              <template #cell(userInfo)="data">
                <router-link :to="`/apps/users/Detail/${data.item.userInfo.userName}`">
                  <span v-if="data.item.userInfo">{{data.item.userInfo.userName}}@</span>
                </router-link>

              </template>
              <template #cell(View)="data">

                <b-link class="cursor-pointer"  v-b-modal.modal-View @click="SetSelectedForum(data.item)">
                  <feather-icon icon="EyeIcon" size="20" class="text-primary" />
                </b-link>

              </template>

            </b-table>
          </b-tab>
          <b-tab >
            <template #title >
              <div @click="status=2">
                <feather-icon icon="MinusIcon"   />
                <span>تالار های تایید نشده</span>
              </div>

            </template>

            <b-col lg="12" md="12" class="my-2">
              <b-row>
                <b-col lg="12" md="12" class="my-2">
                  <b-form-input
                      id="basicInput2"
                      v-model="search"
                      placeholder="جستجو بر اساس تیتر تالار"
                  />
                </b-col>

              </b-row>

            </b-col>


            <b-table
                ref="refUserListTable"
                class="position-relative"
                :items="Forums"
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
                     @click="SetSelectedForum(data.item)"
                >
                  <feather-icon icon="TrashIcon" size="20" class="text-danger" />
                </div>

              </template>
              <template #cell(userInfo)="data">
                <router-link :to="`/apps/users/Detail/${data.item.userInfo.userName}`">
                  <span v-if="data.item.userInfo">{{data.item.userInfo.userName}}@</span>
                </router-link>

              </template>
              <template #cell(View)="data">

                <b-link class="cursor-pointer"  v-b-modal.modal-View @click="SetSelectedForum(data.item)">
                  <feather-icon icon="EyeIcon" size="20" class="text-primary" />
                </b-link>

              </template>

            </b-table>
          </b-tab>
          <b-tab >
            <template #title >
              <div @click="status=3">
                <feather-icon icon="TrelloIcon" />
                <span>گزارش شده توسط ربات</span>
              </div>

            </template>
            <b-col lg="12" md="12" class="my-2">
              <b-row>
                <b-col lg="12" md="12" class="my-2">
                  <b-form-input
                      id="basicInput3"
                      v-model="search"
                      placeholder="جستجو بر اساس تیتر تالار"
                  />
                </b-col>

              </b-row>

            </b-col>


            <b-table
                ref="refUserListTable"
                class="position-relative"
                :items="Forums"
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
                     @click="SetSelectedForum(data.item)"
                >
                  <feather-icon icon="TrashIcon" size="20" class="text-danger" />
                </div>

              </template>
              <template #cell(userInfo)="data">
                <router-link :to="`/apps/users/Detail/${data.item.userInfo.userName}`">
                  <span v-if="data.item.userInfo">{{data.item.userInfo.userName}}@</span>
                </router-link>

              </template>
              <template #cell(View)="data">

                <b-link class="cursor-pointer"  v-b-modal.modal-View @click="SetSelectedForum(data.item)">
                  <feather-icon icon="EyeIcon" size="20" class="text-primary" />
                </b-link>

              </template>

            </b-table>
          </b-tab>
          <b-tab>
            <template #title >
              <div @click="status=4">
                <feather-icon icon="UserIcon" />
                <span>گزارش شده توسط کاربران</span>
              </div>

            </template>
            <b-col lg="12" md="12" class="my-2">
              <b-row>
                <b-col lg="12" md="12" class="my-2">
                  <b-form-input
                      id="basicInput4"
                      v-model="search"
                      placeholder="جستجو بر اساس تیتر تالار"
                  />
                </b-col>

              </b-row>

            </b-col>


            <b-table
                ref="refUserListTable"
                class="position-relative"
                :items="Forums"
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
              <template #cell(userInfo)="data">
                <router-link :to="`/apps/users/Detail/${data.item.userInfo.userName}`">
                  <span v-if="data.item.userInfo">{{data.item.userInfo.userName}}@</span>
                </router-link>

              </template>
              <template #cell(Delete)="data">

                <div class="cursor-pointer d-flex flex-row"
                     v-b-modal.modal-delete
                     @click="SetSelectedForum(data.item)"
                >
                  <feather-icon icon="TrashIcon" size="20" class="text-danger" />
                </div>

              </template>

              <template #cell(View)="data">

                <b-link class="cursor-pointer"  v-b-modal.modal-View @click="SetSelectedForum(data.item)">
                  <feather-icon icon="EyeIcon" size="20" class="text-primary" />
                </b-link>

              </template>

            </b-table>
          </b-tab>
        </b-tabs>



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
  BBadge, BDropdown, BDropdownItem, BPagination, BOverlay, BModal, BFormGroup,BFormSelect, BTabs, BTab, BCardText
} from 'bootstrap-vue'
import vSelect from 'vue-select'

import ForumGetAllRequest from '@/libs/Api/Forum/ForumGetAllRequest'
import DeleteForum from '@/libs/Api/Forum/DeleteForum'
import ChangeForumStatus from '@/libs/Api/Forum/ChangeForumStatus'
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
    BCardText,
    BTabs,
    BTab,
    vSelect,
  },
  title: 'لیست  تالار ها  - پنل ادمین بانو کلاب',
  name: 'UsersList',

  data() {
    return {
      Forums: null,
      totalCount: null,
      showDeleteModal: false,
      currentPage: 1,
      status:1,
      deleteItem: null,
      perPage: 5,
      ForumStatus:[
        {
          name:'فعال',
          status:1
        },
        {
          name:'غیر فعال',
          status:2
        },

      ],
      SelectedForumStatus:0,
      perPageOptions: [10, 20, 30, 40, 50],
      myTableColumns: [
        {
          key: 'forumId',
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
          key: 'userInfo',
          label: 'نام کاربری'
        },

        {
          key: 'Delete',
          label: 'حذف'
        },
        {
          key: 'View',
          label: 'مشاهده'
        },
        // { key: 'parentId'},
      ],
      SelectedCategoryId:0,
      pageNumber: 1,
      count: 10,
      search: '',
      SelectedForum: null,
      tagId:0

    }
  },
  async created() {
    await this.GetAllForum()
  },

  watch: {
    search: function () {
      this.GetAllForum()
    },
    currentPage: function () {
      this.GetAllForum()
    },
    status:function (){
      this.GetAllForum();
    }
  },
  methods: {
    async ChangeStatus(){
      let _this = this
      let changeForumStatus = new ChangeForumStatus(_this)
      let data = {

        status:this.SelectedForumStatus,
        forumId:this.SelectedForum.forumId
      }
      changeForumStatus.setParams(data)
      await changeForumStatus.fetch(function (content) {
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `وضعیت تالار با موفقیت تغییر یافت`,
          },
        })
     _this.GetAllForum()
      }, function (error) {
        console.log(error)
      })
    },
    OpenChangeStatus(){
      this.$refs.OpenStatusBtn.click();
    },
    async  DeleteForum(){
      let _this = this
      let deleteForum = new DeleteForum(_this)

      deleteForum.setId(this.SelectedForum.forumId)
      await deleteForum.fetch(function (content) {
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `تالار حذف شد.`,
          },
        })
        _this.GetAllForum();

      }, function (error) {
        console.log(error)
      })
    },
    SetSelectedForum(item) {
      this.SelectedForum = item
    },

    async GetAllForum() {
      let _this = this
      let forumGetAllRequest = new ForumGetAllRequest(_this)
      let data = {
        pageNumber: _this.currentPage,
        search: _this.search,
        noComments:'',
        mostRated:'',
        mostComments:'',
        status:this.status
      }
      forumGetAllRequest.setParams(data)
      await forumGetAllRequest.fetch(function (content) {
        _this.Forums = content.forums
        _this.totalCount = content.forumsCount
      }, function (error) {
        console.log(error)
      })
    },

  },
}
</script>

<style scoped>

</style>
