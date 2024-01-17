<template>
  <div>
    <div class="d-flex row">
      <div class="col-md-7 col-sm-12">
        <div class="row boxMainContent mx-auto">
          <div class="col-12 text-center p-3" style="background-color: white; border-radius: 10px;box-shadow: 0 1px 3px 0 rgb(0 0 0 / 10%), 0 1px 2px 0 rgb(0 0 0 / 6%)">
            <div class="row d-flex justify-content-center align-items-center p-2">
              <div class="tw-flex px-0">
                <input data-bs-toggle="modal" data-bs-target="#exampleModalCenter"
                       style="border-radius: 50px; background-color: rgb(243 244 246); height: 40px !important;"
                       value="" type="text" class="form-control"
                       placeholder="افزودن پست ...">
                <img
                  v-if="$auth.user && $auth.user.baseData && $auth.user.baseData.selfie"
                  class="img-fluid headerPicture custom_header_size tw-w-10 tw-h-10 mx-2  tw-rounded-full tw-object-cover"
                  :src="`https://subapi.banoclub.com/media/gallery/profile/${$auth.user.baseData.selfie}`"
                  alt=""
                />
                <img
                  v-else
                  class="img-fluid headerPicture custom_header_size tw-w-10 tw-h-10 mx-2 tw-rounded-full  tw-object-cover "
                  :src="userDefault"
                  alt=""
                />
              </div>
            </div>
          </div>
        </div>
        <social-post
          ref="socialPost"
          class="my-4"
          v-if="postData"
          :post-data="postData"
          @updateData="updateData"
        ></social-post>
        <div class="row" v-if="!$fetchState.pending && postData.length === 0">
          <div class="col-12 text-warning fw-bold text-center">
            هیچ محتوای برای نمایش وجود ندارد
          </div>
        </div>
      </div>
      <div class="col-md-5 col-sm-12">
        <AboutMe/>
        <MyFriends class="my-3"/>
        <Groups class="my-3"/>
      </div>
    </div>
    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 style="font-family: iranSans !important;" class="modal-title mx-auto" id="exampleModalLongTitle">افزودن پست</h5>
            <button ref="modalCloseButton" type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body p-0">
            <div class="d-flex flex-row">
              <div class="">
                <form class="position-relative mx-1 mx-md-3">
                  <div class="col-12 mt-3">
                    <!-- <tiptap-editor></tiptap-editor> -->

                  </div>
                  <div class="col-12 text-start my-3">
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
        </div>
      </div>
    </div>
  </div>


</template>

<script>

import AboutMe from "../UserPanel/AboutMe";
import MyFriends from "../UserPanel/MyFriends";
import SocialPost from "../SocialPost";
import Groups from "../UserPanel/Groups";

export default {
  name: "MyPostsTabContents",
  components:{
    Groups,
    SocialPost,
    MyFriends,
    AboutMe,

  },
  data(){
    return{
      userDefault: require("~/assets/images/defaultUser.png"),
      postData: [],
      postContentValue: "",

    }
  },
  async fetch() {
    try {
      const response = await this.$repositories.getMyPosts.getMyPosts();
      this.postData = response.data.posts;
    } catch (error) {
      console.log(error);
    }
  },
  methods:{
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

    updateData() {
      this.$fetch();
    },

  }

}
</script>

<style scoped>


</style>
