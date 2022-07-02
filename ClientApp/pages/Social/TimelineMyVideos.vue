<template>
  <div class="container-fluid px-0">
    <div
      class="offcanvas offcanvas-end"
      style="background: black !important"
      tabindex="-1"
      id="Video"
      aria-labelledby="offcanvasExampleLabel"
    >
      <div class="offcanvas-header">
        <h5 class="offcanvas-title" id="offcanvasExampleLabel"></h5>
        <button
          type="button"
          class="btn-close text-reset"
          data-bs-dismiss="offcanvas"
          aria-label="Close"
        ></button>
      </div>
      <div class="offcanvas-body">
        <div>
          <client-only>
            <swiper
              class="swiper gallery-top"
              :options="swiperOptionTop"
              ref="swiperTop"
            >
              <swiper-slide
                class="slide-1"
                v-for="item in MyVideos"
                :key="item"
              >
                <div class="SinglePhoto" @click="showSidePanel()">
                  <a href="#offcanvasExampleeee" data-bs-toggle="offcanvas"
                    ><img
                      :src="BaseUrl + item"
                      width="100%"
                      height="200px"
                      class="rounded"
                      alt=""
                  /></a>
                </div>
              </swiper-slide>
              <div
                class="swiper-button-next swiper-button-white"
                slot="button-next"
              ></div>
              <div
                class="swiper-button-prev swiper-button-white"
                slot="button-prev"
              ></div>
            </swiper>
          </client-only>
        </div>
      </div>
    </div>
    <div class="row central-meta rounded main-shadow">
      <div class="col-12 text-center mb-4">
        <button
          class="btn btn-sm btn-info text-white w-50"
          @click="uploadNewVideo"
        >
          بارگذاری ویدیو جدید
        </button>
        <input
          type="file"
          ref="Video"
          accept="video/*"
          class="form-control-file my-file d-none"
          id="my-file"
          @change="onFileChangeVideo"
        />
      </div>
      <div
        v-if="UploadedVideo"
        class="col-md-3 col-6 d-inline-block photos position-relative mx-3"
      >
        <video
          width="auto"
          height="auto"
          class="w-100 h-auto"
          controls
          :src="BaseVideoUrl"
        ></video>
      </div>

      <div class="col-12 text-center" v-if="url.length === 0">
        <p class="text-warning fw-bold">هیچ ویدیویی بارگذاری نشده است.</p>
      </div>

      <div class="loadmore">
        <button class="btn-view btn-load-more" @click="UploadVideo">
          <i class="fas fa-plus"></i>
        </button>
      </div>
    </div>
    <div class="row bg-white p-4 mx-1 main-shadow rounded col-12">
      <h4><strong>ویدیو های من </strong></h4>
      <div v-if="MyVideos" class="row col-12">
        <div class="my-4 col-12 col-md-6" v-for="item in MyVideos" :key="item">
          <div
            class="d-flex flex-wrap position-relative my-4"
            @click="showSidePanel()"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              class="custom-delete-icon position-absolute"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
              stroke-width="1.5"
              @click.stop="renderConfirmationModal(item)"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16"
              />
            </svg>
            <video
              width="auto"
              height="auto"
              class="w-100 h-auto custom-video-height"
              controls
            >
              <source :src="BaseUrl + item" type="video/mp4" />
            </video>
          </div>
        </div>
      </div>
    </div>
    <base-modal
      :isRenderingModal="isRenderingDeleteConfirmation"
      @closeModal="closeDeleteConfirmationModal"
    >
      <template v-slot:title>حذف ویدیو</template>
      <template v-slot:content>
        <p>آیا از حذف ویدیو اطمینان دارید؟</p>
      </template>
      <template v-slot:actions>
        <div class="d-flex align-items-center justify-content-end">
          <button
            class="btn btn-danger mx-2"
            @click="closeDeleteConfirmationModal"
          >
            خیر
          </button>
          <button class="btn btn-primary mx-2" @click="confirmDeletingVideo">
            بله
          </button>
        </div>
      </template>
    </base-modal>
  </div>
</template>

