<template>
  <div class="container-fluid mcontainer px-0">
    <CustomHeader/>
    <div class="tab-content" id="pills-tabContent">
      <div class="tab-pane fade show active" id="services" role="tabpanel" aria-labelledby="services-tab">
        <UserServices/>
      </div>
      <div class="tab-pane fade" id="posts-home" role="tabpanel" aria-labelledby="posts-home-tab">
        <div class="d-flex row">
          <div class="col-md-7 col-sm-12" style="height: 1500px;overflow-y: scroll;" @scroll="handleScroll">
            <PostItem class="mb-3" v-for="(post,idx) in postData" :key="idx" :post_details="post"/>
            <Spinner v-if="postData && postData.length !== postCounts"/>
            <div class="row mb-3" v-if="!$fetchState.pending && postData && postData.length === 0">
              <div class="col-12 text-warning fw-bold text-center">
                هیچ پستی برای نمایش وجود ندارد
              </div>
            </div>
            <div v-else-if="userinfo && userinfo.userSetting && userinfo.userSetting.isPrivatePost" class="row mb-3">
              <div class="col-12 text-warning fw-bold text-center">
                پست های این کاربر شخصی هستند.
              </div>
            </div>
            <!--            <div-->
            <!--              class="row boxMainContent position-relative mx-auto main-background"-->
            <!--              v-if="this.$store.state.HeaderData.status != 2 ||this.$store.state.SearchedUserStatus != 3">-->
            <!--              <social-post-->
            <!--                :post-data="postData"-->
            <!--                @checkData="onPostDelete"-->
            <!--                @updateData="updateData"-->
            <!--              ></social-post>-->
            <!--            </div>-->
            <!--            <div v-if="this.$store.state.HeaderData.status === 2" class="row boxMainContent text-warning position-relative bg-white pb-0 p-3">-->
            <!--              <p class="text-center">-->
            <!--                صفحه این کاربر خصوصی است برای مشاهده آن درخواست دوستی ارسال کنید.-->
            <!--                <a href="" @click.prevent="Follow" class="text-decoration-none">ارسال درخواست دوستی</a>-->
            <!--              </p>-->
            <!--            </div>-->
            <!--            <div v-else-if="this.$store.state.HeaderData.status === 3" class="row boxMainContent text-warning position-relative bg-white p-3">-->
            <!--              <p class="text-center">-->
            <!--                برای مشاهده صفحه این کاربر ابتدا وارد اکانت خود شوید-->
            <!--              </p>-->
            <!--            </div>-->
          </div>
          <div class="col-md-5 col-sm-12">
            <div class="widget custom_card p-3">
              <h4 class="tw-text-lg tw-font-semibold"> درباره من </h4>
              <ul class="tw-text-gray-600 space-y-3 tw-mt-3">
                <li class="tw-flex tw-items-center my-2" v-if="userinfo && (userinfo.cityName !== '' || userinfo.stateName !== '')">
                  <HomeIcon style="width: 30px; height: 30px;" fill="rgb(75 85 99)" class="tw-rounded-full tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"/>
                  ساکن
                  <strong class="px-1">{{userinfo.stateName}} <span v-if="userinfo.cityName !== '' && userinfo.stateName !== ''">,</span> {{userinfo.cityName}}</strong>
                </li>
                <li class="tw-flex tw-items-center my-2" v-if="userinfo && userinfo.relationState !== null">
                  <HeartIcon fill="rgb(75 85 99)" style="width: 30px; height: 30px;" class="tw-rounded-full tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"/>
                  <div v-if="userinfo && userinfo.relationState === 1">
                    <strong> مجرد  </strong>
                  </div>
                  <div v-else-if="userinfo && userinfo.relationState === 2">
                    در <strong> رابطه  </strong>
                  </div>
                  <div v-else-if="userinfo && userinfo.relationState === 3">
                    <strong> متاهل  </strong>
                  </div>
                  <div v-else-if="userinfo && userinfo.relationState === 4">
                    <strong> مطلقه  </strong>
                  </div>
                  <div v-else-if="userinfo && userinfo.relationState === 5">
                    <strong> سایر  </strong>
                  </div>
                </li>
                <li class="tw-flex tw-items-center my-2" v-if="userinfo">
                  <AllUsersIcon fill="rgb(75 85 99)" style="width: 30px; height: 30px;" class="tw-rounded-full tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"/>
                  دنبال شده توسط  <strong v-if="userinfo" class="px-1">{{ userinfo.followingsCount }}</strong> نفر
                </li>
              </ul>
              <div class="tw-gap-3 grid tw-grid-cols-2 tw-mt-4">
                <div class="row">
                  <div
                    class="col-md-6 my-3"
                    v-for="(item,index) in Photos"
                    :key="index"
                  >
                    <div class="position-relative">
                      <img
                        v-if="item.priority === 2"
                        :src="`https://banooclubapi.simagar.com/${item.base64}`"
                        style="object-fit: cover;object-position: center; width: 300px;height: 250px"
                        class="rounded"
                        alt=""
                      />
                      <video
                        v-else-if="item.priority === 3"
                        class="rounded w-100"
                        style="object-fit: cover;object-position: center;height: 250px"
                        controls
                        :src="`https://banooclubapi.simagar.com/${item.base64}`"
                      ></video>
                      <!--            </a>-->
                    </div>
                  </div>
                </div>

                <!--      <div class="d-flex justify-content-around mt-3">-->
                <!--        <img style="width: 105px; height: 105px;" src="~/assets/images/products/product_image.jpg" alt="" class="tw-rounded-lg">-->
                <!--        <img style="width: 105px; height: 105px;" src="~/assets/images/products/product_image.jpg" alt="" class="tw-rounded-lg">-->
                <!--        <img style="width: 105px; height: 105px;" src="~/assets/images/products/product_image.jpg" alt="" class="tw-rounded-lg">-->
                <!--      </div>-->

              </div>
            </div>

            <!--            <MyFriends class="my-3"/>-->
            <Groups class="my-3"/>
          </div>

        </div>

      </div>

      <div class="tab-pane fade" id="pills-contact" role="tabpanel" aria-labelledby="pills-contact-tab">
        <UserPhotos :photos="Photos" />
      </div>
    </div>



  </div>

