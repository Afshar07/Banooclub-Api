<template>
  <div :class="UserAds===''?'loading-skeleton':''" class="NewBg  mcontainer">
    <!-- Modal -->
    <div
      class="modal fade"
      id="DeleteModal"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel2">حذف آگهی</h5>
          </div>
          <div class="modal-body">
            <span>آیااز حذف آین آگهی اطمینان دارید؟</span>
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-danger"
              data-bs-dismiss="modal"
            >
              خیر
            </button>
            <button
              type="button"
              class="btn btn-success text-white"
              data-bs-dismiss="modal"
              @click="DeleteAd"
            >
              بله
            </button>
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-md-12 my-3">
        <div class="tw-w-full bg-white rounded tw-shadow p-3 d-flex align-items-center justify-content-between gap-3  rounded ">
          <div class="d-flex align-items-center gap-2">
            <img src="/girl-icon-ads.png" class="tw-w-[7rem] tw-h-[7rem] tw-object-contain" alt="">
            <div class="d-flex align-items-center flex-column">
              <h1 class="text-purple h6" style="font-weight: bolder!important;">آگهی های من</h1>
              <strong class="text-pink">My AdvertiseMents</strong>
            </div>
          </div>

          <button v-tooltip="{content:'ثبت آگهی'}" @click="$router.push('/Migration/CreateAdvertise')" class="btn AddReplyBtn text-white">
            <PlusIcon fill="#ff6f9e" style="width: 30px; height: 30px;"/>
          </button>
        </div>
      </div>
    </div>
    <div class="row bg-white rounded tw-shadow p-3 my-3">
      <div    v-for="item in UserAds" :key="item.adsId" class="col-md-4" >
        <div
          class=" p-3  bg-white AdCard px-2 mb-4 rounded main-shadow"
          style="cursor: pointer;height: 300px"

        >
          <div class="d-flex align-items-center justify-content-between">
            <div class="d-flex flex-wrap mb-2 gap-2 align-items-center">
              <a @click.prevent="EditAd(item.adsId)">
                <small
                ><i
                  class="fas fa-edit tw-text-[#399fff]"
                  v-tooltip="{ content: 'ویرایش' }"
                ></i
                ></small>
              </a>
              <a
                @click.prevent="SetAdId(item.adsId)"
                href="#DeleteModal"
                data-bs-toggle="modal"
              >
                <small>
                  <i
                    class="fas fa-trash tw-text-[#ff408c]"
                    v-tooltip="{ content: 'حذف' }"
                  ></i>
                </small>
              </a>
              <nuxt-link v-tooltip="{content:'ارتقا'}" :to="`/Migration/UpgradeAd/${item.adsId}`">
                <small><i class="fas fa-rocket tw-text-[#39ff7a] "></i></small>
              </nuxt-link>
            </div>
            <div   class=" tw-stack  tw-right-0 tw-cursor-pointer  hover:tw-flex hover:tw-flex-col hover:tw-gap-2 tw-transition ">
              <div v-if="item.planTypes.includes(2)"
                   class="tw--top-3  tw-bg-[#ffe175] tw-font-medium tw-px-2 tw-py-1 tw-shadow tw-left-2 tw-rounded-full tw-text-[#2533e1]">
                پیشنهادات
              </div>
              <div v-if="item.planTypes.includes(1)"
                   class="tw--top-3  tw-bg-[#ffe175] tw-font-medium tw-px-2 tw-py-1 tw-shadow tw-left-2 tw-rounded-full tw-text-[#2533e1]">
                آتیش زدم
              </div>
              <div v-if="item.planTypes.includes(4)"
                   class="tw--top-3  tw-bg-[#ffe175] tw-font-medium tw-px-2 tw-py-1 tw-shadow tw-left-2 tw-rounded-full tw-text-[#2533e1]">
                جدید ترین ها
              </div>
              <div v-if="item.planTypes.includes(3)"
                   class="tw--top-3  tw-bg-[#ffe175] tw-font-medium tw-px-2 tw-py-1 tw-shadow tw-left-2 tw-rounded-full tw-text-[#2533e1]">
                برترین ها
              </div>
            </div>
          </div>

          <div class="row" @click="ChangeRoute(item.adsId)">
            <div class="col-md-6 pt-4">
              <div class="d-flex align-items-center gap-2">
                <small>وضعیت :  </small>
                <small v-if="item.status === 1" class="badge bg-LightGreen"
                >منتشر شده</small
                >
                <small v-if="item.status === 2" class="badge bg-secondary"
                >تایید نشده</small
                >
                <small v-if="item.status === 3" class="badge bg-danger"
                >رد شده
                </small>
              </div>
              <nuxt-link
                class="text-dark text-decoration-none"
                :to="{
                    path: `/Migration/AdvertiseDetail/${item.adsId}`,

                  }"
              >
                <span>{{ item.title }}</span></nuxt-link
              >
              <br />
              <br />
              <div class="d-flex align-items-center gap-2">
                <h6>
                  {{ item.description.substr(0, 10) }}
                  <span class="text-secondary">...</span>
                </h6>
              </div>

              <br />
              <small class="text-muted"
              >{{ new Intl.NumberFormat().format(item.price) }} تومان</small
              >
              <br />
              <div class="d-flex align-items-center justify-content-between">
                <div class="d-flex align-items-center gap-2">
                  <i
                    class="fas fa-clock text-muted "
                    style="font-size: 10px"
                  ></i>
                  <small class="text-muted mt-1" style="font-size: 10px">{{
                      new Date(item.createDate).toLocaleDateString('fa-IR')

                    }}</small>
                </div>
                <div v-if="item.exchangeability" class="my-3">
                  <span class="badge pill bg-info text-white   ">قابل معاوضه</span>
                </div>

              </div>

            </div>
            <div class="col-md-6 d-flex align-items-center justify-content-center justify-content-md-start">
              <img
                v-if="item.photos.length > 0"
                :src="'https://banooclubapi.simagar.com/'+ item.photos[0].base64"
                class="tw-w-full tw-h-[7rem] rounded tw-object-cover"
                alt=""
              />
              <p v-else class="small mt-2">این آگهی عکسی جهت نمایش ندارد</p>
            </div>
          </div>
        </div>
      </div>
      <div
        v-if="UserAds.length === 0"
        class="col-md-12 bg-white d-flex justify-content-center p-4"
      >
        <strong class="text-warning">شما هیچ آگهی ندارید.</strong>
        <nuxt-link to="/Migration/CreateAdvertise" class="text-decoration-none"
        ><small>ثبت آگهی جدید</small></nuxt-link
        >
      </div>
    </div>

  </div>
