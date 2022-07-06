<template>
  <div class="row px-3">
    <div class="col-12 p-0 m-0">
      <div class="col-12 form-group mt-3">
        <div class="form-floating text-end">
          <input
            type="text"
            class=" w-100 with-border"
            :placeholder="inputPlaceholder"
            @input="setInputValue"
            :value="getInputValue"
            @keydown.prevent.enter="sendOtpCode"
            required
          />
        </div>
      </div>
    </div>
<!--    <div class="col-12 d-flex align-items-center justify-content-center mt-4">-->
<!--      <recaptcha @success="setCaptchaSuccess" />-->
<!--    </div>-->
    <div class="col-12 mt-3 text-center">
      <button
        type="button"
        class="tw-bg-blue-600 tw-font-semibold tw-p-3 tw-rounded-md tw-text-center tw-text-white tw-w-full mt-3"
        @click="sendOtpCode"
      >
        ارسال کد
      </button>
<!--      <button-->
<!--        type="button"-->
<!--        class="btn p-2 btn-sm submitRegisterButton"-->
<!--        @click="sendOtpCode"-->
<!--      >-->
<!--        ارسال کد-->
<!--      </button>-->
    </div>
  </div>
</template>

<script>
export default {
  emits: ["OtpSent", "getNumber", "getMail","setCounter","getCodeFields"],
  props: {
    registerType: {
      type: Number,
      default: 1,
    },
  },
  watch: {
    registerType: {
      handler(val) {
        this.mobile = null;
        this.mail = null;
      },
    },
  },
  data() {
    return {
      captcha: true,
      mobile: null,
      mail: null,
      counter:22000
    };
  },
  methods: {
    sendOtpCode() {
      // Handle both otp codes here
      if (this.registerType == 1) {
        this.sendMobileOtpCode();
      } else {
        this.sendEmailOtpCode();
      }
    },
    setInputValue(event) {
      // Handle both login with mobile/email in one component
      if (this.registerType == 1) {
        this.mobile = event.target.value;
        this.mail = "string";
        this.$emit("getCodeFields",this.mobile);

      } else {
        this.mail = event.target.value;
        this.mobile = "string";
        this.$emit("getCodeFields",this.mail);

      }

    },
    async sendEmailOtpCode() {
      try {
        this.$nuxt.$loading.start();

        if (!this.mail) {
          this.$toast.error("لطفا ایمیل خود را وارد کنید");
        } else {
          const response =
            await this.$repositories.sendOtpToEmail.sendOtpToEmail(this.mail);
          if (response.data.message === "Confirmation code has not expired") {
            this.$toast.error("کد تایید منقضی نشده است");
            this.$emit("setCounter",this.counter);
            this.$emit("OtpSent");
          } else if (response.data.hasUser === 1) {
            this.$toast.error("کاربری با این ایمیل قبلا ثبت نام کرده است");
            // this.$emit("OtpSent");
            this.$emit("setCounter",this.counter);
          } else {
            this.$emit("setCounter",this.counter);
            this.$toast.success("کد تایید برای شما ارسال شد");

            this.$emit("OtpSent");
            this.$emit("getMail", this.mail);
            // this.Time = new Date().getTime() + 250000;
          }
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
        if (!this.mobile) {
          this.$toast.error("لطفا موبایل خود را وارد کنید");
        } else if (this.mobile.length < 11) {
          this.$toast.error("شماره موبایل وارد شده معتبر نیست");
        } else {
          const response =
            await this.$repositories.sendOtpToMobile.sendOtpToMobile(
              this.mobile
            );
          if (response.data.message === "Confirmation code has not expired") {
            this.$toast.error("کد تایید منقضی نشده است");
            this.$emit("OtpSent");
            this.$emit("setCounter",this.counter);
          } else if (response.data.hasUser === 1) {
            this.$emit("setCounter",this.counter);
            this.$toast.error(
              "کاربری با این شماره موبایل قبلا ثبت نام کرده است"
            );
            // this.$emit("OtpSent");

          } else {
            this.$emit("setCounter",this.counter);
            this.$toast.success("کد تایید برای شما ارسال شد");
            this.$emit("OtpSent");
            this.$emit("getNumber", this.mobile);

            // this.Time = new Date().getTime() + 250000;
          }
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.$nuxt.$loading.finish();
      }
    },
    setCaptchaSuccess() {
      this.captcha = true;
    },
  },
  computed: {
    inputPlaceholder() {
      return this.registerType === 1 ? "شماره موبایل" : "ایمیل";
    },
    getInputValue() {
      return this.registerType === 1 ? this.mobile : this.mail;
    },
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
  top: 0.75rem;
  left: 0;
}

.hitPassword {
  font-size: 12px;
}

.notEqualPassword {
  border: 1px solid #f24756 !important;
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
