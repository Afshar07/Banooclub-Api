<template>
  <div class="custom_card Bg-Pink d-flex justify-content-between align-items-center p-4" @click="toggleCheckbox">
    <div>{{ upgradeItem.title }}</div>
    <div>
      {{Intl.NumberFormat('fa-IR').format(upgradeItem.price)}}
      تومان
    </div>
    <input  style="width: 50px;height: 15px;" type="checkbox" :checked="checked">
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
  watch:{
    checked(val){
      if(val){
        this.$emit('addOrder',this.upgradeItem)
      }
      else{
        this.$emit('deleteOrder',(this.upgradeItem))
      }

    }
  },
  props:{
    upgradeItem:{
      type: Object,
      required: true
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
