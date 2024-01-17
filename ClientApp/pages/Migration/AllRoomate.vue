<template>
  <div class="container mcontainer">
    <div class="row">
      <div class="col-md-12 p-4">
        <div class="d-flex align-items-center gap-2">
          <h4>
            <i class="fas fa-bed"></i>
          </h4>

          <h4>
            <strong> همه آگهی های هم خانه </strong>
          </h4>
        </div>
      </div>
      <div
        v-if="
          !$fetchState.pending &&
          filteredRoommateAds &&
          filteredRoommateAds.length > 0
        "
        class="row"
      >
        <div
          class="col-md-6 "
          style="min-height: max-content"
          v-for="item in filteredRoommateAds"
          :key="item.roomateId"
        >
          <div
            class="card RoomateCard mb-3 custom-roommate-card-height"
            @click="ChangeRoute(item.userId)"
            style="max-width: 540px"
          >
            <div class="row g-0">
              <div class="col-md-8">
                <div class="card-body">
                  <div class="d-flex align-items-center gap-2">
                    <small class="card-title">تاریخ پذیرش:</small>
                    <small class="card-title text-secondary">{{
                        item.receptionDate | moment("jYYYY-jMM-jDD ")
                      }}</small>
                  </div>

                  <div class="d-flex align-items-center gap-2">
                    <small class="card-title"> اجاره روزانه: </small>
                    <small class="card-title text-secondary"
                    >{{
                        new Intl.NumberFormat().format(item.dailyRent)
                      }}
                      تومان</small
                    >
                  </div>
                  <div class="d-flex align-items-center gap-2">
                    <small class="card-title"> مبلغ رهن: </small>
                    <small class="card-title text-secondary"
                    >{{
                        new Intl.NumberFormat().format(item.mortgage)
                      }}
                      تومان</small
                    >
                  </div>
                </div>
              </div>
              <div
                class="col-md-4 d-flex align-items-center justify-content-start m-0 h-100"
              >
                <img
                  v-if="item.photos.length > 0"
                  :src="BaseUrl + item.photos[0].base64"
                  class="img-fluid rounded-start my-auto custom-roommate-card-img-height w-100"
                  alt="..."
                />
                <img
                  v-else
                  src="/default-home.jpg"
                  class="img-fluid rounded-start my-auto custom-roommate-card-img-height w-100"
                  alt=""
                />
              </div>
            </div>
          </div>
        </div>
      </div>
      <div
        v-else-if="
          !$fetchState.pending &&
          (!filteredRoommateAds || filteredRoommateAds.length == 0)
        "
      >
        <h5 class="text-center text-warning">هیچ آگهی برای نمایش وجود ندارد!</h5>
      </div>
    </div>

  </div>
</template>

<script>
export default {
  name: "AllRoomate",
  layout: "PoshtebamPlusLayout",
  fetchOnServer() {
    return true;
  },
  async fetch() {
    const response = await this.$repositories.getAllRoommates.getAllRoommates();

    this.Allroomate = response.data;
  },
  data() {
    return {
      Allroomate: [],
    };
  },
  head() {
    return {

      title: 'همه آگهی های هم خانه',
      meta: [
        {
          hid: "description",
          name: "description",
          content:'همه آگهی های هم خانه',
        },
      ],
    };
  },
  computed: {
    BaseUrl() {
      return process.env.pic;
    },
    filteredRoommateAds() {
      return this.Allroomate.filter((item) => item.isPrivate != true);
    },
  },

  methods: {
    async ChangeRoute(id) {
      try {
        this.$store.commit("SetSearchedUserId", id);
        const response = await this.$repositories.getUserIndex.getUserIndex(id);
        this.$store.commit("SetUserData", response.data);
        this.$router.push({
          path: "/Migration/RoomMateInfo",
          query: { userId: id },
        });
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>

<style scoped>
@media (min-width: 1024px) {
  .mcontainer {
    max-width: 1000px;
    padding: 30px 0px;
    margin-left: auto;
    margin-right: auto;
  }
}
@media (max-width: 1024px) {
  .mcontainer {
    max-width: 1000px;
    padding: 25px 0px;
    margin-left: auto;
    margin-right: auto;
  }
}
.RoomateCard {
  cursor: pointer;
  transition: 0.3s ease;
}
.RoomateCard:hover {
  transform: scale(1.01);
  box-shadow: 0px 8px 7px 0px rgba(0, 0, 0, 0.3);
  transition: 0.3s ease;
}
@media only screen and (min-width: 769px) {
  .custom-roommate-card-height {
    min-height: 7rem;
    max-height: 7rem;
  }
  .custom-roommate-card-img-height {
    min-height: 6.9rem;
    max-height: 6.9rem;
  }
}
</style>
