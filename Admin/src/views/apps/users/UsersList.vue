<template>
  <b-overlay
      :show="users === null"
      rounded="sm"
  >
    <div v-if="users !== null">
      <b-modal
          id="modal-status"
          centered
          ok-title="تغییر سطح دسترسی"
          cancelTitle="بستن"
          @ok="ChangeUserRole"
      >
        <div class="row">
          <div class="col-md-12">
            <span>تغییر سطح دسترسی به: </span>
          </div>
          <div class="col-md-12 my-2">
            <v-select
                v-model="SelectedUserType"
                :options="UserTypes"
                label="name"
                :reduce="name => name.status"
                :clearable="false"
                class="per-page-selector d-inline-block w-100"
            />
          </div>
        </div>
      </b-modal>
      <!-- Table Container Card -->
      <b-card
          no-body
          class="mb-0"
      >
        <b-col lg="12" md="12" class="my-2">
          <b-form-group

          >
            <b-form-input
                id="basicInput"
                v-model="search"
                placeholder="جستجو بر اساس نام کاربری"
            />
          </b-form-group>
        </b-col>


        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="users"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >

          <!-- Column: delete -->


          <template #cell(type)="data">


            <b-badge v-if="data.item.type===2" variant="primary">
              سوپر ادمین
            </b-badge>
            <b-badge v-if="data.item.type===3" variant="primary">
              فروشنده
            </b-badge>
            <b-badge v-if="data.item.type===1" variant="primary">
              کاربر
            </b-badge>



          </template>
          <template #cell(edit)="data">


            <feather-icon @click="SetSelectedUser(data.item)" v-b-modal.modal-status icon="EditIcon" size="20" class="text-primary cursor-pointer" />


          </template>
          <template #cell(view)="data">


            <feather-icon @click="SetSelectedUser(data.item)" icon="EyeIcon" size="20" class="text-primary cursor-pointer" />


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
            <div v-if="SelectedUser!==null" class="col-md-12 my-1 bg-white p-1 border-top box-shadow-2">
              <div class="row">
                <div class="col-md-3">
                  <span  class="text-secondary">نام  : </span>
                  <span>{{SelectedUser.name}}</span>

                </div>
                <div class="col-md-3">
                  <span  class="text-secondary">نام خانوادگی : </span>
                  <span>{{SelectedUser.familyName}}</span>

                </div>
                <div class="col-md-3 ">
                  <span  class="text-secondary">نام کاربری: </span>
                  <span>{{SelectedUser.userName}}</span>

                </div>
                <div class="col-md-6 my-25 ">
                  <span  class="text-secondary">شماره موبایل : </span>
                  <span v-if="SelectedUser.mobile!==''">{{SelectedUser.mobile}}</span>
                  <span v-else>بدون شماره موبایل</span>
                </div>
                <div class="col-md-12  d-flex align-items-center gap-2">
                  <span  class="text-secondary">آدرس ایمیل: </span>
                  <span>{{SelectedUser.email!==''?SelectedUser.email:'این کاربر آدرس ایمیل ثبت نکرده است'}}</span>


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
import ProductCategoryGetAllRequest from "@/libs/Api/Product/ProductCategoryGetAllRequest";
import {
  BCard, BRow, BCol, BFormInput, BButton, BTable, BMedia, BAvatar, BLink,
  BBadge, BDropdown, BDropdownItem, BPagination,BOverlay,BModal,BFormGroup
} from 'bootstrap-vue'
import vSelect from 'vue-select'
import UsersChangeUserRoleRequest from "@/libs/Api/Users/UsersChangeUserRoleRequest";

import ToastificationContent from "@core/components/toastification/ToastificationContent";

import UsersGetAllRequest from '@/libs/Api/Users/UsersGetAllRequest'

export default {
  title: 'لیست کاربران - پنل ادمین بانو کلاب',
  name: "UsersList",
  data(){
    return{
      users:null,
      totalCount:null,
      showDeleteModal:false,
      currentPage:1,
      deleteItem : null,
      perPage:10,
      SelectedUserType:0,
      UserTypes:[
        {
          name:'کاربر',
          status:1
        },

        {
          name:'فروشنده',
          status:3
        },

      ],
      perPageOptions:[10, 20, 30 , 40 , 50],
      myTableColumns : [
        { key: 'userId',label:'شناسه'},
        { key: 'userName',label:'نام کاربری'},
        { key: 'name',label:'نام'},
        { key: 'familyName',label:'نام خانوادگی'},
        { key: 'type',label:'سطح دسترسی'},
        { key: 'edit',label:'تغییر سطح دسترسی'},
        { key: 'view',label:'مشاهده'},
        // { key: 'parentId'},
      ],
      pageNumber:1,
      count:10,
      search:'',
      SelectedUser:null

    }
  },
  async created(){
    await this.getAllUsers()
  },
  components:{

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
    vSelect,
  },
  watch:{
    search:function (){
      this.getAllUsers()
    },
    currentPage:function (){
      this.getAllUsers()
    }
  },
  methods:{
    SetSelectedUser(item){
      this.SelectedUser = item
    },
    async ChangeUserRole(){
      let _this = this;
      let usersChangeUserRoleRequest = new UsersChangeUserRoleRequest(_this);
      let data = {

        userId:this.SelectedUser.userId,
        status:this.SelectedUserType
      }
      usersChangeUserRoleRequest.setParams(data);
      await usersChangeUserRoleRequest.fetch(function (content){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `سطح دسترسی کاربر تغییر یافت`,
          },
        })
        _this.getAllUsers();
      },function (error){
        console.log(error)
      })
    },

    async getAllUsers(){
      let _this = this;
      let usersGetAllRequest = new UsersGetAllRequest(_this);
      let data = {
        pageNumber:_this.currentPage,
        count:_this.count,
        search:_this.search
      }
      usersGetAllRequest.setParams(data);
      await usersGetAllRequest.fetch(function (content){
        _this.users = content.users;
        _this.totalCount = content.usersCount
      },function (error){
        console.log(error)
      })
    },


  },
}
</script>

<style scoped>

</style>
