<template>
  <div class="row adminPanelBox">
    <div class="col-12">
      <button class="btn btn-sm buttonStyle " @click="showAdd = !showAdd">
        <font-awesome-icon icon="plus" size="lg"/>
        افزودن دسته بندی جدید
      </button>
    </div>
    <div class="col-md-12 my-3">
      <input v-model="searchKey" type="text" class="form-control" placeholder="جست و جو">
    </div>
    <transition name="bounce">
      <div class="col-12 my-3" v-if="showAdd">
        <nav>
          <div class="nav nav-tabs" id="nav-tab" role="tablist">
            <button class="nav-link active" id="nav-home-tab" data-bs-toggle="tab" data-bs-target="#nav-home"
                    type="button" role="tab" aria-controls="nav-home" aria-selected="true">
              دسته بندی اصلی
            </button>
            <button @click="getAllCategoryNotPagination" class="nav-link" id="nav-profile-tab" data-bs-toggle="tab"
                    data-bs-target="#nav-profile"
                    type="button" role="tab" aria-controls="nav-profile" aria-selected="false">
              دسته بندی زیرمجوعه
            </button>
          </div>
        </nav>
        <div class="tab-content" id="nav-tabContent">
          <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
            <div class="d-flex gap-3 mt-5">
              <div class="w-50">
                <input class="border rounded py-2 px-1 w-100" type="text" placeholder="دسته بندی ..." maxlength="50"
                       v-model="newMainCategory"/>
              </div>
              <div class="">
                <button class="btn btn-sm AddCategoryBtn py-2" @click="addMainCategory">
                  افزودن
                </button>
              </div>
            </div>
          </div>
          <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">
            <div class="d-flex flex-column mt-5">
              <div class="d-flex my-3 gap-3">
                <div class="">
                  عنوان دسته بندی :
                </div>
                <div class="w-50">
                  <input class="border rounded py-2 px-1 w-100" type="text" placeholder="دسته بندی ..." maxlength="50"
                         v-model="newSubCategory"/>
                </div>
              </div>
              <div class="d-flex gap-3">
                <div class="">
                  انتخاب دسته بندی اصلی :
                </div>
                <div class="w-50">
                  <select class="form-control" v-model="mainCategoryId">
                    <option v-for="item in allCategoryNotPagination.filter(e=> e.ParentId === null) " :key="item.MainAdsCategoryId" :value="item.MainAdsCategoryId">{{item.MainName}}</option>
                  </select>

                </div>
                <div class="">
                  <button class="btn btn-sm AddCategoryBtn py-2" @click="addSubCategory">
                    افزودن
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </transition>
    <div class="col-12">
      <table class="table table-bordered table-hover table-bordered dataTable table-striped my-3">
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
          <th @click="sortTable('ParentName')">
            <div class="d-flex">
              <div>
                عنوان دسته بندی مادر
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
          <th @click="sortTable('name')">
            <div class="d-flex">
              <div>
                عنوان دسته بندی
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
        <tr  v-for="item in allCategory">
          <td>
            <small>{{item.MainAdsCategoryId}}</small>
          </td>
          <td>
            <small>{{item.ParentName}}</small>
          </td>
          <td>
            <small>{{item.MainName}}</small>
          </td>



          <td>
            <div class="d-flex gap-5 justify-content-evenly">
              <div class="" @click="dataModal(item)">
                <button type="button" class="btn">
                  <!--                  حذف-->
                  <font-awesome-icon icon="trash" color="red" data-bs-toggle="modal"
                                     data-bs-dismiss="modal"
                                     data-bs-target="#staticBackdropDeleteConfirm"/>
                </button>
              </div>
              <div class="" @click="dataModal(item)">
                <button type="button" class="btn" data-bs-toggle="modal"
                        data-bs-target="#exampleModal">
                  <!--                  ویرایش-->
                  <font-awesome-icon icon="edit" color="#3a48db"/>
                </button>
              </div>
            </div>
          </td>
        </tr>
        </tbody>
      </table>
