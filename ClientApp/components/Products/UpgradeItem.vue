<template>
  <div class="custom_card d-flex justify-content-between align-items-center p-5" @click="toggleCheckbox">
    <div>نردبان</div>
    <div>30000 تومان</div>
    <input style="width: 50px;height: 15px;" type="checkbox" :checked="checked">
  </div>

</template>

<script>
export default {
  name: "UpgradeItem",
  data(){
    return{
      checked:false
    }
  },
  methods:{
    toggleCheckbox(){
      this.checked = !this.checked
    }
  }
}
</script>

<style scoped>

</style>
