<template>
  <div class="container-fluid px-0">
    <div class="row px-0">
      <div class="col-md-6 col-lg-6 col-12 w-50">
        <RecentVideos @subSideNavPictureVideo="showSidePanel"/>
        <ShortProfile/>
        <LastActivity/>
      </div>
      <div class="col-md-6 col-lg-6 col-12 w-50">
        <Friends/>
        <EditInformation/>
      </div>
    </div>
  </div>
</template>

<script>
import ShortProfile from "../../layouts/ShortProfile";
import LastActivity from "../../layouts/LastActivity";
import Friends from "../../layouts/Friends";
import EditInformation from "../../layouts/EditInformation";
import RecentVideos from "../../components/RecentVideos";

export default {
  name: "Widgets",
  components: {RecentVideos, EditInformation, Friends, LastActivity, ShortProfile},
  layout: 'BanooClubLayout',
  methods: {
    showSidePanel(videoId) {
      this.$parent.$emit('SideNavPictureVideo', 'video', videoId)
    }
  }
}
</script>

<style scoped>

</style>