<!--      <table class="table table-bordered table-hover table-bordered dataTable table-striped my-3">-->
<!--        <thead>-->
<!--        <tr>-->
<!--          <th @click="sortTable('chatNKId')">-->
<!--            <div class="d-flex">-->
<!--              <div>-->
<!--                شناسه-->
<!--              </div>-->
<!--              <div class="me-auto">-->
<!--                <font-awesome-icon icon="long-arrow-alt-up"-->
<!--                                   :color="!toggleArrow ?'#ccc':'#000'" size="xs"/>-->
<!--                <font-awesome-icon icon="long-arrow-alt-down"-->
<!--                                   :color="toggleArrow ?'#ccc':'#000'" size="xs"/>-->
<!--              </div>-->
<!--            </div>-->
<!--          </th>-->
<!--          <th @click="sortTable('ParentName')">-->
<!--            <div class="d-flex">-->
<!--              <div>-->
<!--                عنوان دسته بندی مادر-->
<!--              </div>-->
<!--              <div class="me-auto">-->
<!--                <font-awesome-icon icon="long-arrow-alt-up"-->
<!--                                   :color="toggleArrow ?'#ccc':'#000'"-->
<!--                                   size="xs"-->
<!--                />-->
<!--                <font-awesome-icon icon="long-arrow-alt-down"-->
<!--                                   :color="!toggleArrow ?'#ccc':'#000'"-->
<!--                                   size="xs"-->
<!--                />-->
<!--              </div>-->
<!--            </div>-->

<!--          </th>-->
<!--          <th @click="sortTable('name')">-->
<!--            <div class="d-flex">-->
<!--              <div>-->
<!--                عنوان دسته بندی-->
<!--              </div>-->
<!--              <div class="me-auto">-->
<!--                <font-awesome-icon icon="long-arrow-alt-up"-->
<!--                                   :color="toggleArrow ?'#ccc':'#000'"-->
<!--                                   size="xs"-->
<!--                />-->
<!--                <font-awesome-icon icon="long-arrow-alt-down"-->
<!--                                   :color="!toggleArrow ?'#ccc':'#000'"-->
<!--                                   size="xs"-->
<!--                />-->
<!--              </div>-->
<!--            </div>-->

<!--          </th>-->
<!--          <th class="">-->
<!--            <div class="d-flex justify-content-evenly">-->
<!--              <div class="">-->
<!--                حذف-->
<!--              </div>-->
<!--              <div class="">-->
<!--                ویرایش-->
<!--              </div>-->
<!--            </div>-->
<!--          </th>-->
<!--        </tr>-->
<!--        </thead>-->
<!--        <tbody>-->
<!--        <tr  v-for="row in allCategory">-->
<!--          <td v-for="col in columns" v-if="!(col === 'isDeleted' || col === 'parentId' )">{{ row[col] }}</td>-->
<!--          <td>-->
<!--            <div class="d-flex gap-5 justify-content-evenly">-->
<!--              <div class="" @click="dataModal(row)">-->
<!--                <button type="button" class="btn">-->
<!--                  &lt;!&ndash;                  حذف&ndash;&gt;-->
<!--                  <font-awesome-icon icon="trash" color="red" data-bs-toggle="modal"-->
<!--                                     data-bs-dismiss="modal"-->
<!--                                     data-bs-target="#staticBackdropDeleteConfirm"/>-->
<!--                </button>-->
<!--              </div>-->
<!--              <div class="" @click="dataModal(row)">-->
<!--                <button type="button" class="btn" data-bs-toggle="modal"-->
<!--                        data-bs-target="#exampleModal">-->
<!--                  &lt;!&ndash;                  ویرایش&ndash;&gt;-->
<!--                  <font-awesome-icon icon="edit" color="#3a48db"/>-->
<!--                </button>-->
<!--              </div>-->
<!--            </div>-->
<!--          </td>-->
<!--        </tr>-->
<!--        </tbody>-->
<!--      </table>-->
    </div>
    <div class="col-12">
      <div class="modal fade modalConfirm" id="staticBackdropDeleteConfirm"
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
                      data-bs-target="#staticBackdrop" @click="deleteCategory">تایید
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="col-12">
      <div class="modal fade modalConfirm" id="staticBackdropConfirm"
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
                      data-bs-target="#staticBackdrop" @click="editCategory">تایید
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>


    <div class="col-12">
      <!-- Modal -->
      <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header position-relative">
              <h5 class="modal-title" id="exampleModalLabel">ویرایش دسته بندی</h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <div class="row">
                <div class="col-12">
                  <label class="d-block" for="updateKey">ویرایش دسته بندی</label>
                  <input class="border rounded w-50 my-3 py-2" id="updateKey" type="text" maxlength="50"
                         placeholder="دسته بندی جدید ..." v-model="newCategory"/>
                </div>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-sm btn-outline-secondary CloseModal" data-bs-dismiss="modal">بستن
              </button>
              <button :disabled="newCategory ===''" type="button" class="btn btn-sm btn-outline-primary"
                      data-bs-toggle="modal"
                      data-bs-dismiss="modal"
                      data-bs-target="#staticBackdropConfirm">ویرایش
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="col-12">
      <nav aria-label="Page navigation example">
        <ul class="pagination">
          <li class="  PaginationItemPrev" @click="updateCurrent(currentPageIndex - 1)">
            <nuxt-link class="page-link" to="/SuperAdmin/AddCategory">صفحه قبل</nuxt-link>
          </li>
          <li :class="pageIndex === pageNumber ? 'page-item active' : 'page-item'"
              v-for="pageIndex in paginateTotal" @click="updateCurrent(pageIndex)">
            <nuxt-link class="page-link" to="/SuperAdmin/AddCategory">{{ pageIndex }}</nuxt-link>
          </li>
          <li class=" PaginationItemNext " @click="updateCurrent(currentPageIndex + 1)">
            <nuxt-link class="page-link" to="/SuperAdmin/AddCategory">صفحه بعد</nuxt-link>
          </li>
        </ul>
      </nav>
    </div>
  </div>
