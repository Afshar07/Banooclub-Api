<template>
  <div class="container mcontainer">
    <div class="d-flex justify-content-between align-items-center">
      <h2 class="tw-text-2xl tw-font-semibold py-3">پرداخت ها</h2>
      <div class="d-flex justify-content-center align-items-center">
        <span style="color: rgb(103, 103, 103)">موجودی کیف پول:
          {{Intl.NumberFormat('fa-IR').format(100000)}}
          تومان</span>
        <button @click="displayChargeSideNav = true" class="btn AddReplyBtn text-white">
          <PlusIcon fill="#2563eb" style="width: 20px; height: 20px;"/>
        </button>
      </div>
    </div>
    <ChargeWalletSideNav :show="displayChargeSideNav" @close="displayChargeSideNav = false"/>
    <div
      class="offcanvas offcanvas-start sidebar-bg"
      tabindex="-1"
      id="offcanvasExample"
      aria-labelledby="offcanvasExample"
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
    <div class="tw-overflow-x-auto">
      <div class="tw-overflow-x-auto">
        <table class="tw-table tw-w-full tw-table-zebra"
               style="border-radius: 10px;box-shadow: rgb(0 0 0 / 10%) 0px 1px 3px 0px, rgb(0 0 0 / 6%) 0px 1px 2px 0px;">
          <!-- head -->
          <thead>
          <tr>
            <th>شناسه</th>
            <th>شناسه پرداخت</th>
            <th>کد پیگیری</th>
            <th>نام و نام خانوادگی</th>
            <th>تاریخ</th>
            <th>وضعیت</th>
            <th>مجموع</th>
          </tr>
          </thead>
          <tbody>
          <!-- rows -->
          <tr class="tw-hover" v-for="(wallet,index) in walletInfo" >
            <th class="fw-normal">{{ index + 1 }}</th>
            <th class="fw-normal">{{wallet.payment_id}}</th>
            <th class="fw-normal">{{wallet.code}}</th>
            <td>{{ wallet.name }}</td>
            <td>{{ wallet.date }}</td>
            <td>
              <div class="tw-bg-red-700 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-if="wallet.status == 'failed'">
                <span class="text-white tw-text-xs">لغو شده</span>
              </div>
              <div class="tw-bg-green-700 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-else-if="wallet.status == 'success'">
                <span class="text-white tw-text-xs">پرداخت شده</span>
              </div>
              <div class="tw-bg-gray-500 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-else>
                <span class="text-white tw-text-xs">در انتظار پرداخت</span>
              </div>
            </td>
            <td>
              {{Intl.NumberFormat('fa-IR').format(wallet.sum)}}
              تومان
            </td>
          </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>


</template>

<script>
import PlusIcon from "../../components/Icons/PlusIcon";
import {data} from "autoprefixer";
import ChargeWalletSideNav from "../../components/ChargeWalletSideNav";

export default {
  name: "index",
  layout: "PoshtebamPlusLayout",
  components: {
    ChargeWalletSideNav,
    PlusIcon
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
      displayChargeSideNav:false,
      walletInfo: [
        {
          payment_id:'hsusyshwsj474dkmd484ijdkfkkgf865',
          code:'123456',
          sum: 50000,
          status: 'success',
          name: 'آرزو حسن نژاد',
          date:'1401/03/1 - 14:13'
        },
        {
          payment_id:'hsusyshwsj474dkmd484ijdkfkkgf865',
          code:'456789',
          sum: 30000,
          status: 'failed',
          name: 'علی حسینی',
          date:'1401/03/1 - 14:13'

        },
        {
          payment_id:'hsusyshwsj474dkmd484ijdkfkkgf865',
          code:'123456',
          sum: 60000,
          status: 'pending',
          name: 'مریم مرادی',
          date:'1401/03/1 - 14:13'

        },
        {
          payment_id:'hsusyshwsj474dkmd484ijdkfkkgf865',
          code:'456789',
          sum: 30000,
          status: 'failed',
          name: 'سارا شادی',
          date:'1401/03/1 - 14:13'

        },
        {
          payment_id:'hsusyshwsj474dkmd484ijdkfkkgf865',
          code:'456789',
          sum: 40000,
          status: 'pending',
          name: 'احمد محمدی',
          date:'1401/03/1 - 14:13'

        },
      ]
    }
  }

}
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
.tw-table :where(thead, tfoot) :where(th, td) {
  background-color: white !important;
}
.tw-table :where(tbody th, tbody td) {
   background-color: rgb(249 250 251) !important;
}
</style>
