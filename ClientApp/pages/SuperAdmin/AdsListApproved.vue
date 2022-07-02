<template>
  <div class="row adminPanelBox">
    <div
      class="modal fade"
      id="staticBackdrop4"
      tabindex="-1"
      aria-labelledby="staticBackdrop2Label"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-lg">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="staticBackdrop4Label">مشاهده</h5>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
              ref="closeApprovedModal"
            ></button>
          </div>
          <div class="modal-body">
            <div class="row">
              <div class="col-md-6 col-12">
                <div class="d-flex flex-column">
                  <div>
                    <h1>
                      {{ approvedAds.title }}
                    </h1>
                  </div>
                  <div>
                    {{ approvedAds.createDate | moment("jYYYY/jM/jD HH:mm") }}
                  </div>
                  <div class="d-flex gap-3 my-3 border-bottom pb-2">
                    <div>تاریخ انقضاء:</div>
                    <div></div>
                  </div>
                  <div class="d-flex gap-3 border-bottom pb-2">
                    <div>قیمت:</div>
                    <div>
                      {{ approvedAds.price }}
                    </div>
                  </div>
                  <div class="d-flex flex-column gap-3 my-3 border-bottom pb-2">
                    <div>
                      <h3>توضیحات</h3>
                    </div>
                    <div>
                      {{ approvedAds.description }}
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-md-6 col-12">
                <div class="thumb-example">
                  <client-only>
                    <swiper
                      class="swiper gallery-top"
                      :options="swiperOptionTop"
                      ref="swiperTop"
                    >
                      <swiper-slide
                        class="slide-1"
                        v-for="item in approvedAds.photos"
                        :key="item.adsId"
                      >
                        <img
                          class="img-fluid mainImage"
                          v-if="
                            approvedAds.photos !== undefined &&
                            approvedAds.photos.length > 0
                          "
                          :src="`https://BanooClubapi.simagar.com/${item.base64}`"
                        />
                      </swiper-slide>
                      <div
                        class="swiper-button-next swiper-button-white"
                        slot="button-next"
                      ></div>
                      <div
                        class="swiper-button-prev swiper-button-white"
                        slot="button-prev"
                      ></div>
                    </swiper>
                  </client-only>
                </div>
              </div>
            </div>
            <div
              class="d-flex align-items-center justify-content-around text-nowrap"
            >
              <label for="status">تغییر وضعیت آگهی :</label>
              <select
                name="status"
                class="form-select form-select-sm w-50"
                id="status"
                v-model="tempAdStatus"
              >
                <option value="1" selected>تایید شده</option>
                <option value="2">تایید نشده</option>
                <option value="3">رد شده</option>
              </select>

              <button
                type="button"
                class="btn btn-primary"
                @click="submitAdStatus"
              >
                ثبت
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="col-12">لیست آگهی</div>

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
            @click="callRejectApi"
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
            @click="callAcceptApi"
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
          <div class="col-md-12 my-3">
            <input
              v-model="searchKey"
              type="text"
              class="form-control"
              placeholder="جست و جو "
            />
          </div>
          <div class="p-3 my-3">
            <table
              class="table table-bordered table-hover table-bordered dataTable table-striped my-3"
            >
              <thead>
                <tr>
                  <th @click="sortTable('photos')">
                    <div class="d-flex">
                      <div>تصاویر آگهی</div>
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
                  <th @click="sortTable('userInfo')">
                    <div class="d-flex">
                      <div>کاربر</div>
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
                  <th @click="sortTable('title')">
                    <div class="d-flex">
                      <div>عنوان آگهی</div>
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
                  <th @click="sortTable('description')">
                    <div class="d-flex">
                      <div>توضیحات آگهی</div>
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
                  <th @click="sortTable('status')">
                    <div class="d-flex">
                      <div>وضعیت آگهی</div>
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
                      <div class="">تایید</div>
                    </div>
                  </th>
                </tr>
              </thead>
              <tbody>
                <tr
                  v-for="row in allAdsListApproved.filter(
                    (m) => m.status === 2
                  )"
                  :key="row.adsId"
                >
                  <td>
                    <img
                      class="mx-1"
                      v-for="(item, index) in row.photos"
                      :key="index"
                      style="width: 20px; height: 20px"
                      :src="`https://BanooClubapi.simagar.com/${item.base64}`"
                    />
                  </td>
                  <td>
                    {{ row.userInfo.name + " " + row.userInfo.familyName }}
                  </td>
                  <td>{{ row.title }}</td>
                  <td>{{ row.description }}</td>
                  <td v-html="row.status === 2 ? 'در انتظار تایید' : ''"></td>
                  <td>
                    <div class="d-flex gap-5 justify-content-evenly">
                      <div class="" @click="dataModal(row)">
                        <button
                          type="button"
                          class="btn"
                          data-bs-toggle="modal"
                          data-bs-target="#staticBackdrop2"
                        >
                          <font-awesome-icon icon="trash" color="red" />
                        </button>
                      </div>
                      <div class="" @click="dataModal(row)">
                        <button
                          type="button"
                          class="btn"
                          data-bs-toggle="modal"
                          data-bs-target="#staticBackdrop"
                        >
                          <font-awesome-icon icon="check" color="blue" />
                        </button>
                      </div>
                    </div>
                  </td>
                </tr>
              </tbody>
            </table>
            <!-- Modal -->
            <div
              class="modal fade"
              id="staticBackdrop2"
              tabindex="-1"
              aria-labelledby="staticBackdrop2Label"
              aria-hidden="true"
            >
              <div class="modal-dialog modal-lg">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 class="modal-title" id="staticBackdrop2Label">حذف</h5>
                    <button
                      type="button"
                      class="btn-close"
                      data-bs-dismiss="modal"
                      aria-label="Close"
                    ></button>
                  </div>
                  <div class="modal-body">
                    <div class="row">
                      <div class="col-md-6 col-12">
                        <div class="d-flex flex-column">
                          <div>
                            <h1>
                              {{ approvedAds.title }}
                            </h1>
                          </div>
                          <div>
                            {{ approvedAds.description }}
                          </div>
                          <div class="d-flex gap-3 my-3 border-bottom pb-2">
                            <div>تاریخ انقضاء:</div>
                            <div>
                              {{
                                approvedAds.createDate
                                  | moment("jYYYY/jM/jD HH:mm")
                              }}
                            </div>
                          </div>
                          <div class="d-flex gap-3 border-bottom pb-2">
                            <div>قیمت:</div>
                            <div>
                              {{ approvedAds.price }}
                            </div>
                          </div>
                          <div
                            class="d-flex flex-column gap-3 my-3 border-bottom pb-2"
                          >
                            <div>
                              <h3>توضیحات</h3>
                            </div>
                            <div>
                              {{ approvedAds.description }}
                            </div>
                          </div>
                        </div>
                      </div>
                      <div class="col-md-6 col-12">
                        <div class="thumb-example">
                          <client-only>
                            <swiper
                              class="swiper gallery-top"
                              :options="swiperOptionTop"
                              ref="swiperTop"
                            >
                              <swiper-slide
                                class="slide-1"
                                v-for="item in approvedAds.photos"
                                :key="item.adsId"
                              >
                                <img
                                  class="img-fluid mainImage"
                                  v-if="
                                    approvedAds.photos !== undefined &&
                                    approvedAds.photos.length > 0
                                  "
                                  :src="`https://BanooClubapi.simagar.com/${item.base64}`"
                                />
                              </swiper-slide>
                              <div
                                class="swiper-button-next swiper-button-white"
                                slot="button-next"
                              ></div>
                              <div
                                class="swiper-button-prev swiper-button-white"
                                slot="button-prev"
                              ></div>
                            </swiper>
                          </client-only>

                          <client-only>
                            <swiper
                              class="swiper gallery-thumbs"
                              :options="swiperOptionThumbs"
                              ref="swiperThumbs"
                            >
                              <swiper-slide
                                class="slide-1"
                                v-for="item in approvedAds.photos"
                                :key="item.adsId"
                              >
                                <img
                                  class="img-fluid subImage"
                                  v-if="
                                    approvedAds.photos !== undefined &&
                                    approvedAds.photos.length > 0
                                  "
                                  :src="`https://BanooClubapi.simagar.com/${item.base64}`"
                                />
                              </swiper-slide>
                            </swiper>
                          </client-only>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="modal-footer">
                    <button
                      type="button"
                      class="btn btn-sm btn-outline-secondary"
                      data-bs-dismiss="modal"
                      ref="closeModal"
                    >
                      بستن
                    </button>
                    <button
                      type="button"
                      data-bs-toggle="modal"
                      data-bs-target="#staticBackdrop3"
                      class="btn btn-sm btn-outline-danger"
                    >
                      حذف
                    </button>
                  </div>
                </div>
              </div>
            </div>
            <div
              class="modal fade"
              id="staticBackdrop3"
              data-bs-backdrop="static"
              data-bs-keyboard="false"
              tabindex="-1"
              aria-labelledby="staticBackdrop3Label"
              aria-hidden="true"
            >
              <div class="modal-dialog">
                <div class="modal-content">
                  <div class="modal-header position-relative">
                    <h5 class="modal-title" id="staticBackdrop3Label">
                      حذف آگهی
                    </h5>
                    <button
                      type="button"
                      class="btn-close position-absolute"
                      data-bs-dismiss="modal"
                      aria-label="Close"
                    ></button>
                  </div>
                  <div class="modal-body">
                    <div class="row">
                      <div class="col-12">
                        <label class="d-block" for="updateKey">علت حذف</label>
                        <input
                          class="border rounded w-75 my-3 py-2"
                          id="updateKey"
                          type="text"
                          placeholder="علت حذف ..."
                          v-model.trim="deleteTextReason"
                        />
                      </div>
                    </div>
                  </div>
                  <div class="modal-footer">
                    <button
                      type="button"
                      class="btn btn-sm btn-outline-secondary text-muted closeNotApprove"
                      data-bs-dismiss="modal"
                      ref="closeNotApproved"
                    >
                      بستن
                    </button>
                    <button
                      type="button"
                      class="btn btn-sm btn-outline-danger"
                      @click="deleteApproved"
                    >
                      حذف
                    </button>
                  </div>
                </div>
              </div>
            </div>
            <!-- Modal -->
            <div
              class="modal fade"
              id="staticBackdrop"
              data-bs-backdrop="static"
              data-bs-keyboard="false"
              tabindex="-1"
              aria-labelledby="staticBackdropLabel"
              aria-hidden="true"
            >
              <div class="modal-dialog modal-xl">
                <div class="modal-content">
                  <div class="modal-header position-relative">
                    <h5 class="modal-title text-muted" id="staticBackdropLabel">
                      تایید آگهی
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
                      <div class="col-md-6 col-12">
                        <div class="d-flex flex-column">
                          <div>
                            <h1>
                              {{ approvedAds.title }}
                            </h1>
                          </div>
                          <div>
                            {{
                              approvedAds.createDate
                                | moment("jYYYY/jM/jD HH:mm")
                            }}
                          </div>
                          <div class="d-flex gap-3 my-3 border-bottom pb-2">
                            <div>تاریخ انقضاء:</div>
                            <div>
                              {{
                                approvedAds.description
                                  | moment("jYYYY/jM/jD HH:mm")
                              }}
                            </div>
                          </div>
                          <div class="d-flex gap-3 border-bottom pb-2">
                            <div>قیمت:</div>
                            <div>
                              {{ approvedAds.price }}
                            </div>
                          </div>
                          <div
                            class="d-flex flex-column gap-3 my-3 border-bottom pb-2"
                          >
                            <div>
                              <h3>توضیحات</h3>
                            </div>
                            <div>
                              {{ approvedAds.description }}
                            </div>
                          </div>
                        </div>
                      </div>
                      <div class="col-md-6 col-12">
                        <div class="thumb-example">
                          <client-only>
                            <swiper
                              class="swiper gallery-top"
                              :options="swiperOptionTop"
                              ref="swiperTop"
                            >
                              <swiper-slide
                                class="slide-1"
                                v-for="item in approvedAds.photos"
                                :key="item.adsId"
                              >
                                <img
                                  class="img-fluid mainImage"
                                  v-if="
                                    approvedAds.photos !== undefined &&
                                    approvedAds.photos.length > 0
                                  "
                                  :src="`https://BanooClubapi.simagar.com/${item.base64}`"
                                />
                              </swiper-slide>
                              <div
                                class="swiper-button-next swiper-button-white"
                                slot="button-next"
                              ></div>
                              <div
                                class="swiper-button-prev swiper-button-white"
                                slot="button-prev"
                              ></div>
                            </swiper>
                          </client-only>

                          <client-only>
                            <swiper
                              class="swiper gallery-thumbs"
                              :options="swiperOptionThumbs"
                              ref="swiperThumbs"
                            >
                              <swiper-slide
                                class="slide-2"
                                v-for="item in approvedAds.photos"
                                :key="item.adsId"
                              >
                                <img
                                  class="img-fluid subImage"
                                  v-if="
                                    approvedAds.photos !== undefined &&
                                    approvedAds.photos.length > 0
                                  "
                                  :src="`https://BanooClubapi.simagar.com/${item.base64}`"
                                />
                              </swiper-slide>
                            </swiper>
                          </client-only>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="modal-footer">
                    <button
                      type="button"
                      class="btn btn-sm btn-outline-secondary closeApprove1"
                      data-bs-dismiss="modal"
                      ref="closeConfirmationModal"
                    >
                      بستن
                    </button>
                    <button
                      type="button"
                      class="btn btn-sm btn-outline-primary"
                      data-bs-toggle="modal"
                      data-bs-dismiss="modal"
                      data-bs-target="#staticBackdropConfirm"
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
                      تایید آگهی
                    </h5>
                    <button
                      type="button"
                      class="btn-close"
                      data-bs-dismiss="modal"
                      aria-label="Close"
                    ></button>
                  </div>
                  <div class="modal-body">
                    آیا از تایید آگهی اطمینان دارید ؟
                  </div>
                  <div class="modal-footer">
                    <button
                      type="button"
                      class="btn btn-sm btn-outline-secondary closeApprove2"
                      data-bs-dismiss="modal"
                      ref="closeConfirmationModalNext"
                    >
                      خیر
                    </button>
                    <button
                      type="button"
                      class="btn btn-sm btn-outline-primary"
                      data-bs-dismiss="modal"
                      data-bs-target="#staticBackdrop"
                      @click="approvedSubmit"
                    >
                      تایید
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-12">
            <nav aria-label="Page navigation example">
              <ul class="pagination">
                <li
                  class="PaginationItemPrev"
                  @click="updateCurrent(currentPageIndex - 1)"
                >
                  <nuxt-link class="page-link" to="/SuperAdmin/AdsListApproved"
                    >صفحه قبل</nuxt-link
                  >
                </li>
                <li
                  :class="
                    pageIndex === pageNumber ? 'page-item active' : 'page-item'
                  "
                  v-for="pageIndex in paginateTotal"
                  :key="pageIndex"
                  @click="updateCurrent(pageIndex)"
                >
                  <nuxt-link
                    class="page-link"
                    to="/SuperAdmin/AdsListApproved"
                    >{{ pageIndex }}</nuxt-link
                  >
                </li>
                <li
                  class="PaginationItemNext"
                  @click="updateCurrent(currentPageIndex + 1)"
                >
                  <nuxt-link class="page-link" to="/SuperAdmin/AdsListApproved"
                    >صفحه بعد</nuxt-link
                  >
                </li>
              </ul>
            </nav>
          </div>
        </div>
        <div
          class="tab-pane fade"
          id="nav-profile"
          role="tabpanel"
          aria-labelledby="nav-profile-tab"
        >
          <div class="col-md-12 my-3">
            <input
              v-model="searchKey2"
              type="text"
              class="form-control"
              placeholder="جست و جو "
            />
          </div>
          <table
            class="table table-bordered table-hover table-bordered dataTable table-striped my-3"
          >
            <thead>
              <tr>
                <th @click="sortTable('photos')">
                  <div class="d-flex">
                    <div>تصاویر آگهی</div>
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
                <th @click="sortTable('userInfo')">
                  <div class="d-flex">
                    <div>کاربر</div>
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
                <th @click="sortTable('title')">
                  <div class="d-flex">
                    <div>عنوان آگهی</div>
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
                <th @click="sortTable('description')">
                  <div class="d-flex">
                    <div>توضیحات آگهی</div>
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
                <th @click="sortTable('status')">
                  <div class="d-flex">
                    <div>وضعیت آگهی</div>
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
                    <div class="">مشاهده</div>
                  </div>
                </th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="row in rejectedList" :key="row.adsId">
                <td>
                  <img
                    class="mx-1"
                    v-for="(item, index) in row.photos"
                    :key="index"
                    style="width: 20px; height: 20px"
                    :src="`https://BanooClubapi.simagar.com/${item.base64}`"
                  />
                </td>
                <td>{{ row.userInfo.name + " " + row.userInfo.familyName }}</td>
                <td>{{ row.title }}</td>
                <td>{{ row.description }}</td>
                <td v-html="row.status === 3 ? 'پذیرفته نشده است' : ''"></td>
                <td>
                  <div class="d-flex gap-5 justify-content-evenly">
                    <div class="" @click="dataModal(row)">
                      <button
                        type="button"
                        class="btn btn-sm SeeReportedPost"
                        data-bs-toggle="modal"
                        data-bs-target="#staticBackdrop4"
                      >
                        <i class="far fa-eye"></i>
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
                <li
                  class="PaginationItemPrev"
                  @click="updateCurrentRejected(currentPageIndexReject - 1)"
                >
                  <nuxt-link class="page-link" to="/SuperAdmin/AdsListApproved"
                    >صفحه قبل</nuxt-link
                  >
                </li>
                <li
                  :class="
                    pageIndexR === pageNumberReject
                      ? 'page-item active'
                      : 'page-item'
                  "
                  v-for="pageIndexR in paginateTotalReject"
                  :key="pageIndexR"
                  @click="updateCurrentRejected(pageIndexR)"
                >
                  <nuxt-link
                    class="page-link"
                    to="/SuperAdmin/AdsListApproved"
                    >{{ pageIndexR }}</nuxt-link
                  >
                </li>
                <li
                  class="PaginationItemNext"
                  @click="updateCurrentRejected(currentPageIndexReject + 1)"
                >
                  <nuxt-link class="page-link" to="/SuperAdmin/AdsListApproved"
                    >صفحه بعد</nuxt-link
                  >
                </li>
              </ul>
            </nav>
          </div>
        </div>
        <div
          class="tab-pane fade"
          id="nav-contact"
          role="tabpanel"
          aria-labelledby="nav-contact-tab"
        >
          <div class="col-md-12 my-3">
            <input
              v-model="searchKey3"
              type="text"
              class="form-control"
              placeholder="جست و جو "
            />
          </div>
          <table
            class="table table-bordered table-hover table-bordered dataTable table-striped my-3"
          >
            <thead>
              <tr>
                <th @click="sortTable('photos')">
                  <div class="d-flex">
                    <div>تصاویر آگهی</div>
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
                <th @click="sortTable('userInfo')">
                  <div class="d-flex">
                    <div>کاربر</div>
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
                <th @click="sortTable('title')">
                  <div class="d-flex">
                    <div>عنوان آگهی</div>
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
                <th @click="sortTable('description')">
                  <div class="d-flex">
                    <div>توضیحات آگهی</div>
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
                <th @click="sortTable('status')">
                  <div class="d-flex">
                    <div>وضعیت آگهی</div>
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
                    <div class="">مشاهده</div>
                  </div>
                </th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="row in Approved" :key="row.adsId">
                <td>
                  <img
                    class="mx-1"
                    v-for="(item, index) in row.photos"
                    :key="index"
                    style="width: 20px; height: 20px"
                    :src="`https://BanooClubapi.simagar.com/${item.base64}`"
                  />
                </td>
                <td v-if="row.userInfo !== null">
                  {{ row.userInfo.name + " " + row.userInfo.familyName }}
                </td>
                <td>{{ row.title }}</td>
                <td>{{ row.description }}</td>
                <td v-html="row.status === 1 ? 'تایید شده است' : ''"></td>
                <td>
                  <div class="d-flex gap-5 justify-content-evenly">
                    <div class="" @click="dataModal(row)">
                      <button
                        type="button"
                        class="btn btn-sm SeeReportedPost"
                        data-bs-toggle="modal"
                        data-bs-target="#staticBackdrop4"
                      >
                        <i class="far fa-eye"></i>
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
                <li
                  class="PaginationItemPrev"
                  @click="updateCurrentAccept(currentPageIndexAccept - 1)"
                >
                  <nuxt-link class="page-link" to="/SuperAdmin/AdsListApproved"
                    >صفحه قبل</nuxt-link
                  >
                </li>
                <li
                  :class="
                    pageIndex === pageNumberAccept
                      ? 'page-item active'
                      : 'page-item'
                  "
                  v-for="pageIndex in paginateTotalAccept"
                  :key="pageIndex"
                  @click="updateCurrentAccept(pageIndex)"
                >
                  <nuxt-link
                    class="page-link"
                    to="/SuperAdmin/AdsListApproved"
                    >{{ pageIndex }}</nuxt-link
                  >
                </li>
                <li
                  class="PaginationItemNext"
                  @click="updateCurrentAccept(currentPageIndexAccept + 1)"
                >
                  <nuxt-link class="page-link" to="/SuperAdmin/AdsListApproved"
                    >صفحه بعد</nuxt-link
                  >
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
import { Swiper, SwiperSlide } from "vue-awesome-swiper";
import "swiper/css/swiper.css";

