<template>
  <div class="container mcontainer">
    <div class="row">
      <h1 class="tw-text-2xl tw-font-semibold">آگهی ها</h1>

    </div>
    <div class="tab-content" id="pills-tabContent" v-if="!$fetchState.pending">
      <div class="tab-pane fade show active" id="products-pills-home" role="tabpanel" aria-labelledby="products-pills-home-tab">
        <div class="row boxMainContent mx-auto">
          <div class="col-12  px-0">
            <div class="row">
              <div class="col-md-3 mt-3">
               <div class="row">
                 <div class="col-md-12">
                   <h6 class="tw-font-bold tw-mt-2">فیلتر ها</h6>
                   <div class="accordion tw-mt-[2.3rem]" id="accordionPanelsStayOpenExample">
                     <div class="accordion-item">
                       <h2 class="accordion-header" id="panelsStayOpen-headingOne">
                         <button class="accordion-button w-100 d-flex align-items-center justify-content-between"  type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseOne" aria-expanded="true" aria-controls="panelsStayOpen-collapseOne">
                           <small>قیمت</small>

                         </button>
                       </h2>
                       <div id="panelsStayOpen-collapseOne" class="accordion-collapse collapse show" aria-labelledby="panelsStayOpen-headingOne">
                         <div class="accordion-body">
                           <input type="tel" v-model="PriceFrom" placeholder="مثلا از 100000 تومان" class="form-control my-1 form-control-sm">
                           <input type="tel" v-model="PriceTo" placeholder="مثلا تا 200000 تومان" class="form-control my-1 form-control-sm">
                         </div>
                       </div>
                     </div>
                     <div class="accordion-item">
                       <h2 class="accordion-header" id="panelsStayOpen-headingTwo">
                         <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseTwo" aria-expanded="true" aria-controls="panelsStayOpen-collapseOne">
                           <small>   دسته بندی</small>

                         </button>
                       </h2>
                       <div id="panelsStayOpen-collapseTwo" class="accordion-collapse collapse show" aria-labelledby="panelsStayOpen-headingOne">
                         <div class="accordion-body">
                           <select  v-model="SelectedCategoryId" class="form-control">
                             <option  :value="0" selected>
                             <small>همه دسته بندی ها </small>
                             </option>
                             <option v-for="item in categories" :value="item.MainAdsCategoryId">
                               <small>    {{item.MainName}}</small>
                           </option>
                           </select>
                         </div>
                       </div>
                     </div>
                     <div class="accordion-item">
                       <h2 class="accordion-header" id="panelsStayOpen-headingThree">
                         <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseThree" aria-expanded="true" aria-controls="panelsStayOpen-collapseOne">
                           <small>  شهر و استان</small>

                         </button>
                       </h2>
                       <div id="panelsStayOpen-collapseThree" class="accordion-collapse collapse show" aria-labelledby="panelsStayOpen-headingOne">
                         <div class="accordion-body">
                           <v-select
                             @input="GetCity"
                             :options="AllStates"
                             label="name"
                             dir="rtl"
                             class="selectWidth w-100"
                             placeholder="استان"
                             v-model="SelectedStateId"
                             :reduce="(name) => name.stateId"

                           ></v-select>
                           <br>
                           <v-select
                             @input="GetCity"
                             :options="AllCities"
                             label="name"
                             dir="rtl"
                             class="selectWidth w-100"
                             placeholder="استان"
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
                <ul class="nav nav-pills align-items-end profile_tabs py-3" id="pills-tab" role="tablist">
                  <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
                    <button class="nav-link active" id="products-pills-home-tab" data-bs-toggle="pill"
                            data-bs-target="#products-pills-home" type="button" role="tab" aria-controls="products-pills-home" aria-selected="true">
                      همه آگهی ها
                    </button>
                  </li>
                  <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
                    <button class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">
                      پیشنهادات
                    </button>
                  </li>
                </ul>
                <div class="row">
                  <div class="col-md-12">
                    <input type="search" v-model="Search"  placeholder="جستجو در میان آگهی ها">
                  </div>
                </div>
                <div class="row my-2">
                  <AllAdsTabContent :Ads="AllAds" :categories="categories"/>
                </div>

              </div>
            </div>

          </div>
        </div>
      </div>
      <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
<!--        <FirstTabContent :services="all_services" :categories="categories"/>-->
      </div>
    </div>


  </div>


</template>

