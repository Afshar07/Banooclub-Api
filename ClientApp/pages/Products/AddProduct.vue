<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class=" mcontainer NewBg  px-5">

    <div class="tw-w-full bg-white tw-shadow p-3 d-flex align-items-center justify-content-between gap-3  rounded ">
      <div class="d-flex align-items-center gap-2">
        <img src="/girl-icon-khadamat-afzodan.png" class="tw-w-[7rem] tw-h-[7rem] tw-object-contain" alt="">
        <div class="d-flex align-items-center flex-column">
          <h1 class="text-purple h6" style="font-weight: bolder!important;"> ثبت خدمت</h1>
          <strong class="text-pink">Submit A Service</strong>
        </div>
      </div>
    </div>
    <div class="row my-3 bg-white px-3 tw-shadow rounded " style="padding-left: 0">
      <div class="col-md-6 col-sm-12 mt-3 " style="padding-left: 0">
        <label>عنوان خدمت *</label>
        <input v-model="service_title" type="text" class="with-border FormInputs" placeholder="آموزش اکستنشن مژه"
               :class="{BorderRed:service_title==='',BorderGreen:service_title!==''}">
      </div>
      <div class="col-md-6 col-sm-12 mt-3 " style="padding-left: 0">
        <label>دسته بندی خدمت *</label>
        <select v-model="service_category" class=" FormInputs" aria-label="Default FormInputs select example"
                :class="{BorderRed:service_category===null,BorderGreen:service_category!==null}">
          <option class="" :value="null">دسته بندی خدمت</option>
          <option class="" v-for="(service_category,idx) in categories" :key="idx" :value="service_category.serviceCategoryId">
            <small>
            {{ service_category.title }}
              <span v-if="service_category.isFunTime" class="badge pill  bg-purple text-white">(شادی تایم)</span>
            </small>

          </option>
        </select>
      </div>
      <div class="col-12 py-3" style="padding-left: 0">
        <label>توضیحات</label>
        <textarea
          v-model="service_desc"
          row="100"
          style="color: #808080;"
          class="form-control border rounded FormInputs w-100 with-border"
          placeholder="در مورد خدمت توضیح دهید"
          id="description"
        ></textarea>
      </div>
      <div class="col-md-6 my-3">
        <v-select
          @input="GetCity"
          :options="AllStates"
          label="name"
          dir="rtl"
          class="selectWidth BgInput w-100"
          placeholder="استان*"
          v-model="SelectedStateId"
          :reduce="(name) => name.stateId"

        ></v-select>
      </div>
      <div class="col-md-6 my-3">
        <v-select
          :options="AllCities"
          label="name"
          dir="rtl"
          class="selectWidth BgInput w-100"
          placeholder="شهر*"
          v-model="SelectedCityId"
          :reduce="(name) => name.cityId"

        ></v-select>
      </div>
      <div class="col-12 py-3" style="padding-left: 0">
        <label>آدرس</label>
        <textarea
          v-model="service_address"
          style="color: #808080;"
          row="100"
          class="form-control border FormInputs rounded w-100"
          placeholder="آدرس خدمت"
          id="address"
        ></textarea>
      </div>
      <div class="col-12 py-3" style="padding-left: 0">
        <div class="labelText">موقعیت روی نقشه</div>
        <SetLocation @getGeoLocation="SetGeoLocation"></SetLocation>
      </div>

      <div class="col-md-6 col-sm-12 pt-3" style="padding-left: 0">
        <label>ظرفیت</label>
        <input maxlength="11" v-model="Qty" type="number" class="with-border FormInputs" placeholder="ظرفیت خدمت">
      </div>
      <div class="col-md-6 col-sm-12 pt-3" style="padding-left: 0">
        <label>شماره موبایل *</label>
        <input maxlength="11" v-model="mobile" type="number" class="with-border FormInputs" placeholder="شماره همراه با صفر"
               :class="{BorderRed:mobile===null,BorderGreen:mobile!==0}">
      </div>
      <div class="col-md-6 col-sm-12 pt-3" style="padding-left: 0">
        <label>شماره تلفن 1</label>
        <input maxlength="11" v-model="phone_number1" type="number" class="with-border FormInputs" placeholder="شماره تلفن">
      </div>
      <div class="col-md-6 col-sm-12 pt-3" style="padding-left: 0">
        <label>شماره تلفن 2</label>
        <input maxlength="11" v-model="phone_number2" type="number" class="with-border FormInputs" placeholder="شماره تلفن">
      </div>

      <div class="col-md-12 col-sm-12 pt-3" style="padding-left: 0">
        <label>ایمیل</label>
        <input v-model="email" type="email" class="with-border FormInputs" placeholder="ایمیل">
      </div>
      <div class="col-md-6 col-sm-12 pt-3" style="padding-left: 0">
        <label class="mb-3">تاریخ شروع خدمت (اختیاری)</label>
        <client-only>
          <date-picker
            format="YYYY-MM-DD"
            display-format="jYYYY-jMM-jDD"
            v-model="StartDate"
            inputClass="vpd-input-group1"
            type="date"
          />
        </client-only>
      </div>
      <div class="col-md-6 col-sm-12 pt-3" style="padding-left: 0">
        <label class="mb-3">تاریخ انقضاء خدمت</label>
        <client-only>
          <date-picker
            format="YYYY-MM-DD"
            display-format="jYYYY-jMM-jDD"
            inputClass="vpd-input-group1"
            v-model="expiration_date"
            type="date"
          />
        </client-only>
      </div>
      <div class="col-12 pt-3" style="padding-left: 0">
        <div class="labelText">
          هشتگ های خدمت
        </div>
        <div class="my-3">
          <div class="d-flex align-items-center gap-2">
            <input @keydown.enter="addTags" v-model="tag" type="text" ref="TagsInput" class=" form-control FormInputs with-border">

            <button class="bg-purple text-white p-2 rounded tw-cursor-pointer" @click="addTags">ثبت</button>
          </div>


          <div class="d-flex align-items-center flex-wrap gap-2 justify-content-start my-2">
            <div class="back_tags p-1 m-1 d-flex" v-for="(tag,index) in tags" :key="index">
              <button @click="removeTag(index)">
                <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-3 tw-w-3 tw-pl-1" fill="none" viewBox="0 0 24 24"
                     stroke="currentColor" stroke-width="2">
                  <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12"/>
                </svg>
              </button>
              {{ tag }}
            </div>
          </div>


        </div>
      </div>
      <div class="col-12 pt-3" style="padding-left: 0">
        <div class="labelText">
          انتخاب عکس خدمت (اولین عکس به عنوان عکس اصلی خدمت بارگذاری می شود - حداکثر 10 عکس می توانید بارگذاری
          کنید)
        </div>
        <div class="d-flex row">
          <div class="py-3 col-lg-2 col-md-3 px-3">
            <div @click="uploadNewPicture"
                 class="tw-bg-[#f9f9f9] tw-border-2 tw-border-dashed tw-flex tw-flex-col tw-h-32 tw-items-center tw-justify-center tw-relative tw-rounded-lg">
              <UploadFileIcon class="tw-fill-[#b44aff]" style="width: 48px; height: 48px;"/>
            </div>
            <input
              ref="picture_file"
              type="file"
              accept="image/*"
              multiple="multiple"
              class="form-control-file my-file d-none"
              id="picture_file"
              @change="onFileChange"
            />
          </div>
          <div
            class="py-3 col-lg-1 col-md-3 px-1 tw-h-40 d-flex photos position-relative"
            v-for="(item,index) in BaseImgUrls"
            :key="index"
          >
            <img class="tw-rounded-lg tw-h-32" :alt="service_title?service_title:''" width="100%" :src="item"
                 style="object-fit: contain" :class="{BorderGreen:index===0}"/>
            <span class="position-absolute deleteIcon m-2" style="top: 15px;" @click="deleteImage(index)">
                <font-awesome-icon icon="trash" color="#ff4d4d" size="sm"/>
              </span>
          </div>
        </div>

      </div>
      <div class="col-12 pt-3" style="padding-left: 0">
        <div class="labelText">
          بارگذاری فیلم (حداکثر زمان ویدئوها 2 دقیقه و حداکثر 3 ویدئو می توانید بارگذاری کنید)
        </div>
        <div class="d-flex flex-row row">
          <div class="col-lg-2 col-md-6 py-3 px-3">
            <div @click="uploadNewVideo" class="tw-bg-[#f9f9f9]
              tw-border-2 tw-border-dashed tw-flex tw-flex-col tw-h-32 tw-items-center
              tw-justify-center tw-relative tw-rounded-lg">
              <UploadFileIcon class="tw-fill-[#b44aff]" style="width: 48px; height: 48px;"/>
            </div>
            <input
              ref="VideoFile"
              type="file"
              accept="video/*"
              multiple="multiple"
              class="form-control-file my-file d-none"
              id="my-file"
              @change="onFileChangeVideo"
            />
          </div>
          <div
            class="col-lg-3 col-md-6 d-inline-block photos position-relative px-1 my-3"
            v-for="(item,idx) in BaseVideos"
            :key="idx"
          >
            <video
              width="auto"
              class="w-100 tw-rounded-lg tw-h-32"
              controls
              :src="item"
            ></video>
            <span class="position-absolute deleteIcon m-3" style="top: 0" @click="deleteVideo(idx)">
                <font-awesome-icon icon="trash" color="#ff4d4d" size="sm"/>
              </span>
          </div>
        </div>

      </div>
      <div class="col-12 py-3" style="padding-left: 0">
        <div class="labelText">
          اگر کاربر دیگری در اجرای این خدمت با شما مشارکت دارد نام او را ثبت کنید
        </div>
        <div class="my-3">
          <div class="d-flex align-items-center gap-2">
            <v-select
              :options="AllUsers"
              label="userName"
              dir="rtl"
              multiple
              class="selectWidth BgInput w-100"
              v-model="SelectedUserIds"
              :reduce="(userName) => userName.userId"

            ></v-select>

          </div>
        </div>
      </div>
      <div class="col-12 pt-3" style="padding-left: 0">

        <div class="d-flex justify-content-between align-items-center">
          <div class="d-flex flex-wrap align-items-center gap-2">
            <div class="labelText">
              افزودن ویژگی خدمت (بعد از تکمیل اطلاعات ویژگی ها را ذخیره کنید)
            </div>
            <button @click="increasePropertyCount" class="btn AddReplyBtn text-white">
              <PlusIcon fill="#fc5399" style="width: 40px; height: 40px;"/>
            </button>
          </div>
          <div class="d-flex align-items-center gap-2">
            <small>خدمت رایگان است</small>

            <label class="switch">
              <input
                type="checkbox"
                id="togBtn12"
                v-model="IsFreeService"
                :checked="IsFreeService"
              />
              <div class="slider round">
                <span class="on">بله</span>
                <span class="off">خیر</span>
              </div>
            </label>
          </div>
        </div>
        <div class="row" style="height: 200px;overflow-y: scroll">
          <div v-for="(property,idx) in property_count" :key="idx" class="col-md-12 my-3">
            <div class="row">
              <div class=" col-md-3 col-sm-12">
                <label>نام ویژگی</label>
                <input :ref="`Title${idx}`" type="text" class="with-border FormInputs" placeholder="نام ویژگی">
              </div>
              <div v-if="!IsFreeService" class=" col-md-3 col-sm-12 d-flex flex-column justify-content-end   ">
                <div class="d-flex align-items-center justify-content-between">


                  <small>قیمت ویژگی</small>
                  <div class="d-flex align-items-center gap-2">
                    <small>ویژگی رایگان</small>
                    <input type="checkbox" @click="Disable(idx)" style="width: 20px;height: 20px" class="form-check ">
                  </div>
                </div>

                <input maxlength="11" :ref="`Price${idx}`" type="number" class="with-border FormInputs" placeholder="قیمت ویژگی">
              </div>
              <div class="col-md-3 d-flex align-items-center">
                <i @click="decreasePropertyCount(idx)" class="fa fa-minus-circle text-danger tw-cursor-pointer"></i>
              </div>
            </div>
          </div>
        </div>
        <div class="row">
          <div v-if="property_count.length>0 " class="col-md-6 d-flex flex-column gap-3">
            <div class="tw-w-full d-flex align-items-center justify-content-between ">
              <span>جمع ویژگی ها :</span>
              <span> {{ totalPrice }} <small> تومان</small></span>
            </div>
            <div class="tw-w-full d-flex align-items-center justify-content-between ">
              <span class="text-purple">جمع کل خدمت :</span>
              <span class="text-purple">{{ totalPrice }} <small>تومان</small></span>
            </div>
            <div class="d-flex justify-content-center my-3">
              <button @click="addProperty()" type="button" class="p-2 rounded text-white bg-pink tw-cursor-pointer">
                ذخیره ویژگی ها
              </button>
            </div>
          </div>
        </div>
      </div>
      <div class="d-flex align-items-center justify-content-center col-12 py-3 border-top ">
        <button type="button" class="tw-bg-[#d277ff] text-white rounded p-2 tw-cursor-pointer"
                @click="submitCreateService">
          ثبت نهایی
        </button>
      </div>
    </div>


  </div>


