<template>
  <div class="my-5">
    <div class="tw-bg-white tw-mb-5 tw-px-4 tw-py-3 tw-rounded-md tw-shadow" v-if="birthdateList.length>0">
      <h4 class="tw-text-line-through tw-font-semibold tw-mb-1"> تولدها </h4>
      <div class="contact-list tw-w-full" v-for="(item,idx) in birthdateList" :key="idx"
           @click="profileLinkGenerator(item)" style="cursor: pointer">
        <div class="d-flex flex-row justify-content-start align-items-center tw-w-full">
          <div class="justify-content-center align-items-center position-relative tw-pr-2">
            <img
              v-if="item.selfieFileData"
              style="width: 35px;height: 35px;"
              class="friendPicture"
              :src="`https://banooclubapi.simagar.com/media/gallery/profile/${item.selfieFileData}`"
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
              <div class="friendName">
                {{ item.name + " " + item.familyName }}
              </div>

            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="tw-w-full my-3">
      <div class="d-flex align-items-center justify-content-between">
        <h6 class=" m-0 p-0 ">درصد تکمیل اطلاعات پروفایل</h6>

        <ServiceProviderLabel v-if="$auth.user && $auth.user.baseData &&$auth.user.baseData.userType===3 "></ServiceProviderLabel>
          <CustomerLabel v-else></CustomerLabel>
      </div>

      <div class="tw-flex tw-items-start  tw-flex-col my-2">
        <div class="progress tw-w-full">
          <div class="progress-bar" role="progressbar" aria-label="Example with label" :style="`width: ${FilledCount}%`"
               aria-valuenow="25" aria-valuemin="0" aria-valuemax="100">{{ FilledCount }}%
          </div>
        </div>

      </div>

    </div>

    <h3 class="tw-text-xl tw-font-semibold">مخاطبان</h3>
    <div>
      <ul class="nav nav-pills align-items-end profile_tabs" id="pills-tab" role="tablist"
          style="border-bottom: 1px solid #e5e7eb">
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link active" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile"
                  type="button" role="tab" aria-controls="pills-profile" aria-selected="false">
            دنبال کنندگان
            <!--            <span class="follower_count"  v-if="userinfo">{{ userinfo.followersCount }}</span>-->
          </button>
        </li>
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link" id="pills-contact-tab" data-bs-toggle="pill" data-bs-target="#pills-contact"
                  type="button" role="tab" aria-controls="pills-contact" aria-selected="false">
            دنبال شوندگان
            <!--            <span class="follower_count" v-if="userinfo">{{ userinfo.followingsCount }}</span>-->
          </button>
        </li>
      </ul>
      <div class="tab-content" id="pills-tabContent">
        <div class="tab-pane show active fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
          <Friends/>
        </div>
        <div class="tab-pane fade" id="pills-contact" role="tabpanel" aria-labelledby="pills-contact-tab">
          <Following/>
        </div>
      </div>
    </div>
  </div>


</template>

<script>


import Friends from "./Friends";
import Following from "./Following";
import ServiceProviderLabel from "@/components/ServiceProviderLabel";
import CustomerLabel from "@/components/CustomerLabel";
export default {
  name: "SideBar",
  components: {
    Friends,
    Following,
    ServiceProviderLabel,
    CustomerLabel,
  },
  async fetch() {
    console.log(this.$auth.user)
    try {
      const birthdate_list = await this.$repositories.getBirthdateList.getBirthdateList();
      this.birthdateList = birthdate_list.data.data;
      console.log(this.birthdateList)
    } catch (error) {
      console.log(error);
    }
  },
  data() {
    return {
      routePage: null,
      friendNameFilter: "",
      birthdateList: [],
      userinfo: {},


    };
  },

  methods: {
    async getUserInfo() {
      try {
        const response = await this.$repositories.getUserByToken.getUserByToken();
        this.userinfo = response.data.userInfo;
        console.log('this.userinfo', this.userinfo)
      } catch (error) {
        console.log(error);
      }
    },
    async goToUserProfile(user) {
      try {
        this.$router.push({path: `/user/${user.userName}/posts`});
      } catch (e) {
        console.log(e)
      }
    },
    profileLinkGenerator(user) {
      console.log('user', user)
      if (user.userId === this.$auth.user.userInfo.userId) {
        this.$router.push("/social/AccountSetting/MyPage");
      } else {
        this.$router.push({path: `/user/${user.userId}/posts`});
      }
    },

  },
  computed: {
    FilledCount() {
      let count = 0
      this.$auth.user.userInfo.name !== '' ? count++ : count - 1
      this.$auth.user.userInfo.familyName !== '' ? count++ : count - 1
      this.$auth.user.userInfo.mobile !== '' ? count++ : count - 1

        this.$auth.user.userInfo.userSetting.birthDate !== '' ? count++ : count - 1
      this.$auth.user.userInfo.userSetting.gender !== null ? count++ : count - 1
      this.$auth.user.userInfo.userSetting.bio !== '' ? count++ : count - 1
      this.$auth.user.userInfo.kartMelliDoc !== '' ? count++ : count - 1
      this.$auth.user.userInfo.cityId !== null ? count++ : count - 1
      this.$auth.user.userInfo.stateId !== null ? count++ : count - 1
      this.$auth.user.userInfo.userSetting !== '' ? count++ : count - 1
      return Math.round(count * (100 / 10))
    },
    VuexHeaderData() {
      return this.$store.state.HeaderData;
    },
    userAvatar() {
      return this.loggedInfoData?.selfie
        ? this.BaseUrl + this.loggedInfoData.selfie
        : "/defaultUser.png";
    },
    friendListFilter() {
      var filter_name = this.friendNameFilter;
      return this.friendNameFilter !== ""
        ? this.friendList.filter(function (d) {
          return d.userInfo.name.indexOf(filter_name) > -1;
        })
        : this.friendList;
    },

    InlineBg() {
      const base = this.BaseUrl;
      if (this.VuexHeaderData.banner) {
        return {
          backgroundImage: `url(${base + this.VuexHeaderData.banner})`,
          backgroundPosition: "center",
          backgroundSize: "cover",
          backgroundRepeat: "no-repeat",
        };
      } else {
        return {
          backgroundImage: `url(${this.DefaultImg})`,
          backgroundPosition: "center",
          backgroundSize: "cover",
          backgroundRepeat: "no-repeat",
        };
      }
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

.friendEmailStatus {
  font-size: 11px;
  color: #999;
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

.custom-width {
  width: 100%;
}

.follower_count {
  padding: 5px 9px;
  background: rgba(42, 65, 232, 0.08);
  border-radius: 30px;
  font-weight: 500;
  font-size: 12px;
  margin-right: 3px;
  color: #2a41e8;
}

.nav-link {
  font-weight: 500;
  color: #8c8d90 !important;
  font-weight: 500;
  padding: 5px 9px 20px;
  border-bottom: 3px solid transparent;
}

.nav-pills .nav-link.active, .nav-pills .show > .nav-link {
  color: #0d6efd !important;
  background-color: transparent;
  font-weight: 500;
  border-bottom: 3px solid #0d6efd;
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
