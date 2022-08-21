<template>
  <div>
    <base-modal
      :isRenderingModal="isRenderingDeleteConfirmation"
      @closeModal="closeDeleteConfirmationModal"
    >
      <template v-slot:title>حذف پست</template>
      <template v-slot:content>
        <p>آیا از حذف پست اطمینان دارید؟</p>
      </template>
      <template v-slot:actions>
        <div class="d-flex align-items-center justify-content-end">
          <button
            class="btn btn-danger mx-2"
            @click="closeDeleteConfirmationModal"
          >
            خیر
          </button>
          <button class="btn btn-primary mx-2" @click="deleteAPost(post_details.postId)">
            بله
          </button>
        </div>
      </template>
    </base-modal>
    <base-modal
      :isRenderingModal="isRenderingReportingPost"
      @closeModal="closeReportPostModal"
    >
      <template v-slot:title>ثبت تخلف پست</template>
      <template v-slot:content>

        <textarea
          row="100"
          v-model="reportReason"
          style="color: #808080;"
          class="form-control border rounded w-100 with-border mt-3"
          placeholder="علت گزارش این پست"
          id="description"
        ></textarea>
      </template>
      <template v-slot:actions>
        <div class="d-flex align-items-center justify-content-end">
          <button
            class="btn btn-danger mx-2"
            @click="closeReportPostModal"
          >
            انصراف
          </button>
          <button class="btn btn-primary mx-2" @click="reportPostSubmit(reportedPostId, reportedPostUserId)">
            ثبت گزارش
          </button>
        </div>
      </template>
    </base-modal>
    <div class="custom_card py-3">
      <div class="d-flex px-3 pb-3 align-items-center justify-content-between">
        <div class="d-flex">
          <div class="d-flex flex-column" @click="goToServiceProfile(post_details.userId)">
            <img
              v-if="post_details.userInfo && post_details.userInfo.selfieFileData && inMyPosts"
              class="IMG-FLUID myPicture"
              :src="`https://banooclubapi.simagar.com/${post_details.userInfo.selfieFileData}`"
              alt=""
              style="width: 40px; height: 40px;border-radius: 50%; object-fit: contain; cursor: pointer"
            />
            <img
              v-else-if="post_details.userInfo && post_details.userInfo.selfieFileData && !inMyPosts"
              class="IMG-FLUID myPicture"
              :src="`https://banooclubapi.simagar.com/media/gallery/profile/${post_details.userInfo.selfieFileData}`"
              alt=""
              style="width: 40px; height: 40px;border-radius: 50%; object-fit: contain; cursor: pointer"
            />
            <img
              v-else-if="!post_details.userInfo || !post_details.userInfo.selfieFileData"
              class="IMG-FLUID myPicture"
              src="../assets/images/defaultUser.png"
              alt=""
              style="width: 40px; height: 40px;cursor: pointer"
            />
          </div>
          <div class="d-flex flex-column px-3 tw-font-semibold tw-capitalize" style="font-size: 14px">
            <div v-if="post_details.userInfo">
              {{post_details.userInfo.name + ' ' + post_details.userInfo.familyName}}
            </div>
            <div>
              {{
                time_ago(post_details.createDate)
              }}
            </div>


          </div>
        </div>
        <div>
          <div class="d-flex">
            <button v-if="!inMyPosts" @click="openReportModal"
                    class="tw-flex tw-items-center tw-flex-1 tw-justify-end text-decoration-none text-dark">
              <div class="tw-p-2 tw-rounded-full tw-text-black">
                <ExclamationMarkIcon style="width: 22px; height: 22px;"/>
              </div>
            </button>
            <button class="tw-text-2xl hover:tw-bg-gray-200 tw-rounded-full tw-p-2 tw-transition tw--mr-1 tw-relative" @click="showMore(post_details.postId)">
              <MoreIcon/>
            </button>
          </div>
          <div :ref="`ShowMore${post_details.postId}`"
               class="tw-z-10 d-none tw-absolute tw-left-0 tw-bg-white tw-w-56 tw-shadow-md tw-mx-auto tw-p-2 tw-rounded-md tw-text-gray-500 tw-text-base tw-border tw-border-gray-100">
            <ul class="tw-pl-0 mb-0">
              <li>
                <button @click="Share" class="tw-text-gray-700 text-decoration-none tw-flex tw-items-center tw-px-3 tw-py-2 hover:tw-bg-gray-200 hover:tw-text-gray-800 tw-rounded-md tw-w-full">
                  <ShareIcon class="tw-ml-1"/>
                  اشتراک گذاری
                </button>
              </li>
              <!--            <li v-if="post_details.userInfo.userId === $auth.user.userInfo.userId">-->
              <!--              <nuxt-link to="#"-->
              <!--                         class="tw-text-gray-700 text-decoration-none tw-flex tw-items-center tw-px-3 tw-py-2 hover:tw-bg-gray-200 hover:tw-text-gray-800 tw-rounded-md">-->
              <!--                <EditIcon style="width: 19px; height: 19px;" class="tw-ml-1"/>-->
              <!--                ویرایش پست-->
              <!--              </nuxt-link>-->
              <!--            </li>-->
              <li v-if="post_details.userInfo && post_details.userInfo.userId === $auth.user.userInfo.userId">
                <button @click="changeCommentingStatus" class="tw-text-gray-700 text-decoration-none tw-flex tw-items-center tw-px-3 tw-py-2 hover:tw-bg-gray-200 hover:tw-text-gray-800 tw-rounded-md tw-w-full">
                  <MessageIcon fill="black" class="tw-ml-1"/>
                  <div v-if="commentingStatus">
                    فعال کردن نظرات
                  </div>
                  <div v-else>
                    غیر فعال کردن نظرات
                  </div>

                </button>
              </li>
              <li v-if="post_details.userInfo && post_details.userInfo.userId === $auth.user.userInfo.userId">
                <hr class="-mx-2 my-2 dark:border-gray-800">
              </li>
              <li v-if="post_details.userInfo && post_details.userInfo.userId === $auth.user.userInfo.userId">
                <button @click="openDeleteModal"
                        class="text-decoration-none tw-flex tw-items-center tw-px-3 tw-py-2 tw-text-red-500 hover:tw-bg-red-100 hover:tw-text-red-500 tw-rounded-md tw-w-full">
                  <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6 tw-ml-1" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
                    <path stroke-linecap="round" stroke-linejoin="round"
                          d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16"/>
                  </svg>
                  حذف
                </button>
              </li>
            </ul>

          </div>
        </div>
      </div>
      <vueper-slides
        v-if="post_details.medias && post_details.medias.length>0"
        class="no-shadow"
        fixed-height="370px"
        :rtl="true"
      >
        <vueper-slide v-for="(media,idx) in post_details.medias" :key="idx" :image="media.image">
          <template #content>
            <img class="post_images" v-if="media.priority === 2" :src="`https://banooclubapi.simagar.com/media/gallery/post/${media.base64}`"/>
            <video class="post_images" v-else-if="media.priority == 3" controls :src="`https://banooclubapi.simagar.com/media/gallery/post/${media.base64}`"></video>
          </template>
        </vueper-slide>
      </vueper-slides>
      <img class="post_images" style="height: 370px;" v-else src="../static/no_image.png"/>
      <div class="pt-3 px-3">
        <div style="text-align: right">{{post_details.description}}</div>
        <div class="tw-flex tw-items-center py-3 tw-pt-2 justify-content-between">
          <div class="dark:tw-text-gray-100 d-flex align-items-center" style="color:#676767;">
            <div style="font-size: 14px">
              پسندیده شده توسط
            </div>
            <strong class="px-1" style="font-size: 16px; font-weight: bolder"> {{ post_details.likesCount }} </strong>
            <div style="font-size: 14px">نفر</div>
          </div>
          <button @click="likePost(post_details.postId, post_details.userId)"
                  class="tw-flex tw-items-center tw-space-x-2 text-decoration-none text-dark">
            <div class="tw-p-2 tw-rounded-full  tw-text-black d-flex align-items-center">
              <LikeIcon v-if="post_details.isLikedByMe" style="width: 20px; height: 20px" fill_color="red"/>
              <DisLikeIcon v-else style="width: 20px; height: 20px"/>
            </div>
          </button>
        </div>
        <div v-if="post_details.comments && !commentingStatus && post_details.comments.length>0"
             class="tw-border-t tw-py-4 tw-space-y-4 dark:tw-border-gray-600"
             style="border-top: 1px solid #e5e7eb;"
        >
          <div v-for="(comItem, index) in post_details.comments" :key="index" class="tw-flex">
            <div class="tw-w-10 tw-h-10 tw-rounded-full tw-relative tw-flex-shrink-0">
              <img style="width: 40px; height: 40px;object-fit: contain" v-if="comItem.userInfo && inMyPosts" class="IMG-FLUID tw-rounded-full" :src="BaseUrl + 'media/gallery/profile/' + comItem.userInfo.selfieFileData || '=../assets/images/defaultUser.png'" alt=""/>
              <img style="width: 40px; height: 40px;object-fit: contain" v-if="comItem.userInfo && !inMyPosts" class="IMG-FLUID tw-rounded-full" :src="BaseUrl + 'media/gallery/profile/'+ comItem.userInfo.selfieFileData || '=../assets/images/defaultUser.png'" alt=""/>
            </div>
            <div>
              <div
                class="tw-text-gray-700 tw-py-2 tw-px-3 tw-rounded-md tw-bg-gray-100 tw-relative lg:tw-ml-5 tw-ml-2 lg:tw-mr-5">
                <p class="leading-6 mb-0">
                  {{ comItem.content }}
                </p>
                <div
                  class="tw-absolute tw-w-3 tw-h-3 tw-top-3 tw--right-1 tw-bg-gray-100 tw-transform tw-rotate-45"></div>
              </div>
            </div>
          </div>
        </div>
        <div v-if="!commentingStatus" class=" d-flex">
          <input  v-on:keyup.enter="addComment" v-model="CommentContent" @click="SetPostComments(post_details)"
                  style="border-radius: 50px; background-color: rgb(243 244 246); height: 40px !important;"
                  value="" type="text" class="form-control mx-1"
                  placeholder="نظر خود را وارد کنید...">
          <button style="font-weight: 600" class="px-2 button mx-1"
                  @click="addComment">
            ثبت
          </button>
        </div>
      </div>

    </div>
  </div>

