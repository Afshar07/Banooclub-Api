<template>
  <transition name="modal">
    <div v-if="is_modal_open" class="tw-modal tw-modal-open tw-bg-transparent" @click="closeRegisterModal">
      <div class="tw-container tw-mx-auto tw-w-11/12 tw-z-[99999]">
        <div style="overflow-x: hidden!important;"
             class=" tw-bg-white tw-modal-box tw-max-w-max tw-p-0 tw-w-full tw-mx-auto tw-rounded-xl tw-shadow-2xl tw-p-0"
             @click.stop>
          <div class="modal_header tw-flex tw-items-center tw-justify-between tw-border-b">
            <div class="tw-px-7 tw-py-5">
              <h2 class="lg:tw-text-2xl tw-text-xl tw-font-semibold tw-mb-1 tw-text-gray-600">ثبت نام</h2>
            </div>
            <button class="tw-p-2 tw-bg-gray-100 tw-rounded-full tw-m-3" type="button" @click="closeRegisterModal">
              <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6 tw-text-gray-500" fill="none"
                   viewBox="0 0 24 24" stroke="currentColor" stroke-width="1">
                <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12"/>
              </svg>
            </button>
          </div>
          <div class="d-flex flex-column">
            <div class="lg:tw-mt-0 lg:tw-w-96  tw-mt-10 tw-w-full custom_navs tw-bg-white tw-shadow-lg tw-rounded-lg">
              <div class="d-flex flex-column justify-content-between">
                <div class="row py-2">
                  <div class="col-12 d-flex align-items-center justify-content-center TabActive  border-end text-center">
                    ثبت نام با شماره موبایل
                  </div>
                </div>

                  <get-otp
                    v-if="ActiveComponent===1"
                    @getNumber="setNumber"
                    @setCounter="setCounter($event)"
                    @getCodeFields="getCodeField($event)"
                  ></get-otp>
                  <GetIntroducerCode v-if="ActiveComponent===2" ref="IntCode" @SendIntCode="SetIntroducerCode"></GetIntroducerCode>
                  <SelectAvatar @SendAvatarPic="SetAvatarPic" v-if="ActiveComponent===3"></SelectAvatar>
                  <SelectBanner @SendBannerPic="SetBannerPic" v-if="ActiveComponent===4"></SelectBanner>
                  <register-form
                    v-if="ActiveComponent===5"
                    :counterNumber="local_counter"
                    :code_field="code_field"
                    @getSignUpPayload="setSignUpPayload"
                    @close_register_modal="closeRegisterModal"
                  ></register-form>

                <div class="col-12 my-3">
                  <nuxt-link
                    to="Login"
                    class="existAccount"
                    :class="{ TextWhite: userType !== 0 }"
                  >
                  </nuxt-link>
                </div>

              </div>

            </div>


          </div>


        </div>
      </div>
    </div>
  </transition>
</template>

<script>


import GetOtp from "../../components/Register/GetOtp";
import RegisterForm from "../../components/Register/RegisterForm.vue";
import SelectAvatar from "@/components/SelectAvatar";
import SelectBanner from "@/components/SelectBanner";
import GetIntroducerCode from "@/components/GetIntroducerCode";
export default {
  name: "RegisterModal",
  components: {RegisterForm,SelectBanner, GetIntroducerCode,GetOtp,SelectAvatar},
  data() {
    return {
      code_field: null,
      local_counter: 0,
      ActiveComponent:1,
      categories: [],
      service_category: null,
      Time: null,
      payload: {
        mobile: "",
        mail: "",
        type: 1,
      },
      userType: 0,
    }
  },
  async fetch() {
    try {
      const allCategories =
        await this.$repositories.getAllServicesCategory.getAllServicesCategory();
      this.categories = allCategories.data.serviceCategories;
    } catch (error) {
      console.error(error)
    }
    // Get categories

  },

  props: {
    is_modal_open: {
      type: Boolean,
      required: true,
      default: false
    }
  },
  emits: ['close_modal'],
  methods: {

    setCounter(counter) {
      this.local_counter = counter
    },
    getCodeField(code_field) {
      this.code_field = code_field
    },
    async sendSignUpRequest() {

      const that = this
      try {
        this.$nuxt.$loading.start();
        const response =
          await this.$repositories.sendSignupRequest.sendSignupRequest(
            this.payload
          );
        if (response.data.status === 3) {
          this.$toast.error("نام کاربری یا رمز عبور اشتباه است.");
        } else if (response.data === 4) {
          this.$toast.error(
            "کد ارسال شده منتقضی شده است،لطفا دوباره امتحان کنید."
          );
        } else if (response.data === 5) {
          this.$toast.error("کد وارد شده اشتباه است.");
        } else if (response.data === 6) {
          this.$toast.error("کاربری با این نام کاربری وجود ندارد.");
        } else if (response.data === 7) {
          this.$toast.success("ثبت نام با موفقیت انجام شد.");
        } else if (response.data === 8) {
          this.$toast.error("قبلا کاربری با این نام کاربری ثبت نام کرده است.");
        } else if (response.data === 9) {
          this.$toast.error("پسورد شما ایمن نیست.");
        } else if (response.data === 10) {
          this.$toast.error("نام کاربری قبلا ثبت نام کرده است.");
        } else if (response.data === 11) {
          this.$toast.error("اکانت شما غیرفعال شده است.");
        } else {
          this.$emit("close_register_modal");
          this.$auth.strategy.token.set(response.data.token)
          this.$auth.setUser(response.data.user)
          this.$auth.fetchUser()
          this.$router.push('/social')
        }

      } catch (error) {
        console.error(error);
      } finally {
        this.$nuxt.$loading.finish();
      }
    },
    SetIntroducerCode(payload){
      Object.keys(payload).forEach(key => {
        this.payload[key] = payload[key]
      })
      this.ActiveComponent=3
    },
    setSignUpPayload(payload) {
      Object.keys(payload).forEach(key => {
        this.payload[key] = payload[key]
      })

      this.sendSignUpRequest();
    },

    SetAvatarPic(AvatarPic){
      if(AvatarPic === 'defaultAvatar0.png'){
        this.payload['avatar'] = null
      }else{

      this.payload['avatar'] = AvatarPic
      }
      this.ActiveComponent=4

    },
    SetBannerPic(BannerPic){
      if(BannerPic === 'defaultBanner0.jpg'){
        this.payload['banner'] = null
      }else{

      this.payload['banner'] = BannerPic
      }
      this.ActiveComponent=5

    },
    setNumber(mobile) {
      this.payload.mobile = mobile;
      this.payload.type = 1;
      this.payload.mail = "";
      this.ActiveComponent = 2
    },
    setUserType() {
      this.userType = 3;
    },


    closeRegisterModal() {
      this.$emit('close_register_modal')
    }
  }
}
</script>

<style scoped>
.TabActive {
  background: white;
  border-bottom: 3px solid #0d6efd;
  color: #0d6efd;
  font-size: 14px;
  padding: 10px 0;
  text-transform: uppercase;
  margin: 10px 0;
  cursor: pointer;
  border-right: none !important;
  border-left: none !important;
}

.deActiveTab {
  background: white none repeat scroll 0 0;
  font-size: 14px;
  padding: 10px 0;
  text-transform: uppercase;
  margin: 10px 0;
  cursor: pointer;
  border-right: none !important;
  border-left: none !important;
}

.deActiveTab:hover {
  color: #0d6efd;
}
</style>
