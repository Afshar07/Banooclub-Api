<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class=" mcontainer   px-3">
    <div class="row">

      <div class="col-md-8 add_post custom_padding_for_posts" style="height: 950px;overflow-y: scroll;" @scroll="handleScroll">

        <LazyAddPost ref="AddPost" @updateMyPosts="updateMyPosts" class="mb-3"/>

        <div         v-if="!$fetchState.pending && postData && postData.length === 0"  class="row mb-3" >
          <div  @click="$refs.AddPost.showModal=true" class="col-12 text-warning fw-bold text-center tw-cursor-pointer">
            <img src="/girl1.jpg" class="tw-w-full tw-h-auto tw-rounded-xl" alt="">
          </div>
        </div>
        <PostItem @PostEvent="GetPosts" class="mb-3" v-for="(post,idx) in postData" :key="idx" :post_details="post" :inMainPage="true" :inMyPosts="false"/>
        <LazySpinner v-if="postData && postData.length !== postCounts"/>


      </div>
      <div class=" col-md-4 px-0">
        <SideBar />
      </div>
    </div>

  </div>

</template>

<script>

import SideBar from "../../layouts/SideBar";

export default {
  head() {
    return {
      title: 'پروفایل من',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'پروفایل من',
        },
      ],
    }
  },
  components: {SideBar},
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
      DataString:'',
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

      this.postData = response.data.posts
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
          if(this.postData.findIndex(e=> e.postId === element.postId)>-1){
            const idx = this.postData.findIndex(e=> e.postId === element.postId)
            this.postData[idx] = element
          }else{
            this.postData.push(element)
          }

        });
      }catch (error) {
        console.log(error);
      }
    },
async GetPosts(){
  try {
    const response = await this.$repositories.getFollowingPosts.getFollowingPosts(
      {
        lastId:this.lastId,
        count:3
      }
    );

    this.postData = response.data.posts
    this.postCounts = response.data.postCount;
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

.mcontainer{
  @apply  tw-bg-[#faf7ff] !important;
}
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
