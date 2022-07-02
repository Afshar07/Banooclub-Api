<template>
  <div class="row adminPanelBox">
    <div class="col-12 my-3">
      <nav>
        <div class="nav nav-tabs" id="nav-tab" role="tablist">
          <button
            class="nav-link active"
            id="nav-home-tab"
            data-bs-toggle="tab"
            data-bs-target="#nav-home"
            type="button"
            role="tab"
            aria-controls="nav-home"
            aria-selected="true"
          >
            در انتظار تایید
          </button>
          <button
            class="nav-link"
            id="nav-profile-tab"
            data-bs-toggle="tab"
            data-bs-target="#nav-profile"
            type="button"
            role="tab"
            aria-controls="nav-profile"
            aria-selected="false"
            @click="getRejectedVideo(1, 10, searchRejected)"
          >
            رد شده
          </button>
          <button
            class="nav-link"
            id="nav-contact-tab"
            data-bs-toggle="tab"
            data-bs-target="#nav-contact"
            type="button"
            role="tab"
            aria-controls="nav-contact"
            aria-selected="false"
            @click="getConfirmedVideo(1, 10, searchConfirmed)"
          >
            تایید شده
          </button>
        </div>
      </nav>
      <div class="tab-content" id="nav-tabContent">
        <div
          class="tab-pane fade show active"
          id="nav-home"
          role="tabpanel"
          aria-labelledby="nav-home-tab"
        >
          <div v-if="NotConfirmedVideo.length > 0" class="col-12">
            <table
              class="table table-bordered table-hover table-bordered dataTable table-striped my-3"
            >
              <thead>
                <tr>
                  <th scope="col">شناسه</th>
                  <th scope="col">ویدیو</th>
                  <th scope="col">کاربر</th>
                  <th scope="col">وضعیت</th>
                  <th scope="col">عملیات</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="item in NotConfirmedVideo" :key="item.userSettingId">
                  <th scope="row">{{ item.userSettingId }}</th>
                  <td>
                    <video
                      :src="BaseUrl + item.videoIdentity"
                      width="50px"
                      height="50px"
                    ></video>
                  </td>
                  <td v-if="item.userInfo">
                    {{ item.userInfo.name + " " + item.userInfo.familyName }}
                  </td>
                  <td>
                    <span
                      v-if="item.videoIdentityStatus === 3"
                      class="badge bg-success text-white"
                      >تایید شده</span
                    >
                    <span
                      v-if="item.videoIdentityStatus === 4"
                      class="badge bg-danger text-white"
                      >رد شده</span
                    >
                    <span
                      v-if="item.videoIdentityStatus === 2"
                      class="badge bg-secondary text-white"
                      >در انتظار تایید</span
                    >
                  </td>
                  <td>
                    <div class="d-flex gap-5 justify-content-evenly">
                      <div class="ShowDoc" @click="SetSingleVideo(item)">
                        <i
                          class="fas fa-eye"
                          data-bs-toggle="modal"
                          data-bs-target="#ShowNotConfirmedVideo"
                        ></i>
                      </div>
                    </div>
                  </td>
                </tr>
              </tbody>
            </table>
            <div
              class="modal fade"
              id="ShowNotConfirmedVideo"
              data-bs-keyboard="false"
              tabindex="-1"
              aria-labelledby="staticBackdropLabel"
              aria-hidden="true"
            >
              <div class="modal-dialog" style="max-width: 800px !important">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 class="modal-title">مشاهده ویدیو احراز هویت</h5>
                    <button
                      type="button"
                      class="btn-close"
                      data-bs-dismiss="modal"
                      aria-label="Close"
                    ></button>
                  </div>

                  <div class="modal-body">
                    <div class="row">
                      <div class="col-md-2">
                        <div class="d-flex align-items-center gap-2">
                          <i class="fas fa-image"></i>
                          <small>ویدیو</small>
                        </div>
                      </div>
                      <div class="col-md-8">
                        <video
                          v-if="SingleVideo"
                          :src="BaseUrl + SingleVideo"
                          class="img-fluid rounded"
                          controls
                        ></video>
                      </div>
                    </div>
                  </div>
                  <div class="modal-footer">
                    <button
                      type="button"
                      class="btn btn-sm btn-danger"
                      @click="SetNotConfirmedStatus(4)"
                      data-bs-toggle="modal"
                      data-bs-target="#UploadedNotConfirmedAction"
                    >
                      رد کردن
                    </button>
                    <button
                      type="button"
                      class="btn btn-sm btn-primary"
                      @click="SetNotConfirmedStatus(3)"
                      data-bs-toggle="modal"
                      data-bs-target="#UploadedNotConfirmedAction"
                    >
                      تایید
                    </button>
                  </div>
                </div>
              </div>
            </div>
            <div
              class="modal fade"
              id="UploadedNotConfirmedAction"
              data-bs-keyboard="false"
              tabindex="-1"
              aria-labelledby="staticBackdropLabel"
              aria-hidden="true"
            >
              <div class="modal-dialog">
                <div class="modal-content">
                  <div class="modal-header position-relative">
                    <button
                      type="button"
                      class="btn-close"
                      data-bs-dismiss="modal"
                      aria-label="Close"
                    ></button>
                  </div>
                  <div class="modal-body">
                    آیا از انجام عملیات مورد نظر اطمینان دارید؟
                  </div>
                  <div class="modal-footer">
                    <button
                      type="button"
                      class="btn btn-sm btn-danger"
                      data-bs-dismiss="modal"
                    >
                      خیر
                    </button>
                    <button
                      type="button"
                      class="btn btn-sm btn-success"
                      data-bs-dismiss="modal"
                      @click="ChangeVideoIdentityStatus()"
                    >
                      بله
                    </button>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-12">
              <nav aria-label="Page navigation example">
                <ul class="pagination">
                  <!--                      <div v-for="i in TotalPages" :key="i" class="PageIndicator   d-flex justify-content-center align-items-center" :class="GetIndicatorClassName(i)" @click="ChangeActivePage(i)">-->
                  <!--                        <small>{{i}}</small>-->
                  <!--                      </div>-->
                  <li class="PaginationItemPrev">
                    <a
                      class="page-link"
                      @click="DecrementNotConfirmed"
                      style="cursor: pointer"
                      >صفحه قبل</a
                    >
                  </li>

                  <li v-for="item in totalNotConfirmedPages" :key="item">
                    <a
                      class="page-link"
                      @click="changeNotConfirmedVideoIdentityActivePage(item)"
                      :class="GetActiveClass(item)"
                      >{{ item }}</a
                    >
                  </li>

                  <li>
                    <a
                      class="page-link"
                      @click="IncrementNotConfirmed"
                      style="cursor: pointer"
                      >صفحه بعد</a
                    >
                  </li>
                </ul>
              </nav>
            </div>
          </div>
          <div v-else class="col-md-12">
            <p>داده ای یافت نشد</p>
          </div>
        </div>
        <div
          class="tab-pane fade"
          id="nav-profile"
          role="tabpanel"
          aria-labelledby="nav-profile-tab"
        >
          <div v-if="RejectedVideo.length > 0" class="col-12">
            <table
              class="table table-bordered table-hover table-bordered dataTable table-striped my-3"
            >
              <thead>
                <tr>
                  <th scope="col">شناسه</th>
                  <th scope="col">ویدیو</th>
                  <th scope="col">کاربر</th>
                  <th scope="col">وضعیت</th>
                  <th scope="col">عملیات</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="item in RejectedVideo" :key="item.userSettingId">
                  <th scope="row">{{ item.userSettingId }}</th>
                  <td>
                    <video
                      :src="BaseUrl + item.videoIdentity"
                      width="50px"
                      height="50px"
                    ></video>
                  </td>
                  <td v-if="item.userInfo">
                    {{ item.userInfo.name + " " + item.userInfo.familyName }}
                  </td>
                  <td>
                    <span
                      v-if="item.videoIdentityStatus === 3"
                      class="badge bg-success text-white"
                      >تایید شده</span
                    >
                    <span
                      v-if="item.videoIdentityStatus === 4"
                      class="badge bg-danger text-white"
                      >رد شده</span
                    >
                    <span
                      v-if="item.videoIdentityStatus === 2"
                      class="badge bg-secondary text-white"
                      >در انتظار تایید</span
                    >
                  </td>
                  <td>
                    <div class="d-flex gap-5 justify-content-evenly">
                      <div
                        class="ShowDoc"
                        @click="SetSingleRejectedVideo(item)"
                      >
                        <i
                          class="fas fa-eye"
                          data-bs-toggle="modal"
                          data-bs-target="#ShowRejectedVideo"
                        ></i>
                      </div>
                    </div>
                  </td>
                </tr>
              </tbody>
            </table>
            <div
              class="modal fade"
              id="ShowRejectedVideo"
              data-bs-keyboard="false"
              tabindex="-1"
              aria-labelledby="staticBackdropLabel"
              aria-hidden="true"
            >
              <div class="modal-dialog" style="max-width: 800px !important">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 class="modal-title">مشاهده ویدیو احراز هویت</h5>
                    <button
                      type="button"
                      class="btn-close"
                      data-bs-dismiss="modal"
                      aria-label="Close"
                    ></button>
                  </div>

                  <div class="modal-body">
                    <div class="row">
                      <div class="col-md-2">
                        <div class="d-flex align-items-center gap-2">
                          <i class="fas fa-image"></i>
                          <small>ویدیو</small>
                        </div>
                      </div>
                      <div class="col-md-8">
                        <video
                          v-if="SingleRejectedVideo"
                          :src="BaseUrl + SingleRejectedVideo"
                          class="img-fluid rounded"
                          controls
                        ></video>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div class="col-12">
              <nav aria-label="Page navigation example">
                <ul class="pagination">
                  <!--                      <div v-for="i in TotalPages" :key="i" class="PageIndicator   d-flex justify-content-center align-items-center" :class="GetIndicatorClassName(i)" @click="ChangeActivePage(i)">-->
                  <!--                        <small>{{i}}</small>-->
                  <!--                      </div>-->
                  <li class="PaginationItemPrev">
                    <a
                      class="page-link"
                      @click="DecrementRejected"
                      style="cursor: pointer"
                      >صفحه قبل</a
                    >
                  </li>

                  <li v-for="item in totalRejectedPages" :key="item">
                    <a
                      class="page-link"
                      @click="changeRejectedVideoIdentityActivePage(item)"
                      :class="GetActiveClassRejected(item)"
                      >{{ item }}</a
                    >
                  </li>

                  <li>
                    <a
                      class="page-link"
                      @click="IncrementRejected"
                      style="cursor: pointer"
                      >صفحه بعد</a
                    >
                  </li>
                </ul>
              </nav>
            </div>
          </div>
          <div v-else class="col-md-12">
            <p>داده ای یافت نشد</p>
          </div>
        </div>
        <div
          class="tab-pane fade"
          id="nav-contact"
          role="tabpanel"
          aria-labelledby="nav-contact-tab"
        >
          <div v-if="ConfirmedVideo.length > 0" class="col-12">
            <table
              class="table table-bordered table-hover table-bordered dataTable table-striped my-3"
            >
              <thead>
                <tr>
                  <th scope="col">شناسه</th>
                  <th scope="col">ویدیو</th>
                  <th scope="col">کاربر</th>
                  <th scope="col">وضعیت</th>
                  <th scope="col">عملیات</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="item in ConfirmedVideo" :key="item.userSettingId">
                  <th scope="row">{{ item.userSettingId }}</th>
                  <td>
                    <video
                      :src="BaseUrl + item.videoIdentity"
                      width="50px"
                      height="50px"
                    ></video>
                  </td>
                  <td v-if="item.userInfo">
                    {{ item.userInfo.name + " " + item.userInfo.familyName }}
                  </td>
                  <td>
                    <span
                      v-if="item.videoIdentityStatus === 3"
                      class="badge bg-success text-white"
                      >تایید شده</span
                    >
                    <span
                      v-if="item.videoIdentityStatus === 4"
                      class="badge bg-danger text-white"
                      >رد شده</span
                    >
                    <span
                      v-if="item.videoIdentityStatus === 2"
                      class="badge bg-secondary text-white"
                      >در انتظار تایید</span
                    >
                  </td>
                  <td>
                    <div class="d-flex gap-5 justify-content-evenly">
                      <div
                        class="ShowDoc"
                        @click="SetSingleConfirmedVideo(item)"
                      >
                        <i
                          class="fas fa-eye"
                          data-bs-toggle="modal"
                          data-bs-target="#ShowConfirmedVideo"
                        ></i>
                      </div>
                    </div>
                  </td>
                </tr>
              </tbody>
            </table>
            <div
              class="modal fade"
              id="ShowConfirmedVideo"
              data-bs-keyboard="false"
              tabindex="-1"
              aria-labelledby="staticBackdropLabel"
              aria-hidden="true"
            >
              <div class="modal-dialog" style="max-width: 800px !important">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 class="modal-title">مشاهده ویدیو احراز هویت</h5>
                    <button
                      type="button"
                      class="btn-close"
                      data-bs-dismiss="modal"
                      aria-label="Close"
                    ></button>
                  </div>

                  <div class="modal-body">
                    <div class="row">
                      <div class="col-md-2">
                        <div class="d-flex align-items-center gap-2">
                          <i class="fas fa-image"></i>
                          <small>ویدیو</small>
                        </div>
                      </div>
                      <div class="col-md-8">
                        <video
                          v-if="SingleConfirmedVideo"
                          :src="BaseUrl + SingleConfirmedVideo"
                          class="img-fluid rounded"
                          controls
                        ></video>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div class="col-12">
              <nav aria-label="Page navigation example">
                <ul class="pagination">
                  <!--                      <div v-for="i in TotalPages" :key="i" class="PageIndicator   d-flex justify-content-center align-items-center" :class="GetIndicatorClassName(i)" @click="ChangeActivePage(i)">-->
                  <!--                        <small>{{i}}</small>-->
                  <!--                      </div>-->
                  <li class="PaginationItemPrev">
                    <a
                      class="page-link"
                      @click="DecrementConfirmed"
                      style="cursor: pointer"
                      >صفحه قبل</a
                    >
                  </li>

                  <li v-for="item in totalConfirmedPages" :key="item">
                    <a
                      class="page-link"
                      @click="changeConfirmedVideoIdentityActivePage(item)"
                      :class="GetActiveClassConfirmed(item)"
                      >{{ item }}</a
                    >
                  </li>

                  <li>
                    <a
                      class="page-link"
                      @click="IncrementConfirmed"
                      style="cursor: pointer"
                      >صفحه بعد</a
                    >
                  </li>
                </ul>
              </nav>
            </div>
          </div>
          <div v-else class="col-md-12">
            <p>داده ای یافت نشد</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "VideoAuthentication",
  layout: "SuperAdminPanel",
  data() {
    return {
      ascending: false,
      sortColumn: "",
      toggleArrow: false,

      NotConfirmedVideo: [],
      AcceptedVideo: [],

      videoInfo: "",
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

      totalNotConfirmedPages: [],
      SelectedNotConfirmedPageId: 1,
      SingleVideo: "",
      searchNotConfirmed: "",
      VideoStatus: 0,
      SelectedUserSettingId: 0,

      RejectedVideo: [],
      totalRejectedPages: [],
      SelectedRejectedPageId: 1,
      SingleRejectedVideo: "",
      searchRejected: "",

      ConfirmedVideo: [],
      totalConfirmedPages: [],
      SelectedConfirmedPageId: 1,
      SingleConfirmedVideo: "",
      searchConfirmed: "",
    };
  },
  watch: {},
  methods: {
    ChangeVideoIdentityStatus() {
      this.$axios
        .post(
          `UserSetting/ChangeVideoStatusByAdmin?userSettingId=${this.SelectedUserSettingId}&videoIdentity=${this.VideoStatus}`,
          null,
          {
           
          }
        )
        .then((res) => {
          this.$toast.success("وضعیت ویدیو با موفقیت تغییر یافت");
          this.getNotCofirmedVideo(1, 5, this.searchNotConfirmed);
        });
    },
    SetNotConfirmedStatus(id) {
      this.VideoStatus = id;
    },
    DecrementNotConfirmed() {
      this.SelectedNotConfirmedPageId--;
      this.getNotCofirmedVideo(
        this.SelectedNotConfirmedPageId,
        5,
        this.searchNotConfirmed
      );
    },
    IncrementNotConfirmed() {
      this.SelectedNotConfirmedPageId++;
      this.getNotCofirmedVideo(
        this.SelectedNotConfirmedPageId,
        5,
        this.searchNotConfirmed
      );
    },
    getNotCofirmedVideo(pageNumber, count, search) {
      if (pageNumber > this.totalNotConfirmedPages.length) {
        pageNumber = 1;
      }
      if (pageNumber < 1) {
        pageNumber = this.totalNotConfirmedPages.length;
      }

      this.$axios
        .post("UserSetting/GetUploadedVideoIdentity", null, {
        
          params: {
            pageNumber: pageNumber,
            count: count,
            search: search,
          },
        })
        .then((response) => {
          this.totalNotConfirmedPages = [];
          const result = Math.ceil(response.data.userSettingsCount / 10);
          for (let i = 1; i <= result; i++) {
            this.totalNotConfirmedPages.push(i);
          }
          this.NotConfirmedVideo = response.data.userSettings;
        })
        .catch((error) => {
          console.log(error);
        });
    },
    SetSingleVideo(item) {
      this.SingleVideo = item.videoIdentity;
      this.SelectedUserSettingId = item.userSettingId;
    },
    changeNotConfirmedVideoIdentityActivePage(id) {
      this.SelectedNotConfirmedPageId = id;
    },
    GetActiveClass(id) {
      if (id === this.SelectedNotConfirmedPageId) {
        return "Active";
      } else {
        return "";
      }
    },

    DecrementRejected() {
      this.SelectedRejectedPageId--;
      this.getRejectedVideo(
        this.SelectedRejectedPageId,
        5,
        this.searchRejected
      );
    },
    IncrementRejected() {
      this.SelectedRejectedPageId++;
      this.getRejectedVideo(
        this.SelectedRejectedPageId,
        5,
        this.searchRejected
      );
    },
    getRejectedVideo(pageNumber, count, search) {
      if (pageNumber > this.totalRejectedPages.length) {
        pageNumber = 1;
      }
      if (pageNumber < 1) {
        pageNumber = this.totalRejectedPages.length;
      }

      this.$axios
        .post("UserSetting/GetRejectedVideoIdentity", null, {
        
          params: {
            pageNumber: pageNumber,
            count: count,
            search: search,
          },
        })
        .then((response) => {
          this.totalRejectedPages = [];
          const result = Math.ceil(response.data.userSettingsCount / 10);
          for (let i = 1; i <= result; i++) {
            this.totalRejectedPages.push(i);
          }
          this.RejectedVideo = response.data.userSettings;
        })
        .catch((error) => {
          console.log(error);
        });
    },
    SetSingleRejectedVideo(item) {
      this.SingleRejectedVideo = item.videoIdentity;
      this.SelectedUserSettingId = item.userSettingId;
    },
    changeRejectedVideoIdentityActivePage(id) {
      this.SelectedRejectedPageId = id;
    },
    GetActiveClassRejected(id) {
      if (id === this.SelectedRejectedPageId) {
        return "Active";
      } else {
        return "";
      }
    },

    DecrementConfirmed() {
      this.SelectedConfirmedPageId--;
      this.getConfirmedVideo(
        this.SelectedConfirmedPageId,
        5,
        this.searchConfirmed
      );
    },

    IncrementConfirmed() {
      this.SelectedConfirmedPageId++;
      this.getConfirmedVideo(
        this.SelectedConfirmedPageId,
        5,
        this.searchConfirmed
      );
    },

    getConfirmedVideo(pageNumber, count, search) {
      if (pageNumber > this.totalConfirmedPages.length) {
        pageNumber = 1;
      }
      if (pageNumber < 1) {
        pageNumber = this.totalConfirmedPages.length;
      }

      this.$axios
        .post("UserSetting/GetConfirmedVideoIdentity", null, {
        
          params: {
            pageNumber: pageNumber,
            count: count,
            search: search,
          },
        })
        .then((response) => {
          this.totalConfirmedPages = [];
          const result = Math.ceil(response.data.userSettingsCount / 10);
          for (let i = 1; i <= result; i++) {
            this.totalConfirmedPages.push(i);
          }
          this.ConfirmedVideo = response.data.userSettings;
        })
        .catch((error) => {
          console.log(error);
        });
    },
    SetSingleConfirmedVideo(item) {
      this.SingleConfirmedVideo = item.videoIdentity;
      this.SelectedUserSettingId = item.userSettingId;
    },
    changeConfirmedVideoIdentityActivePage(id) {
      this.SelectedConfirmedPageId = id;
    },
    GetActiveClassConfirmed(id) {
      if (id === this.SelectedRejectedPageId) {
        return "Active";
      } else {
        return "";
      }
    },

    updateCurrent(pageNumber) {
      if (pageNumber === 0 || pageNumber > this.totalCount) {
        return true;
      } else {
        this.$nuxt.$loading.start();
        this.getNotCofirmedVideo(
          pageNumber,
          this.count,
          this.searchNotConfirmed
        );
        this.pageNumber = pageNumber;
        this.currentPageIndex = pageNumber;
      }
    },
    updateCurrentAccept(pageNumber) {
      if (pageNumber === 0 || pageNumber > this.paginateTotalAccept) {
        return true;
      } else {
        this.$nuxt.$loading.start();
        this.getAcceptedVideo(this.pageNumberAccept, this.count);
        this.pageNumberAccept = pageNumber;
        this.currentPageIndexAccept = pageNumber;
      }
    },
    updateCurrentRejected(pageNumber) {
      if (pageNumber === 0 || pageNumber > this.paginateTotalReject) {
        return true;
      } else {
        this.$nuxt.$loading.start();
        this.getRejectedVideo(this.pageNumberReject, this.count);
        this.pageNumberReject = pageNumber;
        this.currentPageIndexReject = pageNumber;
      }
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

      this.postNK.sort(function (a, b) {
        if (a[col] > b[col]) {
          return ascending ? 1 : -1;
        } else if (a[col] < b[col]) {
          return ascending ? -1 : 1;
        }
        return 0;
      });
    },
    videoData(data) {
      this.videoInfo = data;
    },
    confirmVideo(videoIdentity) {
      this.$axios
        .post("UserSetting/ChangeVideoStatusByAdmin", null, {
        
          params: {
            userSettingId: this.videoInfo.userSettingId,
            videoIdentity: videoIdentity,
          },
        })
        .then((response) => {
          if (response.status === 200) {
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    callRejectApi() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      });
      this.getRejectedVideo(this.pageNumberReject, this.count);
    },
    callAcceptApi() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      });
      this.getAcceptedVideo(this.pageNumberAccept, this.count);
    },
  },
  mounted() {
    this.getNotCofirmedVideo(
      this.pageNumber,
      this.count,
      this.searchNotConfirmed
    );
  },
  computed: {
    BaseUrl() {
      return process.env.pic;
    },
  },
};
</script>

<style scoped>
.adminPanelBox {
  background-color: #fff;
  padding: 1rem;
}

.Active {
  color: white;
  background-color: #2e88e7;
}

#staticBackdropDeleteConfirm {
  margin: 25vh auto;
  left: 0;
}

.btn-close {
  position: absolute;
  left: 1rem;
}

tr {
  padding: 1rem;
}
</style>
