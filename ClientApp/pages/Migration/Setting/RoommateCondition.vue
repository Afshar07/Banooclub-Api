<template>
  <div class="container-fluid px-0 mcontainer">
    <div class="row">
      <form class="col-12 px-4 accordianStyle">
        <div class="col-md-12 my-4">
          <h5><strong>وضعیت هم خانه</strong></h5>
          <div class="d-flex align-items-center gap-2">
            <span>وضعیت فعلی : </span>
            <span
              v-if="$store.state.HeaderData.activeRoomate"
              class="badge bg-success text-white"
            >فعال</span
            >
            <span v-else class="badge bg-danger text-white">غیر فعال</span>
          </div>
          <div class="d-flex align-items-center gap-2 my-2">
            <span>تغییر وضعیت به :</span>
            <select v-model="IsActive" class="form-control w-25">
              <option value="Active">فعال</option>
              <option value="DeActive">غیر فعال</option>
            </select>
            <button
              class="btn btn-primary"
              @click.prevent="ChangeRoomateStatus"
            >
              ثبت
            </button>
          </div>
        </div>
        <div class="accordion" dir="rtl" id="accordionPanelsStayOpenExample">
          <div class="accordion-item">
            <h2 class="accordion-header" id="panelsStayOpen-headingOne">
              <button
                class="accordion-button"
                type="button"
                data-bs-toggle="collapse"
                data-bs-target="#panelsStayOpen-collapseOne"
                aria-expanded="true"
                aria-controls="panelsStayOpen-collapseOne"
              >
                ویژگی های خانه
              </button>
            </h2>
            <div
              id="panelsStayOpen-collapseOne"
              class="accordion-collapse collapse show"
              aria-labelledby="panelsStayOpen-headingOne"
            >
              <div class="accordion-body">
                <div class="d-flex d-inline-flex flex-column">
                  <div class="labelText">نوع مکان زندگی :</div>
                  <div class="my-2 d-flex flex-column flex-md-row">
                    <div class="form-check form-check-inline">
                      <input
                        class="form-check-input"
                        type="radio"
                        name="inlineRadioOptions"
                        id="inlineRadio1"
                        value="1"
                        v-model="RoomType"
                      />
                      <label class="form-check-label" for="inlineRadio1"
                      >آپارتمان</label
                      >
                    </div>
                    <div class="form-check form-check-inline">
                      <input
                        class="form-check-input"
                        type="radio"
                        name="inlineRadioOptions"
                        id="inlineRadio2"
                        value="2"
                        v-model="RoomType"
                      />
                      <label class="form-check-label" for="inlineRadio2"
                      >خانه</label
                      >
                    </div>
                    <div class="form-check form-check-inline">
                      <input
                        class="form-check-input"
                        type="radio"
                        name="inlineRadioOptions"
                        id="inlineRadio3"
                        value="3"
                        v-model="RoomType"
                      />
                      <label class="form-check-label" for="inlineRadio3"
                      >سوییت</label
                      >
                    </div>
                    <div class="form-check form-check-inline">
                      <input
                        class="form-check-input"
                        type="radio"
                        name="inlineRadioOptions"
                        id="inlineRadio4"
                        value="4"
                        v-model="RoomType"
                      />
                      <label class="form-check-label" for="inlineRadio4"
                      >غیره</label
                      >
                    </div>
                  </div>
                  <div class="row my-2">
                    <div class="col-md-6 col-lg-6 col-12">
                      <div class="d-flex flex-column">
                        <div class="">
                          <div class="d-flex flex-row gap-3">
                            <div class="labelText">تعداد اتاق خواب</div>
                            <div class="me-auto">
                              <div
                                class="d-inline-flex"
                                @click="BedroomCount++"
                              >
                                <font-awesome-icon
                                  icon="plus-square"
                                  size="lg"
                                />
                              </div>
                              <div class="d-inline-flex px-3">
                                {{ BedroomCount }}
                              </div>
                              <div
                                class="d-inline-flex"
                                @click="BedroomCountDecrise"
                              >
                                <font-awesome-icon
                                  icon="minus-square"
                                  size="lg"
                                />
                              </div>
                            </div>
                          </div>
                        </div>
                        <div class="my-2">
                          <div class="d-flex flex-row gap-3">
                            <div class="labelText">تعداد سرویس بهداشتی</div>
                            <div class="me-auto">
                              <div
                                class="d-inline-flex"
                                @click="BathroomCount++"
                              >
                                <font-awesome-icon
                                  icon="plus-square"
                                  size="lg"
                                />
                              </div>
                              <div class="d-inline-flex px-3">
                                {{ BathroomCount }}
                              </div>
                              <div
                                class="d-inline-flex"
                                @click="BathroomCountDecrise"
                              >
                                <font-awesome-icon
                                  icon="minus-square"
                                  size="lg"
                                />
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="col-md-6 col-lg-6 col-12">
                      <div class="d-flex flex-column">
                        <div class="">
                          <div class="d-flex flex-row gap-3">
                            <div class="labelText">نوع اتاق خواب</div>
                            <div class="me-auto">
                              <div
                                class="d-flex flex-row justify-content-between"
                              >
                                <div class="">
                                  <label class="switch">
                                    <input
                                      type="checkbox"
                                      id="togBtn1"
                                      v-model="BedroomTypeValue"
                                      @input="BedroomTypeCheck"
                                    />
                                    <div class="slider round">
                                      <span class="on">خصوصی</span>
                                      <span class="off">مشترک</span>
                                    </div>
                                  </label>
                                </div>
                              </div>
                            </div>
                          </div>
                        </div>
                        <div class="">
                          <div class="d-flex flex-row gap-3">
                            <div class="labelText">نوع سرویس بهداشتی</div>
                            <div class="me-auto">
                              <div
                                class="d-flex flex-row justify-content-between"
                              >
                                <div class="">
                                  <label class="switch">
                                    <input
                                      type="checkbox"
                                      id="togBtn2"
                                      v-model="BathroomTypeValue"
                                      @input="BathroomTypeCheck"
                                    />
                                    <div class="slider round">
                                      <span class="on">خصوصی</span>
                                      <span class="off">مشترک</span>
                                    </div>
                                  </label>
                                </div>
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="labelText">نوع مالکیت مکان زندگی :</div>
                  <div class="my-2 d-flex flex-column flex-md-row">
                    <div class="form-check form-check-inline">
                      <input
                        class="form-check-input"
                        type="radio"
                        name="inlineRadioOptions99"
                        id="inlineRadio5"
                        value="1"
                        v-model="OwnerType"
                      />
                      <label class="form-check-label" for="inlineRadio5">
                        مالک هستم
                      </label>
                    </div>
                    <div class="form-check form-check-inline">
                      <input
                        class="form-check-input"
                        type="radio"
                        name="inlineRadioOptions99"
                        id="inlineRadio6"
                        value="2"
                        v-model="OwnerType"
                      />
                      <label class="form-check-label" for="inlineRadio6">
                        من نیز این مکان را اجاره کرده ام
                      </label>
                    </div>
                  </div>
                  <div class="row my-3 d-flex flex-column flex-md-row">
                    <div class="col-lg-3 col-12 w-auto mb-2 mb-md-0">
                      آدرس :
                    </div>
                    <div class="col-lg-3 col-12 w-auto">
                      <input
                        type="text"
                        placeholder="کشور"
                        v-model="countryText"
                      />
                    </div>
                    <div class="col-lg-3 col-12 w-auto my-md-0 my-3">
                      <input type="text" placeholder="شهر" v-model="cityText" />
                    </div>
                    <div class="col-lg-3 col-12 w-auto">
                      <input
                        type="text"
                        placeholder="آدرس"
                        v-model="addressText"
                      />
                    </div>
                  </div>
                  <!--                  <div class="d-flex flex-row gap-3">-->
                  <!--                    <div class="labelText">امکانات خانه :</div>-->
                  <!--                    <div class="w-50">-->
                  <!--                      <v-select-->
                  <!--                        :options="optionsHome"-->
                  <!--                        label="title" dir="rtl"-->
                  <!--                        multiple-->
                  <!--                        v-model="homeOptions"-->
                  <!--                        @input="homeOptionsValue"-->
                  <!--                      ></v-select>-->
                  <!--                    </div>-->
                  <!--                  </div>-->
                  <div>
                    <div class="d-flex flex-column flex-md-row gap-3">
                      <div class="labelText">امکانات خانه :</div>
                      <div class="d-flex flex-column flex-md-row">
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="checkbox"
                            id="inlineCheckbox20"
                            value="1"
                            v-model="HaveCCTV"
                          />
                          <label class="form-check-label" for="inlineCheckbox20"
                          >دوربین مدار بسته</label
                          >
                        </div>
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="checkbox"
                            id="inlineCheckbox21"
                            value="1"
                            v-model="HaveElevator"
                          />
                          <label class="form-check-label" for="inlineCheckbox21"
                          >آسانسور</label
                          >
                        </div>
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="checkbox"
                            id="inlineCheckbox22"
                            value="1"
                            v-model="HaveParking"
                          />
                          <label class="form-check-label" for="inlineCheckbox22"
                          >پارکینگ</label
                          >
                        </div>
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="checkbox"
                            id="inlineCheckbox223"
                            value="1"
                            v-model="HavelobbyMan"
                          />
                          <label class="form-check-label" for="inlineCheckbox22"
                          >سرایدار</label
                          >
                        </div>
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="checkbox"
                            id="inlineCheckbox2232"
                            value="1"
                            v-model="HaveFurniture"
                          />
                          <label class="form-check-label" for="inlineCheckbox22"
                          >فرنیش</label
                          >
                        </div>
                      </div>
                    </div>
                    <div class="col-md-12">
                      <client-only>
                        <l-map :zoom="17" :center="center" @click="addMarker">
                          <l-tile-layer
                            url="http://{s}.tile.osm.org/{z}/{x}/{y}.png"
                          ></l-tile-layer>
                          <l-marker :lat-lng="latlng"></l-marker>
                        </l-map>
                      </client-only>
                    </div>
                  </div>
                  <!--                  <div class="my-3">-->
                  <!--                    <div class="d-flex flex-row gap-3">-->
                  <!--                      <div class="labelText">امکانات محله :</div>-->
                  <!--                      <div class="">-->
                  <!--                        <div class="form-check form-check-inline">-->
                  <!--                          <input class="form-check-input" type="checkbox" id="inlineCheckbox23" value="1"-->
                  <!--                                 v-model="NearPublicTransport">-->
                  <!--                          <label class="form-check-label" for="inlineCheckbox23">نزدیک به ایستگاه اتوبوس</label>-->
                  <!--                        </div>-->
                  <!--                        <div class="form-check form-check-inline">-->
                  <!--                          <input class="form-check-input" type="checkbox" id="inlineCheckbox24" value="1"-->
                  <!--                                 v-model="NearSubway">-->
                  <!--                          <label class="form-check-label" for="inlineCheckbox24">نزدیک به ایستگاه قطار</label>-->
                  <!--                        </div>-->
                  <!--                        <div class="form-check form-check-inline">-->
                  <!--                          <input class="form-check-input" type="checkbox" id="inlineCheckbox25" value="1"-->
                  <!--                                 v-model="NearPark">-->
                  <!--                          <label class="form-check-label" for="inlineCheckbox25">نزدیک به پارک</label>-->
                  <!--                        </div>-->
                  <!--                      </div>-->
                  <!--                    </div>-->
                  <!--                  </div>-->
                  <!--                  <div class="d-flex flex-row gap-3 my-3">-->
                  <!--                    <div class="labelText">امکانات محله :</div>-->
                  <!--                    <div class="w-50">-->
                  <!--                      <v-select-->
                  <!--                        :options="optionsNeighbourhood"-->
                  <!--                        label="title" dir="rtl"-->
                  <!--                        multiple-->
                  <!--                        v-model="Neighbourhood"-->
                  <!--                        @input="NeighbourhoodValue"-->
                  <!--                      ></v-select>-->
                  <!--                    </div>-->
                  <!--                  </div>-->

                  <div class="d-flex flex-column flex-md-row gap-3 my-3">
                    <div class="labelText">ارسال عکس :</div>

                    <div
                      class="border rounded text-center justify-content-center align-items-center InputUiBox"
                      @click="callInputMethod"
                    >
                      <input
                        type="file"
                        accept="image/*"
                        multiple="multiple"
                        class="form-control-file my-file d-none"
                        id="my-file"
                        @change="onFileChange"
                      />
                      <font-awesome-icon icon="plus-square" size="lg" />
                    </div>

                    <div id="preview" v-if="url">
                      <img
                        class="img-fluid"
                        v-for="item in url"
                        :src="item"
                        :key="item"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="accordion-item">
            <h2 class="accordion-header" id="panelsStayOpen-headingTwo">
              <button
                class="accordion-button collapsed"
                type="button"
                data-bs-toggle="collapse"
                data-bs-target="#panelsStayOpen-collapseTwo"
                aria-expanded="false"
                aria-controls="panelsStayOpen-collapseTwo"
              >
                هزینه ها
              </button>
            </h2>
            <div
              id="panelsStayOpen-collapseTwo"
              class="accordion-collapse collapse"
              aria-labelledby="panelsStayOpen-headingTwo"
            >
              <div class="accordion-body">
                <div class="d-flex flex-column">
                  <div>
                    <div class="d-flex flex-column flex-md-row gap-3">
                      <div class="labelText">اجاره روزانه :</div>
                      <div class="d-flex align-items-center gap-2">
                        <input
                          type="text"
                          class="form-control"
                          v-model.trim="DailyRent"
                        />
                        <div class="labelText text-nowrap small">
                          هزار تومان
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="my-3">
                    <div class="d-flex flex-column flex-md-row gap-3">
                      <div class="labelText">مبلغ رهن :</div>
                      <div class="d-flex align-items-center gap-2">
                        <input
                          type="number"
                          v-model.trim="Mortgage"
                          class="form-control"
                        />
                        <div class="labelText text-nowrap">هزار تومان</div>
                      </div>
                    </div>
                  </div>
                  <div>
                    <!--                    <div class="d-flex flex-row">-->
                    <!--                      <div class="labelText">زمان دریافت اجاره :</div>-->
                    <!--                      <div class="">-->
                    <!--                        <div class="form-check form-check-inline">-->
                    <!--                          <input class="form-check-input" type="radio" name="inlineRadioOptions83" id="inlineRadio7"-->
                    <!--                                 value="1" v-model="RentPeriod">-->
                    <!--                          <label class="form-check-label" for="inlineRadio7">ماهانه</label>-->
                    <!--                        </div>-->
                    <!--                        <div class="form-check form-check-inline">-->
                    <!--                          <input class="form-check-input" type="radio" name="inlineRadioOptions83" id="inlineRadio8"-->
                    <!--                                 value="3" v-model="RentPeriod">-->
                    <!--                          <label class="form-check-label" for="inlineRadio8">3 ماه</label>-->
                    <!--                        </div>-->
                    <!--                        <div class="form-check form-check-inline">-->
                    <!--                          <input class="form-check-input" type="radio" name="inlineRadioOptions83" id="inlineRadio9"-->
                    <!--                                 value="6" v-model="RentPeriod">-->
                    <!--                          <label class="form-check-label" for="inlineRadio9">6 ماه</label>-->
                    <!--                        </div>-->
                    <!--                        <div class="form-check form-check-inline">-->
                    <!--                          <input class="form-check-input" type="radio" name="inlineRadioOptions83" id="inlineRadio10"-->
                    <!--                                 value="9" v-model="RentPeriod">-->
                    <!--                          <label class="form-check-label" for="inlineRadio10">9 ماه</label>-->
                    <!--                        </div>-->
                    <!--                        <div class="form-check form-check-inline">-->
                    <!--                          <input class="form-check-input" type="radio" name="inlineRadioOptions83" id="inlineRadio11"-->
                    <!--                                 value="12" v-model="RentPeriod">-->
                    <!--                          <label class="form-check-label" for="inlineRadio11">سالانه</label>-->
                    <!--                        </div>-->
                    <!--                      </div>-->
                    <!--                    </div>-->
                  </div>
                  <div class="my-3">
                    <div
                      class="d-flex flex-column flex-md-row align-items-md-center gap-3"
                    >
                      <div class="labelText">تاریخ پذیرش هم خانه :</div>
                      <client-only>
                        <date-picker
                          format="YYYY-MM-DD"
                          display-format="jYYYY-jMM-jDD"
                          v-model="ReceptionDate"
                          type="date"
                        />
                      </client-only>
                    </div>
                  </div>
                  <div>
                    <div class="d-flex flex-column flex-md-row gap-3">
                      <div class="labelText">
                        امکاناتی که با اجاره محاسبه شده اند :
                      </div>
                      <div class="d-flex flex-column flex-md-row gap-2">
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="checkbox"
                            id="inlineCheckbox1"
                            value="1"
                            v-model="WithWater"
                          />
                          <label class="form-check-label" for="inlineCheckbox1"
                          >آب</label
                          >
                        </div>
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="checkbox"
                            id="inlineCheckbox2"
                            value="1"
                            v-model="WithElectricity"
                          />
                          <label class="form-check-label" for="inlineCheckbox2"
                          >برق</label
                          >
                        </div>
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="checkbox"
                            id="inlineCheckbox3"
                            value="1"
                            v-model="WithGaz"
                          />
                          <label class="form-check-label" for="inlineCheckbox3"
                          >گاز</label
                          >
                        </div>
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="checkbox"
                            id="inlineCheckbox4"
                            value="1"
                            v-model="WithInternet"
                          />
                          <label class="form-check-label" for="inlineCheckbox4"
                          >اینترنت</label
                          >
                        </div>
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="checkbox"
                            id="inlineCheckbox5"
                            value="1"
                            v-model="WithGarbage"
                          />
                          <label class="form-check-label" for="inlineCheckbox5"
                          >جمع آوری زباله</label
                          >
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="labelText mt-3">مشخص کردن مکان روی نقشه:</div>
                  <div class="my-3">
                    <div id="map-wrap" style="height: 50vh">
                      <client-only>
                        <l-map :zoom="17" :center="center" @click="addMarker">
                          <l-tile-layer
                            url="http://{s}.tile.osm.org/{z}/{x}/{y}.png"
                          ></l-tile-layer>
                          <l-marker :lat-lng="latlng"></l-marker>
                        </l-map>
                      </client-only>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="accordion-item">
            <h2 class="accordion-header" id="panelsStayOpen-headingThree">
              <button
                class="accordion-button collapsed"
                type="button"
                data-bs-toggle="collapse"
                data-bs-target="#panelsStayOpen-collapseThree"
                aria-expanded="false"
                aria-controls="panelsStayOpen-collapseThree"
              >
                مشخصات هم خانه
              </button>
            </h2>
            <div
              id="panelsStayOpen-collapseThree"
              class="accordion-collapse collapse"
              aria-labelledby="panelsStayOpen-headingThree"
            >
              <div class="accordion-body">
                <div class="d-flex flex-column">
                  <div>
                    <div class="d-flex flex-column flex-md-row">
                      <div class="labelText d-block">جنسیت هم خانه :</div>
                      <div class="d-flex flex-column flex-md-row mt-3 mt-md-0">
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="checkbox"
                            id="inlineCheckbox6"
                            value="1"
                            v-model="RoomateGenderNot"
                          />
                          <label class="form-check-label" for="inlineCheckbox6"
                          >فرقی نمی کند</label
                          >
                        </div>
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="checkbox"
                            id="inlineCheckbox7"
                            value="2"
                            v-model="RoomateGenderMale"
                          />
                          <label class="form-check-label" for="inlineCheckbox7"
                          >مرد</label
                          >
                        </div>
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="checkbox"
                            id="inlineCheckbox8"
                            value="3"
                            v-model="RoomateGenderFemale"
                          />
                          <label class="form-check-label" for="inlineCheckbox8"
                          >زن</label
                          >
                        </div>
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="checkbox"
                            id="inlineCheckbox9"
                            value="4"
                            v-model="RoomateGenderOther"
                          />
                          <label class="form-check-label" for="inlineCheckbox9"
                          >سایر</label
                          >
                        </div>
                      </div>

                      <div></div>
                    </div>
                  </div>
                  <div>
                    <div class="col-md-12 my-3">
                      <div
                        class="d-flex flex-column flex-md-row flex-wrap gap-3"
                      >
                        <small>محدوده سنی هم خانه</small>
                        <input type="number" v-model="RoomateAgeRangeFrom" />
                        <small>تا</small>
                        <input type="number" v-model="RoomateAgeRangeTo" />
                      </div>
                    </div>
                  </div>
                  <div>
                    <div class="row">
                      <div class="col-md-4 col-lg-4 col-12 my-md-0 my-2">
                        <label for="" class="m-0 mb-2">سیگار </label>
                        <v-select
                          :options="options"
                          dir="rtl"
                          label="title"
                          placeholder="سیگار"
                          class="selectWidth"
                          v-model="RoomateSmoke"
                          :reduce="(title) => title.code"
                        ></v-select>
                      </div>
                      <div class="col-md-4 col-lg-4 col-12 my-md-0 my-2">
                        <label for="" class="m-0 mb-2">همراه(فرزند) </label>
                        <v-select
                          :options="options"
                          dir="rtl"
                          label="title"
                          placeholder="فرزند"
                          class="selectWidth"
                          v-model="RoomateChild"
                          :reduce="(title) => title.code"
                        ></v-select>
                      </div>
                      <!--                      <div class="col-md-4 col-lg-4 col-12 my-md-0 my-2">-->
                      <!--                        <v-select-->
                      <!--                          :options="options"-->
                      <!--                          dir="rtl"-->
                      <!--                          label="title"-->
                      <!--                          placeholder="حیوان خانگی"-->
                      <!--                          class="selectWidth"-->
                      <!--                          v-model="RoomatePet"-->
                      <!--                          :reduce="title => title.code"-->
                      <!--                        ></v-select>-->
                      <!--                      </div>-->
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="accordion-item">
            <h2 class="accordion-header" id="panelsStayOpen-headingFour">
              <button
                class="accordion-button collapsed"
                type="button"
                data-bs-toggle="collapse"
                data-bs-target="#panelsStayOpen-collapseFour"
                aria-expanded="false"
                aria-controls="panelsStayOpen-collapseFour"
              >
                مشخصات من و وضعیت فعلی در خانه
              </button>
            </h2>
            <div
              id="panelsStayOpen-collapseFour"
              class="accordion-collapse collapse"
              aria-labelledby="panelsStayOpen-headingFour"
            >
              <div class="accordion-body">
                <div class="d-flex flex-column">
                  <div class="my-3">
                    <div class="d-flex flex-column flex-md-row">
                      <div class="labelText">چند نفر در خانه ساکن هستند ؟</div>
                      <div>
                        <div class="d-flex flex-column flex-md-row">
                          <div class="form-check form-check-inline">
                            <input
                              class="form-check-input"
                              type="radio"
                              name="inlineRadioOptions50"
                              id="inlineRadio12"
                              value="1"
                              v-model="ResidenceNumber"
                            />
                            <label class="form-check-label" for="inlineRadio12"
                            >فقط من</label
                            >
                          </div>
                          <div class="form-check form-check-inline">
                            <input
                              class="form-check-input"
                              type="radio"
                              name="inlineRadioOptions50"
                              id="inlineRadio13"
                              value="2"
                              v-model="ResidenceNumber"
                            />
                            <label class="form-check-label" for="inlineRadio13"
                            >با خودم 2 نفر
                            </label>
                          </div>
                          <div class="form-check form-check-inline">
                            <input
                              class="form-check-input"
                              type="radio"
                              name="inlineRadioOptions50"
                              id="inlineRadio14"
                              value="3"
                              v-model="ResidenceNumber"
                            />
                            <label class="form-check-label" for="inlineRadio14"
                            >بیش از 2 نفر</label
                            >
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!--                if not completed in profile -->
                  <!--                if not completed in profile -->
                  <!--                if not completed in profile -->
                  <div v-if="true">
                    <div class="d-flex flex-column flex-md-row">
                      <div class="labelText">جنسیت من :</div>
                      <div class="d-flex flex-column flex-md-row gap-2">
                        <!--                        <div class="form-check form-check-inline">-->
                        <!--                          <input class="form-check-input" type="radio" name="inlineRadioOptions20" id="inlineRadio18"-->
                        <!--                                 value="1" v-model="OwnerGender">-->
                        <!--                          <label class="form-check-label" for="inlineRadio18">فرقی نمی کند</label>-->
                        <!--                        </div>-->
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="radio"
                            name="inlineRadioOptions20"
                            id="inlineRadio19"
                            value="2"
                            v-model="OwnerGender"
                          />
                          <label class="form-check-label" for="inlineRadio19"
                          >مرد</label
                          >
                        </div>
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="radio"
                            name="inlineRadioOptions20"
                            id="inlineRadio20"
                            value="3"
                            v-model="OwnerGender"
                          />
                          <label class="form-check-label" for="inlineRadio20"
                          >زن</label
                          >
                        </div>
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="radio"
                            name="inlineRadioOptions20"
                            id="inlineRadio21"
                            value="4"
                            v-model="OwnerGender"
                          />
                          <label class="form-check-label" for="inlineRadio21"
                          >سایر</label
                          >
                        </div>
                      </div>
                    </div>
                    <div class="d-flex flex-column flex-md-row gap-3 my-3">
                      <div class="labelText">سن من :</div>
                      <div>
                        <input
                          type="number"
                          v-model.trim="OwnerAge"
                          value="18"
                        />
                      </div>
                    </div>
                    <div class="d-flex flex-column flex-md-row gap-3">
                      <div class="labelText">زبان های مکالمه :</div>
                      <div>
                        <v-select
                          :options="optionsOwnerLangs"
                          label="title"
                          dir="rtl"
                          class="selectWidth"
                          multiple
                          v-model="ownerLanguages"
                          :reduce="(title) => title.code"
                        ></v-select>
                      </div>
                    </div>
                    <!--                    <div class="d-flex flex-row gap-3 my-3">-->
                    <!--                      <div class="labelText">شغل :</div>-->
                    <!--                      <div>-->
                    <!--                        <input class="border p-1 rounded" maxlength="50" v-model="OwnerJob" type="text"-->
                    <!--                               placeholder="شغل ..."/>-->
                    <!--                      </div>-->
                    <!--                    </div>-->
                  </div>
                  <div class="labelText my-4">توضیحات :</div>
                  <div class="row">
                    <textarea
                      placeholder="توضیحات درباره من یا هم خانه مورد نظر یا مکان زندگی"
                      v-model="OwnerDescription"
                    />
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!--          <div class="accordion-item">-->
          <!--            <h2 class="accordion-header" id="panelsStayOpen-headingFive">-->
          <!--              <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"-->
          <!--                      data-bs-target="#panelsStayOpen-collapseFive" aria-expanded="false"-->
          <!--                      aria-controls="panelsStayOpen-collapseFive">-->
          <!--                ثبت آگهی و پرداخت-->
          <!--              </button>-->
          <!--            </h2>-->
          <!--            <div id="panelsStayOpen-collapseFive" class="accordion-collapse collapse"-->
          <!--                 aria-labelledby="panelsStayOpen-headingFive">-->
          <!--              <div class="accordion-body">-->
          <!--                <div class="d-flex flex-column">-->
          <!--                  <div class="">-->
          <!--                    <div class="d-flex flex-row gap-3">-->
          <!--                      <div class="labelText">مبلغ</div>-->
          <!--                      <div>200000</div>-->
          <!--                      <div class="labelText">تومان</div>-->
          <!--                    </div>-->
          <!--                  </div>-->
          <!--                  <div class="my-3">-->
          <!--                    <div class="d-flex flex-row gap-5">-->
          <!--                      <div>-->
          <!--                        <button class="btn btn-sm btn-info">-->
          <!--                          پرداخت توسط کیف پول-->
          <!--                        </button>-->
          <!--                      </div>-->
          <!--                      <div>-->
          <!--                        <button class="btn btn-sm btn-success">-->
          <!--                          پرداخت توسط درگاه بانکی-->
          <!--                        </button>-->
          <!--                      </div>-->
          <!--                    </div>-->
          <!--                  </div>-->
          <!--                </div>-->
          <!--              </div>-->
          <!--            </div>-->
          <!--          </div>-->
        </div>
        <div class="col-12 my-md-5 my-2 text-center">
          <button
            type="button"
            class="btn btn-sm AddReplyBtn"
            @click="saveRoommateInformation"
          >
            ذخیره اطلاعات هم خانه
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
// import veeno from "veeno";
// import "nouislider/distribute/nouislider.min.css";
import QuillExampleNuxt from "../../../components/editor";

