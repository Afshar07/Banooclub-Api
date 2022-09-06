<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="container mcontainer p-3" v-if="forumDetails">

    <div class="modal fade" id="ReportForum" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">گزارش تالار</h5>

          </div>
          <div class="modal-body">
            <span>آیا از انجام این عملیات مطمئنید؟</span>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-danger" data-bs-dismiss="modal">خیر</button>
            <button type="button" class="btn btn-success" data-bs-dismiss="modal" @click="ReportForum">بله</button>
          </div>
        </div>
      </div>
    </div>
    <div class="tw-w-full bg-white p-3 d-flex align-items-center justify-content-between gap-3  rounded ">
      <div class="d-flex align-items-center gap-2">
        <img src="/girl-icon-forum.png" class="tw-w-[7rem] tw-h-20" alt="">
        <div class="d-flex align-items-center flex-column">
          <strong class="text-purple">{{ forumDetails.title }}</strong>
        </div>
      </div>
      <div class="d-flex align-items-center gap-2">

      <button v-tooltip="{content:'افزودن تالار جدید'}" @click="$router.push('/Forums/AddForum')" class="btn AddReplyBtn text-white">
        <PlusIcon fill="#ff6f9e" style="width: 30px; height: 30px;"/>
      </button>
      <button v-tooltip="{content:'گزارش تالار'}" data-bs-toggle="modal" data-bs-target="#ReportForum" class="btn AddReplyBtn text-white">
        <ExclamationMarkIcon fill="#ff6f9e" style="width: 37px; height: 37px;"/>
      </button>
      </div>

    </div>
    <div class="d-flex flex-wrap justify-content-between align-items-center gap-2">
      <p class="tw-text-sm d-flex align-items-center tw-text-gray-400 tw-my-2">
        پست شده توسط:

        <span v-if="forumDetails.userInfo" data-href="%40tag-dev.html">
          <nuxt-link class="tw-font-semibold tw-px-2 text-decoration-none tw-text-blue-500" :to="`/user/${forumDetails.userInfo.userName}/posts`">
             {{ forumDetails.userInfo.userName }}
          </nuxt-link>

        </span>
        <span class="tw-text-black px-1">
          {{time_ago(forumDetails.createDate) }}
        </span>
      </p>
      <p class="tw-text-sm tw-text-gray-400 tw-my-2 px-3">
        بازدید
        <span class="tw-text-black px-1">
          {{forumDetails.viewsCount}}
        </span>
      </p>
    </div>

    <div class="row py-3">
      <div class="col-lg-8 tw-flex-shirink-0">
        <div class="  mx-auto custom_card">
          <div class="d-flex flex-column">
            <div class="d-flex align-items-center row">
              <div class="d-flex flex-column justify-content-center align-items-center col-1">
                <button @click="ForumLike(1)">
                  <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6" viewBox="0 0 20 20" fill="hsl(210deg 8% 75%)">
                    <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-8.707l-3-3a1 1 0 00-1.414 0l-3 3a1 1 0 001.414 1.414L9 9.414V13a1 1 0 102 0V9.414l1.293 1.293a1 1 0 001.414-1.414z" clip-rule="evenodd" />
                  </svg>
                </button>
                <div style="color:hsl(210,8%,45%);">{{forumDetails.vote}}</div>
                <button @click="ForumLike(2)">
                  <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6" viewBox="0 0 20 20" fill="hsl(210deg 8% 75%)">
                    <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm1-11a1 1 0 10-2 0v3.586L7.707 9.293a1 1 0 00-1.414 1.414l3 3a1 1 0 001.414 0l3-3a1 1 0 00-1.414-1.414L11 10.586V7z" clip-rule="evenodd" />
                  </svg>
                </button>
              </div>
              <div class="d-flex flex-column col-11 pb-3">
                <p class="tw-mt-3 tw-mb-0" style="font-size: 16px">
                  {{forumDetails.description}}
                </p>

                <div class="d-flex justify-content-start px-3">
                  <div v-for="tag in forumDetails.tags" class="back_tags p-1 m-1">{{tag.title}}</div>
                </div>
                <div class="d-flex align-items-center tw-mt-3" style="height: 40px;">
                  <div class="d-flex px-3">
                    <a target="_blank" :href="`https://telegram.me/share/url?url=${site_url+$route.fullPath}`">
                      <TelegramIcon style="width: 20px; height: 20px;"/>
                    </a>
                    <a target="_blank" :href="`https://api.whatsapp.com/send?text=${site_url+$route.fullPath}`">
                      <WhatsappIcon  style="width: 20px; height: 20px;"/>
                    </a>
                    <a target="_blank" :href="`https://www.facebook.com/sharer.php?u=${site_url+$route.fullPath}`">
                      <InstagramIcon style="width: 20px; height: 20px;"/>
                    </a>
                  </div>

                  <div class=" d-flex tw-mr-auto justify-content-center align-items-center" :class="[]">
                    <div v-if="!show_comment_input" class="tw-text-gray-400" style="font-size: 15px; font-style: italic;" @click="show_comment_input = !show_comment_input"> افزودن نظر ...</div>
                    <input v-on:keyup.enter="addComment()" v-if="show_comment_input" v-model="forum_comment"
                           style="border-radius: 50px; background-color: rgb(243 244 246); height: 40px !important;"
                           value="" type="text" class="form-control mx-1"
                           placeholder="نظر خود را وارد کنید...">
                  </div>
                </div>
              </div>
            </div>
            <div v-if="forumDetails.comments.length>0" class="py-3 border-bottom tw-font-bold " >{{ forumDetails.comments.length }} پاسخ</div>

          </div>
          <ul class="tw-divide-y  tw-divide-gray-100 sm:tw-m-0 tw--mx-5">
            <li v-for="comment in forumDetails.comments">
              <div class="row mx-auto my-3">
                <div class="col-md-1">
                  <div class="d-flex flex-lg-column flex-row  justify-content-center align-items-center ">
                      <button @click="ForumCommentLike(1,comment.forumCommentId)">
                        <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6" viewBox="0 0 20 20" fill="hsl(210deg 8% 75%)">
                          <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-8.707l-3-3a1 1 0 00-1.414 0l-3 3a1 1 0 001.414 1.414L9 9.414V13a1 1 0 102 0V9.414l1.293 1.293a1 1 0 001.414-1.414z" clip-rule="evenodd" />
                        </svg>
                      </button>
                      <div style="color:hsl(210,8%,45%);">{{comment.vote}}</div>
                      <button @click="ForumCommentLike(2,comment.forumCommentId)">
                        <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6" viewBox="0 0 20 20" fill="hsl(210deg 8% 75%)">
                          <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm1-11a1 1 0 10-2 0v3.586L7.707 9.293a1 1 0 00-1.414 1.414l3 3a1 1 0 001.414 0l3-3a1 1 0 00-1.414-1.414L11 10.586V7z" clip-rule="evenodd" />
                        </svg>
                      </button>
                  </div>
                </div>
                <div class="col-md-11">
                  <div class="d-flex flex-column  pb-3">
                    <nuxt-link class="text-decoration-none" :to="`/user/${comment.userInfo.userName}/posts`">
                      <span class="text-primary">@{{comment.userInfo.userName}}</span>
                    </nuxt-link>

                    <p class="tw-mt-1 p-1" style="font-size: 13px;overflow-wrap: anywhere">
                      {{comment.text}}
                    </p>
