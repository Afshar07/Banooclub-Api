<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="container mcontainer px-2">
    <div class="d-flex justify-content-between align-items-center">
      <h2 class="tw-text-2xl tw-font-semibold py-3">سفارشات</h2>
    </div>

    <div
      class="offcanvas offcanvas-start sidebar-bg"
      tabindex="-1"
      id="offcanvasExample"
      aria-labelledby="offcanvasExample"
      style="z-index: 9999999;"
    >
    </div>
    <div class="tw-overflow-x-auto">
      <div class="tw-overflow-x-auto">
        <table class="tw-table tw-w-full tw-table-compact tw-table-zebra" style="border-radius: 10px;box-shadow: rgb(0 0 0 / 10%) 0px 1px 3px 0px, rgb(0 0 0 / 6%) 0px 1px 2px 0px;">
          <!-- head -->
          <thead>
          <tr>
            <th>شناسه</th>
            <th>توضیحات</th>
            <th>نام سفارش</th>
            <th>جزئیات</th>
            <th>تاریخ</th>
            <th>وضعیت</th>
            <th>مجموع</th>
            <th>عملیات</th>
          </tr>
          </thead>
          <tbody>
          <!-- rows -->
          <tr class="tw-hover" v-for="(item,idx) in AllOrders" :key="idx" >
            <th class="fw-normal">{{ item.orderId }}</th>
            <th class="fw-normal">{{item.description}}</th>
            <th class="fw-normal">
              <div v-for="(el,idx) in item.subOrders" :key="idx" class="d-flex flex-column">
                <small>{{el.title}}</small>
              </div>
            </th>
            <th v-if="item.serviceInfo!==null" class="fw-normal">
              <small> برای خدمت {{item.serviceInfo.title}} </small>
            </th>
            <th v-if="item.adsInfo!==null" class="fw-normal">
              <small> برای آگهی {{item.adsInfo.title}} </small>
            </th>
            <th v-else-if="item.adsInfo===null && item.serviceInfo===null" class="fw-normal">
              <small> - </small>
            </th>
            <td>{{ new Date(item.createDate).toLocaleDateString('fa-IR') }}</td>
            <td>
              <div class="tw-bg-red-700 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-if="item.status === 3">
                <span class="text-white tw-text-xs">لغو شده</span>
              </div>
              <div class="tw-bg-green-700 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-else-if="item.status === 2">
                <span class="text-white tw-text-xs">پرداخت شده</span>
              </div>
              <div class="tw-bg-gray-500 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-else-if="item.status === 1">
                <span class="text-white tw-text-xs">در حال پردازش</span>
              </div>

            </td>
            <td>
              {{Intl.NumberFormat('fa-IR').format(item.sumPrice)}}
              تومان
            </td>
            <td>
              <div class="tw-bg-green-700 tw-cursor-pointer tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-if="item.status === 3">
                <span class="text-white tw-text-xs">پرداخت </span>
              </div>

              <div class="tw-bg-blue-500 tw-cursor-pointer tw-rounded d-inline-flex justify-content-center align-items-center p-1"  >

                  <span class="text-white tw-text-xs" @click="RouteToOrder(item)">مشاهده </span>


              </div>
            </td>
          </tr>
          </tbody>
        </table>
      </div>
    </div>
      <div class="col-md-12">
        <CustomPagination v-if="totalPages.length>1" :activePage="SelectedPageNumber" :totalPages="totalPages" @PageChanged="changePage($event)"/>
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
    PlusIcon,
    CustomPagination
  },
  head() {
    return {
      bodyAttrs: {
        class: this.displayChargeSideNav ? "fixed-body" : "",
      },
      title: 'بانو کلاب - سفارشات'

    };
  },

  data() {
    return {
      displayChargeSideNav:false,
      AllOrders:[],
      SelectedPageNumber:1,
      totalPages:[]

    }
  },
  watch:{
    SelectedPageNumber:function (){
      this.GetAllOrders()
    }
  },
  methods:{
    async GetAllOrders(){
      try {
        const res = await this.$repositories.OrderGetByUserId.OrderGetByUserId({
          pageNumber:this.SelectedPageNumber,
          count:10

        })
        this.AllOrders = res.data.orders
        this.totalPages = []
        const result = Math.ceil(res.data.orderCount / 10)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
        }
      }catch (e) {
        console.log(e)
      }
    },
    changePage(id){
      this.SelectedPageNumber = id
    },
    RouteToOrder(item){
      if(item && item.subOrders&& item.subOrders[0].serviceId!==null){
        this.$router.push({
          path:`/Orders/ServiceOrderDetail/${item.orderId}`
        })
      }else{
        this.$router.push({
          path:`/Products/Order/${item.orderId}`
        })

      }

    }
  },
  async fetch(){
    try {
      const res = await this.$repositories.OrderGetByUserId.OrderGetByUserId({
        pageNumber:this.SelectedPageNumber,
        count:10

      })
      this.AllOrders = res.data.orders
      this.totalPages = []
      const result = Math.ceil(res.data.orderCount / 10)
      for (let i = 1; i <= result; i++) {
        this.totalPages.push(i);
      }
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
</style>
