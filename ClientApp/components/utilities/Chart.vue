<template>
  <div class="main-section w-full">
    <div class="w-full my-3 px-4">
      <canvas ref="chart" class="tw-w-full tw-h-full" ></canvas>
    </div>
  </div>
</template>

<script>
// import ChartDataLabels from "chartjs-plugin-datalabels";
import Chart from "chart.js"
export default {
  name: "Chart",
  async fetch(){
    try {
      const service_view = await this.$repositories.GetwithView.GetwithView(
        {
          servicePackId:this.$route.params.UpgradeProduct
        }
      )
      this.ServiceViews = service_view.data
      this.ServiceViews.weekViews.data.forEach(el=>{
        this.labels.push(new Date(el.createDate).toLocaleDateString('fa-IR'))
        this.dataSets.push(el.count)
      })
    }
    catch (error){
      console.log(error)
    }
  },

  mounted() {

    this.chart = new Chart(this.$refs.chart, {
      type: "bar",
      data: {
        labels: this.labels,
        datasets: [
          {
            label: "آمار بازدید",
            data: this.dataSets,
            backgroundColor: "#2563eb",
            font: {
              size: 20,
              family: "IranSans",
            },
          },
        ],
      },
      // plugins: [ChartDataLabels],
      options: {
        responsive: true,
        plugins: {
          decimation: true,
          legend: {
            font: {
              size: 14,
              family: "IranSans",
            },
            position: "top",
          },
          title: {
            display: true,
            text: "آمار بازدید",
            font: {
              size: 20,
              family: "IranSans",
            },
          },
        },
        borderWidth: 1,
      },
    });
  },
  data() {
    return {
      ServiceViews:[],
      dataSets: [],
      chart: null,
      labels:[],

    };
  },
  props:{
    serviceViews:{
      type: Array,
      required:true
    }
  }
}
</script>

<style scoped>

</style>
