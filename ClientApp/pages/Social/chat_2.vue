<template>
  <div class="row tw-overflow-y-hidden" style="height: 92vh;">
    <div class="col-lg-3" style="border-left: 1px solid rgb(227 227 227)">
      <div>
        <div class="d-flex justify-content-between" style="border-bottom: 1px solid rgb(227 227 227)">
          <h2 class="text-right py-3" style="font-family: iranSans !important; font-weight: 600;">مکالمات من</h2>
          <button type="button" class="btn">
            <EditIcon style="width: 20px; height: 20px;"/>
          </button>
        </div>
        <div class="ChatSearchBox col-12 w-100 d-flex border py-2 border-1 rounded-3 my-3">
          <input
            v-model="SearchConversation"
            type="search"
            placeholder="جستجو..."
            class="Search col-11"
          />
          <button class="SearchBtn ps-3 col-1 d-flex justify-content-center align-items-center">
            <i class="fas fa-search"></i>
          </button>
        </div>
      </div>

      <div class="row">
        <div class="col-12">
          <ProfileItem/>
        </div>
      </div>
    </div>
    <div class="col-lg-9">
      <div class="d-flex align-items-center w-100" style="border-bottom: 1px solid rgb(227 227 227);" >
        <div class="d-flex justify-content-between" style="height: 79px;">
          <p class="text-right pb-0 my-auto">shahbaz sh</p>
        </div>
      </div>
      <div class="py-3" style="overflow-y: scroll">
        <MyMessageItem/>
        <ContactMessageItem/>
        <MyMessageItem/>
        <MyMessageItem/>
        <ContactMessageItem/>
        <ContactMessageItem/>
        <ContactMessageItem/>
        <MyMessageItem/>
        <MyMessageItem/>
        <MyMessageItem/>
        <MyMessageItem/>
        <MyMessageItem/>
      </div>
      <div class="col-lg-12 col-12 py-4" dir="auto" style="border-top: 1px solid #e3e3e3;position: fixed;bottom: 0;">
        <div class=" d-flex align-items-center justify-content-between col-12">
          <button
            type="button"
            class="send-button-color btn border rounded-3 ms-2 chat-actions-height"
          >
            <!--                          <i-->
            <!--                            class="fas fa-paper-plane ms-1"-->
            <!--                            style="font-size: 16px"-->
            <!--                          ></i>-->
            ارسال
          </button>
          <input
            class="col-9 border p-2 chat-actions-height border-0 overflow-scroll"
            placeholder="پیام شما"
            type="text"
            name="content"
            dir="auto"
          />
          <button
            class="btn me-1 col-1 chat-actions-height"
          >
            <i
              class="far fa-smile-beam"
              style="font-size: 20px"
            ></i>
          </button>
        </div>
      </div>



    </div>
  </div>

</template>

<script>
import EditIcon from "../../components/Icons/EditIcon";
import ProfileItem from "../../components/Chat/ProfileItem";
import MyMessageItem from "../../components/Chat/MyMessageItem";
import ContactMessageItem from "../../components/Chat/ContactMessageItem";


export default {
  name: "chat_2",
  layout: "PoshtebamPlusLayout",
  components: {ContactMessageItem, MyMessageItem, ProfileItem, EditIcon },
  data(){
    return{
      SearchConversation: "",
    }
  }
}
</script>

<style scoped>

</style>
