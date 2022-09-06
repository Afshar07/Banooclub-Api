<template>
  <div class="col-12 mt-3">
    <input v-model="payload.introducerCode" type="text" placeholder="کد معرف" class="with-border">
    <div class="tw-w-full my-2 tw-flex tw-items-center tw-justify-center">
      <button @click="SendIntroducerCode" class="bg-purple rounded text-white tw-w-1/4 p-2 tw-cursor-pointer"> تایید</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "GetIntroducerCode",
  data(){
    return{
      payload:{

      introducerCode:''
      }
    }
  },
  methods:{
    SendIntroducerCode(){
      this.$emit('SendIntCode',this.payload)
    }
  }
}
</script>

<style scoped>

</style>
