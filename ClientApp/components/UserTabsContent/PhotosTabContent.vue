<template>
  <div>
    <div id="MediaModal" aria-hidden="true" aria-labelledby="exampleModalLabel" class="modal fade"
         style="direction: ltr!important" tabindex="-1">
      <div class="modal-dialog modal-dialog-centered modal-lg">
        <div class="modal-content">
          <div class="modal-header">

            <button aria-label="Close" class="btn-close" data-bs-dismiss="modal" type="button"></button>
          </div>
          <div class="modal-body">
            <VueperSlides
              ref="new_carousel"
              :arrows="true"
              :breakpoints="{ 425:{visibleSlides: 1,  slideMultiple: 1}, 426: { visibleSlides: 1,  slideMultiple: 2 } , 769: { visibleSlides: 2,  slideMultiple: 2,  gap:0 }}"
              :bullets="false"
              :gap="1"
              :rtl="true"
              :touchable="false"
              :visible-slides="3"
              fixed-height="30rem"
            >
              <VueperSlide v-for="(item,idx) in MyPhotos">
                <template #content>
                  <img v-if="item.priority === 1 || item.priority === 2"
                       :src="`https://subapi.banoclub.com/media/gallery/galleryimages/${item.base64}`"
                       alt="Product Image"
                       class="tw-w-full tw-h-full "
                       style="object-fit: contain;object-position: center !important;">
                  <video
                    v-else-if="item.priority === 3"
                    :src="`https://subapi.banoclub.com/media/gallery/galleryvideos/${item.base64}`"
                    class="tw-w-full tw-h-full"
                    controls
                  ></video>
                </template>
              </VueperSlide>
            </VueperSlides>
          </div>

          <!--          <div v-if="SelectedMedia!==null" class="modal-body">-->
          <!--            <img v-if="SelectedMedia.priority == 1 || SelectedMedia.priority == 2"-->
          <!--                 :src="`https://subapi.banoclub.com/media/gallery/galleryimages/${SelectedMedia.base64}`"-->
          <!--                 alt="Product Image"-->
          <!--                 class="tw-w-full tw-h-full"-->
          <!--                 style="object-fit: contain;object-position: center !important;height: 330px;!important; ">-->
          <!--            <video-->
          <!--              v-else-if="SelectedMedia.priority == 3"-->
          <!--              :src="`https://subapi.banoclub.com/media/gallery/galleryvideos/${SelectedMedia.base64}`"-->
          <!--              class="w-100 tw-h-full"-->
          <!--              controls-->
          <!--            ></video>-->
          <!--          </div>-->

        </div>
      </div>
    </div>
    <div
      id="offcanvasExample"
      aria-labelledby="offcanvasExample"
      class="offcanvas offcanvas-start sidebar-bg"
      style="z-index: 9999999;"
      tabindex="-1"
    >
      <div class="offcanvas-header pb-0 pt-3">
        <h5 id="offcanvasExampleLabel" class="offcanvas-title">افزودن به گالری</h5>
        <button ref="closeGalleryModal" aria-label="Close" class="btn-close text-reset" data-bs-dismiss="offcanvas"
                type="button"></button>
      </div>

      <div class="offcanvas-body d-flex flex-column">
        <div class="w-full" uk-form-custom>
          <div class="tw-bg-gray-100
              tw-border-2 tw-border-dashed tw-flex tw-flex-col tw-h-32 tw-items-center
              tw-justify-center tw-relative tw-w-full tw-rounded-lg mb-3" @click="uploadNewPicture">
            <UploadFileIcon style="width: 48px; height: 48px;"/>
          </div>
          <input
            id="my-file"
            ref="file"
            accept="image/*,video/*"
            class="form-control-file my-file d-none"
            multiple="multiple"
            type="file"
            @change="onFileChange"
          />
        </div>
        <div class="overflow-scroll">
          <div
            v-for="(item,idx) in BaseImgUrls"
            :key="idx"
            class="col-md-12 col-6 d-inline-block photos position-relative p-0"
          >
            <img v-if="item.priority === 2" :src="item.base64" class="img-fluid tw-rounded-lg mb-3"/>
            <video v-else-if="item.priority == 3" :src="item.base64" class="w-100 tw-h-full tw-rounded-lg mb-3"
                   controls></video>
            <span class="position-absolute deleteIcon m-3" style="top: 0" @click="deleteImage(item)">
              <font-awesome-icon color="#ff4d4d" icon="trash" size="lg"/>
            </span>
          </div>
        </div>
        <div v-if="BaseImgUrls && BaseImgUrls.length === 0" class="col-12 text-center">
          <p class="text-warning fw-bold">هیچ تصویری بارگذاری نشده است.</p>
        </div>
        <div v-if="BaseImgUrls && BaseImgUrls.length > 0" class="loadmore mt-auto">
          <button class="button tw-w-full mt-auto" type="button" @click="UploadImages">
            بارگذاری
          </button>
        </div>
      </div>
    </div>
    <div class="bg-white p-4 tab_content_back">
      <div class="tw-w-full    p-3 d-flex align-items-center justify-content-between gap-3   ">
        <div class="d-flex align-items-center gap-2">
          <img alt="" class="tw-w-[7rem] tw-h-[7rem] tw-object-contain" src="/girl-icon-gallery.png">
          <div class="d-flex align-items-center flex-column">
            <h1 class="text-purple h6" style="font-weight: bolder!important;">گالری من</h1>
            <strong class="text-pink">My Gallery</strong>
          </div>
        </div>
        <button v-tooltip="{content:'افزودن عکس جدید'}" aria-controls="offcanvasExample"
                class="btn AddReplyBtn text-white"
                data-bs-target="#offcanvasExample" data-bs-toggle="offcanvas">
          <LazyPlusIcon fill="#ff6f9e" style="width: 30px; height: 30px;"/>
        </button>
      </div>

      <div class="row">
        <div
          v-for="(item,idx) in MyPhotos"
          :key="idx"
          class="col-lg-4 col-6 my-3"
        >
          <div class="position-relative">
            <!--            <a href="#offcanvasExampleeee" data-bs-toggle="offcanvas">-->
            <div v-if="item.priority === 1 || item.priority === 2" class="  tw-h-full ShowMediaModal">
              <span class="position-absolute tw-left-1 deleteIcon m-3"
                    @click.prevent="renderConfirmationModal(item.base64)">
                <font-awesome-icon color="#ff4d4d" icon="trash" size="lg"/>
              </span>
              <img :src="`https://subapi.banoclub.com/media/gallery/galleryimages/${item.base64}`"
                   class="tw-w-full tw-shadow tw-rounded tw-h-60" data-bs-target="#MediaModal"
                   data-bs-toggle="modal"
                   style="object-fit: cover;object-position: center !important;"
                   @click="SetSelectedMedia(item)">
            </div>
            <button v-else-if="item.priority === 3" class="btn ShowMediaModal w-100 tw-h-full">

                <span class="position-absolute tw-left-1 deleteIcon m-3" style="top: 0"
                      @click.prevent="renderConfirmationModal(item.base64)">
                <font-awesome-icon color="#ff4d4d" icon="trash" size="lg"/>
              </span>
              <video
                :src="`https://subapi.banoclub.com/media/gallery/galleryvideos/${item.base64}`"
                class="w-100 tw-h-full tw-shadow tw-rounded"
                controls
                data-bs-target="#MediaModal"
                data-bs-toggle="modal"
                @click="SetSelectedMedia(item)"
              ></video>
            </button>

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
          <div class="p-2">
            <p>آیا از حذف این مورد اطمینان دارید؟</p>
          </div>

        </template>
        <template v-slot:actions>
          <div class="d-flex align-items-center justify-content-end p-2">
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
import {VueperSlides, VueperSlide} from 'vueperslides'
import 'vueperslides/dist/vueperslides.css'

