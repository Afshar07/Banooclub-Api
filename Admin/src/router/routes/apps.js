export default [
  // *===============================================---*
  // *--------- Groups ---- ---------------------------------------*
  // *===============================================---*
  {
    path: '/apps/Groups/AddGroup',
    name: 'apps-Groups-AddGroups',
    component: () => import('@/views/apps/Groups/AddGroup.vue'),
  },
  {
    path: '/apps/Groups/GroupsList',
    name: 'apps-Groups-GroupsList',
    component: () => import('@/views/apps/Groups/GroupsList.vue'),
  },
  // *===============================================---*
  // *--------- Posts ---- ---------------------------------------*
  // *===============================================---*
  {
    path: '/apps/post/list',
    name: 'apps-post-list',
    component: () => import('@/views/apps/post/PostList.vue'),
  },
  {
    path: '/apps/post/RejectedPosts',
    name: 'apps-post-RejectedPosts',
    component: () => import('@/views/apps/post/PostRejected.vue'),
  },
  {
    path: '/apps/post/PostNotConfirmed',
    name: 'apps-post-NotConfirmed',
    component: () => import('@/views/apps/post/PostNotConfirmed.vue'),
  },
  // *===============================================---*
  // *--------- Tickets ---- ---------------------------------------*
  // *===============================================---*
  {
    path: '/apps/Ticket/AllTickets',
    name: 'apps-Ticket-AllTickets',
    component: () => import('@/views/apps/Ticket/AllTickets'),
  },
  {
    path: '/apps/Ticket/Detail/:id',
    name: 'apps-Ticket-Detail',
    component: () => import('@/views/apps/Ticket/Detail/TicketDetail.vue'),
  },

  // *===============================================---*
  // *--------- USER ---- ---------------------------------------*
  // *===============================================---*

  {
    path: '/apps/users/UsersList',
    name: 'apps-users-UsersList',
    component: () => import('@/views/apps/users/UsersList.vue'),
  },

  // *===============================================---*
  // *--------- Ads ---- ---------------------------------------*
  // *===============================================---*
  {
    path: '/apps/Advertise/adsList',
    name: 'apps-Advertise-adsList',
    component: () => import('@/views/apps/Advertise/adsList.vue'),
  },
  {
    path: '/apps/Advertise/AdsNotConfirmed',
    name: 'apps-Advertise-notConfirmed',
    component: () => import('@/views/apps/Advertise/AdsNotConfirmed.vue'),
  },
  {
    path: '/apps/Advertise/AdsCategories',
    name: 'apps-Advertise-categories',
    component: () => import('@/views/apps/Advertise/AdsCategories.vue'),
  },
  // *===============================================---*
  // *--------- Service ---- ---------------------------------------*
  // *===============================================---*
  {
    path: '/apps/Service/ServicesList',
    name: 'apps-Service-ServicesList',
    component: () => import('@/views/apps/Service/ServicesList.vue'),
  },
  {
    path: '/apps/Service/AllServices',
    name: 'apps-Service-AllServices',
    component: () => import('@/views/apps/Service/AllServices.vue'),
  },
  {
    path: '/apps/Service/ServicesListNotConfirmed',
    name: 'apps-Service-notConfirmed',
    component: () => import('@/views/apps/Service/ServicesListNotConfirmed.vue'),
  },
  {
    path: '/apps/Service/Pending',
    name: 'apps-Service-Pending',
    component: () => import('@/views/apps/Service/ServicesPending.vue'),
  },
  {
    path: '/apps/Service/ServiceCategories',
    name: 'apps-Service-ServiceCategories',
    component: () => import('@/views/apps/Service/ServiceCategories.vue'),
  },
  // *===============================================---*
  // *--------- Financial ---- ---------------------------------------*
  // *===============================================---*
  {
    path: '/apps/Financial/AllPayments',
    name: 'apps-Financial-AllPayments',
    component: () => import('@/views/apps/Financial/AllPayments.vue'),
  },
  // *===============================================---*
  // *--------- Upgrade ---- ---------------------------------------*
  // *===============================================---*
  {
    path: '/apps/Upgrade/UpgradeAd',
    name: 'apps-Upgrade-UpgradeAd',
    component: () => import('@/views/apps/Upgrade/UpgradeAd.vue'),
  },
  {
    path: '/apps/Upgrade/UpgradeService',
    name: 'apps-Upgrade-UpgradeService',
    component: () => import('@/views/apps/Upgrade/UpgradeService.vue'),
  },
  // *===============================================---*
  // *--------- Roomate ---- ---------------------------------------*
  // *===============================================---*
  {
    path: '/apps/Roomate/AllRoomates',
    name: 'apps-Roomate-AllRoomates',
    component: () => import('@/views/apps/Roomate/AllRoomates.vue'),
  },
  // *===============================================---*
  // *--------- Forum ---- ---------------------------------------*
  // *===============================================---*
  {
    path: '/apps/Forum/ForumsList',
    name: 'apps-Forum-ForumsList',
    component: () => import('@/views/apps/Forum/ForumsList.vue'),
  },
  {
    path: '/apps/Forum/ForumsListNotConfirmed',
    name: 'apps-Forum-ForumsListNotConfirmed',
    component: () => import('@/views/apps/Forum/ForumsListNotConfirmed.vue'),
  },
  {
    path: '/apps/Forum/ForumsCategories',
    name: 'apps-Forum-ForumsCategories',
    component: () => import('@/views/apps/Forum/ForumsCategories.vue'),
  },

  {
    path: '/apps/Forum/AddForum',
    name: 'apps-Forum-AddForum',
    component: () => import('@/views/apps/Forum/AddForum.vue'),
  },

  // *===============================================---*
  // *--------- Blog ---- ---------------------------------------*
  // *===============================================---*
  {
    path: '/apps/Blog/AddBlog',
    name: 'apps-Blog-AddBlog',
    component: () => import('@/views/apps/Blog/AddBlog.vue'),
  },
  {
    path: '/apps/Blog/BlogCategories',
    name: 'apps-Blog-BlogCategories',
    component: () => import('@/views/apps/Blog/BlogCategories.vue'),
  },
  {
    path: '/apps/Blog/BlogCategoriesSeo',
    name: 'apps-Blog-BlogCategoriesSeo',
    component: () => import('@/views/apps/Blog/BlogCategoriesSeo.vue'),
  },

  {
    path: '/apps/Blog/BlogComments',
    name: 'apps-Blog-BlogComments',
    component: () => import('@/views/apps/Blog/BlogComments.vue'),
  },
  {
    path: '/apps/Blog/BlogList',
    name: 'apps-Blog-BlogList',
    component: () => import('@/views/apps/Blog/BlogList.vue'),
  },
  {
    path: '/apps/Blog/BlogsSeo',
    name: 'apps-Blog-BlogsSeo',
    component: () => import('@/views/apps/Blog/BlogsSeo.vue'),
  },
  {
    path: '/apps/NegativeKeyWord',
    name: 'apps-NegativeKeyWord',
    component: () => import('@/views/apps/NegativeKeyWord.vue'),
  },
  {
    path: '/apps/Blog/EditBlog/:id',
    name: 'apps-Blog-EditBlog',
    component: () => import('@/views/apps/Blog/EditBlog/_id'),
  },



]
