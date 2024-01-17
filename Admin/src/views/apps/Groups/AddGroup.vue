<template>
  <b-form @submit.prevent>
    <b-row>
      <b-col md="6">
        <b-form-group
            label="نام گروه"

        >
          <b-form-input
           v-model="GroupName"
              placeholder="نام گروه"
          />
        </b-form-group>
      </b-col>
      <b-col md="6">
        <b-form-group
            label="عکس گروه"

        >
          <b-form-file
              ref="File"
              @input="HandleFile"
              placeholder="فایل خود را انتخاب کنید"
              drop-placeholder="Drop file here..."
          />
        </b-form-group>
        <div class="position-relative" v-if="BaseImgUrl!==''">
          <feather-icon icon="TrashIcon" size="20" class="text-danger position-absolute" @click="RemovePic"></feather-icon>
          <img :src="BaseImgUrl" width="100px" height="100px" alt="">
        </div>
      </b-col>
      <b-col md="12">
        <b-form-group
            label="افزودن اعضا"
            label-for="mc-city"
        >
          <v-select
              v-model="SelectedUsersId"
              :options="AllUsers"
              label="userName"
              multiple
              :reduce="userName => userName.userId"
              :clearable="false"
              placeholder="اعضای گروه"

          />
        </b-form-group>
      </b-col>

      <!-- submit and reset -->
      <b-col>
        <b-button
            v-ripple.400="'rgba(255, 255, 255, 0.15)'"
            type="button"
            variant="primary"
            class="mr-1"
            @click="CreateGroup"
        >
          ثبت
        </b-button>
        <b-button
            v-ripple.400="'rgba(186, 191, 199, 0.15)'"
            type="reset"
            variant="outline-secondary"
        >
          پاک کردن همه
        </b-button>
      </b-col>
    </b-row>
  </b-form>
</template>

<script>
import {
  BFormGroup, BFormInput, BFormCheckbox, BForm, BButton, BInputGroup, BInputGroupPrepend, BRow, BCol,BFormFile
} from 'bootstrap-vue'
import Ripple from 'vue-ripple-directive'
import CreateGroup from '@/libs/Api/Groups/CreateGroup'
import UsersGetAllRequest from '@/libs/Api/Users/UsersGetAllRequest'
import vSelect from 'vue-select'
import ToastificationContent from '@core/components/toastification/ToastificationContent'
export default {
  components: {
    BRow,
    BCol,
    BFormGroup,
    BFormInput,
    BFormCheckbox,
    BFormFile,
    BInputGroup,
    BInputGroupPrepend,
    BForm,
    ToastificationContent,
    vSelect,
    BButton,
  },
  directives: {
    Ripple,
  },

  data(){
    return{
      GroupName:'',
        BaseImgUrl:'',
      image:'',
      AllUsers:[],
      SelectedUsersId:[],

    }
  },

  mounted(){
    this.getAllUsers()
  },
  title: 'ساخت گروه جدید  - پنل ادمین بانو کلاب',
  methods:{
    RemovePic(){
      this.BaseImgUrl = ''
      this.image = ''
    },
    HandleFile(){
      const that = this
      let f = ''

      f = this.$refs.File.files[0]
      this.BaseImgUrl = URL.createObjectURL(f)
      const reader = new FileReader()
      reader.onload = (function (theFile) {
        return function () {
          const binaryData = reader.result
          that.image = window.btoa(binaryData)
        }
      })(f)
      reader.readAsBinaryString(f)


    },
    async CreateGroup(){
      let _this = this
      let createGroup = new CreateGroup(_this)
      let data = {
          name: this.GroupName,
          createDate: new Date(Date.now()),
          isActive:false,
          members: this.SelectedUsersId,
          fileData: this.image,
        IsActive:false

      }
       createGroup.setParams(data)
      await createGroup.fetch((content)=>{

        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `گروه با موفقیت ساخته شد`,
          },
        })
        this.$router.push('/apps/Groups/GroupsList')
      },(error)=>{
        console.log(error)
      })

    },
    async getAllUsers(){
      let _this = this;
      let usersGetAllRequest = new UsersGetAllRequest(_this);
      let data = {
        pageNumber:1,
        count:1000,
        search:''
      }
      usersGetAllRequest.setParams(data);
      await usersGetAllRequest.fetch(function (content){
        _this.AllUsers = content.users;
        _this.totalCount = content.usersCount
      },function (error){
        console.log(error)
      })
    },
  }
}
</script>

