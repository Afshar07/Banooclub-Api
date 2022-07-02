<template>
  <div class="row adminPanelBox">
    <div class="col-12 my-3">
      <nav>
        <div class="nav nav-tabs my-4" id="nav-tab1" role="tablist">
          <button class="nav-link active" id="nav-home-tab1" data-bs-toggle="tab" data-bs-target="#nav-home9" type="button" role="tab" aria-controls="nav-home" aria-selected="true" @click="GetUploadedCertificate(SearchUploadedCertificate)"  >پروانه کسب</button>
          <button class="nav-link" id="nav-profile-tab2" data-bs-toggle="tab" data-bs-target="#nav-profile9" type="button" role="tab" aria-controls="nav-profile" aria-selected="false" @click="GetUploadedNewsPaper(SearchUploadedNewsPaper)" >روزنامه رسمی</button>
        </div>
      </nav>
      <div class="tab-content" id="nav-tabContent3">
        <!---lawyer --->
        <div class="tab-pane fade show active" id="nav-home9" role="tabpanel" aria-labelledby="nav-home-tab">
          <nav>
            <div class="nav nav-tabs"  role="tablist">
              <button class="nav-link active"  data-bs-toggle="tab" data-bs-target="#nav-home"
                      type="button" role="tab" aria-controls="nav-home" aria-selected="true">در انتظار تایید
              </button>
              <button class="nav-link"  data-bs-toggle="tab" data-bs-target="#nav-profile" @click="GetRejectedCertificate(SearchNotConfirmedCertificate)"
                      type="button" role="tab" aria-controls="nav-profile" aria-selected="false"
              >رد
                شده
              </button>
              <button class="nav-link"  data-bs-toggle="tab" data-bs-target="#nav-contact" @click="GetConfirmedCertificate(SearchConfirmedCertificate)"
                      type="button" role="tab" aria-controls="nav-contact" aria-selected="false"
                      >تایید شده
              </button>
            </div>
          </nav>
          <div class="tab-content" id="nav-tabContent4">
            <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
              <div class="col-md-12 my-3">
                <input v-model="SearchUploadedCertificate" type="text" class="form-control" placeholder="جستجو...">
              </div>
              <div class="col-12">
                <table v-if="UploadedCertificates.length>0" class="table table-bordered table-hover table-bordered dataTable table-striped my-3">
                  <thead>
                  <tr>
                    <th scope="col">شناسه</th>
                    <th scope="col">تصویر</th>
                    <th scope="col">کاربر</th>
                    <th scope="col">وضعیت</th>
                    <th scope="col">عملیات</th>
                  </tr>
                  </thead>
                  <tbody>
                  <tr v-for="item in UploadedCertificates" :key="item.userSettingId">
                    <th scope="row">{{item.userSettingId}}</th>
                    <td>
                      <img :src="BaseUrl + item.lawyerCertificateDoc" width="20px" height="20px" alt="">
                    </td>
                    <td v-if="item.userInfo">{{ item.userInfo.name + ' ' + item.userInfo.familyName  }}</td>
                    <td>
                      <span v-if="item.lawyerCertificateStatus === 1 " class="badge bg-success text-white">تایید شده</span>
                      <span v-if="item.lawyerCertificateStatus === 3 " class="badge bg-danger text-white">رد شده</span>
                      <span v-if="item.lawyerCertificateStatus === 2 " class="badge bg-secondary text-white">در انتظار تایید</span>
                    </td>
                    <td>
                      <div class="d-flex gap-5 justify-content-evenly">
                        <div class="ShowDoc" @click="SetSingleUploadedCertificate(item)">
                          <i class="fas fa-eye "  data-bs-toggle="modal" data-bs-target="#ShowUploadedCertificate"></i>
                        </div>

                      </div>
                    </td>
                  </tr>
                  </tbody>
                </table>
                <p v-else>اطلاعاتی یافت نشد</p>
                <div class="col-12">
                  <nav aria-label="Page navigation example">
                    <ul class="pagination">
