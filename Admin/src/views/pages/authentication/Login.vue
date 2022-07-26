<template>
  <div class="auth-wrapper auth-v2">
    <b-row class="auth-inner m-0">

      <!-- Brand logo-->
      <b-link class="brand-logo">
        <vuexy-logo />
<!--        <h2 class="brand-text text-primary ml-1">-->
<!--          Vuexy-->
<!--        </h2>-->
      </b-link>
      <!-- /Brand logo-->

      <!-- Left Text-->
      <b-col
        lg="8"
        class="d-none d-lg-flex align-items-center p-5"
      >
        <div class="w-100 d-lg-flex align-items-center justify-content-center px-5">
          <b-img
            fluid
            :src="imgUrl"
            alt="Login V2"
          />
        </div>
      </b-col>
      <!-- /Left Text-->

      <!-- Login-->
      <b-col
        lg="4"
        class="d-flex align-items-center auth-bg px-2 p-lg-5"
      >
        <b-col
          sm="8"
          md="6"
          lg="12"
          class="px-xl-2 mx-auto"
        >
          <b-card-title
            class="mb-1 font-weight-bold"
            title-tag="h2"
          >
            به پنل ادمین بانو کلاب خوش آمدید 👋
          </b-card-title>
          <b-card-text class="mb-2">
            لطفا به حساب کاربری خود وارد شوید.
          </b-card-text>


          <!-- form -->
          <validation-observer
            ref="loginForm"
            #default="{invalid}"
          >
            <b-form
              class="auth-login-form mt-2"
              @submit.prevent="login"
            >
              <!-- email -->
              <b-form-group
                label="تلفن همراه"
                label-for="login-mobile"
              >
                <validation-provider
                  #default="{ errors }"
                  name="Mobile"
                  vid="mobile"
                  rules="required|integer"
                >
                  <b-form-input
                    id="login-mobile"
                    :state="errors.length > 0 ? false:null"
                    v-model="mobile"
                    name="login-mobile"
                    placeholder="---------09"
                  />
                  <small class="text-danger">{{ errors[0] }}</small>
                </validation-provider>
              </b-form-group>

              <!-- forgot password -->
              <b-form-group>
                <div class="d-flex justify-content-between">
                  <label for="login-password">رمز عبور</label>
                  <b-link :to="{name:'auth-forgot-password'}">
<!--                    <small>Forgot Password?</small>-->
                  </b-link>
                </div>
                <validation-provider
                  #default="{ errors }"
                  name="Password"
                  vid="password"
                  rules="required"
                >
                  <b-input-group
                    class="input-group-merge"
                    :class="errors.length > 0 ? 'is-invalid':null"
                  >
                    <b-form-input
                      id="login-password"
                      v-model="password"
                      :state="errors.length > 0 ? false:null"
                      class="form-control-merge"
                      :type="passwordFieldType"
                      name="login-password"
                      placeholder="*****"
                    />
                    <b-input-group-append is-text>
                      <feather-icon
                        class="cursor-pointer"
                        :icon="passwordToggleIcon"
                        @click="togglePasswordVisibility"
                      />
                    </b-input-group-append>
                  </b-input-group>
                  <small class="text-danger">{{ errors[0] }}</small>
                </validation-provider>
              </b-form-group>

              <!-- checkbox -->
              <b-form-group>
                <b-form-checkbox
                  id="remember-me"
                  v-model="status"
                  name="checkbox-1"
                >
                  مرا به خاطر بسپار
                </b-form-checkbox>
              </b-form-group>

              <!-- submit buttons -->
              <b-button
                type="submit"
                variant="primary"
                block
                :disabled="invalid"
              >
                ورود
              </b-button>

            </b-form>
          </validation-observer>

<!--          <b-card-text class="text-center mt-2">-->
<!--            <span>New on our platform? </span>-->
<!--            <b-link :to="{name:'auth-register'}">-->
<!--              <span>&nbsp;Create an account</span>-->
<!--            </b-link>-->
<!--          </b-card-text>-->

