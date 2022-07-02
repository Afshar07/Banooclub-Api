<template>
  <div class="row mb-4 d-none d-lg-block m-0 col-12">
    <div class="col-12 text-end">
      <span class="titleSection">فعالیت های اخیر</span>
    </div>
    <div
      class="d-flex flex-column text-end"
      v-if="this.$store.state.HeaderData.status === 2"
    >
      <div class="">
        <p class="text-warning">صفحه این کاربر خصوصی است</p>
      </div>
    </div>
    <div
      class="d-flex flex-column text-end"
      v-if="
        !this.$store.state.HeaderData.status ||
        this.$store.state.HeaderData.status === 1
      "
    >
      <div class="" v-for="item in PageData" :key="item.activityId">
        <div class="d-flex flex-column activityBox text-end">
          <div class="timeText">
            {{ item.createDate | moment("jYYYY-jMM-jDD HH:mm") }}
          </div>
          <div class="activityText">
            {{
              item.type === 1
                ? "آپدیت پروفایل"
                : item.type === 2
                ? "ایجاد پست جدید"
                : item.type === 3
                ? "بروزرسانی پست"
                : item.type === 4
                ? "حذف پست"
                : item.type === 5
                ? "لایک کردن پست"
                : item.type === 6
                ? "کامنت گذاشتن برای پست"
                : item.type === 7
                ? "ساختن آگهی"
                : item.type === 8
                ? "بروز رسانی آگهی"
                : item.type === 9
                ? "حذف آگهی"
                : item.type === 10
                ? "کامنت گذاشتن برای آگهی"
                : item.type === 11
                ? "ارسال درخواست دوستی"
                : item.type === 12
                ? "دنبال کردن"
                : item.type === 1
                ? "آپدیت پروفایل"
                : item.type === 13
                ? "دنبال شدن"
                : item.type === 14
                ? "گزارش پست"
                : " ایجاد تیکت جدید"
            }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "LastActivity",
  data() {
    return {
      activityData: [],
      userId: null,
    };
  },

  computed: {
    PageData() {
      return this.$store.state.HeaderData.recentActivity;
    },
  },
  mounted() {},

  methods: {
    CheckPrivate() {
      if (this.$store.state.HeaderData.status === 2) {
        return true;
      } else if (!this.$store.state.HeaderData.status) {
        return false;
      }
    },
  },
};
</script>

<style scoped>
.SectionBox {
  margin: 30px auto 0 auto;
  width: 80%;
  background: #fdfdfd;
  border: 1px solid #ede9e9;
  border-radius: 3px;
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
.titleSidebarSection {
  border-bottom: 2px solid #088dcd;
  background: rgba(0, 0, 0, 0) none repeat scroll 0 0;
  color: #545454;
  display: inline-block;
  font-size: 13px;
  margin-bottom: 30px;
  padding: 20px 20px 10px;
  text-transform: uppercase;
}

.timeText {
  color: #999;
  font-size: 11px;
  margin-left: 5px;
  overflow: hidden;
  text-overflow: ellipsis;
  vertical-align: middle;
  white-space: nowrap;
  direction: rtl;
}

.activityBox {
  border-right: 2px solid #088dcd;
  margin-bottom: 20px;
  padding-right: 16px;
}

.activityText {
  cursor: pointer;
  font-weight: 400;
  line-height: 1.5;
  font-size: 13px;
}

.activityText:hover {
  color: #088dcd;
}

.activityAuthor {
  cursor: pointer;
  font-size: 13px;
  margin-bottom: 0;
  margin-top: 7px;
  color: #088dcd;
}

@media (max-width: 576px) {
  .SectionBox {
    width: 100%;
    margin-top: 7rem;
  }
}
</style>
