<template>
  <div class="row">
    <div class="col-md-1"></div>
    <div class="col-md-10 col-sm-12">
      <div class="d-flex flex-column">
        <div class="d-flex flex-column overflow-scroll px-1">
          <UpgradeItem :upgradeItem="plan" v-for="(plan,idx) in plans" :key="idx" class="my-3 upgrade_item"
                       @addOrder="addOrder($event)" @deleteOrder="deleteOrder($event)"/>
        </div>
        <!--        <CustomPagination v-if="totalPages.length>1" :pages="totalPages" @PageChanged="changePage($event)"/>-->

        <div class="col-12 pt-3 mt-auto">
          <div class="d-flex justify-content-between align-items-center">
            <div>
              جمع کل:
              {{ Intl.NumberFormat('fa-IR').format(this.sumPrice) }}
              تومان
            </div>
            <button type="button" class="tw-bg-[#d277ff] tw-rounded tw-text-white tw-p-2 tw-cursor-pointer px-3"
                    @click="CreateOrder()">
              ثبت سفارش
            </button>
          </div>
        </div>
      </div>
    </div>
    <div class="col-md-1"></div>
  </div>

</template>

<script>
import UpgradeItem from "../Products/UpgradeItem";
import CustomPagination from "../../components/utilities/CustomPagination"

export default {
  name: "UpgradeProductTab",
  components: {UpgradeItem, CustomPagination},
  data() {
    return {
      pageNumber: 0,
      totalPages: [],
      plans: null,
      sumPrice: 0,
      selectedPlans: []

    }
  },
  async fetch() {
    try {
      const response = await this.$repositories.getAllPlans.getAllPlans({
        pageNumber: this.pageNumber,
        count: 0
      })
      this.totalPages = []
      const result = Math.ceil(response.data.plansCount / 5)
      for (let i = 1; i <= result; i++) {
        this.totalPages.push(i);
      }
      this.plans = response.data.plans.filter(e => e.entityType === 1)
    } catch (error) {
      console.error(error)
    }

  },
  methods: {
    async CreateOrder() {

      if (this.selectedPlans.length === 0) {
        this.$toast.error('لطفا یک پلن انتخاب کنید')
      } else {
        this.$nextTick(() => {
          this.$nuxt.$loading.start();
        })
        try {
          let tmpSubOrders = []
          let tmpSubOrder = {
            orderId: 0,
            planId: 0,
            count: 1,
            vendorUserId: 0,
            price: 0,
            title: ''
          }
          this.selectedPlans.forEach((item) => {
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
            description: "ارتقا خدمت",
            sumPrice: this.sumPrice,
            serviceId: parseInt((this.$route.params.UpgradeProduct)),
            adsId: 0,
            userId: 0,
            createDate: new Date(Date.now()),
            status: 1,
            subOrders: tmpSubOrders
          });
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
          this.$toast.success("سفارش شما با موفقیت ثبت شد");
          this.$router.push({path: `/Products/Order/${res.data}`});

        } catch (error) {
          console.error(error)
        }
      }
    },
    addOrder(item) {
      this.selectedPlans.push(item)
      this.sumPrice = this.sumPrice + item.price


    },
    deleteOrder(item) {
      const idx = this.selectedPlans.findIndex((e) => e === item);
      this.selectedPlans.splice(idx, 1);
      this.sumPrice = this.sumPrice - item.price


    },
    changePage(id) {
      this.pageNumber = id
      this.$fetch()
    },

  }

}
</script>

<style scoped>
.upgrade_item:hover {
  cursor: pointer;
}

</style>
