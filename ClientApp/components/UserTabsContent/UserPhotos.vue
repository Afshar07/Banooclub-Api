<template>
  <div class="NewBg tab_content_back">
      <div class="row">
        <div
          class="col-md-4 my-3"
          v-for="(item,index) in photos"
          :key="index"

        >
          <div class="position-relative">
            <img
              v-if="item.priority === 2"
              :src="`https://subapi.banoclub.com/media/gallery/galleryImages/${item.base64}`"
              style="object-fit: cover;object-position: center; width: 300px;height: 250px"
              class="rounded"
              alt=""
            />
            <video
              v-else-if="item.priority === 3"
              class="rounded w-100"
              style="object-fit: cover;object-position: center;height: 250px"
              controls
              :src="`https://subapi.banoclub.com/media/gallery/galleryVideos/${item.base64}`"
            ></video>
            <!--            </a>-->
          </div>
        </div>
        <div class="row mb-3" v-if="photos.length === 0">
          <div class="col-12 text-warning fw-bold text-center">
            هیچ عکسی برای نمایش وجود ندارد
          </div>
        </div>
      </div>

    </div>

</template>

<script>
import PlusIcon from "../Icons/PlusIcon";
import UploadFileIcon from "../Icons/UploadFileIcon";
export default {
  name: "UserPhotos",
  components: {UploadFileIcon, PlusIcon},
  props:{
    photos:{
      type: Array,
      required:true
    },

  },
  data() {
    return {
      isRenderingDeleteConfirmation: false,
      tempImageName: null,
      sidePanel: false,
      url: [],
      BaseImgUrls: [],
      images: [],
      imgId: 0,

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



}
</script>

<style scoped>

</style>
