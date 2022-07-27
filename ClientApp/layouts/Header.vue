<template>
  <section class="m-0 position-sticky top-0 tw-z-[1000]" >
    <!--    mobile menu-->
    <div
      class="offcanvas offcanvas-end sidebar-bg right_menu_mobile"
      tabindex="-1"
      id="test"
      aria-labelledby="test"
    >
      <div  class="offcanvas-body px-2" style="height: 700px!important;overflow-y: scroll!important;">
        <div class="d-flex flex-column" style="padding-right: 15px">
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
            <div v-if="!decrease_width" :class="[decrease_width ? '' : '']" class=" tw-cursor-default SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4">
              <AdsIcon class="svg_icons_size"/>
              <span class="header_font_sidebar_size">آگهی ها </span>
              <CheveronIcon class="tw-mr-auto" style="width: 12px; height: 12px"/>
            </div>
            <div class="px-3"  :class="[decrease_width ? '' : '']">
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

            <div v-if="!decrease_width" :class="[decrease_width ? '' : '']" class=" tw-cursor-default SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4">
              <ProductsIcon class="svg_icons_size tw-text-red-500"/>
              <span v-if="!decrease_width" class="header_font_sidebar_size">خدمات </span>
              <CheveronIcon class="tw-mr-auto" style="width: 12px; height: 12px"/>
            </div>
            <div class="px-3"  :class="[decrease_width ? '' : '']">
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
              <div v-if=" $auth.user&& $auth.user && $auth.user.baseData&&$auth.user.baseData.userType===3" class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
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
              <div v-if=" $auth.user&& $auth.user && $auth.user.baseData&& $auth.user.baseData.userType===3" class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
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
          <div v-if="$auth.user && $auth.user.userInfo&&  $auth.user.userInfo.type  &&$auth.user.userInfo.type === 3" class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
            <nuxt-link
              exact-active-class="SingleShortCutActive"
              to="/Payments/"
              class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
            >
              <MoneyIcon fill="#ef4444" class="svg_icons_size"/>
              <span v-if="!decrease_width" class="header_font_sidebar_size">پرداخت ها</span>
            </nuxt-link>
          </div>
          <div v-if="$auth.user && $auth.user.userInfo&&  $auth.user.userInfo.type  && $auth.user.userInfo.type === 3" class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
            <nuxt-link
              exact-active-class="SingleShortCutActive"
              to="/Orders/"
              class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
            >
              <OrderIcon fill="rgb(245 158 11)"  class="svg_icons_size text-warning"/>
              <span v-if="!decrease_width" class="header_font_sidebar_size">سفارشات</span>
            </nuxt-link>
          </div>
          <div :class="[decrease_width ? '' : '']" class="py-1">
            <div v-if="!decrease_width" :class="[decrease_width ? '' : '']" class=" SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4">
              <ForumIcon class="svg_icons_size"/>
              <span v-if="!decrease_width" class="header_font_sidebar_size">انجمن ها </span>
              <CheveronIcon class="tw-mr-auto" style="width: 12px; height: 12px"/>
            </div>
            <div class="px-3"  :class="[decrease_width ? '' : '']">
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

            <div v-if="!decrease_width" :class="[decrease_width ? '' : '']" class=" tw-cursor-default SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4">
              <BedIcon class="svg_icons_size"/>
              <span v-if="!decrease_width" class="header_font_sidebar_size">هم خانه</span>
              <CheveronIcon class="tw-mr-auto" style="width: 12px; height: 12px"/>
            </div>
            <div class="px-3"  :class="[decrease_width ? '' : '']">
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
                  <BedIcon class="svg_icons_size"  v-if="decrease_width"/>
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
        style="z-index: 999999 !important; overflow-x: hidden; border-left: 1px solid #eeeeee"
        :class="{ decrease_width: decrease_width===true ,OffCanvasActive:decrease_width===false }"
        @mouseleave="MouseLeaveRightMenu"
        @mouseenter="MouseOverRightMenu"
      >
        <div class="offcanvas-body px-2" style="height: 700px!important;overflow-y: scroll!important;">
          <div class="d-flex flex-column" style="padding-right: 15px">
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
              <div v-if="!decrease_width" :class="[decrease_width ? '' : '']" class=" tw-cursor-default SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4">
                <AdsIcon class="svg_icons_size"/>
                <span class="header_font_sidebar_size">آگهی ها </span>
                <CheveronIcon class="tw-mr-auto" style="width: 12px; height: 12px"/>
              </div>
              <div class="px-3"  :class="[decrease_width ? '' : '']">
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

              <div v-if="!decrease_width" :class="[decrease_width ? '' : '']" class=" tw-cursor-default SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4">
                <ProductsIcon class="svg_icons_size tw-text-red-500"/>
                <span v-if="!decrease_width" class="header_font_sidebar_size">خدمات </span>
                <CheveronIcon class="tw-mr-auto" style="width: 12px; height: 12px"/>
              </div>
              <div class="px-3"  :class="[decrease_width ? '' : '']">
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
                <div v-if=" $auth.user&& $auth.user && $auth.user.baseData&& $auth.user.baseData.userType===3" class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
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
                <div v-if=" $auth.user&& $auth.user &&  $auth.user.baseData && $auth.user.baseData.userType===3" class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
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
            <div v-if="$auth.user && $auth.user.userInfo&&  $auth.user.userInfo.type  && $auth.user.userInfo.type === 3" class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
              <nuxt-link
                exact-active-class="SingleShortCutActive"
                to="/Payments/"
                class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
              >
                <MoneyIcon fill="#ef4444" class="svg_icons_size"/>
                <span v-if="!decrease_width" class="header_font_sidebar_size">پرداخت ها</span>
              </nuxt-link>
            </div>
            <div v-if="$auth.user && $auth.user.userInfo&&  $auth.user.userInfo.type  && $auth.user.userInfo.type === 3" class="d-flex align-items-center py-1" data-bs-dismiss="offcanvas">
              <nuxt-link
                exact-active-class="SingleShortCutActive"
                to="/Orders/"
                class="SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4"
              >
                <OrderIcon fill="rgb(245 158 11)"  class="svg_icons_size text-warning"/>
                <span v-if="!decrease_width" class="header_font_sidebar_size">سفارشات</span>
              </nuxt-link>
            </div>
            <div :class="[decrease_width ? '' : '']" class="py-1">
              <div v-if="!decrease_width" :class="[decrease_width ? '' : '']" class=" tw-cursor-default SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4">
                <ForumIcon class="svg_icons_size"/>
                <span v-if="!decrease_width" class="header_font_sidebar_size">انجمن ها </span>
                <CheveronIcon class="tw-mr-auto" style="width: 12px; height: 12px"/>
              </div>
              <div class="px-3"  :class="[decrease_width ? '' : '']">
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
              <div v-if="!decrease_width" :class="[decrease_width ? '' : '']" class=" tw-cursor-default SingleShortCut p-3 py-2 w-100 d-flex align-items-center gap-4">
                <BedIcon class="svg_icons_size"/>
                <span v-if="!decrease_width" class="header_font_sidebar_size">هم خانه</span>
                <CheveronIcon class="tw-mr-auto" style="width: 12px; height: 12px"/>
              </div>
              <div class="px-3" :class="[decrease_width ? '' : '']">
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
                    <BedIcon class="svg_icons_size"  v-if="decrease_width"/>
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
      <div class="row headerStyle sticky-top tw-flex tw-flex-row tw-items-center tw-justify-between py-3 position-sticky top-0">
        <div :class="[!isChatLayout ? 'col-xl-4' : 'col-xl-6']" class="col-lg-2 d-flex align-items-center">
          <!--          <button-->
          <!--            class="btn ms-3 ShortCutToggle h-100"-->
          <!--            type="button"-->
          <!--            @click="ToggleShortCut"-->
          <!--            @mouseenter="ToggleShortCut"-->
          <!--          >-->
          <!--            <i class="fas fa-stream"></i>-->
          <!--          </button>-->
          <button v-if="!isChatLayout"
            @click="decreaseWidthButton"
            class="btn ms-3 ShortCutToggle h-100"
            type="button">
            <MenuIcon style="transform: scale(-1,-1);"/>
            <!--            <i class="fas fa-stream"></i>-->
          </button>
          <nuxt-link to="/social">
            بانوکلاب
