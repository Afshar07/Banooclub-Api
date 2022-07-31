<template>
  <div class="tw-flex tw-items-start tw-p-7 mx-auto">
    <!-- Modal -->
    <div class="modal fade" id="DeleteForum" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">حذف انجمن</h5>

          </div>
          <div class="modal-body">
           <span>آیا از حذف این انجمن اظمینان دارید ؟ </span>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-danger" data-bs-dismiss="modal">خیر</button>
            <button type="button" class="btn btn-success" data-bs-dismiss="modal" @click="DeleteForum">بله</button>
          </div>
        </div>
      </div>
    </div>
    <div class="tw-flex tw-flex-col tw-items-center ">
      <img v-if=" forum_details.userInfo &&  forum_details.userInfo.selfieFileData===''" src="~/assets/images/products/product_image.jpg" alt="" class="tw-w-12 tw-h-12 tw-rounded-full">
      <img v-else-if="forum_details.userInfo " :src="`https://banooclubapi.simagar.com/media/gallery/profile/${forum_details.userInfo.selfieFileData}`" alt="" class="tw-w-12 tw-h-12 tw-rounded-full">
      <br>

      <div v-if="forum_details.userId === $auth.user.userInfo.userId && $route.path.toLowerCase() === '/forums/myforums/'" class="tw-flex tw-justify-center tw-items-center tw-gap-2">
        <a  href="#DeleteForum" data-bs-toggle="modal">
          <i class="fas fa-trash tw-text-red-600 hover:tw-text-red-900 hover:tw-transition  tw-cursor-pointer "></i>
        </a>
        <nuxt-link :to="`/Forums/EditForum/${forum_details.forumId}`">
          <i class="fas fa-edit tw-text-blue-600 hover:tw-text-blue-900 hover:tw-transition  tw-cursor-pointer "></i>
        </nuxt-link>

      </div>

    </div>

    <div class="tw-flex-1 tw-mr-4">
      <nuxt-link style="color:#666666;" :to="`/Forums/${forum_details.forumId}/`" class="tw-mb-1 text-decoration-none">
        <h2 class="tw-text-lg tw-font-semibold tw-line-clamp-1">
          {{forum_details.title}}
        </h2>
      </nuxt-link>
      <p v-if="$route.path!=='/Forums/MyForums/'" class="tw-text-sm d-flex align-items-center tw-text-gray-400 tw-my-2">
        پست شده توسط:
        <span v-if="forum_details.userInfo" data-href="%40tag-dev.html"> {{ forum_details.userInfo.userName }} </span>



      </p>
      <p class="tw-text-sm d-flex align-items-center tw-text-gray-400 tw-my-2">
         <span>
             {{ time_ago(forum_details.createDate) }}
        </span>
      </p>

      <p class="tw-leading-6 tw-line-clamp-2 tw-mt-3">
        {{forum_details.description}}
      </p>
      <p v-if=" $route.path!=='/Forums/' && forum_details.status ===3" class="tw-leading-6 tw-text-red-600 tw-line-clamp-2 tw-mt-3">
        انجمن شما به دلیل وجود کلمات نا مناسب توسط ربات سایت گزارش داده شده و در دست بررسی میباشد.
      </p>
      <p v-if=" $route.path!=='/Forums/' && forum_details.status ===4" class="tw-leading-6 tw-text-red-600 tw-line-clamp-2 tw-mt-3">
        انجمن شما به دلیل وجود کلمات نا مناسب توسط کاربران سایت گزارش داده شده و در دست بررسی میباشد.
      </p>
      <p v-if=" $route.path!=='/Forums/' && forum_details.status ===1" class="tw-leading-6 tw-text-green-500 tw-line-clamp-2 tw-mt-3">
      انجمن شما فعال است
      </p>
      <p v-if=" $route.path!=='/Forums/' && forum_details.status ===2" class="tw-leading-6 tw-text-red-600 tw-line-clamp-2 tw-mt-3">
        انجمن شما غیر فعال است
      </p>
    </div>
    <div class="sm:tw-flex tw-items-center">
      <span class="tw-text-x mx-1" style="color: rgb(128 128 128)"> {{ forum_details.comments }} </span>
      <ForumIcon class="svg_icons_size mx-1"/>
    </div>
  </div>

</template>

<script>
import ForumIcon from "../Icons/ForumIcon";
export default {
  name: "ForumMainItem",
  components: {ForumIcon},
  props:{
    forum_details:{
      type: Object,
      required: true
    }
  },
  data(){
    return{
      SelectedForum:null
    }
  },
  methods:{
   async DeleteForum(){
     try {
       const res = await this.$repositories.DeleteForum.DeleteForum({
         id:this.forum_details.forumId
       })
       this.$toast.success('انجمن با موفقیت حذف شد')
       this.$nuxt.refresh();
     }catch (e){
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

  }
}
</script>

<style scoped>

</style>
