<template>
  <div class="d-flex flex-column">
    <div class="d-flex align-items-center gap-2">
      <input @change="emitUserRole" type="radio" id="1" name="type" value="1" />
      <label for="1" class="text-muted">
        به دنبال مهاجرت هستم
      </label>
    </div>
    <div class="d-flex align-items-center gap-2 my-2">
      <input @change="emitUserRole" type="radio" id="4" name="type" value="4" />
      <label for="4" class="text-muted">
        تجربه مهاجرتم رو به اشتراک میگذارم
      </label>
    </div>
    <div class="d-flex align-items-center gap-2">
      <input @change="emitUserRole" type="radio" id="3" name="type" value="3" />
      <label for="3" class="text-muted">
        خدمات حقوقی و مهاجرت ارائه میدهم
      </label>
    </div>
  </div>
</template>

<script>
export default {
  emits: ["getUserRole"],
  data() {
    return {
      isTypeSelected: false,
    };
  },
  methods: {
    emitUserRole(event) {
      this.$emit("getUserRole", event.target.value);
      this.isTypeSelected = true;
    },
  },
};
</script>
<style scoped>
.TextWhite {
  color: white !important;
}
</style>
