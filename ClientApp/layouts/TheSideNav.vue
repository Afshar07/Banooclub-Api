<template>
  <div class="sidenav-container">
    <!-- Modal -->
    <div
      class="modal fade"
      id="exampleModal"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body">...</div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="modal"
            >
              Close
            </button>
            <button type="button" class="btn btn-primary">Save changes</button>
          </div>
        </div>
      </div>
    </div>

    <div v-if="show" class="sidenav-backdrop" @click="$emit('close')"></div>
    <transition name="slide-side">
      <div v-if="show" class="sidenav">
        <div class="row mb-3">حریم خصوصی</div>
        {{ $store.state.loggedInfo.isPrivate }}
        <div class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2">
<!--          <div v-tooltip="{trigger :'manual',show :false,content:''}" class="">-->
<!--            <i class="fas fa-question-circle tw-text-blue-400 position-absolute bottom-0 end-0 "></i>-->
<!--            <div class="sidenavOptionText"></div>-->
<!--          </div>-->
          <div @click="isGallerySelected=!isGallerySelected" v-click-outside="isGallerySelected=false" :class="{'tw-tooltip-open':isGallerySelected}" class="tw-tooltip   position-relative tw-cursor-pointer" data-tip="با فعال کردن این گزینه نمایش گالری شما برای کاربرانی که شما را دنبال نکرده اند قفل میشود">
            <i class="fas fa-question-circle tw-text-blue-400 position-absolute bottom-0 end-0 "></i>
            <button  class="tw-btn tw-bg-white tw-text-gray-400 hover:tw-bg-white tw-p-0 hover:tw-text-gray-400 sidenavOptionTextb">قفل نمایش گالری</button>
          </div>
          <div class="">
            <label class="switch">
              <input
                type="checkbox"
                id="togBt3n12"
                @input="isPrivateGalleryFriendMethod"
                :checked="PrivateGalleryFriendMethod"
              />
              <div class="slider round">
                <span class="on">بله</span>
                <span class="off">خیر</span>
              </div>
            </label>
          </div>
        </div>
        <!--              //TempChange-->
<!--        <div class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2">-->

<!--          <div @click="isPostSelected=!isPostSelected" v-click-outside="isPostSelected=false" :class="{'tw-tooltip-open':isPostSelected}" class="tw-tooltip   position-relative tw-cursor-pointer" data-tip="با فعال کردن این گزینه نمایش گالری شما برای کاربرانی که شما را دنبال نکرده اند قفل میشود">-->
<!--            <i class="fas fa-question-circle tw-text-blue-400 position-absolute bottom-0 end-0 "></i>-->
<!--            <button  class="tw-btn tw-bg-white tw-text-gray-400 hover:tw-bg-white tw-p-0 hover:tw-text-gray-400 sidenavOptionTextb">قفل نمایش پست</button>-->
<!--          </div>-->
<!--          <div class="">-->
<!--            <label class="switch">-->
<!--              <input-->
<!--                type="checkbox"-->
<!--                id="togBtn12"-->
<!--                @input="isPrivatePostMethod"-->
<!--                :checked="PrivatePostMethod"-->
<!--              />-->
<!--              <div class="slider round">-->
<!--                <span class="on">بله</span>-->
<!--                <span class="off">خیر</span>-->
<!--              </div>-->
<!--            </label>-->
<!--          </div>-->
<!--        </div>-->

      </div>
    </transition>
  </div>
</template>

