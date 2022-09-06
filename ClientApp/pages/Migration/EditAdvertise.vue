<template>
  <div class="container mcontainer">
    <div
      class="container-fluid px-0 rounded"
      v-if="!$fetchState.pending"
    >
      <div class="row">

          <div class="col-md-12 p-4">
            <div class="tw-w-full bg-white p-3 d-flex align-items-center justify-content-between gap-3  rounded ">
              <div class="d-flex align-items-center gap-2">
                <img src="/girl-icon-ads-edit.png" class="tw-w-[7rem] tw-h-20" alt="">
                <div class="d-flex align-items-center flex-column">
                  <h1 class="text-purple h6">ویرایش آگهی</h1>
                  <strong class="text-pink">Edit AdvertiseMents</strong>
                </div>
              </div>
            </div>
          </div>

        <div class="col-md-12">
          <div class="row p-3 px-5">
            <div class="col-md-6">
              <div class="labelText">عنوان آگهی*</div>
              <div class="my-3">
                <input
                  class="border w-100 rounded p-1"
                  maxlength="50"
                  type="text"
                  v-model.trim="AdDetail.title"
                  placeholder="مثال: لباس شب مجلسی"
                />
              </div>
            </div>
            <div class="col-md-6">
              <div class="labelText">وضعیت کالا*</div>
              <div class="my-3">
                <select v-model="AdDetail.status" class="form-control w-100">
                  <option :value="1">نو</option>
                  <option :value="2">دست دوم</option>
                </select>
              </div>
            </div>
            <div class="col-md-12">
              <div class="labelText">شماره موبایل*</div>
              <input type="tel" placeholder="با صفر اول وارد کنید" class="form-control with-border" v-model="AdDetail.phoneNumber">
            </div>

            <div class="col-md-12 my-2">
              <div class="labelText">عکس های فعلی آگهی</div>
              <div v-if="AdDetail.photos.length>0" class="row">
                <div
                  class="py-3 col-lg-1 col-md-3 px-1 tw-h-40 d-flex photos position-relative"
                  v-for="(image,index) in AdDetail.photos"
                  :key="index"
                >
                  <img class="tw-rounded-lg tw-h-32" width="100%"
                       :src="`https://banooclubapi.simagar.com/${image.base64}`"
                       style="object-fit: contain" :class="{BorderGreen:index===0}"/>
                  <span class="position-absolute deleteIcon m-3" style="top: 15px" @click="DeleteImage(image,index)">
                <font-awesome-icon icon="trash" color="#ff4d4d" size="sm"/>
              </span>
                </div>
              </div>
              <div v-else class="row my-2">
                <span>این آگهی عکس ندارد</span>
              </div>
            </div>
            <div class="col-md-6">
              <div class="labelText">عکس اصلی آگهی</div>
              <div class="d-flex flex-row gap-3 my-3">
                <div
                  class="border rounded d-flex text-center justify-content-center align-items-center InputUiBox"
                  @click="callInputMethodMainImage"
                >
                  <input
                    type="file"
                    accept="image/*"
                    class="form-control-file MainImage d-none"
                    id="MainImage"
                    @change="onFileChangeMainImage"
                  />
                  <UploadIcon class="tw-fill-[#b44aff]"></UploadIcon>
                </div>
                <div id="main">
                  <img
                    width="70px"
                    height="70px"
                    v-if="url"
                    :src="url"
                    style="object-fit: contain"
                    :key="url"
                  />
                </div>
              </div>
            </div>
            <div class="col-md-6">
              <div class="labelText">عکس های فرعی آگهی</div>
              <div class="d-flex flex-row gap-3 my-3">
                <div
                  class="border rounded d-flex text-center justify-content-center align-items-center InputUiBox"
                  @click="callInputMethodSubImage"
                >
                  <input
                    type="file"
                    accept="image/*"
                    multiple="multiple"
                    class="form-control-file SubImage d-none"
                    id="SubImage"
                    @change="onFileChangeSubImage"
                  />
                  <UploadIcon class="tw-fill-[#b44aff]"></UploadIcon>
                </div>
                <div id="preview" v-if="subUrl">
                  <img
                    class="img-fluid"
                    v-for="item in subUrl"
                    style="object-fit: contain"
                    :src="item"
                    :key="item"
                  />
                </div>
              </div>
            </div>
            <div class="col-md-6 my-3">
              <v-select
                @input="GetCity"
                :options="AllStates"
                label="name"
                dir="rtl"
                class="selectWidth w-100"
                placeholder="استان*"
                v-model="AdDetail.stateId"
                :reduce="(name) => name.stateId"

              ></v-select>
            </div>
            <div class="col-md-6 my-3">
              <v-select
                :options="AllCities"
                label="name"
                dir="rtl"
                class="selectWidth w-100"
                placeholder="شهر*"
                v-model="SelectedCityId"
                :reduce="(name) => name.cityId"

              ></v-select>
            </div>
            <div class="col-md-6">
              <div class="labelText">هشتگ های آگهی (با علامت , جدا شود)</div>
              <div class="my-3">
                <input
                  v-model="AdDetail.tag"
                  type="text"
                  class="form-control"
                  placeholder="لباس,تیشرت,شلوار..."
                />
              </div>
            </div>
            <div class="col-md-6">
              <div class="labelText">قیمت (تومان)*</div>
              <div class="my-3">
                <input
                  class="rounded border p-1 w-100"
                  type="number"
                  v-model.trim="AdDetail.price"
                />
              </div>
            </div>
            <div class="col-md-12">
              <div class="labelText">توضیحات آگهی</div>
              <div class="my-3">
              <textarea
                class="border rounded form-control"
                rows="1"
                placeholder="توضیحات آگهی"
                v-model.trim="AdDetail.description"
              ></textarea>
              </div>
            </div>
            <div class="col-md-12 my-2">

              <div class="labelText mb-2">با کالایی دیگر معاوضه میکنم*</div>

              <label class="switch">
                <input
                  type="checkbox"
                  id="togBtn123"
                  v-model="AdDetail.exchangeability"
                  :checked="AdDetail.exchangeability"
                />
                <div class="slider round">
                  <span class="on">دارد</span>
                  <span class="off">ندارد</span>
                </div>
              </label>

            </div>
            <div class="col-md-12">
              <div class="labelText">موقعیت حدودی روی نقشه*</div>
              <div class="my-3" id="map-wrap" >
                <client-only>
                  <SetLocation @getGeoLocation="SetLocation"
                               :defaultMarkerGeoLoc="[AdDetail.latitude,AdDetail.longitude]"
                               :defaultGeoLoc="[AdDetail.latitude,AdDetail.longitude]"></SetLocation>
                </client-only>
              </div>
            </div>

            <div class="col-md-12 d-flex align-items-center justify-content-start">
              <div class=" text-center">
                <button
                  type="button"
                  class="btn AddReplyBtn"
                  @click="UpdateAdvertise"
                >
                  ثبت آگهی
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>


  </div>

