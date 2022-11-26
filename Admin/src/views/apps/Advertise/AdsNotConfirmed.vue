<template>
  <b-overlay
      :show="Ads === null"
      rounded="sm"
  >
    <div v-if="Ads !== null">
      <b-modal
          id="modal-1"
          centered
          ok-title="بله"
          :hideHeaderClose="false"
          cancelTitle="خیر"
          @ok="ChangeAdStatus"
      >
        <span>آیا از  فعال کردن این آگهی اطمینان دارید ؟ </span>
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
                placeholder="جستجو بر اساس نام آگهی"
            />
          </b-form-group>
        </b-col>


        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="Ads"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >

          <!-- Column: delete -->
          <template #cell(photos[0].base64)="data">
            <img v-if="data.item.photos.length>0"
                 :src="`https://banoclubapi.com/${data.item.photos[0].base64}`" width="75px" height="75px"
                 alt=""
            >

            <span v-else class="text-secondary">بدون عکس</span>
          </template>
          <template #cell(userInfo)="data">
            <router-link :to="`/apps/users/Detail/${data.item.userInfo.userName}`">
              <small >{{data.item.userInfo.userName}}</small>
            </router-link>

          </template>
          <template #cell(createDate)="data">
            <small>{{new Date(data.item.createDate).toLocaleDateString('fa-IR')}}</small>

          </template>

          <template #cell(action)="data">

            <b-button

                variant="success"
                v-b-modal.modal-1
                @click="SetSelectedAd(data.item)"
            >
               فعال کردن آگهی
            </b-button>


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
  BBadge, BDropdown, BDropdownItem, BPagination, BOverlay, BModal, BFormGroup
} from 'bootstrap-vue'
import vSelect from 'vue-select'

import GetAllAdsNotConfirmed from '@/libs/Api/ads/GetAllAdsNotConfirmed'
import ChangeStatus from '@/libs/Api/ads/ChangeStatus'
import ToastificationContent from '@core/components/toastification/ToastificationContent'

export default {
  title: 'لیست آگهی های تایید نشده  - پنل ادمین بانو کلاب',
  name: 'UsersList',
  data() {
    return {
      Ads: null,
      totalCount: null,
      showDeleteModal: false,
      currentPage: 1,
      deleteItem: null,
      perPage: 5,
      perPageOptions: [10, 20, 30, 40, 50],
      myTableColumns: [
        {
          key: 'adsId',
          label: 'شناسه'
        },
        {
          key: 'photos[0].base64',
          label: 'عکس اصلی آگهی'
        },
        {
          key: 'title',
          label: 'تیتر آگهی'
        },
        {
          key: 'createDate',
          label: 'تاریخ ایجاد آگهی'
        },
        {
          key: 'userInfo',
          label: 'توسط'
        },
        {
          key: 'action',
          label: 'عملیات'
        },

        // { key: 'parentId'},
      ],
      pageNumber: 1,
      count: 5,
      search: '',
      SelectedAd: null

    }
  },
  async created() {
    await this.getAllAds()
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
    vSelect,
  },
  watch: {
    search: function () {
      this.getAllAds()
    },
    currentPage: function () {
      this.getAllAds()
    }
  },
  methods: {
    SetSelectedAd(item) {
      this.SelectedAd = item
    },
    async ChangeAdStatus() {

      let _this = this
      let changeStatus = new ChangeStatus(_this)
      let data = {
        adsCategoryParents: this.SelectedAd.adsCategoryParents,
        adsId: this.SelectedAd.adsId,
        categoryId: this.SelectedAd.categoryId,
        city: null,
        cityId: 0,
        condition: 0,
        createDate: '2022-07-07T09:41:29.9519724',
        description: 'string',
        expirationDate: '2022-07-07T05:09:32.034',
        isDeleted: false,
        isFavourite: null,
        latitude: '31',
        longitude: '51',
        photos: [],
        price: 100000,
        reason: null,
        state: null,
        stateId: 0,
        status: 1,
        tag: null,
        title: 'string',
        updateDate: '2022-07-11T16:44:57.9729586+04:30',
        userId: this.SelectedAd.userId,
        userInfo: null,
      }
      changeStatus.setRequestParamDataObj(data)
      await changeStatus.fetch(()=>{
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `وضعیت آگهی به غیرفعال تغییر یافت`,
          },
        })
        _this.getAllAds()
      },(e)=>{
        console.log(e)
      })
    },
    async getAllAds() {
      let _this = this
      let getAllAdsNotConfirmed = new GetAllAdsNotConfirmed(_this)
      let data = {
        pageNumber: _this.currentPage,
        count: _this.count,
        search: _this.search
      }
      getAllAdsNotConfirmed.setParams(data)
      await getAllAdsNotConfirmed.fetch(function (content) {
        _this.Ads = content.ads
        _this.totalCount = content.adsCount
      }, function (error) {
        console.log(error)
      })
    },

    showModal(param) {
      let _this = this

      _this.deleteItem = param
    },
  },
}
</script>

<style scoped>

</style>
