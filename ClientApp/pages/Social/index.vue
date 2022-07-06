<template>
  <div class="container mcontainer">
    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content" style="height: 500px;">
          <div class="modal-header">
            <h5 style="font-family: iranSans !important;" class="modal-title mx-auto" id="exampleModalLongTitle">افزودن پست</h5>
            <button ref="modalCloseButton" type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body tw-overflow-y-scroll">
            <div class="row h-100">
              <div @click.stop v-if="!select_media" class="col-12 my-3" >
                <div class="row">
                  <div class="col-4 my-3 d-flex justify-content-center" v-for="item in galleries" :key="item">
                    <img @click.stop="selectImage(item)" :src="`https://pplusapi.simagar.com/${item}`"
                         style="object-fit: cover;object-position: center; width: 200px;height: 250px" class="rounded " alt="post_image"/>
                  </div>
                </div>
              </div>

              <div v-else-if="select_media" class="col-12 d-flex flex-column py-3">
                <div class="d-flex justify-content-center align-items-center">
                  <img style="object-fit: cover;object-position: center; width: 200px;height: 250px"
                       :src="`https://pplusapi.simagar.com/${selected_media}`" class="rounded " alt="post_image"/>
                </div>
                <textarea
                  row="100"
                  v-model="description"
                  style="color: #808080;"
                  class="form-control border rounded w-100 with-border h-75 mt-3"
                  placeholder="توضیحات"
                  id="description"
                ></textarea>
                <div class="row mt-auto pt-3">
                  <div class="col-md-6 col-12">
                    <button type="button" class="button w-100" @click="select_media = false">
                      بازگشت
                    </button>
                  </div>
                  <div class="col-md-6 col-12">
                    <button type="button" class="button w-100"  @click="submitNewPost">
                      ثبت
                    </button>
                  </div>
                </div>

              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-md-12 col-lg-8 add_post px-0 custom_padding_for_posts">
        <div class="row boxMainContent mx-auto">
          <div class="col-12 text-center p-3"
               style="background-color: white; border-radius: 10px;box-shadow: 0 1px 3px 0 rgb(0 0 0 / 10%), 0 1px 2px 0 rgb(0 0 0 / 6%)">
            <div class="row d-flex justify-content-center align-items-center p-2">
              <div class="tw-flex px-0">
                <input data-bs-toggle="modal"  data-bs-target="#exampleModalCenter"
                       style="border-radius: 50px; background-color: rgb(243 244 246); height: 40px !important;"
                       value="" type="text" class="form-control"
                       placeholder="افزودن پست ...">
                <img
                  v-if="$auth.user && $auth.user.baseData && $auth.user.baseData.selfie"
                  class="img-fluid headerPicture custom_header_size tw-w-10 tw-h-10 mx-2 tw-rounded-full"
                  :src="`https://pplusapi.simagar.com/${$auth.user.baseData.selfie}`"
                  alt=""
                />
                <img
                  v-else
                  class="img-fluid headerPicture custom_header_size tw-w-10 tw-h-10 mx-2 tw-rounded-full"
                  :src="userDefault"
                  alt=""
                />
              </div>
            </div>

          </div>
        </div>
<!--        <social-post-->
<!--          class="my-4"-->
<!--          v-if="postData"-->
<!--          :post-data="postData"-->
<!--          @updateData="updateData"-->
<!--        ></social-post>-->
<!--        <div class="row" v-if="!$fetchState.pending && postData.length === 0">-->
<!--          <div class="col-12 text-warning fw-bold text-center">-->
<!--            هیچ محتوای برای نمایش وجود ندارد-->
<!--          </div>-->
<!--        </div>-->
      </div>
      <div class=" col-lg-4">
        <SideBar @SideNavPictureVideo="SideNavPictureVideoToggle($event)"/></div>

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
      const posts = await this.$repositories.getFollowingPosts.getFollowingPosts({pageNumber:0, count:0});
      this.postData = posts.data;
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
  data() {
    return {
      description:'',
      is_show_post_modal:false,
      is_show_add_post:false,
      galleries:null,
      select_media:false,
      userDefault: require("~/assets/images/defaultUser.png"),
      showCreatePost: false,
      postData: null,
      selected_media:'',
      btoa_selected_media:'',
      post_media:{
        base64:'',
        priority:0
      }
    };
  },
  layout: "PoshtebamPlusLayout",
  methods: {
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
