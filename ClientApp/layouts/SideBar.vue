<template>
  <div class="mb-5">
    <div class="tw-bg-white tw-mb-5 tw-px-4 tw-py-3 tw-rounded-md tw-shadow">
      <h4 class="tw-text-line-through tw-font-semibold tw-mb-1"> تولدها </h4>
      <div class="tw--mx-2 tw-duration-300 tw-flex hover:tw-bg-gray-50 tw-px-2 tw-py-2 tw-rounded-md">
        <img src="~/assets/images/products/product_image.jpg" class="tw-w-9 tw-h-9 tw-ml-3 tw-rounded-full" alt="">
        <p class="tw-line-clamp-2 tw-leading-6"> <strong> علی </strong> و <strong> دو نفر دیگر </strong>
          امروز تولد دارند.
        </p>
      </div>
    </div>
    <div class="col-12 p-0">
      <input
        class="searchFriend"
        type="text"
        placeholder="جستجو"
        v-model.trim="friendNameFilter"
      />
    </div>
    <h3 class="tw-text-xl tw-font-semibold">مخاطبان</h3>
    <div>
      <ul class="nav nav-pills align-items-end profile_tabs" id="pills-tab" role="tablist" style="border-bottom: 1px solid #e5e7eb">
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link active" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">
            دوستان
            <span class="follower_count">{{ VuexHeaderData.followerCount }}</span>
          </button>
        </li>
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link" id="pills-contact-tab" data-bs-toggle="pill" data-bs-target="#pills-contact" type="button" role="tab" aria-controls="pills-contact" aria-selected="false">گروه ها</button>
        </li>
      </ul>

      <div class="tab-content" id="pills-tabContent">
        <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
          <Friends/>
        </div>
        <div class="tab-pane fade" id="pills-contact" role="tabpanel" aria-labelledby="pills-contact-tab">
          <Following/>
        </div>
      </div>
<!--      <div class="row p-0" >-->
<!--        <div class="col-12 custom-width">-->
<!--          <YourPage />-->
<!--          <Friends />-->
<!--          <Following />-->
<!--          <ShortProfile />-->
<!--          <LastActivity />-->
<!--        </div>-->
<!--      </div>-->

    </div>
  </div>


</template>

<script>
import LastActivity from "./LastActivity";
import Following from "./Following";
import SideBarMenu from "./SideBarMenu";
import EditInformation from "./EditInformation";
import Friends from "./Friends";
import Socials from "./Socials";
import ShortProfile from "./ShortProfile";
import YourPage from "./YourPage";
import RecentPhotos from "./RecentPhotos";

export default {
  name: "SideBar",
  components: {
    RecentPhotos,
    Friends,
    YourPage,
    ShortProfile,
    Socials,
    EditInformation,
    SideBarMenu,
    Following,
    LastActivity,
  },
  data() {
    return {
      routePage: null,
      friendNameFilter: "",

    };
  },
  computed:{
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
.follower_count{
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
.nav-pills .nav-link.active, .nav-pills .show>.nav-link {
  color: #0d6efd !important;
  background-color: transparent;
  font-weight: 500;
  border-bottom: 3px solid #0d6efd;
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
