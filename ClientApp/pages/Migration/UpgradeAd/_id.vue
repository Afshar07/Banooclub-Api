<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="container mcontainer">
    <div class="row">
      <div class="col-md-1"></div>
      <div class="col-md-10 col-sm-12">
        <div class="col-md-12 p-4">
          <div class="tw-w-full bg-white p-3 d-flex align-items-center justify-content-between gap-3  rounded ">
            <div class="d-flex align-items-center gap-2">
              <img src="/girl-icon-ads-ertegha.png" class="tw-w-[7rem] tw-h-[7rem] tw-object-contain" alt="">
              <div class="d-flex align-items-center flex-column">
                <h1 class="text-purple h6" style="font-weight: bolder!important;">ارتقا آگهی</h1>
                <strong class="text-pink">Boost AdvertiseMents</strong>
              </div>
            </div>
          </div>
        </div>
        <div class="d-flex flex-column">
          <div class="d-flex flex-column overflow-scroll px-1">
            <UpgradeItem v-tooltip="SetToolTip(plan)" :upgradeItem="plan" v-for="(plan,idx) in plans" :key="idx" class="my-3  upgrade_item" @addOrder="addOrder($event)" @deleteOrder="deleteOrder($event)" />
          </div>
          <div class="col-12 pt-3 mt-auto">
            <div class="d-flex justify-content-between align-items-center">
              <div>
                جمع کل:
                {{Intl.NumberFormat('fa-IR').format(this.sumPrice)}}
                تومان
              </div>
              <button type="button" class="button px-3" @click="CreateOrder()">
                ثبت سفارش
              </button>
            </div>
          </div>
        </div>
      </div>
      <div class="col-md-1"></div>
    </div>
  </div>


</template>

<script>
import UpgradeItem from "../../../components/Ads/UpgradeItem";

import CustomPagination from "../../../components/utilities/CustomPagination"

export default {
  name: "UpgradeProductTab",
  layout:'PoshtebamPlusLayout',
  components: {UpgradeItem,CustomPagination},
  data(){
    return{
      pageNumber:0,
      totalPages:[],
      plans:null,
      sumPrice:0,
      selectedPlans:[]

    }
  },
  head() {
    return {

      title: 'ارتقا آگهی',
      meta: [
        {
          hid: "description",
          name: "description",
          content:' ارتقا آگهی',
        },
      ],
    };
  },
  async fetch(){
    try {
      const response = await this.$repositories.getAllPlans.getAllPlans({
        pageNumber:this.pageNumber,
        count:0
      })
      this.totalPages = []
      const result = Math.ceil(response.data.plansCount / 5)
      for (let i = 1; i <= result; i++) {
        this.totalPages.push(i);
      }
      this.plans = response.data.plans.filter(e=> e.entityType===2)
    }
    catch (error){
      console.error(error)
    }

  },
  methods:{
    SetToolTip(plan){
      if(plan.type===1){
        return {content:'با انتخاب آتیش زدم آگهی شما نشانه دار میشود تا کاربران متوجه شوند شما در فروش این آگهی عجله دارید'}
      }else if (plan.type===3){
        return {content:'با انتخاب برترین آگهی شما به قسمت برترین ها میرود'}
      }else if (plan.type===2){
        return {content:'با انتخاب پیشنهادات آگهی شما به قسمت پیشنهادات میرود'}
      }
    },
    async CreateOrder(){
      this.$nuxt.$loading.start();
      try {
        let tmpSubOrders = []
        let tmpSubOrder = {
          orderId: 0,
          planId: 0,
          count: 1,
          vendorUserId: 0,
          price: 0,
          title:''
        }
        this.selectedPlans.forEach((item)=>{
          tmpSubOrder.planId = item.planId
          tmpSubOrder.price = item.price
          tmpSubOrder.title = item.title
          tmpSubOrder.vendorUserId = 0
          tmpSubOrder.orderId = 0
          tmpSubOrder.count = 1

          const clone = {...tmpSubOrder}
          tmpSubOrders.push(clone)
          tmpSubOrder.planId = 0
          tmpSubOrder.orderId = 0
          tmpSubOrder.count = 0
          tmpSubOrder.vendorUserId = 0
          tmpSubOrder.price = 0
          tmpSubOrder.title = ''

        })
        const res = await this.$repositories.createAOrder.createAOrder({
          isDeleted: false,
          orderId: 0,
          isPayed: false,
          description: "ارتقا آگهی",
          sumPrice: this.sumPrice,
          serviceId:0,
          adsId:parseInt(this.$route.params.id),
          userId: 0,
          createDate: new Date(Date.now()),
          status: 1,
          subOrders: tmpSubOrders
        });
        this.$nuxt.$loading.finish();
        this.$nuxt.loading = false;
        this.$toast.success("سفارش شما با موفقیت ثبت شد");
        this.$router.push({path: `/Products/Order/${res.data}`});

      }
      catch (error){
        console.error(error)
      }
    },
    addOrder(item){
      this.selectedPlans.push(item)
      this.sumPrice = this.sumPrice + item.price


    },
    deleteOrder(item){
      const idx = this.selectedPlans.findIndex((e) => e === item);
      this.selectedPlans.splice(idx, 1);
      this.sumPrice = this.sumPrice - item.price


    },
    changePage(id){
      this.pageNumber = id
      this.$fetch()
    },

  }

}
</script>

<style scoped>
.upgrade_item:hover{
  cursor: pointer;
}

</style>
