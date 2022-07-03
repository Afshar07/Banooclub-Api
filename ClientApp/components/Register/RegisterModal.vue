<template>
  <transition name="modal">
    <div v-if="is_modal_open" class="tw-modal tw-modal-open tw-bg-transparent" @click="closeRegisterModal">
      <div class="tw-container tw-mx-auto tw-w-11/12 tw-z-[99999]">
        <div class=" tw-bg-white tw-modal-box tw-max-w-max tw-p-0 tw-w-full tw-mx-auto tw-rounded-xl tw-shadow-2xl tw-p-0" @click.stop>
          <div class="modal_header tw-flex tw-items-center tw-justify-between tw-border-b">
            <div class="tw-px-7 tw-py-5">
              <h2 class="lg:tw-text-2xl tw-text-xl tw-font-semibold tw-mb-1 tw-text-gray-600">ثبت نام</h2>
            </div>
            <button class="tw-p-2 tw-bg-gray-100 tw-rounded-full tw-m-3" type="button" @click="closeRegisterModal">
              <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6 tw-text-gray-500" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="1">
                <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12" />
              </svg>
            </button>
          </div>
          <div class="d-flex flex-column">
            <div class="lg:tw-mt-0 lg:tw-w-96  tw-mt-10 tw-w-full custom_navs tw-bg-white tw-shadow-lg tw-rounded-lg">
              <div class="d-flex flex-column justify-content-between">
<!--                <p class="log-title-text" :class="{ TextWhite: userType !== 0 }">-->
<!--                  از بانو کلاب پلاس استفاده میکنید ؟-->
<!--                  <nuxt-link-->
<!--                    to="/register"-->
<!--                    href="#"-->
<!--                    title=""-->
<!--                    :class="{ TextWhite: userType !== 0 }"-->
<!--                  >قوانین را بخوانید-->
<!--                  </nuxt-link>-->
<!--                  یا-->
<!--                  <nuxt-link-->
<!--                    to="/"-->
<!--                    href="#"-->
<!--                    title=""-->
<!--                    :class="{ TextWhite: userType !== 0 }"-->
<!--                  >-->
<!--                    اینجا کلیک کنید-->
<!--                  </nuxt-link>-->
<!--                </p>-->
                <div class="row py-2" v-show="!isOtpSent">
                  <div
                    class="col-6 text-center"
                    :class="registerType == 2 ? 'TabActive' : 'deActiveTab'"
                    @click="setRegisterMethod(2)"
                  >
                    ثبت نام با ایمیل
                  </div>
                  <div
                    class="col-6 border-end text-center"
                    :class="registerType == 1 ? 'TabActive' : 'deActiveTab'"
                    @click="setRegisterMethod(1)"
                  >
                    ثبت نام با شماره موبایل
                  </div>
                </div>
                <form>
                  <get-otp
                    v-if="!isOtpSent"
                    @OtpSent="setOtpSentStatus"
                    @getMail="setMail"
                    @getNumber="setNumber"
                    @setCounter="setCounter($event)"
                    :registerType="registerType"
                  ></get-otp>
                  <register-form
                    v-if="isOtpSent"
                    :counterNumber="local_counter"
                    @getUserRole="setUserType"
                    @getSignUpPayload="setSignUpPayload"
                    @close_register_modal="closeRegisterModal"
                  ></register-form>
                </form>
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
            <!--          <div class="tw-mt-8 tw-text-center tw-text-sm">-->
            <!--            <nuxt-link to="#" class="tw-font-semibold hover:tw-underline tw-no-underline">یک صفحه</nuxt-link> برای افراد مشهور، گروه موسیقی یا تجارت ایجاد کنید-->
            <!--          </div>-->

          </div>


          <!--          <div-->
<!--            class="col-md-6 px-0 col-12 order-0 order-md-1 login-reg-bg position-relative"-->
<!--          >-->
<!--            <div class="log-reg-area position-absolute" :class="GetBgClass" dir="rtl">-->
<!--              <p class="log-title-text" :class="{ TextWhite: userType !== 0 }">-->
<!--                از بانو کلاب پلاس استفاده میکنید ؟-->
<!--                <nuxt-link-->
<!--                  to="/register"-->
<!--                  href="#"-->
<!--                  title=""-->
<!--                  :class="{ TextWhite: userType !== 0 }"-->
<!--                >قوانین را بخوانید-->
<!--                </nuxt-link>-->
<!--                یا-->
<!--                <nuxt-link-->
<!--                  to="/"-->
<!--                  href="#"-->
<!--                  title=""-->
<!--                  :class="{ TextWhite: userType !== 0 }"-->
<!--                >-->
<!--                  اینجا کلیک کنید-->
<!--                </nuxt-link>-->
<!--              </p>-->

