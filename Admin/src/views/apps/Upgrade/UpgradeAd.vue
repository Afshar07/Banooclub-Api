<template>
  <b-overlay
      :show="Plans === null"
      rounded="sm"
  >
    <div v-if="Plans !== null">
      <b-modal
          id="modal-delete"
          centered
          ok-title="بله"

          cancelTitle="خیر"
          @ok="DeletePlan"
      >
        <span>آیا از حذف کردن این پلن اطمینان دارید ؟ </span>
      </b-modal>
      <b-modal
          id="modal-edit"
          centered
          ok-title="ویرایش"
          ok-only

          @ok="UpdatePlan"
      >
        <b-form-group>
          <label >نام پلن</label>
          <b-form-input
              v-if="SelectedPlan!==null"
              type="text"
              placeholder="نام دسته بندی"
              v-model="SelectedPlan.title"
          />

        </b-form-group>
        <b-form-group>
          <label >قیمت پلن</label>
          <b-form-input
              v-if="SelectedPlan!==null"
              type="number"
              placeholder="قیمت پلن"
              v-model="SelectedPlan.price"
          />

        </b-form-group>

      </b-modal>
      <!--      <b-modal-->
      <!--          id="modal-Add"-->
      <!--          centered-->
      <!--          ok-title="افزودن دسته بندی"-->
      <!--          ok-only-->

      <!--          @ok.preventDefault()="AddCategory"-->
      <!--      >-->
      <!--        <b-form-group>-->
      <!--          <label for="email">نام دسته بندی</label>-->
      <!--          <b-form-input-->
      <!--              type="text"-->
      <!--              placeholder="نام دسته بندی"-->
      <!--              v-model="CategoryName"-->
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
            <b-col lg="12" md="12" class="my-2">
              <b-form-input
                  id="basicInput"
                  v-model="search"
                  placeholder="جستجو بر اساس نام پلن"
              />
            </b-col>
            <!--            <b-col lg="2" md="12" class="my-2">-->
            <!--              <b-button-->

            <!--                  variant="success"-->
            <!--                  v-b-modal.modal-Add-->

            <!--              >-->
            <!--                افزودن دسته بندی جدید-->
            <!--              </b-button>-->
            <!--            </b-col>-->
          </b-row>
          <b-form-group

          >

          </b-form-group>
        </b-col>


        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="FilteredPlans"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >

          <!-- Column: delete -->
          <template #cell(price)="data">
            <div class="d-flex align-items-center ">
              <span class="mx-25">{{ Intl.NumberFormat('fa-IR').format(data.item.price) }}</span>

              <small>تومان</small>

            </div>

          </template>
          <template #cell(Delete)="data">

            <div class="cursor-pointer d-flex flex-row"
                 v-b-modal.modal-delete
                 @click="SetSelectedPlan(data.item)"
            >
              <feather-icon icon="TrashIcon" size="20" class="text-danger" />
            </div>

          </template>

          <template #cell(Edit)="data">

            <div class="cursor-pointer d-flex flex-row"
                 v-b-modal.modal-edit
                 @click="SetSelectedPlan(data.item)"
            >
              <feather-icon class="text-primary" icon="EditIcon" size="20" />
            </div>

          </template>

        </b-table>
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
//
import ServicePlansGetAll from '@/libs/Api/Plans/ServicePlansGetAll'
import ServicePlanDeleteRequest from '@/libs/Api/Plans/ServicePlansDelete'
import ServicePlanUpdateRequest from '@/libs/Api/Plans/ServicePlansUpdate'

import ToastificationContent from '@core/components/toastification/ToastificationContent'

