<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-10 col-12 order-0">
        <div class="row">
          <div class="col-12" v-if="!this.$store.state.isPrivate || isFollowing">
            <div class="d-flex flex-row gap-4 my-2">
              <div class="" v-if="!this.$store.state.isPrivateAds || isFollowing">
                <div :class="showPost ?'activeMainTab':''" class="titleTab"
                     @click="toggleTab('post')">
                  <font-awesome-icon icon="info"/>
                  پست ها
                </div>
              </div>
              <div class="" v-if="!this.$store.state.isPrivateRoomate || isFollowing">
                <div :class="showRoommate ? 'activeMainTab' : ''" class="titleTab" @click="toggleTab('Roommate')">
                  <font-awesome-icon icon="house-user"/>
                  اطلاعات هم
                  خانه
                </div>
              </div>
              <div class="" v-if="!this.$store.state.isPrivateAds || isFollowing">
                <div :class="showAdvertise ?'activeMainTab':''" class="titleTab" @click="toggleTab('Advertise')">
                  <font-awesome-icon icon="ad"/>
                  آگهی ها
                </div>
              </div>
              <div class="" v-if="!this.$store.state.isPrivateAds || isFollowing">
                <div :class="showPersonalInformation ?'activeMainTab':''" class="titleTab"
                     @click="toggleTab('PersonalInformation')">
                  <font-awesome-icon icon="info"/>
                  اطلاعات شخصی
                </div>
              </div>
              <div v-if="!isFollowing">
                <button @click="sendFollowingRequest" type="button" class="btn btn-outline-danger">
                  ارسال درخواست دوستی
                </button>
              </div>

            </div>
          </div>
          <div class="col-12 infoBox" v-if="showPost && userPostData.length > 0">
            <SocialPost :post-data="userPostData" @updatingDataInPost="getUserPostData"/>
          </div>
          <div class="col-12 infoBox text-center text-warning fw-bold" v-if="showPost && userPostData.status === 2">
            هیچ محتوای برای نمایش وجود ندارد
          </div>
          <div class="col-6 roommateStyle infoBox" v-if="showRoommate">
            <div class="d-flex flex-column" v-if="roomateData.status !== 2">
              <div class="col-6" style="margin: 0 auto">
                <client-only>
                  <swiper class="swiper" :options="swiperOptionTop">
                    <swiper-slide class="slide-1" v-for="item in roomateData.roomate.photos" :key="item.adsId">
                      <img class="img-fluid"
                           v-if="roomateData.roomate.photos !== undefined && roomateData.roomate.photos.length > 0"
                           :src="`https://pplusapi.simagar.com/${ item.base64}`"
                      />
                    </swiper-slide>
                  </swiper>
                </client-only>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">نوع مکان زندگی :</div>
                  <div
                    v-html="roomateData.roomate.roomType === 1 ?
                     'آپارتمان':roomateData.roomate.roomType === 2 ? 'آپارتمان' :roomateData.roomate.roomType === 3?
                      'خانه' :roomateData.roomate.roomType === 4 ? 'سوییت' :''"></div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">تعداد اتاق خواب :</div>
                  <div
                    v-html="roomateData.roomate.bedroomCount"></div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">تعداد سرویس بهداشتی:</div>
                  <div
                    v-html="roomateData.roomate.bedroomCount"></div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">نوع اتاق خواب:</div>
                  <div
                    v-html="roomateData.roomate.bedroomType === 1 ? 'خصوصی' :'مشترک'"></div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">نوع سرویس بهداشتی:</div>
                  <div
                    v-html="roomateData.roomate.bathroomType === 1 ? 'خصوصی' :'مشترک'"></div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">نوع مالکیت مکان زندگی :</div>
                  <div
                    v-html="roomateData.roomate.ownerType === 1 ? 'مالک' :'مستاجر'"></div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">آدرس :</div>
                  <div
                    v-html="roomateData.roomate.address"></div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">امکانات خانه :</div>
                  <div
                    v-html="roomateData.roomate.haveAirCondition === 1 ? 'تهویه هوا' :''"></div>
                  <div
                    v-html="roomateData.roomate.havePool === 1 ? 'استخر' :''"></div>
                  <div
                    v-html="roomateData.roomate.haveParking === 1 ? 'پارکینگ' :''"></div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">امکانات محله :</div>
                  <div
                    v-html="roomateData.roomate.nearPublicTransport === 1 ? 'نزدیک به ایستگاه اتوبوس' :''"></div>
                  <div
                    v-html="roomateData.roomate.nearSubway === 1 ? 'نزدیک به ایستگاه قطار' :''"></div>
                  <div
                    v-html="roomateData.roomate.nearPark === 1 ? 'نزدیک به پارک' :''"></div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">اجاره ماهیانه :</div>
                  <div
                    v-html="roomateData.roomate.monthlyRent"></div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">مبلغ رهن :</div>
                  <div
                    v-html="roomateData.roomate.mortgage"></div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">زمان دریافت اجاره :</div>
                  <div
                    v-html="roomateData.roomate.rentPeriod === 1 ? 'ماهانه' :roomateData.roomate.rentPeriod === 3 ?
                    '3 ماه' :roomateData.roomate.rentPeriod === 6 ?'6 ماه': roomateData.roomate.rentPeriod === 9?'9 ماه' :
                    roomateData.roomate.rentPeriod === 12 ?'سالانه':''
                 "></div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">تاریخ پذیرش هم خانه :</div>
                  <div>{{ roomateData.roomate.receptionDate | moment("jYYYY/jM/jD") }}</div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">امکاناتی که با اجاره محاسبه شده اند :</div>
                  <div v-if="roomateData.roomate.withElectricity === 1">برق</div>
                  <div v-if="roomateData.roomate.withWater === 1">آب</div>
                  <div v-if="roomateData.roomate.withGaz === 1">گاز</div>
                  <div v-if="roomateData.roomate.withInternet === 1">اینترنت</div>
                  <div v-if="roomateData.roomate.withGarbage === 1">جمع آوری زباله</div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">جنسیت های موردنظر برای هم خانه :</div>
                  <div class="d-flex flex-column">
                    <div v-if="roomateData.roomate.roomateGender.indexOf('1') > -1">فرقی نمی کند</div>
                    <div v-if="roomateData.roomate.roomateGender.indexOf('2') > -1">مرد</div>
                    <div v-if="roomateData.roomate.roomateGender.indexOf('3') > -1">مرد</div>
                    <div v-if="roomateData.roomate.roomateGender.indexOf('4') > -1">سایر</div>
                    <div v-if="roomateData.roomate.roomateGender.indexOf('5') > -1">دگرجنس‌گرای غالب و همجنس‌گرا نادر
                    </div>
                    <div v-if="roomateData.roomate.roomateGender.indexOf('6') > -1">دگرجنس‌گرای غالب و گاهی همجنس‌گرا
                    </div>
                    <div v-if="roomateData.roomate.roomateGender.indexOf('7') > -1">به‌طور مساوی دگرجنس‌گرا و
                      همجنس‌گرا
                    </div>
                    <div v-if="roomateData.roomate.roomateGender.indexOf('8') > -1">همجنس‌گرای غالب و گاهی دگرجنس‌گرا
                    </div>
                    <div v-if="roomateData.roomate.roomateGender.indexOf('9') > -1">همجنس‌گرای غالب/دگرجنس‌گرای نادر
                    </div>
                  </div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">محدوده سنی هم خانه :</div>
                  <div>از</div>
                  <div>{{ roomateData.roomate.roomateAgeRangeFrom }}</div>
                  <div>تا</div>
                  <div>{{ roomateData.roomate.roomateAgeRangeTo }}</div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">سیگار کشیدن :</div>
                  <div
                    v-html="roomateData.roomate.roomateSmoke ===1 ?'بله':roomateData.roomate.roomateSmoke ===2 ? 'خیر': roomateData.roomate.roomateSmoke ===3 ?'فرقی نمی کند':null"></div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">فرزند :</div>
                  <div
                    v-html="roomateData.roomate.roomateChild ===1 ?'بله':roomateData.roomate.roomateChild ===2 ? 'خیر': roomateData.roomate.roomateChild ===3 ?'فرقی نمی کند':null"></div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">حیوان خانگی :</div>
                  <div
                    v-html="roomateData.roomate.roomatePet ===1 ?'بله':roomateData.roomate.roomatePet ===2 ? 'خیر': roomateData.roomate.roomatePet ===3 ?'فرقی نمی کند':null"></div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">تعداد ساکنین :</div>
                  <div
                    v-html="roomateData.roomate.residenceNumber"></div>
                  <div>نفر</div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">شغل من :</div>
                  <div
                    v-html="roomateData.roomate.ownerJob"></div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">جنسیت من :</div>
                  <div
                    v-html="roomateData.roomate.ownerGender === 1 ?
                    'فرقی نمی کند':roomateData.roomate.ownerGender === 2 ?
                    'مرد':roomateData.roomate.ownerGender === 3 ?
                    'زن':roomateData.roomate.ownerGender === 4 ?
                    'سایر':roomateData.roomate.ownerGender === 5?
                    'دگرجنس‌گرای غالب/همجنس‌گرای نادر':roomateData.roomate.ownerGender === 6 ?
                    'دگرجنس‌گرای غالب و گاهی همجنس‌گرا':roomateData.roomate.ownerGender === 7 ?
                    'به‌طور مساوی دگرجنس‌گرا و همجنس‌گرا':roomateData.roomate.ownerGender === 8 ?
                    'همجنس‌گرای غالب و گاهی دگرجنس‌گرا':roomateData.roomate.ownerGender === 9 ?
                    'همجنس‌گرای غالب/دگرجنس‌گرای نادر':null"></div>
                </div>
              </div>
              <div>
                <div class="d-flex justify-content-center gap-4 my-2">
                  <div class="labelText">زبان های مکالمه من :</div>
                  <div class="d-flex flex-column">
                    <div v-if="roomateData.roomate.ownerLangs.indexOf('1') > -1">انگلیسی</div>
                    <div v-if="roomateData.roomate.ownerLangs.indexOf('2') > -1">فرانسوی</div>
                    <div v-if="roomateData.roomate.ownerLangs.indexOf('3') > -1">عربی</div>
                    <div v-if="roomateData.roomate.ownerLangs.indexOf('4') > -1">فارسی</div>
                  </div>
                </div>
              </div>
            </div>
            <div class="text-center text-warning fw-bold" v-if="roomateData.status ===2">
              اطلاعاتی برای هم خانه ثبت نشده است.
            </div>
          </div>
          <div class="col-12 infoBox" v-if="showAdvertise">
            <div class="row">
              <div class="col-2 order-0">
                <div class="d-flex flex-column">
                  <div>
                    <div class="">دسته بندی ها</div>
                    <div class="d-flex flex-column pe-5">
                      <div
                        class="category my-1" :class="item.parentId !== null?'mx-5':''" v-for="item in categories"
                        :key="item.adsCategoryId" @click="selectCategoryForFilter(item.adsCategoryId)">
                        <input class="border rounded" type="checkbox" :id="item.adsCategoryId"/>
                        <label :for="item.adsCategoryId"> {{ item.name }}</label>
                      </div>
                    </div>
                  </div>
                  <div>
                    <div class="">قیمت</div>
                    <div class="my-5 mx-3 ">
                      <veeno
                        @end="getSliderValue"
                        connect
                        :tooltips="[true, true]"
                        :handles="[0, 1000]"
                        :rtl="true"
                        :range="{
                'min': 0,
                'max': 1000
                }"
                      />
                    </div>
                  </div>
                  <!--                  <div>-->
                  <!--                    <div class="">محل</div>-->
                  <!--                    <div class="d-flex flex-column my-3">-->
                  <!--                      <div class="">-->
                  <!--                        <input class="border rounded w-100" type="text" placeholder="مکان ..." maxlength="50"-->
                  <!--                               v-model.trim="location"/>-->
                  <!--                      </div>-->
                  <!--                    </div>-->
                  <!--                  </div>-->
                  <!--                  <div class="my-2">-->
                  <!--                    <div class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2 pe-2">-->
                  <!--                      <div class="sidenavOptionText">فقط فوری</div>-->
                  <!--                      <div class="">-->
                  <!--                        <label class="switch">-->
                  <!--                          <input type="checkbox" id="onlyForce" value="onlyForce" v-model="onlyForce">-->
                  <!--                          <div class="slider round">-->
                  <!--                            <span class="on"></span>-->
                  <!--                            <span class="off"></span>-->
                  <!--                          </div>-->
                  <!--                        </label>-->
                  <!--                      </div>-->
                  <!--                    </div>-->
                  <!--                  </div>-->
                  <!--                  <div>-->
                  <!--                    <div class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2 pe-2">-->
                  <!--                      <div class="sidenavOptionText">فقط عکس دار</div>-->
                  <!--                      <div class="">-->
                  <!--                        <label class="switch">-->
                  <!--                          <input type="checkbox" id="onlyPicture" value="onlyPicture" v-model="onlyPicture">-->
                  <!--                          <div class="slider round">-->
                  <!--                            <span class="on"></span>-->
                  <!--                            <span class="off"></span>-->
                  <!--                          </div>-->
                  <!--                        </label>-->
                  <!--                      </div>-->
                  <!--                    </div>-->
                  <!--                  </div>-->
                </div>
              </div>
              <div class="col-10 order-1">
                <div class="row">
                  <div class="col-md-4 col-12" v-for="item in adsListFilter" :key="item.adsId">
                    <AdvertiseCard :advertise-data="item"/>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-6 col-12 infoBox w-auto" style="margin: 0 auto" v-if="showPersonalInformation">
            <div class="d-flex flex-column">
              <div class="friendBox">
                <div class="d-flex flex-row gap-5">
                  <div class="">نام و نام خانوادگی :</div>
                  <div class="">{{ userInfoData.name + ' ' + userInfoData.familyName }}</div>
                </div>
              </div>
              <div class="friendBox">
                <div class="d-flex flex-row">
                  <div class="p-2">
                    <div class="d-flex flex-row">
                      <div class="p-2 ">تعداد دنبال کنندگان :</div>
                      <div class="p-2 ">{{ Follower.length }}</div>
                    </div>
                  </div>
                  <div class="p-2">
                    <div class="d-flex flex-row">
                      <div class="p-2">تعداد دنبال شوندگان:</div>
                      <div class="p-2">{{ Following.length }}</div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="friendBox">
                <nav>
                  <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <button class="nav-link active" id="nav-home-tab" data-bs-toggle="tab" data-bs-target="#nav-home"
                            type="button" role="tab" aria-controls="nav-home" aria-selected="true">اطلاعات تماس
                    </button>
                    <button class="nav-link" id="nav-profile-tab" data-bs-toggle="tab" data-bs-target="#nav-profile"
                            type="button" role="tab" aria-controls="nav-profile" aria-selected="false">دنبال کنندگان
                    </button>
                    <button class="nav-link" id="nav-contact-tab" data-bs-toggle="tab" data-bs-target="#nav-contact"
                            type="button" role="tab" aria-controls="nav-contact" aria-selected="false">دنبال شوندگان
                    </button>
                    <!--                    <button class="nav-link" id="nav-interest-tab" data-bs-toggle="tab" data-bs-target="#nav-interest"-->
                    <!--                            type="button" role="tab" aria-controls="nav-interest" aria-selected="false">علاقمندی ها-->
                    <!--                    </button>-->
                  </div>
                </nav>
                <div class="tab-content" id="nav-tabContent">
                  <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
                    <div class="p-3 my-3">
                      <div class="d-flex flex-row gap-5">
                        <div class="">شماره تماس :</div>
                        <div class="">{{ userInfoData.mobile }}</div>
                      </div>
                    </div>
                    <div class="p-3 my-3">
                      <div class="d-flex flex-row gap-5">
                        <div class="">ایمیل :</div>
                        <div class="">{{ userInfoData.email }}</div>
                      </div>
                    </div>
                  </div>
                  <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">
                    <div class="col-12 friendBox" v-for="(item,index) in Follower" v-if="Follower.length"
                         :key="index">
                      <div class="d-flex flex-row">
                        <div class="">
                          <!--                          <img :src="`https://pplusapi.simagar.com/${Follower.userInfo.selfieFileData}`"-->
                          <!--                               class="pictureFriend img-fluid"/>-->
                        </div>
                        <div class="">
                          <div class="d-flex flex-column px-4">
                            <div class="friendName">{{ item.userInfo.name + ' ' + item.userInfo.familyName }}</div>
                            <div class="friendCaption">{{ item.userInfo.mobile }}</div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="col-12 friendBox" v-if="Follower.length === 0">
                      <p class="text-warning fw-bold">هیچ دنبال کننده ای ندارد.</p>
                    </div>
                  </div>
                  <div class="tab-pane fade" id="nav-contact" role="tabpanel" aria-labelledby="nav-contact-tab">
                    <div class="col-12 friendBox" v-for="(item,index) in Following" v-if="Following.length"
                         :key="index">
                      <div class="d-flex flex-row">
                        <div class="">
                          <!--                          <img :src="`https://pplusapi.simagar.com/${Follower.userInfo.selfieFileData}`"-->
                          <!--                               class="pictureFriend img-fluid"/>-->
                        </div>
                        <div class="">
                          <div class="d-flex flex-column px-4">
                            <div class="friendName">{{ item.userInfo.name + ' ' + item.userInfo.familyName }}</div>
                            <div class="friendCaption">{{ item.userInfo.mobile }}</div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="col-12 friendBox" v-if="Following.length === 0">
                      <p class="text-warning fw-bold">هیچ دنبال شونده ای ندارد.</p>
                    </div>
                  </div>
                  <div class="tab-pane fade" id="nav-interest" role="tabpanel" aria-labelledby="nav-interest-tab">...
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-2 bg-white order-1 d-md-block d-none">
        <div class="row p-0 text-center">
          <div class="col-12">{{ userInfoData.name + ' ' + userInfoData.familyName }}</div>
          <div class="col-12 my-3">
            <img class="img-fluid infoPicture" :src="`https://pplusapi.simagar.com/${userInfoData.selfieFileData}`">
          </div>
          <div class="col-12">
            <div class="d-flex justify-content-center gap-4 my-2">
              <div>شماره تلفن :</div>
              <div>{{ userInfoData.mobile }}</div>
            </div>
          </div>
          <div class="col-12 my-3">
            <div class="d-flex justify-content-center gap-4 my-2">
              <div>ایمیل :</div>
              <div>{{ userInfoData.email }}</div>
            </div>
          </div>
          <div class="col-12">
            <div class="d-flex justify-content-center gap-4 my-2">
              <div>نوع کاربری :</div>
              <div v-html="userInfoData.role==='Admin'?'ادمین':'کاربر معمولی'"></div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import AdvertiseCard from "../../components/AdvertiseCard";
