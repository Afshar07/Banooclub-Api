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
      <div class="col-lg-4 tw-pt-5">
        <div>
          <h2 class="tw-text-xl tw-font-semibold tw-mb-2"> مشارکت کنندگان برتر </h2>
          <p> افرادی که بیشترین بحث را در گفتگوها شروع کردند. </p>
          <br>
          <ul class="tw-space-y-3">
            <li v-for="i in 5">
              <div class="tw-flex tw-items-center my-2">
                <img src="~/assets/images/products/product_image.jpg" alt="" class="tw-w-8 tw-h-8 tw-rounded-full">
                <nuxt-link to="#" class="tw-font-semibold tw-px-2 text-decoration-none tw-text-gray-700"> علی حسینی </nuxt-link>
                <div class="tw-flex tw-items-center">
                  <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6 tw-text-lg" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M8 12h.01M12 12h.01M16 12h.01M21 12c0 4.418-4.03 8-9 8a9.863 9.863 0 01-4.255-.949L3 20l1.395-3.72C3.512 15.042 3 13.574 3 12c0-4.418 4.03-8 9-8s9 3.582 9 8z" />
                  </svg>
                  <span class="px-2"> 137 </span>
                </div>
              </div>
            </li>
          </ul>

        </div>
      </div>
    </div>
  </div>


</template>

<script>
import ForumMainItem from "../../components/Forums/ForumMainItem";

export default {
  name: "MyForums",
  layout: "BanooClubLayout",
  components: {ForumMainItem},

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
