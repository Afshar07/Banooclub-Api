<template>
  <b-overlay
      :show="payments === null"
      rounded="sm"
  >
    <div v-if="payments !== null">
      <!--      <b-modal-->
      <!--          id="modal-delete"-->
      <!--          centered-->
      <!--          ok-title="بله"-->

      <!--          cancelTitle="خیر"-->
      <!--          @ok="DeleteNk"-->
      <!--      >-->
      <!--        <span>آیا از حذف کردن این کلمه اطمینان دارید ؟ </span>-->
      <!--      </b-modal>-->
      <!--      <b-modal-->
      <!--          id="modal-edit"-->
      <!--          centered-->
      <!--          ok-title="ویرایش"-->
      <!--          ok-only-->

      <!--          @ok="UpdateNk"-->
      <!--      >-->
      <!--        <b-form-group>-->
      <!--          <label for="email">نام کلمه</label>-->
      <!--          <b-form-input-->
      <!--              v-if="SelectedPostNk!==null"-->
      <!--              id="email"-->
      <!--              type="text"-->
      <!--              placeholder="نام دسته بندی"-->
      <!--              v-model="SelectedPostNk.name"-->
      <!--          />-->

      <!--        </b-form-group>-->

      <!--      </b-modal>-->
      <!--      <b-modal-->
      <!--          id="modal-Add"-->
      <!--          centered-->
      <!--          ok-title="افزودن کلمه منفی"-->
      <!--          ok-only-->

      <!--          @ok.preventDefault()="AddPostNk"-->
      <!--      >-->
      <!--        <b-form-group>-->
      <!--          <label for="email">کلمه</label>-->
      <!--          <b-form-input-->
      <!--              type="text"-->
      <!--              placeholder="کلمه"-->
      <!--              v-model="PostNkName"-->
      <!--          />-->
      <!--        </b-form-group>-->

      <!--      </b-modal>-->
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
                  placeholder="جستجو"
              />
            </b-col>

          </b-row>
          <b-form-group

          >

          </b-form-group>
        </b-col>


        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="payments"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >

          <!-- Column: delete -->

          <template #cell(status)="data">


            <b-badge v-if="data.item.status === -2 || data.item.status ===-4" variant="danger">
              لغو شده
            </b-badge>
            <b-badge v-else-if="data.item.status === 0" variant="success">
              پرداخت شده
            </b-badge>
            <b-badge v-else-if="data.item.status === -122" variant="secondary">
              در انتظار پرداخت
            </b-badge>
            <b-badge v-else-if="data.item.status === -49" variant="warning">
              تراکنش تکراری
            </b-badge>
            <b-badge v-else-if="data.item.status === -3" variant="info">
              در انتظار پاسخ

            </b-badge>


          </template>

          <template #cell(createDate)="data">

            <small>{{new Date(data.item.createDate).toLocaleDateString('fa-IR')}}</small>

          </template>
          <template #cell(userInfo)="data">

            <small>{{ data.item.userInfo.name + ' ' + data.item.userInfo.familyName   }}</small>

          </template>
          <template #cell(userName)="data">

            <router-link :to="`/apps/users/Detail/${data.item.userInfo.userName}`">
              <span v-if="data.item.userInfo">{{data.item.userInfo.userName}}@</span>
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


  </b-overlay>
</template>

<script>

import {
  BCard, BRow, BCol, BFormInput, BButton, BTable, BMedia, BAvatar, BLink,
  BBadge, BDropdown, BDropdownItem, BPagination, BOverlay, BModal, BFormGroup,BFormSelect
} from 'bootstrap-vue'
import vSelect from 'vue-select'

import PaymentGetAllRequest from '@/libs/Api/Payment/PaymentGetAllRequest'


import ToastificationContent from '@core/components/toastification/ToastificationContent'

