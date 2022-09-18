<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="container NewBg mcontainer  mb-4" v-if="ticketData">
    <div class="modal  fade" id="ShowMedia" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog ">
        <div class="modal-content">
          <div class="modal-body">
            <img :src="BaseUrl + SelectedMedia" class="tw-w-full tw-h-full tw-object-cover" alt="">
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">بستن</button>
          </div>
        </div>
      </div>
    </div>
    <h1 class="p-1 h5">موضوع تیکت: {{ ticketData[0].content }}</h1>
    <div         v-for="item in ticketData" :key="item.ticketId" class="col-md-12">
      <div class="TicketCard  w-100  my-3">
        <span    v-if="item.userType === 3" class="badge pill  " style="background: linear-gradient(to right,rgba(219,60,145,1) 0%,rgba(231,122,122,1) 100%);">توسط شما</span>
        <span    v-if="item.userType === 2" class="badge pill  " style="background: linear-gradient(to right,rgb(137,231,122) 0%,rgb(60,200,219) 100%);">توسط اپراتور</span>

        <p class="my-2">{{item.content}}</p>
        <a  data-bs-target="#ShowMedia" data-bs-toggle="modal" @click="SetSelectedMedia(item.fileData)"  v-if="item.fileData" href="" class="d-flex align-items-center text-decoration-none gap-2">
          <small><i class="fas fa-paperclip"></i></small>
          <small> مشاهده عکس</small>
        </a>
        <small class="text-secondary">{{new Date(item.createDate).toLocaleTimeString('fa-IR')}} </small>
      </div>
    </div>
    <div class="col-12 bg-white p-3 tw-shadow rounded">
      <h6>ثبت پاسخ</h6>
      <div class="row">
        <div class="col-md-8">
          <textarea
            v-model="ticketDescription"
            type="text"
            rows="1"
            class="form-control"
            maxlength="200"
            placeholder="متن تیکت"
          ></textarea>
          <InputLimitation v-if="ticketDescription!==null" :LimitLength="200" :Value="ticketDescription.length"></InputLimitation>
        </div>

        <div class="col-md-2">
          <div class="row">
            <div class="col-md-12  d-flex justify-content-start mb-3">
              <button class="tw-bg-[#ff6f9e] tw-h-10   tw-border-solid border-1  tw-rounded w-100 tw-transition tw-text-white  tw-p-2" @click="openFileUpload">
                آپلود عکس
              </button>
              <input
                ref="file"
                type="file"
                class="File d-none"
                @change="UploadFile"
              />
            </div>
            <div v-if="BaseImgUrl!==''" class="col-md-12 my-4 tw-relative">
              <i class="fas fa-trash tw-absolute tw-p-2 text-danger tw-cursor-pointer" @click="RemovePic"></i>
              <img :src="BaseImgUrl" width="100px" height="100px" class="rounded border" alt="">
            </div>
          </div>
        </div>
        <div class="col-md-2 mt-3 mt-md-0">
          <button class="tw-bg-[#85ffdd]    tw-rounded w-100 tw-transition tw-text-[#f5447d]  tw-p-2" @click="SubmitReply">
            ارسال
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import InputLimitation from "@/components/InputLimitation";
export default {
  layout: "PoshtebamPlusLayout",
  components:{InputLimitation},
  head() {
    return {
      title: 'تیکت ها',
      meta: [
        {
          hid: "description",
          name: "description",
          content: 'تیکت ها',
        },
      ],
    }
  },

  // computed:{
  //   headTitle(){
  //     if (this.ticketData && this.ticketData[0] && this.ticketData[0].content){
  //       return this.ticketData[0].content
  //     }else {
  //       return  ''
  //     }
  //   },
  // },


  fetchOnServer() {
    return true;
  },
  async fetch() {
    try {
      const response =
        await this.$repositories.getAllTicketsByParentId.getAllTicketsByParentId(
          this.$route.params.id
        );
      this.ticketData = response.data;
      console.log(this.ticketData[0].content)
      console.log('this.ticketData',this.ticketData)
    } catch (error) {
      console.log(error);
    }
  },
  data() {
    return {
      ticketData: null,
      ticketTitle: '',
      ticketDescription: null,
      image: null,
      BaseImgUrl:'',
      SelectedMedia:''
    };
  },
  computed:{
    BaseUrl(){
      return process.env.pic
    }
  },
  methods: {
    RemovePic(){
      this.BaseImgUrl = ''
      this.image = ''
    },
    SetSelectedMedia(media){
      this.SelectedMedia = media
    },
    async SubmitReply() {
      this.$nuxt.$loading.start();
      try {
        await this.$repositories.createATicket.createATicket({
          parentId: this.$route.params.id,
          content: this.ticketDescription,
          type: this.ticketData[0].type,
          title: this.ticketData[0].title,
          createDate:new Date(Date.now()),
          fileData: this.image,
        });
        this.ticketTitle = null;
        this.ticketDescription = null;
      } catch (error) {
        console.log(error);
      } finally {
        this.$nuxt.$loading.finish();
        this.$nuxt.loading = false;
        this.$fetch();
      }
    },
    CancelUpload() {
      this.image = "";
      this.BaseImgUrl = "";
      this.Uploaded = false;
    },

    UploadFile(event) {
      this.Uploaded = true;

      const f = event.target.files[0];
      if(f.size>=512000){
        this.$toast.error('سایز عکس نمیتواند بزرگتر از 512 کیلوبایت باشد')
        event.target.value=''
      }else{
        this.BaseImgUrl = URL.createObjectURL(f);
        const reader = new FileReader();
        const that = this;
        reader.onload = (function (theFile) {
          return function () {
            const binaryData = reader.result;
            that.image = window.btoa(binaryData);
          };
        })(f);
        reader.readAsBinaryString(f);
      }
      event.target.value=''

    },
    openFileUpload() {
      this.$refs.file.click();
    },
  },
};
</script>
<style scoped>
@media (min-width: 1024px) {
  .mcontainer {
    max-width: 1000px;
    padding: 30px 0px;
    margin-left: auto;
    margin-right: auto;
  }
}
@media (max-width: 1024px) {
  .mcontainer {
    max-width: 1000px;
    padding: 25px 0px;
    margin-left: auto;
    margin-right: auto;
  }
}
.main-container {
  border-radius: 5px;
}
.my-badge {
  right: -4rem;
  top: 50%;
  transform: translateY(-35%);
}
.your-badge {
  left: -4.5rem;
  top: 50%;
  transform: translateY(-35%);
}
.parent {
  margin: 10px 70px;
  position: relative;
  height: auto;
}
.my-ticket {
  border: 1px solid #2A41E8;
  background-color: #2A41E8;
  color: white!important;
}
.my-ticket:after {
  content: "";
  position: absolute;
  top: 30%;
  left: 100%;
  background-color: #2A41E8;
  color: white;
  border-top: 20px solid transparent;
  border-bottom: 20px solid transparent;
  border-right: 20px solid transparent;
}
.your-ticket {
  border: 1px solid #F4F4F4;
  background-color: #F4F4F4;
  color: #b9b9b9;
}
.TicketCard{
  background: #ebe0ff;
  border-radius: 12px;
  padding: 20px 25px 22px;
  line-height: 26px;
  margin-bottom: 20px;
}

.your-ticket:after {
  content: "";
  position: absolute;
  top: 30%;
  right: 100%;
  background-color: #F4F4F4;
  color: #b9b9b9;
  border-top: 20px solid transparent;
  border-bottom: 20px solid transparent;
  border-left: 20px solid transparent;
}
</style>