<script>
    import ProductItem from "../../components/Products/ProductItem";
    import ChevronRightIcon from "../../components/Icons/RightChevronIcon";
    import ChevronLeftIcon from "../../components/Icons/LeftChevronIcon";
    import RightChevronIcon from "../../components/Icons/RightChevronIcon";
    import LeftChevronIcon from "../../components/Icons/LeftChevronIcon";
    import FirstTabContent from "../../components/Products/FirstTabContent";
    import AllAdsTabContent from "../../components/Ads/AllAdsTabContent";
    export default {
        name: "index",
        layout: "PoshtebamPlusLayout",
        components:{
          AllAdsTabContent,
            FirstTabContent,
            LeftChevronIcon,
            RightChevronIcon,
            ChevronLeftIcon,
            ChevronRightIcon,
            ProductItem,
        },
        head(){
            return{
                title: 'آگهی ها'
            }
        },
        data(){
            return{
                AllAds:null,
                categories:[],
              SelectedCategoryId:0,
              PriceFrom:0,
              PriceTo:0,
              CityId:0,
              SelectedStateId:null,
              SelectedCityId:null,
              AllCities:[
                {
                  name:'همه شهر ها',
                  cityId:0
                }
              ],
              AllStates:[
                {

                  name:'همه استان ها',
                  stateId:0
                }
              ],
              Search:''
            }
        },
      watch:{
        Search:function (val,oldVal){
          this.GetAdd();
        },
        PriceFrom:function (val,oldVal){
          this.GetAdd();
        },
        PriceTo:function (val,oldVal){
          this.GetAdd();
        },
        SelectedCategoryId:function (val,oldVal){
          this.GetAdd();
        },
        SelectedStateId:function (val,oldVal){
          this.GetAdd();
        },
        SelectedCityId:function (val,oldVal){
          this.GetAdd();
        }
      },
      methods:{
        async GetAdd(){
          try {
            if(this.Search==='' || this.PriceTo!==null || this.PriceFrom!== null){
              this.$nextTick(()=>{
                this.$nuxt.$loading.start();
              })
            }

            const res = await this.$repositories.GetAllAds.GetAllAds({
              priceFrom:this.PriceFrom,
              priceTo:this.PriceTo,
              title:this.Search,
              tag:null,
              city:this.SelectedCityId,
              state:this.SelectedStateId,
              firstSearchadsId:null,
              count:20,
              categoryId:this.SelectedCategoryId
            })
            this.AllAds = res.data.ads
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false;
          }
          catch (error){
            console.log(error)
          }finally {
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false;
          }
        },
       async GetCity(){

          this.$nextTick(()=>{
            this.$nuxt.$loading.start()
          })
          try {
            const res = await this.$repositories.GetAllCities.GetAllCities({
              pageNumber:1,
              count:500,
              stateId:this.SelectedStateId

            })
           res.data.cities.forEach((item)=>{
             this.AllCities.push(item)
           })
            this.$nuxt.$loading.finish()
            this.$nuxt.loading = false;
          }catch (e) {
            console.log(e)
          }finally {
            this.$nuxt.$loading.finish()
            this.$nuxt.loading = false;
          }
        }
      },
        async fetch(){
            try {
                const res = await this.$repositories.GetAllAds.GetAllAds({
                  priceFrom:this.PriceFrom,
                  priceTo:this.PriceTo,
                  title:this.Search,
                  tag:null,
                  city:this.SelectedCityId,
                  state:this.SelectedStateId,
                  firstSearchadsId:null,
                  count:20,
                  categoryId:this.SelectedCategoryId
                })
                  this.AllAds = res.data.ads


                const allCategories = await this.$repositories.getAllAdsCategory.getAllAdsCategory();
                this.categories = allCategories.data.adsCategories;

            }
            catch (error){
                console.log(error)
            }

            try {
              const res = await  this.$repositories.GetAllStates.GetAllStates()
              res.data.states.forEach((item)=>{
                this.AllStates.push(item)
              })
              console.log(this.AllStates)

            }catch (e) {
              console.log(e)
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
    border-bottom: 3px solid transparent ;
  }
  .nav-link {
    font-weight: 500;
    color: #8c8d90 !important;
    font-weight: 500;
    padding: 5px 9px 15px;
  }
  .nav-pills .nav-link.active, .nav-pills .show>.nav-link {
    color: #0d6efd !important;
    background-color: transparent;
    font-weight: 500;
    border-bottom: 3px solid #0d6efd !important;
    border-bottom-left-radius: 0;
    border-bottom-right-radius: 0;
  }

  .accordion-button::after{
    content: ''!important;
    background-image: none!important;
  }
  .accordion-button:not(.collapsed) {
    color: white!important;
    background-color: #3b82f6;
    box-shadow: inset 0 -1px 0 rgb(0 0 0 / 13%);
  }
</style>
