<template>
  <div class="sidenav-container">
    <div
      v-if="show"
      class="sidenav-backdrop"
      @click="$emit('close')"></div>
    <transition name="slide-side">
      <div
        v-if="show"
        class="sidenav">
        <div class="row">
          تنظیمات عمومی
        </div>
      </div>
    </transition>
  </div>
</template>

<script>
export default {
  name: "SuperAdminSideNav",
  props: {
    show: {
      type: Boolean,
      default: false
    }
  }
}
</script>

<style scoped>
.sidenav-container {
  height: 100%;
  width: 100%;
  direction: rtl;
}

.sidenav-backdrop {
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.7);
  z-index: 1000;
  position: fixed;
  top: 0;
  left: 0;
}

@media (max-width: 576px) {
  .sidenav {
    height: 100%;
    width: 350px;
    background-color: white;
    z-index: 10000;
    position: fixed;
    top: 0 !important;
    left: 0;
    box-sizing: border-box;
    padding: 30px;
  }
}

.sidenav {
  height: 100%;
  width: 350px;
  background-color: white;
  z-index: 10000;
  position: fixed;
  top: 0;
  left: 0;
  box-sizing: border-box;
  padding: 30px;
}

.slide-side-enter-active,
.slide-side-leave-active {
  transition: all 0.3s ease-out;
}

.slide-side-enter,
.slide-side-leave-to {
  transform: translateX(-100%);
}
</style>
