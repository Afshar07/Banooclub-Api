<template>
  <div class="card "   v-if="service_details">
      <div class="tw-absolute tw-left-2 tw-top-2 tw-rounded-full tw-flex tw-items-center tw-justify-center customProfileImage" @click="goToServiceProfile(service_details.userInfo.userId)">
        <nuxt-link    v-if="service_details.userInfo.selfieFileData" :to="`/user/${service_details.userInfo.userName}/posts`">
        <img
          class="headerPicture"
          :src="`https://banooclubapi.simagar.com/media/gallery/profile/${service_details.userInfo.selfieFileData}`"
          alt=""

          style="border-radius: 50%; cursor: pointer;    width: 30px;  height: 30px;box-shadow: 0 0 9px rgb(72 69 72);"
        />
        </nuxt-link>
        <nuxt-link     v-else :to="`/user/${service_details.userInfo.userName}/posts`">
        <img

          class="headerPicture"
          :src="userDefault"
          alt=""

          style="border-radius: 50%; cursor: pointer;  width: 30px;  height: 30px;"
        />
        </nuxt-link>
      </div>
      <div class="card-media tw-h-40">
        <nuxt-link :to="`/Products/${service_details.servicePackId}`">
          <img v-if="service_details.medias!==null" :src="`https://banooclubapi.simagar.com/media/gallery/Service/${service_details.medias[0].base64}`" :alt="service_details.title" style="object-fit: cover;object-position: center; height: 160px !important;"/>
          <img v-else src="/nopicture.jpg" :alt="service_details.title" style="object-fit: cover;object-position: center; height: 160px !important;"/>
        </nuxt-link>
        <button @click="toggleWishList(service_details)" class="tw-bg-red-100 tw-absolute tw-right-2 tw-top-2 p-1 tw-rounded-full tw-text-red-500 tw-flex tw-items-center tw-justify-center">
          <svg v-if="service_details && service_details.isFavourite" xmlns="http://www.w3.org/2000/svg" class="tw-h-5 tw-w-5" fill="red" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
            <path stroke-linecap="round" stroke-linejoin="round" d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" />
          </svg>
          <svg v-else xmlns="http://www.w3.org/2000/svg" class="tw-h-5 tw-w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
            <path stroke-linecap="round" stroke-linejoin="round" d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" />
          </svg>
        </button>
      </div>
      <div  class="card-body p-2 ">

        <div class="tw-flex tw-items-center  tw-left-2  tw-justify-center position-absolute tw-top-[-15px]">

          <div v-if="!service_details.isFree" class="tw-top-3  tw-border-1 tw-bg-white ServicePrice tw-shadow tw-border-pink-700  tw-font-medium tw-px-2 tw-py-1 tw-left-2  tw-text tw-text-blue-500 ">
            {{Intl.NumberFormat('fa-IR').format(service_details.totalPrice)}}
            <small class="Toman">تومان</small>

          </div>
          <div v-if="service_details.isFree" class="tw-top-3  tw-border-1 tw-bg-white ServicePrice tw-shadow tw-border-pink-700  tw-font-medium tw-px-2 tw-py-1 tw-left-2  tw-text tw-text-blue-500 ">

            <small class="Toman">خدمت رایگان</small>

          </div>

        </div>
        <div class="tw-flex tw-items-center  tw-relative tw-justify-end">
          <div v-if="$route.path === '/Products/MyServices/'"  class=" tw-stack  tw-right-0 tw-cursor-pointer tw-absolute hover:tw-flex hover:tw-flex-col hover:tw-gap-2 tw-transition ">
            <div v-if="service_details.planTypes.includes(2)"
                 class="tw--top-3  tw-bg-yellow-100 tw-font-medium tw-px-2 tw-py-1 tw-shadow tw-left-2 tw-rounded-full tw-text tw-text-yellow-500 tw-">
              پیشنهادات
            </div>
            <div v-if="service_details.planTypes.includes(1)"
                 class="tw--top-3  tw-bg-yellow-100 tw-font-medium tw-px-2 tw-py-1 tw-shadow tw-left-2 tw-rounded-full tw-text tw-text-yellow-500 tw-">
              نردبان
            </div>
            <div v-if="service_details.planTypes.includes(4)"
                 class="tw--top-3  tw-bg-yellow-100 tw-font-medium tw-px-2 tw-py-1 tw-shadow tw-left-2 tw-rounded-full tw-text tw-text-yellow-500 tw-">
              جدید ترین ها
            </div>
            <div v-if="service_details.planTypes.includes(3)"
                 class="tw--top-3  tw-bg-yellow-100 tw-font-medium tw-px-2 tw-py-1 tw-shadow tw-left-2 tw-rounded-full tw-text tw-text-yellow-500 tw-">
              برترین ها
            </div>
          </div>


        </div>

        <nuxt-link :to="`/Products/${service_details.servicePackId}`" class="text-decoration-none">
          <h2 class="tw-text-lg tw-font-medium tw-mt-4 tw-t tw-truncate tw-text-slate-500 product_name tw-text-right ">{{service_details.title}}</h2>
        </nuxt-link>
