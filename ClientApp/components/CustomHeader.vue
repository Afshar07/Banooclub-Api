<template>
  <div class="m-0 p-0 row col-12 bg-white header_in_add_post"
       style="border: 1px solid #ebe9e6;border-radius: 1rem;height: 450px">
    <div class="m-0 p-0 row col-12">
      <div
        class="m-0 p-0 row col-md-12 mb-md-4 d-md-block"
        :class="isChatLayout ? 'd-none' : 'd-block'"
      >
        <div
          class="w-100 col-12 BannerPhoto d-flex flex-lg-column justify-content-end"
          style="height: 285px; background-position: center;border-top-right-radius: 1rem ;border-top-left-radius: 1rem ;"
          :style="InlineBg"
        >
          <div class="row col-12 m-0 p-0">
            <div class="col-lg-3 my-3 col-12 d-flex mb-md-3 align-items-center justify-content-center justify-content-md-start align-items-md-end">
              <div class="d-flex flex-wrap gap-3 change_pic_div">
                <button
                  v-if="!this.$route.params.id"
                  style="cursor: pointer;background-color: #49515c"
                  class="btn ChangePhotoBtn d-flex align-items-center gap-2"
                  @click="callInputMethod"
                >
                  <WhiteCropIcon class="ChangePhotoIcon" fill="white" style="height: 20px; width: 20px;"/>
                  <!--                        <font-awesome-icon icon="camera" style="font-size: 12px" />-->
                  <small class="text-white" style="font-size: 12px;">ویرایش عکس جلد</small>
                  <form>
                    <input
                      ref="file"
                      class="InputEditPhoto d-none"
                      type="file"
                      @change="InputEditPhoto"
                    />
                  </form>
                </button>
                <button
                  v-if="!this.$route.params.id"
                  style="cursor: pointer;background-color: #49515c"
                  class="btn ChangePhotoBtn d-flex align-items-center gap-2"
                  @click="callUserInputMethod"
                >
                  <!--                        <font-awesome-icon icon="camera" style="font-size: 12px" />-->
                  <WhiteCropIcon class="ChangePhotoIcon" fill="white" style="height: 20px; width: 20px;"/>
                  <small class="text-white" style="font-size: 12px">ویرایش عکس پروفایل</small>
                  <form>
                    <input
                      ref="InputUserEditPhoto"
                      class="InputUserEditPhoto d-none"
                      type="file"
                      @change="InputEditUserPhoto"
                    />
                  </form>
                </button>
              </div>
            </div>
            <div class="col-lg-6 m-0 col-12 justify-content-lg-start justify-content-end" :class="{ TopGap: this.$route.query.userId }">
              <div class="row m-0 col-12 h-100 d-flex flex-column justify-content-lg-center justify-content-end align-items-center position-relative profile_section">
                <div class="d-flex align-items-center justify-content-center">
                  <img
                    :src="userAvatar"
                    class="UserPhoto rounded-circle mb-md-3"
                    alt=""
                    style="width: 120px;height: 120px;"
                  />
                </div>
                <div class="d-flex flex-column align-items-center justify-content-center">
                  <h1 class="text-center" style="white-space: nowrap" v-if="!$route.params.id">
                    {{$auth.user.baseData.name + " " + $auth.user.baseData.familyName }}
                  </h1>
                  <h1 class="text-center" style="white-space: nowrap" v-else-if="$route.params.id && getUserDetails && getUserDetails.baseData">
                    {{getUserDetails.baseData.name + " " + getUserDetails.baseData.familyName }}
                  </h1>
                  <p v-if="!$route.params.id" style="color: #9c9c9c;">
                    {{$auth.user.userInfo.userSetting.bio }}
