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
            :items="Orders"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >

          <!-- Column: delete -->
          <template #cell(subOrders)="data">

            <div v-for="(el,idx) in data.item.subOrders" :key="idx" class="d-flex flex-column">
              <small>{{el.title}}</small>
            </div>


          </template>
          <template #cell(userName)="data">

            <router-link :to="`/apps/users/Detail/${data.item.userInfo.userName}`">
              <span v-if="data.item.userInfo">{{data.item.userInfo.userName}}@</span>
            </router-link>
          </template>

          <template #cell(OrderTitle)="data">

            <small v-if="data.item.adsInfo!==null"> برای خدمت {{data.item.adsInfo.title}} </small>
            <small v-if="data.item.serviceInfo!==null"> برای آگهی {{data.item.serviceInfo.title}} </small>
            <small v-else-if="data.item.adsInfo===null && data.item.serviceInfo===null"> - </small>

          </template>
          <template #cell(createDate)="data">

         <small>{{new Date(data.item.createDate).toLocaleDateString('fa-IR')}}</small>

          </template>
          <template #cell(status)="data">
            <b-badge v-if="data.item.status ===3" variant="danger">
              لغو شده
            </b-badge>
            <b-badge v-else-if="data.item.status === 1" variant="success">
            درحال پردازش
            </b-badge>
            <b-badge v-else-if="data.item.status === 2" variant="secondary">
            پرداخت شده
            </b-badge>


          </template>
          <template #cell(sumPrice)="data">
            <small>{{     new Intl.NumberFormat().format(data.item.sumPrice)}}</small>
            <small>تومان</small>
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

import OrderGetAllRequest from "@/libs/Api/Order/OrderGetAllRequest";


import ToastificationContent from '@core/components/toastification/ToastificationContent'

export default {
  title: 'لیست  سفارشات  - پنل ادمین بانو کلاب',
  name: 'UsersList',
  data() {
    return {
      Orders: null,
      totalCount: null,
      showDeleteModal: false,
      currentPage: 1,
      deleteItem: null,
      perPage: 10,
      CategoryName:'',
      perPageOptions: [10, 20, 30, 40, 50],
      myTableColumns: [
        {
          key: 'orderId',
          label: 'شناسه'
        },
        {
          key: 'subOrders',
          label: 'نام سفارش'
        },
        {
          key: 'description',
          label: 'توضیحات'
        },

        {
          key: 'OrderTitle',
          label: 'توضیحات سفارش'
        },
        {
          key: 'userName',
          label: 'نام کاربر'
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
          key: 'sumPrice',
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
    await this.GetAllOrders()
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
      this.GetAllOrders()
    },
    currentPage: function () {
      this.GetAllOrders()
    }
  },
  methods: {
    async GetAllOrders() {
      let _this = this
      let orderGetAllRequest = new OrderGetAllRequest(_this)
      let data = {
        pageNumber: _this.currentPage,
        count: _this.count,
        search: _this.search
      }
      orderGetAllRequest.setParams(data)
      await orderGetAllRequest.fetch(function (content) {
        _this.Orders = content.orders

        _this.totalCount = content.ordersCount
      }, function (error) {
        console.log(error)
      })
    },


  },
}
</script>

<style scoped>

</style>
