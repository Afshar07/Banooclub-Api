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

      <template #cell(status)="data">

        <div class="cursor-pointer d-flex flex-row"
             v-b-modal.modal-edit

        >
          <b-badge v-if="data.item.status===1" pill variant="success">
           فعال
          </b-badge>
          <b-badge v-if="data.item.status===2" pill variant="secondary">
            غیر فعال
          </b-badge>
          <b-badge v-if="data.item.status===3" pill variant="danger">
            گزارش شده توسط ربات
          </b-badge>
          <b-badge v-if="data.item.status===4" pill variant="danger">
            گزارش شده توسط کاربران
          </b-badge>

        </div>

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
import {GetForumByUserId} from "@/libs/Api/Users/UserMethods";
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
        { key: 'forumId' ,label :'شناسه '},
        { key: 'title' ,label :'موضوع'},
        { key: 'createDate' ,label :'تاریخ ایجاد'},
        { key: 'status',label:'وضعیت'},
        { key: 'vote',label:'امتیاز'},
      ],
    }
  },

  async mounted(){
    await this.GetForumByUserId()
  },

  methods:{
    async GetForumByUserId(){

      let _this = this
      let  getForumByUserId = new GetForumByUserId()
      let data = {
        userId:this.userId,
        count:this.perPage,
        pageNumber:this.currentPage
      }
      getForumByUserId.setParams(data)
      await getForumByUserId.fetch((content)=>{
        _this.RecievedData  = content.forums
        _this.totalCount = content.forumCount
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
