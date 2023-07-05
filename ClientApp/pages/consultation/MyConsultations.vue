<template>
  <div class="mcontainer NewBg position-relative  px-2">
    <div class="row">
      <div class="col-md-12 bg-white p-3 mb-3    rounded shadow">
        <div class="row">
          <div class="col-md-3">
            <h1 class="h4 mb-0 text-center mt-1">مشاورین من</h1>
          </div>
          <div class="col-md-9">
            <input v-model="debouncer" class="with-border FormInputs" placeholder="جستجو" type="text">
          </div>
        </div>
      </div>
      <template v-if="myConsultations && myConsultations.length>0">
        <div v-for="(item,idx) in myConsultations" :key="idx" class="col-md-12 my-3 ">
          <nuxt-link class="bg-white text-black cursor-pointer shadow row rounded p-md-4 p-2 row"
                     style="min-height: 10rem;text-decoration: none"
                     to="/">
            <div class="col-md-6 ">
              <div class="d-flex gap-3 h-100 align-items-start">
                <div class="rounded-circle  d-flex flex-column justify-content-center align-items-center"
                     style="height: 100%">
                  <img v-if="item.userPic" :src="baseUrl+'/' +item.userPic" alt=""
                       style="object-fit: contain;width: 5rem;height: 5rem">
                  <img v-else alt="" src="/defaultUser.png" style="object-fit: contain;width: 5rem;height: 5rem">
                </div>
                <div class="d-flex flex-column h-100 justify-content-center align-items-start">
                  <div class="d-flex flex-column justify-content-start align-items-start">
                    <h5 class="tw-font-bold">{{ item.name + ' ' + item.lName }}</h5>
                    <span class="text-muted">{{ item.cats }}</span>
                  </div>

                </div>
              </div>
            </div>
            <div class="col-md-6 my-md-0 my-3">
              <div class="d-flex flex-column justify-content-center h-100 ">
                <div class="d-flex flex-column align-items-start gap-2">
                  <div class="d-flex gap-1">
                    <small class="text-muted">وضعیت پرداخت : </small>
                    <span v-if="item.isPayed" class="badge pill bg-success text-white">پرداخت شده</span>
                    <span v-else class="badge pill bg-danger text-white">پرداخت نشده</span>
                  </div>
                  <div class="d-flex gap-1">
                    <small class="text-muted">وضعیت مشاوره : </small>
                    <small v-if="item.status===1" class="badge pill bg-secondary text-white">در انتظار</small>
                    <small v-if="item.status===2" class="badge pill bg-danger text-white">تمام شده</small>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-md-12 mt-3 d-flex justify-content-end align-items-center">
            <span class="d-flex align-items-center align-self-end gap-2">
                <small class="text-pink">دریافت مشاوره</small>
                <LazyArrowLeftIcon style="width: 20px;height: 20px;fill: #fc5399"></LazyArrowLeftIcon>
                </span>
            </div>
          </nuxt-link>
        </div>
      </template>
      <div class="col-md-12 bg-white p-3 mb-3    rounded shadow">
        <LazyPagination v-if="totalPages.length>1" :pages="totalPages" @PageChanged="changePage"></LazyPagination>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  layout: "PoshtebamPlusLayout",
  name: "MyConsultations",
  data() {
    return {
      myConsultations: null,
      searchCommand: '',
      totalPages: [],
      isRequesting:false,
      timeout: null,
      page: 1,
    }
  },
  head() {
    return {
      title: 'مشاورین من'
    }
  },
  fetchOnServer: false,
  computed: {
    baseUrl() {
      return process.env.pic
    },
    debouncer: {
      get() {
        return this.searchCommand
      },
      set(val) {
        if (this.timeout) {
          clearTimeout(this.timeout)
        }
        this.timeout = setTimeout(() => {
          this.searchCommand = val
        }, 600)
      }
    }
  },
  watch: {
    searchCommand: function (val) {
      this.getMyConsultations()
    }
  },
  async fetch() {
    await Promise.all([
      this.getMyConsultations()
    ])
  },

  methods: {
    changePage(id) {
      this.page = id
      this.getMyConsultations()
    },
    async getMyConsultations() {
      try {
        const res = await this.$repositories.getMyConsultations.setParams({
          fullname: this.searchCommand,
          pageNumber: this.page,
          take: 5
        })
        this.myConsultations = res.data.items
        this.totalPages = []
        const result = Math.ceil(res.data.totalCount / 5)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
        }
      } catch (e) {
        console.log(e)
      }
    },
  }
}
</script>

<style scoped>

</style>
