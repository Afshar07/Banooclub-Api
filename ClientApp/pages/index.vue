<template>
  <div>
    <div v-if="register_modal_is_open" class="tw-z-[99] tw-fixed tw-w-screen tw-h-screen tw-top-0 tw-left-0 tw-bg-gray-200 tw-bg-opacity-80"></div>
    <div class="lg:tw-flex tw-max-w-5xl tw-min-h-screen tw-mx-auto tw-p-6 tw-py-10">
      <div class="tw-flex tw-flex-col tw-items-center lg:tw-flex-row lg:tw-space-x-10">

        <div class="lg:tw-mb-12 tw-flex-1 lg:tw-text-left tw-text-center">
          <p class="tw-font-medium lg:tw-mx-0 md:tw-text-2xl tw-mt-6 tw-mx-auto sm:tw-w-3/4 tw-text-xl tw-text-right" style="color:rgb(102 102 102);">
            بانو کلاب،
            با دوستان و دنیای اطراف خود با بانو کلاب ارتباط برقرار کنید.
          </p>
        </div>
        <div class="d-flex flex-column">
          <div class="lg:tw-mt-0 lg:tw-w-96 tw-mt-10 tw-w-full custom_navs tw-bg-white tw-shadow-lg tw-rounded-lg">
            <div class="d-flex justify-content-between">
              <div
                class="text-center col-6 my-0"
                :class="loginType == 2 ? 'TabActive' : 'deActiveTab'"
                @click="setLoginMethod(2)"
              >
                ورود با ایمیل
              </div>
              <div
                class="border-end text-center col-6 my-0"
                :class="loginType == 1 ? 'TabActive' : 'deActiveTab'"
                @click="setLoginMethod(1)"
              >
                ورود با شماره موبایل
              </div>
            </div>
            <LoginForm :login-type="loginType" @open_register_modal="openRegisterModal()" @close_register_modal="closeRegisterModal()"/>

          </div>
<!--          <div class="tw-mt-8 tw-text-center tw-text-sm">-->
<!--            <nuxt-link to="#" class="tw-font-semibold hover:tw-underline tw-no-underline">یک صفحه</nuxt-link> برای افراد مشهور، گروه موسیقی یا تجارت ایجاد کنید-->
<!--          </div>-->

        </div>

      </div>
    </div>
  </div>
<!--  <div class="row container-fluid px-0 mx-0">-->
<!--    <div class="col-md-6 col-12 px-0 order-0 order-md-1 login-reg-bg position-relative">-->
<!--      <div class="log-reg-area position-absolute" dir="rtl">-->
<!--        <h2 class="log-title">ورود</h2>-->
<!--        <p class="log-title-text">-->
<!--          هنوز از بانو کلاب استفاده نکرده اید؟-->
<!--          <nuxt-link to="/" title="">قوانین را بخوانید</nuxt-link>-->
<!--          یا-->
<!--          <nuxt-link to="/register" title=""> به اینجا ملحق شوید </nuxt-link>-->
<!--        </p>-->
<!--        <div class="row py-2">-->
<!--          <div-->
<!--            class="col-6 text-center"-->
<!--            :class="loginType == 2 ? 'TabActive' : 'deActiveTab'"-->
<!--            @click="setLoginMethod(2)"-->
<!--          >-->
<!--            ورود با ایمیل-->
<!--          </div>-->
<!--          <div-->
<!--            class="col-6 border-end text-center"-->
<!--            :class="loginType == 1 ? 'TabActive' : 'deActiveTab'"-->
<!--            @click="setLoginMethod(1)"-->
<!--          >-->
<!--            ورود با شماره موبایل-->
<!--          </div>-->
<!--        </div>-->
<!--        <login-form :loginType="loginType"></login-form>-->
<!--      </div>-->
<!--    </div>-->
<!--    <div class="col-md-6 col-12 px-0 order-1 order-md-0">-->
<!--      <LoginBanner />-->
<!--    </div>-->
<!--  </div>-->

</template>

<script>
import LoginBanner from "../components/LoginBanner";
import LoginForm from "../components/Login/LoginForm.vue";
import RegisterModal from "../components/Register/RegisterModal";

export default {
  name: "Login",
  loading: false,
  auth: "guest",
  head(){
    return{
      title:'ثبت نام'
    }
  },
  components: {RegisterModal, LoginBanner, LoginForm },
  data() {
    return {
      login_field:'',
      is_modal_open: false,
      register_modal_is_open:false,
      isActive: null,
      loginType: 1,
    };
  },

  mounted() {
    this.$nextTick(() => {
      this.$nuxt.$loading.finish();
    });
  },
  methods: {
    closeRegisterModal(){
      this.register_modal_is_open = false

    },
    openRegisterModal(){
      this.register_modal_is_open = true
    },
    showPassword() {
      this.fieldPassword =
        this.fieldPassword === "password" ? "text" : "password";
      this.isActive = this.isActive === "active" ? "" : "active";
    },
    closeModal(){
      this.is_modal_open = false
    },
    setLoginMethod(type) {
      this.loginType = type;
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
          this.$auth.setUser(response.data.data.user);
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
          this.$router.push({ path: "/social" });
        } else {
          this.$router.push({ path: "/social" });
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.$nuxt.$loading.finish();
      }
    },
  },
};
</script>

<style scoped>
select, textarea, input, input[type="text"], input[type="password"], input[type="email"], input[type="number"] {
  height: 46px;
  line-height: 46px;
  padding: 0 20px;
  outline: none;
  font-size: 15px;
  color: #808080;
  max-width: 100%;
  width: 100%;
  -webkit-box-sizing: border-box;
  box-sizing: border-box;
  display: block;
  background-color: #fff;
  font-weight: 400;
  opacity: 1;
  border-radius: 6px;
  border: none;
}

input.with-border, input[type="text"].with-border, input[type="password"].with-border, input[type="email"].with-border, input[type="number"].with-border {
  border: 1px solid #e0e0e0;
}

/* With Border */
textarea.with-border, select.with-border {
  border: 1px solid #e0e0e0;
}

input.with-border:focus, input[type="text"].with-border:focus, input[type="password"].with-border:focus, input[type="email"].with-border:focus, input[type="number"].with-border:focus {
  border: 1px solid #e0e0e0;
}

textarea.with-border:focus {
  border: 1px solid #e0e0e0;
}


.viewPassword {
  top: 0.75rem;
  left: 0;
}

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
  left: 80%;
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
  color: #088dcd;
  font-size: 14px;
}

.forgetPassword:hover {
  border-bottom: 1px solid #088dcd;
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
