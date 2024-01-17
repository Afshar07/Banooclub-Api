<template>
  <div class="container mcontainer containerBox  ">
    <div class="container-fluid  ">
      <div class="container">
        <div class="row  py-5 d-flex justify-content-center  ">
          <div class="col-md-4 d-flex justify-content-center">
            <h2><strong>پرداخت</strong></h2>
          </div>
        </div>
      </div>
    </div>
    <div class="container   py-5 mx-auto">
      <div class="row">
        <div class="col-md-12">
          <div v-if="ComputedStatus===1" class="d-flex align-items-center flex-column justify-content-center">
            <i class="fas fa-check text-success" style="font-size: 70px"></i>
            <strong class="my-4" style="font-size: 40px">پرداخت موفقیت آمیز</strong>
            <span class="text-secondary" style="font-size: 20px">{{ ComputedMessage }}</span>
            <nuxt-link class="btn bg-success text-white  my-4" to="/Payments/">ادامه</nuxt-link>
          </div>
          <div v-if="ComputedStatus===2" class="d-flex align-items-center flex-column justify-content-center">
            <i class="fas fa-exclamation-circle text-danger" style="font-size: 70px"></i>
            <strong class="my-4" style="font-size: 40px">پرداخت ناموفق</strong>
            <span class="text-secondary" style="font-size: 20px">{{ ComputedMessage }}</span>
            <nuxt-link class="btn bg-danger text-white  my-4" to="/Payments/">ادامه</nuxt-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  auth: false,
  layout: 'PoshtebamPlusLayout',
  head() {
    return {
      title: 'پرداخت',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'پرداخت',
        },
      ],
    }
  },
  name: "ResponseVerify",
  data() {
    return {
      Message: '',
      // status : 1 => Successful , status : 2 => unSuccessful
      Status: 0
    }
  },
  mounted() {
    this.ChangePaymentStatus()
  },
  watch: {
    '$route.query': function (val, oldVal) {
      this.ChangePaymentStatus();
    }
  },
  computed: {
    ComputedMessage() {
      return this.Message
    },
    ComputedStatus() {
      return this.Status
    }
  },
  methods: {
    async GetCredit() {
      try {
        const res = await this.$repositories.GetCredit.GetCredit()
        this.$store.commit('SetWalletAmount', res.data.data)
      } catch (e) {
        console.log(e)
      }
    },
    async ChangePaymentStatus() {

      try {
        this.$nextTick(() => {
          this.$nuxt.$loading.start()
        })
        const res = await this.$repositories.ChangePaymentStatus.ChangePaymentStatus({
          orderId: this.$route.query.order_id,
          transId: this.$route.query.trans_id

        })

        switch (res.data) {
          case 0: {
            this.Status = 1
            this.Message = 'سفارش شما ثبت و پرداخت شما با موفقیت انجام شد.سفارش شما در حال پردازش است.'
            await this.GetCredit();
            break;
          }
          case -1: {
            this.Status = 2
            this.Message = 'خطا در پرداخت.لطفا با پشتیبانی تماس حاصل بفرمایید'
            await this.GetCredit();
            break;
          }
          case -99: {
            this.Status = 2
            this.Message = 'نوبت شما رزرو شده است. در صورت کسر مبلغ از حساب شما تا حداکثر 72 ساعت دیگر عودت داده میشود.'
            await this.GetCredit();
            break;
          }
          case -2 : {
            this.Status = 2
            this.Message = 'پرداخت شما توسط بانک رد شده است'
            break;
          }
          case -4: {
            this.Status = 2
            this.Message = 'پرداخت شما لغو شده است'
            break;
          }
          case -25 : {
            this.Status = 2
            this.Message = 'این تراکنش قبلا انجام شده است'
            break;
          }
          case -30 : {
            this.Status = 2
            this.Message = 'مبلغ مورد نظر کمتر از مقدار پرداختی است'
            break;
          }
          case -8 : {
            this.Status = 1
            this.Message = 'سفارش شما ثبت و پرداخت شما با موفقیت انجام شد.سفارش شما در حال پردازش است.'
            break;
          }
          case -42: {
            this.Status = 2
            this.Message = 'سیستم پرداخت دچار مشکل شده است'
            break;
          }
          case -45: {
            this.Status = 2
            this.Message = 'سیستم پرداخت غیر فعال است'
            break;
          }
          case -49 : {
            this.Status = 2
            this.Message = 'تراکنش مورد نظر تکراریست'
            break;
          }
        }
        this.$nuxt.$loading.finish();
        this.$nuxt.loading = false;
      } catch (e) {
        console.log(e)
        this.$nuxt.$loading.finish();
        this.$nuxt.loading = false;

      } finally {
        this.$nuxt.$loading.finish();
        this.$nuxt.loading = false;
      }
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
</style>
