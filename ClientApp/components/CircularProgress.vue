<template>
  <div class="tw-radial-progress" :class="{'TextRed':RadialProgress<=30,'TextYellow':RadialProgress<=60,'TextGreen':RadialProgress<=100}" :style="`--value:${RadialProgress}`" style=" --size:4rem; --thickness: 2px;">{{ RadialProgress }}%</div>
</template>

<script>
export default {
  props:['RadialProgress'],
  name: "CircularProgress"
}
</script>

<style scoped>
.tw-radial-progress::after {
  inset: calc(70% - var(--thickness) / 2) !important;
  /* inset: 0 !important; */
}
.TextRed{
  @apply tw-text-red-500 !important;
}

.TextGreen{
  @apply tw-text-green-500 !important;
}

.TextYellow{
  @apply tw-text-yellow-500 !important;
}
.tw-radial-progress::after{
  display: none!important;
}
</style>
