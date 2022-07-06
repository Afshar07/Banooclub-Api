<template>
  <div class="container-fluid px-0 mcontainer">
    <div class="row boxMainContent mx-auto">
      <div class="col-xl-10 col-md-12 col-lg-12 add_post px-0 mx-auto">
        <CustomHeader/>
        <div class="tab-content" id="pills-tabContent">
          <div class="tab-pane fade show active" id="services" role="tabpanel" aria-labelledby="services-tab">
            <MyServicesTabContents/>
          </div>
          <div class="tab-pane fade" id="posts-home" role="tabpanel" aria-labelledby="posts-home-tab">
            <MyPostsTabContents/>
          </div>
          <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
            <MyFriendTabContent/>
          </div>
          <div class="tab-pane fade" id="pills-contact" role="tabpanel" aria-labelledby="pills-contact-tab">
            <MyPhotosTabContent/>
          </div>
          <div class="tab-pane fade" id="pills-edit-info" role="tabpanel" aria-labelledby="pills-edit-info-tab">
            <EditInfoContent/>
          </div>
        </div>


      </div>
    </div>
    <transition name="bounce">
      <div
        class="row boxMainContent conditionalOpen bg-white mx-auto"
        v-if="showCreatePost"
      >
        <div class="d-flex flex-row">
          <div class="">
            <form class="position-relative mx-1 mx-md-3">
              <div class="col-12 mt-3">
                <!-- <tiptap-editor></tiptap-editor> -->
                <quill-example-nuxt @content="postContent"/>
              </div>
              <div class="col-12 text-start mt-3">
                <button
                  type="button"
                  class="btn submitFormButton"
                  @click="submitNewPost"
                >
                  ارسال
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </transition>
    <!--    <div class="row">-->
    <!--      <div class="col-md-7">-->
    <!--        <SocialPost-->
    <!--          v-if="postData.length > 0"-->
    <!--          :post-data="postData"-->
    <!--          @updateData="updateData"-->
    <!--        />-->
    <!--      </div>-->
    <!--    </div>-->

    <!--    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">-->
    <!--      <div class="modal-dialog modal-dialog-centered" role="document">-->
    <!--        <div class="modal-content">-->
    <!--          <div class="modal-header">-->
    <!--            <h5 class="modal-title mx-auto" id="exampleModalLongTitle">افزودن پست</h5>-->
    <!--            <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">-->
    <!--              <span aria-hidden="true">&times;</span>-->
    <!--            </button>-->
    <!--          </div>-->
    <!--          <div class="modal-body">-->
    <!--            <div class="d-flex flex-row">-->
    <!--              <div class="">-->
    <!--                <form class="position-relative mx-1 mx-md-3">-->
    <!--                  <div class="col-12 mt-3">-->
    <!--                    &lt;!&ndash; <tiptap-editor></tiptap-editor> &ndash;&gt;-->
    <!--                    <quill-example-nuxt @content="postContent"/>-->
    <!--                  </div>-->
    <!--                  <div class="col-12 text-start mt-3">-->
    <!--                    <button-->
    <!--                      type="button"-->
    <!--                      class="btn submitFormButton"-->
    <!--                      @click="submitNewPost"-->
    <!--                    >-->
    <!--                      ارسال-->
    <!--                    </button>-->
    <!--                  </div>-->
    <!--                </form>-->
    <!--              </div>-->
    <!--            </div>-->

    <!--          </div>-->
    <!--        </div>-->
    <!--      </div>-->
    <!--    </div>-->

  </div>
</template>

<script>

import MyPhotosTabContent from "../../../components/MyTabsContent/MyPhotosTabContent";
import MyFriendTabContent from "../../../components/MyTabsContent/MyFriendTabContent";
import MyPostsTabContents from "../../../components/MyTabsContent/MyPostsTabContents";
import CustomHeader from "../../../components/CustomHeader";
import MyServicesTabContents from "../../../components/Products/MyServicesTabContents";
import EditInfoContent from "../../../components/MyTabsContent/EditInfoContent";

