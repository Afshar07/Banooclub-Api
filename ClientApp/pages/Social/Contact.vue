<template>
  <div class="container-fluid px-0">
    <div class="row">
      <div class="col-lg-7 px-0 col-12 col-12 order-md-0 order-1">
        <form class="row bg-white h-100">
          <div class="col-lg-6 col-12 mt-3">
            <div class="form-floating text-end">
              <input type="text" class="form-control border-0 border-bottom" id="userName" placeholder="نام خانوادگی"
                     v-model.trim="userName">
              <label for="userName">نام خانوادگی</label>
            </div>
          </div>
          <div class="col-lg-6 col-12 form-group mt-3">
            <div class="form-floating text-end">
              <input type="email" class="form-control border-0 border-bottom"
                     placeholder="ایمیل" v-model.trim="email" required/>
              <label>ایمیل</label>
            </div>
          </div>
          <div class="col-lg-6 col-12 mt-3">
            <div class="form-floating text-end">
              <input type="tel" class="form-control border-0 border-bottom" id="phone" placeholder="تلفن"
                     v-model.trim="phone">
              <label for="phone">تلفن</label>
            </div>
          </div>
          <div class="col-lg-6 col-12 form-group mt-3">
            <div class="form-floating text-end">
              <input type="text" class="form-control border-0 border-bottom" id="companey"
                     placeholder="شرکت" v-model.trim="companey" required/>
              <label for="companey">شرکت</label>
            </div>
          </div>
          <div class="col-12 form-group mt-3">
            <div class="form-floating text-end">
              <textarea type="text" class="form-control border-0 border-bottom" id="companey"
                        placeholder="پیام" v-model.trim="companey"/>
              <label for="companey">پیام</label>
            </div>
          </div>
          <div class="col-12">
            <div class="col-12 text-start">
              <button type="button" class="btn btn-sm submitContactForm" @click="submitContactForm">
                <i class="fas fa-paper-plane"></i>
              </button>
            </div>
          </div>
        </form>
      </div>
      <div class="col-lg-5 col-12 col-12 position-relative order-md-1 order-0">
        <div class="cntct-adres">
          <h3> اطلاعات تماس </h3>
          <ul>
            <li>
              <i class="fas fa-map-marker-alt"></i>
              <span>خیابان هیئت امنا بلوار سعادت میدان هدایت</span>
            </li>
            <li>
              <i class="fas fa-mobile-alt"></i>
              <span>(800) 900-200-300</span>
            </li>
            <li>
              <i class="far fa-envelope"></i>
              <span><a class="emailContact" href="/">[ایمیل&nbsp;محافظت
													شده است]</a></span>
            </li>
          </ul>
          <ul class="social-media">
            <li>
              <a href="#" title="">
                <font-awesome-icon :icon="['fab', 'facebook-square']" color="#ccc"/>
              </a>
            </li>
            <li>
              <a href="#" title="">
                <font-awesome-icon :icon="['fab', 'google-plus-square']" color="#ccc"/>
              </a>
            </li>
            <li>
              <a href="#" title="">
                <font-awesome-icon :icon="['fab', 'pinterest-square']" color="#ccc"/>
              </a>
            </li>
            <li>
              <a href="#" title="">
                <font-awesome-icon :icon="['fab', 'twitter-square']" color="#ccc"/>
              </a>
            </li>
          </ul>
          <h1 class="bg-cntct">Winku</h1>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Contact",
  layout: 'PoshtebamPlusLayout',
  data() {
    return {
      userName: '',
      email: '',
      companey: '',
      phone: ''
    }
  },
  methods: {
    submitContactForm() {
      console.log('submitContactForm');
    }
  }
}
</script>

<style scoped>
.emailContact {
  color: #fff;
  text-decoration: none;
}

.bg-cntct {
  bottom: 80px;
  font-size: 90px;
  opacity: 0.1;
  position: absolute;
  right: 10px;
  transform: rotate(
    -50deg);
}

.cntct-adres {
  background: #103f6e;
  color: #fff;
  width: 100%;
  height: 100%;
  padding: 50px;
}

.cnt-title > span {
  float: right;
  font-size: 22px;
  text-transform: capitalize;
  width: 80%;
  line-height: 27px;
}

.cnt-title > i {
  float: left;
  width: auto;
}

.cntct-adres > ul {
  display: inline-block;
  list-style: outside none none;
  padding-right: 0;
  width: 100%;
}

.cntct-adres li {
  display: inline-block;
  margin-bottom: 20px;
  padding-bottom: 20px;
  width: 100%;
}

.cntct-adres > ul li i {
  color: rgba(255, 255, 255, 0.45);
  display: inline-block;
  font-size: 25px;
  text-align: center;
  vertical-align: sub;
  width: 40px;
}

.cntct-adres .social-media > li {
  margin-right: 10px;
  padding-bottom: 0;
  width: auto;
}

.cntct-adres .social-media > li i {
  width: auto;
}


.submitContactForm {
  -webkit-appearance: button;
  font-size: 16px;
  color: #fff;
  background: #088dcd;
  margin-top: 10px;
  padding: 7px 30px;
}

.submitContactForm:hover {
  -webkit-transition: background-color .28s ease, color .28s ease, -webkit-box-shadow .28s ease;
  transition: background-color .28s ease, color .28s ease, -webkit-box-shadow .28s ease;
  transition: background-color .28s ease, color .28s ease, box-shadow .28s ease;
  transition: background-color .28s ease, color .28s ease, box-shadow .28s ease, -webkit-box-shadow .28s ease;
  overflow: hidden;
  -webkit-box-shadow: 0 2px 2px 0 rgb(0 0 0 / 14%), 0 3px 1px -2px rgb(0 0 0 / 20%), 0 1px 5px 0 rgb(0 0 0 / 12%);
  box-shadow: 0 2px 2px 0 rgb(0 0 0 / 14%), 0 3px 1px -2px rgb(0 0 0 / 20%), 0 1px 5px 0 rgb(0 0 0 / 12%);
}

textarea {
  resize: none;
}

.form-floating > label {
  right: 0;
  width: fit-content;
  color: #b7b7b7 !important;
  font-size: 1rem;
  font-weight: 400;
}

.form-floating > .form-control {
  border-bottom: 0.0625rem solid #e1e8ed !important;
}

.form-floating > .form-control:focus,
.form-floating > .form-control:focus:not(:placeholder-shown) {
  padding-bottom: 10px !important;
  border-bottom: 3px solid #088dcd !important;
  transition: left .28s ease, width .28s ease;
  -webkit-transition: left .28s ease, width .28s ease;
  width: 100%;
  left: 50%;
  outline-style: none !important;
}


.form-control input {
  background: 0 0;
  padding: 0.125rem 0.125rem 0.0625rem;
  font-size: 1rem;
  border-width: 0;
  border-color: transparent;
  line-height: 1.9;
  width: 100%;
  color: transparent;
  -webkit-transition: all .28s ease;
  transition: all .28s ease;
  -webkit-box-shadow: none;
  box-shadow: none;
  outline-style: none !important;
}

form {
  padding: 50px 60px;
}
</style>
