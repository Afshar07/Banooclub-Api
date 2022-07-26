<template>
  <div class="container-fluid px-0">
    <div class="accordion" dir="rtl" id="accordionPanelsStayOpenExample">
      <div class="accordion-item">
        <h2 class="accordion-header" id="panelsStayOpen-headingOne">
          <button class="accordion-button" type="button" data-bs-toggle="collapse"
                  data-bs-target="#panelsStayOpen-collapseOne" aria-expanded="true"
                  aria-controls="panelsStayOpen-collapseOne">
            فیلتر هم خانه
          </button>
        </h2>
        <div id="panelsStayOpen-collapseOne" class="accordion-collapse collapse show"
             aria-labelledby="panelsStayOpen-headingOne">
          <div class="accordion-body">
            <div class="d-flex flex-column">
              <div class="my-2">
                <v-select
                  :options="country"
                  label="title"
                  placeholder="کشور" dir="rtl"
                  :reduce="title => title.code"
                ></v-select>
              </div>
              <div class="my-2">
                <v-select
                  :options="country"
                  label="title"
                  placeholder="شهر" dir="rtl"
                  :reduce="title => title.code"
                ></v-select>
              </div>
              <div class="border-bottom my-3 pb-1">
                نوع محل زندگی
              </div>
              <div class="">
                <div class="form-check form-check-inline">
                  <label class="form-check-label" for="inlineCheckbox1">آپارتمان</label>
                  <input class="form-check-input" type="checkbox" id="inlineCheckbox1" value="1" v-model="RoomType1" >
                </div>
                <div class="form-check form-check-inline">
                  <input class="form-check-input" type="checkbox" id="inlineCheckbox2" value="2" v-model="RoomType2" >
                  <label class="form-check-label" for="inlineCheckbox2">خانه</label>
                </div>
              </div>
              <div class="">
                <div class="form-check form-check-inline">
                  <input class="form-check-input" type="checkbox" id="inlineCheckbox3" value="3" v-model="RoomType3" >
                  <label class="form-check-label" for="inlineCheckbox3">سوییت</label>
                </div>
                <div class="form-check form-check-inline">
                  <input class="form-check-input" type="checkbox" id="inlineCheckbox4" value="4" v-model="RoomType4" >
                  <label class="form-check-label" for="inlineCheckbox4">اجاره</label>
                </div>
              </div>
              <div class="border-bottom my-3 pb-1">
                هزینه
              </div>
              <div>
                <div class="form-check">
                  <input class="form-check-input" type="checkbox" value="" id="flexCheckIndeterminate">
                  <label class="form-check-label" for="flexCheckIndeterminate">
                    آیا رهن داشته باشد
                  </label>
                </div>
              </div>

              <div class="border-bottom my-3 pb-1">تاریخ پذیرش</div>
              <div>
                <div class="d-flex flex-row mb-3">
                  <div class="">
                    <input
                      type="text"
                      class="custom-input"
                      placeholder="انتخاب تاریخ ..."
                    />
                    <date-picker
                      auto-submit
                      v-model="ReceptionDate"
                      format="YYYY-MM-DD"
                      display-format="jYYYY-jMM-jDD"
                      custom-input=".custom-input"
                    />
                  </div>
                  <div class=" mx-2">به بعد</div>
                </div>
              </div>
              <div>
                <div class="form-check">
                  <input class="form-check-input" type="checkbox" value="" id="flexCheckIndeterminate1">
                  <label class="form-check-label" for="flexCheckIndeterminate1">
                    آگهی فوری
                  </label>
                </div>
              </div>
              <div>
                <div class="form-check">
                  <input class="form-check-input" type="checkbox" value="" id="flexCheckIndeterminate2"
                         v-model="BedroomType">
                  <label class="form-check-label" for="flexCheckIndeterminate2">
                    اتاق خواب خصوصی باشد
                  </label>
                </div>
              </div>
              <div>
                <div class="form-check">
                  <input class="form-check-input" type="checkbox" value="" id="flexCheckIndeterminate3"
                         v-model="BathroomType">
                  <label class="form-check-label" for="flexCheckIndeterminate3">
                    سرویس بهداشتی خصوصی باشد
                  </label>
                </div>
              </div>
              <div class="border-bottom mb-3 pb-1">
                <div class="form-check">
                  <input class="form-check-input" type="checkbox" value="" id="flexCheckIndeterminate4">
                  <label class="form-check-label" for="flexCheckIndeterminate4">
                    آگهی فقط عکس دار
                  </label>
                </div>
              </div>
              <div class="my-3">آیا شما سیگار می کشید؟</div>
              <div class="">
                <div class="form-check form-check-inline">
                  <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio1"
                         value="1" v-model="RoomateSmoke">
                  <label class="form-check-label" for="inlineRadio1">بله</label>
                </div>
                <div class="form-check form-check-inline">
                  <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio2"
                         value="2" v-model="RoomateSmoke">
                  <label class="form-check-label" for="inlineRadio2">خیر</label>
                </div>
                <div class="form-check form-check-inline">
                  <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio3"
                         value="3" v-model="RoomateSmoke">
                  <label class="form-check-label" for="inlineRadio3">فقط در فضای باز</label>
                </div>
              </div>
              <div class="my-3">آیا شما فرزند دارید؟</div>
              <div class="border-bottom pb-1">
                <div class="form-check form-check-inline">
                  <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio4"
                         value="1" v-model="RoomateChild">
                  <label class="form-check-label" for="inlineRadio4">بله</label>
                </div>
                <div class="form-check form-check-inline">
                  <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio5"
                         value="2" v-model="RoomateChild">
                  <label class="form-check-label" for="inlineRadio5">خیر</label>
                </div>
              </div>
              <div class="my-3">آیا شما حیوان خانگی دارید؟</div>
              <div class="border-bottom pb-1">
                <div class="form-check form-check-inline">
                  <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio6"
                         value="1" v-model="RoomatePet">
                  <label class="form-check-label" for="inlineRadio6">بله</label>
                </div>
                <div class="form-check form-check-inline">
                  <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio7"
                         value="2" v-model="RoomatePet">
                  <label class="form-check-label" for="inlineRadio7">خیر</label>
                </div>
              </div>
              <div class="my-3">جنسیت مالک</div>
              <div class="border-bottom pb-1">
                <div class="form-check form-check-inline">
                  <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio8"
                         value="1" v-model="RoomateGender">
                  <label class="form-check-label" for="inlineRadio8">مرد</label>
                </div>
                <div class="form-check form-check-inline">
                  <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio9"
                         value="2" v-model="RoomateGender">
                  <label class="form-check-label" for="inlineRadio9">زن</label>
                </div>
                <div class="form-check form-check-inline">
                  <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio10"
                         value="0" v-model="RoomateGender">
                  <label class="form-check-label" for="inlineRadio10">سایر</label>
                </div>
              </div>
              <div class="my-3">تعداد نفرات ساکن در محل</div>
              <v-select
                :options="[1,2,3,4,5,6,7,8,9]"
                dir="rtl"
                v-model="ResidenceNumber"
              ></v-select>
            </div>
          </div>
        </div>
      </div>
      <!--      <div class="accordion-item">-->
      <!--        <h2 class="accordion-header" id="panelsStayOpen-headingTwo">-->
      <!--          <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"-->
      <!--                  data-bs-target="#panelsStayOpen-collapseTwo" aria-expanded="false"-->
      <!--                  aria-controls="panelsStayOpen-collapseTwo">-->
      <!--            هم خانه-->
      <!--          </button>-->
      <!--        </h2>-->
      <!--        <div id="panelsStayOpen-collapseTwo" class="accordion-collapse collapse"-->
      <!--             aria-labelledby="panelsStayOpen-headingTwo">-->
      <!--          <div class="accordion-body">-->
      <!--            <div class="d-flex flex-column">-->
      <!--              <div class="">-->
      <!--                <input type="checkbox"/>-->
      <!--                <label>عنوان فیلتر </label>-->
      <!--              </div>-->
      <!--              <div class="">-->
      <!--                <input type="checkbox"/>-->
      <!--                <label>عنوان فیلتر </label>-->
      <!--              </div>-->
      <!--              <div class="">-->
      <!--                <input type="checkbox"/>-->
      <!--                <label>عنوان فیلتر </label>-->
      <!--              </div>-->
      <!--            </div>-->
      <!--          </div>-->
      <!--        </div>-->
      <!--      </div>-->
    </div>
  </div>
