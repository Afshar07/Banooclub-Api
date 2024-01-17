<template>
  <form class="row">
<!--    <div class="col-md-6 col-lg-6 col-12 mt-3">-->
      <div class="col-md-6 col-sm-12">
        <label>نام کاربری</label>
        <input v-model.trim="firstName" type="text" id="firstName" class="with-border" placeholder="نام کاربری">
      </div>
<!--      <div class="form-floating text-end">-->
<!--        <input-->
<!--          type="text"-->
<!--          class="form-control with-border"-->
<!--          id="firstName"-->
<!--          placeholder="نام کاربری"-->
<!--          v-model.trim="firstName"-->
<!--        />-->
<!--        <label for="firstName">نام</label>-->
<!--      </div>-->
<!--    </div>-->
    <div class="col-md-6 col-sm-12">
      <label>نام خانوادگی</label>
      <input v-model.trim="lastName" type="text" id="lastName" class="with-border" placeholder="نام خانوادگی">
<!--      <div class="form-floating text-end">-->
<!--        <input-->
<!--          type="text"-->
<!--          class="form-control with-border"-->
<!--          id="lastName"-->
<!--          placeholder="نام کاربری"-->
<!--          v-model.trim="lastName"-->
<!--        />-->
<!--        <label for="lastName">نام خانوادگی</label>-->
<!--      </div>-->
    </div>
    <!--        <div class="col-12 form-group mt-3">-->
    <!--          <div class="form-floating text-end">-->
    <!--            <input type="email" class="form-control border-0 border-bottom" id="email" v-model.trim="email"-->
    <!--                   placeholder="[ایمیل محافظت شده است]">-->
    <!--            <label for="email">[ایمیل محافظت شده است]</label>-->
    <!--          </div>-->
    <!--        </div>-->
    <div class="col-12 form-group mt-3">
<!--      <div class="form-floating text-end">-->
      <label for="mobile">تلفن</label>
      <input
          type="tel"
          class="with-border"
          id="mobile"
          disabled
          v-model.trim="mobile"
          placeholder="تلفن"
        />
