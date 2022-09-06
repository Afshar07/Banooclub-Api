<template>
  <div class="m-0 p-0 row col-12 bg-white header_in_add_post"
       style="border: 1px solid #ebe9e6;border-radius: 1rem;height: 450px">

    <input type="checkbox" ref="AvatarModal" id="my-modal-4" class="tw-modal-toggle"/>
    <label for="my-modal-4" class="tw-modal cursor-pointer">
      <label class="tw-modal-box tw-max-w-3xl relative" for="">
        <SelectAvatar @SendAvatarPic="UploadAvatar" @SetUserPic="callUserInputMethod"></SelectAvatar>
        <!--        <div class="tw-w-full">-->
        <!--          <div class="tw-w-full tw-flex tw-items-center tw-justify-center">-->
        <!--          <img src="/defaultBanner.jpg" class="tw-w-20 tw-h-20 tw-rounded-full tw-object-cover" alt="">-->
        <!--          </div>-->
<!--                  <div   @click="callInputMethod" class="tw-w-full tw-cursor-pointer tw-bg-stone-200 tw-rounded tw-border-dashed tw-border-2 tw-flex tw-items-center tw-justify-center tw-h-20 my-4">-->
<!--                    <UploadIcon  class="tw-fill-pink-400"></UploadIcon>-->
<!--                  </div>-->
        <!--          <div class="tw-w-full tw-flex tw-items-center tw-flex-wrap tw-gap-2 tw-justify-start ">-->
        <!--            <img v-for="i in 8" src="/defaultBanner.jpg" class="tw-w-20 tw-h-20 my-2 tw-rounded-full hover:tw-transform hover:tw-scale-110 tw-transition-all tw-cursor-pointer tw-rounded tw-object-cover"  alt="">-->

        <!--          </div>-->


        <!--          <div class="tw-w-full my-2 tw-flex tw-items-center tw-justify-center">-->
        <!--            <button class="bg-purple rounded text-white tw-w-1/4 p-2 tw-cursor-pointer"> تایید</button>-->
        <!--          </div>-->
        <!--        </div>-->
      </label>
    </label>
    <input type="checkbox" ref="BannerModal" id="my-modal-5" class="tw-modal-toggle"/>
    <label for="my-modal-5" class="tw-modal cursor-pointer">
      <label class="tw-modal-box tw-max-w-3xl relative" for="">
        <SelectBanner @SetBannerPic="callInputMethod"></SelectBanner>
<!--        <div class="tw-w-full">-->
<!--          <img src="/defaultBanner.jpg" class="tw-w-full tw-h-20 tw-rounded tw-object-cover" alt="">-->

<!--          <div class="tw-w-full tw-flex tw-items-center tw-flex-wrap tw-justify-start ">-->
<!--            <img v-for="i in 8" src="/defaultBanner.jpg"-->
<!--                 class="tw-w-1/4 tw-h-20 my-2 hover:tw-transform hover:tw-scale-110 tw-transition-all tw-cursor-pointer tw-rounded tw-object-cover"-->
<!--                 alt="">-->

<!--          </div>-->

