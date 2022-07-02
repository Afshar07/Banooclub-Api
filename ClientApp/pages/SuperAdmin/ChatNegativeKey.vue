<template>
  <div class="row adminPanelBox">
    <div class="col-12">
      <button class="btn btn-sm buttonStyle" @click="showAdd = !showAdd">
        <font-awesome-icon icon="plus" size="lg"/>
        افزودن کلمه جدید
      </button>
    </div>
    <div class="col-md-12 my-3">
      <input v-model="searchKey" type="text" class="form-control">
    </div>
    <transition name="bounce">
      <div class="col-12 my-3" v-if="showAdd">
        <div class="d-flex gap-3">
          <div class="w-50">
            <input class="border rounded py-2 px-1 w-100" type="text" placeholder="کلمه جدید ..." maxlength="50"
                   v-model="newKey"/>
          </div>
          <div class="">
            <button class="btn btn-sm buttonStyle py-2" @click="addKey">
              افزودن
            </button>
          </div>
        </div>
      </div>
    </transition>
    <div class="col-12">
      <table class="table table-bordered table-hover dataTable table-striped my-3">
        <thead>
        <tr>
          <th @click="sortTable('chatNKId')">
            <div class="d-flex">
              <div>
                شناسه
              </div>
              <div class="me-auto">
                <font-awesome-icon icon="long-arrow-alt-up"
                                   :color="!toggleArrow ?'#ccc':'#000'" size="xs"/>
                <font-awesome-icon icon="long-arrow-alt-down"
                                   :color="toggleArrow ?'#ccc':'#000'" size="xs"/>
              </div>
            </div>
          </th>
          <th @click="sortTable('name')">
            <div class="d-flex">
              <div>
                کلمه
              </div>
              <div class="me-auto">
                <font-awesome-icon icon="long-arrow-alt-up"
                                   :color="toggleArrow ?'#ccc':'#000'" size="xs"/>
                <font-awesome-icon icon="long-arrow-alt-down"
                                   :color="!toggleArrow ?'#ccc':'#000'" size="xs"/>
              </div>
            </div>

          </th>
          <th class="">
            <div class="d-flex justify-content-evenly">
              <div class="">
                حذف
              </div>
              <div class="">
                ویرایش
              </div>
            </div>
          </th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="row in chatNK">
          <td v-for="col in columns" v-if="!(col === 'isDeleted')">{{ row[col] }}</td>
          <td>
            <div class="d-flex justify-content-evenly gap-5">
              <div class="">
                <button type="button" class="btn" data-bs-toggle="modal"
                        data-bs-dismiss="modal"
                        data-bs-target="#staticBackdropDeleteConfirm" @click="editKey(row)">
                  <font-awesome-icon icon="trash" color="red"/>
                </button>
              </div>
              <div class="">
                <button type="button" class="btn" data-bs-toggle="modal"
                        data-bs-target="#exampleModal" @click="editKey(row)">
                  <font-awesome-icon icon="edit" color="blue"/>
                </button>
              </div>
            </div>
          </td>
        </tr>
        </tbody>
      </table>
      <div class="modal fade modal-lg my-0" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel"
           aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered modal-dialog-scrollable">
          <div class="modal-content">
            <div class="modal-header position-relative">
              <h5 class="modal-title" id="exampleModalLabel">ویرایش کلمات نامتعارف چت</h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <div class="row">
                <div class="col-12">
                  <label class="d-block" for="updateKey">عنوان جدید کلمه</label>
                  <input class="border rounded w-50 my-3 py-2" id="updateKey" type="text" maxlength="50"
                         placeholder="کلمه جدید ..." v-model="EditKeyNew"/>
                </div>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="closingModal btn btn-sm btn-outline-danger" data-bs-dismiss="modal">بستن
              </button>
              <button  type="button" class="closingModal btn btn-sm btn-outline-primary" data-bs-toggle="modal" data-bs-dismiss="modal" data-bs-target="#staticBackdropConfirm">ذخیره تغییرات</button>

            </div>
          </div>
        </div>
      </div>
      <div class="modal fade modalConfirm my-0" id="staticBackdropDeleteConfirm" data-bs-backdrop="static"
           data-bs-keyboard="false"
           tabindex="-1" aria-labelledby="staticBackdropLabelDeleteConfirm" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="staticBackdropLabelDeleteConfirm">حذف </h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              آیا از حذف اطمینان دارید ؟
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-sm btn-outline-secondary closeApprove2"
                      data-bs-dismiss="modal">
                خیر
              </button>
              <button type="button" class="btn btn-sm btn-outline-primary" data-bs-dismiss="modal"
                      data-bs-target="#staticBackdrop" @click="deleteKey">تایید
              </button>
            </div>
          </div>
        </div>
      </div>
      <div class="modal fade modalConfirm my-0" id="staticBackdropConfirm" data-bs-backdrop="static"
           data-bs-keyboard="false"
           tabindex="-1" aria-labelledby="staticBackdropLabelConfirm" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="staticBackdropLabelConfirm">ذخیره تغییرات</h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              آیا از ذخیره تغییرات اطمینان دارید ؟
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-sm btn-outline-secondary closeApprove2"
                      data-bs-dismiss="modal">
                خیر
              </button>
              <button type="button" class="btn btn-sm btn-outline-primary" data-bs-dismiss="modal"
                      data-bs-target="#staticBackdrop" @click="updateChatNK">تایید
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="col-12">
      <nav aria-label="Page navigation example">
        <ul class="pagination">
          <li class="PaginationItemPrev" @click="updateCurrent(currentPageIndex - 1)">
            <nuxt-link class="page-link" to="/SuperAdmin/ChatNegativeKey">صفحه قبل</nuxt-link>
          </li>
          <li :class="pageIndex === pageNumber ? 'page-item active' : 'page-item'"
              v-for="pageIndex in paginateTotal" @click="updateCurrent(pageIndex)">
            <nuxt-link class="page-link" to="/SuperAdmin/ChatNegativeKey">{{ pageIndex }}</nuxt-link>
          </li>
          <li class="PaginationItemNext" @click="updateCurrent(currentPageIndex + 1)">
            <nuxt-link class="page-link" to="/SuperAdmin/ChatNegativeKey">صفحه بعد</nuxt-link>
          </li>
        </ul>
      </nav>
    </div>
  </div>
