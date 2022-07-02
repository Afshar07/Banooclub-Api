<template>
  <div class="container mcontainer">
    <div class="row">
      <div class="col-md-12 col-lg-8 add_post px-0 custom_padding_for_posts">
        <div class="row boxMainContent mx-auto">
          <div class="col-12 text-center p-3"
               style="background-color: white; border-radius: 10px;box-shadow: 0 1px 3px 0 rgb(0 0 0 / 10%), 0 1px 2px 0 rgb(0 0 0 / 6%)">
            <div class="row d-flex justify-content-center align-items-center p-2">
              <div class="tw-flex px-0">
                <input data-bs-toggle="modal" data-bs-target="#exampleModalCenter"
                       style="border-radius: 50px; background-color: rgb(243 244 246); height: 40px !important;"
                       value="" type="text" class="form-control"
                       placeholder="افزودن پست ...">
                <img
                  v-if="$auth.user.baseData.selfie"
                  class="img-fluid headerPicture custom_header_size tw-w-10 tw-h-10 mx-2 tw-rounded-full"
                  :src="`https://BanooClubapi.simagar.com/${$auth.user.baseData.selfie}`"
                  alt=""
                />
                <img
                  v-else
                  class="img-fluid headerPicture custom_header_size tw-w-10 tw-h-10 mx-2 tw-rounded-full"
                  :src="userDefault"
                  alt=""
                />
              </div>


<!--              <div class="col-11 px-1" >-->

<!--              </div>-->
<!--              <div class="col-1 d-flex justify-content-center align-items-center px-1 ">-->
<!--                <img-->
<!--                  v-if="loggedInfoData.selfie"-->
<!--                  class="img-fluid headerPicture custom_header_size tw-w-10 tw-h-10"-->
<!--                  :src="`https://BanooClubapi.simagar.com/${loggedInfoData.selfie}`"-->
<!--                  alt=""-->
<!--                />-->
<!--                <img-->
<!--                  v-else-->
<!--                  class="img-fluid headerPicture custom_header_size tw-w-10 tw-h-10"-->
<!--                  :src="userDefault"-->
<!--                  alt=""-->
<!--                />-->
<!--              </div>-->
            </div>



<!--            <button-->
<!--              class="btn btn-sm w-100 btn-primary"-->
<!--              @click="showCreatePost = !showCreatePost"-->
<!--              type="button"-->
<!--            >-->
<!--              <i class="far fa-plus-square"></i>-->
<!--              افزودن پست-->
<!--            </button>-->
          </div>
        </div>
        <social-post
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
      <div class=" col-lg-4">
        <SideBar @SideNavPictureVideo="SideNavPictureVideoToggle($event)"/></div>


      <!-- Modal -->
      <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
          <div class="modal-content">
            <div class="modal-header">
              <h5 style="font-family: iranSans !important;" class="modal-title mx-auto" id="exampleModalLongTitle">افزودن پست</h5>
              <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body p-0">
              <div class="d-flex flex-row">
                <div class="">
                  <form class="position-relative mx-1 mx-md-3">
                    <div class="col-12 mt-3">
                      <!-- <tiptap-editor></tiptap-editor> -->
                      <quill-example-nuxt @content="postContent"/>
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

  </div>


<!--  <div class="container-fluid px-0">-->
<!--    -->
<!--    <social-post-->
<!--      v-if="postData"-->
<!--      :post-data="postData"-->
<!--      @updateData="updateData"-->
<!--    ></social-post>-->
<!--    <div class="row" v-if="!$fetchState.pending && postData.length === 0">-->
<!--      <div class="col-12 text-warning fw-bold text-center">-->
<!--        هیچ محتوای برای نمایش وجود ندارد-->
<!--      </div>-->
<!--    </div>-->
<!--  </div>-->
</template>

<script>
// import SideBar from "./SideBar";

import SocialPost from "../../components/SocialPost.vue";
import SideBar from "../../layouts/SideBar";
import QuillExampleNuxt from "../../components/editor";

export default {
  components: {QuillExampleNuxt, SideBar, SocialPost},
  fetchOnServer() {
    return true
  },
  async fetch() {
    try {
      const response =
        await this.$repositories.getFollowingPosts.getFollowingPosts();
      this.postData = response.data;
    } catch (error) {
      console.log(error);
    }
  },
  data() {
    return {
      userDefault: require("~/assets/images/defaultUser.png"),

      showCreatePost: false,

      postData: null,
    };
  },
  layout: "BanooClubLayout",
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
            this.$router.push("/Social");
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
