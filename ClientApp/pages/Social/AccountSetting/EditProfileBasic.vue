<template>
  <div class=" NewBg mcontainer position-relative">

      <div class="tw-w-full tw-shadow bg-white p-3 d-flex align-items-center gap-3  rounded ">
        <img src="/girl-icon-dashboard.png" class="tw-w-[7rem] tw-h-[7rem] tw-object-contain" alt="">
        <div class="d-flex align-items-center flex-column">
          <h1 class="text-purple h6" style="font-weight: bolder!important;">ویرایش اطلاعات</h1>
          <strong class="text-pink">Edit Profile Data</strong>
        </div>

      </div>

    <div class="row bg-white rounded shadow my-3 p-3 mx-auto">

      <form class="row">
        <div class="col-md-6 col-lg-6 col-12 mt-3">
          <label for="firstName">نام</label>
          <input
              type="text"
              class="FormInputs with-border "
              id="firstName"
              placeholder="نام "
              v-model.trim="firstName"
            />

        </div>
        <div class="col-md-6 col-lg-6 col-12 mt-3">
            <label for="lastName">نام خانوادگی</label>
            <input
              type="text"
              class="FormInputs with-border"
              id="lastName"
              placeholder="نام خانوادگی"
              v-model.trim="lastName"
            />
        </div>
        <div class="col-12 form-group mt-3">

            <label for="mobile">تلفن</label>
            <input
              type="tel"
              v-tooltip="{content:'شماره تلفن غیر قابل ویرایش است'}"
              class="FormInputs with-border tw-cursor-not-allowed"
              id="mobile"
              disabled
              v-model.trim="mobile"
              placeholder="تلفن"
            />

        </div>
        <div class="col-12 mt-3">
          <label for="" class="m-0 text-muted">تاریخ تولد</label>
          <client-only>
            <date-picker
              format="YYYY-MM-DD"
              display-format="jYYYY-jMM-jDD"
              v-model="birthDate"
              type="date"
            />
          </client-only>
        </div>

        <div class="col-md-12 my-3 d-flex align-items-center  gap-2 justify-content-start flex-wrap">
          <span class="text-secondary ">وضعیت تاهل :  </span>
          <div class="d-flex align-items-center gap-3">
            <input
              class="form-check-input"
              type="radio"
              id="inlineCheckbox10"
              :value="1"
              v-model="Relation"
            />
            <label class="m-0" for="inlineCheckbox9">مجرد</label>
          </div>
          <div class="d-flex align-items-center gap-3">
            <input
              class="form-check-input"
              type="radio"
              id="inlineCheckbox11"
              :value="2"
              v-model="Relation"
            />
            <label class="m-0" for="inlineCheckbox9">در رابطه</label>
          </div>
          <div class="d-flex align-items-center gap-3">
            <input
              class="form-check-input"
              type="radio"
              id="inlineCheckbox12"
              :value="3"
              v-model="Relation"
            />
            <label class="m-0" for="inlineCheckbox9">متاهل</label>
          </div>
          <div class="d-flex align-items-center gap-3">
            <input
              class="form-check-input"
              type="radio"
              id="inlineCheckbox13"
              :value="4"
              v-model="Relation"
            />
            <label class="m-0" for="inlineCheckbox9">مطلقه</label>
          </div>
          <div class="d-flex align-items-center gap-3">
            <input
              class="form-check-input"
              type="radio"
              id="inlineCheckbox14"
              :value="5"
              v-model="Relation"
            />
            <label class="m-0" for="inlineCheckbox9">سایر</label>
          </div>
        </div>
        <div class="col-md-12 my-3">
          <div class="row">
            <div class="col-md-6">
              <v-select
                :options="AllStates"
                label="name"
                placeholder="استان" dir="rtl"
                :reduce="name => name.stateId"
                v-model="SelectedStateId"
              ></v-select>
            </div>
            <div class="col-md-6">
              <v-select
                :options="AllCities"
                label="name"
                placeholder="شهر" dir="rtl"
                :reduce="name => name.cityId"
                v-model="SelectedCityId"
              ></v-select>
            </div>
          </div>
        </div>
        <div class="col-12 ">

            <label>درباره من</label>
            <textarea
              class="FormInputs with-border"
              resize="none"
              placeholder="درباره من"
              id="about"
              v-model.trim="bio"
              style="height: 150px"
            ></textarea>

        </div>
        <div class="col-12 mt-3">

            <label>هشتگ های من (با علامت کاما, از هم جدا کنید)</label>
            <textarea
              class="FormInputs with-border"
              resize="none"
              placeholder="هشتگ های من (با علامت کاما, از هم جدا کنید)"
              id="about2"
              v-model.trim="userTag"
              style="height: 150px"
            ></textarea>

        </div>
        <div class="col-12 mt-3">
          <div class="row">
            <div class="col-lg-3 col-md-12">
              <div class="labelText">ارسال عکس کارت ملی :</div>
            </div>
            <div class="col-lg-2 col-md-12">
              <div class="border rounded text-center d-flex justify-content-center align-items-center InputUiBox" @click="callInputMethod">
                <input
                  type="file"
                  accept="image/*"
                  class="form-control-file MainImage d-none"
                  id="MainImage"
                  @change="onFileChange"
                />
                <LazyUploadIcon class="tw-fill-[#b44aff]"></LazyUploadIcon>
              </div>

            </div>
            <div class="col-lg-7 col-md-12">
              <div v-if="cart !== ''">
                <img
                  class="img-fluid"
                  :src="cart"
                />
              </div>
              <div v-else>

                <img
                  class="img-fluid"
                  :src=" BaseUrl + nationalCart"
                />
              </div>
            </div>
          </div>

        </div>

        <div class="col-12 mt-3">

          <button
            type="button"
            class="bg-pink rounded p-2 tw-cursor-pointer text-white"
            @click="submitEditProfileBasic"
          >
            به روز رسانی
          </button>
        </div>
      </form>
    </div>
    <div class="tw-fixed tw-bottom-0 tw-left-0 tw-p-10">
      <LazyCircularProgress :RadialProgress="RadialProgress"></LazyCircularProgress>

    </div>
  </div>
