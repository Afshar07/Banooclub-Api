<template>
  <div class="widget custom_card p-3">
    <h4 class="tw-text-lg tw-font-semibold"> درباره من </h4>
    <ul class="tw-text-gray-600 space-y-3 tw-mt-3">
      <li class="tw-flex tw-items-center my-2">
        <HomeIcon style="width: 30px; height: 30px;" fill="rgb(75 85 99)" class="tw-rounded-full tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"/>
        ساکن <strong> ایران، تهران  </strong>
      </li>
      <li class="tw-flex tw-items-center my-2">
        <GlobeIcon style="width: 30px; height: 30px;" fill="rgb(75 85 99)"  class="tw-rounded-full tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"/>
        از <strong> ایران , تهران  </strong>
      </li>
      <li class="tw-flex tw-items-center my-2">
        <HeartIcon fill="rgb(75 85 99)" style="width: 30px; height: 30px;" class="tw-rounded-full tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"/>
        در <strong> رابطه  </strong>
      </li>
      <li class="tw-flex tw-items-center my-2">
        <AllUsersIcon fill="rgb(75 85 99)" style="width: 30px; height: 30px;" class="tw-rounded-full tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"/>
        فالو شده توسط  <strong class="px-1">{{ $auth.user.baseData.followingsCount }}</strong> نفر
      </li>
    </ul>
    <div class="tw-gap-3 grid tw-grid-cols-3 tw-mt-4">
      <img style="object-fit: cover;object-position: center; width: 335px;height: 332px;" src="~/assets/images/products/product_image.jpg" alt="" class="tw-rounded-lg tw-col-span-full">
      <div class="d-flex justify-content-around mt-3">
        <img style="width: 105px; height: 105px;" src="~/assets/images/products/product_image.jpg" alt="" class="tw-rounded-lg">
        <img style="width: 105px; height: 105px;" src="~/assets/images/products/product_image.jpg" alt="" class="tw-rounded-lg">
        <img style="width: 105px; height: 105px;" src="~/assets/images/products/product_image.jpg" alt="" class="tw-rounded-lg">
      </div>

    </div>
    <button style="background-color: #f0f2f5;color: #666;"  class="tw-rounded-lg tw-mt-3 tw-w-full py-2"> ویرایش </button>
  </div>

</template>

<script>
import HomeIcon from "../../components/Icons/HomeIcon";
import GlobeIcon from "../../components/Icons/GlobeIcon";
import HeartIcon from "../../components/Icons/HeartIcon";
import AllUsersIcon from "../../components/Icons/AllUsersIcon";
import MyPostsTabContents from "../MyTabsContent/MyPostsTabContents";
import MyFriendTabContent from "../MyTabsContent/MyFriendTabContent";
import MyPhotosTabContent from "../MyTabsContent/MyPhotosTabContent";
import CropIcon from "../Icons/WhiteCropIcon";
export default {
  name: "AboutMe",
  components: {
    MyPostsTabContents,
    AllUsersIcon,
    HeartIcon,
    GlobeIcon, HomeIcon, MyFriendTabContent, MyPhotosTabContent, CropIcon},

}
</script>

<style scoped>

</style>
