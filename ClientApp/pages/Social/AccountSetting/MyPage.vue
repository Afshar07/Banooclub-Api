<template>
  <div  class="container-fluid mcontainer px-0 ">
    <CustomHeader/>
    <div class="tab-content" id="pills-tabContent">
      <div class="tab-pane fade " id="services" role="tabpanel" aria-labelledby="services-tab">
        <ServicesTabContent/>
      </div>
      <div class="tab-pane fade " id="Ads" role="tabpanel" aria-labelledby="pill-Ads">
          <MyAdsTabContent @PageChanged="ChangePage" :Ads="AllAds" :totalPages="totalPages" :activePage="SelectedPageId"/>
      </div>
      <div class="tab-pane fade show active" id="posts-home" role="tabpanel" aria-labelledby="posts-home-tab">
        <PostsTabContent/>
      </div>
      <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
        <FriendsTabContent/>
      </div>
      <div class="tab-pane fade" id="pills-contact" role="tabpanel" aria-labelledby="pills-contact-tab">
        <PhotosTabContent/>
      </div>
    </div>

  </div>
</template>

<script>

import EditInfoContent from "../../../components/UserTabsContent/EditInfoContent";
import CustomHeader from "../../../components/CustomHeader";
import FriendsTabContent from "../../../components/UserTabsContent/FriendsTabContent";
import PhotosTabContent from "../../../components/UserTabsContent/PhotosTabContent";
import PostsTabContent from "../../../components/UserTabsContent/PostsTabContent";
import ServicesTabContent from "../../../components/UserTabsContent/ServicesTabContent"
import MyAdsTabContent from "@/components/Ads/MyAdsTabContent";

export default {
  name: "MyPosts",
  head(){
    return{
      title: 'بانوکلاب - صفحه من'
    }
  },
  components: {
    EditInfoContent,
    MyAdsTabContent,
    CustomHeader,
    FriendsTabContent,
    PhotosTabContent,
    PostsTabContent,
    ServicesTabContent
  },
  layout: "PoshtebamPlusLayout",
  async fetch(){
    try {

      await  this.$axios.post(`Ads/GetMyAds?pageNumber=${this.SelectedPageId}&count=10`, null).then((res) => {

            this.AllAds = res.data.ads;
            this.totalPages = []
            const result = Math.ceil(res.data.adsCount / 10)
            for (let i = 1; i <= result; i++) {
              this.totalPages.push(i);
            }
          });


    }catch (e) {
      console.log(e)
    }
  },
  data() {
    return {
      userImage: "",
      postData: [],
      AllAds:[],
      totalPages:[],
      SelectedPageId:1,
      showCreatePost: false,
      user_avatar: '',
    };
  },
  methods: {
    async GetAllAds(){
      try {

        await  this.$axios.post(`Ads/GetMyAds?pageNumber=${this.SelectedPageId}&count=3`, null).then((res) => {

          this.AllAds = res.data.ads;
          this.totalPages = []
          const result = Math.ceil(res.data.adsCount / 3)
          for (let i = 1; i <= result; i++) {
            this.totalPages.push(i);
          }
        });


      }catch (e) {
        console.log(e)
      }
    },
    ChangePage(id){
      this.SelectedPageId = id
      this.GetAllAds();
    }


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
    margin-bottom: 2rem;
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
</style>