</template>

<script>
export default {
  name: "EditProfileBasic",
  layout: "PoshtebamPlusLayout",
  head() {
    return {
      title: 'ویرایش حساب کاربری',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'ویرایش حساب کاربری',
        },
      ],
    }
  },
  data() {
    return {
      image: "",
      Uploaded: false,
      BaseImgUrl: "",
      date: "",
      firstName: "",
      lastName: "",
      // email: '',
      mobile: null,
      bio: "",
      selected: null,
      birthDate: "",
      userInfoData: {},
      gender: null,
      Relation: 0,
      userTag: "",
      pass: null,
      nationalCart: "",
      cart: '',
      userSettingId: null,
      AllStates: [],
      AllCities: [],
      SelectedStateId: null,
      SelectedCityId: null,

        requiredFields: ["firstName", "lastName", "mobile", 'birthDate', 'bio', 'nationalCart', 'SelectedCityId', 'SelectedStateId', 'userTag'],


    };
  },
  watch: {
    SelectedStateId: function (val, oldVal) {

      this.GetCity()
    }
  },
  async fetch() {
    const res = await this.$repositories.GetAllStates.GetAllStates({
      pageNumber: 0,
      count: 0
    })
    this.AllStates = res.data.states

  },
  computed: {
    FilledCount() {
      let count = 0
      this.requiredFields.forEach(item => {
        if(this[item] && this[item]!==''){
          count++
        }
      })
      return count
    },
    RadialProgress() {
      return Math.round(this.FilledCount * (100 / 9))
    },
    BaseUrl() {
      return process.env.pic
    },
  },
  methods: {
    async GetCity() {

      this.$nextTick(() => {
        this.$nuxt.$loading.start()
      })
      try {

        const res = await this.$repositories.GetAllCities.GetAllCities({
          pageNumber: 1,
          count: 500,
          stateId: this.SelectedStateId
        })
        console.log(this.AllCities)
        this.AllCities = res.data.cities
        this.$nuxt.$loading.finish()
        this.$nuxt.loading = false;
      } catch (e) {
        console.log(e)
      } finally {
        this.$nuxt.$loading.finish()
        this.$nuxt.loading = false;
      }
    },

    submitEditProfileBasic() {
      if (this.mobile !== "" && this.mobile.length !== 11) {
        this.$toast.error("فرمت شماره موبایل وارد شده درست نیست");
      } else {
        this.$nextTick(() => {
          this.$nuxt.$loading.start();
        });

        this.$axios
          .post(
            `UserSetting/Update`,
            {
              userId: this.$auth.user.userInfo.userId,
              isPrivate: this.$auth.isPrivate,
              isPrivateRoomate: this.$auth.isPrivateRoomate,
              isPrivateAds: this.$auth.isPrivateAds,
              isPrivateSocial: this.$auth.isPrivateSocial,
              userTag: this.userTag,
              flag: this.countryName,
              bio: this.bio,
              // email: this.email,
              gender: parseInt(this.gender),
              birthDate: this.birthDate,
              kartMelliDoc: this.nationalCart,
              userSettingId: this.userSettingId,
              relationState: this.Relation,
              stateId: this.SelectedStateId,
              cityId: this.SelectedCityId
            },
            {}
          )
          .then((response) => {
            if (response.status === 200) {
              console.log(this.Relation)
              this.$axios.post("Users/Update", {
                  name: this.firstName,
                  familyName: this.lastName,
                  relationState: 1,
                  userId: this.$auth.user.userInfo.userId,
                  stateId: this.SelectedStateId,
                  cityId: this.SelectedCityId
                }
              )
                .then((res) => {
                  if (res.status === 200) {
                    this.GetMyData();
                  }
                });
              this.$toast.success("به روز رسانی با موفقیت انجام شد.");

              this.$nuxt.$loading.finish();
            }
          })
          .catch((error) => {
            console.log(error);
            this.$nuxt.$loading.finish();
          });
      }
    },
    GetMyData() {
      this.$axios.post("Common/GetIndexData", null, {}).then((res) => {
        this.$store.commit("SetUserData", res.data);
        this.userInfoData = res.data.userInfo;
        this.firstName = this.userInfoData.name;
        this.lastName = this.userInfoData.familyName;
        this.email = this.userInfoData.email;
        this.mobile = this.userInfoData.mobile;
        this.countryName = this.userInfoData.userSetting.flag;
        this.bio = this.userInfoData.userSetting.bio;
        this.userTag = this.userInfoData.userSetting.userTag;
        this.gender = this.userInfoData.userSetting.gender;
        this.birthDate = this.userInfoData.userSetting.birthDate;
        this.userSettingId = this.userInfoData.userSetting.userSettingId;
        this.nationalCart = this.userInfoData.userSetting.kartMelliDoc;
        this.Relation = this.userInfoData.relationState
        this.Video = this.userInfoData.userSetting.videoIdentity;
        this.SelectedStateId = this.userInfoData.stateId;

      });
    },

    // National Card
    callInputMethod() {
      document.querySelector("#my-file").click();
    },
    onFileChange(e) {
      const file = e.target.files[0];
      this.cart = URL.createObjectURL(file);
      const that = this;
      const reader = new FileReader();
      reader.onload = (function (theFile) {
        return function () {
          const binaryData = reader.result;
          that.nationalCart = window.btoa(binaryData);
        };
      })(file);
      reader.readAsBinaryString(file);
    },
  },
  mounted() {
    this.GetMyData();
  },
};
</script>

