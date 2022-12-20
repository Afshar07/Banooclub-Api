<template>
  <div class="col-md-12">
    <div class="row">
      <div class="col-md-12 tw-border-b tw-shadow p-3 bg-white">
        <div class="d-flex align-items-center justify-content-between">
          <div class="d-flex align-items-center gap-2">
            <img v-if="ActiveUser.userPhoto!==null && !$route.query.Photo && ActiveUser.userId!==0 "
                 style="width: 50px; height: 50px;" class="rounded-circle"
                 :src="`https://subapi.banoclub.com/media/gallery/profile/${ActiveUser.userPhoto}`"/>
            <img v-else-if="$route.query.Photo" style="width: 50px; height: 50px;" class="rounded-circle"
                 :src="`https://subapi.banoclub.com/media/gallery/profile/${$route.query.Photo}`"/>
            <img v-else-if="ActiveUser.userPhoto!==null && ActiveUser.groupId!==0" style="width: 50px; height: 50px;"
                 class="rounded-circle"
                 :src="`https://subapi.banoclub.com/media/gallery/group/${ActiveUser.userPhoto}`"/>
            <img v-else style="width: 50px; height: 50px;" class="rounded-circle"
                 src="~/assets/images/defaultUser.png"/>
            <small class="tw-text-[#333] tw-text-[15px] tw-font-bold tw-cursor-pointer" @click="goToUserProfile(ActiveUser)">{{ ActiveUser.userName }}</small>

          </div>
          <button @click="GoBack" class="btn"><i class="fas fa-chevron-left"></i></button>
        </div>

      </div>
      <div class="col-md-12  p-0 tw-p-0">
        <div class="row  m-0  " style="width: 100%">

          <div class="col-md-12 p-0 ChatContainer" @scroll="handleScroll" ref="ChatContainer">
            <div v-if="!lastMessage && isLoading" class="col-md-12 d-flex align-items-center justify-content-center">
              <Spinner></Spinner>
            </div>
            <MyMessageItem v-if="ActiveUser.userId!==0" v-for="(item,idx) in ChatData" :key="idx" :Message="item"/>
            <GroupMessageItem v-if="ActiveUser.groupId!==0" v-for="(item,idx) in ChatData" :key="idx"
                              :Message="item"></GroupMessageItem>
          </div>
          <div class="col-md-12 p-0  bg-white  ">
            <SendMessageInput @GetMessage="sendMessage($event)"></SendMessageInput>
          </div>
        </div>


      </div>
    </div>
  </div>
</template>

