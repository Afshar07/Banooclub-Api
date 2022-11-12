<template>
  <div class="card" v-if="AdsDetail" >
    <div class="card-media tw-h-40">
      <nuxt-link :to="`/Migration/AdvertiseDetail/${AdsDetail.adsId}`">
        <img v-if="AdsDetail.adsInfo.photos && AdsDetail.adsInfo.photos.length>0" :src="`https://banooclubapi.oregonserver.com/media/gallery/adsphotos/${AdsDetail.adsInfo.photos[0].base64}`" :alt="AdsDetail.adsInfo.title">
        <img v-else :src="`/nopicture.jpg`" :alt="AdsDetail.title">
      </nuxt-link>
    </div>
    <div  class="card-body p-2 ">
      <div class="tw-relative tw-flex tw-justify-end tw-bottom-[15px] ">
        <div class="tw-bg-[#9cf5ff] tw-rounded tw-shadow tw-font-medium  tw-p-1 tw-text-[#db3cd6] ">
          <small>
            {{Intl.NumberFormat('fa-IR').format(AdsDetail.adsInfo.price)}}
          </small>
          <small class="Toman">تومان</small>
        </div>
      </div>
      <nuxt-link :to="`/Products/${AdsDetail.adsInfo.adsId}`" class="text-decoration-none">
        <h2 class="tw-text-lg tw-font-medium tw-mt-1 tw-t tw-truncate tw-text-slate-500 product_name tw-text-right mt-2">{{AdsDetail.adsInfo.title}}</h2>
      </nuxt-link>
      <div class="tw-text-xs tw-font-semibold tw-uppercase tw-text-yellow-500 text-end my-3">{{ AdsDetail.adsInfo.adsCategoryParents }}</div>
    </div>
  </div>
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
