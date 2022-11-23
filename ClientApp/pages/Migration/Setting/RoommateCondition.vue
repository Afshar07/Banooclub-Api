<template>
  <div  class="container px-0 mcontainer">
    <div class="row">
      <form class="col-12 px-4 accordianStyle">
        <div class="col-md-12 my-4">
          <h5><strong>وضعیت هم خانه</strong></h5>
          <div class="d-flex align-items-center gap-2">
            <span>وضعیت فعلی : </span>

            <span
              v-if="$auth.user.userInfo.userSetting.activeRoomate"
              class="badge bg-success text-white"
            >فعال</span
            >

            <span v-else class="badge bg-danger text-white">غیر فعال</span>
          </div>
          <div class="d-flex align-items-center justify-content-between gap-2 my-2">
            <div class="d-flex align-items-center gap-2 ">
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
        </div>
        <div class="accordion" dir="rtl" id="accordionPanelsStayOpenExample">
          <div class="accordion-item">
            <h2 class="accordion-header" id="panelsStayOpen-headingOne">
              <button
                @click="ActiveAccordion=1"
                class="accordion-button   d-flex align-items-center justify-content-between w-100"
                type="button"
                data-bs-toggle="collapse"
                data-bs-target="#panelsStayOpen-collapseOne"
                aria-expanded="true"
                aria-controls="panelsStayOpen-collapseOne"
              >
                <small>
                  ویژگی های خانه
                </small>
