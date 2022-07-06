<template>
  <div class="row container-fluid px-0 mx-0">
    <div class="col-md-6 col-12 px-0 order-last login-reg-bg position-relative">
      <div class="log-reg-area position-absolute" dir="rtl">
        <h2 class="log-title">فراموشی رمز عبور</h2>
        <!--<p class="log-title-text">
          هنوز از بانو کلاب استفاده نکرده اید؟
          <nuxt-link to="/" href="#" title="">قوانین را بخوانید</nuxt-link>
          یا
          <nuxt-link to="/Register" href="#" title=""> به
            اینجا ملحق شوید
          </nuxt-link>
        </p>-->
        <div class="row py-2">
          <div class="col-6 text-center" :class="withEmail?'TabActive':'deActiveTab'" @click="signInEmail">
            با ایمیل
          </div>
          <div class="col-6 border-end text-center" :class="withMobil?'TabActive':'deActiveTab'" @click="signInMobile">
            با شماره موبایل
          </div>
        </div>
        <form class="row" v-if="withMobil">
          <div class="col-12 mt-3">
            <div class="form-floating text-end">
              <input type="text" class="form-control border-0 border-bottom" id="userName" placeholder="نام کاربری"
                     v-model.trim="userNameMobile">
              <label for="userName">نام کاربری</label>
            </div>
          </div>
          <!--<div class="col-12 mt-3 position-relative">
            <div class="form-floating text-end">
              <input :type="fieldPassword" class="form-control border-0 border-bottom" id="password"
                     placeholder="رمز عبور"
                     v-model.trim="passwordMobile"/>
              <button class="btn position-absolute viewPassword" type="password" @click.prevent="showPassword"
                      :class="{ active: isActive }">
                <font-awesome-icon icon="eye" v-if="fieldPassword === 'password'"/>
                <font-awesome-icon icon="eye-slash" v-if="fieldPassword === 'text'"/>
              </button>
              <label for="password">رمز عبور</label>
            </div>
          </div>-->
          <!--<div class="text-start">
            <nuxt-link class="forgetPassword" to="Login">
              رمز عبور خود را فراموش کرده اید ؟
            </nuxt-link>
          </div>
          <div class="col-12 mt-0 mb-3">
            <div class="form-check">
              <input class="form-check-input" type="checkbox" value="" id="acceptCondition">
              <label class="form-check-label check-box" for="acceptCondition"></label>
              <span class="rememberMe">همیشه من را به یاد داشته باش</span>
            </div>
          </div>-->

          <div class="col-12 mt-3">
            <!--<nuxt-link to="/Register" type="button" class="btn btn-sm submitRegisterButton">
              ثبت نام
            </nuxt-link>-->
            <button type="button" class="btn btn-sm submitRegisterButton" @click="submitLoginFormMobile">
              ارسال رمز عبور
            </button>
          </div>
          <div class="text-start">
            <nuxt-link class="forgetPassword" to="Login">
              برو به صفحه لاگین
            </nuxt-link>
          </div>
        </form>
        <form class="row" v-if="withEmail">
          <div class="col-12 mt-3">
            <div class="form-floating text-end">
              <input type="text" class="form-control border-0 border-bottom" id="userNameEmail" placeholder="نام کاربری"
                     v-model.trim="userNameEmail">
              <label for="userNameEmail">نام کاربری</label>
            </div>
          </div>
          <!--<div class="col-12 mt-3 position-relative">
            <div class="form-floating text-end">
              <input :type="fieldPassword" class="form-control border-0 border-bottom" id="passwordEmail"
                     placeholder="رمز عبور"
                     v-model.trim="passwordEmail">
              <button class="btn position-absolute viewPassword" type="password" @click.prevent="showPassword"
                      :class="{ active: isActive }">
                <font-awesome-icon icon="eye" v-if="fieldPassword === 'password'"/>
                <font-awesome-icon icon="eye-slash" v-if="fieldPassword === 'text'"/>
              </button>
              <label for="passwordEmail">رمز عبور</label>
            </div>
          </div>
          <div class="text-start">
            <nuxt-link class="forgetPassword" to="Login">
              رمز عبور خود را فراموش کرده اید ؟
            </nuxt-link>
          </div>
          <div class="col-12 mt-0 mb-3">
            <div class="form-check">
              <input class="form-check-input" type="checkbox" value="" id="acceptConditionEmail">
              <label class="form-check-label check-box" for="acceptConditionEmail"></label>
              <span class="rememberMe">همیشه من را به یاد داشته باش</span>
            </div>
          </div>-->

          <div class="col-12 mt-3">
            <!--<nuxt-link to="/Register" type="button" class="btn btn-sm submitRegisterButton">
              ثبت نام
            </nuxt-link>-->
            <button type="button" class="btn btn-sm submitRegisterButton" @click="submitLoginFormEmail">
              ارسال رمز عبور
            </button>
          </div>
          <div class="text-start">
            <nuxt-link class="forgetPassword" to="Login">
              برو به صفحه لاگین
            </nuxt-link>
          </div>
        </form>
      </div>
    </div>
    <div class="col-md-6 col-12 px-0 order-0 ">
      <LoginBanner />
    </div>
  </div>
