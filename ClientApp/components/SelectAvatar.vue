<template>
  <div class="col-md-12">
    <small class="my-3">انتخاب عکس پروفایل (میتوانید در ویرایش اطلاعات آن را عوض کنید)</small>
    <div class="tw-w-full">
      <div class="tw-w-full tw-flex tw-items-center tw-justify-center">
        <img src="/defaultBanner.jpg" class="tw-w-20 tw-h-20 tw-rounded-full tw-object-cover" alt="">
      </div>
      <div class="tw-w-full tw-flex tw-items-center tw-flex-wrap tw-gap-2 tw-justify-start ">
        <img v-for="i in 8" src="/defaultBanner.jpg" class="tw-w-20 tw-h-20 my-2 tw-rounded-full hover:tw-transform hover:tw-scale-110 tw-transition-all tw-cursor-pointer tw-rounded tw-object-cover"  alt="">
      </div>
      <div class="tw-w-full my-2 tw-flex tw-items-center tw-justify-center">
        <button @click="SendAvatarName" class="bg-purple rounded text-white tw-w-1/4 p-2 tw-cursor-pointer"> تایید</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "SelectBanner",
  methods:{
    SendAvatarName(){
      this.$emit('SendAvatarPic')
    }
  }
}
</script>

<style scoped>

</style>
