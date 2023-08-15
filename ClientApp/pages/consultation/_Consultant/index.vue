<template>
  <div v-if="consultantInfo" class="container NewBg position-relative  px-2">
    <div class="row ">
      <div v-if="consultantInfo.id === $auth.user.consultantId"
           class="col-12 d-flex align-items-center justify-content-between">
        <div class="d-flex align-items-center gap-2">
          <small>وضعیت مشاور : </small>
          <small v-if="consultantInfo.lastRequestStatus===1" class="badge bg-success text-white">فعال</small>
          <small v-if="consultantInfo.lastRequestStatus===2" class="badge bg-danger text-white">غیرفعال</small>
          <small v-if="consultantInfo.lastRequestStatus===0" class="badge bg-secondary text-white">در انتظار
            تایید</small>
        </div>
        <!--        <button v-if="!isEditingPage" class="p-2 rounded bg-purple text-white tw-cursor-pointer"-->
        <!--                @click="isEditingPage = true">ویرایش-->
        <!--        </button>-->
        <!--        <button v-if="isEditingPage" class="p-2 rounded bg-pink text-white tw-cursor-pointer"-->
        <!--                @click="isEditingPage = false">ثبت-->
        <!--        </button>-->

      </div>

      <div class="col-xl-9 p-4">
        <div class="bg-white shadow row rounded-3 ">
          <div class="rounded-3" style="background-color: #e7b0fe;height: 7rem"></div>
          <div class="row p-3">
            <div class="col-md-6 ">
              <div class="d-flex flex-xl-row flex-column gap-3 h-100 align-items-start">
                <div
                  class="rounded-circle position-relative  d-flex flex-column justify-content-start align-items-center"
                  style="height: 100%;bottom: 5rem">
                  <img :src="baseUrl +consultantInfo.upi" alt="" class="rounded-circle"
                       style="object-fit: cover;width: 5rem;height: 5rem">
                </div>
                <div class="d-flex flex-column h-100 justify-content-between align-items-start">
                  <div class="d-flex flex-column justify-content-start align-items-start">
                    <input v-if="isEditingPage" class="form-control-consultant  with-border mb-2" placeholder="نام"
                           type="text">
                    <h5 v-else class="tw-font-bold">{{ consultantInfo.name + ' ' + consultantInfo.lName }}</h5>
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
                    <span class=" py-1 px-2 rounded d-flex  gap-2 align-items-center "
                          style="background-color: #71ffe2">
                  <LazyCommentIcon style="height: 15px;width: 15px;fill:white"></LazyCommentIcon>
                    <small class="text-white">{{ consultantInfo.commentCount }} دیدگاه</small>
                  </span>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-md-6">
              <div class="d-flex flex-column justify-content-between h-100 ">
                <div class="d-flex flex-column align-items-start gap-2">
                  <div class="d-flex gap-1">
                    <input v-if="isEditingPage" class="form-control-consultant with-border mb-2"
                           placeholder="شماره شناسایی"
                           type="text">
                    <small v-else class="text-muted">شماره شناسایی : {{ consultantInfo.medicalSystemNumber }}</small>
                  </div>

                  <div class="d-flex gap-1">
                    <small v-if="consultantInfo&&consultantInfo.successCount" class="text-muted">مشاوره موفق :
                      {{ consultantInfo.successCount }}</small>
                  </div>
                </div>

              </div>
            </div>
          </div>
        </div>
        <div class="row ">
          <div class="col-md-6  p-3 pe-0 ps-lg-3 ps-0">
            <div class="bg-white shadow d-flex flex-column rounded-3 py-3" style="min-height: 20rem">
              <div class="w-100 d-flex h-100 px-2" style="border-right: solid 3px #e7b0fe">
                <strong>درباره من</strong>
              </div>
              <div style="overflow-wrap: anywhere!important"  class="mt-2 px-2">
                <textarea v-if="isEditingPage" class="form-control with-border" placeholder="درباره من "
                          rows="10"></textarea>
                <div v-else v-html="consultantInfo.des"></div>
              </div>

            </div>
          </div>
          <div class="col-md-6 p-3">
            <div class="bg-white shadow row rounded-3 py-3" style="min-height: 20rem">
              <div class="w-100 d-flex h-100 " style="border-right: solid 3px #e7b0fe">
                <strong>ویدیو معرفی من</strong>
              </div>
              <div class="mt-2 d-flex align-items-center justify-content-center">
                <div v-if="isEditingPage" class="d-flex flex-column gap-2 align-items-center justify-content-center">
                  <img alt="" src="/video-camera.svg">
                  <small class="text-pink " style="cursor: pointer!important">آپلود</small>
                </div>
                <div v-else class="    ">
                  <video :src="baseUrl + consultantInfo.v" controls></video>
                </div>
              </div>
            </div>
          </div>

        </div>
        <div class="bg-white shadow row rounded-3 py-3">
          <div class="w-100 d-flex align-items-center justify-content-between h-100 "
               style="border-right: solid 3px #e7b0fe">
            <strong>نظرات کاربران</strong>
          </div>
          <div class=" row">
            <template v-if="consultantComments">
              <div v-for="(item,idx) in consultantComments" :key="idx" class="col-md-12 my-3">
                <div class="d-flex align-items-center justify-content-between">
                  <div class="d-flex align-items-center gap-3">
                    <div
                      class="rounded-circle   d-flex flex-column justify-content-start align-items-center"
                      style="height: 100%;">
                      <img v-if="item.userPic" :src="baseUrl + item.userPic" alt=""
                           style="object-fit: contain;width: 4rem;height: 4rem">
                      <img v-else alt="" src="/defaultUser.png" style="object-fit: contain;width: 4rem;height: 4rem">
                    </div>
                    <div class="d-flex flex-column align-items-start">
                      <span>{{ item.userFullname }}</span>
                      <div class="d-flex gap-2 align-items-center">
                        <small class=" text-muted">{{ new Date(item.createDate).toLocaleDateString('fa-IR') }}</small>
                        <small class=" text-muted ps-2">{{
                            new Date(item.createDate).toLocaleTimeString('fa-IR')
                          }}</small>
                      </div>
                    </div>
                  </div>
                  <span class=" py-1 px-2 rounded d-flex gap-2 align-items-center " style="background-color: #ffdb66">
                  <LazyStarIcon style="height: 15px;width: 15px;fill:white"></LazyStarIcon>
                    <small class="text-white">{{ item.rate }}</small>
                  </span>
                </div>
                <div style="overflow-wrap: anywhere" class="px-2 mt-3">
                  <p  style="overflow-wrap: anywhere">{{ item.description }}</p>
                </div>
              </div>

            </template>
            <div v-else-if="!consultantComments"
                 class="d-flex flex-column w-100 align-items-center bg-white justify-content-center shadow rounded p-3 ">
              <LazySadFaceIcon style="width: 60px;height: 60px;fill: #6c757d"></LazySadFaceIcon>
              <span class="text-secondary">موردی یافت نشد</span>
            </div>
            <div v-if="totalPages.length>1 && page<totalCount"
                 class="col-md-12 d-flex align-items-center justify-content-center">
              <button class="p-2 rounded bg-pink text-white" type="button" @click="changePageComment">مشاهده نظرات بیشتر
              </button>
            </div>
          </div>
        </div>
      </div>
      <div class="col-xl-3 p-4   overflow-scroll ">
        <div class="bg-white shadow sticky-lg-top   row rounded-3 p-2" style="z-index: 11">
          <div v-for="(item,idx) in consultantInfo.prices" :key="idx"
               :class="{'activePriceClass':selectedPrice && selectedPrice.type===item.type}"
               class="col-md-12   gap-2  p-2   "
               style="cursor: pointer"
               @click="setSelectedPrice(item)">
            <div class="rounded  p-3 " style="border:solid 1px #e7b0fe;">
              <div class=" d-flex align-items-center justify-content-between">
                <div class="d-flex align-items-center gap-2">
                  <div class="p-1 rounded " style="border:solid 1px #e7b0fe;background-color: #e7b0fe">
                    <LazyPhoneIcon v-if="item.type===1" style="width: 20px;height: 20px;fill:#d071ff "></LazyPhoneIcon>
                    <LazySmartPhoneIcon v-if="item.type===2"
                                        style="width: 20px;height: 20px;fill:#d071ff "></LazySmartPhoneIcon>
                    <LazyVideoIcon v-if="item.type===3" style="width: 20px;height: 20px;fill:#d071ff "></LazyVideoIcon>
                  </div>
                  <small>{{ pricesObjects[item.type] }}</small>
                </div>
                <span style="color: #e7b0fe">{{
                    Intl.NumberFormat('fa-IR').format(item.price)
                  }} <small>تومان</small></span>
              </div>
              <div class="d-flex gap-2 flex-column mt-2 ">
              <span v-if="item.type===2" class="text-muted d-flex align-items-center gap-1">
                <LazyClockIcon style="width: 15px;height: 15px;fill: #6c757d!important"></LazyClockIcon>
                <small style="font-size: 12px">
                پاسخ دهی فوری
                </small>
              </span>
              </div>
            </div>
          </div>
          <div v-if="selectedPrice" class="col-md-12 px-2">
            <nuxt-link :to="`/consultation/PrePay/${selectedPrice.type}/${consultantInfo.id}`"
                       class="btn w-100 text-white " style="background-color:#e7b0fe ">
              ادامه و پرداخت
            </nuxt-link>
          </div>


        </div>
      </div>
    </div>


  </div>
