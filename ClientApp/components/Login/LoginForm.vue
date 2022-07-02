<template>
  <div class="tw-p-6 tw-space-y-4 tw-relative">
    <form class="row " autocomplete="off" >
      <div class="col-12 mt-3">
        <div class="form-floating text-end">
          <input
            autocomplete="off"
            type="text"
            class="with-border"
            :placeholder="inputPlaceholder"
            :value="getInputValue"
            @input="setInputValue"
            @keydown.prevent.enter="submitLogin"
          />
          <!--        <label for="">-->
          <!--          {{ inputPlaceholder }}-->
          <!--        </label>-->
        </div>
      </div>
      <div class="col-12 mt-3 position-relative">
        <div class="form-floating text-end">
          <input
            autocomplete="off"
            :type="fieldPassword"
            class="with-border"
            id="password"
            placeholder="رمز عبور"
            v-model.trim="password"
            @keydown.prevent.enter="submitLogin"
          />
          <button
            class="btn position-absolute viewPassword d-flex justify-content-center align-items-center"
            type="password"
            @click.prevent="showPassword"
            :class="{ active: !isShowingPassword }"
          >
            <font-awesome-icon icon="eye" v-if="fieldPassword === 'password'" />
            <font-awesome-icon icon="eye-slash" v-if="fieldPassword === 'text'" />
          </button>
          <!--        <label for="password">رمز عبور</label>-->
        </div>
        <div class="col-12 mt-0 mt-3">
          <div class="form-check d-flex align-items-center">
            <input
              class="form-check-input"
              type="checkbox"
              value=""
              id="acceptCondition"
            />
            <label class="form-check-label check-box" for="acceptCondition"></label>
            <span class="rememberMe px-2 pb-1">همیشه من را به یاد داشته باش</span>
          </div>
        </div>

        <button
          type="button"
          class="tw-bg-blue-600 tw-font-semibold tw-p-3 tw-rounded-md tw-text-center tw-text-white tw-w-full mt-3"
          @click="submitLogin"
        >
          ورود
        </button>

        <div class="text-center mt-3 pb-3" style="border-bottom: 1px solid #80808038">
          <nuxt-link class="forgetPassword text-decoration-none" to="Login">
            رمز عبور خود را فراموش کرده اید ؟
          </nuxt-link>
        </div>

      </div>
      <div class="col-12 mt-5 d-flex">
        <!--      <a href="/register" class="btn btn-sm submitRegisterButton"> ثبت نام </a>-->
        <button class="tw-no-underline tw-bg-green-600 hover:tw-bg-green-500 hover:tw-text-white tw-font-semibold tw-py-3 tw-px-5 tw-rounded-md tw-text-center tw-text-white tw-mx-auto"
                type="button" @click="openRegisterModal">
          حساب جدید بسازید
        </button>
      </div>
    </form>
    <RegisterModal :is_modal_open="is_modal_open" @close_modal="closeModal"/>
  </div>
<!--  <form class="tw-p-6 tw-space-y-4 tw-relative" autocomplete="off">-->
<!--    <input v-model="login_field" type="text" placeholder="ایمیل یا شماره موبایل" class="with-border" autocomplete="off">-->
<!--    <input v-model="password" type="password" placeholder="رمز عبور" class="with-border" autocomplete="off">-->
<!--    <button @click="setLoginMethod" type="button" class="tw-bg-blue-600 tw-font-semibold tw-p-3 tw-rounded-md tw-text-center tw-text-white tw-w-full">-->
<!--      ورود-->
<!--    </button>-->
<!--    <nuxt-link to="#" class="tw-text-blue-500 tw-text-center tw-block tw-no-underline"> رمز عبور خود را فراموش کرده اید؟ </nuxt-link>-->
<!--    <hr class="tw-mb-3.5" style="border-top: 1px rgb(176 179 183) solid">-->
<!--    <div class="tw-flex">-->
<!--      <button class="tw-no-underline tw-bg-green-600 hover:tw-bg-green-500 hover:tw-text-white tw-font-semibold tw-py-3 tw-px-5 tw-rounded-md tw-text-center tw-text-white tw-mx-auto"-->
<!--              type="button" @click="openModal">-->
<!--        حساب جدید بسازید-->
<!--      </button>-->
<!--    </div>-->
<!--  </form>-->

