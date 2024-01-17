<template>
<div>


  <section id="dashboard-ecommerce">
    <b-row class="match-height">
      <b-col
          xl="12"
          md="12"
      >
        <Dashboard :data="DashBoardData" />
      </b-col>
    </b-row>

  </section>
</div>

</template>

<script>
import { BRow, BCol , BCard } from 'bootstrap-vue'

import { getUserData } from '@/auth/utils'
import GetDashBoard from "@/libs/Api/Users/GetDashBoard";
import Dashboard from "@/views/components/Dashboard";

import ToastificationContent from "@core/components/toastification/ToastificationContent";

export default {
  components: {
    BRow,
    BCol,
    BCard,
    Dashboard,

  },
  data() {
    return {

        DashBoardData:null

    }
  },

  async created() {

    let _this = this;
    let getDashBoard = new GetDashBoard(_this);
    let data = {
    }
    getDashBoard.setParams(data);
    await getDashBoard.fetch(function (content){
   _this.DashBoardData = content


    },function (error){
      console.log(error)
    })


  },
}
</script>

<style lang="scss">
.logo-card{
  display: flex;
  flex-flow: column;
  justify-items: center;
  justify-content: center;
  align-items: center;

}
.bg-logo{
  margin-left: auto;
  margin-right: auto;
  width: 10rem;
  height: 10rem;
  background-repeat: no-repeat;
  background-size: contain;
  background-position: center;
  background-image: url("../../../../src/assets/images/logo/logo-jadid.png");
}
.welcome-text{
  margin-left: auto;
  margin-right: auto;
  text-align: center;
  font-weight: 900;
}
@media only screen and (max-width: 576px) {
  *{
    font-size: 1rem !important;
  }
  .bg-logo{
    width: 10rem;
    height: 10rem;
  }
}
@import '@core/scss/vue/pages/dashboard-ecommerce.scss';
@import '@core/scss/vue/libs/chart-apex.scss';
</style>
