<template>
  <section class="m-0 position-sticky top-0 tw-z-[1000]">
    <!--    mobile menu-->
    <div
      class="offcanvas offcanvas-end sidebar-bg right_menu_mobile"
      tabindex="-1"
      id="test"
      aria-labelledby="test"
    >
      <div class="offcanvas-body px-2" style="height: 700px!important;overflow-y: scroll!important;">
        <div class="d-flex flex-column" style="padding-right: 15px">
          <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
            <nuxt-link
              exact-active-class="SingleShortCutActive"
              to="/social/DashBoard"
              class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
            >
              <DashBoard class="svg_icons_size"></DashBoard>
              <!--                <i class="fas fa-home" style="font-size: 18px"></i>-->
              <span v-if="!decrease_width" class="header_font_sidebar_size">داشبورد</span>
            </nuxt-link>
          </div>
          <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
            <nuxt-link
              exact-active-class="SingleShortCutActive"
              to="/social"
              class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
            >
              <HomeIcon fill="#2563eb" class="svg_icons_size"/>
              <!--                <i class="fas fa-home" style="font-size: 18px"></i>-->
              <span v-if="!decrease_width" class="header_font_sidebar_size">خانه</span>
            </nuxt-link>
          </div>
          <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
            <nuxt-link
              exact-active-class="SingleShortCutActive"
              to="/social/accountsetting/MyPage"
              class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
            >
              <ProfileIcon class="svg_icons_size" fill="rgb(245 158 11)"/>
              <!--                <i class="fas fa-plus-square" style="font-size: 18px"></i>-->
              <span v-if="!decrease_width" class="header_font_sidebar_size">صفحه من</span>
            </nuxt-link>
          </div>
          <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
            <nuxt-link
              exact-active-class="SingleShortCutActive"
              to="/social/Chat"
              class="SingleShortCut  p-3 py-2 w-100 d-flex align-items-center gap-4"
            >
              <MessageIcon fill="rgb(245 158 11)" class="svg_icons_size"/>
              <!--                <i class="fas fa-comment" style="font-size: 18px"></i>-->
              <span v-if="!decrease_width" class="header_font_sidebar_size">مکالمات</span>
            </nuxt-link>
          </div>
          <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
            <nuxt-link
              exact-active-class="SingleShortCutActive"
              to="/Blog/AllBlogs"
              class="SingleShortCut  p-3 py-2 w-100 d-flex align-items-center gap-4"
            >
              <BlogIcon fill="rgb(245 158 11)" class="svg_icons_size"/>
              <!--                <i class="fas fa-comment" style="font-size: 18px"></i>-->
              <span v-if="!decrease_width" class="header_font_sidebar_size">مقالات</span>
            </nuxt-link>
          </div>
          <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
            <nuxt-link
              exact-active-class="SingleShortCutActive"
              to="/social/accountsetting/tickets"
              class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
            >
              <TicketIcon class="svg_icons_size"/>
              <!--                <i class="fas fa-ticket-alt" style="font-size: 18px"></i>-->
              <span v-if="!decrease_width" class="header_font_sidebar_size">تیکت ها</span>
            </nuxt-link>
          </div>
          <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
            <nuxt-link
              exact-active-class="SingleShortCutActive"
              to="/social/contacts"
              class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
            >
              <AllUsersIcon fill="#3b82f6" class="svg_icons_size"/>
              <!--                <i class="fas fa-users" style="font-size: 18px"></i>-->
              <span v-if="!decrease_width" class="header_font_sidebar_size">پیدا کردن دوست</span>
            </nuxt-link>
          </div>
          <div :class="[decrease_width ? '' : ' ']" class="py-1">
            <div v-if="!decrease_width"  @click="AdsActive=!AdsActive" class=" tw-cursor-default SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4">
              <AdsIcon class="svg_icons_size"/>
              <span class="header_font_sidebar_size">آگهی ها </span>
              <CheveronIcon :class="{ActiveChevron:AdsActive}" class="tw-mr-auto tw-transition-all " style="width: 12px; height: 12px"/>
            </div>
            <div v-if="!decrease_width" :class="{ActiveMenu:AdsActive}" class=" tw-transition-all px-3 tw-h-0 tw-overflow-hidden" >
              <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                <nuxt-link
                  exact-active-class="SingleShortCutActive"
                  to="/migration/alladvertise"
                  class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                  :class="[decrease_width ? 'p-3' : 'px-3']"
                >
                  <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                  <AdsIcon v-if="decrease_width" class="svg_icons_size"/>
                  <span v-if="!decrease_width" class="font_sidebar_size">همه آگهی ها </span>
                </nuxt-link>
              </div>
              <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                <nuxt-link
                  exact-active-class="SingleShortCutActive"
                  to="/migration/myadvertise"
                  class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                  :class="[decrease_width ? 'p-3' : 'px-3']"
                >
                  <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                  <AdsIcon v-if="decrease_width" class="svg_icons_size"/>
                  <span v-if="!decrease_width" class="font_sidebar_size">آگهی های من </span>
                </nuxt-link>
              </div>
              <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                <nuxt-link
                  exact-active-class="SingleShortCutActive"
                  to="/migration/createadvertise"
                  class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                  :class="[decrease_width ? 'p-3' : 'px-3']"
                >
                  <PlusIcon class="svg_icons_size" v-if="decrease_width" fill="rgb(245 158 11)"/>
                  <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                  <span v-if="!decrease_width" class="font_sidebar_size">افزودن آگهی</span>
                </nuxt-link>
              </div>
            </div>
          </div>
          <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
            <nuxt-link
              exact-active-class="SingleShortCutActive"
              to="/social/MyWishlist"
              class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
            >
              <HeartIcon :fill="'rgb(255,0,0)'" class="svg_icons_size  text-danger"/>
              <!--                <i class="fas fa-users" style="font-size: 18px"></i>-->
              <span v-if="!decrease_width" class="header_font_sidebar_size">علاقه مندیهای من</span>
            </nuxt-link>
          </div>
          <div :class="[decrease_width ? '' : '']" class="py-1">

            <div @click="ServicesActive=!ServicesActive" v-if="!decrease_width" :class="[decrease_width ? '' : '']"
                 class=" tw-cursor-default SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4">
              <ProductsIcon class="svg_icons_size tw-text-red-500"/>
              <span v-if="!decrease_width" class="header_font_sidebar_size">خدمات </span>
              <CheveronIcon :class="{ActiveChevron:ServicesActive}" class=" tw-transition-all tw-mr-auto" style="width: 12px; height: 12px"/>
            </div>
            <div :class="{ActiveMenuService:ServicesActive}" v-if="!decrease_width" class="px-3  tw-h-0 tw-overflow-hidden tw-transition-all" >
              <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                <nuxt-link
                  exact-active-class="SingleShortCutActive"
                  to="/Products/"
                  class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                  :class="[decrease_width ? 'p-3' : 'px-3']"
                >
                  <ProductsIcon v-if="decrease_width" class="svg_icons_size tw-text-red-500"/>
                  <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                  <span v-if="!decrease_width" class="font_sidebar_size">همه خدمات</span>
                </nuxt-link>
              </div>
              <div v-if=" $auth.user&& $auth.user &&  $auth.user.baseData && $auth.user.baseData.userType===3"
                   class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                <nuxt-link
                  exact-active-class="SingleShortCutActive"
                  to="/Products/OrderedProducts/"
                  class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                  :class="[decrease_width ? 'p-3' : 'px-3']"
                >
                  <PlusIcon class="svg_icons_size" v-if="decrease_width" fill="rgb(245 158 11)"/>
                  <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                  <span v-if="!decrease_width" class="font_sidebar_size">خدمات سفارش داده شده</span>
                </nuxt-link>
              </div>
              <div v-if=" $auth.user&& $auth.user && $auth.user.baseData&& $auth.user.baseData.userType===3"
                   class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                <nuxt-link
                  exact-active-class="SingleShortCutActive"
                  to="/Products/MyServices/"
                  class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                  :class="[decrease_width ? 'p-3' : 'px-3']"
                >
                  <PlusIcon class="svg_icons_size" v-if="decrease_width" fill="rgb(245 158 11)"/>
                  <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                  <span v-if="!decrease_width" class="font_sidebar_size">خدمات من</span>
                </nuxt-link>
              </div>
              <div v-if=" $auth.user&& $auth.user &&  $auth.user.baseData && $auth.user.baseData.userType===3"
                   class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                <nuxt-link
                  exact-active-class="SingleShortCutActive"
                  to="/Products/AddProduct/"
                  class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                  :class="[decrease_width ? 'p-3' : 'px-3']"
                >
                  <PlusIcon class="svg_icons_size" v-if="decrease_width" fill="rgb(245 158 11)"/>
                  <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                  <span v-if="!decrease_width" class="font_sidebar_size">افزودن خدمت</span>
                </nuxt-link>
              </div>
            </div>
          </div>
          <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
            <nuxt-link
              exact-active-class="SingleShortCutActive"
              to="/Payments/"
              class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
            >
              <MoneyIcon fill="#ef4444" class="svg_icons_size"/>
              <span v-if="!decrease_width" class="header_font_sidebar_size">پرداخت ها</span>
            </nuxt-link>
          </div>
          <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
            <nuxt-link
              exact-active-class="SingleShortCutActive"
              to="/Orders/"
              class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
            >
              <OrderIcon fill="rgb(245 158 11)" class="svg_icons_size text-warning"/>
              <span v-if="!decrease_width" class="header_font_sidebar_size">سفارشات</span>
            </nuxt-link>
          </div>
          <div :class="[decrease_width ? '' : '']" class="py-1">
            <div  @click="ForumsActive=! ForumsActive" v-if="!decrease_width" :class="[decrease_width ? '' : '']"
                  class=" tw-cursor-default SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4">
              <ForumIcon class="svg_icons_size"/>
              <span v-if="!decrease_width" class="header_font_sidebar_size">انجمن ها </span>
              <CheveronIcon :class="{ActiveChevron:ForumsActive}"  class="tw-mr-auto" style="width: 12px; height: 12px"/>
            </div>
            <div :class="{ActiveMenu:ForumsActive}" v-if="!decrease_width" class="px-3 tw-h-0 tw-overflow-hidden" >
              <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                <nuxt-link
                  exact-active-class="SingleShortCutActive"
                  to="/Forums/"
                  class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                  :class="[decrease_width ? 'p-3' : 'px-3']"
                >
                  <ForumIcon class="svg_icons_size" v-if="decrease_width"/>
                  <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                  <span v-if="!decrease_width" class="font_sidebar_size">همه انجمن ها</span>
                </nuxt-link>
              </div>
              <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                <nuxt-link
                  exact-active-class="SingleShortCutActive"
                  to="/Forums/MyForums/"
                  class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                  :class="[decrease_width ? 'p-3' : 'px-3']"
                >
                  <PlusIcon class="svg_icons_size" v-if="decrease_width" fill="rgb(245 158 11)"/>
                  <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                  <span v-if="!decrease_width" class="font_sidebar_size">انجمن های من</span>
                </nuxt-link>
              </div>
              <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                <nuxt-link
                  exact-active-class="SingleShortCutActive"
                  to="/Forums/AddForum/"
                  class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                  :class="[decrease_width ? 'p-3' : 'px-3']"
                >
                  <PlusIcon class="svg_icons_size" v-if="decrease_width" fill="rgb(245 158 11)"/>
                  <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                  <span v-if="!decrease_width" class="font_sidebar_size">افزودن انجمن</span>
                </nuxt-link>
              </div>
            </div>
          </div>
          <div :class="[decrease_width ? '' : ' ']" class=" py-1">
            <div  @click="RoomateActive=! RoomateActive" v-if="!decrease_width" :class="[decrease_width ? '' : '']"
                  class=" tw-cursor-default SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4">
              <BedIcon class="svg_icons_size"/>
              <span v-if="!decrease_width" class="header_font_sidebar_size">هم خانه</span>
              <CheveronIcon :class="{ActiveChevron:RoomateActive}" class="tw-mr-auto" style="width: 12px; height: 12px"/>
            </div>
            <div :class="{ActiveMenu:RoomateActive}" v-if="!decrease_width" class="px-3 tw-h-0 tw-overflow-hidden" >
              <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                <nuxt-link
                  exact-active-class="SingleShortCutActive"
                  to="/migration/allroomate"
                  class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                  :class="[decrease_width ? 'p-3' : 'px-3']"
                >
                  <BedIcon class="svg_icons_size" v-if="decrease_width"/>
                  <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                  <span v-if="!decrease_width" class="font_sidebar_size"> همه آگهی های هم خانه</span>
                </nuxt-link>
              </div>

              <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                <nuxt-link
                  exact-active-class="SingleShortCutActive"
                  to="/migration/myroomateinfo"
                  class="SingleShortCut  py-2 w-100 d-flex align-items-center gap-4"
                  :class="[decrease_width ? 'p-3' : 'px-3']"
                >
                  <BedIcon class="svg_icons_size" v-if="decrease_width"/>
                  <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>

                  <span v-if="!decrease_width" class="font_sidebar_size">اطلاعات هم خانه</span>
                </nuxt-link>
              </div>
              <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                <nuxt-link
                  exact-active-class="SingleShortCutActive"
                  to="/migration/setting/roommatecondition"
                  class="SingleShortCut  py-2 w-100 d-flex align-items-center gap-4"
                  :class="[decrease_width ? 'p-3' : 'px-3']"
                >
                  <BedIcon class="svg_icons_size" v-if="decrease_width"/>
                  <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>

                  <span v-if="!decrease_width" class="font_sidebar_size">افزودن هم خانه</span>
                </nuxt-link>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!--lg sidebar-->
    <div class="offCanvasWrapper" :class="{ OffCanvasWrapperActive: true }">
      <div
        class="OffCanvas d-none d-xl-block "
        style="z-index: 999999 !important; overflow-x: hidden; border-left: 1px solid #eeeeee;    box-shadow: 0px 0 12px 8px rgb(235 88 255 / 20%);"
        :class="{ decrease_width: decrease_width===true ,OffCanvasActive:decrease_width===false }"
        @mouseleave="MouseLeaveRightMenu"
        @mouseenter="MouseOverRightMenu"
      >
        <div class="offcanvas-body px-2 mt-3" style="height: 750px!important;overflow-y: scroll!important;">
          <div class="d-flex flex-column" style="padding-right: 15px">

            <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
              <nuxt-link
                exact-active-class="SingleShortCutActive"
                to="/social/DashBoard"
                class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
              >
                <DashBoard class="svg_icons_size"></DashBoard>
                <!--                <i class="fas fa-home" style="font-size: 18px"></i>-->
                <span v-if="!decrease_width" class="header_font_sidebar_size">داشبورد</span>
              </nuxt-link>
            </div>
            <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
              <nuxt-link
                exact-active-class="SingleShortCutActive"
                to="/social"
                class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
              >
                <HomeIcon fill="#2563eb" class="svg_icons_size"/>
                <!--                <i class="fas fa-home" style="font-size: 18px"></i>-->
                <span v-if="!decrease_width" class="header_font_sidebar_size">خانه</span>
              </nuxt-link>
            </div>
            <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
              <nuxt-link
                exact-active-class="SingleShortCutActive"
                to="/social/accountsetting/MyPage"
                class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
              >
                <ProfileIcon class="svg_icons_size" fill="rgb(245 158 11)"/>
                <!--                <i class="fas fa-plus-square" style="font-size: 18px"></i>-->
                <span v-if="!decrease_width" class="header_font_sidebar_size">صفحه من</span>
              </nuxt-link>
            </div>
            <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
              <nuxt-link
                exact-active-class="SingleShortCutActive"
                to="/social/Chat"
                class="SingleShortCut  p-3 py-2 w-100 d-flex align-items-center gap-4"
              >
                <MessageIcon fill="rgb(245 158 11)" class="svg_icons_size"/>
                <!--                <i class="fas fa-comment" style="font-size: 18px"></i>-->
                <span v-if="!decrease_width" class="header_font_sidebar_size">مکالمات</span>
              </nuxt-link>
            </div>
            <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
              <nuxt-link
                exact-active-class="SingleShortCutActive"
                to="/Blog/AllBlogs"
                class="SingleShortCut  p-3 py-2 w-100 d-flex align-items-center gap-4"
              >
                <BlogIcon fill="rgb(245 158 11)" class="svg_icons_size"/>
                <!--                <i class="fas fa-comment" style="font-size: 18px"></i>-->
                <span v-if="!decrease_width" class="header_font_sidebar_size">مقالات</span>
              </nuxt-link>
            </div>
            <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
              <nuxt-link
                exact-active-class="SingleShortCutActive"
                to="/social/accountsetting/tickets"
                class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
              >
                <TicketIcon class="svg_icons_size"/>
                <!--                <i class="fas fa-ticket-alt" style="font-size: 18px"></i>-->
                <span v-if="!decrease_width" class="header_font_sidebar_size">تیکت ها</span>
              </nuxt-link>
            </div>
            <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
              <nuxt-link
                exact-active-class="SingleShortCutActive"
                to="/social/contacts"
                class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
              >
                <AllUsersIcon fill="#3b82f6" class="svg_icons_size"/>
                <!--                <i class="fas fa-users" style="font-size: 18px"></i>-->
                <span v-if="!decrease_width" class="header_font_sidebar_size">پیدا کردن دوست</span>
              </nuxt-link>
            </div>
            <div :class="[decrease_width ? '' : ' ']" class="py-1">
              <div v-if="!decrease_width"  @click="AdsActive=!AdsActive" class=" tw-cursor-default SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4">
                <AdsIcon class="svg_icons_size"/>
                <span class="header_font_sidebar_size">آگهی ها </span>
                <CheveronIcon :class="{ActiveChevron:AdsActive}" class="tw-mr-auto tw-transition-all " style="width: 12px; height: 12px"/>
              </div>
              <div v-if="!decrease_width" :class="{ActiveMenu:AdsActive}" class=" tw-transition-all px-3 tw-h-0 tw-overflow-hidden" >
                <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                  <nuxt-link
                    exact-active-class="SingleShortCutActive"
                    to="/migration/alladvertise"
                    class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                    :class="[decrease_width ? 'p-3' : 'px-3']"
                  >
                    <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                    <AdsIcon v-if="decrease_width" class="svg_icons_size"/>
                    <span v-if="!decrease_width" class="font_sidebar_size">همه آگهی ها </span>
                  </nuxt-link>
                </div>
                <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                  <nuxt-link
                    exact-active-class="SingleShortCutActive"
                    to="/migration/myadvertise"
                    class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                    :class="[decrease_width ? 'p-3' : 'px-3']"
                  >
                    <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                    <AdsIcon v-if="decrease_width" class="svg_icons_size"/>
                    <span v-if="!decrease_width" class="font_sidebar_size">آگهی های من </span>
                  </nuxt-link>
                </div>
                <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                  <nuxt-link
                    exact-active-class="SingleShortCutActive"
                    to="/migration/createadvertise"
                    class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                    :class="[decrease_width ? 'p-3' : 'px-3']"
                  >
                    <PlusIcon class="svg_icons_size" v-if="decrease_width" fill="rgb(245 158 11)"/>
                    <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                    <span v-if="!decrease_width" class="font_sidebar_size">افزودن آگهی</span>
                  </nuxt-link>
                </div>
              </div>
            </div>
            <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
              <nuxt-link
                exact-active-class="SingleShortCutActive"
                to="/social/MyWishlist"
                class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
              >
                <HeartIcon :fill="'rgb(255,0,0)'" class="svg_icons_size  text-danger"/>
                <!--                <i class="fas fa-users" style="font-size: 18px"></i>-->
                <span v-if="!decrease_width" class="header_font_sidebar_size">علاقه مندیهای من</span>
              </nuxt-link>
            </div>
            <div :class="[decrease_width ? '' : '']" class="py-1">

              <div @click="ServicesActive=!ServicesActive" v-if="!decrease_width" :class="[decrease_width ? '' : '']"
                   class=" tw-cursor-default SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4">
                <ProductsIcon class="svg_icons_size tw-text-red-500"/>
                <span v-if="!decrease_width" class="header_font_sidebar_size">خدمات </span>
                <CheveronIcon :class="{ActiveChevron:ServicesActive}" class=" tw-transition-all tw-mr-auto" style="width: 12px; height: 12px"/>
              </div>
              <div :class="{ActiveMenuService:ServicesActive}" v-if="!decrease_width" class="px-3  tw-h-0 tw-overflow-hidden tw-transition-all" >
                <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                  <nuxt-link
                    exact-active-class="SingleShortCutActive"
                    to="/Products/"
                    class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                    :class="[decrease_width ? 'p-3' : 'px-3']"
                  >
                    <ProductsIcon v-if="decrease_width" class="svg_icons_size tw-text-red-500"/>
                    <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                    <span v-if="!decrease_width" class="font_sidebar_size">همه خدمات</span>
                  </nuxt-link>
                </div>
                <div v-if=" $auth.user&& $auth.user &&  $auth.user.baseData && $auth.user.baseData.userType===3"
                     class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                  <nuxt-link
                    exact-active-class="SingleShortCutActive"
                    to="/Products/OrderedProducts/"
                    class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                    :class="[decrease_width ? 'p-3' : 'px-3']"
                  >
                    <PlusIcon class="svg_icons_size" v-if="decrease_width" fill="rgb(245 158 11)"/>
                    <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                    <span v-if="!decrease_width" class="font_sidebar_size">خدمات سفارش داده شده</span>
                  </nuxt-link>
                </div>
                <div v-if=" $auth.user&& $auth.user && $auth.user.baseData&& $auth.user.baseData.userType===3"
                     class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                  <nuxt-link
                    exact-active-class="SingleShortCutActive"
                    to="/Products/MyServices/"
                    class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                    :class="[decrease_width ? 'p-3' : 'px-3']"
                  >
                    <PlusIcon class="svg_icons_size" v-if="decrease_width" fill="rgb(245 158 11)"/>
                    <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                    <span v-if="!decrease_width" class="font_sidebar_size">خدمات من</span>
                  </nuxt-link>
                </div>
                <div v-if=" $auth.user&& $auth.user &&  $auth.user.baseData && $auth.user.baseData.userType===3"
                     class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                  <nuxt-link
                    exact-active-class="SingleShortCutActive"
                    to="/Products/AddProduct/"
                    class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                    :class="[decrease_width ? 'p-3' : 'px-3']"
                  >
                    <PlusIcon class="svg_icons_size" v-if="decrease_width" fill="rgb(245 158 11)"/>
                    <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                    <span v-if="!decrease_width" class="font_sidebar_size">افزودن خدمت</span>
                  </nuxt-link>
                </div>
              </div>
            </div>
            <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
              <nuxt-link
                exact-active-class="SingleShortCutActive"
                to="/Payments/"
                class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
              >
                <MoneyIcon fill="#ef4444" class="svg_icons_size"/>
                <span v-if="!decrease_width" class="header_font_sidebar_size">پرداخت ها</span>
              </nuxt-link>
            </div>
            <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
              <nuxt-link
                exact-active-class="SingleShortCutActive"
                to="/Orders/"
                class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
              >
                <OrderIcon fill="rgb(245 158 11)" class="svg_icons_size text-warning"/>
                <span v-if="!decrease_width" class="header_font_sidebar_size">سفارشات</span>
              </nuxt-link>
            </div>
            <div :class="[decrease_width ? '' : '']" class="py-1">
              <div  @click="ForumsActive=! ForumsActive" v-if="!decrease_width" :class="[decrease_width ? '' : '']"
                   class=" tw-cursor-default SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4">
                <ForumIcon class="svg_icons_size"/>
                <span v-if="!decrease_width" class="header_font_sidebar_size">انجمن ها </span>
                <CheveronIcon :class="{ActiveChevron:ForumsActive}"  class="tw-mr-auto" style="width: 12px; height: 12px"/>
              </div>
              <div :class="{ActiveMenu:ForumsActive}" v-if="!decrease_width" class="px-3 tw-h-0 tw-overflow-hidden" >
                <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                  <nuxt-link
                    exact-active-class="SingleShortCutActive"
                    to="/Forums/"
                    class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                    :class="[decrease_width ? 'p-3' : 'px-3']"
                  >
                    <ForumIcon class="svg_icons_size" v-if="decrease_width"/>
                    <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                    <span v-if="!decrease_width" class="font_sidebar_size">همه انجمن ها</span>
                  </nuxt-link>
                </div>
                <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                  <nuxt-link
                    exact-active-class="SingleShortCutActive"
                    to="/Forums/MyForums/"
                    class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                    :class="[decrease_width ? 'p-3' : 'px-3']"
                  >
                    <PlusIcon class="svg_icons_size" v-if="decrease_width" fill="rgb(245 158 11)"/>
                    <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                    <span v-if="!decrease_width" class="font_sidebar_size">انجمن های من</span>
                  </nuxt-link>
                </div>
                <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                  <nuxt-link
                    exact-active-class="SingleShortCutActive"
                    to="/Forums/AddForum/"
                    class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                    :class="[decrease_width ? 'p-3' : 'px-3']"
                  >
                    <PlusIcon class="svg_icons_size" v-if="decrease_width" fill="rgb(245 158 11)"/>
                    <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                    <span v-if="!decrease_width" class="font_sidebar_size">افزودن انجمن</span>
                  </nuxt-link>
                </div>
              </div>
            </div>
            <div :class="[decrease_width ? '' : ' ']" class=" py-1">
              <div  @click="RoomateActive=! RoomateActive" v-if="!decrease_width" :class="[decrease_width ? '' : '']"
                   class=" tw-cursor-default SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4">
                <BedIcon class="svg_icons_size"/>
                <span v-if="!decrease_width" class="header_font_sidebar_size">هم خانه</span>
                <CheveronIcon :class="{ActiveChevron:RoomateActive}" class="tw-mr-auto" style="width: 12px; height: 12px"/>
              </div>
              <div :class="{ActiveMenu:RoomateActive}" v-if="!decrease_width" class="px-3 tw-h-0 tw-overflow-hidden" >
                <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                  <nuxt-link
                    exact-active-class="SingleShortCutActive"
                    to="/migration/allroomate"
                    class="SingleShortCut py-2 w-100 d-flex align-items-center gap-4"
                    :class="[decrease_width ? 'p-3' : 'px-3']"
                  >
                    <BedIcon class="svg_icons_size" v-if="decrease_width"/>
                    <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>
                    <span v-if="!decrease_width" class="font_sidebar_size"> همه آگهی های هم خانه</span>
                  </nuxt-link>
                </div>

                <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                  <nuxt-link
                    exact-active-class="SingleShortCutActive"
                    to="/migration/myroomateinfo"
                    class="SingleShortCut  py-2 w-100 d-flex align-items-center gap-4"
                    :class="[decrease_width ? 'p-3' : 'px-3']"
                  >
                    <BedIcon class="svg_icons_size" v-if="decrease_width"/>
                    <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>

                    <span v-if="!decrease_width" class="font_sidebar_size">اطلاعات هم خانه</span>
                  </nuxt-link>
                </div>
                <div class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
                  <nuxt-link
                    exact-active-class="SingleShortCutActive"
                    to="/migration/setting/roommatecondition"
                    class="SingleShortCut  py-2 w-100 d-flex align-items-center gap-4"
                    :class="[decrease_width ? 'p-3' : 'px-3']"
                  >
                    <BedIcon class="svg_icons_size" v-if="decrease_width"/>
                    <CircleIcon v-if="!decrease_width" style="width: 10px; height: 10px;"/>

                    <span v-if="!decrease_width" class="font_sidebar_size">افزودن هم خانه</span>
                  </nuxt-link>
                </div>
              </div>
            </div>

          </div>
        </div>
      </div>
    </div>
    <!--lg sidebar-->


    <!--Top nav-->
    <section class="container-fluid d-xl-block d-none">
      <div
        class="row headerStyle sticky-top tw-flex tw-flex-row tw-items-center tw-justify-between py-3 position-sticky top-0">
        <div :class="[!isChatLayout ? 'col-xl-4' : 'col-xl-6']" class="col-lg-2 d-flex align-items-center">

          <button  @click="decreaseWidthButton" class="btn ms-3 ShortCutToggle position-relative h-100" type="button">
            <i :class="!decrease_width?'tw-text-pink-400  tw-transform tw-rotate-0':'text-secondary  tw-transform tw-rotate-[24deg]'" class="fas fa-thumbtack   tw-transition tw-z-20 tw-z-10 "></i>

          </button>
          <nuxt-link to="/social">
            بانوکلاب
          </nuxt-link>
        </div>
        <div v-if="!isChatLayout" class="col-lg-5 col-xl-4 header_search tw-relative">
          <SearchIcon
            class="tw-absolute tw-top-1/2 -tw-translate-y-1/2 -tw-translate-x-1/2 tw-flex tw-items-center tw-pointer-events-none"/>
          <input v-model="SearchUsers" value="" type="text" class="form-control" placeholder="جستجو دوستان"
                 autocomplete="off">
          <div
            class="bg-white p-3 SearchContainer"
            v-if="SearchUsers !== ''"
          >
            <div
              class="row styleCursor d-flex justify-content-center align-items-center"
              v-for="(item,idx) in userData"
              :key="idx"
              @click="goToUserProfile(item)"
              style="padding-top: 10px;padding-bottom: 10px;"
            >
              <div class="col-md-1" style="padding-left: 0">
                <img
                  v-if="item.selfieFileData !== ''"
                  class="img-fluid myPicture rounded-circle tw-pl-0"
                  :src="`https://banooclubapi.simagar.com/media/gallery/profile/${item.selfieFileData}`"
                  alt=""
                  style="width: 25px; height: 25px;object-fit: contain"
                />
                <img
                  v-else
                  class="img-fluid myPicture rounded-circle tw-pl-0"
                  :src="userDefault"
                  alt=""
                  style="width: 25px; height: 25px;"
                />

              </div>
              <div class="col-md-11">
                <div class="d-flex flex-column">
                  <div v-if="item.name !== undefined" style="font-size: 16px;font-weight: 500;color: #2d2d2d;">
                    {{ item.name + " " + item.familyName }}
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div :class="[!isChatLayout ? 'col-xl-4' : 'col-xl-6']"
             class="col-lg-4 col-xl-4 icons  d-flex justify-content-end align-items-center">
          <div class="headerIcon">
            <div class="d-flex flex-row justify-content-end align-items-center">
              <nuxt-link to="/social" class="d-flex align-items-center">
                <div v-tooltip.bottom-start="'خانه'"
                     class="custom_header_icon custom_header_size rounded-circle d-flex justify-content-center align-items-center mx-1"
                     @click="goToMyPage">
                  <font-awesome-icon icon="home" color="#999999"></font-awesome-icon>
                </div>
              </nuxt-link>
              <nuxt-link v-tooltip.bottom-start="'مکالمات'" to="/social/Chat" class="d-flex align-items-center">
                <div
                     class="custom_header_icon custom_header_size rounded-circle d-flex justify-content-center align-items-center mx-1"
                     ref="Chat">
                  <i
                    class="far fa-comment"
                    style="color: #999"></i>
                </div>
              </nuxt-link>
              <nuxt-link to="/social/accountsetting/tickets" class="d-flex align-items-center">
                <div v-tooltip.bottom-start="'تیکت ها'"
                     class=" custom_header_icon custom_header_size rounded-circle d-flex justify-content-center align-items-center mx-1">
                  <i class="fas fa-ticket-alt" style="color: #999"></i>
                </div>
              </nuxt-link>

              <div @click.stop="profileHeader" class="position-relative custom_header_size">
                <img
                  v-click-outside="CloseProfileHeader"
                  v-if="$auth.user && $auth.user.baseData &&  $auth.user.baseData.selfie"
                  class="img-fluid headerPicture tw-object-cover"
                  :src="`https://banooclubapi.simagar.com/media/gallery/profile/${$auth.user.baseData.selfie}`"
                  alt=""
                  @click.stop="profileHeader"
                />
                <img
                  v-else
                  class="img-fluid headerPicture tw-object-cover"
                  :src="userDefault"
                  @click.stop="profileHeader"
                  alt=""
                />

                <div
                  class="showProfileHeader position-absolute"
                  v-if="showProfileHeader && !closeHeader"
                >
                  <div class="d-flex flex-column">
                    <div class="userStatus">
                      <nuxt-link to="/Social">
                        <div
                          class="d-flex align-items-center gap-2 flex-row justify-content-start"
                          @click="goToMyPage"
                        >
                          <img
                            v-if="$auth.user && $auth.user.baseData && $auth.user.baseData.selfie"
                            class="img-fluid rounded-circle"
                            style="width: 45px; height: 45px;"
                            :src="`https://banooclubapi.simagar.com/media/gallery/profile/${$auth.user.baseData.selfie}`"
                            alt=""
                            @click="profileHeader"
                          />
                          <img
                            v-else
                            class="img-fluid rounded-circle"
                            style="width: 45px; height: 45px;"
                            :src="userDefault"
                            @click="profileHeader"
                            alt=""
                          />
                          <!--                        <i class="fas fa-user"></i>-->
                          <div
                            v-if="$auth.user &&$auth.user.baseData && $auth.user.baseData.name && $auth.user.baseData.familyName "
                            class="d-flex flex-column align-items-end px-2">
                            <div
                              v-if="$auth.user &&$auth.user.baseData && $auth.user.baseData.name && $auth.user.baseData.familyName "
                              class="profile_pic">{{ $auth.user.baseData.name + ' ' + $auth.user.baseData.familyName }}
                            </div>
                            <div
                              v-if="$auth.user &&$auth.user.baseData && $auth.user.baseData.name && $auth.user.baseData.familyName "
                              class="d-flex profile_username text-primary tw-cursor-pointer"
                              @click="goToUserProfile($auth.user.baseData)">
                              {{ $auth.user.baseData.userName }}
                              @
                            </div>
                          </div>
                        </div>
                      </nuxt-link>
                    </div>
                    <div class="userStatus">
                      <nuxt-link to="/Social/AccountSetting/EditProfileBasic">
                        <div class="d-flex flex-row justify-content-start">
                          <div class="">
                            <!--                            <font-awesome-icon icon="cog" color="black"/>-->
                            <SettingIcon style="width: 18px; height: 18px;"/>
                          </div>
                          <div
                            class="userStatusColor text-dark px-2"
                            @click="CloseProfileHeader"
                            style="font-size: 14px;font-weight: 500;"
                          >
                            ویرایش اطلاعات
                          </div>
                        </div>
                      </nuxt-link>
                    </div>
                    <div class="userStatus defaultUserStatus">
                      <div>
                        <div class="d-flex flex-row justify-content-start align-items-center">
                          <MoneyIcon fill="#565555" style="width: 18px; height: 18px;"/>
                          <div
                            class="userStatusColor text-dark px-2"
                            style="font-size: 14px;font-weight: 500;"
                          >
                            موجودی:
                            {{ Intl.NumberFormat('fa-IR').format($store.state.WalletAmount) }}
                            تومان
                          </div>
                          <button @click="openChargeWalletSideNav" class="btn AddReplyBtn text-white">
                            <PlusIcon fill="#2563eb" style="width: 15px; height: 15px;"/>
                          </button>
                        </div>
                      </div>
                    </div>
                    <div class="userStatus" @click="logoutUser">
                      <div class="d-flex flex-row justify-content-start">
                        <div class="">
                          <LogOutIcon style="width: 18px; height: 18px;"/>
                          <!--                          <font-awesome-icon-->
                          <!--                            class="text-danger"-->
                          <!--                            icon="power-off"-->
                          <!--                          />-->
                        </div>
                        <div class="userStatusColor text-dark px-2" style="font-size: 14px;font-weight: 500;">
                          خروج
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="custom_header_size d-flex justify-content-center align-items-center "
                   @click="$emit('sidenavToggle')">
                <!--                <PrivacyIcon style="width: 25px; height: 25px;"/>-->
                <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-7 tw-w-7" fill="none" viewBox="0 0 24 24"
                     stroke="#999" stroke-width="2">
                  <path stroke-linecap="round" stroke-linejoin="round"
                        d="M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066c-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.996.608 2.296.07 2.572-1.065z"/>
                  <path stroke-linecap="round" stroke-linejoin="round" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"/>
                </svg>

              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    <ChargeWalletSideNav :show="displayChargeSideNav" @close="displayChargeSideNav = false"/>


    <!--Top nav-->
    <!--Mobile topNav-->
    <section class="sticky_header d-xl-none d-block w-100">
      <div class="headerStyleMobile py-2">
        <div v-if="!want_to_search" class="px-2 d-flex justify-content-between align-items-center">
          <div  data-bs-toggle="offcanvas" data-bs-target="#test" class="col-lg-2 d-flex align-items-center">

            <button   class="btn ms-3 ShortCutToggle position-relative h-100" type="button">
              <i :class="!decrease_width?'tw-text-pink-400  tw-transform tw-rotate-0':'text-secondary  tw-transform tw-rotate-[24deg]'" class="fas fa-thumbtack   tw-transition tw-z-20 tw-z-10 "></i>

            </button>
            <nuxt-link to="/social">
              بانوکلاب
            </nuxt-link>
          </div>
          <font-awesome-icon @click="want_to_search = !want_to_search" icon="search" color="#999"/>
          <div class="p-3" @click="$emit('sidenavToggle')">
            <!--            <PrivacyIcon style="width: 25px; height: 25px;"/>-->
            <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-7 tw-w-7" fill="none" viewBox="0 0 24 24" stroke="#999"
                 stroke-width="2">
              <path stroke-linecap="round" stroke-linejoin="round"
                    d="M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066c-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.996.608 2.296.07 2.572-1.065z"/>
              <path stroke-linecap="round" stroke-linejoin="round" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"/>
            </svg>

          </div>
        </div>
        <div v-else class="px-2">
          <div class="d-flex justify-content-between align-items-center">
            <form class="col-11 px-1">
            <span class="searchIconMobile position-absolute">
              <font-awesome-icon icon="search" color="#999"/>
            </span>
              <input v-model="SearchUsers" value="" type="text" class="form-control search_style"
                     placeholder="جستجو دوستان" autocomplete="off">
            </form>
            <button
              type="button"
              class="btn-close text-reset col-1 px-1"
              @click="want_to_search = !want_to_search"
              aria-label="Close"
            ></button>
          </div>
          <div class="w-100 bg-white p-3" v-if="SearchUsers !== ''">
            <div
              class="row styleCursor d-flex justify-content-center align-items-center"
              v-for="(item,idx) in userData"
              :key="idx"
              @click="goToUserProfile(item)"
              style="padding-top: 10px;padding-bottom: 10px;"
            >
              <div class="col-1 d-flex" style="padding-left: 0">
                <img
                  v-if="item.selfieFileData !== ''"
                  style="width: 25px;height: 25px;object-fit: contain"
                  class="myPicture rounded-circle align-self-center"
                  :src="`https://banooclubapi.simagar.com/${item.selfieFileData}`"
                  alt=""
                />
                <img
                  v-else
                  class="img-fluid myPicture rounded-circle tw-pl-0"
                  :src="userDefault"
                  alt=""
                  style="width: 25px; height: 25px;object-fit: contain"
                />
              </div>
              <div class="col-11">
                <div class="d-flex flex-column">
                  <div v-if="item.name !== undefined" style="font-size: 16px; font-weight: 500;color: #2d2d2d;">
                    <small class="text-dark">{{
                        item.name + " " + item.familyName
                      }}</small>
                  </div>

                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    <section class="sticky_footer d-xl-none d-block w-100">
      <div class="headerStyleMobile py-2">
        <div class="headerIcon">
          <div class="d-flex flex-row justify-content-around align-items-center">
            <div
              class="custom_header_icon custom_header_size rounded-circle d-flex justify-content-center align-items-center mx-1"
              @click="goToMyPage">
              <nuxt-link to="/social"
              >
                <font-awesome-icon
                  icon="home"
                  color="#999999"
                  v-tooltip.bottom-start="'صفحه اصلی'"
                >
                  >
                </font-awesome-icon
                >
              </nuxt-link
              >
            </div>

            <div
              class="custom_header_icon custom_header_size rounded-circle d-flex justify-content-center align-items-center mx-1"
              ref="Chat">
              <nuxt-link to="/social/Chat"><i
                class="far fa-comment"
                style="color: #999"
                v-tooltip.bottom-start="'چت'"
              ></i
              ></nuxt-link>
            </div>

            <div
              class=" custom_header_icon custom_header_size rounded-circle d-flex justify-content-center align-items-center mx-1">
              <nuxt-link to="/social/accountsetting/tickets"
              ><i
                class="fas fa-ticket-alt"
                style="color: #999"
                v-tooltip.bottom-start="'تیکت ها'"
              ></i
              ></nuxt-link>
            </div>

              <div  @click="RouteToEditProfile" class="position-relative custom_header_size">
                <img
                  v-if="$auth.user && $auth.user.baseData && $auth.user.baseData.selfie"
                  class="img-fluid headerPicture tw-object-cover"
                  :src="`https://banooclubapi.simagar.com/media/gallery/profile/${$auth.user.baseData.selfie}`"
                  alt=""

                />
                <img
                  v-else
                  class="img-fluid headerPicture  tw-object-cover"
                  :src="userDefault"

                  alt=""
                />

                <div class="showProfileHeader position-absolute" v-if="showProfileHeader && !closeHeader">
                  <div class="d-flex flex-column">
                    <div class="userStatus">
                      <nuxt-link to="/Social">
                        <div
                          class="d-flex align-items-center gap-2 flex-row justify-content-start rounded-circle"
                          @click="goToMyPage"
                        >
                          <img
                            v-if="loggedInfoData.selfie"
                            class="img-fluid rounded-circle"
                            style="width: 45px; height: 45px;"
                            :src="`https://banooclubapi.simagar.com/${loggedInfoData.selfie}`"
                            alt=""
                            @click="profileHeader"
                          />
                          <img
                            v-else
                            class="img-fluid rounded-circle"
                            style="width: 45px; height: 45px;"
                            :src="userDefault"
                            @click="profileHeader"
                            alt=""
                          />
                          <!--                        <i class="fas fa-user"></i>-->
                          <div
                            v-if="$auth.user &&$auth.user.baseData && $auth.user.baseData.name && $auth.user.baseData.familyName "
                            class="d-flex flex-column align-items-end px-2">
                            <div
                              v-if="$auth.user &&$auth.user.baseData && $auth.user.baseData.name && $auth.user.baseData.familyName "
                              class="profile_pic">{{ $auth.user.baseData.name + ' ' + $auth.user.baseData.familyName }}
                            </div>
                            <div
                              v-if="$auth.user &&$auth.user.baseData && $auth.user.baseData.name && $auth.user.baseData.familyName "
                              class="d-flex profile_username text-primary tw-cursor-pointer"
                              @click="goToUserProfile($auth.user.baseData)">
                              {{ $auth.user.baseData.userName }}
                              @
                            </div>
                          </div>
                        </div>
                      </nuxt-link>
                    </div>
                    <div class="userStatus">
                      <nuxt-link to="/Social/AccountSetting/EditProfileBasic">
                        <div class="d-flex flex-row justify-content-start">
                          <div class="">
                            <!--                            <font-awesome-icon icon="cog" color="black"/>-->
                            <SettingIcon style="width: 18px; height: 18px;"/>
                          </div>
                          <div
                            class="userStatusColor text-dark px-2"
                            @click="CloseProfileHeader"
                            style="font-size: 14px;font-weight: 500;"
                          >
                            ویرایش اطلاعات
                          </div>
                        </div>
                      </nuxt-link>
                    </div>
                    <div class="userStatus defaultUserStatus">
                      <div>
                        <div class="d-flex flex-row justify-content-start align-items-center">
                          <MoneyIcon fill="#565555" style="width: 18px; height: 18px;"/>
                          <div
                            class="userStatusColor text-dark px-2"
                            style="font-size: 14px;font-weight: 500;"
                          >
                            موجودی:
                            {{ Intl.NumberFormat('fa-IR').format(100000) }}
                            تومان
                          </div>
                          <button @click="openChargeWalletSideNav" class="btn AddReplyBtn text-white">
                            <PlusIcon fill="#2563eb" style="width: 15px; height: 15px;"/>
                          </button>
                        </div>
                      </div>
                    </div>

                    <div class="userStatus" @click="logoutUser">
                      <div class="d-flex flex-row justify-content-start">
                        <div class="">
                          <LogOutIcon style="width: 18px; height: 18px"/>
                        </div>
                        <div class="userStatusColor text-dark px-2" style="font-size: 14px;font-weight: 500;">
                          خروج
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
          </div>
        </div>

      </div>
    </section>
    <!--Mobile topNav-->

    <div
      class="offcanvas offcanvas-start sidebar-bg"
      tabindex="-1"
      id="offcanvasChargePayment"
      aria-labelledby="offcanvasChargePayment"
      style="z-index: 9999999;"
    >

      <div class="offcanvas-body d-flex flex-column">
        <h3>شارژ کیف پول</h3>
        <div class="col-12 pt-3">
          <label>مبلغ</label>
          <div class="d-flex justify-content-center align-items-center">
            <input type="text" class="with-border" placeholder="مبلغ">
            <span class="px-2">تومان</span>
          </div>
        </div>
        <div class="loadmore mt-auto pt-3">
          <button type="button" class="button tw-w-full mt-auto">
            پرداخت
          </button>
        </div>


      </div>
    </div>


  </section>
