<template>
  <div class=" mcontainer px-3">
    <div class="row">
      <div class="col-md-8 add_post custom_padding_for_posts" style="height: 950px;overflow-y: scroll;" @scroll="handleScroll">
        <AddPost @updateMyPosts="updateMyPosts" class="mb-3"/>
        <PostItem class="mb-3" v-for="(post,idx) in postData" :key="idx" :post_details="post" :inMainPage="true" :inMyPosts="false"/>
        <Spinner v-if="postData && postData.length !== postCounts"/>
        <div class="row mb-3" v-if="!$fetchState.pending && postData && postData.length === 0">
          <div class="col-12 text-warning fw-bold text-center">
            هیچ پستی برای نمایش وجود ندارد
          </div>
        </div>
      </div>
      <div class=" col-md-4">
        <SideBar @SideNavPictureVideo="SideNavPictureVideoToggle($event)"/></div>
    </div>

  </div>

</template>

<script>

import SideBar from "../../layouts/SideBar";
import CustomModal from "../../components/utilities/CustomModal";
import AddPost from "../../components/AddPost";
import Spinner from "../../components/Spinner"


export default {
  head() {
    return {
      title: 'صفحه من',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'صفحه من',
        },
      ],
    }
  },
  components: {SideBar, CustomModal, AddPost, Spinner},
  // mounted() {
  //   window.addEventListener('scroll', this.scrollTrue);
  // },
  fetchOnServer() {
    return true
  },
  data() {
    return {
      lastId:0,
      showModal:false,
      description:'',
      is_show_post_modal:false,
      is_show_add_post:false,
      galleries:null,
      select_media:false,
      userDefault: require("~/assets/images/defaultUser.png"),
      showCreatePost: false,
      postData: null,
      postCounts: 0,
      selected_media:'',
      btoa_selected_media:'',
      post_media:{
        base64:'',
        priority:0
      },
      Atbottom:false,
      scroll:false
    };
  },
  async fetch() {
    try {
const res = await this.$repositories.GetCredit.GetCredit()
     this.$store.commit('SetWalletAmount',res.data.data)
    }catch (e) {
      console.log(e)
    }
    try {
      const response = await this.$repositories.getFollowingPosts.getFollowingPosts(
        {
          lastId:this.lastId,
          count:3
        }
      );
      this.postData = response.data.posts;
      this.postCounts = response.data.postCount;
    }catch (error) {
      console.log(error);
    }
    try {
      
      const galleries = await this.$repositories.getMyPhotos.getMyPhotos();
      this.galleries = galleries.data
    }catch (error) {
      console.log(error);
    }
  },

  layout: "PoshtebamPlusLayout",
  methods: {
    updateMyPosts(){
      this.$fetch()
    },
    async getPostsForInfiniteScroll(id){
      try {
        const response = await this.$repositories.getFollowingPosts.getFollowingPosts(
          {
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

    handleScroll(event){

      if (event.target.offsetHeight + event.target.scrollTop >= event.target.scrollHeight) {
        const lastPostId = Object.values(this.postData).pop();
            this.getPostsForInfiniteScroll(lastPostId.postId)
      }
    },
    selectImage(item) {
      this.select_media = true
      this.selected_media = item
      this.btoa_selected_media = btoa(this.selected_media)
      this.post_media.base64 = this.btoa_selected_media
      this.post_media.priority = 1
    },
    submitNewPost() {
      this.$axios
        .post(
          `Post/Create`,
          {
            postId:0,
            userId: 0,
            title:'عنوان پست',
            description:this.description,
            fileData:this.post_media,
            createDate:new Date(Date.now())
          },
          {}
        )
        .then((response) => {
          if (response.status === 200) {
            this.$toast.success("پست با موفقیت ثبت شد");
            // this.$refs.modalCloseButton.click();
            this.$fetch();

            // this.$router.push("/Social");
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
    loggedInfoData() {
      return this.$store.state.loggedInfo;
    },
    SideNavPictureVideoToggle(data) {
      this.displaySideNav = false;
      this.displayRightSideNav = false;

      this.dataType = data;
      this.SideNavPictures = data;
    },
    reset: function () {
      this.image = null;
      this.preview = null;
      this.image_list = [];
      this.preview_list = [];
    },
    updateData() {
      this.$fetch();
    },
  },
};
</script>
<style scoped>
.selected_image{
  border: 3px solid rgb(37 99 235);
}
.modal-content {
  border-radius: 0.5rem;
}
@media only screen and (min-width: 1024px) {
  .custom_padding_for_posts {
    padding-right: 5rem !important;
    padding-left: 5rem !important;
  }
}
@media (min-width: 1024px) {
  .mcontainer {
    max-width: 1000px;
    padding: 30px 0px;
    margin-left: auto;
    margin-right: auto;
  }
}
@media (max-width: 1024px) {
  .mcontainer {
    max-width: 1000px;
    /*padding: 25px 15px;*/
    margin-left: auto;
    margin-right: auto;
  }
}



</style>
