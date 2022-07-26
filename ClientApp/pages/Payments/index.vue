<template>
  <div class="container mcontainer px-2">
    <div class="d-flex justify-content-between align-items-center">
      <h2 class="tw-text-2xl tw-font-semibold py-3">پرداخت ها</h2>
      <div class="d-flex justify-content-center align-items-center">
        <span style="color: rgb(103, 103, 103)">موجودی کیف پول:
          {{Intl.NumberFormat('fa-IR').format($store.state.WalletAmount)}}
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
        <table class="tw-table tw-w-full tw-table-zebra" style="border-radius: 10px;box-shadow: rgb(0 0 0 / 10%) 0px 1px 3px 0px, rgb(0 0 0 / 6%) 0px 1px 2px 0px;">
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
          <tr class="tw-hover" v-for="(item,idx) in AllPayments" :key="idx" >
            <th class="fw-normal">{{ item.paymentId }}</th>
            <th class="fw-normal">{{item.transId}}</th>
            <th class="fw-normal">{{item.refId}}</th>
            <td v-if="item.userInfo" @click="goToUserProfile(item.userInfo)"> {{ item.userInfo.name + ' ' + item.userInfo.familyName   }}</td>
            <td>{{ new Date(item.createDate).toLocaleTimeString('fa-IR') }}</td>
            <td>
              <div class="tw-bg-red-700 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-if="item.status === -2 || item.status ===-4">
                <span class="text-white tw-text-xs">لغو شده</span>
              </div>
              <div class="tw-bg-green-700 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-else-if="item.status === 0">
                <span class="text-white tw-text-xs">پرداخت شده</span>
              </div>
              <div class="tw-bg-gray-500 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-else-if="item.status === -122">
                <span class="text-white tw-text-xs">در انتظار پرداخت</span>
              </div>
              <div class="tw-bg-yellow-500 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-else-if="item.status === -49">
                <span class="text-white tw-text-xs">تراکنش تکراری</span>
              </div>
              <div class="tw-bg-blue-500 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-else-if="item.status === -3">
                <span class="text-white tw-text-xs">در انتظار پاسخ</span>
              </div>



            </td>
            <td>
              {{Intl.NumberFormat('fa-IR').format(item.amount)}}
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
      title: 'پشت بام پلاس - پرداخت ها'
    };
  },

  data() {
    return {
      displayChargeSideNav:false,
      AllPayments:[],
      SelectedPageNumber:1

    }
  },
  methods:{
    async goToUserProfile(user){
      try {
        this.$router.push({path: `/user/${user.userName}/posts`});
      }catch (e){
        console.log(e)
      }
    },
  },
  async fetch(){
    try {
      const res = await this.$repositories.GetMyPayments.GetMyPayments({
        pageNumber:this.SelectedPageNumber,
        count:10
      })
      this.AllPayments = res.data.payments
    }catch (e) {
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
td{
  background-color: #80808021 !important;
}

</style>
