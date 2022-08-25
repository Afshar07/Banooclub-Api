<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="container-fluid mcontainer px-0">
    <CustomHeader/>
    <div class="tab-content" id="pills-tabContent">
      <div class="tab-pane fade  " id="services" role="tabpanel" aria-labelledby="services-tab">
        <UserServices/>
      </div>
      <div class="tab-pane fade show active" id="posts-home" role="tabpanel" aria-labelledby="posts-home-tab">
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

          </div>
          <div class="col-md-5 col-sm-12">
            <div class="widget custom_card p-3">
              <h4 class="tw-text-lg tw-font-semibold"> درباره من </h4>
              <ul class="tw-text-gray-600 space-y-3 tw-mt-3">
                <li class="tw-flex tw-items-center my-2"
                    v-if="userinfo && (userinfo.cityName !== '' || userinfo.stateName !== '')">
                  <HomeIcon style="width: 30px; height: 30px;" fill="rgb(75 85 99)"
                            lass="tw-rounded-full tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"/>
                  ساکن
                  <strong class="px-1">{{ userinfo.stateName }} <span
                    v-if="userinfo.cityName !== '' && userinfo.stateName !== ''">,</span>
                    {{ userinfo.cityName }}</strong>
                </li>
                <li class="tw-flex tw-items-center my-2"  v-if="  userinfo.userSetting&&userinfo.userSetting.birthDate===null&&userinfo.relationState !== null">
                  <HeartIcon fill="rgb(75 85 99)" style="width: 30px; height: 30px;"
                             class="tw-rounded-full tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"/>
                  <div v-if="userinfo && userinfo.relationState === 1">
                    <strong> مجرد </strong>
                  </div>
                  <div v-else-if="userinfo && userinfo.relationState === 2">
                    در <strong> رابطه </strong>
                  </div>
                  <div v-else-if="userinfo && userinfo.relationState === 3">
                    <strong> متاهل </strong>
                  </div>
                  <div v-else-if="userinfo && userinfo.relationState === 4">
                    <strong> مطلقه </strong>
                  </div>
                  <div v-else-if="userinfo && userinfo.relationState === 5">
                    <strong> سایر </strong>
                  </div>
                </li>
                <li  class="tw-flex tw-items-center my-2" v-else-if="  userinfo.userSetting&& userinfo.userSetting.birthDate!==null">
                  <AllUsersIcon fill="rgb(75 85 99)" style="width: 30px; height: 30px;"  class="tw-rounded-full tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"/>
                  <div >
                    <strong  v-if="  userinfo.userSetting&& userinfo.userSetting.birthDate!==null"> {{ time_ago(userinfo.userSetting.birthDate) }}  </strong>
                  </div>

                </li>
                <li  class="tw-flex tw-items-center my-2" v-if="  userinfo.userSetting">
                  <ServiceProviderLabel v-if="userinfo.type===3"></ServiceProviderLabel>
                  <CustomerLabel v-else></CustomerLabel>
                </li>
                <li class="tw-flex tw-items-center my-2" v-if="userinfo">
                  <AllUsersIcon fill="rgb(75 85 99)" style="width: 30px; height: 30px;"
                                class="tw-rounded-full tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"/>
                  دنبال شده توسط <strong v-if="userinfo" class="px-1">{{ userinfo.followersCount }}</strong> نفر
                </li>
                <li  class="tw-flex tw-items-center my-2" v-if="  userinfo.userSetting">
                  <QuestionIcon fill="rgb(75 85 99)" style="width: 30px; height: 30px;" class="tw-rounded-full tw-w-10 tw-bg-gray-200 tw-text-xl tw-p-1 tw-ml-3"></QuestionIcon>

                  <div >
                    <p class="m-0 ShortDescriptionIndex"> {{ userinfo.userSetting.bio }}  </p>
                  </div>

                </li>
              </ul>
              <div class="tw-gap-3 grid tw-grid-cols-2 tw-mt-4">
                <div class="row">
                  <div
                    class="col-md-6 my-3"
                    v-for="(item,index) in Photos.slice(0,4)"
                    :key="index"
                  >
                    <div class="position-relative">
                      <img
                        v-if="item.priority === 2"
                        :src="`https://banooclubapi.simagar.com/media/gallery/galleryImages/${item.base64}`"
                        style="object-fit: cover;object-position: center; width: 300px;height: 250px"
                        class="rounded"
                        alt=""
                      />
                      <video
                        v-else-if="item.priority === 3"
                        class="rounded w-100"
                        style="object-fit: cover;object-position: center;height: 250px"
                        controls
                        :src="`https://banooclubapi.simagar.com/media/gallery/galleryVideos/${item.base64}`"
                      ></video>
                      <!--            </a>-->
                    </div>
                  </div>
                  <div class="col-md-12 d-flex align-items-center justify-content-center">
                    <a  data-bs-toggle="tab"  class="tw-decoration-0 tw-text-blue-400 tw-cursor-pointer" href="#pills-contact" >مشاهده بیشتر</a>
                  </div>
                </div>
              </div>
            </div>
            <Groups class="my-3"/>
          </div>

        </div>
      </div>
      <div class="tab-pane fade" id="pills-contact" role="tabpanel" aria-labelledby="pills-contact-tab">
        <UserPhotos :photos="Photos"/>
      </div>
      <div class="tab-pane fade" id="Ads" role="tabpanel" aria-labelledby="pills-contact-tab">
        <UserAds @PageChanged="ChangePage" :Ads="AllAds" :totalPages="totalPages" :activePage="SelectedPageId" />
      </div>
    </div>


  </div>

