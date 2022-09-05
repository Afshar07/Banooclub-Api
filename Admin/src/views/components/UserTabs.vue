<template>
  <b-tabs content-class="mt-2" justified>
    <b-tab @click="EmitComponent('FriendsTab')"  title="دوستان">
    </b-tab>
    <b-tab @click="EmitComponent('PostsTab')" title="پست ها">
    </b-tab>
    <b-tab @click="EmitComponent('TicketsTab')" title="تیکت ها">
    </b-tab>
    <b-tab @click="EmitComponent('AdsTab')" title="آگهی ها">
    </b-tab>
    <b-tab @click="EmitComponent('WishListsTab')"  title="علاقه مندی ها">
    </b-tab>
    <b-tab @click="EmitComponent('ServicesTab')" title="خدمات">
    </b-tab>
    <b-tab @click="EmitComponent('PaymentsTab')" title="پرداخت ها">
    </b-tab>
    <b-tab @click="EmitComponent('OrdersTab')" title="سفارشات">
    </b-tab>
    <b-tab @click="EmitComponent('ForumsTab')" title="تالار ها">
    </b-tab>
    <b-tab @click="EmitComponent('RoomateTab')" active title="آگهی هم خانه">
    </b-tab>
  </b-tabs>
</template>

<script>
import { BTabs, BTab, BCardText } from 'bootstrap-vue'

export default {
  methods:{
    EmitComponent(slug){
      this.$emit('ChangeComponent',slug)
    }
  },
  components: {
    BCardText,
    BTabs,
    BTab,
  },
}
</script>
