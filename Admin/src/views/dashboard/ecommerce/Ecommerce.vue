<template>

  <b-card style="min-height: 80vh" class="logo-card">

    <div class="bg-logo"></div>
    <h1 class="text-primary welcome-text">به پنل ادمین بانو کلاب خوش آمدید.</h1>

  </b-card>

<!--  <section id="dashboard-ecommerce">-->
<!--    <b-row class="match-height">-->
<!--      <b-col-->
<!--        xl="4"-->
<!--        md="6"-->
<!--      >-->
<!--        <ecommerce-medal :data="data.congratulations" />-->
<!--      </b-col>-->
<!--      <b-col-->
<!--        xl="8"-->
<!--        md="6"-->
<!--      >-->
<!--        <ecommerce-statistics :data="data.statisticsItems" />-->
<!--      </b-col>-->
<!--    </b-row>-->

<!--    <b-row class="match-height">-->
<!--      <b-col lg="4">-->
<!--        <b-row class="match-height">-->
<!--          &lt;!&ndash; Bar Chart - Orders &ndash;&gt;-->
<!--          <b-col-->
<!--            lg="6"-->
<!--            md="3"-->
<!--            cols="6"-->
<!--          >-->
<!--            <ecommerce-order-chart :data="data.statisticsOrder" />-->
<!--          </b-col>-->
<!--          &lt;!&ndash;/ Bar Chart - Orders &ndash;&gt;-->
<!--          <b-col-->
<!--            lg="6"-->
<!--            md="3"-->
<!--            cols="6"-->
<!--          >-->
<!--            <ecommerce-profit-chart :data="data.statisticsProfit" />-->
<!--          </b-col>-->
<!--          <b-col-->
<!--            lg="12"-->
<!--            md="6"-->
<!--          >-->
<!--            <ecommerce-earnings-chart :data="data.earningsChart" />-->
<!--          </b-col>-->
<!--        </b-row>-->
<!--      </b-col>-->

<!--      &lt;!&ndash; Revenue Report Card &ndash;&gt;-->
<!--      <b-col lg="8">-->
<!--        <ecommerce-revenue-report :data="data.revenue" />-->
<!--      </b-col>-->
<!--      &lt;!&ndash;/ Revenue Report Card &ndash;&gt;-->
<!--    </b-row>-->

<!--    <b-row class="match-height">-->
<!--      &lt;!&ndash; Company Table Card &ndash;&gt;-->
<!--      <b-col lg="8">-->
<!--        <ecommerce-company-table :table-data="data.companyTable" />-->
<!--      </b-col>-->
<!--      &lt;!&ndash;/ Company Table Card &ndash;&gt;-->

<!--      &lt;!&ndash; Developer Meetup Card &ndash;&gt;-->
<!--      <b-col-->
<!--        lg="4"-->
<!--        md="6"-->
<!--      >-->
<!--        <ecommerce-meetup :data="data.meetup" />-->
<!--      </b-col>-->
<!--      &lt;!&ndash;/ Developer Meetup Card &ndash;&gt;-->

<!--      &lt;!&ndash; Browser States Card &ndash;&gt;-->
<!--      <b-col-->
<!--        lg="4"-->
<!--        md="6"-->
<!--      >-->
<!--        <ecommerce-browser-states />-->
<!--      </b-col>-->
<!--      &lt;!&ndash;/ Browser States Card &ndash;&gt;-->

<!--      &lt;!&ndash; Goal Overview Card &ndash;&gt;-->
<!--      <b-col-->
<!--        lg="4"-->
<!--        md="6"-->
<!--      >-->
<!--        <ecommerce-goal-overview :data="data.goalOverview" />-->
<!--      </b-col>-->
<!--      &lt;!&ndash;/ Goal Overview Card &ndash;&gt;-->

<!--      &lt;!&ndash; Transaction Card &ndash;&gt;-->
<!--      <b-col-->
<!--        lg="4"-->
<!--        md="6"-->
<!--      >-->
<!--        <ecommerce-transactions :data="data.transactionData" />-->
<!--      </b-col>-->
<!--      &lt;!&ndash;/ Transaction Card &ndash;&gt;-->
<!--    </b-row>-->
<!--  </section>-->
</template>

<script>
import { BRow, BCol , BCard } from 'bootstrap-vue'

import { getUserData } from '@/auth/utils'
import EcommerceMedal from './EcommerceMedal.vue'
import EcommerceStatistics from './EcommerceStatistics.vue'
import EcommerceRevenueReport from './EcommerceRevenueReport.vue'
import EcommerceOrderChart from './EcommerceOrderChart.vue'
import EcommerceProfitChart from './EcommerceProfitChart.vue'
import EcommerceEarningsChart from './EcommerceEarningsChart.vue'
import EcommerceCompanyTable from './EcommerceCompanyTable.vue'
import EcommerceMeetup from './EcommerceMeetup.vue'
import EcommerceBrowserStates from './EcommerceBrowserStates.vue'
import EcommerceGoalOverview from './EcommerceGoalOverview.vue'
import EcommerceTransactions from './EcommerceTransactions.vue'


export default {
  components: {
    BRow,
    BCol,
    BCard,

    EcommerceMedal,
    EcommerceStatistics,
    EcommerceRevenueReport,
    EcommerceOrderChart,
    EcommerceProfitChart,
    EcommerceEarningsChart,
    EcommerceCompanyTable,
    EcommerceMeetup,
    EcommerceBrowserStates,
    EcommerceGoalOverview,
    EcommerceTransactions,
  },
  data() {
    return {
      data: {},
    }
  },
  created() {
    // data
    this.$http.get('/ecommerce/data')
      .then(response => {
        this.data = response.data

        // ? Your API will return name of logged in user or you might just directly get name of logged in user
        // ? This is just for demo purpose
        const userData = getUserData()
        // this.data.congratulations.name = userData.fullName.split(' ')[0] || userData.username
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
  width: 40rem;
  height: 40rem;
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
    width: 20rem;
    height: 20rem;
  }
}
@import '@core/scss/vue/pages/dashboard-ecommerce.scss';
@import '@core/scss/vue/libs/chart-apex.scss';
</style>