<!--                <i class="fas fa-chevron-down text-white" :class="[{'textPrimary':ActiveAccordion=1}]"></i>-->
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
                        :value="1"
                        v-model="RoomMateInfo.roomType"
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
                        :value="2"
                        v-model="RoomMateInfo.roomType"
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
                        :value="3"
                        v-model="RoomMateInfo.roomType"
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
                        :value="4"
                        v-model="RoomMateInfo.roomType"
                      />
                      <label class="form-check-label" for="inlineRadio4"
                      >غیره</label
                      >
                    </div>
                  </div>
                  <div class="row my-2">
                    <div class=" col-12">
                      <div class="d-flex flex-column">
                        <div class="">
                          <div class="d-flex flex-row align-items-center gap-3">
                            <div class="labelText">تعداد اتاق خواب</div>
                            <div class="me-auto d-flex flex-row align-items-center">
                              <div
                                class="d-inline-flex"
                                @click="RoomMateInfo.bedroomCount++"
                              >
                                <font-awesome-icon
                                  icon="plus-square"
                                  size="lg"
                                />
                              </div>
                              <div class="d-inline-flex px-3">
                                {{ RoomMateInfo.bedroomCount }}
                              </div>
                              <div
                                class="d-inline-flex"
                                @click="RoomMateInfo.bedroomCount--"
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
                            <div class="me-auto d-flex flex-row align-items-center">
                              <div
                                class="d-inline-flex"
                                @click="RoomMateInfo.bathroomCount++"
                              >
                                <font-awesome-icon
                                  icon="plus-square"
                                  size="lg"
                                />
                              </div>
                              <div class="d-inline-flex px-3">
                                {{ RoomMateInfo.bathroomCount }}
                              </div>
                              <div
                                class="d-inline-flex"
                                @click="RoomMateInfo.bathroomCount--"
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
                    <div class=" col-12 my-2">
                      <div class="d-flex flex-column">
                        <div class="">
                          <div class="d-flex flex-row align-items-start gap-3">
                            <div class="labelText ">نوع اتاق خواب</div>
                            <div class="me-auto align-items-center">
                              <div
                                class="d-flex flex-row justify-content-between"
                              >
                                <div class="">
                                  <label class="switch">
                                    <input
                                      type="checkbox"
                                      id="togBtn1"
                                      v-model="RoomMateInfo.bedroomType"
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
                          <div class="d-flex flex-row align-items-start gap-3">
                            <div class="labelText ">نوع سرویس بهداشتی</div>
                            <div class="me-auto">
                              <div
                                class="d-flex flex-row justify-content-between"
                              >
                                <div class="">
                                  <label class="switch">
                                    <input
                                      type="checkbox"
                                      id="togBtn2"
                                      v-model="RoomMateInfo.bathroomType"
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
                        :value="1"
                        v-model="RoomMateInfo.ownerType"
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
                        :value="2"
                        v-model="RoomMateInfo.ownerType"
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
                    <div class=" col-12 w-auto">
                      <textarea class="form-control" v-model="RoomMateInfo.address"></textarea>
                    </div>
                  </div>

                  <div>
                    <div class="d-flex flex-column flex-md-row gap-3">
                      <div class="labelText">امکانات خانه :</div>
                      <div class="d-flex flex-column flex-md-row">
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="checkbox"
                            id="inlineCheckbox20"
                            :value="1"
                            v-model="RoomMateInfo.haveCCTV"
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
                            :value="1"
                            v-model="RoomMateInfo.haveElevator"
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
                            :value="1"
                            v-model="RoomMateInfo.haveParking"
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
                            :value="1"
                            v-model="RoomMateInfo.haveLobbyMan"
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
                            :value="1"
                            v-model="RoomMateInfo.haveFurniture"
                          />
                          <label class="form-check-label" for="inlineCheckbox22"
                          >فرنیش</label
                          >
                        </div>
                      </div>
                    </div>

                  </div>

                  <div class="d-flex flex-column flex-md-row gap-3 my-3">
                    <div class="labelText">ارسال عکس :</div>

                    <div
                      class="border rounded text-center justify-content-center align-items-center InputUiBox"
                      @click="callInputMethod"
                    >
                      <input
                        type="file"
                        ref="RoomateFile"
                        accept="image/*"
                        multiple="multiple"
                        class="form-control-file my-file d-none"
                        id="my-file"
                        @change="onFileChange"
                      />
                      <font-awesome-icon icon="plus-square" size="lg" />
                    </div>

                    <div  class="d-flex  align-items-center gap-2" id="preview " v-if="BaseImgUrls.length>0">
                      <div     v-for="(item,idx) in BaseImgUrls" class="position-relative">
                        <i class="fas fa-trash text-danger position-absolute" @click="RemovePic(idx)"></i>
                        <img
                          width="70px"
                          height="70px"

                          :src="item"
                          :key="item"
                        />
                      </div>

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
                <div class="d-flex flex-column ">
                  <div>
                    <div class="d-flex flex-column align-items-center flex-md-row gap-3">
                      <div class="labelText">اجاره روزانه :</div>
                      <div class="d-flex align-items-center gap-2">
                        <input
                          type="text"
                          class="form-control"
                          v-model.trim="formattedDailyRent"
                        />
                        <div class="labelText text-nowrap small">
                           تومان
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="my-3">
                    <div class="d-flex flex-column align-items-center flex-md-row gap-3">
                      <div class="labelText">مبلغ رهن :</div>
                      <div class="d-flex align-items-center gap-2">
                        <input
                          type="text"
                          v-model.trim=" formattedMortgage"
                          class="form-control"
                        />
                        <div class="labelText text-nowrap"> تومان</div>
                      </div>
                    </div>
                  </div>
                  <div>
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
                          v-model="RoomMateInfo.receptionDate"
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
                            :value="1"
                            v-model="RoomMateInfo.withWater"
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
                            :value="1"
                            v-model="RoomMateInfo.withElectricity"
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
                            :value="1"
                            v-model="RoomMateInfo.withGaz"
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
                            :value="1"
                            v-model="RoomMateInfo.withInternet"
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
                            :value="1"
                            v-model="RoomMateInfo.withGarbage"
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

                      <LazySetLocation  @getGeoLocation="SetLocation"  :defaultMarkerGeoLoc="[RoomMateInfo.latitude,RoomMateInfo.longitude]"  :defaultGeoLoc="[RoomMateInfo.latitude,RoomMateInfo.longitude]" ></LazySetLocation>

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
                            type="radio"
                            id="inlineCheckbox6"
                            :value="1"
                            v-model="RoomMateInfo.roomateGender"
                          />
                          <label class="form-check-label" for="inlineCheckbox6"
                          >فرقی نمی کند</label
                          >
                        </div>
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="radio"
                            id="inlineCheckbox7"
                            :value="2"
                            v-model="RoomMateInfo.roomateGender"
                          />
                          <label class="form-check-label" for="inlineCheckbox7"
                          >مرد</label
                          >
                        </div>
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="radio"
                            id="inlineCheckbox8"
                            :value="3"
                            v-model="RoomMateInfo.roomateGender"
                          />
                          <label class="form-check-label" for="inlineCheckbox8"
                          >زن</label
                          >
                        </div>
                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="radio"
                            id="inlineCheckbox9"
                            :value="4"
                            v-model="RoomMateInfo.roomateGender"
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
                      <small>محدوده سنی هم خانه</small>
                      <div class="d-flex my-2 align-items-center gap-1">
                          <input type="number" class="form-control-sm form-control" v-model="RoomMateInfo.roomateAgeRangeFrom" />
                          <small>تا</small>
                          <input type="number" class="form-control-sm form-control" v-model="RoomMateInfo.roomateAgeRangeTo" />



                      </div>
                    </div>
                  </div>
                  <div>
                    <div class="row">
                      <div class="col-md-4 col-lg-4 col-12 my-md-0 my-2">
                        <label  class="m-0 mb-2">سیگار </label>
                        <v-select
                          :options="options"
                          dir="rtl"
                          label="title"
                          placeholder="سیگار"
                          class="selectWidth"
                          v-model="RoomMateInfo.roomateSmoke"
                          :reduce="(title) => title.code"
                        ></v-select>
                      </div>
                      <div class="col-md-4 col-lg-4 col-12 my-md-0 my-2">
                        <label  class="m-0 mb-2">همراه(فرزند) </label>
                        <v-select
                          :options="options"
                          dir="rtl"
                          label="title"
                          placeholder="فرزند"
                          class="selectWidth"
                          v-model="RoomMateInfo.roomateChild"
                          :reduce="(title) => title.code"
                        ></v-select>
                      </div>
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
                              :value="1"
                              v-model="RoomMateInfo.residenceNumber"
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
                              :value="2"
                              v-model="RoomMateInfo.residenceNumber"
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
                              :value="3"
                              v-model="RoomMateInfo.residenceNumber"
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

                  <div >
                    <div class="d-flex flex-column flex-md-row">
                      <div class="labelText">جنسیت من :</div>
                      <div class="d-flex flex-column flex-md-row gap-2">

                        <div class="form-check form-check-inline">
                          <input
                            class="form-check-input"
                            type="radio"
                            name="inlineRadioOptions20"
                            id="inlineRadio19"
                            :value="2"
                            v-model="RoomMateInfo.ownerGender"
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
                            :value="3"
                            v-model="RoomMateInfo.ownerGender"
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
                            :value="4"
                            v-model="RoomMateInfo.ownerGender"
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
                          v-model.trim="RoomMateInfo.ownerAge"
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
                          v-model="RoomMateInfo.ownerLangs"
                          :reduce="(title) => title.code"
                        ></v-select>
                      </div>
                    </div>

                  </div>
                  <div class="labelText my-4">توضیحات :</div>
                  <div class="row">
                    <textarea
                      placeholder="توضیحات درباره من یا هم خانه مورد نظر یا مکان زندگی"
                      v-model="RoomMateInfo.description"
                    />
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-12 my-3 px-0">
            <button
              type="button"
              class="btn btn-sm AddReplyBtn"
              @click="saveRoommateInformation"
            >
              ذخیره اطلاعات هم خانه
            </button>
          </div>

        </div>

      </form>
    </div>
  </div>
