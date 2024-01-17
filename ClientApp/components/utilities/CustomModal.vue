<template>
  <div class="custom-modal-overlay" @click="closeCustomModal">
    <div class="custom-modal" @click.stop style="overflow-y: scroll">
<!--      style="max-height: 710px"-->
      <div class="custom-modal-header">
        <slot name="title"></slot>
        <button @click="closeCustomModal" class="close_button p-1 d-flex justify-content-between ">
          <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6 tw-text-gray-700 "
               fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
            <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12" />
          </svg>
        </button>
      </div>
      <div class="custom-modal-content">
        <slot name="content"></slot>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "CustomModal",
  methods:{
    closeCustomModal(){
      this.$emit('close-modal')
    }
  }
}
</script>

<style scoped>
@media only screen and (max-width: 425px) {
  .custom-modal{
    min-width: 100%;
  }
}
@media only screen and (min-width: 426px) and (max-width: 768px) {
  .custom-modal{
    width: 60%;
  }
}
@media only screen and (min-width: 769px) {
  .custom-modal{
    max-width: 562px !important;
    min-width: 562px !important;
  }
}

.custom-modal-content{
  padding: 20px;
}
.custom-modal-overlay {
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  display: flex;
  justify-content: center;
  background-color: #e5e7eb80;
  z-index: 9999;
  padding-top: 50px;
  padding-bottom: 50px;
  padding-right: 5px;
  padding-left: 5px;
}
.custom-modal-header{
  padding: 15px;
  display: flex;
  justify-content: space-between;
  border-bottom: 1px solid #80808091;
  align-items: center;
}
.custom-modal {
  border: 1px solid rgb(0 0 0 / 20%);
  background-color: white;
  border-radius: 0.5rem;

}

.close_button {
  background-color: #f3f4f6;
  border-radius: 50%;
}

</style>
