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
          style="height: 30vh; background-position: center;border-top-right-radius: 1rem ;border-top-left-radius: 1rem ;"
          :style="InlineBg"
        >
          <div class="row col-12 m-0 p-0">
            <div
              class="col-lg-3 col-12 d-flex mb-md-3 align-items-center justify-content-center justify-content-md-start align-items-md-end">
              <div class="d-flex flex-wrap gap-3 change_pic_div">
                <!--                <button-->
                <!--                  v-if="!this.$route.params.id"-->
                <!--                  style="cursor: pointer;background-color: #49515c"-->
                <!--                  class="btn ChangePhotoBtn d-flex align-items-center gap-2"-->
                <!--                  @click="callInputMethod"-->
                <!--                >-->
                <!--                  <CropIcon class="ChangePhotoIcon" fill="white" style="height: 20px; width: 20px;"/>-->
                <!--                  &lt;!&ndash;                        <font-awesome-icon icon="camera" style="font-size: 12px" />&ndash;&gt;-->
                <!--                  <small class="text-white" style="font-size: 12px;">ویرایش عکس جلد</small>-->
                <!--                  <form>-->
                <!--                    <input-->
                <!--                      ref="file"-->
                <!--                      class="InputEditPhoto d-none"-->
                <!--                      type="file"-->
                <!--                      @change="InputEditPhoto"-->
                <!--                    />-->
                <!--                  </form>-->
                <!--                </button>-->
                <!--                <button-->
                <!--                  v-if="!this.$route.params.id"-->
                <!--                  style="cursor: pointer;background-color: #49515c"-->
                <!--                  class="btn ChangePhotoBtn d-flex align-items-center gap-2"-->
                <!--                  @click="callUserInputMethod"-->
                <!--                >-->
                <!--                  &lt;!&ndash;                        <font-awesome-icon icon="camera" style="font-size: 12px" />&ndash;&gt;-->
                <!--                  <CropIcon class="ChangePhotoIcon" fill="white" style="height: 20px; width: 20px;"/>-->
                <!--                  <small class="text-white" style="font-size: 12px">ویرایش عکس پروفایل</small>-->
                <!--                  <form>-->
                <!--                    <input-->
                <!--                      class="InputUserEditPhoto d-none"-->
                <!--                      type="file"-->
                <!--                      @change="InputEditUserPhoto"-->
                <!--                    />-->
                <!--                  </form>-->
                <!--                </button>-->
              </div>
            </div>
            <div class="col-lg-6 col-12 justify-content-lg-start justify-content-end"
                 :class="{ TopGap: this.$route.query.userId }">
              <div
                class="row col-12 h-100 d-flex flex-column justify-content-lg-center justify-content-end align-items-center position-relative"
                style="top: 95px;">
                <div class="d-flex align-items-center justify-content-center">
                  <img
                    :src="userAvatar"
                    class="UserPhoto rounded-circle mb-md-3"
                    alt=""
                    style="width: 120px;height: 120px;"
                  />
                </div>
                <div class="d-flex align-items-center justify-content-center">
                  <h1 v-if="getUserDetails && getUserDetails.baseData">
                    {{
                      getUserDetails.baseData.name + " " + getUserDetails.baseData.familyName
                    }}
                  </h1>
                </div>
              </div>
            </div>
            <div class="col-lg-3 col-12 mb-3 d-flex align-items-end justify-content-center gap-2 text-white row">
              <div class="add-btn">
                <!--                      <span>{{ VuexHeaderData.followerCount }}-->
                <!--                        <span class="mx-2">دنبال کننده</span>-->
                <!--                      </span>-->
                <a
                  style="cursor: pointer"
                  v-if="getUserDetails && getUserDetails.followButton"
                  @click="Follow"
                >
                  دنبال کردن
                </a>
              </div>
            </div>
            <!--                  <div v-if="Others()" class="col-md-12 bg-white mx-auto">-->
            <!--                    <div-->
            <!--                      class="Navbar d-flex justify-content-center flex-lg-row flex-column ps-lg-5"-->
            <!--                    >-->
            <!--                      <div-->
            <!--                        class="d-flex flex-lg-column flex-row justify-content-md-end justify-content-between align-items-center my-2"-->
            <!--                      >-->
            <!--                        <h5 class="text-primary m-0">-->
            <!--                          {{-->
            <!--                            VuexHeaderData.name + " " + VuexHeaderData.familyName-->
            <!--                          }}-->
            <!--                        </h5>-->
            <!--                        <div-->
            <!--                          class="d-flex justify-content-start align-items-center gap-1"-->
            <!--                        >-->
            <!--                          <small style="font-size: 12px"-->
            <!--                          >{{ VuexHeaderData.userName }}@</small-->
            <!--                          >-->
            <!--                          <small-->
            <!--                            v-if="VuexHeaderData.type === 3"-->
            <!--                            class="badge bg-warning text-white"-->
            <!--                          >وکیل</small-->
            <!--                          >-->
            <!--                          <small-->
            <!--                            v-if="VuexHeaderData.type === 4"-->
            <!--                            class="badge bg-primary text-white"-->
            <!--                          >کاربر تجربی</small-->
            <!--                          >-->
            <!--                        </div>-->
            <!--                      </div>-->

            <!--                      <div-->
            <!--                        class="d-flex overflow-scroll text-nowrap flex-nowrap justify-content-md-center"-->
            <!--                      >-->
            <!--                        <div-->
            <!--                          class="position-relative d-flex align-items-center mx-2 mx-md-0 h-100"-->
            <!--                          @click="changeActivePage('post')"-->
            <!--                        >-->
            <!--                          <nuxt-link-->
            <!--                            :to="{-->
            <!--                          path: `/user/${$route.params.id}/posts`,-->
            <!--                        }"-->
            <!--                            exact-active-class="active"-->
            <!--                            class="heading-section-link"-->
            <!--                            title=""-->
            <!--                            data-ripple=""-->
            <!--                          >-->
            <!--                            <span class="">پست ها</span>-->
            <!--                            &lt;!&ndash;                          <i class="fas fa-image d-block d-lg-none"></i>&ndash;&gt;-->
            <!--                          </nuxt-link>-->
            <!--                        </div>-->
            <!--                        &lt;!&ndash;                      <div class="position-relative d-flex align-items-center" @click="changeActivePage('post')">&ndash;&gt;-->
            <!--                        &lt;!&ndash;                        <nuxt-link :to="{path:'/Social/TimelineVideos/',query:{userId:this.$store.state.SearchedUser}}" exact-active-class="active"&ndash;&gt;-->
            <!--                        &lt;!&ndash;                                   class="heading-section-link" title=""&ndash;&gt;-->
            <!--                        &lt;!&ndash;                                   data-ripple="">&ndash;&gt;-->
            <!--                        &lt;!&ndash;                          <span >ویدیو ها </span>&ndash;&gt;-->
            <!--                        &lt;!&ndash;&lt;!&ndash;                          <i class="fas fa-video d-block d-lg-none"></i>&ndash;&gt;&ndash;&gt;-->
            <!--                        &lt;!&ndash;                        </nuxt-link>&ndash;&gt;-->
            <!--                        &lt;!&ndash;                      </div>&ndash;&gt;-->
            <!--                        <div-->
            <!--                          class="position-relative d-flex align-items-center mx-2 mx-md-0 h-100"-->
            <!--                          @click="changeActivePage('post')"-->
            <!--                        >-->
            <!--                          <nuxt-link-->
            <!--                            :to="{-->
            <!--                          path: `/user/${$route.params.id}/photos/`,-->
            <!--                        }"-->
            <!--                            exact-active-class="active"-->
            <!--                            class="heading-section-link"-->
            <!--                            title=""-->
            <!--                            data-ripple=""-->
            <!--                          >-->
            <!--                            <span>تصاویر </span>-->
            <!--                            &lt;!&ndash;                          <i class="fas fa-images d-block d-lg-none"></i>&ndash;&gt;-->
            <!--                          </nuxt-link>-->
            <!--                        </div>-->
            <!--                        <div-->
            <!--                          class="position-relative d-flex align-items-center mx-2 mx-md-0 h-100"-->
            <!--                          @click="changeActivePage('post')"-->
            <!--                        >-->
            <!--                          <nuxt-link-->
            <!--                            :to="{-->
            <!--                          path: `/user/${$route.params.id}/roommate/`,-->
            <!--                        }"-->
            <!--                            exact-active-class="active"-->
            <!--                            class="heading-section-link"-->
            <!--                            title=""-->
            <!--                            data-ripple=""-->
            <!--                          >-->
            <!--                            <span>اطلاعات هم خانه </span>-->
            <!--                            &lt;!&ndash;                          <i class="fas fa-bed d-block d-lg-none"></i>&ndash;&gt;-->
            <!--                          </nuxt-link>-->
            <!--                        </div>-->
            <!--                        <div-->
            <!--                          class="position-relative d-flex align-items-center mx-2 mx-md-0 h-100"-->
            <!--                          @click="changeActivePage('post')"-->
            <!--                        >-->
            <!--                          <nuxt-link-->
            <!--                            tag="a"-->
            <!--                            :to="{-->
            <!--                          path: `/user/${$route.params.id}/ads/`,-->
            <!--                        }"-->
            <!--                            exact-active-class="active"-->
            <!--                            class="heading-section-link"-->
            <!--                            title=""-->
            <!--                            data-ripple=""-->
            <!--                          >-->
            <!--                            <span>آگهی ها </span>-->
            <!--                            &lt;!&ndash;                          <i class="fas fa-ad d-block d-lg-none"></i>&ndash;&gt;-->
            <!--                          </nuxt-link>-->
            <!--                        </div>-->
            <!--                        <div-->
            <!--                          class="position-relative d-flex align-items-center mx-2 mx-md-0 h-100"-->
            <!--                          @click="changeActivePage('post')"-->
            <!--                        >-->
            <!--                          <nuxt-link-->
            <!--                            :to="{-->
            <!--                          path: `/user/${$route.params.id}/info/`,-->
            <!--                        }"-->
            <!--                            exact-active-class="active"-->
            <!--                            class="heading-section-link"-->
            <!--                            title=""-->
            <!--                            data-ripple=""-->
            <!--                          >-->
            <!--                            <span>اطلاعات شخصی </span>-->
            <!--                          </nuxt-link>-->
            <!--                        </div>-->
            <!--                      </div>-->
            <!--                    </div>-->
            <!--                  </div>-->
          </div>
        </div>
      </div>
      <ul class="nav nav-pills align-items-end profile_tabs" id="pills-tab" role="tablist">
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link active" id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home"
                  type="button" role="tab" aria-controls="pills-home" aria-selected="true">پست ها
          </button>
        </li>
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile"
                  type="button" role="tab" aria-controls="pills-profile" aria-selected="false">
            دوستان
            <span class="follower_count" v-if="getUserDetails">
                    {{ getUserDetails.followerCount }}
                  </span>
          </button>
        </li>
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link" id="pills-contact-tab" data-bs-toggle="pill" data-bs-target="#pills-contact"
                  type="button" role="tab" aria-controls="pills-contact" aria-selected="false">تصاویر
          </button>
        </li>
      </ul>
    </div>
  </div>


