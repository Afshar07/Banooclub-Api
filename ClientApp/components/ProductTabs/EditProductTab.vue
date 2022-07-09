<template>
  <div class="row">
    <!--      <h2 class="tw-text-2xl tw-font-semibold pb-3">ویرایش خدمت</h2>-->
    <div class="row">
      <div class="col-md-6 col-12 pt-3">
        <label>نام خدمت</label>
        <input v-model="serviceDetailProp.title" type="text" class="with-border" placeholder="نام خدمت">
      </div>
      <div class="col-md-6 col-12 pt-3">
        <label>دسته بندی خدمت</label>
        <select v-model="serviceDetailProp.serviceCategoryId" class="form-select" aria-label="Default select example">
          <option :value="null">دسته بندی خدمت</option>
          <option v-for="service_category in categories" :value="service_category.serviceCategoryId">
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
          class="form-control border rounded w-100"
          placeholder="توضیحات خدمت"
          id="about"
        ></textarea>
      </div>
      <div class="col-12 py-3">
        <label>آدرس</label>
        <textarea
          v-model="serviceDetailProp.address"
          row="100"
          style="color: #808080;"
          class="form-control border rounded w-100"
          placeholder="آدرس خدمت"
          id="about"
        ></textarea>
      </div>
      <div class="col-12 py-3">
        <div class="labelText">موقعیت روی نقشه</div>
        <div class="my-3" id="map-wrap" style="height: 50vh">
          <client-only>
            <l-map :zoom="17" :center="[serviceDetailProp.latitude,serviceDetailProp.longitude]" @click="addMarker">
              <l-tile-layer
                url="http://{s}.tile.osm.org/{z}/{x}/{y}.png"
              ></l-tile-layer>
              <l-marker :lat-lng="[serviceDetailProp.latitude,serviceDetailProp.longitude]"></l-marker>
            </l-map>
          </client-only>
        </div>
      </div>
      <div class="col-md-6 col-12 pt-3">
        <label>شماره تلفن 1</label>
        <input v-model="serviceDetailProp.phoneNumber1" type="text" class="with-border" placeholder="شماره تلفن">
      </div>
      <div class="col-md-6 col-12 pt-3">
        <label>شماره تلفن 2</label>
        <input v-model="serviceDetailProp.phoneNumber2" type="text" class="with-border" placeholder="شماره تلفن">
      </div>
      <div class="col-md-6 col-12 pt-3">
        <label>شماره همراه</label>
        <input v-model="serviceDetailProp.mobile" type="text" class="with-border" placeholder="شماره همراه">
      </div>
      <div class="col-md-6 col-12 pt-3">
        <label>ایمیل</label>
        <input v-model="serviceDetailProp.email" type="text" class="with-border" placeholder="ایمیل">
      </div>
      <div class="col-md-12 col-12 pt-3">
        <div class="labelText">
          هشتگ های خدمت
        </div>
        <div class="my-3">
          <input v-on:keyup.enter="createTag" v-model="tag" type="text" class="with-border" placeholder="ناخن،مو،رنگ...">
          <div class="d-flex">
            <div class="back_tags p-1 m-1 d-flex" v-for="(tag,index) in serviceDetailProp.tags">
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
          انتخاب عکس نمونه کار (اولین عکس به عنوان عکس اصلی خدمت بارگذاری می شود - حداکثر 10 عکس می توانید بارگذاری کنید)
        </div>
        <div class="d-flex row">
          <div class="py-3 col-lg-2 col-md-3 px-3">
            <div @click="uploadNewPicture" class="tw-bg-gray-100 tw-border-2 tw-border-dashed tw-flex tw-flex-col tw-h-32 tw-items-center tw-justify-center tw-relative tw-rounded-lg">
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
            v-for="(image,index) in new_image_preview"
            :key="index"
          >
            <img class="tw-rounded-lg tw-h-32" width="100%" :src="image.base64" :alt="serviceDetailProp.title"
                 style="object-fit: contain" :class="{BorderGreen:index===0}"/>
            <span class="position-absolute deleteIcon m-3" style="top: 15px" @click="deleteNewImage(index)">
                <font-awesome-icon icon="trash" color="#ff4d4d" size="sm"/>
              </span>
          </div>
        </div>
        <div class="d-flex row">
          <div class="py-3 col-lg-2 col-md-3 px-3">
            <div class="tw-bg-gray-100 tw-border-2 tw-border-dashed tw-flex tw-flex-col tw-h-32 tw-items-center tw-justify-center tw-relative tw-rounded-lg">
              تصاویر شما
            </div>
          </div>
          <div
            class="py-3 col-lg-1 col-md-3 px-1 tw-h-40 d-flex photos position-relative"
            v-for="(image,index) in images_preview"
            :key="index"
          >
            <img class="tw-rounded-lg tw-h-32" width="100%" :src="`https://banooclubapi.simagar.com/media/gallery/Service/${image.base64}`" :alt="serviceDetailProp.title"
                 style="object-fit: contain" :class="{BorderGreen:index===0}"/>
            <span class="position-absolute deleteIcon m-3" style="top: 15px" @click="deleteImage(image,index)">
                <font-awesome-icon icon="trash" color="#ff4d4d" size="sm"/>
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
            <div @click="uploadNewPicture" class="tw-bg-gray-100
              tw-border-2 tw-border-dashed tw-flex tw-flex-col tw-h-32 tw-items-center
              tw-justify-center tw-relative tw-rounded-lg">
              <UploadFileIcon style="width: 48px; height: 48px;"/>
            </div>
            <input
              ref="file"
              type="file"
              accept="image/*"
              multiple="multiple"
              class="form-control-file my-file d-none"
              id="my-file"
              @change="onFileChange"
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
                :src="video"
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
            <PlusIcon fill="#2563eb" style="width: 40px; height: 40px;"/>
          </button>
        </div>

        <div class="d-flex flex-column flex-md-row justify-content-between align-items-center px-lg-5"
             v-for="(property,idx) in property_count">
            <span class="deleteIcon m-3 pt-3" style="top: 0" @click="decreasePropertyCount(idx)">
              <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6 tw-text-red-600" viewBox="0 0 20 20"
                   fill="currentColor">
                <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM7 9a1 1 0 000 2h6a1 1 0 100-2H7z"
                      clip-rule="evenodd"/>
              </svg>
            </span>
          <div class="pt-3 col-md-4 col-lg-5 col-sm-12">
            <label>نام ویژگی</label>
            <input v-model="property_name" type="text" class="with-border" placeholder="نام ویژگی">
          </div>
          <div class="pt-3 col-md-4 col-lg-5 col-sm-12">
            <label>قیمت ویژگی</label>
            <input v-model="property_price" maxlength="11" :ref="`Price${idx}`" type="number" class="with-border" placeholder="قیمت ویژگی">
          </div>
          <div class="pt-5">
            <button @click="createProperty" type="button" class="button mt-auto tw-whitespace-nowrap">
              ثبت ویژگی
            </button>
          </div>
        </div>
      </div>
      <div class="col-12 pt-3">
        <div class="d-flex justify-content-between align-items-center">
          <div class="labelText">
            ویژگی های شما
          </div>
        </div>

        <div class="d-flex flex-column flex-md-row justify-content-between align-items-center px-md-5"
             v-for="service_property in serviceDetailProp.properties" :key="service_property.servicePropertyId">
            <span class="deleteIcon m-3 pt-3" style="top: 0"
                  @click="deleteServiceProperty(service_property.servicePropertyId)">
              <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6 tw-text-red-600" viewBox="0 0 20 20"
                   fill="currentColor">
                <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM7 9a1 1 0 000 2h6a1 1 0 100-2H7z"
                      clip-rule="evenodd"/>
              </svg>
            </span>
          <div class="pt-3 col-md-4 col-lg-5 col-sm-12">
            <label>نام ویژگی</label>
            <input  v-model="service_property.name" type="text" class="with-border" placeholder="نام ویژگی">
          </div>
          <div class="pt-3 col-md-4 col-lg-5 col-sm-12">
            <label>قیمت ویژگی</label>
            <input maxlength="11" v-model="service_property.price" type="number" class="with-border" placeholder="قیمت ویژگی">
          </div>
          <div class="pt-5">
            <button type="button" class="button mt-auto tw-whitespace-nowrap">
              ویرایش ویژگی
            </button>
          </div>
        </div>
      </div>
      <div class="col-12 pt-5 tw-text-left">
        <button type="button" class="button mt-auto" @click="updateService">
          ویرایش خدمت
        </button>
      </div>
    </div>


  </div>

