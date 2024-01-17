<template>
  <layout-vertical>

    <router-view />

    <app-customizer
      v-if="showCustomizer"
      slot="customizer"
    />

  </layout-vertical>
</template>

<script>
import LayoutVertical from '@core/layouts/layout-vertical/LayoutVertical.vue'
import AppCustomizer from '@core/layouts/components/app-customizer/AppCustomizer.vue'
import { $themeConfig } from '@themeConfig'
import {getUserData} from "@/auth/utils";
import ToastificationContent from "@core/components/toastification/ToastificationContent";
import { mapGetters } from "vuex";

export default {
  components: {
    AppCustomizer,
    LayoutVertical,
  },
  data() {
    return {
      showCustomizer: $themeConfig.layout.customizer,
    }
  },
  computed:{
    ...mapGetters(["SocketId"]),
  },
  watch: {
    SocketId: function (val, oldVal) {
      if ( val !== getUserData().data.user.userId &&
          !this.$route.path.includes("chats")){
        this.$store.commit("SetDefaultSocketId");
        this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `چت`,
            icon: 'MessageIcon',
            variant: 'success',
            text: `شما 1 پیام جدید دارید.`,
          },
        })
      }


      // let tmpLabel = "";
      // if (
      //     val !== this.$auth.user.userId &&
      //     !this.$route.path.includes("chat")
      // ) {
      //   tmpLabel = "شما یک پیام جدید دارید";
      //   console.log(this.ChatNotifs)
      //   this.ChatNotifs.push(tmpLabel);
      //   tmpLabel = "";
      //
      //   this.$store.commit("SetDefaultSocketId");
      //   this.$toast.success("شما یک پیام جدید دارید");
      // }
    },
  }
}
</script>