</template>

<script>
export default {
  layout: "PoshtebamPlusLayout",
  head() {
    return {
      title: 'مشاور'
    }
  },
  data() {
    return {
      isEditingPage: false,
      consultantInfo: null,
      totalCount: 0,
      page: 1,
      consultantComments: [],
      totalPages: [],
      selectedPrice: null,
      pricesObjects: {
        1: 'مشاوره تلفنی',
        2: 'مشاوره تلفنی فوری',
        3: 'مشاوره ویدیویی'
      }
    }
  },
  async fetch() {
    await Promise.all([
      this.getConsultant(),
      this.getConsultantComments()
    ])
  },

  computed: {
    baseUrl() {
      return process.env.pic
    }
  },
  fetchOnServer:false,

  methods: {
    changePageComment() {
      this.page++
      this.getConsultantComments()
    },
    async getConsultantComments() {
      try {
        const res = await this.$repositories.getConsultantComment.setParams({
          id: this.$route.params.Consultant,
          pageNumber: this.page,
          take: 10
        })
        res.data.data.forEach((item) => {
          this.consultantComments.push(item)
        })
        this.totalCount = res.data.total
        const result = Math.ceil(res.data.total / 10)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
        }

      }
      catch (e) {
        console.log(e)
      }
      finally {

      }
    },

    setSelectedPrice(item) {
      this.selectedPrice = item
    },
    async getConsultant() {
      try {
        this.$nextTick(()=>{
          this.$nuxt.$loading.start()
        })
        const res = await this.$repositories.getConsultantById.setParams({
          id: this.$route.params.Consultant
        })
        this.consultantInfo = res.data.data
      }
      catch (e) {
        console.log(e)
      }
      finally {
        this.$nuxt.$loading.finish()
        this.$nuxt.loading = false

      }
    }
  },
  name: "index"
}
</script>

<style scoped>
.form-control-consultant {
  height: 30px;
  line-height: 19px;
  padding: 0px 10px;
  outline: none;
  font-size: 15px;
  color: #808080;
  max-width: 100%;
  width: 100%;
  -webkit-box-sizing: border-box;
  box-sizing: border-box;
  display: block;
  background-color: #fff;
  font-weight: 400;
  opacity: 1;
  border-radius: 6px;
  border: none;
}

.activePriceClass {
  background-color: #faf7ff;
}
</style>
