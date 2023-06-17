<template>
  <div class="container NewBg  px-2">
    <div class="row  ">
      <div class="col-xl-3 col-lg-3 p-4  ">
        <div class="bg-white shadow row rounded-3 p-2">
          <div class="col-md-12 py-3 gap-2 border-bottom p-2 d-flex align-items-center justify-content-start">
            <LazyFilterIcon></LazyFilterIcon>
            <span>فیلتر ها </span>
          </div>
          <div class="col-md-12 py-3 gap-2 border-bottom p-2   ">
            <div class="d-flex align-items-center justify-content-between">
              <small>مشاوران فعال</small>
              <input type="checkbox" style="width: 15px;height: 15px" class="form-check ">
            </div>
          </div>
          <div class="col-md-12 py-3 gap-2 border-bottom p-2   ">
            <strong>جنسیت مشاوران</strong>
            <div class="d-flex align-items-center gap-3 my-2 justify-content-start">
              <div class="d-flex cursor-pointer flex-column gap-2 align-items-center justify-content-center">
                <div class="p-1 rounded border">
                  <img src="/man.png" style="object-fit: contain;width: 20px;height: 20px" alt="">
                </div>
                <small>آقا</small>
              </div>
              <div class="d-flex cursor-pointer flex-column gap-2 align-items-center justify-content-center">
                <div class="p-1 rounded border">
                  <img src="/woman.png" style="object-fit: contain;width: 20px;height: 20px" alt="">
                </div>
                <small>خانم</small>
              </div>
            </div>
          </div>
          <div class="col-md-12 py-3 gap-2  p-2   ">
            <strong>دسته بندی مشاوران</strong>
            <div class="   my-2 ">
              <v-select
                :options="categories"
                label="name"
                dir="rtl"
                placeholder="همه"
                v-model="selectedCategoryId"
                :reduce="(name) => name.value"

              ></v-select>
            </div>
          </div>
        </div>
      </div>
      <div class="col-xl-9 col-lg-9 p-4 " style="height: 1000px">
        <div class="bg-white shadow mb-3 row rounded p-2">
          <div class="d-flex flex-md-row flex-column align-items-center justify-content-start">
            <span>مرتب سازی بر اساس : </span>

            <ul class="nav nav-pills align-items-end profile_tabs py-3 mr-2" id="pills-tab" role="tablist">
              <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
                <button class="nav-link active p-2" id="products-pills-home-tab" data-bs-toggle="pill"
                        data-bs-target="#products-pills-home" type="button" role="tab"
                        aria-controls="products-pills-home"
                        aria-selected="true">
                  پیشفرض
                </button>
              </li>
              <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
                <button class="nav-link p-2" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile"
                        type="button" role="tab" aria-controls="pills-profile" aria-selected="false">
                  بالاترین تجربه
                </button>
              </li>
              <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
                <button class="nav-link p-2" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile"
                        type="button" role="tab" aria-controls="pills-profile" aria-selected="false">
                  بیشترین امتیاز
                </button>
              </li>
            </ul>
          </div>
        </div>

        <LazyConsultantCard v-for="i in 3"></LazyConsultantCard>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  layout: "PoshtebamPlusLayout",
  name: "consultants",
  data() {
    return {
      categories: [
        {
          name: 'همه',
          value: 0
        },
        {
          name: 'ساده',
          value: 1
        },
        {
          name: 'خاشخاشی',
          value: 2
        },
      ],
      selectedCategoryId: 0
    }
  },
  head(){
    return{
      title:'لیست مشاورین'
    }
  },
}
</script>

<style scoped>

</style>
