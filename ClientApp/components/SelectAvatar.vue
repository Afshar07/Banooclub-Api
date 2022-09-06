<template>
  <div class="col-md-12">
    <small v-if="$route.path!=='/social/accountsetting/MyPage'" class="my-3">انتخاب عکس پروفایل (میتوانید در ویرایش اطلاعات آن را عوض کنید)</small>
    <div class="tw-w-full">
      <div v-if="$route.path!=='/social/accountsetting/MyPage'" class="tw-w-full tw-flex tw-items-center  tw-justify-center border-bottom pb-3">
        <img :src="`/DefaultAvatars/${SelectedAvatar}`" class="tw-w-20 tw-h-20 tw-rounded-full tw-object-cover" alt="">
      </div>
      <div v-if="$route.path==='/social/accountsetting/MyPage'"   @click="callInputMethod" class="tw-w-full tw-cursor-pointer tw-bg-stone-200 tw-rounded tw-border-dashed tw-border-2 tw-flex tw-items-center tw-justify-center tw-h-20 my-4">
        <UploadIcon  class="tw-fill-pink-400"></UploadIcon>
      </div>
      <div  v-if="$route.path!=='/social/accountsetting/MyPage'" class="tw-w-full tw-flex tw-items-center tw-flex-wrap tw-gap-2 tw-justify-center ">
        <img v-for="(item,idx) in 16" :src="`/DefaultAvatars/defaultAvatar${idx+1}.png`" @click="SetDefaultAvatar(idx+1)" class="tw-w-20 tw-h-20 my-2 tw-rounded-full hover:tw-transform hover:tw-scale-110 tw-transition-all tw-cursor-pointer tw-rounded tw-object-cover"  alt="">
      </div>
      <div v-if="$route.path!=='/social/accountsetting/MyPage'" class="tw-w-full my-2 tw-flex tw-items-center tw-justify-center">
        <button @click="SendAvatarName" class="bg-purple rounded text-white tw-w-1/4 p-2 tw-cursor-pointer"> تایید</button>
      </div>
    </div>
  </div>
</template>

<script>
import UploadIcon from "@/components/Icons/UploadIcon";
export default {
  name: "SelectBanner",
  components:{UploadIcon},
  data(){
    return{
      SelectedAvatar:'defaultAvatar1.png',
      AvatarIdx:0
    }
  },
  methods:{
    callInputMethod(){
      this.$emit('SetUserPic')
    },
    SendAvatarName(){
      this.$emit('SendAvatarPic',`defaultAvatar${this.AvatarIdx}.png`)
    },
    SetDefaultAvatar(idx){
     this.SelectedAvatar = `defaultAvatar${idx}.png`
      this.AvatarIdx = idx
    }
  }
}
</script>

<style scoped>

</style>
