<template>
  <div class="container NewBg position-relative  px-2">
    <div class="row">
      <div class="col-md-12">
        <h1 class="h4">ثبت نام مشاور</h1>
      </div>
      <div class="col-md-12 my-3 bg-white rounded shadow">
        <div class="row   p-3">
          <div class="col-md-4">
            <small class="p-2">نام و نام خانوادگی*</small>
            <input  type="text" class="with-border FormInputs mt-1" placeholder="نام و نام خانوادگی">
          </div>
          <div class="col-md-4 my-md-0 my-3">
            <small class="p-2">شماره نظام پزشکی *</small>
            <input  type="tel" class="with-border FormInputs mt-1" placeholder="شماره نظام پزشکی">
          </div>
          <div class="col-md-4">
            <small class="p-2">شماره ملی *</small>
            <input  type="tel" class="with-border FormInputs mt-1" placeholder="شماره ملی">
          </div>
        </div>
        <div class="row px-3">
          <div class="col-md-12 ">
            <h6 >تخصص *</h6>
          </div>
          <div class="col-md-6 my-md-0 my-3">
              <v-select
                :options="categories"
                label="name"
                dir="rtl"
                placeholder="همه"
                v-model="selectedCategoryId"
                :reduce="(name) => name.value"

              ></v-select>
          </div>
          <div class="col-md-6">
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
        <div class="row p-3">
          <div class="col-md-4">
            <small class="p-2">استان *</small>
            <input  type="text" class="with-border FormInputs mt-1" placeholder="استان">
          </div>
          <div class="col-md-4 my-md-0 my-3">
            <small class="p-2">شماره همراه مستقیم *</small>
            <input  type="tel" class="with-border FormInputs mt-1" placeholder="شماره همراه مستقیم">
          </div>
          <div class="col-md-4">
            <small class="p-2"> سابقه طبابت(به سال) *</small>
            <input  type="text" class="with-border FormInputs mt-1" placeholder="سابقه طبابت(به سال)">
          </div>

        </div>
        <div class="row p-3">
          <div class="col-md-12 border-bottom pb-2 mb-2">
            <strong>اطلاعات بانکی</strong>
          </div>
          <div class="col-md-6">
            <small class="p-2">شماره شبا</small>
            <input  type="text" class="with-border FormInputs mt-1" placeholder="شماره شبا">
          </div>
          <div class="col-md-4 my-md-0 my-3">
            <small class="p-2">نام بانک</small>
            <input  type="tel" class="with-border FormInputs mt-1" placeholder="نام بانک">
          </div>
          <div class="col-md-5 mt-2">
            <small class="p-2">شماره کارت</small>
            <input  type="text" class="with-border FormInputs mt-1" placeholder="شماره کارت">
          </div>

        </div>
        <div class="row p-3">
          <div class="col-md-12 border-bottom pb-2 mb-2">
            <strong>تعرفه مشاوره</strong>
          </div>
          <div class="row mb-2">
            <div class="col-md-12  d-flex align-items-center gap-2   my-2">
              <LazyPhoneIcon style="width: 20px;height: 20px;fill: #b44aff "></LazyPhoneIcon>
              <span>مشاوره تلفنی</span>
            </div>
            <div class="col-md-6 ">
              <small class="p-2">توضیحات کوتاه</small>
              <input v-model="description"  type="text" class="with-border FormInputs mt-1" maxlength="100" placeholder="توضیحات کوتاه">
              <span :class="{'text-success':description.length===160}"><small>{{description.length}}</small><small class="text-secondary">/</small><small class="text-secondary">100</small></span>
            </div>
            <div class="col-md-4 my-md-0 my-3">
              <small class="p-2">زمان</small>
              <input  type="tel" class="with-border FormInputs mt-1" placeholder="30 دقیقه">
            </div>
            <div class="col-md-5 mt-2">
              <small class="p-2">قیمت (تومان)</small>
              <input  type="number" class="with-border FormInputs mt-1"  placeholder="120000">
            </div>
          </div>
          <div class="row mb-2">
            <div class="col-md-12  d-flex align-items-center gap-2  my-2">
              <LazySmartPhoneIcon style="width: 20px;height: 20px;fill: #fc5399 " ></LazySmartPhoneIcon>
              <span>مشاوره تلفنی فوری</span>
            </div>
            <div class="col-md-6 ">
              <small class="p-2">توضیحات کوتاه</small>
              <input v-model="description"  type="text" class="with-border FormInputs mt-1" maxlength="100" placeholder="توضیحات کوتاه">
              <span :class="{'text-success':description.length===160}"><small>{{description.length}}</small><small class="text-secondary">/</small><small class="text-secondary">100</small></span>
            </div>
            <div class="col-md-4 my-md-0 my-3">
              <small class="p-2">زمان</small>
              <input  type="tel" class="with-border FormInputs mt-1" placeholder="30 دقیقه">
            </div>
            <div class="col-md-5 mt-2">
              <small class="p-2">قیمت (تومان)</small>
              <input  type="number" class="with-border FormInputs mt-1"  placeholder="120000">
            </div>
          </div>
          <div class="row mb-2">
          <div class="col-md-12  d-flex align-items-center gap-2  my-2">
            <LazyPictureIcon style="width: 20px;height: 20px"></LazyPictureIcon>
            <span>مشاوره ویدیویی</span>
          </div>
          <div class="col-md-6 ">
            <small class="p-2">توضیحات کوتاه</small>
            <input v-model="description"  type="text" class="with-border FormInputs mt-1" maxlength="100" placeholder="توضیحات کوتاه">
            <span :class="{'text-success':description.length===160}"><small>{{description.length}}</small><small class="text-secondary">/</small><small class="text-secondary">100</small></span>
          </div>
          <div class="col-md-4 my-md-0 my-3">
            <small class="p-2">زمان</small>
            <input  type="tel" class="with-border FormInputs mt-1" placeholder="30 دقیقه">
          </div>
          <div class="col-md-5 mt-2">
            <small class="p-2">قیمت (تومان)</small>
            <input  type="number" class="with-border FormInputs mt-1"  placeholder="120000">
          </div>
        </div>

        </div>
        <div class="row p-3">
          <div class="col-md-2 ">
            <button type="button" class="btn w-100 text-white " style="background-color:#e7b0fe ">
              ثبت
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
  name: "BecomeConsultant",
  data(){
    return{
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
      selectedCategoryId: 0,
      description:''
    }
  },
  head(){
    return{
      title:'مشاور شوید'
    }
  },
}
</script>

<style scoped>

</style>
