<template>
  <div class="card" v-if="service_details">
    <div class="card-media tw-h-40">
      <div class="card-media-overly"></div>
      <img :src="`https://BanooClubapi.simagar.com/media/gallery/Profile/${service_details.userInfo.selfieFileData}`" :alt="service_details.title">
      <span class="tw-absolute tw-bg-white tw-px-2 tw-py-1 tw-text-sm tw-rounded-md tw-m-2"> Label</span>
      <button class="tw-bg-red-100 tw-absolute tw-right-2 tw-top-2 p-1 tw-rounded-full tw-text-red-500 tw-flex tw-items-center tw-justify-center">
<!--        <HeartIcon fill="red"/>-->
        <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-5 tw-w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
          <path stroke-linecap="round" stroke-linejoin="round" d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" />
        </svg>
      </button>
    </div>
    <div class="card-body">
      <div
        class="tw--top-3 tw-absolute tw-bg-blue-100 tw-font-medium tw-px-2 tw-py-1 tw-left-2 tw-rounded-full tw-text tw-text-blue-500 tw-">
        {{Intl.NumberFormat('fa-IR').format(service_details.totalPrice)}}
        تومان
      </div>
      <div class="tw-text-xs tw-font-semibold tw-uppercase tw-text-yellow-500 text-end">دسته بندی</div>
      <nuxt-link :to="`/Products/${service_details.servicePackId}`" class="text-decoration-none">
        <h2 class="tw-text-lg tw-font-medium tw-mt-1 tw-t tw-truncate tw-text-slate-500 product_name tw-text-right">{{service_details.title}}</h2>
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
      type: Array,
      required: true
    }
  },
  data(){
    return{
      service_category:[]
    }
  },
  mounted() {
    this.getServiceCategory()
    console.log('this.service_details',this.service_details)
  },
  methods:{
    async getServiceCategory(){
      const serviceCategoryId = this.service_details.serviceCategoryId;
      const category =  await this.$repositories.getAServiceCategory.getAServiceCategory({
        serviceCategoryId
      })
      console.log(category)
    }
  }
}
</script>

<style scoped>
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
