<template>
  <div v-if="!$fetchState.pending" class="mcontainer NewBg position-relative  px-2">
    <div class="row">
      <div id="viewUser" aria-hidden="true" aria-labelledby="exampleModalLabel" class="modal fade"
           tabindex="-1">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h1 id="exampleModalLabel" class="modal-title fs-5">مشخصات کاربر</h1>
            </div>
            <div v-if="userInfo" class="modal-body">
              <div class="row">
                <div class="col-md-12 my-1">
                  <small class="text-secondary">نام و نام خانوادگی</small>
                  <small>{{ userInfo.name + ' ' + userInfo.familyName }}</small>
                </div>
                <div class="col-md-12 my-1">
                  <small class="text-secondary">شماره موبایل</small>
                  <small>{{ userInfo.mobile }}</small>
                </div>
              </div>
            </div>
            <div class="modal-footer">
              <button class="btn btn-secondary" data-bs-dismiss="modal" type="button">بستن</button>
            </div>
          </div>
        </div>
      </div>
      <div id="changeStatus" aria-hidden="true" aria-labelledby="exampleModalLabel" class="modal fade"
           tabindex="-1">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h1 id="exampleModalLabel" class="modal-title fs-5">تغییر وضعیت گفتگو</h1>
            </div>
            <div class="modal-body">
              <div class="row">
                <div class="col-md-12">
                  <label for="">تغییر وضعیت به</label>
                  <v-select
                    v-model="selectedStatus"
                    :options="statuses"
                    :reduce="option => option.value"
                    dir="rtl"
                    label="option"
                  />
                </div>
              </div>
            </div>
            <div class="modal-footer">
              <button class="btn btn-secondary" data-bs-dismiss="modal" type="button">بستن</button>
              <button class=" bg-pink p-2 rounded text-white" data-bs-dismiss="modal" type="button"
                      @click="changeStatus">ثبت
              </button>
            </div>
          </div>
        </div>
      </div>

      <div class="col-md-12 bg-white p-3 mb-3    rounded shadow">
        <div class="row">
          <div class="col-md-3">
            <h1 class="h4 mb-0 text-center mt-1">گفتگو های من</h1>
          </div>
          <div class="col-md-9">
            <input v-model="debouncer" class="with-border FormInputs" placeholder="جستجو" type="text">
          </div>
        </div>
      </div>
      <div v-if="myConsultations && !isRequesting" class="p-0">
        <div v-for="(item,idx) in myConsultations" :key="idx" class="col-md-12 my-3 ">
          <div class="bg-white text-black cursor-pointer shadow row rounded p-md-4 p-2 row"
               style="min-height: 10rem;text-decoration: none"
          >
            <div class="col-md-12 mb-3 d-flex align-items-center justify-content-between">
              <div class="d-flex align-items-center gap-2">
                <small class="text-secondary">تاریخ ثبت :</small>
                <small>{{ new Date(item.createDate).toLocaleDateString('fa-IR') }}</small>
              </div>
              <div class="d-flex align-items-center gap-2">
                <small class="text-secondary">تاریخ رزرو :</small>
                <small>{{ new Date(item.targetDate).toLocaleDateString('fa-IR') + ' - ' + item.hour }}</small>
              </div>

            </div>
            <div class="col-md-6 ">
              <div class="d-flex gap-3 h-100 align-items-start">
                <div class="rounded-circle  d-flex flex-column justify-content-center align-items-center"
                     style="height: 100%">
                  <img v-if="item.userPic" class="rounded-circle" :src="baseUrl+'/' +item.userPic" alt=""
                       style="object-fit: contain;width: 5rem;height: 5rem">
                  <img v-else alt="" class="rounded-circle" src="/defaultUser.png" style="object-fit: contain;width: 5rem;height: 5rem">
                </div>
                <div class="d-flex flex-column h-100 justify-content-center align-items-start">
                  <div class="d-flex flex-column justify-content-start align-items-start">
                    <nuxt-link :to="`/user/${item.userName}/posts`" style="text-decoration: none" class="text-black ">
                      <h5 class="tw-font-bold">{{ item.name + ' ' + item.lName }}</h5>
                    </nuxt-link>
                    <span class="text-muted">{{ item.cats }}</span>
                  </div>
                  <div class="d-flex gap-1">
                    <small class="text-muted">نوع مشاوره : </small>
                    <small v-if="item.type===1" class="badge pill bg-pink text-white">تلفنی</small>
                    <small v-if="item.type===2" class="badge pill bg-purple text-white">تلفنی فوری</small>
                    <small v-if="item.type===3" class="badge pill bg-info text-white">ویدیویی</small>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-md-6 my-md-0 my-3">
              <div class="d-flex flex-column justify-content-center h-100 ">
                <div class="d-flex flex-column align-items-start gap-2">
                  <div class="d-flex gap-1">
                    <small class="text-muted">وضعیت پرداخت : </small>
                    <span v-if="item.isPayed" class="badge pill bg-success text-white">پرداخت شده</span>
                    <span v-else class="badge pill bg-danger text-white">پرداخت نشده</span>
                  </div>
                  <div class="d-flex gap-1">
                    <small class="text-muted">مبلغ : </small>
                    <span class="   ">{{ Intl.NumberFormat('fa-IR').format(item.price) }} <small>تومان</small></span>

                  </div>
                  <div class="d-flex gap-1">
                    <small class="text-muted">وضعیت گفتگو : </small>
                    <small v-if="item.status===1" class="badge pill bg-secondary text-white">در انتظار</small>
                    <small v-if="item.status===2" class="badge pill bg-danger text-white">تمام شده</small>


                  </div>
                  <div class="d-flex gap-1">
                    <button v-if="item.status!==2" class="btn btn-info btn-sm text-white" data-bs-target="#changeStatus"
                       data-bs-toggle="modal"
                       style="cursor:pointer;" @click="selectedConsult = item">تغییر
                      وضعیت
                    </button>
                  </div>

                </div>
              </div>
            </div>
            <div class="col-md-12 mt-3 d-flex justify-content-between gap-2 align-items-center">
              <div>
                <button class="p-1 bg-purple rounded shadow text-white" type="button"
                        @click="$router.push({ path: '/Social/Chat',query: {userId: item.pationUserId, Photo: item.userPic}})">
                  <small>چت</small>
                </button>
              </div>
              <div class="d-flex gap-2 align-items-center">
                <button v-if="!item.isPayed" class="p-1 bg-pink rounded shadow text-white" type="button"
                        @click="createPayment(item.orderId)">
                  <small>پرداخت</small>
                </button>
                <button  data-toggle="tooltip"  title="رمز عبور و نام کاربری شما شماره موبایل شما است" v-if="item.isPayed && item.type===3 && item.status!==2"
                        class="p-1 bg-info rounded shadow text-white" type="button"
                        @click="createSkyRoom(item)">
                  <small>ورود به اسکای روم</small>
                </button>
                <button v-if="item.isPayed && item.type!==3 && item.status!==2" class="d-flex align-items-center gap-2"
                        data-bs-target="#viewUser" data-bs-toggle="modal"
                        type="button" @click="getUser(item)">
                  <small class="text-pink">مشاهده اطلاعات</small>
                  <LazyArrowLeftIcon style="width: 20px;height: 20px;fill: #fc5399"></LazyArrowLeftIcon>
                </button>
              </div>
            </div>
          </div>
        </div>

      </div>
      <LazySpinner v-if="isRequesting"></LazySpinner>
      <div v-if="totalPages.length>1"  class="col-md-12 bg-white p-3 mb-3    rounded shadow">
        <LazyCustomPagination :activePage="page" :totalPages="totalPages"
                              @PageChanged="changePage($event)"/>

      </div>
    </div>
  </div>
