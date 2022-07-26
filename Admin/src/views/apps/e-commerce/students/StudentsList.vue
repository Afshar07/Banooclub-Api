<template>
  <b-overlay
      :show="products === null"
      rounded="sm"
  >

    <div v-if="products !== null">

      <!-- Table Container Card -->
      <b-card
          no-body
          class="mb-0"
      >

        <div class="m-2">

          <!-- Table Top -->
          <b-row>

            <b-col
                cols="12"
                md="6"
                class="d-flex align-items-center justify-content-start mb-1 mb-md-0"
            >
              <label>نمایش</label>
              <v-select
                  v-model="perPage"
                  :dir="$store.state.appConfig.isRTL ? 'rtl' : 'ltr'"
                  :options="perPageOptions"
                  :clearable="false"
                  class="per-page-selector d-inline-block mx-50"
              />
              <label>تعداد</label>
            </b-col>

            <!-- Search -->
            <b-col
                cols="12"
                md="6"
            >
              <div class="d-flex align-items-center justify-content-end">
                <b-form-input
                    v-model="searchCommand"
                    class="d-inline-block mr-1"
                    placeholder="جستجو..."
                />
              </div>
            </b-col>
          </b-row>

        </div>

        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="products"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >

          <!-- Column: product.price -->
          <template #cell(product.price)="data">
            <span v-if="data.item.product.isFree">رایگان</span>
            <span v-else>{{makePrettyPrice(data.item.product.price)}} تومان</span>
          </template>

          <!-- Column: Actions -->
          <template #cell(actions)="data">
            <b-link :to="{ name: 'apps-e-commerce-studentInfoes', params: { id: data.item.product.productId } }">
              <feather-icon icon="EditIcon" size="20"/>
            </b-link>
          </template>

        </b-table>
        <div class="mx-2 mb-2">
          <b-row>

            <b-col
                cols="12"
                sm="6"
                class="d-flex align-items-center justify-content-center justify-content-sm-start"
            >
              <!--            <span class="text-muted">Showing {{ dataMeta.from }} to {{ dataMeta.to }} of {{ dataMeta.of }} entries</span>-->
            </b-col>
            <!-- Pagination -->
            <b-col
                cols="12"
                sm="6"
                class="d-flex align-items-center justify-content-center justify-content-sm-end"
            >

              <b-pagination
                  v-model="currentPage"
                  :total-rows="totalCounts"
                  :per-page="perPage"
                  first-number
                  last-number
                  class="mb-0 mt-1 mt-sm-0"
                  prev-class="prev-item"
                  next-class="next-item"
              >
                <template #prev-text>
                  <feather-icon style="transform: rotate(180deg)"
                                icon="ChevronLeftIcon"
                                size="18"
                  />
                </template>
                <template #next-text>
                  <feather-icon
                      icon="ChevronRightIcon"
                      size="18"
                  />
                </template>
              </b-pagination>

            </b-col>

          </b-row>
        </div>
      </b-card>
    </div>
  </b-overlay>
</template>

<script>
import {
  BCard, BRow, BCol, BFormInput, BButton, BTable, BMedia, BAvatar, BLink,
  BBadge, BDropdown, BDropdownItem, BPagination, BOverlay,
} from 'bootstrap-vue'
import vSelect from 'vue-select'
import OrderGetAllRequest from "@/libs/Api/Order/OrderGetAllRequest";
import Helper from "@/libs/Helper";
import OrderChangeStatusRequest from "@/libs/Api/Order/OrderChangeStatusRequest";
import ToastificationContent from "@core/components/toastification/ToastificationContent";
import OrderDeleteRequest from "@/libs/Api/Order/OrderDeleteRequest";
import OrderGetProductsForStudents from "@/libs/Api/Order/OrderGetProductsForStudents";

export default {
  title:"لیست دانشجویان - پنل ادمین بانو کلاب",
  name: "StudentsList",
  data(){
    return{
      products:null,
      currentPage:1,
      totalCounts:null,
      perPage:10,
      perPageOptions:[10, 20, 30 , 40 , 50],
      myTableColumns : [
        { key: 'product.productId' ,label :'شناسه محصول'},
        { key: 'product.title' ,label :'نام محصول'},
        { key: 'product.price' ,label :'قیمت'},
        { key: 'usersRegistered' ,label :'تعداد دانشجویان'},
        { key: 'actions',label:'نمایش دانشجویان'},
      ],
      searchCommand:'',
      isAddNewUserSidebarActive:false,
    }
  },
  async created() {
    await this.getProducts(this.perPage,this.currentPage,this.searchCommand)
  },
  methods:{
    refreshData(){
      this.getProducts(this.perPage,this.currentPage,this.searchCommand)
    },
    async getProducts(count,pageNumber,searchCommand){
      let _this = this;
      let data = {
        pageNumber:pageNumber,
        count:count,
        searchCommand:searchCommand
      }

      let orderGetProductsForStudents = new OrderGetProductsForStudents(_this);
      orderGetProductsForStudents.setParams(data);
      await orderGetProductsForStudents.fetch(function (content){
        _this.products = content.result;
        _this.totalCounts = content.productsCount;
        console.log(content)
      },function (error){
        console.log(error);
      })
    },
    makePrettyPrice(param){
      return Helper.prettyPrice(param,0)
    },
    createJalaliDate(param){
      return new Date(param).toLocaleDateString("fa-IR",);
    },
  },
  watch:{
    perPage:function (nv,ov){
      this.getProducts(nv,this.currentPage,this.searchCommand);
    },
    currentPage:function (nv,ov) {
      this.getProducts(this.perPage,nv,this.searchCommand);
    },
    searchCommand:function (nv,ov){
      this.getProducts(this.perPage,this.currentPage,nv)
    },
  },
  components: {

    BCard,
    BRow,
    BCol,
    BFormInput,
    BButton,
    BTable,
    BMedia,
    BAvatar,
    BLink,
    BBadge,
    BDropdown,
    BDropdownItem,
    BPagination,
    BOverlay,

    vSelect,
  },
}
</script>

<style lang="scss" scoped>
.per-page-selector {
  width: 90px;
}
</style>

<style lang="scss">
@import '@core/scss/vue/libs/vue-select.scss';
</style>
