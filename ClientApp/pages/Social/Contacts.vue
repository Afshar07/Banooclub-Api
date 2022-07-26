<template>
  <div class="mcontainer px-3">
    <div class="row p-3 rounded-top">
      <div class="col-md-12">
        <input
          class="searchFriend"
          type="text"
          placeholder="جست‌وجو کاربران"
          v-model="searchKey"
        />
      </div>
    </div>
      <div class="row">
        <div class="col-md-12" style="height: 900px;overflow-y: scroll;" @scroll="handleScroll">
          <div class="row">
            <div class="col-12 col-md-6" v-for="(item,index) in AllUsers" :key="index">
              <UserItem class="my-2" :userDetails="item"/>
            </div>
            <Spinner style="text-align: center" v-if="AllUsers && AllUsers.length !== AllUsersCount"/>
          </div>
        </div>
      </div>
  </div>
</template>

<script>
import UserItem from "../../components/UserItem"
import Spinner from "../../components/Spinner"


export default {

  name: "Contacts",
  layout: "PoshtebamPlusLayout",
  head() {
    return {
      title: 'پیدا کردن دوست',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'پیدا کردن دوست',
        },
      ],
    }
  },
  components: {
    UserItem,
    Spinner
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
          userId:this.userId,
          count:20,
          search:this.searchKey
        }
      );
      console.log('response',response)
      this.AllUsers = response.data;
      console.log('this.AllUsers',this.AllUsers)
      this.AllUsersCount = this.AllUsers.length;
    }catch (error) {
      console.log(error);
    }
  },

  // async fetch() {
  //   const response =
  //     await this.$repositories.getAllUsersForUser.getAllUsersForUser(
  //       this.lastUserId,
  //       this.searchKey
  //     );
  //   if (this.lastUserId !== 0) {
  //     response.data.forEach((item) => {
  //       this.AllUsers.push(item);
  //     });
  //   } else {
  //     this.AllUsers = response.data;
  //   }
  //   console.log('this.AllUsers', this.AllUsers)
  // },

  computed: {
    BaseUrl() {
      return process.env.pic;
    },
  },
  watch: {
    searchKey(){
        this.$fetch()
    }

    // searchKey: function (val, oldVal) {
    //   if (!val || val == "") {
    //     this.searchKey = null;
    //   }
    //   this.$nextTick(() => {
    //     this.$nuxt.$loading.start();
    //   });
    //   this.$fetch();
    //   this.$nextTick(() => {
    //     this.$nuxt.$loading.finish();
    //   });
    // },
    // Atbottom: function (val, oldVal) {
    //   if (val) {
    //     if (this.lastUserId != this.AllUsers[this.AllUsers.length - 1].UserId) {
    //       this.lastUserId = this.AllUsers[this.AllUsers.length - 1].UserId;
    //       this.$fetch();
    //     }
    //   }
    // },
  },
  methods: {
    async getUsersForInfiniteScroll(id){
      try {
        const response = await this.$repositories.getAllUsersForUser.getAllUsersForUser(
          {
            userId:this.userId,
            count:20,
            search:''
          }
        );
        const newUsers = response.data;
        newUsers.forEach((element) => {
          this.AllUsers.push(element)
        });
        // console.log('this.postData',this.postData)
      }catch (error) {
        console.log(error);
      }
    },
    handleScroll(event){

      if (event.target.offsetHeight + event.target.scrollTop >= event.target.scrollHeight) {
        const lastUserId = Object.values(this.AllUsers).pop();
        this.getUsersForInfiniteScroll(lastUserId.userId)
      }

      // if (event.target.scrollHeight) {
      //   console.log('event.target.scrollHeight',event.target.scrollHeight)
      //   console.log(event.target.scrollTop)
      //   //this is stupid but it works temprory
      //   if (event.target.scrollTop === event.target.scrollHeight - 969) {
      //
      //   }
      // }
    },

    //
    // handleScroll: function ({
    //                           target: {scrollTop, clientHeight, scrollHeight},
    //                         }) {
    //   if (scrollTop + clientHeight >= scrollHeight) {
    //     this.Atbottom = true;
    //   } else {
    //     this.Atbottom = false;
    //   }
    // },

    async ChangeRoute(item) {
      console.log(item);
      this.$store.commit("SetSearchedUserId", item.UserId);
      // const response = await this.$repositories.getUserIndex.getUserIndex(
      //   item.UserId
      // );
      // this.$store.commit("SetUserData", response.data);
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
