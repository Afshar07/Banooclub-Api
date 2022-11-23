<template>
  <div :class="{'loading-skeleton':$fetchState.pending}" class=" mcontainer tw-overflow-x-hidden p-3">
    <div class="row">
      <div class="col-12">
        <input
          class="SearchStyle "
          type="text"
          placeholder="جستجو"
          v-model="searchKey"
        />
        <div class="row">
          <LazySpinner v-if="$fetchState.pending"></LazySpinner>
          <div v-if="categories.length>0" class="col-xl-3 col-sm-12 col-md-4" v-for="(category,idx) in categories"
               :key="idx">
            <LazyCategoryItem class="m-2" :category_details="category"/>
          </div>
          <div v-else class="tw-flex tw-items-center tw-justify-center">
            <span class="text-secondary">داده ای برای نمایش وجود ندارد</span>
          </div>

          <div v-if="AllServices.length>0" class="col-xl-3 col-sm-12 col-md-4"
               v-for="service in AllServices.filter(e=> e.serviceCategoryId===parseInt($route.params.Categories))">
            <LazyProductItem @GetServices="GetAllServices()" class="my-3" :service_details="service"/>
          </div>
          <div v-else class="tw-flex tw-items-center tw-justify-center">
            <span class="text-secondary">داده ای برای نمایش وجود ندارد</span>
          </div>
          <!--          <CustomPagination v-if="totalPages.length>1"  :pages="totalPages" @PageChanged="changePage($event)"/>-->
        </div>
      </div>
    </div>
  </div>
</template>

<script>

export default {
  name: "Categories",
  layout: 'PoshtebamPlusLayout',
  data() {
    return {
      categories: [],
      AllServices: [],
      searchKey: '',
      SelectedPageId: 1
    }
  },

  watch: {
    searchKey: function (val) {
      if (!this.$route.params.Categories) {

        this.GetAllCategories()


      } else if (this.$route.params.Categories) {

        this.GetAllServices()


      }
    }
  },

  async fetch() {
    if (!this.$route.params.Categories) {
      await this.GetAllCategories()
    } else {
      await this.GetAllServices()
    }

  },

  methods: {
    async GetAllServices() {
      try {
        const res = await this.$repositories.getAllServices.getAllServices(
          {
            pageNumber: this.SelectedPageId,
            count: 0,
            searchCommand: this.searchKey,
            status: 1,

          }
        )
        this.categories = []
        this.AllServices = res.data.services
        this.totalPages = []
        const result = Math.ceil(res.data.servicesCount / 1)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
        }

      } catch (error) {
        console.log(error)
      }
    },
    async GetAllCategories() {
      try {
        const allCategories = await this.$repositories.getAllServicesCategory.getAllServicesCategory({
          searchCommand: this.searchKey
        });
        this.AllServices = []
        this.categories = allCategories.data.serviceCategories;
      } catch (error) {
        console.log(error)
      }

    }
  }
}
</script>

<style scoped>

</style>
