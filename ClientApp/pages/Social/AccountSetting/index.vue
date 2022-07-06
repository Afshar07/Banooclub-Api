<template>
  <div class="container-fluid px-0">
    <div class="row boxMainContent">
      <div class="col-12">
        <h5 class="editAccountSetting text-black d-inline-block">
          <font-awesome-icon icon="cog" color="#088dcd"/>
          تنظیمات حساب کاربری
        </h5>
      </div>
      <div class="col-12">
        <p class="editAccountSettingText">
          در این بخش تنظمات مربوط به پانل کاربری در اختیار کاربر قرار داده خواهد شد.در این بخش کاربر قادر خواهد بود
          امکان دریافت یک نسخه از ایمیل های دریافتی به ایمیل ثبت شده در پروفایل خود را فعال یا غیرفعال نماید
        </p>
      </div>
      <div class="col-12 setting-row">
        <div class="d-flex flex-row">
          <div class="ms-auto">
            <div class="d-flex flex-column">
              <div class="setting-row-title">زیر کاربر</div>
              <div class="setting-row-text">اگر میخواهید مردم شما را دنبال کنند، این را فعال کنید</div>
            </div>
          </div>
          <div class="me-auto pt-4">
            <label class="switch">
              <input type="checkbox" id="togBtn1">
              <div class="slider round">
                <span class="on">روشن</span>
                <span class="off">خاموش</span>
              </div>
            </label>
          </div>
        </div>
      </div>
      <div class="col-12 setting-row">
        <div class="d-flex flex-row">
          <div class="ms-auto">
            <div class="d-flex flex-column">
              <div class="setting-row-title"> فعال کردن دنبال کردن من</div>
              <div class="setting-row-text">اگر میخواهید مردم شما را دنبال کنند، این را فعال کنید</div>
            </div>
          </div>
          <div class="me-auto pt-4">
            <label class="switch">
              <input type="checkbox" id="togBtn2">
              <div class="slider round">
                <span class="on">روشن</span>
                <span class="off">خاموش</span>
              </div>
            </label>
          </div>
        </div>
      </div>
      <div class="col-12 setting-row">
        <div class="d-flex flex-row">
          <div class="ms-auto">
            <div class="d-flex flex-column">
              <div class="setting-row-title">ارسال اعلانهای من</div>
              <div class="setting-row-text">ارسال ایمیل یا پیام پسندیدن دوستان من</div>
            </div>
          </div>
          <div class="me-auto pt-4">
            <label class="switch">
              <input type="checkbox" id="togBtn3">
              <div class="slider round">
                <span class="on">روشن</span>
                <span class="off">خاموش</span>
              </div>
            </label>
          </div>
        </div>
      </div>
      <div class="col-12 setting-row">
        <div class="d-flex flex-row">
          <div class="ms-auto">
            <div class="d-flex flex-column">
              <div class="setting-row-title"> پیام متنی</div>
              <div class="setting-row-text">پیامهای من به تلفن همراه من ارسال کنید</div>
            </div>
          </div>
          <div class="me-auto pt-4">
            <label class="switch">
              <input type="checkbox" id="togBtn4">
              <div class="slider round">
                <span class="on">روشن</span>
                <span class="off">خاموش</span>
              </div>
            </label>
          </div>
        </div>
      </div>
      <div class="col-12 setting-row">
        <div class="d-flex flex-row">
          <div class="ms-auto">
            <div class="d-flex flex-column">
              <div class="setting-row-title"> فعال کردن ضمیمه کردن</div>
              <div class="setting-row-text">دوستان من را فعال کنید تا من را در پستهایشان برچسب گذاری کنند</div>
            </div>
          </div>
          <div class="me-auto pt-4">
            <label class="switch">
              <input type="checkbox" id="togBtn5">
              <div class="slider round">
                <span class="on">روشن</span>
                <span class="off">خاموش</span>
              </div>
            </label>
          </div>
        </div>
      </div>
      <div class="col-12 setting-row">
        <div class="d-flex flex-row">
          <div class="ms-auto">
            <div class="d-flex flex-column">
              <div class="setting-row-title"> فعال کردن صدای اعلان</div>
              <div class="setting-row-text">هنگامی که کسی یک پیام خصوصی به شما ارسال می کند صدای اعلان را می شنوید</div>
            </div>
          </div>
          <div class="me-auto pt-4">
            <label class="switch">
              <input type="checkbox" id="togBtn6">
              <div class="slider round">
                <span class="on">روشن</span>
                <span class="off">خاموش</span>
              </div>
            </label>
          </div>
        </div>
      </div>
      <div class="col-12">
        <button class="btn btn-sm ButtonAccountSetting">لغو</button>
        <button class="btn btn-sm ButtonAccountSetting">به روز رسانی</button>
      </div>

    </div>
  </div>
</template>

<script>
export default {
  name: "index",
  layout: 'PoshtebamPlusLayout',
  mounted() {
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

.editAccountSettingText {
  font-size: 14px;
  line-height: 26px;
  color: #8c8b8b;
}


.switch {
  position: relative;
  display: inline-block;
  width: 62px;
  height: 20px;
}

.switch input {
  display: none;
}

.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #999;
  -webkit-transition: .4s;
  transition: .4s;
}

.slider:before {
  position: absolute;
  content: "";
  height: 14px;
  width: 14px;
  left: 3px;
  bottom: 3px;
  background-color: white;
  -webkit-transition: .4s;
  transition: .4s;
}

input:checked + .slider {
  background-color: #088dcd;
}

input:focus + .slider {
  box-shadow: 0 0 1px #2196F3;
}

input:checked + .slider:before {
  -webkit-transform: translateX(42px);
  -ms-transform: translateX(42px);
  transform: translateX(42px);
}

/*------ ADDED CSS ---------*/
.on {
  display: none;
}

.on {
  color: white;
  position: absolute;
  transform: translate(-50%, -50%);
  top: 50%;
  left: 35%;
  font-size: 10px;
}

.off {
  color: white;
  position: absolute;
  transform: translate(-50%, -50%);
  top: 50%;
  left: 65%;
  font-size: 10px;
}

input:checked + .slider .on {
  display: block;
}

input:checked + .slider .off {
  display: none;
}

/*--------- END --------*/
/* Rounded sliders */
.slider.round {
  /*border-radius: 34px;*/
}

.slider.round:before {
  /*border-radius: 50%;*/
}

.sidenavOptionText {
  font-size: 14px;
  color: #999;
  padding-top: 8px;
}

.setting-row {
  border-bottom: 1px solid #e1e8ed;
  margin-bottom: 7px;
  padding-bottom: 7px;
  width: 100%;
}

.setting-row-title {
  font-size: 14px;
  width: 100%;
  color: #3c3c3c;
}

.setting-row-text {
  color: #6b6b6b;
  font-size: 13px;
  margin-bottom: 5px;
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

  .setting-row-text {
    width: 90%;
  }
}
</style>
