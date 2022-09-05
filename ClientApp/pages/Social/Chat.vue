<template>
<!--Loading Skeleton Classes In Style-->
  <!--I had No idea How to Handle Height Correctly. Open To Contribute :D-->
  <div :class="$fetchState.pending?'loading-skeleton':''" class="row ">
    <div class="d-lg-block d-none ">
      <div class="row bg-white rounded shadow" >
        <!--Desktop Chat-->
        <div class="col-md-3 " style="border-left: solid 1px lightgrey !important">
          <div class="row">
            <div class="col-md-12 border-bottom pb-2 my-2">
              <input type="search" v-model="Search" class="SearchStyle" placeholder="جستجو">
            </div>
            <!--SideBar Desktop For Chat Items. It Takes One UserData Prop With Local Search Command-->
            <div v-if="FilteredChats.length>0" v-for="(item,idx) in FilteredChats" @click="SetActiveUser(item)"
                 :class="GetActiveChatClass(item)" :key="idx"  class="col-md-12 profile_item">
              <ProfileItem :UserData="item"/>
            </div>
          </div>
        </div>
        <!--Desktop Main Chat-->
        <div class="col-md-9 px-0" style="background: url('/chat-bg.jpg') no-repeat center;background-size: cover">
          <!--It Returns One Event => GoBack Removes EveryThing From Current Chat, ChatSelected Boolean Is Required For Handling Show/Hide Chat-->
          <DesktopChat class="w-100 h-100"  @GoBack="RemoveChatData" v-if="ChatSelected" :ActiveUser="ActiveUser"></DesktopChat>
          <div   class="d-flex align-items-center justify-content-center w-100 h-100 " v-if="!ChatSelected">
            <span class="text-secondary">مکالمه ای انتخاب نشده است</span>
          </div>
        </div>
      </div>
    </div>
    <!--Mobile Main Chat-->
    <div v-if="!ChatSelected" class="  d-lg-none d-block col-md-5 lg:tw-border-l bg-white tw-shadow" style="height: 800px;overflow-y: scroll">
      <div class="row">
        <div class="col-md-12">
          <h1 class="h3 mt-3">چت من </h1>
        </div>
        <div class="col-md-12 border-bottom pb-2 my-2">
          <input type="search" v-model="Search" class="SearchStyle" placeholder="جستجو">
        </div>

        <div v-if="FilteredChats.length>0" v-for="(item,idx) in FilteredChats" :key="idx" @click="SetActiveUser(item)"
             :class="GetActiveChatClass(item)" class="col-md-12 profile_item">
          <ProfileItem :UserData="item"/>
        </div>
        <div v-if="FilteredChats.length===0" class="col-md-12 d-flex align-items-center justify-content-center mt-2">
          <small class="text-warning">مکالمه ای با این مشخصات یافت نشد</small>
        </div>
      </div>

    </div>
    <!--It Returns One Event => GoBack Removes EveryThing From Current Chat, ChatSelected Boolean Is Required For Handling Show/Hide Chat-->
    <MainChatData class="d-lg-none d-block" @GoBack="RemoveChatData" v-if="ChatSelected" :ActiveUser="ActiveUser"></MainChatData>
  </div>

</template>

<script>
import EditIcon from "../../components/Icons/EditIcon";
import ProfileItem from "../../components/Chat/ProfileItem";
import MyMessageItem from "../../components/Chat/MyMessageItem";
import ContactMessageItem from "../../components/Chat/ContactMessageItem";
import {mapGetters} from "vuex";
import MainChatData from "../../components/MainChatData";
import DesktopChat from "@/components/Chat/DesktopChat";

