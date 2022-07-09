<template>
  <div class="row adminPanelBox">
    <div class="col-12">
      <input
        type="text"
        class="w-50 border rounded py-2 mb-5"
        v-model="searchKey"
        placeholder="جستجو ..."
      />
    </div>
    <!--    <transition-group name="list">-->
    <div
      class="col-12 col-sm-6 col-md-4 d-flex flex-row my-2"
      v-for="item in filterUserFunction"
      :key="item.userId"
    >
      <div class="card bg-light d-flex flex-fill position-relative">
        <div class="card-header text-muted border-bottom-0">
          <b> {{ item.name + " " + item.familyName }}</b>
        </div>
        <div class="card-body pt-0">
          <div class="row">
            <div class="col-7">
              <div class="d-flex flex-column">
                <div class="my-2">
                  <div class="d-flex gap-2">
                    <div>
                      <font-awesome-icon icon="phone" color="#999" />
                    </div>
                    <div class="text-muted small">{{ item.mobile }}</div>
                  </div>
                </div>
                <div class="my-2">
                  <div class="d-flex gap-2">
                    <div>
                      <font-awesome-icon icon="envelope" color="#999" />
                    </div>
                    <div class="text-muted small">{{ item.email }}</div>
                  </div>
                </div>
                <div class="my-2">
                  <div class="d-flex gap-2">
                    <div>
                      <font-awesome-icon icon="user-tie" color="#999" />
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-5 text-center position-relative">
              <img
                v-if="item.selfieFileData"
                :src="`https://banooclubapi.simagar.com/${item.selfieFileData}`"
                class="img-fluid user-avatar"
                alt="user-avatar"
              />
              <img
                v-else
                src="../../assets/images/defaultUser.png"
                class="img-fluid user-avatar"
                alt="user-avatar"
              />
            </div>
          </div>
        </div>
        <div class="card-footer">
          <div class="d-flex flex-row gap-3">
            <!--            <div>-->
            <!--              <button class="btn btn-sm btn-danger text-white" @click="deleteUser(item.userId)">-->
            <!--                <font-awesome-icon icon="trash-alt"/>-->
            <!--              </button>-->
            <!--            </div>-->
            <div>
              <button
                @click="dataModal(item)"
                data-bs-toggle="modal"
                data-bs-dismiss="modal"
                data-bs-target="#staticBackdropConfirm"
                class="btn btn-sm btn-info text-white"
              >
                <font-awesome-icon icon="user-lock" v-if="item.status === 2" />
                <font-awesome-icon icon="unlock-alt" v-if="item.status === 1" />
              </button>
            </div>
            <!--            <div >-->
            <!--              <a @click="goToUserProfile(item.userId)" target="_blank">-->
            <!--                <font-awesome-icon icon="eye" size="lg"/>-->
            <!--              </a>-->
            <!--            </div>-->
          </div>
        </div>
        <div
          class="modal fade modalConfirm"
          id="staticBackdropConfirm"
          data-bs-backdrop="static"
          data-bs-keyboard="false"
          tabindex="-1"
          aria-labelledby="staticBackdropLabelConfirm"
          aria-hidden="true"
        >
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="staticBackdropLabelConfirm">
                  تغییر وضعیت کاربر
                </h5>
                <button
                  type="button"
                  class="btn-close"
                  data-bs-dismiss="modal"
                  aria-label="Close"
                ></button>
              </div>
              <div class="modal-body">
                آیا از تغییر وضعیت کاربر اطمینان دارید ؟
              </div>
              <div class="modal-footer">
                <button
                  type="button"
                  class="btn btn-sm btn-outline-secondary closeApprove2"
                  data-bs-dismiss="modal"
                >
                  خیر
                </button>
                <button
                  type="button"
                  class="btn btn-sm btn-outline-primary"
                  data-bs-dismiss="modal"
                  data-bs-target="#staticBackdrop"
                  @click="activeChange()"
                >
                  تایید
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!--    </transition-group>-->
    <div class="col-12">
      <nav aria-label="Page navigation example">
        <ul class="pagination">
          <li
            class="PaginationItemPrev"
            @click="updateCurrent(currentPageIndex - 1)"
          >
            <nuxt-link class="page-link" to="/SuperAdmin">صفحه قبل</nuxt-link>
          </li>
          <li
            :class="pageIndex === pageNumber ? 'page-item active' : 'page-item'"
            v-for="pageIndex in paginateTotal"
            :key="pageIndex"
            @click="updateCurrent(pageIndex)"
          >
            <nuxt-link class="page-link" to="/SuperAdmin">{{
              pageIndex
            }}</nuxt-link>
          </li>
          <li
            class="PaginationItemNext"
            @click="updateCurrent(currentPageIndex + 1)"
          >
            <nuxt-link class="page-link" to="/SuperAdmin">صفحه بعد</nuxt-link>
          </li>
        </ul>
      </nav>
    </div>
  </div>