<!--                    <div class="d-flex justify-content-between">-->
<!--                      <div class="d-flex px-3 mt-2">-->
<!--                        <a target="_blank" :href="`https://telegram.me/share/url?url=${site_url+$route.fullPath}`">-->
<!--                          <TelegramIcon style="width: 20px; height: 20px;"/>-->
<!--                        </a>-->
<!--                        <a target="_blank" :href="`https://api.whatsapp.com/send?text=${site_url+$route.fullPath}`">-->
<!--                          <WhatsappIcon  style="width: 20px; height: 20px;"/>-->
<!--                        </a>-->
<!--                        <a target="_blank" :href="`https://www.facebook.com/sharer.php?u=${site_url+$route.fullPath}`">-->
<!--                          <InstagramIcon style="width: 20px; height: 20px;"/>-->
<!--                        </a>-->
<!--                      </div>-->
<!--                      &lt;!&ndash;                    <button type="button" class="button p-2" style="font-size: 12px; height: 28px">&ndash;&gt;-->
<!--                      &lt;!&ndash;                      دنبال کردن&ndash;&gt;-->
<!--                      &lt;!&ndash;                    </button>&ndash;&gt;-->

<!--                    </div>-->
                  </div>
                </div>
              </div>
            </li>

          </ul>
        </div>
      </div>
      <TopCommenters></TopCommenters>

    </div>
  </div>



