<template>
  <div class="row mb-4 d-lg-block m-0 col-12 p-0">
<!--    <div class="col-12 text-end">-->
<!--      <span class="titleSection">دنبال کنندگان</span>-->
<!--    </div>-->
<!--    <div class="col-12 p-0">-->
<!--      <input-->
<!--        class="searchFriend"-->
<!--        type="text"-->
<!--        placeholder="جستجو"-->
<!--        v-model.trim="friendNameFilter"-->
<!--      />-->
<!--    </div>-->
    <div class="col-12 p-0" v-if="friendListFilter.length > 0">
      <transition-group name="list">
        <div class="contact-list tw-w-full"  v-for="(item,idx) in friendListFilter" :key="idx">
          <div class="d-flex flex-row justify-content-start align-items-center tw-w-full">
            <div class="justify-content-center align-items-center position-relative tw-pr-2">
              <img
                v-if="item.userInfo.selfieFileData"
                style="width: 35px;height: 35px;"
                class="friendPicture"
                :src="`https://banooclubapi.simagar.com/${item.userInfo.selfieFileData}`"
                alt=""
              />
              <img
                v-else
                class="friendPicture"
                style="width: 35px;height: 35px;"
                src="../assets/images/defaultUser.png"
                alt=""
              />
            </div>
            <div class="p-2">
              <div class="d-flex flex-column text-end">
                <div class="friendName text-primary tw-cursor-pointer" @click="goToUserProfile(item.userInfo)">
                  {{ item.userInfo.name + " " + item.userInfo.familyName }}
                </div>
<!--                <div class="friendLink" v-if="item.userInfo.bio !== undefined">-->
<!--                  {{ item.userInfo.bio }}-->
<!--                </div>-->

              </div>
            </div>
          </div>
        </div>
      </transition-group>
    </div>
    <div class="col-12 text-warning fw-bold tw-text-right mt-3" v-else>
      هیچ دنبال کننده ای برای نمایش وجود ندارد
    </div>

  </div>
</template>

<script>
export default {
  name: "Friends",
  data() {
    return {
      friendNameFilter: "",
      friendList: [],
    };
  },
  mounted() {
    this.getFollower();
  },
  methods: {
    async goToUserProfile(user){
      try {
        this.$router.push({path: `/user/${user.userName}/posts`});
      }catch (e){
        console.log(e)
      }
    },
    getFollower() {
      this.$axios
        .post(`Follower/GetMyFollowers`, null, {})
        .then((response) => {
          if (response.status === 200) {
            this.friendList = response.data;
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
  computed: {
    friendListFilter() {
      var filter_name = this.friendNameFilter;
      return this.friendNameFilter !== ""
        ? this.friendList.filter(function (d) {
            return d.userInfo.name.indexOf(filter_name) > -1;
          })
        : this.friendList;
    },
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
.friendPicture {
  /*margin-bottom: 20px;*/
  /*max-width: 45px;*/
  /*min-width: 45px;*/
  vertical-align: middle;
  border-radius: 50%;
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


.searchFriend {
  border: 1px solid #eaeaea;
  color: #575757;
  font-size: 14px;
  padding: 5px 10px;
  width: 100%;
  margin-bottom: 20px;
}

.searchFriend::placeholder {
  text-align: right;
  padding: 4px;
}

.status {
  border-radius: 50%;
  bottom: 20px;
  display: inline-block;
  height: 12px;
  padding: 2px;
  position: absolute;
  left: 0;
  width: 12px;
}

.onlineStatus {
  background: #7fba00;
}

.status::after {
  background: white none repeat scroll 0 0;
  border-radius: 100%;
  content: "";
  height: 6px;
  left: 50%;
  position: absolute;
  top: 50%;
  transform: translate(-50%, -50%);
  width: 6px;
}

.friendName {
  display: inline-block;
  /*font-size: 14px;*/
  text-transform: capitalize;
  width: 100%;
  font-size: 16px;
  font-weight: 500;
  color: #2d2d2d;
  /*cursor: pointer;*/
}

.friendEmailStatus {
  font-size: 11px;
  color: #999;
}

.list-enter-from {
}

.list-enter-active {
  animation: list 0.3s ease-out;
}

.list-leave-active {
  animation: list 0.3s ease-in reverse;
}

.list-enter-to {
}

@keyframes list {
  from {
    opacity: 0;
    transform: translateY(-25px) scale(1);
  }
  to {
    opacity: 1;
    transform: translateY(0) scale(1);
  }
}
</style>
