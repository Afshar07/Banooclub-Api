<template>
  <div class="row adminPanelBox">
    <div class="col-12">
      <table class="table table-bordered table-hover dataTable table-striped">
        <thead>
        <tr class="p-3">
          <th @click="sortTable('postId')">شناسه پست</th>
          <th @click="sortTable('userId')">شناسه کاربر</th>
          <th @click="sortTable('reporterUserId')">تاریخ ایجاد</th>
          <th @click="sortTable('updateDate')">تاریخ به روز رسانی</th>
          <th @click="sortTable('status')">وضعیت</th>
          <th>عملیات</th>
        </tr>
        </thead>

        <tbody>
        <tr v-for="row in allPost">
          <td v-for="col in columns" v-if="!(col === 'isDeleted' || col === 'comments'  || col === 'content' )">
            {{ row[col] }}
          </td>
          <td>
            <div class="d-flex">
              <div @click="dataModal(row)">
                <button type="button" class="btn btn-sm btn-outline-primary" data-bs-toggle="modal"
                        data-bs-target="#exampleModal2">
                  ویرایش پست
                </button>
              </div>
            </div>
          </td>
        </tr>
        </tbody>
      </table>
    </div>
    <div class="col-12">
      <div class="modal modal-lg fade" id="exampleModal2" tabindex="-1" aria-labelledby="exampleModal2Label"
           aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered modal-dialog-scrollable">
          <div class="modal-content">
            <div class="modal-header position-relative">
              <h5 class="modal-title" id="exampleModal2Label">ویرایش پست</h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <div class="row">
                <div class="col-12">
                  <quill-example-nuxt @content="postContent" :content="editedPost.content"/>
                </div>
              </div>

            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-sm btn-outline-danger" data-bs-dismiss="modal"
                      @click="reportPostByAdmin">گزارش پست
              </button>
              <button type="button" class="btn btn-sm btn-outline-success" @click="editingPost">ویرایش</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import QuillExampleNuxt from "~/components/editor";

export default {
  name: "AllPosts",
  components: {QuillExampleNuxt},
  layout: 'SuperAdminPanel',
  data() {
    return {
      allPost: [],
      ascending: false,
      sortColumn: '',
      postContentValue: '',
      editedPost: ''
    }
  },
  computed: {
    columns() {
      if (this.allPost.length === 0) {
        return [];
      }
      return Object.keys(this.allPost[0])
    },
  },
  mounted() {
    this.getAllPosts();
    this.$nextTick(() => {
      this.$nuxt.$loading.finish();
    })
  },
  methods: {
    dataModal(data) {
      this.editedPost = data
    },
    postContent(value) {
      this.postContentValue = value;
    },
    getAllPosts() {
      this.$axios.post(`Post/GetAll`, null, {
     
      }).then((response) => {
        if (response.status === 200) {
          this.allPost = response.data
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

      this.allPost.sort(function (a, b) {
        if (a[col] > b[col]) {
          return ascending ? 1 : -1
        } else if (a[col] < b[col]) {
          return ascending ? -1 : 1
        }
        return 0;
      })
    },

    editingPost() {
      this.$axios.post(`Post/Update`, null, {
      
        params: {
          content: this.postContentValue,
          postId: this.editedPost.postId
        }
      }).then((response) => {
        if (response.status === 200) {
          this.$toast.success('پست با موفقیت ویرایش شد.')
          this.getAllPosts();
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    reportPostByAdmin() {

    }
  },
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

.btn-close {
  position: absolute;
  left: 1rem;
}

tr {
  padding: 1rem;
}
</style>
