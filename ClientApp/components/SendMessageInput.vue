<template>
  <div class="col-md-12 p-0  bg-white  SendMessageContainer">
    <div class="input-group  " style="direction: ltr!important;">
      <span class="input-group-text " @click="sendMessage" id="basic-addon1">ارسال</span>
      <textarea v-model="MessageBody" class="form-control tw-z-10 " placeholder="پیام خود را وارد کنید..."
                style="resize: none!important;" rows="1"></textarea>
    </div>
  </div>
</template>

<script>
export default {
  name: "SendMessageInput",
  data(){
    return{
      MessageBody:'',
    }
  },
  methods:{
    sendMessage(){
      this.$emit('GetMessage',this.MessageBody)
      this.MessageBody = ''
    }
  }
}
</script>

<style scoped>
.SendMessageContainer{
  position: sticky;
  bottom: 0;
  box-shadow: 0 5px 20px rgb(0 0 0 / 12%);
  width: 100%;
  z-index: 9999999999999999999999999999!important;
}
.input-group-text {
  background-color: #ffa3b9;
  color: white;
  cursor: pointer;
}
</style>