</template>

<script>
export default {
  name: "AddCategory",
  layout: 'SuperAdminPanel',
  data() {
    return {
      searchKey:null,
      showAdd: false,
      newMainCategory: '',
      newSubCategory: '',
      mainCategoryId: null,
      allCategory: [],
      toggleArrow: false,
      editedCategory: '',
      newCategory: '',
      totalCount: null,
      paginateTotal: null,
      count: 10,
      pageNumber: 1,
      currentPageIndex: 1,
      allCategoryNotPagination: []
    }
  },
  watch:{
    searchKey: function (val, oldVal) {
      this.$axios.post(`AdsCategory/GetAll`, null, {
        
        params: {
          pageNumber: 1,
          count: 10,
          search:val
        }
      }).then((response) => {
        if (response.status === 200) {

          this.allCategory = response.data.adsCategories

          this.totalCount = response.data.adsCategoryCount

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
    dataModal(data) {
      this.editedCategory = data

    },
    addMainCategory() {
      if (this.newMainCategory.length === 0) {
        this.$toast.error('لطفا عنوان دسته بندی را وارد کنید ')
      } else {
        this.$axios.post(`AdsCategory/Create`, {
          name: this.newMainCategory,
          parentId: null
        }, {
         
        }).then((response) => {
          if (response.status === 200) {
            this.$nuxt.$loading.start();
            this.getAllCategory(this.pageNumber, this.count,this.searchKey);
            this.$toast.success('دسته بندی اصلی با موفقیت افزوده شد. ')
            this.newMainCategory = ''

          }
        }).catch((error) => {
          console.log(error)
        })
      }
    },
    addSubCategory() {
      if (this.newSubCategory === '') {
        this.$toast.error('لطفا عنوان دسته بندی را وارد کنید ')
      } else if (this.mainCategoryId === null) {
        this.$toast.error('لطفا دسته بندی اصلی را انخاب کنید ')
      } else {
        this.$axios.post(`AdsCategory/Create`, {
          name: this.newSubCategory,
          parentId: this.mainCategoryId
        }, {
         
        }).then((response) => {
          if (response.status === 200) {
            this.showAdd = false;
            this.newSubCategory = '';
            this.mainCategoryId = null;
            this.$nuxt.$loading.start();
            this.getAllCategory(this.pageNumber, this.count,this.searchKey);
            this.getAllCategoryNotPagination();
            this.$toast.success('دسته بندی غیراصلی با موفقیت افزوده شد. ')
          }
        }).catch((error) => {
          console.log(error)
        })
      }
    },
    getAllCategory(pageNumber, count,search) {
      this.$axios.post(`AdsCategory/GetAll`, null, {
        
        params: {
          pageNumber: pageNumber,
          count: count,
          search:search
        }
      }).then((response) => {
        if (response.status === 200) {

          this.allCategory = response.data.adsCategories

          this.totalCount = response.data.adsCategoryCount

          this.paginateTotal = Math.ceil(this.totalCount / this.count);

          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    getAllCategoryNotPagination() {
      this.$axios.post(`AdsCategory/GetAll`, null, {
        
        params: {
          pageNumber: 1,
          count: 100,
          search:null
        }
      }).then((response) => {
        if (response.status === 200) {
          this.allCategoryNotPagination = response.data.adsCategories

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
        this.toggleArrow = !this.toggleArrow;
      } else {
        this.ascending = true;
        this.sortColumn = col;
      }

      var ascending = this.ascending;

      this.allCategory.sort(function (a, b) {
        if (a[col] > b[col]) {
          return ascending ? 1 : -1
        } else if (a[col] < b[col]) {
          return ascending ? -1 : 1
        }
        return 0;
      })
    },
    editCategory() {
      if (this.newCategory === '') {
        this.$toast.error('لطفا عنوان جدی دسته بندی را وارد کنید.')
      } else {
        this.$axios.post(`AdsCategory/Update`, {
          adsCategoryId: this.editedCategory.MainAdsCategoryId,
          name: this.newCategory,
          parentId: this.editedCategory.ParentId,
        }, {
         
        }).then((response) => {
          if (response.status === 200) {
            this.newCategory = '';
            this.showAdd = false;
            document.querySelector('.CloseModal').click();
            this.$nuxt.$loading.start();
            this.getAllCategory(this.pageNumber, this.count,this.searchKey);
            this.$toast.success('ویرایش دسته بندی با موفقیت انجام شد.')
          }
        }).catch((error) => {
          console.log(error)
        })
      }
    },
    deleteCategory() {
      this.$axios.post(`AdsCategory/Delete?id=${this.editedCategory.MainAdsCategoryId}`, null, {
       
      }).then((response) => {
        if (response.status === 200) {
          this.showAdd = false;
          this.$nuxt.$loading.start();
          this.getAllCategory(this.pageNumber, this.count,this.searchKey);
          this.$toast.success('حذف دسته بندی با موفقیت انجام شد.')
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    updateCurrent(pageNumber) {
      if (pageNumber === 0 || pageNumber > this.paginateTotal) {
        return true
      } else {
        this.$nuxt.$loading.start()
        this.getAllCategory(pageNumber, this.count,this.searchKey);
        this.pageNumber = pageNumber;
        this.currentPageIndex = pageNumber;
      }
    }
  },
  mounted() {
    // this.$nextTick(() => {
    //   this.$nuxt.$loading.start()
    // })
    this.getAllCategory(this.pageNumber, this.count,this.searchKey);
  },
  computed: {
    AllCategory(){
      const data = this.allCategory

    },
    columns() {
      if (this.allCategory.length === 0) {
        return [];
      }
      return Object.keys(this.allCategory[0])
    }
  },
}
</script>

<style >
.buttonStyle {
  padding: 0.5rem;
  border: 1px solid #3a48db;
}

.buttonStyle:hover {
  padding: 0.5rem;
  background-color: #3a48db;
  border: 1px solid #3a48db;
  color: #fff;

}

.AddCategoryBtn{
  background-color: #3a48db;
  color: white;
}

.selectWidth {
  width: 30rem;
}

.PaginationItemNext a{
  border-top-left-radius: 5px!important;
  border-bottom-left-radius: 5px!important;
  border-top-right-radius: 0px!important;
  border-bottom-right-radius: 0px!important;
}
.PaginationItemPrev a{
  border-top-left-radius: 0px!important;
  border-bottom-left-radius: 0px!important;
  border-top-right-radius: 5px!important;
  border-bottom-right-radius: 5px!important;
}



.selectWidth::placeholder {
  color: #cccccc;
  font-size: 10px;
}

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


.modal {

}

.btn-close {
  position: absolute;
  left: 1rem;
}

tr {
  padding: 1rem;
}
</style>
