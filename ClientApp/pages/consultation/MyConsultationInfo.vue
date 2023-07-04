<template>
  <div class="container NewBg position-relative  px-2">
    <div v-if="consultantRequest" class="row">
      <div class="col-md-12">
        <h1 class="h4">اطلاعات مشاوره من</h1>
      </div>
      <div class="col-md-12 my-3 bg-white rounded shadow">
        <div class="row   p-3">

          <div class="col-md-4 my-md-0 my-3">
            <small class="p-2">شماره شناسایی</small>
            <input v-model="consultantRequest.medicalSystemNumber" class="with-border FormInputs mt-1"
                   placeholder="شماره شناسایی"
                   type="tel">
          </div>
          <div class="col-md-4">
            <small class="p-2">کد ملی *</small>
            <input v-model="consultantRequest.nationalCode" class="with-border FormInputs mt-1" placeholder="کد ملی"
                   type="tel">
          </div>
          <div class="col-md-4  ">
            <small class="p-2">شماره همراه مستقیم *</small>
            <input v-model="consultantRequest.phoneNumber" class="with-border FormInputs mt-1"
                   placeholder="شماره همراه مستقیم"
                   type="tel">
          </div>

        </div>
        <div class="row px-3">
          <div class="col-md-12 ">
            <h6>حوزه فعالیت *</h6>
          </div>

          <div class="col-md-6">
            <v-select
              v-model="consultantRequest.categories"
              :options="categories"
              :reduce="(title) => title.id"
              dir="rtl"
              label="title"
              multiple
              placeholder="همه"

            ></v-select>
          </div>

        </div>
        <div class="row p-3">

          <div class="col-md-6">
            <small class="p-2">استان *</small>
            <v-select
              v-model="consultantRequest.stateId"
              :options="states"
              :reduce="(name) => name.stateId"
              class="selectWidth BgInput w-100"
              dir="rtl"
              label="name"
              placeholder="استان*"
              @input="getCity"

            ></v-select>
          </div>
          <div class="col-md-6">
            <small class="p-2">شهر *</small>
            <v-select
              v-model="consultantRequest.cityId"
              :options="cities"
              :reduce="(name) => name.cityId"
              class="selectWidth BgInput w-100"
              dir="rtl"
              label="name"
              placeholder="شهر*"

            ></v-select>
          </div>


        </div>
        <div class="row p-3">
          <div class="col-md-12 border-bottom pb-2 mb-2">
            <strong>اطلاعات بانکی</strong>
          </div>
          <div class="col-md-7">
            <small class="p-2">شماره شبا *</small>
            <div class="d-flex gap-2 align-items-center">
              <input v-model="consultantRequest.shabaNo" class="with-border FormInputs mt-1" maxlength="24"
                     placeholder="شماره شبا"
                     type="text">
              <small>-IR</small>
            </div>
          </div>
          <div class="col-md-3 my-md-0 my-3">
            <small class="p-2">نام بانک *</small>
            <input v-model="consultantRequest.bankName" class="with-border FormInputs mt-1" placeholder="نام بانک"
                   type="tel">
          </div>
          <div class="col-md-5 mt-2">
            <small class="p-2">شماره کارت *</small>
            <input v-model="consultantRequest.cartNo" class="with-border FormInputs mt-1" placeholder="شماره کارت"
                   type="text">
          </div>

        </div>
        <div class="row p-3">
          <div class="col-md-12 border-bottom pb-2 mb-2">
            <strong>تعرفه مشاوره *</strong>
          </div>
          <div class="row mb-2">
            <div class="col-md-4 mt-2">
              <small class="p-2">بازه های مشاوره</small>
              <v-select
                v-model="consultantRequest.durationMinute"
                :options="durations"
                :reduce="(title) => title.id"
                class="selectWidth BgInput w-100"
                dir="rtl"
                label="title"

              ></v-select>
            </div>
            <div v-if="consultantRequest.startAndEndWork" class="col-md-4 mt-2">
              <small class="p-2">زمان شروع</small>
              <v-select
                v-model="consultantRequest.startAndEndWork[0]"
                :options="generatedTimeStamps"
                :reduce="(value) => value.value"
                class="selectWidth BgInput w-100"
                dir="rtl"
                label="value"

              ></v-select>
            </div>
            <div v-if="consultantRequest.startAndEndWork" class="col-md-4 mt-2">
              <small class="p-2">زمان پایان</small>
              <v-select
                v-model="consultantRequest.startAndEndWork[consultantRequest.startAndEndWork.length-1]"
                :options="generatedTimeStamps"
                :reduce="(value) => value.value"
                class="selectWidth BgInput w-100"
                dir="rtl"
                label="value"

              ></v-select>
            </div>
          </div>

          <div v-if="consultantRequest.prices" class="row mb-2">
            <div class="col-md-12  d-flex align-items-center gap-2   my-2">
              <LazyPhoneIcon style="width: 20px;height: 20px;fill: #b44aff "></LazyPhoneIcon>
              <span>مشاوره تلفنی</span>
            </div>
            <div class="col-md-5 mt-2">
              <small class="p-2">قیمت (تومان)</small>
              <input v-model="formattedPhonePrice" class="with-border FormInputs mt-1" type="text">
            </div>
          </div>
          <div v-if="consultantRequest.prices" class="row mb-2">
            <div class="col-md-12  d-flex align-items-center gap-2  my-2">
              <LazySmartPhoneIcon style="width: 20px;height: 20px;fill: #fc5399 "></LazySmartPhoneIcon>
              <span>مشاوره تلفنی فوری</span>
            </div>
            <div class="col-md-5 mt-2">
              <small class="p-2">قیمت (تومان)</small>
              <input v-model="formattedImmediatePhonePrice" class="with-border FormInputs mt-1" placeholder=""
                     type="text">
            </div>
          </div>
          <div v-if="consultantRequest.prices" class="row mb-2">
            <div class="col-md-12  d-flex align-items-center gap-2  my-2">
              <LazyPictureIcon style="width: 20px;height: 20px"></LazyPictureIcon>
              <span>مشاوره ویدیویی</span>
            </div>
            <div class="col-md-5 mt-2">
              <small class="p-2">قیمت (تومان)</small>
              <input v-model="formattedVideoPrice" class="with-border FormInputs mt-1" placeholder=""
                     type="text">
            </div>
          </div>

        </div>
        <div class="row p-3">
          <div class="col-md-12 border-bottom pb-2 mb-2">
            <strong>اطلاعات شخصی</strong>
          </div>
          <div class="row   p-3">

            <div class="col-md-6 my-md-0 my-3">
              <small class="p-2">آپلود عکس *</small>
              <input
                id="MainImage"
                ref="consultantPicture"
                accept="image/*"
                class="form-control-file MainImage d-none"
                type="file"
                @input="handleFile"
              />
              <img
                v-if="newImage!==''"
                :src="newImage"
                class="border rounded-circle mt-2 d-flex text-center justify-content-center align-items-center photo"
                style="object-fit: cover"
                @click="$refs.consultantPicture.click()">
              <img
                v-else
                :src="baseUrl + consultantRequest.imageFileData"
                class="border rounded-circle mt-2 d-flex text-center justify-content-center align-items-center photo"
                style="object-fit: cover"
                @click="$refs.consultantPicture.click()">
            </div>
            <div class="col-md-6">
              <small class="p-2">آپلود ویدیو معرفی </small>
              <input
                id="MainImage"
                ref="consultantVideo"
                accept="video/*"
                class="form-control-file MainImage d-none"
                type="file"
                @input="handleVideo"


              />
              <div v-if="newFileData!==''">
                <button class="btn  btn-sm mt-2 text-white rounded" style="background-color: #b44aff!important"
                        type="button" @click="$refs.consultantVideo.click()">عوض
                  کردن ویدیو
                </button>
                <video
                  :src="newFileData"
                  autoplay
                  class="border rounded mt-2 d-flex text-center justify-content-center align-items-center w-100"
                  controls
                ></video>
              </div>
              <div v-else>
                <button class="btn  btn-sm mt-2 text-white rounded" style="background-color: #b44aff!important"
                        type="button" @click="$refs.consultantVideo.click()">عوض
                  کردن ویدیو
                </button>
                <video
                  :src="baseUrl + consultantRequest.fileData"
                  autoplay
                  class="border rounded mt-2 d-flex text-center justify-content-center align-items-center w-100"
                  controls
                ></video>
              </div>
            </div>
            <div v-if="consultantRequest.description" class="col-md-12 mt-3 ">
              <small class="p-2">درباره من *</small>
              <textarea v-model="consultantRequest.description" class="FormInputs with-border"
                        maxlength="1000"></textarea>
              <div class="d-flex align-items-center gap-2 p-1">
                <small>1000</small>
                <small>/</small>
                <small>{{ consultantRequest.description.length }}</small>
              </div>
            </div>

          </div>
        </div>

        <div class="row p-3">
          <div class="col-md-2 ">
            <button class="btn w-100 text-white " style="background-color:#e7b0fe " type="button" @click="validateData">
              ثبت
            </button>
          </div>
        </div>


      </div>
      <div class="col-md-12 my-3 bg-white rounded shadow">
        <div class="row p-3">
          <div class="col-md-12 border-bottom pb-2 mb-2">
            <strong>زمان های ثبت شده</strong>
          </div>
          <div class="d-flex position-relative align-items-start gap-2" style="height: 400px;overflow-y: scroll">
            <div v-for="(weekDay,weekDayIndex) in 7"
                 class="d-flex  flex-column align-items-center justify-content-start"
                 style="min-width: 5rem">
              <span class="w-100 p-2 sticky-top top-0 rounded bg-pink text-white text-center shadow">{{
                  getWeekDayNumber(weekDayIndex)
                }}</span>
              <template v-for="(time,timeIndex) in generatedTimestamps">

                <button :class="getSelectedClass(weekDayIndex,time)" :disabled="!isRenderingTime(weekDayIndex,time)"
                        class="w-100 p-2 rounded bg-purple mt-2 disabled:tw-bg-gray-300 disabled:tw-cursor-not-allowed cursor-pointer cursor-pointer text-center text-white shadow"
                        type="button"

                        @click="isSelected(weekDayIndex,time)">
                  {{
                    time.value
                  }}
                </button>
              </template>
            </div>
          </div>
        </div>

      </div>

    </div>
  </div>
