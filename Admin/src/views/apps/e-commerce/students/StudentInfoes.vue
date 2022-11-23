<template>
  <b-overlay
      :show="students === null"
      rounded="sm"
  >

    <div v-if="students !== null">

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

<!--            &lt;!&ndash; Search &ndash;&gt;-->
<!--            <b-col-->
<!--                cols="12"-->
<!--                md="6"-->
<!--            >-->
<!--              <div class="d-flex align-items-center justify-content-end">-->
<!--                <b-form-input-->
<!--                    v-model="searchCommand"-->
<!--                    class="d-inline-block mr-1"-->
<!--                    placeholder="جستجو..."-->
<!--                />-->
<!--              </div>-->
<!--            </b-col>-->
          </b-row>

        </div>

        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="students"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی یافت نشد."
        >

          <template #cell(user.name)="data">
            <b-media vertical-align="center">
              <template #aside>
                <b-avatar
                    size="45"
                    :src="`https://api.halamkhoobee.com/${data.item.user.selfieFileData}`"
                    :variant="`success`"
                    :to="{ name: 'apps-users-edit', params: { id: data.item.user.userId } }"
                />
              </template>
              <b-link
                  :to="{ name: 'apps-users-edit', params: { id: data.item.user.userId } }"
                  class="font-weight-bold d-block text-nowrap"
              >
                {{ data.item.user.name+' '+data.item.user.familyName }}
              </b-link>
              <small class="text-muted">@{{ data.item.user.userId }}</small>
            </b-media>
          </template>

          <!-- Column: type -->
          <template #cell(type)="data">
            <b-badge
                pill
                class="text-capitalize"
                :variant="``"
            >
              {{ data.item.type }}
            </b-badge>
          </template>


          <!-- Column: state -->
          <template #cell(user.state)="data">
            <b-badge
                v-if="data.item.user.state === 1"
                pill
                class="text-capitalize"
                :variant="`success`"
            >
              وارد شده
            </b-badge>
            <b-badge
                v-else-if="data.item.user.state === 2"
                pill
                class="text-capitalize"
                :variant="`warning`"
            >
              فالو نکرده
            </b-badge>
            <b-badge
                v-else-if="data.item.user.state === 3"
                pill
                class="text-capitalize"
                :variant="`danger`"
            >
              آیدی اشتباه
            </b-badge>
          </template>

<!--          &lt;!&ndash; Column: buy &ndash;&gt;-->
<!--          <template #cell(buy)="data">-->
<!--            <b-link :to="{ name: 'apps-buy-id', params: { id: data.item.userId } }">-->
<!--              <feather-icon icon="ShoppingCartIcon" size="20"/>-->
<!--            </b-link>-->
<!--          </template>-->

          <!-- Column: chat -->
          <template #cell(chat)="data">
            <b-link :to="{ name: 'apps-chats-id', params: { id: data.item.user.userId } }">
              <feather-icon icon="MessageCircleIcon" size="20"/>
            </b-link>
          </template>

          <!-- Column: state -->
          <template #cell(order.isPayed)="data">
            <b-badge
                v-if="data.item.order.isPayed === true"
                pill
                class="text-capitalize"
                :variant="`success`"
            >
              پرداخت شده
            </b-badge>
            <b-badge
                v-else-if="data.item.order.isPayed === false"
                pill
                class="text-capitalize"
                :variant="`danger`"
            >
               پرداخت نشده
            </b-badge>
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
import OrderGetRegisteredUserRequest from "@/libs/Api/Order/OrderGetRegisteredUserRequest";

export default {
  name: "StudentInfoes",
  title:"لیست دانشجویان - پنل ادمین بانو کلاب",
  data(){
    return{
      students:null,
      productId:this.$route.params.id,
      currentPage:1,
      totalCounts:null,
      perPage:10,
      perPageOptions:[10, 20, 30 , 40 , 50],
      myTableColumns : [
        { key: 'user.name' ,label :'نام کامل'},
        { key: 'user.nationalId' ,label :'کد ملی'},
        { key: 'user.mobile' ,label :'موبایل'},
        { key: 'user.instagramId' ,label :'اینستاگرام'},
        { key: 'user.state' ,label :'وضعیت کاربر'},
        // { key: 'buy',label:'ثبت سفارش'},
        { key: 'chat',label:'چت'},
        { key: 'order.isPayed' ,label :'وضعیت سفارش'},
      ],
      searchCommand:'',
      isAddNewUserSidebarActive:false,
    }
  },
  async created() {
    await this.getStudents(this.perPage,this.currentPage,this.productId)
  },
  methods:{
    refreshData(){
      this.getStudents(this.perPage,this.currentPage,this.productId)
    },
    async getStudents(count,pageNumber,productId){
      let _this = this;
      let data = {
        pageNumber:pageNumber,
        count:count,
        productId:productId
      }

      let orderGetRegisteredUserRequest = new OrderGetRegisteredUserRequest(_this);
      orderGetRegisteredUserRequest.setParams(data);
      await orderGetRegisteredUserRequest.fetch(function (content){
        _this.students = content.result;
        _this.totalCounts = content.dbOrdersCount;
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
      this.getStudents(nv,this.currentPage,this.productId);
    },
    currentPage:function (nv,ov) {
      this.getStudents(this.perPage,nv,this.productId);
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
