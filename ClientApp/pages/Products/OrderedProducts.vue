<template>
  <div class="container mcontainer px-2 h-100">
    <div class="d-flex justify-content-between align-items-center">
      <h2 class="tw-text-2xl tw-font-semibold py-3">خدمات سفارش داده شده</h2>
    </div>
    <div class="tw-overflow-x-auto">
      <div class="tw-overflow-x-auto">
        <table class="tw-table tw-w-full tw-table-zebra"
               style="border-radius: 10px;box-shadow: rgb(0 0 0 / 10%) 0px 1px 3px 0px, rgb(0 0 0 / 6%) 0px 1px 2px 0px;">
          <!-- head -->
          <thead>
          <tr>
            <th>شناسه</th>
            <th>نام خدمت</th>
            <th>ظرفیت باقی مانده</th>
            <th>تاریخ انقضا خدمت</th>
            <th>وضعیت خدمت</th>
            <th>عملیات</th>
          </tr>
          </thead>
          <tbody>
          <!-- rows -->
          <tr class="tw-hover" v-for="(item,idx) in AllOrders" :key="idx">
            <th class="fw-normal">{{ item.servicePackId }}</th>
            <th class="fw-normal">
              <nuxt-link class="tw-text-black tw-transition hover:tw-text-blue-500 text-decoration-none"
                         :to="`/Products/${item.servicePAckId}`">
                {{ item.title }}
              </nuxt-link>

            </th>
            <th class="fw-normal">
              <small>{{ item.maintain }}</small>
            </th>
            <th class="fw-normal">
              <small> {{ new Date(item.expireDate).toLocaleDateString('fa-IR') }} </small>
            </th>
            <th class="fw-normal">
             <span v-if="  item.status===1" class="badge text-white pill bg-success">
               فعال

             </span>
              <span v-if="  item.status===2" class="badge text-white pill bg-danger">
               غیرفعال

             </span>
              <span v-if="  item.status===3" class="badge text-white pill bg-secondary">
               نیاز به بازبینی مجدد

             </span>
            </th>
            <td>
              <div class="tw-bg-green-700 tw-rounded d-inline-flex justify-content-center align-items-center p-1"
                   v-if="item.status === 3">
                <span class="text-white tw-text-xs">پرداخت </span>
              </div>

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

    <div v-if="ServiceMembers!==null" class="d-flex mt-3 justify-content-between align-items-center">
      <h3 class="tw-text-xl tw-font-semibold py-3">کاربرانی که این خدمت را سفارش داده اند</h3>
    </div>
    <div v-if="ServiceMembers!==null" class="tw-overflow-x-auto">
      <div class="tw-overflow-x-auto">
        <table class="tw-table tw-w-full tw-table-zebra"
               style="border-radius: 10px;box-shadow: rgb(0 0 0 / 10%) 0px 1px 3px 0px, rgb(0 0 0 / 6%) 0px 1px 2px 0px;">
          <!-- head -->
          <thead>
          <tr>
            <th>شناسه کاربر</th>
            <th>نام و نام خانوادگی</th>
            <th>نام کاربری</th>
            <th>شماره تماس</th>
            <th>عملیات</th>
          </tr>
          </thead>
          <tbody>
          <!-- rows -->
          <tr class="tw-hover" v-for="(item,idx) in ServiceMembers" :key="idx">
            <th class="fw-normal">{{ item.userId }}</th>
            <th class="fw-normal">
              <small>{{ item.name + ' ' + item.familyName }}</small>

            </th>
            <th class="fw-normal">
              <small>{{ item.userName }}</small>
            </th>
            <th class="fw-normal">
              <small> {{ item.mobile }} </small>
            </th>
            <td>
              <div
                class="tw-bg-blue-500 tw-rounded tw-cursor-pointer d-inline-flex justify-content-center align-items-center p-1"
                @click="SetSelectedUser(item)">
                <span class="text-white tw-text-xs">مشاهده جزئیات کاربر </span>
              </div>
            </td>
          </tr>
          </tbody>
        </table>
      </div>
    </div>
    <div v-if="SelectedService!==null && ServiceMembers===null"
         class="col-md-12 d-flex align-items-center justify-content-center">
      <Spinner></Spinner>
    </div>
    <div v-if="SelectedUser!==null" class="col-md-12 my-3 bg-white p-3 ">
      <div class="row">
        <div class="col-md-6">
          <span  class="text-secondary">نام کوچک : </span>
          <span>{{SelectedUser.name}}</span>

        </div>
        <div class="col-md-6">
          <span  class="text-secondary">نام خانوادگی : </span>
          <span>{{SelectedUser.familyName}}</span>

        </div>
        <div class="col-md-6 my-3">
          <span  class="text-secondary">نام کاربری: </span>
          <nuxt-link class=" tw-transition tw-text-blue-500 text-decoration-none" :to="`/user/${SelectedUser.userName}/posts`">@{{SelectedUser.userName}}</nuxt-link>
        </div>
        <div class="col-md-6 my-3">
          <span  class="text-secondary">شماره موبایل : </span>
          <span v-if="SelectedUser.mobile!=='' || SelectedUser.mobile!==null">{{SelectedUser.mobile}}</span>
          <span v-else>-</span>
        </div>
        <div class="col-md-12 d-flex align-items-center gap-2">
          <span  class="text-secondary">ایمیل : </span>
          <span v-if="SelectedUser.email!=='' || SelectedUser.email!==null">{{SelectedUser.email}}</span>
          <span v-else>-</span>


        </div>

      </div>

    </div>
  </div>


</template>

<script>
import PlusIcon from "../../components/Icons/PlusIcon";
import {data} from "autoprefixer";
import ChargeWalletSideNav from "../../components/ChargeWalletSideNav";
import Spinner from "../../components/Spinner";

export default {
  name: "index",
  layout: "PoshtebamPlusLayout",
  components: {
    ChargeWalletSideNav,
    PlusIcon,
    Spinner
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
      displayChargeSideNav: false,
      AllOrders: [],
      SelectedPageNumber: 1,
      SelectedService: null,
      ServiceMembers: null,
      SelectedUser:null,


    }
  },
  methods: {
    SetSelectedUser(item){
      this.SelectedUser = item
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
  },
  async fetch() {
    try {
      const res = await this.$repositories.GetOrderedServices.GetOrderedServices()
      this.AllOrders = res.data
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
