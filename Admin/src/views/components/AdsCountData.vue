
<template>
  <app-timeline>
    <app-timeline-item
        title="آگهی های ثبت شده امروز"
        :time="AdsData.numberOfAdsRegisteredToday"
        variant="success"
    />

    <app-timeline-item
        title="آگهی های ثبت شده هفته گذشته"
        :time="AdsData.numberOfAdsRegisteredLastWeek"
        variant="info"
    />

    <app-timeline-item
        title="آگهی های ثبت شده ماه گذشته"
        :time="AdsData.numberOfAdsRegisteredLastMonth"
        variant="warning"
    />
  </app-timeline>
</template>

<script>
import AppTimeline from '@core/components/app-timeline/AppTimeline.vue'
import AppTimelineItem from '@core/components/app-timeline/AppTimelineItem.vue'

export default {
  props:['AdsData'],
  components: {
    AppTimeline,
    AppTimelineItem,
  },
}
</script>
