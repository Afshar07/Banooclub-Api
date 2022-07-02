<template>
  <div>
  <div class="row adminPanelBox">

    <div class="row ms-3 d-flex flex-column " style="background-color: #f8f8fb; border-radius: 10px">
      <div class="d-flex justify-content-between p-3 my-3 col-md-12 bg-white rounded">

        <div class="  d-flex flex-column my-3 mb-1 " >
          <div class="d-flex align-items-center gap-2">
            <strong class="text-muted">عنوان تیکت :</strong>

            <span >  {{Title}}</span>
          </div>

          <div class="d-flex align-items-center gap-2 my-3 mb-1">
            <i class="fas fa-user text-muted"></i>
            <small class="text-muted">واحد : </small>
            <span >{{Type === 1 ? 'احراز هویت': Type===2 ? 'پشتیبانی' : 'سایر'}}</span>
          </div>
        </div>
        <div class="d-flex  gap-2">

          <small class="text-muted">{{FirstDate | moment("jYYYY/jM/jD HH:MM")}}</small>
          <i class="far fa-clock"></i>
        </div>
      </div>

        <div class="col-md-9 SingleTicket my-3" v-for="item in ticketData" :key="item.ticketId" :class="{'align-self-end':item.userType ===2}"   >
          <div class="row" :class="{'d-flex flex-row-reverse':item.userType ===2}">
            <div class="col-md-1">
              <img class=" myPicture "
                   :src="`https://BanooClubapi.simagar.com/${item.userInfo.selfieFileData}`" alt=""/>
            </div>
            <div class="col-md-11  bg-white px-3 py-1 MyMessage d-flex flex-column " :class="{' align-items-end':item.userType ===2}" >
              <span class="titleColor">  {{ item.userInfo.name + ' ' + item.userInfo.familyName }}</span>
              <br>
              <div class="d-flex flex-wrap " style="direction: ltr!important;" >
                <div style="overflow-wrap:anywhere">
                  {{ item.content}}

                </div>
               </div>
              <br>
              <div>
                <img v-if="item.fileData" :src="BaseUrl + item.fileData" class="img-fluid" alt="">
              </div>
              <br>
              <div class="d-flex flex-wrap text-muted align-items-center gap-2" style="font-size: 13px"  :class="{'d-flex flex-row-reverse':item.userType ===2}">
                <i class="far fa-clock"></i>
                <small>{{ item.createDate | moment("jYYYY/jM/jD HH:MM") }}  </small>
              </div>
            </div>
          </div>
        </div>
      <hr>
      <div class="col-md-12 my-3">

        <div class="row">
          <div class="d-flex align-items-center">
            <input type="file" accept="image/*" multiple="multiple"
                   class="form-control-file my-file d-none"ref="TicketFile" @change="onFileChange">

            <button class="btn border bg-white TicketAttachment" @click="openFile">
              <i class="fas fa-paperclip"></i>
            </button>
            <input v-model="description" type="text" class="form-control Form">
            <button class="btn btn-primary Send" @click="createTicket">
              <i class="fas fa-paper-plane"></i>
            </button>
          </div>
        </div>
      </div>
      <div v-if="url !==''" class="col-md-12 mb-3">
        <small class="badge bg-danger" style="cursor: pointer" @click="RemoveFile">
          <i class="fas fa-minus text-white"></i>
        </small>
        <img :src="url" width="50px" height="50px" class="rounded"  alt="">
      </div>


    </div>
    </div>
<!--    <div class="col-12">-->
<!--      <div class="d-flex flex-column" >-->
<!--        <div v-for="item in ticketData" :key="item.ticketId" class="my-3 ">-->
<!--          <div class="row" :class="item.userType===2 ?'text-start':'text-end'">-->
<!--            <div class="col-12 ">-->
<!--              <div class="d-flex" :class="item.userType===2 ?'flex-row-reverse ps-5':'flex-row pe-5'">-->
<!--                <div class="justify-content-center align-items-center position-relative">-->
<!--                  <img class=" myPicture "-->
<!--                       :src="`https://BanooClubapi.simagar.com/${item.userInfo.selfieFileData}`" alt=""/>-->
<!--                </div>-->
<!--                <div class="">-->
<!--                  <div class="d-flex flex-column text-end px-3">-->
<!--                    <div class="authorName">-->
<!--                      {{ item.userInfo.name + ' ' + item.userInfo.familyName }}-->
<!--                    </div>-->
<!--                  </div>-->
<!--                </div>-->
<!--              </div>-->
<!--            </div>-->
<!--            <div class="d-flex flex-column" :class="item.userType===2 ?'ps-5':'pe-5'">-->
<!--              <div>{{ item.title }}</div>-->
<!--              <div class="text-muted">{{ item.createDate | moment("jYYYY/jM/jD") }}-->
<!--                <i class="fas fa-clock"></i>-->
<!--              </div>-->
<!--              <div>{{ item.content }}</div>-->
<!--              <div v-if="item.fileData !== ''">-->
<!--                <img class="contentImage" :src="`https://BanooClubapi.simagar.com/${item.fileData}`" alt=""/>-->
<!--              </div>-->
<!--            </div>-->
<!--          </div>-->

