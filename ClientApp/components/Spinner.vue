<template>
  <div class="lds-ring" >
    <div></div>
    <div></div>
    <div></div>
    <div></div>
  </div>
</template>

<script>
export default {
  name: "Spinner"
}
</script>

<style scoped>
.lds-ring {
  position: relative;
  width: 80px;
  height: 80px;
  margin-right: auto;
  margin-left: auto;
  display: flex;
  justify-content: center;
  align-items: center;
}
.lds-ring div {
  box-sizing: border-box;
  display: block;
  position: absolute;
  width: 40px;
  height: 40px;
  margin: 8px;
  border: 5px solid #fff;
  border-radius: 50%;
  animation: lds-ring-data-v-cf6853e4 1.2s cubic-bezier(0.5, 0, 0.5, 1) infinite;
  border-color: #2563eb transparent transparent transparent;
}
.lds-ring div:nth-child(1) {
  animation-delay: -0.45s;
}
.lds-ring div:nth-child(2) {
  animation-delay: -0.3s;
}
.lds-ring div:nth-child(3) {
  animation-delay: -0.15s;
}
@keyframes lds-ring {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}

</style>
