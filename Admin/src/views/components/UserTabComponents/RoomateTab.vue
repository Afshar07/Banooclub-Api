<template>
  <b-overlay :show="Roomate === null" rounded="sm">
    <b-table
        ref="refUserListTable"
        class="position-relative"
        :items="Roomate"
        responsive
        :fields="myTableColumns"
        primary-key="id"
        show-empty
        bordered
        striped
        empty-text="موردی موجود نیست!"
    >

      <!-- Column: delete -->

      <template #cell(receptionDate)="data">

        <div class="cursor-pointer d-flex flex-row"


        >
       <small>{{new Date(data.item.receptionDate).toLocaleDateString()}}</small>
        </div>

      </template>
      <template #cell(mortgage)="data">

        <div class="cursor-pointer d-flex flex-row"


        >
          <small>{{ new Intl.NumberFormat().format(data.item.mortgage)}} تومان</small>
        </div>

      </template>
      <template #cell(dailyRent)="data">

        <div class="cursor-pointer d-flex flex-row"


        >
          <small>{{ new Intl.NumberFormat().format(data.item.dailyRent)}} تومان</small>
        </div>

      </template>
      <template #cell(receptionDate)="data">

        <div class="cursor-pointer d-flex flex-row"


        >
          <small>{{new Date(data.item.receptionDate).toLocaleDateString()}}</small>
        </div>

      </template>

      <template #cell(View)="data">

        <div class="cursor-pointer d-flex flex-row"
             v-b-modal.modal-edit

        >
          <feather-icon class="text-primary" icon="EyeIcon" size="20" />
        </div>

      </template>

    </b-table>
  </b-overlay>
</template>

<script>
import {GetRoomateByUserId} from "@/libs/Api/Users/UserMethods";
import {BOverlay,BTable,BPagination,BRow,BCol} from "bootstrap-vue";
export default {
  name: "RoomateTab",
  props:['userId'],
  components:{BOverlay,BTable,BPagination,BRow,BCol},
  data(){
    return{
      Roomate:null,
      myTableColumns : [
        { key: 'roomateId' ,label :'شناسه '},
        { key: 'receptionDate' ,label :'تاریخ پذیرش'},
        { key: 'dailyRent' ,label :'مبلغ اجاره '},
        { key: 'mortgage',label:'مبلغ رهن'},
        { key: 'View',label:'مشاهده'},
      ],
    }
  },

  async mounted(){
    await this.GetRoomate()
  },

  methods:{
   async GetRoomate(){

      let _this = this
    let  getRoomateByUserId = new GetRoomateByUserId()
     let data = {
        userId:this.userId
     }
     getRoomateByUserId.setParams(data)
     await getRoomateByUserId.fetch((content)=>{
       _this.Roomate  = [content.roomate]
       console.log(_this.Roomate)
     },(e)=>{
       console.log(e)
     })
    },
  }

}
</script>

<style scoped>

</style>
