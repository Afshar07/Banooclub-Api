<template>
  <div class="container NewBg position-relative  px-2">
    <div class="row">
      <div class="col-md-12">
        <h1 class="h4">ثبت نام مشاور</h1>
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
            <div class="col-md-4 mt-2">
              <small class="p-2">زمان شروع</small>
              <v-select
                v-model="startDate"
                :options="generatedTimeStamps"
                :reduce="(value) => value.value"
                class="selectWidth BgInput w-100"
                dir="rtl"
                label="value"

              ></v-select>
            </div>
            <div class="col-md-4 mt-2">
              <small class="p-2">زمان پایان</small>
              <v-select
                v-model="endDate"
                :options="generatedTimeStamps"
                :reduce="(value) => value.value"
                class="selectWidth BgInput w-100"
                dir="rtl"
                label="value"

              ></v-select>
            </div>
          </div>

          <div class="row mb-2">
            <div class="col-md-12  d-flex align-items-center gap-2   my-2">
              <LazyPhoneIcon style="width: 20px;height: 20px;fill: #b44aff "></LazyPhoneIcon>
              <span>مشاوره تلفنی</span>
            </div>
            <div class="col-md-5 mt-2">
              <small class="p-2">قیمت (تومان)</small>
              <input v-model="formattedPhonePrice" class="with-border FormInputs mt-1" type="text">
            </div>
          </div>
          <div class="row mb-2">
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
          <div class="row mb-2">
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
              <div
                v-if="!consultantRequest.imagePictureUrl"
                class="border rounded-circle mt-2 d-flex text-center cursor-pointer justify-content-center align-items-center photo"
                @click="$refs.consultantPicture.click()"
              >

                <LazyUploadIcon class="tw-fill-[#b44aff]"></LazyUploadIcon>
              </div>
              <img
                v-else
                :src="consultantRequest.imagePictureUrl"
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
              <div
                v-if="!consultantRequest.fileDataPictureUrl"
                class="border rounded mt-2 d-flex text-center cursor-pointer justify-content-center align-items-center w-100"
                style="height: 200px"
                @click="$refs.consultantVideo.click()"
              >
                <LazyUploadIcon class="tw-fill-[#b44aff]"></LazyUploadIcon>
              </div>
              <div v-else>
                <button class="btn  btn-sm mt-2 text-white rounded" style="background-color: #b44aff!important"
                        type="button" @click="$refs.consultantVideo.click()">عوض
                  کردن ویدیو
                </button>
                <video
                  :src="consultantRequest.fileDataPictureUrl"
                  class="border rounded mt-2 d-flex text-center justify-content-center align-items-center w-100"
                  controls
                ></video>
              </div>
            </div>
            <div class="col-md-12 mt-3 ">
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
      consultantRequest: {
        nationalCode: "",
        medicalSystemNumber: "",
        stateId: 0,
        cityId: 0,
        phoneNumber: "",
        shabaNo: "",
        bankName: "",
        cartNo: "",
        durationMinute: null,
        description: "",
        fileData: "",
        fileDataPictureUrl: "",
        imageFileData: "",
        imagePictureUrl: "",
        prices: [
          {
            price: null,
            type: 1
          },
          {
            price: null,
            type: 2
          },
          {
            price: null,
            type: 3
          },
        ],
        startAndEndWork: [],
        categories: [],

      },
      categories: [],
      states: [],
      cities: [],
      selectedCategoryId: 0,
      description: '',
      newValue: ''
    }
  },
  async fetch() {
    await Promise.all([
      this.getConsultCategories(),
      this.getStates(),
      this.getDurations()
    ])
  },
  mounted() {
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

  },
  methods: {
    async handleFile() {
      if (this.$refs.consultantPicture.files.length > 0) {
        let result = await this.$utils.makeBase64(this.$refs.consultantPicture.files[0])
        this.consultantRequest.imageFileData = result.base64
        this.consultantRequest.imagePictureUrl = result.pictureUrl
      }
    },
    async handleVideo() {
      if (this.$refs.consultantVideo.files.length > 0) {
        let result = await this.$utils.makeBase64(this.$refs.consultantVideo.files[0])
        this.consultantRequest.fileData = result.base64
        this.consultantRequest.fileDataPictureUrl = result.pictureUrl
      }
    },
    validateData() {
      this.createConsultantRequest()
      //
      // if (this.consultantRequest.durationMinute &&
      //   this.consultantRequest.cityId &&
      //   this.consultantRequest.stateId &&
      //   this.consultantRequest.shabaNo &&
      //   this.consultantRequest.cartNo &&
      //   this.consultantRequest.bankName &&
      //   this.consultantRequest.phoneNumber &&
      //   this.consultantRequest.nationalCode &&
      //   this.consultantRequest.imageFileData &&
      //   this.consultantRequest.fileData &&
      //   this.consultantRequest.description && this.consultantRequest.categories.length > 0) {
      //   this.consultantRequest.prices.forEach((item) => {
      //     item.price = item.price.replaceAll(',', '')
      //   })
      //   // delete this.consultantRequest.fileDataPictureUrl
      //   // delete this.consultantRequest.imagePictureUrl
      //   if (!this.consultantRequest.shabaNo.includes('IR')) {
      //     this.consultantRequest.shabaNo = 'IR' + this.consultantRequest.shabaNo
      //   }
      //   if (this.consultantRequest.startAndEndWork.length > 0) {
      //     this.consultantRequest.startAndEndWork = []
      //   }
      //   this.consultantRequest.startAndEndWork.push(this.startDate)
      //   this.consultantRequest.startAndEndWork.push(this.endDate)
      //   this.createConsultantRequest()
      //
      // } else {
      //   this.$toast.error('لطفا همه فیلد های اجباری را تکمیل نمایید')
      // }
    },
    async createConsultantRequest() {
      try {
        this.$nextTick(() => {
          this.$nuxt.$loading.start()
        })

        const res = await this.$repositories.createConsultantRequest.setPayload(this.consultantRequest)
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
