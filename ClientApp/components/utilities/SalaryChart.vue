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
            backgroundColor: '#de1c61',
            data: []
          },
          {
            label: 'درآمد ',
            backgroundColor: '#03a9f4',
            data: []
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
        tmpData.push(item.incomeAmount)
      })
      tmpDataset.datasets[0].data = tmpData
      tmpData = []
      this.ChartData.forEach((item) => {
        tmpData.push(item.outcomeAmount)
      })
      tmpDataset.datasets[1].data = tmpData
      tmpData = []
      this.chartData = tmpDataset
      console.log(this.chartData)
      this.BarChart = new Chart(this.$refs.BarChart, {
        type: 'bar',
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

        responsive: true,
        maintainAspectRatio: false
      }
    }
  }
}
</script>