export default {
  title: 'لیست پرداخت ها  - پنل ادمین بانو کلاب',
  name: 'UsersList',
  data() {
    return {
      payments: null,
      totalCount: null,
      showDeleteModal: false,
      currentPage: 1,
      deleteItem: null,
      perPage: 10,
      CategoryName:'',
      perPageOptions: [10, 20, 30, 40, 50],
      myTableColumns: [
        {
          key: 'paymentId',
          label: 'شناسه'
        },
        {
          key: 'transId',
          label: 'شناسه پرداخت'
        },
        {
          key: 'refId',
          label: 'کد پیگیری'
        },
        {
          key: 'userInfo',
          label: 'نام و نام خانوادگی'
        },
        {
          key: 'userName',
          label: 'نام کاربری'
        },
        {
          key: 'createDate',
          label: ' تاریخ'
        },
        {
          key: 'status',
          label: 'وضعیت'
        },
        {
          key: 'amount',
          label: 'مجموع'
        },
        // { key: 'parentId'},
      ],

      pageNumber: 1,
      count: 10,

      search: '',



    }
  },
  async created() {
    await this.GetAllPayments()
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
      this.GetAllPayments()
    },
    currentPage: function () {
      this.GetAllPayments()
    }
  },
  methods: {
    // async UpdateNk(){
    //   let _this = this
    //   let updatePostNk = new UpdatePostNk(_this)
    //   let data = {
    //     postNkId:this.SelectedPostNk.postNkId,
    //     name: this.SelectedPostNk.name,
    //   }
    //   updatePostNk.setRequestParamDataObj(data)
    //   await updatePostNk.fetch(function (content) {
    //     _this.$toast({
    //       component: ToastificationContent,
    //       position: 'bottom-center',
    //       props: {
    //         title: `عملیات موفق`,
    //         icon: 'CheckIcon',
    //         variant: 'success',
    //         text: `کلمه بروزرسانی شد`,
    //       },
    //     })
    //     _this.GetAllPostNk();
    //   }, function (error) {
    //     console.log(error)
    //   })
    // },
    // async AddPostNk(){
    //   let _this = this
    //   let addPostNk = new AddPostNk(_this)
    //   let data = {
    //     name: this.PostNkName,
    //   }
    //   addPostNk.setRequestParamDataObj(data)
    //   await addPostNk.fetch(function (content) {
    //     _this.$toast({
    //       component: ToastificationContent,
    //       position: 'bottom-center',
    //       props: {
    //         title: `عملیات موفق`,
    //         icon: 'CheckIcon',
    //         variant: 'success',
    //         text: `کلمه اضافه شد`,
    //       },
    //     })
    //     _this.GetAllPostNk();
    //   }, function (error) {
    //     console.log(error)
    //   })
    // },
    // async  DeleteNk(){
    //   let _this = this
    //   let deletePostNk = new DeletePostNk(_this)
    //   let data = {
    //     id:this.SelectedPostNk.postNkId
    //   }
    //   deletePostNk.setParams(data)
    //   await deletePostNk.fetch(function (content) {
    //     _this.$toast({
    //       component: ToastificationContent,
    //       position: 'bottom-center',
    //       props: {
    //         title: `عملیات موفق`,
    //         icon: 'CheckIcon',
    //         variant: 'success',
    //         text: `کلمه حذف شد.`,
    //       },
    //     })
    //     _this.GetAllPostNk();
    //
    //   }, function (error) {
    //     console.log(error)
    //   })
    // },
    // SetSelectedPostNk(item) {
    //   this.SelectedPostNk = item
    // },

    async GetAllPayments() {
      let _this = this
      let paymentGetAllRequest = new PaymentGetAllRequest(_this)
      let data = {
        pageNumber: _this.currentPage,
        count: _this.count,
        search: _this.search
      }
      paymentGetAllRequest.setParams(data)
      await paymentGetAllRequest.fetch(function (content) {
        _this.payments = content.payments

        _this.totalCount = content.paymentsCount
      }, function (error) {
        console.log(error)
      })
    },


  },
}
</script>

<style scoped>

</style>
