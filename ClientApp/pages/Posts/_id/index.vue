<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class=" mcontainer tw-overflow-x-hidden" >
    <div class="row">
      <div class="col-md-12 col-lg-8 add_post px-0 custom_padding_for_posts">
        <LazyPostItem :post_details="PostDetails" :inMainPage="false" :inMyPosts="false"/>
      </div>
      <div class="col-md-12 col-lg-4 d-none d-lg-block">
        <SideBar class="tw-mt-5" @SideNavPictureVideo="SideNavPictureVideoToggle($event)"/>
      </div>
    </div>
  </div>

</template>



<script>
import SideBar from "../../../layouts/SideBar";
export default {
  name: "index",
  layout: "PoshtebamPlusLayout",
  components:{
    SideBar,

  },
  head(){
    return{
      title: 'بانو کلاب - پست',
      meta: [
        {
          hid: 'description',
          name: 'description',
          content: this.postDescription,
        }
      ]
    }
  },
  computed:{
    postDescription(){
      if (this.PostDetails && this.PostDetails.description){
        return this.PostDetails.description
      }else {
        return  ''
      }
    },
  },
  data(){
    return{
      PostDetails: null,
      is_show_delete_modal:false,

    }
  },
  async fetch(){
    try {
      const service_details = await this.$repositories.getPost.getPost(
        {
          postId:this.$route.params.id
        }
      )
      this.PostDetails = service_details.data

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