</template>

<script>
import UploadFileIcon from "../../components/Icons/UploadFileIcon";
import PlusIcon from "../../components/Icons/PlusIcon";
import SetLocation from "../../components/SetLocation";

export default {
  name: "AddProduct",
  components: {PlusIcon, UploadFileIcon, SetLocation},
  layout: "PoshtebamPlusLayout",
  fetchOnServer: false,
  head() {
    return {
      title: 'افزودن خدمت',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'افزودن خدمت',
        },
      ],
    }
  },
  async fetch() {
    try {
      const following =
        await this.$repositories.getMyFollowings.getMyFollowings();
      following.data.forEach((item)=>{
        this.AllUsers.push(item.userInfo)
      })

    }catch (error) {
      console.log(error);
    }
    try {
      const allCategories =
        await this.$repositories.getAllServicesCategory.getAllServicesCategory();
      this.categories = allCategories.data.serviceCategories;
    } catch (error) {
      console.error(error)
    }

    try {
      const res = await this.$repositories.GetAllStates.GetAllStates()
      this.AllStates = res.data.states
    } catch (e) {
      console.log(e)
    }
    // Get categories

  },

  data() {
    return {
      Discount: {
        type: 0,
        value: 0,
        startDate: '',
        expireDate: '',
        refraction: false
      },
      service_title: '',
      service_category: null,
      service_desc: '',
      IsFreeService: false,
      AllStates: [],
      AllCities: [],
      SelectedCityId: null,
      SelectedStateId: null,
      service_address: '',
      phone_number1: null,
      phone_number2: null,
      SelectedUserIds:[],
      AllUsers:[],
      Qty: 0,
      mobile: null,
      email: '',
      web_address: '',
      BaseImgUrls: [],

      HaveDiscount: false,
      BaseVideos: [],
      categories: [],
      expiration_date: "",
      photos: [],
      Videos: [],
      property_count: [],
      tags: [],
      tag: '',
      render_tags: null,
      StartDate: '',
      properties: null,
      totalPrice: 0,
      latitude: 0,
      longitude: 0
    };
  },
  methods: {

    async GetCity() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start()
      })
      try {
        const res = await this.$repositories.GetAllCities.GetAllCities({
          pageNumber: 1,
          count: 500,
          stateId: this.SelectedStateId

        })
        this.AllCities = res.data.cities
        this.$nuxt.$loading.finish()
        this.$nuxt.loading = false;
      } catch (e) {
        console.log(e)
      } finally {
        this.$nuxt.$loading.finish()
        this.$nuxt.loading = false;
      }
    },

    SetGeoLocation(lat, lang) {
      this.latitude = lat
      this.longitude = lang

    },
    removeTag(index) {
      this.tags.splice(index, 1)
    },
    addTags() {
      if (this.tag === '') {
        this.$toast.error('هشتگ مورد نظر را وارد کنید')
      } else {
        this.tags.push(this.tag)
        this.tag = ''

      }
      this.$refs.TagsInput.focus()
    },
    Disable(idx) {

      if (this.$refs['Price' + idx][0].hasAttribute('disabled')) {
        this.$refs['Price' + idx][0].removeAttribute('disabled')
        this.$refs['Price' + idx][0].classList.remove('DisabledInput')
      } else {
        this.$refs['Price' + idx][0].setAttribute('disabled', '')
        this.$refs['Price' + idx][0].value = 0
        this.$refs['Price' + idx][0].classList.add('DisabledInput')

      }


    },
    addProperty() {
      let tmplist = []
      let tmpitem = {
        name: '',
        price: 0,
        isFree: false
      }
      this.property_count.forEach((item, idx) => {
        tmpitem.name = this.$refs[`Title${idx}`][0].value
        if (!this.IsFreeService) {
          tmpitem.price = this.$refs[`Price${idx}`][0].value
          if (this.$refs[`Price${idx}`][0].value === '0') {
            tmpitem.isFree = true
          }
        } else {
          tmpitem.price = 0
        }

        const clone = {...tmpitem}
        tmplist.push(clone)
        tmpitem.name = ''
        tmpitem.price = 0
      })
      this.properties = tmplist
      this.$toast.success("ویژگی ها با موفقیت ثبت شدند");
      let result = 0
      this.properties.forEach((element) => {
        result += parseInt(element.price)
      })
      this.totalPrice = result

    },
    deleteImage(item) {
      const idx = this.BaseImgUrls.findIndex((e) => e === item);

      this.BaseImgUrls.splice(idx, 1);

      this.photos.splice(idx, 1);
      console.log(this.BaseImgUrls);
    },
    deleteVideo(item) {
      const idx = this.BaseVideos.findIndex((e) => e === item);

      this.BaseVideos.splice(idx, 1);

      this.Videos.splice(idx, 1);
      console.log(this.BaseImgUrls);
    },
    addMarker(event) {
      this.latlng = event.latlng;
    },
    uploadNewPicture() {
      this.$refs.picture_file.click();
    },
    uploadNewVideo() {
      this.$refs.VideoFile.click();
    },
    onFileChange(e) {
      console.log(this.$refs.picture_file.files);
      const that = this;
      const f = [];

      that.Uploaded = true;
      Array.prototype.forEach.call(this.$refs.picture_file.files, (element) => {
        if (element.size > 512000) {
          this.$toast.error('اندازه عکس نمیتواند بیشتر از 512 کیلوبایت باشد')
        } else if (this.photos.length >= 10) {
          this.$toast.error('بیشتر از 10 عکس نمیتوانید بارگذاری کنید')
        } else {
          f.push(element)
        }
      });
      f.forEach((element) => {
        this.BaseImgUrls.push(URL.createObjectURL(element));
        const reader = new FileReader();
        reader.onload = (function (theFile) {
          return function () {
            const binaryData = reader.result;
            that.photos.push(window.btoa(binaryData));
          };
        })(f);
        reader.readAsBinaryString(element);
      });
    },
    onFileChangeVideo() {
      console.log(this.$refs.VideoFile.files);
      const that = this;
      const f = [];

      that.Uploaded = true;
      Array.prototype.forEach.call(this.$refs.VideoFile.files, (element) => {
        if (element.duration > 120000) {
          this.$toast.error('زمان ویدئو بارگذازری شده باید کمتر از 2 دقیقه باشد')
        } else if (this.Videos.length >= 3) {
          this.$toast.error('بیشتر از 3 ویدئو نمیتوانید بارگذاری کنید')
        } else {
          f.push(element)
        }
      });
      f.forEach((element) => {
        this.BaseVideos.push(URL.createObjectURL(element));
        const reader = new FileReader();
        reader.onload = (function (theFile) {
          return function () {
            const binaryData = reader.result;
            that.Videos.push(window.btoa(binaryData));
          };
        })(f);
        reader.readAsBinaryString(element);
      });
    },
    async submitCreateService() {
      if (this.service_title === '') {
        this.$toast.error("لطفا نام خدمت را وارد کنید");
      } else if (this.service_category === null) {
        this.$toast.error("لطفا دسته بندی خدمت را مشخص کنید");
      } else if (this.mobile === null) {
        this.$toast.error("لطفا شماره مستقیم تماس خود را وارد کنید");
      } else if (this.mobile.length > 11) {
        this.$toast.error("فرمت وارد شده برای شماره مستقیم تماس درست نیست");
      } else if (this.phone_number1 !== null && this.phone_number1.length > 11) {
        this.$toast.error("فرمت وارد شده برای شماره تلفن اول درست نیست");
      } else if (this.phone_number2 !== null && this.phone_number2.length > 11) {
        this.$toast.error("فرمت وارد شده برای شماره تلفن دوم درست نیست");
      } else if (this.email !== '' && !this.email.includes('@')) {
        this.$toast.error("ایمیل وارد شده معتبر نیست");
      } else if (this.photos.length === 0) {
        this.$toast.error("حداقل یک عکس به عنوان عکس اصلی وارد کنید");
      }else {
        this.$nuxt.$loading.start();
        try {
          let tmpMedias = []
          let tmpMedia = {
            base64: '',
            priority: 0
          }
          let id = 1
          this.photos.forEach((element) => {
            tmpMedia.base64 = element
            tmpMedia.priority = id
            const clone = {...tmpMedia}
            tmpMedias.push(clone)
            tmpMedia.base64 = ''
            id = 2
          })
          this.Videos.forEach((element) => {
            tmpMedia.base64 = element
            tmpMedia.priority = 3
            const clone = {...tmpMedia}
            tmpMedias.push(clone)
            tmpMedia.base64 = ''
          })

          let tmptags = []
          let tmptag = {
            type: 1,
            title: '',
          }
          this.tags.forEach((element) => {
            tmptag.title = element
            const clone = {...tmptag}
            tmptags.push(clone)
            tmptag.title = ''

          })


          const res = await this.$repositories.createAService.createAService({
            title: this.service_title,
            userId: 0,
            serviceCategoryId: this.service_category,
            description: this.service_desc,
            seoTitle: '',
            seoDescription: '',
            address: this.service_address,
            latitude: this.latitude,
            longitude: this.longitude,
            webAddress: this.web_address,
            phoneNumber1: this.phone_number1,
            phoneNumber2: this.phone_number2,
            mobile: this.mobile,
            quantity: parseInt(this.Qty),
            email: this.email,
            fireDate: '2022-06-25T12:04:09.204Z',
            StartDate: this.StartDate,
            isFree: this.IsFreeService,
            specified: false,
            cityId:this.SelectedCityId,
            stateId:this.SelectedStateId,
            specifiedWithExpireDate: false,
            specifiedExpireDateTime: null,
            ownerUserIds:this.SelectedUserIds,
            totalPrice: this.totalPrice,
            expireDate: this.expiration_date,
            properties: this.properties,
            medias: tmpMedias,
            tags: tmptags
          });
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
          this.$toast.success("ثبت خدمت با موفقیت انجام شد.");
          this.$router.push({path: `/Products/Upgrade/${res.data}`, query: {active_tab: 'preview'}});
        } catch (error) {
          console.log(error);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
        }
      }
    },
    increasePropertyCount() {
      this.property_count.push('property')
    },
    decreasePropertyCount(index) {
      this.property_count.splice(index, 1)
    },
  }


}
</script>

