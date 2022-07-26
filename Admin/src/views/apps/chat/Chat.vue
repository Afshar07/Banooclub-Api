<template>
  <b-overlay
      :show="messages === null"
      rounded="sm"
  >
  <!-- Need to add height inherit because Vue 2 don't support multiple root ele -->
    <div v-if="messages !== null && admin !== null" style="height: 85vh">
    <div
      class="body-content-overlay"

    />

    <!-- Main Area -->
    <section class="chat-app-window w-100">

      <!-- Start Chat Logo -->
<!--      <div-->
<!--        v-if="!activeChat.contact"-->
<!--        class="start-chat-area"-->
<!--      >-->
<!--        <div class="mb-1 start-chat-icon">-->
<!--          <feather-icon-->
<!--            icon="MessageSquareIcon"-->
<!--            size="56"-->
<!--          />-->
<!--        </div>-->
<!--        <h4-->
<!--          class="sidebar-toggle start-chat-text"-->
<!--          @click="startConversation"-->
<!--        >-->
<!--          Start Conversation-->
<!--        </h4>-->
<!--      </div>-->

      <!-- Chat Content -->
      <div

        class="active-chat"
      >
        <!-- Chat Navbar -->
        <div class="chat-navbar">
          <header class="chat-header">

            <!-- Avatar & Name -->
            <div class="d-flex align-items-center">

              <!-- Toggle Icon -->
<!--              <div class="sidebar-toggle d-block d-lg-none mr-1">-->
<!--                <feather-icon-->
<!--                  icon="MenuIcon"-->
<!--                  class="cursor-pointer"-->
<!--                  size="21"-->
<!--                  @click="mqShallShowLeftSidebar = true"-->
<!--                />-->
<!--              </div>-->

              <b-avatar
                size="36"
                :src="`https://api.halamkhoobee.com/${user.selfieFileData}`"
                class="mr-1 cursor-pointer badge-minimal"
              />
              <h6 class="mb-0">
                {{ user.name }} {{ user.familyName }}
              </h6>
            </div>
          </header>
        </div>

        <!-- User Chat Area -->
        <vue-perfect-scrollbar
          ref="refChatLogPS"
          :settings="perfectScrollbarSettings"
          class="user-chats scroll-area"
        >


          <chat-log
            v-for="message in messages"
            :chat-data="message"
            :profile-user-avatar="`https://api.halamkhoobee.com/${admin.selfieFileData}`"
            :admin="admin"
            :user="user"
          />
        </vue-perfect-scrollbar>

        <!-- Message Input -->
        <b-form
          class="chat-app-form"
          @submit.prevent="SendMessage"
        >
          <b-input-group class="input-group-merge form-send-message mr-1">
            <b-form-input
              v-model="content"
              placeholder="پاسخ شما..."
            />
          </b-input-group>
          <b-button
            variant="primary"
            type="submit"
          >
            ارسال
          </b-button>
        </b-form>
      </div>
    </section>
  </div>
  </b-overlay>
</template>

<script>
import store from '@/store'
import {
  ref, onUnmounted, nextTick,
} from '@vue/composition-api'
import {
  BAvatar, BDropdown, BDropdownItem, BForm, BInputGroup, BFormInput, BButton,BOverlay
} from 'bootstrap-vue'
import VuePerfectScrollbar from 'vue-perfect-scrollbar'
// import { formatDate } from '@core/utils/filter'
import { $themeBreakpoints } from '@themeConfig'
import { useResponsiveAppLeftSidebarVisibility } from '@core/comp-functions/ui/app'
import ChatLeftSidebar from './ChatLeftSidebar.vue'
import chatStoreModule from './chatStoreModule'
import ChatActiveChatContentDetailsSidedbar from './ChatActiveChatContentDetailsSidedbar.vue'
import ChatLog from './ChatLog.vue'
import useChat from './useChat'
import UsersGeByTokenRequest from "@/libs/Api/Users/UsersGetByTokenRequest";
import MessageGetConversationRequest from "@/libs/Api/Message/MesaageGetConversationRequest";
import MessageSendRequest from "@/libs/Api/Message/MessageSendRequest";
import UsersGetRequest from "@/libs/Api/Users/UsersGetRequest";
import MessageReadRequest from "@/libs/Api/Message/MessageReadRequest";
import { mapGetters } from "vuex";