<!--      </div>-->
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
    <div class="col-12 mt-3">
      <!--          <div class="custom_radio">-->
      <!--            <input type="radio" id="male" name="featured" checked><label for="male">مرد</label>-->
      <!--            <input type="radio" id="female" name="featured"><label for="female">زن</label>-->
      <!--          </div>-->
      <div>
        <div class="form-check form-check-inline">
          <input
            class="form-check-input"
            type="radio"
            id="inlineCheckbox7"
            value="1"
            v-model="gender"
          />
          <label class="form-check-label" for="inlineCheckbox7">مرد</label>
        </div>
        <div class="form-check form-check-inline">
          <input
            class="form-check-input"
            type="radio"
            id="inlineCheckbox8"
            value="2"
            v-model="gender"
          />
          <label class="form-check-label" for="inlineCheckbox8">زن</label>
        </div>
        <div class="form-check form-check-inline">
          <input
            class="form-check-input"
            type="radio"
            id="inlineCheckbox9"
            value="3"
            v-model="gender"
          />
          <label class="form-check-label" for="inlineCheckbox9">سایر</label>
        </div>
      </div>
    </div>
    <!--        <div class="col-12 my-3">-->
    <!--          <v-select placeholder="کشور" dir="rtl" v-model="countryName"-->
    <!--                    :reduce="(option) => option.id"-->
    <!--                    :options="[-->
    <!--      { label: 'ایران', id: 1 },-->
    <!--      { label: 'آمریکا', id: 2 },-->
    <!--    ]">-->
    <!--            <i slot="spinner" class="icon icon-spinner"></i>-->

    <!--          </v-select>-->
    <!--        </div>-->
    <!--        <div class="col-12 my-3">-->
    <!--          <v-select placeholder="شهر" dir="rtl" v-model="cityName" :reduce="(option) => option.id"-->
    <!--                    :options="[-->
    <!--      { label: 'کرمانشاه', id: 1 },-->
    <!--      { label: 'تهران', id: 2 },-->
    <!--    ]">-->
    <!--            <i slot="spinner" class="icon icon-spinner"></i>-->
    <!--          </v-select>-->
    <!--        </div>-->

    <div class="col-12 mt-3">
      <div class="form-floating">
            <textarea
              class="form-control with-border px-0"
              resize="none"
              placeholder="درباره من"
              id="about"
              v-model.trim="bio"
              style="height: 150px"
            ></textarea>
        <label>درباره من</label>
      </div>
    </div>
    <div class="col-12 mt-3">
      <div class="form-floating">
            <textarea
              class="form-control with-border px-0"
              resize="none"
              placeholder="درباره من"
              id="about"
              v-model.trim="userTag"
              style="height: 150px"
            ></textarea>
        <label>هشتگ های من</label>
      </div>
    </div>
    <div class="col-12 mt-3">
      <div
        v-if="$store.state.HeaderData.type === 4"
        class="d-flex flex-row gap-3 my-3"
      >
        <div class="labelText">ارسال ویدیو احراز هویت :</div>
        <div
          class="border rounded text-center justify-content-center align-items-center InputUiBox"
          @click="callInputMethod3"
        >
          <input
            type="file"
            ref="Video"
            accept="video/*"
            class="form-control-file my-file3 d-none"
            id="my-Video"
            @change="onFileChangeVideo"
          />
          <font-awesome-icon icon="plus-square" size="lg" />
        </div>

        <video
          :src="BaseUrl + Video"
          v-if="Video && Video.includes('.mp4')"
          width="225"
          height="240"
          controls
        >
          <!--              <source  type="video/*">-->
        </video>
        <video v-else :src="BaseVideoUrl" width="225" height="240" controls>
          <!--              <source  type="video/*">-->
        </video>
      </div>
      <div class="row" v-if="$store.state.HeaderData.type === 3">
        <div class="col-lg-3 col-md-12">
          <div class="labelText">ارسال عکس پروانه کسب :</div>
        </div>
        <div class="col-lg-2 col-md-12">
          <div
            class="border rounded text-center justify-content-center align-items-center InputUiBox"
            @click="callInputMethod4"
          >
            <input
              type="file"
              accept="image/*"
              multiple="multiple"
              class="form-control-file my-file d-none"
              id="my-file6"
              @change="onFileChange4"
            />
            <font-awesome-icon icon="plus-square" size="lg" />
          </div>
        </div>
        <div class="col-lg-7 col-md-12">
          <div v-if="Certificate && Certificate.includes('.')">
            <img class="img-fluid" :src="BaseUrl + Certificate" />
          </div>
          <div v-else>
            <img class="img-fluid" :src="Certificate" />
          </div>
        </div>
      </div>
      <div class="row my-3" v-if="$store.state.HeaderData.type === 3">
        <div class="col-lg-3 col-md-12">
          <div class="labelText">ارسال عکس روزنامه رسمی :</div>
        </div>
        <div class="col-lg-2 col-md-12">
          <div
            class="border rounded text-center justify-content-center align-items-center InputUiBox"
            @click="callInputMethod5"
          >
            <input
              type="file"
              accept="image/*"
              multiple="multiple"
              class="form-control-file my-file d-none"
              id="my-file7"
              @change="onFileChange5"
            />
            <font-awesome-icon icon="plus-square" size="lg" />
          </div>
        </div>
        <div class="col-lg-7 col-md-12">
          <div v-if="NewsPaper && NewsPaper.includes('.')">
            <img class="img-fluid" :src="BaseUrl + NewsPaper" />
          </div>
          <div v-else>
            <img class="img-fluid" :src="NewsPaper" />
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-lg-3 col-md-12">
          <div class="labelText">ارسال عکس کارت ملی :</div>
        </div>
        <div class="col-lg-2 col-md-12">
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
        </div>
        <div class="col-lg-7 col-md-12">
          <div v-if="cart">
            <img class="img-fluid" :src="cart" v-if="nationalCart" />
          </div>
          <div v-if="nationalCart !== null && !cart">
            <img
              class="img-fluid"
              :src="`https://subapi.banoclub.com/${nationalCart}`"
            />
          </div>
        </div>
      </div>
      <!--          <div class="row my-3">-->
      <!--            <div class="col-lg-3 col-md-12">-->
      <!--              <div class="labelText">ارسال عکس پاسپورت :</div>-->
      <!--            </div>-->
      <!--            <div class="col-lg-2 col-md-12">-->
      <!--              <div-->
      <!--                class="border rounded text-center justify-content-center align-items-center InputUiBox"-->
      <!--                @click="callInputMethod2"-->
      <!--              >-->
      <!--                <input-->
      <!--                  type="file"-->
      <!--                  accept="image/*"-->
      <!--                  multiple="multiple"-->
      <!--                  class="form-control-file my-file2 d-none"-->
      <!--                  id="my-file2"-->
      <!--                  @change="onFileChange2"-->
      <!--                />-->
      <!--                <font-awesome-icon icon="plus-square" size="lg" />-->
      <!--              </div>-->
      <!--            </div>-->
      <!--            <div class="col-lg-7 col-md-12">-->
      <!--              <div id="preview2" v-if="pass">-->
      <!--                <img class="img-fluid" :src="pass" v-if="passport" />-->
      <!--              </div>-->
      <!--              <div v-if="passport !== null && !pass">-->
      <!--                <img-->
      <!--                  class="img-fluid"-->
      <!--                  :src="`https://subapi.banoclub.com/${passport}`"-->
      <!--                />-->
      <!--              </div>-->
      <!--            </div>-->
      <!--          </div>-->
    </div>

    <!--        <div class="col-12 mt-3">-->
    <!--          <div class="d-flex gap-2 align-items-center">-->
    <!--            <small class="text-muted">کشور:</small>-->
    <!--            <v-select-->
    <!--              :options="country"-->
    <!--              label="name"-->
    <!--              dir="rtl"-->
    <!--              class="selectWidth w-50"-->
    <!--              placeholder="کشور"-->
    <!--              v-model="countryName"-->
    <!--              :reduce="(name) => name.iso3"-->
    <!--              :value="countryName"-->
    <!--            ></v-select>-->
    <!--            <div class="pt-3 d-flex flex-column align-items-center">-->
    <!--              <country-flag-->
    <!--                size="big"-->
    <!--                style="border-radius: 50px"-->
    <!--                :country="countryName"-->
    <!--              />-->
    <!--              <small>{{ countryName }}</small>-->
    <!--            </div>-->
    <!--          </div>-->
    <!--        </div>-->
    <div class="col-12 mt-3">
      <button type="button" class="btn btn-sm ButtonAccountSetting">
        لغو
      </button>
      <button
        type="button"
        class="btn btn-sm ButtonAccountSetting"
        @click="submitEditProfileBasic"
      >
        به روز رسانی
      </button>
    </div>
  </form>

