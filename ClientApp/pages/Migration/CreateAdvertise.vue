<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="container mcontainer bg-white">
    <div
      class="container-fluid px-0 rounded"

    >
      <div class="row">
        <div class="col-md-12 p-4">
          <div class="tw-w-full bg-white p-3 d-flex align-items-center justify-content-between gap-3  rounded ">
            <div class="d-flex align-items-center gap-2">
              <img src="/girl-icon-ads-afzodan.png" class="tw-w-[7rem] tw-h-20" alt="">
              <div class="d-flex align-items-center flex-column">
                <h1 class="text-purple h6">ثبت آگهی</h1>
                <strong class="text-pink">Add AdvertiseMents</strong>
              </div>
            </div>
          </div>
        </div>
        <div class="col-md-12">
          <div class="row p-3 px-5">
            <div class="col-md-12">
              <div class="row">
                <div class="col-md-4">
                  <div class="labelText">دسته بندی*</div>

                  <div class="col-md-12 px-0">
                    <div class="my-3">
                      <select class="form-control" v-model="categoryId">
                        <option
                          v-for="(item, index) in categories"
                          :value="item.adsCategoryId"
                          :key="index"
                        >
                          {{ item.name }}
                        </option>
                      </select>
                    </div>
                  </div>

                </div>
              </div>
            </div>
            <div class="col-md-6">
              <div class="labelText">عنوان آگهی*</div>
              <div class="my-3">
                <input
                  class="border BgInput w-100 rounded p-1"
                  maxlength="50"
                  type="text"
                  v-model.trim="title"
                  placeholder="مثال: لباس شب مجلسی"
                />
              </div>
            </div>
            <div class="col-md-6">
              <div class="labelText">وضعیت کالا*</div>
              <div class="my-3">
                <select v-model="Status" class="form-control BgInput w-100">
                  <option :value="1">نو</option>
                  <option :value="2">دست دوم</option>
                </select>
              </div>
            </div>
            <div class="col-md-12 my-3">
              <div class="labelText">شماره موبایل*</div>
              <input type="tel" placeholder="با صفر اول وارد کنید" class="form-control BgInput with-border" v-model="PhoneNumber">
            </div>





            <div class="col-md-6">
              <div class="labelText">عکس اصلی آگهی</div>
              <div class="d-flex flex-row gap-3 my-3">
                <div
                  class="border rounded text-center d-flex justify-content-center align-items-center InputUiBox"
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
                <div  v-if="url" id="main" class="position-relative">
                  <i class="fas fa-times tw-cursor-pointer text-danger position-absolute top-0 end-0" @click="RemoveMainImage" ></i>
                  <img
                    class="img-fluid tw-shadow tw-rounded"

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
                  class="border rounded text-center d-flex justify-content-center align-items-center InputUiBox"
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
                <div  v-for="(item,idx) in subUrl"   :key="item" id="preview" class="position-relative" v-if="subUrl">
                  <i class="fas fa-times tw-cursor-pointer  text-danger position-absolute top-0 end-0" @click="RemoveOtherImages(idx)" ></i>
                  <img
                    class="img-fluid tw-shadow tw-rounded"
                    style="object-fit: contain"
                    :src="item"

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
            <div class="col-md-6">
              <div class="labelText">هشتگ های آگهی (با علامت , جدا شود)</div>
              <div class="my-3">
                <input
                  v-model="Tags"
                  type="text"
                  class="form-control BgInput with-border"
                  placeholder="لباس,تیشرت,شلوار..."
                />
              </div>
            </div>
            <div class="col-md-6">
              <div class="labelText">قیمت (تومان)*</div>
              <div class="my-3">
                <input
                  class="rounded BgInput border p-1 w-100"
                  type="text"
                  v-model="formattedValuePrice"
                />
              </div>
            </div>
            <div class="col-md-12">
              <div class="labelText">توضیحات آگهی</div>
              <div class="my-3">
              <textarea
                class="border BgInput rounded form-control"
                rows="1"
                placeholder="در مورد ویژگی های کالا توضیح دهید"
                v-model.trim="description"
              ></textarea>
              </div>
            </div>
            <div class="col-md-12 my-2">

              <div class="labelText mb-2">با کالایی دیگر معاوضه میکنم*</div>

              <label class="switch">
                <input

                  type="checkbox"
                  id="togBtn123"
                  v-model="ExchangeAbility"
                  :checked="ExchangeAbility"
                />
                <div class="slider round">
                  <span class="on">بله</span>
                  <span class="off">خیر</span>
                </div>
              </label>

            </div>

            <div class="col-md-12">
              <div class="labelText">موقعیت حدودی روی نقشه*</div>
              <div class="my-3" id="map-wrap" >
                <client-only>
               <SetLocation @getGeoLocation="SetLocation"></SetLocation>
                </client-only>
              </div>
            </div>
            <div class="col-md-12 d-flex align-items-center justify-content-start">
              <div class=" text-center">
                <button
                  type="button"
                  class="btn AddReplyBtn"
                  @click="submitCreateAdvertise"
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
import UploadIcon from "@/components/Icons/UploadIcon";
export default {
  name: "CreateAdvertise",
  layout: "PoshtebamPlusLayout",
  components:{UploadIcon},
  fetchOnServer() {
    return true;
  },
  computed:{
    formattedValuePrice: {
      get: function () {
        return this.price;
      },
      set: function (newValue) {
        // This setter is getting number, replace all commas with empty str
        // Then start to separate numbers with ',' from beginning to prevent
        // from data corruption
        if (newValue) {
          this.price = newValue
            .toString()
            .replaceAll(",", "")
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
          // Remove all characters that are NOT number
          this.price = this.price.replace(
            /[a-zA-Z+*!@#$%^&*()_;:'"|<>/?{}\u0600-\u06EC/\-/\.]/g,
            ""
          );
        } else if (!newValue || this.newValue === "") {
          this.price = null;
        }
      },
    },
  },
  async fetch() {
    // Get categories
    const allCategories =
      await this.$repositories.getAllAdsCategory.getAllAdsCategory();
    this.categories = allCategories.data.adsCategories;

    const res = await  this.$repositories.GetAllStates.GetAllStates()
      this.AllStates = res.data.states

  },
  head() {
    return {

      title: '  افزودن آگهی',
      meta: [
        {
          hid: "description",
          name: "description",
          content:' افزودن آگهی',
        },
      ],
    };
  },

  data() {
    return {

      categories: [],
      title: "",
      description: "",
      url: null,
      subUrl: [],
      price: null,
      ExchangeAbility:false,
      phone: null,
      latitude:0,
      longitude:0,
      mainImage: "",
      subImage: "",
      PhoneNumber:0,
      photos: [],
      categoryId: null,
      Status: 0,
      Tags: "",
        AllCities: [],
        SelectedCityId: null,
        SelectedStateId: null,
        AllStates: [],
    };
  },
  methods: {
    SetLocation(lat,lang){
this.latitude = lat
      this.longitude = lang
    },

      async GetCity(){
        this.$nextTick(()=>{
            this.$nuxt.$loading.start()
        })
          try {
              const res = await this.$repositories.GetAllCities.GetAllCities({
                  pageNumber:1,
                  count:500,
                  stateId:this.SelectedStateId

              })
              this.AllCities = res.data.cities
              this.$nuxt.$loading.finish()
              this.$nuxt.loading = false;
          }catch (e) {
              console.log(e)
          }finally {
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
    RemoveMainImage(){
      this.mainImage = ''
      this.url = ''
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
    RemoveOtherImages(idx){
this.subUrl.splice(idx,1)
      this.photos.splice(idx,1)
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
    async submitCreateAdvertise() {

      if (this.categoryId === null) {
        this.$toast.error("لطفا دسته بندی ر ا انتخاب کنید");
      } else if (this.title === null) {
        this.$toast.error("لطفا عنوان آگهی را مشخص کنید");
      } else if (this.price === 0) {
        this.$toast.error("لطفا قیمت را مشخص کنید");
      }  else if (this.latitude === 0) {
        this.$toast.error("لطفا موقعیت مکانی آگهی را مشخص کنید");
      } else {
        this.$nuxt.$loading.start();
        try {
          await this.$repositories.createAnAd.createAnAd({
            title: this.title,
            categoryId: this.categoryId ,
            price: parseInt(this.price.replaceAll(',','')) ,
            expirationDate: new Date(Date.now()),
            status: 2,
            updateDate: new Date(Date.now()),
            tag: this.Tags,
            condition: this.Status,
            cityId: this.SelectedCityId,
            phoneNumber:this.PhoneNumber,
            exchangeability:this.ExchangeAbility,
            stateId: this.SelectedStateId,
            userId: this.$auth.user.userInfo.userId,
            description: this.description,
            latitude: this.latitude,
            longitude: this.longitude,
            photos: this.photos,
          });
          this.$nuxt.$loading.finish();
          this.$toast.success("ثبت آگهی با موفقیت انجام شد.");
          this.$router.push({ path: "/Migration/MyAdvertise" });
        } catch (error) {
          console.log(error);
        }
      }
    },
  },
};
</script>

<style scoped>
.BgInput{

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
