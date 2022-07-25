<template>
  <div class="container mcontainer">
    <div class="row">
      <h2 class="tw-text-2xl tw-font-semibold pb-3">افزودن انجمن</h2>
      <div class="row">
        <div class="col-6 pt-3">
          <label>نام انجمن *</label>
          <input v-model="forum_title" type="text" class="with-border" placeholder="نام انجمن"
                 :class="{BorderRed:forum_title==='',BorderGreen:forum_title!==''}">
        </div>

        <div class="col-6 pt-3">
          <label>دسته بندی انجمن *</label>
          <v-select
            :options="Categories"
            v-model="SelectedCategoryId"
            label="title"
            placeholder="دسته بندی" dir="rtl"
            :reduce="title => title.forumCategoryId"
          ></v-select>
        </div>
        <div class="col-12 pt-3">
          <div class="labelText">
            هشتگ های انجمن
          </div>
          <div class="my-3">
            <input v-on:keyup.enter="addTags" v-model="tag" type="text" class="with-border"
                   placeholder="هشتگ">
            <div class="d-flex">
              <div class="back_tags p-1 m-1 d-flex" v-for="(tag,index) in tags">
                <button @click="removeTag(index)">
                  <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-3 tw-w-3 tw-pl-1" fill="none" viewBox="0 0 24 24"
                       stroke="currentColor" stroke-width="2">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12"/>
                  </svg>
                </button>
                {{ tag }}
              </div>
            </div>
          </div>
        </div>


        <div class="col-12 py-3">
          <label>توضیحات انجمن</label>
          <textarea
            v-model="forum_desc"
            row="100"
            class="form-control border rounded w-100"
            placeholder="توضیحات انجمن"
            id="about"
          ></textarea>
        </div>

        <div class="col-12 pt-3">
          <button type="button" class="button mt-auto" @click="createForum">
            ثبت
          </button>
        </div>
      </div>


    </div>


  </div>


</template>

<script>
export default {
  name: "AddForum",
  layout: "PoshtebamPlusLayout",
  data() {
    return {
      forum_title: '',
      forum_desc: '',
      tags: [],
      tag: '',
      Categories:[],
      SelectedCategoryId:null
    }
  },
  async fetch(){
    try {
      const res = await this.$repositories.getAllForumCategory.getAllForumCategory({
        pageNumber:1,
        count:100,
        searchCommand:''
      })
      this.Categories = res.data.forumCategories
    }catch (e) {
      console.log(e)
    }
  },
  methods: {
    async createForum() {
      if (this.forum_title === '') {
        this.$toast.error('نام انجمن را وارد کنید')
      } else if (this.forum_desc === '') {
        this.$toast.error("لطفا توضیحات انجمن را مشخص کنید");
      } else if(this.SelectedCategoryId===0){
        this.$toast.error("لطفا دسته بندی انجمن را انتخاب کنید");
      }else {
        let tmptags = []
        let tmptag = {
          type: 2,
          title: '',
        }
        this.tags.forEach((element) => {
          tmptag.title = element
          const clone = {...tmptag}
          tmptags.push(clone)
          tmptag.title = ''

        })
        try {
          this.$nextTick(() => {
            this.$nuxt.$loading.start()
          })
          const response = await this.$repositories.createForum.createForum({
            seoTitle: '',
            seoDescription: '',
            title: this.forum_title,
            description: this.forum_desc,
            userId: 0,
            tags: tmptags,
            forumCategoryId: this.SelectedCategoryId,
          })
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
          this.$toast.success('انجمن با موفقیت ساخته شد')
          this.$router.push({
            path: '/Forums/'
          })

        } catch (e) {
          console.log(e)
        } finally {
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
        }

      }
    },
    removeTag(index) {
      this.tags.splice(index, 1)
    },

    addTags() {
      if (this.tag === '') {
        this.$toast.error('هشتگ مورد نظر را وارد کنید')
      } else {
        this.tags.push(this.tag)
        this.tag = ''
      }
    },
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

.BorderRed {
  border: solid 1px red !important;
}

.BorderGreen {
  border: solid 1px green !important;
}
</style>