</template>

<script>
export default {
  name: "ChatNegativeKey",
  layout: 'SuperAdminPanel',
  data() {
    return {
      showAdd: false,
      newKey: '',
      chatNK: [],
      ascending: false,
      sortColumn: '',
      toggleArrow: false,
      editKeyData: '',
      EditKeyNew: '',
      totalCount: null,
      paginateTotal: null,
      count: 10,
      pageNumber: 1,
      currentPageIndex: 1,
      searchKey:null
    }
  },
  methods: {
    updateCurrent(pageNumber) {
      if (pageNumber === 0 || pageNumber > this.paginateTotal) {
        return true
      } else {
        this.$nuxt.$loading.start()
        this.getAllPostNK(pageNumber, this.count,this.searchKey);
        this.pageNumber = pageNumber;
        this.currentPageIndex = pageNumber;
      }
    },
    addKey() {
      if (this.newKey === '') {
        this.$toast.error('لطفا کلمه جدید را وارد کنید.')
      } else {
        this.$axios.post(`ChatNK/Create`, {
          name: this.newKey
        }, {
        
        }).then((response) => {
          if (response.status === 200) {
            this.$nuxt.$loading.start();
            this.getAllChatNK(this.pageNumber, this.count,this.searchKey);
            this.$toast.success('کلمه جدید با موفقیت افزوده شد.')
          }
        }).catch((error) => {
          console.log(error)
        })
      }

    },
    getAllChatNK(pageNumber, count,search) {
      this.$axios.post(`ChatNK/GetAll`, null, {
       
        params: {
          pageNumber: pageNumber,
          count: count,
          search:search
        }
      }).then((response) => {
        if (response.status === 200) {
          this.chatNK = response.data.chatNKs
          this.totalCount = response.data.chatNKsCount
          this.paginateTotal = Math.ceil(this.totalCount / this.count);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    sortTable(col) {
      this.toggleArrow =!this.toggleArrow
     this.chatNK.reverse()
    },
    deleteKey() {
      this.$axios.post(`ChatNK/Delete`, null, {
       
        params: {
          id: this.editKeyData.chatNKId
        }
      }).then((response) => {
        if (response.status === 200) {
          this.$nuxt.$loading.start();
          this.getAllChatNK(this.pageNumber, this.count,this.searchKey);
          this.$toast.success('حذف با موفقیت انجام شد.')
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    editKey(data) {
      this.editKeyData = data;
    },
    updateChatNK() {
      this.$axios.post(`ChatNK/Update`, {
        chatNKId: this.editKeyData.chatNKId,
        name: this.EditKeyNew,
      }, {
       
      }).then((response) => {
        if (response.status === 200) {
          document.querySelector('.closingModal').click();
          this.$nuxt.$loading.start();
          this.getAllChatNK(this.pageNumber, this.count,this.searchKey);
          this.$toast.success('به روز رسانی با موفقیت انجام شد.');
        }
      }).catch((error) => {
        console.log(error)
      })
    }
  },
  mounted() {
    this.$nextTick(() => {
      this.$nuxt.$loading.start()
    })
    this.getAllChatNK(this.pageNumber, this.count,this.searchKey);
  },
  watch:{
    searchKey:function (val,oldVal){
      this.$axios.post(`ChatNK/GetAll`, null, {
       
        params: {
          pageNumber: 1,
          count: 10,
          search:val
        }
      }).then((response) => {
        if (response.status === 200) {
          this.chatNK = response.data.chatNKs
          this.totalCount = response.data.chatNKsCount
          this.paginateTotal = Math.ceil(this.totalCount / this.count);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
        }
      }).catch((error) => {
        console.log(error)
      })

    }
  },
  computed: {
    columns() {
      if (this.chatNK.length === 0) {
        return [];
      }
      return Object.keys(this.chatNK[0])
    }
  },
}
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
  animation: bounce-in .5s;
}

.bounce-leave-active {
  animation: bounce-in .5s reverse;
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
}

.btn-close {
  position: absolute;
  left: 1rem;
}

tr {
  padding: 1rem;
}

#staticBackdropConfirm, #staticBackdropDeleteConfirm {
  margin: 25vh auto;
  left: 0;

}
</style>
