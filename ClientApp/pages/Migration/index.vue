<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-10 col-12 order-1">
        <div class="row">
          <div class="col-12 my-3 text-start">
            <input
              class="border roommateSearchBox p-2"
              type="text"
              placeholder="جستجو..."
              v-model.trim="userTextSearch"
            />
          </div>
          <div
            class="col-lg-3 col-12"
            v-for="(item, index) in allUser"
            :key="index"
          >
            <div class="p-0 m-0" @click="seeUserProfile(item.userId)">
              <RoommateCard :user-data="item" />
            </div>
          </div>
        </div>
      </div>
      <div class="col-2 bg-white order-0 d-md-block d-none">
        <div class="container-fluid px-0">
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
                  فیلتر هم خانه
                </button>
              </h2>
              <div
                id="panelsStayOpen-collapseOne"
                class="accordion-collapse collapse show"
                aria-labelledby="panelsStayOpen-headingOne"
              >
                <div class="accordion-body">
                  <div class="d-flex flex-column">
                    <div class="my-2">
                      <v-select
                        :options="country"
                        label="title"
                        placeholder="کشور"
                        dir="rtl"
                        :reduce="(title) => title.code"
                      ></v-select>
                    </div>
                    <div class="my-2">
                      <v-select
                        :options="country"
                        label="title"
                        placeholder="شهر"
                        dir="rtl"
                        :reduce="(title) => title.code"
                      ></v-select>
                    </div>
                    <div class="border-bottom my-3 pb-1">نوع محل زندگی</div>
                    <div class="">
                      <div class="form-check form-check-inline">
                        <label class="form-check-label" for="inlineCheckbox1"
                          >آپارتمان</label
                        >
                        <input
                          class="form-check-input"
                          type="checkbox"
                          id="inlineCheckbox1"
                          value="1"
                          v-model="RoomType1"
                        />
                      </div>
                      <div class="form-check form-check-inline">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          id="inlineCheckbox2"
                          value="2"
                          v-model="RoomType2"
                        />
                        <label class="form-check-label" for="inlineCheckbox2"
                          >خانه</label
                        >
                      </div>
                    </div>
                    <div class="">
                      <div class="form-check form-check-inline">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          id="inlineCheckbox3"
                          value="3"
                          v-model="RoomType3"
                        />
                        <label class="form-check-label" for="inlineCheckbox3"
                          >سوییت</label
                        >
                      </div>
                      <div class="form-check form-check-inline">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          id="inlineCheckbox4"
                          value="4"
                          v-model="RoomType4"
                        />
                        <label class="form-check-label" for="inlineCheckbox4"
                          >اجاره</label
                        >
                      </div>
                    </div>
                    <div class="border-bottom my-3 pb-1">هزینه</div>
                    <div>
                      <div class="form-check">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          value=""
                          id="flexCheckIndeterminate"
                        />
                        <label
                          class="form-check-label"
                          for="flexCheckIndeterminate"
                        >
                          آیا رهن داشته باشد
                        </label>
                      </div>
                    </div>
                    <div class="my-5">
                      <veeno
                        @end="getSliderValue"
                        connect
                        :tooltips="[true, true]"
                        :handles="[250, 500]"
                        :rtl="true"
                        :range="{
                          min: 250,
                          max: 500,
                        }"
                      />
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
                        <div class="mx-2">به بعد</div>
                      </div>
                    </div>
                    <div>
                      <div class="form-check">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          value=""
                          id="flexCheckIndeterminate1"
                        />
                        <label
                          class="form-check-label"
                          for="flexCheckIndeterminate1"
                        >
                          آگهی فوری
                        </label>
                      </div>
                    </div>
                    <div>
                      <div class="form-check">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          value=""
                          id="flexCheckIndeterminate2"
                          v-model="BedroomType"
                        />
                        <label
                          class="form-check-label"
                          for="flexCheckIndeterminate2"
                        >
                          اتاق خواب خصوصی باشد
                        </label>
                      </div>
                    </div>
                    <div>
                      <div class="form-check">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          value=""
                          id="flexCheckIndeterminate3"
                          v-model="BathroomType"
                        />
                        <label
                          class="form-check-label"
                          for="flexCheckIndeterminate3"
                        >
                          سرویس بهداشتی خصوصی باشد
                        </label>
                      </div>
                    </div>
                    <div class="border-bottom mb-3 pb-1">
                      <div class="form-check">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          value=""
                          id="flexCheckIndeterminate4"
                        />
                        <label
                          class="form-check-label"
                          for="flexCheckIndeterminate4"
                        >
                          آگهی فقط عکس دار
                        </label>
                      </div>
                    </div>
                    <div class="my-3">آیا شما سیگار می کشید؟</div>
                    <div class="">
                      <div class="form-check form-check-inline">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          name="inlineRadioOptions"
                          id="inlineRadio1"
                          value="1"
                          v-model="RoomateSmokeHasCheck"
                          @input="RoommateSmokeType(1)"
                        />
                        <label class="form-check-label" for="inlineRadio1"
                          >بله</label
                        >
                      </div>
                      <div class="form-check form-check-inline">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          name="inlineRadioOptions"
                          id="inlineRadio2"
                          value="2"
                          v-model="RoomateSmokeNotCheck"
                          @input="RoommateSmokeType(2)"
                        />
                        <label class="form-check-label" for="inlineRadio2"
                          >خیر</label
                        >
                      </div>
                      <div class="form-check form-check-inline">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          name="inlineRadioOptions"
                          id="inlineRadio3"
                          value="3"
                          v-model="RoomateSmokeOther"
                          @input="RoommateSmokeType(3)"
                        />
                        <label class="form-check-label" for="inlineRadio3"
                          >فقط در فضای باز</label
                        >
                      </div>
                    </div>
                    <div class="my-3">آیا شما فرزند دارید؟</div>
                    <div class="border-bottom pb-1">
                      <div class="form-check form-check-inline">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          name="inlineRadioOptions"
                          id="inlineRadio4"
                          value="1"
                          v-model="RoomateChildHasCheck"
                          @input="RoommateChildType(1)"
                        />
                        <label class="form-check-label" for="inlineRadio4"
                          >بله</label
                        >
                      </div>
                      <div class="form-check form-check-inline">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          name="inlineRadioOptions"
                          id="inlineRadio5"
                          value="2"
                          v-model="RoomateChildNotCheck"
                          @input="RoommateChildType(2)"
                        />
                        <label class="form-check-label" for="inlineRadio5"
                          >خیر</label
                        >
                      </div>
                    </div>
                    <div class="my-3">آیا شما حیوان خانگی دارید؟</div>
                    <div class="border-bottom pb-1">
                      <div class="form-check form-check-inline">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          name="inlineRadioOptions"
                          id="inlineRadio6"
                          value="1"
                          v-model="RoomatePetHasCheck"
                          @input="RoommatePetType(1)"
                        />
                        <label class="form-check-label" for="inlineRadio6"
                          >بله</label
                        >
                      </div>
                      <div class="form-check form-check-inline">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          name="inlineRadioOptions"
                          id="inlineRadio7"
                          value="2"
                          v-model="RoomatePetNotCheck"
                          @input="RoommatePetType(2)"
                        />
                        <label class="form-check-label" for="inlineRadio7"
                          >خیر</label
                        >
                      </div>
                    </div>
                    <div class="my-3">جنسیت مالک</div>
                    <div>
                      <div class="form-check form-check-inline">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          id="inlineCheckbox6"
                          value="1"
                          v-model="RoomateGenderNot"
                          @input="roomateGenderValue('1')"
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
                          @input="roomateGenderValue('2')"
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
                          @input="roomateGenderValue('3')"
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
                          value="0"
                          v-model="RoomateGenderOther"
                          @input="roomateGenderValue('4')"
                        />
                        <label class="form-check-label" for="inlineCheckbox9"
                          >سایر</label
                        >
                      </div>
                      <div class="form-check form-check-inline">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          id="inlineCheckbox10"
                          value="0"
                          v-model="RoomateGenderOther1"
                          @input="roomateGenderValue('5')"
                        />
                        <label class="form-check-label" for="inlineCheckbox10"
                          >دگرجنس‌گرای غالب/همجنس‌گرای نادر</label
                        >
                      </div>
                      <div class="form-check form-check-inline">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          id="inlineCheckbox11"
                          value="0"
                          v-model="RoomateGenderOther2"
                          @input="roomateGenderValue('6')"
                        />
                        <label class="form-check-label" for="inlineCheckbox11"
                          >دگرجنس‌گرای غالب و گاهی همجنس‌گرا</label
                        >
                      </div>
                      <div class="form-check form-check-inline">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          id="inlineCheckbox12"
                          value="0"
                          v-model="RoomateGenderOther3"
                          @input="roomateGenderValue('7')"
                        />
                        <label class="form-check-label" for="inlineCheckbox12"
                          >به‌طور مساوی دگرجنس‌گرا و همجنس‌گرا</label
                        >
                      </div>
                      <div class="form-check form-check-inline">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          id="inlineCheckbox13"
                          value="0"
                          v-model="RoomateGenderOther4"
                          @input="roomateGenderValue('8')"
                        />
                        <label class="form-check-label" for="inlineCheckbox13"
                          >همجنس‌گرای غالب و گاهی دگرجنس‌گرا</label
                        >
                      </div>
                      <div class="form-check form-check-inline">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          id="inlineCheckbox14"
                          value="0"
                          v-model="RoomateGenderOther5"
                          @input="roomateGenderValue('9')"
                        />
                        <label class="form-check-label" for="inlineCheckbox14"
                          >همجنس‌گرای غالب/دگرجنس‌گرای نادر</label
                        >
                      </div>
                    </div>
                    <div class="my-3">تعداد نفرات ساکن در محل</div>
                    <v-select
                      :options="[1, 2, 3, 4, 5, 6, 7, 8, 9]"
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
      </div>
      <div
        v-if="!showBurgerMenu"
        @click="showBurgerMenu = true"
        class="button_container d-md-none d-block"
        :class="showBurgerMenu ? 'active' : ''"
        id="toggle"
      >
        <button class="btn btn-sm btn-info w-100 text-white">فیلتر</button>
      </div>
      <div
        class="overlay d-md-none d-block"
        id="overlay"
        :class="showBurgerMenu ? 'open' : ''"
      >
        <div class="overlay-menu">
          <div class="col-12 text-center">
            <button
              v-if="showBurgerMenu"
              @click="showBurgerMenu = false"
              class="btn btn-sm btn-outline-danger py-3 w-100"
            >
              بستن فیلتر
            </button>
          </div>
          <RoommateSidebar />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import RoommateSidebar from "../../components/RoommateSidebar";
