<template>
  <div class="mcontainer NewBg position-relative  px-2">
    <div class="row">
      <div class="col-md-12">
        <h1 class="h4">مشاورین من</h1>
      </div>
      <div class="col-md-12 my-3 ">
        <nuxt-link  to="/"  class="bg-white text-black cursor-pointer shadow row rounded p-md-4 p-2 mb-3   row" style="min-height: 10rem;text-decoration: none">
          <div class="col-md-6 ">
            <div class="d-flex gap-3 h-100 align-items-start">
              <div class="rounded-circle  d-flex flex-column justify-content-center align-items-center"
                   style="height: 100%">
                <img src="/defaultUser.png" alt="" style="object-fit: contain;width: 5rem;height: 5rem">
              </div>
              <div class="d-flex flex-column h-100 justify-content-center align-items-start">
                <div class="d-flex flex-column justify-content-start align-items-start">
                  <h5 class="tw-font-bold">دکتر دایی</h5>
                  <span class="text-muted">متخصص دایی شدن</span>
                </div>

              </div>
            </div>
          </div>
          <div class="col-md-6 my-md-0 my-3">
            <div class="d-flex flex-column justify-content-center h-100 ">
              <div class="d-flex flex-column align-items-start gap-2">
                <div class="d-flex gap-1">
                  <small class="text-muted">وضعیت پرداخت : </small>
                  <small>پرداخت شده</small>
                </div>
                <div class="d-flex gap-1">
                  <small class="text-muted">وضعیت مشاوره : </small>
                  <small>در انتظار</small>
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-12 mt-3 d-flex justify-content-end align-items-center">
            <span class="d-flex align-items-center align-self-end gap-2">
                <small style="color: #00c395">دریافت مشاوره</small>
                <LazyArrowLeftIcon style="width: 20px;height: 20px;fill: #00c395"></LazyArrowLeftIcon>
                </span>
          </div>
        </nuxt-link>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  layout: "PoshtebamPlusLayout",
  name: "MyConsultations",
  data(){
    return{

    }
  },
  head(){
    return{
      title:'مشاورین من'
    }
  },
}
</script>

<style scoped>

</style>
