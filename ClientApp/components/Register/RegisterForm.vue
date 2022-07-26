<template>
  <div class="col-12 p-0 m-0" style="overflow-x: hidden">
    <div class="col-12 mt-3">
      <div class="col-md-12 my-3">
        <select-user-type @getUserRole="setUserRole"></select-user-type>
      </div>
      <div
        class="row SignupForm"
        :class="{ SignUpFormActive: payload.userRole }"
      >
        <div class="form-floating text-end">
          <input
            type="text"
            class="with-border  pe-2"
            id="UserNameEmail"
            placeholder="نام کاربری"
            dir="rtl"
            lang="fa"
            @input="validateUsername"
          />
<!--          <label class="fa-pull-right" for="firstName">نام کاربری</label>-->
          <div class="text-danger fw-bold mt-2" v-if="!isUsernameAvailable">
            این نام کاربری قبلا ثبت شده است.
          </div>
        </div>

        <div class="col-12 mt-3">
          <div class="form-floating text-end">
            <input
              type="text"
              class="with-border"
              id="firstName"
              placeholder="نام"
              dir="rtl"
              lang="fa"
              v-model.trim="payload.firstName"
            />
<!--            <label class="fa-pull-right" for="firstName">نام</label>-->
          </div>
        </div>
        <div class="col-12 mt-3">
          <div class="form-floating text-end">
            <input
              type="text"
              class="with-border"
              id="lastName"
              placeholder="نام خانوادگی"
              dir="rtl"
              lang="fa"
              v-model.trim="payload.lastName"
            />
<!--            <label class="fa-pull-right" for="lastName">نام خانوادگی</label>-->
          </div>
        </div>
        <div class="col-12 mt-3">
          <select v-model="payload.serviceCategoryId" class="form-select" aria-label="Default select example">
            <option :value="0">دسته بندی خدمت</option>
            <option v-for="(service_category,idx) in categories" :key="idx" :value="service_category.serviceCategoryId">
              {{ service_category.title }}
            </option>
          </select>
        </div>
        <div class="col-12 mt-3">
          <input v-model="payload.verification_code" type="text" placeholder="کد معرف" class="with-border">
        </div>
        <div class="col-12 mt-3 position-relative">
          <div class="form-floating text-end">
            <input
              :type="fieldPassword"
              class="with-border  border-bottom"
              id="password"
              placeholder="رمز عبور"
              v-model.trim="payload.password"
            />
            <button
              class="btn position-absolute viewPassword"
              type="password"
              @click.prevent="showPassword"
            >
              <font-awesome-icon
                icon="eye"
                v-if="fieldPassword === 'password'"
              />
              <font-awesome-icon
                icon="eye-slash"
                v-if="fieldPassword === 'text'"
              />
            </button>
<!--            <label for="password">رمز عبور</label>-->
          </div>
        </div>
        <div class="col-12 mt-3 position-relative">
          <div class="form-floating text-end">
            <input
              :class="isPasswordsMatch ? '' : 'notEqualPassword'"
              :type="fieldPassword"
              class="with-border border-bottom"
              id="passwordRepeat"
              placeholder="تکرار رمز عبور"
              v-model.trim="passwordRepeat"
            />
            <button
              class="btn position-absolute viewPassword"
              type="password"
              @click.prevent="showPassword"
            >
              <font-awesome-icon
                icon="eye"
                v-if="fieldPassword === 'password'"
              />
              <font-awesome-icon
                icon="eye-slash"
                v-if="fieldPassword === 'text'"
              />
            </button>
<!--            <label for="passwordRepeat">تکرار رمز عبور</label>-->
            <div class="text-danger fw-bold mt-4" v-if="!isPasswordsMatch">
              رمز تطابق ندارد
            </div>
          </div>
        </div>
        <div class="col-12 mt-3 text-muted hitPassword">
          رمز عبور باید حداقل 8 کاراکتر و شامل حداقل یک حرف کوچک و بزرگ انگلیسی
          و یک عدد و یک کاراکتر ویژه باشد
        </div>
        <div class="col-12 form-group mt-3">
          <div class="form-floating text-end">
            <input
              type="text"
              class="with-border "
              id="otp"
              placeholder="کد تایید"
              v-model.trim="payload.verifyCode"
            />
