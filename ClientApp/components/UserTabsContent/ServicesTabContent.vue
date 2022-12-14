<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="row px-2  py-3">
    <input
      class="SearchStyle"
      type="text"
      placeholder="جستجو خدمت"
      v-model="searchKey"
    />
    <div class="row mx-auto">
      <div @click="$router.push('/Products/AddProduct/')" class="row my-3" v-if="!$fetchState.pending && my_services && my_services.length === 0">
        <div class="col-12 text-warning fw-bold text-center tw-cursor-pointer">
          <img src="/girl2.jpg" class="tw-w-full tw-h-auto tw-rounded-xl" alt="">
        </div>
      </div>
      <div class="col-md-4 col-lg-3" v-for="(service,idx) in my_services" :key="idx">
        <ProductItem @GetServices="GetServices" @updateServiceDetails="updateServiceDetails" class="my-3"
                     :service_details="service" :show_buttons="true"/>
      </div>
    </div>

    <CustomPagination v-if="totalPages.length>1" :pages="totalPages" @PageChanged="changePage($event)"/>
  </div>

</template>

<script>
import ProductItem from "../../components/Products/ProductItem"
import CustomPagination from "../../components/utilities/CustomPagination"

export default {
  name: "ServicesTabContent",
  components: {ProductItem, CustomPagination},
  watch: {
    searchKey() {
      if (this.searchKey.length >= 3) {

        this.$fetch()
      } else if (this.searchKey.length === 0) {

        this.$fetch()
      }
    }
  },
  data() {
    return {
      my_services: null,
      pageNumber: 0,
      totalPages: [],
      searchKey: ''
    }
  },
  async fetch() {
    try {
      const services = await this.$repositories.getMyServices.getMyServices(
        {
          pageNumber: this.pageNumber,
          count: 12,
          searchCommand: this.searchKey
        }
      )
      this.my_services = services.data.services
      this.totalPages = []
      const result = Math.ceil(services.data.servicesCount / 12)
      for (let i = 1; i <= result; i++) {
        this.totalPages.push(i);
      }
    } catch (error) {
      console.log(error)
    }
  },
  methods: {
    async GetServices() {
      try {
        const services = await this.$repositories.getMyServices.getMyServices(
          {
            pageNumber: this.pageNumber,
            count: 12,
            searchCommand: this.searchKey
          }
        )
        this.my_services = services.data.services
        this.totalPages = []
        const result = Math.ceil(services.data.servicesCount / 12)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
        }
      } catch (error) {
        console.log(error)
      }
    },

    changePage(id) {
      this.pageNumber = id
      this.$fetch()
    },

    updateServiceDetails() {
      this.$fetch();
    }
  }
}
</script>

<style scoped>
.searchFriend {
  border: 1px solid #eaeaea;
  color: #575757;
  font-size: 14px;
  padding: 5px 10px;
  width: 100%;
  margin-bottom: 20px;
}

.card {
  /*width: 185px;*/
  background-color: white;
  border-radius: 8px;
  overflow: hidden;
  -webkit-box-shadow: 0 1px 3px 0 rgb(0 0 0 / 10%), 0 1px 2px 0 rgb(0 0 0 / 6%);
  box-shadow: 0 1px 3px 0 rgb(0 0 0 / 10%), 0 1px 2px 0 rgb(0 0 0 / 6%);
}
</style>
