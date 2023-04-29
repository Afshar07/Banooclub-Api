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
            <input  type="text" class="with-border FormInputs mt-1" placeholder="شماره ملی">
          </div>
          <div class="col-md-4 my-md-0 my-3">
            <small class="p-2">شماره همراه مستقیم *</small>
            <input  type="tel" class="with-border FormInputs mt-1" placeholder="شماره ملی">
          </div>
          <div class="col-md-4">
            <small class="p-2"> سابقه طبابت(به سال) *</small>
            <input  type="text" class="with-border FormInputs mt-1" placeholder="شماره ملی">
          </div>
          <div class="col-md-1 my-3">
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
      selectedCategoryId: 0
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
