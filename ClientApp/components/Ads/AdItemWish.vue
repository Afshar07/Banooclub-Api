<template>
  <div class="card" v-if="AdsDetail" >
    <div class="tw-absolute tw-left-2 tw-top-2 tw-rounded-full tw-flex tw-items-center tw-justify-center customProfileImage" @click="goToServiceProfile(AdsDetail.userInfo.userId)">
      <nuxt-link    v-if="AdsDetail.userInfo.selfieFileData" :to="`/user/${AdsDetail.userInfo.userName}/posts`">
        <img
          class="headerPicture"
          :src="`https://banooclubapi.simagar.com/media/gallery/profile/${AdsDetail.userInfo.selfieFileData}`"
          alt=""

          style="border-radius: 50%; cursor: pointer;    width: 30px;  height: 30px;box-shadow: 0 0 9px rgb(72 69 72);"
        />
      </nuxt-link>
      <nuxt-link     v-else :to="`/user/${AdsDetail.userInfo.userName}/posts`">
        <img

          class="headerPicture"
          :src="userDefault"
          alt=""

          style="border-radius: 50%; cursor: pointer;  width: 30px;  height: 30px;"
        />
      </nuxt-link>
    </div>
    <div class="card-media tw-h-40">

      <nuxt-link :to="`/Migration/AdvertiseDetail/${AdsDetail.adsId}`">
        <img v-if="AdsDetail.adsInfo.photos && AdsDetail.adsInfo.photos.length>0" :src="`https://banooclubapi.simagar.com/media/gallery/adsphotos/${AdsDetail.adsInfo.photos[0].base64}`" :alt="AdsDetail.adsInfo.title">
        <img v-else :src="`/nopicture.jpg`" :alt="AdsDetail.title">
      </nuxt-link>

    </div>
    <div  class="card-body p-2 ">

      <div class="tw-flex tw-items-center  tw-left-2  tw-justify-center position-absolute tw-top-[-15px]">

        <div class="tw-top-3  tw-border-1 tw-bg-white ServicePrice tw-shadow tw-border-pink-700  tw-font-medium tw-px-2 tw-py-1 tw-left-2  tw-text tw-text-blue-500 ">
          {{Intl.NumberFormat('fa-IR').format(AdsDetail.price)}}
          <small class="Toman">تومان</small>

        </div>

      </div>


      <nuxt-link :to="`/Products/${AdsDetail.adsId}`" class="text-decoration-none">
        <h2 class="tw-text-lg tw-font-medium tw-mt-1 tw-t tw-truncate tw-text-slate-500 product_name tw-text-right mt-2">{{AdsDetail.title}}</h2>
      </nuxt-link>
      <div class="tw-text-xs tw-font-semibold tw-uppercase tw-text-yellow-500 text-end my-3">{{ AdsDetail.adsCategoryParents }}</div>





      <div v-if="$route.path === '/Products/MyServices/'" class="my-2">
        <small v-if="service_details.status===2" class="text-secondary ">خدمت شما در دست بررسی است</small>
        <small v-if="service_details.status===1" class="text-success ">خدمت شما فعال است</small>
        <small v-if="service_details.status===3" class="text-warning ">خدمت شما نیاز به بازبینی مجدد دارد</small>
      </div>

      <div v-if="show_buttons" class="w-100 d-flex flex-column">
        <button
          type="button"
          class="btn w-100 my-1 product_buttons"
          @click.prevent="$router.push({path: `/Products/Upgrade/${service_details.servicePackId}`, query: { active_tab: 'edit' }})"
        >
          ویرایش خدمت
        </button>
        <button
          type="button"
          class="btn w-100 my-1 product_buttons"
          @click.prevent="$router.push({path: `/Products/Upgrade/${service_details.servicePackId}`, query: { active_tab: 'upgrade' }})">
          ارتقاء خدمت
        </button>
      </div>

    </div>
  </div>
  <!--            router.push({ path: 'register', query: { plan: 'private' } })-->

</template>

<script>
import HeartIcon from "../Icons/HeartIcon";
import AdsDetail from "@/components/Ads/AdsDetail";
export default {
  name: "Product",
  components: {HeartIcon},
  props:{
    AdsDetail: {
      type: Object,
      required: true
    },
    show_buttons:{
      type: Boolean,
      required: true
    }
  },

  methods:{
    async createWishList(){
      try {
        await this.$repositories.createWishList.createWishList({
          objectId: this.AdsDetail.adsId,
          type: 3,
          userId: this.$auth.user.userInfo.userId,
          createDate: new Date(Date.now())
        })
        this.$toast.success("آگهی به علاقمندی ها افزوده شد.");
        this.$nuxt.refresh();


      }
      catch (error){
        console.log(error)
      }

    },
  }
}
</script>

<style scoped>
.product_buttons{
  background-color: #fce6ff;
  color: #ff8100;
}
@media (min-width: 376px) and (max-width: 425px) {
  .card{
    width: 190px !important;
  }
}
@media (min-width: 320px) {
  .card{
    width: 140px !important;
  }
}
@media (min-width: 375px) {
  .card{
    width: 168px !important;
  }
}
@media (min-width: 426px) {
  .card{
    width: 240px !important;
  }
}
@media (min-width: 1024px) {
  .in_my_services{
    width: 190px !important;

  }
  .card{
    width: 190px !important;
  }
}
.card {
  width: 185px;
  background-color: white;
  border-radius: 8px;
  overflow: hidden;
  -webkit-box-shadow: 0 1px 3px 0 rgb(0 0 0 / 10%), 0 1px 2px 0 rgb(0 0 0 / 6%);
  box-shadow: 0 1px 3px 0 rgb(0 0 0 / 10%), 0 1px 2px 0 rgb(0 0 0 / 6%);
}
.product_name:hover{
  color: #007bff !important;
}
</style>
