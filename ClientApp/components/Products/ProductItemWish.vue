<template>
  <div class="card" v-if="service_details.serviceInfo" >
    <div class="card-media tw-h-40">
      <div class="card-media-overly"></div>
      <nuxt-link :to="`/Products/${service_details.serviceInfo.servicePackId}`">
        <img v-if="service_details.serviceInfo && service_details.serviceInfo.medias!==null" :src="`https://banooclubapi.oregonserver.com/media/gallery/service/${service_details.serviceInfo.medias[0].base64}`"  alt="">
        <img v-else :src="`/nopicture.jpg`"  alt="">
      </nuxt-link>

    </div>
    <div  class="card-body p-2 ">

      <div class="tw-flex tw-items-center  tw-left-2  tw-justify-center position-absolute tw-top-[-15px]">

        <div class="tw-top-3  tw-border-1  tw-rounded tw-shadow tw-bg-[#9cf5ff]  tw-font-medium tw-px-2 tw-py-1 tw-left-2  tw-text-[#db3cd6]">
          {{Intl.NumberFormat('fa-IR').format(service_details.serviceInfo.totalPrice)}}
          <small class="Toman">تومان</small>
        </div>

      </div>

      <nuxt-link :to="`/Products/${service_details.serviceInfo.servicePackId}`" class="text-decoration-none">
        <h2 class="tw-text-lg tw-font-medium tw-mt-4 tw-t tw-truncate tw-text-slate-500 product_name tw-text-right ">{{service_details.serviceInfo.title}}</h2>
      </nuxt-link>
    </div>
  </div>
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
    show_buttons:{
      type: Boolean,
      required: true
    }
  },

  methods:{
    async createWishList(){
      try {
        await this.$repositories.createWishList.createWishList({
          objectId: this.service_details.servicePackId,
          type: 1,
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
