<template>
  <div class=" mcontainer px-2">
    <div class="row" style="padding-left: 0">
      <h1 class="tw-text-2xl tw-font-semibold pb-3">افزودن خدمت</h1>
      <div class="row" style="padding-left: 0">
        <div class="col-md-6 col-sm-12 pt-3" style="padding-left: 0">
          <label>نام خدمت *</label>
          <input v-model="service_title" type="text" class="with-border" placeholder="نام خدمت"
                 :class="{BorderRed:service_title==='',BorderGreen:service_title!==''}">
        </div>
        <div class="col-md-6 col-sm-12 pt-3" style="padding-left: 0">
          <label>دسته بندی خدمت *</label>
          <select v-model="service_category" class="form-select" aria-label="Default select example"
                  :class="{BorderRed:service_category===null,BorderGreen:service_category!==null}">
            <option :value="null">دسته بندی خدمت</option>
            <option v-for="(service_category,idx) in categories" :key="idx"  :value="service_category.serviceCategoryId">
              {{ service_category.title }}
            </option>
          </select>
        </div>
        <div class="col-12 py-3" style="padding-left: 0">
          <label>توضیحات</label>
          <textarea
            v-model="service_desc"
            row="100"
            style="color: #808080;"
            class="form-control border rounded w-100 with-border"
            placeholder="توضیحات خدمت"
            id="description"
          ></textarea>
        </div>
        <div class="col-12 py-3" style="padding-left: 0">
          <label>آدرس</label>
          <textarea
            v-model="service_address"
            style="color: #808080;"
            row="100"
            class="form-control border rounded w-100"
            placeholder="آدرس خدمت"
            id="address"
          ></textarea>
        </div>
        <div class="col-12 py-3" style="padding-left: 0">
          <div class="labelText">موقعیت روی نقشه</div>
          <div class="my-3" id="map-wrap" style="height: 50vh">
            <client-only>
              <l-map :zoom="17" :center="center" @click="addMarker">
                <l-tile-layer
                  url="http://{s}.tile.osm.org/{z}/{x}/{y}.png"
                ></l-tile-layer>
                <l-marker :lat-lng="latlng"></l-marker>
              </l-map>
            </client-only>
          </div>
        </div>
        <div class="col-md-6 col-sm-12 pt-3" style="padding-left: 0">
          <label>ظرفیت</label>
          <input maxlength="11" v-model="Qty" type="number" class="with-border" placeholder="ظرفیت خدمت">
        </div>
        <div class="col-md-6 col-sm-12 pt-3" style="padding-left: 0">
          <label>شماره تلفن 1</label>
          <input maxlength="11" v-model="phone_number1" type="number" class="with-border" placeholder="شماره تلفن">
        </div>
        <div class="col-md-6 col-sm-12 pt-3" style="padding-left: 0">
          <label>شماره تلفن 2</label>
          <input maxlength="11" v-model="phone_number2" type="number" class="with-border" placeholder="شماره تلفن">
        </div>
        <div class="col-md-6 col-sm-12 pt-3" style="padding-left: 0">
          <label>شماره مستقیم تماس *</label>
          <input maxlength="11" v-model="mobile" type="number" class="with-border" placeholder="شماره همراه"
                 :class="{BorderRed:mobile===0,BorderGreen:mobile!==0}">
        </div>
        <div class="col-md-6 col-sm-12 pt-3" style="padding-left: 0">
          <label>ایمیل</label>
          <input v-model="email" type="email" class="with-border" placeholder="ایمیل">
        </div>
        <div class="col-md-6 col-sm-12 pt-3" style="padding-left: 0">
          <div class="labelText">
            آدرس وب سایت
          </div>
          <div class="my-3">
            <input type="text" v-model="web_address" class="with-border" placeholder="www.example.com">
          </div>
        </div>
        <div class="col-md-6 col-sm-12 pt-3" style="padding-left: 0">
          <label class="mb-3">تاریخ انقضاء خدمت</label>
          <client-only>
            <date-picker
              format="YYYY-MM-DD"
              display-format="jYYYY-jMM-jDD"
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
            <input v-on:keyup.enter="addTags" v-model="tag" type="text" class="with-border"
                   placeholder="ناخن،مو،رنگ...">
            <div class="d-flex">
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
            انتخاب عکس نمونه کار (اولین عکس به عنوان عکس اصلی خدمت بارگذاری می شود - حداکثر 10 عکس می توانید بارگذاری
            کنید)
          </div>
          <div class="d-flex row">
            <div class="py-3 col-lg-2 col-md-3 px-3">
              <div @click="uploadNewPicture"
                   class="tw-bg-gray-100 tw-border-2 tw-border-dashed tw-flex tw-flex-col tw-h-32 tw-items-center tw-justify-center tw-relative tw-rounded-lg">
                <UploadFileIcon style="width: 48px; height: 48px;"/>
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
              <div @click="uploadNewVideo" class="tw-bg-gray-100
              tw-border-2 tw-border-dashed tw-flex tw-flex-col tw-h-32 tw-items-center
              tw-justify-center tw-relative tw-rounded-lg">
                <UploadFileIcon style="width: 48px; height: 48px;"/>
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
        <div class="col-12 pt-3" style="padding-left: 0">
          <div class="d-flex justify-content-between align-items-center">
            <div class="labelText">
              افزودن ویژگی خدمت (بعد از تکمیل اطلاعات ویژگی ها را ذخیره کنید)
            </div>
            <button @click="increasePropertyCount" class="btn AddReplyBtn text-white">
              <PlusIcon fill="#2563eb" style="width: 40px; height: 40px;"/>
            </button>
          </div>

          <div class="d-flex flex-column flex-md-row justify-content-between align-items-center px-lg-5"
               v-for="(property,idx) in property_count" :key="idx">
            <span class="deleteIcon m-3 pt-3" style="top: 0" @click="decreasePropertyCount(idx)">
              <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6 tw-text-red-600" viewBox="0 0 20 20"
                   fill="currentColor">
                <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM7 9a1 1 0 000 2h6a1 1 0 100-2H7z"
                      clip-rule="evenodd"/>
              </svg>
            </span>
            <div class="pt-3 col-md-6 col-sm-12">
              <label>نام ویژگی</label>
              <input :ref="`Title${idx}`" type="text" class="with-border" placeholder="نام ویژگی">
            </div>
            <div class="pt-3 col-md-6 col-sm-12">
              <label>قیمت ویژگی</label>
              <input maxlength="11" :ref="`Price${idx}`" type="number" class="with-border" placeholder="قیمت ویژگی">
            </div>
          </div>
          <div class="row tw-text-left" v-if="property_count.length>0">
            <div class="col-12 pt-3">
              <button @click="addProperty()" type="button" class="button mt-auto px-2">
                ذخیره ویژگی ها
              </button>
            </div>
          </div>
        </div>
        <div class="tw-text-left col-12 pt-5">
          <button type="button" class="button mt-auto px-2" @click="submitCreateService">
            ثبت نهایی
          </button>
        </div>
      </div>


    </div>


  </div>


