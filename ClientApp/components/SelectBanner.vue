<template>
  <div>

    <small v-if="$route.path!=='/social/accountsetting/MyPage'" class="my-3">انتخاب عکس جلد (میتوانید در ویرایش اطلاعات
      آن را عوض کنید)</small>
    <div class="tw-w-full ">
      <img :src="`/DefaultBanners/${SelectedBanner}`" alt="" class="tw-w-full tw-h-20 tw-rounded tw-object-cover">
      <div class="tw-w-full tw-flex tw-items-center gap-1  tw-flex-wrap tw-justify-center ">
        <img v-for="(item,idx) in 16" :src="`/DefaultBanners/defaultBanner${idx+1}.jpg`"
             alt=""
             class="tw-w-1/4 tw-h-20 my-2 hover:tw-transform hover:tw-scale-110 tw-transition-all tw-cursor-pointer tw-rounded tw-object-cover"
             @click="SetDefaultBanner(idx+1)">

      </div>
      <div v-if="$route.path==='/social/accountsetting/MyPage'"
           class="tw-w-full tw-bg-stone-200 tw-rounded tw-border-dashed tw-border-2 tw-flex tw-items-center tw-justify-center tw-h-20 my-4"
           @click="CallBanner">
        <UploadIcon class="tw-fill-pink-400"></UploadIcon>
      </div>
      <div class="tw-w-full my-2 tw-flex tw-items-center tw-justify-center" @click="SendBannerName">
        <button class="bg-purple rounded text-white tw-w-1/4 p-2 tw-cursor-pointer"> تایید</button>
      </div>
    </div>
  </div>
</template>

<script>
import UploadIcon from "@/components/Icons/UploadIcon";

export default {
  name: "SelectAvatar",
  components: {UploadIcon},
  data() {
    return {
      SelectedBanner: null,
      BannerIdx: 0
    }
  },
  mounted() {

    this.BannerIdx = Math.round(Math.random() * (16 - 1) + 1)
    this.SelectedBanner = `defaultBanner${this.BannerIdx}.jpg`

  },
  methods: {
    CallBanner() {
      this.$emit('SetBannerPic')
    },
    SendBannerName() {
      this.$emit('SendBannerPic', `defaultBanner${this.BannerIdx}.jpg`)

    },
    SetDefaultBanner(idx) {
      this.SelectedBanner = `defaultBanner${idx}.jpg`
      this.BannerIdx = idx
    }
  }
}
</script>

<style scoped>

</style>
