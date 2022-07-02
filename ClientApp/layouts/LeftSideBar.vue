<template>
  <div class="container-fluid px-0">
    <div class="col-10 w-100 m-0 p-0">
      <SideBarMenu />

      <EditInformation />
    </div>
  </div>
</template>

<script>
import Friends from "./Friends";
import ShortProfile from "./ShortProfile";
import YourPage from "./YourPage";
import Banner from "./Banner";
import Following from "./Following";
import LastActivity from "./LastActivity";
import RecentPhotos from "./RecentPhotos";
import SideBarMenu from "./SideBarMenu";
import EditInformation from "./EditInformation";
export default {
  name: "LeftSideBar",
  components: {
    SideBarMenu,
    EditInformation,
    RecentPhotos,
    LastActivity,
    ShortProfile,
    Following,
    Friends,
    Banner,
    YourPage,
  },
};
</script>

<style scoped></style>
