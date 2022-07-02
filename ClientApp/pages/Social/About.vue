<template>
  <div class="container-fluid px-0">
    <div class="row boxMainContent">
      <div class="col-12">
        <h5 class="morePage text-black d-inline-block">
          <font-awesome-icon icon="info-circle" color="#088dcd"/>
          اطلاعات شخصی
        </h5>
      </div>
      <div class="col-12 morePageText">
        من محمد جواد صالحی حدود 8 سال سابقه برنامه نویسی دارم و تخصص اصلی back-end هست و بیشتر در حوضه Asp.NET Core کار
        میکنم و هیچ کاری به اندازی یادگیری و یاد دادن خوشحالم نمی کند.
      </div>
      <div class="row border px-0">
        <div class="col-md-2 col-lg-2 col-12 px-0">
          <div class="d-flex flex-column morePageSection">
            <div :class="activeBox==='mainInformation'?'activeSection':''" @click="switchContent('mainInformation')">
              اطلاعات اصلی
            </div>
            <div :class="activeBox==='location'?'activeSection':''" @click="switchContent('location')">موقعیت</div>
            <div :class="activeBox==='workEducation'?'activeSection':''" @click="switchContent('workEducation')">آموزش و
              کار
            </div>
            <div :class="activeBox==='Interests'?'activeSection':''" @click="switchContent('Interests')">منافع</div>
            <div :class="activeBox==='language'?'activeSection':''" @click="switchContent('language')">زبان</div>
          </div>
        </div>
        <div class="col-md-10 col-lg-10 col-12 px-0">
          <div class="col-12 p-0 m-0 mainInformation" v-if="activeBox==='mainInformation'">
            <ul class="basics">
              <li>
                <div class="d-flex flex-row ">
                  <div class="px-2 morePageColor"><i class="far fa-user"></i></div>
                  <div class="">حدیثه زمانی</div>
                </div>
              </li>
              <li>
                <div class="d-flex flex-row ">
                  <div class="px-2 morePageColor"><i class="fas fa-map-marked-alt"></i></div>
                  <div class="">محل زندگی در دبی</div>
                </div>
              </li>
              <li>
                <div class="d-flex flex-row ">
                  <div class="px-2 morePageColor"><i class="fas fa-mobile-alt"></i></div>
                  <div class="">تلفن:031 4434 34 55</div>
                </div>
              </li>
              <li>
                <div class="d-flex flex-row ">
                  <div class="px-2 morePageColor"><i class="far fa-envelope"></i></div>
                  <div class="">[ایمیل محافظت شده است]</div>
                </div>
              </li>
              <li>
                <div class="d-flex flex-row ">
                  <div class="px-2 morePageColor"><i class="fas fa-globe"></i></div>
                  <div class="">سایت:www.Yoursite.Com</div>
                </div>
              </li>
            </ul>
          </div>
          <div class="col-12 p-0 m-0 mainInformation" v-if="activeBox==='location'"></div>
          <div class="col-12 p-0 m-0 mainInformation" v-if="activeBox==='workEducation'">
            <div class="d-flex flex-column work">
              <div class="mainInformationLink">
                <nuxt-link to="Social/About">ایوانتو</nuxt-link>
              </div>
              <div class="morePageText">کار به عنوان نویسنده در envato themeforest از سال 2013</div>
              <div class="basics">
                <div class="d-flex flex-row ">
                  <div class="morePageColor"><i class="fab fa-facebook-f"></i></div>
                  <div class="educationText">مدرک BSCS از دانشگاه آکسفورد</div>
                </div>
              </div>
              <div class="basics">
                <div class="d-flex flex-row">
                  <div class="morePageColor"><i class="fab fa-twitter"></i></div>
                  <div class="educationText"> مدرک MSCSاز دانشگاه هاروارد</div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-12 p-0 m-0 mainInformation" v-if="activeBox==='Interests'">
            <ul class="basics">
              <li>فوتبال</li>
              <li> اینترنت </li>
              <li>عکاسی</li>
            </ul>
          </div>
          <div class="col-12 p-0 m-0 mainInformation" v-if="activeBox==='language'">
            <ul class="basics">
              <li>انگلیسی</li>
              <li>فرانسوی</li>
              <li>اسپانیایی</li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "About",
  layout: 'BanooClubLayout',
  data() {
    return {
      activeBox: 'mainInformation',
    }
  },
  methods: {
    switchContent(tabName) {
      this.activeBox = tabName;
    }
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

.morePage {
  font-size: 18px;
  text-transform: capitalize;
  margin-bottom: 20px;
  padding-bottom: 20px;
  border-bottom: 3px solid #088dcd;
}

.morePageText {
  color: #7c7c7c;
  font-size: 14px;
  line-height: 26px;
  margin: 0 0 16px;
}

.morePageSection div {
  background: #fff;
  font-size: 16px;
  color: #545454;
  transition: border-color .125s ease-in;
  white-space: nowrap;
  padding: 8px 16px;
  cursor: pointer;
  text-align: center;
}

.morePageSection div.activeSection {
  background: #088dcd;
  font-size: 16px;
  color: #fff;
  transition: border-color .125s ease-in;
  white-space: nowrap;
  padding: 8px 16px;
}

.mainInformation {
  background: #f7f7f7 none repeat scroll 0 0;
  min-height: 100%;
  min-width: 100%;
}

.basics {
  padding-right: 30px;
  padding-top: 20px;
}

.basics > li {
  color: #999;
  font-size: 14px;
  margin-bottom: 20px;
  text-transform: capitalize;
}

.mainInformationLink a {
  color: #088dcd;
  margin-bottom: 5px;
  font-size: 14px;
  text-decoration: none;
}

.morePageColor {
  color: #088dcd;
}

.work {
  padding: 20px;
}
.educationText {
  color: #999;
  font-size: 14px;
  margin-bottom: 20px;
  text-transform: capitalize;
  margin-right: 30px;
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
