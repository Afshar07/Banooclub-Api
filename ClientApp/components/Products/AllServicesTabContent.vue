<template>
  <div>
    <div class="row">
      <div class="col-md-12 d-flex align-items-center ">
        <input
          class="SearchStyle"
          type="text"
          placeholder="جستجو خدمت"
          v-model="Search"
        />
      </div>
    </div>
    <div class="row my-3">
        <div class="col-md-12">
          <div class="row">
            <div class="col-md-3">
              <div class="row">
                <div class="col-md-12">
                  <div class="accordion " id="accordionPanelsStayOpenExample">
                    <div class="accordion-item">
                      <h2 class="accordion-header" id="panelsStayOpen-headingFour">
                        <button
                          class="accordion-button p-2 w-100   "
                          type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseOne"
                          aria-expanded="true" aria-controls="panelsStayOpen-collapseOne">
                          <div class="tw-w-full tw-flex tw-items-center tw-justify-between">

                            <small> فیلتر ها</small>
                            <FilterIcon class="tw-fill-white"></FilterIcon>
                          </div>
                        </button>
                      </h2>
                      <div id="panelsStayOpen-collapseOne" class="accordion-collapse collapse show"
                           aria-labelledby="panelsStayOpen-headingFour">
                        <div class="accordion-body">
                          <div class="d-flex  gap-2">
                            <SlideCheckBox v-model="HasDiscount" :UniqueId="1"></SlideCheckBox>
                            <div class="d-flex align-items-center gap-2">
                              <FireIcon class="tw-fill-red-500 tw-w-6 tw-h-6"></FireIcon>

                              <small>تخفیف دار</small>
                            </div>

                          </div>
                          <div class="d-flex align-items-end my-2  gap-2">
                            <SlideCheckBox v-model="IsFunTime" :UniqueId="2"></SlideCheckBox>
                            <div class="d-flex align-items-center gap-2">
                              <ExChangeIcon class="tw-fill-sky-500 tw-w-6 tw-h-6"></ExChangeIcon>
                              <small>شادی تایم</small>
                            </div>

                          </div>


                        </div>
                      </div>

                    </div>
                    <div class="accordion-item">
                      <h2 class="accordion-header" id="panelsStayOpen-headingSeven">
                        <button
                          class="accordion-button p-2 w-100 d-flex align-items-center justify-content-between"
                          type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseSeven"
                          aria-expanded="true" aria-controls="panelsStayOpen-collapseSeven">
                          <small> قیمت</small>
                        </button>
                      </h2>
                      <div id="panelsStayOpen-collapseSeven" class="accordion-collapse collapse show"
                           aria-labelledby="panelsStayOpen-headingSeven">
                        <div class="accordion-body">
                          <div class="d-flex align-items-center gap-2">
                            <input type="tel" v-model="PriceFrom" placeholder="مثلا از 100000 تومان"
                                   class=" my-1 p-1 SearchStyle Form-Control border ">
                            <small>-</small>
                            <input type="tel" v-model="PriceTo" placeholder="مثلا تا 200000 تومان"
                                   class=" my-1 p-1 SearchStyle Form-Control border ">
                          </div>

                        </div>
                      </div>

                    </div>
                    <div class="accordion-item">
                      <h2 class="accordion-header" id="panelsStayOpen-headingTwo">
                        <button class="accordion-button p-2" type="button" data-bs-toggle="collapse"
                                data-bs-target="#panelsStayOpen-collapseTwo" aria-expanded="true"
                                aria-controls="panelsStayOpen-collapseOne">

                          <small> دسته بندی</small>
                        </button>
                      </h2>
                      <div id="panelsStayOpen-collapseTwo" class="accordion-collapse collapse show"
                           aria-labelledby="panelsStayOpen-headingOne">
                        <div class="accordion-body">
                          <v-select
                          v-model="SelectedCategoryId"
                          :options="Categories"
                          placeholder="جستجو بر اساس دسته بندی"
                          label="title"
                          :reduce="title=> title.serviceCategoryId"
                        />
                        </div>
                      </div>
                    </div>
                    <div class="accordion-item">
                      <h2 class="accordion-header" id="panelsStayOpen-headingThree">
                        <button class="accordion-button p-2" type="button" data-bs-toggle="collapse"
                                data-bs-target="#panelsStayOpen-collapseThree" aria-expanded="true"
                                aria-controls="panelsStayOpen-collapseOne">
                          <small> شهر و استان</small>
                        </button>
                      </h2>
                      <div id="panelsStayOpen-collapseThree" class="accordion-collapse collapse show"
                           aria-labelledby="panelsStayOpen-headingOne">
                        <div class="accordion-body">

                          <v-select
                            @input="GetCity"
                            :options="AllStates"
                            label="name"
                            dir="rtl"
                            placeholder="استان"
                            v-model="SelectedStateId"
                            :reduce="(name) => name.stateId"

                          ></v-select>


                          <v-select
                            :options="AllCities"
                            label="name"
                            dir="rtl"
                            class="my-3"
                            placeholder="شهر"
                            v-model="SelectedCityId"
                            :reduce="(name) => name.cityId"

                          ></v-select>
                        </div>
                      </div>
                    </div>

                  </div>
                </div>
              </div>
            </div>
            <div class="col-md-9">
              <div class="row">

              <div class="col-md-4 col-12" v-for="(service,idx) in AllServices" :key="idx">
                <ProductItem @GetServices="Refresh()" class="my-3" :service_details="service" />
              </div>
              <div v-if="AllServices && AllServices.length === 0" class="col-12 text-warning fw-bold my-3 text-center">
                هیچ خدمتی برای نمایش وجود ندارد
              </div>
              </div>
            </div>

          </div>
        </div>

    </div>
    <div class="row">
      <div class="col-md-12">
        <CustomPagination v-if="totalPages.length>1" :activePage="SelectedPageId" :totalPages="totalPages" @PageChanged="changePage($event)"/>
      </div>
    </div>
  </div>


