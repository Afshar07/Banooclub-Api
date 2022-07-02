<template>
  <div class="row adminPanelBox">
    <div class="col-md-12 my-3" >
      <input  v-model="searchKey" type="text" class="form-control" placeholder="جست و جو">
    </div>
    <div class="col-12">
      <table class="table table-bordered table-hover dataTable table-striped">
        <thead>
        <tr>
          <!--          <th @click="sortTable('postId')">شناسه پست</th>-->
          <th @click="sortTable('userId')">
            <div class="d-flex">
              <div>
                نام کاربر
              </div>
              <div class="me-auto">
                <font-awesome-icon icon="long-arrow-alt-up"
                                   :color="toggleArrow ?'#ccc':'#000'"
                                   size="xs"
                />
                <font-awesome-icon icon="long-arrow-alt-down"
                                   :color="!toggleArrow ?'#ccc':'#000'"
                                   size="xs"
                />
              </div>
            </div>

          </th>
          <th @click="sortTable('userInfo')">
            <div class="d-flex">
              <div>
                وضعیت کاربر
              </div>
              <div class="me-auto">
                <font-awesome-icon icon="long-arrow-alt-up"
                                   :color="toggleArrow ?'#ccc':'#000'"
                                   size="xs"
                />
                <font-awesome-icon icon="long-arrow-alt-down"
                                   :color="!toggleArrow ?'#ccc':'#000'"
                                   size="xs"
                />
              </div>
            </div>

          </th>
          <th @click="sortTable('createDate')">
            <div class="d-flex">
              <div>
                زمان ایجاد پست
              </div>
              <div class="me-auto">
                <font-awesome-icon icon="long-arrow-alt-up"
                                   :color="toggleArrow ?'#ccc':'#000'"
                                   size="xs"
                />
                <font-awesome-icon icon="long-arrow-alt-down"
                                   :color="!toggleArrow ?'#ccc':'#000'"
                                   size="xs"
                />
              </div>
            </div>
          </th>
          <th @click="sortTable('status')">
            <div class="d-flex">
              <div>
                وضعیت پست
              </div>
              <div class="me-auto">
                <font-awesome-icon icon="long-arrow-alt-up"
                                   :color="toggleArrow ?'#ccc':'#000'"
                                   size="xs"
                />
                <font-awesome-icon icon="long-arrow-alt-down"
                                   :color="!toggleArrow ?'#ccc':'#000'"
                                   size="xs"
                />
              </div>
            </div>
          </th>
          <th>ویرایش پست</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="row in allNotConfirmedPost">
          <td>{{ row.userInfo.name + ' ' + row.userInfo.familyName }}</td>
          <td v-html="row.userInfo.status === 1 ? 'کاربر فعال' : 'کاربر غیرفعال'"></td>
          <td>{{ row.updateDate | moment("jYYYY/jM/jD HH:mm") }}</td>
          <td v-html="row.status === 2 ? 'پذیرفته نشده':''"></td>
          <td>
            <div class="d-flex gap-5 justify-content-center">
              <div class="" @click="dataModal(row)">
                <button type="button" class="btn btn-sm SeeReportedPost" data-bs-toggle="modal"
                        data-bs-target="#exampleModal">
                  <font-awesome-icon icon="eye"/>
                </button>
              </div>
            </div>
          </td>
        </tr>
        </tbody>
      </table>
      <div class="modal modal-lg fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel"
           aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered modal-dialog-scrollable">
          <div class="modal-content">
            <div class="modal-header position-relative">
              <h5 class="modal-title" id="exampleModalLabel">ویرایش پست</h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <quill-example-nuxt @content="postContent" :content="notConfirmedPost.content"/>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-sm btn-outline-danger" data-bs-toggle="modal"
                      data-bs-target="#exampleModal3">
                گزارش تخلف
              </button>
              <button type="button" class="btn btn-sm btn-outline-success" data-bs-toggle="modal"
                      data-bs-dismiss="modal"
                      data-bs-target="#staticBackdropConfirm">ویرایش
              </button>
            </div>
          </div>
        </div>
      </div>
      <div class="modal fade" id="exampleModal3" tabindex="-1" aria-labelledby="exampleModal3Label" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered modal-dialog-scrollable">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="exampleModal3Label">علت گزارش</h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <div class="row">
                <div class="col-12">
                  <label class="d-block" for="updateKey">علت گزارش تخلف</label>
                  <input class="border rounded w-50 my-3 py-2" id="updateKey" type="text" maxlength="50"
                         placeholder="کلمه جدید ..." v-model="reportTextReason"/>
                </div>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class=" closeReportAdmin btn btn-sm btn-outline-secondary" data-bs-dismiss="modal">
                بستن
              </button>
              <button :disabled="reportTextReason === ''" type="button" class="btn btn-sm btn-outline-primary"
                      data-bs-toggle="modal"
                      data-bs-dismiss="modal"
                      data-bs-target="#staticBackdropDeleteConfirm">گزارش تخلف
              </button>
            </div>
          </div>
        </div>
      </div>
      <div class="modal fade modalConfirm" id="staticBackdropDeleteConfirm"
           tabindex="-1" aria-labelledby="staticBackdropLabelDeleteConfirm" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="staticBackdropLabelDeleteConfirm">گزارش تخلف </h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              آیا از گزارش تخلف اطمینان دارید ؟
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-sm btn-outline-secondary closeApprove2"
                      data-bs-dismiss="modal">
                خیر
              </button>
              <button type="button" class="btn btn-sm btn-outline-primary" data-bs-dismiss="modal"
                      data-bs-target="#staticBackdrop" @click="reportByAdmin">تایید
              </button>
            </div>
          </div>
        </div>
      </div>
      <div class="modal fade modalConfirm" id="staticBackdropConfirm"

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
                      data-bs-target="#staticBackdrop" @click="editingPost">تایید
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
            <nuxt-link class="page-link" to="/SuperAdmin/NotConfirmedPosts">صفحه قبل</nuxt-link>
          </li>
          <li :class="pageIndex === pageNumber ? 'page-item active' : 'page-item'"
              v-for="pageIndex in paginateTotal" @click="updateCurrent(pageIndex)">
            <nuxt-link class="page-link" to="/SuperAdmin/NotConfirmedPosts">{{ pageIndex }}</nuxt-link>
          </li>
          <li class="PaginationItemNext" @click="updateCurrent(currentPageIndex + 1)">
            <nuxt-link class="page-link" to="/SuperAdmin/NotConfirmedPosts">صفحه بعد</nuxt-link>
          </li>
        </ul>
      </nav>
    </div>
  </div>
