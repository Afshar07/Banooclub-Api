<template>
  <b-overlay :show="RecievedData === null" rounded="sm">
    <b-modal
        id="modal-View"
        centered
        ok-title="بستن"
        ok-only

    >
      <div v-if="SelectedPost" class="row">
        <div class="col-md-12">
          <img v-if="SelectedPost.medias.length>0" style="width: 100%;height: 400px;object-fit: contain" :src="`https://subapi.banoclub.com/media/gallery/post/${SelectedPost.medias[0].base64}`" alt="">
        </div>
        <div class="col-md-12 mt-3 p-2 border-top shadow">
          {{SelectedPost.description}}
        </div>
      </div>

    </b-modal>
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

        <span v-if="data.item.status===1" class="badge pill bg-success text-white">
          <small>فعال</small>
        </span>

      </template>

      <template #cell(View)="data">

        <feather-icon @click="SetSelectedPost(data.item)" v-b-modal.modal-View icon="EyeIcon" class="text-primary"></feather-icon>

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
import {GetServicesByUserName} from "@/libs/Api/Users/UserMethods";
import {BOverlay,BTable,BPagination,BRow,BCol,BBadge,BModal} from "bootstrap-vue";
export default {
  name: "ForumsTab",
  props:['userName'],
  components:{BOverlay,BTable,BPagination,BRow,BCol,BBadge,BModal},
  data(){
    return{
      RecievedData:null,
      currentPage:1,
      perPage:10,
      Search:'',
      totalCount:0,
      lastId:0,
      SelectedPost:null,
      myTableColumns : [
        { key: 'servicePackId' ,label :'شناسه '},
        { key: 'title' ,label :'نام'},
        { key: 'createDate' ,label :'تاریخ ایجاد'},
        { key: 'status',label:'وضعیت'},
        { key: 'View',label:'مشاهده'},
      ],
    }
  },

  async mounted(){
    await this.GetDataByUserId()
  },

  methods:{
    SetSelectedPost(item){
      this.SelectedPost = item
    },
    async GetDataByUserId(){

      let _this = this
      let  getServicesByUserName = new GetServicesByUserName()
      let data = {
        lastId:_this.lastId,
        count:_this.perPage,
        searchCommand:'',
        userName:_this.userName
      }
      getServicesByUserName.setParams(data)
      await getServicesByUserName.fetch((content)=>{
        _this.RecievedData  = content.services
        _this.totalCount = content.servicesCount
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