</template>

<script>
import ProductItem from "../Products/ProductItem";
import CustomPagination from "../../components/utilities/CustomPagination"
  import FireIcon from "@/components/Icons/FireIcon";
import ExChangeIcon from "@/components/Icons/ExChangeIcon";
import FilterIcon from "@/components/Icons/FilterIcon";
export default {
  props:['Categories'],
  name: "AllServicesTabContent",
  components: {
    ProductItem,
    CustomPagination,
    FilterIcon,
    FireIcon,
    ExChangeIcon
  },
  mounted(){

  },
  data(){
    return{
      AllServices:[],
      Search:'',
      pageNumber:0,
      SelectedCategoryId:0,
      totalPages:[],
      SelectedPageId:1,
      PriceFrom:0,
      PriceTo:0,
      IsFunTime:false,
      HasDiscount:false,
      AllStates:[],
      AllCities:[],
      SelectedStateId:0,
      SelectedCityId:0
    }
  },
  async fetch(){
    await  this.GetAllServices();
    try {
      const res = await this.$repositories.GetAllStates.GetAllStates()
      res.data.states.forEach((item) => {
        this.AllStates.push(item)
      })


    } catch (e) {
      console.log(e)
    }
  },
  watch:{
    Search:function (){
      this.GetAllServices()
    },
    HasDiscount:function (){
      this.GetAllServices();
    },
    IsFunTime:function (){
      this.GetAllServices()
    },
    PriceFrom:function (){
      this.GetAllServices();
    },
    PriceTo:function (){
      this.GetAllServices();
    },
    SelectedStateId:function (){
      this.GetAllServices();
    },
    SelectedCityId:function (){
      this.GetAllServices();
    },
    SelectedCategoryId:function (){
      this.GetAllServices();
    }
  },
  methods: {
    Refresh(){
      this.GetAllServices()
    },
    changePage(id){
      this.SelectedPageId = id
      this.GetAllServices()
    },
    async GetCity() {

      this.$nextTick(() => {
        this.$nuxt.$loading.start()
      })
      try {
        const res = await this.$repositories.GetAllCities.GetAllCities({
          pageNumber: 1,
          count: 500,
          stateId: this.SelectedStateId

        })
        res.data.cities.forEach((item) => {
          this.AllCities.push(item)
        })
        this.$nuxt.$loading.finish()
        this.$nuxt.loading = false;
      } catch (e) {
        console.log(e)
      } finally {
        this.$nuxt.$loading.finish()
        this.$nuxt.loading = false;
      }
    },
    async GetAllServices(){
      try {
        const res = await this.$repositories.getAllServices.getAllServices(
          {
            pageNumber:this.SelectedPageId,
            count:10  ,
            searchCommand:this.Search,
            status:1,
            categoryId:this.SelectedCategoryId,
            FromPrice:this.PriceFrom,
            ToPrice:this.PriceTo,
            CityId:this.SelectedCityId,
            stateId:this.SelectedStateId,
            HasDiscount:this.HasDiscount,
            IsFunTime:this.IsFunTime

          }
        )

        this.AllServices = res.data.services
        this.totalPages = []
        const result = Math.ceil( res.data.servicesCount / 10)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
        }

      }
      catch (error){
        console.log(error)
      }
    },

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
</style>
