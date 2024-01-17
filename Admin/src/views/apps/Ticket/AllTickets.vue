<template>
  <b-overlay
      :show="AllTickets === null"
      rounded="sm"
  >
    <div v-if="AllTickets !== null">

      <!-- Table Container Card -->
      <b-card
          no-body
          class="mb-0">
        <b-col lg="12" md="12" class="my-2">
          <b-row>
            <b-col lg="10" md="12" class="my-2">
              <b-form-input
                  id="basicInput"
                  v-model="search"
                  placeholder="جستجو بر اساس نام کاربر "
              />
            </b-col>
            <b-col lg="2" md="12" class="my-2">
              <v-select
                  v-model="TicketType"
                  :clearable="false"
                  label="option"
                  class="bg-white"
                  :options="TicketTypesId"
                  :reduce="option => option.value"
              />
            </b-col>

          </b-row>

        </b-col>
        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="AllTickets"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >

          <!-- Column: delete -->

          <template #cell(userInfo)="data">
            <router-link :to="`/apps/users/Detail/${data.item.userInfo.userName}`">
            <small>{{data.item.userInfo.name+ ' ' + data.item.userInfo.familyName}}</small>
            </router-link>
          </template>
          <template #cell(recipientUserName)="data">
            <router-link :to="`/apps/users/Detail/${data.item.recipientUserName}`">
              <small>{{data.item.recipientUserName}}</small>
            </router-link>
          </template>
          <template #cell(Detail)="data">
              <b-link class="cursor-pointer" :to="`/apps/Ticket/Detail/${data.item.ticketId}`">
                <feather-icon icon="EyeIcon" size="20" class="text-primary" />
              </b-link>
          </template>
          <template #cell(type)="data">
            <span class="badge pill text-white bg-primary" v-if="data.item.type===0">تیکت همگانی</span>
            <span class="badge pill text-white bg-primary" v-if="data.item.type===1">احراز هویت</span>
            <span class="badge pill text-white bg-primary" v-if="data.item.type===2">پشتیبانی</span>
            <span class="badge pill text-white bg-primary" v-if="data.item.type===3">سایر</span>
          </template>
          <template #cell(status)="data">

            <b-badge v-if="data.item.isClosed === false && data.item.isRead === false" variant="secondary">
              منتظر پاسخ
            </b-badge>
            <b-badge v-else-if="data.item.isClosed === false && data.item.isRead === true" variant="success">
              پاسخ داده شده
            </b-badge>
            <b-badge v-else-if="data.item.isClosed == true" variant="danger">
              بسته شده
            </b-badge>
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

import GetAllTicket from '@/libs/Api/Ticket/TicketGetAllRequest'


import ToastificationContent from '@core/components/toastification/ToastificationContent'

export default {
  title: 'تیکت ها  - پنل ادمین بانو کلاب',
  name: 'UsersList',
  data() {
    return {
      AllTickets: null,
      totalCount: null,
      showDeleteModal: false,
      currentPage: 1,
      deleteItem: null,
      perPage: 10,
      CategoryName:'',
      TicketTypesId:[

        {
          option:"احراز هویت",
          value:1
        },
        {
          option:"پشتیبانی",
          value:2
        },
        {
          option:"سایر",
          value:3
        },



      ],
      TicketType:0,
      perPageOptions: [10, 20, 30, 40, 50],
      myTableColumns: [
        {
          key: 'ticketId',
          label: 'شناسه'
        },
        {
          key: 'title',
          label: 'موضوع'
        },
        {
          key: 'userInfo',
          label: 'توسط'
        },
        {
          key: 'recipientUserName',
          label: 'به'
        },
        {
          key: 'type',
          label: 'واحد'
        },
        {
          key: 'Detail',
          label: 'مشاهده'
        },
        {
          key: 'status',
          label: 'وضعیت'
        },
        // { key: 'parentId'},
      ],

      pageNumber: 1,
      count: 10,
      search: '',

      SelectedTicket: null,


    }
  },
  async created() {
    await this.GetAllTicket()
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
      this.GetAllTicket()
    },
    currentPage: function () {
      this.GetAllTicket()
    },
    TicketType:function (){
      this.GetAllTicket()
    }

  },
  methods: {

    async GetAllTicket() {
      let _this = this
      let getAllTicket = new GetAllTicket(_this)
      let data = {
        pageNumber: _this.currentPage,
        count: _this.count,
        searchCommand: _this.search,
        ticketType:this.TicketType
      }
      getAllTicket.setParams(data)
      await getAllTicket.fetch(function (content) {
        _this.AllTickets = content.tickets

        _this.totalCount = content.ticketCount
      }, function (error) {
        console.log(error)
      })
    },


  },
}
</script>

<style scoped>

</style>
