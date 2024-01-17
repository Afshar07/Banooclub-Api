<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class=" mcontainer NewBg px-2">
    <div class="tw-w-full bg-white tw-shadow  p-3 d-flex align-items-center justify-content-between gap-3  rounded ">
      <div class="d-flex align-items-center gap-2">
        <img src="/girl-icon-khadamat.png" class="tw-w-[7rem] tw-h-[7rem] tw-object-contain" alt="">
        <div class="d-flex align-items-center flex-column">
          <h1 class="text-purple h6" style="font-weight: bolder!important;"> خدمات من</h1>
          <strong class="text-pink">My Services</strong>
        </div>
      </div>
    </div>
    <div class="row bg-white rounded shadow my-3 p-3">
      <div class="col-md-4 col-lg-3" v-for="(service,idx) in my_services" :key="idx">
        <LazyProductItem @GetServices="GetMyServices" @updateServiceDetails="updateServiceDetails" class="my-3" :service_details="service" :show_buttons="true"/>
      </div>
      <div class="col-md-12">
        <LazyCustomPagination v-if="totalPages.length>1" :activePage="SelectedPageId" :totalPages="totalPages" @PageChanged="changePage($event)"/>
      </div>
    </div>
  </div>



</template>

<script>

export default {
  name: "MyServices",
  layout: "PoshtebamPlusLayout",

  head() {
    return {
      title: 'خدمات من',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'خدمات من',
        },
      ],
    }

  },
  data(){
    return{
      my_services:null,
      SelectedPageId:1,
      totalPages:[],

    }
  },
  async fetch(){
    try {
      const services = await this.$repositories.getMyServices.getMyServices({pageNumber:this.SelectedPageId, count:10, searchCommand:null})
      this.my_services = services.data.services
      this.totalPages = []
      const result = Math.ceil(services.data.servicesCount / 10)
      for (let i = 1; i <= result; i++) {
        this.totalPages.push(i);
      }
    }
    catch (error){
      console.log(error)
    }
  },
  methods:{
    changePage(id){
      this.SelectedPageId = id
      this.GetMyServices();
    },
   async GetMyServices(){
      try {
        const services = await this.$repositories.getMyServices.getMyServices({pageNumber:this.SelectedPageId, count:10, searchCommand:null})
        this.my_services = services.data.services
        this.totalPages = []
        const result = Math.ceil(services.data.servicesCount / 10)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
        }
      }
      catch (error){
        console.log(error)
      }
    },
    updateServiceDetails(){
      this.GetMyServices();
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
