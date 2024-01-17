<template>
  <div class="d-flex flex-row justify-content-center gap-5 w-100 overflow-scroll text-nowrap">
    <div class="">
      <div class="d-flex flex-row align-items-center titleTab small" :class="activeTabClass('followers')"  @click="toggleTab('followers')">
          دنبال کنندگان
        <div class=" follower_count d-flex align-items-center justify-content-center">
          {{ followerCount }}
        </div>
      </div>
    </div>
    <div class="">
      <div class="d-flex flex-row align-items-center titleTab small" :class="activeTabClass('following')"  @click="toggleTab('following')">
          دنبال شوندگان
        <div class=" follower_count d-flex align-items-center justify-content-center">
          {{ followingCount }}
        </div>
      </div>
    </div>
    <div class="" v-if="in_my_profile">
      <div class="d-flex flex-row align-items-center titleTab small" :class="activeTabClass('friendReqs')"  @click="toggleTab('friendReqs')">
          پیشنهادات دوستی
        <div class=" follower_count d-flex align-items-center justify-content-center">
          {{ followReqsCount }}
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  emits: ["changeTab"],
  data() {
    return {
      activeTab: "followers",
    };
  },
  props: {
    in_my_profile:{
      type:Boolean,
      required: false,
      default: 0,
    },
    followerCount: {
      type: [Number, String],
      required: false,
      default: 0,
    },
    followingCount: {
      type: [Number, String],
      required: false,
      default: 0,
    },
    followReqsCount: {
      type: [Number, String],
      required: false,
      default: 0,
    },
  },
  methods: {
    toggleTab(tab) {
      this.activeTab = tab;
      this.$emit("changeTab", tab);
    },
    activeTabClass(tab) {
      if (this.activeTab == tab) {
        return "active-tab";
      }
    },
  },
};
</script>

<style scoped>
.active-tab {
  color: #0d6efd;
  border-bottom: 3px solid #0d6efd;
  font-weight: 500;
}
.titleTab {
  /*color: #545454;*/
  font-weight: 500;
  line-height: 1.5;
  cursor: pointer;
  padding: 5px 9px 15px;
}

.numberTitle {
  border: 1px solid #ccc;
  border-radius: 30%;
  display: inline-block;
  font-size: 14px;
  line-height: initial;
  margin-right: 5px;
  text-align: center;
  width: 25px;
  color: #8d8d8d;
}
.follower_count {
  padding: 5px 9px;
  background: rgba(42, 65, 232, 0.08);
  border-radius: 30px;
  font-weight: 500;
  font-size: 12px;
  margin-right: 6px;
  color: #2a41e8;
}
</style>
