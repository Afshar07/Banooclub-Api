<template>
  <div class="sidenav-container">
    <div
      v-if="show"
      class="sidenav-backdrop"
      @click="
        $emit('close');
        showSiding = false;
      "
    ></div>
    <transition name="slide-side">
      <div v-if="show" class="sidenav">
        <div class="row" v-if="!showSiding">
          <div class="col-12 text-center">منوی سایت</div>
          <div class="col-12 rightSideItem">
            <div class="d-flex" @click="toggleSiding(1)">
              <div class="">
                <font-awesome-icon icon="chevron-right" />
              </div>
              <div class="mx-3">صفحه اصلی</div>
            </div>
          </div>
          <div class="col-12 rightSideItem">
            <div class="d-flex" @click="toggleSiding(2)">
              <div class="">
                <font-awesome-icon icon="chevron-right" />
              </div>
              <div class="mx-3">جدول زمانی</div>
            </div>
          </div>
          <div class="col-12 rightSideItem">
            <div class="d-flex" @click="toggleSiding(3)">
              <div class="">
                <font-awesome-icon icon="chevron-right" />
              </div>
              <div class="mx-3">تنظیمات حساب</div>
            </div>
          </div>
          <div class="col-12 rightSideItem">
            <div class="d-flex" @click="toggleSiding(4)">
              <div class="">
                <font-awesome-icon icon="chevron-right" />
              </div>
              <div class="mx-3">انجمن</div>
            </div>
          </div>
          <div class="col-12 rightSideItem">
            <div class="d-flex" @click="toggleSiding(5)">
              <div class="">
                <font-awesome-icon icon="chevron-right" />
              </div>
              <div class="mx-3">فروشگاه ما</div>
            </div>
          </div>
          <div class="col-12 rightSideItem">
            <div class="d-flex" @click="toggleSiding(6)">
              <div class="">
                <font-awesome-icon icon="chevron-right" />
              </div>
              <div class="mx-3">وبلاگ ما</div>
            </div>
          </div>
          <div class="col-12 rightSideItem">
            <div class="d-flex" @click="toggleSiding(7)">
              <div class="">
                <font-awesome-icon icon="chevron-right" />
              </div>
              <div class="mx-3">نمونه کارها</div>
            </div>
          </div>
          <div class="col-12 rightSideItem">
            <div class="d-flex" @click="toggleSiding(8)">
              <div class="">
                <font-awesome-icon icon="chevron-right" />
              </div>
              <div class="mx-3">پشتیبانی و راهنمایی</div>
            </div>
          </div>
          <div class="col-12 rightSideItem">
            <div class="d-flex" @click="toggleSiding(9)">
              <div class="">
                <font-awesome-icon icon="chevron-right" />
              </div>
              <div class="mx-3">صفحات بیشتر</div>
            </div>
          </div>
        </div>
        <div class="row" v-if="showSiding">
          <div
            class="d-flex justify-content-around headSiding"
            @click="closeToggle"
          >
            <div class="">
              {{ showSidingId === 1 ? "صفحه اصلی" : "" }}
              {{ showSidingId === 2 ? "جدول زمانی" : "" }}
              {{ showSidingId === 3 ? "تنظیمات حساب" : "" }}
              {{ showSidingId === 4 ? "انجمن" : "" }}
              {{ showSidingId === 5 ? "فروشگاه ما" : "" }}
              {{ showSidingId === 6 ? "وبلاگ ما" : "" }}
              {{ showSidingId === 7 ? "نمونه کارها" : "" }}
              {{ showSidingId === 8 ? "پشتیبانی و راهنمایی" : "" }}
              {{ showSidingId === 9 ? "صفحات بیشتر" : "" }}
            </div>
            <div>
              <font-awesome-icon icon="chevron-left" />
            </div>
          </div>
          <div class="" v-if="showSidingId === 1">
            <div class="d-flex flex-column">
              <div class="sidingItem">
                <nuxt-link to="/"> صفحه اجتماعی </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> 2 صفحه اجتماعی </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> صفحه اصلی شرکت </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> ورود </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> خروج </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> اخبار </nuxt-link>
              </div>
            </div>
          </div>
          <div class="" v-if="showSidingId === 2">
            <div class="d-flex flex-column">
              <div class="sidingItem">
                <nuxt-link to="/"> جدول زمانی </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> جدول زمانی دوستان </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> جدول زمانی گروه ها </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> صفحات جدول زمانی </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> تصاویر جدول زمانی </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> فیلم های جدول زمانی </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> علاقمندی ها </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> گروه ها </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> لایک </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> افراد نزدیک </nuxt-link>
              </div>
            </div>
          </div>
          <div class="" v-if="showSidingId === 3">
            <div class="d-flex flex-column">
              <div class="sidingItem">
                <nuxt-link to="/"> ایجاد صفحه علاقمندی </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> ویرایش حساب کاربری </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> ویرایش علاقه مندی </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> ویرایش رمز عبور </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> ویرایش پروفایل اصلی </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> ویرایش کارهای آموزشی </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> پیام </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> صندوق ورودی </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> اطلاع رسانی </nuxt-link>
              </div>
            </div>
          </div>
          <div class="" v-if="showSidingId === 4">
            <div class="d-flex flex-column">
              <div class="sidingItem">
                <nuxt-link to="/"> صفحه اصلی انجمن </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> دسته بندی انجمن </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> انجمن موضوع آزاد </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> انجمن ایجاد موضوع </nuxt-link>
              </div>
            </div>
          </div>
          <div class="" v-if="showSidingId === 5">
            <div class="d-flex flex-column">
              <div class="sidingItem">
                <nuxt-link to="/"> محصولات فروشگاه </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> فروشگاه مصالح ساختمانی </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> صفحه جزئیات فروشگاه </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> سبد خرید محصول </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> پرداخت محصولات </nuxt-link>
              </div>
            </div>
          </div>
          <div class="" v-if="showSidingId === 6">
            <div class="d-flex flex-column">
              <div class="sidingItem">
                <nuxt-link to="/"> وبلاگ ما </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> وبلاگ با نوار راست </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> وبلاگ با نوار چپ </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> وبلاگ مصالح ساختمانی </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> لیست سبک وبلاگ </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> لیست وبلاگ با نوار راست </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> لیست وبلاگ با نوار چپ </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> جزئیات پست وبلاگ </nuxt-link>
              </div>
            </div>
          </div>
          <div class="" v-if="showSidingId === 7">
            <div class="d-flex flex-column">
              <div class="sidingItem">
                <nuxt-link to="/"> نمونه کار 2 ستون </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> نمونه کار 3 ستون </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> نمونه کار 4 ستون </nuxt-link>
              </div>
            </div>
          </div>
          <div class="" v-if="showSidingId === 8">
            <div class="d-flex flex-column">
              <div class="sidingItem">
                <nuxt-link to="/"> پشتیبانی و راهنمایی </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> پشتیبانی و راهنمای جزئیات </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> پشتیبانی و راهنمای نتایج جستجو </nuxt-link>
              </div>
            </div>
          </div>
          <div class="" v-if="showSidingId === 9">
            <div class="d-flex flex-column">
              <div class="sidingItem">
                <nuxt-link to="/"> حرفه </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> جزئیات کار </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> صفحه خطای 404 </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> استایل 2 404 </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> صفحه پرسش و پاسخ </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> بینش </nuxt-link>
              </div>
              <div class="sidingItem">
                <nuxt-link to="/"> پایگاه دانش </nuxt-link>
              </div>
            </div>
          </div>
        </div>
      </div>
    </transition>
  </div>
