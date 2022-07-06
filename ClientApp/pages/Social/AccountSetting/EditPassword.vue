<template>
  <div class="container-fluid px-0">
    <gb-flag
      code="fr"
      size="small"
    />
    <div class="row boxMainContent">
      <div class="col-12">
        <h5 class="editAccountSetting text-black d-inline-block">
          <i class="fas fa-lock text-primary"></i>
          تغییر رمز عبور
        </h5>
      </div>
      <form class="row">
        <div class="col-12 mt-3">
          <div class="form-floating text-end">
            <input type="password" class="form-control border-0 " id="newPassword" placeholder="رمز عبور جدید"
                   v-model.trim="newPassword">
            <label for="newPassword">رمز عبور جدید</label>
          </div>
        </div>
        <div class="col-12 mt-3">
          <div class="form-floating text-end">
            <input type="password" class="form-control border-0 border-bottom" id="repeatNewPassword"
                   placeholder="تکرار رمز عبور"
                   v-model.trim="repeatNewPassword">
            <label for="repeatNewPassword">تکرار رمز عبور</label>
          </div>
        </div>
        <div class="col-12 my-3">
          <div class="form-floating text-end">
            <input type="password" class="form-control border-0 border-bottom" id="currentPassword"
                   placeholder="رمز عبور فعلی"
                   v-model.trim="currentPassword">
            <label for="currentPassword">رمز عبور فعلی</label>
          </div>
        </div>
        <div class="col-12 forgetPassword text-start">
          <nuxt-link to="/">
            رمز را فراموش کرده
            اید؟
          </nuxt-link>
        </div>
        <div class="col-12 mt-3">
          <button type="button" class="btn btn-sm ButtonAccountSetting">لغو</button>
          <button type="button" class="btn btn-sm ButtonAccountSetting" @click="submitEditPassword">به روز رسانی
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "EditPassword",
  layout: 'PoshtebamPlusLayout',
  data() {
    return {
      newPassword: '',
      repeatNewPassword: '',
      currentPassword: '',
      inCorrectRegex: false
    }
  },
  methods: {
    submitEditPassword() {
      if (this.newPassword === '' || this.repeatNewPassword === '' || this.currentPassword === '') {
        this.$toast.error('لطفا تمام فیلدها را پر کنید.')
      } else if (this.newPassword !== this.repeatNewPassword) {
        this.$toast.error('رمز عبور جدید با تکرار آن یکسان نیست.')
      } else if (!(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*]).{8,}$/.test(this.newPassword))) {
        this.$toast.error('رمز عبور ایمن نیست.')
      }
      else {
        this.$axios.post(`Users/Update`, {
          currentPassword: this.currentPassword,
          password: this.newPassword,
          userId: this.$auth.user.userId,
        }, {

        }).then((response) => {
          if (response.status === 200) {
            this.$toast.success('رمز عبور با موفقیت به روز رسانی شد.')
          }
        }).catch((error) => {
          console.log(error)
        })
      }
    },
    userInfoId(userId) {
      this.$axios.post(`Users/Get`, null, {
      
        params: {id: userId}
      }).then((response) => {
        if (response.status === 200) {
          this.currentPassword = response.data.password
        }
      }).catch((error) => {
        console.log(error)
      })
    },
  },
  mounted() {
    this.userInfoId(this.$auth.user.userId);
    this.$nextTick(() => {
      this.$nuxt.$loading.finish();
    })
  }
}
</script>

<style scoped>
.boxMainContent {
  background: #fdfdfd none repeat scroll 0 0;
  border: 1px solid #ede9e9;
  border-radius: 3px;
  display: inline-block;
  width: 100%;
  margin-bottom: 20px;
  padding: 25px;
}

.editAccountSetting {
  font-size: 18px;
  text-transform: capitalize;
  margin-bottom: 20px;
  padding-bottom: 20px;
  border-bottom: 3px solid #088dcd;
}

.form-floating > label {
  right: 0 !important;
  width: fit-content;
  color: #b7b7b7 !important;
  font-size: 1rem;
  font-weight: 400;
  bottom: 0 !important;
}

.form-floating > .form-control {
  border-bottom: 0.0625rem solid #e1e8ed !important;
}

.form-floating > .form-control:focus,
.form-floating > .form-control:focus:not(:placeholder-shown) {
  /*padding-bottom: 10px !important;*/
  border-bottom: 3px solid #088dcd !important;
  transition: left .28s ease, width .28s ease;
  -webkit-transition: left .28s ease, width .28s ease;
  width: 100%;
  left: 50%;
  outline-style: none !important;
}

.forgetPassword a {
  text-decoration: none;
  color: #088dcd;
  font-size: 14px;
  transition: all 0.2s linear 0s;
}

.forgetPassword a:hover {
  border-bottom: 1px solid #088dcd;
  transition: all 0.2s linear 0s;
}


.ButtonAccountSetting {
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

.ButtonAccountSetting:hover {
  background-color: #088dcd;
  -webkit-box-shadow: 0 6px 10px 0 rgba(0, 0, 0, .14), 0 1px 18px 0 rgba(0, 0, 0, .12), 0 3px 5px -1px rgba(0, 0, 0, .2);
  box-shadow: 0 6px 10px 0 rgba(0, 0, 0, .14), 0 1px 18px 0 rgba(0, 0, 0, .12), 0 3px 5px -1px rgba(0, 0, 0, .2)
}

.ButtonAccountSetting:active::before,
.ButtonAccountSetting:focus::before {
  -webkit-transition: opacity .28s ease 364ms, -webkit-transform 1.12s ease;
  transition: opacity .28s ease 364ms, -webkit-transform 1.12s ease;
  transition: transform 1.12s ease, opacity .28s ease 364ms;
  transition: transform 1.12s ease, opacity .28s ease 364ms, -webkit-transform 1.12s ease;
  -webkit-transform: translate(-50%, -50%) scale(1);
  transform: translate(-50%, -50%) scale(1);
  opacity: 0
}

.ButtonAccountSetting:focus {
  outline-style: none;
}


@media (max-width: 576px) {
  .boxMainContent {
    background: #fdfdfd none repeat scroll 0 0;
    border: 1px solid #ede9e9;
    border-radius: 3px;
    display: inline-block;
    width: 100%;
    margin-bottom: 20px;
    padding: 30px 15px;
  }
}
</style>
