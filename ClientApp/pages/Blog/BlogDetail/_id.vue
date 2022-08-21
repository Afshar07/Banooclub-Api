<template>
  <div :class="$fetchState.pending?'loading-skeleton':''"  class="container mcontainer ">
    <!-- Modal -->
    <div class="modal fade" id="AddBlogComment" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 v-if="SelectedComment!==null" class="modal-title" id="exampleModalLabel">پاسخ به
              {{ SelectedComment.userInfo.userName }}</h5>

          </div>
          <div class="modal-body">
            <input type="text" class="form-control border rounded" v-model="Message"
                   placeholder="متن نظر خود را وارد کنید">
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-danger" data-bs-dismiss="modal">بستن</button>
            <button type="button" class="btn btn-success" data-bs-dismiss="modal" @click="SubmitComment">ثبت</button>
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-md-8  tw-shadow">
            <div class="row  mx-auto ">
              <div class="tw-card tw-card-compact bg-white rounded tw-w-96 tw-bg-base-100 ">
                <img v-if="BlogData.fileData!==''" :src="`https://banooclubapi.simagar.com/${BlogData.fileData}`"
                     class="tw-w-full tw-h-full  tw-inset-0 tw-object-cover" alt="Shoes"/>
                <img v-else src="/nopicture.jpg" width="80px" height="80px" class="rounded-circle" alt="">
                <div class="tw-card-body">
                  <div class="tw-flex tw-items-center tw-justify-between">
                    <h2 class="tw-card-title">{{ BlogData.title }}</h2>
                    <div @click="Share" class="tw-flex tw-items-center tw-gap-1 tw-cursor-pointer">
                      <i  class="fas fa-paperclip tw-text-stone-400"></i>
                    </div>
                    <div @click="LikeBlog" class="tw-flex tw-items-center tw-gap-1 tw-cursor-pointer">
                      <small class=" tw-text-stone-400">{{ BlogData.likeCount }}</small>
                      <i v-if="BlogData.myLikeStatus===1" class="far fa-heart tw-text-stone-400"></i>
                      <i v-else class="fas fa-heart tw-text-red-600"></i>
                    </div>
                  </div>
                  <div class="d-flex align-items-center flex-wrap justify-content-start   my-2 ">
                    <span v-for="item in BlogData.tags" class="tw-badge  tw-badge-primary tw-my-3 tw-p-2 tw-mx-1 ">
                      <small> {{ item.title }}</small>
                     </span>
                  </div>

                  <div class="tw-flex tw-justify-start tw-items-center tw-gap-2">
                    <img v-if="BlogData.userInfo.selfieFileData!==null"
                         :src="`https://banooclubapi.simagar.com/media/gallery/profile/${BlogData.userInfo.selfieFileData}`"
                         width="80px" height="80px" class="rounded-circle" alt="">
                    <img v-else src="/nopicture.jpg" width="80px" height="80px" class="rounded-circle" alt="">
                    <div v-if="BlogData.userInfo" class="tw-flex tw-flex-col tw-my-3">
                      <span class="tw-font-bold">{{
                          BlogData.userInfo.name + ' ' + BlogData.userInfo.familyName
                        }}</span>
                      <nuxt-link class="text-decoration-none tw-text-blue-500" :to="`/user/${BlogData.userInfo.userName}/posts`">
                        <small>@{{ BlogData.userInfo.userName }}</small>
                      </nuxt-link>
                    </div>
                  </div>
                  <hr>
                  <div class="description">
                    <p>
                      {{ BlogData.description }}
                    </p>
                  </div>
                </div>
              </div>
              <div class=" bg-white rounded tw-p-4 tw-border-t-2  tw-w-96 tw-bg-base-100 " style="margin-top: 2rem!important;">
                <strong>مقالات مرتبط</strong>
                <div class="tw-grid tw-grid-cols-3  my-3">
                  <div v-for="(item,idx) in AllBlogs" :key="idx"
                       class="tw-card tw-card-compact w-96 lg:tw-col-span-1 tw-col-span-6  bg-base-100 tw-mx-3.5 shadow-xl">
                    <img v-if="item.fileData!==''" style="border-radius: 1rem!important;"
                         :src="`https://banooclubapi.simagar.com/${item.fileData}`"
                         class="tw-w-full tw-h-full  tw-inset-0 tw-object-cover" alt="Shoes"/>
                    <img v-else src="/nopicture.jpg" style="border-radius: 1rem!important;" width="80px" height="80px"
                         class="rounded-circle" alt="">
                    <nuxt-link class="text-decoration-none tw-text-black hover:tw-text-blue-500 tw-transition"
                               :to="`/Blog/BlogDetail/${item.blogId}/${item.seourl}`">
                      <p class="my-2 ShortDescriptionIndex">{{ item.title }}</p>
                    </nuxt-link>
                    <div class="tw-flex tw-items-center tw-gap-2">
                      <div class="tw-flex tw-items-center tw-gap-1">
                        <small class=" tw-text-stone-400">{{ item.likeCount }}</small>
                        <i  v-if="item.myLikeStatus===1" class="far fa-heart tw-text-stone-400"></i>
                        <i v-else class="fas fa-heart tw-text-red-600"></i>
                      </div>
                      <small class="text-secondary">{{ new Date(Date.now()).toLocaleDateString('fa-IR') }}</small>
                    </div>
                  </div>

                </div>

              </div>
              <div class=" bg-white rounded tw-p-4  tw-w-96 tw-bg-base-100 " style="margin-top: 2rem!important;">
                <strong class="m-lg-4 m-2 py-2 py-lg-2 "> نظرات ({{ BlogData.commentsCount }})</strong>
                <div class=" tw-grid p-2 tw-gap-2 mt-3 tw-grid-cols-12">
                  <input v-model="Message" placeholder="نظر خود را وارد کنید" class=" form-control tw-col-span-10"
                         type="text" style="border:solid 1px grey!important;"/>
                  <button @click="SubmitComment"
                          class=" tw-col-span-2 rounded  tw-bg-stone-400 tw-text-stone-300 hover:tw-text-white hover:tw-bg-stone-500 tw-cursor-pointer p-2">
                    ثبت
                  </button>
                </div>
                <div v-for="(item,idx) in  BlogComments.filter(e=> e.parentId ===0)" :key="idx" class="my-2 tw-grid-cols-1 ">
                  <div class="tw-flex tw-flex-col tw-items-start tw-justify-between p-2 tw-border-b">
                    <div class="tw-flex tw-justify-between tw-items-center tw-gap-2 tw-w-full ">
                      <div v-if="item.userInfo" class="tw-flex tw-justify-between tw-items-center tw-gap-2">
                        <img v-if="item.userInfo.selfieFileData!==null"
                             :src="`https://banooclubapi.simagar.com/media/gallery/profile/${item.userInfo.selfieFileData}`"
                               class="tw-rounded-full tw-w-[50px] tw-h-[50px]" alt="">
                        <img v-else src="/nopicture.jpg" class="tw-rounded-full tw-w-[50px] tw-h-[50px]" alt="">
                        <div class="tw-flex tw-flex-col tw-my-3">
                          <span class="tw-font-bold">{{ item.userInfo.name + ' ' + item.userInfo.familyName }}</span>
                          <nuxt-link class="text-decoration-none tw-text-blue-500" :to="`/user/${item.userInfo.userName}/posts`">
                            <small>@{{ item.userInfo.userName }}</small>
                          </nuxt-link>

                        </div>
                      </div>
                      <button data-bs-toggle="modal" data-bs-target="#AddBlogComment"
                              class=" rounded hover:tw-text-white hover:tw-bg-stone-500 tw-cursor-pointer tw-bg-stone-400 tw-text-stone-300 p-2"
                              @click="SetSelectedComment(item)"> پاسخ
                      </button>
                    </div>

                    <p class="px-5" style="overflow-wrap: anywhere!important;">{{ item.message }}</p>
                    <small class="text-secondary my-2">{{time_ago(item.createDate)}}</small>
                  </div>
                  <div v-for="(el,idx) in  BlogComments.filter(e=> e.parentId === item.blogCommentId)" :key="idx"
                       class="my-2 tw-grid-cols-1 px-5 tw-border-b">
                    <div class="tw-flex tw-flex-col tw-items-start tw-justify-between p-2 ">
                      <div class="tw-flex tw-justify-between tw-items-center tw-gap-2 tw-w-full">
                        <div v-if="el.userInfo" class="tw-flex tw-justify-between tw-items-center tw-gap-2">
                          <img v-if="el.userInfo.selfieFileData!==null"
                               :src="`https://banooclubapi.simagar.com/media/gallery/profile/${el.userInfo.selfieFileData}`"
                               class="tw-rounded-full tw-w-[50px] tw-h-[50px]" alt="">
                          <img v-else src="/nopicture.jpg"  class="tw-rounded-full tw-w-[50px] tw-h-[50px]" alt="">
                          <div class="tw-flex tw-flex-col tw-my-3">
                            <span class="tw-font-bold">{{ el.userInfo.name + ' ' + el.userInfo.familyName }}</span>
                            <nuxt-link class="text-decoration-none tw-text-blue-500" :to="`/user/${el.userInfo.userName}/posts`">
                              <small>@{{ el.userInfo.userName }}</small>
                            </nuxt-link>
                          </div>
                        </div>
                        <!--                          <button data-bs-toggle="modal" data-bs-target="#AddBlogComment" class=" rounded tw-bg-stone-400 tw-text-stone-300 p-2" @click="SetSelectedComment()" > پاسخ</button>-->
                      </div>

                          <p class="px-5  " style="overflow-wrap: anywhere!important;">{{ el.message }}</p>
                          <small class="text-secondary my-2">{{time_ago(el.createDate)}}</small>


                    </div>

                  </div>
                </div>

              </div>
            </div>
      </div>
      <div class="col-md-4">
        <RecentBlogs></RecentBlogs>
      </div>

    </div>

  </div>

