<template>
  <div class="container-fluid px-0">
    <div class="row boxMainContent">
      <div class="col-12">
        <h5 class="editAccountSetting text-black d-inline-block">
          <font-awesome-icon icon="info-circle" color="#088dcd"/>
          ویرایش کار و آموزش
        </h5>
      </div>
      <form class="row">
        <div class="col-12 mt-3">
          <div class="form-check">
            <input class="form-check-input" type="checkbox" value="" id="graduated">
            <label class="form-check-label check-box" for="graduated"></label>
            <span class="privacyAccept">فارغ التحصیل</span>
          </div>
        </div>
        <div class="col-12 mt-3">
          <div class="form-check">
            <input class="form-check-input" type="checkbox" value="" id="master">
            <label class="form-check-label check-box" for="master"></label>
            <span class="privacyAccept">کارشناسی ارشد</span>
          </div>
        </div>
        <div class="col-12 mt-3">
          <div class="form-floating text-end">
            <input type="text" class="form-control border-0 " id="education" placeholder="تحصیل در"
                   v-model.trim="education">
            <label for="education">تحصیل در</label>
          </div>
        </div>
        <div class="col-md-6 col-lg-6 col-12 mt-3">
          <div class="form-floating text-end">
            <input type="text" class="form-control border-0 " id="fromDate" placeholder="از"
                   v-model.trim="fromDate">
            <label for="fromDate">از</label>
          </div>
        </div>
        <div class="col-md-6 col-lg-6 col-12 mt-3">
          <div class="form-floating text-end">
            <input type="text" class="form-control border-0 border-bottom" id="toDate" placeholder="تا"
                   v-model.trim="toDate">
            <label for="toDate">تا</label>
          </div>
        </div>
        <div class="col-12 my-3">
          <v-select placeholder="کشور" dir="rtl" v-model="countryName"
                    :reduce="(option) => option.id"
                    :options="[
      { label: 'ایران', id: 1 },
      { label: 'آمریکا', id: 2 },
    ]">
            <i slot="spinner" class="icon icon-spinner"></i>

          </v-select>
        </div>
        <div class="col-12 my-3">
          <v-select placeholder="شهر" dir="rtl" v-model="cityName" :reduce="(option) => option.id"
                    :options="[
      { label: 'کرمانشاه', id: 1 },
      { label: 'تهران', id: 2 },
    ]">
            <i slot="spinner" class="icon icon-spinner"></i>
          </v-select>
        </div>
        <div class="col-12 mt-3">
          <div class="form-floating">
            <textarea class="form-control border-0" resize="none" placeholder="توضیحات" id="description"
                      v-model.trim="description"
                      style="height: 150px"></textarea>
            <label for="description">توضیحات</label>
          </div>
        </div>
        <div class="col-12 mt-3">
          <button type="button" class="btn btn-sm ButtonAccountSetting">لغو</button>
          <button type="button" class="btn btn-sm ButtonAccountSetting" @click="submitEditWorkEductation">به روز رسانی
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "EditWorkEducation",
  layout: 'BanooClubLayout',
  data() {
    return {
      education: '',
      fromDate: '',
      toDate: '',
      cityName: '',
      description: '',
      countryName: ''
    }
  },
  methods: {
    submitEditWorkEductation() {
      console.log('submit EditWorkEductation');
    }
  },
  mounted() {
    this.$nextTick(() => {
      this.$nuxt.$loading.finish();
    })
  }
}
</script>

<style scoped>
.boxMainContent {
  background: #fdfdfd none repeat scroll 0 0;
  border: 1px solid #ede9e9;
  border-radius: 3px;
  display: inline-block;
  width: 100%;
  margin-bottom: 20px;
  padding: 25px;
}

textarea {
  resize: none;
}