<!--        <div class="tw-text-xs tw-font-semibold tw-uppercase tw-text-yellow-500 text-end">{{ service_details.catName }}</div>-->





        <div v-if="$route.path === '/Products/MyServices/'" class="my-2">
          <small v-if="service_details.status===2" class="text-secondary ">خدمت شما در دست بررسی است</small>
          <small v-if="service_details.status===1" class="text-success ">خدمت شما فعال است</small>
          <small v-if="service_details.status===3" class="text-warning ">خدمت شما نیاز به بازبینی مجدد دارد</small>
        </div>

        <div v-if="$route.path==='/Products/MyServices/'" class="w-100 d-flex flex-column">
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
export default {
  name: "Product",
  components: {HeartIcon},
  props:{
    service_details: {
      type: Object,
      required: true
    },

  },
  data(){
    return{
      headerImage:'',
      userDefault: require("~/assets/images/defaultUser.png"),

    }
  },
  mounted(){
    this.filterHeaderImage(1)
  },
  methods:{
    goToServiceProfile(userId){

      if(this.$auth.user.userInfo.userId === userId){
        this.$router.push({ path: `/Social/AccountSetting/MyPage` });
      }
      else {
        this.$router.push({ path: `/user/${userId}/posts` });
      }
    },
    async toggleWishList(item){
      try {
        await this.$repositories.toggleWishList.toggleWishList({
          objectId:item.servicePackId,
          type:1
        })
        this.$emit('GetServices')
        if(this.service_details.isFavourite){
          this.$toast.success("خدمت از علاقمندی ها حذف شد");
        }
        else{
          this.$toast.success("خدمت به علاقمندی ها اضافه شد");
        }


      }
      catch (error){
        console.log(error)
      }
    },
    filterHeaderImage(priority){
      if(this.service_details && this.service_details.medias!==null){
        this.service_details.medias.filter(item=>{
          if(item.priority == priority){
            this.headerImage = item.base64
          }
        })
      }

    }
  }
}

</script>

<style scoped>
.customProfileImage{
  position: absolute;
  width: 30px;
  height: 30px;
  z-index: 1;
}
.product_buttons{
  background-color: #fce6ff;
  color: #ff8100;
}
/*@media (min-width: 376px) and (max-width: 425px) {*/
/*  .card{*/
/*    width: 190px !important;*/
/*  }*/
/*}*/
/*@media (min-width: 320px) {*/
/*  .card{*/
/*    width: 140px !important;*/
/*  }*/
/*}*/
/*@media (min-width: 375px) {*/
/*  .card{*/
/*    width: 168px !important;*/
/*  }*/
/*}*/
/*@media (min-width: 426px) {*/
/*  .card{*/
/*    width: 240px !important;*/
/*  }*/
/*}*/
/*@media (min-width: 1024px) {*/
/*  .card{*/
/*    width: 190px !important;*/
/*  }*/
/*}*/
.card {
  /*width: 185px;*/
  background-color: white;
  border-radius: 8px;
  overflow: hidden;
  -webkit-box-shadow: 0 1px 3px 0 rgb(0 0 0 / 10%), 0 1px 2px 0 rgb(0 0 0 / 6%);
  box-shadow: 0 1px 3px 0 rgb(0 0 0 / 10%), 0 1px 2px 0 rgb(0 0 0 / 6%);
}
.product_name:hover{
  color: #007bff !important;
}

.ServicePrice{

  border-radius: 7px;
  color: #787878;
  font-size: 12px;
  font-weight: bolder;
  background-color: #ffe6e6;
  box-shadow: 0px 0px 16px rgb(229 173 237);
}

</style>
