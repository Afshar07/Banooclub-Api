<template>
  <div class="row adminPanelBox">
    <div class="col-md-12 my-3">
      <input v-model="searchKey" type="text" class="form-control" placeholder="جست و جو">
    </div>
    <div class="col-12 my-3">
      <nav>
        <div class="nav nav-tabs" id="nav-tab" role="tablist">
          <button class="nav-link active" id="nav-home-tab" data-bs-toggle="tab" data-bs-target="#nav-home"
                  type="button" role="tab" aria-controls="nav-home" aria-selected="true"> خوانده نشده
          </button>
          <button @click="checkApiRead" class="nav-link" id="nav-profile-tab" data-bs-toggle="tab"
                  data-bs-target="#nav-profile"
                  type="button" role="tab" aria-controls="nav-profile" aria-selected="false">
            خوانده شده
          </button>
        </div>
      </nav>
      <div class="tab-content" id="nav-tabContent">
        <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
          <div class="col-12 my-3">
            <v-select
              class="w-25"
              v-model="selectedTypeUnRead"
              @input="checkApiUnRead"
              :reduce="option => option.id"
              :options="typeTicket"
              label="title"
              dir="rtl"
              placeholder="دسته بندی"
            />
          </div>
          <table class="table table-bordered table-hover dataTable table-striped">
            <thead>
            <tr>
              <th >
                <div class="d-flex">
                  <div>
                    کاربر
                  </div>

                </div>
              </th>
              <th>
                <div class="d-flex">
                  <div>
                    عنوان
                  </div>

                </div>
              </th>
              <th @click="Toggleshow">
                <div class="d-flex">
                  <div>
                    تاریخ ایجاد
                  </div>
                  <div class="me-auto">
                    <font-awesome-icon icon="long-arrow-alt-up"
                                       :color="toggleArrow ?'#ccc':'#000'"
                                       size="xs"
                    />
                    <font-awesome-icon icon="long-arrow-alt-down"
                                       :color="!toggleArrow ?'#ccc':'#000'"
                                       size="xs"
                    />
                  </div>
                </div>
              </th>
              <th >
                <div class="d-flex">
                  <div>
                    وضعیت
                  </div>

                </div>
              </th>
            </tr>
            </thead>
            <tbody class="text-center">
            <tr v-for="row in unReadTicket">
              <td v-if="row.userInfo">{{ row.userInfo.name + ' ' + row.userInfo.familyName }}</td>
              <td>{{ row.title }}</td>
              <td>{{ row.createDate | moment("jYYYY/jM/jD HH:mm") }}</td>
              <td v-html="row.isRead ? 'خوانده شده' :'خوانده نشده'"></td>

              <td>
                <div class="d-flex justify-content-center">
                  <div class="">
                    <button type="button" class="btn btn-sm btn-outline-primary"
                            @click="goToTicketDetail(row.ticketId,row.type)">
                      مشاهده تیکت
                    </button>
                  </div>
                </div>
              </td>
            </tr>
            </tbody>
          </table>
          <div class="col-12">
            <nav aria-label="Page navigation example">
              <ul class="pagination">
                <li class="PaginationItemPrev" @click="updateCurrent(currentPageIndexUnRead - 1)">
                  <nuxt-link class="page-link" to="/SuperAdmin/Tickets">صفحه قبل</nuxt-link>
                </li>
                <li :class="pageIndex === pageNumberUnRead ? 'page-item active' : 'page-item'"
                    v-for="pageIndex in paginateTotalUnRead" @click="updateCurrent(pageIndex)">
                  <nuxt-link class="page-link" to="/SuperAdmin/Tickets">{{ pageIndex }}</nuxt-link>
                </li>
                <li class="PaginationItemNext" @click="updateCurrent(currentPageIndexUnRead + 1)">
                  <nuxt-link class="page-link" to="/SuperAdmin/Tickets">صفحه بعد</nuxt-link>
                </li>
              </ul>
            </nav>
          </div>
        </div>
        <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">
          <div class="col-12 my-3">
            <v-select
              class="w-25"
              v-model="selectedTypeRead"
              :reduce="option => option.id"
              @input="checkApiRead"
              :options="typeTicket"
              label="title"
              dir="rtl"
              placeholder="دسته بندی"
            />
          </div>
          <table class="table table-bordered table-hover dataTable table-striped">
            <thead>
            <tr>
              <th>
                <div class="d-flex">
                  <div>
                    کاربر
                  </div>
                </div>
              </th>
              <th >
                <div class="d-flex">
                  <div>
                    عنوان
                  </div>
                </div>
              </th>
              <th @click="sortTable('createDate')">
                <div class="d-flex">
                  <div>
                    تاریخ ایجاد
                  </div>
                  <div class="me-auto">
                    <font-awesome-icon icon="long-arrow-alt-up"
                                       :color="toggleArrow ?'#ccc':'#000'"
                                       size="xs"
                    />
                    <font-awesome-icon icon="long-arrow-alt-down"
                                       :color="!toggleArrow ?'#ccc':'#000'"
                                       size="xs"
                    />
                  </div>
                </div>
              </th>
              <th >
                <div class="d-flex">
                  <div>
                    وضعیت
                  </div>

                </div>
              </th>
            </tr>
            </thead>
            <tbody class="text-center">
            <tr v-for="row in readTicket">
              <td v-if="row.userInfo">{{ row.userInfo.name + ' ' + row.userInfo.familyName }}</td>
              <td>{{ row.title }}</td>
              <td>{{ row.createDate | moment("jYYYY/jM/jD HH:mm") }}</td>
              <td v-html="row.isRead ? 'خوانده شده' :'خوانده نشده'"></td>

              <td>
                <div class="d-flex justify-content-center">
                  <div class="">
                    <button type="button" class="btn btn-sm btn-outline-primary"
                            @click="goToTicketDetail(row.ticketId,row.type)">
                      مشاهده تیکت
                    </button>
                  </div>
                </div>
              </td>
            </tr>
            </tbody>
          </table>
          <div class="col-12">
            <nav aria-label="Page navigation example">
              <ul class="pagination">
                <li class="PaginationItemPrev" @click="updateCurrentAccept(currentPageIndexRead - 1)">
                  <nuxt-link class="page-link" to="/SuperAdmin/Tickets">صفحه قبل</nuxt-link>
                </li>
                <li :class="pageIndex === pageNumberRead ? 'page-item active' : 'page-item'"
                    v-for="pageIndex in paginateTotalRead" @click="updateCurrentAccept(pageIndex)">
                  <nuxt-link class="page-link" to="/SuperAdmin/Tickets">{{ pageIndex }}</nuxt-link>
                </li>
                <li class="PaginationItemNext" @click="updateCurrentAccept(currentPageIndexRead + 1)">
                  <nuxt-link class="page-link" to="/SuperAdmin/Tickets">صفحه بعد</nuxt-link>
                </li>
              </ul>
            </nav>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Tickets",
  layout: 'SuperAdminPanel',
  data() {
    return {
      allTicket: [],
      readTicket: [],
      unReadTicket: [],
      ascending: false,
      sortColumn: '',
      toggleArrow: false,
      totalCountRead: null,
      totalCountUnRead: null,
      paginateTotalRead: null,
      paginateTotalUnRead: null,
      count: 10,
      pageNumberUnRead: 1,
      pageNumberRead: 1,
      currentPageIndexUnRead: 1,
      currentPageIndexRead: 1,
      selectedTypeUnRead: 1,
      selectedTypeRead: 1,
      searchKey:null,
      typeTicket: [
        {title: "احراز هویت", id: 1},
        {title: "پشتیبانی", id: 2},
        {title: "سایر", id: 3},
      ],
    }
  },

  watch:{
      searchKey:function (val,oldVal){
        this.getTicketByType(this.selectedTypeUnRead, false, this.pageNumberRead, this.count,this.searchKey);
      }
  },
  methods: {
    getAllTicket() {
      this.$axios.post(`Ticket/GetAll`, null, {
       
        params: {
          pageNumber: 1,
          count: 10
        }
      }).then((response) => {
        if (response.status === 200) {
          this.allTicket = response.data.tickets
          this.unreadTicketCount = this.allTicket.filter(e => e.isRead === false).length
          this.readTicketCount = this.allTicket.filter(e => e.isRead === true).length
          // this.totalCount = response.data.ticketCount
          this.totalCount = this.unreadTicketCount
          this.paginateTotalRead = Math.ceil(this.totalCount / this.count);
          this.totalCountAccept = this.readTicketCount
          this.paginateTotalUnRead = Math.ceil(this.totalCountAccept / this.count);
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false
          this.unreadTicketCount = this.allTicket.filter(e => e.isRead === false).length
          this.$store.commit('setUnreadTicketCount', {
            payload: this.unreadTicketCount
          });
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    updateCurrent(pageNumber) {
      if (pageNumber === 0 || pageNumber > this.paginateTotalUnRead) {
        return true
      } else {
        this.$nuxt.$loading.start();
        this.getAllTicket(pageNumber, this.count);
        this.pageNumberUnRead = pageNumber;
        this.currentPageIndexUnRead = pageNumber;
      }
    },
    updateCurrentAccept(pageNumber) {
      if (pageNumber === 0 || pageNumber > this.paginateTotalUnRead) {
        return true;
      } else {
        this.$nuxt.$loading.start();
        this.getAllTicket(this.pageNumberRead, this.count);
        this.pageNumberRead = pageNumber;
        this.currentPageIndexRead = pageNumber;
      }

    },
    Toggleshow(){
      this.toggleArrow = !this.toggleArrow


    },
    // sortTable(col) {
    //   if (this.sortColumn === col) {
    //     this.ascending = !this.ascending;
    //   } else {
    //     this.ascending = true;
    //     this.sortColumn = col;
    //   }
    //
    //   var ascending = this.ascending;
    //
    //   this.allTicket.sort(function (a, b) {
    //     if (a[col] > b[col]) {
    //       return ascending ? 1 : -1
    //     } else if (a[col] < b[col]) {
    //       return ascending ? -1 : 1
    //     }
    //     return 0;
    //   })
    // },
    goToTicketDetail(ticketId, type) {
      this.$router.push({path: '/SuperAdmin/TicketDetails', query: {ticketId, type}})
      this.$axios.post(`Ticket/ChangeTicketStatus?parentId=${ticketId}`,null,{
       
      })
    },
    getTicketByType(type, isRead, pageNumber, count,search) {
      this.$axios.post(`Ticket/GetTicketsByFilter`, null, {
       
        params: {
          userType: 1,
          type: type,
          pageNumber: pageNumber,
          count: count,
          isRead: isRead,
          search:search
        }
      }).then((response) => {
        if (response.status === 200) {
          if (isRead === true) {
            this.readTicket = response.data.tickets
            this.totalCountRead = response.data.ticketCounts
            this.paginateTotalRead = Math.ceil(this.totalCountRead / this.count);
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false

          }
          if (isRead === false) {
            this.unReadTicket = response.data.tickets
            this.totalCountUnRead = response.data.ticketCounts
            this.paginateTotalUnRead = Math.ceil(this.totalCountUnRead / this.count);
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false
          }
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    checkApiUnRead() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start()
      })
      this.getTicketByType(this.selectedTypeUnRead, false, this.pageNumberRead, this.count,this.searchKey);
      console.log(this.selectedTypeUnRead)
    },
    checkApiRead() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start()
      })
      this.getTicketByType(this.selectedTypeRead, true, this.pageNumberUnRead, this.count,this.searchKey);
    }
  },
  computed: {
    SortByDate(){

      if(!this.toggleArrow){
        return this.unReadTicket
      }else{

        return    this.unReadTicket.sort(function (a, b) {
          const key1 = a.CreateDate;
          const key2 = b.CreateDate;

          if (key1 < key2) {
            return -1;
          } else if (key1 === key2) {
            return 0;
          } else {
            return 1;
          }
        })

      }
    },
    columns() {
      if (this.allTicket.length === 0) {
        return [];
      }
      return Object.keys(this.allTicket[0])
    }
  },
  mounted() {
    this.$nextTick(() => {
      this.$nuxt.$loading.start()
    })
    // this.getAllTicket();
    this.getTicketByType(this.selectedTypeUnRead, false, this.pageNumberRead, this.count,this.searchKey);
  }
}
</script>

<style scoped>
.adminPanelBox {
  background-color: #fff;
  padding: 1rem;
}
</style>
