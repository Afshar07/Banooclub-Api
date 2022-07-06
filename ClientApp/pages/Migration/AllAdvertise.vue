<template>
  <div class="container mcontainer">
    <div class="row">
      <div class="col-12 d-flex align-items-center gap-3 my-4">
        <i class="fas fa-ad text-muted" style="font-size: 50px"></i>
        <h3 class="m-0">همه آگهی ها</h3>
      </div>

      <div class="col-md-2">
        <div class="d-flex flex-column">
          <input
            v-model="searchKeyword"
            type="search"
            class="form-control my-3"
            placeholder="جستجو..."
          />
          <!-- <div class="d-flex align-items-center gap-3">
            <i class="fas fa-list"></i>
            <strong>دسته بندی ها</strong>
          </div> -->
          <div class="d-flex flex-column">
            <strong
              class="CategoryName w-100 my-3 d-flex align-items-center justify-content-between"
              style="cursor: pointer"
              :class="GetActiveClassName(0)"
              @click="toggleCategories"
            >
              دسته بندی ها
              <svg
                class="custom-icon"
                :class="isCategoriesOpen ? 'open-arrow-style' : ''"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
                stroke-width="2"
              >
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  d="M19 9l-7 7-7-7"
                />
              </svg>
            </strong>
            <transition name="accordion">
              <div class="accordion-container" v-show="isCategoriesOpen">
                <ul
                  class="my-3 p-0"
                  style="list-style-type: none"
                  v-for="item in categories.filter((e) => e.ParentId === null)"
                  :key="item.MainAdsCategoryId"
                >
                  <strong class="CategoryName w-100" style="cursor: pointer">{{
                      item.MainName
                    }}</strong>

                  <div
                    class="pe-3 text-muted mt-3 Child"
                    style="cursor: pointer"
                    v-for="el in categories.filter(
                      (e) => e.ParentId === item.MainAdsCategoryId
                    )"
                    :key="el.MainAdsCategoryId"
                  >
                    <small @click="ChangeActiveCategory(el.MainAdsCategoryId)">
                      {{ el.MainName }}</small
                    >
                    <div
                      class="pe-5 text-muted mt-3 Child"
                      style="cursor: pointer"
                      v-for="third in categories.filter(
                        (e) => e.ParentId === el.MainAdsCategoryId
                      )"
                      :key="third.MainAdsCategoryId"
                    >
                      <small
                        @click="ChangeActiveCategory(third.MainAdsCategoryId)"
                      >
                        {{ third.MainName }}</small
                      >
                    </div>
                  </div>
                </ul>
              </div>
            </transition>
          </div>
          <div class="d-flex align-items-center gap-3">
            <i class="fas fa-coins"></i>
            <strong>قیمت </strong>
          </div>
          <div class="d-flex flex-column">
            <div class="d-flex flex-wrap align-items-center gap-2">
              <input
                v-model="priceFrom"
                type="number"
                class="form-control my-3"
                placeholder="از..."
              />
              <input
                v-model="priceTo"
                type="number"
                class="form-control mb-3"
                placeholder="تا..."
              />
            </div>
          </div>
          <div class="d-flex my-lg-3 align-items-center gap-3">
            <i class="fas fa-planet"></i>
            <strong>موقعیت جغرافیایی</strong>
          </div>
          <div class="d-flex flex-column">
            <div class="d-flex flex-wrap align-items-center gap-2">
              <v-select
                @input="getCities"
                :options="Countries"
                label="name"
                dir="rtl"
                class="selectWidth w-100"
                placeholder="کشور"
                v-model="CountryId"
                :reduce="(name) => name.countryId"
                :value="CountryId"
              ></v-select>
              <v-select
                :options="Cities"
                label="name"
                dir="rtl"
                class="selectWidth my-lg-3 w-100"
                placeholder="شهر"
                v-model="City"
                :reduce="(name) => name.cityId"
                :value="City"
              ></v-select>
            </div>
          </div>
        </div>
      </div>
      <div class="col-md-10 mx-auto pe-lg-2 p-0">
        <div class="row m-0 px-2 gap-3">
          <div
            class="py-1 custom-ad-card-height"
            style="cursor: pointer; width: 49%"
            v-for="item in adsListFilter"
            :key="item.adsId"
          >
            <div
              class="row bg-white AdCard border rounded"
              @click="ChangeRoute(item.adsId)"
            >
              <div class="col-md-6">
                <nuxt-link
                  class="text-dark text-decoration-none"
                  :to="{
                    path: '/Migration/AdvertiseDetail',
                    query: { id: item.adsId },
                  }"
                ><span>{{ item.title }}</span></nuxt-link
                >
                <br />
                <br />
                <small class="text-muted">
                  {{ new Intl.NumberFormat().format(item.price) }} تومان</small
                >
                <br />
                <div class="d-flex align-items-center gap-2">
                  <i class="fas fa-user text-muted" style="font-size: 12px"></i>
                  <small
                    v-if="item.userInfo"
                    class="text-muted"
                    style="font-size: 12px"
                  >{{
                      item.userInfo.name + " " + item.userInfo.familyName
                    }}</small
                  >
                </div>
                <br />
                <div class="d-flex align-items-center gap-2">
                  <i
                    class="fas fa-clock text-muted"
                    style="font-size: 10px"
                  ></i>
                  <small class="text-muted" style="font-size: 10px">{{
                      item.createDate | moment("jYYYY-jMM-jDD")
                    }}</small>
                </div>
              </div>
              <div class="col-md-6 d-flex p-0">
                <img
                  v-if="item.photos.length > 0"
                  :src="BaseUrl + item.photos[0].base64"
                  class="img-fluid rounded-start my-auto custom-ad-image-height w-100"
                  height="50px"
                  width="100%"
                  alt=""
                />
                <p v-else class="small">برای این آگهی عکسی ثبت نشده است</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import AdvertiseCard from "~/components/AdvertiseCard";
