<template>
  <div class="container mcontainer">

    <div class="row">

      <div class="col-lg-8 tw-flex-shirink-0">
        <div class="tab-content" id="pills-tabContent" v-if="!$fetchState.pending">
          <div class="tab-pane fade show active" id="forum-active-content" role="tabpanel" aria-labelledby="forum-active-tab">
            <div class="row boxMainContent mx-auto ">
              <div class="tw-card tw-card-compact bg-white rounded tw-w-96 tw-bg-base-100 ">
                <img src="https://placeimg.com/400/225/arch" class="tw-w-full tw-h-full  tw-inset-0 tw-object-cover" alt="Shoes" />
                <div class="tw-card-body">
                  <h2 class="tw-card-title">مقاله جدید</h2>
                  <div class="tw-flex tw-justify-start tw-items-center tw-gap-2">
                    <img src="/nopicture.jpg" width="80px" height="80px" class="rounded-circle" alt="">
                    <div class="tw-flex tw-flex-col tw-my-3">
                     <span class="tw-font-bold">saman wilson</span>
                      <small>asdasdasdasdasd</small>
                    </div>
                  </div>
                  <hr>
                  <div class="description">
                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing
                      elit. Architecto eius molestias quis sint suscipit temporibus voluptate! A accusantium dolore,
                      ducimus earum est fuga illum ipsam itaque iusto maiores minima nobis nostrum numquam, obcaecati placeat recusanda
                      e reiciendis tempora tenetur ut voluptatum. Ea ex facere illo incidunt laborum nostrum quae quia, sunt.</p>
                  </div>
                </div>
              </div>
              <div class=" bg-white rounded tw-p-4  tw-w-96 tw-bg-base-100 " style="margin-top: 2rem!important;">
                  <strong>مقالات مرتبط</strong>
                <div class="tw-grid tw-grid-cols-3  my-3">
                  <div class="tw-card tw-card-compact w-96 lg:tw-col-span-1 tw-col-span-6  bg-base-100 tw-mx-3.5 shadow-xl">
                   <img src="https://placeimg.com/400/225/arch" style="border-radius: 1rem!important;" alt="Shoes" />
                      <p class="my-2">مقاله جدید</p>
                      <div class="tw-flex tw-items-center tw-gap-2">
                        <div class="tw-flex tw-items-center tw-gap-1">
                          <small class=" tw-text-stone-400">11</small>
                          <i class="fas fa-comments tw-text-stone-400"></i>
                        </div>
                        <small class="text-secondary">{{new Date(Date.now()).toLocaleDateString('fa-IR')}}</small>
                      </div>
                  </div>
                  <div class="tw-card tw-card-compact w-96 lg:tw-col-span-1 tw-col-span-6  bg-base-100 tw-mx-3.5 shadow-xl">
                    <img src="https://placeimg.com/400/225/arch" style="border-radius: 1rem!important;" alt="Shoes" />
                    <p class="p-0 m-0">If a dog chews shoes whose shoes does he choose?</p>
                    <div class="tw-flex tw-items-center tw-gap-2">
                      <small class="text-secondary">{{new Date(Date.now()).toLocaleDateString('fa-IR')}}</small>
                      <div class="tw-flex tw-items-center tw-gap-1">
                        <small class=" tw-text-stone-400">11</small>
                        <i class="fas fa-comments tw-text-stone-400"></i>
                      </div>
                    </div>
                  </div>
                  <div class="tw-card tw-card-compact w-96 lg:tw-col-span-1 tw-col-span-6  bg-base-100 tw-mx-3.5 shadow-xl">
                    <img src="https://placeimg.com/400/225/arch" style="border-radius: 1rem!important;" alt="Shoes" />
                    <p class="p-0 m-0">If a dog chews shoes whose shoes does he choose?</p>
                    <div class="tw-flex tw-items-center tw-gap-2">
                      <small class="text-secondary">{{new Date(Date.now()).toLocaleDateString('fa-IR')}}</small>
                      <div class="tw-flex tw-items-center tw-gap-1">
                        <small class=" tw-text-stone-400">11</small>
                        <i class="fas fa-comments tw-text-stone-400"></i>
                      </div>
                    </div>
                  </div>
                </div>

              </div>
              <div class=" bg-white rounded tw-p-4  tw-w-96 tw-bg-base-100 " style="margin-top: 2rem!important;">
                <strong>  نظرات  (12)</strong>
                  <div class="my-2 tw-grid-cols-1">
                    <div class="tw-flex tw-flex-col tw-items-start tw-justify-between p-2">
                      <div class="tw-flex tw-justify-between tw-items-center tw-gap-2 tw-w-full">
                        <div class="tw-flex tw-justify-between tw-items-center tw-gap-2">
                          <img src="/nopicture.jpg" width="80px" height="80px" class="rounded-circle" alt="">
                          <div class="tw-flex tw-flex-col tw-my-3">
                            <span class="tw-font-bold">saman wilson</span>
                            <small>asdasdasdasdasd</small>
                          </div>
                        </div>
                        <button class=" rounded tw-bg-stone-400 tw-text-stone-300 p-2"> پاسخ</button>
                      </div>

                      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Accusantium animi dicta, eveniet facilis id maiores voluptas? Alias cumque eos sapiente.</p>
                    </div>
                  </div>

              </div>
            </div>
          </div>

        </div>

      </div>
      <RecentBlogs></RecentBlogs>
    </div>

  </div>