export default {
  name: "MyPosts",
  components: {
    EditInfoContent,
    MyServicesTabContents,
    CustomHeader,
    MyPostsTabContents, MyFriendTabContent, MyPhotosTabContent
  },
  layout: "PoshtebamPlusLayout",
  computed: {
    VuexHeaderData() {
      return this.$store.state.HeaderData;
    },


  },

  fetchOnServer() {
    return true;
  },
  data() {
    return {
      userDefault: require("~/assets/images/defaultUser.png"),
      DefaultImg: require("~/assets/images/defaultBanner.jpg"),
      userImage: "",
      postData: [],
      showCreatePost: false,
      user_avatar: '',
    };
  },
  methods: {

    submitNewPost() {
      this.$axios
        .post(
          `Post/Create`,
          {
            userId: 0,
            postId: 0,
            content: this.postContentValue,
          },
          {}
        )
        .then((response) => {
          if (response.status === 200) {
            this.$refs.modalCloseButton.click();
            this.$fetch();
          }
        })
        .catch((error) => {
          console.log(error);
        });
      this.showCreatePost = false;
    },

    postContent(value) {
      this.postContentValue = value;
    },
    changeActivePage(activePage) {
      if (activePage === "home") {
        this.activePage = "home";
      } else if (activePage === "notification") {
        this.activePage = "notification";
      } else if (activePage === "inputBox") {
        this.activePage = "inputBox";
      } else if (activePage === "learn") {
        this.activePage = "learn";
      } else if (activePage === "post") {
        this.activePage = "post";
      } else if (activePage === "requirements") {
        this.activePage = "requirements";
      } else {
        this.activePage = "like";
      }
    },

    Others() {
      if (
        this.$nuxt.$route.path.includes("TimelineVideos") ||
        this.$nuxt.$route.params.id ||
        this.$nuxt.$route.path.includes("TimelinePhotos") ||
        this.$nuxt.$route.path.includes("RoomMateInfo") ||
        this.$nuxt.$route.path.includes("Advertises") ||
        this.$nuxt.$route.path.includes("PersonalInfo") ||
        this.$nuxt.$route.path.includes("UserFollowers") ||
        this.$nuxt.$route.path.includes("UserFollowings")
      ) {
        return true;
      }
    },

    async getUserInfo() {
      try {
        const response = await this.$repositories.getUserByToken.getUserByToken();
        this.$store.commit("SetUserData", response.data);
        let MypageData = {};
        this.userinfo = response.data.userInfo;
        MypageData.logCounts = response.data.logCounts;
        MypageData.logCountsDistinct = response.data.logCountsDistinct;
        MypageData.unreadTicketCount = response.data.unreadTicketCount;
        MypageData.bio = response.data.userInfo.userSetting.bio;
        MypageData.userName = response.data.userInfo.userName;
        MypageData.selfie = response.data.userInfo.selfieFileData;
        this.imgUserAvatar();
        this.imageBackground();
        this.$store.commit("SetUserMypageInfo", MypageData);
      } catch (error) {
        console.log(error);
      }
    },
    imgUserAvatar() {
      if (this.userinfo.selfieFileData !== null) {
        this.userImageApi = `https://pplusapi.simagar.com/${this.userinfo.selfieFileData}`;
      }
    },
    imageBackground() {
      if (this.userinfo.bannerFileData !== null) {
        this.BackgroundApi = `https://pplusapi.simagar.com/${this.userinfo.bannerFileData}`;
      }
    },





    loggedInfoData() {
      return this.$store.state.loggedInfo;
    },

    async GetMyData() {
      const response = await this.$repositories.getUserByToken.getUserByToken();
      this.$store.commit("SetUserData", response.data);
      this.userInfoData = response.data.userInfo;
      this.firstName = this.userInfoData.name;
      this.lastName = this.userInfoData.familyName;
      this.email = this.userInfoData.email;
      this.mobile = this.userInfoData.mobile;
      this.countryName = this.userInfoData.userSetting.flag;
      this.bio = this.userInfoData.userSetting.bio;
      this.userTag = this.userInfoData.userSetting.userTag;
      this.gender = this.userInfoData.userSetting.gender;
      this.birthDate = this.userInfoData.userSetting.birthDate;
      this.userSettingId = this.userInfoData.userSetting.userSettingId;
      this.nationalCart = this.userInfoData.userSetting.kartMelliDoc;
      this.passport = this.userInfoData.userSetting.passportDoc;
      this.Certificate = this.userInfoData.userSetting.lawyerCertificateDoc;
      this.NewsPaper = this.userInfoData.userSetting.newspaperDoc;
    },
    onPostDelete(value) {
      this.deletePost(value);
    },
    deletePost(postId) {
      this.$axios
        .post(`Post/Delete`, null, {
          params: {
            id: postId,
          },
        })
        .then((response) => {
          if (response.status === 200) {
            this.$fetch();
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },

  },
};
</script>

<style scoped>


@media (min-width: 1024px) {
  .mcontainer {
    max-width: 1175px;
    padding: 30px 120px;
    margin-left: auto;
    margin-right: auto;
  }
}

@media only screen and (max-width: 1024px) {
  #pills-tabContent {
    margin-top: 1rem;
    padding-left: 1rem;
    padding-right: 1rem;
  }
}

@media only screen and (min-width: 1024px) {
  #pills-tabContent {
    margin-top: 2rem;
    padding-left: 2rem;
    padding-right: 2rem;
  }
}

.profile_tabs nav-item {
  margin: 0 !important;
}


.ChangePhotoBtn :hover {
  color: #0056b3 !important;
}

.ChangePhotoBtn :hover .ChangePhotoIcon {
  fill: #0056b3 !important;
}

.TopGap {
  position: relative;
  top: 50px;
}

svg .ql-stroke {
  right: -15px !important;
}

.bounce-enter-active {
  animation: bounce-in 0.5s;
}

.bounce-leave-active {
  animation: bounce-in 0.5s reverse;
}

@keyframes bounce-in {
  0% {
    transform: scale(0);
  }
  50% {
    transform: scale(1);
  }
  100% {
    transform: scale(1);
  }
}

.conditionalOpen {
  min-height: auto;
  transition: margin 300ms cubic-bezier(0.17, 0.04, 0.03, 0.94);
}

img {
  max-width: 100%;
  height: auto;
}

.myPicture {
  margin-bottom: 20px;
  max-width: 45px;
  min-width: 45px;
  vertical-align: middle;
  border-radius: 50%;
}

.submitFormButton {
  border: medium none;
  color: #fff;
  font-size: 13px;
  padding: 3px 10px;
  background: #088dcd;
}

.post-comt-box form textarea {
  background: #f3f3f3 none repeat scroll 0 0;
  border-color: transparent;
  border-radius: 3px;
  color: #000000;
  font-size: 13.5px;

  height: 40px;
  line-height: 16px;
}

.post-comt-box form button {
  bottom: 2px;
  position: absolute;
  right: 0;
  background: none;
}

@media only screen and (max-width: 768px) {
  .boxMainContent {
    background: none;
    border: none;
    border-radius: 3px;
    display: inline-block;
    width: 100%;
    margin-bottom: 20px;
    /*padding: 25px;*/
  }



  .BannerPhoto {
    border-top-right-radius: 0 !important;
    border-top-left-radius: 0 !important;
  }

  .change_pic_div {
    position: relative;
    top: 20px;
  }
}

@media only screen and (min-width: 769px) {
  .boxMainContent {
    /*background: #fdfdfd none repeat scroll 0 0;*/
    /*border: 1px solid #ede9e9;*/
    border-radius: 3px;
    display: inline-block;
    width: 100%;
    margin-bottom: 20px;
    /*padding-top: 20px;*/

    /*padding: 25px;*/
  }
}

@media (max-width: 576px) {
  .we-comet > li ul {
    margin-right: 0 !important;
    margin-top: 20px;
    padding-right: 0;
    list-style: none;
  }
}
</style>
