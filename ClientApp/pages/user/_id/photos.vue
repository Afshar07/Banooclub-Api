<template>
  <div class="container-fluid px-0">
    <div
      class="offcanvas offcanvas-end"
      style="
        background: black !important;
        z-index: 999999999999999999999999999999999999999999999999999;
      "
      tabindex="-1"
      id="offcanvasExampleeee"
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
                v-for="item in MyPhotos"
                :key="item"
                @click="showSidePanel()"
              >
                <a href="#offcanvasExampleeee" data-bs-toggle="offcanvas"
                  ><img
                    :src="BaseUrl + item"
                    width="100%"
                    height="200px"
                    style="object-fit: contain"
                    class="rounded"
                    alt=""
                /></a>
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

    <div
      class="row bg-white p-4 mx-1"
      v-if="
        this.$store.state.HeaderData.status === 1 ||
        this.$store.state.HeaderData.status === 4
      "
    >
      <h4><strong>عکس های من </strong></h4>
      <div v-if="MyPhotos" class="row col-12">
        <div
          class="my-md-4 col-md-3 col-6"
          v-for="item in MyPhotos"
          @click="showSidePanel()"
          :key="item"
        >
          <a href="#offcanvasExampleeee" data-bs-toggle="offcanvas"
            ><img
              :src="BaseUrl + item"
              width="100%"
              height="200px"
              style="object-fit: contain"
              class="rounded"
              alt=""
          /></a>
        </div>
      </div>
      <div v-else class="">
        <strong>{{ Msg }}</strong>
      </div>
    </div>
    <div
      v-else-if="this.$store.state.HeaderData.status === 2"
      class="row boxMainContent text-warning position-relative bg-white p-3"
    >
      <p class="text-center">
        صفحه این کاربر خصوصی است برای مشاهده آن درخواست دوستی ارسال کنید
      </p>
    </div>
    <div
      v-else-if="this.$store.state.HeaderData.status === 3"
      class="row boxMainContent text-warning position-relative bg-white p-3"
    >
      <p class="text-center">
        برای مشاهده صفحه این کاربر ابتدا وارد اکانت خود شوید
      </p>
    </div>
  </div>
</template>

<script>
import { Swiper, SwiperSlide } from "vue-awesome-swiper";
import "swiper/css/swiper.css";
export default {
  name: "TimelinePhotos",
  components: {
    Swiper,
    SwiperSlide,
  },
  data() {
    return {
      sidePanel: false,
      url: [],
      BaseImgUrls: [],
      images: [],
      imgId: 0,
      Msg: "",
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
  layout: "PoshtebamPlusLayout",
  mounted() {
    this.GetMyPhotos();
  },

  computed: {
    BaseUrl() {
      return process.env.pic;
    },
  },
  methods: {
    showSidePanel(imageId) {
      this.$parent.$emit("SideNavPictureVideo", this.MyPhotos);
    },
    deleteImage(item) {
      const idx = this.BaseImgUrls.findIndex((e) => e === item);

      this.BaseImgUrls.splice(idx, 1);

      this.images.splice(idx, 1);
      console.log(this.BaseImgUrls);
      console.log(this.images);
    },

    uploadNewPicture() {
      document.querySelector(".my-file").click();
    },
    onFileChange(e) {
      console.log(this.$refs.file.files);
      const that = this;
      const f = [];

      that.Uploaded = true;
      Array.prototype.forEach.call(this.$refs.file.files, (element) => {
        f.push(element);
      });
      f.forEach((element) => {
        this.BaseImgUrls.push(URL.createObjectURL(element));
        const reader = new FileReader();
        reader.onload = (function (theFile) {
          return function () {
            const binaryData = reader.result;
            that.images.push(window.btoa(binaryData));
          };
        })(f);
        reader.readAsBinaryString(element);
      });
    },

    UploadImages() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      });
      let tmpImages = [];
      let tmpImg = {
        base64: "",
        priority: 0,
      };
      this.images.forEach((item) => {
        this.imgId++;
        tmpImg.base64 = item;
        tmpImg.priority = this.imgId;
        const clone = { ...tmpImg };
        tmpImages.push(clone);
        tmpImg.base64 = "";
        tmpImg.priority = 0;
      });

      tmpImages.forEach((item) => {
        this.$axios.post("Users/UploadPhotoGallery", item, {}).then((res) => {
          this.images = [];
          this.BaseImgUrls = [];
          this.$nuxt.$loading.finish();
          this.MyPhotos = [];
          this.GetMyPhotos();
        });
      });
    },
    GetMyPhotos() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      });
      this.$axios
        .post(
          `Users/GetPhotosByUserId?userId=${this.$route.params.id}`,
          null,
          {}
        )
        .then((res) => {
          console.log(res.data);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
          if (res.data.status === 1) {
            res.data.medias.forEach((item) => {
              this.MyPhotos.push(item);
            });
          } else if (res.data.status === 2) {
            this.Msg =
              "این اکانت خصوصی است.برای مشاهده عکس های آن ابتدا باید درخواست دوستی ارسال کنید";
          } else if (res.data.status === 3) {
            this.Msg = "ابتدا وارد اکانت خود شوید";
          }
        });
    },
  },
};
</script>

<style scoped>
.central-meta {
  background: #fdfdfd none repeat scroll 0 0;
  border: 1px solid #ede9e9;
  border-radius: 3px;
  display: inline-block;
  width: 100%;
  margin: 20px auto;
  padding: 25px;
}

.SinglePhoto {
  transition: 0.2s ease;
  cursor: pointer;
}
.SinglePhoto:hover {
  transform: scale(1.1);
  transition: 0.2s ease;
  box-shadow: 0 2px 4px rgb(0 0 0 / 40%);
}

.photos {
  list-style: outside none none;
  margin-bottom: 10px;
  padding-right: 0;
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
  left: 1.5rem;
  top: 1rem;
  cursor: pointer;
}
</style>