export default {
  name: "PhotosTabContent",
  components: {UploadFileIcon, PlusIcon, VueperSlides, VueperSlide},
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
      SelectedMedia: null,
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
    SetSelectedMedia(slide) {
      this.SelectedMedia = slide
    },
    async confirmDeletingPhoto() {
      try {
        this.$nuxt.$loading.start();
        await this.$repositories.deleteMedia.deleteMedia(this.tempImageName);
        this.closeDeleteConfirmationModal();
        this.$fetch();
      }
      catch (error) {
        console.log(error);
      }
      finally {
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
        if (!(element.type === 'image/jpeg' || element.type === 'image/png' || element.type === 'video/mp4')) {
          this.$toast.error('فرمت یکی از فایل های وارد شده نامعتبر است')
        }
        else if ((element.type === 'image/jpeg' || element.type === 'image/png') && element.type > 512000) {
          this.$toast.error('سایز عکس بزرگتر از 512 کیلوبایت است')
        }
        else if (element.type === 'video/mp4' && element.size > 3000000) {
          this.$toast.error('سایز ویدیو بزرگتر از 3 مگابایت است')
        }
        else {
          f.push(element);
        }
      });
      f.forEach((element) => {
        const tmpBaseImgUrls = {
          base64: '',
          priority: 0
        }
        if (this.$refs.file.files[0].type.includes("image")) {
          tmpBaseImgUrls.base64 = URL.createObjectURL(element)
          tmpBaseImgUrls.priority = 2
        }
        else {
          tmpBaseImgUrls.base64 = URL.createObjectURL(element)
          tmpBaseImgUrls.priority = 3
        }
        const clone = {...tmpBaseImgUrls};
        this.BaseImgUrls.push(clone);

        const reader = new FileReader();
        reader.onload = (function (theFile) {
          return function () {
            const binaryData = reader.result;
            const tmpImage = {
              base64: '',
              priority: 0
            }
            if (tmpBaseImgUrls.priority === 2) {
              tmpImage.base64 = window.btoa(binaryData)
              tmpImage.priority = 2
            }
            else {
              tmpImage.base64 = window.btoa(binaryData)
              tmpImage.priority = 3
            }
            const clone = {...tmpImage};
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

