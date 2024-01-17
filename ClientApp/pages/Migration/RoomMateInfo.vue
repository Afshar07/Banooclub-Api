<template>
  <div :class="roomateData===null?'loading-skeleton':''" class="container px-0 mcontainer">
    <div >
      <div class="col-12">
        <div class="col-md-12">
          <div class="row p-4">
            <div class="d-flex align-items-center gap-2 my-2">
              <i class="far fa-building text-muted"></i>
              <h5 class="m-0"><strong>اطلاعات محل زندگی</strong></h5>
            </div>
            <hr />
            <br />
            <div class="col-md-12">
              <div class="row my-3">
                <div class="col-md-12">
                  <div
                    class="SingleRoomMateInfo d-flex gap-2 align-items-center my-3"
                  >
                    <small>نوع مکان زندگی :</small>
                    <small class="text-muted">{{
                        roomateData.roomType === 1
                          ? "آپارتمان"
                          : roomateData.roomType === 2
                            ? "خانه"
                            : roomateData.roomType === 3
                              ? "سوییت"
                              : roomateData.roomType === 4
                                ? "غیره"
                                : ""
                      }}</small>
                  </div>
                </div>

                <div class="col-md-12 my-3">
                  <div
                    class="SingleRoomMateInfo d-flex gap-2 align-items-center"
                  >
                    <small>تعداد اتاق خواب :</small>
                    <small class="text-muted">{{
                        roomateData.bedroomCount
                      }}</small>
                  </div>
                </div>
                <div class="col-md-12 my-3">
                  <div
                    class="SingleRoomMateInfo d-flex gap-2 align-items-center"
                  >
                    <small>نوع اتاق خواب :</small>
                    <small class="text-muted">{{roomateData.bedroomType === 1 ? "خصوصی" : "مشترک"
                      }}</small>
                  </div>
                </div>
                <div class="col-md-12 my-3">
                  <div
                    class="SingleRoomMateInfo d-flex gap-2 align-items-center"
                  >
                    <small>تعداد سرویس بهداشتی :</small>
                    <small class="text-muted">{{
                        roomateData.bathroomCount
                      }}</small>
                  </div>
                </div>
                <div class="col-md-12 my-3">
                  <div
                    class="SingleRoomMateInfo d-flex gap-2 align-items-center"
                  >
                    <small>نوع سرویس بهداشتی :</small>
                    <small class="text-muted">{{
                        roomateData.bathroomType === 1 ? "خصوصی" : "مشترک"
                      }}</small>
                  </div>
                </div>
                <div class="col-md-12 my-3">
                  <div
                    class="SingleRoomMateInfo d-flex gap-2 align-items-center"
                  >
                    <small>نوع مالکیت زندگی :</small>
                    <small class="text-muted">{{
                        roomateData.ownerType === 1
                          ? "من نیز اجاره کرده ام "
                          : "مالک هستم"
                      }}</small>
                  </div>
                </div>
                <div class="col-md-12 my-3">
                  <div
                    class="SingleRoomMateInfo d-flex gap-2 align-items-center"
                  >
                    <small>آدرس :</small>
                    <small class="text-muted">{{ roomateData.address }}</small>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-md-12">
              <client-only>
                <swiper
                  class="swiper gallery-top"
                  :options="swiperOptionTop"
                  ref="swiperTop"
                >
                  <swiper-slide
                    class="slide-1"
                    v-for="item in roomateData.photos"
                    :key="item.base64"
                  >
                    <img
                      :src="BaseUrl + item.base64"
                      class="img-fluid"
                      alt=""
                    />
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
            <div class="col-md-12 my-3">
              <div class="SingleRoomMateInfo d-flex gap-2 align-items-center">
                <small>امکانات خانه :</small>
                <small
                  v-if="roomateData.haveElevator === 1"
                  class="badge bg-primary text-white"
                >آسانسور</small
                >
                <small
                  v-if="roomateData.haveParking === 1"
                  class="badge bg-primary text-white"
                >پارکینگ</small
                >
                <small
                  v-if="roomateData.haveCCTV === 1"
                  class="badge bg-primary text-white"
                >دوربین مدار بسته</small
                >
                <small
                  v-if="roomateData.haveFurniture === 1"
                  class="badge bg-primary text-white"
                >فرنیش</small
                >
                <small
                  v-if="roomateData.haveLobbyMan === 1"
                  class="badge bg-primary text-white"
                >سرایدار</small
                >
              </div>
            </div>
            <!--            <div class="col-md-12 my-3">-->

            <!--                            <div class="SingleRoomMateInfo d-flex gap-2 align-items-center">-->
            <!--                              <small >امکانات محله  :</small>-->
            <!--                              <small v-if="roomateData.nearPark === 1" class=" badge bg-success text-white">نزدیک به پارک </small>-->
            <!--                              <small v-if="roomateData.nearSubway === 1 " class=" badge bg-success text-white">نزدیک به مترو</small>-->
            <!--                              <small v-if="roomateData.nearPublicTransport === 1" class=" badge bg-success text-white">نزدیک به ایستگاه اتوبوس</small>-->
            <!--                            </div>-->
            <!--            </div>-->

            <div class="col-md-12 my-3">
              <div class="SingleRoomMateInfo d-flex gap-2 align-items-center">
                <small>تعداد اتاق خواب :</small>
                <small class="text-muted">{{ roomateData.bedroomCount }}</small>
              </div>
            </div>
            <br />
            <div class="d-flex align-items-center gap-2 my-2">
              <i class="fas fa-coins text-muted"></i>
              <h5 class="m-0"><strong>اطلاعات مالی</strong></h5>
            </div>
            <hr />
            <br />
            <div class="col-md-12 my-3">
              <div
                class="SingleRoomMateInfo d-flex gap-2 align-items-center align-items-md-start"
              >
                <small>اجاره ماهیانه :</small>
                <small class="text-muted"
                >{{
                    new Intl.NumberFormat().format(roomateData.dailyRent)
                  }}
                  تومان</small
                >
              </div>
            </div>
            <div class="col-md-12 my-3">
              <div class="SingleRoomMateInfo d-flex gap-2 align-items-center">
                <small>رهن :</small>
                <small class="text-muted"
                >{{
                    new Intl.NumberFormat().format(roomateData.mortgage)
                  }}
                  تومان</small
                >
              </div>
            </div>

            <div class="col-md-12 my-3">
              <div class="SingleRoomMateInfo d-flex gap-2 align-items-center">
                <small>تاریخ پذیرش هم خانه :</small>
                <small class="text-muted">{{
                    roomateData.receptionDate | moment("jYYYY-jMM-jDD")
                  }}</small>
              </div>
            </div>
            <div class="col-md-12 my-3">
              <div
                class="SingleRoomMateInfo d-flex flex-column flex-md-row gap-2 align-items-md-center justify-content-start"
              >
                <small>امکاناتی که با اجاره محاسبه شده اند :</small>
                <small class="badge bg-success text-white" v-if="roomateData.withElectricity === 1">برق</small>
                <small class="badge bg-success text-white" v-if=" roomateData.withGarbage === 1">جمع آوری زباله</small>
                <small class="badge bg-success text-white" v-if=" roomateData.withGaz === 1">گاز</small>
                <small class="badge bg-success text-white" v-if="roomateData.withInternet === 1">اینترنت</small>
                <small class="badge bg-success text-white" v-if="roomateData.withWater === 1">آب</small>
              </div>
            </div>
            <br />
            <div class="d-flex align-items-center gap-2 my-2">
              <i class="fas fa-bed text-muted"></i>
              <h5 class="m-0"><strong>اطلاعات هم خانه</strong></h5>
            </div>

            <hr />
            <br />
            <div class="col-md-12 my-3">
              <div
                class="SingleRoomMateInfo d-flex flex-wrap gap-2 align-items-center"
              >
                <small>جنسیت های مورد نظر برای هم خانه :</small>
                <small
                  class="badge bg-primary"
                  v-if="
                    roomateData.roomateGender &&
                    roomateData.roomateGender.includes('1')
                  "
                >فرقی نمی کند</small
                >
                <small
                  class="badge bg-primary"
                  v-if="
                    roomateData.roomateGender &&
                    roomateData.roomateGender.includes('2')
                  "
                >مرد</small
                >
                <small
                  class="badge bg-primary"
                  v-if="
                    roomateData.roomateGender &&
                    roomateData.roomateGender.includes('3')
                  "
                >زن</small
                >
                <small
                  class="badge bg-primary"
                  v-if="
                    roomateData.roomateGender &&
                    roomateData.roomateGender.includes('4')
                  "
                >سایر</small
                >
              </div>
            </div>
            <br />
            <div class="col-md-12 my-3">
              <div class="SingleRoomMateInfo d-flex gap-2 align-items-center">
                <small>محدوده سنی هم خانه :</small>
                <small class="text-muted"
                >از {{ roomateData.roomateAgeRangeFrom }} تا
                  {{ roomateData.roomateAgeRangeTo }}</small
                >
              </div>
            </div>
            <div class="col-md-12 my-3">
              <div class="SingleRoomMateInfo d-flex gap-2 align-items-center">
                <small>تعداد ساکنین :</small>
                <small class="text-muted">{{
                    roomateData.residenceNumber
                  }}</small>
              </div>
            </div>

            <div class="col-md-12 my-3">
              <div class="SingleRoomMateInfo d-flex gap-2 align-items-center">
                <small>سیگار کشیدن :</small>
                <small class="text-muted"
                >{{
                    roomateData.roomateSmoke === 1
                      ? "بله"
                      : roomateData.roomateSmoke === 2
                        ? "خیر"
                        : "فرقی نمیکند"
                  }}
                </small>
              </div>
            </div>

            <div class="col-md-12 my-3">
              <div class="SingleRoomMateInfo d-flex gap-2 align-items-center">
                <small>فرزند :</small>
                <small class="text-muted"
                >{{
                    roomateData.roomateChild === 1
                      ? "بله"
                      : roomateData.roomateSmoke === 2
                        ? "خیر"
                        : "فرقی نمیکند"
                  }}
                </small>
              </div>
            </div>
            <br />
            <div class="d-flex align-items-center gap-2 my-2">
              <i class="fas fa-user text-muted"></i>
              <h5 class="m-0"><strong>اطلاعات من</strong></h5>
            </div>

            <hr />
            <br />
            <div class="col-md-12 my-3">
              <div
                class="SingleRoomMateInfo d-flex  align-items-center gap-2 "
              >
                <small>درباره من :</small>
                <small class="text-muted">{{ roomateData.description }}</small>
              </div>
            </div>
            <div class="col-md-12 my-3">
              <div
                class="SingleRoomMateInfo d-flex  gap-2 align-items-center"
              >
                <small>جنسیت من :</small>
                <small class="badge bg-primary text-white">{{
                    roomateData.ownerGender === 2
                      ? "مرد"
                      : roomateData.ownerGender === 3
                        ? "زن"
                        : ""
                  }}</small>
              </div>
            </div>
            <div class="col-md-12 my-3">
              <div
                v-if="roomateData.ownerLangs !== null"
                class="SingleRoomMateInfo d-flex  gap-2 align-items-center"
              >
                <small>زبان های مکالمه من :</small>
                <small
                  class="text-muted"
                  v-if="
                    roomateData.ownerLangs &&
                    roomateData.ownerLangs.indexOf('1') > -1
                  "
                >انگلیسی</small
                >
                <small
                  class="text-muted"
                  v-if="
                    roomateData.ownerLangs &&
                    roomateData.ownerLangs.indexOf('2') > -1
                  "
                >فرانسوی</small
                >
                <small
                  class="text-muted"
                  v-if="
                    roomateData.ownerLangs &&
                    roomateData.ownerLangs.indexOf('3') > -1
                  "
                >عربی</small
                >
                <small
                  class="text-muted"
                  v-if="
                    roomateData.ownerLangs &&
                    roomateData.ownerLangs.indexOf('4') > -1
                  "
                >فارسی</small
                >
              </div>
            </div>
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
  layout: "PoshtebamPlusLayout",
  name: "RoomMateInfo",
  components: {
    Swiper,
    SwiperSlide,
  },
  fetchOnServer() {
    return true;
  },
  head() {
    return {

      title: 'اطلاعات هم خانه',
      meta: [
        {
          hid: "description",
          name: "description",
          content:'اطلاعات هم خانه',
        },
      ],
    };
  },

  async fetch() {
    try {
      const response =
        await this.$repositories.getRoommateByUserId.getRoommateByUserId(
          this.$route.query.userId
        );
      this;
      this.roomateData = response.data.roomate;
    } catch (error) {
      console.log(error);
    }
  },
  data() {
    return {
      roomateData: null,
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
  mounted() {
    // this.getRoommateUserInfo();
  },
  computed: {
    RoomMateGender() {
      return this.roomateData.roomate.roomateGender.split(",");
    },

    BaseUrl() {
      return process.env.pic;
    },
  },
  methods: {
    getRoommateUserInfo() {
      if (this.$route.query.userId) {
        this.$axios
          .post(`Roomate/GetByUserId`, null, {
            params: {
              userId: parseInt(this.$route.query.userId),
            },
          })
          .then((response) => {

            if (response.status === 200) {
              this.$nuxt.$loading.finish();
              this.$nuxt.loading = false;
              this.roomateData = response.data.roomate;
            }
          })
          .catch((error) => {
            console.log(error);
          });
      } else {
        this.$axios
          .post(`Roomate/GetMyRoomate`, null, {})
          .then((response) => {
            if (response.status === 200) {
              this.roomateData = response.data;

            }
          })
          .catch((error) => {
            console.log(error);
          });
      }
    },
  },
};
</script>

<style scoped></style>
