<template>
  <div class="charge-wallet-sidenav-container">
    <div v-if="show" class="charge-wallet-sidenav-backdrop" @click="$emit('close')"></div>
    <transition name="charge-wallet-slide-side">
      <div v-if="show" class="charge-wallet-sidenav">
        <div class="d-flex flex-column">
          <div>
           شارژ کیف پول
          </div>

          <div class="col-12 pt-3 px-0" style="height: 100%;">
            <label>مبلغ</label>
            <div class="d-flex justify-content-center align-items-center">
              <input type="number" v-model="WalletAmount" class="with-border" placeholder="مبلغ">
              <span class="px-2">تومان</span>
            </div>
          </div>
          <div class=" mt-auto pt-3">
            <button type="button" class=" bg-purple text-white p-2 rounded tw-cursor-pointer  tw-w-full mt-auto" @click="createOrder">
              ثبت
            </button>
          </div>
        </div>
      </div>
    </transition>
  </div>

</template>

<script>
export default {
  name: "ChargeWalletSideNav",
  props: {
    show: {
      type: Boolean,
      default: false,
    },
  },

  data(){
    return{
      WalletAmount:0
    }
  },

  methods:{
   async createOrder(){
        if(this.WalletAmount===0){
          this.$toast.error('یک مقدار وارد کنید')
        }else{
          try {
            this.$nextTick(()=>{
              this.$nuxt.$loading.start();
            })

            let tmpSubOrders = []
            let tmpSubOrder = {
              orderId: 0,
              planId: 0,
              count: 1,
              vendorUserId: 0,
              price: 0,
              title:''
            }
            tmpSubOrder.planId = 0
            tmpSubOrder.price = this.WalletAmount
            tmpSubOrder.title = 'شارژ کیف پول'
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


            const res = await this.$repositories.createAOrder.createAOrder({
              isDeleted: false,
              orderId: 0,
              isPayed: false,
              description: "شارژ کیف پول",
              sumPrice: this.WalletAmount,
              userId: 0,
              createDate: new Date(Date.now()),
              status: 1,
              subOrders: tmpSubOrders
            });
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false;
            this.$toast.success("سفارش شما با موفقیت ثبت شد.");
            this.$router.push({path: `/Products/Order/${res.data}`});
            this.WalletAmount = 0
            this.$emit('close')

          }
          catch (error){
            console.error(error)
          }finally {
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false;
          }
        }

    },
  }

}
</script>

<style>
.charge-wallet-sidenav-container {
  direction: rtl;
}

.charge-wallet-sidenav-backdrop {
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.7);
  z-index: 999999;
  position: fixed;
  top: 0;
  left: 0;
}

@media (max-width: 576px) {
  .charge-wallet-sidenav {
    height: 100%;
    width: 350px;
    background-color: white;
    z-index: 10000;
    position: fixed;
    top: 0 !important;
    left: 0;
    box-sizing: border-box;
    padding: 30px;
  }
}

.charge-wallet-sidenav {
  height: 100%;
  width: 300px;
  background-color: white;
  z-index: 999999;
  position: fixed;
  top: 0;
  left: 0;
  box-sizing: border-box;
  padding: 15px;
}

.charge-wallet-slide-side-enter-active,
.charge-wallet-slide-side-leave-active {
  transition: all 0.3s ease-out;
}

.charge-wallet-slide-side-enter,
.charge-wallet-slide-side-leave-to {
  transform: translateX(-100%);
}

.nav-list {
  list-style: none;
  padding: 0;
  margin: 0;
  text-align: left;
}

.nav-item {
  margin: 20px 0;
}

.nav-item a {
  text-decoration: none;
  color: black;
  font-size: 1.5rem;
}

.nav-item a:hover,
.nav-item a:active {
  color: red;
}

.switch {
  position: relative;
  display: inline-block;
  width: 90px;
  height: 34px;
}

.switch input {
  display: none;
}

.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #999;
  -webkit-transition: 0.4s;
  transition: 0.4s;
}

.slider:before {
  position: absolute;
  content: "";
  height: 26px;
  width: 26px;
  left: 4px;
  bottom: 4px;
  background-color: white;
  -webkit-transition: 0.4s;
  transition: 0.4s;
}

input:checked + .slider {
  background-color: #088dcd;
}

input:focus + .slider {
  box-shadow: 0 0 1px #2196f3;
}

input:checked + .slider:before {
  -webkit-transform: translateX(55px);
  -ms-transform: translateX(55px);
  transform: translateX(55px);
}

/*------ ADDED CSS ---------*/
.on {
  display: none;
}

.on {
  color: white;
  position: absolute;
  transform: translate(-50%, -50%);
  top: 50%;
  left: 35%;
  font-size: 10px;
}

.off {
  color: white;
  position: absolute;
  transform: translate(-50%, -50%);
  top: 50%;
  left: 65%;
  font-size: 10px;
}

input:checked + .slider .on {
  display: block;
}

input:checked + .slider .off {
  display: none;
}

/*--------- END --------*/
/* Rounded sliders */
.slider.round {
  border-radius: 34px;
}

.slider.round:before {
  border-radius: 50%;
}

.charge-wallet-sidenavOptionText {
  font-size: 14px;
  color: #999;
  padding-top: 8px;
}
</style>
