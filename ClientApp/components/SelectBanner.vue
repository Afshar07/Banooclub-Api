
<template>
  <div class="col-md-12">
    <small class="my-3">انتخاب عکس جلد (میتوانید در ویرایش اطلاعات آن را عوض کنید)</small>
    <div class="tw-w-full ">
      <img src="/defaultBanner.jpg" class="tw-w-full tw-h-20 tw-rounded tw-object-cover" alt="">

      <div class="tw-w-full tw-flex tw-items-center tw-flex-wrap tw-justify-start ">
        <img v-for="i in 8" src="/defaultBanner.jpg" class="tw-w-1/4 tw-h-20 my-2 hover:tw-transform hover:tw-scale-110 tw-transition-all tw-cursor-pointer tw-rounded tw-object-cover"  alt="">

      </div>

      <!--              @click="callUserInputMethod"-->
      <div @click="SendBannerName" class="tw-w-full my-2 tw-flex tw-items-center tw-justify-center">
        <button class="bg-purple rounded text-white tw-w-1/4 p-2 tw-cursor-pointer"> تایید</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "SelectAvatar",
  methods:{
    SendBannerName(){
      this.$emit('SendBannerPic')
    }
  }
}
</script>

<style scoped>

</style>
