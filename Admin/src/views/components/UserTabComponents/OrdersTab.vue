<template>
  <b-overlay :show="RecievedData === null" rounded="sm">
    <b-table
        ref="refUserListTable"
        class="position-relative"
        :items="RecievedData"
        responsive
        :fields="myTableColumns"
        primary-key="id"
        show-empty
        bordered
        striped
        empty-text="موردی موجود نیست!"
    >

      <!-- Column: delete -->
      <template #cell(subOrders)="data">

        <div v-for="(el,idx) in data.item.subOrders" :key="idx" class="d-flex flex-column">
          <small>{{el.title}}</small>
        </div>


      </template>


      <template #cell(OrderTitle)="data">

        <small v-if="data.item.adsInfo!==null"> برای خدمت {{data.item.adsInfo.title}} </small>
        <small v-if="data.item.serviceInfo!==null"> برای آگهی {{data.item.serviceInfo.title}} </small>
        <small v-else-if="data.item.adsInfo===null && data.item.serviceInfo===null"> - </small>

      </template>
      <template #cell(createDate)="data">

        <small>{{new Date(data.item.createDate).toLocaleDateString('fa-IR')}}</small>

      </template>
      <template #cell(status)="data">
        <b-badge v-if="data.item.status ===3" variant="danger">
          لغو شده
        </b-badge>
        <b-badge v-else-if="data.item.status === 1" variant="success">
          درحال پردازش
        </b-badge>
        <b-badge v-else-if="data.item.status === 2" variant="secondary">
          پرداخت شده
        </b-badge>


      </template>
      <template #cell(sumPrice)="data">
    <small>{{     new Intl.NumberFormat().format(data.item.sumPrice)}}</small>
    <small>تومان</small>
      </template>


    </b-table>
    <div class="mx-2 mb-2">
      <b-row>
        <!-- Pagination -->
        <b-col
            cols="12"
            sm="6"
            class="d-flex align-items-center justify-content-center justify-content-sm-end"
        >

          <b-pagination
              v-model="currentPage"
              :total-rows="totalCount"
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
  </b-overlay>
</template>

<script>
import {GetOrdersByUserId} from "@/libs/Api/Users/UserMethods";
import {BOverlay,BTable,BPagination,BRow,BCol,BBadge} from "bootstrap-vue";
export default {
  name: "ForumsTab",
  props:['userId'],
  components:{BOverlay,BTable,BPagination,BRow,BCol,BBadge},
  data(){
    return{
      RecievedData:null,
      currentPage:1,
      perPage:10,
      totalCount:0,
      myTableColumns: [
        {
          key: 'orderId',
          label: 'شناسه'
        },
        {
          key: 'subOrders',
          label: 'نام سفارش'
        },
        {
          key: 'description',
          label: 'توضیحات'
        },

        {
          key: 'OrderTitle',
          label: 'توضیحات سفارش'
        },

        {
          key: 'createDate',
          label: ' تاریخ'
        },
        {
          key: 'status',
          label: 'وضعیت'
        },
        {
          key: 'sumPrice',
          label: 'مجموع'
        },
        // { key: 'parentId'},
      ],
    }
  },

  async mounted(){
    await this.GetDataByUserId()
  },
  watch:{
    currentPage:function (val){
      this.GetDataByUserId()
    }
  },

  methods:{
    async GetDataByUserId(){

      let _this = this
      let  getOrdersByUserId = new GetOrdersByUserId()
      let data = {
        userId:this.userId,
        count:this.perPage,
        pageNumber:this.currentPage
      }
      getOrdersByUserId.setParams(data)
      await getOrdersByUserId.fetch((content)=>{
        _this.RecievedData  = content.orders
        _this.totalCount = content.orderCount
        console.log(content)
      },(e)=>{
        console.log(e)
      })
    },
  }

}
</script>

<style scoped>

</style>
