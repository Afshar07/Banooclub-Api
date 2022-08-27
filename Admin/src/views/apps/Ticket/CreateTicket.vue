<template>
  <b-form @submit.prevent>
    <b-row>
      <b-col md="6">
        <b-form-group
            label="موضوع تیکت "
            label-for="mc-first-name"
        >
          <b-form-input
              v-model="TicketTitle"
              id="mc-first-name"
              placeholder="موضوع تیکت "
          />
        </b-form-group>
      </b-col>
      <b-col md="6">
        <b-form-group
            label="نوع تیکت"
            label-for="mc-last-name"
        >
          <v-select
              v-model="TicketTypeId"
              :clearable="false"
              label="option"
              class="bg-white"
              :options="TicketTypesId"
              :reduce="option => option.value"
          />
        </b-form-group>
      </b-col>
      <b-col v-if="TicketTypeId!==0" md="12">
        <b-form-group
            label="انتخاب کاربران"
            label-for="mc-last-name"
        >
          <v-select
              v-model="UserIds"
              :clearable="false"
              multiple
              label="userName"
              class="bg-white"
              :options="AllUsers"
              :reduce="userName => userName.userId"
          />
        </b-form-group>
      </b-col>
      <b-col md="12">
        <b-form-group
            label="متن تیکت"
            label-for="mc-city"
        >
          <b-form-textarea
              v-model="TicketDescription"
              id="mc-city"
              placeholder="متن تیکت"
          />
        </b-form-group>
      </b-col>




      <!-- submit and reset -->
      <b-col>
        <b-button
            v-ripple.400="'rgba(255, 255, 255, 0.15)'"
            type="submit"
            variant="primary"
            class="mr-1"
            @click="CreateTicket"
        >
          ارسال
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
  BRow, BCol, BFormGroup, BFormInput, BFormCheckbox, BForm, BButton,BFormTextarea
} from 'bootstrap-vue'
import Ripple from 'vue-ripple-directive'
import vSelect from "vue-select";
import UsersGetAllRequest from "@/libs/Api/Users/UsersGetAllRequest";
import TicketCreateRequest from "@/libs/Api/Ticket/TicketCreateRequest";
import ToastificationContent from "@core/components/toastification/ToastificationContent";
export default {
  components: {
    BRow,
    BCol,
    vSelect,
    BFormGroup,
    BFormInput,
    BFormTextarea,
    BFormCheckbox,
    BForm,
    BButton,
  },
  directives: {
    Ripple,
  },

  data(){
    return {
      TicketTypesId:[
        {
          option:"ارسال به همه کاربران",
          value:0
        },
        {
          option:"احراز هویت",
          value:1
        },
        {
          option:"پشتیبانی",
          value:2
        },
        {
          option:"سایر",
          value:3
        },



      ],
      TicketTypeId:null,
      UserIds:[],
      AllUsers:[],
      TicketDescription:'',
      TicketTitle:'',
      image:''
    }
  },

  mounted(){
this.GetAllUsers()
  },

  methods:{
    async CreateTicket(){

      let _this = this
      if(_this.TicketTitle===''){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات ناموفق`,
            icon: 'AlertCircleIcon',
            variant: 'danger',
            text: `لطفا موضوع تیکت را وارد کنید`,
          },
        })
      }else if (_this.TicketDescription===''){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات ناموفق`,
            icon: 'AlertCircleIcon',
            variant: 'danger',
            text: `لطفا متن تیکت را وارد کنید`,
          },
        })
      }else if (_this.TicketTypeId===null){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات ناموفق`,
            icon: 'AlertCircleIcon',
            variant: 'danger',
            text: `لطفا نوع تیکت را انتخاب کنید`,
          },
        })
      }else{
        let  ticketCreateRequest = new TicketCreateRequest()
        let data = {
          parentId: 0,
          content: this.TicketDescription,
          type: this.TicketTypeId,
          title: this.TicketTitle,
          createDate:new Date(Date.now()),
          fileData: this.image,
          recipientIds:this.UserIds,
          recipientUserId:0
        }
        ticketCreateRequest.setParams(data)
        await ticketCreateRequest.fetch((content)=>{
          console.log(content)
        },(e)=>{
          console.log(e)
        })
      }

    },
    async GetAllUsers(){
     let  _this = this
      let usersGetAllRequest = new UsersGetAllRequest()
      let data = {
       pageNumber:1,
        count:10000,
        search:''

      }
      usersGetAllRequest.setParams(data)
      await usersGetAllRequest.fetch((content)=>{
        _this.AllUsers = content.users
      },(e)=>{
        console.log(e)
      })
    },
  }
}
</script>