</template>

<script>
import UploadFileIcon from "../../components/Icons/UploadFileIcon";
import PlusIcon from "../../components/Icons/PlusIcon";

export default {
  name: "AddProduct",
  components: {PlusIcon, UploadFileIcon},
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
      const allCategories =
        await this.$repositories.getAllServicesCategory.getAllServicesCategory();
      this.categories = allCategories.data.serviceCategories;
    } catch (error) {
      console.error(error)
    }
    // Get categories

  },

  data() {
    return {
      service_title: '',
      service_category: null,
      service_desc: '',
      service_address: '',
      phone_number1: 0,
      phone_number2: 0,
      Qty:0,
      mobile: 0,
      email: '',
      web_address: '',
      BaseImgUrls: [],
      BaseVideos: [],
      categories: [],
      expiration_date: "",
      center: [35.757539, 51.409968],
      latlng: [35, 51],
      photos: [],
      Videos: [],
      property_count: [],
      tags: [],
      tag: '',
      render_tags: null,
      properties: null,
      totalPrice: 0
    };
  },
  methods: {
    removeTag(index) {
      this.tags.splice(index, 1)
    },
    addTags() {
      if(this.tag === ''){
        this.$toast.error('هشتگ مورد نظر را وارد کنید')
      }
      else{
        this.tags.push(this.tag)
        this.tag = ''
      }
    },

    addProperty() {
      let tmplist = []
      let tmpitem = {
        name: '',
        price: 0
      }
      this.property_count.forEach((item, idx) => {
        tmpitem.name = this.$refs[`Title${idx}`][0].value
        tmpitem.price = this.$refs[`Price${idx}`][0].value
        const clone = {...tmpitem}
        tmplist.push(clone)
        tmpitem.name = ''
        tmpitem.price = 0
      })
      this.properties = tmplist
      this.$toast.success("ویژگی ها با موفقیت ثبت شدند");
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
      } else if (this.mobile === 0) {
        this.$toast.error("لطفا شماره مستقیم تماس خود را وارد کنید");
      } else if (this.mobile !== 0 && this.mobile.length>11) {
        this.$toast.error("فرمت وارد شده برای شماره مستقیم تماس درست نیست");
      } else if (this. phone_number1 !== 0 && this. phone_number1.length>11) {
        this.$toast.error("فرمت وارد شده برای شماره تلفن اول درست نیست");
      } else if (this. phone_number2 !== 0 && this. phone_number2.length>11) {
        this.$toast.error("فرمت وارد شده برای شماره تلفن دوم درست نیست");
      } else if (this.email !== '' && !this.email.includes('@')) {
        this.$toast.error("ایمیل وارد شده معتبر نیست");
      } else if (this.photos.length === 0) {
        this.$toast.error("حداقل یک عکس به عنوان عکس اصلی وارد کنید");
      }else if (this.properties === null) {
        this.$toast.error("حداقل یک ویژگی وارد کنید");
      } else {
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

          let result = 0
          this.properties.forEach((element) => {
            result += parseInt(element.price)
          })
          this.totalPrice = result
            const res = await this.$repositories.createAService.createAService({
              title: this.service_title,
              userId:0,
              serviceCategoryId: this.service_category,
              description: this.service_desc,
              seoTitle: '',
              seoDescription: '',
              address: this.service_address,
              latitude: this.latlng.lat,
              longitude: this.latlng.lng,
              webAddress: this.web_address,
              phoneNumber1: this.phone_number1,
              phoneNumber2: this.phone_number2,
              mobile: this.mobile,
              quantity:parseInt(this.Qty),
              email: this.email,
              fireDate: '2022-06-25T12:04:09.204Z',
              specified: false,
              specifiedWithExpireDate: false,
              specifiedExpireDateTime: null,
              totalPrice: this.totalPrice,
              expireDate: this.expiration_date,
              properties:this.properties,
              medias: tmpMedias,
              tags:tmptags
            });
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false;
            this.$toast.success("ثبت خدمت با موفقیت انجام شد.");
            this.$router.push({path: `/Products/Upgrade/${res.data}`, query: { active_tab: 'preview' }});
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
</style>

