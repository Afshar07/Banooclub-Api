<template>
  <div class="container-fluid main-container bg-white mb-4" v-if="ticketData">
    <h1 class="p-4">موضوع تیکت: {{ ticketData[0].content }}</h1>
    <div class="col-12">
      <div
        v-for="item in ticketData"
        :key="item.ticketId"
        class="card w-75 my-5 parent"
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
                class="col-12 d-flex"
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
                    <small> <i class="fas text-muted fa-clock"></i></small>
                    <small class="text-muted">{{
                      item.createDate | moment("jYYYY/jM/jD HH:mm")
                    }}</small>
                  </p>
                </div>
              </div>

              <p
                class="card-text text-secondary"
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
        <div class="col-md-6">
          <input
            v-model="ticketTitle"
            type="text"
            class="form-control"
            placeholder="عنوان تیکت"
          />
        </div>

        <div class="col-md-6">
          <textarea
            v-model="ticketDescription"
            type="text"
            rows="1"
            class="form-control"
            placeholder="متن تیکت"
          ></textarea>
        </div>
        <div class="col-md-12">
          <div class="row">
            <div class="col-md-12 d-flex justify-content-start my-3">
              <button class="btn btn-primary" @click="openFileUpload">
                آپلود عکس
              </button>
              <input
                ref="file"
                type="file"
                class="File d-none"
                @change="UploadFile"
              />
            </div>
          </div>
        </div>
        <div class="col-md-12 mb-5">
          <button class="btn btn-primary w-100" @click="SubmitReply">
            ثبت
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  layout: "PoshtebamPlusLayout",
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
    } catch (error) {
      console.log(error);
    }
  },
  data() {
    return {
      ticketData: null,
      ticketTitle: null,
      ticketDescription: null,
      image: null,
    };
  },
  methods: {
    async SubmitReply() {
      this.$nuxt.$loading.start();
      try {
        await this.$repositories.createATicket.createATicket({
          parentId: this.$route.params.id,
          content: this.ticketDescription,
          type: 0,
          title: this.ticketTitle,
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
  border: 1px solid #198754;
}
.my-ticket:after {
  content: "";
  position: absolute;
  top: 30%;
  left: 100%;
  border-left: 20px solid #198754;
  border-left-color: #198754;
  border-top: 20px solid transparent;
  border-bottom: 20px solid transparent;
  border-right: 20px solid transparent;
}
.your-ticket {
  border: 1px solid #0d6efd;
}
.your-ticket:after {
  content: "";
  position: absolute;
  top: 30%;
  right: 100%;
  border-right: 20px solid #0d6efd;
  border-right-color: #0d6efd;
  border-top: 20px solid transparent;
  border-bottom: 20px solid transparent;
  border-left: 20px solid transparent;
}
</style>
