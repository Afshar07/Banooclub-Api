import Vue from 'vue'
import { ToastPlugin, ModalPlugin } from 'bootstrap-vue'
import VueCompositionAPI from '@vue/composition-api'
import VuePersianDatetimePicker from 'vue-persian-datetime-picker';
import Helper from "@/libs/Helper";
// import vueSignalR from "@/libs/vue-signalR";
import titleMixin from './mixins/titleMixin'
Vue.mixin(titleMixin)

import i18n from '@/libs/i18n'
import router from './router'
import store from './store'
import App from './App.vue'

// Quill
import VueQuillEditor from 'vue-quill-editor'

// require styles
import 'quill/dist/quill.core.css'
import 'quill/dist/quill.snow.css'
import 'quill/dist/quill.bubble.css'

Vue.use(VueQuillEditor, /* { default global options } */)


// Global Components
import './global-components'

// 3rd party plugins
import '@axios'
import '@/libs/acl'
import '@/libs/portal-vue'
import '@/libs/clipboard'
import '@/libs/toastification'
import '@/libs/sweet-alerts'
import '@/libs/vue-select'
import '@/libs/tour'
import '@/libs/Api/WebServiceRequest'
import '@/libs/vue-signalR'

// Axios Mock Adapter
import '@/@fake-db/db'


// BSV Plugin Registration
Vue.use(ToastPlugin)
Vue.use(ModalPlugin)

// Composition API
Vue.use(VueCompositionAPI)

// Firebase
import firebase from 'firebase/app';
import 'firebase/firestore';
import {inject} from "@vue/composition-api/dist/vue-composition-api.esm";

// Feather font icon - For form-wizard
// * Shall remove it if not using font-icons of feather-icons - For form-wizard
require('@core/assets/fonts/feather/iconfont.css') // For form-wizard

// import core styles
require('@core/scss/core.scss')

// import assets styles
require('@/assets/scss/style.scss')

Vue.config.productionTip = false

Vue.use(VuePersianDatetimePicker, {
  name: 'custom-date-picker',
  props: {
    inputFormat: 'YYYY-MM-DD HH:mm',
    format: 'jYYYY-jMM-jDD HH:mm',
    editable: false,
    inputClass: 'form-control my-custom-class-name',
    placeholder: 'Please select a date',
    altFormat: 'YYYY-MM-DD HH:mm',
    color: '#00acc1',
    autoSubmit: false,
    clearable: true,

    //...
    //... And whatever you want to set as default
    //...
  }
});
Vue.component('date-picker', VuePersianDatetimePicker);

// Firebase configurations
const firebaseConfig = {

  apiKey: "AIzaSyCVbu8W2u-ZE7HfE7IY9VsNpqs60kMp7Mw",

  authDomain: "vue-admin-dashboard-a6334.firebaseapp.com",

  projectId: "vue-admin-dashboard-a6334",

  storageBucket: "vue-admin-dashboard-a6334.appspot.com",

  messagingSenderId: "884810706591",

  appId: "1:884810706591:web:60a0891689ce30514b3caa"

};


// Stream

let firebaseApp = firebase.initializeApp(firebaseConfig);

let firestore = firebaseApp.firestore();
export default firestore;

new Vue({
  router,
  store,
  i18n,
  render: h => h(App),
}).$mount('#app')
