<template>
  <div class="container mcontainer">
    <!-- Modal -->
    <div
      class="modal fade"
      id="LadderModal"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">نردبان</h5>
          </div>
          <div class="modal-body">
            <span>آیا از استفاده از نردبان مطمئن هستید؟</span>
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
              @click="ApplyLadder"
            >
              بله
            </button>
          </div>
        </div>
      </div>
    </div>
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

    <div
      class="row"
      v-if="
        this.$store.state.HeaderData.status === 1 ||
        this.$store.state.HeaderData.status === 4 ||
        !this.$store.state.HeaderData.status
      "
    >
      <div class="col-md-12">
        <div class="row">
          <div
            class="col-md-4 p-3 bg-white AdCard mx-md-3 mb-4 rounded main-shadow"
            style="cursor: pointer"
            v-for="item in UserAds"
            :key="item.adsId"
          >
            <div class="d-flex flex-wrap mb-2 gap-2 align-items-center">
              <a @click.prevent="EditAd(item.adsId)">
                <small
                  ><i
                    class="fas fa-edit text-primary"
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
                    class="fas fa-trash text-danger"
                    v-tooltip="{ content: 'حذف' }"
                  ></i>
                </small>
              </a>
              <a
                @click.prevent="SetAdId(item.adsId)"
                href="#LadderModal"
                data-bs-toggle="modal"
                ><small
                  ><i
                    class="fas fa-bars text-warning"
                    v-tooltip="{
                      content:
                        'با استفاده از نردبان,آگهی شما به بالای لیست آگهی ها منتقل میشود',
                    }"
                  ></i></small
              ></a>
            </div>
            <div class="row" @click="ChangeRoute(item.adsId)">
              <div class="col-md-6 pt-4">
                <div class="d-flex align-items-center gap-2">
                  <small>وضعیت </small>
                  <small v-if="item.status === 1" class="badge bg-success"
                    >منتشر شده</small
                  >
                  <small v-if="item.status === 2" class="badge bg-secondary"
                    >تایید نشده</small
                  >
                  <small v-if="item.status === 3" class="badge bg-danger"
                    >رد شده
                  </small>
                </div>

                <br />
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
                <div class="d-flex align-items-center gap-2">
                  <i
                    class="fas fa-clock text-muted"
                    style="font-size: 10px"
                  ></i>
                  <small class="text-muted" style="font-size: 10px">{{
                      new Date(item.createDate).toLocaleDateString('fa-IR')

                  }}</small>
                </div>
              </div>
              <div class="col-md-6 d-flex align-items-center justify-content-center justify-content-md-start">
                <img
                  v-if="item.photos.length > 0"
                  :src="'https://banooclubapi.simagar.com/'+ item.photos[0].base64"
                  class="img-fluid rounded image-custom-height"
                  alt=""
                />
                <p v-else class="small mt-2">این آگهی عکسی جهت نمایش ندارد</p>
              </div>
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

    <div
      v-else-if="this.$store.state.HeaderData.status === 2"
      class="row boxMainContent text-warning position-relative bg-white p-3"
    >
      <p class="text-center">
        صفحه این کاربر خصوصی است برای مشاهده آن درخواست دوستی ارسال کنید
      </p>
    </div>
    <div
      v-else-if="this.$store.state.HeaderData.status === 3"
      class="row boxMainContent text-warning position-relative bg-white p-3"
    >
      <p class="text-center">
        برای مشاهده صفحه این کاربر ابتدا وارد اکانت خود شوید
      </p>
    </div>
  </div>
</template>

<script>
export default {
  layout: "PoshtebamPlusLayout",
  name: "Advertises",
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
