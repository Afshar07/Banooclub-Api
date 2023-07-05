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
                    <small class="text-muted">مشاوره موفق : 213456</small>
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
              <div class="mt-2 px-2">
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
            <!--            <button v-if="canIComment" class="bg-purple text-white rounded shadow p-1" data-bs-target="#createComment"-->
            <!--                    data-bs-toggle="modal">ثبت نظر-->
            <!--            </button>-->
            <!--            <button v-else class="bg-secondary text-white rounded shadow p-1" >برای ثبت نظر باید از این مشاور مشاوره گرفته باشید-->
            <!--            </button>-->
          </div>
          <div class=" row">
            <div v-for="i in 6" class="col-md-12 my-3">
              <div class="d-flex align-items-center justify-content-between">
                <div class="d-flex align-items-center gap-3">
                  <div
                    class="rounded-circle   d-flex flex-column justify-content-start align-items-center"
                    style="height: 100%;">
                    <img alt="" src="/defaultUser.png" style="object-fit: contain;width: 4rem;height: 4rem">
                  </div>
                  <div class="d-flex flex-column align-items-start">
                    <span>امیرحسین سلیمانی</span>
                    <div class="d-flex gap-2 align-items-center">
                      <small class=" text-muted">1402/02/05</small>
                      <small class=" text-muted ps-2">19:49</small>
                    </div>
                  </div>
                </div>
                <span class=" py-1 px-2 rounded d-flex gap-2 align-items-center " style="background-color: #ffdb66">
                  <LazyStarIcon style="height: 15px;width: 15px;fill:white"></LazyStarIcon>
                    <small class="text-white">4.6</small>
                  </span>
              </div>
              <div class="px-2 mt-3">
                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Autem blanditiis dolore eum explicabo
                  facere nemo odio, rem sed temporibus ut.</p>
              </div>
            </div>
            <div class="col-md-12 d-flex align-items-center justify-content-center">
              <button class="p-2 rounded bg-pink text-white" type="button">مشاهده نظرات بیشتر
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
            <nuxt-link :to="`/consultation/PrePay/${selectedPrice.type}/${consultantInfo.id}`" class="btn w-100 text-white " style="background-color:#e7b0fe ">
              ادامه و پرداخت
            </nuxt-link>
          </div>


        </div>
      </div>
    </div>

    <!--    &lt;!&ndash; Modal &ndash;&gt;-->
    <!--    <div id="createComment" aria-hidden="true" aria-labelledby="exampleModalLabel" class="modal fade" tabindex="-1">-->
    <!--      <div class="modal-dialog">-->
    <!--        <div class="modal-content">-->
    <!--          <div class="modal-header">-->
    <!--            <h1 id="exampleModalLabel" class="modal-title fs-5">ثبت نظر</h1>-->
    <!--          </div>-->
    <!--          <div class="modal-body">-->
    <!--            <div class="w-100 row">-->
    <!--              <div class="col-md-12">-->
    <!--                <small class="text-secondary">امتیاز</small>-->
    <!--                <client-only>-->
    <!--                  <vue-star-rating v-model="commentObject.rate" :read-only="false" :show-rating="false"-->
    <!--                                   :star-size="20" ></vue-star-rating>-->
    <!--                </client-only>-->
    <!--              </div>-->
    <!--              <div class="col-md-12">-->
    <!--                <small class="text-secondary">متن نظر</small>-->
    <!--                <textarea v-model="commentObject.description" class="with-border FormInputs"></textarea>-->
    <!--              </div>-->
    <!--            </div>-->
    <!--          </div>-->
    <!--          <div class="modal-footer">-->
    <!--            <button class="btn btn-secondary" data-bs-dismiss="modal" type="button">بستن</button>-->
    <!--            <button class="bg-pink text-white p-2 rounded " type="button">ثبت</button>-->
    <!--          </div>-->
    <!--        </div>-->
    <!--      </div>-->
    <!--    </div>-->
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
      selectedPrice: null,
      canIComment: false,
      commentObject: {
        id: this.$route.params.Consultant,
        rate: 0,
        description: ""
      },
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
      this.canComment()
    ])
  },

  computed: {
    baseUrl() {
      return process.env.pic
    }
  },
  methods: {
    async canComment() {
      try {
        const res = await this.$repositories.canComment.setParams({
          id: this.$route.params.Consultant
        })
        this.canIComment = res.data
      } catch (e) {
        console.log(e)
      } finally {

      }
    },
    setSelectedPrice(item) {
      this.selectedPrice = item
    },
    async getConsultant() {
      try {
        const res = await this.$repositories.getConsultantById.setParams({
          id: this.$route.params.Consultant
        })
        this.consultantInfo = res.data.data
      } catch (e) {
        console.log(e)
      } finally {

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
