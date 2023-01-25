<template>
  <div class="mcontainer NewBg px-2 Rtl text-right">
    <div class="container-fluid bg-headers">
      <div class="container p-0">
        <div class="row    ">

          <nav aria-label="breadcrumb" style="direction: rtl">
            <ol class="breadcrumb   d-flex align-items-center gap-2  pb-3  p-0 m-0">

              <li class=" ">
                <nuxt-link v-if="!$auth.loggedIn" class="text-secondary " style="cursor: pointer;text-decoration: none" to="/">ثبت نام
                </nuxt-link>
                <nuxt-link v-else class="text-secondary " style="cursor: pointer;text-decoration: none" to="/social/AccountSetting/MyPage">پروفایل من
                </nuxt-link>
              </li>
              <li> /</li>
              <li class="  "><a class="text-dark " href="#" style="cursor: pointer;text-decoration: none"> درباره
                ما </a></li>

            </ol>

          </nav>

        </div>
      </div>
      <div class="container">
        <div class="row  py-5 d-flex justify-content-center  ">
          <div class="col-md-4 d-flex justify-content-center">
            <h2><strong>درباره ما</strong></h2>
          </div>
        </div>
      </div>
    </div>
    <div class="container">
      <div class="row border-top-colored ">

        <div class="col-md-12 d-flex flex-column justify-content-center ">
          <p style="text-align: justify" v-html="content">

          </p>
          <nuxt-link class="tw-cursor-pointer mb-2 " style="text-decoration: none!important" to="/">
            اگر هنوز به جمع ما نپیوستید از اینجا ثبت نام کنید.
          </nuxt-link>
          <small> بانوکلاب</small>
        </div>
      </div>

    </div>
    <LoginFooter></LoginFooter>
  </div>
</template>

<script>
export default {
  auth: false,
  name: "AboutUs",
  data() {
    return {
      SelectedCollapseId: 0,
      content: '<p class="ql-align-center ql-direction-rtl">بانوکلاب</p><p class="ql-direction-rtl ql-align-center">پلتفرم تخصصی کسب و کار</p><p class="ql-direction-rtl">امروزه با گستردگی و سهولت ارتباطات، بخش زیادی از فروش محصولات و ارائه خدمات بصورت آنلاین و از طریق اینترنت انجام میشه و کاربران و صاحبان کسب و کار، علاوه بر افزایش روزافزون تنوع محصول و خدمات، در بستر وسیعی از بازار کار و رقابت بسر میبرند. به همین منظور، کاربران به اهدافی نظیر جستجوی محصول و خدمات، آموزش و مشاوره، درآمدزایی بیشتر و تبلیغ کسب و کارشون در محیطی گسترده، رایگان و امن نیاز دارند.</p><p class="ql-direction-rtl">بانوکلاب با ایجاد بستری حمایت گر و پویا بمنظور رشد و حمایت از کسب و کارها، و همچنین تسهیل و ارتقاء سطح کیفی زندگی شهروندان عزیز از طریق ارائه خدمات مستقیم و بی واسطه به یکدیگر ایجاد شده است. بانوکلاب همواره بر این امر واقف بوده که بها بخشیدن و ارائه فرصتهای مناسب، کمک بسزایی در شکوفایی استعدادها، افزایش شادی و امیدواری و متعاقبا پیشرفت در سطح روحی و کیفی همگانی، و همچنین استقلال در درآمدزایی و اشتغال زایی جامعه خواهد داشت. بنابراین افتخار همراهی و همیاری کاربران عزیز توسط پلتفرم بانوکلاب با اهداف و چشم انداز زیر صورت خواهد گرفت:</p><p class="ql-direction-rtl">·&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;بستری یاری رسان به کسب و کارها از طریق ایجاد ارتباط مستقیم و بی واسطه به سایرین</p><p class="ql-direction-rtl">·&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;یاری رسانی و درآمدزایی کاربران از طریق مشاوره های آموزشی آنلاین به سایر کاربران </p><p class="ql-direction-rtl">·&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;حمایت از مشاغل خانگی و کوچک و فرصتی برای دیده شدن بصورت رایگان</p><p class="ql-direction-rtl">·&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;کمک به ارتقاء سطح دانش، فرهنگ و آگاه سازی کاربران از طریق تبادل اطلاعات و آموزش های ارزان و رایگان توسط سایر کاربران </p><p class="ql-direction-rtl">·&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;کاهش مخارج تبلیغات و بازاریابی کسب و کارها (مخصوصا کسب و کارهای کوچک و تازه تاسیس)</p><p class="ql-direction-rtl">·&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;افزایش امنیت و اعتماد در کسب و کار، خرید و فرش محصول و خدمات، و ارتباطات </p><p class="ql-direction-rtl">·&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;معرفی کاربران موفق و کار‌آفرین، کسب و کارهای نو با هدف ایجاد انگیزه و تشکیل کسب و کار جدید&nbsp;</p><h1 class="ql-align-center"><br></h1>'
    }
  },
  head() {
    return {
      title: 'درباره ما',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'درباره ما',
        },
      ],
    }
  },
  methods: {

    ChangeCollapse(id) {
      if (this.SelectedCollapseId === id) {
        this.SelectedCollapseId = 0
      } else {
        this.SelectedCollapseId = id
      }


    },
    GetCollapseClassName(id) {
      if (id === this.SelectedCollapseId) {
        return 'CollapseHeaderActive'
      } else {
        return ''
      }
    }
  }
}
</script>

<style>


</style>