<!--              <div class="row py-2" v-show="!isOtpSent">-->
<!--                <div-->
<!--                  class="col-6 text-center"-->
<!--                  :class="registerType == 2 ? 'TabActive' : 'deActiveTab'"-->
<!--                  @click="setRegisterMethod(2)"-->
<!--                >-->
<!--                  ثبت نام با ایمیل-->
<!--                </div>-->
<!--                <div-->
<!--                  class="col-6 border-end text-center"-->
<!--                  :class="registerType == 1 ? 'TabActive' : 'deActiveTab'"-->
<!--                  @click="setRegisterMethod(1)"-->
<!--                >-->
<!--                  ثبت نام با شماره موبایل-->
<!--                </div>-->
<!--              </div>-->
<!--              <form>-->
<!--                <get-otp-->
<!--                  v-if="!isOtpSent"-->
<!--                  @OtpSent="setOtpSentStatus"-->
<!--                  @getMail="setMail"-->
<!--                  @getNumber="setNumber"-->
<!--                  :registerType="registerType"-->
<!--                ></get-otp>-->

<!--                <register-form-->
<!--                  v-if="isOtpSent"-->
<!--                  @getUserRole="setUserType"-->
<!--                  @getSignUpPayload="setSignUpPayload">-->
<!--                </register-form>-->

<!--              </form>-->
<!--              <div class="col-12 my-3">-->
<!--                <nuxt-link-->
<!--                  to="Login"-->
<!--                  class="existAccount"-->
<!--                  :class="{ TextWhite: userType !== 0 }"-->
<!--                >-->
<!--                </nuxt-link>-->
<!--              </div>-->
<!--            </div>-->
<!--          </div>-->

<!--          <form class="tw-p-7 tw-space-y-5">-->
<!--            <div class="tw-grid lg:tw-grid-cols-2 tw-gap-5">-->
<!--              <input type="text" placeholder="نام" class="with-border">-->
<!--              <input type="text" placeholder="نام خانوادگی" class="with-border">-->
<!--            </div>-->
<!--            <div class="tw-grid lg:tw-grid-cols-2 tw-gap-5">-->
<!--              <input type="text" placeholder="شماره همراه" class="with-border">-->
<!--              <input type="text" placeholder="کد تایید" class="with-border">-->
<!--            </div>-->
<!--            <div class="tw-grid lg:tw-grid-cols-2 tw-gap-5">-->
<!--              <input type="text" placeholder="شماره معرف" class="with-border">-->
<!--              <select v-model="service_category" class="form-select" aria-label="Default select example">-->
<!--                <option :value="null">دسته بندی خدمت</option>-->
<!--                <option v-for="service_category in categories" :value="service_category.serviceCategoryId">-->
<!--                  {{ service_category.title }}-->
<!--                </option>-->
<!--              </select>-->
<!--            </div>-->
<!--            <div class="flex text-center">-->
<!--              <button type="button" class="tw-bg-blue-600 tw-font-semibold tw-mx-auto tw-px-10 tw-py-3 tw-rounded-md tw-text-center tw-text-white">-->
<!--                ثبت نام-->
<!--              </button>-->
<!--            </div>-->
<!--          </form>-->
        </div>
      </div>
    </div>
  </transition>
</template>

<script>


import GetOtp from "../../components/Register/GetOtp";
import RegisterForm from "../../components/Register/RegisterForm.vue";
export default {
  name: "RegisterModal",
  components: {RegisterForm, GetOtp},
  data(){
    return{
      loginType: 1,
      local_counter:0,

      categories:[],
      service_category: null,
      isOtpSent: false,
      Time: null,
      registerType: 1,
      payload: {
        mobile: "",
        mail: "",
        firstName: null,
        lastName: null,
        password: null,
        verifyCode: null,
        encryptedMail: "string",
        type: 1,
        userName: null,
        userRole: 0,
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
  computed: {
    GetBgClass() {
      switch (this.userType) {
        case "1":
          return "lightBlue";

        case "4":
          return "lightBlue2";
        case "3":
          return "lightBlue3";
      }
    },
    checkEqualPasswordMobile() {
      return this.passwordMobile === this.repeatPasswordMobile;
    },
    checkEqualPasswordEmail() {
      return this.passwordEmail === this.repeatPasswordEmail;
    },
  },

  props:{
    is_modal_open:{
      type: Boolean,
      required: true,
      default: false
    }
  },
  emits:['close_modal'],
  methods:{
    setCounter(counter){
      this.local_counter = counter
      console.log('this.local_counter',this.local_counter)

    },
    setLoginMethod(type) {
      this.loginType = type;
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
        }else{
          this.$emit("close_register_modal");
          this.$auth.strategy.token.set(response.data.token)
          this.$auth.setUser(response.data.token.user)
          this.$router.push('/social')
        }
        // this.$store.dispatch("login", {
        //   payload: response.data,
        // });

        // console.log('b man resid')
        // this.$router.replace('/social')
        // this.$nuxt.refresh();
        // console.log('b man resid')


        // this.$router.push("/social");
      } catch (error) {
        console.error(error);
      } finally {
        this.$nuxt.$loading.finish();
      }
    },
    setSignUpPayload(payload) {
      for (let i in payload) {
        this.payload[i] = payload[i];
      }
      this.sendSignUpRequest();
    },
    setMail(mail) {
      this.payload.mail = mail;
      this.payload.type = 2;
      this.payload.mobile = "";
    },
    setNumber(mobile) {
      this.payload.mobile = mobile;
      this.payload.type = 1;
      this.payload.mail = "";
    },
    setUserType(userType) {
      this.userType = userType;
    },
    setRegisterMethod(type) {
      this.registerType = type;
    },
    setOtpSentStatus() {
      this.isOtpSent = true;
    },
    closeRegisterModal(){
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