export default {
  name: "AdsListApproved",
  layout: "SuperAdminPanel",
  components: {
    Swiper,
    SwiperSlide,
  },
  data() {
    return {
      tempAdStatus: 1,
      searchKey: null,
      searchKey2: null,
      searchKey3: null,
      ascending: false,
      sortColumn: "",
      toggleArrow: false,
      approvedPost: "",
      allAdsListApproved: [],
      Approved: [],
      rejectedList: [],
      approvedAds: {},
      categoryName: {},
      swiperOption: {
        slidesPerView: 1,
        spaceBetween: 0,
        slidesPerGroup: 3,
        loop: false,
        loopFillGroupWithBlank: true,
        pagination: {
          el: ".swiper-pagination",
          clickable: true,
        },
        navigation: {
          nextEl: ".swiper-button-next",
          prevEl: ".swiper-button-prev",
        },
      },
      deleteTextReason: "",
      swiperOptionTop: {
        loop: false,
        loopedSlides: 5,
        spaceBetween: 0,
        slidesPerView: 1,
        navigation: {
          nextEl: ".swiper-button-next",
          prevEl: ".swiper-button-prev",
        },
      },
      swiperOptionThumbs: {
        loop: true,
        slidesPerView: 3,
        loopedSlides: 5,
        spaceBetween: 0,
        centeredSlides: true,
        touchRatio: 0.2,
        slideToClickedSlide: true,
      },
      center: [35.757539, 51.409968],

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
    };
  },
  watch: {
    searchKey: async function (val, oldVal) {
      try {
        const response =
          await this.$repositories.getAllPendingAds.getAllPendingAds(
            1,
            10,
            val
          );
        this.allAdsListApproved = response.data.ads;
        this.totalCount = response.data.adsCount;
        this.paginateTotal = Math.ceil(this.totalCount / this.count);
        this.$nuxt.$loading.finish();
        this.$nuxt.loading = false;
      } catch (error) {
        console.log(error);
      }
    },
    searchKey2: function (val, oldVal) {
      this.$axios
        .post(`Ads/GetRejected`, null, {
          params: {
            pageNumber: 1,
            count: 10,
            search: val,
          },
        })
        .then((response) => {
          if (response.status === 200) {
            this.rejectedList = response.data.ads;
            this.totalCountReject = response.data.adsCount;
            this.paginateTotalReject = Math.ceil(
              this.totalCountReject / this.count
            );
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false;
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    searchKey3: function (val, oldVal) {
      this.$axios
        .post(`Ads/GetAll`, null, {
          params: {
            pageNumber: 1,
            count: 10,
            search: val,
          },
        })
        .then((response) => {
          if (response.status === 200) {
            this.Approved = response.data.ads;
            this.totalCountAccept = response.data.adsCount;
            this.paginateTotalAccept = Math.ceil(this.totalCount / this.count);
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
    changeAdStatus(event) {
      this.tempAdStatus = event.target.value;
    },
    async submitAdStatus() {
      try {
        await this.$repositories.changeAnAdStatus.changeAnAdStatus({
          adsId: this.approvedAds.adsId,
          title: this.approvedAds.title,
          categoryId: this.approvedAds.categoryId,
          price: this.approvedAds.price,
          expirationDate: this.approvedAds.expirationDate,
          status: this.tempAdStatus,
          userId: this.approvedAds.userId,
          description: this.approvedAds.description,
          latitude: this.approvedAds.latitude,
          longitude: this.approvedAds.longitude,
          createDate: this.approvedAds.createDate,
          photos: this.approvedAds.photos,
        });
        this.$refs.closeApprovedModal.click();
        this.getApproved(this.pageNumber, this.count, this.searchKey);
        this.getAdsListApproved(this.pageNumber, this.count, this.searchKey);
        this.getRejected(this.pageNumberReject, this.count);
      } catch (error) {
        console.log(error);
      }
    },
    async getRejected(pageNumber, count, search) {
      try {
        const response =
          await this.$repositories.getAllRejectedAds.getAllRejectedAds(
            pageNumber,
            count,
            search
          );
        this.rejectedList = response.data.ads;
        this.totalCountReject = response.data.adsCount;
        this.paginateTotalReject = Math.ceil(
          this.totalCountReject / this.count
        );
      } catch (error) {
        console.log(error);
      } finally {
        this.$nuxt.$loading.finish();
        this.$nuxt.loading = false;
      }
    },
    async getAdsListApproved(pageNumber, count, search) {
      try {
        const response =
          await this.$repositories.getAllPendingAds.getAllPendingAds(
            pageNumber,
            count,
            search
          );
        this.allAdsListApproved = response.data.ads;
        this.totalCount = response.data.adsCount;
        this.paginateTotal = Math.ceil(this.totalCount / this.count);
        this.$nuxt.$loading.finish();
        this.$nuxt.loading = false;
      } catch (error) {
        console.log(error);
      }
      // this.$axios
      //   .post(`Ads/GetNotConfirmed`, null, {
      //     params: {
      //       pageNumber: pageNumber,
      //       count: count,
      //       search: search,
      //     },
      //   })
      //   .then((response) => {
      //     if (response.status === 200) {
      //       this.allAdsListApproved = response.data.ads;
      //       this.totalCount = response.data.adsCount;
      //       this.paginateTotal = Math.ceil(this.totalCount / this.count);
      //       this.$nuxt.$loading.finish();
      //       this.$nuxt.loading = false;
      //     }
      //   })
      //   .catch((error) => {
      //     console.log(error);
      //   });
    },
    getApproved(pageNumber, count, search) {
      this.$axios
        .post(`Ads/GetAll`, null, {
          params: {
            pageNumber: pageNumber,
            count: count,
            search: search,
          },
        })
        .then((response) => {
          if (response.status === 200) {
            this.Approved = response.data.ads;
            this.totalCountAccept = response.data.adsCount;
            this.paginateTotalAccept = Math.ceil(this.totalCount / this.count);
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false;
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    updateCurrent(pageNumber) {
      if (pageNumber === 0 || pageNumber > this.totalCount) {
        return true;
      } else {
        this.$nuxt.$loading.start();
        this.getAdsListApproved(pageNumber, this.count, this.searchKey);
        this.pageNumber = pageNumber;
        this.currentPageIndex = pageNumber;
      }
    },
    updateCurrentRejected(pageNumber) {
      if (pageNumber === 0 || pageNumber > this.paginateTotalReject) {
        return true;
      } else {
        this.$nuxt.$loading.start();
        this.getRejected(this.pageNumberReject, this.count);
        this.pageNumberReject = pageNumber;
        this.currentPageIndexReject = pageNumber;
      }
    },
    updateCurrentAccept(pageNumber) {
      if (pageNumber === 0 || pageNumber > this.paginateTotalAccept) {
        return true;
      } else {
        this.$nuxt.$loading.start();
        this.getApproved(this.pageNumberAccept, this.count, this.searchKey);
        this.pageNumberAccept = pageNumber;
        this.currentPageIndexAccept = pageNumber;
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

      this.allAdsListApproved.sort(function (a, b) {
        if (a[col] > b[col]) {
          return ascending ? 1 : -1;
        } else if (a[col] < b[col]) {
          return ascending ? -1 : 1;
        }
        return 0;
      });
    },
    dataModal(data) {
      this.approvedAds = data;
      this.$axios
        .post(`AdsCategory/Get`, null, {
          params: {
            id: this.approvedAds.categoryId,
          },
        })
        .then((response) => {
          if (response.status === 200) {
            this.categoryName = response.data;
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    async approvedSubmit() {
      try {
        this.$nuxt.$loading.start();

        await this.$repositories.changeAnAdStatus.changeAnAdStatus({
          adsId: this.approvedAds.adsId,
          title: this.approvedAds.title,
          categoryId: this.approvedAds.categoryId,
          price: this.approvedAds.price,
          expirationDate: this.approvedAds.expirationDate,
          status: 1,
          userId: this.approvedAds.userId,
          description: this.approvedAds.description,
          latitude: this.approvedAds.latitude,
          longitude: this.approvedAds.longitude,
          createDate: this.approvedAds.createDate,
          photos: this.approvedAds.photos,
        });
        this.$refs.closeConfirmationModalNext.click();
        this.getAdsListApproved(this.pageNumber, this.count, this.searchKey);
        this.$toast.success("تایید آگهی با موفقیت انجام شد.");
      } catch (error) {
        console.log(error);
      } finally {
        this.$nuxt.$loading.finish();
      }
    },
    async deleteApproved() {
      try {
        await this.$repositories.changeAnAdStatus.changeAnAdStatus({
          adsId: this.approvedAds.adsId,
          title: this.approvedAds.title,
          categoryId: this.approvedAds.categoryId,
          price: this.approvedAds.price,
          expirationDate: this.approvedAds.expirationDate,
          status: 3,
          userId: this.approvedAds.userId,
          description: this.approvedAds.description,
          latitude: this.approvedAds.latitude,
          longitude: this.approvedAds.longitude,
          createDate: this.approvedAds.createDate,
          photos: this.approvedAds.photos,
          reason: this.deleteTextReason,
        });
        this.$refs.closeNotApproved.click();
        this.getAdsListApproved(this.pageNumber, this.count, this.searchKey);
        this.$toast.error("حذف آگهی با موفقیت انجام شد.");
      } catch (error) {
        console.log(error);
      }
    },
    callRejectApi() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      });
      this.getRejected(this.pageNumberReject, this.count);
    },
    callAcceptApi() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      });
      this.getApproved(this.pageNumberAccept, this.count, this.searchKey);
    },
  },
  mounted() {
    setTimeout(() => {
      this.$nextTick(() => {
        const swiperTop = this.$refs.swiperTop.$swiper;
        const swiperThumbs = this.$refs.swiperThumbs.$swiper;
        swiperTop.controller.control = swiperThumbs;
        swiperThumbs.controller.control = swiperTop;
      });
    }, 10);
    this.$nextTick(() => {
      this.$nuxt.$loading.start();
    });
    this.getAdsListApproved(this.pageNumber, this.count, this.searchKey);

    this.getApproved(this.pageNumberAccept, this.count, this.searchKey);
  },
  computed: {
    columns() {
      if (this.allAdsListApproved.length === 0) {
        return [];
      }
      return Object.keys(this.allAdsListApproved[0]);
    },
  },
};
</script>

<style scoped>
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

/*.swiper-slide.swiper-slide-active{*/
/*  width: 250px!important;*/
/*}*/

.mainImage {
  width: 15vw;
}

.subImage {
  width: 7vw;
}

.modalConfirm {
  margin: 25vh auto !important;
}
</style>
