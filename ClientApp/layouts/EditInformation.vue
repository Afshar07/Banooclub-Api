<template>
  <div class="row SectionBox d-none d-lg-block rounded main-shadow">
    <div class="col-12 text-end">
      <span class="titleSection">ویرایش اطلاعات</span>
    </div>
    <div class="col-12 accountSetting">
      <div class="d-flex flex-row ">
        <div class="ms-2 text-secondary">
          <i class="fas fa-info-circle"></i>
        </div>
        <div class="accountSettingLink">
          <nuxt-link to="/social/accountsetting/editprofilebasic">
            اطلاعات اصلی
          </nuxt-link>
        </div>
      </div>
    </div>
<!--    <div class="col-12 accountSetting">-->
<!--      <div class="d-flex flex-row ">-->
<!--        <div class="ms-2 text-secondary">-->
<!--          <i class="fas fa-mouse"></i>-->
<!--        </div>-->
<!--        <div class="accountSettingLink">-->
<!--          <nuxt-link to="/Social/AccountSetting/EditWorkEducation">-->
<!--            آموزش و کار-->
<!--          </nuxt-link>-->
<!--        </div>-->
<!--      </div>-->
<!--    </div>-->
<!--    <div class="col-12 accountSetting">-->
<!--      <div class="d-flex flex-row ">-->
<!--        <div class="ms-2 text-secondary">-->
<!--          <i class="far fa-heart"></i>-->
<!--        </div>-->
<!--        <div class="accountSettingLink">-->
<!--          <nuxt-link to="/Social/AccountSetting/EditInterest">-->
<!--            علاقه مندی های من-->
<!--          </nuxt-link>-->
<!--        </div>-->
<!--      </div>-->
<!--    </div>-->
<!--    <div class="col-12 accountSetting">-->
<!--      <div class="d-flex flex-row ">-->
<!--        <div class="ms-2 text-secondary">-->
<!--          <i class="fas fa-cog"></i>-->
<!--        </div>-->
<!--        <div class="accountSettingLink">-->
<!--          <nuxt-link to="/Social/AccountSetting">-->
<!--            تنظیمات حساب کاربری-->
<!--          </nuxt-link>-->
<!--        </div>-->
<!--      </div>-->
<!--    </div>-->
    <div class="col-12 accountSetting">
      <div class="d-flex flex-row ">
        <div class="ms-2 text-secondary">
          <i class="fas fa-portrait"></i>
        </div>
        <div class="accountSettingLink">
          <nuxt-link to="/social">
            پست های من
          </nuxt-link>
        </div>
      </div>
    </div>
<!--    <div class="col-12 accountSetting">-->
<!--      <div class="d-flex flex-row ">-->
<!--        <div class="ms-2 text-secondary">-->
<!--          <i class="fas fa-infinity"></i>-->
<!--        </div>-->
<!--        <div class="accountSettingLink">-->
<!--          <nuxt-link to="/Social/AccountSetting/Statistics">-->
<!--            آمار بازدید پروفایل من-->
<!--          </nuxt-link>-->
<!--        </div>-->
<!--      </div>-->
<!--    </div>-->
<!--    <div class="col-12 accountSetting">-->
<!--      <div class="d-flex flex-row ">-->
<!--        <div class="ms-2 text-secondary">-->
<!--          <i class="fas fa-lock"></i>-->
<!--        </div>-->
<!--        <div class="accountSettingLink">-->
<!--          <nuxt-link to="/Social/AccountSetting/EditPassword">-->
<!--            تغییر رمز عبور-->
<!--          </nuxt-link>-->
<!--        </div>-->
<!--      </div>-->
<!--    </div>-->

  </div>
</template>

<script>
export default {
  name: "EditInformation"
}
</script>

<style scoped>
.titleSection {
  border-bottom: 2px solid #088dcd;
  background: rgba(0, 0, 0, 0) none repeat scroll 0 0;
  color: #424242;
  display: inline-block;
  font-size: 15px;
  margin-bottom: 30px;
  padding: 20px 0 10px;
  position: relative;
  text-transform: uppercase;
  width: max-content;
}

.SectionBox {
  margin: 0 auto;
  background: #fdfdfd;
  border: 1px solid #ede9e9;
  border-radius: 3px;
  width: 90%;
}

.accountSetting {
  margin-bottom: 20px;
}

.accountSettingLink a {
  color: #545454;
  text-decoration: none;
  font-size: 15px;
}

.accountSettingLink a:hover {
  color: #088dcd;
}
</style>