</template>

<script>
import PostItem from "../../../components/PostItem"

import CustomHeader from "../../../components/CustomHeader";
import AboutUser from "../../../components/UserPanel/AboutUser";
import MyFriends from "../../../components/UserPanel/MyFriends";
import Groups from "../../../components/UserPanel/Groups";
import UserServices from "../../../components/UserTabsContent/UserServices"
import UserPhotos from "../../../components/UserTabsContent/UserPhotos"
import {mapGetters} from "vuex";
import Spinner from "../../../components/Spinner";
import UserFriends from "../../../components/UserTabsContent/UserFriends"
import GlobeIcon from "../../../components/Icons/GlobeIcon";
import HeartIcon from "../../../components/Icons/HeartIcon";
import HomeIcon from "../../../components/Icons/HomeIcon"
import AllUsersIcon from "../../../components/Icons/AllUsersIcon"
export default {
  layout: "PoshtebamPlusLayout",
  name: "MyPosts",
  components: {Groups, MyFriends, AboutUser, CustomHeader, PostItem,UserServices, UserPhotos, Spinner, UserFriends, AllUsersIcon,
    HeartIcon,
    GlobeIcon, HomeIcon },
  fetchOnServer() {
    return true;
  },
  computed: {
    ...mapGetters("userDetails", ["getUserDetails"])
  },


  data(){
    return{
      last_id:0,
      postData:[],
      Photos:[],
      postCounts: 0,
      userId:0,
      userinfo:[]
    }
  },
  async fetch() {
    try {
      const response = await this.$repositories.GetUserByUserName.GetUserByUserName({userName:this.$route.params.slug});
      this.userinfo = response.data.userInfo;

    } catch (error) {
      console.log(error);
    }
    if(!this.userinfo.userSetting.isPrivatePost){
      try {
        const posts = await this.$repositories.getPostsByUserName.getPostsByUserName(
          {
            userName:this.$route.params.slug,
            lastId:this.last_id,
            count:3
          }
        )
        // const medias = await this.$repositories.getUserMedias.getUserMedias({userId:2});
        this.postData = posts.data.posts;
        this.postCounts = this.postData.length

        // this.medias = medias.data
      }
      catch (error) {
        console.log(error);
      }
    }

    if(!this.userinfo.userSetting.isPrivateGalleryFriend) {
      try {
        const gallery = await this.$repositories.getUserMediasByUserName.getUserMediasByUserName({
          userName: this.$route.params.slug
        });
        this.Photos = gallery.data.medias;
      } catch (error) {
        console.log(error);
      }
    }
  },

  methods: {


    handleScroll(event){

      if (event.target.offsetHeight + event.target.scrollTop >= event.target.scrollHeight) {
        const lastPostId = Object.values(this.postData).pop();
        this.getPostsForInfiniteScroll(lastPostId.postId)
      }

      // if (event.target.scrollHeight) {
      //   console.log('event.target.scrollHeight',event.target.scrollHeight)
      //   console.log(event.target.scrollTop)
      //   //this is stupid but it works temprory
      //   if (event.target.scrollTop === event.target.scrollHeight - 969) {
      //
      //   }
      // }
    },
    async getPostsForInfiniteScroll(id){
      try {
        const response = await this.$repositories.getPostsByUserId.getPostsByUserId(
          {
            userId:this.$route.params.id,
            lastId:id,
            count:3
          }
        );
        const newPosts = response.data.posts;
        newPosts.forEach((element) => {
          this.postData.push(element)
        });
        // console.log('this.postData',this.postData)
      }catch (error) {
        console.log(error);
      }
    },


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


/*@media (min-width: 1024px) {*/
/*  .mcontainer {*/
/*    max-width: 1175px;*/
/*    padding: 30px 120px;*/
/*    margin-left: auto;*/
/*    margin-right: auto;*/
/*  }*/
/*}*/

/*@media only screen and (max-width: 1024px) {*/
/*  #pills-tabContent {*/
/*    margin-top: 1rem;*/
/*    padding-left: 1rem;*/
/*    padding-right: 1rem;*/
/*  }*/
/*}*/

/*@media only screen and (min-width: 1024px) {*/
/*  #pills-tabContent {*/
/*    margin-top: 2rem;*/
/*    padding-left: 4rem;*/
/*    padding-right: 4rem;*/
/*  }*/
/*}*/
/*.bounce-enter-active {*/
/*  animation: bounce-in 0.5s;*/
/*}*/

/*.bounce-leave-active {*/
/*  animation: bounce-in 0.5s reverse;*/
/*}*/

/*@keyframes bounce-in {*/
/*  0% {*/
/*    transform: scale(0);*/
/*  }*/
/*  50% {*/
/*    transform: scale(1);*/
/*  }*/
/*  100% {*/
/*    transform: scale(1);*/
/*  }*/
/*}*/

/*.conditionalOpen {*/
/*  min-height: 30rem;*/
/*  transition: margin 300ms cubic-bezier(0.17, 0.04, 0.03, 0.94);*/
/*}*/

/*img {*/
/*  max-width: 100%;*/
/*  height: auto;*/
/*}*/

/*.myPicture {*/
/*  margin-bottom: 20px;*/
/*  max-width: 45px;*/
/*  min-width: 45px;*/
/*  vertical-align: middle;*/
/*  border-radius: 50%;*/
/*}*/

/*.boxMainContent {*/
/*  !* background: #fdfdfd none repeat scroll 0 0; *!*/
/*  !* border: 1px solid #ede9e9; *!*/
/*  border-radius: 3px;*/
/*  display: inline-block;*/
/*  width: 100%;*/
/*  margin-bottom: 20px;*/
/*  padding: 25px;*/
/*}*/

/*.submitFormButton {*/
/*  !* border: medium none; *!*/
/*  color: #fff;*/
/*  font-size: 13px;*/
/*  padding: 3px 10px;*/
/*  background: #088dcd;*/
/*}*/

/*.post-comt-box form textarea {*/
/*  background: #f3f3f3 none repeat scroll 0 0;*/
/*  border-color: transparent;*/
/*  border-radius: 3px;*/
/*  color: #000000;*/
/*  font-size: 13.5px;*/

/*  height: 40px;*/
/*  line-height: 16px;*/
/*}*/

/*.post-comt-box form button {*/
/*  bottom: 2px;*/
/*  position: absolute;*/
/*  right: 0;*/
/*  background: none;*/
/*}*/

/*.boxMainContent {*/
/*   !*background: #fdfdfd none repeat scroll 0 0; *!*/
/*   !*border: 1px solid #ede9e9; *!*/
/*  background-color: white !important;*/
/*  border-radius: 3px;*/
/*  display: inline-block;*/
/*  width: 100%;*/
/*  margin-bottom: 20px;*/
/*  padding: 25px;*/
/*}*/

/*@media (max-width: 576px) {*/
/*  .boxMainContent {*/
/*    !* background: #fdfdfd none repeat scroll 0 0; *!*/
/*    !* border: 1px solid #ede9e9; *!*/
/*    border-radius: 3px;*/
/*    display: inline-block;*/
/*    width: 100%;*/
/*    margin-bottom: 20px;*/
/*    padding: 0;*/
/*  }*/

/*  .we-comet > li ul {*/
/*    margin-right: 0 !important;*/
/*    margin-top: 20px;*/
/*    padding-right: 0;*/
/*    list-style: none;*/
/*  }*/
/*}*/
</style>