</template>

<script>
import timestamps from '../../static/timeStamps.json'

export default {
  layout: "PoshtebamPlusLayout",
  name: "BecomeConsultant",
  data() {
    return {
      durations: [],
      startDate: '',
      endDate: '',
      generatedTimeStamps: timestamps,
      consultantRequest: {},
      newImage: '',
      newFileData: '',
      categories: [],
      states: [],
      cities: [],
      selectedCategoryId: 0,
      description: '',
      newValue: '',
      generatedTimestamps: [],
      selectedStamps: [],
      selectedWeekDays: [],
      weekDays: [
        {
          name: 'شنبه',
          value: 1
        },
        {
          name: 'یک شنبه',
          value: 2
        },
        {
          name: 'دوشنبه',
          value: 3
        },
        {
          name: 'سه شنبه',
          value: 4
        },
        {
          name: 'چهارشنبه',
          value: 5
        },
        {
          name: 'پنجشنبه',
          value: 6
        },
        {
          name: 'جمعه',
          value: 7
        },
      ],
      nowDate: new Date(Date.now()),
      dates: [],
      dayInMili: 86400000,
      sessionDuration: 30,
      defaultStartHour: 8,
      defaultEndHour: 23,
      consultantRequestt: null,
    }
  },
  async fetch() {
    await Promise.all([
      this.getConsultantRequest(),
      this.getConsultCategories(),
      this.getStates(),
      this.getDurations(),

    ])
  },
  mounted() {
    this.getSchedules()
  },

  computed: {
    formattedPhonePrice: {
      get: function () {
        return this.consultantRequest.prices[0].price;
      },
      set: function (newValue) {
        // This setter is getting number, replace all commas with empty str
        // Then start to separate numbers with ',' from beginning to prevent
        // from data corruption
        if (newValue) {
          this.consultantRequest.prices[0].price = newValue
            .toString()
            .replaceAll(",", "")
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
          // Remove all characters that are NOT number
          this.consultantRequest.prices[0].price = this.consultantRequest.prices[0].price.replace(
            /[a-zA-Z+*!@#$%^&*()_;:'"|<>/?{}\u0600-\u06EC/\-/\.]/g,
            ""
          );
        } else if (!newValue || this.newValue === "") {
          this.consultantRequest.prices[0].price = null;
        }
      },
    },
    formattedImmediatePhonePrice: {
      get: function () {
        return this.consultantRequest.prices[1].price;
      },
      set: function (newValue) {
        // This setter is getting number, replace all commas with empty str
        // Then start to separate numbers with ',' from beginning to prevent
        // from data corruption
        if (newValue) {
          this.consultantRequest.prices[1].price = newValue
            .toString()
            .replaceAll(",", "")
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
          // Remove all characters that are NOT number
          this.consultantRequest.prices[1].price = this.consultantRequest.prices[1].price.replace(
            /[a-zA-Z+*!@#$%^&*()_;:'"|<>/?{}\u0600-\u06EC/\-/\.]/g,
            ""
          );
        } else if (!newValue || this.newValue === "") {
          this.consultantRequest.prices[1].price = null;
        }
      },
    },
    formattedVideoPrice: {
      get: function () {
        return this.consultantRequest.prices[2].price;
      },
      set: function (newValue) {
        // This setter is getting number, replace all commas with empty str
        // Then start to separate numbers with ',' from beginning to prevent
        // from data corruption
        if (newValue) {
          this.consultantRequest.prices[2].price = newValue
            .toString()
            .replaceAll(",", "")
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
          // Remove all characters that are NOT number
          this.consultantRequest.prices[2].price = this.consultantRequest.prices[2].price.replace(
            /[a-zA-Z+*!@#$%^&*()_;:'"|<>/?{}\u0600-\u06EC/\-/\.]/g,
            ""
          );
        } else if (!newValue || this.newValue === "") {
          this.consultantRequest.prices[2].price = null;
        }
      },
    },
    baseUrl() {
      return process.env.pic
    },
  },
  methods: {
    async getSchedules() {
      try {
        const res = await this.$repositories.getSchedules.setParams({
          id: this.consultantRequest.consultantId
        })
        console.log(res)
      } catch (e) {
        console.log(e)
      } finally {

      }
    },
    generateTimeStamps() {
      let todayDate = new Date(this.nowDate)

      todayDate = new Date(todayDate.setHours(this.defaultStartHour, 0, 0))
      let tempDate = {
        value: '',
        minute: '',
        hour: '',
        timestamp: 0
      }
      for (let i = 0; todayDate.getHours() < this.defaultEndHour; i++) {
        tempDate.value = new Date(todayDate).toLocaleTimeString('en-US', {
          hour12: false,
          hour: "2-digit",
          minute: '2-digit',
          second: '2-digit'
        })
        tempDate.minute = new Date(todayDate).getMinutes()
        tempDate.hour = new Date(todayDate).getHours()
        tempDate.timestamp = new Date(todayDate).getTime()
        this.generatedTimestamps.push(tempDate)
        tempDate = {
          value: '',
          minute: '',
          hour: ''
        }
        todayDate = new Date(todayDate.setMinutes(todayDate.getMinutes() + this.sessionDuration))
      }

    },
    getSelectedClass(weekDay, timeObj) {
      let calculatedDate = new Date(this.calculateDate(weekDay, timeObj))
      calculatedDate = new Date(calculatedDate).getTime()
      if (this.selectedStamps.findIndex(e => e.timestamp === calculatedDate) > -1) {
        return 'bg-success'
      } else {
        return
      }

    },
    isSelected(weekDay, timeObj) {
      let calculatedDate = new Date(this.calculateDate(weekDay, timeObj))
      let tempDate = {
        value: '',
        minute: '',
        hour: '',
        timestamp: 0
      }
      tempDate.value = new Date(calculatedDate).toLocaleTimeString('en-US', {
        hour12: false,
        hour: "2-digit",
        minute: '2-digit'
      })
      tempDate.minute = new Date(calculatedDate).getMinutes()
      tempDate.hour = new Date(calculatedDate).getHours()
      tempDate.timestamp = new Date(calculatedDate).getTime()
      let selectedIdx = this.selectedStamps.findIndex(e => e.timestamp === tempDate.timestamp)
      if (selectedIdx > -1) {
        this.selectedStamps.splice(selectedIdx, 1)
      } else {
        this.selectedStamps.push(tempDate)
      }
      tempDate = {
        value: '',
        minute: '',
        hour: '',
        timestamp: 0
      }
      // let idx = this.selectedStamps.findIndex(e=> e.timestamp === tempDate.timestamp)
      // if(idx>-1){
      //   this.$set(this.selectedStamps,idx,tempDate)
      // }else{
      // }


    },
    calculateDate(weekDay, timeObj) {
      let calculateDate = new Date(this.nowDate)
      calculateDate = new Date(calculateDate.setDate(calculateDate.getDate() + weekDay))
      calculateDate = new Date(calculateDate.setHours(timeObj.hour, timeObj.minute, 0))
      return calculateDate
    },
    isRenderingTime(weekDay, timeObj) {
      if (weekDay === 0) {
        let calculateDate = new Date(this.nowDate)
        calculateDate = new Date(calculateDate.setHours(timeObj.hour, timeObj.minute, 0))
        if (calculateDate.getHours() >= this.nowDate.getHours() + 2) {
          return true
        }
        return false
      } else {
        return true
      }
    },
    getWeekDayNumber(idx) {
      let calculatedDate = new Date(this.nowDate)
      calculatedDate = calculatedDate.setDate(calculatedDate.getDate() + idx)
      return new Date(calculatedDate).toLocaleDateString('fa-IR', {
        weekday: 'long'
      })
    },
    async getConsultantRequest() {
      try {
        const res = await this.$repositories.getConsultantRequest.setTag()
        this.consultantRequest = res.data
      } catch (e) {
        console.log(e)
      }
    },
    async handleFile() {
      if (this.$refs.consultantPicture.files.length > 0) {
        let result = await this.$utils.makeBase64(this.$refs.consultantPicture.files[0])
        this.consultantRequest.imageFileData = result.base64
        this.newImage = result.pictureUrl
      }
    },
    async handleVideo() {
      if (this.$refs.consultantVideo.files.length > 0) {
        let result = await this.$utils.makeBase64(this.$refs.consultantVideo.files[0])
        this.consultantRequest.fileData = result.base64
        this.newFileData = result.pictureUrl
      }
    },
    validateData() {
      if (this.consultantRequest.durationMinute &&
        this.consultantRequest.cityId &&
        this.consultantRequest.stateId &&
        this.consultantRequest.shabaNo &&
        this.consultantRequest.cartNo &&
        this.consultantRequest.bankName &&
        this.consultantRequest.phoneNumber &&
        this.consultantRequest.nationalCode &&
        this.consultantRequest.imageFileData &&
        this.consultantRequest.fileData &&
        this.consultantRequest.description && this.consultantRequest.categories.length > 0) {
        this.consultantRequest?.prices?.forEach((item) => {
          if (typeof item === 'string') {
            item.price = item.price.replaceAll(',', '')
          }
        })
        delete this.consultantRequest.fileDataPictureUrl
        delete this.consultantRequest.imagePictureUrl
        if (!this.consultantRequest.shabaNo.includes('IR')) {
          this.consultantRequest.shabaNo = 'IR' + this.consultantRequest.shabaNo
        }
        if (this.startDate) {
          this.consultantRequest.startAndEndWork.push(this.startDate)
        }
        if (this.startDate) {
          this.consultantRequest.startAndEndWork.push(this.endDate)
        }
        this.createConsultantRequest()

      } else {
        this.$toast.error('لطفا همه فیلد های اجباری را تکمیل نمایید')
      }
    },
    async createConsultantRequest() {
      try {
        this.$nextTick(() => {
          this.$nuxt.$loading.start()
        })

        const res = await this.$repositories.createConsultantRequest.setPayload(this.consultantRequest)
        console.log(res)
        if (!res.data.isSuccess) {
          this.$toast.error(res.data.errorMessage)

        } else {
          this.$toast.success('درخواست شما با موفقیت ثبت شد')
          this.$auth.fetchUser()
          this.$router.push('/social/accountsetting/MyPage')
        }
      } catch (e) {
        console.log(e)
      } finally {
        this.$nuxt.$loading.finish()
        this.$nuxt.loading = false

      }
    },
    async getDurations() {

      try {
        const res = await this.$repositories.getDurations.setTag()
        this.durations = res.data
      } catch (e) {
        console.log(e)
      }

    },
    async getStates() {

      try {
        const res = await this.$repositories.GetAllStates.GetAllStates()
        this.states = res.data.states
      } catch (e) {
        console.log(e)
      }

    },
    async getCity() {
      try {
        const res = await this.$repositories.GetAllCities.GetAllCities({
          pageNumber: 1,
          count: 500,
          stateId: this.consultantRequest.stateId

        })
        this.cities = res.data.cities
      } catch (e) {
        console.log(e)
      }
    },
    async getConsultCategories() {
      try {
        const res = await this.$repositories.getAllConsultCategory.setTag()
        this.categories = res.data.data
      } catch (e) {
        console.log(e)
      } finally {

      }
    },
  },
  head() {
    return {
      title: 'مشاور شوید'
    }
  },
}
</script>

<style scoped>
.photo {
  width: 5rem;
  height: 5rem;
  border-radius: 100%;
  cursor: pointer;
}
</style>
