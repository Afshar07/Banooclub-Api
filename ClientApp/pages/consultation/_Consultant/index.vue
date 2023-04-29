<template>
  <div class="container NewBg position-relative  px-2">
    <div class="row ">


      <div class="col-md-9 p-4">
        <div class="bg-white shadow row rounded-3 ">
          <div class="rounded-3" style="background-color: #e7b0fe;height: 7rem"></div>
          <div class="row p-3">
            <div class="col-md-6 ">
              <div class="d-flex gap-3 h-100 align-items-start">
                <div
                  class="rounded-circle position-relative  d-flex flex-column justify-content-start align-items-center"
                  style="height: 100%;bottom: 5rem">
                  <img src="/defaultUser.png" alt="" style="object-fit: contain;width: 7rem;height: 7rem">
                </div>
                <div class="d-flex flex-column h-100 justify-content-between align-items-start">
                  <div class="d-flex flex-column justify-content-start align-items-start">
                    <h5 class="tw-font-bold">دکتر دایی</h5>
                    <span class="text-muted">متخصص دایی شدن</span>
                  </div>
                  <div class="d-flex gap-3 my-3 align-items-center flex-wrap">
                  <span class=" py-1 px-2 rounded d-flex gap-2 align-items-center " style="background-color: #ffdb66">
                  <LazyStarIcon style="height: 15px;width: 15px;fill: white"></LazyStarIcon>
                    <small class="text-white">4.6</small>
                  </span>
                    <span class=" py-1 px-2 rounded d-flex gap-2 text-muted align-items-center " >
                  <LazyCommentIcon style="height: 15px;width: 15px"></LazyCommentIcon>
                    <small>123 نظر</small>
                  </span>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-md-6">
              <div class="d-flex flex-column justify-content-between h-100 ">
                <div class="d-flex flex-column align-items-start gap-2">
                  <div class="d-flex gap-1">
                    <small class="text-muted">شماره نظام پزشکی : </small>
                    <small>86014</small>
                  </div>
                  <div class="d-flex gap-1">
                    <small class="text-muted">تجربه : </small>
                    <small>22 سال</small>
                  </div>
                  <div class="d-flex gap-1">
                    <small class="text-muted">مشاوره موفق : </small>
                    <small>213456</small>
                  </div>
                </div>

              </div>
            </div>
          </div>
        </div>
        <div class="row ">
          <div class="col-md-6  p-3">
            <div class="bg-white shadow row rounded-3 py-3" style="min-height: 15rem">
              <div class="w-100" style="border-right: solid 3px #e7b0fe">
                <strong>درباره من</strong>
              </div>
              <div class="mt-2">
                <p>لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است، چاپگرها
                  و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است، و برای شرایط فعلی تکنولوژی مورد نیاز، و
                  کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد، کتابهای زیادی در شصت و سه درصد گذشته حال و
                  آینده، شناخت فراوان جامعه و متخصصان را می طلبد، تا با نرم افزارهاید داشوجود طراحی اساسا مورد استفاده
                  قرار گیرد.</p>
              </div>

            </div>
          </div>
          <div class="col-md-6 p-3">
            <div class="bg-white shadow row rounded-3 py-3" style="min-height: 15rem">
              <div class="w-100 d-flex h-100 " style="border-right: solid 3px #e7b0fe">
                <strong>ویدیو معرفی من</strong>
              </div>
              <div class="mt-2 d-flex align-items-center justify-content-center">
                <div class="d-flex flex-column gap-2 align-items-center justify-content-center">
                  <img src="/video-camera.svg" alt="">
                  <small class="text-muted">در حال آماده سازی</small>
                </div>
              </div>
            </div>
          </div>

        </div>
        <div class="bg-white shadow row rounded-3 py-3">
          <div class="w-100 d-flex h-100 " style="border-right: solid 3px #e7b0fe">
            <strong>نظرات کاربران</strong>
          </div>
          <div class=" row">
            <div v-for="i in 6" class="col-md-12 my-3">
              <div class="d-flex align-items-center justify-content-between">
                <div class="d-flex align-items-center gap-3">
                  <div
                    class="rounded-circle   d-flex flex-column justify-content-start align-items-center"
                    style="height: 100%;">
                    <img src="/defaultUser.png" alt="" style="object-fit: contain;width: 4rem;height: 4rem">
                  </div>
                  <div class="d-flex flex-column align-items-start">
                    <span>امیرحسین سلیمانی</span>
                    <div class="d-flex gap-2 align-items-center">
                      <small class=" text-muted">1402/02/05</small>
                      <small class=" text-muted ps-2">19:49</small>
                    </div>
                  </div>
                </div>
                <span class=" py-1 px-2 rounded d-flex gap-2 align-items-center " style="background-color: #ffdb66">
                  <LazyStarIcon style="height: 15px;width: 15px;fill:white"></LazyStarIcon>
                    <small class="text-white">4.6</small>
                  </span>
              </div>
              <div class="px-2 mt-3">
                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Autem blanditiis dolore eum explicabo facere nemo odio, rem sed temporibus ut.</p>
              </div>
            </div>
            <div class="col-md-12 d-flex align-items-center justify-content-center">
              <button type="button" class="p-2 " style="border-radius: 30px;border:solid 1px black">مشاهده نظرات بیشتر</button>

            </div>
          </div>
        </div>
      </div>
      <div class="col-md-3 p-4   overflow-scroll ">
        <div class="bg-white shadow sticky-lg-top   row rounded-3 p-2" style="z-index: 11">
          <div v-for="i in 3" class="col-md-12 cursor-pointer  gap-2  p-2   ">
            <div style="border:solid 1px #e7b0fe;background-color: #faf7ff" class="rounded  p-3 ">
              <div class=" d-flex align-items-center justify-content-between">
                <div class="d-flex align-items-center gap-2">
                  <div class="p-1 rounded " style="border:solid 1px #e7b0fe;background-color: #e7b0fe">
                    <LazyPhoneIcon style="width: 20px;height: 20px;fill:#d071ff "></LazyPhoneIcon>
                  </div>
                  <small>مشاوره تلفنی فوری</small>
                </div>
                <span style="color: #e7b0fe">12000 تومان</span>
              </div>
              <div class="d-flex gap-2 flex-column mt-3 pe-5">
              <span class="text-muted d-flex align-items-center gap-1">
                <LazyClockIcon style="width: 15px;height: 15px;fill: #6c757d!important"></LazyClockIcon>
                <small style="font-size: 12px">
                پاسخ دهی فوری
                </small>
              </span>
                <span class="text-muted d-flex align-items-center gap-1">
                <LazyClockIcon style="width: 15px;height: 15px;fill: #6c757d!important"></LazyClockIcon>
                <small style="font-size: 12px">
                مدت زمان گفتگو 15 دقیقه
                </small>
              </span>
              </div>
            </div>
          </div>
          <div class="col-md-12 px-2">
            <button type="button" class="btn w-100 text-white " style="background-color:#e7b0fe ">
              ادامه و پرداخت
            </button>
          </div>


        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  layout: "PoshtebamPlusLayout",
  head(){
    return{
      title:'مشاور'
    }
  },
  name: "index"
}
</script>

<style scoped>

</style>