.form-floating > label {
  right: 0 !important;
  width: fit-content;
  color: #b7b7b7 !important;
  font-size: 1rem;
  font-weight: 400;
  border-bottom: 0.0625rem solid #e1e8ed !important;
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

.editAccountSetting {
  font-size: 18px;
  text-transform: capitalize;
  margin-bottom: 20px;
  padding-bottom: 20px;
  border-bottom: 3px solid #088dcd;
}

/* Checkmark style starts */
@-moz-keyframes dothabottomcheck {
  0% {
    height: 0;
  }
  100% {
    height: 10px;
  }
}

@-webkit-keyframes dothabottomcheck {
  0% {
    height: 0;
  }
  100% {
    height: 10px;
  }
}

@keyframes dothabottomcheck {
  0% {
    height: 0;
  }
  100% {
    height: 10px;
  }
}

@keyframes dothatopcheck {
  0% {
    height: 0;
  }
  50% {
    height: 0;
  }
  100% {
    height: 20px;
  }
}

@-webkit-keyframes dothatopcheck {
  0% {
    height: 0;
  }
  50% {
    height: 0;
  }
  100% {
    height: 20px;
  }
}

@-moz-keyframes dothatopcheck {
  0% {
    height: 0;
  }
  50% {
    height: 0;
  }
  100% {
    height: 20px;
  }
}

input[type=checkbox] {
  display: none;
}

.check-box {
  height: 1rem;
  width: 1rem;
  background-color: transparent;
  border: 0.125rem solid #999;
  border-radius: 0.0625rem;
  position: relative;
  display: inline-block;
  -moz-box-sizing: border-box;
  -webkit-box-sizing: border-box;
  box-sizing: border-box;
  -moz-transition: border-color ease 0.2s;
  -o-transition: border-color ease 0.2s;
  -webkit-transition: border-color ease 0.2s;
  transition: border-color ease 0.2s;
  cursor: pointer;
}

.check-box::before, .check-box::after {
  -moz-box-sizing: border-box;
  -webkit-box-sizing: border-box;
  box-sizing: border-box;
  position: absolute;
  height: 0;
  width: 3px;
  background-color: #088dcd;
  display: inline-block;
  -moz-transform-origin: left top;
  -ms-transform-origin: left top;
  -o-transform-origin: left top;
  -webkit-transform-origin: left top;
  transform-origin: left top;
  border-radius: 5px;
  content: ' ';
  -webkit-transition: opacity ease 0.5s;
  -moz-transition: opacity ease 0.5s;
  transition: opacity ease 0.5s;
}

.check-box::before {
  top: 10px;
  left: 7px;
  -moz-transform: rotate(-135deg);
  -ms-transform: rotate(-135deg);
  -o-transform: rotate(-135deg);
  -webkit-transform: rotate(-135deg);
  transform: rotate(-135deg);
}

.check-box::after {
  top: 4px;
  left: 0;
  -moz-transform: rotate(-45deg);
  -ms-transform: rotate(-45deg);
  -o-transform: rotate(-45deg);
  -webkit-transform: rotate(-45deg);
  transform: rotate(-45deg);
}

input[type=checkbox]:checked + .check-box, .check-box.checked {
  border: 0.125rem solid #088dcd;
}

input[type=checkbox]:checked + .check-box::after, .check-box.checked::after {
  height: 5px;
  -moz-animation: dothabottomcheck 0.2s ease 0s forwards;
  -o-animation: dothabottomcheck 0.2s ease 0s forwards;
  -webkit-animation: dothabottomcheck 0.2s ease 0s forwards;
  animation: dothabottomcheck 0.2s ease 0s forwards;
}

input[type=checkbox]:checked + .check-box::before, .check-box.checked::before {
  height: 8px;
  -moz-animation: dothatopcheck 0.4s ease 0s forwards;
  -o-animation: dothatopcheck 0.4s ease 0s forwards;
  -webkit-animation: dothatopcheck 0.4s ease 0s forwards;
  animation: dothatopcheck 0.4s ease 0s forwards;
}


.ButtonAccountSetting {
  font-size: 16px;
  background-color: #4f93ce;
  color: white;
  margin-top: 10px;
  padding: 7px 30px;
  cursor: pointer;
  -webkit-transition: background-color .28s ease, color .28s ease, -webkit-box-shadow .28s ease;
  transition: background-color .28s ease, color .28s ease, -webkit-box-shadow .28s ease;
  transition: background-color .28s ease, color .28s ease, box-shadow .28s ease;
  transition: background-color .28s ease, color .28s ease, box-shadow .28s ease, -webkit-box-shadow .28s ease;
  overflow: hidden;
  -webkit-box-shadow: 0 2px 2px 0 rgba(0, 0, 0, .14), 0 3px 1px -2px rgba(0, 0, 0, .2), 0 1px 5px 0 rgba(0, 0, 0, .12);
  box-shadow: 0 2px 2px 0 rgba(0, 0, 0, .14), 0 3px 1px -2px rgba(0, 0, 0, .2), 0 1px 5px 0 rgba(0, 0, 0, .12);
}

.ButtonAccountSetting:hover {
  background-color: #088dcd;
  -webkit-box-shadow: 0 6px 10px 0 rgba(0, 0, 0, .14), 0 1px 18px 0 rgba(0, 0, 0, .12), 0 3px 5px -1px rgba(0, 0, 0, .2);
  box-shadow: 0 6px 10px 0 rgba(0, 0, 0, .14), 0 1px 18px 0 rgba(0, 0, 0, .12), 0 3px 5px -1px rgba(0, 0, 0, .2)
}

.ButtonAccountSetting:active::before,
.ButtonAccountSetting:focus::before {
  -webkit-transition: opacity .28s ease 364ms, -webkit-transform 1.12s ease;
  transition: opacity .28s ease 364ms, -webkit-transform 1.12s ease;
  transition: transform 1.12s ease, opacity .28s ease 364ms;
  transition: transform 1.12s ease, opacity .28s ease 364ms, -webkit-transform 1.12s ease;
  -webkit-transform: translate(-50%, -50%) scale(1);
  transform: translate(-50%, -50%) scale(1);
  opacity: 0
}

.ButtonAccountSetting:focus {
  outline-style: none;
}


@media (max-width: 576px) {
  .boxMainContent {
    background: #fdfdfd none repeat scroll 0 0;
    border: 1px solid #ede9e9;
    border-radius: 3px;
    display: inline-block;
    width: 100%;
    margin-bottom: 20px;
    padding: 30px 15px;
  }
}
</style>
