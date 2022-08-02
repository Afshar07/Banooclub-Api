<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="mcontainer">
    <div class="container ">
      <div class="row ">
        <div class="well col-xs-10 col-sm-10 p-4 rounded col-md-12 col-xs-offset-1 col-sm-offset-1 col-md-offset-3 bg-white tw-shadow-xl">
          <div class="row">
            <div class="col-xs-6 col-sm-6 col-md-6">

                <nuxt-link  class="text-decoration-none " :to="`/Products/${serviceDetail.servicePackId}`">
                  <strong class="text-primary mb-3">{{ serviceDetail.title }}</strong>
                </nuxt-link>

                <br>
                <div class="d-flex align-items-center justify-content-between">
                  <small class="text-secondary">قیمت : </small>
                  <small class="">{{ serviceDetail.totalPrice }} </small>
                </div>
                <br>
                <div class="d-flex align-items-center justify-content-between">
                  <small class="text-secondary">ظرفیت باقی مانده : </small>
                  <small class="">{{ serviceDetail.maintain }} </small>
                </div>
                <br>
                <div class="d-flex align-items-center justify-content-between">
                  <small class="text-secondary">تاریخ انقضا : </small>
                  <small class="">{{ new Date(serviceDetail.expireDate).toLocaleDateString('fa-IR')  }} </small>
                </div>

            </div>
            <div v-if="serviceDetail.userInfo" class="col-xs-6 col-sm-6 col-md-6">

              <div class="d-flex align-items-center justify-content-between">
                <strong class="text-primary ">ارائه دهنده :</strong>
                <nuxt-link class="text-decoration-none" :to="`/user/${serviceDetail.userInfo.userName }/posts`">
                  <small class="">{{ serviceDetail.userInfo.userName }}@ </small>
                </nuxt-link>

              </div>
              <div class="d-flex align-items-center justify-content-between">
                <small class="text-secondary">نام و نام خانوادگی : </small>
                <small class="">{{ serviceDetail.userInfo.name + ' ' +  serviceDetail.userInfo.familyName }} </small>
              </div>
              <br>
              <div class="d-flex align-items-center justify-content-between">
                <small class="text-secondary">شماره تماس : </small>
                <small class="">{{ serviceDetail.userInfo.mobile }} </small>
              </div>
              <br>
              <div class="d-flex align-items-center justify-content-between">
                <small class="text-secondary">ایمیل : </small>
                <small class="">{{ serviceDetail.userInfo.email }} </small>
              </div>
              <div class="tw-grid tw-grid-cols-2 tw-gap-x-2">
                <div class="d-flex tw-col-span-1 align-items-center justify-content-between">
                  <button @click="GoToChat(serviceDetail)" class="tw-bg-blue-600 tw-flex tw-flex-1 tw-font-semibold tw-h-10 tw-items-center tw-justify-center tw-px-4 tw-rounded-md tw-text-white my-1">
                    چت
                  </button>
                </div>
                <div class="d-flex tw-col-span-1 align-items-center justify-content-between">
                  <a :href="`tel:${serviceDetail.mobile}`" class="text-decoration-none tw-bg-gray-200 tw-flex tw-flex-1 tw-font-semibold tw-h-10 tw-items-center tw-justify-center tw-px-4 tw-rounded-md my-1">
                    تماس
                  </a>
                </div>
              </div>



            </div>
          </div>
          <div class="row my-3">
            <div class="text-right">
              <h4 class="tw-font-bold ">جزئیات پرداخت</h4>
            </div>
            <table v-if="OrderData.paymentInfo" class="tw-table tw-w-full tw-table-zebra" style="border-radius: 10px;box-shadow: rgb(0 0 0 / 10%) 0px 1px 3px 0px, rgb(0 0 0 / 6%) 0px 1px 2px 0px;">
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
              <tr class="tw-hover"  >
                <th class="fw-normal">{{ OrderData.paymentInfo.paymentId }}</th>
                <th class="fw-normal" v-tooltip.bottom-start="OrderData.paymentInfo.transId">{{OrderData.paymentInfo.transId.substr(0,6)}}...</th>
                <th class="fw-normal">{{OrderData.paymentInfo.refId}}</th>
                <td>{{ new Date(OrderData.paymentInfo.createDate).toLocaleTimeString('fa-IR') }}</td>
                <td>
                  <div class="tw-bg-red-700 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-if="OrderData.paymentInfo.status === -2 || OrderData.paymentInfo.status ===-4">
                    <span class="text-white tw-text-xs">لغو شده</span>
                  </div>
                  <div class="tw-bg-green-700 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-else-if="OrderData.paymentInfo.status === 0">
                    <span class="text-white tw-text-xs">پرداخت شده</span>
                  </div>
                  <div class="tw-bg-gray-500 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-else-if="OrderData.paymentInfo.status === -122">
                    <span class="text-white tw-text-xs">در انتظار پرداخت</span>
                  </div>
                  <div class="tw-bg-yellow-500 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-else-if="OrderData.paymentInfo.status === -49">
                    <span class="text-white tw-text-xs">تراکنش تکراری</span>
                  </div>
                  <div class="tw-bg-blue-500 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-else-if="OrderData.paymentInfo.status === -3">
                    <span class="text-white tw-text-xs">در انتظار پاسخ</span>
                  </div>



                </td>
                <td>
                  {{Intl.NumberFormat('fa-IR').format(OrderData.paymentInfo.amount)}}
                  تومان
                </td>
              </tr>
              </tbody>
            </table>

          </div>
        </div>
      </div>
    </div>
  </div>

</template>

<script>
export default {
  name: "ServiceOrderDetail",
  layout:'PoshtebamPlusLayout',
  data(){
    return{
      OrderData:'',
      serviceDetail:''
    }
  },
  methods:{
    GoToChat(item){
      this.$router.push({path:'/Social/Chat',query:{userId:item.userInfo.userId,Photo:item.userInfo.selfieFileData}})
    },
  },

  async fetch(){
    try {
      const res = await this.$repositories.getAOrder.getAOrder({
        orderId:this.$route.params.id
      })
      this.OrderData = res.data
        this.serviceDetail = res.data.subOrders[0].serviceInfo
    }catch (e) {

    }
  },
}
</script>

<style scoped>
body{
  background-color: #eee;
}
.card{
  background-color: #fff;
  width: 600px;
  border: none;
  border-radius: 16px;

}

.info{
  line-height: 19px;
}

.name{
  color: #4c40e0;
  font-size: 18px;
  font-weight: 600;
}

.order{
  font-size: 14px;
  font-weight: 400;
  color: #b7b5b5;
}
.detail{

  line-height:19px;
}

.summery{


  color: #d2cfcf;
  font-weight: 400;
  font-size: 13px;
}


.text{

  line-height:15px;
}
.new{

  color: #000;
  font-size: 14px;
  font-weight: 600;
}
.money{


  font-size: 14px;
  font-weight:500;
}
.address{

  color: #d2cfcf;
  font-weight:500;
  font-size:14px;
}

.last{

  font-size: 10px;
  font-weight: 500;

}


.address-line{
  color: #4C40E0;
  font-size: 11px;
  font-weight: 700;
}
</style>