<!--            <img-->
<!--              src="../assets/images/PoshtebamPlusLogo.png"-->
<!--              width="100px"-->
<!--              height="40px"-->
<!--              alt="PoshtebamPlus Logo"-->
<!--            />-->
          </nuxt-link>
        </div>
        <div v-if="!isChatLayout" class="col-lg-5 col-xl-4 header_search tw-relative">
          <SearchIcon class="tw-absolute tw-top-1/2 -tw-translate-y-1/2 -tw-translate-x-1/2 tw-flex tw-items-center tw-pointer-events-none"/>
          <input v-model="SearchUsers" value="" type="text" class="form-control" placeholder="جست و جو دوستان" autocomplete="off">
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
        <div :class="[!isChatLayout ? 'col-xl-4' : 'col-xl-6']" class="col-lg-4 col-xl-4 icons  d-flex justify-content-end align-items-center">
          <div class="headerIcon">
            <div class="d-flex flex-row justify-content-end align-items-center">
              <nuxt-link to="/social" class="d-flex align-items-center">
                <div v-tooltip.bottom-start="'خانه'" class="custom_header_icon custom_header_size rounded-circle d-flex justify-content-center align-items-center mx-1" @click="goToMyPage">
                  <font-awesome-icon icon="home" color="#999999"></font-awesome-icon>
                </div>
              </nuxt-link>
<!--              <div class="custom_header_icon custom_header_size rounded-circle d-flex justify-content-center align-items-center mx-1">-->
<!--                <nuxt-link to="/social/accountsetting/myposts" class="d-flex align-items-center">-->
<!--                  <i class="far fa-plus-square" style="color: #999" v-tooltip.bottom-start="'افزودن پست'"></i>-->
<!--                </nuxt-link>-->
<!--              </div>-->
              <nuxt-link to="/social/Chat" class="d-flex align-items-center">
                <div v-tooltip.bottom-start="'مکالمات'" class="custom_header_icon custom_header_size rounded-circle d-flex justify-content-center align-items-center mx-1" ref="Chat">
                  <i
                    class="far fa-comment"
                    style="color: #999"></i>
                </div>
              </nuxt-link>

              <!--              <div class="custom_header_icon custom_header_size rounded-circle d-flex justify-content-center align-items-center mx-1">-->
<!--                <nuxt-link to="/migration/alladvertise"-->
<!--                ><i-->
<!--                  class="fas fa-ad"-->
<!--                  style="color: #999"-->
<!--                ></i-->
<!--                ></nuxt-link>-->
<!--              </div>-->
              <nuxt-link to="/social/accountsetting/tickets" class="d-flex align-items-center">
                <div v-tooltip.bottom-start="'تیکت ها'" class=" custom_header_icon custom_header_size rounded-circle d-flex justify-content-center align-items-center mx-1">
                  <i class="fas fa-ticket-alt" style="color: #999"></i>
                </div>
              </nuxt-link>

<!--              <div  @click="notificationList" class=" position-relative custom_header_size custom_header_icon rounded-circle d-flex justify-content-center align-items-center mx-1">-->
<!--                <font-awesome-icon-->
<!--                  icon="bell"-->
<!--                  color="#999999"-->
<!--                ></font-awesome-icon>-->
<!--                <span v-if="Notifications" class="headerNumberNotification position-absolute d-flex justify-content-center align-items-center rounded-circle">-->
<!--                  <strong>{{ Notifications.length }}</strong>-->
<!--                </span>-->
<!--                <div class="listNotification position-absolute" v-if="showNotificationList">-->
<!--                  <div v-if="Notifications" class="col-12 numberMessageText">-->
<!--                    {{ Notifications.length }} نوتیفکیشن جدید-->
<!--                  </div>-->
<!--                  &lt;!&ndash;-lawyer-&ndash;&gt;-->
<!--                  <div class="col-12" v-for="(item,idx) in Notifications" :key="idx">-->
<!--                    <div class="d-flex flex-row border-bottom w-100 h-100 overflow-hidden">-->
<!--                      <div class="d-flex flex-column text-end position-relative p-2">-->
<!--                        <div class="senderName">-->
<!--                          <small class="text-muted"><i class="fas fa-balance-scale"></i></small>-->
<!--                          <small>{{ item.title }}</small>-->
<!--                        </div>-->
<!--                        <br/>-->
<!--                        <small class="messageText"><small>{{ item.body }}</small>-->
<!--                          <nuxt-link-->
<!--                            v-if="item.link"-->
<!--                            to="/social/accountsetting/editprofilebasic">اینجا کلیک کنید-->
<!--                          </nuxt-link>-->
<!--                        </small>-->
<!--                      </div>-->
<!--                    </div>-->
<!--                  </div>-->
<!--                </div>-->
<!--              </div>-->
<!--              <div  @click="messageList" class="position-relative custom_header_size custom_header_icon rounded-circle d-flex justify-content-center align-items-center mx-1">-->
<!--                <font-awesome-icon-->
<!--                  icon="comment-alt"-->
<!--                  color="#999999"-->
<!--                ></font-awesome-icon>-->
<!--                <span class="headerNumberNotification position-absolute rounded-circle d-flex justify-content-center align-items-center">-->
<!--                  <strong>-->
<!--                    {{ChatNotifs.length }}-->
<!--                  </strong>-->
<!--                </span>-->
<!--                <div-->
<!--                  class="listMessage position-absolute"-->
<!--                  v-if="showMessageList"-->
<!--                >-->
<!--                  <div class="col-12 numberMessageText">-->
<!--                    {{ ChatNotifs.length }} پیام جدید-->
<!--                  </div>-->
<!--                  <div class="col-12" v-for="(item,idx) in ChatNotifs" :key="idx">-->
<!--                    <div-->
<!--                      class="d-flex flex-column text-end position-relative p-2"-->
<!--                    >-->
<!--                      <div class="senderName">-->
<!--                        <small class="text-muted"-->
<!--                        ><i class="fas fa-comment"></i-->
<!--                        ></small>-->
<!--                        <small>مکالمات من</small>-->
<!--                      </div>-->

