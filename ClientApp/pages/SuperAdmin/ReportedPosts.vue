<template>
  <div class="row adminPanelBox">
    <div class="col-md-12 my-3">
      <input v-model="searchKey" type="text" class="form-control" placeholder="جست و جو">
    </div>
    <div class="col-12">
      <table class="table table-bordered table-hover dataTable table-striped">
        <thead>
        <tr>
          <th @click="sortTable('reportedUserInfo')">
            <div class="d-flex">
              <div>
                کاربر
              </div>

            </div>
          </th>
          <th @click="sortTable('reason')">
            <div class="d-flex">
              <div>
                علت گزارش
              </div>
            </div>
          </th>
          <th @click="sortTable('reporterUserInfo')">
            <div class="d-flex">
              <div>
                کاربر گزارش دهنده
              </div>
            </div>
          </th>
          <th @click="Toggleshow()">
            <div class="d-flex">
              <div>
                تاریخ گزارش
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
          <th>مشاهده</th>
        </tr>
        </thead>
        <tbody class="text-center">
        <tr v-for="row in SortByDate">
          <td>{{ row.UserName }}</td>
          <td>{{ row.Reason }}</td>
          <td>{{ row.ReporterName }}</td>
          <!--          <td>{{ row.isConfirmed ? 'پذیرش شده' : 'پذیرش نشده' }}</td>-->
          <td>{{ row.CreateDate | moment("jYYYY/jM/jD HH:mm") }}</td>
          <td>
            <div class="d-flex justify-content-center gap-5">
              <div @click="dataModal(row)">
                <button type="button" class="btn SeeReportedPost" data-bs-toggle="modal"
                        data-bs-target="#exampleModal">
                  <font-awesome-icon icon="eye"/>
                </button>
              </div>
            </div>
          </td>
        </tr>
        </tbody>
      </table>
      <div class="modal fade modalConfirm" id="staticBackdropDeleteConfirm"

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
                      data-bs-target="#staticBackdrop">تایید
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
      <!-- Modal -->
      <div class="modal modal-lg fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel"
           aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered modal-dialog-scrollable">
          <div class="modal-content">
            <div class="modal-header position-relative">
              <h5 class="modal-title" id="exampleModalLabel">مشاهده پست</h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <!--              <quill-example-nuxt @content="postContent" :content="editedPost.content"/>-->
              <div class="mw-100">
                <div class="row">

                  <div class="col-md-6">
                    <div class="d-flex align-items-center gap-2">
                      <small> <i class="fas fa-clock text-muted"></i></small>
                      <small
                        v-if="SingleReported.postContent">{{
                          SingleReported.postContent.createDate | moment('jYYYY/jMM/jDD HH:MM')
                        }}</small>
                    </div>
                  </div>
                  <div class="col-md-6">
                    <div class="d-flex align-items-center gap-2">
                      <small class="text-muted">علت گزارش :</small>
                      <small v-if="SingleReported.postReport">{{ SingleReported.postReport.reason }}</small>

                    </div>

                  </div>
                  <div class="col-md-12 my-3">
                    <div v-if="SingleReported.postContent" v-html="SingleReported.postContent.content"></div>
                  </div>
                </div>
              </div>
            </div>
            <div class="modal-footer">
              <!--              <button type="button" class="btn btn-sm btn-outline-danger" data-bs-toggle="modal"-->
              <!--                      data-bs-target="#exampleModal3">-->
              <!--                گزارش تخلف-->
              <!--              </button>-->
              <button type="button" class=" closeReportAdmin btn btn-sm btn-outline-secondary" data-bs-dismiss="modal">
                بستن
              </button>
            </div>
          </div>
        </div>
      </div>
      <!--      <div class="modal fade" id="exampleModal3" tabindex="-1" aria-labelledby="exampleModal3Label" aria-hidden="true">-->
      <!--        <div class="modal-dialog modal-lg modal-dialog-centered modal-dialog-scrollable">-->
      <!--          <div class="modal-content">-->
      <!--            <div class="modal-header">-->
      <!--              <h5 class="modal-title" id="exampleModal3Label">علت گزارش</h5>-->
      <!--              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>-->
      <!--            </div>-->
      <!--            <div class="modal-body">-->
      <!--              <div class="row">-->
      <!--                <div class="col-12">-->
      <!--                  <label class="d-block" for="updateKey">علت گزارش تخلف</label>-->
      <!--                  <input class="border rounded w-50 my-3 py-2" id="updateKey" type="text" maxlength="50"-->
      <!--                         placeholder="کلمه جدید ..." v-model="reportTextReason"/>-->
      <!--                </div>-->
      <!--              </div>-->
      <!--            </div>-->
      <!--            <div class="modal-footer">-->
      <!--                    <button type="button" class=" closeReportAdmin btn btn-sm btn-outline-secondary" data-bs-dismiss="modal">-->
      <!--                      بستن-->
      <!--                    </button>-->
      <!--&lt;!&ndash;              <button type="button" class="btn btn-sm btn-outline-primary" @click="reportByAdmin">گزارش تخلف</button>&ndash;&gt;-->
      <!--            </div>-->
      <!--          </div>-->
      <!--        </div>-->
      <!--      </div>-->
    </div>
    <div class="col-12 my-auto mx-0">
      <nav aria-label="Page navigation example">
        <ul class="pagination">
          <li class="PaginationItemPrev" @click="updateCurrent(currentPageIndex - 1)">
            <nuxt-link class="page-link" to="/SuperAdmin/ReportedPosts">صفحه قبل</nuxt-link>
          </li>
          <li :class="pageIndex === pageNumber ? 'page-item active' : 'page-item'"
              v-for="pageIndex in paginateTotal" @click="updateCurrent(pageIndex)">
            <nuxt-link class="page-link" to="/SuperAdmin/ReportedPosts">{{ pageIndex }}</nuxt-link>
          </li>
          <li class="PaginationItemNext" @click="updateCurrent(currentPageIndex + 1)">
            <nuxt-link class="page-link" to="/SuperAdmin/ReportedPosts"> صفحه بعد</nuxt-link>
          </li>
        </ul>
      </nav>
    </div>
  </div>
