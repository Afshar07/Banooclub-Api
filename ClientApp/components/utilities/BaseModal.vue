<template>
  <div class="custom_container w-100 h-100 tw-absolute tw-top-0 tw-z-[999999]">
    <div @click="closeModal" class="tw-fixed tw-w-screen tw-h-screen tw-top-0 tw-left-0 tw-bg-gray-200 tw-bg-opacity-80"></div>
    <div class="tw-bg-white tw-absolute tw-left-1/2 -tw-translate-x-1/2 -tw-translate-y-1/2 tw-p-4 tw-rounded-lg">
      <div class="header tw-flex tw-items-center tw-justify-between">
        <slot name="title"></slot>
        <button class="tw-bg-gray-100 tw-rounded-full tw-cursor-pointer tw-p-2" @click="closeModal">
          <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6 tw-text-gray-700 "
               fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
            <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12" />
          </svg>
        </button>
      </div>
      <div class="content tw-overflow-y-auto">
        <slot name="content"></slot>
      </div>
    </div>


<!--    <div class="tw-w-1/2 tw-h-1/2 tw-bg-white tw-absolute tw-top-1/2 tw-left-1/2 -tw-translate-x-1/2 -tw-translate-y-1/2 tw-p-4 tw-rounded-lg">-->
<!--      <div class="header tw-flex tw-items-center tw-justify-between">-->
<!--        <slot name="title"></slot>-->
<!--        <button class="tw-bg-gray-100 tw-rounded-full tw-cursor-pointer tw-p-2" @click="closeModal">-->
<!--          <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6 tw-text-gray-700 "-->
<!--               fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">-->
<!--            <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12" />-->
<!--          </svg>-->
<!--        </button>-->

<!--      </div>-->
<!--      <div class="content tw-overflow-y-auto">-->
<!--        <slot name="content"></slot>-->
<!--      </div>-->

<!--    </div>-->


  </div>

</template>

<script>
export default {
  name: "BaseModal",
  emits:['close_modal'],
  methods:{
    closeModal(){
      this.$emit('close_modal')
    }
  }
}
</script>

<style scoped>

</style>