</template>

<script>
import {mapGetters} from "vuex";

export default {
  name: "OtherUserHeader",
  props: {
    isChatLayout: {
      type: Boolean,
      required: false,
      default: false,
    },
  },
  computed: {
    BaseUrl() {
      return process.env.pic;
    },
    userAvatar() {
      if (this.getUserDetails && this.getUserDetails.baseData && this.getUserDetails.baseData.selfie) {
        return this.BaseUrl +'media/gallery/profile/'+ this.getUserDetails.baseData.selfie;

      } else {
        return "/defaultUser.png"
      }

    },

    InlineBg() {
      const base = this.BaseUrl;

      if (this.getUserDetails && this.getUserDetails.baseData && this.getUserDetails.baseData.banner) {

        return {
          backgroundImage: `url(${base + this.getUserDetails.baseData.banner})`,
          backgroundPosition: "center",
          backgroundSize: "cover",
          backgroundRepeat: "no-repeat",
        };
      }else{
        return {
          backgroundImage: `url(/defaultBanner.jpg)`,
          backgroundPosition: "center",
          backgroundSize: "cover",
          backgroundRepeat: "no-repeat",
        };
      }

    },
    ...mapGetters("userDetails", ["getUserDetails"]),

  },
  methods: {
    callInputMethod() {
      this.$refs.file.click();
    },

  }

}
</script>

<style scoped>

</style>