</template>

<script>
import PostItem from "../../../components/PostItem"
import QuestionIcon from "@/components/Icons/QuestionIcon";
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
import UserAds from "@/components/Ads/UserAds";
export default {
  layout: "PoshtebamPlusLayout",
  name: "MyPosts",
  components: {
    Groups, MyFriends, AboutUser, CustomHeader, PostItem, UserServices, UserPhotos, Spinner, UserFriends, AllUsersIcon,
    HeartIcon,
    UserAds,
    QuestionIcon,
    GlobeIcon, HomeIcon
  },
  fetchOnServer() {
    return true;
  },
  computed: {
    ...mapGetters("userDetails", ["getUserDetails"])
  },


  data() {
    return {
      ActiveTab:1,
      last_id: 0,
      postData: [],
      Photos: [],
      postCounts: 0,
      userId: 0,
      userinfo: [],
      AllAds:[],
      SelectedPageId:1,
      totalPages:[]
    }
  },
  async fetch() {
    try {
      const response = await this.$repositories.GetUserByUserName.GetUserByUserName({userName: this.$route.params.slug});
      this.userinfo = response.data.userInfo;

    } catch (error) {
      console.log(error);
    }
    if (!this.userinfo.userSetting.isPrivatePost) {
      try {
        const posts = await this.$repositories.getPostsByUserName.getPostsByUserName(
          {
            userName: this.$route.params.slug,
            lastId: this.last_id,
            count: 3
          }
        )

        this.postData = posts.data.posts;
        this.postCounts = this.postData.length

        // this.medias = medias.data
      } catch (error) {
        console.log(error);
      }
    }

    if (!this.userinfo.userSetting.isPrivateGalleryFriend) {
      try {
        const gallery = await this.$repositories.getUserMediasByUserName.getUserMediasByUserName({
          userName: this.$route.params.slug
        });
        this.Photos = gallery.data.medias;
      } catch (error) {
        console.log(error);
      }
    }
    try {
      const res = await  this.$repositories.getAdsByUserId.getAdsByUserId({
        userId:this.userinfo.userId,
        pageNumber:this.SelectedPageId,
        count:10

      })
      this.AllAds = res.data.ads

      this.totalPages = []
      const result = Math.ceil(response.data.adsCount / 10)
      for (let i = 1; i <= result; i++) {
        this.totalPages.push(i);
      }
    }catch (e) {
      console.log(e)
    }
  },

  methods: {
    ChangePage(id){
      this.SelectedPageId = id
    },
    time_ago(time) {
      switch (typeof time) {
        case 'number':
          break;
        case 'string':
          time = +new Date(time);
          break;
        case 'object':
          if (time.constructor === Date) time = time.getTime();
          break;
        default:
          time = +new Date();
      }
      let time_formats = [
        [60, 'ثانیه ', 1], // 60
        [120, '1 دقیقه ', 'یک دقیقه '], // 60*2
        [3600, 'دقیقه ', 60], // 60*60, 60
        [7200, '1 ساعت ', '1 ساعت '], // 60*60*2
        [86400, 'ساعت ', 3600], // 60*60*24, 60*60
        [172800, 'دیروز', 'فردا'], // 60*60*24*2
        [604800, 'روز ', 86400], // 60*60*24*7, 60*60*24
        [1209600, 'هفته ', 'هفته '], // 60*60*24*7*4*2
        [2419200, 'هفته', 604800], // 60*60*24*7*4, 60*60*24*7
        [4838400, 'ماه ', 'ماه '], // 60*60*24*7*4*2
        [29030400, 'ماه', 2419200], // 60*60*24*7*4*12, 60*60*24*7*4
        [58060800, 'سال ', 'سال '], // 60*60*24*7*4*12*2
        [2903040000, 'سال', 29030400], // 60*60*24*7*4*12*100, 60*60*24*7*4*12

      ];
      let seconds = (+new Date() - time) / 1000,
        token = '',
        list_choice = 1;

      if (seconds == 0) {
        return ''
      }
      if (seconds < 0) {
        seconds = Math.abs(seconds);
        token = '';
        list_choice = 2;
      }
      let i = 0,
        format;
      while (format = time_formats[i++])
        if (seconds < format[0]) {
          if (typeof format[2] == 'string')
            return format[list_choice];
          else
            return Math.floor(seconds / format[2]) + ' ' + format[1] + ' ' + token;
        }
      return time;
    },
    handleScroll(event) {

      if (event.target.offsetHeight + event.target.scrollTop >= event.target.scrollHeight) {
        const lastPostId = Object.values(this.postData).pop();
        this.getPostsForInfiniteScroll(lastPostId.postId)
      }
    },
    async getPostsForInfiniteScroll(id) {
      try {
        const response = await this.$repositories.getPostsByUserId.getPostsByUserId(
          {
            userId: this.$route.params.id,
            lastId: id,
            count: 3
          }
        );
        const newPosts = response.data.posts;
        newPosts.forEach((element) => {
          this.postData.push(element)
        });
        // console.log('this.postData',this.postData)
      } catch (error) {
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