</template>

<script>
import QuillExampleNuxt from "~/components/editor";

export default {
  name: "NotConfirmedPosts",
  components: {QuillExampleNuxt},
  layout: 'SuperAdminPanel',
  data() {
    return {
      notConfirmedPost: '',
      allNotConfirmedPost: [],
      ascending: false,
      sortColumn: '',
      toggleArrow: false,
      postContentValue: '',
      reportTextReason: '',
      totalCount: null,
      paginateTotal: null,
      count: 10,
      pageNumber: 1,
      currentPageIndex: 1,
      searchKey:null
    }
  },
  watch:{
    searchKey:function (val,oldVal){
      this.$axios.post(`Post/GetNotConfirmed`, null, {
      
        params: {
          pageNumber: 1,
          count: 10,
          search:val
        }
      }).then((response) => {
        if (response.status === 200) {
          this.allNotConfirmedPost = response.data.posts
          this.totalCount = response.data.postCount
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
      if (this.allNotConfirmedPost.length === 0) {
        return [];
      }
      return Object.keys(this.allNotConfirmedPost[0])
    }
  },
  methods: {
    updateCurrent(pageNumber) {
      if (pageNumber === 0 || pageNumber > this.paginateTotal) {
        return true
      } else {
        this.$nuxt.$loading.start()
        this.getAllNotConfirmed(pageNumber, this.count,this.searchKey);
        this.pageNumber = pageNumber;
        this.currentPageIndex = pageNumber;
      }
    },
    postContent(value) {
      this.postContentValue = value;
    },
    sortTable(col) {
      if (this.sortColumn === col) {
        this.ascending = !this.ascending;
      } else {
        this.ascending = true;
        this.sortColumn = col;
      }

      var ascending = this.ascending;

      this.allNotConfirmedPost.sort(function (a, b) {
        if (a[col] > b[col]) {
          return ascending ? 1 : -1
        } else if (a[col] < b[col]) {
          return ascending ? -1 : 1
        }
        return 0;
      })
    },
    dataModal(data) {
      this.notConfirmedPost = data
    },
    getAllNotConfirmed(pageNumber, count,search) {
      this.$axios.post(`Post/GetNotConfirmed`, null, {
      
        params: {
          pageNumber: pageNumber,
          count: count,
          search:search
        }
      }).then((response) => {
        if (response.status === 200) {
          this.allNotConfirmedPost = response.data.posts
          this.totalCount = response.data.postCount
          this.paginateTotal = Math.ceil(this.totalCount / this.count);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    editingPost() {
      this.$axios.post(`Post/ChangePostStatus`, {
        content: this.postContentValue,
        postId: this.notConfirmedPost.postId,
        userId: this.notConfirmedPost.userId,
        createDate: this.notConfirmedPost.createDate,
        status: 1
      }, {
       
      }).then((response) => {
        if (response.status === 200) {
          document.querySelector('.closeReportAdmin').click();
          this.$nuxt.$loading.start()
          this.getAllNotConfirmed(this.pageNumber, this.count,this.searchKey);
          this.$toast.success('پست با موفقیت ویرایش شد.')
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    reportByAdmin() {
      this.$axios.post(`PostReport/Create`, {
        postId: this.notConfirmedPost.postId,
        userId: this.notConfirmedPost.userId,
        reason: this.reportTextReason,
        reporterUserId: this.$auth.user.userId,
      }, {
       
      }).then((response) => {
        if (response.status === 200) {
          document.querySelector('.closeReportAdmin').click();
          this.$nuxt.$loading.start()
          this.getAllNotConfirmed(this.pageNumber, this.count,this.searchKey);
          this.$toast.success('گزارش تخلف با موفقیت انجام شد.')
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
    this.getAllNotConfirmed(this.pageNumber, this.count,this.searchKey);
  }
}
</script>

<style scoped>
.adminPanelBox {
  background-color: #fff;
  padding: 1rem;
}

.modal {
  left: 25vw;
}

#exampleModal3 {
  right: 5vw;
}

.btn-close {
  position: absolute;
  left: 1rem;
}

#staticBackdropConfirm, #staticBackdropDeleteConfirm {
  margin: 25vh auto;
  left: 0;
}
</style>