<!--                      <div v-for="i in TotalPages" :key="i" class="PageIndicator   d-flex justify-content-center align-items-center" :class="GetIndicatorClassName(i)" @click="ChangeActivePage(i)">-->
<!--                        <small>{{i}}</small>-->
<!--                      </div>-->
                      <li class="PaginationItemPrev" >
                        <a class="page-link" @click="DecrementUploadedCertificate">صفحه قبل</a>
                      </li>

                      <li v-for="item in totalUploadedCertificatePages" :key="item" >  <a class="page-link" @click="changeUploadedCertificateActivePage(item)" :class="getUploadedCertificateClass(item)" >{{ item }}</a> </li>

                      <li>
                        <a class="page-link" @click="IncrementUploadedCertificate" >صفحه بعد</a>
                      </li>
                    </ul>
                  </nav>
                </div>
                <!-- Modal -->
                <div class="modal fade" id="ShowUploadedCertificate"  data-bs-keyboard="false" tabindex="-1"
                     aria-labelledby="staticBackdropLabel" aria-hidden="true">
                  <div class="modal-dialog" style="max-width: 800px!important" >
                    <div class="modal-content">
                      <div class="modal-header ">
                        <h5 class="modal-title" >مشاهده پروانه کسب</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                      </div>

                      <div class="modal-body">
                        <div class="row">
                          <div class="col-md-2">
                            <div class="d-flex align-items-center gap-2">
                              <i class="fas fa-image"></i>
                              <small>پروانه کسب</small>
                            </div>


                          </div>
                          <div class="col-md-8">
                            <img v-if="SingleUploadedCertificate" :src=" BaseUrl +  SingleUploadedCertificate.lawyerCertificateDoc" class="img-fluid rounded" alt="">
                          </div>
                        </div>
                      </div>
                      <div class="modal-footer">
                        <button type="button" class="btn btn-sm btn-danger" @click="SetlewerCertificateStatus(3)" data-bs-toggle="modal" data-bs-target="#UploadedCertificateAction">رد کردن</button>
                        <button type="button" class="btn btn-sm btn-primary" @click="SetlewerCertificateStatus(1)" data-bs-toggle="modal" data-bs-target="#UploadedCertificateAction">تایید</button>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="modal fade" id="UploadedCertificateAction"  data-bs-keyboard="false" tabindex="-1"
                     aria-labelledby="staticBackdropLabel" aria-hidden="true">
                  <div class="modal-dialog">
                    <div class="modal-content">
                      <div class="modal-header position-relative">

                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                      </div>
                      <div class="modal-body">
                       آیا از انجام عملیات مورد نظر اطمینان دارید؟
                      </div>
                      <div class="modal-footer">
                        <button type="button" class="btn btn-sm btn-danger"  data-bs-dismiss="modal">خیر</button>
                        <button type="button" class="btn btn-sm btn-success" data-bs-dismiss="modal" @click="ChangeLawerCertificateStatus()">بله</button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">
              <div class="col-md-12 my-3">
                <input v-model="SearchNotConfirmedCertificate" type="text" class="form-control" placeholder="جستجو...">
              </div>
              <table v-if="NotConfirmedCertificates.length>0" class="table table-bordered table-hover table-bordered dataTable table-striped my-3">
                <thead>
                <tr>
                  <th scope="col">شناسه</th>
                  <th scope="col">تصویر</th>
                  <th scope="col">کاربر</th>
                  <th scope="col">وضعیت</th>
                  <th scope="col">عملیات</th>
                </tr>
                </thead>
                <tbody>
                <tr v-for="item in NotConfirmedCertificates" :key="item.userSettingId">
                  <th scope="row">{{item.userSettingId}}</th>
                  <td>
                    <img :src="BaseUrl + item.lawyerCertificateDoc" width="20px" height="20px" alt="">
                  </td>
                  <td v-if="item.userInfo">{{ item.userInfo.name + ' ' + item.userInfo.familyName  }}</td>
                  <td>
                    <span v-if="item.lawyerCertificateStatus === 1 " class="badge bg-success text-white">تایید شده</span>
                    <span v-if="item.lawyerCertificateStatus === 3 " class="badge bg-danger text-white">رد شده</span>
                    <span v-if="item.lawyerCertificateStatus === 2 " class="badge bg-secondary text-white">در انتظار تایید</span>
                  </td>
                  <td>
                    <div class="d-flex gap-5 justify-content-evenly">
                      <div class="ShowDoc" @click="SetSingleNotConfirmedCertificate(item)">
                        <i class="fas fa-eye "  data-bs-toggle="modal" data-bs-target="#ShowNotConfirmedCertificate"></i>
                      </div>

                    </div>
                  </td>
                </tr>

                </tbody>
              </table>
              <p v-else>اطلاعاتی یافت نشد</p>
              <div class="col-12">
                <nav aria-label="Page navigation example">
                  <ul class="pagination">
                    <!--                      <div v-for="i in TotalPages" :key="i" class="PageIndicator   d-flex justify-content-center align-items-center" :class="GetIndicatorClassName(i)" @click="ChangeActivePage(i)">-->
                    <!--                        <small>{{i}}</small>-->
                    <!--                      </div>-->
                    <li class="PaginationItemPrev" >
                      <a class="page-link" @click="DecrementNotConfirmedCertificateClass">صفحه قبل</a>
                    </li>

                    <li v-for="item in totalNotConfirmedCertificatePages" :key="item" >  <a class="page-link" @click="changeNotConfirmedCertificateActivePage(item)" :class="getNotConfirmedCertificateClass(item)">{{ item }}</a> </li>

                    <li>
                      <a class="page-link" @click="IncrementNotConfirmedCertificateClass" >صفحه بعد</a>
                    </li>
                  </ul>
                </nav>
              </div>
              <div class="modal fade" id="ShowNotConfirmedCertificate"  data-bs-keyboard="false" tabindex="-1"
                   aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div class="modal-dialog" style="max-width: 800px!important" >
                  <div class="modal-content">
                    <div class="modal-header ">
                      <h5 class="modal-title" >مشاهده پروانه کسب</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>

                    <div class="modal-body">
                      <div class="row">
                        <div class="col-md-2">
                          <div class="d-flex align-items-center gap-2">
                            <i class="fas fa-image"></i>
                            <small>پروانه کسب</small>
                          </div>


                        </div>
                        <div class="col-md-8">
                          <img v-if="SingleNotConfirmedCertificate" :src=" BaseUrl +  SingleNotConfirmedCertificate.lawyerCertificateDoc" class="img-fluid rounded" alt="">
                        </div>
                      </div>
                    </div>
                    <div class="modal-footer">
                      <button type="button" class="btn btn-sm btn-secondary"  data-bs-dismiss="modal">بستن</button>

                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="tab-pane fade" id="nav-contact" role="tabpanel" aria-labelledby="nav-contact-tab">
              <div class="col-md-12 my-3">
                <input v-model="SearchConfirmedCertificate" type="text" class="form-control" placeholder="جستجو...">
              </div>
              <div class="col-12">
                <table v-if="ConfirmedCertificates.length>0" class="table table-bordered table-hover table-bordered dataTable table-striped my-3">
                  <thead>
                  <tr>
                    <th scope="col">شناسه</th>
                    <th scope="col">تصویر</th>
                    <th scope="col">کاربر</th>
                    <th scope="col">وضعیت</th>
                    <th scope="col">عملیات</th>
                  </tr>
                  </thead>
                  <tbody>
                  <tr v-for="item in ConfirmedCertificates" :key="item.userSettingId">
                    <th scope="row">{{item.userSettingId}}</th>
                    <td>
                      <img :src="BaseUrl + item.lawyerCertificateDoc" width="20px" height="20px" alt="">
                    </td>
                    <td v-if="item.userInfo">{{ item.userInfo.name + ' ' + item.userInfo.familyName  }}</td>
                    <td>
                      <span v-if="item.lawyerCertificateStatus === 1 " class="badge bg-success text-white">تایید شده</span>
                      <span v-if="item.lawyerCertificateStatus === 3 " class="badge bg-danger text-white">رد شده</span>
                      <span v-if="item.lawyerCertificateStatus === 2 " class="badge bg-secondary text-white">در انتظار تایید</span>
                    </td>
                    <td>
                      <div class="d-flex gap-5 justify-content-evenly">
                        <div class="ShowDoc" @click="SetSingleConfirmedCertificate(item)">
                          <i class="fas fa-eye "  data-bs-toggle="modal" data-bs-target="#ShowConfirmedCertificate"></i>
                        </div>

                      </div>
                    </td>
                  </tr>

                  </tbody>
                </table>
                <p v-else>اطلاعاتی یافت نشد</p>
              </div>
              <div class="col-12">
                <nav aria-label="Page navigation example">
                  <ul class="pagination">
                    <!--                      <div v-for="i in TotalPages" :key="i" class="PageIndicator   d-flex justify-content-center align-items-center" :class="GetIndicatorClassName(i)" @click="ChangeActivePage(i)">-->
                    <!--                        <small>{{i}}</small>-->
                    <!--                      </div>-->
                    <li class="PaginationItemPrev" >
                      <a class="page-link" @click="DecrementConfirmedCertificateClass">صفحه قبل</a>
                    </li>

                    <li v-for="item in totalConfirmedCertificatePages" :key="item" >  <a class="page-link" @click="changeConfirmedCertificateActivePage(item)" :class="getConfirmedCertificateClass(item)">{{ item }}</a> </li>

                    <li>
                      <a class="page-link" @click="IncrementConfirmedCertificateClass" >صفحه بعد</a>
                    </li>
                  </ul>
                </nav>
              </div>
              <div class="modal fade" id="ShowConfirmedCertificate" data-bs-keyboard="false" tabindex="-1"
                   aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div class="modal-dialog" style="max-width: 800px!important" >
                  <div class="modal-content">
                    <div class="modal-header ">
                      <h5 class="modal-title" >مشاهده پروانه کسب</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>

                    <div class="modal-body">
                      <div class="row">
                        <div class="col-md-2">
                          <div class="d-flex align-items-center gap-2">
                            <i class="fas fa-image"></i>
                            <small>پروانه کسب</small>
                          </div>


                        </div>
                        <div class="col-md-8">
                          <img v-if="SingleConfirmedCertificate" :src=" BaseUrl +  SingleConfirmedCertificate.lawyerCertificateDoc" class="img-fluid rounded" alt="">
                        </div>
                      </div>
                    </div>
                    <div class="modal-footer">
                      <button type="button" class="btn btn-sm btn-secondary"  data-bs-dismiss="modal" >بستن</button>

                    </div>
                  </div>
                </div>
              </div>

            </div>
          </div>

        </div>
        <!---NewsPaper --->
        <div class="tab-pane fade" id="nav-profile9" role="tabpanel" aria-labelledby="nav-profile-tab">
          <nav>
            <div class="nav nav-tabs" id="nav-tab" role="tablist">
              <button class="nav-link active" id="nav-home-tab" data-bs-toggle="tab" data-bs-target="#nav-home10"
                      type="button" role="tab" aria-controls="nav-home" aria-selected="true">در انتظار تایید
              </button>
              <button class="nav-link" id="nav-profile-tab" data-bs-toggle="tab" data-bs-target="#nav-profile11" @click="GetRejectedNewsPaper(SearchNotConfirmedNewsPaper)"
                      type="button" role="tab" aria-controls="nav-profile" aria-selected="false"
              >رد
                شده
              </button>
              <button class="nav-link" id="nav-contact-tab" data-bs-toggle="tab" data-bs-target="#nav-contact12" @click="GetConfirmedNewsPaper(SearchConfirmedNewsPaper)"
                      type="button" role="tab" aria-controls="nav-contact" aria-selected="false"
                     >تایید شده
              </button>
            </div>
          </nav>
          <div class="tab-content" id="nav-tabContent6">
            <div class="tab-pane fade show active" id="nav-home10" role="tabpanel" aria-labelledby="nav-home-tab">
              <div class="col-md-12 my-3">
                <input v-model="SearchUploadedNewsPaper" type="text" class="form-control" placeholder="جستجو...">
              </div>
              <div class="col-12">
                <table v-if="UploadedNewsPaper.length>0" class="table table-bordered table-hover table-bordered dataTable table-striped my-3">
                  <thead>
                  <tr>
                    <th scope="col">شناسه</th>
                    <th scope="col">تصویر</th>
                    <th scope="col">کاربر</th>
                    <th scope="col">وضعیت</th>
                    <th scope="col">عملیات</th>
                  </tr>
                  </thead>
                  <tbody>
                  <tr v-for="item in UploadedNewsPaper" :key="item.userSettingId">
                    <th scope="row">{{item.userSettingId}}</th>
                    <td>
                      <img :src="BaseUrl + item.newspaperDoc" width="20px" height="20px" alt="">
                    </td>
                    <td v-if="item.userInfo">{{ item.userInfo.name + ' ' + item.userInfo.familyName  }}</td>
                    <td>
                      <span v-if="item.newspaperStatus === 1 " class="badge bg-success text-white">تایید شده</span>
                      <span v-if="item.newspaperStatus === 3 " class="badge bg-danger text-white">رد شده</span>
                      <span v-if="item.newspaperStatus === 2 " class="badge bg-secondary text-white">در انتظار تایید</span>
                    </td>
                    <td>
                      <div class="d-flex gap-5 justify-content-evenly">
                        <div class="ShowDoc" @click="SetSingleUploadedNewsPaper(item)">
                          <i class="fas fa-eye "  data-bs-toggle="modal" data-bs-target="#ShowUploadedNewsPaper"></i>
                        </div>

                      </div>
                    </td>
                  </tr>

                  </tbody>
                </table>
                <p v-else>اطلاعاتی یافت نشد</p>

                <div class="col-12">
                  <nav aria-label="Page navigation example">
                    <ul class="pagination">
                      <!--                      <div v-for="i in TotalPages" :key="i" class="PageIndicator   d-flex justify-content-center align-items-center" :class="GetIndicatorClassName(i)" @click="ChangeActivePage(i)">-->
                      <!--                        <small>{{i}}</small>-->
                      <!--                      </div>-->
                      <li class="PaginationItemPrev" >
                        <a class="page-link" @click="DecrementPageUploadedNewsPaper">صفحه قبل</a>
                      </li>

                      <li v-for="item in totalUploadedNewsPaperPages" :key="item" >  <a class="page-link" @click="changeUploadedNewsPaperActivePage(item)" :class="getUploadedNewsPaperClass(item)">{{ item }}</a> </li>

                      <li>
                        <a class="page-link" @click="IncrementPageUploadedNewsPaper" >صفحه بعد</a>
                      </li>
                    </ul>
                  </nav>
                </div>
                <!-- Modal -->
                <div class="modal fade" id="ShowUploadedNewsPaper"  data-bs-keyboard="false" tabindex="-1"
                     aria-labelledby="staticBackdropLabel" aria-hidden="true">
                  <div class="modal-dialog" style="max-width: 800px!important" >
                    <div class="modal-content">
                      <div class="modal-header ">
                        <h5 class="modal-title" >مشاهده روزنامه رسمی</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                      </div>

                      <div class="modal-body">
                        <div class="row">
                          <div class="col-md-2">
                            <div class="d-flex align-items-center gap-2">
                              <i class="fas fa-image"></i>
                              <small>روزنامه رسمی</small>
                            </div>


                          </div>
                          <div class="col-md-8">
                            <img v-if="SingleUploadedNewsPaper" :src=" BaseUrl +  SingleUploadedNewsPaper.newspaperDoc" class="img-fluid rounded" alt="">
                          </div>
                        </div>
                      </div>
                      <div class="modal-footer">
                        <button type="button" class="btn btn-sm btn-danger" @click="SetNewsPaperStatus(3)" data-bs-toggle="modal" data-bs-target="#UploadedNEwsPaperAction">رد کردن</button>
                        <button type="button" class="btn btn-sm btn-primary" @click="SetNewsPaperStatus(1)" data-bs-toggle="modal" data-bs-target="#UploadedNEwsPaperAction">تایید</button>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="modal fade" id="UploadedNEwsPaperAction"  data-bs-keyboard="false" tabindex="-1"
                     aria-labelledby="staticBackdropLabel" aria-hidden="true">
                  <div class="modal-dialog">
                    <div class="modal-content">
                      <div class="modal-header position-relative">

                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                      </div>
                      <div class="modal-body">
                        آیا از انجام عملیات مورد نظر اطمینان دارید؟
                      </div>
                      <div class="modal-footer">
                        <button type="button" class="btn btn-sm btn-danger"  data-bs-dismiss="modal">خیر</button>
                        <button type="button" class="btn btn-sm btn-success" data-bs-dismiss="modal" @click="ChangeNewsPaperStatus()">بله</button>
                      </div>
                    </div>
                  </div>
                </div>

              </div>
            </div>
            <div class="tab-pane fade" id="nav-profile11" role="tabpanel" aria-labelledby="nav-profile-tab">
              <div class="col-md-12 my-3">
                <input v-model="SearchNotConfirmedNewsPaper" type="text" class="form-control" placeholder="جستجو...">
              </div>
              <div class="col-12">
                <div class="col-12">
                  <table v-if="NotConfirmedNewsPaper.length>0" class="table table-bordered table-hover table-bordered dataTable table-striped my-3">
                    <thead>
                    <tr>
                      <th scope="col">شناسه</th>
                      <th scope="col">تصویر</th>
                      <th scope="col">کاربر</th>
                      <th scope="col">وضعیت</th>
                      <th scope="col">عملیات</th>
                    </tr>
                    </thead>
                    <tbody>
                    <tr v-for="item in NotConfirmedNewsPaper" :key="item.userSettingId">
                      <th scope="row">{{item.userSettingId}}</th>
                      <td>
                        <img :src="BaseUrl + item.newspaperDoc" width="20px" height="20px" alt="">
                      </td>
                      <td v-if="item.userInfo">{{ item.userInfo.name + ' ' + item.userInfo.familyName  }}</td>
                      <td>
                        <span v-if="item.newspaperStatus === 1 " class="badge bg-success text-white">تایید شده</span>
                        <span v-if="item.newspaperStatus === 3 " class="badge bg-danger text-white">رد شده</span>
                        <span v-if="item.newspaperStatus === 2 " class="badge bg-secondary text-white">در انتظار تایید</span>
                      </td>
                      <td>
                        <div class="d-flex gap-5 justify-content-evenly">
                          <div class="ShowDoc" @click="SetSingleNotConfirmedNewsPaper(item)">
                            <i class="fas fa-eye "  data-bs-toggle="modal" data-bs-target="#ShowNotConfirmedNewsPaper"></i>
                          </div>

                        </div>
                      </td>
                    </tr>

                    </tbody>
                  </table>
                  <p v-else>اطلاعاتی یافت نشد</p>
                </div>

                <div class="col-12">
                  <nav aria-label="Page navigation example">
                    <ul class="pagination">
                      <!--                      <div v-for="i in TotalPages" :key="i" class="PageIndicator   d-flex justify-content-center align-items-center" :class="GetIndicatorClassName(i)" @click="ChangeActivePage(i)">-->
                      <!--                        <small>{{i}}</small>-->
                      <!--                      </div>-->
                      <li class="PaginationItemPrev" >
                        <a class="page-link" @click="DecrementPageNotConfirmedNewsPaper">صفحه قبل</a>
                      </li>

                      <li v-for="item in totalNotConfirmedNewsPaperPages" :key="item" >  <a class="page-link" @click="changeNotConfirmedNewsPaperActivePage(item)" :class="getNotConfirmedNewsPaperClass(item)">{{ item }}</a> </li>

                      <li>
                        <a class="page-link" @click="IncrementPageNotConfirmedNewsPaper" >صفحه بعد</a>
                      </li>
                    </ul>
                  </nav>
                </div>
                <div class="modal fade" id="ShowNotConfirmedNewsPaper"  data-bs-keyboard="false" tabindex="-1"
                     aria-labelledby="staticBackdropLabel" aria-hidden="true">
                  <div class="modal-dialog" style="max-width: 800px!important" >
                    <div class="modal-content">
                      <div class="modal-header ">
                        <h5 class="modal-title" >مشاهده روزنامه رسمی</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                      </div>

                      <div class="modal-body">
                        <div class="row">
                          <div class="col-md-2">
                            <div class="d-flex align-items-center gap-2">
                              <i class="fas fa-image"></i>
                              <small>روزنامه رسمی</small>
                            </div>


                          </div>
                          <div class="col-md-8">
                            <img v-if="SingleNotConfirmedNewsPaper" :src=" BaseUrl +  SingleNotConfirmedNewsPaper.newspaperDoc" class="img-fluid rounded" alt="">
                          </div>
                        </div>
                      </div>
                      <div class="modal-footer">
                        <button type="button" class="btn btn-sm btn-secondary" data-bs-dismiss="modal">بستن</button>

                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="tab-pane fade" id="nav-contact12" role="tabpanel" aria-labelledby="nav-contact-tab">
              <div class="col-12">
                <div class="col-md-12 my-3">
                  <input v-model="SearchConfirmedNewsPaper" type="text" class="form-control" placeholder="جستجو...">
                </div>
                <table v-if="ConfirmedNewsPaper.length>0" class="table table-bordered table-hover table-bordered dataTable table-striped my-3">
                  <thead>
                  <tr>
                    <th scope="col">شناسه</th>
                    <th scope="col">تصویر</th>
                    <th scope="col">کاربر</th>
                    <th scope="col">وضعیت</th>
                    <th scope="col">عملیات</th>
                  </tr>
                  </thead>
                  <tbody>
                  <tr v-for="item in ConfirmedNewsPaper" :key="item.userSettingId">
                    <th scope="row">{{item.userSettingId}}</th>
                    <td>
                      <img :src="BaseUrl + item.newspaperDoc" width="20px" height="20px" alt="">
                    </td>
                    <td v-if="item.userInfo">{{ item.userInfo.name + ' ' + item.userInfo.familyName  }}</td>
                    <td>
                      <span v-if="item.newspaperStatus === 1 " class="badge bg-success text-white">تایید شده</span>
                      <span v-if="item.newspaperStatus === 3 " class="badge bg-danger text-white">رد شده</span>
                      <span v-if="item.newspaperStatus === 2 " class="badge bg-secondary text-white">در انتظار تایید</span>
                    </td>
                    <td>
                      <div class="d-flex gap-5 justify-content-evenly">
                        <div class="ShowDoc" @click="SetSingleConfirmedNewsPaper(item)">
                          <i class="fas fa-eye "  data-bs-toggle="modal" data-bs-target="#ShowConfirmedNewsPaper"></i>
                        </div>

                      </div>
                    </td>
                  </tr>

                  </tbody>
                </table>
                <p v-else>اطلاعاتی یافت نشد</p>
              </div>
              <div class="modal fade" id="ShowConfirmedNewsPaper"  data-bs-keyboard="false" tabindex="-1"
                   aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div class="modal-dialog" style="max-width: 800px!important" >
                  <div class="modal-content">
                    <div class="modal-header ">
                      <h5 class="modal-title" >مشاهده روزنامه رسمی</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>

                    <div class="modal-body">
                      <div class="row">
                        <div class="col-md-2">
                          <div class="d-flex align-items-center gap-2">
                            <i class="fas fa-image"></i>
                            <small>روزنامه رسمی</small>
                          </div>


                        </div>
                        <div class="col-md-8">
                          <img v-if="SingleConfirmedNewsPaper" :src=" BaseUrl +  SingleConfirmedNewsPaper.newspaperDoc" class="img-fluid rounded" alt="">
                        </div>
                      </div>
                    </div>
                    <div class="modal-footer">
                      <button type="button" class="btn btn-sm btn-secondary" data-bs-dismiss="modal">بستن</button>

                    </div>
                  </div>
                </div>
              </div>
              <div class="col-12">
                <div class="col-12">
                  <nav aria-label="Page navigation example">
                    <ul class="pagination">
                      <!--                      <div v-for="i in TotalPages" :key="i" class="PageIndicator   d-flex justify-content-center align-items-center" :class="GetIndicatorClassName(i)" @click="ChangeActivePage(i)">-->
                      <!--                        <small>{{i}}</small>-->
                      <!--                      </div>-->
                      <li class="PaginationItemPrev" >
                        <a class="page-link" @click="DecrementPageConfirmedNewsPaper">صفحه قبل</a>
                      </li>

                      <li v-for="item in totalConfirmedNewsPaperPages" :key="item" >  <a class="page-link" @click="changeConfirmedNewsPaperActivePage(item)" :class="getConfirmedNewsPaperClass(item)">{{ item }}</a> </li>

                      <li>
                        <a class="page-link" @click="IncrementPageConfirmedNewsPaper" >صفحه بعد</a>
                      </li>
                    </ul>
                  </nav>
                </div>
              </div>
            </div>
          </div>
        </div>

      </div>

    </div>
  </div>
