<template>
  <div class="mcontainer  NewBg position-relative  px-2">
    <div class="row ">
      <div v-if="consultantInfo" class="col-md-12  my-3 rounded shadow p-3"
           style="background-color: #f6e2ff;border:solid 1px #e7b0fe">
        <div class="w-100 d-flex align-items-center gap-3">
          <img v-if="consultantInfo.upi" :src="baseUrl +consultantInfo.upi" alt="" class="rounded-circle"
               style="object-fit: cover;width: 5rem;height: 5rem">
          <img v-else alt="" height="60" src="/defaultUser.png" style="object-fit: contain" width="60">
          <div class="d-flex flex-column ">
            <strong>{{ consultantInfo.name + ' ' + consultantInfo.lName }}</strong>
            <div class="d-flex align-items-center gap-2">

            <span v-for="(item,idx) in consultantInfo.cats" :key="idx" class="badge pill bg-pink text-white ">{{
                item
              }}</span>
            </div>
          </div>
        </div>

      </div>
      <div class="col-md-12 bg-white  my-3 rounded shadow p-3">
        <div class="w-100 d-flex align-items-center gap-3">
          <strong class="">علت مشاوره</strong>
        </div>
        <div class="w-100 mt-2">
          <small class="text-muted">توضیحات</small>
          <textarea v-model="description" class="form-control mt-2"
                    placeholder="لطفا شرح حال خود را بیان کنید"></textarea>
        </div>
      </div>
      <div class="col-md-12 bg-white  my-3 rounded shadow p-3">
        <div class="w-100 d-flex align-items-center justify-content-between gap-3">
          <strong class="">انتخاب زمان</strong>
          <div class="d-flex align-items-center gap-2">
            <span class="badge pill rounded" style="background-color: #b44aff">انتخاب شده</span>
            <span class="badge pill  bg-success rounded" style="">آزاد</span>
            <span class="badge pill  bg-danger rounded" style="">رزرو شده</span>
            <span class="badge pill  bg-primary rounded" style="">پرداخت شده</span>
          </div>
        </div>
        <div class="my-3">
          <client-only>
            <date-picker
              v-model="startDate"
              :min="new Date(Date.now())"
              display-format="jYYYY-jMM-jDD"
              format="YYYY-MM-DD"
              inputClass="vpd-input-group1"
              type="date"
            />
          </client-only>
        </div>
        <div v-if="schedules" class="d-flex position-relative  align-items-start gap-2"
             style="height: 400px;overflow-y: scroll">
          <div v-for="(day,idx) in schedules.filter(e=> e.dayOfWeek === selectedWeekDay)" :key="idx"
               class="d-flex  flex-column align-items-center justify-content-start"
               style="min-width: 5rem">
              <span
                class="w-100 p-2 sticky-top top-0 rounded bg-pink text-white text-center shadow">{{
                  getWeekDayNumber(day)
                }}</span>
            <template v-for="(time,timeIndex) in day.hours">
              <button
                :class="getSelectedClass(time,day)"
                :disabled="time.status===-1|| time.status===4"
                class="w-100 p-2 rounded bg-purple mt-2 disabled:tw-bg-gray-300 disabled:tw-cursor-not-allowed cursor-pointer cursor-pointer text-center text-white shadow"
                type="button"
                @click="isSelected(time,day)">
                {{ time.startTime }}
              </button>
            </template>
          </div>
        </div>

      </div>
      <!--      <div class="col-md-12 bg-white  my-3 rounded shadow p-3">-->
      <!--        <div class="w-100 d-flex align-items-center gap-3">-->
      <!--          <strong class=""> کد تخفیف</strong>-->
      <!--        </div>-->
      <!--        <div class="w-100 mt-2">-->
      <!--          <small class="text-muted">کد تخفیف را وارد کنید</small>-->
      <!--          <div class=" d-flex my-2 gap-2">-->
      <!--            <input  type="text" class="with-border FormInputs" placeholder="کد تخفیف را وارد کنید">-->
      <!--            <button type="button" class=" text-white rounded p-2 cursor-pointer" style="background-color:#d277ff ">-->
      <!--              ثبت-->
      <!--            </button>-->
      <!--          </div>-->
      <!--        </div>-->
      <!--      </div>-->
      <div v-if="consultantInfo" class="col-md-12 bg-white  my-3 rounded shadow p-3">
        <div class="w-100 d-flex align-items-center gap-3">
          <strong class="">اطلاعات پرداخت</strong>
        </div>
        <div v-for="(item,idx) in computedPrice"
             class="w-100 p-2 border-bottom d-flex align-items-center justify-content-between">
          <small class="text-pink">{{ pricesObjects[item.type] }}</small>
          <small class="text-pink">{{ Intl.NumberFormat('fa-IR').format(item.price) }} <small>تومان</small></small>
        </div>
        <div v-for="(item,idx) in computedPrice"
             class="w-100 p-2 my-2 rounded  d-flex align-items-center justify-content-between"
             style="background-color: #fcf5ff">
          <small class="text-pink">مبلغ قابل پرداخت</small>
          <small class="text-pink">{{ Intl.NumberFormat('fa-IR').format(item.price) }} <small>تومان</small></small>


        </div>

      </div>
      <div class="w-100 px-0 my-3">
        <button class="btn w-100 text-white " style="background-color:#e7b0fe " type="button" @click="createPayment">
          پرداخت
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  layout: 'PoshtebamPlusLayout',
  name: "index",
  head() {
    return {
      title: 'پرداخت'
    }
  },
  data() {
    return {
      consultantInfo: null,
      priceType: this.$route.params.priceId,
      schedules: null,
      startDate: '',
      description: '',
      selectedWeekDay: 0,
      selectedTime: '',
      pricesObjects: {
        1: 'مشاوره تلفنی',
        2: 'مشاوره تلفنی فوری',
        3: 'مشاوره ویدیویی'
      },
      weekDays: {
        0: 'یکشنبه',
        1: 'دوشنبه',
        2: 'سه شنبه',
        3: 'چهار شنبه',
        4: 'پنجشنبه',
        5: 'جمعه',
        6: 'شنبه',
      },
    }
  },
  fetchOnServer: false,
  async fetch() {
    await Promise.all([
      this.getConsultant(),
    ])
  },
  computed: {
    baseUrl() {
      return process.env.pic
    },
    computedPrice() {
      return this.consultantInfo.prices.filter(e => e.type === parseInt(this.priceType))
    }

  },
  watch: {
    startDate: function (val) {
      this.getSchedules()
    }
  },
  methods: {
    validateData() {
      if (this.selectedTime === '' && this.startDate === '') {
        return true
      }
    },
    getSelectedClass(time, day) {
      if (this.selectedTime === time.startTime) {
        return 'bg-pink'
      }
      else if (time.status === 1) {
        return 'bg-success'
      }
      else if (time.status === 2) {
        return 'bg-danger'
      }
      else if (time.status === 3) {
        return 'bg-primary'
      }
      else if (time.status === 4) {
        return 'bg-Gray'
      }
    },

    async createPayment() {
      if (this.startDate !== '' && this.selectedTime !== '' && this.$route.params.priceId && this.$route.params.consultantId) {
        try {
          const res = await this.$repositories.createConsultantPayment.setParams({
            description: this.description,
            id: parseInt(this.$route.params.consultantId),
            type: parseInt(this.$route.params.priceId),
            targetTime: this.selectedTime,
            targetDate: this.startDate
          })
          if (res.data.isSuccess) {
            this.createConsultantPayment(res.data.data)
          }
          else {
            this.$toast.error('خطا در انجام عملیات')
          }
        }
        catch (e) {
          console.log(e)
        }
      }
      else {
        this.$toast.error('لطفا همه فیلد هارا پر کنید')
      }
    },
    async createConsultantPayment(orderId) {
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
    async getSchedules() {
      this.selectedWeekDay = new Date(this.startDate).getDay()
      try {
        const res = await this.$repositories.getSchedules.setParams({
          id: this.$route.params.consultantId,
          targetDate: this.startDate
        })
        this.schedules = res.data
      }
      catch (e) {
        console.log(e)
      }
      finally {

      }
    },
    isSelected(time, day) {
      if (time.status === 3) {
        this.$toast.error('این زمان رزرو شده است')

      }
      else if (time.status === 4) {
        this.$toast.error('زمان گذشته است')
      }
      else {
        if (time === this.selectedTime) {
          this.selectedTime = ''
        }
        else {
          this.selectedTime = time.startTime
        }
      }
    },

    getWeekDayNumber(day) {
      return this.weekDays[day.dayOfWeek]
    },
    async getConsultant() {
      try {
        const res = await this.$repositories.getConsultantById.setParams({
          id: this.$route.params.consultantId
        })
        this.consultantInfo = res.data.data
      }
      catch (e) {
        console.log(e)
      }
      finally {

      }
    }
  }
}
</script>

<style scoped>

</style>
