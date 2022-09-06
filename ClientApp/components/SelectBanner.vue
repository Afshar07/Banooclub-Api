
<template>
  <div class="col-md-12">
    <small v-if="$route.path!=='/social/accountsetting/MyPage'" class="my-3">انتخاب عکس جلد (میتوانید در ویرایش اطلاعات آن را عوض کنید)</small>
    <div class="tw-w-full ">
      <img v-if="$route.path!=='/social/accountsetting/MyPage'" :src="`/DefaultBanners/${SelectedBanner}`" class="tw-w-full tw-h-20 tw-rounded tw-object-cover" alt="">

      <div v-if="$route.path!=='/social/accountsetting/MyPage'" class="tw-w-full tw-flex tw-items-center tw-flex-wrap tw-justify-start ">
        <img v-for="(item,idx) in 16" :src="`/DefaultBanners/defaultBanner${idx+1}.jpg`"  @click="SetDefaultBanner(idx+1)" class="tw-w-1/4 tw-h-20 my-2 hover:tw-transform hover:tw-scale-110 tw-transition-all tw-cursor-pointer tw-rounded tw-object-cover"  alt="">

      </div>

      <div @click="CallBanner"
           class="tw-w-full tw-bg-stone-200 tw-rounded tw-border-dashed tw-border-2 tw-flex tw-items-center tw-justify-center tw-h-20 my-4">
        <UploadIcon class="tw-fill-pink-400"></UploadIcon>
      </div>
      <div v-if="$route.path!=='/social/accountsetting/MyPage'" @click="SendBannerName" class="tw-w-full my-2 tw-flex tw-items-center tw-justify-center">
        <button class="bg-purple rounded text-white tw-w-1/4 p-2 tw-cursor-pointer"> تایید</button>
      </div>
    </div>
  </div>
</template>

<script>
import UploadIcon from "@/components/Icons/UploadIcon";
export default {
  name: "SelectAvatar",
  components:{UploadIcon},
  data(){
    return{
      SelectedBanner:'defaultAvatar1.png',
      BannerIdx:0
    }
  },

  methods:{
    CallBanner(){
      this.$emit('SetBannerPic')
    },
    SendBannerName(){
      this.$emit('SendBannerPic',`defaultBanner${this.BannerIdx}.jpg`)

    },
    SetDefaultBanner(idx){
      this.SelectedBanner = `defaultBanner${idx}.jpg`
      this.BannerIdx = idx
    }
  }
}
</script>

<style scoped>

</style>
