<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="NewBg mcontainer p-3">
    <div class="tw-w-full bg-white p-3 d-flex align-items-center tw-rounded tw-shadow justify-content-between gap-3  rounded ">
      <div class="d-flex align-items-center gap-2">
        <img src="/girl-icon-forum.png" class="tw-w-[7rem] tw-h-[7rem] tw-object-contain" alt="">
        <div class="d-flex align-items-center flex-column">
          <h1 class="text-purple h6" style="font-weight: bolder!important;">تالار  من</h1>
          <strong class="text-pink">My Forum</strong>
        </div>
      </div>
      <button v-tooltip="{content:'افزودن تالار جدید'}" @click="$router.push('/Forums/AddForum')" class="btn AddReplyBtn text-white">
        <PlusIcon fill="#ff6f9e" style="width: 30px; height: 30px;"/>
      </button>
    </div>
    <div class="row my-3 p-3 rounded shadow bg-white">
      <div class="col-lg-8 tw-flex-shirink-0">
        <ul class="custom_card tw-divide-y tw-divide-gray-100 sm:tw-m-0 tw--mx-5">
          <li v-if="my_forums.length>0" v-for="Forum in my_forums">
            <ForumMainItem :forum_details="Forum"/>
          </li>
          <li class="text-center p-3" v-if="my_forums.length===0">
            <span class="text-warning">شما تالار فعال ندارید.</span>
          </li>
        </ul>
      </div>
      <TopCommenters></TopCommenters>
    </div>
  </div>


</template>

<script>
import ForumMainItem from "../../components/Forums/ForumMainItem";
import TopCommenters from '../../components/Forums/TopCommenters';
import PlusIcon from "@/components/Icons/PlusIcon";
export default {

  name: "MyForums",
  layout: "PoshtebamPlusLayout",
  components: {ForumMainItem,TopCommenters,PlusIcon},

  head() {
    return {

      title: 'تالار های من',
      meta: [
        {
          hid: "description",
          name: "description",
          content:'تالار های من',
        },
      ],
    };
  },
  data(){
    return{
      my_forums:[],
    }
  },
  async fetch(){
    try {
      const forums = await this.$repositories.getMyForums.getMyForums({pageNumber:0, count:0, searchCommand:null})
      this.my_forums = forums.data.forums

    }
    catch (error){
      console.log(error)
    }
  }

}
</script>

<style scoped>
@media (min-width: 1024px) {
  .mcontainer {
    max-width: 1000px;
    padding: 30px 0px;
    margin-left: auto;
    margin-right: auto;
  }
}
@media (max-width: 1024px) {
  .mcontainer {
    max-width: 1000px;
    padding: 25px 0px;
    margin-left: auto;
    margin-right: auto;
  }
}

</style>
