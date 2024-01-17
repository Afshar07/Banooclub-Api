<template>
  <b-overlay
      :show="overlay"
      rounded="sm"
  >
    <b-card
        v-if="consultant"
        class="mb-0"
    >
      <b-row>
        <b-col class="border-bottom pb-1 mb-1" cols="12">
          <strong>اطلاعات عمومی</strong>
        </b-col>
        <b-col cols="12" md="4">
          <span class="p-2 text-secondary">شماره شناسایی</span>
          <span>{{ consultant.medicalSystemNumber }}</span>
        </b-col>
        <b-col cols="12" md="4">
          <span class="p-2 text-secondary">کدملی</span>
          <span>{{ consultant.nationalCode }}</span>
        </b-col>
        <b-col cols="12" md="4">
          <span class="p-2 text-secondary">شماره همراه مستقیم</span>
          <span>{{ consultant.phoneNumber }}</span>
        </b-col>

        <b-col class="mt-1" cols="12" md="4">
          <span class="p-2 text-secondary">استان محل فعالیت </span>
          <span>{{  consultant.stateName  }}</span>
        </b-col>
        <b-col class="mt-1" cols="12" md="4">
          <span class="p-2 text-secondary">شهر محل فعالیت </span>
          <span>{{ consultant.cityName }}</span>
        </b-col>
        <b-col class="mt-1 d-flex align-items-center " cols="12" md="12">
          <span class=" py-0 px-2 text-secondary">حوزه فعالیت</span>
          <div class="d-flex align-items-center  flex-wrap">
            <b-badge v-for="(item,idx) in consultant.categories" :key="idx" class="mx-25" variant="primary">
              {{ item.title }}
            </b-badge>
          </div>
        </b-col>
      </b-row>
      <b-row>
        <b-col class="border-bottom pb-1 my-1" cols="12">
          <strong>اطلاعات بانکی</strong>
        </b-col>
        <b-col class="mt-1" cols="12" md="7">
          <span class="p-2 text-secondary">شماره شبا</span>
          <span>{{ consultant.shabaNo }}</span>
        </b-col>
        <b-col class="mt-1" cols="12" md="4">
          <span class="p-2 text-secondary">نام بانک </span>
          <span>{{ consultant.bankName }}</span>
        </b-col>
        <b-col class="mt-1" cols="12" md="4">
          <span class="p-2 text-secondary">شماره کارت</span>
          <span>{{ consultant.cartNo }}</span>
        </b-col>
      </b-row>
      <b-row>
        <b-col class="border-bottom pb-1 my-1" cols="12">
          <strong>تعرفه مشاوره </strong>
        </b-col>
        <b-col class="mt-1" cols="12" md="4 ">
          <span class="p-2 text-secondary">بازه های مشاوره</span>
          <span>{{ consultant.durationMinute }} دقیقه</span>
        </b-col>
        <b-col class="mt-1" cols="12" md="4">
          <span class="p-2 text-secondary">زمان شروع وقت دهی در روز</span>
          <span>{{ consultant.startAndEndWork[0] }}</span>
        </b-col>
        <b-col class="mt-1" cols="12" md="4">
          <span class="p-2 text-secondary">زمان پایان وقت دهی در روز</span>
          <span>{{ consultant.startAndEndWork[consultant.startAndEndWork.length - 1] }}</span>
        </b-col>
        <b-col class="mt-1" cols="12" md="4">
          <span class="p-2 text-secondary">مشاوره تلفنی</span>
          <span>{{ Intl.NumberFormat('fa-IR').format(consultant.prices[0].price) }} <small>تومان</small></span>
        </b-col>
        <b-col class="mt-1" cols="12" md="4">
          <span class="p-2 text-secondary">مشاوره تلفنی فوری</span>
          <span>{{ Intl.NumberFormat('fa-IR').format(consultant.prices[1].price) }} <small>تومان</small></span>

        </b-col>
        <b-col class="mt-1" cols="12" md="4">
          <span class="p-2 text-secondary">مشاوره ویدیویی</span>
          <span>{{ Intl.NumberFormat('fa-IR').format(consultant.prices[2].price) }} <small>تومان</small></span>

        </b-col>
      </b-row>
      <b-row>
        <b-col class="border-bottom pb-1 my-1" cols="12">
          <strong>اطلاعات شخصی</strong>
        </b-col>
        <b-col class="mt-1" cols="12" md="12">
          <span class="py-2  text-secondary">نام و نام خانوادگی</span>
          <span> {{ consultant.userfullname }} </span>
        </b-col>
        <b-col class="mt-1" cols="12" md="6">
          <img
              :src=" baseUrl +consultant.imageFileData"
              class="border rounded-circle mt-2 d-flex text-center justify-content-center align-items-center "
              style="object-fit: cover;width: 5rem;height: 5rem"
          >
        </b-col>
        <b-col class="mt-1" cols="12" md="6">
          <video
              :src="baseUrl + consultant.fileData"
              class="border rounded mt-2 d-flex text-center justify-content-center align-items-center w-100"
              controls
          ></video>
        </b-col>
        <b-col class="mt-1" cols="12" md="12">
          <span class="py-2  text-secondary">درباره مشاور</span>
          <p style="overflow-wrap: anywhere">{{ consultant.description }}</p>
        </b-col>

      </b-row>
      <b-row>
        <b-col class="mt-1 d-flex align-items-center" cols="12">
          <b-button class="mx-25" variant="primary" @click="changeRequestStatus(1)">
            تایید درخواست
          </b-button>
          <b-button class="mx-25" variant="danger" @click="changeRequestStatus(2)">
            رد درخواست
          </b-button>
        </b-col>
      </b-row>
    </b-card>

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
import {AcceptConsultantRequest, GetConsultantRequest, RejectConsultantRequest} from "@/libs/Api/consultant";
import ToastificationContent from "@core/components/toastification/ToastificationContent.vue";
import Helper from "@/libs/Helper";