<!--            <label for="otp">کد تایید</label>-->
          </div>
        </div>
        <div class="col-12 my-2">
          <div class="form-check d-flex align-items-center">
            <input
              class="form-check-input"
              type="checkbox"
              value=""
              id="tos"
              v-model="isAcceptedTos"
            />
            <label
              class="form-check-label check-box text-muted mx-2"
              for="tos"
            ></label>
            <span class="privacyAccept text-muted">
              شرایط و ضوابط را قبول دارم
            </span>
            <br />
<!--            <nuxt-link to="/">-->
<!--              <span class="privacyAccept text-black"-->
<!--                >قبلا ثبت نام کرده اید ؟ اینجا کلیک کنید-->
<!--              </span>-->
<!--            </nuxt-link>-->
          </div>
        </div>

        <div class="col-md-12 d-flex align-items-start gap-1 ">

          <button
            @click="returnToRegister"
            type="button"
            class="mb-3 tw-bg-blue-600 tw-font-semibold tw-p-3 tw-rounded-md tw-text-center tw-text-white tw-w-full"
          >
            تغییر روش ثبت نام
          </button>

        </div>
<!--        <div class="col-md-12 d-flex align-items-start gap-1 ">-->

<!--          <button-->
<!--            @click="sendOtpCode"-->
<!--            type="button"-->
<!--            class="mb-3 tw-bg-blue-600 tw-font-semibold tw-p-3 tw-rounded-md tw-text-center tw-text-white tw-w-full"-->
<!--          >-->
<!--            ارسال مجدد کد-->
<!--          </button>-->

<!--        </div>-->

<!--        <div class="col-md-12 d-flex align-items-center gap-2">-->
<!--          <client-only>-->
<!--            <div v-if="!CodeSent" >-->
<!--              <client-only>-->
<!--                <countdown @finish="CodeSent =! CodeSent" :end-time="new Date().getTime() + localCounter">-->
<!--             <span slot-scope="{ timeObj }" class=" px-3 gap-2 rounded " >-->
<!--               <div class="d-flex ">-->
<!--                 <small>{{`${timeObj.m}`}}</small>-->
<!--               <small>:</small>-->
<!--               <small>{{`${timeObj.s}`}}</small>-->
<!--               </div>-->
<!--             </span>-->
<!--                </countdown>-->
<!--              </client-only>-->
<!--            </div>-->
<!--          </client-only>-->
<!--        </div>-->
<!--        <div v-if="CodeSent" class="col-md-12 my-3">-->
<!--          <button-->
<!--            type="button"-->
<!--            class="tw-bg-blue-600 tw-font-semibold tw-p-3 tw-rounded-md tw-text-center tw-text-white tw-w-full mt-3"-->
<!--            @click="sendOtpCode"-->
<!--          >-->
<!--            ارسال کد-->
<!--          </button>-->
<!--      </div>-->

        <div class="col-md-12 d-flex align-items-start gap-1 ">

          <button
            @click="sendSignUpRequest"
            :disabled="isButtonDisabled"
            type="button"
            class="tw-bg-blue-600 tw-font-semibold tw-p-3 tw-rounded-md
            tw-text-center tw-text-white tw-w-full"
            :class="[isButtonDisabled?'disable_button':'']"
          >
            ثبت نام
          </button>

        </div>

        <!-- <div
          class="col-md-6 d-flex align-items-center"
          v-if="!ShowSendAgainBtn"
        >
          <client-only>
            <countdown
              ref="Counter"
              @finish="renderSendAgainBtn"
              :end-time="Time"
            >
              <span
                slot-scope="{ timeObj }"
                class="d-flex align-items-center gap-2 rounded"
              >
                <div class="d-flex flex-column justify-content-center mb-2">
                  <span class="text-white text-center">
                    <strong>{{ `${timeObj.s}` }}</strong></span
                  >
                </div>

                <div class="d-flex flex-column justify-content-center mb-2">
                  <span class="text-white text-center">
                    <strong>:</strong></span
                  >
                </div>
                <div class="d-flex flex-column justify-content-center mb-2">
                  <span class="text-white text-center">
                    <strong>{{ `${timeObj.m}` }}</strong>
                  </span>
                </div>
              </span>
            </countdown>
          </client-only>
        </div> -->
      </div>
    </div>
  </div>