export default {
  name: "chat",
  layout: "PoshtebamPlusLayout",
  components: {ContactMessageItem,DesktopChat, MyMessageItem, ProfileItem, EditIcon, MainChatData},
  data() {
    return {
      SearchConversation: "",
      AllChats: [],
      ActiveUser: 0,
      ChatData: [],
      MessageBody: '',
      FirstId: 0,
      lastMessage: false,
      isLoading: false,
      Search: '',
      ChatSelected: false,


    }
  },
  head() {
    return {
      title: 'چت  من'
    }
  },
  computed: {
    FilteredChats() {
      const data = this.AllChats
      if (this.Search === '') {
        return data
      } else {
        return this.AllChats.filter((element) => {
          return element.userName.match(this.Search)
        })
      }
    },
    ...mapGetters(["SocketId"]),
  },
  watch: {
    SocketId: {
      immediate: true,
      async handler(val) {
        if (val) {
          await this.DeliverMessage();
          await this.GetMenu();
        }

      },
    },

  },
  methods: {
    // Main Methods
    async GetMenu() {

      try {
        const res = await this.$repositories.GetMenu.GetMenu()
        this.AllChats = res.data
        this.lastMessage = false
      } catch (e) {
        console.log(e)
      } finally {

      }
    },
    async DeliverMessage() {
      try {
        const res = await this.$repositories.DeliverMessage.DeliverMessage()
      } catch (e) {
        console.log(e)
      }
    },
    // Component methods
    async SocketListen() {
      await this.GetMenu();
    },
    RemoveChatData() {
      this.ChatSelected = false
      this.ChatData = []
      this.lastMessage = false
      this.FirstId = 0
      if (this.$route.query.userId) {
        this.$router.push(this.$route.path)
      }
    },
    SetActiveUser(item) {
      this.ActiveUser = item
      this.ChatData = []
      this.lastMessage = false
      this.FirstId = 0
      this.ChatSelected = true
    },
    GetActiveChatClass(item) {
      if (item.userId !== 0) {
        if (item.userId === this.ActiveUser.userId) {
          return 'ActiveChatClass'
        }
      } else if (item.groupId !== 0) {
        if (item.groupId === this.ActiveUser.groupId) {
          return 'ActiveChatClass'
        }
      }

    },

  },


  async fetch() {
    if (this.$route.query.userId) {
      this.ChatSelected = true
    }
    await this.DeliverMessage();
    await this.GetMenu();


  },
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
  height: 700px;
  max-height: 700px;
  min-height: 700px;
  overflow-y: scroll;
  background: url("../../static/chat-bg.jpg") no-repeat;
  background-size: cover;
  padding: 0 !important;
  position: relative;
}

.SendMessageContainer {
  position: sticky;
  bottom: 0;
  box-shadow: 0 5px 20px rgb(0 0 0 / 12%);
  width: 100%;
  z-index: 9999999999999999999999999999 !important;
}

/*<!-- Loading Skeleton -->*/
.loading-skeleton h1, .loading-skeleton h2, .loading-skeleton h3, .loading-skeleton h4, .loading-skeleton h5, .loading-skeleton h6, .loading-skeleton p, .loading-skeleton li, .loading-skeleton .btn, .loading-skeleton span, .loading-skeleton small, .loading-skeleton i, .loading-skeleton a, .loading-skeleton strong, .loading-skeleton label, .loading-skeleton .form-control {
  color: transparent;
  appearance: none;
  -webkit-appearance: none;
  background-color: #eee;
  border-color: #eee;
}
.loading-skeleton h1::placeholder, .loading-skeleton h2::placeholder, .loading-skeleton h3::placeholder, .loading-skeleton h4::placeholder, .loading-skeleton h5::placeholder, .loading-skeleton h6::placeholder, .loading-skeleton p::placeholder, .loading-skeleton li::placeholder, .loading-skeleton .btn::placeholder, .loading-skeleton span::placeholder, .loading-skeleton small::placeholder, .loading-skeleton i::placeholder, .loading-skeleton a::placeholder, .loading-skeleton strong::placeholder, .loading-skeleton label::placeholder, .loading-skeleton .form-control::placeholder {
  color: transparent;
}
@keyframes loading-skeleton {
  from {
    opacity: 0.4;
  }
  to {
    opacity: 1;
  }
}
.loading-skeleton {
  pointer-events: none;
  animation: loading-skeleton 1s infinite alternate;
}
.loading-skeleton img {
  filter: grayscale(100) contrast(0%) brightness(1.8);
}
.loading-skeleton h1, .loading-skeleton h2, .loading-skeleton h3, .loading-skeleton h4, .loading-skeleton h5, .loading-skeleton h6, .loading-skeleton p, .loading-skeleton li, .loading-skeleton .btn, .loading-skeleton span, .loading-skeleton small, .loading-skeleton i, .loading-skeleton a, .loading-skeleton strong, .loading-skeleton label, .loading-skeleton .form-control {
  color: transparent !important;
}
.loading-skeleton span {
  background-color: transparent !important;
}
.loading-skeleton div {
  background-image: none !important;
  color: transparent !important;
}


</style>
