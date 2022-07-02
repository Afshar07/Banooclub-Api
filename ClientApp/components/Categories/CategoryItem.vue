<template>
  <div class="tw-rounded-md tw-overflow-hidden tw-relative tw-w-full tw-h-36" v-if="category_details">
    <div class="tw-absolute tw-w-full tw-h-3/4 tw--bottom-12 tw-bg-gradient-to-b tw-from-transparent tw-to-gray-800 tw-z-10"></div>
    <img src="../../assets/images/products/product_image.jpg" alt="" class="tw-absolute tw-w-full tw-h-full tw-object-cover">
    <h2 class="tw-absolute tw-bottom-0 tw-w-full tw-p-3 tw-text-white tw-z-20 tw-font-semibold tw-text-lg"> {{category_details.title}} </h2>
  </div>

</template>

<script>
export default {
  name: "CategoryItem",
  props:{
    category_details:{
      type:Array,
      required: true
    }
  }
}
</script>

<style scoped>

</style>
