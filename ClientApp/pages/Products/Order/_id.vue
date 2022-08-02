<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="container mcontainer  containerBox">


      <div class="px-4 py-5">

        <h5 class="text-uppercase">{{ $auth.user.userInfo.userName }}</h5>



        <h4 class="mt-5 theme-color mb-5">سفارش شما ثبت شد</h4>

        <span class="theme-color">خلاصه پرداخت</span>
        <div class="mb-3">
          <hr class="new1">
        </div>

        <div class="d-flex justify-content-between" v-for="order in OrderData.subOrders">
          <span class="font-weight-bold">{{ order.title }}</span>
          <span class="text-muted">{{  Intl.NumberFormat('fa-IR').format(order.price) }} تومان</span>
        </div>

        <div class="d-flex justify-content-between mt-3">
          <span class="font-weight-bold">مجموع : </span>
          <span class="font-weight-bold theme-color">{{  Intl.NumberFormat('fa-IR').format(OrderData.sumPrice)  }} تومان</span>
        </div>

<!--        v-if="OrderData && OrderData.subOrders && OrderData.subOrders[0].planId !==0 "-->
        <ChargeWalletSideNav :show="displaySideNav" @close="displaySideNav = false"/>
        <div v-if="OrderData.status!==2" class="text-center mt-5 d-flex align-items-center justify-content-center gap-2">
          <div v-if=" OrderData&&OrderData.subOrders &&OrderData.subOrders[0].title !== 'شارژ کیف پول'"  class="d-flex flex-column justify-content-center">
            <button class="btn btn-warning text-white px-2" :disabled="OrderData.sumPrice>$store.state.WalletAmount" @click="PayByWallet()">پرداخت با کیف پول</button>
            <small class="my-2">موجودی کیف پول شما : {{  Intl.NumberFormat('fa-IR').format($store.state.WalletAmount) }} تومان</small>
            <small v-if="OrderData.sumPrice>$store.state.WalletAmount">موجودی کیف پول شما کافی نمیباشد <a class="text-decoration-none text-primary tw-cursor-pointer" @click="displaySideNav= true">افزایش موجودی</a></small>
          </div>
          <button class="button px-2" @click="createPayment()">پرداخت</button>
        </div>

      </div>



    </div>


</template>
<script>
import ChargeWalletSideNav from "../../../components/ChargeWalletSideNav";
export default {
  layout: "PoshtebamPlusLayout",
components:{ChargeWalletSideNav},
  name: "OrderComplete",
  head() {
    return {
      title: 'فاکتور سفارش',
      meta: [
        {
          hid: "description",
          name: "description",
          content:'فاکتور سفارش',
        },
      ],
    }
  },
  async fetch(){
    try {
      const response = await this.$repositories.getAOrder.getAOrder({
        orderId:this.$route.params.id
      })
      this.OrderData = response.data
      console.log(this.OrderData)
    }
    catch (error){
      console.error(error)
    }

  },

  data(){
    return{
      OrderData:'',
      AddressData:'',
      displaySideNav:false
    }
  },
  methods:{
    async PayByWallet(){
        try {
          const res = await this.$repositories.PayByWallet.PayByWallet({
              orderId: this.OrderData.orderId
          })
          if(res.data===1){
            this.$toast.success('سفارش شما با موفقیت پرداخت شد')
            this.$router.push('/Orders')
          await  this.GetCredit()

          }else if (res.data===0){
            this.$toast.success('موجودی کیف پول شما کافی نمیباشد')
          }
        }catch (e){
          console.log(e)
        }
    },
    async GetCredit(){
      try {
        const res = await this.$repositories.GetCredit.GetCredit()
        this.$store.commit('SetWalletAmount',res.data.data)
      }catch (e) {
        console.log(e)
      }
    },
    async createPayment(){
      let walletCharge = false
      if(this.OrderData.subOrders[0].title === 'شارژ کیف پول' ){
        walletCharge = true
      }
      try {
        const res = await this.$repositories.createAPayment.createAPayment({
          orderId: parseInt(this.$route.params.id),
          amount: this.OrderData.sumPrice,
          createDate: "2022-06-13T09:09:35.951Z",
          transId: "",
          description:walletCharge?'شارژ کیف پول':this.OrderData.description,
          walletCharge:walletCharge,
          status: 0,
          refId: ""
        })
        window.location.replace(res.data);
        walletCharge = false

      }
      catch (error){
        console.error(error)
      }
    }
  },
  mounted(){

    //this.GetBillingAddress();
  },

}
</script>
<style  scoped>
body {
  background-color: #5165ff;
  height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center
}

.modal-body {
  background-color: #fff;
  border-color: #fff;

}


.close {
  color: #000;
  cursor: pointer;
}

.close:hover {
  color: #000;
}


.theme-color{

  color: #004cb9;
}
hr.new1 {
  border-top: 2px dashed #fff;
  margin: 0.4rem 0;
}


.btn-primary {
  color: #fff;
  background-color: #004cb9;
  border-color: #004cb9;
  padding: 12px;
  padding-right: 30px;
  padding-left: 30px;
  border-radius: 1px;
  font-size: 17px;
}


.btn-primary:hover {
  color: #fff;
  background-color: #004cb9;
  border-color: #004cb9;
  padding: 12px;
  padding-right: 30px;
  padding-left: 30px;
  border-radius: 1px;
  font-size: 17px;
}

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

</style>
