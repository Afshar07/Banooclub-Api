<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="container-fluid  tw-bg-[#faf7ff]">
    <LazyChargeWalletSideNav :show="ShowSideNav" @close="ShowSideNav = false">
    </LazyChargeWalletSideNav>
    <div class="tw-p-10 ">
      <div class="tw-w-full tw-shadow bg-white p-3 d-flex align-items-center gap-3  rounded ">
        <img src="/girl-icon-dashboard.png" class="tw-w-[7rem] tw-h-[7rem] tw-object-contain" alt="">
        <div class="d-flex align-items-center flex-column">
          <h1 class="text-purple h6" style="font-weight: bolder!important;">داشبورد</h1>
          <strong class="text-pink">Dashboard</strong>
        </div>

      </div>
    </div>
    <div class="tw-grid tw-p-10  tw-grid-cols-1 tw-py-10  tw-gap-y-20 md:tw-grid-cols-2 lg:tw-grid-cols-4">
      <div class="tw-px-4  ">
        <div class="tw-w-full tw-bg-white tw-rounded-xl  tw-shadow-md tw-p-4 ">
          <div class="tw-flex tw-flex-col tw-gap-3 tw-flex-wrap tw-border-b tw-border-gray-200 ">
            <div

              class="tw-bg-gradient-to-tr tw-bg-[#ff297e] tw--mt-10  tw-mb-4 tw-rounded-xl tw-text-white tw-grid tw-items-center tw-w-24 tw-h-24 tw-py-4 tw-px-4 tw-justify-center tw-shadow-lg tw-mb-0">
              <span class=" tw-text-white tw-text-3xl tw-leading-none"><i class="fas fa-coins"></i></span>

            </div>
            <div class="tw-w-full  tw-p-0 tw-max-w-full tw-flex-grow tw-flex-1 tw-mb-2 tw-text-right  ">
              <h5 class="tw-text-gray-500 tw-font-light tw-tracking-wide  tw-text-base tw-mb-1">موجودی کیف پول من</h5>
              <div class="tw-flex tw-items-center tw-justify-between">
                <span
                  class="tw-text-xl tw-text-gray-900 tw-flex tw-items-center  "> {{ new Intl.NumberFormat().format(WalletAmount) }} <small
                  class="Toman">تومان</small> </span>

                <button @click="ShowSideNav=true"
                        class="tw-bg-gradient-to-tr tw-from-pink-500 tw-to-pink-700 tw-py-1  tw-px-2 tw-rounded tw-text-white hover:tw-text-pink-500 hover:tw-bg-white tw-transition">
                  <i class="fas fa-plus"></i></button>
              </div>

            </div>
          </div>
          <div class="tw-text-sm tw-w-full tw-text-gray-700 tw-pt-4 tw-flex tw-items-center ">
            <div class=" tw-collapse   tw-w-full">
              <input type="checkbox" class="peer tw-py-0 tw-px-0  tw-h-0"/>
              <div
                class="tw-collapse-title tw-px-0 tw-h-4 tw-flex tw-items-center tw-justify-between  tw-py-0 peer-checked:bg-secondary peer-checked:text-secondary-content"
                style="min-height: 1rem!important;">
                <span class="tw-font-bold">در یک ماه گذشته</span>
                <span class="tw-font-bold"><i
                  class="fas fa-chevron-down tw-mt-1 tw-transition tw-transform tw-rotate-180"></i></span>
              </div>
              <div
                class="tw-collapse-content tw-flex tw-flex-col tw-gap-y-2  tw-px-0  peer-checked:bg-secondary peer-checked:text-secondary-content">
                <div class="tw-flex tw-items-center tw-justify-between">
                  <small class="">مجموع شارژ کیف پول : </small>
                  <small>  {{ new Intl.NumberFormat().format(DashBoard.lastMonthWalletChargeAmount) }} <small class="Toman">تومان</small></small>
                </div>
                <div class="tw-flex tw-items-center tw-justify-between">
                  <small class="">مجموع دریافتی : </small>
                  <small>  {{ new Intl.NumberFormat().format(DashBoard.userLastMonthIncomeAmount) }} <small class="Toman">تومان</small></small>
                </div>
                <div class="tw-flex tw-items-center tw-justify-between">
                  <small class="">مجموع پرداختی : </small>
                  <small>  {{ new Intl.NumberFormat().format(DashBoard.userLastMonthOutcomeAmount) }} <small class="Toman">تومان</small></small>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="tw-px-4  ">
        <div class="tw-w-full tw-bg-white tw-rounded-xl  tw-shadow-md tw-p-4 ">
          <div class="tw-flex tw-flex-col tw-gap-3 tw-flex-wrap tw-border-b tw-border-gray-200 ">
            <div
              class="tw-bg-gradient-to-tr tw-bg-[#59fabc] tw--mt-10  tw-mb-4 tw-rounded-xl tw-text-white tw-grid tw-items-center tw-w-24 tw-h-24 tw-py-4 tw-px-4 tw-justify-center tw-shadow-lg tw-mb-0">
              <span class=" tw-text-white tw-text-3xl tw-leading-none"><i class="fas fa-list"></i></span>
            </div>
            <div class="tw-w-full tw-pl-4 tw-max-w-full tw-flex-grow tw-flex-1 tw-mb-2 tw-text-right  ">
              <h5 class="tw-text-gray-500 tw-font-light tw-tracking-wide  tw-text-base tw-mb-1">سفارشات من</h5>
              <span class="tw-text-xl tw-text-gray-900">{{
                  DashBoard.lastMonthNotPayedOrdersCount + DashBoard.lastMonthPayedOrdersCount
                }}</span>
            </div>
          </div>
          <div class="tw-text-sm tw-text-gray-700 tw-pt-4 tw-flex tw-items-center ">
            <div class=" tw-collapse   tw-w-full">
              <input type="checkbox" class="peer tw-py-0 tw-px-0  tw-h-0"/>
              <div
                class="tw-collapse-title tw-px-0 tw-h-4 tw-flex tw-items-center tw-justify-between  tw-py-0 peer-checked:bg-secondary peer-checked:text-secondary-content"
                style="min-height: 1rem!important;">
                <span class="tw-font-bold">در یک ماه گذشته</span>
                <span class="tw-font-bold"><i
                  class="fas fa-chevron-down tw-mt-1 tw-transition tw-transform tw-rotate-180"></i></span>
              </div>
              <div
                class="tw-collapse-content tw-flex tw-flex-col tw-gap-y-2  tw-px-0  peer-checked:bg-secondary peer-checked:text-secondary-content">
                <div class="tw-flex tw-items-center tw-justify-between">
                  <small class="">سفارشات پرداخت شده : </small>
                  <small>  {{ DashBoard.lastMonthPayedOrdersCount }}</small>
                </div>
                <div class="tw-flex tw-items-center tw-justify-between">
                  <small class="">سفارشات پرداخت نشده : </small>
                  <small>  {{ DashBoard.lastMonthNotPayedOrdersCount }}</small>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="tw-px-4  ">
        <div class="tw-w-full tw-bg-white tw-rounded-xl  tw-shadow-md tw-p-4 ">
          <div class="tw-flex  tw-flex-col tw-gap-3 tw-flex-wrap tw-border-b tw-border-gray-200 ">
            <div
              class="tw-bg-gradient-to-tr tw-bg-[#8e54e9] tw--mt-10  tw-mb-4 tw-rounded-xl tw-text-white tw-grid tw-items-center tw-w-24 tw-h-24 tw-py-4 tw-px-4 tw-justify-center tw-shadow-lg tw-mb-0">
              <span class=" tw-text-white tw-text-3xl tw-leading-none"><i class="fas fa-comments"></i></span>
            </div>
            <div class="tw-w-full tw-pl-4 tw-max-w-full tw-flex-grow tw-flex-1 tw-mb-2 tw-text-right  ">
              <h5 class="tw-text-gray-500 tw-font-light tw-tracking-wide  tw-text-base tw-mb-1">نظرات من</h5>
              <span v-if="DashBoard"
                    class="tw-text-xl tw-text-gray-900">{{ DashBoard.weekForumComments + DashBoard.weekPostCommentsCount }}</span>
            </div>
          </div>
          <div class="tw-text-sm tw-text-gray-700 tw-pt-4 tw-flex tw-items-center undefined">
            <div class=" tw-collapse   tw-w-full">
              <input type="checkbox" class="peer tw-py-0 tw-px-0  tw-h-0"/>
              <div
                class="tw-collapse-title tw-px-0 tw-h-4 tw-flex tw-items-center tw-justify-between  tw-py-0 peer-checked:bg-secondary peer-checked:text-secondary-content"
                style="min-height: 1rem!important;">
                <span class="tw-font-bold">در یک هفته گذشته</span>
                <span class="tw-font-bold"><i
                  class="fas fa-chevron-down tw-mt-1 tw-transition tw-transform tw-rotate-180"></i></span>
              </div>
              <div
                class="tw-collapse-content tw-flex tw-flex-col tw-gap-y-2  tw-px-0  peer-checked:bg-secondary peer-checked:text-secondary-content">
                <div class="tw-flex tw-items-center tw-justify-between">
                  <small class="">نظرات تالار ها : </small>
                  <small v-if="DashBoard">  {{ DashBoard.weekForumComments }}</small>
                </div>
                <div class="tw-flex tw-items-center tw-justify-between">
                  <small class="">نظرات پست ها : </small>
                  <small>  {{ DashBoard.weekPostCommentsCount }}</small>
                </div>

              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="tw-px-4  ">
        <div class="tw-w-full tw-bg-white tw-rounded-xl  tw-shadow-md tw-p-4 ">
          <div class="tw-flex tw-flex-col tw-gap-3 tw-flex-wrap tw-border-b tw-border-gray-200 ">
            <div
              class="tw-bg-gradient-to-tr tw-bg-[#ff4040] tw--mt-10  tw-mb-4 tw-rounded-xl tw-text-white tw-grid tw-items-center tw-w-24 tw-h-24 tw-py-4 tw-px-4 tw-justify-center tw-shadow-lg tw-mb-0">
              <span class=" tw-text-white tw-text-3xl tw-leading-none"><i class="fas fa-heart"></i></span>
            </div>
            <div class="tw-w-full tw-pl-4 tw-max-w-full tw-flex-grow tw-flex-1 tw-mb-2 tw-text-right  ">
              <h5 class="tw-text-gray-500 tw-font-light tw-tracking-wide  tw-text-base tw-mb-1">لایک های من</h5>
              <span class="tw-text-xl tw-text-gray-900">{{
                  DashBoard.weekForumRate + DashBoard.weekPostLikesCount
                }}</span>
            </div>
          </div>
          <div class="tw-text-sm tw-text-gray-700 tw-pt-4 tw-flex tw-items-center undefined">
            <div class=" tw-collapse   tw-w-full">
              <input type="checkbox" class="peer tw-py-0 tw-px-0  tw-h-0"/>
              <div
                class="tw-collapse-title tw-px-0 tw-h-4 tw-flex tw-items-center tw-justify-between  tw-py-0 peer-checked:bg-secondary peer-checked:text-secondary-content"
                style="min-height: 1rem!important;">
                <span class="tw-font-bold">در یک هفته گذشته</span>
                <span class="tw-font-bold"><i
                  class="fas fa-chevron-down tw-mt-1 tw-transition tw-transform tw-rotate-180"></i></span>
              </div>
              <div
                class="tw-collapse-content tw-flex tw-flex-col tw-gap-y-2  tw-px-0  peer-checked:bg-secondary peer-checked:text-secondary-content">
                <div class="tw-flex tw-items-center tw-justify-between">
                  <small class="">لایک های تالار : </small>
                  <small>  {{ DashBoard.weekForumRate }}</small>
                </div>
                <div class="tw-flex tw-items-center tw-justify-between">
                  <small class="">لایک های پست ها  : </small>
                  <small>  {{ DashBoard.weekPostLikesCount }}</small>
                </div>

              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
       <div class="tw-grid tw-p-10   tw-grid-cols-1 tw-gap-y-20 md:tw-grid-cols-1 tw-gap-4">
      <div class="tw-grid tw-p-10   tw-grid-cols-1 tw-gap-y-20 md:tw-grid-cols-1 tw-gap-4">
        <div class="tw-w-full tw-col-span-1 tw-bg-white tw-rounded-xl tw-shadow-md tw-p-4 ">
          <div
            class="tw-bg-gradient-to-tr tw-from-orange-400 tw-to-orange-600 tw--mt-10 tw-mb-4 tw-rounded-xl tw-text-white tw-grid tw-items-center tw-w-full tw-h-24 tw-py-4 tw-px-8  shadow-lg-blue ">
            <div class="tw-w-full tw-flex tw-items-center tw-justify-between">
              <h5 class="tw-text-white tw-text-md">نمودار رشد دنبال کنندگان </h5>

            </div>
          </div>
          <div class="tw-p-4 ">
            <div v-if="!$fetchState.pending  && DashBoard" class="tw-overflow-x-auto">
              <LazyFollowersChart :ChartData="DashBoard.monthFollowersChartSeperate5Days"></LazyFollowersChart>
            </div>
          </div>
        </div>

      </div>
      <div class="tw-w-full tw-bg-white tw-col-span-1 tw-rounded-xl tw-shadow-md tw-p-4 ">
        <div
          class="tw-bg-gradient-to-tr tw-bg-[#59fabc] tw--mt-10 tw-mb-4 tw-rounded-xl tw-text-white tw-grid tw-items-center tw-w-full tw-h-24 tw-py-4 tw-px-8  shadow-lg-blue ">
          <div class="tw-w-full tw-flex tw-items-center tw-gap-x-4 tw-cursor-pointer  ">
            <h5 class="tw-text-sky-700 tw-text-md tw-cursor-pointer  tw-transition ">نمودار درآمد و پرداخت</h5>

          </div>
        </div>
        <div class="tw-p-4 ">
          <div v-if="!$fetchState.pending  && DashBoard" class="tw-overflow-x-auto">
            <LazySalaryChart :ChartData="DashBoard.yearIOReports"></LazySalaryChart>
          </div>
        </div>
      </div>
    </div>

    <div class="tw-grid tw-p-10  tw-mt-8 tw-grid-cols-1 tw-gap-y-20 md:tw-grid-cols-1 tw-gap-4">
      <div class="tw-w-full tw-bg-white  tw-rounded-xl tw-shadow-md tw-p-4 ">
        <div
          class="tw-bg-gradient-to-tr tw-from-blue-500 tw-to-blue-700 tw--mt-10 tw-mb-4 tw-rounded-xl tw-text-white tw-grid tw-items-center tw-w-full tw-h-24 tw-py-4 tw-px-8  shadow-lg-blue ">
          <div
            class="tw-w-full tw-h-fit tw-flex tw-flex-col md:tw-flex-col sm:tw-flex-row lg:tw-flex-row  tw-items-center tw-justify-between">
            <h5 class="tw-text-white tw-text-md">آخرین پرداختی ها</h5>
            <div class="tw-btn-group">
              <button class="tw-btn Prev tw-text-white tw-bg-[#59fabc] " @click.stop="DecreasePageNumber()"> <</button>
              <button v-for="item in totalPagesPayment" @click.stop="SetSelectedPageId(item)"
                      :class="GetSelectedPageClass(item)" :key="item" class="tw-btn tw-text-white tw-bg-[#59fabc] ">{{ item }}
              </button>
              <button class="tw-btn Next  tw-text-white tw-bg-[#59fabc] " @click.stop="IncreasePageNumber()"> ></button>
            </div>
          </div>
        </div>
        <div class="tw-p-4   ">
          <div class="tw-overflow-x-scroll ">
            <table class="tw-items-center tw-w-full tw-table-compact   tw-bg-transparent tw-border-collapse">
              <thead>
              <tr>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  شناسه
                </th>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  شناسه پرداخت
                </th>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  کد پیگیری
                </th>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  نام و نام خانوادگی
                </th>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  تاریخ
                </th>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  وضعیت
                </th>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  مجموع
                </th>

              </tr>
              </thead>
              <tbody>
              <tr v-for="(item,idx) in AllPayments" :key="idx">
                <th
                  class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                  {{ item.paymentId }}
                </th>
                <td
                  v-tooltip.bottom-start="`${item.transId}`"
                  class="tw-border-b  tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                  {{ item.transId.substr(0, 6) }}...
                </td>
                <td
                  v-tooltip.bottom-start="`${item.refId}`"
                  class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                  {{ item.refId.substr(0, 6) }}...
                </td>
                <td
                  v-if="item.userInfo" @click="goToUserProfile(item.userInfo)"
                  class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                  {{ item.userInfo.name + ' ' + item.userInfo.familyName }}
                </td>
                <td
                  class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                  {{ new Date(item.createDate).toLocaleTimeString('fa-IR') }}
                </td>
                <td
                  class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                  <div class="tw-bg-red-700 tw-rounded d-inline-flex justify-content-center align-items-center p-1"
                       v-if="item.status === -2 || item.status ===-4">
                    <span class="text-white tw-text-xs">لغو شده</span>
                  </div>
                  <div class="tw-bg-green-700 tw-rounded d-inline-flex justify-content-center align-items-center p-1"
                       v-else-if="item.status === 0">
                    <span class="text-white tw-text-xs">پرداخت شده</span>
                  </div>
                  <div class="tw-bg-gray-500 tw-rounded d-inline-flex justify-content-center align-items-center p-1"
                       v-else-if="item.status === -122">
                    <span class="text-white tw-text-xs">در انتظار پرداخت</span>
                  </div>
                  <div class="tw-bg-yellow-500 tw-rounded d-inline-flex justify-content-center align-items-center p-1"
                       v-else-if="item.status === -49">
                    <span class="text-white tw-text-xs">تراکنش تکراری</span>
                  </div>
                  <div class="tw-bg-blue-500 tw-rounded d-inline-flex justify-content-center align-items-center p-1"
                       v-else-if="item.status === -3">
                    <span class="text-white tw-text-xs">در انتظار پاسخ</span>
                  </div>


                </td>
                <td
                  class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                  {{ Intl.NumberFormat('fa-IR').format(item.amount) }} <small class="Toman">تومان</small>
                </td>
              </tr>

              </tbody>
            </table>
          </div>
        </div>
      </div>

    </div>
    <div class="tw-grid tw-p-10  tw-mt-8 tw-grid-cols-1 tw-gap-y-20 md:tw-grid-cols-1 tw-gap-4">
      <div v-if="$auth.user && $auth.user.userInfo &&$auth.user.userInfo.type && $auth.user.userInfo.type===3  "
           class="tw-w-full tw-bg-white tw-rounded-xl tw-shadow-md tw-p-4 ">
        <div
          class=" tw-h-auto tw-bg-gradient-to-tr tw-from-purple-500 tw-to-purple-700 tw--mt-10 tw-mb-4 tw-rounded-xl tw-text-white tw-grid tw-items-center tw-w-full tw-h-24 tw-py-4 tw-px-8  shadow-lg-blue ">
          <div
            class="tw-w-full  tw-flex md:tw-flex-col tw-flex-col sm:tw-flex-row lg:tw-flex-row tw-items-center tw-justify-between">
            <h5 class="tw-text-white tw-text-md"> خدمات خریداری شده از من</h5>

          </div>
        </div>
        <div class="tw-p-4 ">
          <div class="tw-overflow-x-scroll">
            <table class="tw-items-center tw-w-full tw-bg-transparent tw-table-compact tw-border-collapse">
              <thead>
              <tr>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  شناسه
                </th>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  نام خدمت
                </th>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  ظرفیت باقی مانده
                </th>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  تاریخ انقضا خدمت
                </th>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  وضعیت خدمت
                </th>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  عملیات
                </th>
              </tr>
              </thead>
              <tbody>
              <tr v-for="(item,idx) in OrderedServices" :key="idx">
                <th
                  class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                  {{ item.servicePackId }}
                </th>
                <td
                  class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                  <nuxt-link class="tw-text-black tw-transition hover:tw-text-blue-500 text-decoration-none"
                             :to="`/Products/${item.servicePAckId}`">
                    {{ item.title }}
                  </nuxt-link>
                </td>
                <td
                  class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                  <small>{{ item.maintain }}</small>
                </td>
                <td
                  class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                  <small> {{ new Date(item.expireDate).toLocaleDateString('fa-IR') }} </small>
                </td>
                <td
                  class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                    <span v-if="  item.status===1" class="badge text-white pill bg-success">
               فعال

             </span>
                  <span v-if="  item.status===2" class="badge text-white pill bg-danger">
               غیرفعال

             </span>
                  <span v-if="  item.status===3" class="badge text-white pill bg-secondary">
               نیاز به بازبینی مجدد

             </span>
                </td>
                <td
                  class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">


                  <div
                    class="tw-bg-blue-500 tw-rounded tw-cursor-pointer d-inline-flex justify-content-center align-items-center p-1"
                    @click="GetServiceDetail(item)">

                    <span class="text-white tw-text-xs">مشاهده جزئیات </span>


                  </div>
                </td>
              </tr>

              </tbody>
            </table>
          </div>
        </div>
        <div v-if="ServiceMembers!==null" class=" my-3 tw-px-10 ">
          <span class="my-3 tw-font-bold">کاربرانی که این خدمت را خریداری کرده اند</span>
          <div class="tw-overflow-x-scroll">
            <table class="tw-items-center tw-w-full tw-bg-transparent tw-table-compact tw-border-collapse">
              <thead>
              <tr>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  شناسه کاربر
                </th>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  نام و نام خانوادگی
                </th>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  نام کاربری
                </th>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  شماره تماس
                </th>
                <th
                  class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                  ایمیل
                </th>
              </tr>
              </thead>
              <tbody>
              <tr v-for="(item,idx) in ServiceMembers" :key="idx">
                <th
                  class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                  {{ item.userId }}
                </th>
                <td
                  class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                  <small>{{ item.name + ' ' + item.familyName }}</small>
                </td>
                <td
                  class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                  <small>{{ item.userName }}</small>
                </td>
                <td
                  class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                  <small> {{ item.mobile }} </small>
                </td>
                <td
                  class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                  <small> {{ item.email }} </small>
                </td>


              </tr>

              </tbody>
            </table>
          </div>
        </div>
        <!--          <div v-if="SelectedUser!==null" class="col-md-12 my-3  bg-white p-4 ">-->
        <!--                <div class="tw-grid tw-grid-cols-2">-->
        <!--                  <div class="UserInfoCard  tw-bg-white tw-rounded tw-shadow">-->
        <!--                    <div class="UserInfoCardImg tw-justify-center tw-flex tw-shadow tw-object-cover ">-->
        <!--                      <img :src="`https://banooclubapi.oregonserver.com/media/gallery/profile/${SelectedUser.selfieFileData}`" class="tw-rounded-full tw-shadow-xl tw-relative tw-bottom-10 tw-w-24 tw-h-24"  alt="">-->
        <!--                    </div>-->
        <!--                    <div class="UserInfoCardBody tw-flex tw-flex-col">-->
        <!--                      <div class="tw-flex tw-items-center tw-justify-between">-->
        <!--                        <small>sad</small>-->
        <!--                        <small>dsds</small>-->
        <!--                      </div>-->

        <!--                    </div>-->
        <!--                  </div>-->
        <!--                </div>-->

        <!--            <div class="row">-->
        <!--              <div class="col-md-3">-->
        <!--                <span  class="text-secondary">نام کوچک : </span>-->
        <!--                <span>{{SelectedUser.name}}</span>-->

        <!--              </div>-->
        <!--              <div class="col-md-3">-->
        <!--                <span  class="text-secondary">نام خانوادگی : </span>-->
        <!--                <span>{{SelectedUser.familyName}}</span>-->

        <!--              </div>-->
        <!--              <div class="col-md-3 ">-->
        <!--                <span  class="text-secondary">نام کاربری: </span>-->
        <!--                <nuxt-link class=" tw-transition tw-text-blue-500 text-decoration-none" :to="`/user/${SelectedUser.userName}/posts`">@{{SelectedUser.userName}}</nuxt-link>-->
        <!--              </div>-->
        <!--              <div class="col-md-3 ">-->
        <!--                <span  class="text-secondary">شماره موبایل : </span>-->
        <!--                <span v-if="SelectedUser.mobile!=='' || SelectedUser.mobile!==null">{{SelectedUser.mobile}}</span>-->
        <!--                <span v-else>-</span>-->
        <!--              </div>-->
        <!--              <div class="col-md-12  d-flex align-items-center gap-2">-->
        <!--                <span  class="text-secondary">ایمیل : </span>-->
        <!--                <span v-if="SelectedUser.email!=='' || SelectedUser.email!==null">{{SelectedUser.email}}</span>-->
        <!--                <span v-else>-</span>-->


        <!--              </div>-->

        <!--            </div>-->

        <!--          </div>-->
      </div>

    </div>


  </div>

