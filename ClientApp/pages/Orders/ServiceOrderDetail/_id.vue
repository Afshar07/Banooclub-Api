<template>

  <div class="tw-py-14 tw-px-4 md:tw-px-6 2xl:tw-px-20 2xl:container 2xl:tw-mx-auto">
    <div class="tw-flex tw-justify-start item-start tw-space-y-2 tw-flex-col">
      <h1 class="tw-text-3xl dark:tw-text-white lg:tw-text-4xl tw-font-semibold tw-leading-7 lg:tw-leading-9 tw-text-gray-800">شناسه سفارش : </h1>
      <p class="tw-text-base dark:tw-text-gray-300 tw-font-medium tw-leading-6 tw-text-gray-600">تاریخ : {{new Date(Date.now()).toLocaleDateString('fa-IR')}}</p>
    </div>
    <div class="tw-mt-10 tw-flex tw-flex-col xl:tw-flex-row jusitfy-center tw-items-stretch tw-w-full xl:tw-space-x-8 tw-space-y-4 md:tw-space-y-6 xl:tw-space-y-0">
      <div class="tw-flex tw-flex-col tw-justify-start tw-items-start tw-w-full tw-space-y-4 md:tw-space-y-6 xl:tw-space-y-8">
        <div class="tw-flex tw-flex-col tw-justify-start tw-items-start dark:tw-bg-gray-800 tw-bg-gray-50 tw-px-4 tw-py-4 md:tw-py-6 md:tw-p-6 xl:tw-p-8 tw-w-full">
          <div class="tw-mt-4 md:tw-mt-6 tw-flex tw-flex-col md:tw-flex-row tw-justify-start tw-items-start md:tw-items-center md:tw-space-x-6 xl:tw-space-x-8 tw-w-full">
            <div class="tw-pb-4 md:tw-pb-8 tw-w-full md:tw-w-40">
              <img class="tw-w-full tw-hidden md:tw-block" src="https://i.ibb.co/84qQR4p/Rectangle-10.png" alt="dress" />
              <img class="tw-w-full md:tw-hidden" src="https://i.ibb.co/L039qbN/Rectangle-10.png" alt="dress" />
            </div>
            <div class="tw-border-b tw-border-gray-200 md:tw-flex-row tw-flex-col tw-flex tw-justify-between tw-items-start tw-w-full tw-pb-8 tw-space-y-4 md:tw-space-y-0">
              <div class="tw-w-full tw-flex tw-flex-col tw-justify-start tw-items-start tw-space-y-8">
                <h3 class="tw-text-xl dark:tw-text-white xl:tw-text-2xl tw-font-semibold tw-leading-6 tw-text-gray-800">Premium Quaility Dress</h3>
                <div class="tw-flex tw-justify-start tw-items-start tw-flex-col tw-space-y-2">
                  <p class="tw-text-sm dark:tw-text-white tw-leading-none tw-text-gray-800"><span class="dark:tw-text-gray-400 tw-text-gray-300">Style: </span> Italic Minimal Design</p>
                  <p class="tw-text-sm dark:tw-text-white tw-leading-none tw-text-gray-800"><span class="dark:tw-text-gray-400 tw-text-gray-300">Size: </span> Small</p>
                  <p class="tw-text-sm dark:tw-text-white tw-leading-none tw-text-gray-800"><span class="dark:tw-text-gray-400 tw-text-gray-300">Color: </span> Light Blue</p>
                </div>
              </div>
              <div class="tw-flex tw-justify-between tw-space-x-8 tw-items-start tw-w-full">
                <p class="tw-text-base dark:tw-text-white xl:tw-text-lg tw-leading-6">$36.00 <span class="tw-text-red-300 tw-line-through"> $45.00</span></p>
                <p class="tw-text-base dark:tw-text-white xl:tw-text-lg tw-leading-6 tw-text-gray-800">01</p>
                <p class="tw-text-base dark:tw-text-white xl:tw-text-lg tw-font-semibold tw-leading-6 tw-text-gray-800">$36.00</p>
              </div>
            </div>
          </div>

        </div>
        <div class="tw-flex tw-justify-center tw-flex-col md:tw-flex-row tw-flex-col tw-items-stretch tw-w-full tw-space-y-4 md:tw-space-y-0 md:tw-space-x-6 xl:tw-space-x-8">
          <div class="tw-flex tw-flex-col tw-px-4 tw-py-6 md:tw-p-6 xl:tw-p-8 tw-w-full tw-bg-gray-50 dark:tw-bg-gray-800 tw-space-y-6">
            <h3 class="tw-text-xl dark:tw-text-white tw-font-semibold tw-leading-5 tw-text-gray-800">Summary</h3>
            <div class="tw-flex tw-justify-center tw-items-center tw-w-full tw-space-y-4 tw-flex-col tw-border-gray-200 tw-border-b tw-pb-4">
              <div class="tw-flex tw-justify-between tw-w-full">
                <p class="tw-text-base dark:tw-text-white tw-leading-4 tw-text-gray-800">Subtotal</p>
                <p class="tw-text-base dark:tw-text-gray-300 tw-leading-4 tw-text-gray-600">$56.00</p>
              </div>
              <div class="tw-flex tw-justify-between tw-items-center tw-w-full">
                <p class="tw-text-base dark:tw-text-white tw-leading-4 tw-text-gray-800">Discount <span class="tw-bg-gray-200 tw-p-1 tw-text-xs tw-font-medium dark:tw-bg-white dark:tw-text-gray-800 tw-leading-3 tw-text-gray-800">STUDENT</span></p>
                <p class="tw-text-base dark:tw-text-gray-300 tw-leading-4 tw-text-gray-600">-$28.00 (50%)</p>
              </div>
              <div class="tw-flex tw-justify-between tw-items-center tw-w-full">
                <p class="tw-text-base dark:tw-text-white tw-leading-4 tw-text-gray-800">Shipping</p>
                <p class="tw-text-base dark:tw-text-gray-300 tw-leading-4 tw-text-gray-600">$8.00</p>
              </div>
            </div>
            <div class="tw-flex tw-justify-between tw-items-center tw-w-full">
              <p class="tw-text-base dark:tw-text-white tw-font-semibold tw-leading-4 tw-text-gray-800">Total</p>
              <p class="tw-text-base dark:tw-text-gray-300 tw-font-semibold tw-leading-4 tw-text-gray-600">$36.00</p>
            </div>
          </div>

        </div>
      </div>
      <div class="tw-bg-gray-50 dark:tw-bg-gray-800 tw-w-full xl:tw-w-96 tw-flex tw-justify-between tw-items-center md:tw-items-start tw-px-4 tw-py-6 md:tw-p-6 xl:tw-p-8 tw-flex-col">
        <h3 class="tw-text-xl dark:tw-text-white tw-font-semibold tw-leading-5 tw-text-gray-800">Customer</h3>
        <div class="tw-flex tw-flex-col md:tw-flex-row xl:tw-flex-col tw-justify-start tw-items-stretch tw-h-full tw-w-full md:tw-space-x-6 lg:tw-space-x-8 xl:tw-space-x-0">
          <div class="tw-flex tw-flex-col tw-justify-start tw-items-start tw-flex-shrink-0">
            <div class="tw-flex tw-justify-center tw-w-full md:tw-justify-start tw-items-center tw-space-x-4 tw-py-8 tw-border-b tw-border-gray-200">
              <img src="https://i.ibb.co/5TSg7f6/Rectangle-18.png" alt="avatar" />
              <div class="tw-flex tw-justify-start tw-items-start tw-flex-col tw-space-y-2">
                <p class="tw-text-base dark:tw-text-white tw-font-semibold tw-leading-4 tw-text-left tw-text-gray-800">David Kent</p>
                <p class="tw-text-sm dark:tw-text-gray-300 tw-leading-5 tw-text-gray-600">10 Previous Orders</p>
              </div>
            </div>

            <div class="tw-flex tw-justify-center tw-text-gray-800 dark:tw-text-white md:tw-justify-start tw-items-center tw-space-x-4 tw-py-4 tw-border-b tw-border-gray-200 tw-w-full">
              <img class="dark:tw-hidden" src="https://tuk-cdn.s3.amazonaws.com/can-uploader/order-summary-3-svg1.svg" alt="email">
              <img class="tw-hidden dark:tw-block" src="https://tuk-cdn.s3.amazonaws.com/can-uploader/order-summary-3-svg1dark.svg" alt="email">
              <p class="tw-cursor-pointer tw-text-sm tw-leading-5 ">david89@gmail.com</p>
            </div>
          </div>
          <div class="tw-flex tw-justify-between xl:tw-h-full tw-items-stretch tw-w-full tw-flex-col tw-mt-6 md:tw-mt-0">
            <div class="tw-flex tw-justify-center md:tw-justify-start xl:tw-flex-col tw-flex-col md:tw-space-x-6 lg:tw-space-x-8 xl:tw-space-x-0 tw-space-y-4 xl:tw-space-y-12 md:tw-space-y-0 md:tw-flex-row tw-items-center md:tw-items-start">
              <div class="tw-flex tw-justify-center md:tw-justify-start tw-items-center md:tw-items-start tw-flex-col tw-space-y-4 xl:tw-mt-8">
                <p class="tw-text-base dark:tw-text-white tw-font-semibold tw-leading-4 tw-text-center md:tw-text-left tw-text-gray-800">Shipping Address</p>
                <p class="tw-w-48 lg:tw-w-full dark:tw-text-gray-300 xl:tw-w-48 tw-text-center md:tw-text-left tw-text-sm tw-leading-5 tw-text-gray-600">180 North King Street, Northhampton MA 1060</p>
              </div>
              <div class="tw-flex tw-justify-center md:tw-justify-start tw-items-center md:tw-items-start tw-flex-col tw-space-y-4">
                <p class="tw-text-base dark:tw-text-white tw-font-semibold tw-leading-4 tw-text-center md:tw-text-left tw-text-gray-800">Billing Address</p>
                <p class="tw-w-48 lg:tw-w-full dark:tw-text-gray-300 xl:tw-w-48 tw-text-center md:tw-text-left tw-text-sm tw-leading-5 tw-text-gray-600">180 North King Street, Northhampton MA 1060</p>
              </div>
            </div>
            <div class="tw-flex tw-w-full tw-justify-center tw-items-center md:tw-justify-start md:tw-items-start">
              <button class="tw-mt-6 md:tw-mt-0 dark:tw-border-white dark:hover:tw-bg-gray-900 dark:tw-bg-transparent dark:tw-text-white tw-py-5 hover:tw-bg-gray-200 focus:tw-outline-none focus:tw-ring-2 focus:tw-ring-offset-2 focus:tw-ring-gray-800 tw-border tw-border-gray-800 tw-font-medium tw-w-96 2xl:tw-w-full tw-text-base tw-font-medium tw-leading-4 tw-text-gray-800">Edit Details</button>
            </div>
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
      OrderData:null
    }
  },

  async fetch(){
    try {
      const res = this.$repositories.getAOrder.getAOrder({
        orderId:this.$route.params.id
      })
      this.OrderData = res.data
      console.log(this.OrderData)
    }catch (e) {

    }
  },
}
</script>

<style scoped>

</style>
