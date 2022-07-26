<template>
  <div>
    <CustomModal v-show="showModal" @close-modal="showModal = false" >
      <template v-slot:title>
        <h5 class="text-right mb-0">افزودن پست</h5>
      </template>
      <template v-slot:content>
        <div class="row" style="position: relative">
          <div @click.stop v-if="!select_media" class="col-12">
            <div class="row" style="overflow-y: scroll;">
              <!--              style="overflow-y: scroll; height: 410px;"-->
              <div class="col-xl-4 col-md-6 col-12 my-3 d-flex justify-content-center" v-for="(item,index) in galleries" :key="index">
                <img  v-if="item.priority === 2" @click.stop="selectImage(item)" :src="`https://banooclubapi.simagar.com/media/gallery/galleryimages/${item.base64}`" class="rounded posts_media_size" alt="post_image"/>
                <video
                  @click.stop="selectImage(item)"
                  v-else-if="item.priority === 3"
                  class="rounded w-100"
                  style="object-fit: cover;object-position: center;height: 250px"
                  controls
                  :autoplay="false"
                  :src="`https://banooclubapi.simagar.com/media/gallery/galleryvideos/${item.base64}`"
                ></video>
                <span :class="GetSelectedImageClass(item)"  class="position-absolute d-none deleteIcon m-2" style="left: 15px;">
                  <font-awesome-icon icon="check" color="green" size="lg"/>
                </span>
              </div>
              <div class="col-xl-4 col-md-6 col-12 my-3 d-flex justify-content-center" v-for="(item,index) in BaseImgUrls" :key="index">
                <img v-if="item.priority === 2" @click.stop="selectInputImages(item,index)" :src="`${item.base64}`"
                     class="rounded posts_media_size" alt="post_image"/>
                <video
                  @click.stop="selectInputImages(item,index)"
                  v-else-if="item.priority === 3"
                  class="rounded w-100"
                  style="object-fit: cover;object-position: center;height: 250px"
                  controls
                  :autoplay="false"
                  :src="`${item.base64}`"
                ></video>
                <span :class="GetInputSelectedImageClass(item)"  class="position-absolute d-none deleteIcon m-2" style="left: 15px;">
                  <font-awesome-icon icon="check" color="green" size="lg"/>
                </span>
                <span class="position-absolute deleteIcon m-2" style="right: 15px;" @click="deleteImage(index)">
                <font-awesome-icon icon="trash" color="#ff4d4d" size="sm"/>
              </span>
              </div>
              <div class="row mb-3" v-if="galleries.length===0">
                <div class="col-12 text-warning fw-bold text-center">
                  عکسی در گالری شما وجود ندارد.
                </div>
              </div>
            </div>
            <div class="w-full py-3">
              <div @click="uploadNewPicture" class="tw-bg-gray-100
              tw-border-2 tw-border-dashed tw-flex tw-flex-col tw-h-32 tw-items-center
              tw-justify-center tw-relative tw-w-full tw-rounded-lg">
                <UploadFileIcon style="width: 48px; height: 48px;"/>
              </div>
              <input
                ref="file"
                type="file"
                accept="image/*,video/*"
                multiple="multiple"
                class="form-control-file my-file d-none"
                id="my-file"
                @change="onFileChange"
              />
              <button @click="select_media=true" type="button" class="button w-100 mt-3">
                بعدی
              </button>
            </div>
          </div>
          <div v-else-if="select_media" class="col-12 d-flex flex-column">
            <div class="row" style="overflow-y: scroll;">
              <div class="col-xl-4 col-md-6 col-12 my-3 d-flex justify-content-center" v-for="(media,idx) in selected_media" :key="idx">
                <img v-if="media.blob === '' && media.priority===2" :src="`https://banooclubapi.simagar.com/media/gallery/galleryimages/${media.base64}`" class="rounded posts_media_size" alt="post_image"/>
                <video
                  v-else-if="media.blob === '' && media.priority===3"
                  class="rounded w-100"
                  style="object-fit: cover;object-position: center;height: 250px"
                  controls
                  :autoplay="false"
                  :src="`https://banooclubapi.simagar.com/media/gallery/galleryvideos/${media.base64}`"
                ></video>
                <img v-else-if="media.blob !== '' && media.priority===2" :src="`${media.blob}`" class="rounded posts_media_size" alt="post_image"/>
                <video
                  v-else-if="media.blob !== '' && media.priority===3"
                  class="rounded w-100"
                  style="object-fit: cover;object-position: center;height: 250px"
                  controls
                  :autoplay="false"
                  :src="`${media.blob}`"
                ></video>
              </div>
            </div>
            <div class="row mt-auto pt-3">
              <textarea
                row="100"
                v-model="description"
                style="color: #808080;"
                class="form-control border rounded w-100 with-border my-3"
                placeholder="توضیحات"
                id="description"
              ></textarea>
              <div class="col-md-6 col-12 my-2">
                <button type="button" class="button w-100" @click="select_media = false">
                  بازگشت
                </button>
              </div>
              <div class="col-md-6 col-12 my-2">
                <button type="button" class="button w-100"  @click="submitNewPost">
                  ثبت
                </button>
              </div>
            </div>
          </div>
        </div>
      </template>
    </CustomModal>
    <button @click="showModal = true" class="button d-flex justify-content-center align-items-center px-2" style="background-color: rgb(37 99 235); color: white">
      <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-5 tw-w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
        <path stroke-linecap="round" stroke-linejoin="round" d="M12 6v6m0 0v6m0-6h6m-6 0H6" />
      </svg>
      <span class="px-2">افزودن پست</span>
    </button>
  </div>
