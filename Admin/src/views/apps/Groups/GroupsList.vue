<template>
  <b-overlay
      :show="Groups === null"
      rounded="sm"
  >
    <div v-if="Groups !== null">
      <b-modal
          id="modal-delete"
          centered
          ok-title="بله"

          cancelTitle="خیر"
          @ok="DeleteGroup"
      >
        <span>آیا از حذف کردن این گروه اطمینان دارید ؟ </span>
      </b-modal>
      <b-modal
          id="modal-status"
          centered
          ok-title="تغییر وضعیت"
          cancelTitle="بستن"
          @ok="ChangeGroupStatus"
      >
        <div class="row">
          <div class="col-md-12">
            <span>آیا از انجام این عملیات اطمینان دارید؟ </span>
          </div>
<!--          <div class="col-md-12 my-2">-->
<!--            <v-select-->
<!--                v-model="SelectedGroupStatus"-->
<!--                :options="GroupsStatus"-->
<!--                label="name"-->
<!--                :reduce="name => name.status"-->
<!--                :clearable="false"-->
<!--                class="per-page-selector d-inline-block w-100"-->
<!--            />-->
<!--          </div>-->
        </div>
      </b-modal>

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
                  placeholder="جستجو بر اساس نام گروه"
              />
            </b-col>

          </b-row>
        </b-col>


        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="Groups"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >

          <!-- Column: delete -->
          <template #cell(fileData)="data">

            <img :src="`https://api.banooclub.simagar.com/${data.item.fileData}`" width="50px" height="50px" class="rounded-circle" alt="">

          </template>
          <template #cell(createDate)="data">

          <small>{{new Date(data.item.createDate).toLocaleDateString('fa-IR')}}</small>

          </template>
          <template #cell(isActive)="data">

            <b-badge v-if="data.item.isActive"  variant="success">
              فعال
            </b-badge>
            <b-badge v-else variant="danger">
              غیر فعال
            </b-badge>

          </template>
          <template #cell(Delete)="data">

            <div class="cursor-pointer d-flex flex-row"
                 v-b-modal.modal-delete
                 @click="SetSelectedGroup(data.item)"
            >
              <feather-icon icon="TrashIcon" size="20" class="text-danger" />
            </div>

          </template>
          <template #cell(View)="data">


              <feather-icon @click="SetSelectedGroup(data.item)" icon="EyeIcon" size="20" class="text-primary" />


          </template>

          <template #cell(status)="data">

            <div class="cursor-pointer d-flex flex-row"
                 v-b-modal.modal-status
                 @click="SetSelectedGroup(data.item)"
            >
              <feather-icon class="text-primary" icon="EditIcon" size="20" />
            </div>

          </template>

        </b-table>
        <div v-if="SelectedGroup" class=" my-3 col-md-12">
          لیست کاربران گروه
          <b-table
              ref="refUserListTable"
              class="position-relative"
              :items="SelectedGroup.userInfoes"
              responsive
              :fields="UserTableColumns"
              primary-key="id"
              show-empty
              bordered
              striped
              empty-text="موردی موجود نیست!"
          >

            <!-- Column: delete -->





            <template #cell(name)="data">

             <small>{{data.item.name + ' ' + data.item.familyName }}</small>
            </template>

          </b-table>
        </div>
        <div class="mx-2 mb-2">
          <b-row>
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

import GroupGetAll from '@/libs/Api/Groups/GroupsGetAll'
import DeleteGroup from '@/libs/Api/Groups/DeleteGroup'


import ToastificationContent from '@core/components/toastification/ToastificationContent'
import ChangeGroupStatus from "@/libs/Api/Groups/ChangeGroupStatus";

export default {
  title: 'لیست گروه ها  - پنل ادمین بانو کلاب',
  name: 'UsersList',
  data() {
    return {
      Groups: null,
      totalCount: null,
      showDeleteModal: false,
      currentPage: 1,
      deleteItem: null,
      perPage: 10,
      perPageOptions: [10, 20, 30, 40, 50],
      UserTableColumns:[
        {
          key: 'userId',
          label: 'شناسه'
        },
        {
          key: 'userName',
          label: ' نام کاربری'
        },
        {
          key: 'name',
          label: 'نام'
        },
      ],
      myTableColumns: [
        {
          key: 'groupId',
          label: 'شناسه'
        },
        {
          key: 'fileData',
          label: 'عکس گروه'
        },
        {
          key: 'name',
          label: 'نام'
        },
        {
          key: 'createDate',
          label: 'تاریخ ایجاد'
        },
        {
          key: 'isActive',
          label: 'وضعیت'
        },
        {
          key: 'status',
          label: 'تغییر وضعیت'
        },
        {
          key: 'Delete',
          label: 'حذف'
        },
        {
          key: 'View',
          label: 'مشاهده کاربران'
        },
      ],
      pageNumber: 1,
      count: 10,
      search: '',
      SelectedGroupStatus:0,
      GroupsStatus:[
        {
          name:'فعال',
          status:1
        },
        {
          name:'غیر فعال',
          status:2
        },
      ],
      SelectedGroup: null,
    }
  },
  async created() {
    await this.GetAllGroups()
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
      this.GetAllGroups()
    },
    currentPage: function () {
      this.GetAllGroups()
    }
  },
  methods: {
    async ChangeGroupStatus(){
  let _this = this
      let changeGroupStatus =  new ChangeGroupStatus(_this)
      changeGroupStatus.setId(this.SelectedGroup.groupId)
      await  changeGroupStatus.fetch((content)=>{

        if (content.data){
          _this.$toast({
            component: ToastificationContent,
            position: 'bottom-center',
            props: {
              title: `عملیات موفق`,
              icon: 'CheckIcon',
              variant: 'success',
              text: `وضعیت گروه با موفقیت تغییر یافت`,
            },
          })
          _this.GetAllGroups();
        }else{
          _this.$toast({
            component: ToastificationContent,
            position: 'bottom-center',
            props: {
              title: `عملیات موفق`,
              icon: 'CheckIcon',
              variant: 'success',
              text: `وضعیت گروه با موفقیت تغییر یافت`,
            },
          })
          _this.GetAllGroups();
        }

      },(e)=>{
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `وضعیت گروه با موفقیت تغییر یافت`,
          },
        })
        _this.GetAllGroups();
      })
    },
    async  DeleteGroup(){
      let _this = this
      let deleteGroup = new DeleteGroup(_this)
      deleteGroup.setId(this.SelectedGroup.groupId)
      await deleteGroup.fetch(function (content) {
        console.log(content)

          _this.$toast({
            component: ToastificationContent,
            position: 'bottom-center',
            props: {
              title: `عملیات ناموفق`,
              icon: 'AlertCircleIcon',
              variant: 'success',
              text: `گروه با موفقیت حذف شد`,
            },
          })
          _this.GetAllGroups();
      }, function (error) {
        console.log(error)
      })
    },
    SetSelectedGroup(item) {
      this.SelectedGroup = item
    },
    async GetAllGroups() {
      let _this = this
      let groupGetAll = new GroupGetAll(_this)

      await groupGetAll.fetch(function (content) {
        _this.Groups = content

      }, function (error) {
        console.log(error)
      })
    },

  },
}
</script>

<style scoped>

</style>