</template>

<script>
  import LoginBanner from "../components/LoginBanner";

  export default {
    name: "Login",
    loading: false,
    components: { LoginBanner },
    data() {
      return {
        userNameMobile: '',
        userNameEmail: '',
        passwordMobile: null,
        passwordEmail: null,
        withMobil: true,
        withEmail: false,
        email: 'string',
        firstName: 'string',
        lastName: 'string',
        verifyCode: 'string',
        encryptedMail: 'string',
        fieldPassword: "password",
        isActive: null
      }
    },

    mounted() {
      this.$nextTick(() => {
        this.$nuxt.$loading.finish();
      })
    },
    methods: {
      showPassword() {
        this.fieldPassword =
          this.fieldPassword === "password" ? "text" : "password";
        this.isActive = this.isActive === "active" ? "" : "active";
      },
      signInEmail() {
        this.withMobil = false;
        this.withEmail = true;
      },
      signInMobile() {
        this.withEmail = false;
        this.withMobil = true;
      },
      submitLoginFormMobile() {
        this.$nextTick(() => {
          this.$nuxt.$loading.start()
        })
        this.$axios.post(`Account/ForgotPassword`, null, {
          params: {
            PhoneOrEmail: this.userNameMobile,
            type: 1
          }
        }).then((response) => {
          this.$nuxt.$loading.finish();
          if (response.status === 200) {
            console.log(response.data)
            if (response.data.data.status === 6) {
              this.$toast.error('کاربری یافت نشد .')
            } else if (response.data.data.status === 7) {
              this.$toast.success('رمز عبور ارسال شد.')
              this.$router.push('/')
            }
          }
        }).catch((error) => {
          console.log(error)
        })
      },
      submitLoginFormEmail() {
        this.$axios.post(`Account/ForgotPassword`, null, {
          params: {
            PhoneOrEmail: this.userNameEmail,
            type: 2
          }
        }).then((response) => {
          this.$nuxt.$loading.finish();
          if (response.status === 200) {
            alert(response.data.data.status)
            if (response.data.status === 6) {
              this.$toast.error('کاربری یافت نشد .')
            } else if (response.data.data.status === 7) {
              this.$toast.success('رمز عبور ارسال شد.')
              this.$router.push('/')
            }
          }
        }).catch((error) => {
          console.log(error)
        })
      }
    },
  }
</script>