<script>
export default {
  name: "TheSideNav",
  props: {
    show: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      isGallerySelected:false,
      currentState: false,
      isPostSelected:false,
      isPrivate: this.$store.state.loggedInfo?.IsPrivateActivity,
      isPrivateSocial: this.$store.state.isPrivateSocial,
      isPrivateGalleryFriend: this.$store.state.isPrivateGalleryFriend,
      isPrivateRoomate: this.$store.state.isPrivateRoomate,
      isPrivateAds: this.$store.state.isPrivateAds,
    };
  },
  computed: {
    Private() {
      return this.$store.state.loggedInfo.Private;
    },
    PrivateGalleryFriendMethod(){
      console.log(this.$auth.user.userInfo.userSetting.isPrivateGalleryFriend)
      return this.$auth.user.userInfo.userSetting.isPrivateGalleryFriend
    },
    PrivatePostMethod(){
      console.log(this.$auth.user.userInfo.userSetting.isPrivatePost)
      return this.$auth.user.userInfo.userSetting.isPrivatePost
    }
  },
  methods: {
    updateMap(e, val) {
      console.log(e, val);
    },

    isPrivateSocialMethod(e) {
      this.$store.dispatch("setIsPrivateSocial", e.target.value);
      this.$axios
        .post(
          "UserSetting/Update",
          {
            isPrivateSocial: this.$store.state.isPrivateSocial,
          },
          {}
        )
        .then((response) => {
          if (response.status === 200) {
            this.$toast.success(
              "تنظیمات نمایش شبکه اجتماعی با موفقیت به روز رسانی شد. "
            );
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    async isPrivateGalleryFriendMethod(e) {

      this.$axios
        .post(
          "UserSetting/Update",
          {
            isPrivateGalleryFriend: !this.$auth.user.userInfo.userSetting.isPrivateGalleryFriend
          },
          {}
        )
        .then(async (response) => {
          if (response.status === 200) {
           await this.$auth.fetchUser()
            this.$toast.success(
              "تنظیمات نمایش گالری  با موفقیت به روز رسانی شد. "
            );
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    async isPrivatePostMethod(e) {

      this.$axios
        .post(
          "UserSetting/Update",
          {
            isPrivatePost: !this.$auth.user.userInfo.userSetting.isPrivatePost
          },
          {}
        )
        .then(async (response) => {
          if (response.status === 200) {
           await this.$auth.fetchUser()
            this.$toast.success(
              "تنظیمات نمایش پست با موفقیت به روز رسانی شد. "
            );
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },

    isPrivateMethod(e) {
      let value = false;
      if (this.isPrivate) {
        value = false;
      } else {
        value = true;
      }

      this.$axios
        .post(
          "UserSetting/Update",
          {
            isPrivateActivity: value,
          },
          {}
        )
        .then((response) => {
          if (response.status === 200) {
            this.$toast.success(
              "تنظیمات نمایش پروفایل با موفقیت به روز رسانی شد. "
            );
            this.$axios
              .post(`Common/GetIndexData`, null, {})
              .then((response) => {
                this.$store.commit("SetUserData", response.data);
              });
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    isPrivateRoomateMethod(e) {
      this.$store.dispatch("setIsPrivateRoomate", !this.isPrivateRoomate);
      this.isPrivateRoomate = !this.isPrivateRoomate;
      this.$axios
        .post(
          "UserSetting/Update",
          {
            isPrivateRoomate: this.isPrivateRoomate,
          },
          {}
        )
        .then((response) => {
          if (response.status === 200) {
            this.$toast.success(
              "تنظیمات نمایش هم خانه با موفقیت به روز رسانی شد. "
            );
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    isPrivateAdsMethod(e) {
      this.$store.dispatch("setIsPrivateAds", !!e.target.value);
      this.$axios
        .post(
          "UserSetting/Update",
          {
            isPrivateAds: this.$store.state.isPrivateAds,
          },
          {}
        )
        .then((response) => {
          if (response.status === 200) {
            this.$toast.success(
              "تنظیمات نمایش آگهی با موفقیت به روز رسانی شد. "
            );
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
};
</script>

<style>
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
    height: 100%;
    width: 350px;
    background-color: white;
    z-index: 10000;
    position: fixed;
    top: 0 !important;
    left: 0;
    box-sizing: border-box;
    padding: 30px;
  }
}

.sidenav {
  height: 100%;
  width: 300px;
  background-color: white;
  z-index: 10000;
  position: fixed;
  top: 0;
  left: 0;
  box-sizing: border-box;
  padding: 30px;
}

.slide-side-enter-active,
.slide-side-leave-active {
  transition: all 0.3s ease-out;
}

.slide-side-enter,
.slide-side-leave-to {
  transform: translateX(-100%);
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
  background-color: #fb3587;
}

input:focus + .slider {
  box-shadow: 0 0 1px #fb3587;
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
