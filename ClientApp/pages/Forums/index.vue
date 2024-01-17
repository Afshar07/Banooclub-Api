<template>
  <div :class="$fetchState.pending?'loading-skeleton':''" class="NewBg mcontainer p-3">
    <div class="tw-w-full bg-white tw-rounded tw-shadow p-3 d-flex align-items-center justify-content-between gap-3  rounded ">
      <div class="d-flex align-items-center gap-2">
        <img src="/girl-icon-forum.png" class="tw-w-[7rem] tw-h-[7rem] tw-object-contain" alt="">
        <div class="d-flex align-items-center flex-column">
          <h1 class="text-purple h6" style="font-weight: bolder!important;">تالار  گفتگو</h1>
          <strong class="text-pink">Forum</strong>
        </div>
      </div>
      <button v-tooltip="{content:'افزودن تالار جدید'}" @click="$router.push('/Forums/AddForum')" class="btn AddReplyBtn text-white">
        <LazyPlusIcon fill="#ff6f9e" style="width: 30px; height: 30px;"/>
      </button>
    </div>
    <div class="row bg-white tw-rounded tw-shadow p-3 my-3">
        <div class="col-md-12">
          <div class="row">
            <div class="col-md-8 d-flex align-items-center">
              <input type="text" class="SearchStyle" v-model="Search" placeholder="جستجو تالار ها">
            </div>
            <div class="col-md-4">
              <v-select
                v-model="SelectedCategoryId"
                :options="Categories"
                placeholder="جستجو بر اساس دسته بندی"
                label="title"
                :reduce="title=> title.forumCategoryId"
              />
            </div>
          </div>
        </div>
      <div class="col-lg-8 tw-flex-shirink-0">
        <div class="row">

          <div class="col-md-12">
            <ul class="nav nav-pills align-items-end profile_tabs py-3" id="pills-tab" role="tablist">
              <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
                <button :value="1" class="nav-link active" id="forum-active-tab" data-bs-toggle="pill"
                        @click="GetAllForums(1)"
                        data-bs-target="#products-pills-home" type="button" role="tab"
                        aria-controls="products-pills-home" aria-selected="true">
                  پر بازدید ها
                </button>
              </li>
              <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
                <button :value="2" class="nav-link" id="forum-second-tab" data-bs-toggle="pill"
                        data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile"
                        aria-selected="false"
                        @click="GetAllForums(2)">
                  برترین ها
                </button>
              </li>
              <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
                <button :value="3" class="nav-link" id="forum-third-tab" data-bs-toggle="pill"
                        data-bs-target="#pills-contact" type="button" role="tab" aria-controls="pills-contact"
                        aria-selected="false"
                        @click="GetAllForums(3)">
                  بیشترین پاسخ
                </button>
              </li>
              <li class="nav-item" role="presentation m-0" style="margin: 0 !important;">
                <button :value="4" class="nav-link" id="forum-fourth-tab" data-bs-toggle="pill"
                        data-bs-target="#pills-contact" type="button" role="tab" aria-controls="pills-contact"
                        aria-selected="false"
                        @click="GetAllForums(4)">
                  بی پاسخ
                </button>
              </li>
            </ul>
          </div>
          <div class="col-md-12">
            <div class="tab-content" id="pills-tabContent" v-if="!$fetchState.pending">
              <div class="tab-pane fade show active" id="forum-active-content" role="tabpanel"
                   aria-labelledby="forum-active-tab">
                <div class="row boxMainContent mx-auto">
                  <ul class="custom_card tw-divide-y tw-divide-gray-100 sm:tw-m-0  ">
                    <li v-for="Forum in AllForums.filter(e=> e.status===1)">
                      <nuxt-link class="text-decoration-none text-black" :to="`/Forums/${Forum.forumId}/`">
                        <LazyForumMainItem :forum_details="Forum"/>
                      </nuxt-link>

                    </li>
                  </ul>

                </div>
              </div>

            </div>
          </div>
          <div class="col-md-12">

            <div v-if="totalPages.length>1" class="col-md-12  d-flex justify-content-center my-5 border-top pt-2">
              <nav aria-label="Page navigation example">
                <ul class="pagination px-0">
                  <li class=" Next" @click="DecrementSelectedPageId">
                    <a class="page-link Next" href="#" aria-label="Previous">
                      <span aria-hidden="true">&laquo;</span>
                    </a>
                  </li>
                  <li @click="SetSelectedPageId(i)" class="page-item" v-for="i in totalPages" :key="i"><a
                    class="page-link" :class="GetClassName(i)" href="#">{{ i }}</a></li>

                  <li class=" Prev " @click="IncrementSelectedPageId">
                    <a class="page-link Prev" href="#" aria-label="Next">
                      <span aria-hidden="true">&raquo;</span>
                    </a>
                  </li>
                </ul>
              </nav>
            </div>
          </div>
        </div>


      </div>
      <LazyTopCommenters></LazyTopCommenters>
    </div>

  </div>

