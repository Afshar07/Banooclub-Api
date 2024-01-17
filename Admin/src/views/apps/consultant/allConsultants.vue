<template>
  <b-overlay
      :show="overlay"
      rounded="sm"
  >
    <div v-if="consultants !== null">

      <b-modal
          id="modal-delete"
          cancelTitle="بستن"
          centered
          ok-title="ثبت"
          @ok="deleteComment"
      >
        <div class="row">
          <span>آیا از حذف این نظر اطمینان دارید؟</span>
        </div>
      </b-modal>
      <!-- Table Container Card -->
      <b-card
          class="mb-0"
          no-body
      >
        <b-col class="my-2" lg="12" md="12">
          <b-row>
            <b-col class="my-2" cols="12" md="4">
              <b-form-input
                  id="basicInput"
                  v-model="debouncer"
                  placeholder="جستجو بر اساس نام مشاور"
              />
            </b-col>
            <b-col class="my-2" cols="12" md="2">
              <v-select
                  v-model="selectedIsPayed"
                  :options="isPayedStatuses"
                  :reduce="title => title.value"
                  dir="rtl"
                  label="title"
                  placeholder="وضعیت پرداخت"
              />
            </b-col>
            <b-col class="my-2" cols="12" md="2">
              <v-select
                  v-model="selectedStatus"
                  :options="statuses"
                  :reduce="title => title.value"
                  dir="rtl"
                  label="title"
                  placeholder="وضعیت مشاوره"
              />
            </b-col>
          </b-row>

        </b-col>


        <b-table
            ref="refUserListTable"
            :fields="myTableColumns"
            :items="consultantRequests"
            bordered
            class="position-relative"
            empty-text="موردی موجود نیست!"
            primary-key="id"
            responsive
            show-empty
            striped
        >
          <template #cell(createDate)="data">

            <span>{{ new Date(data.item.createDate).toLocaleDateString('fa-IR') }}</span>


          </template>
          <!-- Column: delete -->


          <template #cell(isPayed)="data">
            <b-badge v-if="data.item.isPayed" variant="primary">
              پرداخت شده
            </b-badge>
            <b-badge v-else variant="danger">
              پرداخت شده
            </b-badge>

          </template>

          <template #cell(status)="data">
            <b-badge v-if="data.item.status===1" variant="secondary">
              در انتظار
            </b-badge>
            <b-badge v-else variant="danger">
             تمام شده
            </b-badge>

          </template>
          <template #cell(View)="data">

            <b-link v-b-modal.modal-View class="cursor-pointer" @click="setSelectedConsultant(data.item)">
              <feather-icon class="text-primary" icon="EyeIcon" size="20"/>
            </b-link>

          </template>

        </b-table>


        <div class="mx-2 mb-2">
          <b-row>

            <b-col
                class="d-flex align-items-center justify-content-center justify-content-sm-start"
                cols="12"
                sm="6"
            >
              <!--            <span class="text-muted">Showing {{ dataMeta.from }} to {{ dataMeta.to }} of {{ dataMeta.of }} entries</span>-->
            </b-col>
            <!-- Pagination -->
            <b-col
                class="d-flex align-items-center justify-content-center justify-content-sm-end"
                cols="12"
                sm="6"
            >

              <b-pagination
                  v-model="currentPage"
                  :per-page="perPage"
                  :total-rows="totalCount"
                  class="mb-0 mt-1 mt-sm-0"
                  first-number
                  last-number
                  next-class="next-item"
                  prev-class="prev-item"
              >
                <template #prev-text>
                  <feather-icon icon="ChevronLeftIcon"
                                size="18"
                                style="transform: rotate(180deg)"
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
  BBadge, BDropdown, BDropdownItem, BPagination, BOverlay, BModal, BFormGroup, BFormSelect, BTabs, BTab, BCardText
} from 'bootstrap-vue'
import vSelect from 'vue-select'

import ChangeForumStatus from '@/libs/Api/Forum/ChangeForumStatus'
import ToastificationContent from '@core/components/toastification/ToastificationContent'
import {GetAllConsultants} from "@/libs/Api/consultant";

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
      consultantRequests: null,
      isPayedStatuses: [
        {
          title: 'پرداخت شده',
          value: 1
        },
        {
          title: 'پرداخت نشده',
          value: 2
        },
      ],
      selectedStatus: 0,
      statuses: [
        {
          title: 'در انتظار',
          value: 1
        },
        {
          title: 'تمام شده',
          value: 2
        },
      ],
      selectedIsPayed: 0,
      totalCount: null,
      currentPage: 1,
      timeout: null,
      status: 1,
      overlay: false,
      perPage: 5,
      consultantStatus: [
        {
          name: 'فعال',
          status: 1
        },
        {
          name: 'غیر فعال',
          status: 2
        },

      ],
      selectedConsultantStatus: 0,
      perPageOptions: [10, 20, 30, 40, 50],
      myTableColumns: [
        {
          key: 'id',
          label: 'شناسه'
        },
        {
          key: 'createDate',
          label: 'تاریخ ایجاد'
        },
        {
          key: 'consultFullname',
          label: 'نام مشاور'
        },
        {
          key: 'userFullname',
          label: 'نام مشاوره گیرنده'
        },
        {
          key: 'isPayed',
          label: 'وضعیت پرداخت'
        },
        {
          key: 'status',
          label: 'وضعیت مشاوره'
        },
        {
          key: 'reserveHour',
          label: 'تاریخ رزرو شده'
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
    selectedStatus: function () {
      this.getAllConsultants()
    },
    selectedIsPayed: function () {
      this.getAllConsultants()
    },

  },
  computed: {
    debouncer: {
      get() {
        return this.search
      },
      set(val) {
        if (this.timeout) {
          clearTimeout(this.timeout)
        }
        this.timeout = setTimeout(() => {
          this.search = val
        }, 600)
      }
    }
  },
  methods: {

    setSelectedConsultant(item) {
      this.selectedConsultant = item
    },
    async getAllConsultants() {
      let _this = this
      let getAllConsultants = new GetAllConsultants(_this)
      let data = {
        pageNumber: _this.currentPage,
        take: 10,
        isPayed: _this.selectedIsPayed === 1?true:_this.selectedIsPayed === 2?false:'',
        status: _this.selectedStatus ? _this.selectedStatus : '',
        consultFullname: _this.search
      }
      getAllConsultants.setParams(data)
      await getAllConsultants.fetch(function (content) {
        console.log(content)
        _this.consultantRequests = content.data
        _this.totalCount = content.total
      }, function (error) {
        console.log(error)
      })
    },

  },
}
</script>

<style scoped>

</style>
