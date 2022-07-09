<template>
  <div class="container mcontainer">
    <h1 class="tw-text-2xl tw-font-semibold"> انجمن  </h1>
    <div class="row">
      <div class="col-lg-8 tw-flex-shirink-0">
        <ul class="nav nav-pills align-items-end profile_tabs py-3" id="pills-tab" role="tablist">
          <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
            <button class="nav-link active" id="forum-active-tab" data-bs-toggle="pill"
                    data-bs-target="#products-pills-home" type="button" role="tab" aria-controls="products-pills-home" aria-selected="true">
              پر بازدید ها
            </button>
          </li>
          <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
            <button class="nav-link" id="forum-second-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">
              ویژه ها
            </button>
          </li>
          <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
            <button class="nav-link" id="forum-third-tab" data-bs-toggle="pill" data-bs-target="#pills-contact" type="button" role="tab" aria-controls="pills-contact" aria-selected="false">
              اخیر
            </button>
          </li>
          <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
          <button class="nav-link" id="forum-fourth-tab" data-bs-toggle="pill" data-bs-target="#pills-contact" type="button" role="tab" aria-controls="pills-contact" aria-selected="false">
            بی پاسخ
          </button>
        </li>
        </ul>
        <div class="tab-content" id="pills-tabContent" v-if="!$fetchState.pending">
          <div class="tab-pane fade show active" id="forum-active-content" role="tabpanel" aria-labelledby="forum-active-tab">
            <div class="row boxMainContent mx-auto">
              <ul class="custom_card tw-divide-y tw-divide-gray-100 sm:tw-m-0 tw--mx-5">
                <li v-for="Forum in AllForums">
                  <ForumMainItem :forum_details="Forum" />
                </li>
              </ul>

            </div>
          </div>
          <div class="tab-pane fade" id="forum-second-tab-content" role="tabpanel" aria-labelledby="forum-second-tab">
          </div>
          <div class="tab-pane fade" id="forum-third-tab-content" role="tabpanel" aria-labelledby="forum-third-tab">
          </div>
          <div class="tab-pane fade" id="forum-fourth-tab-content" role="tabpanel" aria-labelledby="forum-fourth-tab">
          </div>
        </div>
      </div>
        <TopCommenters></TopCommenters>
    </div>

  </div>

</template>

<script>
import ForumMainItem from "../../components/Forums/ForumMainItem";
import TopCommenters from '../../components/Forums/TopCommenters';
export default {
  name: "index",
  components: {ForumMainItem,TopCommenters},
  layout: "PoshtebamPlusLayout",
  data(){
    return{
      AllForums: null
    }
  },
  head(){
    return{
      title:'انجمن'
    }
  },
  async fetch(){
    try{
      const forums = await this.$repositories.getAllForums.getAllForums({
          pageNumber:0,
          count:0,
          searchCommand:null
        }
      )
      this.AllForums = forums.data.forums
    }
    catch (error){
      console.log(error)
    }
  },


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
.nav-link {
  font-weight: 500;
  color: #8c8d90 !important;
  font-weight: 500;
  padding: 5px 9px 15px;
  border-bottom: 3px solid transparent;
}
.nav-pills .nav-link.active, .nav-pills .show>.nav-link {
  color: #0d6efd !important;
  background-color: transparent;
  font-weight: 500;
  border-bottom: 3px solid #0d6efd;
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

</style>