import veeno from "veeno";
import "nouislider/distribute/nouislider.min.css";

export default {
  name: "AllAdvertise",
  components: { AdvertiseCard, veeno },
  layout: "PoshtebamPlusLayout",
  data() {
    return {
      priceFrom: 0,
      priceTo: 0,
      searchKeyword: "",
      CountryId: 0,
      allAdvertise: [],
      categories: [],
      SelectedCategoryId: 0,
      categoryFilter: [],
      SecondLevel: [],
      ThirdLevel: [],
      ScrollY: 0,
      minFilterValue: null,
      maxFilterValue: null,
      CategoryClass: ["ActiveCategory,ActiveSubmenu"],
      Cities: [],
      Countries: [],
      City: 0,
      isCategoriesOpen: false,
    };
  },
  mounted() {
    window.addEventListener("scroll", this.updateScroll);
    this.getAllAdvertise(0, 0, 0, 0, 0, this.searchKeyword);
    this.getAllCategory();
    this.getCountries();
  },
  watch: {
    searchKeyword: function (val, oldVal) {
      this.getAllAdvertise(
        this.priceFrom,
        this.priceTo,
        this.City,
        this.CountryId,
        this.SelectedCategoryId,
        val
      );
    },
    priceFrom: function (val, oldVal) {
      this.getAllAdvertise(
        val,
        this.priceTo,
        this.City,
        this.CountryId,
        this.SelectedCategoryId,
        this.searchKeyword
      );
    },
    priceTo: function (val, oldVal) {
      this.getAllAdvertise(
        this.priceFrom,
        val,
        this.City,
        this.CountryId,
        this.SelectedCategoryId,
        this.searchKeyword
      );
    },
    CountryId: function (val, oldVal) {
      this.getAllAdvertise(
        this.priceFrom,
        this.priceTo,
        this.City,
        val,
        this.SelectedCategoryId,
        this.searchKeyword
      );
    },
    City: function (val, oldVal) {
      this.getAllAdvertise(
        this.priceFrom,
        this.priceTo,
        val,
        this.CountryId,
        this.SelectedCategoryId,
        this.searchKeyword
      );
    },
    SelectedCategoryId: function (val, oldVal) {
      this.getAllAdvertise(
        this.priceFrom,
        this.priceTo,
        this.City,
        this.CountryId,
        val,
        this.searchKeyword
      );
    },
  },
  methods: {
    toggleCategories() {
      this.isCategoriesOpen = !this.isCategoriesOpen;
    },
    getAllAdvertise(PF, PT, Cid, CoId, CatId, search) {
      if (CoId === null) {
        CoId = 0;
      }
      if (Cid === null) {
        Cid = 0;
      }

      this.$axios
        .post(
          `Ads/GetAdsByFilter?priceFrom=${PF}&priceTo=${PT}&title=${search}&city=${Cid}&country=${CoId}&firstSearchadsId=0&count=50&categoryId=${CatId}`,
          null
        )
        .then((response) => {
          if (response.status === 200) {
            this.allAdvertise = response.data.ads;
          }
        })
        .catch((error) => {});
    },
    getAllCategory() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      });
      this.$axios
        .post(`AdsCategory/GetAll?pageNumber=1&count=50`, null, {
          headers: {},
        })
        .then((response) => {
          if (response.status === 200) {
            this.categories = response.data.adsCategories;
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false;
          }
        })
        .catch((error) => {});
    },
    getCountries() {
      this.$axios.get(`CNTRY/GetCNTRIESWithRedis`).then((res) => {
        this.Countries = res.data;
      });
    },
    getCities() {
      this.$axios
        .get(
          `City/GetCitiesByCountryId?pageNumber=0&count=0&countryId=${this.CountryId}`
        )
        .then((res) => {
          this.Cities = res.data.cities;
        });
    },

    ChangeRoute(id) {
      this.$router.push({
        path: "/Migration/AdvertiseDetail",
        query: { id: id },
      });
    },
    ChangeActiveCategory(id) {
      this.SelectedCategoryId = id;
    },
    GetActiveClassName(id) {
      if (id === this.SelectedCategoryId) {
        return "ActiveCategory";
      } else {
        return "";
      }
    },
    GetSubmenuActiveClassName(id) {
      if (id === this.SelectedCategoryId) {
        return "ActiveSubmenu";
      } else {
        return "";
      }
    },
    selectCategoryForFilter(categoryId) {
      if (this.categoryFilter.includes(categoryId)) {
        this.categoryFilter.splice(this.categoryFilter.indexOf(categoryId), 1);
      } else {
        this.categoryFilter.push(categoryId);
      }
    },
  },

  computed: {
    BaseUrl() {
      return process.env.pic;
    },

    FilterCategories() {
      const data = this.categories;
      return data.filter((item) => {
        return item.parentId === null;
      });
    },

    adsListFilter() {
      return this.categoryFilter.length > 0
        ? this.allAdvertise.filter(function (d) {
            return this.indexOf(d.categoryId) > -1;
          }, this.categoryFilter)
        : this.allAdvertise;
    },
  },
};
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
.custom-ad-card-height {
  min-height: 14rem !important;
  max-height: 14rem !important;
}
.custom-ad-image-height {
  min-height: 13.8rem;
  max-height: 13.8rem;
}
@media only screen and (min-width: 769px) {
  .AdCard:hover {
    box-shadow: 0px 8px 7px 0px rgba(0, 0, 0, 0.3);
    transform: scale(1.02);
    transition: 0.2s ease;
  }
}
.AdCard {
  transition: 0.2s ease;
}

.CategoryName {
  transition: 0.2s ease;
}

.CategoryName:hover {
  color: black !important;
  border-bottom: solid 0.2px orange;
  transition: 0.2s ease;
}
.ActiveCategory {
  color: black !important;
  border-bottom: solid 0.2px orange;
  transition: 0.2s ease;
}

.Submenu {
  height: 0;
  overflow-y: hidden;
  transition: 0.2s ease;
}
.ActiveSubmenu {
  height: 100%;
  transition: 0.2s ease;
}

.Child:hover {
  color: orange !important;
  transition: 0.2s ease;
}
.custom-icon {
  width: 1rem;
  height: 1rem;
  transition: all 0.3s;
}
.open-arrow-style {
  transform: rotate(180deg);
}
.accordion-enter-active {
  animation: accordion 0.3s ease-in-out;
}
.accordion-leave-active {
  animation: accordion reverse 0.3s ease-in-out;
}
@keyframes accordion {
  0% {
    max-height: 0;
    opacity: 0;
  }
  100% {
    max-height: 1000px;
    opacity: 1;
  }
}
</style>
