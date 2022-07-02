<template>
  <div class="container-fluid px-0 mcontainer">
    <CustomHeader/>
    <div class="tab-content" id="pills-tabContent">
      <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
        <div class="d-flex row">
          <div class="col-md-7 col-sm-12">
            <div
              class="row boxMainContent position-relative mx-auto main-background"
              v-if="this.$store.state.HeaderData.status != 2 ||this.$store.state.SearchedUserStatus != 3">
              <social-post
                :post-data="postData"
                @checkData="onPostDelete"
                @updateData="updateData"
              ></social-post>
            </div>
            <div v-if="this.$store.state.HeaderData.status === 2" class="row boxMainContent text-warning position-relative bg-white pb-0 p-3">
              <p class="text-center">
                صفحه این کاربر خصوصی است برای مشاهده آن درخواست دوستی ارسال کنید.
                <a href="" @click.prevent="Follow" class="text-decoration-none">ارسال درخواست دوستی</a>
              </p>
            </div>
            <div v-else-if="this.$store.state.HeaderData.status === 3" class="row boxMainContent text-warning position-relative bg-white p-3">
              <p class="text-center">
                برای مشاهده صفحه این کاربر ابتدا وارد اکانت خود شوید
              </p>
            </div>
          </div>
          <div class="col-md-5 col-sm-12">
            <AboutMe/>
            <MyFriends class="my-3"/>
            <Groups class="my-3"/>
          </div>

        </div>


      </div>
      <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
<!--        <MyFriendTabContent/>-->
      </div>
      <div class="tab-pane fade" id="pills-contact" role="tabpanel" aria-labelledby="pills-contact-tab">
<!--        <MyPhotosTabContent/>-->
      </div>
    </div>


  </div>

</template>

<script>
import SocialPost from "../../../components/SocialPost.vue";
import QuillExampleNuxt from "~/components/editor";
import OtherUserHeader from "../../../components/OtherUserHeader";
import CustomHeader from "../../../components/CustomHeader";
import AboutMe from "../../../components/UserPanel/AboutMe";
import MyFriends from "../../../components/UserPanel/MyFriends";
import Groups from "../../../components/UserPanel/Groups";

export default {
  name: "MyPosts",
  components: {Groups, MyFriends, AboutMe, CustomHeader, OtherUserHeader, QuillExampleNuxt, SocialPost },
  fetchOnServer() {
    return true;
  },
  async fetch() {
    try {
      const response =
        await this.$repositories.getPostsByUserId.getPostsByUserId(
          this.$route.params.id
        );

      this.postData = response.data.posts;
    } catch (error) {
      console.log(error);
    }
  },
  data() {
    return {
      postData: [],
    };
  },
  methods: {
    async Follow() {
      try {
        const response =
          await this.$repositories.createAFollowRequest.createAFollowRequest(
            this.$route.query.userId
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
    updateData() {
      this.$fetch();
    },
  },
};
</script>

<style scoped>
.nav-link {
  font-weight: 500;
  color: #8c8d90 !important;
  font-weight: 500;
  padding: 5px 9px 15px;
  border-bottom: 3px solid transparent;
}

.nav-pills .nav-link.active, .nav-pills .show > .nav-link {
  color: #0d6efd;
  background-color: transparent;
  font-weight: 500;
  border-bottom: 3px solid #0d6efd;
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

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
    padding-left: 4rem;
    padding-right: 4rem;
  }
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
  min-height: 30rem;
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

.boxMainContent {
  /* background: #fdfdfd none repeat scroll 0 0; */
  /* border: 1px solid #ede9e9; */
  border-radius: 3px;
  display: inline-block;
  width: 100%;
  margin-bottom: 20px;
  padding: 25px;
}

.submitFormButton {
  /* border: medium none; */
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

.boxMainContent {
   /*background: #fdfdfd none repeat scroll 0 0; */
   /*border: 1px solid #ede9e9; */
  background-color: white !important;
  border-radius: 3px;
  display: inline-block;
  width: 100%;
  margin-bottom: 20px;
  padding: 25px;
}

@media (max-width: 576px) {
  .boxMainContent {
    /* background: #fdfdfd none repeat scroll 0 0; */
    /* border: 1px solid #ede9e9; */
    border-radius: 3px;
    display: inline-block;
    width: 100%;
    margin-bottom: 20px;
    padding: 0;
  }

  .we-comet > li ul {
    margin-right: 0 !important;
    margin-top: 20px;
    padding-right: 0;
    list-style: none;
  }
}
</style>
