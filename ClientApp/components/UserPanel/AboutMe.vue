<template>
  <div class="widget custom_card p-3">
    <div class="tw-flex tw-items-center tw-justify-between">
      <h4 class="tw-text-lg tw-font-semibold"> درباره من </h4>
      <nuxt-link to="/Social/AccountSetting/EditProfileBasic" class="btn btn-secondary btn-sm">ویرایش</nuxt-link>
    </div>

    <ul class="tw-text-gray-600 space-y-3 tw-mt-3">
      <li class="tw-flex tw-items-center my-2" v-if="userinfo && (userinfo.cityName !== '' || userinfo.stateName !== '')">
        <HomeIcon style="width: 30px; height: 30px;" fill="rgb(75 85 99)" class="tw-rounded-full tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"/>
        ساکن
         <strong class="px-1">{{userinfo.stateName}} <span v-if="userinfo.cityName !== '' && userinfo.stateName !== ''">,</span> {{userinfo.cityName}}</strong>
      </li>
      <li  class="tw-flex tw-items-center my-2" v-if="  userinfo.userSetting&&userinfo.userSetting.birthDate===null&&userinfo.relationState !== null">
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
      <li  class="tw-flex tw-items-center my-2" v-else-if="  userinfo.userSetting">
        <AllUsersIcon fill="rgb(75 85 99)" style="width: 30px; height: 30px;"  class="tw-rounded-full tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"/>
        <div >
          <strong  v-if="  userinfo.userSetting&& userinfo.userSetting.birthDate!==null"> {{ time_ago(userinfo.userSetting.birthDate) }}  </strong>
        </div>

      </li>
      <li  class="tw-flex tw-items-center my-2" v-else-if="  userinfo.userSetting&& userinfo.userSetting.birthDate!==null">
          <ServiceProviderLabel v-if="userinfo.type===3"></ServiceProviderLabel>
        <CustomerLabel v-else></CustomerLabel>
      </li>
      <li class="tw-flex tw-items-center my-2">
        <AllUsersIcon fill="rgb(75 85 99)" style="width: 30px; height: 30px;"  class="tw-rounded-full tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"/>
        دنبال شده توسط  <strong class="px-1">{{ $auth.user.baseData.followersCount }}</strong> نفر
      </li>
      <li  class="tw-flex tw-items-center my-2" v-if="  userinfo.userSetting">
        <QuestionIcon fill="rgb(75 85 99)" style="width: 30px; height: 30px;" class="tw-rounded-full tw-w-10 tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"></QuestionIcon>

        <div >
          <p class="m-0 ShortDescriptionIndex"> {{ userinfo.userSetting.bio }}  </p>
        </div>

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
import QuestionIcon from '../../components/Icons/QuestionIcon'
import CustomerLabel from "@/components/CustomerLabel";
import ServiceProviderLabel from "@/components/ServiceProviderLabel";
import CropIcon from "../Icons/WhiteCropIcon";
export default {
  name: "AboutMe",
  components: {
    AllUsersIcon,
    QuestionIcon,
    HeartIcon,
    ServiceProviderLabel,
    CustomerLabel,
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
        [120, '1 دقیقه ', 'یک دقیقه '], // 60*2
        [3600, 'دقیقه ', 60], // 60*60, 60
        [7200, '1 ساعت ', '1 ساعت '], // 60*60*2
        [86400, 'ساعت ', 3600], // 60*60*24, 60*60
        [172800, 'دیروز', 'فردا'], // 60*60*24*2
        [604800, 'روز ', 86400], // 60*60*24*7, 60*60*24
        [1209600, 'هفته ', 'هفته '], // 60*60*24*7*4*2
        [2419200, 'هفته', 604800], // 60*60*24*7*4, 60*60*24*7
        [4838400, 'ماه ', 'ماه '], // 60*60*24*7*4*2
        [29030400, 'ماه', 2419200], // 60*60*24*7*4*12, 60*60*24*7*4
        [58060800, 'سال ', 'سال '], // 60*60*24*7*4*12*2
        [2903040000, 'سال', 29030400], // 60*60*24*7*4*12*100, 60*60*24*7*4*12

      ];
      let seconds = (+new Date() - time) / 1000,
        token = '',
        list_choice = 1;

      if (seconds == 0) {
        return ''
      }
      if (seconds < 0) {
        seconds = Math.abs(seconds);
        token = '';
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
    async getUserInfo() {
      try {
        const response = await this.$repositories.getUserByToken.getUserByToken();
        this.userinfo = response.data.userInfo;
console.log(this.userinfo)
      } catch (error) {
        console.log(error);
      }
    },

  }


}
</script>

<style scoped>

</style>