</template>

<script>
import SelectUserType from "./SelectUserType.vue";
export default {
  emits: ["getUserRole", "getSignUpPayload","close_register_modal","returnToRegister"],
  props:['counterNumber','code_field'],
  components: {
    SelectUserType,
  },
  async fetch() {

    if(this.counterNumber){
     this.localCounter = this.counterNumber
    }

    try {
      const allCategories =
        await this.$repositories.getAllServicesCategory.getAllServicesCategory();
      this.categories = allCategories.data.serviceCategories;
    } catch (error) {
      console.error(error)
    }
    // Get categories

  },

  data() {
    return {
      localCounter:0,
      categories:[],
      CodeSent:false,

      payload: {
        userName: null,
        firstName: null,
        lastName: null,
        password: null,
        verifyCode: null,
        userRole: 0,
        serviceCategoryId:0,
        introducerCodeL:null
      },
      passwordRepeat: null,
      fieldPassword: "password",
      isUsernameAvailable: true,
      isAcceptedTos: false,
      ShowSendAgainBtn: false,
    };
  },
  methods: {
    returnToRegister(){
      this.$emit("returnToRegister");
    },

    sendOtpCode() {
      // Handle both otp codes here
      if (this.set_reg_type == 1) {
        this.sendMobileOtpCode();
      } else {
        this.sendEmailOtpCode();
      }
    },
    async sendEmailOtpCode() {
      try {
        this.$nuxt.$loading.start();

        // if (!this.captcha) {
        //   this.$toast.error("لطفا کپچا را تکمیل کنید");
        // } else {
          const response =
            await this.$repositories.sendOtpToEmail.sendOtpToEmail(this.code_field);
          if (response.data.message === "Confirmation code has not expired") {
            this.$toast.error("کد تایید منقضی نشده است");
          } else if (response.data.hasUser === 1) {
            this.$toast.error("کاربری با این ایمیل قبلا ثبت نام کرده است");
          } else {
            this.$toast.success("کد تایید برای شما ارسال شد");
            this.$emit("OtpSent");
            this.$emit("getMail", this.code_field);
            // this.Time = new Date().getTime() + 250000;
          }
      } catch (error) {
        console.log(error);
      } finally {
        this.$nuxt.$loading.finish();
      }
    },
    async sendMobileOtpCode() {
      try {
        this.$nuxt.$loading.start();

        // if (this.mobile.length < 11) {
        //   this.$toast.error("شماره موبایل وارد شده معتبر نیست");
        // }  else {
          const response =
            await this.$repositories.sendOtpToMobile.sendOtpToMobile(
              this.code_field
            );
          if (response.data.message === "Confirmation code has not expired") {
            this.$toast.error("کد تایید منقضی نشده است");
          } else if (response.data.hasUser === 1) {
            this.$toast.error(
              "کاربری با این شماره موبایل قبلا ثبت نام کرده است"
            );
          } else {
            this.$toast.success("کد تایید برای شما ارسال شد");
            this.$emit("OtpSent");
            this.$emit("getNumber", this.code_field);

            // this.Time = new Date().getTime() + 250000;
          }
        // }
      } catch (error) {
        console.log(error);
      } finally {
        this.$nuxt.$loading.finish();
      }
    },
    sendSignUpRequest() {
      if (!this.isAcceptedTos) {
        this.$toast.error("برای ثبت نام باید شرایط و قوانین را بپذیرید.");
      } else if (!this.payload.firstName) {
        this.$toast.error("نام را وارد کنید.");
      } else if (!this.payload.lastName) {
        this.$toast.error("نام خانوادگی را وارد کنید.");
      } else if (this.payload.password.length < 8) {
        this.$toast.error("رمز عبور باید حداقل 8 کارکتر باشد.");
      } else if (!this.payload.verifyCode) {
        this.$toast.error("کد تایید را وارد کنید.");
      } else {
        this.$emit("getSignUpPayload", this.payload);

      }
    },
    // startTimer(duration, display) {
    //   let timer = duration,
    //     minutes,
    //     seconds;
    //   setInterval(function () {
    //     minutes = parseInt(timer / 60, 10);
    //     seconds = parseInt(timer % 60, 10);

    //     minutes = minutes < 10 ? "0" + minutes : minutes;
    //     seconds = seconds < 10 ? "0" + seconds : seconds;

    //     display.textContent = minutes + ":" + seconds;

    //     if (--timer < 0) {
    //       timer = duration;
    //     }
    //   }, 1000);
    // },
    renderSendAgainBtn() {
      this.ShowSendAgainBtn = true;
    },
    showPassword() {
      this.fieldPassword =
        this.fieldPassword === "password" ? "text" : "password";
    },
    setUserRole(type) {
      this.payload.userRole = type;
    },
    async validateUsername(event) {
      this.payload.userName = event.target.value;
      try {
        const response =
          await this.$repositories.validateUsername.validateUsername(
            this.payload.userName
          );
        this.isUsernameAvailable = response.data;
      } catch (error) {
        console.log(error);
      }
    },
  },
  computed: {
    isPasswordsMatch() {
      if (this.payload.password && this.passwordRepeat) {
        return this.payload.password === this.passwordRepeat;
      } else {
        return true;
      }
    },
    isButtonDisabled() {
      if (
        this.isAcceptedTos &&
        this.isPasswordsMatch &&
        this.isUsernameAvailable
      ) {
        return false;
      } else {
        return true;
      }
    },
  },
};
</script>