<script>
import BaseModal from "../../components/Base/BaseModal.vue";
import { Swiper, SwiperSlide } from "vue-awesome-swiper";
import "swiper/css/swiper.css";
import { format } from "path";
export default {
  name: "TimelineVideos",
  components: {
    Swiper,
    SwiperSlide,
    BaseModal,
  },
  fetchOnServer() {
    return true;
  },
  async fetch() {
    try {
      const response = await this.$repositories.getAUserVideos.getAUserVideos();
      this.MyVideos = response.data;
    } catch (error) {
      console.log(error);
    }
  },
  data() {
    return {
      isRenderingDeleteConfirmation: false,
      tempVideoName: null,
      url: [
        "/_nuxt/static/photo1.jpg",
        "/_nuxt/static/photo1.jpg",
        "/_nuxt/static/photo1.jpg",
      ],
      BaseVideoUrl: "",
      Video: "",
      MyVideos: [],
      UploadedVideo: false,
      VidId: 0,
      base: "",
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
  layout: "BanooClubLayout",

  computed: {
    BaseUrl() {
      return process.env.pic;
    },
  },
  methods: {
    async confirmDeletingVideo() {
      try {
        this.$nuxt.$loading.start();
        await this.$repositories.deleteMedia.deleteMedia(this.tempVideoName);
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
      this.tempVideoName = null;
    },
    renderConfirmationModal(item) {
      this.isRenderingDeleteConfirmation = true;
      // Split item and get name of media
      this.tempVideoName = item.split("/")[item.split("/").length - 1];
    },
    async UploadVideo() {
      try {
        this.$nuxt.$loading.start();

        const response = await this.$repositories.uploadVideo.uploadVideo({
          base64: this.base,
          priority: 0,
        });
        console.log(response);
      } catch (error) {
        console.log(error);
      } finally {
        this.$nuxt.$loading.finish();
      }
    },
    showSidePanel(videoId) {
      this.$parent.$emit("SideNavPictureVideo", "video", videoId);
    },
    onFileChangeVideo() {
      const that = this;

      that.UploadedVideo = true;

      const f = this.$refs.Video.files[0];
      // 100 MB limit
      if (f.size > 100000000) {
        this.$toast.error("سایز ویدیو نباید بیشتر از صد مگابایت باشد");
      } else if (this.$refs.Video.files.length > 1) {
        this.$toast.error("در آن واحد بیشتر از یک ویدیو نمیتوان آپلود کرد");
      } else {
        this.BaseVideoUrl = URL.createObjectURL(f);

        const reader = new FileReader();

        reader.onload = (function (theFile) {
          return function () {
            const binaryData = reader.result;
            that.base = window.btoa(binaryData);
          };
        })(f);
        reader.readAsBinaryString(f);
      }
    },
    uploadNewVideo() {
      this.$refs.Video.click();
    },
    deleteVideo() {
      this.Video = "";
      this.BaseImgUrl = "";
    },
  },
};
</script>

<style scoped>
.custom-delete-icon {
  width: 1.5rem;
  height: 1.5rem;
  top: -2rem;
  right: 50%;
  color: #cf0000;
  cursor: pointer;
}
.custom-video-height {
  min-height: 300px;
  max-height: 300px;
}
.playIcon {
  position: absolute;
  top: 40%;
  left: 40%;
  cursor: pointer;
  border: 1px solid #fff;
  border-radius: 50%;
  padding: 8px;
}

.central-meta {
  background: #fdfdfd none repeat scroll 0 0;
  border: 1px solid #ede9e9;
  border-radius: 3px;
  display: inline-block;
  width: 100%;
  margin: 0 auto 20px auto;
  padding: 25px;
}

.photos {
  list-style: outside none none;
  margin-bottom: 10px;
  padding-right: 0;
  position: relative;
}

.photos img:hover {
  cursor: pointer;
  transform: scale(1.01);
}

.lodmore {
  display: inline-block;
  width: 100%;
}

.btn-view.btn-load-more {
  background: #fff;
  border: medium none;
  border-radius: 100%;
  color: transparent;
  display: block;
  height: 50px;
  margin: 30px auto 0;
  position: relative;
  width: 50px;
  transition: all 0.2s linear 0s;
  z-index: 77;
}

.central-meta button.btn-view {
  background: #f1f1f1;
  color: #545454;
}

.btn-view.btn-load-more:hover {
  transform: translate(0, 0) rotate(0deg);
  z-index: 111;
  transition: all 0.2s linear 0s;
  background: #088dcd;
  color: #fff;
}

.btn-view.btn-load-more:hover:hover {
  color: #fff;
  transform: translate(0, 0) rotate(360deg);
}

.deleteIcon {
  left: 1rem;
  top: 1rem;
  cursor: pointer;
}
</style>
