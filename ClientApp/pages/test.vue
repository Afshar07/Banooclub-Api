<template>
  <div class="col-md-12 my-3 bg-white rounded shadow">
    <div class="row p-3">
      <div class="col-md-12 border-bottom pb-2 mb-2">
        <strong>زمان های ثبت شده</strong>
      </div>
      <div class="col-md-12 d-flex flex-column align-items-start justify-content-start">
        <small class="mb-2">انتخاب روز</small>
        <v-select
          class="col-md-3 p-0 "
          style="z-index: 9999;"
          v-model="selectedWeekDays"
          label="name"
          multiple
          :options="weekDays"
          :reduce="name => name.value"
        />
      </div>
      <div class="col-md-12 my-3 d-flex  gap-3 align-items-center ">
        <div class="d-flex flex-column gap-2">
          <small>تاریخ شروع</small>
          <date-picker
            format="YYYY-MM-DD"
            display-format="jYYYY-jMM-jDD"
            :min="new Date(Date.now())"
            :color="`#fc5399`"
            v-model="dates.startDate"
            type="date"
          />
        </div>
        <div class="d-flex flex-column gap-2">
          <small>تاریخ پایان</small>
          <date-picker
            format="YYYY-MM-DD"
            display-format="jYYYY-jMM-jDD"
            v-model="dates.endDate"
            :color="`#fc5399`"
            type="date"
          />
        </div>
      </div>


      <div style="height: 400px;overflow-y: scroll" class="d-flex position-relative align-items-start gap-2">
        <div v-for="(weekDay,weekDayIndex) in 7"
             style="min-width: 5rem"
             class="d-flex  flex-column align-items-center justify-content-start">
              <span class="w-100 p-2 sticky-top top-0 rounded bg-pink text-white text-center shadow">{{
                  getWeekDayNumber(weekDayIndex)
                }}</span>
          <template v-for="(time,timeIndex) in generatedTimestamps">

            <button type="button" :disabled="!isRenderingTime(weekDayIndex,time)"
                    :class="getSelectedClass(weekDayIndex,time)"
                    @click="isSelected(weekDayIndex,time)"

                    class="w-100 p-2 rounded bg-purple mt-2 disabled:tw-bg-gray-300 disabled:tw-cursor-not-allowed cursor-pointer cursor-pointer text-center text-white shadow">
              {{
                time.value
              }}
            </button>
          </template>
        </div>
      </div>
    </div>

  </div>

</template>

<script>
export default {
  name: "test",
  data(){
    return{
      generatedTimestamps: [],
      selectedStamps: [],
      selectedWeekDays: [],
      weekDays: [
        {
          name: 'شنبه',
          value: 1
        },
        {
          name: 'یک شنبه',
          value: 2
        },
        {
          name: 'دوشنبه',
          value: 3
        },
        {
          name: 'سه شنبه',
          value: 4
        },
        {
          name: 'چهارشنبه',
          value: 5
        },
        {
          name: 'پنجشنبه',
          value: 6
        },
        {
          name: 'جمعه',
          value: 7
        },
      ],
      nowDate: new Date(Date.now()),
      dayInMili: 86400000,
      sessionDuration: 30,
      defaultStartHour: 8,
      defaultEndHour: 23
    }
  },
  methods:{
    generateTimeStamps() {
      let todayDate = new Date(this.nowDate)

      todayDate = new Date(todayDate.setHours(this.defaultStartHour, 0, 0))
      let tempDate = {
        value: '',
        minute: '',
        hour: '',
        timestamp: 0
      }
      for (let i = 0; todayDate.getHours() < this.defaultEndHour; i++) {
        tempDate.value = new Date(todayDate).toLocaleTimeString('en-US', {
          hour12: false,
          hour: "2-digit",
          minute: '2-digit',
          second:'2-digit'
        })
        tempDate.minute = new Date(todayDate).getMinutes()
        tempDate.hour = new Date(todayDate).getHours()
        tempDate.timestamp = new Date(todayDate).getTime()
        this.generatedTimestamps.push(tempDate)
        tempDate = {
          value: '',
          minute: '',
          hour: ''
        }
        todayDate = new Date(todayDate.setMinutes(todayDate.getMinutes() + this.sessionDuration))
      }

    },
    getSelectedClass(weekDay, timeObj) {
      let calculatedDate = new Date(this.calculateDate(weekDay, timeObj))
      calculatedDate = new Date(calculatedDate).getTime()
      if (this.selectedStamps.findIndex(e => e.timestamp === calculatedDate) > -1) {
        return 'bg-success'
      } else {
        return
      }

    },
    isSelected(weekDay, timeObj) {
      let calculatedDate = new Date(this.calculateDate(weekDay, timeObj))
      let tempDate = {
        value: '',
        minute: '',
        hour: '',
        timestamp: 0
      }
      tempDate.value = new Date(calculatedDate).toLocaleTimeString('en-US', {
        hour12: false,
        hour: "2-digit",
        minute: '2-digit'
      })
      tempDate.minute = new Date(calculatedDate).getMinutes()
      tempDate.hour = new Date(calculatedDate).getHours()
      tempDate.timestamp = new Date(calculatedDate).getTime()
      let selectedIdx = this.selectedStamps.findIndex(e => e.timestamp === tempDate.timestamp)
      if (selectedIdx > -1) {
        this.selectedStamps.splice(selectedIdx, 1)
      } else {
        this.selectedStamps.push(tempDate)
      }
      tempDate = {
        value: '',
        minute: '',
        hour: '',
        timestamp: 0
      }
      // let idx = this.selectedStamps.findIndex(e=> e.timestamp === tempDate.timestamp)
      // if(idx>-1){
      //   this.$set(this.selectedStamps,idx,tempDate)
      // }else{
      // }


    },
    calculateDate(weekDay, timeObj) {
      let calculateDate = new Date(this.nowDate)
      calculateDate = new Date(calculateDate.setDate(calculateDate.getDate() + weekDay))
      calculateDate = new Date(calculateDate.setHours(timeObj.hour, timeObj.minute, 0))
      return calculateDate
    },
    isRenderingTime(weekDay, timeObj) {
      if (weekDay === 0) {
        let calculateDate = new Date(this.nowDate)
        calculateDate = new Date(calculateDate.setHours(timeObj.hour, timeObj.minute, 0))
        if (calculateDate.getHours() >= this.nowDate.getHours() + 2) {
          return true
        }
        return false
      } else {
        return true
      }
    },
    getWeekDayNumber(idx) {
      let calculatedDate = new Date(this.nowDate)
      calculatedDate = calculatedDate.setDate(calculatedDate.getDate() + idx)
      return new Date(calculatedDate).toLocaleDateString('fa-IR', {
        weekday: 'long'
      })
    }
  }
}
</script>

<style scoped>

</style>
