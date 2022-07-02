<template>
  <div class="row firstSectionBox mx-auto d-lg-block d-none rounded main-shadow">
    <div class="col-12 text-end">
      <span class="titleSidebarSection">میانبرها</span>
    </div>
    <div class="col-12">
      <ul class="naves text-end pe-md-1">
        <!--        <li>-->
        <!--          <nuxt-link dir="rtl" to="/" href="newsfeed.html" title="">-->
        <!--            <font-awesome-icon :icon="['fas', 'clipboard']" color="#999"/>-->
        <!--            <span class="px-2">اخبار</span>-->
        <!--          </nuxt-link>-->
        <!--        </li>-->
        <!--        <li>-->

        <!--          <nuxt-link to="/" href="inbox.html" title="">-->
        <!--            <font-awesome-icon :icon="['fas', 'mouse']" color="#999"/>-->
        <!--            <span class="px-2">صندوق ورودی</span>-->
        <!--          </nuxt-link>-->
        <!--        </li>-->
        <li>
          <nuxt-link to="/social" exact style="cursor: pointer" title="">
            <HomeIcon style="width: 26px; height: 26px;"/>
<!--            <font-awesome-icon :icon="['fas', 'sticky-note']" color="#999" />-->
            <span class="px-2">صفحه من</span>
          </nuxt-link>
        </li>
        <li>
          <nuxt-link to="/social/timelinefriends" title="">
            <font-awesome-icon :icon="['fas', 'user']" color="#999" />
            <span class="px-2">دوستان</span>
          </nuxt-link>
        </li>
        <li>
          <nuxt-link to="/social/chat" title="">
            <font-awesome-icon :icon="['fas', 'comment']" color="#999" />
            <span class="px-2">چت</span>
          </nuxt-link>
        </li>
        <li>
          <nuxt-link to="/social/timelinemyphotos" title="">
            <font-awesome-icon :icon="['fas', 'image']" color="#999" />
            <span class="px-2">تصاویر</span>
          </nuxt-link>
        </li>
        <li>
          <nuxt-link to="/social/timelinemyvideos" title="">
            <font-awesome-icon :icon="['fas', 'camera']" color="#999" />
            <span class="px-2">فیلم</span>
          </nuxt-link>
        </li>
        <li>
          <nuxt-link to="/social/contacts" title="">
            <font-awesome-icon :icon="['fas', 'user']" color="#999" />
            <span class="px-2">همه کاربران</span>
          </nuxt-link>
        </li>
        <li>
          <nuxt-link
            to="/migration/allroomate"
            style="cursor: pointer"
            title=""
          >
            <font-awesome-icon :icon="['fas', 'bed']" color="#999" />
            <span class="px-2">همه آگهی های هم خانه</span>
          </nuxt-link>
        </li>
        <li></li>
        <li>
          <nuxt-link to="/migration/createadvertise" title="">
            <font-awesome-icon :icon="['fas', 'ad']" color="#999" />
            <span class="px-2">افزودن آگهی</span>
          </nuxt-link>
        </li>
        <li>
          <nuxt-link to="/migration/myadvertise" title="">
            <font-awesome-icon :icon="['fas', 'ad']" color="#999" />
            <span class="px-2">آگهی های من </span>
          </nuxt-link>
        </li>
        <li>
          <nuxt-link to="/migration/setting/roommatecondition" title="">
            <font-awesome-icon :icon="['fas', 'user']" color="#999" />
            <span class="px-2">افزودن هم خانه</span>
          </nuxt-link>
        </li>
        <li>
          <nuxt-link to="/migration/myroomateinfo" title="">
            <font-awesome-icon :icon="['fas', 'user']" color="#999" />
            <span class="px-2">اطلاعات هم خانه</span>
          </nuxt-link>
        </li>
        <!--        <li>-->
        <!--          <nuxt-link to="/" href="messages.html" title="">-->
        <!--            <font-awesome-icon :icon="['fas', 'comments']" color="#999"/>-->
        <!--            <span class="px-2">پیام</span>-->
        <!--          </nuxt-link>-->
        <!--        </li>-->
        <!--        <li>-->
        <!--          <nuxt-link to="/" href="notifications.html" title="">-->
        <!--            <font-awesome-icon :icon="['fas', 'bell']" color="#999"/>-->
        <!--            <span class="px-2">اعلان ها</span>-->
        <!--          </nuxt-link>-->
        <!--        </li>-->
        <!--        <li>-->
        <!--          <nuxt-link to="/" href="people-nearby.html" title="">-->
        <!--            <font-awesome-icon :icon="['fas', 'share']" color="#999"/>-->
        <!--            <span class="px-2">افراد نزدیک به من</span>-->
        <!--          </nuxt-link>-->
        <!--        </li>-->
        <!--        <li>-->
        <!--          <nuxt-link to="/Migration" title="">-->
        <!--            <font-awesome-icon :icon="['fas', 'users']" color="#999"/>-->
        <!--            <span class="px-2">همه ی افراد</span>-->
        <!--          </nuxt-link>-->
        <!--        </li>-->
        <!--        <li>-->
        <!--          <nuxt-link to="/" href="insights.html" title="">-->
        <!--            <font-awesome-icon :icon="['fas', 'chart-bar']" color="#999"/>-->
        <!--            <span class="px-2">فهم</span>-->
        <!--          </nuxt-link>-->
        <!--        </li>-->
        <li>
          <div style="cursor: pointer" @click="logoutUser">
            <font-awesome-icon :icon="['fas', 'sign-out-alt']" color="#999" />
            <span class="px-2">خروج</span>
          </div>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import HomeIcon from "../components/Icons/HomeIcon";
export default {
  name: "SideBarMenu",
  components: {HomeIcon},
  methods: {
    goToMyPage() {
      this.$router.push({
        path: "/Social",
      });
    },
    async logoutUser() {
      await this.$auth.logout();
    },
  },
};
</script>

<style scoped>
.nuxt-link-active > * {
  color: #088dcd !important;
}
.naves {
  text-decoration: none;
  list-style: none;
}

.naves li:first-child {
  margin-bottom: 20px;
  margin-top: 20px;
}

.naves li {
  margin-bottom: 20px;
}

.naves li a {
  text-decoration: none;
  color: black;
  margin-right: 10px;
  font-size: 15px;
}

.naves li :hover {
  color: #088dcd;
}

.naves li :hover > .svg-inline--fa {
  color: #088dcd !important;
}

.titleSidebarSection {
  border-bottom: 2px solid #088dcd;
  background: rgba(0, 0, 0, 0) none repeat scroll 0 0;
  color: #424242;
  display: inline-block;
  font-size: 15px;
  margin-bottom: 30px;
  padding: 20px 20px 10px;
  text-transform: uppercase;
}

.firstSectionBox {
  background: #fdfdfd;
  border: 1px solid #ede9e9;
  border-radius: 3px;
  width: 90%;
  margin-bottom: 30px;
}

@media (max-width: 576px) {
  .firstSectionBox {
    width: 100%;
    margin-top: 7rem;
  }
}
</style>