<style scoped>
@media (min-width: 1024px) {
  .mcontainer {
    max-width: 1000px;
    padding: 30px 0px;
    margin-left: auto;
    margin-right: auto;
  }
}

.InputUiBox {
  padding: 2rem 0 0 0;
  width: 100px;
  height: 100px;
  cursor: pointer;
}

img {
  width: 100px;
  height: 100px;
  margin: 0 8px;
  border-radius: 8px;
}

#preview img {
  width: 100px;
  height: 100px;
  margin: 0 8px;
  border-radius: 8px;
}

#preview2 img {
  width: 100px;
  height: 100px;
  margin: 0 8px;
  border-radius: 8px;
}

.labelText {
  color: #545454;
  font-size: 14px;
  cursor: pointer;
}

.boxMainContent {
  background: #fdfdfd none repeat scroll 0 0;
  border: 1px solid #ede9e9;
  border-radius: 3px;
  display: inline-block;
  width: 100%;
  margin-bottom: 20px;
  padding: 25px;
}

textarea {
  resize: none;
}

.custom-input {
  width: 100%;
  border: none;
  border-bottom: 0.0625rem solid #e1e8ed;
  margin-bottom: 20px;
  margin-top: 20px;
}

.custom-input::placeholder {
  color: #b7b7b7;
  font-size: 1rem;
  font-weight: 400;
}