</template>

<script>



export default {
  name: "RoommateSidebar",
  data() {
    return {
      country: [
        {title: "آمریکا" , code:1},
        {title: "هلند" , code:2},
        {title: "انگلیس" , code:3},
        {title: "فرانسه" , code:4},
        {title: "آلمان" , code:5},
        {title: "کانادا" , code:6}
      ],
      ReceptionDate: '',
      RoomType: null,
      RoomType1: null,
      RoomType2: null,
      RoomType3: null,
      RoomType4: null,
      BedroomType: null,
      BathroomType: null,
      RoomateGender: null,
      RoomateAgeRangeFrom: null,
      RoomateAgeRangeTo: null,
      RoomateSmoke: null,
      RoomateChild: null,
      RoomatePet: null,
      ResidenceNumber: null,
      MortgageMin: 0,
      MortgageMax: 0,
    }
  },
  methods: {
    getSliderValue(value) {
      this.MortgageMin = value.positions[0];
      this.MortgageMax = value.positions[1];
    },
  }
}
</script>

<style scoped>
.accordion-button::after {
  position: absolute;
  left: 8px;
}

label {
  margin-right: 2rem;
  font-size: 14px;
  color: #545454;
}

.form-check .form-check-input {
  float: right;
}

.noUi-horizontal {
  height: 8px;
}

.noUi-handle {
  border: 1px solid #D9D9D9;
  border-radius: 3px;
  background: #FFF;
  cursor: default;
  box-shadow: inset 0 0 1px #FFF;
}

.custom-input {
  max-width: 7rem;
  border: 1px solid #ccc;
  border-radius: 5px;
}

.custom-input::placeholder {
  color: #ccc;
  font-size: 12px;
}
</style>
