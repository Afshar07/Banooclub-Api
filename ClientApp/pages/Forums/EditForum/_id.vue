<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="NewBg mcontainer">
      <div class="tw-w-full bg-white tw-rounded tw-shadow p-3 d-flex align-items-center tw-rounded tw-shadow justify-content-between gap-3  rounded ">
        <div class="d-flex align-items-center gap-2">
          <img src="/girl-icon-forum.png" class="tw-w-[7rem] tw-h-[7rem] tw-object-contain" alt="">
          <div class="d-flex align-items-center flex-column">
            <h1 class="text-purple h6">ویرایش تالار</h1>
            <strong class="text-pink">Edit Forum</strong>
          </div>
        </div>
        <button v-tooltip="{content:'افزودن تالار جدید'}" @click="$router.push('/Forums/AddForum')" class="btn AddReplyBtn text-white">
          <PlusIcon fill="#ff6f9e" style="width: 30px; height: 30px;"/>
        </button>
      </div>
      <div class="row bg-white rounded shadow p-3 my-3">
        <div class="col-12 pt-3">
          <label>نام تالار *</label>
          <input v-model="ForumData.title" type="text" class="with-border FormInputs" placeholder="نام تالار"
                 :class="{BorderRed:ForumData.title==='',BorderGreen:ForumData.title!==''}">
        </div>
        <div class="col-12 pt-3">
          <div class="labelText">
            هشتگ های تالار
          </div>
          <div class="my-3">
            <div class="d-flex align-items-center gap-2">
              <input v-model="tag" type="text" class="with-border FormInputs" placeholder="هشتگ">
              <button class="bg-purple p-2 rounded shadow text-white" @click="addTags">
              <small>ثبت </small>
              </button>
            </div>


            <div class="d-flex">
              <div class="back_tags p-1 m-1 d-flex" v-for="(tag,index) in ForumData.tags">
                <button @click="removeTag(tag.tagId)">
                  <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-3 tw-w-3 tw-pl-1" fill="none" viewBox="0 0 24 24"
                       stroke="currentColor" stroke-width="2">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12"/>
                  </svg>
                </button>
                {{ tag.title }}
              </div>
            </div>
          </div>
        </div>


        <div class="col-12 py-3">
          <label>توضیحات تالار</label>
          <textarea
            v-model="ForumData.description"
            row="100"
            class=" FormInputs   border rounded w-100"
            placeholder="توضیحات تالار"
            id="about"
          ></textarea>
        </div>

        <div class="col-12 pt-3">
          <button type="button" class="button mt-auto" @click="UpdateForum">
            ویرایش
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
        ForumData:[],
      tag:'',
      ForumTitle:''
    }
  },
  head() {
    return {

      title: 'ویرایش تالار',
      meta: [
        {
          hid: "description",
          name: "description",
          content:'ویرایش تالار',
        },
      ],
    };
  },
  async fetch(){
    try {

      const res = await this.$repositories.getAForum.getAForum({
        forumId: this.$route.params.id
      })
      console.log(res)
      this.ForumData = res.data


    }catch (e) {
      console.log(e)
    }finally {

    }
  },
  methods: {
    async UpdateForum() {
      if (this.ForumData.title === '') {
        this.$toast.error('نام تالار را وارد کنید')
      } else if (this.ForumData.description === '') {
        this.$toast.error("لطفا توضیحات تالار را مشخص کنید");
      } else {
        try {
          this.$nextTick(() => {
            this.$nuxt.$loading.start()
          })
          const response = await this.$repositories.UpdateForum.UpdateForum({
            comments: this.ForumData.comments,
            createDate: this.ForumData.createDate,
            description: this.ForumData.description,
            forumCategoryId: this.ForumData.forumCategoryId,
            forumId: this.ForumData.forumId,
            rate: this.ForumData.rate,
            seoDescription: this.ForumData.seoDescription,
            seoTitle: this.ForumData.seoTitle,
            tags: this.ForumData.tags,
            title: this.ForumData.title,
            userId: this.ForumData.userId,
            viewsCount: this.ForumData.viewsCount,
          })
          this.$nuxt.$loading.finish();
          this.$nuxt.loading = false;
          this.$toast.success('تالار با موفقیت بروزرسانی شد')
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
   async removeTag(id) {
      try {
        const res = await this.$repositories.DeleteTag.DeleteTag({
       id:id
        })
        this.$toast.success('هشتگ با موفقیت حذف شد')
        this.$nuxt.refresh();

      }catch (e) {
        console.log(e)
      }
    },
  async  addTags() {
      try {
        const res = await this.$repositories.AddTag.AddTag({
          type: 2,
          objectId: this.ForumData.forumId,
          title:this.tag,
          createDate: new Date(Date.now())

        })
        this.tag = ''
        this.$toast.success('هشتگ جدید با موفقیت ساخته شد')
        this.$nuxt.refresh();

      }catch (e) {
        console.log(e)
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
