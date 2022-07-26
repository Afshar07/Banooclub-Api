<template>
  <div class="widget custom_card p-3">
    <h4 class="tw-text-lg tw-font-semibold"> درباره من </h4>
    <ul class="tw-text-gray-600 space-y-3 tw-mt-3">
      <li class="tw-flex tw-items-center my-2" v-if="userinfo && (userinfo.cityName !== '' || userinfo.stateName !== '')">
        <HomeIcon style="width: 30px; height: 30px;" fill="rgb(75 85 99)" class="tw-rounded-full tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"/>
        ساکن
         <strong class="px-1">{{userinfo.stateName}} <span v-if="userinfo.cityName !== '' && userinfo.stateName !== ''">,</span> {{userinfo.cityName}}</strong>
      </li>
      <li class="tw-flex tw-items-center my-2" v-if="userinfo.relationState !== null">
        <HeartIcon fill="rgb(75 85 99)" style="width: 30px; height: 30px;" class="tw-rounded-full tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"/>
        <div v-if="userinfo.relationState === 1">
           <strong> مجرد  </strong>
        </div>
        <div v-else-if="userinfo.relationState === 2">
          در <strong> رابطه  </strong>
        </div>
        <div v-else-if="userinfo.relationState === 3">
           <strong> متاهل  </strong>
        </div>
        <div v-else-if="userinfo.relationState === 4">
           <strong> مطلقه  </strong>
        </div>
        <div v-else-if="userinfo.relationState === 5">
           <strong> سایر  </strong>
        </div>
      </li>
      <li class="tw-flex tw-items-center my-2">
        <AllUsersIcon fill="rgb(75 85 99)" style="width: 30px; height: 30px;" class="tw-rounded-full tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"/>
        دنبال شده توسط  <strong class="px-1">{{ $auth.user.baseData.followersCount }}</strong> نفر
      </li>
    </ul>
    <div class="tw-gap-3 grid tw-grid-cols-2 tw-mt-4">
      <div class="row">
        <div
          class="col-lg-6 my-3"
          v-for="(item,index) in MyPhotos.slice(0,4)"
          :key="index"
        >
          <div class="position-relative">
            <img
              v-if="item.priority === 2"
              :src="`https://banooclubapi.simagar.com/media/gallery/galleryimages/${item.base64}`"
              style="object-fit: cover;object-position: center; width: 300px;height: 250px"
              class="rounded"
              alt=""
            />
            <video
              v-else-if="item.priority === 3"
              class="rounded w-100"
              style="object-fit: cover;object-position: center;height: 250px"
              controls
              :src="`https://banooclubapi.simagar.com/media/gallery/galleryvideos/${item.base64}`"
            ></video>
            <!--            </a>-->
          </div>
        </div>
      </div>

      <!--      <div class="d-flex justify-content-around mt-3">-->
      <!--        <img style="width: 105px; height: 105px;" src="~/assets/images/products/product_image.jpg" alt="" class="tw-rounded-lg">-->
      <!--        <img style="width: 105px; height: 105px;" src="~/assets/images/products/product_image.jpg" alt="" class="tw-rounded-lg">-->
      <!--        <img style="width: 105px; height: 105px;" src="~/assets/images/products/product_image.jpg" alt="" class="tw-rounded-lg">-->
      <!--      </div>-->

    </div>
<!--    <button style="background-color: #f0f2f5;color: #666;"  class="tw-rounded-lg tw-mt-3 tw-w-full py-2"> ویرایش </button>-->
  </div>

</template>

<script>
import HomeIcon from "../../components/Icons/HomeIcon";
import GlobeIcon from "../../components/Icons/GlobeIcon";
import HeartIcon from "../../components/Icons/HeartIcon";
import AllUsersIcon from "../../components/Icons/AllUsersIcon";
// import MyPostsTabContents from "../MyTabsContent/MyPostsTabContents";
// import MyFriendTabContent from "../MyTabsContent/MyFriendTabContent";
// import MyPhotosTabContent from "../MyTabsContent/MyPhotosTabContent";
import CropIcon from "../Icons/WhiteCropIcon";
export default {
  name: "AboutMe",
  components: {
    AllUsersIcon,
    HeartIcon,
    GlobeIcon, HomeIcon, CropIcon
  },
  data(){
    return{
      MyPhotos:[],
      userinfo: {},

    }
  },
  mounted(){
    this.getUserInfo()
  },
  async fetch() {
    const response = await this.$repositories.getMyMediaGallery.getMyMediaGallery();
    this.MyPhotos = response.data;
  },
  methods:{
    async getUserInfo() {
      try {
        const response = await this.$repositories.getUserByToken.getUserByToken();
        this.userinfo = response.data.userInfo;

      } catch (error) {
        console.log(error);
      }
    },

  }


}
</script>

<style scoped>

</style>