.editAccountSetting {
  font-size: 18px;
  text-transform: capitalize;
  margin-bottom: 20px;
  padding-bottom: 20px;
  border-bottom: 3px solid #088dcd;
}

.ButtonAccountSetting {
  font-size: 16px;
  background-color: #4f93ce;
  color: white;
  margin-top: 10px;
  padding: 7px 30px;
  cursor: pointer;
  -webkit-transition: background-color 0.28s ease, color 0.28s ease,
  -webkit-box-shadow 0.28s ease;
  transition: background-color 0.28s ease, color 0.28s ease,
  -webkit-box-shadow 0.28s ease;
  transition: background-color 0.28s ease, color 0.28s ease,
  box-shadow 0.28s ease;
  transition: background-color 0.28s ease, color 0.28s ease,
  box-shadow 0.28s ease, -webkit-box-shadow 0.28s ease;
  overflow: hidden;
  -webkit-box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.14),
  0 3px 1px -2px rgba(0, 0, 0, 0.2), 0 1px 5px 0 rgba(0, 0, 0, 0.12);
  box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.14), 0 3px 1px -2px rgba(0, 0, 0, 0.2),
  0 1px 5px 0 rgba(0, 0, 0, 0.12);
}

.ButtonAccountSetting:hover {
  background-color: #088dcd;
  -webkit-box-shadow: 0 6px 10px 0 rgba(0, 0, 0, 0.14),
  0 1px 18px 0 rgba(0, 0, 0, 0.12), 0 3px 5px -1px rgba(0, 0, 0, 0.2);
  box-shadow: 0 6px 10px 0 rgba(0, 0, 0, 0.14), 0 1px 18px 0 rgba(0, 0, 0, 0.12),
  0 3px 5px -1px rgba(0, 0, 0, 0.2);
}

.ButtonAccountSetting:active::before,
.ButtonAccountSetting:focus::before {
  -webkit-transition: opacity 0.28s ease 364ms, -webkit-transform 1.12s ease;
  transition: opacity 0.28s ease 364ms, -webkit-transform 1.12s ease;
  transition: transform 1.12s ease, opacity 0.28s ease 364ms;
  transition: transform 1.12s ease, opacity 0.28s ease 364ms,
  -webkit-transform 1.12s ease;
  -webkit-transform: translate(-50%, -50%) scale(1);
  transform: translate(-50%, -50%) scale(1);
  opacity: 0;
}

.ButtonAccountSetting:focus {
  outline-style: none;
}

.form-floating > label {
  right: 0 !important;
  width: fit-content;
  color: #b7b7b7 !important;
  font-size: 1rem;
  font-weight: 400;
  border-bottom: 0.0625rem solid #e1e8ed !important;
}

.form-floating > .form-control {
  border-bottom: 0.0625rem solid #e1e8ed !important;
}

.form-floating > .form-control:focus,
.form-floating > .form-control:focus:not(:placeholder-shown) {
  padding-bottom: 10px !important;
  border-bottom: 3px solid #088dcd !important;
  transition: left 0.28s ease, width 0.28s ease;
  -webkit-transition: left 0.28s ease, width 0.28s ease;
  width: 100%;
  left: 50%;
  outline-style: none !important;
}

.form-control input {
  background: 0 0;
  padding: 0.125rem 0.125rem 0.0625rem;
  font-size: 1rem;
  border-width: 0;
  border-color: transparent;
  line-height: 1.9;
  width: 100%;
  color: transparent;
  -webkit-transition: all 0.28s ease;
  transition: all 0.28s ease;
  -webkit-box-shadow: none;
  box-shadow: none;
  outline-style: none !important;
}

.custom_radio input[type="radio"] {
  display: none;
}

.custom_radio input[type="radio"] + label {
  position: relative;
  display: inline-block;
  padding-left: 1.5em;
  margin-right: 2em;
  cursor: pointer;
  line-height: 1em;
  -webkit-transition: all 0.3s ease-in-out;
  transition: all 0.3s ease-in-out;
  color: #676767;
  font-size: 13px;
}

