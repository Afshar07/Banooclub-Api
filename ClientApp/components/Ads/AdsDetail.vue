<template>
  <div>

    <div class="custom_card">
      <!-- post header-->
      <div class="tw-flex tw-justify-between tw-items-center lg:tw-p-4 tw-p-2.5">
        <div class="tw-flex tw-items-center">
          <nuxt-link :to="`/user/${AdsDetail.userInfo.userId}/posts`" class="text-decoration-none">
            <img v-if="AdsDetail.userInfo.selfieFileData!==null" :src="`https://banooclubapi.simagar.com/media/gallery/profile/${AdsDetail.userInfo.selfieFileData}`" alt="profile_image" class="tw-bg-gray-200 tw-border tw-border-white tw-rounded-full tw-w-10 tw-h-10">
            <img v-else :src="`/nopicture.jpg`" alt="profile_image" class="tw-bg-gray-200 tw-border tw-border-white tw-rounded-full tw-w-10 tw-h-10">
          </nuxt-link>
          <div class="d-flex flex-column tw-font-semibold tw-capitalize tw-mr-4">
            <h2 class="tw-text-black text-decoration-none service_name">{{ AdsDetail.userInfo.userName }}</h2>
            <div class="d-flex">
              <div class="d-flex justify-content-start align-items-center">
                <AllUsersIcon fill="#374151" style="width: 15px; height: 15px;"/>
                <div class="text-gray-700 tw-flex tw-items-center px-2">
                  {{
                    time_ago(AdsDetail.createDate)
                  }}
                </div>
              </div>
            </div>

          </div>
        </div>
        <div >
          <button v-click-outside="showMoreDiv" @click.stop="showMoreDiv"   class="tw-text-2xl hover:tw-bg-gray-200 tw-rounded-full tw-p-2 tw-transition tw--mr-1 tw-relative">
            <svg  xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
              <path stroke-linecap="round" stroke-linejoin="round" d="M5 12h.01M12 12h.01M19 12h.01M6 12a1 1 0 11-2 0 1 1 0 012 0zm7 0a1 1 0 11-2 0 1 1 0 012 0zm7 0a1 1 0 11-2 0 1 1 0 012 0z" />
            </svg>
          </button>

          <div v-if="show_more" class="tw-absolute tw-z-10 tw-left-5 tw-bg-white tw-w-56 tw-shadow-md tw-mx-auto tw-p-2 tw-rounded-md tw-text-gray-500 tw-text-base tw-border tw-border-gray-100">
            <ul class="tw-pl-0 mb-0">
              <li>
                <a @click="Share" class="tw-w-full tw-text-gray-700 text-decoration-none tw-flex tw-items-center tw-px-3 tw-py-2 hover:tw-bg-gray-200 hover:tw-text-gray-800 tw-rounded-md">

                  <small class="d-flex tw-text-gray-700">
                    <ShareIcon  class="tw-ml-1"/>

                    اشتراک گذاری
                  </small>
                </a>
              </li>
              <li>
                <button v-if="!AdsDetail.isFavourite" @click.stop="createWishList(AdsDetail.adsId)" class="tw-w-full tw-text-gray-700 text-decoration-none tw-flex tw-items-center tw-px-3 tw-py-2 hover:tw-bg-gray-200 hover:tw-text-gray-800 tw-rounded-md">
                <small class="d-flex">
                    <EmptyStar/>
                    افزودن به علاقه مندی ها
                  </small>
                </button>
                <button v-else @click="deleteWishList(AdsDetail.adsId)" class="tw-w-full tw-text-gray-700 text-decoration-none tw-flex tw-items-center tw-px-3 tw-py-2 hover:tw-bg-gray-200 hover:tw-text-gray-800 tw-rounded-md">
                 <small  class="d-flex">
                    <SolidSter/>
                    حذف از علاقه مندی ها
                  </small>
                </button>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <vueper-slides
        class="my-2 no-shadow"
        ref="vueperslides1"
        @slide="$refs.vueperslides2.goToSlide($event.currentSlide.index, { emit: false })"
        :rtl="true"
        :slide-ratio="1 / 4"
        :bullets="false"
        :touchable="false"
        fade
        :arrows="true"
        :autoplay="false"
        fixed-height="340px">
        <vueper-slide
          v-for="(slide, i) in AdsDetail.photos"
          :key="i"
        >
          <template #content>
            <img v-if="slide.priority == 1 || slide.priority == 2" class="tw-w-full tw-h-full" :src="`https://banooclubapi.simagar.com/${slide.base64}`" :alt="AdsDetail.title"
                 style="object-fit: contain;object-position: center !important;height: 330px;!important; ">
            <video
              v-else-if="slide.priority == 3"
              class="w-100 tw-h-full"
              controls
              :src="`https://banooclubapi.simagar.com/${slide.base64}`"
            ></video>
          </template>
        </vueper-slide>
      </vueper-slides>
      <vueper-slides
        class="no-shadow mb-0"
        ref="vueperslides2"
        :rtl="true"
        :slide-ratio="1 / 8"
        :dragging-distance="50"
        @slide="$refs.vueperslides1.goToSlide($event.currentSlide.index, { emit: false })"
        :visible-slides="3"
        fixed-height="100px"
        :arrows="true"
        :bullets="false"
        :touchable="true"
        :gap="2">
        <vueper-slide
          v-for="(slide, i) in AdsDetail.photos"
          :key="i"
          @click.native="$refs.vueperslides2 && $refs.vueperslides2.goToSlide(i)">
          <template #content>
            <img v-if="slide.priority == 1 || slide.priority == 2" class=" tw-w-full" :src="`https://banooclubapi.simagar.com/${slide.base64}`" :alt="AdsDetail.title" style="object-fit: cover;object-position: center !important; height: 100px;">
            <img v-else-if="slide.priority == 3" class=" tw-w-full"
                 src="/video_player.jpg"
                 :alt="AdsDetail.title" style="object-fit: contain;object-position: center !important; height: 100px;">
          </template>
        </vueper-slide>
      </vueper-slides>

      <div class="tw-p-4 tw-space-y-3">
        <div class="d-flex justify-content-between">
          <h1 class="tw-text-2xl tw-font-semibold tw-text-gray-600 tw-pt-2">{{AdsDetail.title}}</h1>
          <div class="d-flex align-items-center gap-2">
            <div class="tw-bg-gray-100 tw-text-gray-600 tw-font-semibold tw-px-3 tw-py-1 tw-rounded-full tw-text tw-text-sm d-flex justify-content-center align-items-center">
              {{Intl.NumberFormat('fa-IR').format(AdsDetail.price)}}
              تومان
            </div>
            <div v-if="AdsDetail.exchangeability" class="tw-bg-sky-400 tw-text-gray-600 tw-font-semibold tw-px-3 tw-py-1 tw-rounded-full tw-text tw-text-sm d-flex justify-content-center align-items-center">
              قابل معاوضه
            </div>
          </div>

        </div>
        <div class="d-flex mt-0">
          <div class="back_tags p-1 m-1" v-for="(tag,index) in AdsDetail.tags">
            {{ tag.title }}
          </div>
        </div>

        <h2 class="tw-text-base tw-font-semibold tw-text-gray-600 tw-pt-2">توضیحات</h2>
        <p class="tw-text-gray-600 mt-1">
          {{AdsDetail.description}}
        </p>
        <div v-if="AdsDetail.tag!==null" class="tw-text-gray-600 d-flex flex-column tw-text-sm md:tw-pt-3">
          <h2 class="tw-text-base tw-font-semibold tw-text-gray-600 tw-pt-2 mb-0">هشتگ ها</h2>
          <div class="d-flex align-items-center gap-2">
            <span class="text-secondary"  v-for="item in ArrangedTags(AdsDetail.tag)"> #{{ item}} </span>
          </div>
        </div>
        <hr>
        <!--        <div class="tw-grid md:tw-grid-cols-2 md:tw-gap-4 tw-mb-5 xs:tw-grid-cols-12">-->
        <!--          <a :href="`tel:${AdsDetail.mobile}`" class="text-decoration-none tw-bg-gray-200 tw-flex tw-flex-1 tw-font-semibold tw-h-10 tw-items-center tw-justify-center tw-px-4 tw-rounded-md my-1">-->
        <!--            تماس-->
        <!--          </a>-->
        <button @click="GoToChat(AdsDetail)" class="tw-bg-blue-600 tw-flex tw-flex-1 tw-font-semibold tw-h-10 tw-items-center tw-justify-center tw-px-4 tw-rounded-md tw-text-white my-1">
          چت
        </button>
        <h2 class="tw-text-base tw-font-semibold tw-text-gray-600 tw-pt-2">موقعیت روی نقشه</h2>
        <div class="my-3" >
          <client-only>
          <Map v-if="AdsDetail.latitude" :latitude="AdsDetail.latitude" :longitude="AdsDetail.longitude"></Map>
          </client-only>
        </div>


        <div>
        </div>


      </div>

    </div>
  </div>