</template>
<script>
import CustomModal from "../components/utilities/CustomModal";
import PlusIcon from "../components/Icons/PlusIcon";
import UploadFileIcon from "../components/Icons/UploadFileIcon";
export default {
  name: "AddPost",
  head() {
    return {
      bodyAttrs: {
        class: this.showModal ? "fixed-body" : "",
      },
    };
  },
  components:{
    CustomModal,
    PlusIcon,
    UploadFileIcon
  },
  fetchOnServer() {
    return true
  },
  data(){
    return{
      showModal:false,
      description:'',
      postData: [],
      galleries:[],
      select_media:false,
      selected_media:[],
      post_media:{
        base64:'',
        priority:0
      },
      selectedIndex:0,
      BaseImgUrls: [],
      selectedMediasIndex:[],
      images: [],
    }
  },
  methods:{
    deleteImage(item) {
      const idx = this.BaseImgUrls.findIndex((e) => e === item);
      this.BaseImgUrls.splice(idx, 1);
      this.selected_media.splice(idx,1)
      this.images.splice(idx, 1);
    },
    uploadNewPicture() {
      this.$refs.file.click();
    },
    onFileChange(e) {
      const that = this;
      const f = [];
      that.Uploaded = true;
      Array.prototype.forEach.call(this.$refs.file.files, (element) => {
        f.push(element);
      });
      f.forEach((element) => {
        const tmpBaseImgUrls = {
          base64:'',
          priority:0
        }
        if(this.$refs.file.files[0].type.includes("image")){
          tmpBaseImgUrls.base64 = URL.createObjectURL(element)
          tmpBaseImgUrls.priority = 2
        }
        else {
          tmpBaseImgUrls.base64 = URL.createObjectURL(element)
          tmpBaseImgUrls.priority = 3
        }
        const clone = { ...tmpBaseImgUrls };
        this.BaseImgUrls.push(clone);
        const reader = new FileReader();
        reader.onload = (function (theFile) {
          return function () {
            const binaryData = reader.result;
            const tmpImage = {
              base64:'',
              priority:0
            }
            if(tmpBaseImgUrls.priority === 2){
              tmpImage.base64 = window.btoa(binaryData)
              tmpImage.priority = 2
            }
            else{
              tmpImage.base64 = window.btoa(binaryData)
              tmpImage.priority = 3
            }
            const clone = { ...tmpImage };
            that.images.push(clone);
            // that.images.push(window.btoa(binaryData));
          };
        })(f);
        reader.readAsBinaryString(element);
      });
    },
    GetSelectedImageClass(item){
      if(this.selected_media.findIndex(e => e.base64=== item.base64)>-1){
        return 'D-block'
      }else{
        return ''
      }
    },
    GetInputSelectedImageClass(item){
      if(this.selected_media.findIndex(e=> e.blob === item.base64)>-1){
        return 'D-block'
      }else{
        return ''
      }
    },
    selectImage(item) {
      if(this.selected_media.findIndex(e => e.base64=== item.base64)>-1){
        const idx = this.selected_media.findIndex(e => e.base64=== item.base64)
        this.selected_media.splice(idx,1)
      }else{
        const tmpSelectedMedia = {
          base64:'',
          blob:'',
          priority:0,
          uploadType:0,
          SelectedIndex:0
        }
        tmpSelectedMedia.base64 = item.base64
        tmpSelectedMedia.blob = ''
        tmpSelectedMedia.priority = item.priority
        tmpSelectedMedia.uploadType = 2
        tmpSelectedMedia.SelectedIndex = this.selectedIndex
        this.selectedMediasIndex.push(this.selectedMediasIndex)
        this.selectedIndex++
        const clone = { ...tmpSelectedMedia };
        this.selected_media.push(clone)
      }
    },
    selectInputImages(item,index){
      if(this.selected_media.findIndex(e=> e.blob === item.base64)>-1){
        const idx = this.selected_media.findIndex(e=> e.blob === item.base64)
        this.selected_media.splice(idx,1)
      }else{
        const tmpSelectedMedia = {
          base64:'',
          blob:'',
          priority:0,
          uploadType:0
        }
        tmpSelectedMedia.base64 = this.images[index].base64
        tmpSelectedMedia.blob = item.base64
        tmpSelectedMedia.priority = item.priority
        tmpSelectedMedia.uploadType = 1
        const clone = { ...tmpSelectedMedia };
        this.selected_media.push(clone)
      }
    },
    async submitNewPost() {
      if (this.selected_media.length === 0) {
        this.$toast.error("لطفا عکس یا فیلم مورد نظر خود را انتخاب کنید");
      }
      else if (this.description === '') {
        this.$toast.error("لطفا توضیحی برای پست خود وارد کنید");
      }
      else {
        this.$nuxt.$loading.start();
        try {
          let tmpSendMedias = []
          let tmpSendMedia = {
            base64: "",
            priority: 0,
            uploadType: 0
          }
          this.selected_media.forEach((element) => {
            tmpSendMedia.base64 = element.base64
            tmpSendMedia.priority = element.priority
            tmpSendMedia.uploadType = element.uploadType
            const clone = { ...tmpSendMedia };
            tmpSendMedias.push(clone)
            tmpSendMedia = {
              base64: "",
              priority: 0,
              uploadType: 0
            }
          })
          const response = await this.$axios.post(`Post/Create`,
            {
              userId: 0,
              title:'عنوان پست',
              description:this.description,
              medias:tmpSendMedias,
              createDate:new Date(Date.now())
            },
          )
            .then((response) => {
              if (response.status === 200) {
                this.$nuxt.$loading.finish();
                this.$nuxt.loading = false;
                this.$toast.success("پست با موفقیت ثبت شد");
                this.showModal = false;
                this.selected_media = []
                this.description = ''
                this.$emit('updateMyPosts')
                this.$fetch();
              }
            })
            .catch((error) => {
              console.log(error);
              this.$nuxt.$loading.finish();
              this.$nuxt.loading = false;
            });
        }
        catch (error){
          console.error(error)
        }
      }
    },
  },
  async fetch() {
    // try {
    //   const posts = await this.$repositories.getFollowingPosts.getFollowingPosts({pageNumber:0, count:0});
    //   this.postData = posts.data;
    // }catch (error) {
    //   console.log(error);
    // }
    try {
      const response = await this.$repositories.getMyMediaGallery.getMyMediaGallery();
      this.galleries = response.data;
    }catch (error) {
      console.log(error);
    }
  }
}
</script>
<style scoped>
.posts_media_size{
  object-fit: cover;
  object-position: center;
  width: 200px;
  height: 250px;
}
.selected{
  display: block;
}
.unSelected{
  display: none;
}
.D-block{
  display: block!important;
}
</style>
