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
      class=" tw-grid tw-grid-cols-12 gaping  position-relative col-12 mx-auto tw-overflow-x-hidden bg-white px-0"
      style="min-height: 100vh!important;"
      @click="closingHeader = true"
    >
      <transition name="CollapseSide">
        <div :class="decrease_width?'lg:tw-col-span-1':'lg:tw-col-span-2'"  class="  tw-transition-all tw-duration-1000  m-0 p-0">

        </div>
      </transition>

      <transition name="CollapseMain" mode="out-in">
        <div :class="decrease_width?'  tw-col-span-12 lg:tw-mr-20':'lg:tw-col-span-10 tw-col-span-12'"  class=" tw-duration-1000 tw-transition-all tw-min-h-screen   tw-bg-[#faf7ff]">
          <Nuxt
            @GetFullName="SetFullName($event)"
          />
        </div>
      </transition>


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

.CollapseMain-enter-active{
  animation: Collapse 2s ease-in-out;

}
.CollapseMain-leave-active{
  animation: Collapse reverse 2s ease-in-out;

}
.CollapseSide-enter-active{
  animation: CollapseSideAnimation 2s ease-in-out;

}
.CollapseSide-leave-active{
  animation: CollapseSideAnimation reverse 2s ease-in-out;

}

@keyframes CollapseSideAnimation {
  0%{
    @apply lg:tw-col-span-2
  }
  100%{
    @apply lg:tw-col-span-1
  }

}
@keyframes Collapse {
  0%{
    @apply lg:tw-col-span-10 tw-col-span-12
  }
  100%{
@apply tw-col-span-12 lg:tw-mr-20
}

}
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
