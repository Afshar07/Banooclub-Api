<template>
  <div class="row mb-4 d-none d-lg-block m-0 col-12">
    <div
      class="col-12 text-center"
      v-if="this.$store.state.HeaderData.status === 2"
    >
      <p class="text-warning">صفحه این کاربر خصوصی است</p>
    </div>
    <div
      class="d-flex flex-column text-end pb-4 w-100 mx-auto mb-4 rounded"
      v-if="!this.$store.state.HeaderData.status"
    >
      <p class="titleSection">اعلانات</p>

      <div class="">
        <div class="d-flex flex-row">
          <div class="px-3">
            <div class="d-flex flex-column">
              <div class="">
                <div class="d-flex flex-row mb-1 gap-2" style="font-size: 12px">
                  <div class="">
                    <i class="far fa-comment"></i>
                  </div>
                  <div class=" ">پیام :</div>
                  <div class="">0</div>
                </div>
              </div>
              <div class="">
                <div class="d-flex flex-row mb-1 gap-2" style="font-size: 12px">
                  <div class="">
                    <i class="far fa-bell"></i>
                  </div>
                  <div class="">تیکت های خوانده نشده :</div>
                  <div class="">{{ PageData.unreadTicketCount }}</div>
                </div>
              </div>
              <div class="">
                <div class="d-flex flex-row mb-1 gap-2" style="font-size: 12px">
                  <div class="">
                    <i class="far fa-eye"></i>
                  </div>
                  <div class="">کل بازدید ها :</div>
                  <div class="">{{ PageData.logCounts }}</div>
                </div>
              </div>
              <div class="">
                <div class="d-flex flex-row mb-1 gap-2" style="font-size: 12px">
                  <div class="">
                    <i class="far fa-eye"></i>
                  </div>
                  <div class="">کسانی که از پروفایل شما بازدید کرده اند :</div>

                  <div class="">{{ PageData.logCountsDistinct }}</div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="row text-center mx-2">
          <div class="">
            <div class="users-thumb-list">
              <a
                v-for="item in PageData.lastViewers"
                :key="item.userId"
                href="#"
                v-tooltip="{ content: item.name + ' ' + item.familyName }"
                data-toggle="tooltip"
              >
                <img
                  :src="BaseUrl + item.selfie"
                  width="40px"
                  height="40px"
                  alt=""
                />
              </a>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "YourPage",
  data() {
    return {
      showLike: false,
      showView: true,
      Selfie: "",
    };
  },
  mounted() {
    this.Selfie = this.$store.state.HeaderData.selfie;
  },

  computed: {
    PageData() {
      return this.$store.state.HeaderData;
    },
    selfie() {
      return this.$store.state.HeaderData.selfie;
    },
    BaseUrl() {
      return process.env.pic;
    },
  },
  methods: {
    showingLike() {
      this.showView = false;
      this.showLike = true;
    },
    showingView() {
      this.showLike = false;
      this.showView = true;
    },
  },
};
</script>

<style scoped>
.titleSection {
  /*border-bottom: 2px solid #088dcd;*/
  background: rgba(0, 0, 0, 0) none repeat scroll 0 0;
  color: #424242;
  display: inline-block;
  font-size: 1.25rem;
  font-weight: 600;
  margin-bottom: 30px;
  padding: 20px 0 10px;
  position: relative;
  line-height: 1.75rem;
  text-transform: uppercase;
  width: max-content;
}
.myPicture {
  margin-bottom: 20px;
  width: 40px;
  height: 40px;
  border-radius: 50%;
}

.titleSection {
  /*border-bottom: 2px solid #088dcd;*/
  background: rgba(0, 0, 0, 0) none repeat scroll 0 0;
  color: #424242;
  display: inline-block;
  font-size: 1.25rem;
  font-weight: 600;
  margin-bottom: 30px;
  padding: 20px 0 10px;
  position: relative;
  text-transform: uppercase;
  width: max-content;
}

.myPage {
  color: #088dcd;
  font-size: 16px;
  text-transform: capitalize;
  margin-bottom: 12px;
}

.myPage:hover {
  border-bottom: 1px solid #2fa7cd;
}

.firstSectionBox {
  margin: 0 auto;
  background: #fdfdfd;
  border: 1px solid #ede9e9;
  border-radius: 3px;
  width: 80%;
}

.likeViewText {
  color: green;
  float: left;
  font-size: 13px;
  text-align: center;
  text-transform: capitalize;
}

.likeViewText:hover {
  border-bottom: 1px solid green;
}

.deActiveTab {
  background: #eeeeee none repeat scroll 0 0;
  font-size: 11px;
  padding: 5px 0;
  text-transform: uppercase;
  margin: 16px 0;
  cursor: pointer;
}

.deActiveTab:hover {
  color: #2fa7cd;
}

.numberView {
  font-size: 22px;
  font-style: normal;
  text-align: center;
  color: #545454;
  font-weight: 400;
  line-height: 1.5;
}

.users-thumb-list {
  float: left;
  text-align: center;
  width: 100%;
  margin-top: 20px;
  margin-bottom: 20px;
}

.users-thumb-list > a {
  display: inline-block;
  margin-left: -17px;
}

.users-thumb-list > a:first-child {
  margin-right: 0;
}

.users-thumb-list > a img {
  border: 2px solid #fff;
  border-radius: 50%;
}

.TabActive {
  background: #088dcd;
  color: white;
  font-size: 11px;
  padding: 5px 0;
  margin: 16px 0;
  text-transform: uppercase;
  cursor: pointer;
}

@media (max-width: 576px) {
  .firstSectionBox,
  .bgWidget {
    width: 100%;
  }
}
</style>