</template>

<script>


import {VueperSlides, VueperSlide} from 'vueperslides';
import 'vueperslides/dist/vueperslides.css';
import MoreIcon from "../components/Icons/MoreIcon";
import LikeIcon from "../components/Icons/LikeIcon";
import ForbiddenIcon from "../components/Icons/ForbiddenIcon";
import MessageIcon from "../components/Icons/MessageIcon";
import ShareIcon from "../components/Icons/ShareIcon";
import ExclamationMarkIcon from "../components/Icons/ExclamationMarkIcon"
import DisLikeIcon from "../components/Icons/DisLikeIcon";

export default {
  name: "PostDetails",
  data(){
    return {
      isRenderingDeleteConfirmation: false,
      isRenderingReportingPost:false,
      show_more: false,
      CommentContent: "",
      SelectedPostComments: [],
      SelectedPostId: 0,
      commentingStatus:false,
      site_url: 'banooclub.simagar.com',
      reportReason:'',
      reportedPostId:0,
      reportedPostUserId:0
    }
  },
  computed:{
    BaseUrl() {
      return process.env.pic;
    },
  },
  components: {
    DisLikeIcon,
    VueperSlides,
    VueperSlide,
    MoreIcon,
    LikeIcon,
    ForbiddenIcon,
    MessageIcon,
    ShareIcon,
    ExclamationMarkIcon
  },
  props:{
    post_details:{
      type:Object,
      required:true
    },
    inMyPosts:{
      type: Boolean,
      required: false
    }
  },
  methods:{
    goToServiceProfile(userId){

      if(this.$auth.user.userInfo.userId === userId){
        this.$router.push({ path: `/Social/AccountSetting/MyPage` });
      }
      else {
        this.$router.push({ path: `/user/${userId}/posts` });
      }
    },

    reportPostSubmit(postId, userId) {
      if (this.reportReason.length === 0) {
        this.$toast.error("لطفا علت گزارش را بنویسید");
      } else {
        this.$axios
          .post(
            `PostReport/Create`,
            {
              postId: postId,
              userId: 0,
              reporterUserId:userId,
              postReportId:0,
              reason: this.reportReason,
              createDate: new Date(Date.now())
            },
            {}
          )
          .then((response) => {
            if (response.status === 200) {
              this.$toast.success("ثبت گزارش با موفقیت انجام شد.");
              this.isRenderingReportingPost = false
              this.$nuxt.refresh()
            }
          })
          .catch((error) => {
            console.log(error);
          });
      }
    },
    openReportModal(){
      this.isRenderingReportingPost = true
      this.reportedPostId = this.post_details.postId
      this.reportedPostUserId = this.post_details.userId
    },

    openDeleteModal(){
      this.isRenderingDeleteConfirmation = true;
    },
    closeDeleteConfirmationModal() {
      this.isRenderingDeleteConfirmation = false;
    },
    closeReportPostModal(){
      this.isRenderingReportingPost = false;

    },
    Share(){
      if(navigator.share){
        navigator.share({
          title: this.post_details.description,
          url: this.site_url+this.$route.fullPath
        })
      }

    },
    changeCommentingStatus(){
      this.commentingStatus = !this.commentingStatus
      if(this.commentingStatus){
        this.$toast.success("امکان افزودن نظر غیر فعال شد");
      }
      else {
        this.$toast.success("امکان افزودن غیر فعال شد");
      }
    },
    addComment() {
      if(this.CommentContent === ''){
        this.$toast.error("لطفا نظر خود را وارد کنید");
      }
      else{
        try {
          this.$axios
            .post(
              `PostComment/Create`,
              {
                content: this.CommentContent,
                parentId: 0,
                postId: this.SelectedPostId,
              },
              {}
            )
            .then((response) => {
              if (response.status === 200) {
                this.CommentContent = "";
                this.$toast.success("نظر شما ثبت شد");
                this.$nuxt.refresh();
              }
            })
        }
        catch (error){
          console.log(error);
        }
      }

    },
    async deleteAPost(id){
      await this.$repositories.deleteAPost.deleteAPost({
        id
      })
      this.$toast.success("پست شما با موفقیت حذف شد");
      this.$refs[`ShowMore${id}`].classList.remove('d-block')
      this.$refs[`ShowMore${id}`].classList.add('d-none')
      this.closeDeleteConfirmationModal()
      this.$nuxt.refresh()

    },
    SetPostComments(item) {
      this.SelectedPostComments = item.comments;
      this.SelectedPostId = item.postId;
    },
    likePost(postId, userId) {
      this.$axios
        .post(`PostLike/Create`, null, {
          params: {
            postId: postId,
          },
        })
        .then((response) => {
          if (response.status === 200) {
            this.$nuxt.refresh();
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    showMore(id) {
      if (this.$refs[`ShowMore${id}`].classList.contains('d-block')) {
        this.$refs[`ShowMore${id}`].classList.remove('d-block')
        this.$refs[`ShowMore${id}`].classList.add('d-none')
      } else {
        this.$refs[`ShowMore${id}`].classList.add('d-block')
        this.$refs[`ShowMore${id}`].classList.remove('d-none')
      }
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
        [120, '1 دقیقه پیش', 'یک دقیقه پیش'], // 60*2
        [3600, 'دقیقه ', 60], // 60*60, 60
        [7200, '1 ساعت پیش', '1 ساعت پیش'], // 60*60*2
        [86400, 'ساعت ', 3600], // 60*60*24, 60*60
        [172800, 'دیروز', 'فردا'], // 60*60*24*2
        [604800, 'روز ', 86400], // 60*60*24*7, 60*60*24
        [1209600, 'هفته پیش', 'هفته بعد'], // 60*60*24*7*4*2
        [2419200, 'هفته', 604800], // 60*60*24*7*4, 60*60*24*7
        [4838400, 'ماه پیش', 'ماه بعد'], // 60*60*24*7*4*2
        [29030400, 'ماه', 2419200], // 60*60*24*7*4*12, 60*60*24*7*4
        [58060800, 'سال پیش', 'سال بعد'], // 60*60*24*7*4*12*2
        [2903040000, 'سال', 29030400], // 60*60*24*7*4*12*100, 60*60*24*7*4*12

      ];
      let seconds = (+new Date() - time) / 1000,
        token = 'پیش',
        list_choice = 1;

      if (seconds == 0) {
        return 'پیش'
      }
      if (seconds < 0) {
        seconds = Math.abs(seconds);
        token = 'پیش';
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
  }
}
</script>

<style scoped>
.comments_section_height{
  height: 300px;
  overflow-y: scroll;
  border-bottom: 1px solid #e5e7eb;
}
.service_name{
  font-size: 16px;
  font-weight: 600;
}
.leaflet-top, .leaflet-bottom {
  z-index: 999 !important;
}
</style>
