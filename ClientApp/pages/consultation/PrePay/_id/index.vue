<template>
  <div class="mcontainer  NewBg position-relative  px-2">
    <div class="row ">
      <div style="background-color: #f6e2ff;border:solid 1px #e7b0fe" class="col-md-12  my-3 rounded shadow p-3">
        <div class="w-100 d-flex align-items-center gap-3">
          <img src="/defaultUser.png" width="60" height="60" style="object-fit: contain" alt="">
          <div class="d-flex flex-column ">
            <strong>دکتر دایی</strong>
            <small class="text-muted">متخصص دایی شدن</small>
          </div>
        </div>
        <div class="w-100 p-3">
          <p>
            سلام من دکتر دایی هستم.

            شما مشاوره متنی را انتخاب کردید.

            بعد از وارد کردن “علت مراجعه” و نوشتن علائم بیماری خود و پرداخت هزینه، در کمتر از 10 ساعت آینده به شما پیام
            خواهم داد و تا پاسخگویی کامل به شما همراهتان خواهم بود.
          </p>
        </div>
      </div>
      <div class="col-md-12 bg-white  my-3 rounded shadow p-3">
        <div class="w-100 d-flex align-items-center gap-3">
          <strong class="">علت مشاوره</strong>
        </div>
        <div class="w-100 mt-2">
          <small class="text-muted">توضیحات</small>
          <textarea class="form-control mt-2" placeholder="لطفا شرح حال خود را بیان کنید"></textarea>
        </div>
      </div>
      <div class="col-md-12 bg-white  my-3 rounded shadow p-3">
        <div class="w-100 d-flex align-items-center gap-3">
          <strong class="">انتخاب زمان</strong>
        </div>
        <div class="w-100 p-2 border-bottom d-flex align-items-center justify-content-between">
          <small class="text-pink">مشاوره پرداخت</small>
          <small class="text-pink">119000 تومان</small>

        </div>
        <div style="background-color: #fcf5ff" class="w-100 p-2 my-2 rounded  d-flex align-items-center justify-content-between">
          <small class="text-pink">مبلغ قابل پرداخت</small>
          <small class="text-pink">119000 تومان</small>

        </div>

      </div>
      <div class="col-md-12 bg-white  my-3 rounded shadow p-3">
        <div class="w-100 d-flex align-items-center gap-3">
          <strong class=""> کد تخفیف</strong>
        </div>
        <div class="w-100 mt-2">
          <small class="text-muted">کد تخفیف را وارد کنید</small>
          <div class=" d-flex my-2 gap-2">
            <input  type="text" class="with-border FormInputs" placeholder="کد تخفیف را وارد کنید">
            <button type="button" class=" text-white rounded p-2 cursor-pointer" style="background-color:#d277ff ">
              ثبت
            </button>
          </div>
        </div>
      </div>
      <div class="col-md-12 bg-white  my-3 rounded shadow p-3">
        <div class="w-100 d-flex align-items-center gap-3">
          <strong class="">اطلاعات پرداخت</strong>
        </div>
        <div class="w-100 p-2 border-bottom d-flex align-items-center justify-content-between">
        <small class="text-pink">مشاوره پرداخت</small>
          <small class="text-pink">119000 تومان</small>

        </div>
        <div style="background-color: #fcf5ff" class="w-100 p-2 my-2 rounded  d-flex align-items-center justify-content-between">
          <small class="text-pink">مبلغ قابل پرداخت</small>
          <small class="text-pink">119000 تومان</small>

        </div>

      </div>
      <div class="w-100 px-0 my-3">
        <button type="button" class="btn w-100 text-white " style="background-color:#e7b0fe ">
          پرداخت
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  layout: 'PoshtebamPlusLayout',
  name: "index",
  head(){
    return{
      title:'پرداخت'
    }
  },
  data() {
    return {}
  }
}
</script>

<style scoped>

</style>
