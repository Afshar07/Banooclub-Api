<template>
  <b-overlay

      rounded="sm"
  >
    <!-- Need to add height inherit because Vue 2 don't support multiple root ele -->
    <div  style="height: 85vh">
      <div
          class="body-content-overlay"

      />
      <b-modal
          id="modal-edit"
          centered
          ok-only
          ok-title="بستن"
          @ok="ChangeTicketType"
      >
        <span>تغییر واحد پشتیبانی به  </span>
        <v-select
            v-model="ticketType"
            :clearable="false"
            label="option"
            class="bg-white my-2"
            :options="TicketTypesId"
            :reduce="option => option.value"
        />
      </b-modal>
      <!-- Main Area -->
      <section class="chat-app-window w-100">
        <!-- Chat Content -->
        <div

            class="active-chat"
        >
          <!-- Chat Navbar -->
          <div class="chat-navbar">
            <header class="chat-header">

              <!-- Avatar & Name -->
              <div class="d-flex align-items-center">
                <b-avatar
                    v-if="Tickets!== null && Tickets[0].userInfo"
                    size="36"
                    :src="`https://subapi.banoclub.com/${Tickets[0].userInfo.selfieFileData}`"
                    class="mr-1 cursor-pointer badge-minimal"
                />

                <b-avatar
                    v-else
                    size="36"
                    src="@/assets/images/logo/logo-jadid.png"
                    class="mr-1 cursor-pointer badge-minimal"
                />
              </div>
              <div class="d-flex  flex-lg-row flex-column align-items-center gap-2">
                <div class="d-flex align-items-center">
                  <b-button
                      v-b-modal.modal-edit

                      variant="primary"
                  >
                    تغییر واحد پشتیبانی تیکت
                  </b-button>

                </div>
                <div class="d-flex mx-25 align-items-center">
                  <b-button
                      @click="CloseTicket"

                      variant="danger"
                  >
                   بستن تیکت
                  </b-button>

                </div>
                <div class="d-flex  align-items-center ">
                  <b-button
                      variant="primary"
                  >
                    <router-link class="text-white " to="/apps/Ticket/AllTickets">
                      بازگشت
                    </router-link>

                  </b-button>

                </div>

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
                v-if="admin!==null && Tickets!== null && Tickets[0].userInfo"
                v-for="Ticket in Tickets"
                :chat-data="Ticket"
                :profile-user-avatar="`https://subapi.banoclub.com/${Tickets[0].userInfo.selfieFileData}`"
                :admin="admin"
                :user="user"
            />
          </vue-perfect-scrollbar>

          <!-- Message Input -->
          <b-form
              class="chat-app-form"

          >
            <b-input-group class="input-group-merge form-send-message mr-1">
              <b-form-input
                  v-model="content"
                  placeholder="پاسخ شما..."
              />
            </b-input-group>
            <b-button
                @click="SendMessage"
                variant="primary"
                type="button"
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
  BAvatar, BDropdown, BDropdownItem, BForm, BInputGroup, BFormInput, BButton,BOverlay,
} from 'bootstrap-vue'
import VuePerfectScrollbar from 'vue-perfect-scrollbar'
import ClsoeTicket from "@/libs/Api/Ticket/ClsoeTicket";
import { $themeBreakpoints } from '@themeConfig'
import { useResponsiveAppLeftSidebarVisibility } from '@core/comp-functions/ui/app'
import ChatLeftSidebar from '../../chat/ChatLeftSidebar.vue'
import chatStoreModule from '../../chat/chatStoreModule'
import ChatActiveChatContentDetailsSidedbar from '../../chat/ChatActiveChatContentDetailsSidedbar.vue'
import ChatLog from '../../chat/ChatLog.vue'
import useChat from '../../chat/useChat'

import vSelect from "vue-select";
import TicketGetAllByParentIdRequest from "@/libs/Api/Ticket/TicketGetAllByParentIdRequest";
import TicketCreateRequest from "@/libs/Api/Ticket/TicketCreateRequest";
import UsersGetByTokenRequest from "@/libs/Api/Users/UsersGetByTokenRequest";
import ChangeTicketType from "@/libs/Api/Ticket/ChangeTicketType";
import { mapGetters } from "vuex";
import UsersGetRequest from '@/libs/Api/Users/UsersGetRequest'
import ToastificationContent from "@core/components/toastification/ToastificationContent";

export default {
  data(){
    return{
      ticketId:this.$route.params.id,
      user:null,
      Tickets:null,
      admin:null,
      refreshTicket:null,
      TicketTypesId:[

        {
          option:"احراز هویت",
          value:1
        },
        {
          option:"پشتیبانی",
          value:2
        },
        {
          option:"سایر",
          value:3
        },



      ],
      ticketType:0,
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
  },
  watch:{

  },
  async created(){
    await  this.GetAllByParentId();
    await  this.getAdmin()

  },

  methods:{
    async CloseTicket(){
      let _this = this
      let  clsoeTicket = new  ClsoeTicket(_this)
      let data = {
        parentId:this.Tickets[0].ticketId,
      }
      clsoeTicket.setParams(data)
      await clsoeTicket.fetch((content)=>{
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `واحد پشتیبانی تیکت تغییر یافت`,
          },
        })
        _this.$router.push('/apps/Ticket/AllTickets')

      },(e)=>{
        console.log(e)
      })
    },
    async ChangeTicketType(){

      let _this = this
      let  changeTicketType = new  ChangeTicketType(_this)
      let data = {
        parentId:this.Tickets[0].ticketId,
        ticketType:_this.ticketType
      }
      changeTicketType.setParams(data)
      await changeTicketType.fetch((content)=>{
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `واحد پشتیبانی تیکت تغییر یافت`,
          },
        })
        _this.$router.push('/apps/Ticket/AllTickets')

      },(e)=>{
        console.log(e)
      })
    },
    async GetAllByParentId(){
      let _this = this
      let  ticketGetAllByParentIdRequest = new  TicketGetAllByParentIdRequest(_this)
      ticketGetAllByParentIdRequest.setId(_this.ticketId)
      await ticketGetAllByParentIdRequest.fetch((content)=>{
          _this.Tickets = content
        _this.GetUser(content[0])
      },(e)=>{
        console.log(e)
      })
    },
    async SendMessage(){
  let _this = this
      let ticketCreateRequest = new TicketCreateRequest(_this)
      let data = {
        parentId: this.$route.params.id,
            content: this.content,
            type: this.Tickets[0].type,
            title: this.Tickets[0].title,
            createDate:new Date(Date.now()),
        recipientIds:[],
        recipientUserId:this.Tickets[0].userId
      }
      ticketCreateRequest.setRequestParamDataObj(data)
      await ticketCreateRequest.fetch(()=>{

        _this.GetAllByParentId();
        this.content = ''
      },(e)=>{
        console.log(e)
      })

    },
    async GetUser(data){
      let _this = this;

        let usersGetRequest = new UsersGetRequest(_this);
        usersGetRequest.setUserId(data.userInfo.userId);
        await usersGetRequest.fetch(function (content){
          _this.user = content

        },function (error){
          console.log(error)
        })

    },
    async getAdmin(){
      let _this = this;
      let usersGetByTokenRequest = new UsersGetByTokenRequest(_this);
      await usersGetByTokenRequest.fetch(function (content){
        _this.admin = content

      },function (error){
        console.log(error);
      })
    },

  },
  components: {

    // BSV
    BAvatar,
    BDropdown,
    BDropdownItem,
    BForm,
    vSelect,
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