</template>

<script>


export default {
  head() {
    return {
      title: 'داشبورد'
    }
  },
  layout: "PoshtebamPlusLayout",
  name: "DashBoard",
  data() {
    return {
      ShowSideNav: false,
      SelectedPageNumberPayment: 1,
      totalPagesPayment: [],
      AllPayments: [],
      OrderedServices: [],
      ServiceMembers: null,
      SelectedUser: null,
      DashBoard: ''
    }
  },
  computed: {
    WalletAmount() {
      return this.$store.state.WalletAmount
    },
  },

  async fetch() {
    try {
      const res = await this.$repositories.GetUserDashBoard.GetUserDashBoard();
      this.DashBoard = res.data

    } catch (e) {
      console.log(e)
    }
    try {
      const res = await this.$repositories.GetCredit.GetCredit()
      this.$store.commit('SetWalletAmount', res.data.data)
    } catch (e) {
      console.log(e)
    }
    try {
      const res = await this.$repositories.GetMyPayments.GetMyPayments({
        pageNumber: this.SelectedPageNumberPayment,
        count: 2
      })
      this.AllPayments = res.data.payments
      this.totalPagesPayment = []
      const result = Math.ceil(res.data.paymentsCount / 2)
      for (let i = 1; i <= result; i++) {
        this.totalPagesPayment.push(i);
      }
    } catch (e) {
      console.log(e)
    }

    try {
      const res = await this.$repositories.GetOrderedServices.GetOrderedServices()
      this.OrderedServices = res.data
    } catch (e) {
      console.log(e)
    }
  },
  methods: {
    SetSelectedUser(item) {
      this.SelectedUser = item
    },
    async GetAllPayments() {
      try {

        if (this.SelectedPageNumberPayment > this.totalPagesPayment.length) {
          this.SelectedPageNumberPayment = 1
        }
        if (this.SelectedPageNumberPayment < 1) {
          this.SelectedPageNumberPayment = 1
        }
        const res = await this.$repositories.GetMyPayments.GetMyPayments({
          pageNumber: this.SelectedPageNumberPayment,
          count: 2
        })
        this.AllPayments = res.data.payments
        this.totalPagesPayment = []
        const result = Math.ceil(res.data.paymentsCount / 2)
        for (let i = 1; i <= result; i++) {
          this.totalPagesPayment.push(i);
        }
      } catch (e) {
        console.log(e)
      }
    },
    GetSelectedPageClass(id) {
      if (id === this.SelectedPageNumberPayment) {
        return 'tw-btn-active'
      } else {
        return ''
      }
    },
    SetSelectedPageId(id) {
      this.SelectedPageNumberPayment = id
      this.GetAllPayments()
    },
    DecreasePageNumber() {
      this.SelectedPageNumberPayment--
      this.GetAllPayments()
    },
    IncreasePageNumber() {
      this.SelectedPageNumberPayment++
      this.GetAllPayments()
    },

    async GetServiceDetail(item) {
      try {
        this.SelectedService = item
        const res = await this.$repositories.GetOrderedServicesMembers.GetOrderedServicesMembers({
          serviceId: item.servicePackId
        })
        this.ServiceMembers = res.data
      } catch (e) {
        console.log(e)
      }
    },
    async goToUserProfile(user) {
      try {
        this.$router.push({path: `/user/${user.userName}/posts`});
      } catch (e) {
        console.log(e)
      }
    },
  }
}
</script>

<style scoped>

/* width */
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

.table td {
  vertical-align: middle !important;
  horiz-align: center !important;
}


</style>
