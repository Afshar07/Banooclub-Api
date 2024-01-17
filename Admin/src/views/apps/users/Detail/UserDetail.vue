<template>
  <b-overlay
      :show="UserDetail === null"
      rounded="sm"
  >
    <div class="row">
      <div class="col-md-12 ">
        <UserCard :UserIndex="UserDetail"></UserCard>
      </div>
      <div class="col-md-12 bg-white m-0">
        <UserTabs @ChangeComponent="ChangeComponent($event)"></UserTabs>
        <component v-if="UserDetail.userInfo" :userName="UserDetail.userInfo.userName" :userId="UserDetail.userInfo.userId" :is="componentInstance"></component>
      </div>
    </div>

  </b-overlay>
</template>

<script>

import {
  BCard, BRow, BCol, BFormInput, BButton, BTable, BMedia, BAvatar, BLink,
  BBadge, BDropdown, BDropdownItem, BPagination, BOverlay, BModal, BFormGroup, BFormSelect, BTabs, BTab, BCardText
} from 'bootstrap-vue'
import {UserIndex} from "@/libs/Api/Users/UserMethods";
import vSelect from 'vue-select'
import UserCard from "@/views/components/UserCard";
import ToastificationContent from '@core/components/toastification/ToastificationContent'
import UserTabs from "@/views/components/UserTabs";
import AdsTab from "@/views/components/UserTabComponents/AdsTab";
import ForumsTab from "@/views/components/UserTabComponents/ForumsTab";
import FriendsTab from "@/views/components/UserTabComponents/FriendsTab";
import OrdersTab from "@/views/components/UserTabComponents/OrdersTab";
import PaymentsTab from "@/views/components/UserTabComponents/PaymentsTab";
import PostsTab from "@/views/components/UserTabComponents/PostsTab";
import RoomateTab from "@/views/components/UserTabComponents/RoomateTab";
import ServicesTab from "@/views/components/UserTabComponents/ServicesTab";
import TicketsTab from "@/views/components/UserTabComponents/TicketsTab";
import WishListsTab from "@/views/components/UserTabComponents/WishListsTab";
export default {
  components: {
    AdsTab,
    ForumsTab,
    FriendsTab,
    OrdersTab,
    PaymentsTab,
    PostsTab,
    RoomateTab,
    ServicesTab,
    TicketsTab,
    WishListsTab,
    UserCard,
    UserTabs,
    BCard,
    BRow,
    BCol,
    BFormInput,
    BFormGroup,
    BButton,
    BTable,
    BMedia,
    BAvatar,
    BLink,
    BBadge,
    BDropdown,
    BDropdownItem,
    BPagination,
    BOverlay,
    BModal,
    BFormSelect,
    BCardText,
    BTabs,
    BTab,
    vSelect,

  },
  title: 'صفحه کاربر  - پنل ادمین بانو کلاب',
  name: 'UsersList',

  data() {
    return {
      UserDetail: false,
      Slug:'RoomateTab'
    }
  },
  async created() {
    await this.GetUserIndex()
  },

  watch: {},

  computed: {
    componentInstance() {
        return this.Slug

    },
  },
  methods: {
    ChangeComponent(slug){
      this.Slug = slug

    },
    async GetUserIndex() {
      let _this = this
      let userIndex = new UserIndex()
      let data = {
        userName: this.$route.params.id
      }
      userIndex.setParams(data)
      await userIndex.fetch((content) => {
        console.log(content)
        _this.UserDetail = content
      }, (e) => {
        console.log(e)
      })
    }
  },
}
</script>

<style scoped>

</style>
