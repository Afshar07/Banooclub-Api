<template>
  <b-overlay
      :show="tickets === null"
      rounded="sm"
  >
    <div v-if="tickets !== null">

      <!-- Table Container Card -->
      <b-card
          no-body
          class="mb-0"
      >

        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="tickets"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >

          <!-- Column: isRead -->

          <template #cell(isRead)="data">

            <div class="d-flex flex-row">
              <span v-if="data.item.isRead" class="text-success ">خوانده شده</span>
              <span v-else class="text-warning">خوانده نشده</span>
<!--              <feather-icon icon="TrashIcon" class="text-danger" />-->
            </div>

          </template>

          <!-- Column: delete -->

          <template #cell(delete)="data">

            <div class="cursor-pointer d-flex flex-row w-25"
                 @click="showModal(data.item.ticketId)"
            >
              <feather-icon icon="TrashIcon" class="text-danger" />
            </div>

          </template>

          <template #cell(edit)="data">

            <router-link :to="{ name: 'apps-tickets-id', params: { id: data.item.ticketId } }" class="cursor-pointer d-flex flex-row w-25">
              <feather-icon icon="EditIcon" />
            </router-link>

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
      </b-card>
    </div>
    <!-- modal vertical center -->
    <b-modal
        v-if="showDeleteModal"
        id="modal-center"
        centered
        title="ویرایش خصوصیت"
        ok-title="حذف"
        cansel-title="انصراف"
        @cancel="showDeleteModal = !showDeleteModal"
        @ok="deleteTicket(deleteItem)"

    >
     <span>حذف شود؟</span>
    </b-modal>
  </b-overlay>
</template>

<script>
import {
  BCard, BRow, BCol, BFormInput, BButton, BTable, BMedia, BAvatar, BLink,
  BBadge, BDropdown, BDropdownItem, BPagination,BOverlay,BModal
} from 'bootstrap-vue'
import TicketGetAllRequest from "@/libs/Api/Ticket/TicketGetAllRequest";
import ToastificationContent from "@core/components/toastification/ToastificationContent";
import TicketDeleteRequest from "@/libs/Api/Ticket/TicketDeleteRequest";
export default {
  name: "Tickets",
  data(){
    return{
      tickets:null,
      totalCount:null,
      showDeleteModal:false,
      deleteItem : null,
      currentPage:1,
      perPage:10,
      defaultGetData:{
        pageNumber:1,
        count:10,
      },
      myTableColumns : [
        { key: 'content',label:'محتوا'},
        { key: 'isRead',label:'وضعیت'},
        { key: 'delete',label:'حذف'},
        { key: 'edit',label:'نمایش'},
      ],
    }
  },
  async created(){
    await this.getTickets(this.defaultGetData.pageNumber,this.defaultGetData.count);
  },
  methods:{
    async getTickets(pageNumber,count){
      let _this = this;
      let defaultGetData = {
        pageNumber : pageNumber,
        count: count
      }

      let ticketGetAllRequest = new TicketGetAllRequest(_this);
      ticketGetAllRequest.setParams(defaultGetData);
      await ticketGetAllRequest.fetch(function (content){
        _this.tickets = content.tickets;
        _this.totalCount = content.ticketCount;
      },function (error){
        console.log(error)
      })
    },
    async deleteTicket(param){
      let _this = this;

      let ticketDeleteRequest = new TicketDeleteRequest(_this);
      ticketDeleteRequest.setId(param);
      await ticketDeleteRequest.fetch(function (content){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `دسته بندی جدید حذف شد.`,
          },
        })
        _this.getTickets(_this.defaultGetData.pageNumber,_this.defaultGetData.count)
      },function (error){
        console.log(error)
      })
    },
    showModal(param){
      let _this = this;

      _this.showDeleteModal = true;
      console.log(_this.showDeleteModal)
      _this.deleteItem = param
    },
  },
  watch:{
    currentPage:function (nv,ov) {
      this.getTickets(nv,this.defaultGetData.count);
    },
  },
  components:{
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
    BModal,
  },
}
</script>

<style scoped>

</style>