</template>

<script>
export default {
  layout: "PoshtebamPlusLayout",
  name: "MyConsultations",
  data() {
    return {
      myConsultations: null,
      searchCommand: '',
      totalPages: [],
      isRequesting: false,
      timeout: null,
      page: 1,
      selectedConsult: null,
      userInfo: null,
      selectedStatus: 0,
      statuses: [
        {
          option: 'تمام شده',
          value: 2
        }
      ],
    }
  },
  head() {
    return {
      title: 'گفتگو های من'
    }
  },
  fetchOnServer: false,
  computed: {
    baseUrl() {
      return process.env.pic
    },
    debouncer: {
      get() {
        return this.searchCommand
      },
      set(val) {
        if (this.timeout) {
          clearTimeout(this.timeout)
        }
        this.timeout = setTimeout(() => {
          this.searchCommand = val
        }, 600)
      }
    }
  },
  watch: {
    searchCommand: function (val) {
      this.getMyConsultations()
    }
  },
  async fetch() {
    await Promise.all([
      this.getMyConsultations()
    ])
  },

  methods: {
    async changeStatus() {
      try {
        const res = await this.$repositories.changeConsultStatus.setParams({
          id: this.selectedConsult.id
        })
        if (res.data.isSuccess) {
          this.$toast.success('وضعیت مشاوره بروزرسانی شد')
          this.getMyConsultations()
        }
        else {
          this.$toast.error(res.data.errorMessage)
        }
      }
      catch (e) {
        console.log(e)
      }
    },
    async getUser(item) {
      try {
        this.$nextTick(() => {
          this.$nuxt.$loading.start()
        })
        const res = await this.$repositories.getUser.setParams({
          id: item.pationUserId
        })
        this.userInfo = res.data
      }
      catch (e) {
        console.log(e)
      }
      finally {
        this.$nuxt.$loading.finish()
        this.$nuxt.loading = false
      }
    },
    async createSkyRoom(item) {
      try {
        this.$nextTick(() => {
          this.$nuxt.$loading.start()
        })
        const res = await this.$repositories.createSkyRoom.setParams({
          consultantUserScheduleId: item.id
        })
        if(res.data.isSuccess){
          window.location.replace(res.data.data)
        }else{
          this.$toast.error(res.data.errorMessage)
        }

      }
      catch (e) {
        console.log(e)
      }
      finally {
        this.$nuxt.$loading.finish()
        this.$nuxt.loading = false

      }
    },
    async createPayment(orderId) {
      try {
        this.$nextTick(() => {
          this.$nuxt.$loading.start()
        })
        const res = await this.$repositories.createPaymentForConsultant.setParams({
          orderId: orderId
        })
        window.location.replace(res.data.data);

      }
      catch (e) {
        console.log(e)
      }
      finally {
        this.$nuxt.$loading.finish()
        this.$nuxt.loading = false

      }

    },
    changePage(id) {
      this.page = id
      this.getMyConsultations()
    },
    async getMyConsultations() {
      try {
        this.isRequesting = true
        const res = await this.$repositories.getMyConsultationsForConsulter.setParams({
          fullname: this.searchCommand,
          pageNumber: this.page,
          take: 5
        })
        this.myConsultations = res.data.items
        this.totalPages = []
        const result = Math.ceil(res.data.totalCount / 5)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
        }
      }
      catch (e) {
        console.log(e)
      }
      finally {
        this.isRequesting = false
      }
    },
  }
}
</script>

<style scoped>

</style>
