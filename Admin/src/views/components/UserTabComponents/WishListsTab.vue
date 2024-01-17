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

      <template #cell(createDate)="data">

        <div class="cursor-pointer d-flex flex-row"


        >
          <small>{{new Date(data.item.createDate).toLocaleDateString('fa-IR')}}</small>
        </div>

      </template>

      <template #cell(Info)="data">

      <small v-if="data.item.type===1">{{data.item.serviceInfo.title}}</small>
        <small v-else >{{data.item.adsInfo.title}}</small>
      </template>
      <template #cell(View)="data">

    <feather-icon icon="EyeIcon" class="text-primary"></feather-icon>
      </template>
      <template #cell(type)="data">

        <small v-if="data.item.type===1">خدمت</small>
        <small v-else >آگهی</small>
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
import {GetWishlistByUserId} from "@/libs/Api/Users/UserMethods";
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
      myTableColumns : [
        { key: 'wishListId' ,label :'شناسه '},
        { key: 'Info' ,label :'نام'},
        { key: 'type' ,label :'نوع علاقه مندی'},
        { key: 'View',label:'مشاهده'},
      ],
    }
  },

  async mounted(){
    await this.GetForumByUserId()
  },

  methods:{
    async GetForumByUserId(){

      let _this = this
      let  getWishlistByUserId = new GetWishlistByUserId()
      let data = {
        userId:this.userId,
      }
      getWishlistByUserId.setParams(data)
      await getWishlistByUserId.fetch((content)=>{
        let Content = []
        content.adsWishes.forEach((item)=>{
          Content.push(item)
        })
        content.serviceWishes.forEach((item)=>{
          Content.push(item)
        })
        _this.RecievedData  =Content
        // _this.totalCount = content.forumCount
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
