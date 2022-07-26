<template>
  <div>
    <div class="d-flex row">
      <div class="col-md-7 col-sm-12" style="height: 1500px;overflow-y: scroll;" @scroll="handleScroll">
        <AddPost @updateMyPosts="updateMyPosts" class="mb-3"/>
        <PostItem class="mb-3" v-for="(post,idx) in postData" :key="idx" :post_details="post" :inMainPage="false" :inMyPosts="false"/>
        <Spinner style="text-align: center" v-if="postData && postData.length !== postCounts"/>
        <div class="row mb-3" v-if="!$fetchState.pending && postData && postData.length === 0">
          <div class="col-12 text-warning fw-bold text-center">
            هیچ پستی برای نمایش وجود ندارد
          </div>
        </div>
      </div>
      <div class="col-md-5 col-sm-12">
        <AboutMe/>
<!--        <MyFriends class="my-3"/>-->
        <Groups class="my-3"/>
      </div>
    </div>
  </div>
</template>

<script>
import AboutMe from "../UserPanel/AboutMe";
import MyFriends from "../UserPanel/MyFriends";
import Groups from "../UserPanel/Groups";
import PlusIcon from "../Icons/PlusIcon";
import CustomModal from "../../components/utilities/CustomModal";
import AddPost from "../../components/AddPost";
import PostItem from "../../components/PostItem";
import Spinner from "../../components/Spinner"



export default {
  name: "PostsTabContent",
  components:{
    Groups,
    MyFriends,
    AboutMe,
    PlusIcon,
    CustomModal,
    AddPost,
    PostItem,
    Spinner
  },
  data(){
    return{
      postData: null,
      showModal:false,
      lastId:0,
      postCounts: 0,

    }
  },
  async fetch() {
    try {
      const response = await this.$repositories.getMyPosts.getMyPosts(
        {
          lastId:this.lastId,
          count:3
        });
      this.postData = response.data.posts;
      this.postCounts = response.data.postCount;


    } catch (error) {
      console.log(error);
    }
  },
  methods:{
    async getPostsForInfiniteScroll(id){
      try {
        const response = await this.$repositories.getMyPosts.getMyPosts(
          {
            lastId:id,
            count:3
          }
        );
        const newPosts = response.data.posts;
        newPosts.forEach((element) => {
          this.postData.push(element)
        });

      }catch (error) {
        console.log(error);
      }
    },

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

    updateMyPosts(){
      this.$fetch()
    },
    updateData() {
      this.$fetch();
    },
  }
}
</script>

<style scoped>

</style>
