<template>
  <div class="row adminPanelBox">
    <!-- <div
      class="modal fade modal-lg my-0"
      id="exampleModal"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div
        class="modal-dialog modal-lg modal-dialog-centered modal-dialog-scrollable"
      >
        <div class="modal-content">
          <div class="modal-header position-relative">
            <h5 class="modal-title" id="exampleModalLabel">
              ویرایش کلمات نامتعارف پست
            </h5>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body">
            <div class="row">
              <div class="col-12">
                <label class="d-block" for="updateKey">عنوان جدید کلمه</label>
                <input
                  class="border rounded w-50 my-3 py-2"
                  id="updateKey"
                  type="text"
                  maxlength="50"
                  placeholder="کلمه جدید ..."
                  v-model="EditKeyNew"
                />
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="closingModal btn btn-sm btn-outline-danger"
              data-bs-dismiss="modal"
            >
              بستن
            </button>
            <button
              type="button"
              class="closingModal btn btn-sm btn-outline-primary"
              data-bs-toggle="modal"
              data-bs-dismiss="modal"
              data-bs-target="#staticBackdropConfirm"
            >
              ذخیره تغییرات
            </button>
          </div>
        </div>
      </div>
    </div> -->
    <div class="col-12">
      <button
        class="btn btn-sm btn-outline-success"
        @click="showAdd = !showAdd"
      >
        <font-awesome-icon icon="plus" size="lg" />
        افزودن کلمه جدید
      </button>
    </div>

    <div class="col-md-12 my-3">
      <input v-model="searchKey" type="text" class="form-control" />
    </div>
    <transition name="bounce">
      <div class="col-12 my-3" v-if="showAdd">
        <div class="d-flex gap-3">
          <div class="w-50">
            <input
              class="border rounded py-2 px-1 w-100"
              type="text"
              placeholder="کلمه جدید ..."
              maxlength="50"
              v-model="newKey"
            />
          </div>
          <div class="">
            <button
              class="btn btn-sm btn-success py-2"
              data-bs-toggle="modal"
              data-bs-target="#staticBackdropAddedNewKey"
            >
              افزودن
            </button>
          </div>
        </div>
      </div>
    </transition>
    <!-- <div
      class="modal fade"
      id="staticBackdropAddedNewKey"
      tabindex="-1"
      aria-labelledby="staticBackdropLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="staticBackdropLabel">
              افزودن کلمه جدید
            </h5>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body">آیا از افزودن کلمه جدید اطمینان دارید؟</div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="modal"
            >
              خیر
            </button>
            <button
              type="button"
              class="btn btn-primary"
              data-bs-dismiss="modal"
              @click="addKey"
            >
              بله
            </button>
          </div>
        </div>
      </div>
    </div> -->

    <div
      class="col-12 pt-5 border-bottom w-auto pb-2 border-primary border-2 text-success"
    >
      کلمه های موجود
    </div>
    <div class="col-12">
      <table
        class="table table-bordered table-hover table-bordered dataTable table-striped my-3"
      >
        <thead>
          <tr>
            <th @click="sortTable('postNkId')">
              <div class="d-flex">
                <div>شناسه</div>
                <div class="me-auto">
                  <font-awesome-icon
                    icon="long-arrow-alt-up"
                    :color="!toggleArrow ? '#ccc' : '#000'"
                    size="xs"
                  />
                  <font-awesome-icon
                    icon="long-arrow-alt-down"
                    :color="toggleArrow ? '#ccc' : '#000'"
                    size="xs"
                  />
                </div>
              </div>
            </th>
            <th @click="sortTable('name')">
              <div class="d-flex">
                <div>کلمه</div>
                <div class="me-auto">
                  <font-awesome-icon
                    icon="long-arrow-alt-up"
                    :color="toggleArrow ? '#ccc' : '#000'"
                    size="xs"
                  />
                  <font-awesome-icon
                    icon="long-arrow-alt-down"
                    :color="!toggleArrow ? '#ccc' : '#000'"
                    size="xs"
                  />
                </div>
              </div>
            </th>
            <th class="">
              <div class="d-flex justify-content-evenly">
                <div class="">حذف</div>
                <div class="">ویرایش</div>
              </div>
            </th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="row in postNKListFilter" :key="row">
            <td v-for="col in columns" :key="col" v-if="!(col === 'isDeleted')">
              {{ row[col] }}
            </td>
            <td>
              <div class="d-flex gap-5 justify-content-evenly">
                <div class="">
                  <button
                    type="button"
                    class="btn"
                    data-bs-dismiss="modal"
                    data-bs-target="#staticBackdropDeleteConfirm"
                    @click="renderDeleteModal(row)"
                  >
                    <font-awesome-icon icon="trash" color="red" />
                  </button>
                </div>
                <div class="">
                  <button
                    type="button"
                    class="btn"
                    @click="renderEditModal(row)"
                  >
                    <font-awesome-icon icon="edit" color="#3a48db" />
                  </button>
                </div>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
      <div
        class="modal fade modalConfirm"
        id="staticBackdropDeleteConfirm"
        tabindex="-1"
        aria-labelledby="staticBackdropLabelDeleteConfirm"
        aria-hidden="true"
      >
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="staticBackdropLabelDeleteConfirm">
                حذف
              </h5>
              <button
                type="button"
                class="btn-close"
                data-bs-dismiss="modal"
                aria-label="Close"
              ></button>
            </div>
            <div class="modal-body">آیا از حذف اطمینان دارید ؟</div>
            <div class="modal-footer">
              <button
                type="button"
                class="btn btn-sm btn-outline-secondary closeApprove2"
                data-bs-dismiss="modal"
              >
                خیر
              </button>
              <button
                type="button"
                class="btn btn-sm btn-outline-primary"
                data-bs-dismiss="modal"
                data-bs-target="#staticBackdrop"
                @click="deleteKey"
              >
                تایید
              </button>
            </div>
          </div>
        </div>
      </div>
      <div
        class="modal fade modalConfirm"
        id="staticBackdropConfirm"
        data-bs-backdrop="static"
        data-bs-keyboard="false"
        tabindex="-1"
        aria-labelledby="staticBackdropLabelConfirm"
        aria-hidden="true"
      >
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="staticBackdropLabelConfirm">
                ذخیره تغییرات
              </h5>
              <button
                type="button"
                class="btn-close"
                data-bs-dismiss="modal"
                aria-label="Close"
              ></button>
            </div>
            <div class="modal-body">آیا از ذخیره تغییرات اطمینان دارید ؟</div>
            <div class="modal-footer">
              <button
                type="button"
                class="btn btn-sm btn-outline-secondary closeApprove2"
                data-bs-dismiss="modal"
              >
                خیر
              </button>
              <button
                type="button"
                class="btn btn-sm btn-outline-primary"
                data-bs-dismiss="modal"
                data-bs-target="#staticBackdrop"
                @click="updatePostNK"
              >
                تایید
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="col-12 mx-auto">
      <nav aria-label="Page navigation example mx-auto">
        <ul class="pagination m-0 p-0 mx-auto">
          <li
            class="PaginationItemPrev"
            @click="updateCurrent(currentPageIndex - 1)"
          >
            <nuxt-link class="page-link" to="/SuperAdmin/PostNegativeKey"
              >صفحه قبل</nuxt-link
            >
          </li>
          <li
            :class="pageIndex === pageNumber ? 'page-item active' : 'page-item'"
            v-for="pageIndex in filteredPagination"
            :key="pageIndex"
            @click="updateCurrent(pageIndex)"
          >
            <nuxt-link class="page-link" to="/SuperAdmin/PostNegativeKey">{{
              pageIndex
            }}</nuxt-link>
          </li>
          <li
            class="PaginationItemNext"
            @click="updateCurrent(currentPageIndex + 1)"
          >
            <nuxt-link class="page-link" to="/SuperAdmin/PostNegativeKey"
              >صفحه بعد</nuxt-link
            >
          </li>
        </ul>
      </nav>
    </div>
    <base-modal
      :isRenderingModal="isRenderingEditModal"
      @closeModal="closeEditModal"
    >
      <template v-slot:title>ویرایش کلمات نامتعارف پست</template>
      <template v-slot:content>
        <div class="col-12 d-flex mt-4">
          <label class="d-block col-4" for="updateKey">عنوان جدید کلمه</label>
          <input
            class="border rounded col-8 p-1"
            type="text"
            maxlength="50"
            placeholder="کلمه جدید ..."
            v-model="EditKeyNew"
          />
        </div>
      </template>
      <template v-slot:actions>
        <div class="d-flex justify-content-end gap-4">
          <button class="btn btn-danger" @click="closeEditModal">بستن</button>
          <button class="btn btn-primary" @click="updatePostNK">
            ذخیره تغییرات
          </button>
        </div>
      </template>
    </base-modal>
    <base-modal
      :isRenderingModal="isRenderingDeleteModal"
      @closeModal="closeDeleteModal"
    >
      <template v-slot:title>حذف کلمات نامتعارف پست</template>
      <template v-slot:content>
        <div class="col-12 d-flex mt-4">آیا از حذف کلمه اطمینان دارید؟</div>
      </template>
      <template v-slot:actions>
        <div class="d-flex justify-content-end gap-4">
          <button class="btn btn-danger" @click="closeDeleteModal">خیر</button>
          <button class="btn btn-primary" @click="deleteKey">بله</button>
        </div>
      </template>
    </base-modal>
  </div>