<!--                      <h4 class="messageTextt">-->
<!--                        <small>{{ item }}</small-->
<!--                        >-->
<!--                        <nuxt-link-->
<!--                          tag="small text-primary mx-2"-->
<!--                          to="/social/Chat"-->
<!--                        >اینجا کلیک کنید-->
<!--                        </nuxt-link-->
<!--                        >-->
<!--                      </h4>-->
<!--                    </div>-->
<!--                  </div>-->
<!--                </div>-->
<!--              </div>-->
              <div class="position-relative custom_header_size">
                <img
                  v-if="$auth.user && $auth.user.baseData &&  $auth.user.baseData.selfie"
                  class="img-fluid headerPicture"
                  :src="`https://banooclubapi.simagar.com/media/gallery/profile/${$auth.user.baseData.selfie}`"
                  alt=""
                  @click="profileHeader"
                />
                <img
                  v-else
                  class="img-fluid headerPicture"
                  :src="userDefault"
                  @click="profileHeader"
                  alt=""
                />

                <div
                  class="showProfileHeader position-absolute"
                  v-if="showProfileHeader && !closeHeader"
                >
                  <div class="d-flex flex-column">
                    <!--                    <div class="userStatus">-->
                    <!--                      <div class="d-flex flex-row justify-content-between">-->
                    <!--                        <div class="">آنلاین</div>-->
                    <!--                        <div class=" "><span class="statusShowingProfile onlineStatus"></span></div>-->
                    <!--                      </div>-->
                    <!--                    </div>-->
                    <!--                    <div class="userStatus">-->
                    <!--                      <div class="d-flex flex-row justify-content-between">-->
                    <!--                        <div class="userStatusColor">دور</div>-->
                    <!--                        <div class=" "><span class="statusShowingProfile awayStatus"></span></div>-->
                    <!--                      </div>-->
                    <!--                    </div>-->
                    <!--                    <div class="userStatus">-->
                    <!--                      <div class="d-flex flex-row justify-content-between">-->
                    <!--                        <div class="userStatusColor">آفلاین</div>-->
                    <!--                        <div class=" "><span class="statusShowingProfile offlineStatus"></span></div>-->
                    <!--                      </div>-->
                    <!--                    </div>-->
                    <!--                    <div class="userStatus">-->
                    <!--                      <nuxt-link to="/">-->
                    <!--                        <div class="d-flex flex-row justify-content-start">-->
                    <!--                          <div class="">-->
                    <!--                            <font-awesome-icon icon="user" color="007bff"/>-->
                    <!--                          </div>-->
                    <!--                          <div class="userStatusColor">نمایش پروفایل</div>-->
                    <!--                        </div>-->
                    <!--                      </nuxt-link>-->
                    <!--                    </div>-->
                    <!--                    <div class="userStatus">-->
                    <!--                      <nuxt-link to="/">-->
                    <!--                        <div class="d-flex flex-row justify-content-start">-->
                    <!--                          <div class="">-->
                    <!--                            <font-awesome-icon icon="user-edit" color="#007bff"/>-->
                    <!--                          </div>-->
                    <!--                          <div class="userStatusColor">ویرایش پروفایل</div>-->
                    <!--                        </div>-->
                    <!--                      </nuxt-link>-->
                    <!--                    </div>-->
                    <!--                    <div class="userStatus">-->
                    <!--                      <nuxt-link to="/">-->
                    <!--                        <div class="d-flex flex-row justify-content-start">-->
                    <!--                          <div class="">-->
                    <!--                            <font-awesome-icon icon="history" color="007bff"/>-->
                    <!--                          </div>-->
                    <!--                          <div class="userStatusColor">گزارش فعالیت</div>-->
                    <!--                        </div>-->
                    <!--                      </nuxt-link>-->
                    <!--                    </div>-->

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
                          <div v-if="$auth.user &&$auth.user.baseData && $auth.user.baseData.name && $auth.user.baseData.familyName " class="d-flex flex-column align-items-end px-2">
                            <div v-if="$auth.user &&$auth.user.baseData && $auth.user.baseData.name && $auth.user.baseData.familyName "  class="profile_pic">{{$auth.user.baseData.name + ' ' + $auth.user.baseData.familyName}}</div>
                            <div v-if="$auth.user &&$auth.user.baseData && $auth.user.baseData.name && $auth.user.baseData.familyName "  class="d-flex profile_username text-primary tw-cursor-pointer" @click="goToUserProfile($auth.user.baseData)">
                              {{ $auth.user.baseData.userName }}
                              @
                            </div>
                          </div>
                        </div>
                      </nuxt-link>
                    </div>
                    <div class="userStatus">
                      <nuxt-link to="/social/accountsetting/editprofilebasic">
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
                            {{Intl.NumberFormat('fa-IR').format($store.state.WalletAmount)}}
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
                          خروج</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="custom_header_size d-flex justify-content-center align-items-center " @click="$emit('sidenavToggle')">
<!--                <PrivacyIcon style="width: 25px; height: 25px;"/>-->
                <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-7 tw-w-7" fill="none" viewBox="0 0 24 24" stroke="#999" stroke-width="2">
                  <path stroke-linecap="round" stroke-linejoin="round" d="M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066c-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.996.608 2.296.07 2.572-1.065z" />
                  <path stroke-linecap="round" stroke-linejoin="round" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
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
    <section  class="sticky_header d-xl-none d-block w-100">
      <div class="headerStyleMobile py-2" >
        <div v-if="!want_to_search" class="px-2 d-flex justify-content-between align-items-center">
          <div class="p-3" data-bs-toggle="offcanvas" data-bs-target="#test">
            <font-awesome-icon
              ref="HiddenCanvas"
              icon="bars"
              size="lg"
              color="#999"
            ></font-awesome-icon>
          </div>
          <div>
            <a href="/social">
              بانوکلاب
<!--              <img-->
<!--                data-not-lazy-->
<!--                class="headerMobileIcon"-->
<!--                width="100px"-->
<!--                height="60px"-->
<!--                src="../assets/images/PoshtebamPlusLogo.png"-->
<!--              />-->
            </a>
          </div>
          <font-awesome-icon @click="want_to_search = !want_to_search" icon="search" color="#999"/>
          <div class="p-3" @click="$emit('sidenavToggle')">
<!--            <PrivacyIcon style="width: 25px; height: 25px;"/>-->
            <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-7 tw-w-7" fill="none" viewBox="0 0 24 24" stroke="#999" stroke-width="2">
              <path stroke-linecap="round" stroke-linejoin="round" d="M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066c-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.996.608 2.296.07 2.572-1.065z" />
              <path stroke-linecap="round" stroke-linejoin="round" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
            </svg>

            <!--            <font-awesome-icon-->
<!--              icon="sliders-h"-->
<!--              size="lg"-->
<!--              color="#999"-->
<!--            ></font-awesome-icon>-->
          </div>
        </div>
        <div v-else class="px-2">
          <div class="d-flex justify-content-between align-items-center">
            <form class="col-11 px-1">
            <span class="searchIconMobile position-absolute">
              <font-awesome-icon icon="search" color="#999"/>
            </span>
              <input v-model="SearchUsers" value="" type="text" class="form-control search_style" placeholder="جست و جو دوستان" autocomplete="off">
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
<!--                  <div-->
<!--                    v-if="item.userSetting !== undefined"-->
<!--                    class="fs-6 fw-light text-muted"-->
<!--                  >-->
<!--                    {{ item.userSetting.bio }}-->
<!--                  </div>-->
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    <section  class="sticky_footer d-xl-none d-block w-100">
      <div class="headerStyleMobile py-2">
        <div class="headerIcon">
          <div class="d-flex flex-row justify-content-around align-items-center">
            <div class="custom_header_icon custom_header_size rounded-circle d-flex justify-content-center align-items-center mx-1" @click="goToMyPage">
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
<!--            <div class="custom_header_icon custom_header_size rounded-circle d-flex justify-content-center align-items-center mx-1">-->
<!--              <nuxt-link to="/social/accountsetting/myposts"-->
<!--              ><i-->
<!--                class="far fa-plus-square"-->
<!--                style="color: #999"-->
<!--                v-tooltip.bottom-start="'افزودن پست'"-->
<!--              ></i-->
<!--              ></nuxt-link>-->
<!--            </div>-->
            <div class="custom_header_icon custom_header_size rounded-circle d-flex justify-content-center align-items-center mx-1" ref="Chat">
              <nuxt-link to="/social/Chat"><i
                class="far fa-comment"
                style="color: #999"
                v-tooltip.bottom-start="'چت'"
              ></i
              ></nuxt-link>
            </div>
