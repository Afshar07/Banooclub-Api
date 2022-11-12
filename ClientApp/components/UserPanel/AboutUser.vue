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
        دنبال شده توسط  <strong class="px-1">{{ userDetails.baseData.userName }}</strong> نفر
      </li>
    </ul>
    <div class="tw-gap-3 grid tw-grid-cols-2 tw-mt-4">
      <div class="row">
        <div
          class="col-md-6 my-3"
          v-for="(item,index) in Photos.slice(0,4)"
          :key="index"
        >
          <div class="position-relative">
            <img
              v-if="item.priority === 2"
              :src="`https://banooclubapi.oregonserver.com/${item.base64}`"
              style="object-fit: cover;object-position: center; width: 300px;height: 250px"
              class="rounded"
              alt=""
            />
            <video
              v-else-if="item.priority === 3"
              class="rounded w-100"
              style="object-fit: cover;object-position: center;height: 250px"
              controls
              :src="`https://banooclubapi.oregonserver.com/${item.base64}`"
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
  </div>

</template>

<script>

export default {
  name: "AboutUser",
  data(){
    return{
      Photos:[]
    }
  },
  props:{
    userDetails:{
      type:Array,
      required: true
    },
  },
  async fetch() {
    const response = await this.$repositories.getUserMedias.getUserMedias({
      userId:this.$route.params.id
    });
    this.Photos = response.data.medias;
  },

}
</script>

<style scoped>

</style>
