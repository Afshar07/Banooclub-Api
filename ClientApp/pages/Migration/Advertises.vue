<template>
  <div class="container-fluid">
    <div
      class="row"
      v-if="
        this.$store.state.HeaderData.status === 1 ||
        this.$store.state.HeaderData.status === 4
      "
    >
      <div v-if="UserAds.length > 0" class="col-md-12">
        <div class="row">
          <div
            class="col-md-4 p-3"
            style="cursor: pointer"
            v-for="item in UserAds"
            :key="item.adsId"
          >
            <div class="row bg-white AdCard border rounded">
              <div class="col-md-6 pt-4">
                <nuxt-link
                  class="text-dark text-decoration-none"
                  :to="{
                    path: '/Migration/AdvertiseDetail',
                    query: { id: item.adsId },
                  }"
                  ><span>{{ item.title }}</span></nuxt-link
                >
                <br />
                <br />
                <small class="text-muted">{{ item.price }} تومان</small>
                <br />
                <div class="d-flex align-items-center gap-2">
                  <i class="fas fa-user text-muted" style="font-size: 12px"></i>
                  <small class="text-muted" style="font-size: 12px">{{
                    item.userInfo.name + " " + item.userInfo.familyName
                  }}</small>
                </div>
                <br />
                <div class="d-flex align-items-center gap-2">
                  <i
                    class="fas fa-clock text-muted"
                    style="font-size: 10px"
                  ></i>
                  <small class="text-muted" style="font-size: 10px">{{
                    item.createDate | moment("jYYYY/jMM/jDD HH:MM")
                  }}</small>
                </div>
              </div>
              <div class="col-md-6 d-flex align-items-center">
                <img
                  v-if="item.photos.length > 0"
                  :src="BaseUrl + item.photos[0].base64"
                  style="object-fit: contain"
                  class="rounded"
                  height="150px"
                  width="100%"
                  alt=""
                />
                <p v-else class="text-center">بدون عکس</p>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div v-else class="col-md-12">
        <p>این کاربر آگهی ثبت نکرده است</p>
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
  fetchOnServer() {
    return true;
  },
  async fetch() {
    try {
      const response = await this.$repositories.getAdsByUserId.getAdsByUserId(
        this.$route.query.userId
      );
      this.UserAds = response.data.ads;
    } catch (error) {
      console.log(error);
    }
  },
  data() {
    return {
      UserAds: "",
    };

    // this.$router.push({
    //   path: '/Migration/AdvertiseDetail',
    //   query: {
    //     id: adsId
    //   }
    // })
  },

  computed: {
    BaseUrl() {
      return process.env.pic;
    },
  },
};
</script>

<style scoped>
@media only screen and (min-width: 768px) {
  .AdCard:hover {
    box-shadow: 0px 8px 7px 0px rgba(0, 0, 0, 0.3);
    transform: scale(1.1);
    transition: 0.2s ease;
  }
}
.AdCard {
  transition: 0.2s ease;
}
</style>