<!--          &lt;!&ndash;              @click="callUserInputMethod"&ndash;&gt;-->
<!--          <div class="tw-w-full my-2 tw-flex tw-items-center tw-justify-center">-->
<!--            <button class="bg-purple rounded text-white tw-w-1/4 p-2 tw-cursor-pointer"> تایید</button>-->
<!--          </div>-->
<!--        </div>-->
      </label>
    </label>
    <!-- Modal -->
    <div class="m-0 p-0 row col-12">
      <div class="m-0 p-0 row col-md-12 mb-md-4 d-md-block" :class="isChatLayout ? 'd-none' : 'd-block'">
        <div class="w-100 col-12 BannerPhoto d-flex flex-lg-column justify-content-end"
             style="height: 285px; background-position: center;border-top-right-radius: 1rem ;border-top-left-radius: 1rem ;"
             :style="InlineBg">
          <div class="row col-12 m-0 p-0">
            <div class="col-lg-12 m-0 col-12 justify-content-lg-start justify-content-end"
                 :class="{ TopGap: this.$route.query.userId }">
              <div
                class="row m-0 col-12 h-100 d-flex flex-column justify-content-lg-center justify-content-end align-items-center position-relative profile_section">
                <div class="d-flex align-items-center justify-content-center">
                  <img
                    :src="userAvatar"
                    class="UserPhoto rounded-circle mb-md-3"
                    alt=""
                    style="width: 120px;height: 120px;object-fit: contain;box-shadow: inset 0 0 20px white"
                  />
                </div>
                <div class="d-flex flex-column align-items-center justify-content-center">
                  <div class="d-flex flex-column align-items-center" v-if="!$route.params.slug">
                    <h1 v-if="$auth.user && $auth.user.baseData && $auth.user.baseData.name" class="text-center mb-0"
                        style="white-space: nowrap">
                      {{ $auth.user.baseData.name + " " + $auth.user.baseData.familyName }}
                    </h1>
                    <small class="text-primary tw-cursor-pointer" @click="goToUserProfile($auth.user.baseData)"
                           style="text-align: center">
                      {{ $auth.user.baseData.userName }}@
                    </small>
                    <div class="d-flex mb-4" style="position: relative;top: 16px;">
                      <div class="mx-2 d-flex" style="align-items: center">
                        <div class="tw-pl-2" style="font-size: 13px">
                          {{ $auth.user.userInfo.followersCount }}
                        </div>
                        <div class="tw-pl-2" style="font-size: 10px;font-weight: bold">دنبال کننده</div>

                        <FollowerIcon style="fill: #0d6efd;width: 24px;height: 24px;"/>

                      </div>
                      <div class="mx-2 d-flex" style="align-items: center">

                        <FollowingIcon style="fill: #0d6efd;width: 24px;height: 24px;"/>
                        <div class="tw-pr-2" style="font-size: 13px">
                          {{ $auth.user.userInfo.followingsCount }}
                        </div>
                        <div class="tw-pr-2" style="font-size: 10px;font-weight: bold">دنبال شونده</div>

                      </div>
                    </div>

                  </div>
                  <div class="d-flex flex-column align-items-center"
                       v-else-if="$route.params.slug && getUserDetails && getUserDetails.baseData">
                    <h1 class="text-center mb-0" style="white-space: nowrap">
                      {{ getUserDetails.baseData.name + " " + getUserDetails.baseData.familyName }}
                    </h1>
                    <small class="text-primary tw-cursor-pointer" @click="goToUserProfile(getUserDetails.baseData)"
                           style="text-align: center">
                      {{ getUserDetails.baseData.userName }}@
                    </small>
                    <div class="d-flex mb-4" v-if="$route.params.slug && getUserDetails"
                         style="position: relative;top: 16px;">
                      <div class="mx-2 d-flex" style="align-items: center">
                        <div class="tw-pl-2" style="font-size: 13px">
                          {{ getUserDetails.baseData.followersCount }}
                        </div>
                        <div class="tw-pl-2" style="font-size: 10px;font-weight: bold">دنبال کننده</div>

                        <FollowerIcon style="fill: #0d6efd;width: 24px;height: 24px;"/>

                      </div>
                      <div class="mx-2 d-flex" style="align-items: center">

                        <FollowingIcon style="fill: #0d6efd;width: 24px;height: 24px;"/>
                        <div class="tw-pr-2" style="font-size: 13px">
                          {{ getUserDetails.baseData.followingsCount }}
                        </div>
                        <div class="tw-pr-2" style="font-size: 10px;font-weight: bold">دنبال شونده</div>

                      </div>
                    </div>
                  </div>


                </div>
              </div>
            </div>
            <div class="col-lg-3 col-12 mb-3 d-flex align-items-end justify-content-center gap-2 text-white row">

            </div>
          </div>
        </div>
      </div>
      <ul class="nav nav-pills align-items-end profile_tabs tw-overflow-x-auto px-3" id="pills-tab" role="tablist"
          style="z-index: 99;">
        <li class="nav-item" style="margin: 0 !important;">
          <button @click="$emit('SetActiveTab',1)" class="nav-link active customFontSize" id="posts-home-tab"
                  data-bs-toggle="pill" data-bs-target="#posts-home"
                  type="button" role="tab" aria-controls="posts-home" aria-selected="true">
            <span v-if="!$route.params.slug">پست های من</span>
            <span class="d-flex" v-else-if="$route.params.slug && getUserDetails && getUserDetails.baseData">
              <span class="px-1">پست ها</span>
              <svg
                v-if="getUserDetails && getUserDetails.userInfo && getUserDetails.userInfo.userSetting && getUserDetails.userInfo.userSetting.isPrivatePost"
                xmlns="http://www.w3.org/2000/svg" class="tw-h-4 tw-w-4" viewBox="0 0 20 20" fill="#ff6f9e">
                <path fill-rule="evenodd"
                      d="M5 9V7a5 5 0 0110 0v2a2 2 0 012 2v5a2 2 0 01-2 2H5a2 2 0 01-2-2v-5a2 2 0 012-2zm8-2v2H7V7a3 3 0 016 0z"
                      clip-rule="evenodd"/>
              </svg>
            </span>

          </button>
        </li>
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button @click="$emit('SetActiveTab',2)" class="nav-link  customFontSize" id="services-tab"
                  data-bs-toggle="pill" data-bs-target="#services" type="button" role="tab" aria-controls="services"
                  aria-selected="true">
            <span v-if="!$route.params.slug && $auth.user && $auth.user.baseData ">خدمات من</span>
            <span v-else-if="$route.params.slug && getUserDetails && getUserDetails.baseData">خدمات</span>
          </button>
        </li>

        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button @click="$emit('SetActiveTab',3)" class="nav-link customFontSize" id="pill-Ads" data-bs-toggle="pill"
                  data-bs-target="#Ads"
                  type="button" role="tab" aria-controls="pill-Ads" aria-selected="false">
            <span v-if="!$route.params.slug">
               آگهی های من
            </span>
            <span class="d-flex" v-else>
              <span class="px-1">آگهی ها </span>
            </span>
          </button>
        </li>
        <li v-if="!$route.params.slug" class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button @click="$emit('SetActiveTab',4)" class="nav-link customFontSize" id="pills-profile-tab"
                  data-bs-toggle="pill" data-bs-target="#pills-profile"
                  type="button" role="tab" aria-controls="pills-profile" aria-selected="false">
            <span v-if="!$route.params.slug">
               دوستان من
            </span>
          </button>
        </li>
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button @click="$emit('SetActiveTab',5)" class="nav-link customFontSize" id="pills-contact-tab"
                  data-bs-toggle="pill" data-bs-target="#pills-contact"
                  type="button" role="tab" aria-controls="pills-contact" aria-selected="false">
            <span v-if="!$route.params.slug">
              گالری من
            </span>
            <span class="d-flex" v-else>
              <span class="px-1">گالری</span>
              <svg
                v-if="getUserDetails && getUserDetails.userInfo && getUserDetails.userInfo.userSetting && getUserDetails.userInfo.userSetting.isPrivateGalleryFriend"
                xmlns="http://www.w3.org/2000/svg" class="tw-h-4 tw-w-4" viewBox="0 0 20 20" fill="#ff6f9e">
                <path fill-rule="evenodd"
                      d="M5 9V7a5 5 0 0110 0v2a2 2 0 012 2v5a2 2 0 01-2 2H5a2 2 0 01-2-2v-5a2 2 0 012-2zm8-2v2H7V7a3 3 0 016 0z"
                      clip-rule="evenodd"/>
              </svg>
            </span>
          </button>
        </li>

        <li class="d-flex mb-1 align-items-end" style="margin-right: auto">
          <div class="add-btn" v-if="$route.params.slug && getUserDetails">
            <button
              @click="Follow"
              class="p-md-2 mx-1 tw-rounded-lg d-flex"
              style="cursor: pointer;background-color: #2563eb;color: white;font-size: 13px;align-items: center;"
              v-if="getUserDetails.followStatus === 1"
            >
              <AddFriendIcon class="tw-ml-2" style="width: 24px; height: 24px;"/>
              افزودن به دوستان
            </button>
            <button
              class="p-md-2 mx-1 p-1 tw-rounded-lg d-flex"
              style="cursor: auto;background-color: rgb(235 115 37);color: white;font-size: 13px;align-items: center;"
              v-else-if="getUserDetails.followStatus === 2"
            >
              <AddFriendIcon class="tw-ml-2" style="width: 24px; height: 24px;"/>
              درخواست شده
            </button>
            <button
              class="p-md-2 px-md-3 p-1 mx-1 tw-rounded-lg d-flex"
              style="background-color: #85ffdd;color: #f5447d;font-size: 13px;align-items: center;cursor: auto"
              v-else-if="getUserDetails.followStatus === 3"
            >
              <CheckIcon class="tw-ml-2" style="width: 18px; height: 24px;fill: #f5447d;"/>
              دوست من
            </button>
          </div>
          <div class="add-btn" v-if="$route.params.slug && getUserDetails ">
            <button
              class="py-md-2 px-md-3 p-1 mx-1 tw-rounded-lg d-flex"
              style="cursor: pointer;background-color: #ff6f9e;color: white;font-size: 13px;align-items: center;"
              @click="SendFirstMessage(getUserDetails)"
            >
              <svg class="tw-ml-3" fill="white" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 30 30" width="24px"
                   height="24px">
                <path
                  d="M 15 3 C 7.832 3 2 7.9188438 2 13.964844 C 2 17.461844 3.9979687 20.749828 7.2929688 22.798828 C 7.1919688 22.995828 7.0672656 23.188812 6.9472656 23.382812 C 6.3672656 24.318812 5.2265625 25.587891 5.2265625 25.587891 C 4.6885625 26.364891 5.3188437 27.205187 6.3398438 27.117188 C 7.4008437 27.026187 8.2667344 26.798594 9.1777344 26.433594 C 10.408734 25.939594 11.635781 25.441375 12.550781 24.734375 C 12.607647 24.74354 12.665638 24.74739 12.722656 24.755859 C 14.240757 25.979238 16.26846 26.734375 18.5 26.734375 C 19.027 26.734375 19.554172 26.691469 20.076172 26.605469 C 20.555172 26.883469 21.168078 27.23175 21.705078 27.46875 C 22.475078 27.80775 23.162859 28.138219 24.380859 28.324219 C 25.308859 28.466219 26.043922 27.63875 25.544922 26.84375 C 24.970922 26.29975 24.531859 25.625016 24.255859 24.791016 C 25.996859 23.412016 27 21.465188 27 19.367188 C 27 19.01627 26.96127 18.673913 26.90625 18.335938 C 27.604473 16.996387 28 15.520442 28 13.964844 C 28 7.9188437 22.168 3 15 3 z M 18.5 14 C 22.084 14 25 16.408188 25 19.367188 C 25 21.183187 23.892109 22.863328 22.037109 23.861328 C 21.813109 23.982328 21.716547 24.247516 21.810547 24.478516 C 21.950547 24.823516 22.110109 25.139922 22.287109 25.419922 C 21.687109 25.215922 21.117156 24.931469 20.660156 24.605469 C 20.577156 24.546469 20.481859 24.515625 20.380859 24.515625 C 20.343859 24.515625 20.305531 24.518344 20.269531 24.527344 C 19.687531 24.663344 19.092 24.732422 18.5 24.732422 C 14.916 24.732422 12 22.324234 12 19.365234 C 12 16.407234 14.916 14 18.5 14 z M 15.412109 18.3125 A 1.087 1.087 0 1 0 15.412109 20.486328 A 1.087 1.087 0 1 0 15.412109 18.3125 z M 18.537109 18.3125 A 1.087 1.087 0 1 0 18.537109 20.486328 A 1.087 1.087 0 1 0 18.537109 18.3125 z M 21.587891 18.3125 A 1.087 1.087 0 1 0 21.587891 20.486328 A 1.087 1.087 0 1 0 21.587891 18.3125 z"/>
              </svg>
              ارسال پیام
            </button>
          </div>
          <div class="add-btn" v-if="!$route.params.slug">

            <button
              style="cursor: pointer;background-color: #e2a7ff"
              type="button"
              class="btn ChangePhotoBtn d-flex align-items-center py-md-2 px-md-3 mx-1"
            >

              <label for="my-modal-5" class="m-0">
                <small class="text-white" style="font-size: 10px;">ویرایش عکس جلد</small>
              </label>


              <input
                ref="file"
                class="InputEditPhoto d-none"
                type="file"
                @change="InputEditPhoto"
              />

            </button>
          </div>
          <div class="add-btn" v-if="!$route.params.slug">
            <button
              style="cursor: pointer;background-color: #e2a7ff"
              class="btn ChangePhotoBtn d-flex align-items-center py-md-2 px-md-3 mx-1"
            >
              <label for="my-modal-4" class="m-0 tw-w-full tw-h-full">
                <small class="text-white tw-text-[10px]">ویرایش عکس پروفایل</small>
              </label>
              <input
                ref="InputUserEditPhoto"
                class="InputUserEditPhoto d-none"
                type="file"
                @change="InputEditUserPhoto"
              />

            </button>
          </div>
        </li>
      </ul>
    </div>
  </div>

