<template>
  <div class="container mcontainer">
    <div class="row">
      <div class="row mb-3 p-4 mx-auto col-12">
        <div class="col-12">
          <div class="col-md-12 my-5 mt-2">
            <h1 class="tw-text-2xl tw-font-semibold">تیکت ها</h1>
          </div>

          <div class="col-12" style="overflow-y: scroll">
<!--            <h2>لیست تیکت ها</h2>-->
<!--            <hr />-->
            <div
              class="d-flex flex-column flex-md-row align-content-center gap-1 gap-md-3"
            >
              <table class="table">
                <thead>
                <tr>
                  <th scope="col">#</th>
                  <th scope="col">نام</th>
                  <th scope="col">موضوع</th>
                  <th scope="col">تاریخ</th>
                </tr>
                </thead>
                <tbody>
                <tr
                  class="table-row"
                  v-for="(item, index) in AllTickets"
                  :key="item.ticketId"
                  @click="ticketRouteGenerator(item)"
                >
                  <th scope="row">
                    {{ index + 1 }}
                  </th>
                  <td>{{ item.title.substr(0, 7) }}</td>
                  <td>
                    <span v-if="item.type === 0">احراز هویت</span>
                    <span v-if="item.type === 1">پشتیبانی</span>
                    <span v-if="item.type === 2">سایر</span>
                  </td>
                  <td>
                    {{ item.createDate | moment("jYYYY/jM/jD HH:mm") }}
                  </td>
                </tr>
                </tbody>
              </table>
            </div>
          </div>
          <div class="col-md-12">
            <!--- Main  Ticket Tab --->
            <div
              id="v-pills-home"
              class="tab-pane fade show active"
              role="tabpanel"
              aria-labelledby="v-pills-home-tab"
            >
              <div
                v-for="item in SingleTicket"
                :key="item.ticketId"
                class="card SingleTicketCard Stu my-5"
              >
                <div class="row g-0">
                  <div class="col-12 col-sm-10">
                    <div class="card-body">
                      <div class="row">
                        <div class="col-12 d-flex justify-content-between">
                          <div>
                            <strong class="card-text">
                              <span
                                v-if="item.userType === 1"
                                class="badge pill bg-success mb-2"
                                style="line-height: 1.5rem"
                              >من</span
                              >
                              <span
                                v-else
                                class="badge pill bg-primary mb-2"
                                style="line-height: 1.5rem"
                              >اپراتور</span
                              >
                            </strong>
                            <p
                              class="card-text gap-2 d-flex align-items-center"
                            >
                              <small>
                                <i class="fas text-muted fa-clock"></i
                                ></small>
                              <small class="text-muted">{{
                                  item.createDate | moment("jYYYY/jM/jD HH:mm")
                                }}</small>
                            </p>
                          </div>
                        </div>
                        <div
                          class="col-md-6 d-flex justify-content-end my-3"
                        ></div>
                      </div>

                      <p class="card-text text-secondary">
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
                <div></div>
              </div>

              <!--- Create Ticket --->
              <div class="row mx-auto mt-4">
                <h2>تیکت جدید</h2>
                <hr />
                <div class="col-6 my-3">
                  <input
                    v-model="TicketTitle"
                    type="text"
                    class="form-control"
                    placeholder="عنوان تیکت"
                  />
                </div>
                <div class="col-6 my-3">
                  <select
                    id="categoryType"
                    v-model="TicketType"
                    class="form-control"
                    placeholder="Select Issue"
                  >
                    <option :value="1">احراز هویت</option>
                    <option :value="2" selected>پشتیبانی</option>
                    <option :value="3">سایر</option>
                  </select>
                </div>
                <div class="col-12 my-4">
                  <textarea
                    v-model="TicketDescription"
                    class="form-control"
                    placeholder="متن تیکت"
                  ></textarea>
                </div>
                <div class="col-md-12">
                  <div class="row">
                    <div class="col-md-2  d-flex justify-content-start my-3">
                      <button class="tw-h-10 tw-bg-[#ef4444] hover:tw-bg-white hover:tw-text-[#ef4444] tw-border-solid border-1 tw-border-[#ef4444] tw-rounded w-100 tw-transition tw-text-white  tw-p-2" @click="openFileUpload">
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
                <div class="col-md-12">
                  <button class=" tw-bg-[#ef4444] hover:tw-bg-white hover:tw-text-[#ef4444] tw-border-solid border-1 tw-border-[#ef4444] tw-rounded w-100 tw-transition tw-text-white  tw-p-2" @click="CreateTicket">
                    ثبت
                  </button>
                </div>
              </div>
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
  name: "tickets",

  fetchOnServer() {
    return true;
  },
  async fetch() {
    try {
      const response = await this.$repositories.getAllTickets.getAllTickets();
      this.AllTickets = response.data.tickets;
    } catch (error) {
      console.log(error);
    }
  },

  data() {
    return {
      SelectedTicketId: 0,
      SingleTicket: [],
      AllTickets: "",
      TicketTitle: "",
      TicketType: 0,
      TicketDescription: "",
      image: "",
      BaseImgUrl: "",
      Uploaded: false,
    };
  },
  computed: {
    BaseUrl() {
      return process.env.pic;
    },
  },

  methods: {
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
          this.TicketType = 0
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
