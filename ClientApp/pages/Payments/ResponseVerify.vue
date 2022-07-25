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
            <strong style="font-size: 40px" class="my-4">پرداخت موفقیت آمیز</strong>
            <span style="font-size: 20px" class="text-secondary">{{ ComputedMessage }}</span>
            <nuxt-link to="/Payments/" class="btn bg-success text-white  my-4">ادامه</nuxt-link>
          </div>
          <div  v-if="ComputedStatus===2" class="d-flex align-items-center flex-column justify-content-center">
            <i class="fas fa-exclamation-circle text-danger" style="font-size: 70px"></i>
            <strong style="font-size: 40px" class="my-4">پرداخت ناموفق</strong>
            <span style="font-size: 20px" class="text-secondary">{{ ComputedMessage }}</span>
            <nuxt-link to="/Payments/" class="btn bg-danger text-white  my-4">ادامه</nuxt-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  auth:false,
  layout:'PoshtebamPlusLayout',
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
  data(){
    return{
      Message:''  ,
      // status : 1 => Successful , status : 2 => unSuccessful
      Status:0
    }
  },
  mounted(){
    this.ChangePaymentStatus()
  },
  watch:{
    '$route.query':function (val,oldVal){
      this.ChangePaymentStatus();
    }
  },
  computed:{
    ComputedMessage(){
      return this.Message
    },
    ComputedStatus(){
      return this.Status
    }
  },
  methods:{
   async ChangePaymentStatus(){

      try {
        this.$nextTick(()=>{
          this.$nuxt.$loading.start()
        })
        const res = await this.$repositories.ChangePaymentStatus.ChangePaymentStatus({
          orderId:this.$route.query.order_id,
          transId:this.$route.query.trans_id

        })

        switch (res.data){
          case 0:{
            this.Status = 1
            this.Message = 'سفارش شما ثبت و پرداخت شما با موفقیت انجام شد.سفارش شما در حال پردازش است.'
            break;
          }
          case -2 :{
            this.Status = 2
            this.Message = 'پرداخت شما توسط بانک رد شده است'
            break;
          }
          case -4:{
            this.Status = 2
            this.Message = 'پرداخت شما لغو شده است'
            break;
          }
          case -25 :{
            this.Status = 2
            this.Message = 'این تراکنش قبلا انجام شده است'
            break;
          }
          case -30 :{
            this.Status = 2
            this.Message = 'مبلغ مورد نظر کمتر از مقدار پرداختی است'
            break;
          }
          case -8 :{
            this.Status = 1
            this.Message = 'سفارش شما ثبت و پرداخت شما با موفقیت انجام شد.سفارش شما در حال پردازش است.'
            break;
          }
          case -42:{
            this.Status = 2
            this.Message = 'سیستم پرداخت دچار مشکل شده است'
            break;
          }
          case -45:{
            this.Status = 2
            this.Message = 'سیستم پرداخت غیر فعال است'
            break;
          }
          case -49 :{
            this.Status = 2
            this.Message = 'تراکنش مورد نظر تکراریست'
            break;
          }
        }
        this.$nuxt.$loading.finish();
        this.$nuxt.loading= false;
      }catch (e) {
        console.log(e)
        this.$nuxt.$loading.finish();
        this.$nuxt.loading= false;

      }finally {
        this.$nuxt.$loading.finish();
        this.$nuxt.loading= false;
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