</template>

<script>
import WhiteCropIcon from "./Icons/WhiteCropIcon";
import {mapGetters} from "vuex";
import AddFriendIcon from "../components/Icons/AddFriendIcon";
import FollowerIcon from "../components/Icons/FollowerIcon"
import FollowingIcon from "../components/Icons/FollowingIcon";
import CheckIcon from "../components/Icons/CheckIcon"
import UploadIcon from "@/components/Icons/UploadIcon";
import SelectAvatar from '@/components/SelectAvatar';
import SelectBanner from "@/components/SelectBanner";

export default {
  name: "CustomHeader",
  components: {WhiteCropIcon, UploadIcon, SelectBanner,AddFriendIcon, FollowerIcon, FollowingIcon, SelectAvatar, CheckIcon},
  props: {
    isChatLayout: {
      type: Boolean,
      required: false,
      default: false,
    },
  },
  computed: {
    InlineBg() {
      const base = this.BaseUrl;
      if (!this.$route.params.slug && this.$auth.user && this.$auth.user.baseData && this.$auth.user.baseData.banner) {
        return {
          backgroundImage: `url(${this.$auth.user.baseData.banner ? base + this.$auth.user.baseData.banner : "/defaultBanner.jpg"})`,
          backgroundPosition: "center",
          backgroundSize: "cover",
          backgroundRepeat: "no-repeat",
        };
      } else if (this.$route.params.slug && this.getUserDetails && this.getUserDetails.baseData) {
        return {
          backgroundImage: `url(${this.getUserDetails.baseData.banner ? base + this.getUserDetails.baseData.banner : "/defaultBanner.jpg"})`,
          backgroundPosition: "center",
          backgroundSize: "cover",
          backgroundRepeat: "no-repeat",
        }
      }

    },
    userAvatar() {
      if (!this.$route.params.slug && this.$auth.user && this.$auth.user.userInfo && this.$auth.user.userInfo.selfieFileData) {
        return this.$auth.user.userInfo.selfieFileData
          ? this.BaseUrl + this.$auth.user.userInfo.selfieFileData
          : "/defaultUser.png";
      } else if (this.$route.params.slug && this.getUserDetails && this.getUserDetails.baseData) {
        return this.getUserDetails.baseData.selfie ? this.BaseUrl + this.getUserDetails.baseData.selfie : "/defaultUser.png";
      }
    },
    BaseUrl() {
      return process.env.pic;
    },
    ...mapGetters("userDetails", ["getUserDetails"])
  },
  methods: {

    async SendFirstMessage(user) {
      console.log(user)
      this.$router.push({
        path: '/Social/Chat',
        query: {userId: user.userInfo.userId, Photo: user.userInfo.selfieFileData}
      })
    },
    async goToUserProfile(user) {
      try {
        this.$router.push({path: `/user/${user.userName}/posts`});
      } catch (e) {
        console.log(e)
      }
    },
    callInputMethod() {
      this.$refs.file.click();
    },
    UploadAvatar(Avatar){
      setTimeout(async () => {
        try {
          await this.$repositories.updateUserDetails.updateUserDetails({
            avatar: Avatar,
          });
          await this.$auth.fetchUser()
          this.$fetch()
        } catch (error) {
          console.log(error);
        } finally {
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
        }
      }, 300);
    },
    InputEditPhoto(e) {
      this.$nuxt.$loading.start();
      const file = e.target.files[0];
      this.newImage = URL.createObjectURL(file);
      const that = this;
      const reader = new FileReader();
      reader.onload = (function (theFile) {
        return function () {
          const binaryData = reader.result;
          that.bannerImage = window.btoa(binaryData);
        };
      })(file);
      reader.readAsBinaryString(file);

      setTimeout(async () => {
        try {
          await this.$repositories.updateUserDetails.updateUserDetails({
            bannerFileData: this.bannerImage,
          });
          this.$refs.BannerModal.click();
          await this.$auth.fetchUser()
          this.$fetch()
          // this.getUserInfo();
        } catch (error) {
          console.log(error);
        } finally {
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
        }
      }, 300);
    },
    callUserInputMethod() {
      this.$refs.InputUserEditPhoto.click();

    },
    InputEditUserPhoto(e) {

      this.$nuxt.$loading.start();
      const file = e.target.files[0];
      this.newImageUser = URL.createObjectURL(file);
      const that = this;
      const reader = new FileReader();
      reader.onload = (function (theFile) {
        return function () {
          const binaryData = reader.result;
          that.userImage = window.btoa(binaryData);
        };
      })(file);
      reader.readAsBinaryString(file);

      setTimeout(async () => {
        try {
          await this.$repositories.updateUserDetails.updateUserDetails({
            selfieFileData: this.userImage,
          });
          this.$refs.AvatarModal.click();
          await this.$auth.fetchUser();
          this.$fetch()

          // this.getUserInfo();
        } catch (error) {
          console.log(error);
        } finally {
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
        }
      }, 1000);
    },
    async Follow() {
      try {
        const response =
          await this.$repositories.createAFollowRequest.createAFollowRequest(
            this.getUserDetails.userInfo.userId
          );
        if (response.data) {
          this.$toast.success("درخواست دوستی شما ارسال شد");
          this.$nuxt.refresh()
        } else {
          this.$toast.error("عملیات قابل اجرا نیست");
        }
      } catch (error) {
        console.log(error);
      }
    },


  }

}
</script>

<style scoped>
.customFontSize {
  font-size: 13px;
}

@media (max-width: 375px) {
  .profile_section {
    top: 150px;
  }
}

@media (min-width: 376px) and (max-width: 768px) {
  .profile_section {
    top: 150px;
  }
}

@media (min-width: 769px) {
  .profile_section {
    top: 158px;
  }
}

@media (max-width: 425px) {
  .profile_tabs {
    position: relative;
    top: 18px;
  }

}


/*.header_in_add_post {*/
/*  border-radius: 0 !important;*/
/*}*/

.follower_count {
  padding: 5px 9px;
  background: rgba(42, 65, 232, 0.08);
  border-radius: 30px;
  font-weight: 500;
  font-size: 12px;
  margin-right: 3px;
  color: #2a41e8;
}

</style>
