<template>
  <div class="container mcontainer">
    <div class="row bg-white p-4 rounded-top">
        <div class="col-md-12">
          <div class="d-flex">
            <button class="btn ContactsSearchBtn">
              <i class="fas fa-search"></i>
            </button>
            <input
              v-model="searchKey"
              type="text"
              class="ContactsSearch"
              placeholder="جستجوی کاربران"
            />
          </div>
        </div>
      </div>
    <div
        class="row bg-white p-4 rounded-bottom main-shadow"
        style="height: 1000px; overflow-y: scroll"
        @scroll="handleScroll($event)"
      >
        <div
          class="col-md-12 mt-3 mx-auto"
          v-for="item in AllUsers"
          :key="item.userId"
          @click="ChangeRoute(item)"
        >
          <div class="row col-12 mx-auto SingleContact p-3">
            <div
              class="col-lg-2 col-12 mx-auto d-flex align-items-center justify-content-center justify-content-md-start"
            >
              <img
                v-if="item.PictureUrl"
                :src="BaseUrl + 'media/gallery/profile/' + item.PictureUrl"
                style="object-fit: cover"
                class="img-fluid ContactImg border mb-3 mx-auto"
                alt=""
              />
              <img
                v-else
                src="/defaultUser.png"
                class="img-fluid ContactImg border mb-3 mx-auto"
                alt=""
              />
            </div>
            <div class="col-lg-10 col-12">
              <div
                class="d-flex flex-md-row flex-column justify-content-lg-between"
              >
                <div
                  class="content-width d-flex flex-column justify-content-start justify-content-md-end text-center text-md-end"
                >
                  <h5 style="cursor: pointer">
                    <strong>{{ item.Name + " " + item.FamilyName }}</strong>
                    <small
                      v-if="item.Type === 4"
                      class="badge bg-primary text-white px-3"
                    >
                      <small style="font-size: 13px">کاربر تجربی</small>
                    </small>
                    <small
                      v-if="item.Type === 3"
                      class="badge bg-warning text-white px-3"
                    >
                      <small style="font-size: 13px">وکیل</small>
                    </small>
                  </h5>
                  <h6>
                    <small class="text-muted">{{ item.UserName }}@</small>
                  </h6>
                  <h6 v-if="item.Bio !== ''" class="text-truncate content-width">
                    <small class="text-muted">{{ item.Bio }}</small>
                  </h6>
                  <h6 v-else>
                    <small
                      class="text-muted"
                      style="max-width: 20rem; min-width: 20rem"
                    >این کاربر بیوگرافی ثبت نکرده است</small
                    >
                  </h6>
                </div>
                <div class="d-flex flex-wrap gap-3 justify-content-between">
                  <div class="d-flex flex-column align-items-center">
                    <small>{{ item.FollowersCount }}</small>
                    <small class="text-muted">دنبال کنندگان</small>
                  </div>

                  <div class="d-flex flex-column align-items-center">
                    <small>{{ item.FollowingsCount }}</small>
                    <small class="text-muted">دنبال شوندگان</small>
                  </div>
                </div>
                <div
                  class="d-flex flex-column mt-4 mt-md-0 justify-content-center"
                >
                  <button
                    v-if="!item.IsFollowing"
                    class="btn btn-primary w-100"
                    @click.prevent="FollowReq(item.UserId)"
                  >
                    دنبال کردن
                  </button>
                  <p v-else class="w-100 mx-3">دنبال شده</p>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="col-md-12">
          <div
            class="d-flex justify-content-center align-items-center p-4 rounded-circle"
          >
            <i class="fas fa-plus"></i>
          </div>
        </div>
      </div>
  </div>
</template>

<script>
export default {
  name: "Contacts",
  layout: "PoshtebamPlusLayout",
  data() {
    return {
      searchKey: null,
      AllUsers: [],
      lastUserId: 0,
      Atbottom: false,
    };
  },
  fetchOnServer() {
    return true;
  },
  async fetch() {
    const response =
      await this.$repositories.getAllUsersForUser.getAllUsersForUser(
        this.lastUserId,
        this.searchKey
      );
    if (this.lastUserId != 0) {
      response.data.forEach((item) => {
        this.AllUsers.push(item);
      });
    } else {
      this.AllUsers = response.data;
    }
  },

  computed: {
    BaseUrl() {
      return process.env.pic;
    },
  },
  watch: {
    searchKey: function (val, oldVal) {
      if (!val || val == "") {
        this.searchKey = null;
      }
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      });
      this.$fetch();
      this.$nextTick(() => {
        this.$nuxt.$loading.finish();
      });
    },
    Atbottom: function (val, oldVal) {
      if (val) {
        if (this.lastUserId != this.AllUsers[this.AllUsers.length - 1].UserId) {
          this.lastUserId = this.AllUsers[this.AllUsers.length - 1].UserId;
          this.$fetch();
        }
      }
    },
  },
  methods: {
    handleScroll: function ({
      target: { scrollTop, clientHeight, scrollHeight },
    }) {
      if (scrollTop + clientHeight >= scrollHeight) {
        this.Atbottom = true;
      } else {
        this.Atbottom = false;
      }
    },

    async ChangeRoute(item) {
      console.log(item);
      this.$store.commit("SetSearchedUserId", item.UserId);
      // const response = await this.$repositories.getUserIndex.getUserIndex(
      //   item.UserId
      // );
      // this.$store.commit("SetUserData", response.data);
      this.$router.push({
        path: `/user/${item.UserId}/posts`,
      });
      this.searchKey = null;
    },
    async FollowReq(id) {
      try {
        const response =
          await this.$repositories.createAFollowRequest.createAFollowRequest(
            id
          );
        if (response.data) {
          this.$toast.success("درخواست دوستی شما ارسال شد");
        } else {
          this.$toast.error("عملیات قابل اجرا نیست");
        }
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
.ContactsSearch {
  width: 100%;
  border: solid 1px #ccc;
  border-radius: 50px;
  border-right: none;
  border-bottom-right-radius: 0px;
  border-top-right-radius: 0px;
}
.SingleContact {
  transition: 0.2s ease;
  border-radius: 5px;
}
.SingleContact:hover {
  cursor: pointer;
  transform: scale(1.01);

  box-shadow: 1px 1px 1px rgba(0, 0, 0, 0.15), 0 0 1px rgba(0, 0, 0, 0.15);
}

.ContactImg {
  width: 100px;
  height: 100px;
  border-radius: 50px;
}

.ContactsSearchBtn {
  border: solid 1px #ccc;
  border-radius: 50px;
  border-left: none;
  border-bottom-left-radius: 0px;
  border-top-left-radius: 0px;
}

@media only screen and (max-width: 768px) {
  .content-width {
    min-width: 100%;
    max-width: 100%;
  }
}
@media only screen and (min-width: 769px) {
  .content-width {
    min-width: 20rem;
    max-width: 20rem;
  }
}
</style>
