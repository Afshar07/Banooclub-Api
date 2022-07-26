<template>
  <div class="container mcontainer tw-overflow-x-hidden" v-if="!$fetchState.pending">
    <div class="row">
      <div class="col-md-12 col-lg-8 add_post px-0 custom_padding_for_posts">
        <AdsDetail :AdsDetail="AdsDetail"/>
      </div>
<!--      <div class=" col-lg-4">-->
<!--        <SideBar class="tw-mt-5" @SideNavPictureVideo="SideNavPictureVideoToggle($event)"/>-->
<!--      </div>-->
    </div>
  </div>

</template>

<script>

import AdsDetail from "../../../components/Ads/AdsDetail";
import BaseModal from "../../../components/utilities/BaseModal";

export default {
  name: "AdvertiseDetail",
  head() {
    return {

      title: 'جزئیات آگهی',
      meta: [
        {
          hid: "description",
          name: "description",
          content:'جزئیات آگهی',
        },
      ],
    };
  },
  layout: "PoshtebamPlusLayout",
  components:{
    AdsDetail,

    BaseModal,
  },

  computed:{

  },
  data(){
    return{
      AdsDetail: null,
      is_show_delete_modal:false,

    }
  },
  async fetch(){
    try {
      const res = await this.$repositories.getAnAd.getAnAd(
        {
          id:this.$route.params.id
        }
      )
      this.AdsDetail = res.data
    }
    catch (error){
      console.log(error)
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