export default {
  title: ' پلن های ارتقا خدمت  - پنل ادمین بانو کلاب',
  name: 'UsersList',
  data() {
    return {
      Plans: null,
      totalCount: null,
      showDeleteModal: false,
      currentPage: 1,
      deleteItem: null,
      perPage: 10,
      CategoryName:'',
      perPageOptions: [10, 20, 30, 40, 50],
      myTableColumns: [
        {
          key: 'planId',
          label: 'شناسه'
        },
        {
          key: 'title',
          label: 'نام'
        },
        {
          key: 'price',
          label: 'قیمت'
        },
        {
          key: 'Delete',
          label: 'حذف'
        },
        {
          key: 'Edit',
          label: 'ویرایش'
        },
        // { key: 'parentId'},
      ],
      SelectedCategoryId:0,
      pageNumber: 1,
      count: 10,
      search: '',
      SelectedPlan: null,

    }
  },
  async created() {
    await this.GetAllPlans()
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

    currentPage: function () {
      this.GetAllPlans()
    }
  },
  computed:{
    FilteredPlans(){
      const data = this.Plans
      if(this.search===''){
        return data
      }  else{
        return data.filter((element)=>{
          return element.title.match(this.search) || element.planId === parseInt(this.search)
        })
      }
    }
  },
  methods: {
    // async AddCategory(){
    //   let _this = this
    //   if(_this.CategoryName!==''){
    //     let blogCategoryCreateRequest = new BlogCategoryCreateRequest(_this)
    //     let data = {
    //       name: this.CategoryName,
    //       seoDescription: this.CategoryName
    //     }
    //     blogCategoryCreateRequest.setRequestParamDataObj(data)
    //     await blogCategoryCreateRequest.fetch(function (content) {
    //       _this.$toast({
    //         component: ToastificationContent,
    //         position: 'bottom-center',
    //         props: {
    //           title: `عملیات موفق`,
    //           icon: 'CheckIcon',
    //           variant: 'success',
    //           text: `دسته بندی ساخته شد شد`,
    //         },
    //       })
    //       _this.GetAllBlogCategory();
    //       _this.CategoryName = ''
    //     }, function (error) {
    //       console.log(error)
    //     })
    //   }else{
    //     _this.$toast({
    //       component: ToastificationContent,
    //       position: 'bottom-center',
    //       props: {
    //         title: `عملیات ناموفق`,
    //         icon: 'AlertCircleIcon',
    //         variant: 'danger',
    //         text: `نام دسته بندی را وارد کنید`,
    //       },
    //     })
    //   }
    //
    //
    // },
    async UpdatePlan(){
      let _this = this
      let servicePlanUpdateRequest = new ServicePlanUpdateRequest(_this)
      let data = {
        planId: this.SelectedPlan.planId,
        createDate: this.createDate,
        price: parseInt(this.SelectedPlan.price) ,
        type: this.SelectedPlan.type,
        title: this.SelectedPlan. title,
        updateDate: this.SelectedPlan.updateDate,
        entityType:this.SelectedPlan.entityType
      }
      servicePlanUpdateRequest.setData(data)
      await servicePlanUpdateRequest.fetch(function (content) {
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `پلن بروزرسانی شد`,
          },
        })
        _this.GetAllPlans();
      }, function (error) {
        console.log(error)
      })
    },
    async  DeletePlan(){
      let _this = this
      let servicePlanDeleteRequest = new ServicePlanDeleteRequest(_this)
      servicePlanDeleteRequest.setId(this.SelectedPlan.planId)
      await servicePlanDeleteRequest.fetch(function (content) {

        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `پلن  حذف شد.`,
          },
        })
        _this.GetAllPlans();

      }, function (error) {
        console.log(error)
      })
    },
    SetSelectedPlan(item) {
      this.SelectedPlan = item
    },

    async GetAllPlans() {
      let _this = this
      let servicePlansGetAll = new ServicePlansGetAll(_this)
      let data = {
        pageNumber:_this.currentPage
      }
      servicePlansGetAll.setParams(data)
      await servicePlansGetAll.fetch(function (content) {
        _this.Plans = content.plans.filter(e=> e.entityType===2)
        _this.totalCount = content.plansCount
      }, function (error) {
        console.log(error)
      })
    },

  },
}
</script>

<style scoped>

</style>