<!--        </div>-->
<!--      </div>-->
<!--    </div>-->
<!--    <div class="col-12 my-3">-->
<!--      افزودن پاسخ-->
<!--    </div>-->
<!--    <div class="col-md-9 col-12">-->
<!--      <input type="text" class="border rounded w-100 py-1" placeholder="عنوان ..." v-model="ticketTitle"/>-->
<!--    </div>-->
<!--    <div class="col-md-3 col-12">-->
<!--      <div class="d-flex gap-3">-->
<!--        <div>دسته بندی :</div>-->
<!--        <div-->
<!--          v-html="$route.query.type === '1' ? 'احراز هویت' :$route.query.type === '2' ? 'پشتیبانی' :$route.query.type === '3' ? 'سایر' : ''"></div>-->
<!--      </div>-->
<!--    </div>-->
<!--    <div class="col-md-9 col-12">-->
<!--      <textarea class="w-100 border rounded my-3" cols="50"-->
<!--                placeholder="لطفا توضیحات خود را در این قسمت بنویسید." v-model="description"></textarea>-->
<!--    </div>-->
<!--    <div class="col-md-3 col-12">-->
<!--      <div class="d-flex flex-row gap-3 my-3 w-100">-->
<!--        <div class="border rounded text-center justify-content-center align-items-center InputUiBox"-->
<!--             @click="callInputMethod">-->
<!--          <input type="file" accept="image/*" multiple="multiple"-->
<!--                 class="form-control-file my-file d-none" id="my-file" @change="onFileChange">-->
<!--          <div class="labelText">ارسال عکس</div>-->
<!--          <font-awesome-icon icon="plus-square" size="lg"/>-->
<!--        </div>-->
<!--      </div>-->
<!--    </div>-->
<!--    <div class="col-12 my-3 text-center">-->
<!--      <button class="btn btn-primary" @click=" ">افزودن</button>-->
<!--    </div>-->
<!--    <div class="col-12">-->
<!--      <div id="preview">-->
<!--        <img class="img-fluid preview" v-if="url" :src="url"/>-->
<!--      </div>-->
<!--    </div>-->
  </div>
</template>

<script>
export default {
  name: "TicketDetails",
  layout: 'SuperAdminPanel',
  data() {
    return {
      url: '',
      ticketData: {},
      selectedType: null,
      description: '',
      mainImage: '',
      ticketTitle: '',
      Title:'',
      Type:0,
      FirstDate:''
    }
  },

    computed:{
        BaseUrl(){

            return process.env.pic
        }
    },


  methods: {
    callInputMethod() {
      document.querySelector('.my-file').click();
    },
    onFileChange(e) {
      const file = e.target.files[0];
      this.url = URL.createObjectURL(file);
      const that = this
      const reader = new FileReader()
      reader.onload = (function (theFile) {
        return function () {
          const binaryData = reader.result
          that.mainImage = window.btoa(binaryData)
        }
      })(file)
      reader.readAsBinaryString(file)

    },

    RemoveFile(){
      this.url = ''
      this.mainImage = ''
    },

    getTicketData(parentId) {
      this.$axios.post(`Ticket/GetAllByParentId`, null, {
      
        params: {
          parentId: parentId
        }
      }).then((response) => {
        if (response.status === 200) {
          this.ticketData = response.data
          this.Title = this.ticketData[0].title
          this.Type = this.ticketData[0].type
          this.FirstDate = this.ticketData[0].createDate
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
        }
      }).catch((error) => {
        console.log(error)
      })
    },

    openFile(){
      this.$refs.TicketFile.click();
    },
    createTicket() {
        if (this.description === '') {
        this.$toast.error('لطفا توضیحات را وارد کنید.')
      } else {
        this.$axios.post('Ticket/CreateTicket', {
          parentId: this.$nuxt.$route.query.ticketId,
          content: this.description,
          type: this.$nuxt.$route.query.type,
          title: this.ticketTitle,
          fileData: this.mainImage,
        }, {
        
        }).then((response) => {
          if (response.status === 200) {
            this.ticketTitle = ''
            this.description = ''
            this.$nuxt.$loading.start()
            this.getTicketData(this.$nuxt.$route.query.ticketId);
            this.$toast.success('تیکت با موفقیت ثبت شد')
          }
        })
      }
    }
  },
  mounted() {
    this.$nextTick(() => {
      this.$nuxt.$loading.start()
    })

    this.getTicketData(this.$nuxt.$route.query.ticketId)
  }
}
</script>

<style >
.adminPanelBox {
  background-color: #fff;
  padding: 1rem;
}

textarea {
  resize: none;
  min-height: 12rem;
}

textarea::placeholder {
  color: #cccccc;
  padding: 5rem 0;
  text-align: center;
  font-size: 14px;
}

.InputUiBox {
  padding: 2rem 0 0 0;
  width: 100px;
  height: 100px;
  cursor: pointer;
}

.titleColor{
  color:   #556ee6;
}
.TicketAttachment{
  border-left: none!important;
  border-top-right-radius: 20px;
  border-bottom-right-radius: 20px;
  border-top-left-radius: 0px;
  border-bottom-left-radius: 0px;
}

.Form{
border-radius: 0px!important;
  border-left: none!important;
border-right: none!important;
}

.Send{
  border-top-left-radius: 20px;
  border-bottom-left-radius: 20px;
  border-top-right-radius: 0px;
  border-bottom-right-radius: 0px;

}
.MyMessage{
  border-radius: 10px;
  border-top-right-radius: 0px;
}
.TheirMessage{

}

.labelText {
  color: #545454;
  font-size: 14px;
  cursor: pointer;
}

.preview {
  width: 10rem;
  height: 10rem;
}

.myPicture {
  margin-bottom: 20px;
  max-width: 45px;
  min-width: 45px;
  max-height: 45px;
  min-height: 45px;
  vertical-align: middle;
  border-radius: 50%;
  cursor: pointer;
}

.contentImage {
  max-width: 20vw;
  max-height: 20vh;
  height: 20vh;
  width: 20vw;

}
</style>
