<template>
  <b-overlay
      :show="overlay"
      rounded="sm"
  >
    <div v-if="comments !== null">
<!--      <b-modal-->
<!--          id="modal-View"-->
<!--          cancelTitle="بستن"-->
<!--          centered-->
<!--          ok-title="تغییر وضعیت"-->
<!--          @ok="OpenChangeStatus"-->
<!--      >-->
<!--        <div v-if="SelectedForum!==null" class="row">-->
<!--          <div class="col-md-12">-->
<!--            <div class="d-flex align-items-center gap-2">-->
<!--              <span class="text-secondary">نام تالار : </span>-->
<!--              <span>{{ SelectedForum.title }}</span>-->
<!--            </div>-->
<!--          </div>-->
<!--          <div class="col-md-12 my-2">-->
<!--            <div class="text-secondary" style="overflow-wrap: anywhere">-->
<!--              {{ SelectedForum.description }}-->
<!--            </div>-->
<!--          </div>-->
<!--        </div>-->
<!--      </b-modal>-->
      <!-- Table Container Card -->
      <b-card
          class="mb-0"
          no-body
      >

        <b-row>
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
              <!-- Column: delete -->

              <template #cell(userInfo)="data">
                <router-link :to="`/apps/users/Detail/${data.item.userInfo.userName}`">
                  <span v-if="data.item.userInfo">{{ data.item.userInfo.userName }}@</span>
                </router-link>

              </template>
              <template #cell(View)="data">

                <b-link v-b-modal.modal-View class="cursor-pointer" @click="setSelectedComment(data.item)">
                  <feather-icon class="text-primary" icon="EyeIcon" size="20"/>
                </b-link>

              </template>

            </b-table>
          </b-col>
          <b-col
              class="d-flex align-items-center justify-content-center justify-content-sm-end"
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
      overlay: false,
      perPage: 10,
      myTableColumns: [
        {
          key: 'forumId',
          label: 'شناسه'
        },
        {
          key: 'createDate',
          label: 'تاریخ ایجاد'
        },
        {
          key: 'title',
          label: 'نام'
        },
        {
          key: 'userInfo',
          label: 'نام کاربری'
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

    }
  },
  async created() {
    await this.getAlLComments()
  },


  methods: {
    // async ChangeStatus() {
    //   let _this = this
    //   let changeForumStatus = new ChangeForumStatus(_this)
    //   let data = {
    //
    //     status: this.SelectedForumStatus,
    //     forumId: this.SelectedForum.forumId
    //   }
    //   changeForumStatus.setParams(data)
    //   await changeForumStatus.fetch(function (content) {
    //     _this.$toast({
    //       component: ToastificationContent,
    //       position: 'bottom-center',
    //       props: {
    //         title: `عملیات موفق`,
    //         icon: 'CheckIcon',
    //         variant: 'success',
    //         text: `وضعیت تالار با موفقیت تغییر یافت`,
    //       },
    //     })
    //     _this.GetAllForum()
    //   }, function (error) {
    //     console.log(error)
    //   })
    // },
    setSelectedComment(item) {
      this.selectedComment = item
    },
    async getAlLComments() {
      let _this = this
      let forumGetAllRequest = new ForumGetAllRequest(_this)
      let data = {
        pageNumber: _this.currentPage,
        search: _this.search,
        noComments: '',
        mostRated: '',
        mostComments: '',
        status: this.status
      }
      forumGetAllRequest.setParams(data)
      await forumGetAllRequest.fetch(function (content) {
        _this.Forums = content.forums
        _this.totalCount = content.forumsCount
      }, function (error) {
        console.log(error)
      })
    },

  },
}
</script>

<style scoped>

</style>
