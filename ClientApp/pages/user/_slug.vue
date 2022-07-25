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
  head() {
    return {
      title: 'صفحه کاربر',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'صفحه کاربر',
        },
      ],
    }
  },
  layout: "PoshtebamPlusLayout",
  fetchOnServer() {
    return false;
  },
  async fetch() {
    if (this.$route.params.slug) {
      await this.setUserDetails(null);
      const userData = await this.$repositories.GetUserByUserName.GetUserByUserName({
          userName:this.$route.params.slug
      }
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