import veeno from 'veeno';
import 'nouislider/distribute/nouislider.min.css';
import 'swiper/css/swiper.css'
import SocialPost from "../../components/SocialPost";
import {Swiper, SwiperSlide} from "vue-awesome-swiper";

export default {
  name: "UserInfo",
  components: {
    SocialPost, AdvertiseCard, veeno, Swiper,
    SwiperSlide
  },
  layout: 'PoshtebamPlusLayout',

  data() {
    return {
      showRoommate: false,
      showAdvertise: false,
      showPersonalInformation: true,
      showPost: false,
      userInfoData: {},
      onlyForce: false,
      onlyPicture: true,
      minFilterValue: null,
      maxFilterValue: null,
      location: '',
      Follower: [],
      Following: [],
      ClientIp: '',
      browserName: '',
      isFollowing: false,
      userPostData: [],
      userAds: [],
      roomateData: {},
      swiperOptionTop: {
        loop: false,
        loopedSlides: 5,
        spaceBetween: 10,
        slidesPerView: 1,
        navigation: {
          nextEl: '.swiper-button-next',
          prevEl: '.swiper-button-prev'
        }
      },
      categories: [],
      categoryFilter: []
    }
  },
  methods: {
    getAllCategory() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      })
      this.$axios.post(`AdsCategory/GetAll`, null, {
       
      }).then((response) => {
        if (response.status === 200) {
          this.categories = response.data
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    toggleTab(tabName) {
      if (tabName === 'Roommate') {
        this.showPersonalInformation = false;
        this.showAdvertise = false
        this.showRoommate = true;
        this.showPost = false;

      } else if (tabName === 'Advertise') {
        this.showRoommate = false;
        this.showPersonalInformation = false
        this.showAdvertise = true;
        this.showPost = false;

      } else if (tabName === 'PersonalInformation') {
        this.showRoommate = false;
        this.showAdvertise = false;
        this.showPersonalInformation = true;
        this.showPost = false;

      } else if (tabName === 'post') {
        this.showRoommate = false;
        this.showAdvertise = false;
        this.showPersonalInformation = false;
        this.showPost = true;
      }
    },
    userInfoId(userId) {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      })
      this.$axios.post(`Users/Get`, null, {
        
        params: {id: userId}
      }).then((response) => {
        if (response.status === 200) {
          this.userInfoData  = response.data
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    getSliderValue(value) {
      this.minFilterValue = value.positions[0];
      this.maxFilterValue = value.positions[1];
    },
    goAdvertiseDetail(AdvertiseId) {
      this.$router.push({path: '/Migration/AdvertiseDetail', query: {AdvertiseId}})
    },
    getFollowing() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      })
      this.$axios.post(`Following/GetByUserId`, null, {
        
      }).then((response) => {
        if (response.status === 200) {
          this.Following = response.data;
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    getFollower() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      })
      this.$axios.post(`Follower/GetByUserId`, null, {
      
      }).then((response) => {
        if (response.status === 200) {
          this.Follower = response.data;
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    logUser(ip, browser) {
      this.$axios.post(`Log/Create`, {
        ip: ip,
        browserOS: browser,
        userId: parseInt(this.$route.query.userId),
        viewUserId: this.$auth.userId ? this.$auth.userId : null,
        url: this.$nuxt.$route.fullPath
      }, {
       
      }).then().catch((error) => {
        console.log(error)
      })
    },
    checkPrivacy() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      })
      const following = parseInt(this.$nuxt.$route.query.userId);
      this.$axios.post(`Following/IsFollowing`, {
        userId: this.$auth.userId,
        followingUserId: following
      }, {
      
      }).then((response) => {
        if (response.status === 200) {
          this.isFollowing = response.data
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    getUserPostData() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      })
      this.$axios.post(`Post/GetByUserId`, null, {
        params: {
          userId: parseInt(this.$nuxt.$route.query.userId)
        }
      }).then((response => {
        if (response.status === 200) {
          this.userPostData = response.data.posts
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
        }
      })).catch((error) => {
        console.log(error)
      })
    },
    getRoommateUserInfo() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      })
      this.$axios.post(`Roomate/GetByUserId`, null, {
       
        params: {
          userId: parseInt(this.$nuxt.$route.query.userId)
        }
      }).then((response) => {
        if (response.status === 200) {
          this.roomateData = response.data
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    sendFollowingRequest() {
      this.$axios.post('FollowRequest/Create', null, {
       
        params: {
          followingUserId: parseInt(this.$nuxt.$route.query.userId)
        }
      }).then((response) => {
        if (response.status === 200) {
          this.$toast.success('درخواست دوستی با موفقیت ارسال شد.')
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    getUserAds(userId) {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      })
      this.$axios.post(`Ads/GetByUserId`, null, {
        params: {
          userId: userId
        },
      
      }).then((response) => {
        if (response.status === 200) {
          this.userAds = response.data
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    selectCategoryForFilter(categoryId) {
      if (this.categoryFilter.includes(categoryId)) {
        this.categoryFilter.splice(this.categoryFilter.indexOf(categoryId), 1)
      } else {
        this.categoryFilter.push(categoryId)
      }
    }
  },
  mounted() {
    this.userInfoId(this.$route.query.userId)
    this.getUserAds(this.$route.query.userId)
    this.getAllCategory();
    this.getFollowing();
    this.getRoommateUserInfo()
    this.getFollower();
    this.$axios.get('https://api.ipify.org?format=json').then((res) => {
      this.ClientIp = res.data.ip
    })
    let userAgent = navigator.userAgent;


    if (userAgent.match(/chrome|chromium|crios/i)) {
      this.browserName = "chrome";
    } else if (userAgent.match(/firefox|fxios/i)) {
      this.browserName = "firefox";
    } else if (userAgent.match(/safari/i)) {
      this.browserName = "safari";
    } else if (userAgent.match(/opr\//i)) {
      this.browserName = "opera";
    } else if (userAgent.match(/edg/i)) {
      this.browserName = "edge";
    } else {
      this.browserName = "No browser detection";
    }

    setTimeout(() => {
      this.logUser(this.ClientIp, this.browserName);
    }, 1000)

    this.checkPrivacy();
    this.getUserPostData();
  },
  computed: {
    adsListFilter() {
      return (this.categoryFilter.length > 0) ?
        this.userAds.filter(function (d) {
          return this.indexOf(d.categoryId) > -1;
        }, this.categoryFilter) :
        this.userAds;
    }
  },
}
</script>

<style scoped>
.category {
  cursor: pointer;
}

.labelText {
  color: #545454;
  font-size: 14px;
  cursor: pointer;
}

.roommateStyle {
  width: auto;
  margin: 0 auto;
}

input::placeholder {
  font-size: 12px;
  color: #cccccc;
}

.noUi-horizontal {
  height: 8px;
}

.noUi-handle {
  border: 1px solid #D9D9D9;
  border-radius: 3px;
  background: #FFF;
  cursor: default;
  box-shadow: inset 0 0 1px #FFF;
}

.titleTab {
  color: #737373;
  background: #cccccc;
  font-weight: 400;
  line-height: 1.5;
  cursor: pointer;
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.activeMainTab {
  color: #486abe;
  padding: 8px;
  border: 1px solid #486abe;
  background: #fff;
  line-height: 1.5;
}

.infoBox {
  background: #fff none repeat scroll 0 0;
  border: 1px solid #eaf1f6;
  padding: 25px;
  width: 100%;
  transition: all 0.15s linear 0s;
  margin-bottom: 0;
  margin-top: 35px;
  min-width: 50vw;
}

.infoPicture {
  margin: 0 auto;
  max-width: 15rem;
  min-width: 15rem;
  vertical-align: middle;
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
  -webkit-transition: .4s;
  transition: .4s;
}

.slider:before {
  position: absolute;
  content: "";
  height: 26px;
  width: 26px;
  left: 4px;
  bottom: 4px;
  background-color: white;
  -webkit-transition: .4s;
  transition: .4s;
}

input:checked + .slider {
  background-color: #088dcd;
}

input:focus + .slider {
  box-shadow: 0 0 1px #2196F3;
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

.friendBox {
  background: #fff none repeat scroll 0 0;
  border: 1px solid #eaf1f6;
  padding: 25px;
  width: 100%;
  transition: all 0.15s linear 0s;
  margin-bottom: 0;
  margin-top: 35px;
}

.friendBox:hover {
  box-shadow: 0 8px 8px #e1e8ec;
  border-color: transparent;
}
</style>
