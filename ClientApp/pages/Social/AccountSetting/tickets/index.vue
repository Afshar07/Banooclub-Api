<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="container NewBg mcontainer  containerBox my-5">
    <div
      class="offcanvas offcanvas-start sidebar-bg"
      tabindex="-1"
      id="offcanvasExample"
      aria-labelledby="offcanvasExample"
      style="z-index: 9999999;"
      @click.stop
    >
      <div class="offcanvas-header pb-0 pt-3" >
        <h5 class="offcanvas-title" id="offcanvasExampleLabel">تیکت جدید</h5>
        <button ref="closeTicketModal" type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas"
                aria-label="Close"></button>
      </div>

      <div class="offcanvas-body d-flex flex-column">
        <div uk-form-custom class="w-full">
          <div class="row mx-auto">
            <div class="col-12 my-3 p-0">
              <input
                v-model="TicketTitle"
                type="text"
                maxlength="100"
                class="SearchStyle with-border"
                placeholder="عنوان تیکت"
              />
            </div>
            <InputLimitation :LimitLength="100" :Value="TicketTitle.length"></InputLimitation>
            <div class="col-12 my-3 p-0" @click.stop>
              <select
                id="categoryType"
                v-model="TicketType"
                class="form-select"
                placeholder="Select Issue"
                @click.stop
              >
                <option  @click.stop >دسته بندی تیکت</option>
                <option  @click.stop :value="1">احراز هویت</option>
                <option  @click.stop :value="2">پشتیبانی</option>
                <option  @click.stop :value="3">سایر</option>
              </select>
            </div>
            <div class="col-12 my-4 p-0">
              <textarea
                v-model="TicketDescription"
                class="form-control"
                maxlength="200"
                placeholder="متن تیکت"
              ></textarea>
              <InputLimitation class="my-2" :LimitLength="200" :Value="TicketDescription.length"></InputLimitation>
            </div>
            <div class="col-md-12">
              <div class="row">
                <div class="col-md-12  d-flex justify-content-start my-3 p-0">
                  <button type="button" class="button tw-w-full mt-auto" @click="openFileUpload">
                    آپلود عکس
                  </button>
                  <input
                    ref="file"
                    type="file"
                    class="File d-none"
                    @change="UploadFile"
                  />
                </div>
                <div v-if="BaseImgUrl!==''" class="col-md-12 mb-3 tw-relative p-0">
                  <i class="fas fa-trash tw-absolute tw-p-2 text-danger tw-cursor-pointer" @click="RemovePic"></i>
                  <img :src="BaseImgUrl" style="object-fit: contain;width: 100px; height: 100px;" class="rounded border" alt="">
                </div>
              </div>
            </div>

          </div>

        </div>
        <div class="loadmore">
          <button type="button" class="button tw-w-full mt-auto" @click="CreateTicket">
            ثبت
          </button>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="tw-w-full bg-white p-3 d-flex align-items-center justify-content-between gap-3  rounded ">
        <div class="d-flex align-items-center gap-2">
        <img src="/girl-icon-ticket.png" class="tw-w-[7rem] tw-h-20" alt="">
        <div class="d-flex align-items-center flex-column">
          <h1 class="text-purple h5">تیکت ها </h1>
          <h1 class="text-pink h5">Tickets</h1>
        </div>
        </div>
        <button v-tooltip="{content:'ثبت تیکت'}" data-bs-toggle="offcanvas" data-bs-target="#offcanvasExample" aria-controls="offcanvasExample" class="btn AddReplyBtn text-white">
          <PlusIcon fill="#ff6f9e" style="width: 30px; height: 30px;"/>
        </button>
      </div>

      <div class="col-md-12 my-3">
          <input
            class="SearchStyle "
            type="text"
            placeholder="جستجو تیکت"
            v-model="searchKey"
          />
      </div>

          <div class="tw-overflow-x-scroll" v-if="FilteredTickets.length>0">
            <div class="tw-overflow-x-scroll ">
              <table class="tw-items-center  tw-w-full tw-table-compact   tw-bg-white p-3 rounded tw-border-collapse">
                <thead>
                <tr>
                  <th
                    class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                    شناسه
                  </th>
                  <th
                    class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                    موضوع
                  </th>
                  <th
                    class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                    واحد
                  </th>
                  <th
                    class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                    تاریخ
                  </th>
                  <th
                    class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                    وضعیت
                  </th>
                  <th
                    class="tw-px-2 tw-text-teal-500 tw-align-middle tw-border-b tw-border-solid tw-border-gray-200 tw-py-3 tw-text-sm tw-whitespace-nowrap tw-font-light tw-text-left">
                    مشاهده
                  </th>
                </tr>
                </thead>
                <tbody>
                <tr v-for="(item, index) in FilteredTickets" :key="index">
                  <th
                    class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                    {{ item.ticketId }}
                  </th>

                  <td
                    class="tw-border-b  tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                    {{ item.title.substr(0, 7) }}
                  </td>

                  <td
                    class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                    <span v-if="item.type === 0">احراز هویت</span>
                    <span v-if="item.type === 1">پشتیبانی</span>
                    <span v-if="item.type === 2">سایر</span>
                  </td>

                  <td
                    class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">
                    {{ new Date(item.createDate).toLocaleTimeString('fa-IR') }}
                  </td>

                  <td
                    class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-text-left">

                    <div class="tw-bg-red-700 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-if="item.isClosed == false && item.isRead == false">
                      <span class="text-white tw-text-xs">منتظر پاسخ</span>
                    </div>
                    <div class="tw-bg-green-700 tw-rounded d-inline-flex justify-content-center align-items-center p-1" v-else-if="item.isClosed == false && item.isRead == true">
                      <span class="text-white tw-text-xs">پاسخ داده شده</span>
                    </div>
                    <div class="tw-bg-stone-300  tw-rounded d-inline-flex justify-content-center align-items-center p-1" style="border: 1px solid black" v-else-if="item.isClosed == true">
                      <span class="tw-text-xs tw-text-white" style="color:#36454F;">بسته شده</span>
                    </div>
                  </td>

                  <th
                    class="tw-border-b tw-border-gray-200 tw-align-middle tw-font-light tw-text-sm tw-whitespace-nowrap tw-px-2 tw-py-4 tw-flex tw-items-center tw-justify-end">
                    <svg @click="ticketRouteGenerator(item)" style="cursor: pointer" xmlns="http://www.w3.org/2000/svg" class="tw-h-5 tw-w-5" fill="none" viewBox="0 0 24 24" stroke="green" stroke-width="2">
                      <path stroke-linecap="round" stroke-linejoin="round" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
                      <path stroke-linecap="round" stroke-linejoin="round" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z" />
                    </svg>
                  </th>
                </tr>

                </tbody>
              </table>
            </div>
          </div>
          <div class="row mb-3" v-else>
            <div class="col-12 text-warning fw-bold text-center">
              هیچ تیکتی برای نمایش وجود ندارد
            </div>
          </div>
      </div>
  </div>
