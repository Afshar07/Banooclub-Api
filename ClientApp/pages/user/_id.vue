<template>
  <div class="container-fluid">
    <nuxt-child></nuxt-child>
  </div>
</template>

<script>
import {mapActions} from "vuex";
// This parent is created only for making request before rendering child page
// To cleanup header shitshow
export default {
  layout: "PoshtebamPlusLayout",
  fetchOnServer() {
    return false;
  },
  async fetch() {
    if (this.$route.params.id) {
      await this.setUserDetails(null);
      const userData = await this.$repositories.getUserIndex.getUserIndex(
        this.$route.params.id
      );
      this.$store.commit("SetUserData", userData.data);
      this.setUserDetails(userData.data);
    }
  },
  methods: {
    ...mapActions("userDetails", ["setUserDetails"]),
  },
};
</script>
