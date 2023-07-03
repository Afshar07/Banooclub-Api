<template>
  <div class="chats">
    <div
      class="chat"
      :class="{'chat-left': chatData.userInfo.userId === admin.userId}"
    >
      <div class="chat-avatar">
        <b-avatar
          size="36"
          class="avatar-border-2 box-shadow-1"
          variant="transparent"
          v-if="chatData.userInfo.userId !== admin.userId"

        />
<!--        :src="chatData.userInfo.userId === formattedChatData.contact.id ? formattedChatData.contact.avatar : profileUserAvatar"-->
      </div>
      <div class="chat-body">
        <div class="chat-content position-relative " :class="{'pb-2': chatData.userInfo.userId === admin.userId}">
          <p>{{ chatData.content }}</p>
          <img v-if="chatData.fileData!==''" :src="`https://api.banooclub.simagar.com/${chatData.fileData}`" style="object-fit: contain;object-position: center" class="my-2" width="200px" height="100px" alt="">
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/composition-api'
import { BAvatar } from 'bootstrap-vue'
import DoubleCheckIcon from "@/views/components/icons/DoubleCheckIcon";
// import { mapGetters } from "vuex";

export default {
  components: {
    DoubleCheckIcon,
    BAvatar,
  },
  props: {
    chatData: {
      type: Object,
      required: true,
    },
    admin: {
      type: Object,
      required: true,
    },
    user: {
      type: Object,
      required: true,
    },
    profileUserAvatar: {
      type: String,
      required: true,
    },
  },
  computed:{
    // ...mapGetters(["SocketId"]),
  },
  mounted() {
    // console.log(this.chatData.userInfo.userId)
  },
  setup(props) {
    const formattedChatData = computed(() => {
      const contact = {
        id: props.chatData.userId,
        avatar: `https://api.halamkhoobee.com/${props.user.selfieFileData}`,
      }

      let chatLog = []
      if (props.chatData.chat) {
        chatLog = props.chatData.chat.chat
      }

      const formattedChatLog = []
      let chatMessageSenderId = chatLog[0] ? chatLog[0].senderId : undefined
      let msgGroup = {
        sender: chatMessageSenderId,
        messages: [],
      }

      chatLog.forEach((msg, index) => {
        if (chatMessageSenderId === msg.senderId) {
          msgGroup.messages.push({
            msg: msg.message,
            time: msg.time,
          })
        } else {
          chatMessageSenderId = msg.senderId
          formattedChatLog.push(msgGroup)
          msgGroup = {
            senderId: msg.senderId,
            messages: [{
              msg: msg.message,
              time: msg.time,
            }],
          }
        }
        if (index === chatLog.length - 1) formattedChatLog.push(msgGroup)
      })

      return {
        formattedChatLog,
        contact,
        profileUserAvatar: props.profileUserAvatar,
      }
    })

    return {
      formattedChatData,
    }
  },
}
</script>

<style scoped>
.ticket-img{
  width: 10rem;
  height: 10rem;
  margin-top: 10px;
  background-size: contain;
  background-repeat: no-repeat;
}
.deliver{
  position: absolute;
  bottom:5px;
  right: 5px;
}
</style>
