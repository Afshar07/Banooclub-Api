<template>
  <nuxt-link :to="`/Migration/AdsFilter/Categories/${category_details.adsCategoryId}`">
    <div class="tw-rounded-md tw-overflow-hidden tw-relative tw-w-full tw-h-36" v-if="category_details">
      <div class="tw-absolute tw-w-full tw-h-3/4 tw--bottom-12 tw-bg-gradient-to-b tw-from-transparent tw-to-gray-800 tw-z-10"></div>
      <img v-if="category_details.fileData!==''&&category_details.fileData!==null" style="width: 192px;height: 144px;"  :src="`https://subapi.banoclub.com/media/gallery/adsCategory/${category_details.fileData}`" alt="" class="tw-absolute tw-object-contain">
      <img style="width: 192px;height: 144px;" v-else src="/nopicture.jpg" alt="" class="tw-absolute tw-object-contain">
      <h2 class="tw-absolute tw-bottom-0 tw-w-full tw-p-3 tw-text-white tw-z-20 tw-font-semibold tw-text-lg"> {{category_details.name}} </h2>
    </div>
  </nuxt-link>


</template>

<script>
export default {
  name: "CategoryItem",
  props:{
    category_details:{
      type:Object,
      required: true
    }
  }


}
</script>

<style scoped>

</style>
