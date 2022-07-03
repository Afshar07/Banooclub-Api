<template>
  <div class="container mcontainer tw-overflow-x-hidden" v-if="!$fetchState.pending" >
    <div class="row">
      <div class="col-md-12 col-lg-8 add_post px-0 custom_padding_for_posts">
        <ProductDetail :service_details="ServiceDetails"/>
      </div>
      <div class=" col-lg-4">
        <SideBar class="tw-mt-5" @SideNavPictureVideo="SideNavPictureVideoToggle($event)"/>
      </div>
    </div>

  </div>

</template>

<script>
import SideBar from "../../layouts/SideBar";
import ProductDetail from "../../components/Products/PDetails";
export default {
  name: "_id",
  layout: "BanooClubLayout",
  components:{
    ProductDetail,
    SideBar,
  },
  data(){
    return{
      ServiceDetails: null
    }
  },
  async fetch(){
    try {
      const service_details = await this.$repositories.getAService.getAService(
        {
          servicePackId:this.$route.params.id
        }
      )
      this.ServiceDetails = service_details.data
      console.log('this.ServiceDetails',this.ServiceDetails)

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
