<template>
  <div class="main-background">
    <Header
      @sidenavToggle="sideNavToggle"
      @rightsidenavToggle="rightSideNavToggle"
      :close-header="closingHeader"
      @click="clickingOnHeader = true"
      @open="closingHeader = false"
      @close="closingHeader = true"
    />
    <TheSideNav :show="displaySideNav" @close="displaySideNav = false" />
    <TheRightSideNav
      :show="displayRightSideNav"
      @close="displayRightSideNav = false"
    />
    <div class="row col-12 mx-auto" @click="closingHeader = true">
      <div class="col-12 bg-white py-4 col-12 order-1 order-md-1">
        <Nuxt />
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
    <Footer class="w-100" />
  </div>
</template>

<script>
import Header from "./Header";
import TheSideNav from "./TheSideNav";
import TheRightSideNav from "./TheRightSideMobile";
import Footer from "./Footer";

export default {
  name: "MigrationLayout",
  components: {
    Header,
    TheSideNav,
    TheRightSideNav,
    Footer,
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
    };
  },
  mounted: function () {
    window.addEventListener("scroll", this.handleScroll);
  },
  destroyed: function () {
    window.removeEventListener("scroll", this.handleScroll);
  },
  methods: {
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

.gaping {
  padding: 60px 0;
}
</style>
