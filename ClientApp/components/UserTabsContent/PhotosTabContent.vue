<template>
  <div>
    <div
      class="offcanvas offcanvas-start sidebar-bg"
      tabindex="-1"
      id="offcanvasExample"
      aria-labelledby="offcanvasExample"
      style="z-index: 9999999;"
    >
      <div class="offcanvas-header pb-0 pt-3">
        <h5 class="offcanvas-title" id="offcanvasExampleLabel">افزودن به گالری</h5>
        <button ref="closeGalleryModal" type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas"
                aria-label="Close"></button>
      </div>

      <div class="offcanvas-body d-flex flex-column">
        <div uk-form-custom class="w-full">
          <div @click="uploadNewPicture" class="tw-bg-gray-100
              tw-border-2 tw-border-dashed tw-flex tw-flex-col tw-h-32 tw-items-center
              tw-justify-center tw-relative tw-w-full tw-rounded-lg mb-3">
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
        </div>
        <div class="overflow-scroll">
          <div
            class="col-md-12 col-6 d-inline-block photos position-relative p-0"
            v-for="(item,idx) in BaseImgUrls"
            :key="idx"
          >
            <img v-if="item.priority === 2" class="img-fluid tw-rounded-lg mb-3" :src="item.base64"/>
            <video v-else-if="item.priority == 3" class="w-100 tw-h-full tw-rounded-lg mb-3" controls :src="item.base64"></video>
            <span class="position-absolute deleteIcon m-3" style="top: 0" @click="deleteImage(item)">
              <font-awesome-icon icon="trash" color="#ff4d4d" size="lg"/>
            </span>
          </div>
        </div>
        <div class="col-12 text-center" v-if="BaseImgUrls && BaseImgUrls.length === 0">
          <p class="text-warning fw-bold">هیچ تصویری بارگذاری نشده است.</p>
        </div>
        <div class="loadmore mt-auto" v-if="BaseImgUrls && BaseImgUrls.length > 0">
          <button type="button" class="button tw-w-full mt-auto" @click="UploadImages">
            بارگذاری
          </button>
        </div>
      </div>
    </div>
    <div class="bg-white p-4 tab_content_back">
      <div class="d-flex flex-row justify-content-between align-items-center">
        <h2 class="tw-text-xl tw-font-bold" style="font-family: iranSans !important;">گالری من</h2>
        <button data-bs-toggle="offcanvas" data-bs-target="#offcanvasExample" aria-controls="offcanvasExample" class="btn AddReplyBtn text-white">
          <PlusIcon fill="#2563eb" style="width: 30px; height: 30px;"/>
        </button>
      </div>
      <div class="row">
        <div
          class="col-md-4 my-3"
          v-for="(item,idx) in MyPhotos"
          :key="idx"
          @click="showSidePanel()"
        >
          <div class="position-relative">
<!--            <a href="#offcanvasExampleeee" data-bs-toggle="offcanvas">-->
              <img
                v-if="item.priority === 2"
                :src="`https://banooclubapi.simagar.com/media/gallery/galleryimages/${item.base64}`"
                style="object-fit: cover;object-position: center; width: 300px;height: 250px"
                class="rounded"
                alt=""
              />
            <video
              v-else-if="item.priority === 3"
              class="rounded w-100"
              style="object-fit: cover;object-position: center;height: 250px"
              controls
              :src="`https://banooclubapi.simagar.com/media/gallery/galleryvideos/${item.base64}`"
            ></video>
              <span class="position-absolute deleteIcon m-3" style="top: 0" @click.stop="renderConfirmationModal(item.base64)">
                <font-awesome-icon icon="trash" color="#ff4d4d" size="lg"/>
              </span>
