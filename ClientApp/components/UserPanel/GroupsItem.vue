<template>
  <div class="tw-flex tw-items-center tw-rounded-md p-2 hover:tw-bg-gray-50">
    <nuxt-link to="#" class="text-decoration-none ">
      <img style="width: 49px; height: 49px;" src="~/assets/images/products/product_image.jpg" class=" tw-w-full tw-h-full tw-rounded-full " alt="">
    </nuxt-link>
    <div class="tw-flex-1 px-3">
      <nuxt-link to="#" class="tw-text-base tw-font-semibold tw-capitalize text-decoration-none"> نام گروه  </nuxt-link>
      <div class="tw-text-sm tw-text-gray-500 tw-mt-0.5"> 15 هزار دنبال کننده</div>
    </div>
    <nuxt-link to="#" class="text-decoration-none d-flex justify-content-center align-items-center tw-h-8 tw-px-3 tw-rounded-md tw-text-sm tw-border tw-font-semibold tw-bg-blue-500 tw-text-white">
      پیوستن
    </nuxt-link>
  </div>

</template>

<script>
export default {
  name: "GroupsItem"
}
</script>

<style scoped>

</style>
