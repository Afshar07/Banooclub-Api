<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="NewBg mcontainer p-3">
      <div class="tw-w-full bg-white tw-rounded tw-shadow p-3 d-flex align-items-center justify-content-between gap-3  rounded ">
        <div class="d-flex align-items-center gap-2">
          <img src="/girl-icon-forum.png" class="tw-w-[7rem] tw-h-[7rem] tw-object-contain" alt="">
          <div class="d-flex align-items-center flex-column">
            <h1 class="text-purple h6" style="font-weight: bolder!important;">افزودن تالار</h1>
            <strong class="text-pink">Add Forum</strong>
          </div>
        </div>

      </div>
      <div class="row bg-white rounded shadow p-3 my-3">
        <div class="col-6 pt-3">
          <label>نام تالار گفتگو *</label>
          <input v-model="forum_title" type="text" class="with-border FormInputs" style="height: 55px!important;" placeholder="موضوع گفتمان را بنویسید">
        </div>

        <div class="col-6 pt-3">
          <label>دسته بندی *</label>
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
            هشتگ های مرتبط
          </div>
          <div class="my-3">
            <div class="d-flex align-items-center gap-2">
              <input  v-model="tag" type="text" class="with-border FormInputs"
                     placeholder="هشتگ">
              <button class="p-2 rounded bg-purple text-white tw-cursor-pointer" @click="addTags">ثبت</button>
            </div>

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
          <label>توضیحات گفتگو</label>
          <textarea
            v-model="forum_desc"
            row="100"
            class="FormInputs border rounded w-100"
            placeholder="در مورد موضوع گفتگو یا سوالتان شرح دهید"
            id="about"
          ></textarea>
        </div>

        <div class="col-12 d-flex align-items-center justify-content-start pt-3">
          <button type="button" class="p-2 rounded bg-purple text-white tw-cursor-pointer" @click="createForum">
            ثبت
          </button>
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
  head() {
    return {

      title: 'افزودن تالار',
      meta: [
        {
          hid: "description",
          name: "description",
          content:'افزودن تالار',
        },
      ],
    };
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
        this.$toast.error('نام تالار را وارد کنید')
      } else if (this.forum_desc === '') {
        this.$toast.error("لطفا توضیحات تالار را مشخص کنید");
      } else if(this.SelectedCategoryId===0){
        this.$toast.error("لطفا دسته بندی تالار را انتخاب کنید");
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
          this.$toast.success('تالار با موفقیت ساخته شد')
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
