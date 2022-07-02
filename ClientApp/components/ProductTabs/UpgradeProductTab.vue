<template>
  <div class="row">
    <div class="col-md-1"></div>
    <div class="col-md-10 col-sm-12">
      <div class="d-flex flex-column">
        <div class="d-flex flex-column overflow-scroll px-1" style="max-height: 560px">
          <UpgradeItem class="my-3"/>
          <UpgradeItem class="my-3"/>
          <UpgradeItem class="my-3"/>
          <UpgradeItem class="my-3"/>
          <UpgradeItem class="my-3"/>
        </div>
        <div class="col-12 pt-3 mt-auto">
          <div class="d-flex justify-content-between align-items-center">
            <div>جمع کل: 30000 تومان</div>
            <button type="button" class="button">
              پرداخت
            </button>
          </div>

        </div>
      </div>
    </div>
    <div class="col-md-1"></div>
  </div>

</template>

<script>
import UpgradeItem from "../Products/UpgradeItem";
export default {
  name: "UpgradeProductTab",
  components: {UpgradeItem}
}
</script>

<style scoped>

</style>
