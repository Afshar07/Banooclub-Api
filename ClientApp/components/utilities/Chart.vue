<template>
  <div class="bg-white rounded BoxShadow p-3" style="height: 300px!important;">
      <canvas ref="LineChart" class="w-100 " style="height: 300px!important;"></canvas>

  </div>
</template>

<script>


import Chart from "chart.js";

export default  {
  name: 'Chart',
  async fetch() {
    try {
      const service_view = await this.$repositories.GetwithView.GetwithView(
        {
          servicePackId:this.$route.params.UpgradeProduct
        })

      this.ChartData = service_view.data.servicePack.weekViews.data
      console.log(this.ChartData)

    }catch (e) {
      console.log(e)
    }
    if (this.ChartData) {

      let tmpLabels = []
      let tmpData = []
      let tmpDataset = {
        labels: [],
        datasets: [
          {
            label: 'آمار بازدید',
            backgroundColor: '#8e54e9',
            data: []
          }
        ]
      }
      this.ChartData.forEach((item) => {
        tmpLabels.push( new Date(item.createDate).toLocaleDateString('fa-IR') )
        tmpData.push(item.count)
      })
      tmpDataset.labels = tmpLabels
      tmpDataset.datasets[0].data = tmpData

      this.chartData = tmpDataset

    }
    this.BarChart = new Chart(this.$refs.LineChart, {
      type: 'line',
      data: {
        labels: this.chartData.labels,
        datasets: this.chartData.datasets
      },
      options: this.chartOptions
    })

  },

  data() {
    return {
      BarChart: null,
      ChartData:null,
      chartOptions: {
        responsive: true,
        maintainAspectRatio: false
      }
    }
  }
}
</script>
