<template>
  <div class="tw-p-3 tw-flex tw-items-center  tw-bg-purple-400 tw-justify-start tw-fixed tw-bottom-0 tw-w-full tw-right-0">
    <div class="tw-flex tw-items-center tw-justify-start tw-gap-4 tw-flex-wrap">
      <nuxt-link style="text-decoration: none!important" to="/AboutUs">
        <span class="text-white tw-cursor-pointer  tw-cursor-pointer hover:tw-text-purple-400">درباره</span>
      </nuxt-link>
      <nuxt-link style="text-decoration: none!important" to="/ContactUs">
        <span class="text-white tw-cursor-pointer hover:tw-text-purple-400">تماس با ما</span>
      </nuxt-link>
      <span class="text-white tw-cursor-pointer hover:tw-text-purple-400">مقالات</span>
      <span class="text-white tw-cursor-pointer hover:tw-text-purple-400">راهنما</span>
      <nuxt-link style="text-decoration: none!important" to="/terms">
        <span class="text-white tw-cursor-pointer hover:tw-text-purple-400">قوانین و مقررات</span>
      </nuxt-link>
      <span class="text-white tw-cursor-pointer hover:tw-text-purple-400">تالار گفتگو</span>
      <nuxt-link v-if="$route.path==='/terms'||$route.path==='/ContactUs'||$route.path==='/AboutUs'" style="text-decoration: none!important" to="/">
        <span class="text-white tw-cursor-pointer hover:tw-text-purple-400">ثبت نام</span>

      </nuxt-link>
    </div>
  </div>
</template>

<script>
export default {
  name: "LoginFooter"
}
</script>

<style scoped>

</style>
