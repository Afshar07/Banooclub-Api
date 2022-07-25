<template>
  <div class=" mcontainer tw-overflow-x-hidden " v-if="!$fetchState.pending">

    <div class="row">
      <div class="col-md-12 col-lg-8 add_post px-0 custom_padding_for_posts">
        <PDetails @updateServiceDetails="updateServiceDetails" :service_details="ServiceDetails"/>
      </div>
      <div class=" col-lg-4">
        <SideBar class="tw-mt-5" @SideNavPictureVideo="SideNavPictureVideoToggle($event)"/>
      </div>
    </div>
  </div>

</template>

<script>
import SideBar from "../../layouts/SideBar";
import PDetails from "../../components/Products/PDetails";
import BaseModal from "../../components/utilities/BaseModal";

export default {
  name: "PDetail",
  layout: "PoshtebamPlusLayout",
  components:{
    PDetails,
    SideBar,
    BaseModal,
  },
  head(){
    return{
      title: 'بانو کلاب - ' + this.headTitle,
      meta: [
        {
          hid: 'description',
          name: 'description',
          content: this.serviceDescription,
        }
      ]
    }
  },
  computed:{
    headTitle(){
      if (this.ServiceDetails && this.ServiceDetails.title){
        return this.ServiceDetails.title
      }else {
        return  ''
      }
    },
    serviceDescription(){
      if (this.ServiceDetails && this.ServiceDetails.description){
        return this.ServiceDetails.description
      }else {
        return  ''
      }
    },
  },
  data(){
    return{
      ServiceDetails: null,
      is_show_delete_modal:false,

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

    }
    catch (error){
      console.log(error)
    }
  },
  methods:{
    updateServiceDetails(){
      this.$fetch()
    },
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