<!--            </a>-->
          </div>
        </div>
      </div>
      <base-modal
        :isRenderingModal="isRenderingDeleteConfirmation"
        @closeModal="closeDeleteConfirmationModal"
      >
        <template v-slot:title>حذف</template>
        <template v-slot:content>
          <p>آیا از حذف این مورد اطمینان دارید؟</p>
        </template>
        <template v-slot:actions>
          <div class="d-flex align-items-center justify-content-end">
            <button
              class="btn btn-danger mx-2"
              @click="closeDeleteConfirmationModal"
            >
              خیر
            </button>
            <button class="btn btn-primary mx-2" @click="confirmDeletingPhoto">
              بله
            </button>
          </div>
        </template>
      </base-modal>

    </div>
  </div>

</template>

<script>
import PlusIcon from "../Icons/PlusIcon";
import UploadFileIcon from "../Icons/UploadFileIcon";
export default {
  name: "PhotosTabContent",
  components: {UploadFileIcon, PlusIcon},
  data() {
    return {
      isRenderingDeleteConfirmation: false,
      tempImageName: null,
      sidePanel: false,
      url: [],
      BaseImgUrls: [],
      images: [],
      imgId: 0,
      MyPhotos: [],
      swiperOptionTop: {
        loop: false,
        loopedSlides: 5,
        spaceBetween: 10,
        slidesPerView: 1,
        navigation: {
          nextEl: ".swiper-button-next",
          prevEl: ".swiper-button-prev",
        },
      },
    };
  },
  fetchOnServer() {
    return true;
  },
  async fetch() {
    const response = await this.$repositories.getMyMediaGallery.getMyMediaGallery();
    this.MyPhotos = response.data;
  },
  methods: {
    async confirmDeletingPhoto() {
      try {
        this.$nuxt.$loading.start();
        await this.$repositories.deleteMedia.deleteMedia(this.tempImageName);
        this.closeDeleteConfirmationModal();
        this.$fetch();
      } catch (error) {
        console.log(error);
      } finally {
        this.$nuxt.$loading.finish();
      }
    },

    closeDeleteConfirmationModal() {
      this.isRenderingDeleteConfirmation = false;
      this.tempImageName = null;
    },

    renderConfirmationModal(item) {
      this.isRenderingDeleteConfirmation = true;
      // Split item and get name of media
      this.tempImageName = item.split("/")[item.split("/").length - 1];
    },

    showSidePanel(imageId) {
      this.$parent.$emit("SideNavPictureVideo", this.MyPhotos);
    },
    async UploadImages() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      });
      // let tmpImages = [];
      // let tmpImg = {
      //   base64: "",
      //   priority: 0,
      // };
      // this.images.forEach((item) => {
      //   this.imgId++;
      //   tmpImg.base64 = item;
      //   tmpImg.priority = this.imgId;
      //   const clone = { ...tmpImg };
      //   tmpImages.push(clone);
      //   tmpImg.base64 = "";
      //   tmpImg.priority = 0;
      // });

      this.images.forEach(async (item) => {
        await this.$repositories.uploadMediaGallery.uploadMediaGallery(
          item
        );
        this.images = [];
        this.BaseImgUrls = [];
        this.$nuxt.$loading.finish();
        this.$nuxt.loading = false;
        this.$refs.closeGalleryModal.click();
        this.MyPhotos = [];
        this.$fetch();
      });
    },

    uploadNewPicture() {
      this.$refs.file.click();
    },
    onFileChange(e) {

      const that = this;
      const f = [];

      that.Uploaded = true;
      Array.prototype.forEach.call(this.$refs.file.files, (element) => {
        if (element.size > 512000) {
          this.$toast.error('اندازه عکس نمیتواند بیشتر از 512 کیلوبایت باشد')
        }
        else{
          f.push(element);
        }
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

          };
        })(f);
        reader.readAsBinaryString(element);
      });
    },
    deleteImage(item) {
      const idx = this.BaseImgUrls.findIndex((e) => e === item);

      this.BaseImgUrls.splice(idx, 1);

      this.images.splice(idx, 1);

    },


  }

}
</script>

<style scoped>
.tab_content_back {
  border: 1px solid rgb(236 237 238);
  box-shadow: 0 1px 3px 0 rgb(0 0 0 / 10%), 0 1px 2px 0 rgb(0 0 0 / 6%);
  border-radius: 8px !important;

}
</style>

