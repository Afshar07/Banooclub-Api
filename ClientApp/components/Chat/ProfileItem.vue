<template>
  <div class="d-flex align-items-center profile_item p-2">
    <img style="width: 50px; height: 50px;" class="rounded-circle" src="~/assets/images/defaultUser.png"/>
    <div class="d-flex justify-content-between w-100 px-3">
      <div class="d-flex flex-column">
        <div style="color: #333;font-size: 15px;font-weight: 600;">shahbaz sh</div>
        <div style="color: #888;font-size: 14px;">سلام، سایت تست....</div>
      </div>
      <small style="color: #888;">9:35</small>
    </div>


  </div>
</template>

<script>
export default {
  name: "ProfileItem"
}
</script>

<style scoped>
.profile_item:hover{
  background-color: #f3f3f3 !important;
  transition: .2s ease !important;
  border-radius: 10px;
}
</style>
