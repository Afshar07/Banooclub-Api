<template>
  <div class="bg-white row footerStyle col-12 p-0 m-0">
    <div class="col-12">
      <div class="col-12 text-end order-md-1 order-1">
        <div class="flexContainer justify-content-center gap-5">
          <div class="d-flex flex-column">
            <div class="text-end mb-3">
              <!--                <img src="~/static/logo.png">-->
            </div>
            <div class="text-md-end text-center footerMainText">
              شرکت نوآوران هوشمند سیماگر (سهامی خاص) دارای دو برند معتبر در
              زمینه طراحی وب سایت و سئو سایت بنام های SEORAZ و SIMAGAR میباشد که
              علاوه بر این دارای پنج استارت آپ که در اختیار مالکیت خویش میباشد
              بنام های "پشت بام" و "سیماجاب" و "سیماملک" و "بام گردی" و "بام
              خبر" و زیرساخت حرفه ای و دانش بنیان "فرادیدار" میباشد.
            </div>
            <div class="text-md-end text-center w-100 mt-4">
              <div
                class="d-flex justify-content-center justify-content-md-start flex-row gap-md-3"
              >
                <div>
                  <font-awesome-icon icon="map-marked-alt"></font-awesome-icon>
                </div>
                <div class="footerSubText text-center text-md-end">
                  دفتر مرکزی : تهران، خیابان شریعتی، بالاتر از میرداماد، کوچه
                  بهشت 1 ، پلاک 48، واحد 7
                </div>
              </div>
            </div>
            <div class="mt-4">
              <div
                class="d-flex flex-row justify-content-center justify-content-md-start gap-md-3"
              >
                <div class="">
                  <font-awesome-icon icon="mobile-alt"></font-awesome-icon>
                </div>
                <div class="footerSubText text-center text-md-end">
                  +1-56-346 345
                </div>
              </div>
            </div>
          </div>
          <div class="">
            <div class="d-flex flex-column align-items-center text-center text-md-end align-items-md-start">
              <div class="footerTitle">دنبال کنید</div>
              <div class="footerLink">
                <font-awesome-icon :icon="['fab', 'facebook-square']" />
                فیسبوک
              </div>
              <div class="footerLink">
                <font-awesome-icon :icon="['fab', 'twitter-square']" />
                توییتر
              </div>
              <div class="footerLink">
                <font-awesome-icon :icon="['fab', 'instagram-square']" />
                اینستاگرام
              </div>
              <div class="footerLink">
                <font-awesome-icon :icon="['fab', 'google-plus-square']" />
                گوگل +
              </div>
              <div class="footerLink">
                <font-awesome-icon :icon="['fab', 'pinterest-square']" />
                پینترست
              </div>
            </div>
          </div>
          <div class="">
            <div class="d-flex flex-column align-items-center align-items-start text-center text-md-end">
              <div class="footerTitle">هدایت</div>
              <div class="footerLink">درباره ما</div>
              <div class="footerLink">تماس با ما</div>
              <div class="footerLink">شرایط و ضوابط</div>
              <!--                <div class="footerLink">پیوند RSS</div>-->
              <!--                <div class="footerLink">نقشه سایت</div>-->
            </div>
          </div>
          <div class="">
            <div class="d-flex flex-column align-items-center align-items-md-start text-center text-md-end">
              <div class="footerTitle">لینک های مفید</div>
              <div class="footerLink">لیزینگ</div>
              <div class="footerLink">مسیر را ارسال کنید</div>
              <div class="footerLink">چگونه کار می کند ؟</div>
              <div class="footerLink">لیست نمایندگی ها</div>
              <div class="footerLink">نمایش همه</div>
            </div>
          </div>
          <div class="">
            <div class="d-flex flex-column align-items-center align-items-md-start mb-3">
              <div class="footerTitle">دانلود اپلیکیشن</div>
              <div class="downloadApp">
                <font-awesome-icon :icon="['fab', 'android']" />
                اندروید
              </div>
              <div class="downloadApp">
                <font-awesome-icon :icon="['fab', 'apple']" />
                آیفون
              </div>
              <div class="downloadApp">
                <font-awesome-icon :icon="['fab', 'windows']" />
                ویندوز
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row position-relative">
      <div class="col-12 bottomBar row mx-auto">
        <div class="d-flex justify-content-around">
          <div class="">حق کپی رایت برای بانو کلاب محفوظ است</div>
          <!--          <div class=""><img src="~/static/credit-cards.png" alt="" class="img-fluid"/></div>-->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Footer",
};
</script>

<style scoped>
.footerStyle {
  border-top: 3px solid #2fa7cd;
}

.footerMainText {
  font-size: 14px;
  line-height: 26px;
  color: #8c8b8b;
  max-width: 410px;
  width: fit-content;
}

.footerSubText {
  font-size: 14px;
  vertical-align: top;
  margin-bottom: 0;
  color: #545454;
}

.footerTitle {
  color: #424242;
  font-size: 14px;
  text-transform: uppercase;
  border-bottom: 2px solid #088dcd;
  background: rgba(0, 0, 0, 0) none repeat scroll 0 0;
  display: inline-block;
  margin-bottom: 30px;
  padding: 20px 20px 10px;
  width: max-content;
}

.downloadApp {
  border: 1px solid #5d6371;
  border-radius: 5px;
  font-size: 14px;
  margin-bottom: 15px;
  padding: 5px 0;
  text-align: center;
  text-transform: capitalize;
  width: fit-content;
  min-width: 200px;
  transition: all 0.2s linear 0s;
}

.downloadApp:hover {
  background: #088dcd;
  color: white;
  cursor: pointer;
}

.footerLink {
  font-size: 14px;
  margin-bottom: 12px;
  text-transform: capitalize;
  cursor: pointer;
  width: 100%;
  border-bottom: 1px solid white;
}

.footerLink:hover {
  border-bottom: 1px solid #545454;
}

.bottomBar {
  background: #f6f6f6;
  padding: 20px 0;
  width: 100%;
  position: absolute;
  bottom: 0;
  font-size: 13px;
  color: #545454;
  margin-top: 75px;
}

.flexContainer {
  padding: 75px 0 150px 0;
  display: flex;
  flex-direction: row;
}

.group {
  display: flex;
  flex-direction: column;
}

@media (max-width: 768px) {
  .flexContainer {
    flex-direction: column;
    max-width: 100%;
    padding-right: 12px;
    padding-left: 12px;
    align-items: center;
  }

  .group {
    flex-direction: row;
  }
}
</style>
