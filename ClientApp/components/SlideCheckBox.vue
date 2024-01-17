<template>

  <label class="switch " :for="`checkbox${UniqueId}`">
    <input type="checkbox"   v-bind:checked="checked" v-on:change="$emit('change', $event.target.checked)" :id="`checkbox${UniqueId}`" />
    <div class="slider round"></div>
  </label>

</template>

<script>
export default {
  name: "SlideCheckBox",
  props: {
    checked: Boolean,
    UniqueId:Number,
  },
  model: {
    prop: 'checked',
    event: 'change'
  },
}
</script>

<style scoped>
.switch {
  display: inline-block;
  height: 20px;
  position: relative;
  width: 40px;
  margin:0px!important;
}

.switch input {
  display:none;
}

.slider {
  background-color: #ccc;
  bottom: 0;
  cursor: pointer;
  left: 0;
  position: absolute;
  right: 0;
  top: 0;
  transition: .4s;
}

.slider:before {
  background-color: #fff;
  bottom: 4px;
  content: "";
  height: 12px;
  left: 1px;
  position: absolute;
  transition: .4s;
  width: 12px;
}

input:checked + .slider {
  background-color:#fc5399;


}

input:checked + .slider:before {
  transform: translateX(26px);
}

.slider.round {
  border-radius: 34px;
}

.slider.round:before {
  border-radius: 50%;
}
/* general styling */
html, body {
  height: 100%;
  margin: 0;
}



</style>
