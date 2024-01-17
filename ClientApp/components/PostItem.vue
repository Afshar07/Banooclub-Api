<template>
  <div v-if="post_details">
    <base-modal
      :isRenderingModal="isRenderingDeleteConfirmation"
      @closeModal="closeDeleteConfirmationModal"
    >
      <template v-slot:title>

        <div class="p-3">حذف پست</div>
        </template>
      <template v-slot:content>
        <p class="p-5 pb-0">آیا از حذف پست اطمینان دارید؟</p>
      </template>
      <template v-slot:actions>
        <div class="d-flex align-items-center justify-content-end p-3">
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
      <template v-slot:title>گزارش پست</template>
      <template v-slot:content>
        <div class="d-flex flex-column  p-3 ">
          <div v-for="item in PostReports" :key="item.ReportId" class="d-flex align-items-center gap-2">
            <input type="radio" class="form-check" v-model="reportReason" :value="item.ReportDescription" name="Reports">
            <small>{{ item.ReportDescription }}</small>
          </div>

        </div>

        <div class="d-flex align-items-center gap-2 p-3">
          <input type="checkbox" style="    height: 10px;width: 15px;" v-model="CustomDescription" :value="true" name="Reports">
          <small>توضیحات</small>
        </div>
        <textarea
          v-if="CustomDescription"
          row="100"
          v-model="reportReason"
          style="color: #808080;"
          class="form-control border rounded w-100 with-border mt-3"
          placeholder="علت گزارش این پست"
          id="description"
        ></textarea>
      </template>
      <template v-slot:actions>
        <div class="d-flex align-items-center justify-content-end p-3">
          <button
            class="tw-bg-[#fc5399] rounded p-2 text-white mx-2"
            @click="closeReportPostModal"
          >
            انصراف
          </button>
          <button class=" tw-bg-[#e7b0fe] rounded p-2 text-white mx-2" @click="reportPostSubmit(reportedPostId, reportedPostUserId)">
            ثبت گزارش
          </button>
        </div>
      </template>
    </base-modal>
    <div class="custom_card py-3">
      <div class="d-flex px-3 pb-3 align-items-center justify-content-between">
        <div class="d-flex">
          <div class="d-flex flex-column" @click="goToServiceProfile(post_details.userInfo)">
            <img
              v-if="(post_details.userInfo && post_details.userInfo.selfieFileData && $route.path==='/social')"
              class="IMG-FLUID myPicture tw-rounded"
              :src="`https://subapi.banoclub.com/media/gallery/profile/${post_details.userInfo.selfieFileData}`"
              alt=""
              style="width: 40px; height: 40px;border-radius: 50%; object-fit: contain; cursor: pointer"
            />
            <img
              v-else-if="post_details.userInfo && post_details.userInfo.selfieFileData && $route.path!=='/social'"
              class="IMG-FLUID myPicture tw-rounded"
              :src="`https://subapi.banoclub.com/${post_details.userInfo.selfieFileData}`"
              alt=""
              style="width: 40px; height: 40px;border-radius: 50%; object-fit: contain; cursor: pointer"
            />
            <img
              v-else-if="!post_details.userInfo || !post_details.userInfo.selfieFileData"
              class="IMG-FLUID myPicture tw-rounded"
              src="../assets/images/defaultUser.png"
              alt=""
              style="width: 40px; height: 40px;cursor: pointer"
            />
          </div>
          <div class="d-flex flex-column px-3 tw-font-semibold tw-capitalize" style="font-size: 14px">
            <div v-if="post_details.userInfo" class="text-primary tw-cursor-pointer" @click="goToServiceProfile(post_details.userInfo)">
              {{post_details.userInfo.userName}} @
            </div>
            <div>
              {{
                time_ago(post_details.createDate)
              }}
            </div>


          </div>
        </div>
        <div>
          <div class="d-flex align-items-center mx-3">
            <div v-if="$route.path==='/social/accountsetting/MyPage'&&post_details.status===1" class="tw-badge tw-border-none tw-bg-[#12fa69] tw-text-white">منتشر شده</div>
            <div v-if="$route.path==='/social/accountsetting/MyPage'&&post_details.status===2" class="tw-badge tw-border-none tw-bg-[#e81f87] tw-text-white">گزارش شده</div>
            <div v-if="$route.path==='/social/accountsetting/MyPage'&&post_details.status===3" class="tw-badge tw-border-none tw-badge-warning">گزارش شده</div>
            <button v-if=" $auth.user && $auth.user.userInfo && $auth.user.userInfo.userId &&  post_details.userInfo && !(post_details.userInfo.userId === $auth.user.userInfo.userId)" @click="openReportModal" class="tw-flex tw-items-center tw-flex-1 tw-justify-end text-decoration-none text-dark">
              <div v-tooltip="{content:'گزارش پست'}" class="tw-p-2 tw-rounded-full tw-text-black">
                <ExclamationMarkIcon style="width: 22px; height: 22px;"/>
              </div>
            </button>
            <button v-click-outside="hideMoreDiv" class="tw-text-2xl mx-3 hover:tw-bg-gray-200 tw-rounded-full tw-p-2 tw-transition tw--mr-1 tw-relative"
                    @click.stop="showMoreDiv">
              <MoreIcon/>
            </button>

          </div>
          <div v-if="show_more" class="tw-z-10 tw-absolute tw-left-0 tw-bg-white tw-w-56 tw-shadow-md tw-mx-auto tw-p-2 tw-rounded-md tw-text-gray-500 tw-text-base tw-border tw-border-gray-100">
            <ul class="tw-pl-0 mb-0">
              <li>
                <button @click="Share" class="tw-text-gray-700 text-decoration-none tw-flex tw-items-center tw-px-3 tw-py-2 hover:tw-bg-gray-200 hover:tw-text-gray-800 tw-rounded-md tw-w-full">
                  <ShareIcon class="tw-ml-1"/>
                  اشتراک گذاری
                </button>
              </li>

              <li v-if="post_details.userInfo && post_details.userInfo.userId === $auth.user.userInfo.userId">
                <button @click="changeCommentingStatus" class="tw-text-gray-700 text-decoration-none tw-flex tw-items-center tw-px-3 tw-py-2 hover:tw-bg-gray-200 hover:tw-text-gray-800 tw-rounded-md tw-w-full">
                  <MessageIcon fill="black" class="tw-ml-1"/>
                  <div v-if="post_details.isActiveComment">
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
            <img class="post_images" v-if="media.priority === 2" :src="`https://subapi.banoclub.com/media/gallery/post/${media.base64}`"/>
            <video class="post_images" v-else-if="media.priority == 3" controls :src="`https://subapi.banoclub.com/media/gallery/post/${media.base64}`"></video>
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
          <button v-if="post_details.userInfo && !(post_details.userInfo.userId === $auth?.user?.userInfo?.userId)" @click="likePost(post_details.postId, post_details.userId)"
                  class="tw-flex tw-items-center tw-space-x-2 text-decoration-none text-dark">
            <div class="tw-p-2 tw-rounded-full  tw-text-black d-flex align-items-center">
              <img v-if="post_details.isLikedByMe" src="/like-small.png" style="width: 30px; height: 30px" alt="">
              <img v-else src="/dislike-small.png" style="width: 30px; height: 30px" alt="">
            </div>
          </button>
        </div>
        <div v-if="post_details.comments && !post_details.isActiveComment && post_details.comments.length>0"
             class="tw-border-t tw-py-4 tw-space-y-4 dark:tw-border-gray-600"
             style="border-top: 1px solid #e5e7eb;"
        >
          <div v-if="inMainPage && post_details.comments.length>5" style="display: flex; flex-direction: column">
            <div v-for="(comItem, index) in post_details.comments.slice(0,5)" :key="index" class="tw-flex my-2">
              <div class="tw-w-10 tw-h-10 tw-rounded-full tw-relative tw-flex-shrink-0">
                <img style="width: 40px; height: 40px;object-fit: contain" v-if="comItem.userInfo && inMyPosts" class="IMG-FLUID tw-rounded-full" :src="BaseUrl + '/' + comItem.userInfo.selfieFileData || '=../assets/images/defaultUser.png'" alt=""/>
                <img style="width: 40px; height: 40px;object-fit: contain" v-else-if="comItem.userInfo && !inMyPosts" class="IMG-FLUID tw-rounded-full" :src="BaseUrl + '/media/gallery/Profile/' + comItem.userInfo.selfieFileData || '=../assets/images/defaultUser.png'" alt=""/>
                <img style="width: 40px; height: 40px;object-fit: contain" v-else-if="comItem.userInfo && !inMainPage" class="IMG-FLUID tw-rounded-full" :src="BaseUrl + comItem.userInfo.selfieFileData || '=../assets/images/defaultUser.png'" alt=""/>
              </div>
              <div>
                <div
                  class="tw-text-gray-700 tw-py-2 tw-px-3 tw-rounded-md tw-bg-gray-100 tw-relative lg:tw-mr-5 tw-mr-2 lg:tw-mr-5">
                  <p class="leading-6 mb-0">
                    {{ comItem.content }}
                  </p>
                  <div
                    class="tw-absolute tw-w-3 tw-h-3 tw-top-3 tw--right-1 tw-bg-gray-100 tw-transform tw-rotate-45"></div>
                </div>
              </div>
            </div>
            <div class="mt-3">
              <nuxt-link :to="`/Posts/${post_details.postId}`">مشاهده همه نظرات</nuxt-link>
            </div>
          </div>
          <div v-else v-for="(comItem, index) in post_details.comments" :key="index" class="tw-flex">
            <div class="tw-w-10 tw-h-10 tw-rounded-full tw-relative tw-flex-shrink-0">
              <img style="width: 40px; height: 40px;object-fit: contain" v-if="comItem.userInfo && inMyPosts" class="IMG-FLUID tw-rounded-full" :src="BaseUrl + '/' + comItem.userInfo.selfieFileData || '=../assets/images/defaultUser.png'" alt=""/>
              <img style="width: 40px; height: 40px;object-fit: contain" v-if="comItem.userInfo && !inMyPosts" class="IMG-FLUID tw-rounded-full" :src="BaseUrl + '/media/gallery/Profile/' + comItem.userInfo.selfieFileData || '=../assets/images/defaultUser.png'" alt=""/>
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
        <div v-if="!post_details.isActiveComment" class=" d-flex">
          <input v-model="CommentContent" @click="SetPostComments(post_details)"
                 style="border-radius: 50px; background-color: #fcf5ff; height: 40px !important;"
                 value="" type="text" class="form-control mx-1"
                 placeholder="نظر خود را وارد کنید...">
          <button style="font-weight: 600" class="px-2 tw-bg-[#e2a7ff] text-white p-2 rounded mx-1"
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
import MoreIcon from "./Icons/MoreIcon";
import LikeIcon from "./Icons/LikeIcon";
import ForbiddenIcon from "./Icons/ForbiddenIcon";
import MessageIcon from "./Icons/MessageIcon";
import ShareIcon from "./Icons/ShareIcon";
import ExclamationMarkIcon from "./Icons/ExclamationMarkIcon"
import DisLikeIcon from "./Icons/DisLikeIcon";
// import vClickOutside from 'v-click-outside'



