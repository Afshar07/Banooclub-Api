<template>
  <transition name="modal">
    <div v-if="is_modal_open" style="background: rgba(1,1,1,0.7)" class="tw-modal my-0 tw-modal-open tw-bg-transparent" @click="closeModal">
      <div class="tw-container tw-mx-auto tw-w-11/12 tw-z-[99999]">
        <div  style="overflow-x: hidden!important;" class=" tw-bg-white tw-modal-box tw-max-w-max tw-p-0 tw-w-full tw-mx-auto tw-rounded-xl tw-shadow-2xl tw-p-0 forget_pass_modal"  @click.stop>
          <div class="modal_header tw-flex tw-items-center tw-justify-between tw-border-b">
            <div class="tw-px-7 tw-py-5">
              <div class="lg:tw-text-2xl tw-text-xl tw-font-semibold tw-mb-1 tw-text-gray-600">فراموشی رمز عبور</div>
            </div>
            <button class="tw-p-2 tw-bg-gray-100 tw-rounded-full tw-m-3" type="button" @click="closeModal">
              <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6 tw-text-gray-500" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="1">
                <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12" />
              </svg>
            </button>
          </div>
          <div class="tw-p-7 tw-space-y-5 ">
            <div class="col-12">
              <input
                type="text"
                class="with-border"
                v-model="PhoneOrEmail"
                placeholder="شماره موبایل یا ایمیل"
              />
            </div>
            <div class="flex text-center">
              <button @click="submitForgetPass" type="button" class="tw-bg-purple-400 tw-font-semibold tw-mx-auto tw-px-10 tw-py-3 tw-rounded-md tw-text-center tw-text-white">
                ارسال رمز عبور
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </transition>

</template>

<script>
export default {
  name: "ForgetPassModal",
  data(){
    return{
      forget_type:null,
      PhoneOrEmail:'',
    }
  },
  watch:{
    forget_type(val){
      console.log('val',val)
    }
  },
  computed:{



  },
  props:{
    is_modal_open:{
      type: Boolean,
      required: true,
      default: false
    }
  },
  methods:{
    closeModal(){
      this.$emit('close_modal')
    },
    async submitForgetPass() {
      if(this.PhoneOrEmail === ''){
        this.$toast.error("موبایل یا ایمیل را وارد کنید");
      }else if(!this.PhoneOrEmail.includes('@') && this.PhoneOrEmail.length>11){

        this.$toast.error("شماره موبایل یا ایمیل وارد شده معتبر نیست");
      }else if(!this.PhoneOrEmail.includes('@') && this.PhoneOrEmail.length!==11){

        this.$toast.error("شماره موبایل یا ایمیل وارد شده معتبر نیست");
      }
      else {
        try {
          this.$nuxt.$loading.start();

          const response = await this.$repositories.forgetPassword.forgetPassword({
            type:this.PhoneOrEmail.includes('@')?2:1,
            PhoneOrEmail:this.PhoneOrEmail
          })
          if (response.data.data.status === 3) {
            this.$toast.error("نام کاربری یا رمز عبور اشتباه است.");
          } else if (response.data.data.status === 4) {
            this.$toast.error(
              "کد ارسال شده منتقضی شده است،لطفا دوباره امتحان کنید."
            );
          } else if (response.data.data.status === 5) {
            this.$toast.error("کد وارد شده اشتباه است.");
          } else if (response.data.data.status === 6) {
            this.$toast.error("کاربری با این شماره موبایل یا ایمیل وجود ندارد.");
          } else if (response.data.data.status === 7) {
            this.$toast.success("رمز عبور با موفقیت ارسال شد");
            this.$emit('close_modal')
            this.PhoneOrEmail=''
          } else if (response.data.data.status === 8) {
            this.$toast.error("قبلا کاربری با این نام کاربری ثبت نام کرده است.");
          } else if (response.data.data.status === 9) {
            this.$toast.error("پسورد شما ایمن نیست.");
          } else if (response.data.data.status === 10) {
            this.$toast.error("نام کاربری قبلا ثبت نام کرده است.");
          } else if (response.data.data.status === 11) {
            this.$toast.error("اکانت شما غیرفعال شده است.");
          }
        } catch (error) {
          console.error(error);
        } finally {
          this.$nuxt.$loading.finish();
        }
      }
    },


  }


}
</script>

<style scoped>
@media (min-width: 426px) {
  .forget_pass_modal{
    min-width: 450px !important;
    width: 100%!important;
  }
}

</style>
