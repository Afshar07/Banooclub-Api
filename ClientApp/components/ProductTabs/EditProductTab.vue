<template>
  <div class="row  p-3">
    <div
      class="modal fade"
      id="AddDiscount"
      tabindex="-1"
      role="dialog"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">ایجاد تخفیف جدید</h5>

          </div>
          <div class="modal-body">
            <div class="row">


              <div class="col-md-12">
                <small>نوع تخفیف : </small>
                <select class="form-control" placeholder="نوع تخفیف را انتخاب کنید" v-model="Discount.type">
                  <option disabled selected>نوع تخفیف را انتخاب کنید</option>
                  <option :value="1">درصدی</option>
                  <option :value="2">عددی</option>
                </select>
              </div>
              <div class="col-md-12 my-3">
                <small>مقدار تخفیف</small>
                <input type="number" maxlength="10" class="form-control with-border" v-model="Discount.value"
                       placeholder="مقدار تخفیف را وارد کنید">
              </div>
              <div class="col-md-12">
                <small>تاریخ اتمام تخفیف</small>
                <client-only>
                  <date-picker
                    format="YYYY-MM-DD HH:MM"
                    display-format="jYYYY-jMM-jDD HH:MM"
                    v-model="Discount.expireDate"
                    type="date"
                  />
                </client-only>
              </div>
              <!--                <div class="col-md-3">-->
              <!--                  <small>تاریخ شروع تخفیف</small>-->
              <!--                  <client-only>-->
              <!--                    <date-picker-->
              <!--                      format="YYYY-MM-DD"-->
              <!--                      display-format="jYYYY-jMM-jDD"-->
              <!--                      v-model="Discount.startDate"-->
              <!--                      type="date"-->
              <!--                    />-->
              <!--                  </client-only>-->
              <!--                </div>-->


            </div>
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="modal"
            >
              بستن
            </button>
            <button @click="AddDiscount" type="button" class="bg-pink p-2 text-white rounded shadow tw-cursor-pointer"
                    data-bs-dismiss="modal">
              ایجاد تخفیف
            </button>
          </div>
        </div>
      </div>
    </div>
    <div
      v-if="serviceDetailProp&& serviceDetailProp.discount!==null"
      class="modal fade"
      id="EditDiscount"
      tabindex="-1"
      role="dialog"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabe2l">ویرایش تخفیف</h5>

          </div>
          <div class="modal-body">
            <div class="row">


              <div class="col-md-12">
                <small>نوع تخفیف : </small>
                <select class="form-control" v-model="serviceDetailProp.discount.type">
                  <option disabled selected>نوع تخفیف را انتخاب کنید</option>
                  <option :value="1">درصدی</option>
                  <option :value="2">عددی</option>
                </select>
              </div>
              <div class="col-md-12 my-3">
                <small>مقدار تخفیف</small>
                <input type="number" maxlength="10" class="form-control with-border"
                       v-model="serviceDetailProp.discount.value" placeholder="مقدار تخفیف را وارد کنید">
              </div>
              <div class="col-md-12">
                <small>تاریخ اتمام تخفیف</small>
                <client-only>
                  <date-picker
                    format="YYYY-MM-DD HH:MM"
                    display-format="jYYYY-jMM-jDD HH:MM"
                    v-model="serviceDetailProp.discount.expireDate"
                    type="date"
                  />
                </client-only>
              </div>
              <!--                <div class="col-md-3">-->
              <!--                  <small>تاریخ شروع تخفیف</small>-->
              <!--                  <client-only>-->
              <!--                    <date-picker-->
              <!--                      format="YYYY-MM-DD"-->
              <!--                      display-format="jYYYY-jMM-jDD"-->
              <!--                      v-model="Discount.startDate"-->
              <!--                      type="date"-->
              <!--                    />-->
              <!--                  </client-only>-->
              <!--                </div>-->


            </div>
          </div>
          <div class="modal-footer">
            <button @click="UpdateDiscount" type="button"
                    class="bg-pink p-2 text-white rounded shadow tw-cursor-pointer" data-bs-dismiss="modal">
              بروزرسانی
            </button>
            <button @click="DeleteDiscount" type="button"
                    class="bg-purple p-2 text-white rounded shadow tw-cursor-pointer" data-bs-dismiss="modal">
              حذف
            </button>
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="modal"
            >
              بستن
            </button>


          </div>
        </div>
      </div>
    </div>
    <!--      <h2 class="tw-text-2xl tw-font-semibold pb-3">ویرایش خدمت</h2>-->
    <div v-if="serviceDetailProp" class="row px-5">
      <div class="col-md-6 col-12 pt-3">
        <label>عنوان خدمت *</label>
        <input v-model="serviceDetailProp.title" type="text" class=" FormInputs with-border"
               placeholder="آموزش اکستنشن مژه">
      </div>
      <div class="col-md-6 col-12 pt-3">
        <label>دسته بندی خدمت *</label>
        <select v-model="serviceDetailProp.serviceCategoryId" class="form-select FormInputs"
                aria-label="Default select example">
          <option :value="null">دسته بندی خدمت</option>
          <option v-for="(service_category,idx) in categories" :key="idx" :value="service_category.serviceCategoryId">
            {{ service_category.title }}
          </option>
        </select>
      </div>
      <div class="col-12 py-3">
        <label>توضیحات</label>
        <textarea
          v-model="serviceDetailProp.description"
          row="100"
          style="color: #808080;"
          class="FormInputs border rounded w-100"
          placeholder="در مورد خدمت توضیح دهید"
          id="about"
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
          v-model="serviceDetailProp.stateId"
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
          v-model="serviceDetailProp.cityId"
          :reduce="(name) => name.cityId"

        ></v-select>
      </div>
      <div class="col-12 py-3">
        <label>آدرس</label>
        <textarea
          v-model="serviceDetailProp.address"
          row="100"
          style="color: #808080;"
          class="FormInputs border rounded w-100"
          placeholder="آدرس خدمت"
          id="about2"
        ></textarea>
      </div>
      <div class="col-12 py-3">
        <div class="labelText">موقعیت روی نقشه</div>
        <div class="my-3" id="map-wrap">

          <SetLocation @getGeoLocation="SetLcoation"
                       :defaultMarkerGeoLoc="[serviceDetailProp.latitude,serviceDetailProp.longitude]"
                       :defaultGeoLoc="[serviceDetailProp.latitude,serviceDetailProp.longitude]"></SetLocation>

        </div>
      </div>

      <div class="col-md-6 col-12 pt-3">
        <label>شماره موبایل *</label>
        <input v-model="serviceDetailProp.mobile" type="text" class=" FormInputs with-border"
               placeholder="شماره همراه با صفر">
      </div>
      <div class="col-md-6 col-12 pt-3">
        <label>شماره تلفن 1</label>
        <input v-model="serviceDetailProp.phoneNumber1" type="text" class=" FormInputs with-border"
               placeholder="شماره تلفن">
      </div>
      <div class="col-md-6 col-12 pt-3">
        <label>شماره تلفن 2</label>
        <input v-model="serviceDetailProp.phoneNumber2" type="text" class=" FormInputs with-border"
               placeholder="شماره تلفن">
      </div>

      <div class="col-md-6 col-12 pt-3">
        <label>ایمیل</label>
        <input v-model="serviceDetailProp.email" type="text" class=" FormInputs with-border" placeholder="ایمیل">
      </div>
      <div class="col-md-6 col-sm-12 pt-3" style="padding-left: 0">
        <label class="mb-3">تاریخ شروع خدمت (اختیاری)</label>
        <client-only>
          <date-picker
            format="YYYY-MM-DD"
            display-format="jYYYY-jMM-jDD"
            v-model="serviceDetailProp.startDate"
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
            v-model="serviceDetailProp.expireDate"
            type="date"
          />
        </client-only>
      </div>
      <div class="col-md-6 col-sm-12 pt-3" style="padding-left: 0">
        <label>ظرفیت</label>
        <input maxlength="11" v-model="serviceDetailProp.quantity" type="number" class="with-border FormInputs"
               placeholder="ظرفیت خدمت">
      </div>
      <div class="col-md-12 col-12 pt-3">
        <div class="labelText">
          هشتگ های خدمت
        </div>
        <div class="my-3">
          <div class="d-flex align-items-center gap-2">
            <input v-on:keyup.enter="createTag" v-model="tag" type="text" class=" FormInputs with-border">

            <button class="bg-purple text-white p-2 rounded tw-cursor-pointer" @click="createTag">ثبت</button>
          </div>

          <div class="d-flex">
            <div class="back_tags p-1 m-1 d-flex" v-for="(tag,index) in serviceDetailProp.tags" :key="index">
              <button @click="deleteServiceTag(tag.tagId)">
                <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-3 tw-w-3 tw-pl-1" fill="none" viewBox="0 0 24 24"
                     stroke="currentColor" stroke-width="2">
                  <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12"/>
                </svg>
              </button>
              {{ tag.title }}
            </div>
          </div>

        </div>
      </div>
      <div class="col-12 pt-3">
        <div class="labelText">
          انتخاب عکس خدمت (اولین عکس به عنوان عکس اصلی خدمت بارگذاری می شود - حداکثر 10 عکس می توانید بارگذاری کنید)
        </div>
        <div class="d-flex row">
          <div class="py-3 col-lg-2 col-md-3 px-3">
            <div @click="uploadNewPicture"
                 class="tw-bg-gray-100 tw-border-2 tw-border-dashed tw-flex tw-flex-col tw-h-32 tw-items-center tw-justify-center tw-relative tw-rounded-lg">
              <UploadFileIcon class="tw-fill-[#85ffdd]" style="width: 48px; height: 48px;"/>
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
            v-for="(image,index) in new_image_preview"
            :key="index"
          >
            <img class="tw-rounded-lg tw-h-32" width="100%" :src="image.base64" :alt="serviceDetailProp.title"
                 style="object-fit: contain" :class="{BorderGreen:index===0}"/>
            <span class="position-absolute deleteIcon m-3" style="top: 15px" @click="deleteNewImage(index)">
                <font-awesome-icon icon="trash" color="#85ffdd" size="sm"/>
              </span>
          </div>
        </div>
        <div class="d-flex row">
          <div class="py-3 col-lg-2 col-md-3 px-3">
            <div
              class="tw-bg-gray-100 tw-border-2 tw-border-dashed tw-flex tw-flex-col tw-h-32 tw-items-center tw-justify-center tw-relative tw-rounded-lg">
              تصاویر شما
            </div>
          </div>
          <div
            class="py-3 col-lg-1 col-md-3 px-1 tw-h-40 d-flex photos position-relative"
            v-for="(image,index) in images_preview"
            :key="index"
          >
            <img class="tw-rounded-lg tw-h-32" width="100%"
                 :src="`https://api.banooclub.simagar.com/media/gallery/Service/${image.base64}`"
                 :alt="serviceDetailProp.title"
                 style="object-fit: contain" :class="{BorderGreen:index===0}"/>
            <span class="position-absolute deleteIcon m-3" style="top: 15px" @click="deleteImage(image,index)">
                <font-awesome-icon icon="trash" color="#85ffdd" size="sm"/>
              </span>
          </div>
        </div>
      </div>
      <div class="col-12 pt-3">
        <div class="labelText">
          بارگذاری فیلم (حداکثر زمان ویدئوها 2 دقیقه و حداکثر 3 ویدئو می توانید بارگذاری کنید)
        </div>
        <div class="d-flex flex-row row">
          <div class="col-lg-2 col-md-6 py-3 px-3">
            <div @click="uploadNewVideo" class="tw-bg-gray-100
              tw-border-2 tw-border-dashed tw-flex tw-flex-col tw-h-32 tw-items-center
              tw-justify-center tw-relative tw-rounded-lg">
              <UploadFileIcon class="tw-fill-[#85ffdd]" style="width: 48px; height: 48px;"/>
            </div>
            <input
              ref="VideoFile"
              type="file"
              accept="video/*"

              class="form-control-file my-file d-none"
              id="my-file"
              @change="onFileChangeVideo"
            />
          </div>
          <div
            class="col-lg-3 col-md-6 d-inline-block photos position-relative px-1 my-3"
            v-for="(video,index) in serviceVideos"
            :key="index"
          >
            <video
              width="auto"
              height="auto"
              class="w-100 tw-rounded-lg tw-h-32"
              controls
              :src="video.base64"
            ></video>
            <span class="position-absolute deleteIcon m-3" style="top: 0" @click="deleteVideo(index)">
                <font-awesome-icon icon="trash" color="#ff4d4d" size="sm"/>
              </span>
          </div>

        </div>

      </div>
      <div class="col-12 pt-3">
        <div class="d-flex justify-content-between align-items-center">
          <div class="labelText">
            افزودن ویژگی خدمت
          </div>
          <button @click="increasePropertyCount" class="btn AddReplyBtn text-white">
            <PlusIcon fill="#fc5399" style="width: 40px; height: 40px;"/>
          </button>
        </div>

        <div v-for="(property,idx) in property_count" :key="idx" class="col-md-12 my-3">
          <div class="row">
            <div class="col-md-1 d-flex align-items-center">
              <i @click="decreasePropertyCount(idx)" class="fa fa-minus-circle text-danger tw-cursor-pointer"></i>
            </div>

            <div class=" col-md-3 col-sm-12">
              <label>نام ویژگی</label>
              <input :ref="`Title${idx}`" v-model="property_name" type="text" class=" FormInputs with-border"
                     placeholder="نام ویژگی">
            </div>
            <div class=" col-md-3 col-sm-12 d-flex flex-column justify-content-end   ">
              <div class="d-flex align-items-center justify-content-between">


                <small>قیمت ویژگی</small>
                <div class="d-flex align-items-center gap-2">
                  <small>ویژگی رایگان</small>
                  <input type="checkbox" v-model="property_IsFree" style="width: 20px;height: 20px"
                         class="  form-check">
                </div>
              </div>

              <input maxlength="11" :ref="`Price${idx}`" :disabled="property_IsFree"
                     :class="{DisabledInput:property_IsFree}" v-model="property_price" type="number"
                     class=" FormInputs with-border" placeholder="قیمت ویژگی">
            </div>
            <div class="col-md-3 d-flex align-items-end">
              <button @click="createProperty" type="button" class="bg-pink p-2 rounded text-white tw-cursor-pointer">
                ثبت ویژگی
              </button>
            </div>

          </div>
        </div>
      </div>
      <div class="col-12 pt-3">
        <div class="d-flex justify-content-between align-items-center">
          <div class="labelText">
            ویژگی های شما
          </div>
        </div>
        <div v-for="(service_property,idx) in serviceDetailProp.properties" :key="idx" class="col-md-12 my-3">
          <div class="row">
            <div class="col-md-1 pb-3 d-flex align-items-end">
              <i @click="deleteServiceProperty(service_property.servicePropertyId)"
                 class="fa fa-minus-circle text-danger tw-cursor-pointer"></i>
            </div>
            <div class=" col-md-3 col-sm-12">
              <label>نام ویژگی</label>
              <input v-model="service_property.name" type="text" class="with-border" placeholder="نام ویژگی">
            </div>
            <div class=" col-md-3 col-sm-12 d-flex flex-column justify-content-end   ">
              <div class="d-flex align-items-center justify-content-between">


                <small>قیمت ویژگی</small>
                <div class="d-flex align-items-center gap-2">
                  <small>ویژگی رایگان</small>
                  <input type="checkbox" :checked="service_property.isFree"
                         @click="service_property.isFree=!service_property.isFree" style="width: 20px;height: 20px"
                         class="form-check">
                </div>
              </div>

              <input maxlength="11" :disabled="service_property.isFree" :class="{DisabledInput:service_property.isFree}"
                     v-model="service_property.price" type="number" class="with-border" placeholder="قیمت ویژگی">
            </div>

            <div class="col-md-3 d-flex align-items-center">
              <button @click="UpdateProperty(service_property)" type="button"
                      class="bg-pink p-2 rounded text-white tw-cursor-pointer mt-auto">
                ویرایش ویژگی
              </button>
            </div>
          </div>
        </div>

      </div>
      <div class="col-md-6">
        <div class="row">
          <div v-if="serviceDetailProp.properties.length>0 " class="col-md-6 d-flex flex-column gap-3">
            <div class="tw-w-full d-flex align-items-center justify-content-between ">
              <span>جمع ویژگی ها :</span>
              <span> {{ serviceDetailProp.totalPrice }} <small> تومان</small></span>
            </div>
            <div class="tw-w-full d-flex align-items-center justify-content-between ">
              <span class="text-purple">جمع کل خدمت :</span>
              <span class="text-purple">{{ serviceDetailProp.totalPrice }} <small>تومان</small></span>
            </div>
          </div>
        </div>
      </div>
      <div v-if="serviceDetailProp.discount!==null" class="col-md-12 my-3">
        <div class="labelText">
          ویرایش تخفیف خدمت
        </div>
        <table class="   tw-w-full tw-table-compact tw-shadow   tw-bg-white p-3 rounded tw-border-collapse">
          <thead>
          <tr>
            <th
              class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-right">
              شناسه
            </th>
            <th
              class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-right">
              نوع
            </th>
            <th
              class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-right">
              مقدار
            </th>
            <th
              class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-right">
              تاریخ انقضا
            </th>
            <th
              class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-right">
              عملیات
            </th>
          </tr>
          </thead>
          <tbody>
          <tr>
            <th
              class=" tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-right">
              {{ serviceDetailProp.discount.discountId }}
            </th>

            <td
              class="  tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-right">
              <span v-if="serviceDetailProp.discount.type===1" class="badge pill text-white bg-info">درصدی</span>
              <span v-if="serviceDetailProp.discount.type===2" class="badge pill text-white bg-info">عددی</span>
            </td>

            <td
              class=" tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-right">
              {{ serviceDetailProp.discount.value }}
            </td>

            <td
              class=" tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-right">
              {{ new Date(serviceDetailProp.discount.expireDate).toLocaleDateString('fa-IR') }}
            </td>

            <td
              class=" tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-right">
              <button data-bs-target="#EditDiscount" data-bs-toggle="modal" class="btn btn-info btn-sm text-white">
                ویرایش
              </button>
            </td>
          </tr>

          </tbody>
        </table>

      </div>
      <div class="col-12 py-5 tw-text-left">
        <div class="d-flex align-items-center justify-content-between">
          <button type="button" class="bg-pink p-2 rounded text-white tw-cursor-pointer mt-auto" @click="updateService">
            به روز رسانی خدمت
          </button>
          <button v-if="serviceDetailProp.discount===null" type="button" class="btn btn-info text-white mt-auto px-2"
                  data-bs-toggle="modal" data-bs-target="#AddDiscount">
            ایجاد تخفیف برای خدمت
          </button>


        </div>

      </div>


    </div>


  </div>

