<template>
  <div class="container-fluid">
    <div
      class="row"
      v-if="
        this.$store.state.HeaderData.status === 1 ||
        this.$store.state.HeaderData.status === 4
      "
    >
      <div class="col-md-12 my-4">
        <div class="row bg-white p-4">
          <div class="col-md-10 d-flex justify-content-center mb-3 mb-md-0">
            <div class="d-flex align-items-center gap-5">
              <nuxt-link
                tag="span"
                :to="{
                  path: '/Social/UserFollowings',
                  query: { userId: $store.state.SearchedUser },
                }"
              >
                <div
                  class="d-flex text-dark align-items-center flex-column"
                  style="cursor: pointer"
                >
                  <strong class="text-decoration-none">{{
                    userInfoData.followingsCount
                  }}</strong>
                  <small>دنبال شوندگان</small>
                </div>
              </nuxt-link>
              <nuxt-link
                tag="span"
                :to="{
                  path: '/Social/UserFollowers',
                  query: { userId: $store.state.SearchedUser },
                }"
              >
                <div
                  class="d-flex align-items-center text-dark flex-column"
                  style="cursor: pointer"
                >
                  <strong class="text-decoration-none">{{
                    userInfoData.followersCount
                  }}</strong>
                  <small>دنبال کنندگان</small>
                </div>
              </nuxt-link>
            </div>
          </div>
          <div class="col-md-2">
            <img
              :src="BaseUrl + userInfoData.selfieFileData"
              class="img-fluid rounded-circle"
              alt=""
            />
          </div>

          <div class="col-md-12 my-4">
            <div class="row">
              <h4 class="my-4 mt-md-0 text-center text-md-end">اطلاعات کلی</h4>
              <div class="col-md-12 my-4">
                <div class="d-flex align-items-center gap-2 singlePersonalItem">
                  <i class="fas fa-user"></i>
                  <small>نام کاربری : </small>
                  <small class="text-muted">{{ userInfoData.userName }}</small>
                </div>
              </div>

              <div class="col-md-12 my-4">
                <div class="d-flex align-items-center gap-2 singlePersonalItem">
                  <i class="fas fa-user"></i>
                  <small>نام و نام خانوادگی : </small>
                  <small class="text-muted">{{
                    userInfoData.name + " " + userInfoData.familyName
                  }}</small>
                </div>
              </div>
              <div class="col-md-12 my-4">
                <div class="d-flex align-items-center gap-2 singlePersonalItem">
                  <i class="fas fa-phone"></i>
                  <small>شماره تماس : </small>
                  <small class="text-muted">{{ userInfoData.mobile }}</small>
                </div>
              </div>
              <div class="col-md-12 my-4">
                <div class="d-flex align-items-center gap-2 singlePersonalItem">
                  <i class="far fa-envelope"></i>
                  <small>ایمیل : </small>
                  <small class="text-muted">{{
                    userInfoData.email
                      ? userInfoData.email
                      : "ایمیلی ثبت نشده است"
                  }}</small>
                </div>
              </div>
              <div
                class="col-md-12 my-4"
                v-if="
                  userInfoData.userSetting && userInfoData.userSetting.gender
                "
              >
                <div class="d-flex align-items-center gap-2 singlePersonalItem">
                  <i class="fas fa-venus-mars"></i>
                  <small>جنسیت : </small>
                  <small class="text-muted">{{
                    userInfoData.userSetting.gender === 1
                      ? "مرد"
                      : userInfoData.userSetting.gender === 2
                      ? "زن"
                      : userInfoData.userSetting.gender === 3
                      ? "سایر"
                      : userInfoData.userSetting.gender === 4
                      ? "دگر جنس گرای غالب و همجنسگرای نادر"
                      : userInfoData.userSetting.gender === 5
                      ? "دگرجنسگرای غالب و گاهی همجنسگرا"
                      : userInfoData.userSetting.gender === 6
                      ? "به طور مساوی دگرجنسگرا و همجنسگرا"
                      : userInfoData.userSetting.gender === 7
                      ? "همجنسگرای غالب و دگرجنسگرای نادر"
                      : userInfoData.userSetting.gender === 8
                      ? "همجنسگرای غالب و گاهی دگرجنسگرا"
                      : ""
                  }}</small>
                </div>
              </div>
              <div class="col-md-12 my-4">
                <div class="d-flex align-items-center gap-2 singlePersonalItem">
                  <i class="fas fa-question"></i>
                  <small>نوع کاربر : </small>
                  <small class="text-muted">{{
                    userInfoData.type === 1
                      ? "کاربر معمولی"
                      : userInfoData.type === 2
                      ? "ادمین"
                      : userInfoData.type === 3
                      ? "وکیل"
                      : userInfoData.type === 4
                      ? "کاربر تجربی"
                      : ""
                  }}</small>
                </div>
              </div>
              <hr />
              <div class="row">
                <div class="col-md-12 my-4 d-flex flex-column">
                  <div
                    class="d-flex align-items-center gap-2 singlePersonalItem"
                  >
                    <i class="far fa-comment"></i>
                    <small>بیوگرافی : </small>
                  </div>
                  <small
                    v-if="userInfoData.userSetting"
                    class="text-muted mt-3"
                    >{{
                      userInfoData.userSetting.bio
                        ? userInfoData.userSetting.bio
                        : "این کاربر بیوگرافی ثبت نکرده است"
                    }}</small
                  >
                </div>
                <div class="col-md-12 my-4">
                  <div
                    class="d-flex align-items-center gap-2 singlePersonalItem"
                  >
                    <i class="far fa-calendar-alt"></i>
                    <small>تاریخ تولد : </small>
                    <small v-if="userInfoData.userSetting" class="text-muted">{{
                      userInfoData.userSetting.birthDate
                        | moment("jYYYY/jMM/jDD")
                    }}</small>
                  </div>
                </div>
                <div class="col-md-12 my-4">
                  <div
                    class="d-flex align-items-center gap-2 singlePersonalItem"
                  >
                    <i class="fas fa-hashtag"></i>
                    <small>تگ ها : </small>
                    <span
                      v-if="userInfoData.userSetting"
                      class="badge bg-secondary"
                      >{{ userInfoData.userSetting.userTag }}</span
                    >
                  </div>
                </div>
              </div>
            </div>

            <br />
          </div>
        </div>
      </div>
    </div>
    <div
      v-else-if="this.$store.state.HeaderData.status === 2"
      class="row boxMainContent text-warning position-relative bg-white p-3"
    >
      <p class="text-center">
        صفحه این کاربر خصوصی است برای مشاهده آن درخواست دوستی ارسال کنید
      </p>
    </div>
    <div
      v-else-if="this.$store.state.HeaderData.status === 3"
      class="row boxMainContent text-warning position-relative bg-white p-3"
    >
      <p class="text-center">
        برای مشاهده صفحه این کاربر ابتدا وارد اکانت خود شوید
      </p>
    </div>
  </div>
</template>

<script>
export default {
  layout: "PoshtebamPlusLayout",
  name: "PersonalInfo",
  fetchOnServer() {
    return true;
  },
  async fetch() {
    try {
      const response = await this.$repositories.getAUserById.getAUserById(
        this.$route.params.id
      );
      this.userInfoData = response.data;
    } catch (error) {
      console.log(error);
    }
  },
  data() {
    return {
      userInfoData: "",
      userId: this.$route.params.id,
    };
  },

  computed: {
    BaseUrl() {
      return process.env.pic;
    },
  },
};
</script>

<style scoped></style>
