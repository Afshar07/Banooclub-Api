<template>
  <transition name="modal">
    <div v-if="is_modal_open" class="tw-modal tw-modal-open tw-bg-transparent" @click="closeModal">
      <div class="tw-container tw-mx-auto tw-w-11/12 tw-z-[99999]">
        <div class=" tw-bg-white tw-modal-box tw-max-w-max tw-p-0 tw-w-full tw-mx-auto tw-rounded-xl tw-shadow-2xl tw-p-0" @click.stop>
          <div class="modal_header tw-flex tw-items-center tw-justify-between tw-border-b">
            <div class="tw-px-7 tw-py-5">
              <div class="lg:tw-text-2xl tw-text-xl tw-font-semibold tw-mb-1 tw-text-gray-600">ثبت نام</div>
              <div class="tw-text-base tw-text-gray-600">این سریع و آسان است.</div>
            </div>
            <button class="tw-p-2 tw-bg-gray-100 tw-rounded-full tw-m-3" type="button" @click="closeModal">
              <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6 tw-text-gray-500" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="1">
                <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12" />
              </svg>
            </button>
          </div>
          <form class="tw-p-7 tw-space-y-5">
            <div class="tw-grid lg:tw-grid-cols-2 tw-gap-5">
              <input type="text" placeholder="نام" class="with-border">
              <input type="text" placeholder="نام خانوادگی" class="with-border">
            </div>
            <div class="tw-grid lg:tw-grid-cols-2 tw-gap-5">
              <input type="text" placeholder="شماره همراه" class="with-border">
              <input type="text" placeholder="کد تایید" class="with-border">
            </div>
            <input type="email" placeholder="Info@example.com" class="with-border">
            <input type="password" placeholder="******" class="with-border">

            <div class="tw-grid lg:tw-grid-cols-2 tw-gap-3">
              <div>
                <label class="tw-mb-0"> Gender </label>
                <select class="selectpicker tw-mt-2 tw-with-border">
                  <option>Male</option>
                  <option>Female</option>
                </select>

              </div>
              <div>
                <label class="tw-mb-2"> Phone: optional  </label>
                <input type="text" placeholder="+543 5445 0543" class="with-border">
              </div>
            </div>
            <p class="tw-text-xs tw-text-gray-400 tw-pt-3">By clicking Sign Up, you agree to our
              <a href="#" class="tw-text-blue-500">Terms</a>,
              <a href="#">Data Policy</a> and
              <a href="#">Cookies Policy</a>.
              You may receive SMS Notifications from us and can opt out any time.
            </p>
            <div class="flex">
              <button type="button" class="tw-bg-blue-600 tw-font-semibold tw-mx-auto tw-px-10 tw-py-3 tw-rounded-md tw-text-center tw-text-white">
                Get Started
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </transition>
</template>

<script>
export default {
  name: "RegisterModal",
  props:{
    is_modal_open:{
      type: Boolean,
      required: true,
      default: false
    }
  },
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