</template>

<script>

import RecentBlogs from '../../../components/Blog/RecentBlogs';

export default {
  name: "index",
  components: {RecentBlogs},
  layout: "PoshtebamPlusLayout",
  data() {
    return {
      AllForums: null,
      Search: '',
      SelectedPageId: 1,
      totalPages: [],
      BlogData: null,
      Message: '',
      BlogComments: [],
      SelectedComment: null,
      AllBlogs: [],
      seoTitle: '',
      SeoDescription: '',
      site_url:'https://banooclub.simagar.com'

    }
  },
  head() {
    return {
      title: this.seoTitle,
      meta: [
        {
          hid: "description",
          name: "description",
          content: this.SeoDescription,
        },
      ],
    }
  },
  watch: {},
  methods: {
    Share(){
      if(navigator.share){
        navigator.share({
          title: this.BlogData.title,
          url: this.site_url+this.$route.fullPath
        })
      }

    },
    LikeBlog() {
      try {
        // blog like type 7
        let status = 0
        if (this.BlogData.myLikeStatus === 1) {
          status = 2
        } else if (this.BlogData.myLikeStatus === 2) {
          status = 1
        } else {
          status = 1
        }
        const res = this.$repositories.CreateBlogLike.CreateBlogLike({
          objectId: parseInt(this.$route.params.blogId),
          type: 7,
          status: status,
          createDate: new Date(Date.now()),
          userId: this.$auth.user.userInfo.userId

        })
        this.$toast.success('مقاله لایک شد')
        this.$nuxt.refresh()
        this.GetBlog()
      } catch (e) {
        console.log(e)
      }
    },
    async GetBlog() {
      try {
        const res = await this.$repositories.GetBlog.GetBlog({
          blogName: this.$route.params.seourl
        })
        this.BlogData = res.data

      } catch (e) {
        console.log(e)
      }
    },
    SetSelectedComment(item) {
      this.SelectedComment = item
    },
    async GetAllBlogComment() {
      try {
        const res = await this.$repositories.BlogCommentGetById.BlogCommentGetById({
          blogId: this.$route.params.blogId
        })
        this.BlogComments = res.data
      } catch (e) {
        console.log(e)
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

    async SubmitComment() {
      try {
        let parentId = 0
        if (this.SelectedComment !== null) {
          parentId = this.SelectedComment.blogCommentId
        }

        if(this.Message===''){
          this.$toast.error('لطفا متن نظر خود را وارد کنید')
        }else{
          const res = await this.$repositories.CreateBlogComment.CreateBlogComment({
            blogId: parseInt(this.$route.params.blogId),
            message: this.Message,
            parentId: parentId
          })
          this.Message = ''
          this.$toast.success('نظر شما ثبت شد ')
          this.GetAllBlogComment();
        }

      } catch (e) {
        console.log(e)
        this.$toast.success('خطایی رخ داده است')
      }
    },
  },
  mounted() {
    this.GetAllBlogComment();
  },
  async fetch() {
    try {
      const res = await this.$repositories.GetBlog.GetBlog({
        blogName: this.$route.params.seourl
      })
      this.BlogData = res.data
      this.seoTitle = res.data.seoTitle
      this.SeoDescription = res.data.seoDescription

    } catch (e) {
      console.log(e)
    }
    try {
      const res = await this.$repositories.GetAllBlog.GetAllBlog({
        searchByTitle: this.Search,
        pageNumber: this.SelectedPageId,
        count: 3,
        tagId: 0,
      })
      this.AllBlogs = res.data.blogs
      this.totalPages = []
      const result = Math.ceil(res.data.blogsCount / 10)
      for (let i = 1; i <= result; i++) {
        this.totalPages.push(i);
      }
    } catch (e) {
      console.log(e)
    }
  },


}
</script>

<style scoped>
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
    padding: 25px 0px;
    margin-left: auto;
    margin-right: auto;
  }
}

.nav-link {
  font-weight: 500;
  color: #8c8d90 !important;
  font-weight: 500;
  padding: 5px 9px 15px;
  border-bottom: 3px solid transparent;
}

.nav-pills .nav-link.active, .nav-pills .show > .nav-link {
  color: #0d6efd !important;
  background-color: transparent;
  font-weight: 500;
  border-bottom: 3px solid #0d6efd;
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.ActivePage {
  color: white;
  background-color: #00adef;
}

</style>