export default {
  name: "consultantRequestDetail",
  components: {
    ToastificationContent,
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
  data() {
    return {
      baseUrl: Helper.baseUrl,
      overlay: false,
      requestId: this.$route.params.id,
      consultant: null
    }
  },
  mounted() {
    this.getConsultantRequest()
  },
  methods: {
    async changeRequestStatus(actionType) {
      let _this = this

      if (actionType === 1) {
        _this.overlay = true
        let acceptConsultantRequest = new AcceptConsultantRequest(_this)
        let data = {
          requestId: parseInt(_this.requestId)
        }
        acceptConsultantRequest.setParams(data)
        await acceptConsultantRequest.fetch(function (content) {
          if (content.isSuccess) {
            _this.$toast({
              component: ToastificationContent,
              position: 'bottom-center',
              props: {
                title: `عملیات موفق`,
                icon: 'CheckIcon',
                variant: 'success',
                text: `وضعیت درخواست به تایید شده تغییر یافت`,
              },
            })
            _this.$router.push({
              name: 'apps-consultant-consultantRequests'
            })
          }
        }, function (error) {
          console.log(error)
        })
      } else if (actionType === 2) {
        _this.overlay = true
        let rejectConsultantRequest = new RejectConsultantRequest(_this)
        let data = {
          requestId: parseInt(_this.requestId)
        }
        rejectConsultantRequest.setParams(data)
        await rejectConsultantRequest.fetch(function (content) {
          if (content.isSuccess) {
            _this.$toast({
              component: ToastificationContent,
              position: 'bottom-center',
              props: {
                title: `عملیات موفق`,
                icon: 'CheckIcon',
                variant: 'success',
                text: `وضعیت درخواست به رد شده تغییر یافت`,
              },
            })
            _this.$router.push({
              name: 'apps-consultant-consultantRequests'
            })
          }
        }, function (error) {
          console.log(error)
        })
      }

      _this.overlay = false

    },
    async getConsultantRequest() {
      let _this = this
      _this.overlay = true
      let getConsultantRequest = new GetConsultantRequest(_this)
      let data = {
        id: parseInt(_this.requestId)
      }
      getConsultantRequest.setParams(data)
      await getConsultantRequest.fetch(function (content) {
        _this.consultant = content
      }, function (error) {
        console.log(error)
      })
      _this.overlay = false

    },
  }
}
</script>

<style scoped>

</style>
