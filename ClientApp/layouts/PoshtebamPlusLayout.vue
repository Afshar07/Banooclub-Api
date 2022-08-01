<template>
  <div class="m-0 p-0 main-background app">
    <Header
      @toggle_width="toggleWidth"
      @sidenavToggle="sideNavToggle"
      @rightsidenavToggle="rightSideNavToggle"
      :close-header="closingHeader"
      @click="clickingOnHeader = true"
      @open="closingHeader = false"
      @close="closingHeader = true"
      :fullName="SearchedUserData"
      :isChatLayout="false"
      :decrease_width="decrease_width"
    />
    <TheSideNav :show="displaySideNav" @close="displaySideNav = false" />


    <div
      class="row gaping position-relative col-12 mx-auto tw-overflow-x-hidden bg-white px-0"
      @click="closingHeader = true"
    >

      <div  class=" col-xl-2  m-0 p-0">

      </div>
      <div  class="col-xl-10 m-0 p-0">
        <Nuxt
          @GetFullName="SetFullName($event)"
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
  </div>
</template>

<script>
import Header from "./Header";
import Footer from "./Footer";
import TheSideNav from "./TheSideNav";
export default {
  name: "PoshtebamPlusLayout",

  components: {

    TheSideNav,
    Footer,
    Header,
  },
  data() {
    return {

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
      change_cols_width: false,
      decrease_width: false,
    };
  },
  created() {
    this.$root.$on("change-cols",()=>{
      this.change_cols_width = !this.change_cols_width

    })
  },

  mounted: function () {
    window.addEventListener("click", this.documentClick);
    window.addEventListener("scroll", this.handleScroll);
  },
  destroyed: function () {
    window.removeEventListener("scroll", this.handleScroll);
    window.removeEventListener("click", this.documentClick);
  },
  methods: {
    toggleWidth() {
      this.decrease_width = !this.decrease_width
    },
    documentClick(e) {},
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

      this.displaySideNav = !this.displaySideNav;
    },
    rightSideNavToggle() {
      this.displaySideNav = false;

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
@media only screen and (max-width: 768px) {
  .custom-padding {
    padding: 0;
  }
  .main-background{
    margin-bottom: 66px!important;
  }
}
.main-background{
  background-color: #f9fafb !important;
  font-family: iranSans !important;
}
/*@media only screen and (min-width: 1440px) {*/
/*  .custom-padding {*/
/*    padding: 0 300px;*/
/*  }*/
/*}*/
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

/*@media screen and (max-width: 1024px) {*/
/*  .gaping {*/
/*    padding: 60px 0;*/
/*  }*/
/*}*/


.englishFont {
  font-family: "Roboto", sans-serif !important;
}

/*@media screen and (max-width: 600px) {*/
/*  .gaping {*/
/*    margin-top: 2rem;*/
/*  }*/
/*}*/
</style>