<style scoped>
.disable_button{
  background-color: #0000ff4d;
}
.SignupForm {
  height: 0px;
  overflow-y: scroll;
  transition: 1s ease;
}

.SignUpFormActive {
  height: 600px;
  transition: 1s ease;
}

.TextWhite {
  color: white !important;
}

.lightBlue {
  background-color: #1fb6ff !important;
  transition: 1s ease;
}
.lightBlue2 {
  background-color: #71d3ff !important;
  transition: 1s ease;
}
.lightBlue3 {
  background-color: #21b4fc !important;
  transition: 1s ease;
}

.lightBlue p small h2 h1 span {
  color: white !important;
}
.viewPassword {
  top: 0.4rem;
  left: 0;
}

.hitPassword {
  font-size: 12px;
}

.TabActive {
  background: #088dcd;
  color: white;
  font-size: 14px;
  padding: 10px 0;
  text-transform: uppercase;
  margin: 10px 0;
  cursor: pointer;
}

.deActiveTab {
  background: #eeeeee none repeat scroll 0 0;
  font-size: 14px;
  padding: 10px 0;
  text-transform: uppercase;
  margin: 10px 0;
  cursor: pointer;
}

.deActiveTab:hover {
  color: #2fa7cd;
}

.privacyAccept {
  color: #999;
  font-size: 13px;
  margin-top: 1rem;
  margin-bottom: 1rem;
}

input:focus {
  outline-width: 0 !important;
}

.login-reg-bg {
  background: #f4f7f6 none repeat scroll 0 0;
  height: 100vh;
}

.log-reg-area {
  background: #fff none repeat scroll 0 0;
  box-shadow: 0 0 35px #e2e2e2;
  display: block;
  left: 50%;
  padding: 35px 55px;
  top: 50%;
  transform: translate(-50%, -50%);
  width: 505px;
}

.log-reg-area > p {
  color: #878787;
  font-size: 14px;
}

.form-floating > .form-control {
  border-bottom: 0.0625rem solid #e1e8ed !important;
}

.form-floating > label {
  top: 10%;
  right: 0.3rem !important;
  padding: 0 !important;
  width: fit-content;
  color: #b7b7b7 !important;
  font-size: 1rem;
  font-weight: 400;
}

.form-floating > .form-control:focus > label {
  color: #088dcd !important;
}
.form-control:focus {
  border-bottom: 0.06rem solid #c4c4c4 !important;
  box-shadow: none !important;
}
.form-floating > .form-control:focus,
.form-floating > .form-control:focus:not(:placeholder-shown) {
  padding-bottom: 10px !important;
  /* border-bottom: 3px solid #088dcd !important; */
  transition: left 0.28s ease, width 0.28s ease;
  -webkit-transition: left 0.28s ease, width 0.28s ease;
  width: 100%;
  /* left: 50%; */
  /* outline-style: none !important; */
  color: #088dcd !important;
}