</template>

<script>
export default {
  name: "TheRightSideNav",
  props: {
    show: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      showSiding: false,
      showSidingId: false,
    };
  },
  methods: {
    toggleSiding(sideId) {
      this.showSidingId = sideId;
      this.showSiding = !this.showSiding;
    },
    closeToggle() {
      this.showSiding = false;
    },
  },
};
</script>

<style scoped>
.headSiding {
  color: rgba(0, 0, 0, 0.3);
  padding: 5px 0;
  border-bottom: 1px solid #ccc;
}

.sidingItem {
  text-decoration: none;
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow: hidden;
  color: inherit;
  display: block;
  padding: 10px 10px 10px 20px;
  margin: 0;
}

.sidingItem a {
  text-decoration: none;
  color: #545454;
  line-height: 20px;
  font-size: 14px;
}

.rightSideItem {
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow: hidden;
  color: inherit;
  padding: 10px 10px 10px 20px;
  margin: 0;
  cursor: pointer;
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
  top: 0;
  left: 0;
}

@media (max-width: 576px) {
  .sidenav {
  }
}

.sidenav {
  height: 100%;
  width: 350px;
  background-color: rgb(184 206 219);
  z-index: 10000;
  position: fixed;
  top: 0;
  right: -10px;
  box-sizing: border-box;
  padding: 10px 30px 20px;
}

.slide-side-enter-active,
.slide-side-leave-active {
  transition: all 0.3s ease-in;
}

.slide-side-enter,
.slide-side-leave-to {
  transform: translateX(100%);
}

.nav-list {
  list-style: none;
  padding: 0;
  margin: 0;
  text-align: left;
}

.nav-item {
  margin: 20px 0;
}

.nav-item a {
  text-decoration: none;
  color: black;
  font-size: 1.5rem;
}

.nav-item a:hover,
.nav-item a:active {
  color: red;
}

.switch {
  position: relative;
  display: inline-block;
  width: 90px;
  height: 34px;
}

.switch input {
  display: none;
}

.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #999;
  -webkit-transition: 0.4s;
  transition: 0.4s;
}

.slider:before {
  position: absolute;
  content: "";
  height: 26px;
  width: 26px;
  left: 4px;
  bottom: 4px;
  background-color: white;
  -webkit-transition: 0.4s;
  transition: 0.4s;
}

input:checked + .slider {
  background-color: #088dcd;
}

input:focus + .slider {
  box-shadow: 0 0 1px #2196f3;
}

input:checked + .slider:before {
  -webkit-transform: translateX(55px);
  -ms-transform: translateX(55px);
  transform: translateX(55px);
}

/*------ ADDED CSS ---------*/
.on {
  display: none;
}

.on {
  color: white;
  position: absolute;
  transform: translate(-50%, -50%);
  top: 50%;
  left: 35%;
  font-size: 10px;
}

.off {
  color: white;
  position: absolute;
  transform: translate(-50%, -50%);
  top: 50%;
  left: 65%;
  font-size: 10px;
}

input:checked + .slider .on {
  display: block;
}

input:checked + .slider .off {
  display: none;
}

/*--------- END --------*/
/* Rounded sliders */
.slider.round {
  border-radius: 34px;
}

.slider.round:before {
  border-radius: 50%;
}

.sidenavOptionText {
  font-size: 14px;
  color: #999;
  padding-top: 8px;
}
</style>
