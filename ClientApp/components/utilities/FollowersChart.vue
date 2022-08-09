<template>
  <div class="bg-white rounded BoxShadow p-3" style="height: 500px!important;">
    <canvas ref="LineChart" class="w-100 " style="height: 500px!important;"></canvas>

  </div>
</template>

<script>


import Chart from "chart.js";

export default {
  name: 'Chart',
  props:['ChartData'],
  mounted() {

    if (this.ChartData) {


      let tmpLabels = []
      let tmpData = []
      let tmpDataset = {
        labels: [],
        datasets: [
          {
            label: 'جدول رشد دنبال کنندگان',
            borderColor: '#de1c61',
            backgroundColor:'transparent',

            data: []
          }
        ]
      }
      this.ChartData.forEach((item) => {
        tmpLabels.push(item.persianDate)
        tmpData.push(item.followersCount)
      })
      tmpDataset.labels = tmpLabels
      tmpDataset.datasets[0].data = tmpData

      this.chartData = tmpDataset
      console.log(this.chartData)
      this.BarChart = new Chart(this.$refs.LineChart, {
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
      chartData:'',
      BarChart: null,
      chartOptions: {

        responsive: true,
        maintainAspectRatio: false
      }
    }
  }
}
</script>