<!--                    <nuxt-link class="px-2 text-decoration-none" to="/social/accountsetting/editprofilebasic">ویرایش</nuxt-link>-->
                  </p>
                  <p  v-else-if="$route.params.id && getUserDetails && getUserDetails.baseData">
                    {{getUserDetails.userInfo.userSetting.bio }}
                  </p>

                </div>
              </div>
            </div>
            <div class="col-lg-3 col-12 mb-3 d-flex align-items-end justify-content-center gap-2 text-white row">
              <div class="add-btn">
                <buttton
                  @click="Follow"
                  class="p-1 tw-rounded-lg"
                  style="cursor: pointer;background-color: #2563eb;"
                  v-if="$route.params.id && getUserDetails && getUserDetails.followButton"
                >
                  دنبال کردن
                </buttton>
              </div>
            </div>
          </div>
        </div>
      </div>
      <ul class="nav nav-pills align-items-end profile_tabs tw-overflow-x-auto" id="pills-tab" role="tablist">
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link active" id="services-tab" data-bs-toggle="pill" data-bs-target="#services"
                  type="button" role="tab" aria-controls="services" aria-selected="true">
            <span v-if="!$route.params.id">خدمات من</span>
            <span v-else-if="$route.params.id && getUserDetails && getUserDetails.baseData">خدمات</span>


          </button>
        </li>
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link" id="posts-home-tab" data-bs-toggle="pill" data-bs-target="#posts-home"
                  type="button" role="tab" aria-controls="posts-home" aria-selected="true">
            <span v-if="!$route.params.id">پست های من</span>
            <span v-else-if="$route.params.id && getUserDetails && getUserDetails.baseData">پست ها</span>

          </button>
        </li>
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile"
                  type="button" role="tab" aria-controls="pills-profile" aria-selected="false">
            <span v-if="!$route.params.id">
               دوستان من
                <span class="follower_count">
              {{ $auth.user.baseData.followersCount }}
                </span>
            </span>
            <span v-else-if="$route.params.id && getUserDetails && getUserDetails.baseData">
               دوستان
              <span class="follower_count">
                {{getUserDetails.baseData.followersCount }}
              </span>
            </span>
          </button>
        </li>
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link" id="pills-contact-tab" data-bs-toggle="pill" data-bs-target="#pills-contact"
                  type="button" role="tab" aria-controls="pills-contact" aria-selected="false">
            <span v-if="!$route.params.id">
              گالری من
            </span>
            <span v-else>
              گالری
            </span>
          </button>
        </li>
        <li v-if="!$route.params.id" class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link" id="pills-edit-info-tab" data-bs-toggle="pill" data-bs-target="#pills-edit-info"
                  type="button" role="tab" aria-controls="pills-edit-info" aria-selected="false">
            ویرایش اطلاعات
          </button>
        </li>
      </ul>
    </div>
  </div>

</template>

<script>
import WhiteCropIcon from "./Icons/WhiteCropIcon";
import {mapGetters} from "vuex";

export default {
  name: "CustomHeader",
  components: {WhiteCropIcon},
  props: {
    isChatLayout: {
      type: Boolean,
      required: false,
      default: false,
    },
  },
  computed: {
    InlineBg() {
      // const base = this.BaseUrl;
      // if (this.$auth.user && this.$auth.user.baseData && this.$auth.user.baseData.banner) {
      //   return {
      //     backgroundImage: `url(${base + this.$auth.user.baseData.banner})`,
      //     backgroundPosition: "center",
      //     backgroundSize: "cover",
      //     backgroundRepeat: "no-repeat",
      //   };
      // }
      //
      const base = this.BaseUrl;
      if (!this.$route.params.id) {
        return {
          backgroundImage: `url(${this.$auth.user.baseData.banner ? base + this.$auth.user.baseData.banner : "/defaultBanner.jpg"})`,
          backgroundPosition: "center",
          backgroundSize: "cover",
          backgroundRepeat: "no-repeat",
        };
      } else if (this.$route.params.id && this.getUserDetails && this.getUserDetails.baseData) {
        return {
          backgroundImage: `url(${this.getUserDetails.baseData.banner ? base + this.getUserDetails.baseData.banner : "/defaultBanner.jpg"})`,
          backgroundPosition: "center",
          backgroundSize: "cover",
          backgroundRepeat: "no-repeat",
        }
      }

    },
    userAvatar() {
      if (!this.$route.params.id) {
        return this.$auth.user.userInfo.selfieFileData
          ? this.BaseUrl + this.$auth.user.userInfo.selfieFileData
          : "/defaultUser.png";
      } else if (this.$route.params.id && this.getUserDetails && this.getUserDetails.baseData) {
        return this.getUserDetails.baseData.selfie ? this.BaseUrl + this.getUserDetails.baseData.selfie : "/defaultUser.png";
      }
    },
    BaseUrl() {
      return process.env.pic;
    },
    ...mapGetters("userDetails", ["getUserDetails"])
  },
  methods: {
    callInputMethod() {
      this.$refs.file.click();
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
            this.$route.params.id
          );
        if (response.data) {
          this.$toast.success("درخواست دوستی شما ارسال شد");
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
@media (max-width: 375px) {
  .profile_section{
    top: 80px;
  }
}
@media (min-width: 376px) and (max-width: 768px) {
  .profile_section{
    top: 125px;
  }
}
@media (min-width: 769px) {
  .profile_section{
    top: 135px;
  }
}


.header_in_add_post {
  border-radius: 0 !important;
}

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
