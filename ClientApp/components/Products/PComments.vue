<template>
  <div class="d-flex flex-column">
    <div class="d-flex flex-column tw-py-1" >
      <div class="tw-flex tw-py-1">
        <div class="tw-w-10 tw-h-10 tw-rounded-full tw-relative tw-flex-shrink-0">
          <img style="width: 40px;height: 40px;object-fit: contain;" v-if="Comment.userInfo.selfieFileData" :src="`https://banooclubapi.oregonserver.com/${Comment.userInfo.selfieFileData}`" alt="" class="tw-absolute tw-rounded-full">
          <img v-else :src="userDefault" alt="" class="tw-absolute tw-h-full tw-rounded-full tw-w-full">
        </div>
        <div>
          <div class="tw-text-gray-700 tw-py-2 tw-px-3 tw-rounded-md tw-bg-gray-100 tw-relative lg:tw-mr-5 tw-mr-2 lg:tw-ml-12">
            <div class="tw-absolute tw-w-3 tw-h-3 top-3 tw--right-1 tw-bg-gray-100 tw-transform tw-rotate-45"></div>
            <p class="tw-leading-6 tw-flex justify-content-center align-items-center mb-0">
              {{Comment.text}}
            </p>
          </div>
          <div class="tw-text-sm tw-flex tw-items-center tw-mt-2 tw-ml-5 mx-3">
            <!--                    <button class="tw-flex tw-text-red-600 tw-ml-2">-->
            <!--                      <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-5 tw-w-5 mx-1" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">-->
            <!--                        <path stroke-linecap="round" stroke-linejoin="round" d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" />-->
            <!--                      </svg>-->
            <!--                      لایک-->
            <!--                    </button>-->
            <button @click="replyAComment(Comment)" class="mx-2 hover:tw-text-blue-500"> پاسخ </button>
            <!--                    <span class="mx-2"> 3 روز </span>-->
          </div>
        </div>
      </div>
        <div  class="tw-flex tw-py-3 tw-pr-5" v-for="(el,idx) in Comment.filter(e=> e.baseId === Comment.serviceCommentId)" :key="idx" >
          <div class="tw-w-10 tw-h-10 tw-rounded-full tw-relative tw-flex-shrink-0">
            <img :src="`https://banooclubapi.oregonserver.com/${el.userInfo.selfieFileData}`" alt="" class="tw-absolute tw-h-full tw-rounded-full tw-w-full">
          </div>
          <div>
            <div class="tw-text-gray-700 tw-py-2 tw-px-3 tw-rounded-md tw-bg-gray-100 tw-relative lg:tw-mr-5 tw-mr-2 lg:tw-ml-12">
              <div class="tw-absolute tw-w-3 tw-h-3 top-3 tw--right-1 tw-bg-gray-100 tw-transform tw-rotate-45"></div>
              <p class="tw-leading-6 tw-flex justify-content-center align-items-center mb-0">
                {{el.text}}
              </p>
            </div>
            <div class="tw-text-sm tw-flex tw-items-center tw-mt-2 tw-ml-5 mx-3">
              <button class="tw-flex tw-text-red-600 tw-ml-2">
                <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-5 tw-w-5 mx-1" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
                  <path stroke-linecap="round" stroke-linejoin="round" d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" />
                </svg>
                لایک
              </button>
              <button @click="replyAComment(el)" class="mx-2"> پاسخ </button>
              <span class="mx-2"> 3 روز </span>
            </div>
          </div>
        </div>
    </div>

  </div>
</template>

<script>
export default {
  name: "PComments",
  props:['Comment'],
  data(){
    return{
      userDefault: require("~/assets/images/defaultUser.png"),
    }
  },

  computed:{

  },

  methods:{
    replyAComment(comment){

      const selected_comment = comment
        let PlaceHolder = ''
      let parentId = 0
      let BaseId = 0

      console.log(selected_comment)
      // this.want_to_reply = true
      PlaceHolder='در حال پاسخ به ' +selected_comment.userInfo.userName + ' هستید'
      if(comment.parentId === 0 && comment.baseId === 0 ){
        parentId = selected_comment.serviceCommentId
        BaseId = selected_comment.serviceCommentId
      }else if(comment.baseId!==0){
     parentId = selected_comment.serviceCommentId
        BaseId = selected_comment.baseId
      }

      this.$emit('ReplyComment',PlaceHolder,parentId,BaseId)
    },
  }
}
</script>

<style scoped>

</style>