</template>

<script>
import UploadFileIcon from "../../components/Icons/UploadFileIcon";
import PlusIcon from "../../components/Icons/PlusIcon";

export default {
  name: "EditProductTab",
  components: {PlusIcon, UploadFileIcon},
  async fetch() {
    this.serviceDetailProp = JSON.parse(JSON.stringify(this.service_details))
    this.serviceDetailProp.medias=[]
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
      tag:'',
      property_price:0,
      property_name:'',
      serviceDetailProp: null,
      categories: [],
      center: [35.757539, 51.409968],
      latlng: [35, 51],
      photos: [],
      Videos:[],
      servicePhotos:[],
      serviceVideos:[],
      images_preview:[],
      new_image_preview:[],
      is_first_image:false,
      properties:null,
      property_count: [],


    };
  },
  methods: {
    async createProperty(){
      if(this.property_name === ''){
        this.$toast.error("لطفا نام ویژگی را وارد کنید");
      }
      else if(this.property_price === 0){
        this.$toast.error("لطفا قیمت ویژگی را وارد کنید");
      }
      else {
        this.$nuxt.$loading.start();
        try {
          await this.$repositories.createAServiceProperty.createAServiceProperty({
            serviceId: this.serviceDetailProp.servicePackId,
            name: this.property_name,
            price: this.property_price,
          })
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
          this.$toast.success("ویژگی با موفقیت ثبت شد");
          this.$nuxt.refresh();
        }catch (error){
          console.log(error);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
        }
      }
    },
    async createTag(){
      if(this.tag === ''){
        this.$toast.error("لطفا نام تگ را وارد کنید");
      }
      else {
        this.$nuxt.$loading.start();
        try {
          await this.$repositories.createAServiceTag.createAServiceTag({
            objectId: this.serviceDetailProp.servicePackId,
            title: this.tag,
            type:1,
          })
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
          this.$toast.success("هشتگ با موفقیت ثبت شد");
          this.$nuxt.refresh();
          this.tag = '';
        }catch (error){
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
      this.property_count.push('property')
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
      console.log(this.BaseImgUrls);
    },

    filteredMedias() {
      this.service_details.medias.filter(item => {
        if (item.priority === 1 || item.priority === 2) {
          this.images_preview.push(item)
        }
        if(item.priority === 3){
          this.serviceVideos.push(item)
        }
      })
    },
    deleteNewImage(index){
      this.new_image_preview.splice(index,1)
      this.serviceDetailProp.medias.splice(index,1)
      console.log('this.new_image_preview',this.new_image_preview)
      console.log('this.serviceDetailProp.medias',this.serviceDetailProp.medias)
    },
    deleteImage(item,index) {
      console.log(item)
      this.serviceDetailProp.medias.push(
        {
          base64:item.base64,
          priority:0
        }
      )
      console.log( this.serviceDetailProp)
      if(this.images_preview[index].priority === 1){
        this.is_first_image = true
      }
      this.images_preview.splice(index,1)
    },

    addMarker(event) {
      this.latlng = event.latlng;
    },

    uploadNewPicture() {
      this.$refs.picture_file.click();
    },
    onFileChange(e) {
      console.log(this.$refs.picture_file.files);
      this.is_first_image = false
      const that = this;
      const f = [];
      that.Uploaded = true;
      Array.prototype.forEach.call(this.$refs.picture_file.files, (element) => {
        if(this.new_image_preview.length + this.images_preview.length >= 10){
          this.$toast.error('بیشتر از 10 عکس نمیتوانید بارگذاری کنید')
        }else if(element.size > 512000){
          this.$toast.error('اندازه عکس نمیتواند بیشتر از 512 کیلوبایت باشد')
        }
        else {
          f.push(element);
        }
      });
      f.forEach((element) => {
        console.log('this.images_preview',this.images_preview)
        if(that.images_preview.length===0 && that.new_image_preview.length===0){
          console.log('salam')
          this.is_first_image = true
          this.new_image_preview.push({
            base64:URL.createObjectURL(element),
            priority:1
          });
        }
        else{
          console.log('salam,khobi')
          that.new_image_preview.push({
            base64:URL.createObjectURL(element),
            priority:2
          });
        }
        console.log('that.new_image_preview',that.new_image_preview)
        const reader = new FileReader();
        reader.onload = (function (theFile) {
          return function () {
            const binaryData = reader.result;
            if(this.is_first_image){
              that.serviceDetailProp.medias.push({
                  base64: window.btoa(binaryData),
                  priority:1
                }
              );
            }
            else {
              that.serviceDetailProp.medias.push({
                  base64: window.btoa(binaryData),
                  priority:2
                }
              );
            }

          };
        })(f);
        console.log('that.serviceDetailProp.medias',that.serviceDetailProp.medias)
        reader.readAsBinaryString(element);
      });
    },

    async updateService() {
      if(this.serviceDetailProp.medias.length===0 && this.images_preview.length===0){
        console.log('khaliiiii')
        this.$toast.error('حداقل یک عکس بارگذاری کنید')
      }
      else {
        this.$nuxt.$loading.start();
        let result = 0
        if(this.properties !== null){
          this.properties.forEach((element) => {
            result += parseInt(element.price)
          })
          // this.serviceDetailProp.totalPrice = this.serviceDetailProp.totalPrice + result
        }
        try {
          const res = await this.$repositories.updateAService.updateAService(this.serviceDetailProp);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
          console.log('res', res)
          this.$toast.success("خدمت با موفقیت ویرایش شد");
          this.$router.push({path: `/Products/${res.data.servicePackId}`});

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
</style>
