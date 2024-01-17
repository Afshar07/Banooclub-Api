<template>

    <div class="tw-pt-3">
      <h2 class="tw-text-xl tw-font-semibold tw-mb-2"> مقالات اخیر </h2>
      <ul class="tw-space-y-3">
        <li v-for="item in RecentBlog">
          <div class="tw-flex tw-flex-col tw-items-start my-2">
            <nuxt-link class="text-decoration-none tw-text-black hover:tw-text-blue-500 tw-transition" :to="`/Blog/BlogDetail/${item.blogId}/${item.seourl}`">
              <p class="m-0">{{item.title}}</p>
            </nuxt-link>
            <div class="tw-flex tw-items-center tw-justify-end tw-gap-2">
              <div class="tw-flex tw-items-center tw-gap-1">
                <small class=" tw-text-stone-400">{{item.commentsCount}}</small>
                <i class="fas fa-comments tw-text-stone-400"></i>
              </div>
              <small class="text-secondary">{{new Date(item.createDate).toLocaleDateString('fa-IR')}}</small>
            </div>
          </div>
        </li>
      </ul>

    </div>

</template>

<script>
export default {
  name: "RecentBlogs",
  data(){
    return{
      RecentBlog:[]
    }
  },

  async fetch (){
      try {
        const res = await this.$repositories.GetMostPopular.GetMostPopular({
          pageNumber:1,
          count:5
        })
        this.RecentBlog = res.data.blogs
      }catch (e){
        console.log(e)
      }
  }
}
</script>

<style scoped>

</style>