import RoommateCard from "../../components/RoommateCard";
import veeno from "veeno";
import "nouislider/distribute/nouislider.min.css";

export default {
  name: "index",
  components: { RoommateCard, RoommateSidebar, veeno },
  layout: "MigrationLayout",
  fetchOnServer() {
    return true;
  },
  async fetch() {
    const response = await this.$repositories.getAllUsers.getAllUsers();
    this.allUser = response.data;
  },
  data() {
    return {
      userTextSearch: "",
      showBurgerMenu: false,
      allUser: [],
      country: [
        { title: "آمریکا", code: 1 },
        { title: "هلند", code: 2 },
        { title: "انگلیس", code: 3 },
        { title: "فرانسه", code: 4 },
        { title: "آلمان", code: 5 },
        { title: "کانادا", code: 6 },
      ],
      ReceptionDate: "",
      RoomType: null,
      RoomType1: null,
      RoomType2: null,
      RoomType3: null,
      RoomType4: null,
      BedroomType: null,
      BathroomType: null,
      RoomateGenderNot: false,
      RoomateGenderMale: false,
      RoomateGenderFemale: false,
      RoomateGenderOther: false,
      RoomateGenderOther1: false,
      RoomateGenderOther2: false,
      RoomateGenderOther3: false,
      RoomateGenderOther4: false,
      RoomateGenderOther5: false,
      RoomateAgeRangeFrom: null,
      RoomateAgeRangeTo: null,
      RoomateSmokeHas: null,
      RoomateSmokeHasCheck: null,
      RoomateSmokeNot: null,
      RoomateSmokeNotCheck: null,
      RoomateSmokeOther: null,
      RoomateSmokeOtherCheck: null,
      RoomateChildHas: null,
      RoomateChildHasCheck: null,
      RoomateChildNot: null,
      RoomateChildNotCheck: null,
      RoomatePetHas: null,
      RoomatePetHasCheck: null,
      RoomatePetNot: null,
      RoomatePetNotCheck: null,
      ResidenceNumber: null,
      MortgageMin: null,
      MortgageMax: null,
    };
  },
  methods: {
    // seeUserProfile(userId) {
    //   this.$router.push({path: '/Migration/UserInfo', query: {userId}})
    // },
    getAllUser() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      });
      this.$axios
        .post(`Users/GetAll`, null, {})
        .then((response) => {
          if (response.status === 200) {
            this.allUser = response.data;
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false;
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    getSliderValue(value) {
      this.MortgageMin = value.positions[0];
      this.MortgageMax = value.positions[1];
    },
    RoommateChildType(ChildType) {
      if (ChildType === 1) {
        this.RoomateChildHas = this.RoomateChildHasCheck ? 0 : 1;
      }
      if (ChildType === 2) {
        this.RoomateChildNot = this.RoomateChildNotCheck ? 0 : 1;
      }
    },
    RoommatePetType(PetType) {
      if (PetType === 1) {
        this.RoomatePetHas = this.RoomatePetHasCheck ? 0 : 1;
      }
      if (PetType === 2) {
        this.RoomatePetNot = this.RoomatePetNotCheck ? 0 : 1;
      }
    },
    RoommateSmokeType(SmokeType) {
      if (SmokeType === 1) {
        this.RoomateSmokeHas = this.RoomateSmokeHasCheck ? 0 : 1;
      }
      if (SmokeType === 2) {
        this.RoomateSmokeNot = this.RoomateSmokeNotCheck ? 0 : 1;
      }
      if (SmokeType === 3) {
        this.RoomateSmokeOther = this.RoomateSmokeOtherCheck ? 0 : 1;
      }
    },
    roomateGenderValue(GenderValue) {
      if (GenderValue !== null) {
        this.RoomateGender = this.RoomateGender + GenderValue;
      }
    },
  },
  mounted() {
    // this.getAllUser();
  },
  computed: {
    filterCard() {
      this.allUser.filter();
    },
  },
};
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
  border: 1px solid #d9d9d9;
  border-radius: 3px;
  background: #fff;
  cursor: default;
  box-shadow: inset 0 0 1px #fff;
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

.roommateSearchBox {
  width: 50%;
}

.roommateSearchBox::placeholder {
  color: #cccccc;
  font-size: 12px;
}

.button_container {
  position: relative;
  cursor: pointer;
  transition: opacity 0.25s ease;
  z-index: 101;
  width: 100%;
}

.button_container:hover {
  opacity: 0.7;
}

.button_container.active .top {
  transform: translateY(11px) translateX(0) rotate(45deg);
  background: black;
}

.button_container.active .middle {
  opacity: 0;
  background: black;
}

.button_container.active .bottom {
  transform: translateY(-11px) translateX(0) rotate(-45deg);
  background: black;
}

.button_container span {
  background: black;
  border: none;
  height: 5px;
  width: 100%;
  position: absolute;
  top: 0;
  left: 0;
  transition: all 0.35s ease;
  cursor: pointer;
}

.button_container span:nth-of-type(2) {
  top: 11px;
}

.button_container span:nth-of-type(3) {
  top: 22px;
}

.overlay {
  position: fixed;
  background: #fff;
  min-height: 100vh;
  min-width: 100vw;
  top: 0;
  left: 0;
  width: 100%;
  height: 0;
  padding: 0;
  opacity: 0;
  visibility: hidden;
  transition: opacity 0.35s, visibility 0.35s, height 0.35s;
  overflow-y: scroll;
  overflow-x: hidden;
  z-index: 100;
}

.overlay.open {
  opacity: 1;
  visibility: visible;
  height: 100%;
  position: fixed;
  width: 100%;
  min-width: 100%;
  min-height: 100%;
  overflow-y: scroll;
  padding: 1rem;
}

.overlay.open li {
  animation: fadeInRight 0.5s ease forwards;
  animation-delay: 0.35s;
}

.overlay.open li:nth-of-type(2) {
  animation-delay: 0.4s;
}

.overlay.open li:nth-of-type(3) {
  animation-delay: 0.45s;
}

.overlay.open li:nth-of-type(4) {
  animation-delay: 0.5s;
}

.overlay nav {
  position: relative;
  height: 70%;
  top: 50%;
  transform: translateY(-50%);
  font-size: 50px;
  font-family: "Varela Round", serif;
  font-weight: 400;
  text-align: center;
}

.overlay ul {
  list-style: none;
  padding: 0;
  margin: 0 auto;
  display: inline-block;
  position: relative;
  height: 100%;
}

.overlay ul li {
  display: block;
  height: 25%;
  height: calc(100% / 4);
  min-height: 50px;
  position: relative;
  opacity: 0;
}

.overlay ul li a {
  display: block;
  position: relative;
  color: #fff;
  text-decoration: none;
  overflow: hidden;
}

.overlay ul li a:hover:after,
.overlay ul li a:focus:after,
.overlay ul li a:active:after {
  width: 100%;
}

.overlay ul li a:after {
  content: "";
  position: absolute;
  bottom: 0;
  left: 50%;
  width: 0;
  transform: translateX(-50%);
  height: 3px;
  background: #fff;
  transition: 0.35s;
}

@keyframes fadeInRight {
  0% {
    opacity: 0;
    left: 20%;
  }
  100% {
    opacity: 1;
    left: 0;
  }
}

@media screen and (max-width: 726px) {
  .roommateSearchBox {
    width: 100%;
  }
}
</style>