</template>

<script>
export default {
  name: "index",
  layout: "PoshtebamPlusLayout",
  data() {
    return {
      AllForums: null,
      Search: '',
      SelectedPageId: 1,
      totalPages: [],
      SelectedCategoryId: null,
      Categories: []
    }
  },
  head() {
    return {
      title: 'تالار'
    }
  },
  watch: {
    Search: function (val, oldVal) {
      this.GetAllForums(0)
    },
    SelectedCategoryId: function () {
      this.GetAllForums(0)
    }
  },
  methods: {
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

    async GetAllForums(id) {
      try {
        if (this.Search === '') {
          this.$nextTick(() => {
            this.$nuxt.$loading.start()
          })
        }

        if (this.SelectedPageId > this.totalPages.length) {

          this.SelectedPageId = 1
        }
        if (this.SelectedPageId < 1) {

          this.SelectedPageId = 1
        }
        const forums = await this.$repositories.getAllForums.getAllForums({
            pageNumber: this.SelectedPageId,
            count: 10,
            searchCommand: this.Search,
            noComments: id === 4,
            mostRated: id === 2,
            mostComments: id === 3,
            mostViewed: id === 1,
            categoryId: this.SelectedCategoryId
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
      } catch (error) {
        console.log(error)
      } finally {
        this.$nuxt.$loading.finish()
        this.$nuxt.loading = false;
      }
    }
  },
  async fetch() {
    try {
      const res = await this.$repositories.getAllForumCategory.getAllForumCategory({
        pageNumber: 1,
        count: 100,
        searchCommand: ''
      })
      this.Categories = res.data.forumCategories
    } catch (e) {
      console.log(e)
    }
    try {
      if (this.SelectedPageId > this.totalPages.length) {

        this.SelectedPageId = 1
      }
      if (this.SelectedPageId < 1) {

        this.SelectedPageId = 1
      }
      const forums = await this.$repositories.getAllForums.getAllForums({
          pageNumber: 1,
          count: 10,
          searchCommand: this.Search,
          noComments: this.NoComments,
          mostRated: this.MostRated,
          mostComments: this.MostComments,
        }
      )
      this.totalPages = []
      const result = Math.ceil(forums.data.forumsCount / 10)
      for (let i = 1; i <= result; i++) {
        this.totalPages.push(i);
      }
      this.AllForums = forums.data.forums
    } catch (error) {
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

.nav-pills .nav-link.active, .nav-pills .show > .nav-link {
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


.Next {
  border-top-right-radius: 5px !important;
  border-bottom-right-radius: 5px !important;
  border-top-left-radius: 0px !important;
  border-bottom-left-radius: 0px !important;
}


.Prev {
  border-top-right-radius: 0px !important;
  border-bottom-right-radius: 0px !important;
  border-top-left-radius: 5px !important;
  border-bottom-left-radius: 5px !important;
}

</style>
