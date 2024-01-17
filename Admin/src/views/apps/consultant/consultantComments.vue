<template>
  <b-overlay
      :show="overlay"
      rounded="sm"
  >
    <div v-if="comments !== null">
      <b-modal
          id="modal-View"
          cancelTitle="بستن"
          centered
          ok-title="تایید"
          @ok="changeStatus"
      >
        <div v-if="commentDescription!==''" class="row">
          <div class="col-md-12">
            <span>{{ commentDescription }}</span>
          </div>
        </div>
      </b-modal>
      <b-modal
          id="modal-delete"
          cancelTitle="بستن"
          centered
          ok-title="ثبت"
          @ok="deleteComment"
      >
        <div class="row">
          <span>آیا از حذف این نظر اطمینان دارید؟</span>
        </div>
      </b-modal>
      <!-- Table Container Card -->
      <b-card
          class="mb-0"
      >

        <b-row>
          <b-col class="mb-2" cols="12" >
            <b-row>
              <b-col cols="12" md="5">
                <b-form-input
                    v-model="debouncer"
                    placeholder="نام مشاور"
                    type="text"
                />
              </b-col>
            </b-row>
          </b-col>
          <b-col cols="12">
            <b-table
                ref="refUserListTable"
                :fields="myTableColumns"
                :items="comments"
                bordered
                class="position-relative"
                empty-text="موردی موجود نیست!"
                primary-key="id"
                responsive
                show-empty
                striped
            >
              <template #cell(createDate)="data">

                <span>{{ new Date(data.item.createDate).toLocaleDateString('fa-IR') }}</span>


              </template>
              <template #cell(isConfirmed)="data">
                <b-badge
                    v-if="data.item.isConfirm"
                    pill
                    variant="primary"
                >
                  تایید شده
                </b-badge>
                <b-badge
                    v-if="!data.item.isConfirm"
                    pill
                    variant="danger"
                >
                 تایید نشده
                </b-badge>

              </template>
              <!-- Column: delete -->


              <template #cell(Delete)="data">

                <b-link v-b-modal.modal-delete class="cursor-pointer" @click="setSelectedComment(data.item)">
                  <feather-icon class="text-danger" icon="TrashIcon" size="20"/>
                </b-link>

              </template>
              <template #cell(View)="data">

                <b-link v-b-modal.modal-View class="cursor-pointer" @click="setSelectedComment(data.item)">
                  <feather-icon class="text-primary" icon="EyeIcon" size="20"/>
                </b-link>

              </template>

            </b-table>
          </b-col>
          <b-col
              class="d-flex align-items-center justify-content-end "
              cols="12"
              sm="6"
          >

            <b-pagination
                v-model="currentPage"
                :per-page="perPage"
                :total-rows="totalCount"
                class="mb-0 mt-1 mt-sm-0"
                first-number
                last-number
                next-class="next-item"
                prev-class="prev-item"
            >
              <template #prev-text>
                <feather-icon icon="ChevronLeftIcon"
                              size="18"
                              style="transform: rotate(180deg)"
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

      </b-card>
    </div>


  </b-overlay>
</template>

<script>

import {
  BCard, BRow, BCol, BFormInput, BButton, BTable, BMedia, BAvatar, BLink,
  BBadge, BDropdown, BDropdownItem, BPagination, BOverlay, BModal, BFormGroup, BFormSelect, BTabs, BTab, BCardText
} from 'bootstrap-vue'
import vSelect from 'vue-select'

import ForumGetAllRequest from '@/libs/Api/Forum/ForumGetAllRequest'
import DeleteForum from '@/libs/Api/Forum/DeleteForum'
import ChangeForumStatus from '@/libs/Api/Forum/ChangeForumStatus'
import ToastificationContent from '@core/components/toastification/ToastificationContent'
import {
  ConfirmConsultantComment,
  ConfirmConsultantRequest, DeleteConsultantComment,
  GetAllComments,
  GetAllConsultantComments,
  GetCommentDescription
} from "@/libs/Api/consultant";

export default {
  components: {

    BCard,
    BRow,
    BCol,
    BFormInput,
    BFormGroup,
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
    BFormSelect,
    BCardText,
    BTabs,
    BTab,
    vSelect,
  },
  title: 'لیست  نظرات مشاوران  - پنل ادمین بانو کلاب',
  name: 'UsersList',

  data() {
    return {
      comments: null,
      totalCount: null,
      currentPage: 1,
      status: 1,
      selectedStatus: 0,
      overlay: false,
      perPage: 10,
      commentDescription: '',
      createDate: '',
      search: '',

      myTableColumns: [
        {
          key: 'id',
          label: 'شناسه'
        },
        {
          key: 'createDate',
          label: 'تاریخ ایجاد'
        },
        {
          key: 'rate',
          label: 'امتیاز'
        },
        {
          key: 'isConfirmed',
          label: 'وضعیت نظر'
        },
        {
          key: 'consultFullname',
          label: 'نام مشاور'
        },
        {
          key: 'userFullname',
          label: 'نام مشاوره گیرنده'
        },
        {
          key: 'Delete',
          label: 'حذف'
        },
        {
          key: 'View',
          label: 'مشاهده'
        },
        // { key: 'parentId'},
      ],
      selectedComment: null,
      timeout:null

    }
  },
  async created() {
    await this.getAlLComments()
  },
  watch:{
    search:function (val){
      this.getAlLComments()
    }
  },
  computed:{
    debouncer:{
      get() {
        return this.search
      },
      set(val) {
        if (this.timeout) {
          clearTimeout(this.timeout)
        }
        this.timeout = setTimeout(() => {
          this.search = val
        }, 600)
      }
    }
  },
  methods: {
    async changeStatus() {
      let _this = this
      let confirmConsultantComment = new ConfirmConsultantComment(_this)
      let data = {
        id: _this.selectedComment.id,
      }
      confirmConsultantComment.setParams(data)
      await confirmConsultantComment.fetch(function (content) {
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `نظر با موفقیت تایید شد`,
          },
        })
        _this.getAlLComments()
      }, function (error) {
        console.log(error)
      })
    },

    async deleteComment() {
      let _this = this
      let deleteConsultantComment = new DeleteConsultantComment(_this)
      let data = {
        id: _this.selectedComment.id,
      }
      deleteConsultantComment.setParams(data)
      await deleteConsultantComment.fetch(function (content) {
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `نظر با موفقیت حذف شد`,
          },
        })
        _this.getAlLComments()
      }, function (error) {
        console.log(error)
      })
    },
    setSelectedComment(item) {
      this.selectedComment = item
      this.getCommentDescription()
    },
    async getCommentDescription() {
      let _this = this
      let getCommentDescription = new GetCommentDescription(_this)
      let data = {
        id: this.selectedComment.id
      }
      getCommentDescription.setParams(data)
      await getCommentDescription.fetch(function (content) {
        _this.commentDescription = content.description

      }, function (error) {
        console.log(error)
      })
    },
    async getAlLComments() {
      let _this = this
      _this.overlay = true
      let getAllConsultantComments = new GetAllConsultantComments(_this)
      let data = {
        pageNumber: _this.currentPage,
        take: 10,
        consultFullname: _this.search,
        createDate: this.createDate,
      }
      getAllConsultantComments.setParams(data)
      await getAllConsultantComments.fetch(function (content) {
        _this.comments = content.data
        _this.totalCount = content.total
      }, function (error) {
        console.log(error)
      })
      _this.overlay = false
    },

  },
}
</script>

<style scoped>

</style>