</template>

<script>

import RecentBlogs from '../../../components/Blog/RecentBlogs';
export default {
  name: "index",
  components: {RecentBlogs},
  layout: "PoshtebamPlusLayout",
  data(){
    return{
      AllForums: null,
      Search:'',
      SelectedPageId:1,
      totalPages:[]
    }
  },
  head(){
    return{
      title:'انجمن'
    }
  },
  watch:{
    Search:function (val,oldVal){
      this.GetAllForums(0)
    }
  },
  methods:{
    GetClassName(id) {
      if (id === this.SelectedPageId) {
        return 'ActivePage'
      } else {
        return ''
      }
    },
    SetSelectedPageId(id) {
      this.SelectedPageId = id
      this.GetAllForums();
    },
    IncrementSelectedPageId() {
      this.SelectedPageId++
      this.GetAllForums();
    },
    DecrementSelectedPageId() {
      this.SelectedPageId--
      this.GetAllForums();
    },

    async GetAllForums(id){
      try{
        this.$nextTick(()=>{
          this.$nuxt.$loading.start()
        })
        if (this.SelectedPageId > this.totalPages.length) {

          this.SelectedPageId = 1
        }
        if (this.SelectedPageId < 1) {

          this.SelectedPageId = 1
        }
        const forums = await this.$repositories.getAllForums.getAllForums({
            pageNumber:this.SelectedPageId,
            count:10,
            searchCommand:this.Search,
            noComments:id === 4,
            mostRated:id === 2,
            mostComments:id === 3,
          }
        )
        this.totalPages = []
        const result = Math.ceil(forums.data.forumsCount / 10)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
        }

        this.AllForums = forums.data.forums
        this.$nuxt.$loading.finish()
        this.$nuxt.loading = false;
      }
      catch (error){
        console.log(error)
      }finally {
        this.$nuxt.$loading.finish()
        this.$nuxt.loading = false;
      }
    }
  },
  async fetch(){
    try{
      if (this.SelectedPageId > this.totalPages.length) {

        this.SelectedPageId = 1
      }
      if (this.SelectedPageId < 1) {

        this.SelectedPageId = 1
      }
      const forums = await this.$repositories.getAllForums.getAllForums({
          pageNumber:1,
          count:10,
          searchCommand:this.Search,
          noComments:this.NoComments,
          mostRated:this.MostRated,
          mostComments:this.MostComments,
        }
      )
      this.totalPages = []
      const result = Math.ceil(forums.data.forumsCount / 10)
      for (let i = 1; i <= result; i++) {
        this.totalPages.push(i);
      }
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

.ActivePage {
  color: white;
  background-color: #00adef;
}

</style>
