<template>
  <b-overlay
      :show="PostNk === null"
      rounded="sm"
  >
    <div v-if="PostNk !== null">
      <b-modal
          id="modal-delete"
          centered
          ok-title="بله"

          cancelTitle="خیر"
          @ok="DeleteNk"
      >
        <span>آیا از حذف کردن این کلمه اطمینان دارید ؟ </span>
      </b-modal>
      <b-modal
          id="modal-edit"
          centered
          ok-title="ویرایش"
          ok-only

          @ok="UpdateNk"
      >
        <b-form-group>
          <label for="email">نام کلمه</label>
          <b-form-input
              v-if="SelectedPostNk!==null"
              id="email"
              type="text"
              placeholder="نام دسته بندی"
              v-model="SelectedPostNk.name"
          />

        </b-form-group>

      </b-modal>
      <b-modal
          id="modal-Add"
          centered
          ok-title="افزودن کلمه منفی"
          ok-only

          @ok.preventDefault()="AddPostNk"
      >
        <b-form-group>
          <label for="email">کلمه</label>
          <b-form-input
              type="text"
              placeholder="کلمه"
              v-model="PostNkName"
          />
        </b-form-group>

      </b-modal>
      <!-- Table Container Card -->
      <b-card
          no-body
          class="mb-0"
      >
        <b-col lg="12" md="12" class="my-2">
          <b-row>
            <b-col lg="10" md="12" class="my-2">
              <b-form-input
                  id="basicInput"
                  v-model="search"
                  placeholder="جستجو بر اساس کلمه"
              />
            </b-col>
            <b-col lg="2" md="12" class="my-2">
              <b-button

                  variant="success"
                  v-b-modal.modal-Add

              >
                افزودن کلمه
              </b-button>
            </b-col>
          </b-row>
          <b-form-group

          >

          </b-form-group>
        </b-col>


        <b-table
            ref="refUserListTable"
            class="position-relative"
            :items="PostNk"
            responsive
            :fields="myTableColumns"
            primary-key="id"
            show-empty
            bordered
            striped
            empty-text="موردی موجود نیست!"
        >

          <!-- Column: delete -->

          <template #cell(Delete)="data">

            <div class="cursor-pointer d-flex flex-row"
                 v-b-modal.modal-delete
                 @click="SetSelectedPostNk(data.item)"
            >
              <feather-icon icon="TrashIcon" size="20" class="text-danger" />
            </div>

          </template>

          <template #cell(Edit)="data">

            <div class="cursor-pointer d-flex flex-row"
                 v-b-modal.modal-edit
                 @click="SetSelectedPostNk(data.item)"
            >
              <feather-icon class="text-primary" icon="EditIcon" size="20" />
            </div>

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


  </b-overlay>
</template>

<script>

import {
  BCard, BRow, BCol, BFormInput, BButton, BTable, BMedia, BAvatar, BLink,
  BBadge, BDropdown, BDropdownItem, BPagination, BOverlay, BModal, BFormGroup,BFormSelect
} from 'bootstrap-vue'
import vSelect from 'vue-select'

import GetAllPostNk from '@/libs/Api/PostNk/GetAllPostNk'
import DeletePostNk from '@/libs/Api/PostNk/DeletePostNk'
import AddPostNk from '@/libs/Api/PostNk/AddPostNk'
import UpdatePostNk from '@/libs/Api/PostNk/UpdatePostNk'

import ToastificationContent from '@core/components/toastification/ToastificationContent'

export default {
  title: 'لیست کلمات منفی  - پنل ادمین بانو کلاب',
  name: 'UsersList',
  data() {
    return {
      PostNk: null,
      totalCount: null,
      showDeleteModal: false,
      currentPage: 1,
      deleteItem: null,
      perPage: 10,
      CategoryName:'',
      perPageOptions: [10, 20, 30, 40, 50],
      myTableColumns: [
        {
          key: 'postNkId',
          label: 'شناسه'
        },
        {
          key: 'name',
          label: 'نام'
        },
        {
          key: 'Delete',
          label: 'حذف'
        },
        {
          key: 'Edit',
          label: 'ویرایش'
        },
        // { key: 'parentId'},
      ],

      pageNumber: 1,
      count: 10,
      PostNkName:'',
      search: '',
      SelectedPostNk: null,


    }
  },
  async created() {
    await this.GetAllPostNk()
  },
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
    vSelect,
  },
  watch: {
    search: function () {
      this.GetAllPostNk()
    },
    currentPage: function () {
      this.GetAllPostNk()
    }
  },
  methods: {
    async UpdateNk(){
      let _this = this
      let updatePostNk = new UpdatePostNk(_this)
      let data = {
        postNkId:this.SelectedPostNk.postNkId,
        name: this.SelectedPostNk.name,
      }
      updatePostNk.setRequestParamDataObj(data)
      await updatePostNk.fetch(function (content) {
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `کلمه بروزرسانی شد`,
          },
        })
        _this.GetAllPostNk();
      }, function (error) {
        console.log(error)
      })
    },
    async AddPostNk(){
      let _this = this
      let addPostNk = new AddPostNk(_this)
      let data = {
        name: this.PostNkName,
      }
      addPostNk.setRequestParamDataObj(data)
      await addPostNk.fetch(function (content) {
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `دسته بندی بروزرسانی شد`,
          },
        })
        _this.GetAllPostNk();
      }, function (error) {
        console.log(error)
      })
    },
    async  DeleteNk(){
      let _this = this
      let deletePostNk = new DeletePostNk(_this)
      let data = {
        id:this.SelectedPostNk.postNkId
      }
      deletePostNk.setParams(data)
      await deletePostNk.fetch(function (content) {
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
        _this.GetAllPostNk();

      }, function (error) {
        console.log(error)
      })
    },
    SetSelectedPostNk(item) {
      this.SelectedPostNk = item
    },

    async GetAllPostNk() {
      let _this = this
      let getAllPostNk = new GetAllPostNk(_this)
      let data = {
        pageNumber: _this.currentPage,
        count: _this.count,
        search: _this.search
      }
      getAllPostNk.setParams(data)
      await getAllPostNk.fetch(function (content) {
        _this.PostNk = content.postNKs

        _this.totalCount = content.postNKsCount
      }, function (error) {
        console.log(error)
      })
    },


  },
}
</script>

<style scoped>

</style>