</template>

<script>
export default {
  name: "FormalInfo",
  layout: 'SuperAdminPanel',
  data() {
    return {
      ascending: false,
      sortColumn: '',
      toggleArrow: false,


      UploadedCertificates: [],
      SelectedPageUploadedCertificate: 1,
      SearchUploadedCertificate: null,
      totalUploadedCertificatePages: [],
      SingleUploadedCertificate: null,
      LawerStatus: 0,

      NotConfirmedCertificates: [],
      SearchNotConfirmedCertificate: null,
      SingleNotConfirmedCertificate: null,
      SelectedPageNotConfirmedCertificate: 1,
      totalNotConfirmedCertificatePages: [],

      ConfirmedCertificates: [],
      SelectedPageConfirmedCertificate: 1,
      SearchConfirmedCertificate: null,
      SingleConfirmedCertificate: null,
      totalConfirmedCertificatePages: [],

      UploadedNewsPaper: [],
      SelectedPageUploadedNewsPaper: 1,
      SearchUploadedNewsPaper: null,
      totalUploadedNewsPaperPages: [],
      SingleUploadedNewsPaper: null,
      NewsPaperStatus: 0,

      NotConfirmedNewsPaper: [],
      SearchNotConfirmedNewsPaper: null,
      SingleNotConfirmedNewsPaper: null,
      SelectedPageNotConfirmedNewsPaper: 1,
      totalNotConfirmedNewsPaperPages: [],

      ConfirmedNewsPaper: [],
      SearchConfirmedNewsPaper: null,
      SingleConfirmedNewsPaper: null,
      SelectedPageConfirmedNewsPaper: 1,
      totalConfirmedNewsPaperPages: [],

    }
  },
  mounted() {
    // this.GetUploadedCertificate(this.SelectedPageUploadedCertificate,this.SearchUploadedCertificate)
this.GetUploadedCertificate(this.SearchUploadedCertificate)

  },
  watch:{
    SearchUploadedCertificate:function (val,oldVal){
      this.GetUploadedCertificate(val)
    },
    SearchNotConfirmedCertificate:function (val,oldVal){
      this.GetRejectedCertificate(val)
    },
    SearchConfirmedCertificate:function (val,oldVal){
      this.GetConfirmedCertificate(val)
    },
    SearchUploadedNewsPaper:function (val,oldVal){
      this.GetUploadedNewsPaper(val)
    },
    SearchNotConfirmedNewsPaper:function (val,oldVal){
      this.GetRejectedNewsPaper(val)
    },
    SearchConfirmedNewsPaper:function (val,oldVal){
      this.GetConfirmedNewsPaper(val)
    }
  },
  computed: {
    BaseUrl() {
      return process.env.pic
    },
  },
    methods: {
      //Uploaded Certificate
      GetUploadedCertificate(val) {
        this.$nextTick(()=>{
          this.$nuxt.$loading.start();
        })
        if (this.SelectedPageUploadedCertificate > this.totalUploadedCertificatePages.length) {
          this.SelectedPageUploadedCertificate = 1
        }
        if (this.SelectedPageUploadedCertificate < 1) {
          this.SelectedPageUploadedCertificate = this.totalUploadedCertificatePages.length

        }
        this.totalUploadedCertificatePages = []

        this.$axios.post(`UserSetting/GetUploadedLawyerCertificates?pageNumber=${this.SelectedPageUploadedCertificate}&count=10`, null, {
        
          params: {
            search: val
          }
        }).then((res) => {
          this.$nuxt.$loading.finish();
          this.$nuxt.loading =false;
          const result = Math.ceil(res.data.userSettingsCount / 10)
          for (let i = 1; i <= result; i++) {
            this.totalUploadedCertificatePages.push(i);
          }

            this.UploadedCertificates =  res.data.userSettings

        })

      },
      IncrementUploadedCertificate(){
        this.SelectedPageUploadedCertificate++
        this.GetUploadedCertificate(this.SearchUploadedCertificate)

      },
      DecrementUploadedCertificate(){
        this.SelectedPageUploadedCertificate--
        this.GetUploadedCertificate(this.SearchUploadedCertificate)

      },
      SetlewerCertificateStatus(status) {
        this.LawerStatus = status

      },
      ChangeLawerCertificateStatus() {
        this.$axios.post(`UserSetting/ChangeLawyerCertificateStatusByAdmin?userSettingId=${this.SingleUploadedCertificate.userSettingId}&status=${this.LawerStatus}`, null, {
        
        }).then((res) => {
          this.$toast.success('وضعیت اطلاعات کاربر تغییر یافت')
          this.GetUploadedCertificate(this.SearchUploadedCertificate);
          this.GetRejectedCertificate(this.SearchNotConfirmedCertificate)
        })
      },
      getUploadedCertificateClass(id) {
        if (id === this.SelectedPageUploadedCertificate) {
          return 'ActivePage'
        } else {
          return ''
        }
      },
      SetSingleUploadedCertificate(item) {
        this.SingleUploadedCertificate = item
      },
      changeUploadedCertificateActivePage(Id) {

        this.SelectedPageUploadedCertificate = Id


      },

      //Rejected Certificate
      getNotConfirmedCertificateClass(id){
        if (id === this.SelectedPageNotConfirmedCertificate) {
          return 'ActivePage'
        } else {
          return ''
        }
      },
      IncrementNotConfirmedCertificateClass(){
        this.SelectedPageNotConfirmedCertificate++
        this.GetRejectedCertificate(this.SearchNotConfirmedCertificate)
      },
      DecrementNotConfirmedCertificateClass(){
        this.SelectedPageNotConfirmedCertificate--
        this.GetRejectedCertificate(this.SearchNotConfirmedCertificate)
      },
      GetRejectedCertificate(val) {
        if (this.SelectedPageNotConfirmedCertificate > this.totalNotConfirmedCertificatePages.length) {
          this.SelectedPageNotConfirmedCertificate = 1
        }
        if (this.SelectedPageNotConfirmedCertificate < 1) {
          this.SelectedPageNotConfirmedCertificate = this.totalNotConfirmedCertificatePages.length

        }
        this.totalNotConfirmedCertificatePages = []

        this.$axios.post(`UserSetting/GetRejectedLawyerCertificates?pageNumber=${this.SelectedPageNotConfirmedCertificate}&count=10`, null, {
         
          params: {
            search: val
          }
        }).then((res) => {
          const result = Math.ceil(res.data.userSettingsCount / 10)
          for (let i = 1; i <= result; i++) {
            this.totalNotConfirmedCertificatePages.push(i);
          }

         this.NotConfirmedCertificates =  res.data.userSettings


        })


      },
      changeNotConfirmedCertificateActivePage(Id) {

        this.SelectedPageNotConfirmedCertificate = Id


      },
      SetSingleNotConfirmedCertificate(item) {
        this.SingleNotConfirmedCertificate = item

      },
      //Confirmed Certificate
      getConfirmedCertificateClass(id){
        if (id === this.SelectedPageConfirmedCertificate) {
          return 'ActivePage'
        } else {
          return ''
        }
      },
      IncrementConfirmedCertificateClass(){
        this.SelectedPageConfirmedCertificate++
        this.GetConfirmedCertificate(this.SearchConfirmedCertificate)
      },
      DecrementConfirmedCertificateClass(){
        this.SelectedPageConfirmedCertificate--
        this.GetConfirmedCertificate(this.SearchConfirmedCertificate)
      },
      GetConfirmedCertificate(val) {
        if (this.SelectedPageConfirmedCertificate > this.totalConfirmedCertificatePages.length) {
          this.SelectedPageConfirmedCertificate = 1
        }
        if (this.SelectedPageConfirmedCertificate < 1) {
          this.SelectedPageConfirmedCertificate = this.totalConfirmedCertificatePages.length

        }
        this.totalConfirmedCertificatePages = []

        this.$axios.post(`UserSetting/GetConfirmedLawyerCertificates?pageNumber=${this.SelectedPageConfirmedCertificate}&count=10`, null, {
         
          params: {
            search: val
          }
        }).then((res) => {
          const result = Math.ceil(res.data.userSettingsCount / 10)
          for (let i = 1; i <= result; i++) {
            this.totalConfirmedCertificatePages.push(i);
          }
        this.ConfirmedCertificates =  res.data.userSettings


        })


      },
      changeConfirmedCertificateActivePage(id) {
        this.SelectedPageConfirmedCertificate = id
      },
      SetSingleConfirmedCertificate(item) {
        this.SingleConfirmedCertificate = item
      },
        //Uploaded NewsPaper
      GetUploadedNewsPaper(val) {
        this.$nextTick(()=>{
          this.$nuxt.$loading.start();
        })


        if (this.SelectedPageUploadedNewsPaper > this.totalUploadedNewsPaperPages.length) {
          this.SelectedPageUploadedNewsPaper = 1
        }
        if (this.SelectedPageUploadedNewsPaper < 1) {
          this.SelectedPageUploadedNewsPaper = this.totalUploadedNewsPaperPages.length

        }
        this.totalUploadedNewsPaperPages = []

        this.$axios.post(`UserSetting/GetUploadedNewspaperDocs?pageNumber=${this.SelectedPageUploadedNewsPaper}&count=10`, null, {
         
          params: {
            search: val
          }
        }).then((res) => {
          this.$nuxt.$loading.finish();
          this.$nuxt.loading =false;
          const result = Math.ceil(res.data.userSettingsCount / 10)
          for (let i = 1; i <= result; i++) {
            this.totalUploadedNewsPaperPages.push(i);
          }
       this.UploadedNewsPaper =    res.data.userSettings


        })


      },
      IncrementPageUploadedNewsPaper(){
        this.SelectedPageUploadedNewsPaper++
        this.GetUploadedNewsPaper(this.SearchUploadedNewsPaper)
      },
      DecrementPageUploadedNewsPaper(){
        this.SelectedPageUploadedNewsPaper--
        this.GetUploadedNewsPaper(this.SearchUploadedNewsPaper)
      },
      getUploadedNewsPaperClass(id){
        if (id === this.SelectedPageUploadedNewsPaper) {
          return 'ActivePage'
        } else {
          return ''
        }
      },
      changeUploadedNewsPaperActivePage(id) {
        this.SelectedPageUploadedNewsPaper = id

      },
      SetSingleUploadedNewsPaper(item) {
        this.SingleUploadedNewsPaper = item
      },
      SetNewsPaperStatus(status) {
        this.NewsPaperStatus = status
      },
      ChangeNewsPaperStatus() {
        this.$axios.post(`UserSetting/ChangeNewspaperDocStatusByAdmin?userSettingId=${this.SingleUploadedNewsPaper.userSettingId}&status=${this.NewsPaperStatus}`, null,{
         
        }).then((res) => {
          this.$toast.success('وضعیت اطلاعات کاربر تغییر یافت')
          this.GetUploadedNewsPaper(this.SearchUploadedNewsPaper)
          this.GetRejectedNewsPaper(this.SearchNotConfirmedNewsPaper)
          this.GetConfirmedNewsPaper(this.SearchConfirmedNewsPaper)
        })
      },
      //Rejected NewsPaper
      GetRejectedNewsPaper(val) {
        if (this.SelectedPageNotConfirmedNewsPaper > this.totalNotConfirmedNewsPaperPages.length) {
          this.SelectedPageNotConfirmedNewsPaper = 1
        }
        if (this.SelectedPageNotConfirmedNewsPaper < 1) {
          this.SelectedPageNotConfirmedNewsPaper = this.totalNotConfirmedNewsPaperPages.length

        }
        this.totalNotConfirmedNewsPaperPages = []

        this.$axios.post(`UserSetting/GetRejectedNewspaperDocs?pageNumber=${this.SelectedPageNotConfirmedNewsPaper}&count=10`, null, {
         
          params: {
            search: val
          }
        }).then((res) => {
          const result = Math.ceil(res.data.userSettingsCount / 10)
          for (let i = 1; i <= result; i++) {
            this.totalNotConfirmedNewsPaperPages.push(i);
          }
         this.NotConfirmedNewsPaper =  res.data.userSettings

        })



      },
      IncrementPageNotConfirmedNewsPaper(){
        this.SelectedPageNotConfirmedNewsPaper++
        this.GetRejectedNewsPaper(this.SearchNotConfirmedNewsPaper)
      },
      DecrementPageNotConfirmedNewsPaper(){
        this.SelectedPageNotConfirmedNewsPaper--
        this.GetRejectedNewsPaper(this.SearchNotConfirmedNewsPaper)
      },
      getNotConfirmedNewsPaperClass(id){
        if (id === this.SelectedPageNotConfirmedNewsPaper) {
          return 'ActivePage'
        } else {
          return ''
        }
      },
      SetSingleNotConfirmedNewsPaper(item){
        this.SingleNotConfirmedNewsPaper = item
      },
      changeNotConfirmedNewsPaperActivePage(id){
        this.SelectedPageNotConfirmedNewsPaper = id
      },
      //Confirmed NewsPaper

      GetConfirmedNewsPaper(val) {
        if (this.SelectedPageConfirmedNewsPaper > this.totalConfirmedNewsPaperPages.length) {
          this.SelectedPageConfirmedNewsPaper = 1
        }
        if (this.SelectedPageConfirmedNewsPaper < 1) {
          this.SelectedPageConfirmedNewsPaper = this.totalConfirmedNewsPaperPages.length

        }
        this.totalConfirmedNewsPaperPages = []

        this.$axios.post(`UserSetting/GetConfirmedNewspaperDocs?pageNumber=${this.SelectedPageConfirmedNewsPaper}&count=10`, null, {
         
          params: {
            search: val
          }
        }).then((res) => {
          const result = Math.ceil(res.data.userSettingsCount / 10)
          for (let i = 1; i <= result; i++) {
            this.totalConfirmedNewsPaperPages.push(i);
          }
        this.ConfirmedNewsPaper =   res.data.userSettings


        })



      },
      IncrementPageConfirmedNewsPaper(){
        this.SelectedPageConfirmedNewsPaper++
        this.GetConfirmedNewsPaper(this.SearchConfirmedNewsPaper)
      },
      DecrementPageConfirmedNewsPaper(){
        this.SelectedPageConfirmedNewsPaper--
        this.GetConfirmedNewsPaper(this.SearchConfirmedNewsPaper)
      },
      getConfirmedNewsPaperClass(id){
        if (id === this.SelectedPageConfirmedNewsPaper) {
          return 'ActivePage'
        } else {
          return ''
        }
      },

      SetSingleConfirmedNewsPaper(item){
        this.SingleConfirmedNewsPaper = item
      },
      changeConfirmedNewsPaperActivePage(id){
        this.SelectedPageConfirmedNewsPaper = id
      },



    },



}

</script>

<style scoped>

.ActivePage{
  background-color: #2e88e7;
  color:white;

}
.page-link{
  cursor: pointer!important;
}

.ShowDoc{
  cursor: pointer;
  transition: .2s ease;

}
.ShowDoc:hover{
    color: #2e88e7;
  transition: .2s ease;
}

</style>
