<template>
  <div>
    <b-row>
      <b-col cols="12">
        <b-card>
          <b-form>
            <b-row>

          <!-- Field: type -->
          <b-col
              cols="12"
              md="4"
          >
            <b-form-group
                label="نوع محصول"
                label-for="type"
            >
              <v-select
                  v-model="productType"
                  :options="types"
                  :reduce="val => val.value"
                  :clearable="false"
                  input-id="productCategoryId"
                  disabled
              />
            </b-form-group>
          </b-col>

          <!-- Field: fileData -->
          <b-col
              cols="12"
              md="4"
          >

            <b-button
                class="mt-2 col-12"
                variant="warning"
                @click="$refs.refInputEl2.click()"
            >
              <input
                  ref="refInputEl2"
                  type="file"
                  accept=".rar , .zip"
                  class="d-none"
                  @change="select"
              >
              <span class="">بارگذاری</span>
            </b-button>

          </b-col>
        </b-row>
      </b-form>

          <b-button
              variant="primary"
              class="mt-1 mb-sm-0 mr-0 mr-sm-1 col-12 col-md-2"
              :block="$store.getters['app/currentBreakPoint'] === 'xs'"
              @click="upload"
          >
            ثبت
          </b-button>

             <b-progress
              v-if="uploaded !== 0 && uploaded !== 100"
              :value="uploaded"
              max="100"
              variant="primary"
              class=" mt-2"
             />

          <b-alert
              :show="uploaded === 100"
              variant="success"
              class="col-md-2 col-12"
          >
            <div class="alert-body mt-2">
              <feather-icon
                  class="mr-25"
                  icon="CheckIcon"
              />
              <span class="ml-25">فایل شما آپلود شده است.</span>
            </div>
          </b-alert>
          <b-alert
              :show="errorAlert"
              variant="danger"
              class="col-md-2 col-12"
          >
            <div class="alert-body mt-2">
              <feather-icon
                  class="mr-25"
                  icon="CloseIcon"
              />
              <span class="ml-25">آپلود فایل با مشکل مواجه شد!</span>
            </div>
          </b-alert>
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>

<script>
import {
  BAlert, BAvatar,
  BButton,
  BCard,
  BCardBody,
  BCardText,
  BCol,
  BDropdown,
  BDropdownItem, BForm, BFormGroup, BFormInput, BFormTextarea,
  BImg,
  BLink, BMedia, BOverlay,
  BRow, BTab, BTabs , BProgress
} from "bootstrap-vue";
import vSelect from "vue-select";
import axios from "axios";
import {getUserData} from "@/auth/utils";
import ToastificationContent from "@core/components/toastification/ToastificationContent";

export default {
  title: 'مدیای مححصول - پنل ادمین بانو کلاب',
  name: "ECommerceAddProductMedia",
  components:{
    BCard,
    BCardBody,
    BRow,
    BCol,
    BImg,
    BCardText,
    BLink,
    BButton,
    BDropdown,
    BDropdownItem,
    BAlert,
    BTab,
    BTabs,
    BForm,
    BFormGroup,
    BFormInput,
    BFormTextarea,
    BMedia,
    BAvatar,
    BOverlay,
    vSelect,
    BProgress
  },
  // watch: {
  //   chunks(n, o) {
  //     if (n.length > 0) {
  //       this.upload();
  //     }
  //   }
  // },
  data() {
    return {
      productId:this.$route.params.id,
      productType:this.$route.query.productType,
      userToken:getUserData().data.token,
      errorAlert:false,
      types:[
        {label : 'دوره' , value : 1},
        {label : 'فیلم' , value : 2},
        {label : 'فایل' , value : 3},
        {label : 'پادکست' , value : 4},
      ],
      file: null,
      chunks: [],
      uploaded: 0
    };
  },

  computed: {
    // progress() {
    //   if (this.file !== null){
    //     return Math.floor((this.uploaded * 100) / this.file.size);
    //   }
    // },
    formData() {
      let formData = new FormData;

      if (this.file !== null){
        // formData.set('is_last', this.chunks.length === 1);
        // formData.set('file', this.chunks[0], `${this.file.name}`);
        formData.set('file', this.file, `${this.file.name}`);
      }
      return formData;
    },
    config() {
      return {
        method: 'POST',
        data: this.formData,
        url: 'https://api.halamkhoobee.com/api/Product/UploadFile?productId='+this.productId+'&productType='+this.productType,
        headers: {
          'Content-Type': 'application/octet-stream',
          'Authorization': this.userToken,
        },
        onUploadProgress: progressEvent => {
          this.uploaded = Math.round((progressEvent.loaded * 100) / progressEvent.total);
        }
      };
    }
  },

  methods: {
    select(event) {
      this.file = event.target.files.item(0);
      // this.createChunks();
    },
    upload() {
      axios(this.config).then(response => {

        this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `فایل با موفقیت آپلود شد.`,
          },
        })

        // this.productType = 0;
        this.file = null;

      }).catch(error => {
        this.errorAlert = true;
        console.log(error);
      });
    },
    // createChunks() {
    //   let size = 10000000, chunks = Math.ceil(this.file.size / size);  //  for 10MB
    //
    //   for (let i = 0; i < chunks; i++) {
    //     this.chunks.push(this.file.slice(
    //         i * size, Math.min(i * size + size, this.file.size), this.file.type
    //     ));
    //   }
    // }
  }
}
</script>

<style scoped>

</style>
