<template>
  <div class="container mcontainer">
    <div class="row">
      <div class="col-md-12 col-lg-8 add_post px-0 custom_padding_for_posts">
        <LazyProductDetail/>
      </div>
      <div class=" col-lg-4">
        <SideBar />
      </div>
    </div>

  </div>


</template>

<script>
import SideBar from "../../layouts/SideBar";
export default {
  name: "PDetails",
  layout: "PoshtebamPlusLayout",
  components:{

    SideBar,
  },
  head() {
    return {
      title: 'جزئیات خدمت',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'جزئیات خدمت',
        },
      ],
    }
  },
  methods:{
    SideNavPictureVideoToggle(data) {
      this.displaySideNav = false;
      this.displayRightSideNav = false;

      this.dataType = data;
      this.SideNavPictures = data;
    },
  }

}
</script>

<style scoped>
@media only screen and (min-width: 1024px) {
  .custom_padding_for_posts {
    padding-right: 5rem !important;
    padding-left: 5rem !important;
  }
}
@media (min-width: 1024px) {
  .mcontainer {
    max-width: 1000px;
    padding: 30px 0px;
    margin-left: auto;
    margin-right: auto;
  }
}
@media (max-width: 1024px) {
  .mcontainer {
    max-width: 1000px;
    padding: 25px 15px;
    margin-left: auto;
    margin-right: auto;
  }
}

</style>
