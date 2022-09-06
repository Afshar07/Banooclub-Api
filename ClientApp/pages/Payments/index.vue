<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="container NewBg mcontainer px-2">
    <div class="d-flex justify-content-between align-items-center">
      <div class="tw-w-full bg-white p-3 d-flex align-items-center justify-content-between gap-3  rounded ">
        <div class="d-flex align-items-center gap-2">
          <img src="/girl-icon-mali.png" class="tw-w-[7rem] tw-h-20" alt="">
          <div class="d-flex align-items-center flex-column">
            <h1 class="text-purple h6">پرداخت ها</h1>
            <strong class="text-pink">Payments</strong>
          </div>
        </div>
        <div class="d-flex justify-content-center align-items-center">
          <div class="d-flex flex-column">
             <span class="text-purple">موجودی کیف پول:</span>
             <span class="text-pink">{{ Intl.NumberFormat('fa-IR').format($store.state.WalletAmount) }}تومان</span>
          </div>
          <button @click="displayChargeSideNav = true" v-tooltip="{content:'شارژ کیف پول'}"
                  class="btn AddReplyBtn text-white">
            <PlusIcon fill="#ff6f9e" style="width: 30px; height: 30px;"/>
          </button>
        </div>
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

      <div class="tw-overflow-x-auto bg-white p-2 py-3 rounded my-3">
        <table class="tw-table tw-w-full tw-table-auto tw-rounded" >
          <!-- head -->
          <thead>
          <tr>
            <th>شناسه</th>
            <th>شناسه پرداخت</th>
            <th>کد پیگیری</th>
            <th>تاریخ</th>
            <th>وضعیت</th>
            <th>مجموع</th>
          </tr>
          </thead>
          <tbody>
          <!-- rows -->
          <tr class="tw-hover" v-for="(item,idx) in AllPayments" :key="idx">
            <th class="fw-normal">{{ item.paymentId }}</th>
            <th class="fw-normal" v-tooltip.bottom-start="item.transId">{{ item.transId.substr(0, 6) }}...</th>
            <th class="fw-normal">{{ item.refId }}</th>
            <td>{{ new Date(item.createDate).toLocaleDateString('fa-IR') }}</td>
            <td>
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
            <td>
              {{ Intl.NumberFormat('fa-IR').format(item.amount) }}
              تومان
            </td>
          </tr>
          </tbody>
        </table>
        <CustomPagination class="my-3" :activePage="SelectedPageNumber" :totalPages="totalPages"
                          @PageChanged="changePage($event)"/>
      </div>

  </div>


</template>

<script>
import PlusIcon from "../../components/Icons/PlusIcon";
import {data} from "autoprefixer";
import ChargeWalletSideNav from "../../components/ChargeWalletSideNav";
import CustomPagination from "@/components/utilities/CustomPagination";

export default {
  name: "index",
  layout: "PoshtebamPlusLayout",
  components: {
    ChargeWalletSideNav,
    CustomPagination,
    PlusIcon
  },
  head() {
    return {
      bodyAttrs: {
        class: this.displayChargeSideNav ? "fixed-body" : "",
      },
      title: 'بانو کلاب - پرداخت ها'
    };
  },

  data() {
    return {
      displayChargeSideNav: false,
      AllPayments: [],
      SelectedPageNumber: 1,
      totalPages: []

    }
  },
  methods: {
    async goToUserProfile(user) {
      try {
        this.$router.push({path: `/user/${user.userName}/posts`});
      } catch (e) {
        console.log(e)
      }
    },
    changePage(id) {
      this.SelectedPageNumber = id
      this.GetMyPayments()
    },

    async GetMyPayments() {
      try {
        const res = await this.$repositories.GetMyPayments.GetMyPayments({
          pageNumber: this.SelectedPageNumber,
          count: 10
        })
        this.AllPayments = res.data.payments
        this.totalPages = []
        const result = Math.ceil(res.data.paymentsCount / 10)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
        }
      } catch (e) {
        console.log(e)
      }
    },
  },


  async fetch() {
    try {
      const res = await this.$repositories.GetMyPayments.GetMyPayments({
        pageNumber: this.SelectedPageNumber,
        count: 10
      })
      this.AllPayments = res.data.payments
      this.totalPages = []
      const result = Math.ceil(res.data.paymentsCount / 10)
      for (let i = 1; i <= result; i++) {
        this.totalPages.push(i);
      }
    } catch (e) {
      console.log(e)
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
  background-color: #80808021 !important;

}

th.fw-normal {
  background-color: #80808021 !important;
}

td {
  background-color: #80808021 !important;
}

</style>