<style scoped>
.form-select {
  color: #808080;
  border: 1px solid #e0e0e0;
  border-radius: 6px;
}

.form-select:focus {
  border-color: #ced4da !important;
  outline: 0;
}

@media (min-width: 1024px) {
  .mcontainer {
    max-width: 1000px;
    padding: 30px 0px;
    margin-left: auto;
    margin-right: auto;
  }
}

@media (max-width: 1024px) {
  .mcontainer {
    max-width: 1000px;
    padding: 25px 0px;
    margin-left: auto;
    margin-right: auto;
  }
}

.vpd-input-group label {
  margin-bottom: 0 !important;
}

.BorderRed {
  border: solid 1px red !important;
}

.BorderGreen {
  border: solid 1px green !important;
}

.switch {
  position: relative;
  display: inline-block;
  width: 90px;
  height: 34px;
}

.switch input {
  display: none;
}

.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #999;
  -webkit-transition: 0.4s;
  transition: 0.4s;
}

.slider:before {
  position: absolute;
  content: "";
  height: 26px;
  width: 26px;
  left: 4px;
  bottom: 4px;
  background-color: white;
  -webkit-transition: 0.4s;
  transition: 0.4s;
}

input:checked + .slider {
  background-color: #088dcd;
}

input:focus + .slider {
  box-shadow: 0 0 1px #2196f3;
}

input:checked + .slider:before {
  -webkit-transform: translateX(55px);
  -ms-transform: translateX(55px);
  transform: translateX(55px);
}

/*------ ADDED CSS ---------*/
.on {
  display: none;
}

.on {
  color: white;
  position: absolute;
  transform: translate(-50%, -50%);
  top: 50%;
  left: 35%;
  font-size: 10px;
}

.off {
  color: white;
  position: absolute;
  transform: translate(-50%, -50%);
  top: 50%;
  left: 65%;
  font-size: 10px;
}

.DisabledInput {
  @apply tw-bg-stone-400

}

input:checked + .slider .on {
  display: block;
}

input:checked + .slider .off {
  display: none;
}

/*--------- END --------*/
/* Rounded sliders */
.slider.round {
  border-radius: 34px;
}

.slider.round:before {
  border-radius: 50%;
}

.sidenavOptionText {
  font-size: 14px;
  color: #999;
  padding-top: 8px;
}
.vpd-input-group1{
  background-color: #faf7ff!important;
}

</style>

