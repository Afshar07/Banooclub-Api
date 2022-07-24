<template>
  <div class="container mcontainer">
    <h1 class="tw-text-2xl tw-font-semibold">انجمن های من</h1>
    <div class="row py-3">
      <div class="col-lg-8 tw-flex-shirink-0">
        <ul class="custom_card tw-divide-y tw-divide-gray-100 sm:tw-m-0 tw--mx-5">
          <li v-for="Forum in my_forums">
            <ForumMainItem :forum_details="Forum"/>
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
export default {
  name: "MyForums",
  layout: "PoshtebamPlusLayout",
  components: {ForumMainItem,TopCommenters},

  head(){
    return{
      title: 'خدمات من'
    }
  },
  data(){
    return{
      my_forums:null,
    }
  },
  async fetch(){
    try {
      const forums = await this.$repositories.getMyForums.getMyForums({pageNumber:0, count:0, searchCommand:null})
      this.my_forums = forums.data.forums
      console.log(this.my_forums)
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
