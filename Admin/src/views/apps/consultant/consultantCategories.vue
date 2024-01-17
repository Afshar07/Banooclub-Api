<template>
  <b-overlay
      :show="overlay"
      rounded="sm"
  >
    <div v-if="categories !== null">
      <b-modal
          id="modal-delete"
          cancelTitle="خیر"
          centered

          ok-title="بله"
          @ok="deleteCategory"
      >
        <span>آیا از حذف کردن این دسته بندی اطمینان دارید ؟ </span>
      </b-modal>
      <b-modal
          id="modal-edit"
          centered
          ok-only
          ok-title="ویرایش"

          @ok="updateCategory"
      >
        <b-form-group>
          <label>نام دسته بندی</label>
          <b-form-input
              v-if="selectedCategory!==null"
              v-model="selectedCategory.title"
              placeholder="نام دسته بندی"
              type="text"
          />

        </b-form-group>


      </b-modal>
      <b-modal
          id="modal-Add"
          centered
          ok-only
          ok-title="افزودن دسته بندی"

          @ok.preventDefault()="createCategory"
      >
        <b-form-group>
          <label for="email">نام دسته بندی</label>
          <b-form-input
              v-model="consultantCategory.title"
              placeholder="نام دسته بندی"
              type="text"
          />
        </b-form-group>
      </b-modal>
      <!-- Table Container Card -->
      <b-card
          class="mb-0"
          no-body
      >
        <b-col class="my-2" lg="12" md="12">
          <b-row>
            <!--            <b-col lg="10" md="12" class="my-2">-->
            <!--              <b-form-input-->
            <!--                  id="basicInput"-->
            <!--                  v-model="search"-->
            <!--                  placeholder="جستجو بر اساس نام دسته بندی"-->
            <!--              />-->
            <!--            </b-col>-->
            <b-col class="my-2" lg="2" md="12">
              <b-button

                  v-b-modal.modal-Add
                  variant="success"

              >
                افزودن دسته بندی جدید
              </b-button>
            </b-col>
          </b-row>
          <b-form-group

          >

          </b-form-group>
        </b-col>


        <b-table
            ref="refUserListTable"
            :fields="myTableColumns"
            :items="categories"
            bordered
            class="position-relative"
            empty-text="موردی موجود نیست!"
            primary-key="id"
            responsive
            show-empty
            striped
        >

          <!-- Column: delete -->

          <template #cell(Delete)="data">

            <div v-b-modal.modal-delete
                 class="cursor-pointer d-flex flex-row"
                 @click="setSelectedCategory(data.item)"
            >
              <feather-icon class="text-danger" icon="TrashIcon" size="20"/>
            </div>

          </template>

          <template #cell(Edit)="data">

            <div v-b-modal.modal-edit
                 class="cursor-pointer d-flex flex-row"
                 @click="setSelectedCategory(data.item)"
            >
              <feather-icon class="text-primary" icon="EditIcon" size="20"/>
            </div>

          </template>

        </b-table>
        <div class="mx-2 mb-2">
          <b-row>

            <b-col
                class="d-flex align-items-center justify-content-center justify-content-sm-start"
                cols="12"
                sm="6"
            >
              <!--            <span class="text-muted">Showing {{ dataMeta.from }} to {{ dataMeta.to }} of {{ dataMeta.of }} entries</span>-->
            </b-col>
            <!-- Pagination -->
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
        </div>
      </b-card>
    </div>


  </b-overlay>
</template>

<script>

import {
  BAvatar,
  BBadge,
  BButton,
  BCard,
  BCol,
  BDropdown,
  BDropdownItem,
  BFormGroup,
  BFormInput,
  BFormSelect,
  BLink,
  BMedia,
  BModal,
  BOverlay,
  BPagination,
  BRow,
  BTable
} from 'bootstrap-vue'
import vSelect from 'vue-select'
import {
  ConsultCategoryCreate,
  ConsultCategoryDelete,
  ConsultCategoryGetAll,
  ConsultCategoryUpdate
} from "@/libs/Api/consultant";
import ToastificationContent from '@core/components/toastification/ToastificationContent'

export default {
  title: 'لیست دسته بندی مشاوره ها   - پنل ادمین بانو کلاب',
  name: 'UsersList',
  data() {
    return {
      overlay: false,
      categories: null,
      totalCount: null,
      currentPage: 1,
      perPage: 5,
      myTableColumns: [
        {
          key: 'id',
          label: 'شناسه'
        },
        {
          key: 'title',
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
      search: '',
      selectedCategory: null,
      consultantCategory: {
        title: '',
        parentId: null
      }

    }
  },
  async created() {
    await this.getAllConsultCategory()
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
      this.getAllConsultCategory()
    },
    currentPage: function () {
      this.getAllConsultCategory()
    }
  },
  methods: {
    async updateCategory() {
      let _this = this
      _this.overlay = true
      let consultCategoryUpdate = new ConsultCategoryUpdate(_this)
      consultCategoryUpdate.setParams(_this.selectedCategory)
      await consultCategoryUpdate.fetch(function (content) {
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
        _this.getAllConsultCategory();
      }, function (error) {
        console.log(error)
      })
      _this.overlay = false

    },
    async createCategory() {
      let _this = this

      if (_this.consultantCategory.title !== '') {
        _this.overlay = true
        let consultCategoryCreate = new ConsultCategoryCreate(_this)
        consultCategoryCreate.setRequestParamDataObj(_this.consultantCategory)
        await consultCategoryCreate.fetch(function (content) {
          _this.$toast({
            component: ToastificationContent,
            position: 'bottom-center',
            props: {
              title: `عملیات موفق`,
              icon: 'CheckIcon',
              variant: 'success',
              text: `دسته بندی ساخته شد شد`,
            },
          })
          _this.getAllConsultCategory();
          _this.consultantCategory.title = ''

        }, function (error) {
          console.log(error)
        })
      } else {
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات ناموفق`,
            icon: 'AlertCircleIcon',
            variant: 'danger',
            text: `نام دسته بندی را وارد کنید`,
          },
        })
      }
      _this.overlay = false


    },
    async deleteCategory() {
      let _this = this
      _this.overlay = true

      let consultCategoryDelete = new ConsultCategoryDelete(_this)
      consultCategoryDelete.setId(this.selectedCategory.id)
      await consultCategoryDelete.fetch(function (content) {
        if (!content.isSuccess) {
          _this.$toast({
            component: ToastificationContent,
            position: 'bottom-center',
            props: {
              title: `عملیات ناموفق`,
              icon: 'CheckIcon',
              variant: 'success',
              text: `این دسته بندی مشاور فعال دارد`,
            },
          })
        } else {
          _this.$toast({
            component: ToastificationContent,
            position: 'bottom-center',
            props: {
              title: `عملیات موفق`,
              icon: 'CheckIcon',
              variant: 'success',
              text: `دسته بندی  حذف شد.`,
            },
          })
          _this.getAllConsultCategory();
          _this.selectedCategory = null

        }


      }, function (error) {
        console.log(error)
      })
      _this.overlay = false

    },
    setSelectedCategory(item) {
      this.selectedCategory = item
    },
    async getAllConsultCategory() {
      let _this = this
      _this.overlay = true
      let consultCategoryGetAll = new ConsultCategoryGetAll(_this)
      await consultCategoryGetAll.fetch(function (content) {
        _this.categories = content.data

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