export default {
  name: "RoommateCondition",
  components: { QuillExampleNuxt },
  layout: "BanooClubLayout",
  data() {
    return {
      url: [],
      homeOptions: [],
      optionsHome: [
        { title: "آسانسور", code: 1 },
        { title: "دوربین مدار بسته", code: 2 },
        { title: "پارکینگ", code: 3 },
      ],
      optionsOwnerLangs: [
        { title: "انگلیسی", code: 1 },
        { title: "فرانسوی", code: 2 },
        { title: "عربی", code: 3 },
        { title: "فارسی", code: 4 },
        { title: "آلمانی", code: 5 },
        { title: "ترکی", code: 6 },
      ],
      options: [
        { title: "بله", code: 1 },
        { title: "خیر", code: 2 },
        { title: "فرقی نمی کند", code: 3 },
      ],
      imagesUrl: [],
      countryText: "",
      cityText: "",
      addressText: "",
      RoommateId: null,
      RoomType: null,
      BedroomCount: 2,
      BedroomType: null,
      BedroomTypeValue: null,
      BathroomCount: 2,
      BathroomType: null,
      BathroomTypeValue: null,
      OwnerType: null,
      Address: "",
      Longtitude: null,
      Latitude: null,
      HavelobbyMan: null,
      HaveCCTV: null,
      HaveElevator: null,
      HaveFurniture: null,
      HaveParking: null,
      DailyRent: 0,
      IsActive: false,
      Mortgage: null,
      RentPeriod: null,
      ReceptionDate: "",
      WithElectricity: null,
      WithWater: null,
      WithGaz: null,
      WithInternet: null,
      WithGarbage: null,
      RoomateGender: "",
      RoomateAgeRangeFrom: null,
      RoomateAgeRangeTo: null,
      RoomateSmoke: null,
      RoomateChild: null,
      RoomatePet: null,
      ResidenceNumber: null,
      OwnerGender: null,
      OwnerAge: null,
      OwnerLangs: "",
      OwnerJob: "",
      OwnerDescription: "",
      UserId: null,
      PaymentId: null,
      center: [35.757539, 51.409968],
      latlng: [35.757539, 51.409968],
      RoomateGenderNot: false,
      RoomateGenderMale: false,
      RoomateGenderFemale: false,
      RoomateGenderOther: false,
      RoomateGenderOther1: false,
      RoomateGenderOther2: false,
      RoomateGenderOther3: false,
      RoomateGenderOther4: false,
      RoomateGenderOther5: false,
      roomateData: {},
      myRoommateData: {},
      photos: [],
      homeImage: "",
      ownerLanguages: [],
    };
  },
  methods: {
    addMarker(event) {
      this.latlng = event.latlng;
      this.Latitude = event.latlng[0];
      this.Longtitude = event.latlng[1];
    },
    getSliderValue(value) {
      this.RoomateAgeRangeFrom = value.positions[0];
      this.RoomateAgeRangeTo = value.positions[1];
    },
    callInputMethod() {
      document.querySelector(".my-file").click();
    },
    BedroomCountDecrise() {
      if (this.BedroomCount > 0) {
        this.BedroomCount--;
      }
    },
    BathroomCountDecrise() {
      if (this.BathroomCount > 0) {
        this.BathroomCount--;
      }
    },
    textOnPictureHandler(value) {
      this.OwnerDescription = value;
    },
    onFileChange(e) {
      const file = e.target.files[0];
      this.url.push(URL.createObjectURL(file));
      const that = this;
      const reader = new FileReader();
      reader.onload = (function (theFile) {
        return function () {
          const binaryData = reader.result;
          that.homeImage = window.btoa(binaryData);
          // console.log(that.subImage)
          that.photos.push({
            base64: that.homeImage,
            priority: 1,
          });
        };
      })(file);
      reader.readAsBinaryString(file);
    },

    saveRoommateInformation() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      });
      this.$axios
        .post(
          `Roomate/Update`,
          {
            roomateId: this.myRoommateData.roomateId,
            roomType: parseInt(this.RoomType),
            bedroomCount: this.BedroomCount,
            bedroomType: this.BedroomType === null ? 0 : 1,
            bathroomCount: this.BathroomCount,
            bathroomType: this.BathroomType === null ? 0 : 1,
            ownerType: parseInt(this.OwnerType),
            address: this.addressConcat,
            longtitude: this.Longtitude,
            latitude: this.Latitude,
            haveLobbyMan: this.HavelobbyMan ? 1 : 0,
            haveParking: this.HaveParking ? 1 : 0,
            haveFurniture: this.HaveFurniture ? 1 : 0,
            haveCCTV: this.HaveCCTV ? 1 : 0,
            haveElevator: this.HaveElevator ? 1 : 0,
            dailyRent: parseInt(this.DailyRent),
            mortgage: parseInt(this.Mortgage),
            receptionDate: this.ReceptionDate,
            rentPeriod: this.RentPeriod === null ? 1 : this.RentPeriod,
            withElectricity: parseInt(this.WithElectricity ? "1" : "0"),
            withWater: parseInt(this.WithWater ? "1" : "0"),
            withGaz: parseInt(this.WithGaz ? "1" : "0"),
            withInternet: parseInt(this.WithInternet ? "1" : "0"),
            withGarbage: parseInt(this.WithGarbage ? "1" : "0"),
            roomateGender: this.roomateGenderString,
            roomateAgeRangeFrom: Math.round(this.RoomateAgeRangeFrom),
            roomateAgeRangeTo: Math.round(this.RoomateAgeRangeTo),
            roomateSmoke: this.RoomateSmoke === null ? 3 : this.RoomateSmoke,
            roomateChild: this.RoomateChild === null ? 3 : this.RoomateChild,
            residenceNumber: parseInt(this.ResidenceNumber),
            ownerGender: parseInt(this.OwnerGender),
            ownerAge: parseInt(this.OwnerAge),
            ownerLangs: this.StringOwnerLangs,
            description: this.OwnerDescription,
            userId: this.$auth.user.userId,
            paymentId: null,
            photos: this.photos,
          },
          {}
        )
        .then((response) => {
          if (response.status === 200) {
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false;
            this.$toast.success("ثبت اطلاعات هم خانه  با موفقیت انجام شد.");
            this.$router.push({ path: "/Migration/MyRoomateInfo" });
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    BathroomTypeCheck(e) {
      this.BathroomType = this.BathroomTypeValue ? 1 : 0;
    },
    BedroomTypeCheck(e) {
      this.BedroomType = this.BedroomTypeValue ? 1 : 0;
    },
    ChangeRoomateStatus() {
      let active = false;
      if (this.IsActive === "Active") {
        active = true;
      } else if (this.IsActive === "DeActive") {
        active = false;
      }
      this.$axios
        .post(`/Roomate/ChangeRoomateActivation?state=${active}`, null, {})
        .then((res) => {
          if (res.data === 3) {
            this.$toast.error(
              "پاسپورت و کارت ملی شما توسط ادمین رد شده است و به همین دلیل امکان تغییر وضعیت هم خانه وجود ندارد"
            );
          } else if (res.data === 2) {
            this.$toast.error(
              "پاسپورت و کارت ملی شما هنوز توسط ادمین تایید نشده است !"
            );
          } else if (res.data === 4) {
            this.$toast.error(
              "شما هنوز پاسپورت و کارت ملی خود را آپلود نکرده اید."
            );
          }
          this.$axios.post(`Common/GetIndexData`, null, {}).then((response) => {
            this.$store.commit("SetUserData", response.data);
          });
        });
    },

    getRoomateInfo() {
      this.$axios
        .post(`Roomate/GetMyRoomate`, null, {})
        .then((response) => {
          if (response.data.status !== 2) {
            this.roomateData = response.data;
            this.myRoommateData = response.data.roomate;
            this.RoomType = this.myRoommateData.roomType;
            this.BedroomCount = this.myRoommateData.bedroomCount;
            this.BathroomCount = this.myRoommateData.bathroomCount;
            this.BedroomType = this.myRoommateData.bedroomType;
            this.BathroomTypeValue = this.myRoommateData.bathroomType;
            this.OwnerType = this.myRoommateData.ownerType;
            this.HaveParking = this.myRoommateData.haveParking;
            this.HaveElevator = this.myRoommateData.haveElevator;
            this.HaveFurniture = this.myRoommateData.haveFurniture;
            this.HaveCCTV = this.myRoommateData.haveCCTV;
            this.HavelobbyMan = this.myRoommateData.haveLobbyMan;
            this.DailyRent = this.myRoommateData.dailyRent;
            this.Mortgage = this.myRoommateData.mortgage;
            this.RentPeriod = this.myRoommateData.rentPeriod;
            this.ReceptionDate = this.myRoommateData.receptionDate;
            this.WithGarbage = this.myRoommateData.withGarbage;
            this.WithInternet = this.myRoommateData.withInternet;
            this.WithGaz = this.myRoommateData.withGaz;
            this.WithWater = this.myRoommateData.withWater;
            this.WithElectricity = this.myRoommateData.withElectricity;
            this.RoomateGenderNot =
              this.myRoommateData.roomateGender.includes("1");
            this.RoomateGenderMale =
              this.myRoommateData.roomateGender.includes("2");
            this.RoomateGenderFemale =
              this.myRoommateData.roomateGender.includes("3");
            this.RoomateGenderOther =
              this.myRoommateData.roomateGender.includes("4");
            this.minAge = this.myRoommateData.roomateAgeRangeFrom;
            this.maxAge = this.myRoommateData.roomateAgeRangeTo;
            this.RoomateSmoke = this.myRoommateData.roomateSmoke;
            this.RoomateChild = this.myRoommateData.roomateChild;
            this.ResidenceNumber = this.myRoommateData.residenceNumber;
            this.OwnerGender = this.myRoommateData.ownerGender;
            this.OwnerAge = this.myRoommateData.ownerAge;
            this.ownerLanguages = this.deStructLanuage(
              this.myRoommateData.ownerLangs
            );
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },

    deStructLanuage(data) {
      const unrepeated = (str) => [...new Set(str)].join("");
      data = unrepeated(data);
      var words = data.split(" ");
      var arr = [];

      for (var i = 0; i < words.length; i++) {
        arr.push(words[i]);
      }
      return arr;
    },
  },
  mounted() {
    this.getRoomateInfo();
  },
  computed: {
    addressConcat() {
      return this.Address.concat(
        this.countryText,
        "-",
        this.cityText,
        "-",
        this.addressText
      );
    },
    roomateGenderString() {
      return this.RoomateGender.concat(
        this.RoomateGenderNot ? "1," : "",
        this.RoomateGenderMale ? "2," : "",
        this.RoomateGenderFemale ? "3," : "",
        this.RoomateGenderOther ? "4," : "",
        this.RoomateGenderOther1 ? "5," : "",
        this.RoomateGenderOther2 ? "6," : "",
        this.RoomateGenderOther3 ? "7," : "",
        this.RoomateGenderOther4 ? "8," : "",
        this.RoomateGenderOther5 ? "9," : ""
      );
    },
    StringOwnerLangs() {
      for (let j = 0; j < this.ownerLanguages.length; j++) {
        this.OwnerLangs += " " + this.ownerLanguages[j];
      }
      return this.OwnerLangs;
    },
  },
};
</script>

<style scoped>
@media (min-width: 1024px) {
  .mcontainer {
    max-width: 1175px;
    padding: 30px 120px;
    margin-left: auto;
    margin-right: auto;
  }
}
.accordianStyle {
  margin: 0 auto;
}

.labelText {
  color: #545454;
  font-size: 14px;
  cursor: pointer;
}

[type="search"]::placeholder {
  font-size: 12px !important;
  color: #cccccc !important;
}

.AddReplyBtn {
  background-color: #3a49df;
  color: white;
  transition: 0.2s ease;
}

#preview img {
  width: 100px;
  height: 100px;
  margin: 0 8px;
  border-radius: 8px;
}

textarea {
  resize: none;
  border: 1px solid #cccccc;
  border-radius: 5px;
  min-height: 10rem;
  min-width: 40rem;
}

textarea::placeholder {
  text-align: center;
  font-size: 12px;
  color: #cccccc;
  padding-top: 4rem;
}

.noUi-horizontal {
  height: 8px;
}

.noUi-handle {
  border: 1px solid #d9d9d9;
  border-radius: 3px;
  background: #fff;
  cursor: default;
  box-shadow: inset 0 0 1px #fff;
}

.selectWidth {
  width: 15rem;
}

.selectWidth::placeholder {
  color: #cccccc;
  font-size: 10px;
}

.InputUiBox {
  padding: 2rem 0 0 0;
  width: 100px;
  height: 100px;
  cursor: pointer;
}

.switch {
  position: relative;
  display: inline-block;
  width: 90px;
  height: 34px;
}

.switch input {
  display: none;
}

.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #999;
  -webkit-transition: 0.4s;
  transition: 0.4s;
}

.slider:before {
  position: absolute;
  content: "";
  height: 26px;
  width: 26px;
  left: 4px;
  bottom: 4px;
  background-color: white;
  -webkit-transition: 0.4s;
  transition: 0.4s;
}

input:checked + .slider {
  background-color: #088dcd;
}

input:focus + .slider {
  box-shadow: 0 0 1px #2196f3;
}

input:checked + .slider:before {
  -webkit-transform: translateX(55px);
  -ms-transform: translateX(55px);
  transform: translateX(55px);
}

.on {
  display: none;
}

.on {
  color: white;
  position: absolute;
  transform: translate(-50%, -50%);
  top: 50%;
  left: 35%;
  font-size: 10px;
}

.off {
  color: white;
  position: absolute;
  transform: translate(-50%, -50%);
  top: 50%;
  left: 65%;
  font-size: 10px;
}

input:checked + .slider .on {
  display: block;
}

input:checked + .slider .off {
  display: none;
}

.slider.round {
  border-radius: 34px;
}

.slider.round:before {
  border-radius: 50%;
}

.accordion-button::after {
  position: absolute;
  left: 3rem;
}

label {
  margin-right: 2rem;
  font-size: 14px;
  color: #545454;
}

.form-check .form-check-input {
  float: right;
}

input {
  border: 1px solid #ccc;
  border-radius: 5px;
}

input::placeholder {
  color: #ccc;
  font-size: 12px;
}

@media screen and (max-width: 576px) {
  textarea {
    min-width: 20rem;
  }

  .accordianStyle {
    width: fit-content;
  }
}
</style>