</template>

<script>

import RegisterModal from "../Register/RegisterModal";
import BaseModal from "../../components/utilities/BaseModal";
export default {
  components: {BaseModal, RegisterModal},
  props: {
    loginType: {
      type: Number,
      default: 1,
    },
  },
  watch: {
    loginType: {
      immediate: true,
      handler(val) {
        this.mail = null;
        this.mobile = null;
        this.password = null;
        this.isShowingPassword = false;
      },
    },
  },
  data() {
    return {
      is_modal_open:false,
      is_show_register_modal:false,
      mobile: null,
      mail: null,
      password: null,
      isShowingPassword: false,
    };
  },
  mounted() {
    console.log(this.$auth.loggedIn);
  },
  emits:['open_register_modal'],
  methods: {
    closeModal(){
      this.is_modal_open = false
    },
    openRegisterModal(){
      this.is_modal_open = true
      this.$emit('open_register_modal')
    },
    setInputValue(event) {
      if (this.loginType == 1) {
        this.mobile = event.target.value;
        this.mail = "string";
      } else {
        this.mail = event.target.value;
        this.mobile = "string";
      }
    },
    async submitLogin() {
      try {
        this.$nuxt.$loading.start();
        const response = await this.$auth.loginWith("local", {
          data: {
            mobile: this.mobile,
            mail: this.mail,
            firstName: "string",
            lastName: "string",
            password: this.password,
            verifyCode: "string",
            encryptedMail: "string",
            // userName: "string",
            userRole: 0,
            type: this.loginType,
          },
        });
        if (response.data.status === 3) {
          this.$toast.error("نام کاربری یا رمز عبور اشتباه است.");
        } else if (response.data.status === 4) {
          this.$toast.error(
            "کد ارسال شده منتقضی شده است،لطفا دوباره امتحان کنید."
          );
        } else if (response.data.status === 5) {
          this.$toast.error("کد وارد شده اشتباه است.");
        } else if (response.data.status === 6) {
          this.$toast.error("کاربری با این نام کاربری وجود ندارد.");
        } else if (response.data.status === 7) {
          await this.$auth.fetchUser();
        } else if (response.data.status === 8) {
          this.$toast.error("قبلا کاربری با این نام کاربری ثبت نام کرده است.");
        } else if (response.data.status === 9) {
          this.$toast.error("پسورد شما ایمن نیست.");
        } else if (response.data.status === 10) {
          this.$toast.error("نام کاربری قبلا ثبت نام کرده است.");
        } else if (response.data.status === 11) {
          this.$toast.error("اکانت شما غیرفعال شده است.");
        }
        // this.$store.dispatch("login", {
        //   payload: response.data.data,
        // });
        // this.$store.commit("setUser", {
        //   user: response.data.data.user,
        // });
        if (response.data.data.user.role === "Admin") {
          this.$router.push({ name: "SuperAdmin", path: "/SuperAdmin" });
        } else {
          this.$router.push({ path: "/social" });
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.$nuxt.$loading.finish();
      }
    },
    showPassword() {
      this.isShowingPassword = !this.isShowingPassword;
    },
  },
  computed: {
    fieldPassword() {
      return this.isShowingPassword ? "text" : "password";
    },
    inputPlaceholder() {
      return this.loginType === 1 ? "شماره موبایل" : "ایمیل";
    },
    getInputValue() {
      return this.loginType === 1 ? this.mobile : this.mail;
    },
  },
};
</script>

<style scoped>
.viewPassword {
  left: 0;
  bottom: 0;
  top: 0;
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

.form-floating > label {
  right: 0;
  width: fit-content;
  color: #b7b7b7 !important;
  font-size: 1rem;
  font-weight: 400;
}

.form-floating > .form-control {
  border-bottom: 0.0625rem solid #e1e8ed !important;
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

.rememberMe {
  color: #999;
  font-size: 13px;
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

.forgetPassword {
  text-decoration: none;
  color: #0d6efd;
  font-size: 14px;
}

.forgetPassword:hover {
  border-bottom: 1px solid #0d6efd;
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
