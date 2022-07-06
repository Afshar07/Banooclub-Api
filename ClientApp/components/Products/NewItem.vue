<template>
  <div class="card d-flex flex-row p-2">
    <div class="tw-w-20 tw-h-24 tw-overflow-hidden tw-rounded-lg">
      <div class="card-media tw-h-24">
        <img :src="`https://pplusapi.simagar.com/media/gallery/Profile/${service_details.userInfo.selfieFileData}`" :alt="service_details.title">
      </div>
    </div>
    <div class="d-flex flex-column tw-w-full mx-3">
      <div class="d-flex justify-content-between align-items-center">
        <div class="tw-text-xs tw-font-semibold tw-uppercase tw-text-yellow-500">{{ service_details.catName }}</div>
        <div class="tw-bg-gray-100 tw-font-semibold tw-px-2.5 tw-py-1 tw-rounded-full tw-text tw-text-blude-500 tw-text-sm">
          {{Intl.NumberFormat('fa-IR').format(service_details.totalPrice)}}
          تومان
        </div>
      </div>
      <nuxt-link :to="`/Products/${service_details.servicePackId}`" class="text-decoration-none">
        <h2 class="tw-text-lg tw-font-medium tw-mt-1 tw-t tw-truncate tw-text-slate-500 product_name tw-text-right">{{service_details.title}}</h2>
      </nuxt-link>
      <div class="tw-flex tw-items-center tw-text-sm tw-text-gray-400 tw-capitalize">
        <div> 15 لایک </div>
        <div class="mr-2">·</div>
        <div> {{ service_details.viewsCount }} بازدید </div>
      </div>


    </div>
  </div>


</template>

<script>
export default {
  name: "NewItem",
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
.card {
  background-color: white;
  border-radius: 8px;
  overflow: hidden;
  -webkit-box-shadow: 0 1px 3px 0 rgb(0 0 0 / 10%), 0 1px 2px 0 rgb(0 0 0 / 6%);
  box-shadow: 0 1px 3px 0 rgb(0 0 0 / 10%), 0 1px 2px 0 rgb(0 0 0 / 6%);
}
</style>
