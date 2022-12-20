<template>
  <b-overlay
      :show="conversations === null"
      rounded="sm"
  >
    <div v-if="conversations !== null">

      <!-- Table Container Card -->
      <b-card
          no-body
          class="mb-0"
      >
        <b-row>
          <!-- Search -->
          <b-col
              cols="12"
              md="6"
              class="m-2"
          >
            <div class="d-flex align-items-center justify-content-end">
              <b-form-input
                  v-model="search"
                  class="d-inline-block mr-1"
                  placeholder="جستجو..."
              />
            </div>
          </b-col>
        </b-row>

        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="conversations"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >

          <template #cell(userId)="data">
            <b-media vertical-align="center">
              <template #aside>
                <b-avatar
                    size="45"
                    :src="`https://api.halamkhoobee.com/${data.item.userPhoto}`"
                    :variant="`success`"
                    :to="{ name: 'apps-chats-id', params: { id: data.item.userId } }"
                />
              </template>
              <b-link
                  :to="{ name: 'apps-chats-id', params: { id: data.item.userId } }"
                  class="font-weight-bold d-block text-nowrap"
              >
                {{ data.item.userName }}
              </b-link>
              <small class="text-muted">@{{ data.item.userId }}</small>
            </b-media>
          </template>

          <template #cell(edit)="data">

            <router-link :to="{ name: 'apps-chats-id', params: { id: data.item.userId } }" class="cursor-pointer d-flex flex-row w-25">
              <feather-icon icon="EditIcon" size="20"/>
            </router-link>

          </template>

          <template #cell(unReadCount)="data">

            <b-badge
                v-if="data.item.unReadCount !== 0"
                pill
                class="text-capitalize"
                :variant="`primary`"
            >
              {{data.item.unReadCount}}
            </b-badge>
            <span v-else>{{data.item.unReadCount}}</span>

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
<!--            <b-col-->
<!--                cols="12"-->
<!--                sm="6"-->
<!--                class="d-flex align-items-center justify-content-center justify-content-sm-end"-->
<!--            >-->

<!--              <b-pagination-->
<!--                  v-model="currentPage"-->
<!--                  :total-rows="totalCount"-->
<!--                  :per-page="perPage"-->
<!--                  first-number-->
<!--                  last-number-->
<!--                  class="mb-0 mt-1 mt-sm-0"-->
<!--                  prev-class="prev-item"-->
<!--                  next-class="next-item"-->
<!--              >-->
<!--                <template #prev-text>-->
<!--                  <feather-icon style="transform: rotate(180deg)"-->
<!--                                icon="ChevronLeftIcon"-->
<!--                                size="18"-->
<!--                  />-->
<!--                </template>-->
<!--                <template #next-text>-->
<!--                  <feather-icon-->
<!--                      icon="ChevronRightIcon"-->
<!--                      size="18"-->
<!--                  />-->
<!--                </template>-->
<!--              </b-pagination>-->

<!--            </b-col>-->

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
import MessageGetMenuRequest from "@/libs/Api/Message/MessageGetMenuRequest";
import MessageDeliverRequest from "@/libs/Api/Message/MessageDeliverRequest";
import { mapGetters } from "vuex";
export default {
  name: "Chats",
  title: 'چت ها - پنل ادمین بانو کلاب',
  data(){
    return{
      conversations:null,
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
      search:'',
      myTableColumns : [
        { key: 'userId',label:'اطلاعات کاربر'},
        { key: 'unReadCount',label:'پیام های خوانده نشده'},
        // { key: 'userName',label:'نام کاربر'},
        { key: 'edit',label:'نمایش'},
      ],
    }
  },
  computed:{
    ...mapGetters(["SocketId"]),
  },
  watch:{
    SocketId: function (val, oldVal) {
      this.getMenu(this.search);
      this.$store.commit("SetDefaultSocketId");
    },
    search:function (nv,ov){
      this.getMenu(nv)
    }
  },
  async created(){
    await this.getMenu(this.search);
  },
  methods:{
    async getMenu(search){
      let _this = this;

      let messageGetMenuRequest = new MessageGetMenuRequest(_this);
      messageGetMenuRequest.setRequestParam(search)
      await messageGetMenuRequest.fetch(function (content){
        _this.conversations = content.filter(_this.onlyUnique);
        _this.deliverMessages();
      },function (error){
        console.log(error)
      })
    },
    async deliverMessages(){
      let _this = this;

      let messageDeliverRequest = new MessageDeliverRequest(_this);
      await messageDeliverRequest.fetch(function (content){

      },function (error){
        console.log(error)
      })
    },
    showModal(param){
      let _this = this;

      _this.showDeleteModal = true;
      _this.deleteItem = param
    },
    onlyUnique(value, index, self) {
      return self.indexOf(value) === index;
    },
  },
  // watch:{
  //   currentPage:function (nv,ov) {
  //     this.getTickets(nv,this.defaultGetData.count);
  //   },
  // },
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