</template>

<script>
export default {
  name: "EditInfoContent",
  data() {
    return {
      AllTickets: "",
      TicketTitle: "",
      TicketType: "",
      TicketDescription: "",
      image: "",
      Uploaded: false,
      BaseImgUrl: "",
      date: "",
      firstName: "",
      lastName: "",
      // email: '',
      mobile: null,
      bio: "",
      cityName: "",
      selected: null,
      countryName: "",
      birthDate: "",
      userInfoData: {},
      gender: null,
      userTag: "",
      cart: null,
      pass: null,
      nationalCart: "",
      passport: "",
      country: [],
      userSettingId: null,
      SelectedTicketId: 0,
      SingleTicket: [],
      BaseVideoUrl: "",
      Video: null,
      UploadedVideo: false,
      Certificate: "",
      CertificateImg: null,
      NewsPaper: "",
      NewsPaperImg: null,
    };
  },
  computed: {
    BaseUrl() {
      return process.env.pic;
    },
  },
  methods: {
    submitEditProfileBasic() {
      // if(this.email !== '' && !this.email.includes('@')){
      //
      //     this.$toast.error('فرمت ایمیل وارد شده درست نیست')
      //
      //
      // }

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
              userId: this.$auth.user.userId,
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
              passportDoc: this.passport,
              videoIdentity: this.Video,
              userSettingId: this.userSettingId,
              newspaperDoc: this.NewsPaperImg,
              lawyerCertificateDoc: this.CertificateImg,
            },
            {}
          )
          .then((response) => {
            if (response.status === 200) {
              this.$axios
                .post(
                  "Users/Update",
                  {
                    name: this.firstName,
                    familyName: this.lastName,
                  },
                  {}
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
        this.passport = this.userInfoData.userSetting.passportDoc;
        this.Certificate = this.userInfoData.userSetting.lawyerCertificateDoc;
        this.NewsPaper = this.userInfoData.userSetting.newspaperDoc;
        this.Video = this.userInfoData.userSetting.videoIdentity;
      });
    },

    getCountryList() {
      this.$axios
        .get(`Country/GetCountriesWithRedis`, null, {})
        .then((response) => {
          if (response.status === 200) {
            this.country = response.data;
          }
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
          console.log(that.nationalCart);
        };
      })(file);
      reader.readAsBinaryString(file);
    },
    // Passport
    callInputMethod2() {
      document.querySelector(".my-file2").click();
    },
    onFileChange2(e) {
      const file = e.target.files[0];
      this.pass = URL.createObjectURL(file);
      const that = this;
      const reader = new FileReader();
      reader.onload = (function (theFile) {
        return function () {
          const binaryData = reader.result;
          that.passport = window.btoa(binaryData);
        };
      })(file);
      reader.readAsBinaryString(file);
    },
    // Video
    callInputMethod3() {
      document.querySelector(".my-file3").click();
    },
    onFileChangeVideo() {
      const that = this;

      that.UploadedVideo = true;

      const f = this.$refs.Video.files[0];
      console.log(f);
      if (f.size > 2000000) {
        this.$toast.error("سایز ویدیو نباید بیشتر از دو مگابایت باشد");
      } else {
        this.BaseVideoUrl = URL.createObjectURL(f);
        const reader = new FileReader();

        reader.onload = (function (theFile) {
          return function () {
            const binaryData = reader.result;
            that.Video = window.btoa(binaryData);
            console.log(that.Video);
          };
        })(f);
        reader.readAsBinaryString(f);
      }
    },
    // Certificate
    callInputMethod4() {
      document.querySelector("#my-file6").click();
    },
    onFileChange4(e) {
      const file = e.target.files[0];
      this.Certificate = URL.createObjectURL(file);
      const that = this;
      const reader = new FileReader();
      reader.onload = (function (theFile) {
        return function () {
          const binaryData = reader.result;
          that.CertificateImg = window.btoa(binaryData);
        };
      })(file);
      reader.readAsBinaryString(file);
    },
    // NewsPaper
    callInputMethod5() {
      document.querySelector("#my-file7").click();
    },
    onFileChange5(e) {
      const file = e.target.files[0];
      this.NewsPaper = URL.createObjectURL(file);
      const that = this;
      const reader = new FileReader();
      reader.onload = (function (theFile) {
        return function () {
          const binaryData = reader.result;
          that.NewsPaperImg = window.btoa(binaryData);
        };
      })(file);
      reader.readAsBinaryString(file);
    },
  },
  mounted() {
    this.getCountryList();
    this.GetMyData();
  },
}
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
  /*margin-right: 2rem;*/
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
