<template>
  <div class="row px-5 py-0 p-md-2 p-0 d-flex align-items-center justify-content-center">
    <div v-if="!ChatSelected" class="col-md-5 lg:tw-border-l tw-shadow" style="height: 800px;overflow-y: scroll">
      <div class="row">
        <div class="col-md-12">
          <h1 class="h3">مکالمات من </h1>
        </div>
        <div class="col-md-12 border-bottom pb-2 my-2">
          <input type="search" v-model="Search" class="form-control" placeholder="جستجو">
        </div>
        <div v-for="(item,idx) in FilteredChats" :key="idx"  @click="SetActiveUser(item)" :class="GetActiveChatClass(item.userId)" class="col-md-12 profile_item">
          <ProfileItem    :UserData="item"/>
        </div>
      </div>
    </div>
    <MainChatData @GoBack="RemoveChatData" @SocketListen="SocketListen"  v-if="ChatSelected" :ActiveUser="ActiveUser"  ></MainChatData>
  </div>

</template>

<script>
import EditIcon from "../../components/Icons/EditIcon";
import ProfileItem from "../../components/Chat/ProfileItem";
import MyMessageItem from "../../components/Chat/MyMessageItem";
import ContactMessageItem from "../../components/Chat/ContactMessageItem";
import {mapGetters} from "vuex";
import MainChatData from "../../components/MainChatData";


export default {
  name: "chat",
  layout: "PoshtebamPlusLayout",
  components: {ContactMessageItem, MyMessageItem, ProfileItem, EditIcon,MainChatData },
  data(){
    return{
      SearchConversation: "",
      AllChats:[],
      ActiveUser:0,
      ChatData:[],
      MessageBody:'',
      FirstId:0,
      lastMessage:false,
      isLoading:false,
      Search:'',
      ChatSelected:false,


    }
  },

  computed:{
    FilteredChats()
    {
      const data = this.AllChats
      if(this.Search===''){
        return data
      }else{
        return   this.AllChats.filter((element)=>{
          return element.userName.match(this.Search)
        })
      }
    } ,
    ...mapGetters(["SocketId"]),
  },
  watch:{
    SocketId: {
      immediate: true,
      async handler(val) {
        if(val){
          await this.DeliverMessage();
          await this.GetMenu();
        }

      },
    },

  },
  methods:{
    // Main Methods
    async GetMenu(){

      try {
        const res = await this.$repositories.GetMenu.GetMenu()
        this.AllChats = res.data
        this.lastMessage = false
      }catch (e) {
        console.log(e)
      }finally {

      }
    },
    async DeliverMessage(){
      try {
        const res = await this.$repositories.DeliverMessage.DeliverMessage()
      }catch (e) {
        console.log(e)
      }
    },
    // Component methods
    async  SocketListen(){
      await this.GetMenu();
    },
    RemoveChatData(){
      this.ChatSelected = false
      this.ChatData = []
      this.lastMessage = false
      this.FirstId = 0
      this.$router.push(this.$route.path)
    },
    SetActiveUser(item){
      this.ActiveUser = item
      this.ChatData = []
      this.lastMessage = false
      this.FirstId = 0
      this.ChatSelected = true
    },
    GetActiveChatClass(id){
      if(id === this.ActiveUser.userId){
        return 'ActiveChatClass'
      }
    },

  },


  async fetch ( ){
    if(this.$route.query.userId){
      this.ChatSelected = true
    }
    await this.DeliverMessage();
    await this.GetMenu();



  },
}
</script>

<style scoped>
.ActiveChatClass{
  background-color: #cbcbcb !important;
  transition: .2s ease !important;
  border-radius: 10px;
}
.profile_item:hover{
  background-color: #f3f3f3 !important;
  transition: .2s ease !important;
  border-radius: 10px;
}
.BoxShadow {
  box-shadow: 0 5px 20px rgb(0 0 0 / 12%);
}

.ChatContainer{
  height: 700px;
  max-height: 700px;
  min-height: 700px;
  overflow-y: scroll;
  background: url("../../static/chat-bg.jpg") no-repeat;
  background-size: cover;
  padding: 0!important;
  position: relative;
}

.SendMessageContainer{
  position: sticky;
  bottom: 0;
  box-shadow: 0 5px 20px rgb(0 0 0 / 12%);
  width: 100%;
  z-index: 9999999999999999999999999999!important;
}

</style>