</template>

<script>

import SetLocation from '../../components/SetLocation'
import UploadIcon from "@/components/Icons/UploadIcon";
export default {
  name: "CreateAdvertise",
  layout: "PoshtebamPlusLayout",
  components: {SetLocation,UploadIcon},
  fetchOnServer() {
    return true;
  },
  async fetch() {
    // Get categories
    const allCategories =
      await this.$repositories.getAllAdsCategory.getAllAdsCategory();
    this.categories = allCategories.data.adsCategories;

    try {
      const res = await this.$repositories.GetAllStates.GetAllStates()
      this.AllStates = res.data.states
    } catch (e) {
      console.log(e)
    }

    try {
      const res = await this.$repositories.getAnAd.getAnAd({
        id: this.$route.query.adId
      })
      this.AdDetail = res.data


    } catch (e) {
      console.log(e)
    }


  },
  data() {
    return {
      AdDetail: null,
      categories: [],
      title: "",
      description: "",
      url: null,
      subUrl: [],
      price: 0,
      phone: null,
      mainImage: "",
      subImage: "",
      photos: [],
      categoryId: null,
      categoryId2: null,
      categoryId3: null,
      Status: 0,
      Tags: "",
      AllCities: [],
      SelectedCityId: 0,
      SelectedStateId: 0,
      AllStates: [],
      DeleteImageList: []
    };
  },
  head() {
    return {

      title: 'ویرایش آگهی',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'ویرایش آگهی',
        },
      ],
    };
  },
  methods: {
    SetLocation(lat, lang) {
      this.AdDetail.latitude = lat
      this.AdDetail.longitude = lang
    },
    DeleteImage(image, Idx) {

      let tmpImage = {
        base64: image.base64,
        priority: 0
      }
      this.DeleteImageList.push(tmpImage)
      // tmpImage.base64 = ''
      this.AdDetail.photos.splice(Idx, 1)
    },

    async GetCity() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start()
      })
      try {
        const res = await this.$repositories.GetAllCities.GetAllCities({
          pageNumber: 1,
          count: 500,
          stateId: this.AdDetail.stateId

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
    callInputMethodMainImage() {
      document.querySelector(".MainImage").click();
    },
    callInputMethodSubImage() {
      document.querySelector(".SubImage").click();
    },
    onFileChangeMainImage(e) {
      const file = e.target.files[0];
      this.url = URL.createObjectURL(file);
      const that = this;
      const reader = new FileReader();
      reader.onload = (function (theFile) {
        return function () {
          const binaryData = reader.result;
          that.mainImage = window.btoa(binaryData);
          that.photos.push({
            base64: that.mainImage,
            priority: 1,
          });
        };
      })(file);
      reader.readAsBinaryString(file);
    },
    onFileChangeSubImage(e) {
      const file = e.target.files[0];
      this.subUrl.push(URL.createObjectURL(file));
      const that = this;
      const reader = new FileReader();
      reader.onload = (function (theFile) {
        return function () {
          const binaryData = reader.result;
          that.subImage = window.btoa(binaryData);
          that.photos.push({
            base64: that.subImage,
            priority: 2,
          });
        };
      })(file);
      reader.readAsBinaryString(file);
    },
    async UpdateAdvertise() {

      if (this.AdDetail.categoryId === null) {
        this.$toast.error("لطفا دسته بندی ر ا انتخاب کنید");
      } else if (this.title === null) {
        this.$toast.error("لطفا عنوان آگهی را مشخص کنید");
      } else if (this.AdDetail.price === 0) {
        this.$toast.error("لطفا قیمت را مشخص کنید");
      } else if (!this.AdDetail.latitude) {
        this.$toast.error("لطفا موقعیت مکانی آگهی را مشخص کنید");
      } else {
        this.$nextTick(() => {
          this.$nuxt.$loading.start();
        })

        try {
          this.DeleteImageList.forEach((item) => {
            this.photos.push(item)
          })

          await this.$repositories.UpdateAd.UpdateAd({
            adsId: this.AdDetail.adsId,
            title: this.AdDetail.title,
            categoryId: this.AdDetail.categoryId,
            price: parseInt(this.AdDetail.price),
            expirationDate: this.AdDetail.expirationDate,
            createDate: new Date(Date.now()),
            status: this.AdDetail.status,
            updateDate: new Date(Date.now()),
            tag: this.AdDetail.tag,
            condition: this.AdDetail.condition,
            cityId: this.SelectedCityId !== 0 ? this.SelectedCityId : this.AdDetail.cityId,
            stateId: this.AdDetail.stateId,
            userId: this.$auth.user.userInfo.userId,
            description: this.AdDetail.description,
            latitude: this.AdDetail.latitude,
            longitude: this.AdDetail.longitude,
            photos: this.photos,
          });
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
          this.$toast.success("آگهی با موفقیت بروزرسانی شد");
          this.$router.push({path: "/Migration/MyAdvertise"});
        } catch (error) {
          console.log(error);
        }
      }
    },
  },
};
</script>

<style scoped>
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

.AddReplyBtn {
  background-color: #d277ff;
  color: white;
  transition: 0.2s ease;
}

.centerContent {
  margin: 0 auto;
  padding-right: 2rem;
}

input::placeholder {
  font-size: 12px;
  color: #cccccc;
}

.labelText {
  color: #545454;
  font-size: 14px;
  cursor: pointer;
}

.InputUiBox {

  width: 100px;
  height: 100px;
  cursor: pointer;
}

#preview img {
  width: 100px;
  height: 100px;
  margin: 0 8px;
  border-radius: 8px;
}

#main img {
  width: 350px;
  height: 350px;
  margin: 0 8px;
  border-radius: 8px;
}
</style>
