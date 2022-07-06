<template>
  <div class="container-fluid px-0">
    <div class="row boxMainContent">
      <div class="col-12">
        <span class="titleSection">آمار بازدید پروفایل </span>
      </div>
      <div class="col-12 fw-bold w-auto">
        کلیه بازدیدها
      </div>
      <div class="bar-chart my-3">
        <client-only>
          <line-chart :data="finalDay" :options="options"/>
        </client-only>
      </div>
      <div class="col-12 fw-bold w-auto">
        بازدیدهای روزانه
      </div>
      <div class="bar-chart my-3">
        <client-only>
          <line-chart :data="finalDay" :options="options"/>
        </client-only>
      </div>
      <div class="col-12 fw-bold w-auto">
        بازدیدهای هفتگی
      </div>
      <div class="bar-chart my-3">
        <client-only>
          <line-chart :data="chartdataWeek" :options="options"/>
        </client-only>
      </div>
      <div class="col-12 fw-bold w-auto">
        بازدیدهای ماهانه
      </div>
      <div class="bar-chart my-3">
        <client-only>
          <line-chart ref="myMountRef" :data="finalMounth" :options="options"/>
        </client-only>
      </div>
      <div class="col-12 fw-bold w-auto">
        بازدیدهای سالانه
      </div>
      <div class="bar-chart my-3">
        <client-only>
          <line-chart :data="finalYear" :options="options"/>
        </client-only>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Statistics",
  layout: 'PoshtebamPlusLayout',
  data() {
    return {
      chartdataDay: {
        labels: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24"],
        datasets: [
          {
            data: [86, 114, 106, 106, 107, 111, 133, 221, 783, 2478],
            borderColor: "#3E95CD",
            fill: false
          },
        ]
      },
      chartdataWeek: {
        labels: ["1", "2", "3", "4", "5", "6", "7"],
        datasets: [
          {
            data: [86, 114, 106, 106, 107, 111, 133, 221, 783, 2478],
            borderColor: "#3E95CD",
            fill: false
          },
          {
            data: [282, 350, 411, 502, 635, 809, 947, 1402, 3700, 5267],
            borderColor: "#8E5EA2",
            fill: false
          },
          {
            data: [168, 170, 178, 190, 203, 276, 408, 547, 675, 734],
            borderColor: "#3CBA9F",
            fill: false
          },
          {
            data: [40, 20, 10, 16, 24, 38, 74, 167, 508, 784],
            borderColor: "#E8C3B9",
            fill: false
          },
          {
            data: [6, 3, 2, 2, 7, 26, 82, 172, 312, 433],
            borderColor: "#C45850",
            fill: false
          }
        ]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        legend: {
          display: false
        },
        scales: {
          xAxes: [{
            gridLines: {
              display: true
            }
          }],
          yAxes: [{
            gridLines: {
              display: true
            },
            ticks: {
              min: 0,
            }
          }]
        },
        tooltips: {
          bodySpacing: 5,
          bodyFontSize: 16, bodyFontFamily: "'Vazir'", titleFontFamily: "'Vazir'"
        }
      },
      logView: [],
      year: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
      mounth: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
      day: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
      myMounts: {},
    }
  },
  methods: {
    getUserLogProfile() {
      this.$axios.post(`Log/GetByUserId`, null, {
        params: {
          userId: this.$auth.user.userId
        },
       
      }).then((response) => {
        if (response.status === 200) {
          this.logView = response.data
          this.logView.sort(function (a, b) {
            return new Date(a.createDate) - new Date(b.createDate)
          })
          this.filterDay(this.logView);
          this.filterWeek(this.logView);
          this.filterMounth(this.logView);
          this.filterYear(this.logView);
        }
      }).catch((error) => {
        console.log(error)
      })
    },
    filterDay(data) {
      const tmpDay = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
      const timeData = data.map(i => Object.values(i)[6].substring(0, 10).slice(8));
      for (let i = 0; i < timeData.length; i++) {
        if (timeData[i] === '01') {
          tmpDay[0]++;
        } else if (timeData[i] === '02') {
          tmpDay[1]++;
        } else if (timeData[i] === '03') {
          tmpDay[2]++;
        } else if (timeData[i] === '04') {
          tmpDay[3]++;
        } else if (timeData[i] === '05') {
          tmpDay[4]++;
        } else if (timeData[i] === '06') {
          tmpDay[5]++;
        } else if (timeData[i] === '07') {
          tmpDay[6]++;
        } else if (timeData[i] === '08') {
          tmpDay[7]++;
        } else if (timeData[i] === '09') {
          tmpDay[8]++;
        } else if (timeData[i] === '10') {
          tmpDay[9]++;
        } else if (timeData[i] === '11') {
          tmpDay[10]++;
        } else if (timeData[i] === '12') {
          tmpDay[11]++;
        } else if (timeData[i] === '13') {
          tmpDay[12]++;
        } else if (timeData[i] === '14') {
          tmpDay[13]++;
        } else if (timeData[i] === '15') {
          tmpDay[14]++;
        } else if (timeData[i] === '16') {
          tmpDay[15]++;
        } else if (timeData[i] === '17') {
          tmpDay[16]++;
        } else if (timeData[i] === '18') {
          tmpDay[17]++;
        } else if (timeData[i] === '19') {
          tmpDay[18]++;
        } else if (timeData[i] === '20') {
          tmpDay[19]++;
        } else if (timeData[i] === '21') {
          tmpDay[20]++;
        } else if (timeData[i] === '22') {
          tmpDay[21]++;
        } else if (timeData[i] === '23') {
          tmpDay[22]++;
        } else if (timeData[i] === '24') {
          tmpDay[23]++;
        } else if (timeData[i] === '25') {
          tmpDay[24]++;
        } else if (timeData[i] === '26') {
          tmpDay[25]++;
        } else if (timeData[i] === '27') {
          tmpDay[26]++;
        } else if (timeData[i] === '28') {
          tmpDay[27]++;
        } else if (timeData[i] === '29') {
          tmpDay[28]++;
        } else if (timeData[i] === '30') {
          tmpDay[29]++;
        } else if (timeData[i] === '31') {
          tmpDay[30]++;
        }
      }
      this.day = tmpDay;

    },
    filterWeek(data) {
    },
    filterMounth(data) {
      var tmpMounth = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
      const timeData = data.map(i => Object.values(i)[6].substring(0, 10).substring(0, 7).slice(5));
      for (let i = 0; i < timeData.length; i++) {
        if (timeData[i] === '01') {
          tmpMounth[0]++;
        } else if (timeData[i] === '02') {
          tmpMounth[1]++;
        } else if (timeData[i] === '03') {
          tmpMounth[2]++;
        } else if (timeData[i] === '04') {
          tmpMounth[3]++;
        } else if (timeData[i] === '05') {
          tmpMounth[4]++;
        } else if (timeData[i] === '06') {
          tmpMounth[5]++;
        } else if (timeData[i] === '07') {
          tmpMounth[6]++;
        } else if (timeData[i] === '08') {
          tmpMounth[7]++;
        } else if (timeData[i] === '09') {
          tmpMounth[8]++;
        } else if (timeData[i] === '10') {
          tmpMounth[9]++;
        } else if (timeData[i] === '11') {
          tmpMounth[10]++;
        } else if (timeData[i] === '12') {
          tmpMounth[11]++;
        }
      }
      this.mounth = tmpMounth
    },
    filterYear(data) {
      const tmpYear = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
      const timeData = data.map(i => Object.values(i)[6].substring(0, 4));
      for (let i = 0; i < timeData.length; i++) {
        if (timeData[i] === '2020') {
          tmpYear[0]++;
        } else if (timeData[i] === '2021') {
          tmpYear[1]++;
        } else if (timeData[i] === '2022') {
          tmpYear[2]++;
        } else if (timeData[i] === '2023') {
          tmpYear[3]++;
        } else if (timeData[i] === '2024') {
          tmpYear[4]++;
        } else if (timeData[i] === '2025') {
          tmpYear[5]++;
        } else if (timeData[i] === '2026') {
          tmpYear[6]++;
        } else if (timeData[i] === '2027') {
          tmpYear[7]++;
        } else if (timeData[i] === '2028') {
          tmpYear[8]++;
        } else if (timeData[i] === '2029') {
          tmpYear[9]++;
        } else if (timeData[i] === '2030') {
          tmpYear[10]++;
        } else if (timeData[i] === '2031') {
          tmpYear[11]++;
        } else {
          tmpYear[12]++;
        }
      }
      this.year = tmpYear;
    },
  },
  mounted() {
    this.$axios.post(`Log/GetByUserId`, null, {
      params: {
        userId: this.$auth.user.userId
      },
      
    }).then((response) => {
      if (response.status === 200) {

        this.logView = response.data
        this.logView.sort(function (a, b) {
          return new Date(a.createDate) - new Date(b.createDate)
        })
        var data = this.logView;

        this.filterMounth(data);
        this.filterYear(data);
        this.filterDay(data);

      }
    }).catch((error) => {
      console.log(error)
    })
    this.$nextTick(() => {
      this.$nuxt.$loading.finish();
    })
  },
  computed: {
    finalMounth() {
      return {
        labels: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"],
        datasets: [
          {
            data: this.mounth,
            borderColor: "#3E95CD",
            fill: false
          },
        ]
      }
    },
    finalYear() {
      return {
        labels: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"],
        datasets: [
          {
            data: this.year,
            borderColor: "#3E95CD",
            fill: false
          },
        ]
      }
    },
    finalDay() {
      return {
        labels: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"],
        datasets: [
          {
            data: this.day,
            borderColor: "#3E95CD",
            fill: false
          },
        ]
      }
    },
  }
}
</script>
<style scoped>
.boxMainContent {
  background: #FDFDFD none repeat scroll 0 0;
  border: 1px solid #EDE9E9;
  border-radius: 3px;
  display: inline-block;
  width: 100%;
  margin-bottom: 20px;
  padding: 25px;
}

.titleSection {
  border-bottom: 2px solid #088DCD;
  background: rgba(0, 0, 0, 0) none repeat scroll 0 0;
  color: #424242;
  display: inline-block;
  font-size: 15px;
  margin-bottom: 30px;
  padding: 20px 0 10px;
  position: relative;
  text-transform: uppercase;
  width: max-content;
}
</style>
