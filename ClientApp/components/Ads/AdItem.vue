<template>
  <div class="card" v-if="AdsDetail" @click="$router.push(`/Migration/AdvertiseDetail/${AdsDetail.adsId}`)">
    <div
      class="tw-absolute tw-left-2 tw-top-2 tw-rounded-full tw-flex tw-items-center tw-justify-center customProfileImage"
      @click="goToServiceProfile(AdsDetail.userInfo.userId)">
      <nuxt-link v-if="AdsDetail.userInfo && AdsDetail.userInfo.selfieFileData"
                 :to="`/user/${AdsDetail.userInfo.userName}/posts`">
        <img
          class="headerPicture"
          :src="`https://api.banooclub.simagar.com/${AdsDetail.userInfo.selfieFileData}`"
          alt=""

          style="border-radius: 50%; cursor: pointer;    width: 30px;  height: 30px;box-shadow: 0 0 9px rgb(72 69 72);"
        />
      </nuxt-link>
      <nuxt-link v-else-if="AdsDetail.userInfo" :to="`/user/${AdsDetail.userInfo.userName}/posts`">
        <img
          class="headerPicture"
          :src="userDefault"
          alt=""
          style="border-radius: 50%; cursor: pointer;  width: 30px;  height: 30px;"
        />
      </nuxt-link>
    </div>
    <div class="cardImage tw-relative">
      <button @click="toggleWishList(AdsDetail)" class="tw-bg-red-100 tw-absolute tw-right-2 tw-top-2 p-1 tw-rounded-full tw-text-red-500 tw-flex tw-items-center tw-justify-center">
        <svg v-if="AdsDetail.isFavourite" xmlns="http://www.w3.org/2000/svg" class="tw-h-5 tw-w-5" fill="red" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
          <path stroke-linecap="round" stroke-linejoin="round" d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" />
        </svg>
        <svg v-else xmlns="http://www.w3.org/2000/svg" class="tw-h-5 tw-w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
          <path stroke-linecap="round" stroke-linejoin="round" d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" />
        </svg>
      </button>
      <img v-if="AdsDetail.photos && AdsDetail.photos.length>0" class="tw-w-full tw-h-[7rem] tw-object-cover"
           :src="`https://api.banooclub.simagar.com/${AdsDetail.photos[0].base64}`" :alt="AdsDetail.title">
      <img v-else src="/nopicture.jpg" class="tw-w-full tw-h-[7rem] tw-object-cover" :alt="AdsDetail.title">
    </div>




      <div class="card-body p-2 ">

        <div class="tw-flex tw-items-center mb-3  tw-left-2  tw-justify-center position-absolute tw-top-[-15px]">

          <div
            class="tw-top-3  tw-border-1 tw-bg-[#9cf5ff] tw-relative tw-rounded tw-shadow-lg   tw-font-medium tw-px-2 tw-py-1 tw-left-2   tw-text-[#db3cd6] ">
            <i v-if="AdsDetail.planTypes && AdsDetail.planTypes.length>0 &&  AdsDetail.planTypes.includes(1)" class="fas fa-fire tw-cursor-pointer text-danger tw-absolute tw-top-0 tw-left-0 tw-shadow" v-tooltip="{content:'آتیش زدم'}"></i>
            {{ Intl.NumberFormat('fa-IR').format(AdsDetail.price) }}
            <small class="Toman">تومان</small>

          </div>

        </div>
        <br>
        <nuxt-link :to="`/Migration/AdvertiseDetail/${AdsDetail.adsId}`" class="text-decoration-none">
          <h2
            class="tw-text-lg tw-font-medium  tw-truncate tw-text-slate-500 product_name tw-text-right mt-2 ShortDescriptionIndex">
            {{ AdsDetail.title }}</h2>
        </nuxt-link>
        <div v-if="AdsDetail.adsCategoryParents!==''" class="tw-text-xs tw-font-semibold tw-uppercase tw-text-[#9170f5] text-end my-3">
          {{ AdsDetail.adsCategoryParents }}
        </div>
        <div v-else class="tw-text-xs tw-font-semibold tw-uppercase tw-text-[#9170f5] text-end my-3">
         بدون دسته بندی
        </div>
        <div v-if="$route.path==='/social/accountsetting/MyPage'" class="w-100 d-flex  align-items-center justify-content-between">
          <i v-tooltip="{content:'ویرایش'}" @click.prevent="$router.push({path: `/Migration/EditAdvertise/`,query:{adId:AdsDetail.adsId}})" class=" tw-cursor-pointer fas fa-edit tw-text-[#949494]"></i>
          <i v-tooltip="{content:'ارتقا'}" @click.prevent="$router.push({path: `/Migration/UpgradeAd/${AdsDetail.adsId}`})" class=" tw-cursor-pointer fas fa-rocket tw-text-[#949494]"></i>
          <button @click="Share(AdsDetail)">
          <ShareAltIcon   v-tooltip="{content:'اشتراک گذاری'}" class=" tw-cursor-pointer tw-fill-[#949494]"></ShareAltIcon>
          </button>

        </div>
      </div>

    </div>


</template>

<script>
import HeartIcon from "../Icons/HeartIcon";
import AdsDetail from "@/components/Ads/AdsDetail";
import ShareAltIcon from "@/components/Icons/ShareAltIcon";
export default {
  name: "Product",
  components: {HeartIcon,ShareAltIcon},
  props: {
    AdsDetail: {
      type: Object,
      required: true
    },

  },
  data() {
    return {
      userDefault: require("~/assets/images/defaultUser.png"),
      site_url: 'https://banooclub.simagar.com',
    }
  },

  methods: {
    Share(item){
      if(navigator.share){
        navigator.share({
          title: item.title,
          url: this.site_url+this.$route.fullPath
        })
      }

    },
    goToServiceProfile(userId) {

      if (this.$auth.user.userInfo.userId === userId) {
        this.$router.push({path: `/Social/AccountSetting/MyPage`});
      } else {
        this.$router.push({path: `/user/${userId}/posts`});
      }
    },
    async toggleWishList(item) {
      try {
        await this.$repositories.toggleWishList.toggleWishList({
          objectId: item.adsId,
          type: 3
        })
        if (item.isFavourite) {

          this.$toast.success("آگهی از علاقمندی ها حذف شد");
        } else {
          this.$toast.success("آگهی به علاقمندی ها اضافه شد");

        }
        this.$nuxt.refresh()
        this.$fetch()
      } catch (error) {
        console.log(error)
      }
    },

  }
}
</script>

<style scoped>
.product_buttons {
  background-color: #fce6ff;
  color: #ff8100;
}

@media (min-width: 376px) and (max-width: 425px) {
  .card {
    width: 190px !important;
  }
}

@media (min-width: 320px) {
  .card {
    width: 140px !important;
  }
}

@media (min-width: 375px) {
  .card {
    width: 168px !important;
  }
}

@media (min-width: 426px) {
  .card {
    width: 240px !important;
  }
}

@media (min-width: 1024px) {
  .in_my_services {
    width: 190px !important;

  }

  .card {
    width: 190px !important;
  }
}

.card {
  width: 185px;
  height: 270px;
  background-color: white;
  border-radius: 8px;
  overflow: hidden;
  -webkit-box-shadow: 0 1px 3px 0 rgb(0 0 0 / 10%), 0 1px 2px 0 rgb(0 0 0 / 6%);
  box-shadow: 0 1px 3px 0 rgb(0 0 0 / 10%), 0 1px 2px 0 rgb(0 0 0 / 6%);
}

.product_name:hover {
  color: #007bff !important;
}
</style>
