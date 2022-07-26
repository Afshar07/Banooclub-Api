<template>
  <div class="row container-fluid px-0 mx-0">
    <div
      class="col-md-6 px-0 col-12 order-0 order-md-1 login-reg-bg position-relative"
    >
      <div class="log-reg-area position-absolute" :class="GetBgClass" dir="rtl">
        <h2 class="log-title" :class="{ TextWhite: userType !== 0 }">
          ثبت نام
        </h2>
        <p class="log-title-text" :class="{ TextWhite: userType !== 0 }">
          از بانو کلاب استفاده میکنید ؟
          <nuxt-link
            to="/register"
            href="#"
            title=""
            :class="{ TextWhite: userType !== 0 }"
            >قوانین را بخوانید
          </nuxt-link>
          یا
          <nuxt-link
            to="/"
            href="#"
            title=""
            :class="{ TextWhite: userType !== 0 }"
          >
            اینجا کلیک کنید
          </nuxt-link>
        </p>
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
            :registerType="registerType"
          ></get-otp>

          <register-form
            v-if="isOtpSent"
            @getUserRole="setUserType"
            @getSignUpPayload="setSignUpPayload"
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
    <div class="col-md-6 px-0 col-12 order-1 order-md-0">
      <LoginBanner />
    </div>
  </div>
</template>

<script>
import LoginBanner from "../components/LoginBanner";
import GetOtp from "../components/Register/GetOtp.vue";
import RegisterForm from "../components/Register/RegisterForm.vue";

export default {
  head() {
    return {
      title: 'ثبت نام',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'ثبت نام',
        },
      ],
    }
  },
  name: "Register",
  components: { LoginBanner, GetOtp, RegisterForm },
  auth: "guest",
  data() {
    return {
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
    };
  },
  methods: {
    async sendSignUpRequest() {
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
            this.$router.push("/social");
            this.$auth.setUser(response.data.data.user);
          this.$toast.success("ثبت نام با موفقیت انجام شد.");
        } else if (response.data === 8) {
          this.$toast.error("قبلا کاربری با این نام کاربری ثبت نام کرده است.");
        } else if (response.data === 9) {
          this.$toast.error("پسورد شما ایمن نیست.");
        } else if (response.data === 10) {
          this.$toast.error("نام کاربری قبلا ثبت نام کرده است.");
        } else if (response.data === 11) {
          this.$toast.error("اکانت شما غیرفعال شده است.");
        }


      } catch (error) {
        console.log(error);
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
  mounted() {
    this.$nextTick(() => {
      this.$nuxt.$loading.finish();
    });
  },
};
</script>

<style scoped>
.SignupForm {
  height: 0px;
  overflow-y: scroll;
  transition: 1s ease;
}

.SignUpFormActive {
  height: 100%;
  max-height: 100vh;
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
  top: 0.75rem;
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
  right: 1rem !important;
  width: fit-content;
  color: #b7b7b7 !important;
  font-size: 1rem;
  font-weight: 400;
}

.form-floating > .form-control > label:focus {
  color: #088dcd !important;
}

.form-floating > .form-control:focus,
.form-floating > .form-control:focus:not(:placeholder-shown) {
  padding-bottom: 10px !important;
  border-bottom: 3px solid #088dcd !important;
  transition: left 0.28s ease, width 0.28s ease;
  -webkit-transition: left 0.28s ease, width 0.28s ease;
  width: 100%;
  left: 50%;
  outline-style: none !important;
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
