<template>
  <div class="d-flex flex-column superAdminSidebar">
    <div class="superAdminSidebarItem" :class="activeItem==='allUser'?'activeItemStyle':''">
      <div class="d-flex flex-row gap-3">
        <div class="">
          <font-awesome-icon :class="activeItem==='allUser'?'activeItemIcon':''" icon="user"
                             size="lg"/>
        </div>
        <div class="">
          <nuxt-link to="/SuperAdmin" @click="switchActiveItem('allUser')">
            همه کاربران
          </nuxt-link>
        </div>
      </div>
    </div>
    <div class="superAdminSidebarItem" :class="activeItem==='AddCategory'?'activeItemStyle':''">
      <div class="d-flex flex-row gap-3">
        <div class="">
          <font-awesome-icon :class="activeItem==='AddCategory'?'activeItemIcon':''" icon="folder-plus"
                             size="lg"/>
        </div>
        <div class="">
          <nuxt-link to="/SuperAdmin/AddCategory" @click="switchActiveItem('AddCategory')">
            دسته بندی آگهی ها
          </nuxt-link>
        </div>
      </div>
    </div>
    <!--    <div class="superAdminSidebarItem" :class="activeItem==='AllPosts'?'activeItemStyle':''"-->
    <!--         @click="switchActiveItem('AllPosts')">-->
    <!--      <div class="d-flex flex-row gap-3">-->
    <!--        <div class="">-->
    <!--          <font-awesome-icon :class="activeItem==='AllPosts'?'activeItemIcon':''" icon="blog"-->
    <!--                             size="lg"/>-->
    <!--        </div>-->
    <!--        <div class="">-->
    <!--          <nuxt-link to="/SuperAdmin/AllPosts">-->
    <!--            همه پست ها-->
    <!--          </nuxt-link>-->
    <!--        </div>-->
    <!--      </div>-->
    <!--    </div>-->
    <div class="superAdminSidebarItem" :class="activeItem==='ReportedPosts'?'activeItemStyle':''">
      <div class="d-flex flex-row gap-3">
        <div class="">
          <font-awesome-icon :class="activeItem==='ReportedPosts'?'activeItemIcon':''" icon="exclamation-triangle"
                             size="lg"/>
        </div>
        <div class="">
          <nuxt-link to="/SuperAdmin/ReportedPosts" @click="switchActiveItem('ReportedPosts')">
            پست های گزارش شده
          </nuxt-link>
        </div>
      </div>
    </div>
    <div class="superAdminSidebarItem" :class="activeItem==='NotConfirmedPosts'?'activeItemStyle':''">
      <div class="d-flex flex-row gap-3">
        <div class="">
          <font-awesome-icon :class="activeItem==='NotConfirmedPosts'?'activeItemIcon':''" icon="ban"
                             size="lg"/>
        </div>
        <div class="">
          <nuxt-link to="/SuperAdmin/NotConfirmedPosts" @click="switchActiveItem('NotConfirmedPosts')">
            پست های تایید نشده
          </nuxt-link>
        </div>
      </div>
    </div>
    <div class="superAdminSidebarItem" :class="activeItem==='PostNk'?'activeItemStyle':''">
      <div class="d-flex flex-row gap-3">
        <div class="">
          <font-awesome-icon :class="activeItem==='PostNk'?'activeItemIcon':''" icon="user-slash"
                             size="lg"/>
        </div>
        <div class="">
          <nuxt-link to="/SuperAdmin/PostNegativeKey" @click="switchActiveItem('PostNk')">
            کلمات نامتعارف پست
          </nuxt-link>
        </div>
      </div>
    </div>
    <div class="superAdminSidebarItem" :class="activeItem==='ChatNk'?'activeItemStyle':''">
      <div class="d-flex flex-row gap-3">
        <div class="">
          <font-awesome-icon :class="activeItem==='ChatNk'?'activeItemIcon':''" icon="head-side-mask"
                             size="lg"/>
        </div>
        <div class="">
          <nuxt-link to="/SuperAdmin/ChatNegativeKey" @click="switchActiveItem('ChatNk')">
            کلمات نامتعارف چت
          </nuxt-link>
        </div>
      </div>
    </div>
    <div class="superAdminSidebarItem" :class="activeItem==='WaitingListApproved'?'activeItemStyle':''">
      <div class="d-flex flex-row gap-3">
        <div class="">
          <font-awesome-icon :class="activeItem==='WaitingListApproved'?'activeItemIcon':''" icon="user-check"
                             size="lg"/>
        </div>
        <div class="">
          <nuxt-link to="/SuperAdmin/WaitingListApproved" @click="switchActiveItem('WaitingListApproved')">
            افراد در انتظار تایید
          </nuxt-link>
        </div>
      </div>
    </div>
    <div class="superAdminSidebarItem" :class="activeItem==='AdsListApproved'?'activeItemStyle':''">
      <div class="d-flex flex-row gap-3">
        <div class="">
          <font-awesome-icon :class="activeItem==='AdsListApproved'?'activeItemIcon':''" icon="ad"
                             size="lg"/>
        </div>
        <div class="">
          <nuxt-link to="/SuperAdmin/AdsListApproved" @click="switchActiveItem('AdsListApproved')">
            آگهی ها
          </nuxt-link>
        </div>
      </div>
    </div>
    <div class="superAdminSidebarItem" :class="activeItem==='Tickets'?'activeItemStyle':''">
      <div class="d-flex flex-row gap-3">
        <div class="">
          <font-awesome-icon :class="activeItem==='Tickets'?'activeItemIcon':''" icon="ticket-alt"
                             size="lg"/>
        </div>
        <div class="" @click="switchActiveItem('Tickets')">
          <nuxt-link to="/SuperAdmin/Tickets">
            تیکت ها
          </nuxt-link>
        </div>
      </div>
    </div>
    <div class="superAdminSidebarItem" :class="activeItem==='VideoAuthentication'?'activeItemStyle':''">
      <div class="d-flex flex-row gap-3">
        <div class="">
          <font-awesome-icon :class="activeItem==='VideoAuthentication'?'activeItemIcon':''" icon="fingerprint"
                             size="lg"/>
        </div>
        <div class="">
          <nuxt-link to="/SuperAdmin/VideoAuthentication" @click="switchActiveItem('VideoAuthentication')">
            احراز هویت ویدیویی
          </nuxt-link>
        </div>
      </div>
    </div>
    <div class="superAdminSidebarItem" :class="activeItem==='FormalInfo'?'activeItemStyle':''">
      <div class="d-flex flex-row gap-3">
        <div class="">
          <font-awesome-icon :class="activeItem==='FormalInfo'?'activeItemIcon':''" icon="book"
                             size="lg"/>
        </div>
        <div class="">
          <nuxt-link  to="/SuperAdmin/FormalInfo" @click="switchActiveItem('FormalInfo')">
            تایید اطلاعات حقوقی
          </nuxt-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "SuperAdminSidebar",
  data() {
    return {
      activeItem: ''
    }
  },
  methods: {
    switchActiveItem(activeItem) {
      this.activeItem = activeItem;
    }
  },
  mounted() {
    if ($nuxt.$route.fullPath === '/SuperAdmin/ReportedPosts') {
      this.activeItem = 'ReportedPosts'
    } else if ($nuxt.$route.fullPath === '/SuperAdmin') {
      this.activeItem = 'allUser';
    } else if ($nuxt.$route.fullPath === '/SuperAdmin/AllPosts') {
      this.activeItem = 'AllPosts'
    } else if ($nuxt.$route.fullPath === '/SuperAdmin/ReportedPosts') {
      this.activeItem = 'ReportedPosts'
    } else if ($nuxt.$route.fullPath === '/SuperAdmin/AddCategory') {
      this.activeItem = 'AddCategory'
    } else if ($nuxt.$route.fullPath === '/SuperAdmin/AdsListApproved') {
      this.activeItem = 'AdsListApproved'
    } else if ($nuxt.$route.fullPath === '/SuperAdmin/WaitingListApproved') {
      this.activeItem = 'WaitingListApproved'
    } else if ($nuxt.$route.fullPath === '/SuperAdmin/NotConfirmedPosts') {
      this.activeItem = 'NotConfirmedPosts'
    } else if ($nuxt.$route.fullPath === '/SuperAdmin/PostNegativeKey') {
      this.activeItem = 'PostNk'
    } else if ($nuxt.$route.fullPath === '/SuperAdmin/ChatNegativeKey') {
      this.activeItem = 'ChatNk'
    } else if ($nuxt.$route.fullPath === '/SuperAdmin/Tickets' || $nuxt.$route.fullPath.includes('TicketDetails')) {
      this.activeItem = 'Tickets'
    } else if ($nuxt.$route.fullPath === '/SuperAdmin/VideoAuthentication') {
      this.activeItem = 'VideoAuthentication'
    } else if ($nuxt.$route.fullPath === '/SuperAdmin/FormalInfo') {
      this.activeItem = 'FormalInfo'
    }
  },
  watch: {
    '$route.path': function () {
      if ($nuxt.$route.fullPath === '/SuperAdmin/ReportedPosts') {
        this.activeItem = 'ReportedPosts'
      } else if ($nuxt.$route.fullPath === '/SuperAdmin') {
        this.activeItem = 'allUser';
      } else if ($nuxt.$route.fullPath === '/SuperAdmin/AllPosts') {
        this.activeItem = 'AllPosts'
      } else if ($nuxt.$route.fullPath === '/SuperAdmin/ReportedPosts') {
        this.activeItem = 'ReportedPosts'
      } else if ($nuxt.$route.fullPath === '/SuperAdmin/AddCategory') {
        this.activeItem = 'AddCategory'
      } else if ($nuxt.$route.fullPath === '/SuperAdmin/AdsListApproved') {
        this.activeItem = 'AdsListApproved'
      } else if ($nuxt.$route.fullPath === '/SuperAdmin/WaitingListApproved') {
        this.activeItem = 'WaitingListApproved'
      } else if ($nuxt.$route.fullPath === '/SuperAdmin/NotConfirmedPosts') {
        this.activeItem = 'NotConfirmedPosts'
      } else if ($nuxt.$route.fullPath === '/SuperAdmin/PostNegativeKey') {
        this.activeItem = 'PostNk'
      } else if ($nuxt.$route.fullPath === '/SuperAdmin/ChatNegativeKey') {
        this.activeItem = 'ChatNk'
      } else if ($nuxt.$route.fullPath === '/SuperAdmin/Tickets' || $nuxt.$route.fullPath.includes('TicketDetails')) {
        this.activeItem = 'Tickets'
      } else if ($nuxt.$route.fullPath === '/SuperAdmin/VideoAuthentication') {
        this.activeItem = 'VideoAuthentication'
      } else if ($nuxt.$route.fullPath === '/SuperAdmin/FormalInfo') {
        this.activeItem = 'FormalInfo'
      }
    }
  }
}
</script>

<style scoped>
a {
  text-decoration: none;
  color: #2e88e7;
  cursor: pointer;
}

.superAdminSidebar {
  width: 100%;
  background-color: #fff;
  min-height: 100vw;
}

.superAdminSidebarItem {
  padding: 1rem;
  border-bottom: 1px solid #ccc;
}

.activeItemStyle {
  background-color: #2e88e7;
  color: #eb9c6c !important;
  /*transition: all 0.15s linear 2s;*/
  border-bottom: 1px solid #3f6791;
}


.activeItemIcon {
  color: #fff;
}

.activeItemStyle a {
  color: #fff;
}
</style>