</template>

<script>

export default {
  name: "RoommateCondition",
  layout: "PoshtebamPlusLayout",
  head() {
    return {

      title: 'افزودن هم خانه',
      meta: [
        {
          hid: "description",
          name: "description",
          content:'افزودن هم خانه',
        },
      ],
    };
  },
  data() {
    return {
      longitude:'',
      latitude:'',
      optionsHome: [
        { title: "آسانسور", code: 1 },
        { title: "دوربین مدار بسته", code: 2 },
        { title: "پارکینگ", code: 3 },
      ],
      optionsOwnerLangs: [
        { title: "انگلیسی", code: '1' },
        { title: "فرانسوی", code: '2'},
        { title: "عربی", code: '3' },
        { title: "فارسی", code: '4' },
        { title: "آلمانی", code: '5' },
        { title: "ترکی", code: '6' },
      ],
      options: [
        { title: "بله", code: 1 },
        { title: "خیر", code: 2 },
        { title: "فرقی نمی کند", code: 3 },
      ],
      ActiveAccordion:0,
      RoomMateInfo:{
        roomateId:0,
        roomType:0,
        bedroomCount:0,
        bedroomType:0 ,
        bathroomCount:0,
        bathroomType:0,
        ownerType:0,
        address:'',
        longitude:0,
        latitude:0,
        haveLobbyMan:0,
        haveFurniture:0,
        haveCCTV:0,
        haveElevator:0,
        dailyRent:0,
        mortgage:0,
        receptionDate:'',
        withElectricity:0,
        withWater:0,
        withGaz:0,
        withInternet:0,
        withGarbage:0,
        roomateGender:'',
        roomateAgeRangeFrom:0,
        roomateAgeRangeTo:0,
        roomateSmoke:0,
        roomateChild:0,
        residenceNumber:0,
        ownerGender:0,
        ownerAge:0,
        ownerLangs:'',
        description:'',
        userId:this.$auth.user.userInfo.userId,
        paymentId:null,
        haveParking:0,
        photos:[],
        isPrivate:false
      },
      IsActive:false,
      BaseImgUrls:[],
      center:[30,50],
      images:[],
      latLang:[]
    };
  },

  methods: {
    SetLocation(lat,lang){
      this.RoomMateInfo.latitude = lat
      this.RoomMateInfo.longitude = lang
    },
    RemovePic(idx){
      this.BaseImgUrls.splice(idx,1)
      this.images.splice(idx,1)
    },

    callInputMethod() {
      this.$refs.RoomateFile.click();

    },
    onFileChange(e) {

      const that = this;
      const f = [];
      Array.prototype.forEach.call(this.$refs.RoomateFile.files, (element) => {
        f.push(element);
      });
      f.forEach((element) => {
        this.BaseImgUrls.push(URL.createObjectURL(element));
        const reader = new FileReader();
        reader.onload = (function (theFile) {
          return function () {
            const binaryData = reader.result;
            that.images.push(window.btoa(binaryData));
          };
        })(f);
        reader.readAsBinaryString(element);
      });
    },
    saveRoommateInformation() {

      let tmpPhotos = []
      let tmpPhoto = {
        base64:'',
        priority:1
      }
      this.images.forEach((el)=>{
        tmpPhoto.base64 = el
        const clone = {...tmpPhoto}
        tmpPhotos.push(clone)
        tmpPhoto.base64 = ''
      })
      let bedType =0
      let bathType = 0
      if(this.RoomMateInfo.bedroomType){
        bedType = 1
      }
      if(this.RoomMateInfo.bathroomType){
        bathType = 1
      }
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      });
      this.$axios
        .post(
          `Roomate/Update`,
          {
            roomateId: this.RoomMateInfo.roomateId,
            roomType: this.RoomMateInfo.roomType,
            bedroomCount: this.RoomMateInfo.bedroomCount,
            bedroomType: bedType ,
            bathroomCount: this.RoomMateInfo.bathroomCount,
            bathroomType: bathType ,
            ownerType: this.RoomMateInfo.ownerType,
            address: this.RoomMateInfo.address,
            longitude: this.RoomMateInfo.longitude,
            latitude: this.RoomMateInfo.latitude,
            haveLobbyMan: this.RoomMateInfo.haveLobbyMan?1:0 ,
            haveParking: this.RoomMateInfo.haveParking?1:0 ,
            haveFurniture: this.RoomMateInfo.haveFurniture?1:0,
            haveCCTV: this.RoomMateInfo.haveCCTV?1:0,
            haveElevator: this.RoomMateInfo.haveElevator?1:0,
            dailyRent: parseInt(this.RoomMateInfo.dailyRent.replaceAll(',','')) ,
            mortgage: parseInt(this.RoomMateInfo.mortgage.replaceAll(',','')) ,
            receptionDate: this.RoomMateInfo.receptionDate,
            rentPeriod: this.RoomMateInfo.rentPeriod,
            withElectricity: this.RoomMateInfo.withElectricity?1:0,
            withWater: this.RoomMateInfo.withWater?1:0,
            withGaz: this.RoomMateInfo.withGaz?1:0,
            withInternet: this.RoomMateInfo.withInternet?1:0,
            withGarbage:this.RoomMateInfo.withGarbage?1:0,
            roomateGender: this.RoomMateInfo.roomateGender,
            roomateAgeRangeFrom:this.RoomMateInfo.roomateAgeRangeFrom,
            roomateAgeRangeTo: this.RoomMateInfo.roomateAgeRangeTo,
            roomateSmoke:this.RoomMateInfo.roomateSmoke,
            roomateChild: this.RoomMateInfo.roomateChild,
            residenceNumber: this.RoomMateInfo.residenceNumber,
            ownerGender: this.RoomMateInfo.ownerGender,
            ownerAge:this.RoomMateInfo.ownerAge,
            ownerLangs: this.RoomMateInfo.ownerLangs.toString(),
            description:  this.RoomMateInfo.description,
            userId: this.$auth.user.userInfo.userId,
            paymentId: this.RoomMateInfo.paymentId,
            photos: tmpPhotos,
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
        .then(async (res) => {
          // if (res.data === 3) {
          //   this.$toast.error(
          //     "پاسپورت و کارت ملی شما توسط ادمین رد شده است و به همین دلیل امکان تغییر وضعیت هم خانه وجود ندارد"
          //   );
          // } else if (res.data === 2) {
          //   this.$toast.error(
          //     "پاسپورت و کارت ملی شما هنوز توسط ادمین تایید نشده است !"
          //   );
          // } else if (res.data === 4) {
          //   this.$toast.error(
          //     "شما هنوز پاسپورت و کارت ملی خود را آپلود نکرده اید."
          //   );
          // }
         await this.$auth.fetchUser()
          this.$toast.success('وضعیت هم خانه با موفقیت تغییر یافت')
        });
    },

    getRoomateInfo() {
      this.$axios
        .post(`Roomate/GetMyRoomate`, null, {})
        .then((response) => {
          if (response.data.status !== 2) {
            this.RoomMateInfo.roomateId = response.data.roomate.roomateId,
              this.RoomMateInfo.roomType = response.data.roomate.roomType
              this.RoomMateInfo.bedroomCount = response.data.roomate.bedroomCount
              this.RoomMateInfo.bedroomType = response.data.roomate.bedroomType
              this.RoomMateInfo.bathroomCount = response.data.roomate.bathroomCount
              this.RoomMateInfo.bathroomType = response.data.roomate.bathroomType
              this.RoomMateInfo.ownerType= response.data.roomate.ownerType
              this.RoomMateInfo.address = response.data.roomate.address
              this.RoomMateInfo.longitude = response.data.roomate.longitude
              this.RoomMateInfo.latitude = response.data.roomate.latitude
            this.latLang = [this.RoomMateInfo.latitude,this.RoomMateInfo.longtitude]
              this.RoomMateInfo.haveLobbyMan = response.data.roomate.haveLobbyMan
              this.RoomMateInfo.haveFurniture =  response.data.roomate.haveFurniture
              this.RoomMateInfo.haveCCTV =  response.data.roomate.haveCCTV
              this.RoomMateInfo.haveElevator =  response.data.roomate.haveElevator
              this.RoomMateInfo.dailyRent = response.data.roomate.dailyRent
              this.RoomMateInfo.mortgage = response.data.roomate.mortgage
              this.RoomMateInfo.receptionDate = response.data.roomate.receptionDate
              this.RoomMateInfo.withElectricity = response.data.roomate.withElectricity
              this.RoomMateInfo.withWater = response.data.roomate.withWater
              this.RoomMateInfo.withGaz = response.data.roomate.withGaz
              this.RoomMateInfo.withInternet = response.data.roomate.withInternet
              this.RoomMateInfo.withGarbage = response.data.roomate.withGarbage
              this.RoomMateInfo.roomateGender = response.data.roomate.roomateGender
              this.RoomMateInfo.roomateAgeRangeFrom = response.data.roomate.roomateAgeRangeFrom
              this.RoomMateInfo.roomateAgeRangeTo  = response.data.roomate.roomateAgeRangeTo
              this.RoomMateInfo.roomateSmoke  = response.data.roomate.roomateSmoke
              this.RoomMateInfo.roomateChild  = response.data.roomate.roomateChild
              this.RoomMateInfo.residenceNumber = response.data.roomate.residenceNumber
              this.RoomMateInfo.ownerGender = response.data.roomate.ownerGender
              this.RoomMateInfo.ownerAge = response.data.roomate.ownerAge
              this.RoomMateInfo.ownerLangs = response.data.roomate.ownerLangs
              this.RoomMateInfo.description = response.data.roomate.description
              this.RoomMateInfo.userId = response.data.roomate.userId
              this.RoomMateInfo.paymentId = response.data.roomate.paymentId
              this.RoomMateInfo.haveParking  = response.data.roomate.haveParking
              this.RoomMateInfo.photos  = response.data.roomate.photos
              this.RoomMateInfo.isPrivate = response.data.roomate.isPrivate
            console.log(this.RoomMateInfo)
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },

    // deStructLanuage(data) {
    //   const unrepeated = (str) => [...new Set(str)].join("");
    //   data = unrepeated(data);
    //   var words = data.split(" ");
    //   var arr = [];
    //
    //   for (var i = 0; i < words.length; i++) {
    //     arr.push(words[i]);
    //   }
    //   return arr;
    // },
  },
  mounted() {
    this.getRoomateInfo();
  },
  computed: {
    formattedDailyRent: {
      get: function () {
        return this.RoomMateInfo.dailyRent;
      },
      set: function (newValue) {
        // This setter is getting number, replace all commas with empty str
        // Then start to separate numbers with ',' from beginning to prevent
        // from data corruption
        if (newValue) {
          this.RoomMateInfo.dailyRent = newValue
            .toString()
            .replaceAll(",", "")
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
          // Remove all characters that are NOT number
          this.RoomMateInfo.dailyRent = this.RoomMateInfo.dailyRent.replace(
            /[a-zA-Z+*!@#$%^&*()_;:'"|<>/?{}\u0600-\u06EC/\-/\.]/g,
            ""
          );
        } else if (!newValue || this.newValue === "") {
          this.RoomMateInfo.dailyRent = null;
        }
      },
    },
    formattedMortgage: {
      get: function () {
        return this.RoomMateInfo.mortgage;
      },
      set: function (newValue) {
        // This setter is getting number, replace all commas with empty str
        // Then start to separate numbers with ',' from beginning to prevent
        // from data corruption
        if (newValue) {
          this.RoomMateInfo.mortgage = newValue
            .toString()
            .replaceAll(",", "")
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
          // Remove all characters that are NOT number
          this.RoomMateInfo.mortgage = this.RoomMateInfo.mortgage.replace(
            /[a-zA-Z+*!@#$%^&*()_;:'"|<>/?{}\u0600-\u06EC/\-/\.]/g,
            ""
          );
        } else if (!newValue || this.newValue === "") {
          this.RoomMateInfo.mortgage = null;
        }
      },
    },

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


.textPrimary{
  color: #00adef!important;
}
</style>
