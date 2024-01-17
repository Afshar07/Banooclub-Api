<template>
  <div class="t-w-full tw-min-h-screen tw-bg-white ">

    <div class="tw-w-full tw-min-h-screen   tw-grid lg:tw-grid-cols-2 tw-grid-cols-1">
      <div
        class="tw-flex tw-relative   lg:tw-order-first   tw-order-last  tw-items-center tw-flex-col  tw-gap-5 tw-justify-center">
        <!--        <div class="  gap-2 tw-items-center  lg:tw-flex tw-hidden tw-absolute tw-top-4 tw-right-4 ">-->
        <!--          <span class="text-purple tw-text-[50px] tw-font-bold">بانوی</span>-->
        <!--          <span class="text-pink tw-text-[50px]  tw-font-bold">با انگیزه</span>-->
        <!--        </div>-->
        <!--        <div class="tw-flex tw-gap-5 tw-flex-col sm:tw-flex-row  tw-items-center">-->
        <!--          <div-->
        <!--            class=" tw-flex tw-flex-col bg-white  tw-items-center tw-shadow-xl tw-border-purple-200 tw-w-[18rem]  tw-border  p-3 tw-shadow-purple-400 tw-rounded ">-->
        <!--            <img src="/item.png" class="tw-w-[10rem] tw-h-[10rem] tw-object-contain" alt="">-->
        <!--            <span class="text-pink my-3 tw-font-bold">آگهی نیازمندی های بانوان</span>-->
        <!--            <p class="text-purple  text-center" style="overflow-wrap: anywhere">خرید و فروش کالای نو و دست دوم مخصوص-->
        <!--              بانوان</p>-->

        <!--          </div>-->
        <!--          <div-->
        <!--            class=" tw-flex tw-flex-col bg-white  tw-items-center tw-shadow-xl tw-border-purple-200 tw-w-[18rem]  tw-border  p-3 tw-shadow-purple-400 tw-rounded ">-->
        <!--            <img src="/item2.png" class="tw-w-[10rem] tw-h-[10rem] tw-object-contain" alt="">-->
        <!--            <span class="text-pink my-3 tw-font-bold">شادی تفریح و سرگرمی</span>-->
        <!--            <p class="text-purple text-center  " style="overflow-wrap: anywhere">خدمات سفر,تور,گردشگری,آموزشی,آرایشی و-->
        <!--              بهداشتی</p>-->
        <!--          </div>-->
        <!--        </div>-->
        <img alt="" src="/banoclub-logo.png" style="width: 25rem!important;height: 15rem!important;">
      </div>
      <div class="tw-flex lg:tw-order-last tw-bg-purple-200  tw-order-first tw-items-center tw-justify-center">
        <div class="d-flex flex-column justify-content-center align-items-center tw-p-5">

          <div
            class="lg:tw-mt-0 lg:tw-w-[30rem] tw-mt-10 tw-w-full custom_navs tw-shadow-xl tw-shadow-pink-300 tw-border-pink-200  tw-border tw-bg-white tw-shadow-lg tw-rounded-lg">
            <div class="d-flex justify-content-center">
              <div class="border-end text-center col-6 my-0 TabActive">
                ورود با شماره موبایل
              </div>
            </div>
            <LazyLoginForm/>
          </div>
        </div>
      </div>
    </div>
    <LoginFooter></LoginFooter>
  </div>
</template>

<script>


export default {
  name: "Login",
  loading: false,
  auth: "guest",
  head() {
    return {
      title: 'ثبت نام'
    }
  },


  mounted() {
    this.$nextTick(() => {
      this.$nuxt.$loading.finish();
      this.$nuxt.loading = false;
    })
  },
  methods: {},
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

.bgImg {
  background: url("/21.png") center no-repeat;
  background-size: contain;

}

.TabActive {
  background: white;
  border-bottom: 3px solid #b44aff;
  color: #b44aff;
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
