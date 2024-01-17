<template>
  <svg :fill="fill" xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6" viewBox="0 0 20 20" >
    <path fill-rule="evenodd" d="M18 5v8a2 2 0 01-2 2h-5l-5 4v-4H4a2 2 0 01-2-2V5a2 2 0 012-2h12a2 2 0 012 2zM7 8H5v2h2V8zm2 0h2v2H9V8zm6 0h-2v2h2V8z" clip-rule="evenodd" />
  </svg>
</template>

<script>
export default {
  name: "MessageIcon",
  props:{
    fill:{
      type: String,
      required: true
    }
  }
}
</script>

<style scoped>

</style>