</template>

<script>
import QuillExampleNuxt from "~/components/editor";

export default {
  name: "ReportedPosts",
  components: {QuillExampleNuxt},
  layout: 'SuperAdminPanel',
  data() {
    return {
      reportPost: [],
      ascending: false,
      sortColumn: '',
      editedPost: '',
      postContentValue: '',
      reportTextReason: '',
      SingleReported: '',
      toggleArrow: false,
      totalCount: null,
      paginateTotal: null,
      count: 10,
      pageNumber: 1,
      currentPageIndex: 1,
      searchKey:null
    }
  },
  computed: {
    SortByDate(){

      if(!this.toggleArrow){
        return this.reportPost
      }else{

        return    this.reportPost.sort(function (a, b) {
          const key1 = a.CreateDate;
          const key2 = b.CreateDate;

          if (key1 < key2) {
            return -1;
          } else if (key1 === key2) {
            return 0;
          } else {
            return 1;
          }
        })

      }
    },

    columns() {
      if (this.reportPost.length === 0) {
        return [];
      }
      return Object.keys(this.reportPost[0])
    }
  },
  watch:{
    searchKey:function (val,oldVal){
      this.$axios.post(`PostReport/GetAll`, null, {

        params: {
          pageNumber: 1,
          count: 10,
          search:val
        }
      }).then((response) => {
        if (response.status === 200) {
          this.reportPost = response.data.reports
          this.totalCount = response.data.reportsCount
          this.paginateTotal = Math.ceil(this.totalCount / this.count);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
        }
      }).catch((error) => {
        console.log(error)
      })

    }
  },
  methods: {
    Toggleshow(){
     this.toggleArrow = !this.toggleArrow


    },
    updateCurrent(pageNumber) {
      if (pageNumber === 0 || pageNumber > this.paginateTotal) {
        return true
      } else {
        this.$nuxt.$loading.start()
        this.getAllReportPost(pageNumber, this.count,this.searchKey);
        this.pageNumber = pageNumber;
        this.currentPageIndex = pageNumber;
      }
    },
    postContent(value) {
      this.postContentValue = value;
    },
    getAllReportPost(pageNumber, count,search) {
      this.$axios.post(`PostReport/GetAll`, null, {

        params: {
          pageNumber: pageNumber,
          count: count,
          search:search
        }
      }).then((response) => {
        if (response.status === 200) {
          this.reportPost = response.data.reports
          this.totalCount = response.data.reportsCount
          this.paginateTotal = Math.ceil(this.totalCount / this.count);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    sortTable(col) {
      if (this.sortColumn === col) {
        this.ascending = !this.ascending;
      } else {
        this.ascending = true;
        this.sortColumn = col;
      }

      var ascending = this.ascending;

      this.reportPost.sort(function (a, b) {
        if (a[col] > b[col]) {
          return ascending ? 1 : -1
        } else if (a[col] < b[col]) {
          return ascending ? -1 : 1
        }
        return 0;
      })
    },
    dataModal(data) {
      this.SingleReported = ''
      this.$axios.post(`PostReport/Get?postReportId=${data.PostReportId}`, null, {

      }).then((res) => {
        this.SingleReported = res.data
      })
    },
    editingPost() {
      this.$axios.post(`Post/ChangePostStatus`, {
        content: this.postContentValue,
        postId: this.editedPost.postId,
        userId: this.editedPost.userId,
        status: 1,
        createDate: this.editedPost.createDate,
        updateDate: this.editedPost.updateDate

      }, {

      }).then((response) => {
        if (response.status === 200) {
          document.querySelector('.btn-close').click();
          this.$nuxt.$loading.start()
          this.getAllReportPost(this.pageNumber, this.count,this.searchKey);
          this.$toast.success('پست با موفقیت ویرایش شد.')
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    reportByAdmin() {
      this.$axios.post(`PostReport/Create`, {
        postId: this.editedPost.postId,
        userId: this.editedPost.userId,
        reason: this.reportTextReason,
        reporterUserId: this.$auth.user.userInfo.userId,
      }, {

      }).then((response) => {
        if (response.status === 200) {
          document.querySelector('.closeReportAdmin').click();
          this.$nuxt.$loading.start()
          this.getAllReportPost(this.pageNumber, this.count,this.searchKey);
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
    this.getAllReportPost(this.pageNumber, this.count,this.searchKey);
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
.SeeReportedPost:hover{
  color: #0a98dc!important;
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