.form-control input {
  background: 0 0;
  padding: 0.125rem 0.125rem 0.0625rem;
  font-size: 1rem;
  border-width: 0;
  border-color: transparent;
  line-height: 1.9;
  width: 100%;
  color: transparent;
  -webkit-transition: all 0.28s ease;
  transition: all 0.28s ease;
  -webkit-box-shadow: none;
  box-shadow: none;
  outline-style: none !important;
}

.submitRegisterButton {
  font-size: 16px;
  background-color: #4f93ce;
  color: white;
  margin-top: 10px;
  padding: 7px 30px;
  cursor: pointer;
  -webkit-transition: background-color 0.28s ease, color 0.28s ease,
    -webkit-box-shadow 0.28s ease;
  transition: background-color 0.28s ease, color 0.28s ease,
    -webkit-box-shadow 0.28s ease;
  transition: background-color 0.28s ease, color 0.28s ease,
    box-shadow 0.28s ease;
  transition: background-color 0.28s ease, color 0.28s ease,
    box-shadow 0.28s ease, -webkit-box-shadow 0.28s ease;
  overflow: hidden;
  -webkit-box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.14),
    0 3px 1px -2px rgba(0, 0, 0, 0.2), 0 1px 5px 0 rgba(0, 0, 0, 0.12);
  box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.14), 0 3px 1px -2px rgba(0, 0, 0, 0.2),
    0 1px 5px 0 rgba(0, 0, 0, 0.12);
}

.submitRegisterButton:hover {
  background-color: #088dcd;
  -webkit-box-shadow: 0 6px 10px 0 rgba(0, 0, 0, 0.14),
    0 1px 18px 0 rgba(0, 0, 0, 0.12), 0 3px 5px -1px rgba(0, 0, 0, 0.2);
  box-shadow: 0 6px 10px 0 rgba(0, 0, 0, 0.14), 0 1px 18px 0 rgba(0, 0, 0, 0.12),
    0 3px 5px -1px rgba(0, 0, 0, 0.2);
}

.submitRegisterButton:active::before,
.submitRegisterButton:focus::before {
  -webkit-transition: opacity 0.28s ease 364ms, -webkit-transform 1.12s ease;
  transition: opacity 0.28s ease 364ms, -webkit-transform 1.12s ease;
  transition: transform 1.12s ease, opacity 0.28s ease 364ms;
  transition: transform 1.12s ease, opacity 0.28s ease 364ms,
    -webkit-transform 1.12s ease;
  -webkit-transform: translate(-50%, -50%) scale(1);
  transform: translate(-50%, -50%) scale(1);
  opacity: 0;
}

.submitRegisterButton:focus {
  outline-style: none;
}

.existAccount {
  color: #088dcd;
  text-decoration: none;
}

.existAccount:hover {
  border-bottom: 1px solid #088dcd;
}

.custom_radio input[type="radio"] {
  display: none;
}

.custom_radio input[type="radio"] + label {
  position: relative;
  display: inline-block;
  padding-left: 1.5em;
  margin-right: 2em;
  cursor: pointer;
  line-height: 1em;
  -webkit-transition: all 0.3s ease-in-out;
  transition: all 0.3s ease-in-out;
  color: #676767;
  font-size: 13px;
}

.custom_radio input[type="radio"] + label:before,
.custom_radio input[type="radio"] + label:after {
  content: "";
  position: absolute;
  top: 0;
  right: -1.5rem;
  width: 1em;
  height: 1em;
  text-align: center;
  color: white;
  border-radius: 50%;
  -webkit-transition: all 0.3s ease;
  transition: all 0.3s ease;
}

.custom_radio input[type="radio"] + label:before {
  -webkit-transition: all 0.3s ease;
  transition: all 0.3s ease;
  box-shadow: inset 0 0 0 0.2em white, inset 0 0 0 1em white;
  border: 1px solid #8f8e8e;
}

.custom_radio input[type="radio"] + label:hover:before {
  -webkit-transition: all 0.3s ease;
  transition: all 0.3s ease;
  box-shadow: inset 0 0 0 0.3em white, inset 0 0 0 1em #c6c6c6;
}

.custom_radio input[type="radio"]:checked + label:before {
  -webkit-transition: all 0.3s ease;
  transition: all 0.3s ease;
  box-shadow: inset 0 0 0 0.2em white, inset 0 0 0 1em #088dcd;
  border: 1px solid #088dcd;
}

