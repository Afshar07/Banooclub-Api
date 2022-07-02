<template>
  <div class="container-fluid px-0">
    <div class="row boxMainContent">
      <div class="col-12">
        <input
          type="text"
          class="w-100 border rounded py-2 mb-5"
          v-model.trim="userSearch"
          placeholder="جستجوی کاربران ..."
        />
      </div>
      <div class="col-12" v-if="!(userData === null)">
        <div
          class="d-flex gap-5 styleCursor"
          v-for="item in userData"
          :key="item"
          @click="goToUserProfile(item.userId)"
        >
          <div class="">
            <img
              class="img-fluid myPicture"
              :src="`https://BanooClubapi.simagar.com/${item.selfieFileData}`"
              alt=""
            />
          </div>
          <div class="">
            <div class="d-flex flex-column">
              <div class="fs-5">{{ item.name + " " + item.familyName }}</div>
              <div
                v-if="item.userSetting !== undefined"
                class="fs-6 fw-light text-muted"
              >
                {{ item.userSetting.bio }}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "SearchUser",
  layout: "BanooClubLayout",
  data() {
    return {
      userSearch: "",
      showSearchUser: false,
      userData: [],
    };
  },
  methods: {
    seeUserProfile(userId) {
      this.$router.push({
        path: "/social/accountsetting/myposts",
        query: { UserId: userId },
      });
      this.$store.commit("SetSearchedUserId", userId);
    },
    goToUserProfile(userId) {
      this.$router.push({
        path: "/social/accountsetting/posts",
        query: { userId: userId },
      });
      this.$store.commit("SetSearchedUserId", userId);
    },
  },
  watch: {
    userSearch: async function (val, oldVal) {
      try {
        const response =
          await this.$repositories.searchUserByName.searchUserByName(val);
        this.userData = response.data;
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>

<style scoped>
.boxMainContent {
  background: #fdfdfd none repeat scroll 0 0;
  border: 1px solid #ede9e9;
  border-radius: 3px;
  display: inline-block;
  width: 100%;
  margin-bottom: 20px;
  padding: 25px;
}

input::placeholder {
  font-size: 16px;
  color: #cccccc;
}

.styleCursor {
  cursor: pointer;
}

.myPicture {
  margin-bottom: 20px;
  max-width: 45px;
  min-width: 45px;
  vertical-align: middle;
  border-radius: 50%;
  cursor: pointer;
}
</style>