export default {
  // directives: {
  //   clickOutside: vClickOutside.directive
  // },
  name: "PostItem",
  data(){
    return {
      isRenderingDeleteConfirmation: false,
      isRenderingReportingPost:false,
      show_more: false,
      CommentContent: "",
      SelectedPostComments: [],
      CustomDescription:false,
      SelectedPostId: 0,
      commentingStatus:false,
      site_url: 'pplus.simagar.com',
      reportReason:'',
      reportedPostId:0,
      reportedPostUserId:0,
      PostReports:[
        {
          ReportId:1,
          ReportDescription:"استفاده از الفاظ رکیک و محتوای غیر اخلاقی"
        },
        {
          ReportId:2,
          ReportDescription:"آزار لفظی و مزاحمت"
        },
        {
          ReportId:3,
          ReportDescription:"کاربر غیر واقعی و متخذی"
        },
      ]
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
    },
    inMainPage:{
      type: Boolean,
      required: false
    }
  },
  methods:{
    async goToUserProfile(user){
      try {
        this.$router.push({path: `/user/${user.userName}/posts`});
      }catch (e){
        console.log(e)
      }
    },
    goToServiceProfile(userInfo){
      console.log(userInfo)

      if(this.$auth.user.userInfo.userId === userInfo.userId){
        this.$router.push({ path: `/Social/AccountSetting/MyPage` });
      }
      else {
        this.$router.push({ path: `/user/${userInfo.userName}/posts` });
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
              this.$emit('PostEvent')
              // this.$nuxt.refresh()
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
    showMoreDiv(){
      console.log(this.show_more)
      if(this.show_more){
        this.show_more = false
      }
      else {
        this.show_more = true
      }
    },
    hideMoreDiv(){
      this.show_more = false
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
    async changeCommentingStatus(){
        try {
          const res = await this.$repositories.ChangePostCommentActivation.ChangePostCommentActivation({
            postId:this.post_details.postId

          })
          if(this.post_details.isActiveComment){
            this.$toast.success("امکان افزودن نظر فعال شد");
          }else{
            this.$toast.success("امکان افزودن نظر غیر فعال شد");

          }
          this.$emit('PostEvent')
        }catch (e) {
          console.log(e)
        }
      if(this.commentingStatus){

      }
      else {

      }
    },
    addComment() {
      if(this.CommentContent === '' || this.CommentContent.match(/^ *$/) !== null){
        this.$toast.error("لطفا متن نظر خود را وارد کنید");
      }
      else{
        try {
          this.$axios
            .post(
              `PostComment/Create`,
              {
                content: this.CommentContent.trim(),
                parentId: 0,
                postId: this.SelectedPostId,
              },
              {}
            )
            .then((response) => {
              if (response.status === 200) {
                this.CommentContent = "";
                this.$toast.success("نظر شما ثبت شد");
                this.$emit('PostEvent')

                // this.$nuxt.refresh();
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
      // this.$refs[`ShowMore${id}`].classList.remove('d-block')
      // this.$refs[`ShowMore${id}`].classList.add('d-none')
      this.$emit('')
      this.isRenderingDeleteConfirmation = false;
      this.$emit('PostEvent')
      // this.$nuxt.refresh()

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
            this.$emit('PostEvent')
            // this.$nuxt.refresh();
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
    hideMore(id){
      console.log(this.$refs[`ShowMore${id}`])
      if(this.$refs[`ShowMore${id}`]){
        this.$refs[`ShowMore${id}`].classList.remove('d-block')
        this.$refs[`ShowMore${id}`].classList.add('d-none')
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
        [120, '1 دقیقه پیش', 'یک دقیقه از الان'], // 60*2
        [3600, 'دقیقه ', 60], // 60*60, 60
        [7200, '1 ساعت پیش', '1 ساعت از الان'], // 60*60*2
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
        return 'همین حالا'
      }
      if (seconds < 0) {
        seconds = Math.abs(seconds);
        token = 'از الان';
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
.post_images{
  height: 100%;
  border-radius: 20px;
  width: 100%;
  object-fit: contain;
  object-position: center;
}
.show_more_div{
  top: 110px;
  left: 30px;
  z-index: 5;
}
</style>
