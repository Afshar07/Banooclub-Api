<template>
  <div class="col-12 form-group mt-3">
    <div class="form-floating text-end">
      <input
        type="text"
        class="with-border "
        id="otp"
        placeholder="کد تایید"
        v-model.trim="payload.verifyCode"
      />
      <div class="tw-w-full my-2 tw-flex tw-items-center tw-justify-center">
        <button @click="SendIntroducerCode" class="bg-purple rounded text-white tw-w-1/4 p-2 tw-cursor-pointer"> تایید</button>
      </div>
    </div>
  </div>

</template>

<script>
export default {
  name: "GetIntroducerCode",
  data(){
    return{
      payload:{

      verifyCode:''
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