</template>

<script>

import {mapGetters} from "vuex";
import SearchIcon from "../components/Icons/SearchIcon";
import HomeIcon from "../components/Icons/HomeIcon";
import MessageIcon from "../components/Icons/MessageIcon";
import TicketIcon from "../components/Icons/TicketIcon";
import AllUsersIcon from "../components/Icons/AllUsersIcon";
import MenuIcon from "../components/Icons/MenuIcon";
import PictureIcon from "../components/Icons/PictureIcon";
import BedIcon from "../components/Icons/BedIcon";
import FriendsIcon from "../components/Icons/FriendsIcon";
import AdsIcon from "../components/Icons/AdsIcon";
import DashBoard from "../components/Icons/DashBoard";
import OrderIcon from "../components/Icons/OrderIcon";
import BlogIcon from "../components/Icons/BlogIcon";
import PlusIcon from "../components/Icons/PlusIcon";
import SettingIcon from "../components/Icons/SettingIcon";
import LogOutIcon from "../components/Icons/LogOutIcon";
import ProductsIcon from "../components/Icons/ProductsIcon";
import MoneyIcon from "../components/Icons/MoneyIcon";
import ForumIcon from "../components/Icons/ForumIcon";
import CircleIcon from "../components/Icons/CircleIcon";
import CheveronIcon from "../components/Icons/CheveronIcon";
import ChargeWalletSideNav from "../components/ChargeWalletSideNav";
import PrivacyIcon from "../components/Icons/PrivacyIcon"
import ProfileIcon from "../components/Icons/ProfileIcon";
import HeartIcon from "@/components/Icons/HeartIcon";