.custom_radio input[type="radio"] + label:before,
.custom_radio input[type="radio"] + label:after {
  content: "";
  position: absolute;
  top: 0;
  right: -1.5rem;
  width: 1em;
  height: 1em;
  text-align: center;
  color: white;
  border-radius: 50%;
  -webkit-transition: all 0.3s ease;
  transition: all 0.3s ease;
}

.custom_radio input[type="radio"] + label:before {
  -webkit-transition: all 0.3s ease;
  transition: all 0.3s ease;
  box-shadow: inset 0 0 0 0.2em white, inset 0 0 0 1em white;
  border: 1px solid #8f8e8e;
}

.custom_radio input[type="radio"] + label:hover:before {
  -webkit-transition: all 0.3s ease;
  transition: all 0.3s ease;
  box-shadow: inset 0 0 0 0.3em white, inset 0 0 0 1em #c6c6c6;
}

.custom_radio input[type="radio"]:checked + label:before {
  -webkit-transition: all 0.3s ease;
  transition: all 0.3s ease;
  box-shadow: inset 0 0 0 0.2em white, inset 0 0 0 1em #088dcd;
  border: 1px solid #088dcd;
}

label {

  font-size: 14px;
  color: #545454;
}

.form-check .form-check-input {
  float: right;
}

@media (max-width: 576px) {
  .boxMainContent {
    background: #fdfdfd none repeat scroll 0 0;
    border: 1px solid #ede9e9;
    border-radius: 3px;
    display: inline-block;
    width: 100%;
    margin-bottom: 20px;
    padding: 30px 15px;
  }
}

.Customermsg {
  background-color: #2441e7;
  border-radius: 10px;
}

.TicketFile {
  opacity: 0;
}

.File {
  opacity: 0;
  position: relative;
  bottom: 45px;
  cursor: pointer;
}

.SingleTicketCard {
  border: none !important;
}

.SingleTicketCard:hover {
  transform: translateY(0px) !important;
}

.TicketImgSize {
  width: 50px;
  height: 50px;
  border-radius: 50%;
}

.activeClass {
  background-color: rgb(241, 241, 241);
  border-right: solid 0.2px blue;
}

.SendFile {
  position: relative;
}

.ParentsContainer {
  border-right: solid 0.1px rgb(224, 224, 224);
}

.DetailContainer {
  background-color: rgb(250, 250, 250);
  border-top-right-radius: 30px;
  border-bottom-right-radius: 30px;
}

.TicketsImg {
  width: 50px;
  height: 50px;
  border-radius: 50%;
}

.MyCoursesCard {
  border: none !important;
}

.MyCoursesCard:hover {
  transform: none !important;
}

.CreateTicketBtn {
  background-color: rgb(27, 213, 75);
  color: white;
}

.UserClose {
  background-color: #ff1053;
  color: white;
}

.MyCoursesButton-Overlay {
  border-radius: 50px;
  background-color: transparent;
  color: white;
  border: solid 0.5px white;
  width: 20%;
  height: 15%;
}

.MyCourses-Header-Bg {
  background-color: rgb(225, 230, 239);
  border-radius: 10px;
}

.MyCoursesBtn {
  background-size: 15px;
  background-color: white;
  border: solid 1px;
  border-top-right-radius: 10px;
  border-bottom-right-radius: 10px;
  border-top-left-radius: 0px;
  border-bottom-left-radius: 0px;
  border: 1px solid #ced4da;
  border-left: none;
}

.overlay-items {
  margin-right: 5vh;
  color: black !important;
}

.MyCoursesControl {
  border-right: none !important;
  border-top-right-radius: 0px;
  border-bottom-right-radius: 0px;
}

.Panel-BreadCrumb {
  color: black !important;
}

.DeleteUserBtn {
  background-color: #ff1053;
  color: white;
}

.DeleteUserBtn:hover {
  border: solid 1px #ff1053;
  color: #ff1053;
  background-color: white;
  transition: 0.1s ease;
}

.MyCoursesMain {
  box-shadow: 0px 1px 4px 0px rgb(0 0 0 / 9%);
  background-color: white;
  border-radius: 5px;
}
</style>
