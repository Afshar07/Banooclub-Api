export default [
  {
    header: 'دسترسی ها',
  },
  {
    title: 'چت گروهی',
    icon: 'UsersIcon',
    children: [
      {
        title: 'لیست گروه ها',
        route: 'apps-Groups-GroupsList',
        resource: 'ProductManager',
        action: 'ProductManager',
      },
      {
        title: 'افزودن گروه ',
        route: 'apps-Groups-AddGroups',
        resource: 'ProductManager',
        action: 'ProductManager',
      },

    ],
  },
  {
    title: 'پست ها',
    icon: 'GridIcon',
    children: [
      {
        title: ' پست های تایید شده',
        route: 'apps-post-list',
      },
      {
        title: ' پست های تایید نشده',
        route: 'apps-post-NotConfirmed',
      },
      {
        title: ' پست های گزارش شده',
        route: 'apps-post-RejectedPosts',
      }
    ],
  },
  {
    title: 'تیکت ها',
    icon: 'MessageCircleIcon',
    children:[
      {
        title: ' همه تیکت ها',
        route: 'apps-Ticket-AllTickets',
      },
      {
        title: ' ساخت تیکت جدید',
        route: 'apps-Ticket-CreateTicket',
      }
    ]
  },


  {
    title: 'کاربران',
    route: 'apps-users-UsersList',
    icon: 'UsersIcon',
  },

  {
    title: 'آگهی ها',
    icon: 'ListIcon',
    children: [
      {
        title: ' آگهی های تایید شده',
        route: 'apps-Advertise-adsList',
        // resource: 'ProductManager',
        // action: 'ProductManager',
      },
      {
        title: ' آگهی های تایید نشده',
        route: 'apps-Advertise-notConfirmed',
        resource: 'ProductManager',
        action: 'ProductManager',
      },
      {
        title: ' دسته بندی آگهی ها',
        route: 'apps-Advertise-categories',
        resource: 'ProductManager',
        action: 'ProductManager',
      },
    ],
  },



  {
    title: 'خدمات',
    icon: 'BriefcaseIcon',
    children: [
      {
        title: 'همه خدمات',
        route: 'apps-Service-AllServices',
        resource: 'ProductManager',
        action: 'ProductManager',
      },
      {
        title: 'خدمات تایید شده',
        route: 'apps-Service-ServicesList',
        resource: 'ProductManager',
        action: 'ProductManager',
      },
      {
        title: 'خدمات تایید نشده',
        route: 'apps-Service-notConfirmed',
        resource: 'ProductManager',
        action: 'ProductManager',
      },
      {
        title: 'نیاز به بازبینی مجدد',
        route: 'apps-Service-Pending',
        resource: 'ProductManager',
        action: 'ProductManager',
      },
      {
        title: 'منقضی شده',
        route: 'apps-Service-Expired',
        resource: 'ProductManager',
        action: 'ProductManager',
      },
      {
        title: ' دسته بندی  خدمات',
        route: 'apps-Service-ServiceCategories',
        resource: 'ProductManager',
        action: 'ProductManager',
      },
    ],
  },


  {
    title: 'مالی',
    icon: 'DollarSignIcon',
    children: [
      {
        title: 'لیست پرداخت ها',
        route: 'apps-Financial-AllPayments',
        resource: 'Accountent',
        action: 'Accountent',
      },
      {
        title: 'لیست سفارشات',
        route: 'apps-Financial-AllOrders',
        resource: 'Accountent',
        action: 'Accountent',
      },

    ],
  },
  {
    title: 'مشاوره',
    icon: 'HeartIcon',
    children: [
      {
        title: 'لیست مشاور های فعال',
        route: 'apps-consultant-allConsultants',
      },
      {
        title: 'لیست درخواست های مشاور شدن',
        route: 'apps-consultant-consultantRequests',
      },
      {
        title: 'دسته بندی های مشاوره',
        route: 'apps-consultant-consultantCategories',
      },
      {
        title: 'نظرات مشاوران',
        route: 'apps-consultant-consultantComments',
      },

    ],
  },
  {
    title: 'تبلیغات',
    icon: 'CodesandboxIcon',
    children: [
      {
        title: 'ارتقا خدمت',
        route: 'apps-Upgrade-UpgradeService',
        resource: 'ProductManager',
        action: 'ProductManager',
      },
      {
        title: 'ارتقا آگهی',
        route: 'apps-Upgrade-UpgradeAd',
        resource: 'ProductManager',
        action: 'ProductManager',
      },

    ],
  },


  {
    title: 'هم خانه',
    icon: 'UserIcon',
    children: [
      {
        title: 'همه آگهی های هم خانه',
        route: 'apps-Roomate-AllRoomates',
        resource: 'Accountent',
        action: 'Accountent',
      },
    ],
  },


  {
    title: 'تالار ها',
    icon: 'CopyIcon',
    children: [
      {
        title: ' لیست تالار ها',
        route: 'apps-Forum-ForumsList',
        resource: 'Accountent',
        action: 'Accountent',
      },

      {
        title: 'دسته بندی  تالار',
        route: 'apps-Forum-ForumsCategories',
        resource: 'Accountent',
        action: 'Accountent',
      },
      {
        title: 'افزودن تالار',
        route: 'apps-Forum-AddForum',
        resource: 'Accountent',
        action: 'Accountent',
      },
    ],
  },
  {
    title: 'مقالات',
    icon: 'BookOpenIcon',
    // resource: 'Blogger',
    // action: 'Blogger',
    children: [
      {
        title: 'لیست مقالات',
        route: 'apps-Blog-BlogList',
        resource: 'Blogger',
        action: 'Blogger',
      },
      {
        title: 'آرشیو',
        route: 'apps-Blog-BlogArchived',
        resource: 'Blogger',
        action: 'Blogger',
      },
      {
        title: 'افزودن مقاله',
        route: 'apps-Blog-AddBlog',
        resource: 'Blogger',
        action: 'Blogger',
      },
      {
        title: 'دسته بندی های مقاله',
        route: 'apps-Blog-BlogCategories',
        resource: 'Blogger',
        action: 'Blogger',
      },
      {
        title: 'لیست نظرات مقاله',
        route: 'apps-Blog-BlogComments',
        resource: 'Blogger',
        action: 'Blogger',
      },
      {
        title: 'سئو مقالات',
        route: 'apps-Blog-BlogsSeo',
        resource: 'Blogger',
        action: 'Blogger',
      },
      {
        title: 'سئو دسته بندی مقالات',
        route: 'apps-Blog-BlogCategoriesSeo',
        resource: 'Blogger',
        action: 'Blogger',
      },
    ],
  },

  {
    title: 'لیست کلمات منفی',
    route: 'apps-NegativeKeyWord',
    icon: 'AlertCircleIcon',
  },
]