export default {
  name: "Header",
  components: {
    HeartIcon,
    ProfileIcon,
    BlogIcon,
    PrivacyIcon,
    ChargeWalletSideNav,
    DashBoard,
    CheveronIcon,
    CircleIcon,
    ForumIcon,
    MoneyIcon,
    ProductsIcon,
    LogOutIcon,
    SettingIcon,
    PlusIcon,
    AdsIcon,
    FriendsIcon, BedIcon, PictureIcon, MenuIcon, AllUsersIcon, TicketIcon, MessageIcon, HomeIcon, SearchIcon, OrderIcon
  },
  head() {
    return {
      bodyAttrs: {
        class: this.displayChargeSideNav ? "fixed-body" : "",
      },
    };
  },

  data() {
    return {
      AdsActive:false,
      ServicesActive:false,
      ForumsActive:false,
      RoomateActive:false,
      displayChargeSideNav: false,
      want_to_search: false,
      decrease_width_button: false,
      showProfileHeader: false,
      numberMessage: [1, 2, 3, 4, 5],
      newImage: null,
      newUserImage: null,
      bannerImage: "",
      userImage: "",
      userImageApi: "",
      BackgroundApi: "",
      userinfo: {},
      ScrollPosition: 0,
      SearchUsers: "",
      userData: " ",
      FollowBtn: true,
      ShortCut: false,
      DefaultImg: require("../assets/images/defaultBanner.jpg"),
      userDefault: require("../assets/images/defaultUser.png"),
    };
  },
  emits: ['toggle_width'],
  props: {
    closeHeader: {
      required: false,
    },
    isChatLayout: {
      type: Boolean,
      required: false,
      default: false,
    },
    decrease_width: {
      type: Boolean,
      required: true,
      default: false,
    }
  },
  watch: {
    SocketId: function (val, oldVal) {
      let tmpLabel = "";

      if (
        val === this.$auth.user.userId &&
        !this.$route.path.includes("chat")
      ) {
        tmpLabel = "شما یک پیام جدید دارید";
        tmpLabel = "";
        this.$store.commit("SetDefaultSocketId");
        this.$toast.success("شما یک پیام جدید دارید");
      }
    },
    SearchUsers: async function (val, oldVal) {
      try {
        const response =
          await this.$repositories.searchUserByName.searchUserByName(val);
        this.userData = response.data;
        console.log(this.userData);
      } catch (error) {
        console.log(error);
      }
    },
    closeHeader: function (newVal, oldVal) {
      if (newVal) {


        this.showProfileHeader = false;

      }
    },
  },
  computed: {
    ...mapGetters(["SocketId"]),
    userAvatar() {
      return this.loggedInfoData?.selfie
        ? this.BaseUrl + this.loggedInfoData.selfie
        : "/defaultUser.png";
    },
    BaseUrl() {
      return process.env.pic;
    },


    loggedInfoData() {
      if (this.$store.state.loggedInfo) {
        return this.$store.state.loggedInfo;
      } else {
        return this.$store.state.HeaderData
      }
    },
    async userInfo() {
      let userInfo = [];
      if (this.$route.params.id) {
        try {
          const response = await this.$repositories.getUserIndex.getUserIndex(
            this.$route.params.id
          );
          this.$store.commit("SetSearchedUserStatus", response.data.status);
          this.FollowBtn = response.data.followButton;
          userInfo = response.data.baseData;
          this.imgUserAvatar();
          this.imageBackground();
          return userInfo;
        } catch (error) {
          console.log(error);
        }
      } else {
        try {
          const response =
            await this.$repositories.getUserByToken.getUserByToken();
          userInfo = response.data.userInfo;

          this.imgUserAvatar();
          this.imageBackground();
          return userInfo;
        } catch (error) {
          console.log(error);
        }
      }
    },
  },
  methods: {
    RouteToEditProfile(){
      this.$router.push('/Social/AccountSetting/EditProfileBasic/')
    },
    openChargeWalletSideNav() {
      this.displayChargeSideNav = true
      this.showProfileHeader = false
    },
    decreaseWidthButton() {
      this.decrease_width_button = !this.decrease_width_button
      this.decrease_width = !this.decrease_width
      this.$root.$emit("change-cols")
      this.$emit("toggle_width")
    },
    MouseOverRightMenu() {
      if (this.decrease_width_button) {
        this.decrease_width = !this.decrease_width
        this.$emit("toggle_width")
      }

    },
    MouseLeaveRightMenu() {
      if (this.decrease_width_button) {
        this.decrease_width = !this.decrease_width
        this.$emit("toggle_width")
      }

    },
    updateScroll() {
      this.ScrollPosition = window.scrollY;
    },
    async goToUserProfile(user) {
      try {
        this.$router.push({
          path: `/user/${user.userName}/posts`
        });
        this.SearchUsers = "";
      } catch (e) {
        console.log(e)
      }
    },
    async UnFollow() {
      try {
        await this.$repositories.deleteASingleFollowing.deleteASingleFollowing(
          this.$route.params.id
        );
      } catch (error) {
        console.log(error);
      }
    },
    async Follow() {
      try {
        const response =
          await this.$repositories.createAFollowRequest.createAFollowRequest(
            this.$route.params.id
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
    CloseProfileHeader() {

      this.showProfileHeader = false;
    },
    search() {



      this.showProfileHeader = false;
      this.$emit("open");
    },
    profileHeader() {



      this.closeHeader = false

      this.showProfileHeader = !this.showProfileHeader;
      this.$emit("open");
    },
    InputEditPhoto(e) {
      this.$nuxt.$loading.start();
      const file = e.target.files[0];
      this.newImage = URL.createObjectURL(file);
      const that = this;
      const reader = new FileReader();
      reader.onload = (function (theFile) {
        return function () {
          const binaryData = reader.result;
          that.bannerImage = window.btoa(binaryData);
        };
      })(file);
      reader.readAsBinaryString(file);

      setTimeout(async () => {
        try {
          await this.$repositories.updateUserDetails.updateUserDetails({
            bannerFileData: this.bannerImage,
          });
          this.getUserInfo();
        } catch (error) {
          console.log(error);
        } finally {
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
        }
      }, 1000);
    },
    callInputMethod() {
      document.querySelector(".InputEditPhoto").click();
    },
    InputEditUserPhoto(e) {
      this.$nuxt.$loading.start();
      const file = e.target.files[0];
      this.newImageUser = URL.createObjectURL(file);
      const that = this;
      const reader = new FileReader();
      reader.onload = (function (theFile) {
        return function () {
          const binaryData = reader.result;
          that.userImage = window.btoa(binaryData);
        };
      })(file);
      reader.readAsBinaryString(file);

      setTimeout(async () => {
        try {
          await this.$repositories.updateUserDetails.updateUserDetails({
            selfieFileData: this.userImage,
          });
          this.getUserInfo();
        } catch (error) {
          console.log(error);
        } finally {
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
        }
      }, 1000);
    },
    callUserInputMethod() {
      document.querySelector(".InputUserEditPhoto").click();
    },
    async logoutUser() {
      await this.$auth.logout();
    },
    imgUserAvatar() {
      if (this.userinfo.selfieFileData !== null) {
        this.userImageApi = `https://banooclubapi.simagar.com/${this.userinfo.selfieFileData}`;
      }
    },
    imageBackground() {
      if (this.userinfo.bannerFileData !== null) {
        this.BackgroundApi = `https://banooclubapi.simagar.com/${this.userinfo.bannerFileData}`;
      }
    },
    async getUserInfo() {
      try {
        const response =
          await this.$repositories.getUserByToken.getUserByToken();
        this.$store.commit("SetUserData", response.data);
        let MypageData = {};
        this.userinfo = response.data.userInfo;
        MypageData.logCounts = response.data.logCounts;
        MypageData.logCountsDistinct = response.data.logCountsDistinct;
        MypageData.unreadTicketCount = response.data.unreadTicketCount;
        MypageData.bio = response.data.userInfo.userSetting.bio;
        MypageData.userName = response.data.userInfo.userName;
        MypageData.selfie = response.data.userInfo.selfieFileData;
        this.imgUserAvatar();
        this.imageBackground();
        this.$store.commit("SetUserMypageInfo", MypageData);
      } catch (error) {
        console.log(error);
      }
    },
    async goToMyPage() {
      try {
        const response =
          await this.$repositories.getUserByToken.getUserByToken();
        this.$store.commit("SetUserData", response.data);
      } catch (error) {
        console.log(error);
      }
    },
  },
  async mounted() {
    window.addEventListener("scroll", this.updateScroll);
    if (!this.$route.query.id) {
          await this.goToMyPage()
    } else {
      try {
        const response = await this.$repositories.getUserIndex.getUserIndex(
          this.$route.params.id
        );
        this.$store.commit("SetUserData", response.data);
      } catch (error) {
        console.log(error);
      }
    }


  },
};
</script>

<style scoped>
@media screen and (max-width: 425px) {
  .right_menu_mobile {
    width: 75% !important;

  }
}

.styleCursor:hover {
  cursor: pointer;
  background: #f0f2f5;
  border-radius: 10px;
  padding-top: 10px;
  padding-bottom: 10px;

}

@media screen and (max-width: 1024px) {
  .showProfileHeader {
    top: -158px !important;
    left: -26px !important;
  }
}

.profile_username {
  font-size: 12.5px;
  font-weight: normal;
  color: black;
}

.profile_pic {
  line-height: 20px;
  font-weight: 600;
  color: #333;
  font-size: 16px;
}

.svg_icons_size {
  width: 24px !important;
  height: 24px !important;
}

.decrease_width {
  width: 90px !important;
}

.search_style {
  height: 44px;
  color: #393939;
  padding: 0 30px 0 40px;
  outline: 0;
  background: #f0f2f5;
  border: 1px solid transparent;
  border-radius: 2rem;
  /*min-width: 550px;*/
  font-size: 15px;
}

.sticky_header {
  z-index: 2;
  position: sticky;
  top: 0

}

.sticky_footer {
  z-index: 2;
  position: fixed;
  bottom: 0

}

.header_font_sidebar_size {
  font-size: 16px;
  font-weight: 600;
}

.font_sidebar_size {
  font-size: 14px;
  font-weight: 600;
}

.header_search input {
  height: 44px;
  color: #393939;
  padding: 0 30px 0 40px;
  outline: 0;
  background: #f0f2f5;
  border: 1px solid transparent;
  border-radius: 2rem;
  min-width: 500px !important;
  font-size: 15px;
}

@media screen and (min-width: 1441px) {
  .header_search input {
    min-width: 550px !important;

  }
}


.header_search .header_search_dropdown {
  width: 550px;
  padding: 10px;
  margin-top: 0px;
}

.header_search .header_search_dropdown .search_title {
  padding-left: 0.625rem;
  padding-right: 0.625rem;
  margin-top: 0.5rem;
  margin-bottom: 0.25rem;
  font-size: 1.125rem;
  line-height: 1.75rem;
  font-weight: 600;
}

.header_search .header_search_dropdown ul {
  list-style: none;
}

.header_search .header_search_dropdown ul a {
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-align: center;
  -ms-flex-align: center;
  align-items: center;
  padding: 6px 0;
  border-radius: 8px;
  padding-left: 6px;
}

.header_search .header_search_dropdown ul a:hover {
  background: #f0f2f5;
}

.header_search .header_search_dropdown ul .list-avatar {
  width: 35px;
  height: 35px;
  border-radius: 50%;
  margin-right: 15px;
  -o-object-fit: cover;
  object-fit: cover;
}

.header_search .header_search_dropdown ul .list-name {
  -webkit-box-flex: 1;
  -ms-flex: 1 1 0%;
  flex: 1 1 0%;
  font-size: 16px;
  font-weight: 500;
  color: #2d2d2d;
}


.OffCanvas {
  z-index: 99999;
  height: 100%;
  background-color: white;
  transition: 0.5s ease;
  top: 70px;
  right: -15px;
  width: 0px;

  border-left: 1px solid rgba(0, 0, 0, 0.2);
  position: fixed;
}

.OffCanvasActive {
  width: 17.3% !important;
  transition: 0.5s ease;
}

.SingleShortCut {
  color: #585757;
  transition: 0.2s ease;
  border-radius: 10px;
}

.SingleShortCut:hover {
  background-color: #f3f4f6;
  /*color: white;*/
  transition: 0.2s ease;
}

.SingleShortCutActive {
  color: #2a41e8 !important;
  background-color: rgb(42 65 232 / 7%) !important;
  border-radius: 10px !important;
}

.ShortCutToggle {
  transition: 0.2s ease;
  border:none!important;
  box-shadow: 2px 6px 50px -6px rgba(255,28,204,0.76);
}

.ShortCutToggle:hover {

  transition: 0.2s ease;
  color: #3948dc;
  box-shadow: 2px 6px 50px -6px rgba(255,28,204,0.76);
}




.list-enter-active {
  animation: list 0.3s ease-out;
}

.list-leave-active {
  animation: list 0.3s ease-in reverse;
}

@keyframes list {
  from {
    opacity: 0;
    transform: translateY(-25px) scale(1);
  }

  to {
    opacity: 1;
    transform: translateY(0) scale(1);
  }
}


::-webkit-scrollbar {
  width: 5px;
}
/* Track */
::-webkit-scrollbar-track {
  background: #f1f1f1;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #888;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #555;
}
.list-enter-active,
.list-leave-active,
.list-move {
  transition: 500ms cubic-bezier(0.59, 0.12, 0.34, 0.95);
  transition-property: opacity, transform;
}

.list-enter {
  opacity: 0;
  transform: translateX(50px) scaleY(0.5);
}

.list-enter-to {
  opacity: 1;
  transform: translateX(0) scaleY(1);
}

.list-leave-active {
  position: absolute;
}

.list-leave-to {
  opacity: 0;
  transform: scaleY(0);
  transform-origin: center top;
}


.searchIconMobile {
  left: 20px;
  bottom: 10px;
}

.userStatus {
  font-size: 12px;
  text-transform: capitalize;
  background: white;
  padding: 10px 15px;
}

.userStatus:hover {
  background: #f1f1f1;
  border-radius: 8px !important;
}

.userStatus:hover .padding_on_hover {
  padding: 8px !important;
}

.userStatusColor:hover {
  color: #007bff;
}

.userStatusColor {
  color: #595459;
  margin-right: 8px;
}

.defaultUserStatus:hover {
  cursor: default !important;
}

.showProfileHeader {
  top: 40px;
  left: -10px;
  background: white;
  border-radius: 8px;
  width: 270px;
  line-height: initial;
  border: 1px solid #e1e8ed;
  padding: 8px;
}



.headerStyle {
  background: #fff;
  /* height: 60px; */
  /* padding: 0px 0px 0px 48px; */
  box-shadow: 0 0 50px rgb(251 226 255);
  z-index: 999999;
  position: relative;
  justify-content: space-between;
}

.headerStyleMobile {
  width: 100%;
  box-shadow: 0 0 5px rgba(0, 0, 0, 0.2);
  z-index: 8;
  position: relative;
  background: white;
  color: #fff;
  height: 65px;
}

a {
  text-decoration: none;
}



.SearchContainer {
  position: absolute;
  width: 96%;
  height: auto;
  top: 44px;
  box-shadow: rgb(0 0 0 / 10%) 0px 10px 25px -5px, rgb(0 0 0 / 4%) 0px 13px 10px -5px;
}


.headerIcon {
  cursor: pointer;
}

.headerPicture {
  width: 38px;
  height: 38px;
  vertical-align: middle;
  border-radius: 50px;
  /*transform: scale(0.7);*/
}

.sidebar-bg {
  background-color: white;
  right: -15px !important;
  z-index: 999999 !important;
}


.ActiveChevron{
  transition: .2s ease;
  transform: rotate(180deg);
}
.ActiveMenu{
  transition: .2s ease;
 @apply   tw-h-[150px]

}
.ActiveMenuService{
  transition: .2s ease;
  @apply   tw-h-[170px]

}
</style>
