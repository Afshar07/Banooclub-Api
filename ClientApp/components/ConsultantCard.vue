<template>
  <nuxt-link class="bg-white text-black cursor-pointer shadow row rounded p-md-4 p-2 mb-3   row"
             style="min-height: 10rem;text-decoration: none"
             :to="`/consultation/${consultantInfo.id}`">
    <div class="col-md-6 ">
      <div class="d-flex gap-3 h-100 align-items-start">
        <div class="rounded-circle  d-flex flex-column justify-content-start align-items-center"
             style="height: 100%">
          <img v-if="consultantInfo.userProfile" :src="baseUrl +consultantInfo.userProfile" alt=""
               class="rounded-circle" style="object-fit: cover;width: 5rem;height: 5rem">
          <img v-else alt="" src="/defaultUser.png" style="object-fit: contain;width: 5rem;height: 5rem">
        </div>
        <div class="d-flex flex-column h-100 justify-content-between align-items-start">
          <div class="d-flex flex-column justify-content-start align-items-start">
            <h5 class="tw-font-bold">{{ consultantInfo.name + ' ' + consultantInfo.lName }}</h5>
            <div class="d-flex align-items-center flex-wrap gap-1">
              <span v-for="(item,idx) in consultantInfo.cats" :key="idx" class="badge pill bg-pink text-white ">{{
                  item
                }}</span>
            </div>
          </div>
          <div class="d-flex gap-3 my-3 align-items-center flex-wrap">
                  <span class=" py-1 px-2 rounded d-flex gap-2 align-items-center " style="background-color: #ffdb66">
                  <LazyStarIcon style="height: 15px;width: 15px;fill: white"></LazyStarIcon>
                    <small class="text-white">{{ consultantInfo.rating }}</small>
                  </span>
            <span class=" py-1 px-2 rounded d-flex  gap-2 align-items-center " style="background-color: #71ffe2">
                  <LazyCommentIcon style="height: 15px;width: 15px;fill:white"></LazyCommentIcon>
                    <small class="text-white">{{ consultantInfo.commentCount }} دیدگاه</small>
                  </span>
          </div>
          <div v-if="consultantInfo.prices.includes(2)" class="d-flex gap-3   align-items-center flex-wrap">
                   <span class=" py-1 px-2 rounded d-flex gap-2 align-items-center " style="background-color: #e7b0fe">
                  <LazyPhoneIcon style="height: 15px;width: 15px;fill:white"></LazyPhoneIcon>
                    <small class="text-white">فوری</small>
                  </span>
            <span v-if="consultantInfo.prices.includes(1)" class=" py-1 px-2 rounded d-flex gap-2 align-items-center " style="background-color: #e7b0fe">
                  <LazySmartPhoneIcon style="height: 15px;width: 15px;fill:white"></LazySmartPhoneIcon>
                    <small class="text-white">تلفنی</small>
                  </span>
            <span v-if="consultantInfo.prices.includes(3)" class=" py-1 px-2 rounded d-flex gap-2 align-items-center " style="background-color: #e7b0fe">
                  <LazyCommentFillIcon style="height: 15px;width: 15px;fill:white"></LazyCommentFillIcon>
                    <small class="text-white">ویدیویی</small>
                  </span>
            <!--                  <span class=" py-1 px-2 rounded d-flex gap-2 align-items-center " style="background-color: #e6f8f5">-->
            <!--                  <LazyCommentIcon style="height: 15px;width: 15px"></LazyCommentIcon>-->
            <!--                    <small> ویدیویی</small>-->
            <!--                  </span>-->
          </div>
        </div>
      </div>
    </div>
    <div class="col-md-6 my-md-0 my-3">
      <div class="d-flex flex-column justify-content-between h-100 ">
        <div class="d-flex flex-column align-items-start gap-2">
          <div class="d-flex gap-1">
            <small class="text-muted">شماره شناسایی : </small>
            <small>{{ consultantInfo.medicalSystemNumber }}</small>
          </div>
          <div class="d-flex gap-1">
            <small class="text-muted">مشاوره موفق : </small>
            <small>{{ consultantInfo.successCount }}</small>
          </div>
        </div>
        <span class="d-flex align-items-center align-self-end gap-2">
                <small class="text-pink">دریافت مشاوره</small>
                <LazyArrowLeftIcon style="width: 20px;height: 20px;fill: #fc5399"></LazyArrowLeftIcon>
                </span>
      </div>
    </div>
  </nuxt-link>
</template>

<script>
export default {
  name: "ConsultantCard",
  props: {
    consultantInfo: {
      type: Object,
      required: false
    }
  },
  computed: {
    baseUrl() {
      return process.env.pic
    }
  }

}
</script>

<style scoped>

</style>
