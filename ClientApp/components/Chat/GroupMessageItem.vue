<template>
  <div v-if="Message.CreatorUserId===this.$auth.user.userInfo.userId"
       class="d-flex align-items-center gap-2 justify-content-start mb-4 px-2">
    <div class="my_message_item p-3 py-1" style="margin-left: 20px;width: 300px;">
      <p style="overflow-wrap: anywhere">
        {{ Message.MessageBody }}
      </p>
      <div class="d-flex align-items-center justify-content-between">
        <div class="d-flex align-items-center gap-1">
          <i class="far fa-clock tw-text-[10px]"></i>
          <small
            class="text-white mt-1 tw-text-[10px] ">{{ new Date(Message.CreateDate).toLocaleTimeString('fa-IR') }}</small>
        </div>
<!--        <div class="d-flex align-items-center justify-content-end">-->
<!--          <small v-if="!Message.IsDelivered" class="tw-text-[8px]"> <i class="fas fa-check"></i></small>-->
<!--          <small v-if="Message.IsDelivered" class="tw-text-[8px]"> <i class="fas fa-check-double " :class="{'ReadMessage':Message.IsRead}"></i></small>-->
<!--        </div>-->
      </div>
    </div>
  </div>
  <div v-else
       class="d-flex align-items-center gap-2 justify-content-end mb-4 px-2">
    <div class="your_message_item p-3 py-1" style="margin-left: 20px;width: 300px;">
      <p style="overflow-wrap: anywhere">
        {{ Message.MessageBody }}
      </p>
      <div class="d-flex align-items-center justify-content-between">
        <div class="d-flex align-items-center gap-1">
          <i class="far fa-clock tw-text-[10px]"></i>
          <small
            class="text-black mt-1 tw-text-[10px] ">{{ new Date(Message.CreateDate).toLocaleTimeString('fa-IR') }}</small>
        </div>
        <!--        <div class="d-flex align-items-center justify-content-end">-->
        <!--          <small v-if="!Message.IsDelivered" class="tw-text-[8px]"> <i class="fas fa-check"></i></small>-->
        <!--          <small v-if="Message.IsDelivered" class="tw-text-[8px]"> <i class="fas fa-check-double " :class="{'YourReadMessage':Message.IsRead}"></i></small>-->
        <!--        </div>-->
      </div>
    </div>
  </div>


</template>

<script>
export default {
  props: ['Message'],
  name: "ContactMessageItem"
}
</script>

<style scoped>
.my_message_item {
  background-color: #2a41e8;
  border-radius: 6px;
  color: white;
}

.ReadMessage {
  color: aquamarine !important;
}

.YourReadMessage{
  color:#2a41e8!important;
}

.your_message_item {
  background-color: #f3f3f3;
  border-radius: 6px;
  color: #333;
}

/*.my_message_item::after {*/
/*  content: "a";*/
/*  position: absolute;*/
/*  right: 0;*/
/*  top: 50%;*/
/*  width: 0;*/
/*  height: 0;*/
/*  border: 19px solid red;*/
/*  border-right: 0;*/
/*  border-bottom: 0;*/
/*  margin-top: -9.5px;*/
/*  margin-right: -19px;*/
/*}*/

/*.your-message::after {*/
/*  content: "";*/
/*  position: absolute;*/
/*  left: 0;*/
/*  top: 50%;*/
/*  width: 0;*/
/*  height: 0;*/
/*  border: 19px solid transparent;*/

/*  border-left: 0;*/
/*  border-bottom: 0;*/
/*  margin-top: -9.5px;*/
/*  margin-left: -19px;*/
/*}*/
</style>
