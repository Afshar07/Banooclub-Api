<template>
  <b-overlay
      :show="consultants === null"
      rounded="sm"
  >
    <div v-if="consultants !== null">
      <b-modal
          id="modal-View"
          centered
          ok-title="تغییر وضعیت"
          cancelTitle="بستن"
          @ok="OpenChangeStatus"
      >
        <div v-if="selectedConsultant!==null" class="row">
          <div class="col-md-12">
            <div class="d-flex align-items-center gap-2">
              <span class="text-secondary">نام مشاور : </span>
              <span>{{selectedConsultant.title}}</span>
            </div>
          </div>
          <div class="col-md-12 my-2">
            <div class="text-secondary" style="overflow-wrap: anywhere">
              {{selectedConsultant.description}}
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
                v-model="selectedConsultantStatus"
                :options="consultantStatus.filter(e=> e.status!== status)"
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
        <b-col lg="12" md="12" class="my-2">
          <b-row>
            <b-col lg="12" md="12" class="my-2">
              <b-form-input
                  id="basicInput"
                  v-model="search"
                  placeholder="جستجو بر اساس نام مشاور"
              />
            </b-col>

          </b-row>

        </b-col>


        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="consultants"
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
                 @click="setSelectedConsultant(data.item)"
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

            <b-link class="cursor-pointer"  v-b-modal.modal-View @click="setSelectedConsultant(data.item)">
              <feather-icon icon="EyeIcon" size="20" class="text-primary" />
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
  title: 'لیست  مشاور ها  - پنل ادمین بانو کلاب',
  name: 'allConsultants',

  data() {
    return {
      consultants: null,
      totalCount: null,
      currentPage: 1,
      status:1,
      deleteItem: null,
      perPage: 5,
      consultantStatus:[
        {
          name:'فعال',
          status:1
        },
        {
          name:'غیر فعال',
          status:2
        },

      ],
      selectedConsultantStatus:0,
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
      pageNumber: 1,
      count: 10,
      search: '',
      selectedConsultant: null,
    }
  },
  async created() {
    await this.getAllConsultants()
  },

  watch: {
    search: function () {
      this.getAllConsultants()
    },
    currentPage: function () {
      this.getAllConsultants()
    },

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
            text: `وضعیت مشاور با موفقیت تغییر یافت`,
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
            text: `مشاور حذف شد.`,
          },
        })
        _this.GetAllForum();

      }, function (error) {
        console.log(error)
      })
    },
    setSelectedConsultant(item) {
      this.selectedConsultant = item
    },
    async getAllConsultants() {
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
        _this.consultants = content.forums
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
