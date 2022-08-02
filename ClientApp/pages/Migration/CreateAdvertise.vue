<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="container mcontainer bg-white">
    <div
      class="container-fluid px-0 rounded"

    >
      <div class="row">
        <div class="col-md-12 p-4">
          <h4><strong>ثبت آگهی</strong></h4>
        </div>
        <div class="col-md-12">
          <div class="row p-3">
            <div class="col-md-12">
              <div class="row">
                <div class="col-md-4">
                  <div class="labelText">دسته بندی</div>

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
<!--                  <div-->
<!--                    v-if="-->
<!--                categoryId !== null &&-->
<!--                categories.findIndex((e) => e.ParentId === categoryId) > -1-->
<!--              "-->
<!--                    class="col-md-4"-->
<!--                  >-->
<!--                    <div class="my-3">-->
<!--                      <select class="form-control" v-model="categoryId2">-->
<!--                        <option-->
<!--                          v-for="(item, index) in categories.filter(-->
<!--                      (e) => e.ParentId === categoryId-->
<!--                    )"-->
<!--                          :value="item.MainAdsCategoryId"-->
<!--                          :key="index"-->
<!--                        >-->
<!--                          {{ item.MainName }}-->
<!--                        </option>-->
<!--                      </select>-->
<!--                    </div>-->
<!--                  </div>-->
<!--                  <div-->
<!--                    v-if="-->
<!--                categoryId2 !== null &&-->
<!--                categories.findIndex((e) => e.ParentId === categoryId2) > -1-->
<!--              "-->
<!--                    class="col-md-4"-->
<!--                  >-->
<!--                    <div class="my-3">-->
<!--                      <select class="form-control" v-model="categoryId3">-->
<!--                        <option-->
<!--                          v-for="(item, index) in categories.filter(-->
<!--                      (e) => e.ParentId === categoryId2-->
<!--                    )"-->
<!--                          :value="item.MainAdsCategoryId"-->
<!--                          :key="index"-->
<!--                        >-->
<!--                          {{ item.MainName }}-->
<!--                        </option>-->
<!--                      </select>-->
<!--                    </div>-->
<!--                  </div>-->
                </div>
              </div>
            </div>

            <div class="col-md-6">
              <div class="labelText">عنوان آگهی</div>
              <div class="my-3">
                <input
                  class="border w-100 rounded p-1"
                  maxlength="50"
                  type="text"
                  v-model.trim="title"
                  placeholder="عنوان آگهی ..."
                />
              </div>
            </div>
            <div class="col-md-6">
              <div class="labelText">وضعیت کالا</div>
              <div class="my-3">
                <select v-model="Status" class="form-control w-100">
                  <option :value="1">نو</option>
                  <option :value="2">دست دوم</option>
                </select>
              </div>
            </div>


            <div class="col-md-6">
              <div class="labelText">عکس اصلی آگهی</div>
              <div class="d-flex flex-row gap-3 my-3">
                <div
                  class="border rounded text-center justify-content-center align-items-center InputUiBox"
                  @click="callInputMethodMainImage"
                >
                  <input
                    type="file"
                    accept="image/*"
                    class="form-control-file MainImage d-none"
                    id="MainImage"
                    @change="onFileChangeMainImage"
                  />
                  <font-awesome-icon icon="plus-square" size="lg" />
                </div>
                <div id="main">
                  <img
                    class="img-fluid"
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
                  class="border rounded text-center justify-content-center align-items-center InputUiBox"
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
                  <font-awesome-icon icon="plus-square" size="lg" />
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
                placeholder="استان"
                v-model="SelectedStateId"
                :reduce="(name) => name.stateId"

              ></v-select>
            </div>
            <div class="col-md-6 my-3">
              <v-select
                :options="AllCities"
                label="name"
                dir="rtl"
                class="selectWidth w-100"
                placeholder="شهر"
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
                  class="form-control"
                  placeholder="لباس,تیشرت,شلوار..."
                />
              </div>
            </div>
            <div class="col-md-6">
              <div class="labelText">قیمت</div>
              <div class="my-3">
                <input
                  class="rounded border p-1 w-100"
                  type="number"
                  placeholder="قیمت به تومان"
                  v-model.trim="price"
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
                v-model.trim="description"
              ></textarea>
              </div>
            </div>

            <div class="col-md-12">
              <div class="labelText">موقعیت روی نقشه</div>
              <div class="my-3" id="map-wrap" style="height: 50vh">
                <client-only>
               <SetLocation @getGeoLocation="SetLocation"></SetLocation>
                </client-only>
              </div>
            </div>

            <div class="col-md-12">
              <div class="my-3 text-center">
                <button
                  type="button"
                  class="btn btn-sm AddReplyBtn"
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
export default {
  name: "CreateAdvertise",
  layout: "PoshtebamPlusLayout",
  fetchOnServer() {
    return true;
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
      price: 0,
      phone: null,
      latitude:0,
      longitude:0,
      mainImage: "",
      subImage: "",
      photos: [],
      categoryId: null,

      Status: 0,
      Tags: "",
        AllCities: [],
        SelectedCityId: 0,
        SelectedStateId: 0,
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
    async submitCreateAdvertise() {

      if (this.categoryId === null) {
        this.$toast.error("لطفا دسته بندی ر ا انتخاب کنید");
      } else if (this.title === null) {
        this.$toast.error("لطفا عنوان آگهی را مشخص کنید");
      } else if (this.price === 0) {
        this.$toast.error("لطفا قیمت را مشخص کنید");
      } else if (this.photos === null) {
        this.$toast.error("لطفا تصاویر را بارگذاری کنید");
      }  else if (this.latitude === 0) {
        this.$toast.error("لطفا موقعیت مکانی آگهی را مشخص کنید");
      } else {
        this.$nuxt.$loading.start();
        try {
          await this.$repositories.createAnAd.createAnAd({
            title: this.title,
            categoryId: this.categoryId ,
            price: parseInt(this.price) ,
            expirationDate: new Date(Date.now()),
            status: 2,
            updateDate: new Date(Date.now()),
            tag: this.Tags,
            condition: this.Status,
            cityId: this.SelectedCityId,
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
  background-color: #3a49df;
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
  padding: 2rem 0 0 0;
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
