<template>
  <div class="main-background">
    <!-- <header class="d-none d-md-block"> -->
    <Header
      @sidenavToggle="sideNavToggle"
      @rightsidenavToggle="rightSideNavToggle"
      :close-header="closingHeader"
      @click="clickingOnHeader = true"
      @open="closingHeader = false"
      @close="closingHeader = true"
      :fullName="SearchedUserData"
      :isChatLayout="true"
      :decrease_width="true"
    />
    <!-- </header> -->

    <TheSideNav :show="displaySideNav" @close="displaySideNav = false" />
    <TheRightSideNav
      :show="displayRightSideNav"
      @close="displayRightSideNav = false"
    />
    <SideNavPictureVideo
      :show="displaySideNavPictureVideo"
      :type-data="dataType"
      @close="displaySideNavPictureVideo = false"
    />
    <div class="row gaping position-relative col-12 mx-auto bg-white px-0 overflow-hidden h-100"
         @click="closingHeader = true">
<!--      <div class="col-md-2 col-12 order-2">-->
<!--        <LeftSideBar @SideNavPictureVideo="SideNavPictureVideoToggle" />-->
<!--      </div>-->
      <div class="col-md-12 col-12 order-1 order-md-1 row mx-auto mx-md-0 bg-white px-0" style="min-height: 87vh">
        <Nuxt
          @GetFullName="SetFullName($event)"
          @SideNavPictureVideo="SideNavPictureVideoToggle($event)"
        />
      </div>

      <div
        class="scrollTopButton"
        v-if="showScrollTopIcon"
        @click="scrollToTop"
      >
        <button class="btn p-0">
          <font-awesome-icon icon="chevron-up" color="#fff" />
        </button>
      </div>
    </div>
<!--    <Footer class="w-100" />-->
  </div>
</template>

<script>
import Header from "./Header";
import Footer from "./Footer";
import SideBar from "./SideBar";
import TheSideNav from "./TheSideNav";
import TheRightSideNav from "./TheRightSideMobile";
import LeftSideBar from "./LeftSideBar";
import SideNavPictureVideo from "../components/SideNavPictureVideo";

export default {
  name: "PoshtebamPlusLayout",

  components: {
    SideNavPictureVideo,
    TheRightSideNav,
    TheSideNav,
    SideBar,
    Footer,
    Header,
    LeftSideBar,
  },
  data() {
    return {
      displaySideNavPictureVideo: false,
      displaySideNav: false,
      displayRightSideNav: false,
      showScrollTopIcon: false,
      closingHeader: false,
      clickingOnHeader: false,
      dataType: "",
      SideNavPictures: [],
      SearchedUserData: {
        fullName: "",
        img: "",
      },
    };
  },
  mounted: function () {
    window.addEventListener("scroll", this.handleScroll);
  },
  destroyed: function () {
    window.removeEventListener("scroll", this.handleScroll);
  },
  methods: {
    SetFullName(data) {
      this.SearchedUserData.fullName = data.name.concat(" ", data.familyName);
      this.SearchedUserData.img = data.selfieFileData;
    },
    scrollToTop() {
      window.scrollTo({ top: 0, behavior: "smooth" });
    },
    handleScroll() {
      this.showScrollTopIcon = window.scrollY > 600;
    },
    sideNavToggle() {
      this.displayRightSideNav = false;
      this.displaySideNavPictureVideo = false;
      this.displaySideNav = !this.displaySideNav;
    },
    rightSideNavToggle() {
      this.displaySideNav = false;
      this.displaySideNavPictureVideo = false;
      this.displayRightSideNav = !this.displayRightSideNav;
    },
    SideNavPictureVideoToggle(data) {
      this.displaySideNav = false;
      this.displayRightSideNav = false;

      this.dataType = data;
      this.SideNavPictures = data;
    },
  },
};
</script>

<style scoped>
.scrollTopButton {
  position: fixed;
  bottom: 10px;
  right: 5px;
  cursor: pointer;
  border-radius: 5px;
  color: #fff;
  display: inline-block;
  font-size: 30px;
  line-height: 40px;
  text-align: center;
  transform: rotate(-15deg);
  transition: all 0.2s linear 0s;
  width: 40px;
  z-index: 99;
  background: #088dcd;
}

.scrollTopButton > button {
  transform: rotate(0);
}

.scrollTopButton:hover {
  transform: rotate(0);
}

/*.gaping {*/
/*  padding: 60px 0;*/
/*}*/

.englishFont {
  font-family: "Roboto", sans-serif !important;
}
</style>
