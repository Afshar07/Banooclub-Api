<template>
  <div class="row adminPanelBox">
    <div class="modal fade my-0" id="staticBackdrop3"
         tabindex="-1" aria-labelledby="staticBackdrop2Label" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="staticBackdrop3Label">عدم پذیرش</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="row">
              <div class="col-12 my-3">
                <div class="flex flex-column gap-3">
                  <div>تصویر کارت ملی :</div>
                  <div class="w-100">
                    <img class="img-fluid"
                         :src="`https://BanooClubapi.simagar.com/${approvedPost.kartMelliImage}`"/>
                  </div>
                </div>
              </div>
              <div class="col-12">
                <div class="flex flex-column gap-3">
                  <div>تصویر پاسپورت :</div>
                  <div class="w-100">
                    <img class="img-fluid"
                         :src="`https://BanooClubapi.simagar.com/${approvedPost.passportImage}`"/>
                  </div>
                </div>
              </div>

            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-sm btn-outline-secondary" data-bs-dismiss="modal">بستن</button>

          </div>
        </div>
      </div>
    </div>
    <div class="col-12 pt-5 border-bottom w-auto pb-2 border-primary border-2 text-muted">
      لیست افراد
    </div>
    <div class="col-12 my-3">
      <nav>
        <div class="nav nav-tabs" id="nav-tab" role="tablist">
          <button class="nav-link active" id="nav-home-tab" data-bs-toggle="tab" data-bs-target="#nav-home"
                  type="button" role="tab" aria-controls="nav-home" aria-selected="true">در انتظار تایید
          </button>
          <button class="nav-link" id="nav-profile-tab" data-bs-toggle="tab" data-bs-target="#nav-profile"
                  type="button" role="tab" aria-controls="nav-profile" aria-selected="false"
                  @click="callRejectApi">رد
            شده
          </button>
          <button class="nav-link" id="nav-contact-tab" data-bs-toggle="tab" data-bs-target="#nav-contact"
                  type="button" role="tab" aria-controls="nav-contact" aria-selected="false" @click="callAcceptApi">
            تایید شده
          </button>
        </div>
      </nav>
      <div class="tab-content" id="nav-tabContent">
        <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
          <div class="col-md-12 my-3">
            <input v-model="searchKey" type="text" class="form-control" placeholder="جست و جو">
          </div>
          <div class="p-3 my-3">
            <table class="table table-bordered table-hover table-bordered dataTable table-striped my-3">
              <thead>
              <tr>
                <th @click="sortTable('roomateDocId')">
                  <div class="d-flex">
                    <div>
                      تصاویر
                    </div>
                    <div class="me-auto">
                      <font-awesome-icon icon="long-arrow-alt-up"
                                         :color="!toggleArrow ?'#ccc':'#000'" size="xs"/>
                      <font-awesome-icon icon="long-arrow-alt-down"
                                         :color="toggleArrow ?'#ccc':'#000'" size="xs"/>
                    </div>
                  </div>
                </th>
                <th @click="sortTable('userId')">
                  <div class="d-flex">
                    <div>
                      کاربر
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
                <th @click="sortTable('updatedDate')">
                  <div class="d-flex">
                    <div>
                      آخرین تاریخ بروز رسانی
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
                <th class="text-center">وضعیت</th>
                <th class="text-center">عملیات</th>
              </tr>
              </thead>
              <tbody>
              <tr v-for="row in NotConfirmed">
                <td>
                  <img v-if="row.kartMelliImage!==null" class="mx-1 smallPicture"
                       :src="`https://BanooClubapi.simagar.com/${row.kartMelliImage}`"/>
                  <img v-if="row.passportImage!==null" class="mx-1 smallPicture"
                       :src="`https://BanooClubapi.simagar.com/${row.passportImage}`"/>
                </td>
                <!--                <td>{{ row.roomateDocId }}</td>-->
                <td>{{ row.userInfo.name + '' + row.userInfo.familyName }}</td>
                <td>{{ row.updatedDate | moment("jYYYY/jM/jD HH:mm") }}</td>
                <td v-html="row.status === 2 ? 'در انتظار تایید' :''"></td>
                <td>
                  <div class="d-flex gap-5 justify-content-center">
                    <div class="" @click="dataModal(row)">
                      <button type="button" class="btn btn-sm btn-outline-danger" data-bs-toggle="modal"
                              data-bs-target="#staticBackdrop2">
                        عدم پذیرش
                      </button>
                    </div>
                    <div class="" @click="dataModal(row)">
                      <button type="button" class="btn btn-sm btn-outline-primary" data-bs-toggle="modal"
                              data-bs-target="#staticBackdrop">
                        تایید
                      </button>
                    </div>
                  </div>
                </td>
              </tr>
              </tbody>
            </table>
            <div class="col-12">
              <nav aria-label="Page navigation example">
                <ul class="pagination">
                  <li class="page-item" @click="updateCurrent(currentPageIndex - 1)">
                    <nuxt-link class="page-link" to="/SuperAdmin/WaitingListApproved">Previous</nuxt-link>
                  </li>
                  <li :class="pageIndex === pageNumber ? 'page-item active' : 'page-item'"
                      v-for="pageIndex in paginateTotal" @click="updateCurrent(pageIndex)">
                    <nuxt-link class="page-link" to="/SuperAdmin/WaitingListApproved">{{ pageIndex }}</nuxt-link>
                  </li>
                  <li class="page-item" @click="updateCurrent(currentPageIndex + 1)">
                    <nuxt-link class="page-link" to="/SuperAdmin/WaitingListApproved">Next</nuxt-link>
                  </li>
                </ul>
              </nav>
            </div>
            <!-- Modal -->
            <div class="modal fade" id="staticBackdrop2"
                 tabindex="-1" aria-labelledby="staticBackdrop2Label" aria-hidden="true">
              <div class="modal-dialog">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 class="modal-title" id="staticBackdrop2Label">عدم پذیرش</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                    <div class="row">
                      <div class="col-12 my-3">
                        <div class="flex flex-column gap-3">
                          <div>تصویر کارت ملی :</div>
                          <div class="w-100">
                            <img class="img-fluid"
                                 :src="`https://BanooClubapi.simagar.com/${approvedPost.kartMelliImage}`"/>
                          </div>
                        </div>
                      </div>
                      <div class="col-12">
                        <div class="flex flex-column gap-3">
                          <div>تصویر پاسپورت :</div>
                          <div class="w-100">
                            <img class="img-fluid"
                                 :src="`https://BanooClubapi.simagar.com/${approvedPost.passportImage}`"/>
                          </div>
                        </div>
                      </div>
                      <div class="my-3">
                        <input class="w-100 rounded border" type="text" v-model.trim="stateDesc"
                               placeholder="دلیل عدم پذیرش..."/>
                      </div>
                    </div>
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-outline-secondary" data-bs-dismiss="modal">بستن</button>
                    <button type="button" class="btn btn-sm btn-outline-danger" data-bs-toggle="modal"
                            data-bs-dismiss="modal"
                            data-bs-target="#staticBackdropDeleteConfirm">رد کردن
                    </button>
                  </div>
                </div>
              </div>
            </div>
            <!-- Modal -->
            <div class="modal fade" id="staticBackdrop"
                  tabindex="-1"
                 aria-labelledby="staticBackdropLabel" aria-hidden="true">
              <div class="modal-dialog">
                <div class="modal-content">
                  <div class="modal-header position-relative">
                    <h5 class="modal-title text-muted " id="staticBackdropLabel">تایید کاربر</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                    <div class="row">
                      <div class="col-12 my-3">
                        <div class="flex flex-column gap-3">
                          <div>تصویر کارت ملی :</div>
                          <div class="w-100">
                            <img class="img-fluid"
                                 :src="`https://BanooClubapi.simagar.com/${approvedPost.kartMelliImage}`"/>
                          </div>
                        </div>
                      </div>
                      <div class="col-12">
                        <div class="flex flex-column gap-3">
                          <div>تصویر پاسپورت :</div>
                          <div class="w-100">
                            <img class="img-fluid"
                                 :src="`https://BanooClubapi.simagar.com/${approvedPost.passportImage}`"/>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-outline-secondary closeApprove" data-bs-dismiss="modal">
                      بستن
                    </button>
                    <button type="button" class="btn btn-sm btn-outline-primary"
                            data-bs-dismiss="modal"
                            data-bs-toggle="modal"
                            data-bs-target="#staticBackdropConfirm">تایید
                    </button>
                  </div>
                </div>
              </div>
            </div>
            <div class="modal fade modalConfirm" id="staticBackdropDeleteConfirm" data-bs-backdrop="static"
                 data-bs-keyboard="false"
                 tabindex="-1" aria-labelledby="staticBackdropLabelDeleteConfirm" aria-hidden="true">
              <div class="modal-dialog">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 class="modal-title" id="staticBackdropLabelDeleteConfirm">عدم پذیرش </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                    آیا از عدم پذیرش اطمینان دارید ؟
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-outline-secondary closeApprove2"
                            data-bs-dismiss="modal">
                      خیر
                    </button>
                    <button type="button" class="btn btn-sm btn-outline-primary" data-bs-dismiss="modal"
                            data-bs-target="#staticBackdrop" @click="deleteApproved">تایید
                    </button>
                  </div>
                </div>
              </div>
            </div>
            <div class="modal fade modalConfirm" id="staticBackdropConfirm" data-bs-backdrop="static"
                 data-bs-keyboard="false"
                 tabindex="-1" aria-labelledby="staticBackdropLabelConfirm" aria-hidden="true">
              <div class="modal-dialog">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 class="modal-title" id="staticBackdropLabelConfirm">تایید کاربر</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                    آیا از تایید کاربر اطمینان دارید ؟
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-outline-secondary closeApprove2"
                            data-bs-dismiss="modal">
                      خیر
                    </button>
                    <button type="button" class="btn btn-sm btn-outline-primary" data-bs-dismiss="modal"
                            @click="approved">تایید
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">
          <div class="col-md-12 my-3">
            <input v-model="searchKey3" type="text" class="form-control" placeholder="جست و جو">
          </div>
          <table class="table table-bordered table-hover table-bordered dataTable table-striped my-3">

            <thead>
            <tr>
              <th @click="sortTableRejected('roomateDocId')">
                <div class="d-flex">
                  <div>
                    تصاویر
                  </div>
                  <div class="me-auto">
                    <font-awesome-icon icon="long-arrow-alt-up"
                                       :color="!toggleArrow ?'#ccc':'#000'" size="xs"/>
                    <font-awesome-icon icon="long-arrow-alt-down"
                                       :color="toggleArrow ?'#ccc':'#000'" size="xs"/>
                  </div>
                </div>
              </th>
              <th @click="sortTableRejected('userId')">
                <div class="d-flex">
                  <div>
                    کاربر
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
              <th @click="sortTableRejected('updatedDate')">
                <div class="d-flex">
                  <div>
                    آخرین تاریخ بروز رسانی
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
              <th class="text-center">وضعیت</th>
              <th class="text-center">عملیات</th>
            </tr>
            </thead>
            <tbody class="text-center">
            <tr v-for="row in rejectedList">
              <td>
                <img v-if="row.kartMelliImage!==null" class="mx-1" style="width: 20px;height: 20px"
                     :src="`https://BanooClubapi.simagar.com/${row.kartMelliImage}`"/>
                <img v-if="row.passportImage!==null" class="mx-1" style="width: 20px;height: 20px"
                     :src="`https://BanooClubapi.simagar.com/${row.passportImage}`"/>
              </td>
              <td>{{ row.userInfo.name + '' + row.userInfo.familyName }}</td>
              <td>{{ row.updatedDate | moment("jYYYY/jM/jD HH:mm") }}</td>
              <td v-html="row.status ===3 ?'تایید نشده است.':''"></td>
              <td>
                <div class="d-flex gap-5 justify-content-center">
                  <div class="" @click="dataModal(row)">
                    <button type="button" class="btn btn-sm SeeReportedPost" data-bs-toggle="modal" data-bs-target="#staticBackdrop3"><i class="far fa-eye"></i></button>
                  </div>
                </div>
              </td>
            </tr>
            </tbody>
          </table>
          <div class="col-12">
            <nav aria-label="Page navigation example">
              <ul class="pagination">
                <li class="page-item" @click="updateCurrentRejected(currentPageIndexReject -1 )">
                  <nuxt-link class="page-link" to="/SuperAdmin/WaitingListApproved">Previous</nuxt-link>
                </li>
                <li :class="pageIndexR === pageNumberReject ? 'page-item active' : 'page-item'"
                    v-for="pageIndexR in paginateTotalReject" @click="updateCurrentRejected(pageIndexR)">
                  <nuxt-link class="page-link" to="/SuperAdmin/WaitingListApproved">{{ pageIndexR }}</nuxt-link>
                </li>
                <li class="page-item" @click="updateCurrentRejected(currentPageIndexReject + 1)">
                  <nuxt-link class="page-link" to="/SuperAdmin/WaitingListApproved">Next</nuxt-link>
                </li>
              </ul>
            </nav>
          </div>
        </div>
        <div class="tab-pane fade" id="nav-contact" role="tabpanel" aria-labelledby="nav-contact-tab">
          <div class="col-md-12 my-3">
            <input v-model="searchKey2" type="text" class="form-control" placeholder="جست و جو">
          </div>
          <table class="table table-bordered table-hover table-bordered dataTable table-striped my-3">
            <thead>
            <tr>
              <th @click="sortTableAccept('roomateDocId')">
                <div class="d-flex">
                  <div>
                    تصاویر
                  </div>
                  <div class="me-auto">
                    <font-awesome-icon icon="long-arrow-alt-up"
                                       :color="!toggleArrow ?'#ccc':'#000'" size="xs"/>
                    <font-awesome-icon icon="long-arrow-alt-down"
                                       :color="toggleArrow ?'#ccc':'#000'" size="xs"/>
                  </div>
                </div>
              </th>
              <th @click="sortTableAccept('userId')">
                <div class="d-flex">
                  <div>
                    کاربر
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
              <th @click="sortTableAccept('updatedDate')">
                <div class="d-flex">
                  <div>
                    آخرین تاریخ بروز رسانی
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
              <th class="text-center">وضعیت</th>
              <th class="text-center">عملیات</th>
            </tr>
            </thead>
            <tbody class="text-center">
            <tr v-for="row in NotAprrovedList.filter(m => m.status === 1)">
              <td>
                <img v-if="row.kartMelliImage!==null" class="mx-1" style="width: 20px;height: 20px"
                     :src="`https://BanooClubapi.simagar.com/${row.kartMelliImage}`"/>
                <img v-if="row.passportImage!==null" class="mx-1" style="width: 20px;height: 20px"
                     :src="`https://BanooClubapi.simagar.com/${row.passportImage}`"/>
              </td>
              <td>{{ row.userInfo.name + '' + row.userInfo.familyName }}</td>
              <td>{{ row.updatedDate | moment("jYYYY/jM/jD HH:mm") }}</td>
              <td v-html="row.status === 1 ? 'تایید شده است.':''"></td>
              <td>
                <div class="d-flex gap-5 justify-content-center">
                  <div class="" @click="dataModal(row)">
                    <button type="button" class="btn btn-sm SeeReportedPost" data-bs-toggle="modal" data-bs-target="#staticBackdrop3"><i class="far fa-eye"></i></button>
                  </div>
                </div>
              </td>
            </tr>
            </tbody>
          </table>
          <div class="col-12">
            <nav aria-label="Page navigation example">
              <ul class="pagination">
                <li class="page-item" @click="updateCurrentAccept(currentPageIndexAccept - 1)">
                  <nuxt-link class="page-link" to="/SuperAdmin/WaitingListApproved">Previous</nuxt-link>
                </li>
                <li :class="pageIndex === pageNumberAccept ? 'page-item active' : 'page-item'"
                    v-for="pageIndex in paginateTotalAccept" @click="updateCurrentAccept(pageIndex)">
                  <nuxt-link class="page-link" to="/SuperAdmin/WaitingListApproved">{{ pageIndex }}</nuxt-link>
                </li>
                <li class="page-item" @click="updateCurrentAccept(currentPageIndexAccept + 1)">
                  <nuxt-link class="page-link" to="/SuperAdmin/WaitingListApproved">Next</nuxt-link>
                </li>
              </ul>
            </nav>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "WaitingListApproved",
  layout: 'SuperAdminPanel',
  data() {
    return {
      NotAprrovedList: [],
      NotConfirmed: [],
      ascending: false,
      sortColumn: '',
      toggleArrow: false,
      approvedPost: '',
      stateDesc: '',
      rejectedList: [],
      totalCount: null,
      totalCountAccept: null,
      totalCountReject: null,
      paginateTotal: null,
      paginateTotalAccept: null,
      paginateTotalReject: null,
      count: 10,
      pageNumber: 1,
      pageNumberReject: 1,
      pageNumberAccept: 1,
      currentPageIndex: 1,
      currentPageIndexReject: 1,
      currentPageIndexAccept: 1,
      searchKey:null,
      searchKey2:null,
      searchKey3:null
    }
  },
  methods: {
    updateCurrent(pageNumber) {
      if (pageNumber === 0 || pageNumber > this.totalCount) {
        return true;
      } else {
        this.$nuxt.$loading.start()
        this.getNotConfirmed(pageNumber, this.count,this.searchKey);
        this.pageNumber = pageNumber;
        this.currentPageIndex = pageNumber;
      }
    },
    updateCurrentAccept(pageNumber) {
      if (pageNumber === 0 || pageNumber > this.paginateTotalAccept) {
        return true;
      } else {
        this.$nuxt.$loading.start()
        this.getAllNotAprrovedList(this.pageNumberAccept, this.count,this.searchKey2);
        this.pageNumberAccept = pageNumber;
        this.currentPageIndexAccept = pageNumber;
      }

    },
    updateCurrentRejected(pageNumber) {
      if (pageNumber === 0 || pageNumber > this.paginateTotalReject) {
        return true;
      } else {
        this.$nuxt.$loading.start()
        this.getAllNotAprrovedList(this.pageNumberReject, this.count,this.searchKey2);
        this.pageNumberReject = pageNumber;
        this.currentPageIndexReject = pageNumber;
      }
    },
    getRejected(pageNumber, count,search) {
      this.$axios.post(`RoomateDoc/GetRejected`, null, {
       
        params: {
          pageNumber: pageNumber,
          count: count,
          search:search
        }
      }).then((response) => {
        if (response.status === 200) {
          this.rejectedList = response.data.roomateDocs
          this.totalCountReject = response.data.roomateDocsCount
          this.paginateTotalReject = Math.ceil(this.totalCountReject / this.count);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    getAllNotAprrovedList(pageNumber, count,search) {
      this.$axios.post(`RoomateDoc/GetAll`, null, {
       
        params: {
          pageNumber: pageNumber,
          count: count,
          search:search
        }
      }).then((response) => {
        if (response.status === 200) {
          this.NotAprrovedList = response.data.roomateDocs
          this.totalCountAccept = response.data.roomateDocsCount
          this.paginateTotalAccept = Math.ceil(this.totalCountAccept / this.count);
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

      this.NotConfirmed.sort(function (a, b) {
        if (a[col] > b[col]) {
          return ascending ? 1 : -1
        } else if (a[col] < b[col]) {
          return ascending ? -1 : 1
        }
        return 0;
      })
    },
    sortTableRejected(col) {
      if (this.sortColumn === col) {
        this.ascending = !this.ascending;
        this.toggleArrow = !this.toggleArrow;
      } else {
        this.ascending = true;
        this.sortColumn = col;
      }

      var ascending = this.ascending;

      this.rejectedList.sort(function (a, b) {
        if (a[col] > b[col]) {
          return ascending ? 1 : -1
        } else if (a[col] < b[col]) {
          return ascending ? -1 : 1
        }
        return 0;
      })
    },
    sortTableAccept(col) {
      if (this.sortColumn === col) {
        this.ascending = !this.ascending;
        this.toggleArrow = !this.toggleArrow;
      } else {
        this.ascending = true;
        this.sortColumn = col;
      }

      var ascending = this.ascending;

      this.NotAprrovedList.sort(function (a, b) {
        if (a[col] > b[col]) {
          return ascending ? 1 : -1
        } else if (a[col] < b[col]) {
          return ascending ? -1 : 1
        }
        return 0;
      })
    },
    dataModal(data) {
      this.approvedPost = data
    },
    approved() {
      this.$axios.post(`RoomateDoc/ChangeStatus`, null, {
       
        params: {
          roomateDocId: this.approvedPost.roomateDocId,
          StateDesc: '',
          status: 1
        }
      }).then((response) => {
        if (response.status === 200) {
          document.querySelector('.closeApprove').click();
          this.$nuxt.$loading.start()
          this.getNotConfirmed(this.pageNumber, this.count,this.searchKey);
          this.$toast.success('تایید با موفقیت انجام شد.')
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    deleteApproved() {
      this.$axios.post(`RoomateDoc/ChangeStatus`, null, {
       
        params: {
          roomateDocId: this.approvedPost.roomateDocId,
          StateDesc: this.stateDesc,
          status: 3
        }
      }).then((response) => {
        if (response.status === 200) {
          document.querySelector('.closeApprove2').click();
          this.$nuxt.$loading.start()
          this.getNotConfirmed(this.pageNumber, this.count,this.searchKey);
          this.$toast.success('عدم تایید با موفقیت انجام شد.')
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    getNotConfirmed(pageNumber, count,search) {
      this.$axios.post(`RoomateDoc/GetNotConfirmed`, null, {
       
        params: {
          pageNumber: pageNumber,
          count: count,
          search:search
        }
      }).then(
        (response) => {
          if (response.status === 200) {
            this.NotConfirmed = response.data.roomateDocs
            this.totalCount = response.data.roomateDocsCount
            this.paginateTotal = Math.ceil(this.totalCount / this.count);
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false
          }
        }
      ).catch((error) => {
        console.log(error)
      })
    },
    callRejectApi() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start()
      })
      this.getRejected(this.pageNumberReject, this.count);
    },
    callAcceptApi() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start()
      })
      this.getAllNotAprrovedList(this.pageNumberAccept, this.count,this.searchKey2);
    }
  },
  watch:{
    searchKey:function (val,oldVal){
      this.$axios.post(`RoomateDoc/GetNotConfirmed`, null, {
       
        params: {
          pageNumber: 1,
          count: 10,
          search:val
        }
      }).then(
        (response) => {
          if (response.status === 200) {
            this.NotConfirmed = response.data.roomateDocs
            this.totalCount = response.data.roomateDocsCount
            this.paginateTotal = Math.ceil(this.totalCount / this.count);
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false
          }
        }
      ).catch((error) => {
        console.log(error)
      })

    },
    searchKey2:function (val,oldVal){
      this.$axios.post(`RoomateDoc/GetAll`, null, {
       
        params: {
          pageNumber: 1,
          count: 10,
          search:val
        }
      }).then((response) => {
        if (response.status === 200) {
          this.NotAprrovedList = response.data.roomateDocs
          this.totalCountAccept = response.data.roomateDocsCount
          this.paginateTotalAccept = Math.ceil(this.totalCountAccept / this.count);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
        }
      }).catch((error) => {
        console.log(error)
      })

    },
    searchKey3:function (val,oldVal){
      this.$axios.post(`RoomateDoc/GetRejected`, null, {
       
        params: {
          pageNumber: 1,
          count: 10,
          search:val
        }
      }).then((response) => {
        if (response.status === 200) {
          this.rejectedList = response.data.roomateDocs
          this.totalCountReject = response.data.roomateDocsCount
          this.paginateTotalReject = Math.ceil(this.totalCountReject / this.count);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
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
    this.getAllNotAprrovedList(this.pageNumberAccept, this.count,this.searchKey2);
    this.getNotConfirmed(this.pageNumber, this.count,this.searchKey);
  },
  computed: {
    columns() {
      if (this.NotConfirmed.length === 0) {
        return [];
      }
      return Object.keys(this.NotConfirmed[0])
    },
    columnsAccepted() {
      if (this.NotConfirmed.length === 0) {
        return [];
      }
      return Object.keys(this.NotAprrovedList[0])
    },
    columnsRejected() {
      if (this.NotConfirmed.length === 0) {
        return [];
      }
      return Object.keys(this.NotAprrovedList[0])
    }
  },
}
</script>

<style scoped>
.smallPicture {
  width: 20px;
  height: 20px
}

.smallPicture:hover {
}

.adminPanelBox {
  background-color: #fff;
  padding: 1rem;
}

.btn-close {
  position: absolute;
  left: 1rem;
}

input::placeholder {
  font-size: 14px;
  color: #cccccc;
}

#staticBackdropConfirm, #staticBackdropDeleteConfirm {
  margin: 25vh auto;
  left: 0;
}
</style>
