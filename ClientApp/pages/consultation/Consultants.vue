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
              <input v-model="isActive" class="form-check " style="width: 15px;height: 15px" type="checkbox">
            </div>
          </div>
          <!--          <div class="col-md-12 py-3 gap-2 border-bottom p-2   ">-->
          <!--            <strong>جنسیت مشاوران</strong>-->
          <!--            <div class="d-flex align-items-center gap-3 my-2 justify-content-start">-->
          <!--              <div class="d-flex cursor-pointer flex-column gap-2 align-items-center justify-content-center">-->
          <!--                <div class="p-1 rounded border">-->
          <!--                  <img src="/man.png" style="object-fit: contain;width: 20px;height: 20px" alt="">-->
          <!--                </div>-->
          <!--                <small>آقا</small>-->
          <!--              </div>-->
          <!--              <div class="d-flex cursor-pointer flex-column gap-2 align-items-center justify-content-center">-->
          <!--                <div class="p-1 rounded border">-->
          <!--                  <img src="/woman.png" style="object-fit: contain;width: 20px;height: 20px" alt="">-->
          <!--                </div>-->
          <!--                <small>خانم</small>-->
          <!--              </div>-->
          <!--            </div>-->
          <!--          </div>-->
          <div class="col-md-12 py-3 gap-2  p-2   ">
            <strong>دسته بندی مشاوران</strong>
            <div class="   my-2 ">
              <v-select
                v-model="catIds"
                :options="categories"
                :reduce="(title) => title.id"
                dir="rtl"
                label="title"
                placeholder="همه"

              ></v-select>
            </div>
          </div>
        </div>
      </div>
      <div class="col-xl-9 col-lg-9 p-4 " style="height: 1000px">
        <div class="bg-white shadow mb-3 row rounded p-2">
          <div class="d-flex flex-md-row flex-column align-items-center justify-content-start">
            <span>مرتب سازی بر اساس : </span>

            <ul id="pills-tab" class="nav nav-pills align-items-end profile_tabs py-3 mr-2" role="tablist">
              <li class="nav-item" role="presentation m-0" style="margin: 0 !important;" @click="sort =0">
                <button id="products-pills-home-tab" aria-controls="products-pills-home" aria-selected="true"
                        class="nav-link active p-2" data-bs-target="#products-pills-home" data-bs-toggle="pill"
                        role="tab"
                        type="button">
                  پیشفرض
                </button>
              </li>
              <!--              <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">-->
              <!--                <button class="nav-link p-2" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile"-->
              <!--                        type="button" role="tab" aria-controls="pills-profile" aria-selected="false">-->
              <!--                  بالاترین تجربه-->
              <!--                </button>-->
              <!--              </li>-->
              <li class="nav-item" role="presentation m-0" style="margin: 0 !important;" @click="sort =1">
                <button id="pills-profile-tab" aria-controls="pills-profile" aria-selected="false"
                        class="nav-link p-2"
                        data-bs-target="#pills-profile" data-bs-toggle="pill" role="tab" type="button">
                  بیشترین امتیاز
                </button>
              </li>
            </ul>
          </div>
          <input v-model="debouncer" class="with-border FormInputs" placeholder="جستجو" type="text">
        </div>
        <template v-if="!isRequesting">
          <template v-if="consultants && consultants.length>0">
            <LazyConsultantCard v-for="(item,idx) in consultants" :key="idx"
                                :consultantInfo="item"></LazyConsultantCard>
          </template>
          <div v-else
               class="d-flex flex-column w-100 align-items-center bg-white justify-content-center shadow rounded p-3 ">
            <LazySadFaceIcon style="width: 60px;height: 60px;fill: #6c757d"></LazySadFaceIcon>
            <span class="text-secondary">موردی یافت نشد</span>
          </div>
        </template>
        <div v-else>
            <LazySpinner></LazySpinner>
        </div>
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
      categories: [],
      catIds: [],
      pageNumber: 1,
      take: 10,
      isActive: false,
      searchInput: '',
      sort: 0,
      timeout: false,
      consultants: null,
      isRequesting:false
    }
  },
  head() {
    return {
      title: 'لیست مشاورین'
    }
  },


  async fetch() {
    await Promise.all([
      this.getAllConsultants(),
      this.getAllConsultantCategories()
    ])
  },

  watch: {
    sort: function (val) {
      this.getAllConsultants()
    },
    catIds: function (val) {
      this.getAllConsultants()
    },
    isActive: function (val) {
      this.getAllConsultants()
    },
    searchInput: function () {
      this.getAllConsultants()
    },

  },
  computed: {
    debouncer: {
      get() {
        return this.searchInput
      },
      set(val) {
        if (this.timeout) {
          clearTimeout(this.timeout)
        }
        this.timeout = setTimeout(() => {
          this.searchInput = val
        }, 600)
      }
    }
  },
  methods: {
    async getAllConsultants() {
      try {
        this.isRequesting = true
        const res = await this.$repositories.getAllConsultants.setParams({
          sort: this.sort !== 0 ? this.sort : '',
          catIds: this.catIds,
          searchInput: this.searchInput,
          isActive: this.isActive,
          pageNumber: this.pageNumber,
          take: 10
        })
        this.consultants = res.data
      } catch (e) {
        console.log(e)
      }finally {
        this.isRequesting = false

      }
    },
    async getAllConsultantCategories() {
      try {
        const res = await this.$repositories.getAllConsultCategory.setTag()
        this.categories = res.data.data
      } catch (e) {

      }
    }

  }
}
</script>

<style scoped>

</style>
