<template>
  <div class="row mb-4 d-none d-lg-block m-0 col-12 p-0">
<!--    <div class="col-12 text-end">-->
<!--      <span class="titleSection">دنبال میکنید</span>-->
<!--    </div>-->
    <div class="d-flex flex-column text-end p-0">
      <div @click="goToUserProfile(item.followingUserId)" class="contact-list tw-w-full" v-for="item in Following" :key="item.userId">
        <div class="d-flex flex-row justify-content-start align-items-center tw-w-full">
          <div class="tw-pr-2">
            <img
              v-if="item.userInfo.selfieFileData"
              class="friendPicture"
              :src="`https://pplusapi.simagar.com/${item.userInfo.selfieFileData}`"
              style="width: 35px;height: 35px;"

              alt=""
            />
            <img
              v-else
              class="friendPicture"
              src="../assets/images/defaultUser.png"
              style="width: 35px;height: 35px;"

              alt=""
            />
          </div>
          <div class="px-3 py-3">
            <div class="d-flex flex-column">
              <div class="friendName">
                {{ item.userInfo.name + " " + item.userInfo.familyName }}
              </div>
              <div class="friendLink" v-if="item.userInfo.bio !== undefined">
                {{ item.userInfo.bio }}
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
  name: "Following",
  data() {
    return {
      Following: [],
    };
  },
  methods: {
    getFollowing() {
      this.$axios
        .post(`Following/GetMyFollowings`, null, {})
        .then((response) => {
          if (response.status === 200) {
            this.Following = response.data;
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    goToUserProfile(userId) {
      if (userId === this.$auth.user.userId) {
        this.$router.push("/social");
      } else {
        this.$router.push({
          path: "/social/accountsetting/posts",
          query: { userId: userId },
        });
        this.$store.commit("SetSearchedUserId", userId);

        this.$axios
          .post(`Common/GetUserIndex?userId=${userId}`, null, {})
          .then((res) => {
            this.$store.commit("SetUserData", res.data);
          });
      }
    },
  },
  mounted() {
    this.getFollowing();
  },
};
</script>

<style scoped>
.contact-list :hover {
  background: #f0f2f5;
  cursor: pointer;
  border-radius: 10px;
}

.contact-list {
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-align: center;
  -ms-flex-align: center;
  align-items: center;
  padding: 8px 0;
  border-radius: 10px;
  /*padding-left: 6px;*/
}
.titleSection {
  /*border-bottom: 2px solid #088dcd;*/
  background: rgba(0, 0, 0, 0) none repeat scroll 0 0;
  color: #424242;
  display: inline-block;
  font-size: 1.25rem;
  font-weight: 600;
  margin-bottom: 30px;
  padding: 20px 0 10px;
  position: relative;
  text-transform: uppercase;
  width: max-content;
}
.SectionBox {
  margin: 0 auto;
  width: 80%;
  background: #fdfdfd;
  border: 1px solid #ede9e9;
  border-radius: 3px;
}

.titleSidebarSection {
  border-bottom: 2px solid #088dcd;
  background: rgba(0, 0, 0, 0) none repeat scroll 0 0;
  color: #424242;
  display: inline-block;
  font-size: 15px;
  margin-bottom: 30px;
  text-transform: uppercase;
}

.friendPicture {
  /*margin-bottom: 20px;*/
  /*max-width: 45px;*/
  /*min-width: 45px;*/
  vertical-align: middle;
  border-radius: 50%;
}

.friendLink {
  color: #088dcd;
  cursor: pointer;
  font-size: 12px;
}

.friendLink:hover {
  border-bottom: 1px solid #088dcd;
}

.friendName {
  line-height: 1.2;
  font-size: 16px;
  font-weight: 500;
  color: #2d2d2d;
  /*cursor: pointer;*/
}

/*.friendName:hover {*/
/*  color: #2fa7cd;*/
/*}*/
</style>