<style scoped>
  .viewPassword {
    top: 0.75rem;
    left: 0;
  }

  .TabActive {
    background: #088dcd;
    ;
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
      transition: left .28s ease, width .28s ease;
      -webkit-transition: left .28s ease, width .28s ease;
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
    -webkit-transition: all .28s ease;
    transition: all .28s ease;
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
    -webkit-transition: background-color .28s ease, color .28s ease, -webkit-box-shadow .28s ease;
    transition: background-color .28s ease, color .28s ease, -webkit-box-shadow .28s ease;
    transition: background-color .28s ease, color .28s ease, box-shadow .28s ease;
    transition: background-color .28s ease, color .28s ease, box-shadow .28s ease, -webkit-box-shadow .28s ease;
    overflow: hidden;
    -webkit-box-shadow: 0 2px 2px 0 rgba(0, 0, 0, .14), 0 3px 1px -2px rgba(0, 0, 0, .2), 0 1px 5px 0 rgba(0, 0, 0, .12);
    box-shadow: 0 2px 2px 0 rgba(0, 0, 0, .14), 0 3px 1px -2px rgba(0, 0, 0, .2), 0 1px 5px 0 rgba(0, 0, 0, .12);
  }

    .submitRegisterButton:hover {
      background-color: #088dcd;
      -webkit-box-shadow: 0 6px 10px 0 rgba(0, 0, 0, .14), 0 1px 18px 0 rgba(0, 0, 0, .12), 0 3px 5px -1px rgba(0, 0, 0, .2);
      box-shadow: 0 6px 10px 0 rgba(0, 0, 0, .14), 0 1px 18px 0 rgba(0, 0, 0, .12), 0 3px 5px -1px rgba(0, 0, 0, .2)
    }

    .submitRegisterButton:active::before,
    .submitRegisterButton:focus::before {
      -webkit-transition: opacity .28s ease 364ms, -webkit-transform 1.12s ease;
      transition: opacity .28s ease 364ms, -webkit-transform 1.12s ease;
      transition: transform 1.12s ease, opacity .28s ease 364ms;
      transition: transform 1.12s ease, opacity .28s ease 364ms, -webkit-transform 1.12s ease;
      -webkit-transform: translate(-50%, -50%) scale(1);
      transform: translate(-50%, -50%) scale(1);
      opacity: 0
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
        content: '';
        position: absolute;
        top: 0;
        right: -1.5rem;
        width: 1em;
        height: 1em;
        text-align: center;
        color: white;
        border-radius: 50%;
        -webkit-transition: all .3s ease;
        transition: all .3s ease;
      }

      .custom_radio input[type="radio"] + label:before {
        -webkit-transition: all .3s ease;
        transition: all .3s ease;
        box-shadow: inset 0 0 0 0.2em white, inset 0 0 0 1em white;
        border: 1px solid #8f8e8e;
      }

      .custom_radio input[type="radio"] + label:hover:before {
        -webkit-transition: all .3s ease;
        transition: all .3s ease;
        box-shadow: inset 0 0 0 0.3em white, inset 0 0 0 1em #c6c6c6;
      }

    .custom_radio input[type="radio"]:checked + label:before {
      -webkit-transition: all .3s ease;
      transition: all .3s ease;
      box-shadow: inset 0 0 0 0.2em white, inset 0 0 0 1em #088dcd;
      border: 1px solid #088dcd;
    }

  .log-title {
    font-size: 2rem;
  }

  .log-title-text a {
    text-decoration: none;
    color: #088dcd
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

  input[type=checkbox] {
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

    .check-box::before, .check-box::after {
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
      content: ' ';
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

    input[type=checkbox]:checked + .check-box, .check-box.checked {
      border: 0.125rem solid #088dcd;
    }

      input[type=checkbox]:checked + .check-box::after, .check-box.checked::after {
        height: 5px;
        -moz-animation: dothabottomcheck 0.2s ease 0s forwards;
        -o-animation: dothabottomcheck 0.2s ease 0s forwards;
        -webkit-animation: dothabottomcheck 0.2s ease 0s forwards;
        animation: dothabottomcheck 0.2s ease 0s forwards;
      }

      input[type=checkbox]:checked + .check-box::before, .check-box.checked::before {
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