</template>

<script>
import UploadFileIcon from "../../components/Icons/UploadFileIcon";
import PlusIcon from "../../components/Icons/PlusIcon";
import SetLocation from "../../components/SetLocation";

export default {
  name: "EditProductTab",
  components: {PlusIcon, UploadFileIcon, SetLocation},
  async fetch() {
    try {
      const res = await this.$repositories.GetAllStates.GetAllStates()
      this.AllStates = res.data.states
    } catch (e) {
      console.log(e)
    }
    this.serviceDetailProp = JSON.parse(JSON.stringify(this.service_details))
    this.serviceDetailProp.medias = []
    try {
      const allCategories =
        await this.$repositories.getAllServicesCategory.getAllServicesCategory();
      this.categories = allCategories.data.serviceCategories;
    } catch (error) {
      console.error(error)
    }
  },
  props: {
    service_details: {
      type: Object,
      required: true
    }
  },
  mounted() {
    this.filteredMedias()
  },
  data() {
    return {
      tag: '',
      property_price: 0,
      property_name: '',
      property_IsFree: false,
      HaveDiscount: false,
      Discount: {
        type: 0,
        value: 0,
        startDate: '',
        expireDate: '',
        refraction: false
      },
      serviceDetailProp: null,
      categories: [],
      center: [35.757539, 51.409968],
      latlng: [35, 51],
      photos: [],
      Videos: [],
      servicePhotos: [],
      serviceVideos: [],
      images_preview: [],
      AllStates: [],
      AllCities: [],
      SelectedCityId: null,
      SelectedStateId: null,
      new_image_preview: [],
      is_first_image: false,
      properties: null,
      property_count: [],


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
          stateId: this.serviceDetailProp.stateId

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
    DeleteDiscount() {
      try {
        const res = this.$repositories.DeleteDiscount.DeleteDiscount({
          id: this.serviceDetailProp.discount.discountId,
        })
        this.$toast.success('تخفیف با موفقیت حذف شد')

        this.$emit('DiscountChanged')
      } catch (e) {
        console.log(e)
      }
    },
    UpdateDiscount() {
      if (this.serviceDetailProp.discount.type === 1 && this.serviceDetailProp.discount.value >= 99) {
        this.$toast.error('مقدار تخفیف درصدی نمیتواند بیشتر از 99 درصد باشد')
      } else if (this.serviceDetailProp.discount.type === 2 && this.serviceDetailProp.discount.value >= this.serviceDetailProp.totalPrice) {
        this.$toast.error('مقدار تخفیف عددی نمیتواند بیشتر از مبلغ کل باشد')
      } else {
        try {
          const res = this.$repositories.UpdateDiscount.UpdateDiscount({
            discountId: this.serviceDetailProp.discount.discountId,
            expireDate: this.serviceDetailProp.discount.expireDate,
            type: this.serviceDetailProp.discount.type,
            value: parseInt(this.serviceDetailProp.discount.value),
            refraction: false,
            servicePackId: this.serviceDetailProp.discount.servicePackId,
            startDate: ""

          })
          this.$toast.success('تخفیف با موفقیت بروزرسانی شد')
          this.$emit('DiscountChanged')
        } catch (e) {
          console.log(e)
        }
      }
    },
    AddDiscount() {
      if (this.Discount.type === 1 && this.Discount.value >= 99) {
        this.$toast.error('مقدار تخفیف درصدی نمیتواند بیشتر از 99 درصد باشد')
      } else if (this.Discount.type === 2 && this.Discount.value >= this.serviceDetailProp.totalPrice) {
        this.$toast.error('مقدار تخفیف عددی نمیتواند بیشتر از مبلغ کل باشد')
      } else {

        try {
          const res = this.$repositories.CreateDiscount.CreateDiscount({
            expireDate: this.Discount.expireDate,
            type: this.Discount.type,
            value: parseInt(this.Discount.value),
            refraction: false,
            servicePackId: this.service_details.servicePackId,
            startDate: ""

          })
          this.$toast.success('تخفیف برای خدمت ساخته شد')
          this.Discount = {
            type: 0,
            value: 0,
            startDate: '',
            expireDate: '',
            refraction: false
          }
          this.$emit('DiscountChanged')
        } catch (e) {
          console.log(e)
        }
      }
    },
    SetLcoation(lat, lang) {
      this.serviceDetailProp.latitude = lat
      this.serviceDetailProp.longitude = lang
    },
    async createProperty() {
      if (this.property_name === '') {
        this.$toast.error("لطفا نام ویژگی را وارد کنید");
      } else {
        if (this.property_IsFree) {
          this.property_price = 0
        }
        this.$nuxt.$loading.start();
        try {
          await this.$repositories.createAServiceProperty.createAServiceProperty({
            serviceId: this.serviceDetailProp.servicePackId,
            name: this.property_name,
            price: this.property_price,
            isFree: this.property_IsFree
          })
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
          this.$toast.success("ویژگی با موفقیت ثبت شد");
          this.$nuxt.refresh();
        } catch (error) {
          console.log(error);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
        }
      }
    },
    async createTag() {
      if (this.tag === '') {
        this.$toast.error("لطفا نام تگ را وارد کنید");
      } else {
        this.$nuxt.$loading.start();
        try {
          await this.$repositories.createAServiceTag.createAServiceTag({
            objectId: this.serviceDetailProp.servicePackId,
            title: this.tag,
            type: 1,
          })
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
          this.$toast.success("هشتگ با موفقیت ثبت شد");
          this.$nuxt.refresh();
          this.tag = '';
        } catch (error) {
          console.log(error);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
        }
      }
    },
    decreasePropertyCount(index) {
      this.property_count.splice(index, 1)
    },
    increasePropertyCount() {
      if (this.property_count.length > 0) {
        this.$toast.error('یک ویژگی در آن واحد میتوانید اضافه کنید')
      } else {

        this.property_count.push('property')
      }
    },
    UpdateProperty(item) {
      try {
        const res = this.$repositories.updateAServiceProperty.updateAServiceProperty({

          servicePropertyId: item.servicePropertyId,
          serviceId: item.serviceId,
          name: item.name,
          description: item.description,
          serviceCategoryId: item.serviceCategoryId,
          price: item.price,
          isFree: item.isFree,
          createDate: item.createDate
        })
        this.$emit('RefetchService')
        this.$toast.success('ویژگی با موفقیت ویرایش شد')
      } catch (e) {
        console.log(e)
      }
    },
    deleteServiceProperty(id) {
      this.$repositories.deleteAServiceProperty.deleteAServiceProperty({
        id
      })
      this.$toast.success("ویژگی مورد نظر با موفقیت حذف شد");
      this.$nuxt.refresh();
    },
    deleteServiceTag(id) {
      this.$repositories.deleteAServiceTag.deleteAServiceTag({
        id
      })
      this.$toast.success("هشتگ مورد نظر با موفقیت حذف شد");
      this.$nuxt.refresh();
    },
    deleteVideo(item) {
      const idx = this.BaseVideos.findIndex((e) => e === item);

      this.BaseVideos.splice(idx, 1);

      this.Videos.splice(idx, 1);

    },

    filteredMedias() {
      this.service_details.medias.filter(item => {
        if (item.priority === 1 || item.priority === 2) {
          this.images_preview.push(item)
        }
        if (item.priority === 3) {
          this.serviceVideos.push(item)
        }
      })
    },
    deleteNewImage(index) {
      this.new_image_preview.splice(index, 1)
      this.serviceDetailProp.medias.splice(index, 1)

    },
    deleteImage(item, index) {
      this.serviceDetailProp.medias.push(
        {
          base64: item.base64,
          priority: 0
        }
      )
      if (this.images_preview[index].priority === 1) {
        this.is_first_image = true
      }
      this.images_preview.splice(index, 1)
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
    onFileChangeVideo() {
      const that = this;
      const f = [];
      that.Uploaded = true;
      Array.prototype.forEach.call(this.$refs.VideoFile.files, (element) => {
        if (element.duration > 120000) {
          this.$toast.error('زمان ویدئو بارگذاری شده باید کمتر از 2 دقیقه باشد')
        } else {
          f.push(element)
        }
      });
      f.forEach((element) => {
        if (that.serviceVideos.length === 0 && that.serviceVideos.length === 0) {
          this.serviceVideos.push({
            base64: URL.createObjectURL(element),
            priority: 3
          });
        } else {

          that.serviceVideos.push({
            base64: URL.createObjectURL(element),
            priority: 3
          });
        }

        const reader = new FileReader();
        reader.onload = (function (theFile) {
          return function () {
            const binaryData = reader.result;
            that.serviceDetailProp.medias.push({
                base64: window.btoa(binaryData),
                priority: 3
              }
            );
          };
        })(f);

        reader.readAsBinaryString(element);
        // this.BaseVideos.push(URL.createObjectURL(element));
        // const reader = new FileReader();
        // reader.onload = (function (theFile) {
        //   return function () {
        //     const binaryData = reader.result;
        //     that.Videos.push(window.btoa(binaryData));
        //   };
        // })(f);
        // reader.readAsBinaryString(element);
      });
    },
    onFileChange(e) {
      this.is_first_image = false
      const that = this;
      const f = [];
      that.Uploaded = true;
      Array.prototype.forEach.call(this.$refs.picture_file.files, (element) => {
        if (this.new_image_preview.length + this.images_preview.length >= 10) {
          this.$toast.error('بیشتر از 10 عکس نمیتوانید بارگذاری کنید')
        } else if (element.size > 512000) {
          this.$toast.error('اندازه عکس نمیتواند بیشتر از 512 کیلوبایت باشد')
        } else {
          f.push(element);
        }
      });
      f.forEach((element) => {

        if (that.images_preview.length === 0 && that.new_image_preview.length === 0) {

          this.is_first_image = true
          this.new_image_preview.push({
            base64: URL.createObjectURL(element),
            priority: 1
          });
        } else {

          that.new_image_preview.push({
            base64: URL.createObjectURL(element),
            priority: 2
          });
        }

        const reader = new FileReader();
        reader.onload = (function (theFile) {
          return function () {
            const binaryData = reader.result;
            if (that.is_first_image) {

              that.serviceDetailProp.medias.push({
                  base64: window.btoa(binaryData),
                  priority: 1
                }
              );
            } else {
              that.serviceDetailProp.medias.push({
                  base64: window.btoa(binaryData),
                  priority: 2
                }
              );
            }

          };
        })(f);

        reader.readAsBinaryString(element);
      });
    },

    async updateService() {
      if (this.serviceDetailProp.medias.length === 0 && this.images_preview.length === 0) {

        this.$toast.error('حداقل یک عکس بارگذاری کنید')
      } else {
        this.$nuxt.$loading.start();
        let result = 0
        if (this.properties !== null) {
          this.properties.forEach((element) => {
            result += parseInt(element.price)
          })

        }
        try {
          const res = await this.$repositories.updateAService.updateAService(this.serviceDetailProp);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
          this.$nuxt.refresh();
          this.$toast.success("خدمت با موفقیت ویرایش شد");
          this.$router.push({path: `/Products/Upgrade/${res.data.servicePackId}`, query: {active_tab: 'preview'}});


        } catch (error) {
          console.log(error);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
        }
      }

    }

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

.DisabledInput {
  @apply tw-bg-stone-400

}

.vpd-input-group input {
  background-color: #faf7ff !important;
}
</style>