</template>

<script>
export default {
  name: "index",
  layout: "SuperAdminPanel",
  data() {
    return {
      allUser: [],
      isActive: true,
      userFilter: "",
      userSelect: {},
      totalCount: null,
      paginateTotal: null,
      count: 10,
      pageNumber: 1,
      currentPageIndex: 1,
      searchKey: null,
    };
  },

  watch: {
    searchKey: function (val, oldVal) {
      this.$axios
        .post(`Users/GetAll`, null, {
          params: {
            pageNumber: 1,
            count: 10,
            search: val,
          },
        })
        .then((response) => {
          if (response.status === 200) {
            this.allUser = response.data.users;
            this.totalCount = response.data.usersCount;
            this.paginateTotal = Math.ceil(this.totalCount / this.count);
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false;
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
  methods: {
    goToUserProfile(userId) {
      if (userId === this.$auth.user.userInfo.userId) {
        this.$router.push("/Social");
      } else {
        this.$router.push({
          path: "/Social/AccountSetting/Posts",
          query: { userId: userId },
        });
        this.$store.commit("SetSearchedUserId", userId);

        this.$axios
          .post(`Common/GetUserIndex?userId=${userId}`, null, {})
          .then((res) => {
            this.$store.commit("SetUserData", res.data);
          });
      }
    },
    dataModal(data) {
      this.userSelect = data;
    },
    getAllUser(pageNumber, count, search) {
      this.$axios
        .post(`Users/GetAll`, null, {
          params: {
            pageNumber: pageNumber,
            count: count,
            search: search,
          },
        })
        .then((response) => {
          if (response.status === 200) {
            this.allUser = response.data.users;
            this.totalCount = response.data.usersCount;
            this.paginateTotal = Math.ceil(this.totalCount / this.count);
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false;
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    activeChange() {
      this.$axios
        .post(
          `/Users/UpdateUserByAdmin`,
          {
            userId: this.userSelect.userId,
            status: this.userSelect.status === 1 ? 2 : 1,
          },
          {}
        )
        .then((response) => {
          if (response.status === 200) {
            this.$nuxt.$loading.start();
            this.getAllUser(this.pageNumber, this.count, this.searchKey);
            this.$toast.success("تغییر وضعیت کاربر با موفقیت انجام شد.");
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    deleteUser(userId) {
      this.$axios
        .post(`Users/Delete`, null, {
          params: {
            id: userId,
          },
        })
        .then((response) => {
          if (response.status === 200) {
            this.getAllUser(this.pageNumber, this.count, this.searchKey);
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    updateCurrent(pageNumber) {
      if (pageNumber === 0 || pageNumber > this.paginateTotal) {
        return true;
      } else {
        this.$nuxt.$loading.start();
        this.getAllUser(pageNumber, this.count, this.searchKey);
        this.pageNumber = pageNumber;
        this.currentPageIndex = pageNumber;
      }
    },
  },
  mounted() {
    this.$nextTick(() => {
      this.$nuxt.$loading.start();
    });
    this.getAllUser(this.pageNumber, this.count, this.searchKey);
  },
  computed: {
    filterUserFunction: function () {
      var filter_name = this.userFilter;
      return this.userFilter !== ""
        ? this.allUser.filter(
            (e) =>
              e.userSetting.bio.includes(filter_name) ||
              e.name.includes(filter_name)
          )
        : this.allUser;
    },
  },
};
</script>

<style scoped>
#staticBackdropConfirm {
  margin: 25vh auto;
  left: 0;
}

.adminPanelBox {
  background-color: #fff;
  padding: 1rem;
}

.user-avatar {
  vertical-align: middle;
  border-style: none;
  display: block;
  height: 100%;
  left: 0;
  object-fit: cover;
  position: absolute;
  top: 0;
  width: 100%;
}

.btn-close {
  position: absolute;
  left: 1rem;
}

.card {
  color: #343a40;
  cursor: pointer;
}

input::placeholder {
  font-size: 16px;
  color: #cccccc;
}

.bg-light {
  color: #1f2d3d !important;
}

.list-enter-from {
}

.list-enter-active {
  animation: list 0.3s ease-out;
}

.list-leave-active {
  animation: list 0.3s ease-in reverse;
}

.list-enter-to {
}

@keyframes list {
  from {
    opacity: 0;
    transform: translateX(-25px) scale(1);
  }
  to {
    opacity: 1;
    transform: translateX(0) scale(1);
  }
}

@media (max-width: 576px) {
  .SectionBox {
    width: 100%;
  }
}
</style>
