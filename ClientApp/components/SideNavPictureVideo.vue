<template>
  <div class="sidenav-container">
    <div
      v-if="show"
      class="sidenav-backdrop"
      @click="$emit('close');showSiding=false"></div>
    <transition name="slide-side">
      <div
        v-if="show"
        class="sidenav px-0">
        <div class="closeIcon position-relative" @click="$emit('close');showSiding=false">
          <div class="Icon position-absolute">
            <font-awesome-icon icon="times" color="white"/>
          </div>
        </div>
        <div class="container px-0" v-if="typeData === 'images'">
          <div v-swiper="swiperOptions">
            <div class="swiper-wrapper">
              <div class="swiper-slide" v-for="index in 6" :key="index">
                <img src="~/static/photo1.jpg"/>
              </div>
            </div>
            <div class="swiper-button-prev" slot="button-prev"></div>
            <div class="swiper-button-next" slot="button-next"></div>
          </div>
        </div>
        <div class="container px-0" v-if="typeData === 'video'">
          <div v-swiper="swiperOptions">
            <div class="swiper-wrapper">
              <div class="swiper-slide" v-for="index in 6" :key="index">
                <video controls autoplay>
                  <source src="https://www.aparat.com/v/HtQjh/" type="video/mp4">
                </video>
              </div>
            </div>
            <div class="swiper-button-prev" slot="button-prev"></div>
            <div class="swiper-button-next" slot="button-next"></div>
          </div>
        </div>
      </div>
    </transition>
  </div>
</template>

<script>
import "swiper/css/swiper.css";
import {directive} from 'vue-awesome-swiper'

export default {
  directives: {
    swiper: directive
  },
  name: "SideNavPictureVideo",
  props: {
    show: {
      type: Boolean,
      default: false
    },
    typeData: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      swiperOptions: {
        loop: false,
        navigation: {
          nextEl: '.swiper-button-next',
          prevEl: '.swiper-button-prev'
        }
      },
    }
  },
  methods: {
    toggleSiding(sideId) {
      this.showSidingId = sideId;
      this.showSiding = !this.showSiding;
    },
    closeToggle() {
      this.showSiding = false;
    },
  }
}
</script>

<style scoped>
.Icon {
  top: 12px;
  right: 12px;
  height: 26px;
  width: 26px;
  background-position: 50% 50%;
  background-repeat: no-repeat;
  cursor: pointer;
  z-index: 2;
}

.swiper-slide {
  display: flex;
  justify-content: center;
  align-items: center;
  text-align: center;
  font-size: 38px;
  font-weight: 700;
  background-color: #eee;
  margin: 40vh auto;
  width: 100%;
  min-height: 300px;
}

.swiper-button-prev {
  opacity: 1;
  color: #fff;
  cursor: pointer;
}

.swiper-button-next {
  opacity: 1;
  color: #fff;
  cursor: pointer;
}

.sidingItem a {
  text-decoration: none;
  color: #545454;
  line-height: 20px;
  font-size: 14px;
}

.sidenav-container {
  height: 100%;
  width: 100%;
  direction: rtl;
}

.sidenav-backdrop {
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.7);
  z-index: 1000;
  position: fixed;
  top: 60px;
  left: 0;
}

.sidenav {
  height: 100%;
  background: #292929;
  z-index: 10000;
  position: fixed;
  top: 0;
  right: 0;
  box-sizing: border-box;
  padding: 10px 30px 20px;
  width: 600px;
}

.slide-side-enter-active,
.slide-side-leave-active {
  transition: all 0.3s ease-in;
}

.slide-side-enter,
.slide-side-leave-to {
  transform: translateX(100%);
}

@media (max-width: 576px) {
  .sidenav {
    background: #292929;
    z-index: 10000;
    position: fixed;
    top: 0;
    right: 0;
    box-sizing: border-box;
    padding: 10px 30px 20px;
    width: 100vw;
    height: 100vh;
    min-width: 100%;
    min-height: 100%;
  }
}
</style>
