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

<!--        <div-->
<!--          class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2"-->
<!--        >-->
<!--          <div class="sidenavOptionText">نمایش پروفایل</div>-->
<!--          <div class="">-->
<!--            <label class="switch">-->
<!--              <input-->
<!--                type="checkbox"-->
<!--                id="togBtn5"-->
<!--                @input="isPrivateMethod"-->
<!--                v-model="isPrivate"-->
<!--              />-->
<!--              <div class="slider round">-->
<!--                <span class="on">بله</span>-->
<!--                <span class="off">خیر</span>-->
<!--              </div>-->
<!--            </label>-->
<!--          </div>-->
<!--        </div>-->
<!--        <div-->
<!--          class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2"-->
<!--        >-->
<!--          <div class="sidenavOptionText">نمایش شبکه اجتماعی</div>-->
<!--          <div class="">-->
<!--            <label class="switch">-->
<!--              <input-->
<!--                type="checkbox"-->
<!--                id="togBtn12"-->
<!--                @input="isPrivateSocialMethod"-->
<!--                v-model="isPrivateSocial"-->
<!--                :disabled="!isPrivate ? false : true"-->
<!--              />-->
<!--              <div class="slider round">-->
<!--                <span class="on">بله</span>-->
<!--                <span class="off">خیر</span>-->
<!--              </div>-->
<!--            </label>-->
<!--          </div>-->
<!--        </div>-->
        <div class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2">
          <div class="sidenavOptionText">نمایش گالری</div>
          <div class="">
            <label class="switch">
              <input
                type="checkbox"
                id="togBtn12"
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
        <div class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2">
          <div class="sidenavOptionText">نمایش پست</div>
          <div class="">
            <label class="switch">
              <input
                type="checkbox"
                id="togBtn12"
                @input="isPrivatePostMethod"
                :checked="PrivatePostMethod"
              />
              <div class="slider round">
                <span class="on">بله</span>
                <span class="off">خیر</span>
              </div>
            </label>
          </div>
        </div>