</template>

<script>
import PlusIcon from "../../../../components/Icons/PlusIcon"
import CustomPagination from  "../../../../components/utilities/CustomPagination"
import InputLimitation from "@/components/InputLimitation";
export default {
  layout: "PoshtebamPlusLayout",
  name: "tickets",
  components:{
    PlusIcon,
    InputLimitation,
    CustomPagination
  },
  head(){
    return{
      title: 'بانوکلاب - تیکت ها'
    }
  },
  data() {
    return {
      searchKey:'',
      SelectedTicketId: 0,
      SingleTicket: [],
      AllTickets: "",
      TicketTitle: "",
      TicketType: 0,
      TicketDescription: "",
      image: "",
      BaseImgUrl: "",
      Uploaded: false,
      pageNumber:1,
      totalPages:[]
    };
  },


  fetchOnServer() {
    return true;
  },
  async fetch() {
    try {
      const response = await this.$repositories.getAllTickets.getAllTickets({
        pageNumber:this.pageNumber,
        count:10,
      });
      this.totalPages = []
      const result = Math.ceil(response.data.ticketsCount / 10)
      for (let i = 1; i <= result; i++) {
        this.totalPages.push(i);
      }
      this.AllTickets = response.data.tickets;
    } catch (error) {
      console.log(error);
    }
  },

  computed: {
    BaseUrl() {
      return process.env.pic;
    },
    FilteredTickets(){
      const data = this.AllTickets
      if(this.searchKey===''){
        return data
      } else{
        return data.filter((element)=>{
          return element.title.match(this.searchKey) || element.ticketId === parseInt(this.searchKey)
        })
      }
    }
  },

  methods: {
    changePage(id){
      this.pageNumber = id
      this.$fetch()
    },
    openFileUpload() {
      this.$refs.file.click();
    },
    ticketRouteGenerator(ticket) {
      this.$router.push({
        path:`/social/accountsetting/tickets/${ticket.ticketId}`
      })

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
            console.log(that.image)
          };
        })(f);
        reader.readAsBinaryString(f);
      }

    },
    RemovePic(){
      this.BaseImgUrl = ''
      this.image = ''
    },
    async CreateTicket() {
      if(this.TicketTitle===''){
        this.$toast.error('لطفا عنوان تیکت را وارد کنید')
      }else if(this.TicketType===0){
        this.$toast.error('لطفا واحد مورد نظر خود را انتخاب کنید')
      }else if(this.TicketDescription===''){
        this.$toast.error('لطفا متن تیکت را وارد کنید')
      }else{
        this.$nuxt.$loading.start();
        try {

          await this.$repositories.createATicket.createATicket({
            parentId: 0,
            content: this.TicketDescription,
            type: parseInt(this.TicketType),
            title: this.TicketTitle,
            createDate:new Date(Date.now()),
            fileData: this.image,
          });
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
          this.$toast.success("تیکت با موفقیت ثبت شد");
          this.$fetch();
          this.TicketDescription = "";
          this.TicketTitle = "";
          this.TicketType = 0;
          this.BaseImgUrl =''

          this.$refs.closeTicketModal.click();

        } catch (error) {
          console.log(error);
        } finally {
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
        }
      }

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
.searchFriend {
  border: 1px solid #eaeaea;
  color: #575757;
  font-size: 14px;
  padding: 5px 10px;
  width: 100%;
  margin-bottom: 20px;
}
.form-select {
  color: #6c757d;
}
.table-row {
  cursor: pointer;
}
.Customermsg {
  background-color: #2441e7;
  border-radius: 10px;
}

.TicketFile {
  opacity: 0;
}

.File {
  opacity: 0;
  position: relative;
  bottom: 45px;
  cursor: pointer;
}

.SingleTicketCard:hover {
  transform: translateY(0px) !important;
}

.TicketImgSize {
  width: 50px;
  height: 50px;
  border-radius: 50%;
}
.activeClass {
  background-color: rgb(241, 241, 241);
  border-right: solid 0.2px blue;
}

.SendFile {
  position: relative;
}
.ParentsContainer {
  border-right: solid 0.1px rgb(224, 224, 224);
}
.DetailContainer {
  background-color: rgb(250, 250, 250);
  border-top-right-radius: 30px;
  border-bottom-right-radius: 30px;
}

.TicketsImg {
  width: 50px;
  height: 50px;
  border-radius: 50%;
}

.MyCoursesCard {
  border: none !important;
}

.MyCoursesCard:hover {
  transform: none !important;
}

.CreateTicketBtn {
  background-color: rgb(27, 213, 75);
  color: white;
}

.UserClose {
  background-color: #ff1053;
  color: white;
}

.MyCoursesButton-Overlay {
  border-radius: 50px;
  background-color: transparent;
  color: white;
  border: solid 0.5px white;
  width: 20%;
  height: 15%;
}

.MyCourses-Header-Bg {
  background-color: rgb(225, 230, 239);
  border-radius: 10px;
}
.MyCoursesBtn {
  background-size: 15px;
  background-color: white;
  border: solid 1px;
  border-top-right-radius: 10px;
  border-bottom-right-radius: 10px;
  border-top-left-radius: 0px;
  border-bottom-left-radius: 0px;
  border: 1px solid #ced4da;
  border-left: none;
}

.overlay-items {
  margin-right: 5vh;
  color: black !important;
}

.MyCoursesControl {
  border-right: none !important;
  border-top-right-radius: 0px;
  border-bottom-right-radius: 0px;
}
.Panel-BreadCrumb {
  color: black !important;
}

.DeleteUserBtn {
  background-color: #ff1053;
  color: white;
}

.DeleteUserBtn:hover {
  border: solid 1px #ff1053;
  color: #ff1053;
  background-color: white;
  transition: 0.1s ease;
}

.MyCoursesMain {
  box-shadow: 0px 1px 4px 0px rgb(0 0 0 / 9%);
  background-color: white;
  border-radius: 5px;
}
</style>