export default {
  data(){
    return{
      userId:this.$route.params.id,
      user:null,
      messages:null,
      admin:null,
      refreshTicket:null,
      content:'',
    }
  },
  computed:{
    currentDate(){
      let today = new Date();
      let date = today.getFullYear()+'-'+(today.getMonth()+1)+'-'+today.getDate();
      let time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
      return  date+' '+time;
    },
    ...mapGetters(["SocketId"]),
  },
  watch:{
    SocketId: function (val, oldVal) {
      this.getConversation();
      this.$store.commit("SetDefaultSocketId");
    },
  },
  async created(){
    await this.getConversation();
    await this.getUser();
    await this.getAdmin();
    // this.refreshTiCket = setInterval(this.getConversation,20000);
    // console.log(this.$store.state.message.SocketUserId)
  },
  // beforeDestroy() {
  //   clearInterval(this.refreshTiCket);
  // },
  methods:{
    async getConversation(){
      let _this = this;
      let data = {
        userId : _this.$route.params.id,
        count : 20
      }

      let messageGetConversationRequest = new MessageGetConversationRequest(_this);
      messageGetConversationRequest.setData(data)
      await messageGetConversationRequest.fetch(function (content){
        _this.messages = content
        _this.readMessages();
        _this.messages.sort(function (a, b) {
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
      },function (error){
        console.log(error);
      })
    },
    async getUser(){
      let _this = this;

      if (_this.messages !== null){
        let usersGetRequest = new UsersGetRequest(_this);
        usersGetRequest.setUserId(_this.userId);
        await usersGetRequest.fetch(function (content){
          _this.user = content
        },function (error){
          console.log(error)
        })
      }
    },
    async getAdmin(){
      let _this = this;

      let usersGeByTokenRequest = new UsersGeByTokenRequest(_this);
      await usersGeByTokenRequest.fetch(function (content){
        _this.admin = content
      },function (error){
        console.log(error);
      })
    },
    async SendMessage(){
      let _this = this;
      let createMessageData={
        messageId: 0,
        isDeleted: false,
        subject:
            _this.content.length > 20
                ? _this.content.substr(0, 20)
                : _this.content,
        messageBody: _this.content,
        parentMessageId: null,
        recipientUserId: Number(_this.$route.params.id),
        recipientGroupId: null,
        isForwarded: false,
        createDate: _this.currentDate,
        userId: 0,
      }

      let messageSendRequest = new MessageSendRequest(_this);
      messageSendRequest.setParams(createMessageData)
      await messageSendRequest.fetch(function (content){
       _this.getConversation();
       _this.content = '';
      },function (error){
        console.log(error);
      })
    },
    async readMessages(){
      let _this = this;

      let messageReadRequest = new MessageReadRequest(_this);
      messageReadRequest.setId(_this.userId);
      await messageReadRequest.fetch(function (conntent){

      },function (error){
        console.log(error)
      })
    },
  },
  components: {

    // BSV
    BAvatar,
    BDropdown,
    BDropdownItem,
    BForm,
    BInputGroup,
    BFormInput,
    BButton,
    BOverlay,

    // 3rd Party
    VuePerfectScrollbar,

    // SFC
    ChatLeftSidebar,
    ChatActiveChatContentDetailsSidedbar,
    ChatLog,
  },
  setup() {
    const CHAT_APP_STORE_MODULE_NAME = 'app-chat'

    // Register module
    if (!store.hasModule(CHAT_APP_STORE_MODULE_NAME)) store.registerModule(CHAT_APP_STORE_MODULE_NAME, chatStoreModule)

    // UnRegister on leave
    onUnmounted(() => {
      if (store.hasModule(CHAT_APP_STORE_MODULE_NAME)) store.unregisterModule(CHAT_APP_STORE_MODULE_NAME)
    })

    const { resolveAvatarBadgeVariant } = useChat()

    // Scroll to Bottom ChatLog
    const refChatLogPS = ref(null)
    const scrollToBottomInChatLog = () => {
      const scrollEl = refChatLogPS.value.$el || refChatLogPS.value
      scrollEl.scrollTop = scrollEl.scrollHeight
    }

    // ------------------------------------------------
    // Chats & Contacts
    // ------------------------------------------------
    const chatsContacts = ref([])
    const contacts = ref([])

    const fetchChatAndContacts = () => {
      store.dispatch('app-chat/fetchChatsAndContacts')
        .then(response => {
          chatsContacts.value = response.data.chatsContacts
          contacts.value = response.data.contacts
          // eslint-disable-next-line no-use-before-define
          profileUserDataMinimal.value = response.data.profileUser
        })
    }

    fetchChatAndContacts()

    // ------------------------------------------------
    // Single Chat
    // ------------------------------------------------
    const activeChat = ref({})
    const chatInputMessage = ref('')
    const openChatOfContact = userId => {
      // Reset send message input value
      chatInputMessage.value = ''

      store.dispatch('app-chat/getChat', { userId })
        .then(response => {
          activeChat.value = response.data

          // Set unseenMsgs to 0
          const contact = chatsContacts.value.find(c => c.id === userId)
          if (contact) contact.chat.unseenMsgs = 0

          // Scroll to bottom
          nextTick(() => { scrollToBottomInChatLog() })
        })

      // if SM device =>  Close Chat & Contacts left sidebar
      // eslint-disable-next-line no-use-before-define
      mqShallShowLeftSidebar.value = false
    }
    const sendMessage = () => {
      if (!chatInputMessage.value) return
      const payload = {
        contactId: activeChat.value.contact.id,
        // eslint-disable-next-line no-use-before-define
        senderId: profileUserDataMinimal.value.id,
        message: chatInputMessage.value,
      }
      store.dispatch('app-chat/sendMessage', payload)
        .then(response => {
          const { newMessageData, chat } = response.data

          // ? If it's not undefined => New chat is created (Contact is not in list of chats)
          if (chat !== undefined) {
            activeChat.value = { chat, contact: activeChat.value.contact }
            chatsContacts.value.push({
              ...activeChat.value.contact,
              chat: {
                id: chat.id,
                lastMessage: newMessageData,
                unseenMsgs: 0,
              },
            })
          } else {
            // Add message to log
            activeChat.value.chat.chat.push(newMessageData)
          }

          // Reset send message input value
          chatInputMessage.value = ''

          // Set Last Message for active contact
          const contact = chatsContacts.value.find(c => c.id === activeChat.value.contact.id)
          contact.chat.lastMessage = newMessageData

          // Scroll to bottom
          nextTick(() => { scrollToBottomInChatLog() })
        })
    }

    const perfectScrollbarSettings = {
      maxScrollbarLength: 150,
    }

    // User Profile Sidebar
    // ? Will contain all details of profile user (e.g. settings, about etc.)
    const profileUserData = ref({})
    // ? Will contain id, name and avatar & status
    const profileUserDataMinimal = ref({})

    const shallShowUserProfileSidebar = ref(false)
    const showUserProfileSidebar = () => {
      store.dispatch('app-chat/getProfileUser')
        .then(response => {
          profileUserData.value = response.data
          shallShowUserProfileSidebar.value = true
        })
    }

    // Active Chat Contact Details
    const shallShowActiveChatContactSidebar = ref(false)

    // UI + SM Devices
    // Left Sidebar Responsiveness
    const { mqShallShowLeftSidebar } = useResponsiveAppLeftSidebarVisibility()
    const startConversation = () => {
      if (store.state.app.windowWidth < $themeBreakpoints.lg) {
        mqShallShowLeftSidebar.value = true
      }
    }

    return {
      // Filters
      // formatDate,

      // useChat
      resolveAvatarBadgeVariant,

      // Chat & Contacts
      chatsContacts,
      contacts,

      // Single Chat
      refChatLogPS,
      activeChat,
      chatInputMessage,
      openChatOfContact,
      sendMessage,

      // Profile User Minimal Data
      profileUserDataMinimal,

      // User Profile Sidebar
      profileUserData,
      shallShowUserProfileSidebar,
      showUserProfileSidebar,

      // Active Chat Contact Details
      shallShowActiveChatContactSidebar,

      // UI
      perfectScrollbarSettings,

      // UI + SM Devices
      startConversation,
      mqShallShowLeftSidebar,
    }
  },
}
</script>

<style lang="scss" scoped>

</style>

<style lang="scss">
@import "~@core/scss/base/pages/app-chat.scss";
@import "~@core/scss/base/pages/app-chat-list.scss";
</style>
