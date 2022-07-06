<template>
  <div class="d-flex flex-row gap-2 SuperAdminHeader">
    <!--    <div class="">-->
    <!--      <h5 class="text-primary fw-bold">پنل ادمین</h5>-->
    <!--    </div>-->
    <div class="">
      PoshtebamPlus
<!--      <img src="~/assets/images/PoshtebamPlusLogo.png" class="img-fluid" />-->
    </div>
    <!--    <div class="">-->
    <!--      <h6>-->
    <!--        <nuxt-link class="text-secondary" to="/SuperAdmin">-->
    <!--          خانه-->
    <!--        </nuxt-link>-->
    <!--      </h6>-->
    <!--    </div>-->
    <!--    <div class="">-->
    <!--      <h6>-->
    <!--        <nuxt-link class="text-secondary" to="/SuperAdmin">-->
    <!--          ارتباط با پشتیبانی-->
    <!--        </nuxt-link>-->
    <!--      </h6>-->
    <!--    </div>-->
    <div class="me-auto">
      <div class="d-flex flex-row gap-2 py-1">
        <!--        <div class="headerItem">-->
        <!--          <span class="position-absolute number text-secondary fw-bold">10</span>-->
        <!--          <font-awesome-icon icon="bell" size="2x" color="#343a40"/>-->
        <!--        </div>-->
        <!--        <div class="headerItem">-->
        <!--          <span class="position-absolute number bg-warning text-primary fw-bold">20</span>-->
        <!--          <font-awesome-icon icon="comment" size="2x" color="#343a40"/>-->
        <!--        </div>-->
        <div class="headerItem" @click="goToTicket">
          <span class="position-absolute number">{{
            $store.state.unreadTicketCount
          }}</span>
          <FontAwesomeIcon icon="envelope" size="x" color="#3f6791" />
        </div>
        <div class="">
          <button @click="logoutAdmin" class="btn btn-sm text-secondary py-0">
            خروج
          </button>
        </div>
        <!--        <div class="headerItem" @click="$emit('sidenavToggleAdmin')">-->
        <!--          <font-awesome-icon icon="cogs" size="2x" color="#343a40"/>-->
        <!--        </div>-->
      </div>
      <!--      <nuxt-link to="/">-->
      <!--        <button class="btn btn-sm text-secondary py-0">خروج</button>-->
      <!--      </nuxt-link>-->
    </div>
  </div>
</template>

<script>
export default {
  name: "SuperAdminHeader",
  props: {
    unreadTicketCount: {
      type: Number,
      required: false,
    },
  },
  methods: {
    async logoutAdmin() {
      await this.$auth.logout();
    },

    goToTicket() {
      this.$router.push({ path: "/SuperAdmin/Tickets" });
    },
  },
};
</script>

<style scoped>
.img-fluid {
  max-width: 40%;
}

.SuperAdminHeader {
  padding: 2rem 1rem 1rem 1rem;
  vertical-align: middle;
  background: #f9f9f9;
}

.headerItem {
  cursor: pointer;
  position: relative;
}

.number {
  top: -1rem;
  left: 0.5rem;
  font-size: 16px;
  border-radius: 50%;
  width: 20px;
  height: 20px;
  background: #7cb4ec;
  color: #fff;
  text-align: center;
}

a {
  text-decoration: none;
}
</style>
