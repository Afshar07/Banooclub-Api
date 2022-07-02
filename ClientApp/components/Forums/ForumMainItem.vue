<template>
  <div class="tw-flex tw-items-start tw-p-7">
    <img src="~/assets/images/products/product_image.jpg" alt="" class="tw-w-12 tw-h-12 tw-rounded-full">
    <div class="tw-flex-1 tw-mr-4">
      <nuxt-link style="color:#666666;" :to="`/Forums/${forum_details.forumId}/`" class="tw-mb-1 text-decoration-none">
        <h2 class="tw-text-lg tw-font-semibold tw-line-clamp-1">
          {{forum_details.title}}
        </h2>
      </nuxt-link>
      <p class="tw-text-sm tw-text-gray-400 tw-my-2">
        پست شده توسط:
        <span data-href="%40tag-dev.html">علی</span>
        {{
          time_ago(forum_details.createDate)
        }}
      </p>
      <p class="tw-leading-6 tw-line-clamp-2 tw-mt-3">
        {{forum_details.description}}
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
  methods:{
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

</style>