<script>
import ContactMessageItem from "@/components/Chat/ContactMessageItem";
import GroupMessageItem from "@/components/Chat/GroupMessageItem";
import MyMessageItem from "@/components/Chat/MyMessageItem";
import ProfileItem from "@/components/Chat/ProfileItem";
import EditIcon from "@/components/Icons/EditIcon";
import {mapGetters} from "vuex";
import SendMessageInput from "@/components/SendMessageInput";
export default {

  components: {ContactMessageItem, SendMessageInput,GroupMessageItem, MyMessageItem, ProfileItem, EditIcon},
  name: "MainChatData",
  props: {
    // Active User Is Selected Chat Header Data. It Handles Both User And Group Type
    ActiveUser: {
      type: Object,
      required: true
    }
  },


  data() {
    return {
      MessageBody: '',
      FirstId: 0,
      SearchConversation: "",
      AllChats: [],
      ActiveUserId:0,
      ChatData: [],
      lastMessage: false,
      isLoading: false,
      Search: ''
    }
  },


  async fetch() {
    this.ActiveUserId = this.ActiveUser.userId
    await this.ReadMessage()
    await this.GetConversationWithScroll()
  },
  computed: {
    ...mapGetters(["SocketId"]),
  },
  watch: {
    ActiveUser:{

      immediate:true,
      async  handler(val){
if(val.userId!==this.ActiveUserId){
  this.ChatData = []
}
    this.$nuxt.refresh();
        await this.ReadMessage();
        await this.GetConversationWithScroll();
      }
    },
    SocketId: {
      immediate: true,
      async handler(val) {
        if (val) {
          await this.ReadMessage()
          await this.GetConversationWithScroll();
          this.$store.commit("SetDefaultSocketId");

        }

      },
    },

  },
  methods: {
    async DeliverMessage() {
      try {
        const res = await this.$repositories.DeliverMessage.DeliverMessage()
      } catch (e) {
        console.log(e)
      }
    },
    async ReadMessage() {
      try {
        const res = this.$repositories.ReadMessage.ReadMessage({
          userId: this.ActiveUser.userId
        })
      } catch (e) {
        console.log(e)
      }
    },
    async GetConversationWithOutScroll() {
      try {
        this.isLoading = true
        // this.ActiveUser = id
        if (!this.lastMessage) {
          const res = await this.$repositories.GetConversation.GetConversation({
            groupId: this.ActiveUser.groupId !== 0 ? this.ActiveUser.groupId : null,
            userId: this.ActiveUser.userId !== 0 ? this.ActiveUser.userId : null,
            count: 20,
            messageId: this.FirstId
          })
          await this.ReadMessage();
          if (res.data.length > 0) {
            res.data.forEach((item) => {
              if (this.ChatData.findIndex(e => e.MessageId === item.MessageId) > -1) {
                return
                // let idx = this.ChatData.findIndex(e=> e.MessageId === item.MessageId)
                // this.ChatData[idx] = item
              } else {
                this.ChatData.push(item)
              }
            })

            this.ChatData = this.ChatData.sort(function (a, b) {
              const key1 = a.CreateDate;
              const key2 = b.CreateDate;

              if (key1 < key2) {
                return -1;
              } else if (key1 === key2) {
                return 0;
              } else {
                return 1;
              }
            });
            this.lastMessage = false

          } else {
            this.lastMessage = true
          }

        } else {

        }

      } catch (e) {

      } finally {
        this.isLoading = false
      }

    },
    async GetConversationWithScroll() {
      try {
        this.isLoading = true
        // this.ActiveUser = id
        if (!this.lastMessage) {
          const res = await this.$repositories.GetConversation.GetConversation({
            groupId: this.ActiveUser.groupId !== 0 ? this.ActiveUser.groupId : null,
            userId: this.ActiveUser.userId !== 0 ? this.ActiveUser.userId : null,
            count: 20,
            messageId: this.FirstId
          })
          if (res.data.length > 0) {
            res.data.forEach((item) => {
              if (this.ChatData.findIndex(e => e.MessageId === item.MessageId) > -1) {
                const idx = this.ChatData.findIndex(e => e.MessageId === item.MessageId)
                this.ChatData[idx] = item
              } else {
                this.ChatData.push(item)
              }
            })

            this.ChatData = this.ChatData.sort(function (a, b) {
              const key1 = a.CreateDate;
              const key2 = b.CreateDate;

              if (key1 < key2) {
                return -1;
              } else if (key1 === key2) {
                return 0;
              } else {
                return 1;
              }
            });
            this.lastMessage = false
          } else {
            this.lastMessage = true
          }
        } else {
        }

      } catch (e) {
        console.log(e)

      } finally {
        this.$nextTick(() => {
          this.scrollToBottom()
        })
        this.isLoading = false

      }

    },
    handleScroll: function ({target: {scrollTop, clientHeight, scrollHeight},}) {

      if (scrollTop === 0) {
        if (this.ChatData.length > 0)
          this.FirstId = this.ChatData[0].MessageId
        this.GetConversationWithOutScroll()

      }

    },
    GoBack() {
      this.$emit('GoBack')
    },
    scrollToBottom() {

      if (this.ChatData.length > 0 && this.$refs.ChatContainer) {

        this.$refs.ChatContainer.scrollTop = this.$refs.ChatContainer.scrollHeight
      }
    },
    async sendMessage(MessageBody) {
      // await this.ReadMessage();

      let userId = null
      if (this.$route.query.userId) {
        userId = this.$route.query.userId
      } else if (this.ActiveUser.userId !== 0) {
        userId = this.ActiveUser.userId
      } else {
        userId = null
      }
      try {
        const res = await this.$repositories.SendMessage.SendMessage({
          messageId: 0,
          subject: MessageBody.length > 20 ? MessageBody.substr(0, 20) : MessageBody,
          messageBody: MessageBody,
          recipientUserId: userId,
          recipientGroupId: this.ActiveUser.groupId !== 0 ? this.ActiveUser.groupId : null

        })
        this.MessageBody = ''
        this.FirstId = 0
        if (this.$route.query.userId) {
          this.GoBack()
        }
        // await this.GetConversation();
      } catch (e) {
        console.log(e)
      } finally {
        await this.GetConversationWithScroll()
        if (this.$route.params.userId) {
          this.GoBack()
        }
        this.scrollToBottom()
      }
    },
    async goToUserProfile(user) {
      if(user.groupId===0){
        try {
          this.$router.push({path: `/user/${user.userName}/posts`});
        } catch (e) {
          console.log(e)
        }
      }

    },
  }
}
</script>

<style scoped>
.ActiveChatClass {
  background-color: #cbcbcb !important;
  transition: .2s ease !important;
  border-radius: 10px;
}

.profile_item:hover {
  background-color: #f3f3f3 !important;
  transition: .2s ease !important;
  border-radius: 10px;
}

.BoxShadow {
  box-shadow: 0 5px 20px rgb(0 0 0 / 12%);
}

.ChatContainer {
  height: 800px;
  max-height: 800px;
  min-height: 800px;
  overflow-y: scroll;
  background: url("/chat-bg.jpg") no-repeat;
  background-size: cover;
  padding: 0 !important;
  position: relative;
}

</style>
