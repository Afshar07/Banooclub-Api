<template>
  <b-overlay
      :show="Services === null"
      rounded="sm"
  >
    <div v-if="Services !== null">
      <b-modal
          id="modal-delete"
          centered
          ok-title="بله"

          cancelTitle="خیر"
          @ok="DeleteService"
      >
        <span>آیا از حذف کردن این خدمت اطمینان دارید ؟ </span>
      </b-modal>
      <b-modal
          id="modal-view"
          ok-title="تغییر وضعیت"
          cancelTitle="بستن"
          centered
          size="lg"
          @ok="OpenStatusModal"
          title="مشاهده خدمت"
      >
        <serviceCard v-if="SelectedService!==null" :ServiceData="SelectedService"></serviceCard>
        <span ref="statusBtn" v-b-modal.modal-status style="display: none"></span>
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
                v-model="SelectedPostStatus"
                :options="PostStatus"
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
          <b-row>
            <b-col lg="12" md="12" class="my-2">
              <b-form-input
                  id="basicInput"
                  v-model="search"
                  placeholder="جستجو بر اساس نام خدمت"
              />
            </b-col>

          </b-row>

        </b-col>


        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="Services"
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
                 @click="SetSelectedService(data.item)"
            >
              <feather-icon icon="TrashIcon" size="20" class="text-danger" />
            </div>

          </template>
          <template #cell(status)="data">


            <b-badge v-if="data.item.status===1" variant="success">
              <feather-icon
                  icon="ActivityIcon"
                  class="mr-25"
              />
              <span>فعال</span>
            </b-badge>
            <b-badge v-if="data.item.status===2" variant="danger">
              <feather-icon
                  icon="MinusIcon"
                  class="mr-25"
              />
              <span>غیر فعال</span>
            </b-badge>
            <b-badge v-if="data.item.status===3" variant="primary">
              <feather-icon
                  icon="MinusIcon"
                  class="mr-25"
              />
              <span>نیاز به بازبینی مجدد</span>
            </b-badge>


          </template>

          <template #cell(view)="data">


            <feather-icon @click="SetSelectedService(data.item)" v-b-modal.modal-view icon="EyeIcon" size="20" class="text-primary cursor-pointer" />


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

import ServiceGetAllRequest from '@/libs/Api/Service/ServiceGetAll'

import ToastificationContent from '@core/components/toastification/ToastificationContent'
import ServiceDeleteRequest from '@/libs/Api/Service/ServiceDeleteRequest'
import serviceCard from "@/views/components/service/serviceCard";
import ServiceChangeStatus from "@/libs/Api/Service/ServiceChangeStatus";
export default {
  components: {
    serviceCard,
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
  title: 'خدمات تایید شده  - پنل ادمین بانو کلاب',
  name: 'UsersList',
  data() {
    return {
      Services: null,
      totalCount: null,
      showDeleteModal: false,
      currentPage: 1,
      deleteItem: null,
      perPage: 10,
      perPageOptions: [10, 20, 30, 40, 50],
      myTableColumns: [
        {
          key: 'servicePackId',
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
          key: 'status',
          label: 'وضعیت'
        },
        {
          key: 'Delete',
          label: 'حذف'
        },
        {
          key: 'view',
          label: 'مشاهده'
        },
        // { key: 'parentId'},
      ],
      SelectedCategoryId:0,
      pageNumber: 1,
      count: 10,
      search: '',
      SelectedService: null,
      SelectedPostStatus:1,
      PostStatus:[
        {
          name:'غیرفعال',
          status:2
        },
        {
          name:'نیاز به بازبینی مجدد',
          status:3
        }
      ],

    }
  },
  async created() {
    await this.GetAllServices()
  },

  watch: {
    search: function () {
      this.GetAllServices()
    },
    currentPage: function () {
      this.GetAllServices()
    }
  },
  methods: {
    async ChangeStatus () {
      let _this = this
      let serviceChangeStatus = new ServiceChangeStatus(_this)

      let data ={
        status:this.SelectedPostStatus,
        serviceId: this.SelectedService.servicePackId,
      }
      serviceChangeStatus.setParams(data)
      await serviceChangeStatus.fetch(()=>{
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `وضعیت خدمت با موفقیت تغییر یافت`,
          },
        })

        this.GetAllServices();
      },()=>{

      })
    },

    OpenStatusModal(){
      this.$refs.statusBtn.click();
    },
    async  DeleteService(){
      let _this = this
      let serviceDeleteRequest = new ServiceDeleteRequest(_this)

      serviceDeleteRequest.setId(this.SelectedService.servicePackId)
      await serviceDeleteRequest.fetch(function (content) {
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `خدمت حذف شد.`,
          },
        })
        _this.GetAllServices();

      }, function (error) {
        console.log(error)
      })
    },
    SetSelectedService(item) {
      this.SelectedService = item
    },

    async GetAllServices() {
      let _this = this
      let serviceGetAllRequest = new ServiceGetAllRequest(_this)
      let data = {
        pageNumber: _this.currentPage,
        search: _this.search,
        status:1
      }
      serviceGetAllRequest.setParams(data)
      await serviceGetAllRequest.fetch(function (content) {
        _this.Services = content.services
        _this.totalCount = content.servicesCount
      }, function (error) {
        console.log(error)
      })
    },


  },
}
</script>

<style scoped>

</style>
