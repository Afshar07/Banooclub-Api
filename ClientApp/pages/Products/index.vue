<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="container NewBg mcontainer px-2">
    <div class="tw-w-full bg-white tw-shadow p-3 d-flex align-items-center justify-content-between gap-3  rounded ">
      <div class="d-flex align-items-center gap-2">
        <img src="/girl-icon-khadamat.png" class="tw-w-[7rem] tw-h-[7rem] tw-object-contain" alt="">
        <div class="d-flex align-items-center flex-column">
          <h1 class="text-purple h6" style="font-weight: bolder!important;"> خدمات</h1>
          <strong class="text-pink">Services</strong>
        </div>
      </div>

      <button v-if=" $auth.user&& $auth.user &&  $auth.user.baseData && $auth.user.baseData.userType===3" v-tooltip="{content:'ثبت خدمت'}" @click="$router.push('/Products/AddProduct')"
              class="btn AddReplyBtn text-white">
        <LazyPlusIcon fill="#ff6f9e" style="width: 30px; height: 30px;"/>
      </button>
    </div>
    <div class="bg-white tw-shadow rounded my-3 p-3">
      <ul class="nav nav-pills align-items-end profile_tabs py-3" id="pills-tab" role="tablist">
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link active" id="products-pills-home-tab" data-bs-toggle="pill"
                  data-bs-target="#products-pills-home" type="button" role="tab" aria-controls="products-pills-home"
                  aria-selected="true">
            همه خدمات
          </button>
        </li>
        <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile"
                  type="button" role="tab" aria-controls="pills-profile" aria-selected="false">
            پیشنهادات
          </button>
        </li>
      </ul>

      <div class="tab-content" id="pills-tabContent">
        <div class="tab-pane fade show active" id="products-pills-home" role="tabpanel"
             aria-labelledby="products-pills-home-tab">
          <div class="row boxMainContent mx-auto">
            <div class="col-12 text-center ">
              <LazyAllServicesTabContent :Categories="categories"/>
            </div>
          </div>
        </div>
        <div class="tab-pane fade " id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
          <LazyFirstTabContent @RefetchServices="GetAllServices" :AllServices="AllServicesFirstTab"
                           :categories="categories"/>
        </div>
      </div>
    </div>


  </div>


</template>

<script>






export default {
  name: "index",
  layout: "PoshtebamPlusLayout",

  head() {
    return {
      title: 'همه خدمات',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'همه خدمات',
        },
      ],
    }
  },
  data() {
    return {
      categories: [],
      AllServices: [],
      totalPages: [],
      AllServicesFirstTab: [],
      SelectedPageId: 1,
      Search: '',
      SelectedCategoryId: 0
    }
  },
  methods: {

    async GetAllServices() {
      try {
        const res = await this.$repositories.getAllServices.getAllServices(
          {
            pageNumber: this.SelectedPageId,
            count: 10,
            searchCommand: this.Search,
            status: 1,
            categoryId: this.SelectedCategoryId

          })
        this.AllServices = res.data.services
        this.totalPages = []
        const result = Math.ceil(res.data.servicesCount / 10)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
        }

      } catch (error) {
        console.log(error)
      }
    },

  },

  async fetch() {

    try {
      const res = await this.$repositories.getAllServicesCategory.getAllServicesCategory()
      console.log(res)
      this.categories = res.data.serviceCategories
    } catch (e) {
      console.log(e)
    }


    try {
      const res = await this.$repositories.getAllServices.getAllServices(
        {
          pageNumber: this.SelectedPageId,
          count: 1000,
          searchCommand: '',
          status: 1,

        }
      )
      this.AllServicesFirstTab = res.data.services
      this.totalPages = []
      const result = Math.ceil(res.data.servicesCount / 10)
      for (let i = 1; i <= result; i++) {
        this.totalPages.push(i);
      }


    } catch (error) {
      console.log(error)
    }

  },
}
</script>


<style>
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

/*@media (max-width: 375px) {*/
/*  .vueperslide.vueperslide--active.vueperslide--visible{*/
/*    margin-left: 3% !important;*/
/*  }*/
/*  .vueperslide.vueperslide--visible{*/
/*    margin-left: 3% !important;*/
/*  }*/
/*  .vueperslide{*/
/*    margin-left: 3% !important;*/
/*  }*/
/*}*/
.nav-pills .nav-link {
  border-bottom: 3px solid transparent;
}

.nav-link {
  font-weight: 500;
  color: #8c8d90 !important;
  font-weight: 500;
  padding: 5px 9px 15px;
}

.nav-pills .nav-link.active, .nav-pills .show > .nav-link {
  color: #0d6efd !important;
  background-color: transparent;
  font-weight: 500;
  border-bottom: 3px solid #0d6efd !important;
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

</style>