<!--            <div class="custom_header_icon custom_header_size rounded-circle d-flex justify-content-center align-items-center mx-1">-->
<!--              <nuxt-link to="/migration/alladvertise"-->
<!--              ><i-->
<!--                class="fas fa-ad"-->
<!--                style="color: #999"-->
<!--                v-tooltip.bottom-start="'آگهی ها'"-->
<!--              ></i-->
<!--              ></nuxt-link>-->
<!--            </div>-->
            <div class=" custom_header_icon custom_header_size rounded-circle d-flex justify-content-center align-items-center mx-1">
              <nuxt-link to="/social/accountsetting/tickets"
              ><i
                class="fas fa-ticket-alt"
                style="color: #999"
                v-tooltip.bottom-start="'تیکت ها'"
              ></i
              ></nuxt-link>
            </div>
            <div class=" position-relative custom_header_size custom_header_icon rounded-circle d-flex justify-content-center align-items-center mx-1">
              <font-awesome-icon
                v-tooltip.bottom-start="'اعلانات'"
                icon="bell"
                color="#999999"
                @click="notificationList"
              ></font-awesome-icon>
              <span v-if="Notifications" class="headerNumberNotification position-absolute d-flex justify-content-center align-items-center rounded-circle"
              ><strong>{{ Notifications.length }}</strong></span
              >
              <div
                class="listNotification position-absolute"
                v-if="showNotificationList && Notifications"
              >
                <div class="col-12 numberMessageText" v-if="Notifications">
                  {{ Notifications.length }} نوتیفکیشن جدید
                </div>
                <!---lawyer--->
                <div class="col-12" v-for="(item,idx) in Notifications" :key="idx">
                  <div
                    class="d-flex flex-row border-bottom w-100 h-100 overflow-hidden"
                  >
                    <div
                      class="d-flex flex-column text-end position-relative p-2"
                    >
                      <div class="senderName">
                        <small class="text-muted"
                        ><i class="fas fa-balance-scale"></i
                        ></small>
                        <small>{{ item.title }}</small>
                      </div>
                      <br/>
                      <small class="messageText"
                      ><small>{{ item.body }}</small
                      >
                        <nuxt-link
                          v-if="item.link"
                          to="/social/accountsetting/editprofilebasic"
                        >اینجا کلیک کنید
                        </nuxt-link
                        >
                      </small
                      >
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="position-relative custom_header_size custom_header_icon rounded-circle d-flex justify-content-center align-items-center mx-1">
              <font-awesome-icon
                icon="comment-alt"
                color="#999999"
                @click="messageList"
              ></font-awesome-icon>
              <span class="headerNumberNotification position-absolute rounded-circle d-flex justify-content-center align-items-center">
                  <strong>
                    {{ChatNotifs.length }}
                  </strong>
                </span>
              <div
                class="listMessage position-absolute"
                v-if="showMessageList"
              >
                <div class="col-12 numberMessageText">
                  {{ ChatNotifs.length }} پیام جدید
                </div>
                <div class="col-12" v-for="(item,idx) in ChatNotifs" :key="idx">
                  <div
                    class="d-flex flex-column text-end position-relative p-2"
                  >
                    <div class="senderName">
                      <small class="text-muted"
                      ><i class="fas fa-comment"></i
                      ></small>
                      <small>مکالمات من</small>
                    </div>

                    <h4 class="messageTextt">
                      <small>{{ item }}</small
                      >
                      <nuxt-link
                        tag="small text-primary mx-2"
                        to="/social/Chat"
                      >اینجا کلیک کنید
                      </nuxt-link
                      >
                    </h4>
                  </div>
                </div>
              </div>
            </div>
            <div class="position-relative custom_header_size">
              <img
                v-if="$auth.user && $auth.user.baseData && $auth.user.baseData.selfie"
                class="img-fluid headerPicture"
                :src="`https://banooclubapi.simagar.com/media/gallery/profile/${$auth.user.baseData.selfie}`"
                alt=""
                @click="profileHeader"
              />
              <img
                v-else
                class="img-fluid headerPicture"
                :src="userDefault"
                @click="profileHeader"
                alt=""
              />

              <div
                class="showProfileHeader position-absolute"
                v-if="showProfileHeader && !closeHeader"
              >
                <div class="d-flex flex-column">
                  <!--                    <div class="userStatus">-->
                  <!--                      <div class="d-flex flex-row justify-content-between">-->
                  <!--                        <div class="">آنلاین</div>-->
                  <!--                        <div class=" "><span class="statusShowingProfile onlineStatus"></span></div>-->
                  <!--                      </div>-->
                  <!--                    </div>-->
                  <!--                    <div class="userStatus">-->
                  <!--                      <div class="d-flex flex-row justify-content-between">-->
                  <!--                        <div class="userStatusColor">دور</div>-->
                  <!--                        <div class=" "><span class="statusShowingProfile awayStatus"></span></div>-->
                  <!--                      </div>-->
                  <!--                    </div>-->
                  <!--                    <div class="userStatus">-->
                  <!--                      <div class="d-flex flex-row justify-content-between">-->
                  <!--                        <div class="userStatusColor">آفلاین</div>-->
                  <!--                        <div class=" "><span class="statusShowingProfile offlineStatus"></span></div>-->
                  <!--                      </div>-->
                  <!--                    </div>-->
                  <!--                    <div class="userStatus">-->
                  <!--                      <nuxt-link to="/">-->
                  <!--                        <div class="d-flex flex-row justify-content-start">-->
                  <!--                          <div class="">-->
                  <!--                            <font-awesome-icon icon="user" color="007bff"/>-->
                  <!--                          </div>-->
                  <!--                          <div class="userStatusColor">نمایش پروفایل</div>-->
                  <!--                        </div>-->
                  <!--                      </nuxt-link>-->
                  <!--                    </div>-->
                  <!--                    <div class="userStatus">-->
                  <!--                      <nuxt-link to="/">-->
                  <!--                        <div class="d-flex flex-row justify-content-start">-->
                  <!--                          <div class="">-->
                  <!--                            <font-awesome-icon icon="user-edit" color="#007bff"/>-->
                  <!--                          </div>-->
                  <!--                          <div class="userStatusColor">ویرایش پروفایل</div>-->
                  <!--                        </div>-->
                  <!--                      </nuxt-link>-->
                  <!--                    </div>-->
                  <!--                    <div class="userStatus">-->
                  <!--                      <nuxt-link to="/">-->
                  <!--                        <div class="d-flex flex-row justify-content-start">-->
                  <!--                          <div class="">-->
                  <!--                            <font-awesome-icon icon="history" color="007bff"/>-->
                  <!--                          </div>-->
                  <!--                          <div class="userStatusColor">گزارش فعالیت</div>-->
                  <!--                        </div>-->
                  <!--                      </nuxt-link>-->
                  <!--                    </div>-->

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
                        <div v-if="$auth.user &&$auth.user.baseData && $auth.user.baseData.name && $auth.user.baseData.familyName " class="d-flex flex-column align-items-end px-2">
                          <div v-if="$auth.user &&$auth.user.baseData && $auth.user.baseData.name && $auth.user.baseData.familyName " class="profile_pic">{{$auth.user.baseData.name + ' ' + $auth.user.baseData.familyName}}</div>
                          <div v-if="$auth.user &&$auth.user.baseData && $auth.user.baseData.name && $auth.user.baseData.familyName " class="d-flex profile_username text-primary tw-cursor-pointer" @click="goToUserProfile($auth.user.baseData)">
                            {{ $auth.user.baseData.userName }}
                            @
                          </div>
                        </div>
                      </div>
                    </nuxt-link>
                  </div>
                  <div class="userStatus">
                    <nuxt-link to="/social/accountsetting/editprofilebasic">
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
                          {{Intl.NumberFormat('fa-IR').format(100000)}}
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
                        <!--                          <font-awesome-icon-->
                        <!--                            class="text-danger"-->
                        <!--                            icon="power-off"-->
                        <!--                          />-->
                      </div>
                      <div class="userStatusColor text-dark px-2" style="font-size: 14px;font-weight: 500;">
                        خروج</div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!--            <div class="custom_header_size d-flex justify-content-center align-items-center " @click="$emit('sidenavToggle')">-->
            <!--              <font-awesome-icon-->
            <!--                icon="bars"-->
            <!--                size="lg"-->
            <!--                color="#999999"-->
            <!--              ></font-awesome-icon>-->
            <!--            </div>-->
          </div>
        </div>

      </div>
    </section>
    <!--Mobile topNav-->
    <!--conditions-->
    <div class="col-md-12">
      <div
        v-if="
         $store.state.loggedInfo &&
          $store.state.loggedInfo.type &&
          $store.state.loggedInfo.type === 3 &&
          $store.state.loggedInfo.lawyerCertificateStatus === 4 &&
          $store.state.loggedInfo.newspaperStatus === 4
        "
        class="alert alert-danger m-0"
        :class="{ Alertsticky: ScrollPosition > 0 }"
      >
        <p>
          حساب کاربری شما غیر فعال است. لطفا برای فعال کردن آن روزنامه رسمی و
          پروانه کسب خود را آپلود کنید
        </p>
      </div>
    </div>
    <div class="col-md-12">
      <div
        v-if="
        $store.state.loggedInfo && $store.state.loggedInfo.type &&
          $store.state.loggedInfo.type === 4 &&
          $store.state.loggedInfo.videoIdentityStatus === 1
        "
        class="alert alert-danger m-0"
        :class="{ Alertsticky: ScrollPosition > 0 }"
      >
        <p>
          حساب کاربری شما غیر فعال است. لطفا برای فعال کردن آن ویدیو احراز هویت
          خود را آپلود کنید
        </p>
      </div>
    </div>
    <!--conditions-->
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
// import DefaultUserImage from '/static/user-avatar.jpg';
// import DefaultBackgroundImage from 'static/timeline-4.jpg'
import RoommateBack from "static/timeline-1.jpg";
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
    CheveronIcon,
    CircleIcon,
    ForumIcon,
    MoneyIcon,
    ProductsIcon,
    LogOutIcon,
    SettingIcon,
    PlusIcon,
    AdsIcon,
    FriendsIcon, BedIcon, PictureIcon, MenuIcon, AllUsersIcon, TicketIcon, MessageIcon, HomeIcon, SearchIcon,OrderIcon},
  head() {
    return {
      bodyAttrs: {
        class: this.displayChargeSideNav ? "fixed-body" : "",
      },
    };
  },

  data() {
    return {
      displayChargeSideNav:false,
      want_to_search:false,
      on_search_click: false,
      decrease_width_button:false,
      showLanguageList: false,
      showMessageList: false,
      showSearch: false,
      showNotificationList: false,
      showProfileHeader: false,
      activeLanguage: "english",
      numberMessage: [1, 2, 3, 4, 5],
      newImage: null,
      newUserImage: null,
      activePage: "home",
      editImage: false,
      editBackground: false,
      bannerImage: "",
      userImage: "",
      userImageApi: "",
      BackgroundApi: "",
      userinfo: {},
      ScrollPosition: 0,
      SearchUsers: "",
      userData: " ",
      FollowBtnMsg: "دنبال کردن",
      FollowBtn: true,
      ShortCut: false,
      Entered: false,
      DefaultImg: require("../assets/images/defaultBanner.jpg"),
      userDefault: require("../assets/images/defaultUser.png"),
      ChatNotifs: [],
    };
  },
  emits:['toggle_width'],
  // props: ["closeHeader", "fullName", "isChatLayout"],
  props: {
    closeHeader: {
      required: false,
    },
    fullName: {
      required: false,
    },
    isChatLayout: {
      type: Boolean,
      required: false,
      default: false,
    },
    decrease_width:{
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
        this.ChatNotifs.push(tmpLabel);
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
        this.showSearch = false;
        this.showMessageList = false;
        this.showNotificationList = false;
        this.showProfileHeader = false;
        this.showLanguageList = false;
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
    Notifications() {
      let tmpNotifs = [];
      let tmpnotif = {
        title: "",
        body: "",
        link: false,
      };

      const data = this.$store.state.loggedInfo;
      if(data){

      if (data && data.type && data.type === 3 && data.lawyerCertificateStatus === 4) {
        tmpnotif.title = "اطلاعات حقوقی";
        tmpnotif.body =
          "حساب کاربری شما غیر فعال است. لطفا پروانه کسب خود را آپلود نمایید";
        tmpnotif.link = true;
        const clone = {...tmpnotif};
        tmpNotifs.push(clone);
        tmpnotif.title = "";
        tmpnotif.body = "";
        tmpnotif.link = false;
      }
      if (data?.type === 3 && data?.lawyerCertificateStatus === 3) {
        tmpnotif.title = "اطلاعات حقوقی";
        tmpnotif.body =
          "پروانه کسب شما توسط ادمین تایید نشد! لطفا مجددا آن را آپلود نمایید";
        tmpnotif.link = true;
        const clone = {...tmpnotif};
        tmpNotifs.push(clone);
        tmpnotif.title = "";
        tmpnotif.body = "";
        tmpnotif.link = false;
      }
      if (data?.type === 3 && data?.lawyerCertificateStatus === 2) {
        tmpnotif.title = "اطلاعات حقوقی";
        tmpnotif.body =
          "شما پروانه کسب خود را آپلود کرده اید. لطفا تا زمان تایید ادمین منتظر بمانید";
        tmpnotif.link = false;
        const clone = {...tmpnotif};
        tmpNotifs.push(clone);
        tmpnotif.title = "";
        tmpnotif.body = "";
        tmpnotif.link = false;
      }
      if (
        data.type === 3 &&
        data.lawyerCertificateStatus === 1 &&
        data.newspaperStatus === 1
      ) {
        tmpnotif.title = "اطلاعات حقوقی";
        tmpnotif.body =
          "تبریک! حساب کاربری شما فعال است. از بانوکلاب لذت ببرید";
        tmpnotif.link = false;
        const clone = {...tmpnotif};
        tmpNotifs.push(clone);
        tmpnotif.title = "";
        tmpnotif.body = "";
        tmpnotif.link = false;
      }
      if (data.type === 3 && data.newspaperStatus === 4) {
        tmpnotif.title = "اطلاعات حقوقی";
        tmpnotif.body =
          "حساب کاربری شما غیر فعال است. لطفا روزنامه رسمی خود را آپلود کنید";
        tmpnotif.link = true;
        const clone = {...tmpnotif};
        tmpNotifs.push(clone);
        tmpnotif.title = "";
        tmpnotif.body = "";
        tmpnotif.link = false;
      }
      if (data.type === 3 && data.newspaperStatus === 3) {
        tmpnotif.title = "اطلاعات حقوقی";
        tmpnotif.body =
          "روزنامه رسمی شما توسط ادمین تایید نشد! لطفا مجددا آن را آپلود نمایید";
        tmpnotif.link = true;
        const clone = {...tmpnotif};
        tmpNotifs.push(clone);
        tmpnotif.title = "";
        tmpnotif.body = "";
        tmpnotif.link = false;
      }
      if (data.type === 3 && data.newspaperStatus === 2) {
        tmpnotif.title = "اطلاعات حقوقی";
        tmpnotif.body =
          "شما روزنامه رسمی خود را آپلود کرده اید. لطفا تا زمان تایید توسط ادمین منتظر بمانید";
        tmpnotif.link = false;
        const clone = {...tmpnotif};
        tmpNotifs.push(clone);
        tmpnotif.title = "";
        tmpnotif.body = "";
        tmpnotif.link = false;
      }
      if (data.type === 4 && data.videoIdentityStatus === 1) {
        tmpnotif.title = "احراز هویت";
        tmpnotif.body =
          "حساب کاربری شما غیر فعال است. لطفا ویدیو احراز هویت خود را آپلود نمایید";
        tmpnotif.link = true;
        const clone = {...tmpnotif};
        tmpNotifs.push(clone);
        tmpnotif.title = "";
        tmpnotif.body = "";
        tmpnotif.link = false;
      }
      if (data.type === 4 && data.videoIdentityStatus === 2) {
        tmpnotif.title = "احراز هویت";
        tmpnotif.body =
          "شما ویدیو احراز هویت خود را آپلود کرده اید. لطفا تا زمان تایید توسط ادمین منتظر بمانید";
        tmpnotif.link = true;
        const clone = {...tmpnotif};
        tmpNotifs.push(clone);
        tmpnotif.title = "";
        tmpnotif.body = "";
        tmpnotif.link = false;
      }
      if (data.type === 4 && data.videoIdentityStatus === 3) {
        tmpnotif.title = "احراز هویت";
        tmpnotif.body =
          "تبریک ! حساب کاربری شما فعال است. از بانوکلاب لذت ببرید";
        tmpnotif.link = true;
        const clone = {...tmpnotif};
        tmpNotifs.push(clone);
        tmpnotif.title = "";
        tmpnotif.body = "";
        tmpnotif.link = false;
      }
      if (data.type === 4 && data.videoIdentityStatus === 4) {
        tmpnotif.title = "احراز هویت";
        tmpnotif.body =
          "ویدیو احراز هویت شما توسط ادمین تایید نشد! لطفا مجددا آن را آپلود نمایید";
        tmpnotif.link = true;
        const clone = {...tmpnotif};
        tmpNotifs.push(clone);
        tmpnotif.title = "";
        tmpnotif.body = "";
        tmpnotif.link = false;
      }
      return tmpNotifs;
      }

    },
    BaseUrl() {
      return process.env.pic;
    },
    InlineBg() {
      const base = this.BaseUrl;
      if (this.VuexHeaderData.banner) {
        return {
          backgroundImage: `url(${base + this.VuexHeaderData.banner})`,
          backgroundPosition: "center",
          backgroundSize: "cover",
          backgroundRepeat: "no-repeat",
        };
      } else {
        return {
          backgroundImage: `url(${this.DefaultImg})`,
          backgroundPosition: "center",
          backgroundSize: "cover",
          backgroundRepeat: "no-repeat",
        };
      }
    },

    VuexHeaderData() {
      return this.$store.state.HeaderData;
    },
    loggedInfoData() {
      if(this.$store.state.loggedInfo){
        return this.$store.state.loggedInfo;
      }
      else {
        return this.$store.state.HeaderData
      }
    },
    // async userInfo() {
    //   let userInfo = [];
    //   if (this.$route.params.id) {
    //     try {
    //       const response = await this.$repositories.getUserIndex.getUserIndex(
    //         this.$route.params.id
    //       );
    //       this.$store.commit("SetSearchedUserStatus", response.data.status);
    //       this.FollowBtn = response.data.followButton;

    //       alert(this.$store.state.SearchedUserStatus);
    //       userInfo = response.data.baseData;
    //       this.imgUserAvatar();
    //       this.imageBackground();
    //       return userInfo;
    //     } catch (error) {
    //       console.log(error);
    //     }
    //   } else {
    //     try {
    //       const response =
    //         await this.$repositories.getUserByToken.getUserByToken();
    //       userInfo = response.data.userInfo;

    //       this.imgUserAvatar();
    //       this.imageBackground();
    //       return userInfo;
    //     } catch (error) {
    //       console.log(error);
    //     }
    //   }
    // },
  },
  methods: {
    openChargeWalletSideNav(){
      this.displayChargeSideNav = true
      this.showProfileHeader = false
    },
    decreaseWidthButton(){
      this.decrease_width_button = ! this.decrease_width_button
      this.decrease_width = ! this.decrease_width
      this.$root.$emit("change-cols")
      this.$emit("toggle_width")
    },
    MouseOverRightMenu(){
      if(this.decrease_width_button){
        this.decrease_width = ! this.decrease_width
      }
    },
    MouseLeaveRightMenu(){
      if(this.decrease_width_button){
        this.decrease_width = ! this.decrease_width
      }
    },
    toggleSearchDropDown() {
      this.on_search_click = !this.on_search_click
    },
    ToggleEnter() {
      this.Entered = !this.Entered;
    },
    ToggleShortCut() {
      this.ShortCut = !this.ShortCut;
    },
    CloseNotifList() {
      this.showNotificationList = false;
    },
    updateScroll() {
      this.ScrollPosition = window.scrollY;
    },
   async goToUserProfile(user){
      try {
        // const res = await this.$repositories.GetUserByUserName.GetUserByUserName({
        //   userName:user.userName
        // })
        // this.$store.commit('SetUserData',res.data)
        this.$router.push({
          path: `/user/${user.userName}/posts`
        });
        this.SearchUsers = "";
      }catch (e){
        console.log(e)
      }
    },
    // async goToUserProfile(user) {
    //   this.$store.commit("SetSearchedUserId", user.userId);
    //   try {
    //     const response = await this.$repositories.getUserIndex.getUserIndex(
    //       user.userId
    //     );
    //     this.$store.commit("SetUserData", response.data);
    //
    //
    //   } catch (error) {
    //     console.log(error);
    //   }
    //
    //   this.SearchUsers = "";
    // },

    Others() {
      if (
        this.$nuxt.$route.path.includes("TimelineVideos") ||
        this.$nuxt.$route.params.id ||
        this.$nuxt.$route.path.includes("TimelinePhotos") ||
        this.$nuxt.$route.path.includes("RoomMateInfo") ||
        this.$nuxt.$route.path.includes("Advertises") ||
        this.$nuxt.$route.path.includes("PersonalInfo") ||
        this.$nuxt.$route.path.includes("UserFollowers") ||
        this.$nuxt.$route.path.includes("UserFollowings")
      ) {
        return true;
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

    OthersFollow() {
      if (this.$route.params.id) {
        return true;
      } else {
        return false;
      }
    },

    CloseProfileHeader() {
      this.showProfileHeader = false;
    },
    LanguageList() {
      this.showSearch = false;
      this.showMessageList = false;
      this.showNotificationList = false;
      this.showProfileHeader = false;
      this.showLanguageList = !this.showLanguageList;
      this.$emit("open");
    },
    changeLanguage(language) {
      if (language === "english") {
        document.getElementsByTagName("body")[0].className = "englishFont";
        this.activeLanguage = language;
        this.showLanguageList = false;
      } else if (language === "arabic") {
        document.getElementsByTagName("body")[0].className = "arabicFont";
        this.activeLanguage = language;
        this.showLanguageList = false;
      } else if (language === "netherlands") {
        document.getElementsByTagName("body")[0].className = "netherlandsFont";
        this.activeLanguage = language;
        this.showLanguageList = false;
      } else if (language === "french") {
        document.getElementsByTagName("body")[0].className = "frenchFont";
        this.activeLanguage = language;
        this.showLanguageList = false;
      }
    },
    messageList() {
      this.showSearch = false;
      this.showLanguageList = false;
      this.showNotificationList = false;
      this.showProfileHeader = false;
      this.showMessageList = !this.showMessageList;
      this.$emit("open");
    },
    notificationList() {
      this.showSearch = false;
      this.showLanguageList = false;
      this.showMessageList = false;
      this.showProfileHeader = false;
      this.showNotificationList = !this.showNotificationList;
      this.$emit("open");
    },

    search() {
      this.showLanguageList = false;
      this.showMessageList = false;
      this.showNotificationList = false;
      this.showProfileHeader = false;
      this.showSearch = !this.showSearch;
      this.$emit("open");
    },
    profileHeader() {
      this.showLanguageList = false;
      this.showMessageList = false;
      this.showNotificationList = false;
      this.showSearch = false;
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
    changeActivePage(activePage) {
      if (activePage === "home") {
        this.activePage = "home";
      } else if (activePage === "notification") {
        this.activePage = "notification";
      } else if (activePage === "inputBox") {
        this.activePage = "inputBox";
      } else if (activePage === "learn") {
        this.activePage = "learn";
      } else if (activePage === "post") {
        this.activePage = "post";
      } else if (activePage === "requirements") {
        this.activePage = "requirements";
      } else {
        this.activePage = "like";
      }
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
    console.log(this.$auth.user)
    window.addEventListener("scroll", this.updateScroll);
    if (!this.$route.query.id) {
      try {

        const response =
          await this.$repositories.getUserByToken.getUserByToken();
        this.$store.commit("SetUserData", response.data);
      } catch (error) {
        console.log(error);
      }
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

    if (this.$nuxt.$route.fullPath === "/Social/AccountSetting/MyPosts") {
      this.activePage = "post";
    } else if (this.$nuxt.$route.fullPath === "/Social") {
      this.activePage = "home";
    } else if (this.$nuxt.$route.fullPath === "/Social") {
      this.activePage = "requirements";
    }
  },
};
</script>

<style scoped>
@media screen and (max-width: 425px) {
  .right_menu_mobile{
    width: 75% !important;

  }
}
.styleCursor:hover{
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

.profile_username{
  font-size: 12.5px;
  font-weight: normal;
  color: black;
}
.profile_pic{
  line-height: 20px;
  font-weight: 600;
  color: #333;
  font-size: 16px;
}
.svg_icons_size{
  width: 24px !important;
  height: 24px !important;
}
.decrease_width{
  width: 90px !important;
}
.search_style{
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
.sticky_header{
  z-index: 2;
  position: sticky;
  top:0

}
.sticky_footer{
  z-index: 2;
  position: fixed;
  bottom:0

}
.header_font_sidebar_size{
  font-size: 16px;
  font-weight: 600;
}
.font_sidebar_size{
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
  .header_search input{
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


.ChangePhotoBtn {
  color: white;
  border: solid 1px transparent;
  background: rgba(0, 0, 0, 0.4);
  transition: 0.5s ease;
}

.ChangePhotoBtn:hover {
  background: transparent;
  border: solid 1px #2e88e7;
  transition: 0.5s ease;
}

.TopGap {
  position: relative;
  top: 50px;
}

.UserPhoto {
  border: solid 4px white;
}

.SideBarHidden {
  position: fixed;
  right: 0;
  top: 0;
  height: 100%;
  width: 0px;
  background-color: #088dcd;
  z-index: 999999;
  overflow-x: hidden;
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
  width: 300px !important;
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
}

.ShortCutToggle:hover {
  transition: 0.2s ease;
  color: #3948dc;
}

.contactBanner {
  background: #088dcd;
  min-height: 300px;
  padding-top: 40px;
}

.faq-question {
  bottom: 2rem;
  right: 2rem;
}

.faq {
  top: 20%;
  padding-top: 40px;
}

.topBanner {
  background: #088dcd;
  min-height: 245px;
  padding-top: 40px;
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

.feature-photo {
  background-repeat: no-repeat;
  background-size: contain;
  box-sizing: border-box;
  display: block;
  width: 100%;
  height: 66vh;
  padding: 0;
  margin: 0;
}

.timeline-section {
  background: #fff none repeat scroll 0 0;
  position: absolute;
  bottom: 0vh;
  padding: 0.7rem 0;
}

.timeline-info {
  background: #fff none repeat scroll 0 0;
}

.sticky {
  position: fixed !important;
  width: 100%;
  top: 0;
}

.Alertsticky {
  position: fixed !important;
  width: 100%;
  top: 3.7rem;
  z-index: 99999999;
}

.heading-section-link {
  font-size: 14px;
  margin-right: 2vw;
  text-transform: capitalize;
  color: #545454;
}

.heading-section-link:hover {
  color: #088dcd;
  border-bottom: 1px solid #088dcd;
}

.user-avatar {
  border: 8px solid rgba(255, 255, 255, 0.8);
  border-radius: 50rem;
  box-shadow: 0 1px 0 #e1e8ed;
  background-repeat: no-repeat;
  background-position: center center;
  background-size: cover;
  width: 15vw;
  height: 18vh;
  right: 3vw;
  bottom: 0;
}

.edit-photo {
  background: rgba(0, 0, 0, 0.7) none repeat scroll 0 0;
  color: #fff;
  font-size: 10px;
  padding: 5px 20px;
  z-index: 9;
  border-radius: 3px;
  border: 1px solid transparent;
  transition: 0.5s ease;
}

.edit-photo:hover {
  background: transparent none repeat scroll 0 0;
  border: 1px solid #088dcd;
  color: #fff;
  transition: 0.5s ease;
}

.edit-user-avatar {
  background: rgba(0, 0, 0, 0.7) none repeat scroll 0 0;
  bottom: 30px;
  color: #fff;
  right: 20vw;
  padding: 5px 20px;
  position: absolute;
  z-index: 9;
  border-radius: 3px;
  font-size: 10px;
  border: 1px solid transparent;
  transition: 0.5s ease;
}

.edit-user-avatar:hover {
  background: transparent none repeat scroll 0 0;
  border: 1px solid #088dcd;
  color: #fff;
  transition: 0.5s ease;
}

.admin-name {
  color: #088dcd;
  font-size: 20px;
  margin-bottom: 0;
  padding: 0;
  vertical-align: super;
}

.timeline-info-role {
  bottom: 1vh;
  color: #3a3a3a;
  font-size: 10px;
  line-height: 5px;
}

.timeline-info .admin-name > h5 {
  color: #088dcd;
  display: inline-block;
  font-size: 14px;
  margin-bottom: 0;
  vertical-align: super;
}

.heading-section-link::before {
  bottom: 0;
  content: "";
  height: 1px;
  left: 0;
  opacity: 0;

  position: absolute;
  transition: all 0.2s linear 0s;
  visibility: hidden;
  width: 0;
}

.heading-section-link::after {
  border-bottom: 8px solid;
  border-left: 8px solid transparent;
  border-right: 8px solid transparent;
  bottom: 0px;
  content: "";
  left: 30%;
  position: absolute;
  transform: translate(-50%);
  display: none;
}

.heading-section-link.active::after {
  display: block;
  color: #088dcd;
}

.heading-section-link:hover::before {
  opacity: 1;
  visibility: visible;
  width: 100%;
  color: #088dcd;
}

.add-btn {
  left: 4rem;
  bottom: 5.5rem;
}

.add-btn > span {
  color: #fff;
  margin-left: 1vw;
  text-shadow: 0 2px 0 #4a4a4a;
  /*font-size: 14px;*/
}

.add-btn > a:hover {
  background: transparent none repeat scroll 0 0;
  border: 1px solid #088dcd;
  transition: all 0.3s linear 0s;
  color: #088dcd;
}

.add-btn > a {
  border-radius: 2px;
  color: #fff;
  display: inline-block;
  font-size: 14px;
  padding: 4px 20px;
  background: #088dcd;
  border: 1px solid transparent;
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

@keyframes menuHeader {
  from {
    opacity: 0;
    transform: translateY(100px) scale(1);
  }

  to {
    opacity: 1;
    transform: translateY(0) scale(1);
  }
}

.searchIconMobile {
  left: 20px;
  bottom: 10px;
}

.headerMobileIcon {
  padding: 5px 0;
}

.paddingForm {
  padding: 10px;
}

.inputMobileHeader {
  background: #fff;
  border: none;
  border-radius: 3px;
  font-size: 14px;
  line-height: 26px;
  box-sizing: border-box;
  height: 26px;
  width: 100%;
  padding: 0 10px;
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
.defaultUserStatus:hover{
  cursor: default !important;
}
.showProfileHeader {
  top: 40px;
  left: -10px;
  background: white;
  border-radius:8px;
  width: 270px;
  line-height: initial;
  border: 1px solid #e1e8ed;
  padding: 8px;
}

.submitButton {
  left: 7px;
  bottom: 7px;
}

.searchFriend {
  width: 0px;
  overflow-x: hidden;
  transition: 0.5s ease;
}

.searchFriendActive {
  width: 250px;
  transition: 0.5s ease;
}

.searchFriend input {
  border-style: none none solid;
  border-width: medium medium 1px;
  color: #999999;
  font-size: 15px;
  padding: 9px 18px;
  width: 100%;
}

.moreMessage {
  background: #f1f1f1 none repeat scroll 0 0;
  color: #088dcd;
  font-size: 13px;
  margin-bottom: 2px;
  padding: 5px 10px;
}

.messageStatus {
  color: #fff;
  font-size: 11px;
  padding: 0 6px;
  left: -106px;
  top: 0;
  background: #33b7a0;
}

.messageTime {
  color: #999;
  font-size: 12px;
  font-style: normal;
}

.senderPicture {
  margin: 10px;
  border-radius: 100%;
}

.senderName {
  color: #2a2a2a;
  cursor: pointer;
  font-size: 14px;
}

.messageTextt {
  font-size: 15px;
  color: #999;
}

.numberMessageText {
  border-bottom: 1px solid #ccc;
  font-size: 14px;
  padding: 5px 10px;
  text-align: center;
}

.listMessage {
  background: #fff none repeat scroll 0 0;
  border: 1px solid #e1e8ed;
  list-style: outside none none;
  max-height: 294px;
  overflow: auto;
  padding-left: 0;
  right: -50px;
  text-align: right;
  top: 60px;
  width: 285px;
  transition: all 0.3s linear 0s;
}

.listNotification {
  background: #fff none repeat scroll 0 0;
  border: 1px solid #e1e8ed;
  list-style: outside none none;
  max-height: 294px;
  overflow: auto;
  padding-left: 0;
  right: -50px;
  text-align: right;
  top: 60px;
  width: 285px;
  transition: all 0.3s linear 0s;
}

.listLanguage {
  position: absolute;
  top: 60px;
  left: -20px;
  width: 100px;
  border: 1px solid #e1e8ed;
}

.listLanguage > div {
  background: #fafafa none repeat scroll 0 0;
  display: block;
  font-size: 13px;
  margin-bottom: 2px;
  padding: 5px 10px;
  text-align: center;
  cursor: pointer;
  list-style: outside none none;
  max-height: 294px;
  transition: all 0.3s linear 0s;
  border-bottom: 1px solid #fff;
}

.menu-header {
  color: #575b5f;
  font-size: 14px;
  text-transform: capitalize;
  line-height: 59px;
  margin-right: 30px;
  padding-left: 30px;
  margin-left: 25px;
  vertical-align: top;
}

.logoPicture {
  padding-top: 9px;
}

.headerStyle {
  background: #fff;
  /* height: 60px; */
  /* padding: 0px 0px 0px 48px; */
  box-shadow: 0 0 5px rgba(0, 0, 0, 0.2);
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

ul {
  color: #5d6371;
  background: white;
  list-style: none;
  margin: 0;
  padding-left: 0;
}

ul > .parentMenu {
  color: #5d6371;
  background: white;
  display: block;
  float: left;
  position: relative;
  text-decoration: none;
  /*transition-duration: 0.5s;*/
  font-size: 14px;
  text-transform: capitalize;
}

li a {
  color: #5d6371;
  line-height: 59px;
  padding: 10px 15px;
}

li a:hover {
  color: #5d6371;
  cursor: pointer;
}

.SearchContainer {
  position: absolute;
  width: 96%;
  height: auto;
  top: 44px;
  box-shadow: rgb(0 0 0 / 10%) 0px 10px 25px -5px, rgb(0 0 0 / 4%) 0px 13px 10px -5px;
}

li:hover {
  cursor: pointer;
}

ul li ul {
  background: white;
  color: #5d6371;
  visibility: hidden;
  opacity: 0;
  min-width: 5rem;
  position: absolute;
  padding: 0;
  left: 0;
  top: 61px;
  display: none;
  border-top: 2px solid #2fa7cd;
  text-align: right;
  transition: all 0.2s linear 0s;
  border-radius: 0 0 5px 5px;
}

nav > ul > .parentMenu:hover .dropdown {
  opacity: 1;
  visibility: visible;
}

ul li:hover > .dropdown,
ul li:focus-within > .dropdown
  /* this is the line we add */
  /*ul li .dropdown:hover*/
{
  visibility: visible;
  opacity: 1;
  display: block;
  /*transition: transform 250ms cubic-bezier(0.1, 0.1, 0.1, 0.4);*/
}

ul li .dropdown:active {
  opacity: 0;
  visibility: hidden;
}

ul li ul li {
  clear: both;
  /*min-width: 200px;*/
  padding: 0;
  text-align: end;
  width: 100%;
}

.subMenuList a {
  color: #5f5f5f;
  display: inline-block;
  font-size: 14px;
  padding: 0 15px;
  width: 100%;
  text-transform: capitalize;
  transition: all 0.2s linear 0s;
  border-radius: 0 0 5px 5px;
}

ul .subMenuList :hover {
  background-color: #f1f1f1;
  color: #2fa7cd;
}

.menu {
  cursor: pointer;
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

.headerNumberNotification {
  font-size: 11px;
  left: 0;
  bottom: 25px;
  color: white;
  font-size: 11px;
  width: 16px;
  height: 16px;
  background-color: #ec5252;
}

.status {
  border-radius: 50%;
  display: inline-block;
  height: 12px;
  padding: 2px;
  position: absolute;
  bottom: 30px;
  left: 36px;
  width: 12px;
}

.statusShowingProfile {
  border-radius: 50%;
  display: inline-block;
  height: 12px;
  padding: 2px;
  position: absolute;
  left: 20px;
  width: 12px;
}

.MarginTop {
  margin-top: -2px !important;
}

.statusShowingProfile::after {
  background: white none repeat scroll 0 0;
  border-radius: 100%;
  content: "";
  height: 6px;
  left: 50%;
  position: absolute;
  top: 50%;
  transform: translate(-50%, -50%);
  width: 6px;
}

.onlineStatus {
  background: #7fba00;
}

.status::after {
  background: white none repeat scroll 0 0;
  border-radius: 100%;
  content: "";
  height: 6px;
  left: 50%;
  position: absolute;
  top: 50%;
  transform: translate(-50%, -50%);
  width: 6px;
}

.awayStatus {
  background: #ffd300;
}

.offlineStatus {
  background: #a9a9a9;
}

.top-area > ul.main-menu > li ul {
  right: 0;
  list-style: outside none none;
  padding-right: 0;
  position: absolute;
  text-align: right;
  top: 100%;
  background: #fff;
  width: 200px;
  opacity: 0;
  visibility: hidden;
  box-shadow: 4px 7px 20px 0 rgba(0, 0, 0, 0.09);
  transition: all 0.2s linear 0s;
  border-radius: 0 0 5px 5px;
  border-top: 2px solid;
}

.timeline-section-toolbar {
  display: flex;
  flex-direction: row;
  list-style: outside none none;
  margin-bottom: 0;
  padding-left: 19%;
  vertical-align: middle;
}

.sidebar-bg {
  background-color: white;
  right: -15px !important;
  z-index: 999999 !important;
}
</style>
