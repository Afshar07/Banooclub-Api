<template>
  <div class="container-fluid bg-white px-0">
    <div class="row">
      <div class="col-md-12 p-4">
        <h4><strong>ثبت آگهی</strong></h4>
      </div>
      <div class="col-md-12">
        <div class="row p-3">
<!--          <div class="col-md-12">-->
<!--            <div class="labelText">-->
<!--              دسته بندی-->
<!--            </div>-->
<!--          </div>-->

<!--          <div class="col-md-4">-->

<!--              <div class="my-3">-->
<!--                <select disabled="" class="form-control"   v-model="categoryId">-->
<!--                  <option >-->
<!--                    {{categoryId}}-->
<!--                  </option>-->
<!--                </select>-->
<!--              </div>-->

<!--          </div>-->
          <div v-if=" categoryId!== null &&categories.findIndex(e=>  e.ParentId === categoryId  )>-1" class="col-md-4">
            <div class="my-3">
              <select class="form-control "   v-model="categoryId2">
                <option v-for="item in categories.filter(e=> e.ParentId === categoryId) " :value="item.MainAdsCategoryId">
                  {{item.MainName}}
                </option>
              </select>

            </div>
          </div>
          <div v-if=" categoryId2!== null &&categories.findIndex(e=>  e.ParentId === categoryId2  )>-1" class="col-md-4">
            <div class="my-3">
              <select class="form-control "   v-model="categoryId3">
                <option v-for="item in categories.filter(e=> e.ParentId === categoryId2) " :value="item.MainAdsCategoryId">
                  {{item.MainName}}
                </option>
              </select>

            </div>

          </div>
          <div class="col-md-12">
            <div class="labelText">
              عنوان آگهی
            </div>
            <div class="my-3">
              <input class="border w-50 rounded p-1" maxlength="50" type="text" v-model.trim="title"
                     placeholder="عنوان آگهی ..."/>
            </div>
          </div>
          <div class="col-md-12">
            <div class="labelText">
              وضعیت کالا
            </div>
            <div class="my-3">
              <select v-model="Status" class="form-control w-25">
                <option value="1">نو</option>
                <option value="2">دست دوم</option>
              </select>
            </div>
          </div>
          <div class="col-md-12">
            <div class="labelText">
              توضیحات آگهی
            </div>
            <div class="my-3">
              <textarea class="border rounded form-control" placeholder="توضیحات آگهی" v-model.trim="description"></textarea>
            </div>
          </div>
          <div class="col-md-12">
            <div class="labelText">
              هشتگ های آگهی  (با علامت , جدا شود)
            </div>
            <div class="my-3">
              <input v-model="Tags" type="text" class="form-control" placeholder="لباس,تیشرت,شلوار...">
            </div>
          </div>
          <div class="col-md-12 my-2">
            <small class="text-secondary">اگر قصد اضافه کردن عکس به آگهی را ندارید قسمت پایین را خالی بگذارید</small>
          </div>
          <div class="col-md-6">
            <div class="labelText">
              عکس اصلی آگهی
            </div>
            <div class="d-flex flex-row gap-3 my-3">
              <div class="border rounded text-center justify-content-center align-items-center InputUiBox"
                   @click="callInputMethodMainImage">
                <input type="file" accept="image/*"
                       class="form-control-file MainImage d-none" id="MainImage" @change="onFileChangeMainImage">
                <font-awesome-icon icon="plus-square" size="lg"/>
              </div>
              <div id="main">
                <img class="img-fluid" v-if="url" :src="url" style="object-fit: contain" :key="url"/>
              </div>
            </div>
          </div>
          <div class="col-md-6">
            <div class="labelText">
              عکس های فرعی آگهی
            </div>
            <div class="d-flex flex-row gap-3 my-3">
              <div class="border rounded text-center justify-content-center align-items-center InputUiBox"
                   @click="callInputMethodSubImage">
                <input type="file" accept="image/*" multiple="multiple"
                       class="form-control-file SubImage d-none" id="SubImage" @change="onFileChangeSubImage">
                <font-awesome-icon icon="plus-square" size="lg"/>
              </div>
              <div id="preview">
                <img class="img-fluid" v-for="item in subUrl" style="object-fit: contain" v-if="subUrl" :src="item" :key="item"/>
              </div>
            </div>
          </div>
          <div class="col-md-6 my-3">
            <input v-model="City" type="text" class="form-control" placeholder="شهر">
          </div>
          <div class="col-md-6 my-3">

            <v-select
              :options="Countries"
              label="name" dir="rtl"
              class="selectWidth "
              placeholder="کشور"
              v-model="countryName"
              :reduce="name => name.id"
              :value="countryName"
            ></v-select>
          </div>
          <div class="col-md-12">
            <div class="labelText">
              موقعیت روی نقشه
            </div>
            <div class="my-3" id="map-wrap" style="height: 50vh">
              <client-only>
                <l-map :zoom=17 :center="center" @click="addMarker">
                  <l-tile-layer url="http://{s}.tile.osm.org/{z}/{x}/{y}.png"></l-tile-layer>
                  <l-marker :lat-lng="latlng"></l-marker>
                </l-map>
              </client-only>
            </div>
          </div>
          <div class="col-md-12">
            <div class="labelText">
              قیمت
            </div>
            <div class="my-3">
              <input class="rounded border p-1 w-50" type="number" placeholder="قیمت به تومان" v-model.trim="price"/>
            </div>
          </div>
          <div class="col-md-12">
            <div class="my-3 text-center">
              <button type="button" class="btn btn-sm AddReplyBtn" @click="submitCreateAdvertise">
                ثبت ویرایش
              </button>
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
        layout: 'PoshtebamPlusLayout',
        data() {
            return {
                createDate:'',
                updateDate:'',
                countryName:'',
                categories: [],
                title: '',
                description: '',
                url: null,
                subUrl: [],
                price: 0,
                phone: null,
                expirationDate: '',
                center: [35.757539, 51.409968],
                latlng: [35,51],
                mainImage: '',
                subImage: '',
                photos: [],
                categoryId: null,
                categoryId2: null,
                categoryId3: null,
                Status:0,
                Tags:'',
                City:'',
                Country:'',
                Countries:[]

            }
        },
        methods: {

            GetCountries(){
                this.$axios.get(`Country/GetCountriesWithRedis`).then((res)=>{
                    this.Countries = res.data
                    console.log(this.Countries)
                })
            },

            aa() {

            },


            addMarker(event) {
                this.latlng = event.latlng;

            },
            callInputMethodMainImage() {
                document.querySelector('.MainImage').click();
            },
            callInputMethodSubImage() {
                document.querySelector('.SubImage').click();
            },
            onFileChangeMainImage(e) {
                const file = e.target.files[0];
                this.url = (URL.createObjectURL(file));
                const that = this
                const reader = new FileReader()
                reader.onload = (function (theFile) {
                    return function () {
                        const binaryData = reader.result
                        that.mainImage = window.btoa(binaryData)
                        that.photos.push({
                            base64: that.mainImage,
                            priority: 1
                        })
                    }
                })(file)
                reader.readAsBinaryString(file)
            },
            onFileChangeSubImage(e) {
                const file = e.target.files[0];
                this.subUrl.push(URL.createObjectURL(file));
                const that = this
                const reader = new FileReader()
                reader.onload = (function (theFile) {
                    return function () {
                        const binaryData = reader.result
                        that.subImage = window.btoa(binaryData)
                        that.photos.push({
                            base64: that.subImage,
                            priority: 2
                        })
                    }
                })(file)
                reader.readAsBinaryString(file)
            },

            getAllCategory() {
                this.$nextTick(() => {
                    this.$nuxt.$loading.start();
                })
                this.$axios.post(`AdsCategory/GetAll?pageNumber=1&count=50`, null, {
                   
                }).then((response) => {
                    if (response.status === 200) {
                        this.$nuxt.$loading.finish();
                        this.categories = response.data.adsCategories
                        console.log(this.categories)
                        this.$nuxt.$loading.finish();
                        this.$nuxt.loading = false
                    }
                }).catch((error) => {
                    console.log(error)
                })
            },
            submitCreateAdvertise() {
                let CategoryId = 0
                if (this.categoryId3 !== null) {
                    CategoryId = this.categoryId3
                } else if (this.categoryId2 !== null) {
                    CategoryId = this.categoryId2

                } else if (this.categoryId !== null) {
                    CategoryId = this.categoryId

                }

                if (this.categoryId === null) {
                    this.$toast.error('لطفا دسته بندی ر ا انتخاب کنید')
                } else if (this.title === null) {
                    this.$toast.error('لطفا عنوان آگهی را مشخص کنید')
                } else if (this.price === 0) {
                    this.$toast.error('لطفا قیمت را مشخص کنید')
                } else if (this.photos === null) {
                    this.$toast.error('لطفا تصاویر را بارگذاری کنید')
                } else if (this.expirationDate === null) {
                    this.$toast.error('لطفا تاریخ انقضای آگهی را مشخص کنید')
                } else if(!this.latlng.lat) {
                    this.$toast.error('لطفا موقعیت مکانی آگهی را مشخص کنید')

                }else{
                    this.$nuxt.$loading.start();
                    this.$axios.post(`Ads/Create`, {
                        title: this.title,
                        categoryId: CategoryId,
                        price: parseInt(this.price),
                        expirationDate: this.expirationDate,
                        adsId: parseInt(this.$route.query.adId),
                        status: 2,
                        createDate: this.createDate,
                        tag: this.Tags,
                        condition: parseInt(this.Status),
                        countryId: this.countryName,
                        userId: parseInt(this.$auth.user.userId),
                        description: this.description,
                        latitude: this.latlng.lat,
                        longitude: this.latlng.lng,
                        photos: this.photos
                    }, {
                       
                    }).then((response) => {
                        if (response.status === 200) {
                            this.$nuxt.$loading.finish();
                            this.$toast.success('ثبت آگهی با موفقیت انجام شد.')
                            this.$router.push({path: '/Migration/MyAdvertise'})
                        }
                    }).catch((error) => {
                        console.log(error)
                    })
                }
            },
            GetAd(){
                this.$axios.post(`Ads/Get?id=${this.$route.query.adId}`,null,{
                
                }).then((res)=>{
                    console.log(res)
                    this.categoryId = res.data.categoryId
                    this.title = res.data.title
                    this.Status = res.data.condition
                    this.description = res.data.description
                    this.Tags = res.data.tag
                    this.photos =res.data.photos
                    this.countryName = res.data.countryId
                    this.latlng.lat = res.data.latitude
                    this.latlng.lng = res.data.longitude
                    this.price = res.data.price
                    this.createDate = res.data.createDate
                })
            }
        },
        mounted() {
            this.getAllCategory();
            this.GetAd()
            this.GetCountries()
        }
    }
</script>

<style scoped>
  .AddReplyBtn{
    background-color: #3a49df;
    color: white;
    transition: .2s ease;
  }
  .centerContent {
    margin: 0 auto;
    padding-right: 2rem;
  }

  input::placeholder {
    font-size: 12px;
    color: #cccccc;
  }

  textarea {
    resize: none;
    min-height: 20rem;
    width: 100%;
  }

  textarea::placeholder {
    font-size: 14px;
    color: #cccccc;
    margin: 0 auto;
    text-align: center;
    padding-top: 8rem;
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
