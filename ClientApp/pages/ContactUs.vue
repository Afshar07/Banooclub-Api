<template>
  <div class="mcontainer NewBg px-2 Rtl text-right">
    <div class="container-fluid bg-headers">
      <div class="container p-0">
        <div class="row    "  >

          <nav aria-label="breadcrumb" style="direction: rtl" >
            <ol class="breadcrumb   d-flex align-items-center gap-2    p-0 m-0" >

              <li class=" ">
                <nuxt-link v-if="!$auth.loggedIn" class="text-secondary " style="cursor: pointer;text-decoration: none" to="/">ثبت نام
                </nuxt-link>
                <nuxt-link v-else class="text-secondary " style="cursor: pointer;text-decoration: none" to="/social/AccountSetting/MyPage">پروفایل من
                </nuxt-link>
              </li>
              <li> / </li>
              <li class="  "><a class="text-dark " style="cursor: pointer;text-decoration: none" href="#"> تماس با ما </a></li>

            </ol>

          </nav>

        </div>
      </div>
      <div class="container">
        <div class="row   d-flex justify-content-center  ">
          <div class="col-md-4 d-flex justify-content-center">
            <h2><strong>تماس با ما</strong></h2>
          </div>
        </div>
      </div>
      <div class="container">
        <div class="row   d-flex justify-content-center  ">
          <div class="col-md-12">
            <h5><strong>ارسال یک پیام به ما</strong></h5>
            <div class="row">
              <div class="col-md-12">
                <small class="my-2">نام شما</small>
                <input v-model="name"  type="text" class="form-control" >
              </div>
              <div class="col-md-12 my-3">
                <small class="my-2">ایمیل شما</small>
                <input  v-model="email" type="text" class="form-control" >
              </div>
              <div class="col-md-12">
                <small class="my-2">پیام شما</small>
                <textarea v-model="message" class="form-control"></textarea>
              </div>
              <div class="col-md-12 my-2">
                <button @click="sendMessage" class="btn btn-dark">ارسال</button>
              </div>
            </div>
          </div>
        </div>
      </div>

    </div>
    <LoginFooter></LoginFooter>
  </div>
</template>

<script>
export default {
  auth: false,
  name: "contactUs",
  data(){
    return {
      name:'',
      email:'',
      message:''
    }
  },
methods:{
  sendMessage(){

  }
},
  head() {
    return {
      title: 'تماس با ما',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'تماس با ما',
        },
      ],
    }
  },

}
</script>

<style>


</style>
