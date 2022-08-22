<template>
<div class="tw-flex tw-items-center tw-gap-1">
  <small :class="{'tw-text-red-300':Value===LimitLength}">{{Value}}</small>
  <small>/</small>
  <small>{{LimitLength}}</small>
</div>
</template>

<script>
export default {
  props:['LimitLength','Value'],
  name: "InputLimitation"
}
</script>

<style scoped>

</style>