</template>

<script>
import TelegramIcon from "../../components/Icons/TelegramIcon";
import WhatsappIcon from "../../components/Icons/WhatsappIcon";
import InstagramIcon from "../../components/Icons/InstagramIcon";
import TopCommenters from '../../components/Forums/TopCommenters';
import PlusIcon from "@/components/Icons/PlusIcon";
import ExclamationMarkIcon from "@/components/Icons/ExclamationMarkIcon";
export default {
  name: "ForumDetail",

  components: {InstagramIcon, WhatsappIcon, ExclamationMarkIcon,TelegramIcon,TopCommenters,PlusIcon},
  layout: "PoshtebamPlusLayout",
  head(){
    return{
      title: this.forumDetails?this.forumDetails.title:'تالار'
    }
  },
  async fetch() {
    try {
      const forum_details = await this.$repositories.getAForum.getAForum(
        {
          forumId:this.$route.params.id
        }
      )
      this.forumDetails = forum_details.data

    }
    catch (error){
      console.log(error)
    }

  },
  data(){
    return{
      likes:0,
      show_comment_input:false,
      forumDetails:null,
      forum_comment:'',
      site_url: 'https://banooclub.simagar.com',

    }
  },
  methods:{
    async ReportForum(){
    try {
      this.$nextTick(()=>{
        this.$nuxt.$loading.start()
      })
    const res = await this.$repositories.ReportForum.ReportForum({
      forumId: parseInt(this.$route.params.id)
    })
      this.$router.push('/Forums/')
      this.$toast.success('گزارش شما ثبت و در دست بررسی توسط ادمین است')
      this.$nuxt.$loading.finish()
      this.$nuxt.loading = false
    }catch (e){
      this.$nuxt.$loading.finish()
      this.$nuxt.loading = false
    }finally {
      this.$nuxt.$loading.finish()
      this.$nuxt.loading = false
    }
    },
    async ForumCommentLike(status,id){
      try {
        const res = await this.$repositories.CreateLike.CreateLike({
          objectId:id,
          type: 5,
          status: status,
          userId: this.$auth.user.userInfo.userId
        })
        if(res.data ===3){
          this.$toast.success('نمره شما ثبت شد')
        }else if(res.data ===1 ||res.data===2 ){
          this.$toast.error('شما به این تالار نمره داده اید')
        }
        this.$nuxt.refresh();
      }catch (e) {
        console.log(e)
      }

    },


    async ForumLike(status){
      try {
        const res = await this.$repositories.CreateLike.CreateLike({
          objectId: this.forumDetails.forumId,
          type: 2,
          status: status,
          userId: this.$auth.user.userInfo.userId
        })

        if(res.data ===3){
          this.$toast.success('نمره شما ثبت شد')
        }else if(res.data ===1 ||res.data===2 ){
          this.$toast.error('شما به این تالار نمره داده اید')
        }

        this.$nuxt.refresh();
      }catch (e) {
        console.log(e)
      }


    },
    async addComment(){
      if(this.forum_comment == ''){
        this.$toast.error("لطفا متن نظر را وارد کنید");
      }
      else{
        this.$nuxt.$loading.start();
        try{
          await this.$repositories.createForumComment.createForumComment({
            userId: 0,
            forumId: this.forumDetails.forumId,
            text: this.forum_comment,
            parentId: this.forumDetails.forumId,
          })
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
          this.forum_comment = ''
          this.$nuxt.refresh();
        } catch (error) {
          console.log(error);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
        }
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

.forum_links{
  color:hsl(210,8%,45%);
  font-size: 13px;
}

</style>
