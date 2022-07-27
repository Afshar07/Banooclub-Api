<template>
  <div class="container mcontainer">
    <h1 class="tw-text-2xl tw-font-semibold"> مقالات  </h1>
    <div class="row">

      <div class="col-lg-8 tw-flex-shirink-0">
        <div class="col-md-12">
          <input type="text" class="form-control " v-model="Search" placeholder="جستجوی مقالات">
        </div>
<!--        <ul class="nav nav-pills align-items-end profile_tabs py-3" id="pills-tab" role="tablist">-->
<!--          <li class="nav-item" role="presentation m-0" style="margin: 0 !important;" >-->
<!--            <button   :value="1" class="nav-link active" id="forum-active-tab" data-bs-toggle="pill"-->
<!--                      data-bs-target="#products-pills-home" type="button" role="tab" aria-controls="products-pills-home" aria-selected="true">-->
<!--              پر بازدید ها-->
<!--            </button>-->
<!--          </li>-->
<!--          <li class="nav-item" role="presentation m-0" style="margin: 0 !important;"  >-->
<!--            <button  :value="2" class="nav-link" id="forum-second-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">-->
<!--              برترین ها-->
<!--            </button>-->
<!--          </li>-->

<!--        </ul>-->
        <div class="tab-content" id="pills-tabContent" v-if="!$fetchState.pending">
          <div class="tab-pane fade show active" id="forum-active-content" role="tabpanel" aria-labelledby="forum-active-tab">
            <div class="row boxMainContent mx-auto">
              <ul class="custom_card tw-divide-y tw-divide-gray-100 sm:tw-m-0 tw--mx-5">
                <li v-for="item in AllBlogs">
                  <nuxt-link class="text-decoration-none" :to="`/Blog/BlogDetail/${item.blogId}/${item.seourl}`">
                    <div class="tw-card lg:tw-card-side bg-base-100 shadow-xl">

                      <div class="tw-card-body">
                        <h2 class="tw-card-title tw-text-stone-500">{{ item.title }}</h2>
                        <p class="tw-card-text tw-text-stone-400">{{ item.summary }}</p>
                        <div class="tw-flex tw-gap-2 tw-justify-end">
                          <div class="tw-flex tw-items-center tw-gap-1">
                            <small class=" tw-text-stone-400">{{ new Date(item.createDate).toLocaleDateString('fa-IR') }}</small>
                            <i class="far fa-clock tw-text-stone-400"></i>
                          </div>
                          <div class="tw-flex tw-items-center tw-gap-1">
                            <small class=" tw-text-stone-400">{{ item.commentsCount }}</small>
                            <i class="fas fa-comments tw-text-stone-400"></i>
                          </div>
                          <div class="tw-flex tw-items-center tw-gap-1">
                            <small class=" tw-text-stone-400">{{ item.likeCount }}</small>
                            <i v-if="!item.isFavourite"  class="far fa-heart tw-text-stone-400"></i>
                            <i v-else  class="fas fa-heart tw-text-red-600"></i>
                          </div>

                        </div>
                      </div>
                      <div class="tw-relative">
                        <div v-if="item.tags.length>0" class="tw-badge tw-absolute tw-badge-primary tw-m-3 tw-p-2 tw-left-0">{{ item.tags[0].title }}</div>
                        <img :src="`https://banooclubapi.simagar.com/${item.fileData}`" alt="Album" class="tw-rounded tw-m-2 tw-object-cover tw-object-center" width="300px" style="height: 170px!important;">
                      </div>

                    </div>
                  </nuxt-link>

                </li>
              </ul>

            </div>
          </div>

        </div>
        <Pagination v-if="totalPages.length>1" :pages="totalPages" @PageChanged="GetAllBlog($event)"></Pagination>
      </div>
      <RecentBlogs></RecentBlogs>
    </div>

  </div>

</template>

<script>
import ForumMainItem from "../../components/Forums/ForumMainItem";
import RecentBlogs from '../../components/Blog/RecentBlogs';
import Pagination from '../../components/Pagination';
export default {
  name: "index",
  components: {ForumMainItem,RecentBlogs,Pagination},
  layout: "PoshtebamPlusLayout",
  data(){
    return{
      AllBlogs: null,
      Search:'',
      SelectedPageId:1,
      totalPages:[]
    }
  },
  head(){
    return{
      title:'مقالات'
    }
  },
  watch:{
    Search:function (val,oldVal){
      this.GetAllBlog(1)
    }
  },
  methods:{

    async GetAllBlog(id){
      try {
        this.$nextTick(()=>{
          this.$nuxt.$loading.start()
        })
        const res = await this.$repositories.GetAllBlog.GetAllBlog({
          searchByTitle:this.Search,
          pageNumber:id,
          count:10,
          tagId:0,
        })
        this.AllBlogs = res.data.blogs
        this.totalPages = []
        const result = Math.ceil(res.data.blogsCount / 10)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
        }
        this.$nuxt.$loading.finish();
        this.$nuxt.loading = false;
      }catch (e) {
        console.log(e)
      }finally {
        this.$nuxt.$loading.finish();
        this.$nuxt.loading = false;
      }
    }
  },
  async fetch(){
      try {
          const res = await this.$repositories.GetAllBlog.GetAllBlog({
            searchByTitle:this.Search,
            pageNumber:this.SelectedPageId,
            count:10,
            tagId:0,
          })
        this.AllBlogs = res.data.blogs
        this.totalPages = []
        const result = Math.ceil(res.data.blogsCount / 10)
        for (let i = 1; i <= result; i++) {
          this.totalPages.push(i);
        }
      }catch (e) {
        console.log(e)
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