<!--        <div-->
<!--          class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2"-->
<!--        >-->
<!--          <div class="sidenavOptionText">نمایش هم خانه</div>-->
<!--          <div class="">-->
<!--            <label class="switch">-->
<!--              <input-->
<!--                type="checkbox"-->
<!--                id="togBtn13"-->
<!--                @input="isPrivateRoomateMethod"-->
<!--                v-model="isPrivateRoomate"-->
<!--              />-->
<!--              <div class="slider round">-->
<!--                <span class="on">خصوصی</span>-->
<!--                <span class="off">عمومی</span>-->
<!--              </div>-->
<!--            </label>-->
<!--          </div>-->
<!--        </div>-->
<!--        <div-->
<!--          class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2"-->
<!--        >-->
<!--          <div class="sidenavOptionText">نمایش آگهی</div>-->
<!--          <div class="">-->
<!--            <label class="switch">-->
<!--              <input-->
<!--                type="checkbox"-->
<!--                id="togBtn14"-->
<!--                @input="isPrivateAdsMethod"-->
<!--                v-model="isPrivateAds"-->
<!--              />-->
<!--              <div class="slider round">-->
<!--                <span class="on">عمومی</span>-->
<!--                <span class="off">خصوصی</span>-->
<!--              </div>-->
<!--            </label>-->
<!--          </div>-->
<!--        </div>-->
        <!--        <div class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2">-->
        <!--          <div class="sidenavOptionText">از حالت شب استفاده کنید</div>-->
        <!--          <div class="">-->
        <!--            <label class="switch">-->
        <!--              <input type="checkbox" id="togBtn1">-->
        <!--              <div class="slider round">-->
        <!--                <span class="on">روشن</span>-->
        <!--                <span class="off">خاموش</span>-->
        <!--              </div>-->
        <!--            </label>-->
        <!--          </div>-->
        <!--        </div>-->
        <!--        <div class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2">-->
        <!--          <div class="sidenavOptionText">اعلان ها</div>-->
        <!--          <div class="">-->
        <!--            <label class="switch">-->
        <!--              <input type="checkbox" id="togBtn2">-->
        <!--              <div class="slider round">-->
        <!--                <span class="on">روشن</span>-->
        <!--                <span class="off">خاموش</span>-->
        <!--              </div>-->
        <!--            </label>-->
        <!--          </div>-->
        <!--        </div>-->
        <!--        <div class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2">-->
        <!--          <div class="sidenavOptionText">صدای اعلان ها</div>-->
        <!--          <div class="">-->
        <!--            <label class="switch">-->
        <!--              <input type="checkbox" id="togBtn3">-->
        <!--              <div class="slider round">-->
        <!--                <span class="on">روشن</span>-->
        <!--                <span class="off">خاموش</span>-->
        <!--              </div>-->
        <!--            </label>-->
        <!--          </div>-->
        <!--        </div>-->
        <!--        <div class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2">-->
        <!--          <div class="sidenavOptionText">پروفایل من</div>-->
        <!--          <div class="">-->
        <!--            <label class="switch">-->
        <!--              <input type="checkbox" id="togBtn4">-->
        <!--              <div class="slider round">-->
        <!--                <span class="on">روشن</span>-->
        <!--                <span class="off">خاموش</span>-->
        <!--              </div>-->
        <!--            </label>-->
        <!--          </div>-->
        <!--        </div>-->
        <!--        <div class="row mt-4 mb-4">-->
        <!--          تنظیمات حساب کاربری-->
        <!--        </div>-->
        <!--        <div class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2">-->
        <!--          <div class="sidenavOptionText">زیر کار</div>-->
        <!--          <div class="">-->
        <!--            <label class="switch">-->
        <!--              <input type="checkbox" id="togBtn6">-->
        <!--              <div class="slider round">-->
        <!--                <span class="on">روشن</span>-->
        <!--                <span class="off">خاموش</span>-->
        <!--              </div>-->
        <!--            </label>-->
        <!--          </div>-->
        <!--        </div>-->
        <!--        <div class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2">-->
        <!--          <div class="sidenavOptionText">حساب شخصی</div>-->
        <!--          <div class="">-->
        <!--            <label class="switch">-->
        <!--              <input type="checkbox" id="togBtn7">-->
        <!--              <div class="slider round">-->
        <!--                <span class="on">روشن</span>-->
        <!--                <span class="off">خاموش</span>-->
        <!--              </div>-->
        <!--            </label>-->
        <!--          </div>-->
        <!--        </div>-->
        <!--        <div class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2">-->
        <!--          <div class="sidenavOptionText">حساب کسب و کار</div>-->
        <!--          <div class="">-->
        <!--            <label class="switch">-->
        <!--              <input type="checkbox" id="togBtn8">-->
        <!--              <div class="slider round">-->
        <!--                <span class="on">روشن</span>-->
        <!--                <span class="off">خاموش</span>-->
        <!--              </div>-->
        <!--            </label>-->
        <!--          </div>-->
        <!--        </div>-->
        <!--        <div class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2">-->
        <!--          <div class="sidenavOptionText">نمایش افراد آنلاین</div>-->
        <!--          <div class="">-->
        <!--            <label class="switch">-->
        <!--              <input type="checkbox" id="togBtn9">-->
        <!--              <div class="slider round">-->
        <!--                <span class="on">روشن</span>-->
        <!--                <span class="off">خاموش</span>-->
        <!--              </div>-->
        <!--            </label>-->
        <!--          </div>-->
        <!--        </div>-->
        <!--        <div class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2">-->
        <!--          <div class="sidenavOptionText">حذف تاریخچه</div>-->
        <!--          <div class="">-->
        <!--            <label class="switch">-->
        <!--              <input type="checkbox" id="togBtn10">-->
        <!--              <div class="slider round">-->
        <!--                <span class="on">روشن</span>-->
        <!--                <span class="off">خاموش</span>-->
        <!--              </div>-->
        <!--            </label>-->
        <!--          </div>-->
        <!--        </div>-->
        <!--        <div class="d-flex flex-row justify-content-between border-bottom pb-2 mt-2">-->
        <!--          <div class="sidenavOptionText">نمایش نام نویسنده</div>-->
        <!--          <div class="">-->
        <!--            <label class="switch">-->
        <!--              <input type="checkbox" id="togBtn11">-->
        <!--              <div class="slider round">-->
        <!--                <span class="on">روشن</span>-->
        <!--                <span class="off">خاموش</span>-->
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
      currentState: false,
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