<!--          &lt;!&ndash; divider &ndash;&gt;-->
<!--          <div class="divider my-2">-->
<!--            <div class="divider-text">-->
<!--              or-->
<!--            </div>-->
<!--          </div>-->

<!--          &lt;!&ndash; social buttons &ndash;&gt;-->
<!--          <div class="auth-footer-btn d-flex justify-content-center">-->
<!--            <b-button-->
<!--              variant="facebook"-->
<!--              href="javascript:void(0)"-->
<!--            >-->
<!--              <feather-icon icon="FacebookIcon" />-->
<!--            </b-button>-->
<!--            <b-button-->
<!--              variant="twitter"-->
<!--              href="javascript:void(0)"-->
<!--            >-->
<!--              <feather-icon icon="TwitterIcon" />-->
<!--            </b-button>-->
<!--            <b-button-->
<!--              variant="google"-->
<!--              href="javascript:void(0)"-->
<!--            >-->
<!--              <feather-icon icon="MailIcon" />-->
<!--            </b-button>-->
<!--            <b-button-->
<!--              variant="github"-->
<!--              href="javascript:void(0)"-->
<!--            >-->
<!--              <feather-icon icon="GithubIcon" />-->
<!--            </b-button>-->
<!--          </div>-->
        </b-col>
      </b-col>
    <!-- /Login-->
    </b-row>
  </div>
</template>

<script>
/* eslint-disable global-require */
import { ValidationProvider, ValidationObserver } from 'vee-validate'
import VuexyLogo from '@core/layouts/components/Logo.vue'
import {
  BRow, BCol, BLink, BFormGroup, BFormInput, BInputGroupAppend, BInputGroup, BFormCheckbox, BCardText, BCardTitle, BImg, BForm, BButton, BAlert, VBTooltip,BOverlay
} from 'bootstrap-vue'
import useJwt from '@/auth/jwt/useJwt'
import { required, email , integer} from '@validations'
import { togglePasswordVisibility } from '@core/mixins/ui/forms'
import store from '@/store/index'
import { getHomeRouteForLoggedInUser } from '@/auth/utils'

import ToastificationContent from '@core/components/toastification/ToastificationContent.vue'