</template>

<script>
import AllUsersIcon from "../Icons/AllUsersIcon";
import MoreIcon from "../Icons/MoreIcon";
import LikeIcon from "../Icons/LikeIcon";
import MessageIcon from "../Icons/MessageIcon";
import ShareIcon from "../Icons/ShareIcon";
import EditIcon from "../Icons/EditIcon";
import {VueperSlides, VueperSlide} from 'vueperslides'
import 'vueperslides/dist/vueperslides.css';
import BaseModal from "../../components/utilities/BaseModal";
import TelegramIcon from "../Icons/TelegramIcon";
import WhatsappIcon from "../Icons/WhatsappIcon";
import InstagramIcon from "../Icons/InstagramIcon";
import Map from "../Map";
import FacebookIcon from "../Icons/FacebookIcon";
import EmptyStar from "../Icons/EmptyStar";
import SolidSter from "../Icons/SolidSter";


export default {
  name: "PDetails",
  components: {
    SolidSter,
    EmptyStar,
    FacebookIcon,
    InstagramIcon,
    WhatsappIcon,
    Map,
    TelegramIcon,
    ShareIcon, MessageIcon, LikeIcon, MoreIcon, AllUsersIcon, EditIcon,  VueperSlides, VueperSlide, BaseModal },
  data(){
    return{
      // site_url: 'pplus.simagar.com',
      show_more: false,
      images_preview:[],
      serviceVideos:[],
      is_show_delete_modal:false,
      service_comment:'',
      others_can_add_comments:true,
      show_reply_modal:false,
      place_holder:'',
      want_to_reply:false,
      reply_parent_id:0,
      site_url: 'https://banooclub.simagar.com',
      base_id:0,
      parentComments:[],
      service_rate:0,
      rates:[],
      center: [35.757539, 51.409968],
      latlng: [35, 51],
      service_rates_show:[1,2,3,4,5],
      custom_service_rates:[1,2,3,4,5]
    }
  },
  async fetch(){

  },
  methods:{
    GoToChat(item){
      this.$router.push({path:'/Social/Chat',query:{userId:item.userInfo.userId,Photo:item.userInfo.selfieFileData}})
    },
    Share(){
      if(navigator.share){
        navigator.share({
          title: this.AdsDetail.title,
          url: this.site_url+this.$route.fullPath
        })
      }

    },
    ArrangedTags(tags){
      let data = tags.split(',')
      return data
    },
    addMarker(event) {
      this.latlng = event.latlng;
    },
    filteredMedias() {
      this.AdsDetail.photos.filter(item => {
        if (item.priority === 1 || item.priority === 2) {
          this.images_preview.push(item)
        }
        if(item.priority === 3){
          this.serviceVideos.push(item)

        }
      })
    },
    time_ago(time) {
      switch (typeof time) {
        case 'number':
          break;
        case 'string':
          time = +new Date(time);
          break;
        case 'object':
          if (time.constructor === Date) time = time.getTime();
          break;
        default:
          time = +new Date();
      }
      let time_formats = [
        [60, 'ثانیه ', 1], // 60
        [120, '1 دقیقه پیش', 'یک دقیقه پیش'], // 60*2
        [3600, 'دقیقه ', 60], // 60*60, 60
        [7200, '1 ساعت پیش', '1 ساعت پیش'], // 60*60*2
        [86400, 'ساعت ', 3600], // 60*60*24, 60*60
        [172800, 'دیروز', 'فردا'], // 60*60*24*2
        [604800, 'روز ', 86400], // 60*60*24*7, 60*60*24
        [1209600, 'هفته پیش', 'هفته بعد'], // 60*60*24*7*4*2
        [2419200, 'هفته', 604800], // 60*60*24*7*4, 60*60*24*7
        [4838400, 'ماه پیش', 'ماه بعد'], // 60*60*24*7*4*2
        [29030400, 'ماه', 2419200], // 60*60*24*7*4*12, 60*60*24*7*4
        [58060800, 'سال پیش', 'سال بعد'], // 60*60*24*7*4*12*2
        [2903040000, 'سال', 29030400], // 60*60*24*7*4*12*100, 60*60*24*7*4*12

      ];
      let seconds = (+new Date() - time) / 1000,
        token = 'پیش',
        list_choice = 1;

      if (seconds == 0) {
        return 'پیش'
      }
      if (seconds < 0) {
        seconds = Math.abs(seconds);
        token = 'پیش';
        list_choice = 2;
      }
      let i = 0,
        format;
      while (format = time_formats[i++])
        if (seconds < format[0]) {
          if (typeof format[2] == 'string')
            return format[list_choice];
          else
            return Math.floor(seconds / format[2]) + ' ' + format[1] + ' ' + token;
        }
      return time;
    },

    async createWishList(id){
      try {
        await this.$repositories.createWishList.createWishList({
          objectId: id,
          type: 3,
          userId: this.$auth.user.userInfo.userId,
          createDate: new Date(Date.now())
        })
        this.$toast.success("خدمت به علاقمندی ها افزوده شد.");
        this.$nuxt.refresh();


      }
      catch (error){
        console.log(error)
      }

    },
    async deleteWishList(id){
      try {
        await this.$repositories.deleteWishList.deleteWishList({
          objectId: id,
          type: 3,
          userId: this.$auth.user.userInfo.userId,
          createDate: new Date(Date.now())
        })
        this.$toast.success("خدمت از علاقمندی ها حذف شد");
        this.$nuxt.refresh();
      }
      catch (error){
        console.log(error)
      }

    },
    showMoreDiv(){

      this.show_more = !this.show_more
    },
  },
  mounted() {
  },
  watch:{
  },
  props:{
    AdsDetail:{
      type: Object,
      required: true
    }
  }
}
</script>

<style>
.comments_section_height{
  height: 300px;
  overflow-y: scroll;
  border-bottom: 1px solid #e5e7eb;
}
.service_name{
  font-size: 16px;
  font-weight: 600;
}
.leaflet-top, .leaflet-bottom {
  z-index: 999 !important;
}

</style>