</template>

<script>
import PlusIcon from "@/components/Icons/PlusIcon";
export default {
  layout: "PoshtebamPlusLayout",
  name: "Advertises",
  components:{PlusIcon},
  data() {
    return {
      UserAds: "",
      SelectedAdId: 0,
    };

    // this.$router.push({
    //   path: '/Migration/AdvertiseDetail',
    //   query: {
    //     id: adsId
    //   }
    // })
  },

  mounted() {
    this.GetUserAds(this.$route.query.userId);
  },

  computed: {
    BaseUrl() {
      return process.env.pic;
    },
  },
  head() {
    return {

      title: 'آگهی های من',
      meta: [
        {
          hid: "description",
          name: "description",
          content:'آگهی های من',
        },
      ],
    };
  },
  methods: {
    EditAd(id) {
      this.$router.push({
        path: "/Migration/EditAdvertise",
        query: { adId: id },
      });
    },

    ApplyLadder() {
      this.$axios
        .post(`Ads/Ladder?adsId=${this.SelectedAdId}`, null, {})
        .then((res) => {
          this.$toast.success("آگهی شما به بالای لیست آگهی ها انتقال یافت");
        });
    },
    DeleteAd() {
      this.$axios
        .post(`Ads/Delete?id=${this.SelectedAdId}`, null, {})
        .then((res) => {
          this.$toast.success("آگهی با موفقیت حذف شد");
          this.GetUserAds();
        });
    },

    SetAdId(id) {
      this.SelectedAdId = id;
    },
    GetUserAds() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      });
      this.$axios
        .post("Ads/GetMyAds?pageNumber=1&count=50", null, {})
        .then((res) => {
          this.UserAds = res.data.ads;
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
        });
    },
    ChangeRoute(id) {
      this.$router.push({
        path: `/Migration/AdvertiseDetail/${id}`,
      });
    },
  },
};
</script>

<style scoped>
@media only screen and (min-width: 769px) {
  .AdCard:hover {
    box-shadow: 0px 8px 7px 0px rgba(0, 0, 0, 0.3);
    transform: scale(1.1);
    transition: 0.2s ease;
  }
}

.AdCard {
  transition: 0.2s ease;
}
@media only screen and (max-width: 768px) {
  .image-custom-height {
    min-height: 13rem;
    max-height: 13rem;
  }
}
</style>