</template>

<script>
import BaseModal from "../../components/Base/BaseModal.vue";
export default {
  name: "PostNegativeKey",
  layout: "SuperAdminPanel",
  components: { BaseModal },
  data() {
    return {
      isRenderingEditModal: false,
      isRenderingDeleteModal: false,
      showAdd: false,
      newKey: "",
      postNK: [],
      ascending: false,
      sortColumn: "",
      toggleArrow: false,
      editKeyData: "",
      EditKeyNew: "",
      totalCount: null,
      paginateTotal: null,
      count: 10,
      pageNumber: 1,
      currentPageIndex: 1,
      searchPostNK: "",
      searchKey: null,
    };
  },
  watch: {
    searchKey: function (val, oldVal) {
      this.$axios
        .post(`PostNK/GetAll`, null, {
          params: {
            pageNumber: 1,
            count: 10,
            search: val,
          },
        })
        .then((response) => {
          if (response.status === 200) {
            this.postNK = response.data.postNKs;
            this.totalCount = response.data.postNKsCount;
            this.paginateTotal = Math.ceil(this.totalCount / this.count);
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false;
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
  methods: {
    renderDeleteModal(data) {
      this.editKeyData = data;
      this.isRenderingDeleteModal = true;
    },
    closeDeleteModal() {
      this.isRenderingDeleteModal = false;
    },
    renderEditModal(data) {
      this.editKeyData = data;
      this.isRenderingEditModal = true;
    },
    closeEditModal() {
      this.isRenderingEditModal = false;
    },
    addKey() {
      if (this.newKey === "") {
        this.$toast.error("لطفا کلمه جدید را وارد کنید.");
      } else {
        this.$axios
          .post(
            `PostNK/Create`,
            {
              name: this.newKey,
            },
            {}
          )
          .then((response) => {
            if (response.status === 200) {
              this.showAdd = false;
              this.newKey = "";
              this.$nuxt.$loading.start();
              this.getAllPostNK(this.pageNumber, this.count, this.searchKey);
              this.$toast.success("کلمه جدید با موفقیت افزوده شد.");
            }
          })
          .catch((error) => {
            console.log(error);
          });
      }
    },
    sortTable(col) {
      this.toggleArrow = !this.toggleArrow;
      this.postNK.reverse();
      //
      //
      // if (this.sortColumn === col) {
      //   this.ascending = !this.ascending;
      //   this.toggleArrow = !this.toggleArrow;
      // } else {
      //   this.ascending = true;
      //   this.sortColumn = col;
      // }
      //
      // var ascending = this.ascending;
      //
      // this.postNK.sort(function (a, b) {
      //   if (a[col] > b[col]) {
      //     return ascending ? 1 : -1
      //   } else if (a[col] < b[col]) {
      //     return ascending ? -1 : 1
      //   }
      //   return 0;
      // })
    },
    getAllPostNK(pageNumber, count, search) {
      this.$axios
        .post(`PostNK/GetAll`, null, {
          params: {
            pageNumber: pageNumber,
            count: count,
            search: search,
          },
        })
        .then((response) => {
          if (response.status === 200) {
            this.postNK = response.data.postNKs;
            this.totalCount = response.data.postNKsCount;
            this.paginateTotal = Math.ceil(this.totalCount / this.count);
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false;
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    deleteKey() {
      this.$axios
        .post(`PostNK/Delete`, null, {
          params: {
            id: this.editKeyData.postNkId,
          },
        })
        .then((response) => {
          if (response.status === 200) {
            this.isRenderingDeleteModal = false;
            this.getAllPostNK(this.pageNumber, this.count, this.searchKey);
            this.$toast.success("حذف با موفقیت انجام شد.");
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    editKey(data) {
      this.editKeyData = data;
    },
    updatePostNK() {
      this.$axios
        .post(
          `PostNK/Update`,
          {
            postNkId: this.editKeyData.postNkId,
            name: this.EditKeyNew,
          },
          {}
        )
        .then((response) => {
          if (response.status === 200) {
            this.closeEditModal();
            this.getAllPostNK(this.pageNumber, this.count, this.searchKey);
            this.$toast.success("به روز رسانی با موفقیت انجام شد.");
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    updateCurrent(pageNumber) {
      if (pageNumber === 0 || pageNumber > this.paginateTotal) {
        return true;
      } else {
        this.$nuxt.$loading.start();
        this.getAllPostNK(pageNumber, this.count, this.searchKey);
        this.pageNumber = pageNumber;
        this.currentPageIndex = pageNumber;
      }
    },
  },
  mounted() {
    this.$nextTick(() => {
      this.$nuxt.$loading.start();
    });
    this.getAllPostNK(this.pageNumber, this.count, this.searchKey);
  },
  computed: {
    filteredPagination() {
      const paginate = [];
      if (this.paginateTotal > 5) {
        if (this.currentPageIndex == 1) {
          for (let i = 0; i <= 5; i++) {
            paginate.push(this.currentPageIndex + i);
          }
        } else {
          for (let i = 0; i <= 5; i++) {
            if (this.currentPageIndex - 2 + i != 0) {
              paginate.push(this.currentPageIndex - 2 + i);
            }
          }
        }
      } else {
        paginate.push(this.paginateTotal);
      }
      return paginate;
    },
    columns() {
      if (this.postNK.length === 0) {
        return [];
      }
      return Object.keys(this.postNK[0]);
    },
    postNKListFilter() {
      var filter_name = this.searchPostNK;
      return this.searchPostNK !== ""
        ? this.postNK.filter(function (d) {
            return d.name.indexOf(filter_name) > -1;
          })
        : this.postNK;
    },
  },
};
</script>

<style scoped>
.adminPanelBox {
  background-color: #fff;
  padding: 1rem;
}

input::placeholder {
  font-size: 16px;
  color: #cccccc;
}

.bounce-enter-active {
  animation: bounce-in 0.5s;
}

.bounce-leave-active {
  animation: bounce-in 0.5s reverse;
}

@keyframes bounce-in {
  0% {
    transform: scale(0);
  }
  50% {
    transform: scale(1);
  }
  100% {
    transform: scale(1);
  }
}

.listBox:hover {
  box-shadow: 0 8px 8px #e1e8ec;
  border-color: transparent;
}

.modal {
  left: 25vw;
  /*margin: 20vh auto;*/
}

.btn-close {
  position: absolute;
  left: 1rem;
}

tr {
  padding: 1rem;
}

#staticBackdropConfirm,
#staticBackdropDeleteConfirm,
#staticBackdropAddedNewKey {
  margin: 25vh auto;
  left: 0;
}
</style>