.log-title {
  font-size: 2rem;
}

.log-title-text a {
  text-decoration: none;
  color: #088dcd;
}

/* Checkmark style starts */
@-moz-keyframes dothabottomcheck {
  0% {
    height: 0;
  }
  100% {
    height: 10px;
  }
}

@-webkit-keyframes dothabottomcheck {
  0% {
    height: 0;
  }
  100% {
    height: 10px;
  }
}

@keyframes dothabottomcheck {
  0% {
    height: 0;
  }
  100% {
    height: 10px;
  }
}

@keyframes dothatopcheck {
  0% {
    height: 0;
  }
  50% {
    height: 0;
  }
  100% {
    height: 20px;
  }
}

@-webkit-keyframes dothatopcheck {
  0% {
    height: 0;
  }
  50% {
    height: 0;
  }
  100% {
    height: 20px;
  }
}

@-moz-keyframes dothatopcheck {
  0% {
    height: 0;
  }
  50% {
    height: 0;
  }
  100% {
    height: 20px;
  }
}

input[type="checkbox"] {
  display: none;
}

.check-box {
  height: 1rem;
  width: 1rem;
  background-color: transparent;
  border: 0.125rem solid #999;
  border-radius: 0.0625rem;
  position: relative;
  display: inline-block;
  -moz-box-sizing: border-box;
  -webkit-box-sizing: border-box;
  box-sizing: border-box;
  -moz-transition: border-color ease 0.2s;
  -o-transition: border-color ease 0.2s;
  -webkit-transition: border-color ease 0.2s;
  transition: border-color ease 0.2s;
  cursor: pointer;
}

.check-box::before,
.check-box::after {
  -moz-box-sizing: border-box;
  -webkit-box-sizing: border-box;
  box-sizing: border-box;
  position: absolute;
  height: 0;
  width: 3px;
  background-color: #088dcd;
  display: inline-block;
  -moz-transform-origin: left top;
  -ms-transform-origin: left top;
  -o-transform-origin: left top;
  -webkit-transform-origin: left top;
  transform-origin: left top;
  border-radius: 5px;
  content: " ";
  -webkit-transition: opacity ease 0.5s;
  -moz-transition: opacity ease 0.5s;
  transition: opacity ease 0.5s;
}

.check-box::before {
  top: 10px;
  left: 7px;
  -moz-transform: rotate(-135deg);
  -ms-transform: rotate(-135deg);
  -o-transform: rotate(-135deg);
  -webkit-transform: rotate(-135deg);
  transform: rotate(-135deg);
}

.check-box::after {
  top: 4px;
  left: 0;
  -moz-transform: rotate(-45deg);
  -ms-transform: rotate(-45deg);
  -o-transform: rotate(-45deg);
  -webkit-transform: rotate(-45deg);
  transform: rotate(-45deg);
}

input[type="checkbox"]:checked + .check-box,
.check-box.checked {
  border: 0.125rem solid #088dcd;
}

input[type="checkbox"]:checked + .check-box::after,
.check-box.checked::after {
  height: 5px;
  -moz-animation: dothabottomcheck 0.2s ease 0s forwards;
  -o-animation: dothabottomcheck 0.2s ease 0s forwards;
  -webkit-animation: dothabottomcheck 0.2s ease 0s forwards;
  animation: dothabottomcheck 0.2s ease 0s forwards;
}

input[type="checkbox"]:checked + .check-box::before,
.check-box.checked::before {
  height: 8px;
  -moz-animation: dothatopcheck 0.4s ease 0s forwards;
  -o-animation: dothatopcheck 0.4s ease 0s forwards;
  -webkit-animation: dothatopcheck 0.4s ease 0s forwards;
  animation: dothatopcheck 0.4s ease 0s forwards;
}

@media screen and (max-width: 576px) {
  .log-reg-area {
    background: #fff none repeat scroll 0 0;
    box-shadow: 0 0 35px #e2e2e2;
    display: block;
    left: 50%;
    padding: 35px 55px;
    top: 50%;
    height: 100vh;
    transform: translate(-50%, -50%);
    width: 100%;
  }
}
</style>
