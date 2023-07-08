<template>
  <b-overlay
      :show="overlay"
      rounded="sm"
  >
    <div v-if="consultants !== null">
      <!--      <b-modal-->
      <!--          id="modal-status"-->
      <!--          centered-->
      <!--          ok-title="بروزرسانی"-->
      <!--          cancelTitle="بستن"-->
      <!--          @ok="ChangeStatus"-->
      <!--      >-->
      <!--        <div class="row">-->
      <!--          <div class="col-md-12">-->
      <!--            <span>تغییر وضعیت به: </span>-->
      <!--          </div>-->
      <!--          <div class="col-md-12 my-2">-->
      <!--            <v-select-->
      <!--                v-model="selectedConsultantStatus"-->
      <!--                :options="consultantStatus.filter(e=> e.status!== status)"-->
      <!--                label="name"-->
      <!--                :reduce="name => name.status"-->
      <!--                :clearable="false"-->
      <!--                class="per-page-selector d-inline-block w-100"-->
      <!--            />-->
      <!--          </div>-->
      <!--        </div>-->
      <!--      </b-modal>-->


      <span ref="OpenStatusBtn" v-b-modal.modal-status style="display: none"></span>
      <!-- Table Container Card -->
      <b-card

          class="mb-0"
      >
        <b-row>
<!--          <b-col class="" cols="12" md="9">-->
<!--            <b-form-input-->
<!--                id="basicInput"-->
<!--                v-model="search"-->
<!--                placeholder="جستجو بر اساس نام مشاور"-->
<!--            />-->
<!--          </b-col>-->
          <b-col class="" cols="12" md="3">
            <v-select
                v-model="selectedStatus"
                :options="statuses"
                :reduce="title => title.value"
                dir="rtl"
                placeholder="وضعیت"
                label="title"
            />
          </b-col>

        </b-row>


        <b-table
            ref="refUserListTable"
            :fields="myTableColumns"
            :items="consultants"
            bordered
            class="position-relative mt-1"
            empty-text="موردی موجود نیست!"
            primary-key="id"
            responsive
            show-empty
            striped
        >


          <template #cell(View)="data">
            <router-link :to="{ name: 'apps-consultant-consultantRequestDetail',params:{id:data.item.id} }"
                         class="cursor-pointer">
              <feather-icon class="text-primary" icon="EyeIcon" size="20"/>
            </router-link>


          </template>
          <template #cell(status)="data">
            <b-badge v-if="data.item.status===1" variant="primary">
              تایید شده
            </b-badge>
            <b-badge v-if="data.item.status===2" variant="danger">
              رد شده
            </b-badge>
            <b-badge v-if="data.item.status===0" variant="secondary">
              در انتظار تایید
            </b-badge>


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
                class="d-flex  align-items-center justify-content-center justify-content-sm-end"
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
  BAvatar,
  BBadge,
  BButton,
  BCard,
  BCardText,
  BCol,
  BDropdown,
  BDropdownItem,
  BFormGroup,
  BFormInput,
  BFormSelect,
  BLink,
  BMedia,
  BModal,
  BOverlay,
  BPagination,
  BRow,
  BTab,
  BTable,
  BTabs
} from 'bootstrap-vue'
import vSelect from 'vue-select'
import {GetAllConsultantRequests} from "@/libs/Api/consultant";

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
  title: 'لیست درخواست های مشاور شدن  - پنل ادمین بانو کلاب',
  name: 'allConsultants',

  data() {
    return {
      consultants: null,
      totalCount: null,
      currentPage: 1,
      overlay: false,
      status: 1,
      selectedStatus: null,
      perPage: 5,
      statuses: [
        {
          title: 'در انتظار تایید',
          value: 0
        },
        {
          title: 'تایید شده',
          value: 1
        },
        {
          title: 'رد شده',
          value: 2
        },
      ],
      perPageOptions: [10, 20, 30, 40, 50],
      myTableColumns: [
        {
          key: 'id',
          label: 'شناسه'
        },
        {
          key: 'userFullName',
          label: 'نام و نام خانوادگی'
        },
        {
          key: 'phoneNumber',
          label: 'شماره تماس'
        },
        {
          key: 'natiaonCode',
          label: 'کد ملی'
        },
        {
          key: 'status',
          label: 'وضعیت'
        },
        {
          key: 'View',
          label: 'مشاهده'
        },
        // { key: 'parentId'},
      ],
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
    selectedStatus:function (){
      this.getAllConsultants()
    }

  },
  methods: {

    setSelectedConsultant(item) {
      this.selectedConsultant = item
    },
    async getAllConsultants() {
      let _this = this
      _this.overlay = true
      let getAllConsultantRequests = new GetAllConsultantRequests(_this)
      let data = {
        page: _this.currentPage,
        size: 10,
        status: _this.selectedStatus!==null ?_this.selectedStatus :''
      }
      getAllConsultantRequests.setParams(data)
      await getAllConsultantRequests.fetch(function (content) {
        _this.consultants = content.data.items
        _this.totalCount = content.data.totalCount
      }, function (error) {
        console.log(error)
      })
      _this.overlay = false

    },

  },
}
</script>

<style scoped>

</style>
