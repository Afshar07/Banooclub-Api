<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="container mcontainer bg-white mb-4" v-if="ticketData">
    <h1 class="p-1 h3">موضوع تیکت: {{ ticketData[0].content }}</h1>
    <div class="col-12">
      <div
        v-for="(item,idx) in ticketData"
        :key="idx"
        class="card w-75 my-3 parent"
        :class="
          item.userType == 1
            ? 'justify-content-start ms-auto text-end my-ticket'
            : 'justify-content-end me-auto text-start your-ticket'
        "
      >
        <div class="bubble"></div>
        <div>
          <div>
            <div class="card-body">
              <div
                class="col-12 d-flex p-0"
                :class="
                  item.userType == 1
                    ? 'justify-content-start ms-auto text-end'
                    : 'justify-content-end me-auto text-start'
                "
              >
                <div
                  class="d-flex flex-column"
                  :class="
                    item.userType == 1 ? 'align-items-start' : 'align-items-end'
                  "
                >
                  <strong class="card-text">
                    <span
                      v-if="item.userType === 1"
                      class="badge pill bg-success mb-2 my-badge position-absolute"
                      style="line-height: 1.5rem"
                      >من
                    </span>
                    <span
                      v-else
                      class="badge pill bg-primary mb-2 your-badge position-absolute"
                      style="line-height: 1.5rem"
                      >اپراتور
                    </span>
                  </strong>
                  <p class="card-text gap-2 d-flex align-items-center">
                    <small> <i class="fas  fa-clock"></i></small>
                    <small class="">{{new Date(item.createDate).toLocaleDateString('fa-IR')}}</small>
                  </p>
                </div>
              </div>

              <p
                class="card-text "
                :class="item.userType == 1 ? 'text-end' : 'text-start'"
              >
                {{ item.content }}
              </p>
              <img
                v-if="item.fileData"
                :src="BaseUrl + item.fileData"
                class="img-fluid rounded-start"
              />
            </div>
          </div>
        </div>
      </div>

      <h4>ثبت پاسخ</h4>
      <hr />
      <div class="row">
        <div class="col-md-10">
          <textarea
            v-model="ticketDescription"
            type="text"
            rows="1"
            class="form-control"
            placeholder="متن تیکت"
          ></textarea>
        </div>
        <div class="col-md-2 mt-3 mt-md-0">
          <button class="tw-bg-[#2A41E8] hover:tw-bg-white hover:tw-text-[#2A41E8] tw-border-solid border-1 tw-border-[#2A41E8] tw-rounded w-100 tw-transition tw-text-white  tw-p-2" @click="SubmitReply">
            ارسال
          </button>
        </div>
        <div class="col-md-12">
          <div class="row">
            <div class="col-md-2 d-flex justify-content-start my-3">
              <button class="tw-bg-[#2A41E8] tw-h-10 hover:tw-bg-white hover:tw-text-[#2A41E8] tw-border-solid border-1 tw-border-[#2A41E8] tw-rounded w-100 tw-transition tw-text-white  tw-p-2" @click="openFileUpload">
                آپلود عکس
              </button>
              <input
                ref="file"
                type="file"
                class="File d-none"
                @change="UploadFile"
              />
            </div>
            <div v-if="BaseImgUrl!==''" class="col-md-2 my-4 tw-relative">
              <i class="fas fa-trash tw-absolute tw-p-2 text-danger tw-cursor-pointer" @click="RemovePic"></i>
              <img :src="BaseImgUrl" width="100px" height="100px" class="rounded border" alt="">
            </div>
          </div>
        </div>

      </div>
    </div>
  </div>
</template>

<script>
export default {
  layout: "PoshtebamPlusLayout",
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
      BaseImgUrl:''
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
    async SubmitReply() {
      this.$nuxt.$loading.start();
      try {
        await this.$repositories.createATicket.createATicket({
          parentId: this.$route.params.id,
          content: this.ticketDescription,
          type: 0,
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
