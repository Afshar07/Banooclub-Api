<template>
  <div class="container-fluid px-0">
    <div class="row">
      <div class="col-12">
        <h1>سوالات متداول</h1>
      </div>
      <div class="col-12">
        <div class="accordion">
          <span class="target-fix" id="accordion"></span>

          <div>
            <span class="target-fix" id="accordion1"></span>

            <a href="#accordion1" id="open-accordion1" title="open">
              چگونه وب سایت اجتماعی خود را ایجاد کنید؟
            </a>

            <a href="#accordion" id="close-accordion1" title="close">
              چگونه وب سایت اجتماعی خود را ایجاد کنید؟
            </a>

            <div class="accordion-content">
              <div class="col-12">
                شبکه‌های اجتماعی می‌توانند موجب دست یافتن به مقدار زیادی از مشتریان جدید شوند. اگر واقعا بخواهید با کمک
                شبکه‌های اجتماعی میزان مشتریان و بازگشت سرمایه را افزایش دهید برای انجام آن باید اصولی را رعایت کنید
              </div>
              <ol>
                <li> شبکه اجتماعی خود را به طور آگاهانه انتخاب کنید</li>
                <li>پروفایل خود را کامل کنید.</li>
                <li>با رهبران متفکر و تأثیرگذاران ارتباط برقرار کنید</li>
              </ol>
            </div>
          </div>

          <div>
            <span class="target-fix" id="accordion2"></span>
            <a href="#accordion2" id="open-accordion2" title="open">
              چگونه تنظیمات صفحه من را ویرایش کنم؟
            </a>
            <a href="#accordion" id="close-accordion2" title="close">
              چگونه تنظیمات صفحه من را ویرایش کنم؟
            </a>
            <div class="accordion-content">
              <div class="col-12">
                شبکه‌های اجتماعی می‌توانند موجب دست یافتن به مقدار زیادی از مشتریان جدید شوند. اگر واقعا بخواهید با کمک
                شبکه‌های اجتماعی میزان مشتریان و بازگشت سرمایه را افزایش دهید برای انجام آن باید اصولی را رعایت کنید.
              </div>
            </div>
          </div>

          <div>
            <span class="target-fix" id="accordion3"></span>
            <a href="#accordion3" id="open-accordion3" title="open">
              چگونه رمز عبور را عوض کنم
            </a>
            <a href="#accordion" id="close-accordion3" title="close">
              چگونه رمز عبور را عوض کنم
            </a>
            <div class="accordion-content">
              <div class="col-12">
                شبکه‌های اجتماعی می‌توانند موجب دست یافتن به مقدار زیادی از مشتریان جدید شوند. اگر واقعا بخواهید با کمک
                شبکه‌های اجتماعی میزان مشتریان و بازگشت سرمایه را افزایش دهید برای انجام آن باید اصولی را رعایت کنید.
              </div>
            </div>
          </div>
          <div>
            <span class="target-fix" id="accordion4"></span>
            <a href="#accordion4" id="open-accordion4" title="open">
              چگونه مردمی را که در نزدیکی من هستند را جستجو کنیم؟
            </a>
            <a href="#accordion" id="close-accordion4" title="close">
              چگونه مردمی را که در نزدیکی من هستند را جستجو کنیم؟
            </a>
            <div class="accordion-content">
              <div class="col-12">
                شبکه‌های اجتماعی می‌توانند موجب دست یافتن به مقدار زیادی از مشتریان جدید شوند. اگر واقعا بخواهید با کمک
                شبکه‌های اجتماعی میزان مشتریان و بازگشت سرمایه را افزایش دهید برای انجام آن باید اصولی را رعایت کنید.
              </div>
            </div>
          </div>
          <div>
            <span class="target-fix" id="accordion5"></span>
            <a href="#accordion5" id="open-accordion5" title="open">
              چگونه می توان صفحه مورد علاقه خود را ایجاد کرد؟
            </a>
            <a href="#accordion" id="close-accordion5" title="close">
              چگونه می توان صفحه مورد علاقه خود را ایجاد کرد؟
            </a>
            <div class="accordion-content">
              <div class="col-12">
                شبکه‌های اجتماعی می‌توانند موجب دست یافتن به مقدار زیادی از مشتریان جدید شوند. اگر واقعا بخواهید با کمک
                شبکه‌های اجتماعی میزان مشتریان و بازگشت سرمایه را افزایش دهید برای انجام آن باید اصولی را رعایت کنید.
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Faq",
  layout: 'BanooClubLayout'
}
</script>

<style scoped>
.accordion {
  position: relative;
  margin: 60px auto;
  width: 100%;

}

[id*="open-accordion"], [id*="close-accordion"] {
  background: #fff;
  border-bottom: 1px solid #ccc;
  line-height: 40px;
  height: 40px;
  display: block;
  margin: 0 auto;
  position: relative;
  color: black;
  width: 100%;
}

[id*="close-accordion"] {
  display: none;
}

.accordion a {
  font-weight: normal;
  text-decoration: none;
  text-shadow: none;
  width: 100%;
  color: #636363;
  font-size: 18px;
  text-align: right;
  border-radius: 0;
  white-space: normal;
  padding: 10px 20px;
  background: #fff;
  line-height: 1;
  margin-bottom: 6px;

}

[id*="open-accordion"]:after, [id*="close-accordion"]:after {
  content: "";
  border-left: 5px solid transparent;
  border-right: 5px solid transparent;
  border-top: 5px solid #999999;
  position: absolute;
  left: 15px;
  top: 15px;
  z-index: 999;
  transform: rotate(0deg);
  -webkit-transform: rotate(0deg);

}

.target-fix {
  display: block;
  top: 0;
  left: 0;
  position: fixed;
}

.accordion-content {
  background: #fff;
  height: 0;
  margin: -1px auto 0;
  padding: 0 2.5%;
  position: relative;
  overflow: hidden;
  width: 100%;
  transition: all 0.1s ease;
  -webkit-transition: all 0.1s ease;
  -moz-transition: all 0.1s ease;
}

.accordion span:target ~ .accordion-content {
  display: block;
  height: auto;
  padding-bottom: 25px;
  padding-top: 10px;
  width: 100%;
  line-height: 30px;
  color: #888888;
  margin-bottom: 8px;
}

.accordion span:target ~ [id*="close-accordion"] {
  display: block;
  background: #088dcd;
  color: #fff;
}

.accordion span:target ~ [id*="open-accordion"] {
  display: none;
}

.accordion span:target ~ [id*="close-accordion"]:after {
  border-top: 5px solid #fff;
  transform: rotate(0deg);
  -webkit-transform: rotate(-180deg);

}
</style>
