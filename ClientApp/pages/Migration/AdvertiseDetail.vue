<template>
  <div class="container bg-white p-3 rounded">
    <div class="row col-12">
      <div class="col-md-6 col-12">
        <div class="d-flex flex-column">
          <div class="h1" style="overflow-wrap: anywhere">
            {{ advertiseDetail.title }}
          </div>

          <div class="d-flex gap-3 my-3 border-bottom pb-2">
            <div>تاریخ انقضاء:</div>
            <div>
              {{ advertiseDetail.createDate | moment("jYYYY/jM/jD HH:mm") }}
            </div>
          </div>
          <div class="d-flex gap-3 border-bottom pb-2">
            <div>قیمت:</div>
            <div>
              {{ new Intl.NumberFormat().format(advertiseDetail.price) }}

              تومان
            </div>
          </div>
          <div class="d-flex flex-column gap-3 my-3 border-bottom pb-2 w-100">
            <div>
              <h3>توضیحات</h3>
            </div>
            <div style="overflow-wrap: anywhere">
              <p class="small">
                {{ advertiseDetail.description }}
              </p>
            </div>
          </div>
        </div>
      </div>
      <div class="col-md-6 col-12">
        <div class="thumb-example">
          <client-only>
            <swiper
              class="swiper gallery-top px-5"
              :options="swiperOptionTop"
              ref="swiperTop"
            >
              <swiper-slide
                class="slide-1"
                v-for="item in advertiseDetail.photos"
                :key="item.adsId"
              >
                <img
                  class="img-fluid custom-img-height"
                  v-if="
                    advertiseDetail.photos !== undefined &&
                    advertiseDetail.photos.length > 0
                  "
                  :src="`https://BanooClubapi.simagar.com/${item.base64}`"
                />
              </swiper-slide>
              <div
                class="swiper-button-next swiper-button-white SwiperBtn"
                slot="button-next"
              ></div>
              <div
                class="swiper-button-prev swiper-button-white SwiperBtn"
                slot="button-prev"
              ></div>
            </swiper>
          </client-only>

          <div
            class="my-3"
            id="map-wrap"
            style="height: 25vh"
            v-if="
              advertiseDetail.latitude !== undefined ||
              advertiseDetail.longitude !== undefined
            "
          >
            <client-only>
              <l-map
                :zoom="17"
                :center="[advertiseDetail.latitude, advertiseDetail.longitude]"
              >
                <l-tile-layer
                  url="http://{s}.tile.osm.org/{z}/{x}/{y}.png"
                ></l-tile-layer>
                <l-marker
                  :lat-lng="[
                    advertiseDetail.latitude,
                    advertiseDetail.longitude,
                  ]"
                ></l-marker>
              </l-map>
            </client-only>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { Swiper, SwiperSlide } from "vue-awesome-swiper";
import "swiper/css/swiper.css";

export default {
  name: "AdvertiseDetail",
  layout: "BanooClubLayout",
  fetchOnServer() {
    return true;
  },
  async fetch() {
    try {
      const response = await this.$repositories.getAnAd.getAnAd(
        this.$route.query.id
      );
      this.advertiseDetail = response.data;
    } catch (error) {
      console.log(error);
    }
  },
  components: {
    Swiper,
    SwiperSlide,
  },
  data() {
    return {
      advertiseDetail: {},

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
      swiperOptionThumbs: {
        loop: true,
        slidesPerView: 3,
        loopedSlides: 5,
        spaceBetween: 10,
        centeredSlides: true,
        touchRatio: 0.2,
        slideToClickedSlide: true,
      },
    };
  },
};
</script>

<style scoped>
.custom-img-height {
  max-height: 20rem;
}
.card {
  margin: 0 auto;
  max-width: 75vw;
}

img {
  width: auto;
  height: auto;
}

.SwiperBtn {
  color: #e9a72d;
  cursor: pointer;
}
</style>
