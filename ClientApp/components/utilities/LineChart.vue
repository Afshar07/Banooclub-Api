<template>
  <div class="bg-white rounded BoxShadow p-3">
    <div class="bg-white rounded BoxShadow p-3">
      <canvas ref="LineChart" class="w-100 " style="height: 200px"></canvas>
    </div>
  </div>

</template>

<script>


import Chart from "chart.js";

export default {
  name: 'LineChart',
  mounted() {
    if (this.ChartData) {
      let tmpLabels = []
      let tmpData = []
      let tmpDataset = {
        labels: [],
        datasets: [
          {
            label: 'رشد فالوور ها',
            backgroundColor: '#1470ce',
            data: []
          }
        ]
      }
      // this.ChartData.forEach((item) => {
      //   tmpLabels.push(item.persianDate)
      //   tmpData.push(item.sumPrices)
      // })
      // tmpDataset.labels = tmpLabels
      // tmpDataset.datasets[0].data = tmpData

      this.chartData = tmpDataset
    }
    this.BarChart = new Chart(this.$refs.LineChart,{
      type:'line',
      data:{
        labels: this.chartData.labels,
        datasets: this.chartData.datasets
      },
      options: this.chartOptions
    })

  },
  props: {

    ChartData:{
      type:Array,
    },
  },
  data() {
    return {
      LineChart:null,
      chartData: {
        labels: [],
        datasets: [
          {

          }
        ]
      },
      chartOptions: {
        responsive: true,
        maintainAspectRatio: false
      }
    }
  }
}
</script>
