<template>
  <div class="container-fluid px-0">
    <div class="row m-0 position-relative">
      <div class="col-12">
        <SuperAdminHeader @sidenavToggleAdmin="sidenavToggleAdmin"/>
        <SuperAdminSideNav :show="displaySideNav" @close="displaySideNav =false"/>
      </div>
      <div class="col-md-2 col-12">
        <SuperAdminSidebar/>
      </div>
      <div class="col-md-10 col-12 bg-white">
        <Nuxt/>
        <SuperAdminFooter/>
      </div>
    </div>
  </div>
</template>

<script>
import SuperAdminSidebar from "./SuperAdminSidebar";
import SuperAdminHeader from "./SuperAdminHeader";
import SuperAdminSideNav from "./SuperAdminSideNav";
import SuperAdminFooter from "./SuperAdminFooter";

export default {
  name: "SuperAdminPanel",
  components: {SuperAdminFooter, SuperAdminSideNav, SuperAdminHeader, SuperAdminSidebar},
  data() {
    return {
      displaySideNav: false
    }
  },
  methods: {
    sidenavToggleAdmin() {
      this.displaySideNav = !this.displaySideNav
    }
  },
  mounted() {
   
  }
}
</script>

<style scoped>

</style>
