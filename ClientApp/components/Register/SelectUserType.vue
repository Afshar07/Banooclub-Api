<template>
  <div class="d-flex flex-column">
    <div class="d-flex align-items-center gap-2">
      <input @change="emitUserRole" type="radio" id="1" name="type" value="1" />
      <label for="1" class="text-muted">
        به دنبال خدمت میگردم
      </label>
    </div>
    <div class="d-flex align-items-center gap-2 my-2">
      <input @change="emitUserRole" type="radio" id="3" name="type" value="3" />
      <label for="3" class="text-muted">
        ارائه دهنده خدمت هستم
      </label>
    </div>
<!--    <div class="d-flex align-items-center gap-2">-->
<!--      <input @change="emitUserRole" type="radio" id="3" name="type" value="3" />-->
<!--      <label for="3" class="text-muted">-->
<!--        خدمات حقوقی و بانو کلاب ارائه میدهم-->
<!--      </label>-->
<!--    </div>-->
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