export default {
  directives: {
    'b-tooltip': VBTooltip,
  },
  components: {
    BRow,
    BCol,
    BLink,
    BFormGroup,
    BFormInput,
    BInputGroupAppend,
    BInputGroup,
    BFormCheckbox,
    BCardText,
    BCardTitle,
    BImg,
    BForm,
    BButton,
    BAlert,
    VuexyLogo,
    BOverlay,

    ValidationProvider,
    ValidationObserver,
  },
  mixins: [togglePasswordVisibility],
  data() {
    return {
      status: '',
      password: '',
      mobile:'',
      showOverlay:false,
      // userEmail: 'admin@demo.com',
      sideImg: require('@/assets/images/pages/login-v2.svg'),

      // validation rules
      required,
      integer
    }
  },
  computed: {
    passwordToggleIcon() {
      return this.passwordFieldType === 'password' ? 'EyeIcon' : 'EyeOffIcon'
    },
    imgUrl() {
      if (store.state.appConfig.layout.skin === 'dark') {
        // eslint-disable-next-line vue/no-side-effects-in-computed-properties
        this.sideImg = require('@/assets/images/pages/login-v2-dark.svg')
        return this.sideImg
      }
      return this.sideImg
    },
  },
  methods: {
    login() {
      this.$refs.loginForm.validate().then(success => {
        if (success) {
          useJwt.login({
            mobile: this.mobile,
            mail: 'string',
            firstName: "string",
            lastName: "string",
            password: this.password,
            verifyCode: "string",
            encryptedMail: "string",
            // userName: "string",
            userRole: 0,
            type: 1,
            nationalId: "string",
            instagramId: "string",
            sexuality: 1,
            introducerCode: "string"
          })
              .then(response => {

                // Errors
                if (response.data.status === 3) {
                  return this.$toast({
                    component: ToastificationContent,
                    position: 'bottom-left',
                    props: {
                      variant: 'danger',
                      text: "نام کاربری یا رمز عبور اشتباه است.",
                    },
                  })
                } else if (response.data.status === 4) {
                  return  this.$toast({
                    component: ToastificationContent,
                    position: 'bottom-left',
                    props: {
                      variant: 'danger',
                      text: "کد ارسال شده منتقضی شده است،لطفا دوباره امتحان کنید.",
                    },
                  })
                } else if (response.data.status === 5) {
                  return  this.$toast({
                    component: ToastificationContent,
                    position: 'bottom-left',
                    props: {
                      variant: 'danger',
                      text: "کد وارد شده اشتباه است.",
                    },
                  })
                } else if (response.data.status === 6) {
                  return  this.$toast({
                    component: ToastificationContent,
                    position: 'bottom-left',
                    props: {
                      variant: 'danger',
                      text: "کاربری با این نام کاربری وجود ندارد.",
                    },
                  })
                } else if (response.data.status === 8) {
                  return  this.$toast({
                    component: ToastificationContent,
                    position: 'bottom-left',
                    props: {
                      variant: 'danger',
                      text: "قبلا کاربری با این نام کاربری ثبت نام کرده است.",
                    },
                  })
                } else if (response.data.status === 9) {
                  return  this.$toast({
                    component: ToastificationContent,
                    position: 'bottom-left',
                    props: {
                      variant: 'danger',
                      text: "پسورد شما ایمن نیست.",
                    },
                  })
                } else if (response.data.status === 10) {
                  return  this.$toast({
                    component: ToastificationContent,
                    position: 'bottom-left',
                    props: {
                      variant: 'danger',
                      text: "نام کاربری قبلا ثبت نام کرده است.",
                    },
                  })
                } else if (response.data.status === 11) {
                  return  this.$toast({
                    component: ToastificationContent,
                    position: 'bottom-left',
                    props: {
                      variant: 'danger',
                      text: "اکانت شما غیرفعال شده است.",
                    },
                  })
                }

                // Success
                const userData = response.data
                const userRole = userData.data.user.role;

                if(userRole === 'Subscriber'){
                  return  this.$toast({
                    component: ToastificationContent,
                    position: 'bottom-left',
                    props: {
                      variant: 'danger',
                      text: "شما اجازه ورود به پنل ادمین ندارید.",
                    },
                  })
                }

                userData["ability"] = [
                  {
                    action: "manage",
                    subject: "all",
                  },
                ];

                if(userRole === 'Blogger'){
                  userData.ability[0].action = 'Blogger';
                  userData.ability[0].subject = 'Blogger';
                }
                if(userRole === 'Accountent'){
                  userData.ability[0].action = 'Accountent';
                  userData.ability[0].subject = 'Accountent';
                }
                if(userRole === 'ProductManager'){
                  userData.ability[0].action = 'ProductManager';
                  userData.ability[0].subject = 'ProductManager';
                }
                if(userRole === 'Support'){
                  userData.ability[0].action = 'Support';
                  userData.ability[0].subject = 'Support';
                }

                useJwt.setToken(response.data.data.token)
                useJwt.setRefreshToken(response.data.data.token)
                localStorage.setItem('userData', JSON.stringify(userData))
                this.$ability.update(userData.ability)

                // this.$store.commit('app-ecommerce/UPDATE_CART_ITEMS_COUNT', userData.extras.eCommerceCartItemsCount)

                // ? This is just for demo purpose. Don't think CASL is role based in this case, we used role in if condition just for ease
                this.$router.replace(getHomeRouteForLoggedInUser(userRole))
                    .then(() => {
                      this.$toast({
                        component: ToastificationContent,
                        position: 'bottom-left',
                        props: {
                          title: `خوش آمدید ${userData.data.user.name}`,
                          icon: 'CoffeeIcon',
                          variant: 'success',
                          text: `شما به عنوان ${userData.data.user.role} وارد شدید.`,
                        },
                      })
                    })
              })
              .catch(error => {
                console.log(error)
                // this.$refs.loginForm.setErrors(error.response.data.error)
              })
        }
      })
    },
  },
}
</script>

<style lang="scss">
@import '@core/scss/vue/pages/page-auth.scss';
</style>
