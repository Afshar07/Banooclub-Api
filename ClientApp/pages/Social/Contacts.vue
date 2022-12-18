<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="mcontainer NewBg  px-3">
    <div class="row p-3 my-3 rounded tw-shadow bg-white rounded-top">
      <div class="col-md-12">
        <input
          class="SearchStyle tw-shadow"
          type="text"
          placeholder="جستجو کاربران"
          v-model="searchKey"
        />
      </div>
      <div class="col-md-12" style="height: 700px;overflow-y: scroll;" @scroll="handleScroll">
        <div v-if="AllUsers.length>0" class="row">
          <div class="col-md-3 col-12" v-for="(item,index) in AllUsers" :key="index">
            <LazyUserItem class="my-2 tw-shadow tw-rounded-xl " :userDetails="item"/>
          </div>
          <LazySpinner style="text-align: center" v-if="Atbottom"/>
        </div>
        <div v-else class="row">
          <div class="col-12 d-flex align-items-center justify-content-center" >
            <small class="text-warning">کاربری  با این مشخصات یافت نشد</small>
          </div>

        </div>
      </div>
    </div>

  </div>
</template>

<script>


export default {

  name: "Contacts",
  layout: "PoshtebamPlusLayout",
  head() {
    return {
      title: 'دوست یاب',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'دوست یاب',
        },
      ],
    }
  },

  data() {
    return {
      searchKey: '',
      AllUsers: [],
      AllUsersCount:0,
      lastUserId: 0,
      Atbottom: false,
      userId:0
    };
  },
  fetchOnServer() {
    return true;
  },
  async fetch() {
      try {
        const response = await this.$repositories.getAllUsersForUser.getAllUsersForUser(
          {
            userId:0,
            count:20,
            search:this.searchKey
          }
        );
        if(response.data){
          console.log(response.data)
          this.Atbottom = true
          response.data.forEach((item)=>{
            if(this.AllUsers.findIndex(e=> e.UserId === item.UserId)>-1){
            return
            }else{
              this.AllUsers.push(item)
            }
          })
        this.AllUsersCount = this.AllUsers.length;
        }else{
          this.Atbottom = false
        }

      }catch (error) {
        console.log(error);
      }
  },
  computed: {
    BaseUrl() {
      return process.env.pic;
    },
  },
  watch: {
    searchKey(){
        this.GetAllUsers()
    }
  },
  methods: {
    async GetAllUsers(){
      try {
        const response = await this.$repositories.getAllUsersForUser.getAllUsersForUser(
          {
            userId:this.userId,
            count:10,
            search:this.searchKey
          }
        );

        this.AllUsers = response.data;

        this.AllUsersCount = this.AllUsers.length;
      }catch (error) {
        console.log(error);
      }
    },

    async getUsersForInfiniteScroll(id){
      console.log(id)
      try {
        const response = await this.$repositories.getAllUsersForUser.getAllUsersForUser(
          {
            userId:id,
            count:10,
            search:''
          }
        );
        if(response.data.length>0){
          this.Atbottom = true
          response.data.forEach((item)=>{
            if(this.AllUsers.findIndex(e=> e.UserId === item.UserId)>-1){
              return
            }else{
              this.AllUsers.push(item)
            }
          })
          this.AllUsersCount = this.AllUsers.length;
        }else{
          this.Atbottom = false
        }

      }catch (error) {
        console.log(error);
      }
    },
    handleScroll(event){

      if (event.target.offsetHeight + event.target.scrollTop >= event.target.scrollHeight) {
     const id = this.AllUsers[this.AllUsers.length-1].UserId

         this.getUsersForInfiniteScroll(id)
      }

    },
    async ChangeRoute(item) {
      console.log(item);
      this.$store.commit("SetSearchedUserId", item.UserId);
      this.$router.push({
        path: `/user/${item.UserId}/posts`,
      });
      this.searchKey = null;
    },
    async FollowReq(id) {
      try {
        const response =
          await this.$repositories.createAFollowRequest.createAFollowRequest(
            id
          );
        if (response.data) {
          this.$toast.success("درخواست دوستی شما ارسال شد");
          this.GetAllUsers();
        } else {
          this.$toast.error("عملیات قابل اجرا نیست");
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
};
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

.searchFriend {
  border: 1px solid #eaeaea;
  color: #575757;
  font-size: 14px;
  padding: 5px 10px;
  width: 100%;
  margin-bottom: 20px;
}

.ContactsSearch {
  width: 100%;
  border: solid 1px #ccc;
  border-radius: 50px;
  border-right: none;
  border-bottom-right-radius: 0px;
  border-top-right-radius: 0px;
}

.SingleContact {
  transition: 0.2s ease;
  border-radius: 5px;
}

.SingleContact:hover {
  cursor: pointer;
  transform: scale(1.01);

  box-shadow: 1px 1px 1px rgba(0, 0, 0, 0.15), 0 0 1px rgba(0, 0, 0, 0.15);
}

.ContactImg {
  width: 100px;
  height: 100px;
  border-radius: 50px;
}

.ContactsSearchBtn {
  border: solid 1px #ccc;
  border-radius: 50px;
  border-left: none;
  border-bottom-left-radius: 0px;
  border-top-left-radius: 0px;
}

@media only screen and (max-width: 768px) {
  .content-width {
    min-width: 100%;
    max-width: 100%;
  }
}

@media only screen and (min-width: 769px) {
  .content-width {
    min-width: 20rem;
    max-width: 20rem;
  }
}
</style>
