<template>
  <div class="bg-white rounded BoxShadow p-3" style="height: 500px!important;">
    <canvas ref="BarChart" class="w-100 " style="height: 500px!important;"></canvas>

  </div>
</template>

<script>


import Chart from "chart.js";

export default {
  name: 'Chart',
  props: ['ChartData'],
  mounted() {

    if (this.ChartData) {


      let tmpLabels = []
      let tmpData = []
      let tmpDataset = {
        labels: [],
        datasets: [
          {
            label: 'پرداخت ',
            borderColor: '#2aebc9',
            backgroundColor:'transparent',
            data: [],

          },
          {
            label: 'درآمد ',
            borderColor: '#fc5399',
            backgroundColor:'transparent',
            data: [],

          }
        ]
      }

      // push Labels
      this.ChartData.forEach((item) => {
        tmpLabels.push(item.persianDate)
      })
      tmpDataset.labels = tmpLabels
      // push Data
      this.ChartData.forEach((item) => {
        tmpData.push(item.outcomeAmount)
      })
      tmpDataset.datasets[0].data = tmpData
      tmpData = []
      this.ChartData.forEach((item) => {
        tmpData.push(item.incomeAmount)
      })
      tmpDataset.datasets[1].data = tmpData
      tmpData = []
      this.chartData = tmpDataset
      console.log(this.chartOptions)

      this.BarChart = new Chart(this.$refs.BarChart, {
        type: 'line',
        data: {
          labels: this.chartData.labels,
          datasets: this.chartData.datasets
        },
        options: this.chartOptions
      })


    }

  },

  data() {
    return {
      chartData: '',
      BarChart: null,
      chartOptions: {
        plugins: {
          legend: {
            display: false,
            labels: {
              fontColor: 'red'
            }
          },
        },
        responsive: true,
        maintainAspectRatio: false
      }
    }
  }
}
</script>
<style>

</style>
