<!-- CustomInput.vue -->

<template>
  <input
    type="text"
    class="with-border"
    :value="DataString"
    @input="$emit('input',$event.target.value)"

  >
</template>

<script>
export default {
  props: {
    DataString:String

  },
  model: {
    prop: 'DataString',
    event: 'input'
  },
}
</script>

