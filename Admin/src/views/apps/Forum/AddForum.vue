<template>
  <b-form >
    <b-row>
      <b-col md="6">
        <b-form-group
            label="نام تالار"
            label-for="mc-first-name"
        >
          <b-form-input
              id="mc-first-name"
              placeholder="نام تالار"
              v-model="ForumName"
          />
        </b-form-group>
      </b-col>
      <b-col md="6">
        <b-form-group
            label="توضیحات تالار"
            label-for="mc-last-name"
        >
          <b-form-input
              id="mc-last-name"
              placeholder="توضیحات تالار"
              v-model="ForumDescription"
          />
        </b-form-group>
      </b-col>
      <b-col md="6">
        <b-form-group
            label="دسته بندی تالار"
            label-for="mc-last-name"
        >
          <v-select
              v-model="SelectedCategoryId"
              :options="Categories"
              label="title"
              :reduce="title => title.forumCategoryId"
              :clearable="false"
              class="per-page-selector d-inline-block w-100"
          />
        </b-form-group>

      </b-col>

      <b-col md="6">
        <b-form-group
            label="برچسب تالار (پس از افزودن دکمه enter را بفشارید)"
            label-for="mc-last-name"
        >
          <b-form-input
              v-model="NewTag"
              id="mc-Tag-name"
              placeholder="نام برچسب "
              @keydown.enter.prevent="AddNewTag"
          />
        </b-form-group>
        <div class="d-flex align-items-center flex-wrap  justify-content-start  ">
          <div v-for="(item,idx) in newTags"  :key="idx" class="d-flex align-items-center mx-25 my-1 ">
            <b-badge variant="primary" @click.prevent="RemoveTag(item)">
              <feather-icon
                  icon="MinusIcon"
                  class="mr-25"
              />
              <span>{{item.title}}</span>
            </b-badge>


          </div>

        </div>
      </b-col>
      <!-- submit and reset -->
      <b-col lg="12 my-2">
        <b-button
            v-ripple.400="'rgba(255, 255, 255, 0.15)'"
            type="button"
            variant="primary"
            class="mr-1"
            @click="CreateForum"
        >
          ثبت
        </b-button>
        <b-button
            v-ripple.400="'rgba(186, 191, 199, 0.15)'"
            type="reset"
            variant="outline-secondary"
        >
          پاک کردن همه
        </b-button>
      </b-col>
    </b-row>
  </b-form>
</template>


<script>
import {
  BRow, BCol, BFormGroup, BFormInput, BFormCheckbox, BForm, BButton,BBadge
} from 'bootstrap-vue'
import Ripple from 'vue-ripple-directive'
import vSelect from 'vue-select'
import ForumCreateRequest from '@/libs/Api/Forum/CreateForum'

import ForumCategoryGetAll from '@/libs/Api/Forum/ForumCategoryGetAll'
import ToastificationContent from '@core/components/toastification/ToastificationContent'
export default {
  components: {
    BRow,
    BCol,
    BBadge,
    BFormGroup,
    BFormInput,
    BFormCheckbox,
    BForm,
    vSelect,
    BButton,
  },
  directives: {
    Ripple,
  },
  data(){
    return{
      ForumName:'',
      ForumDescription:'',
      Categories:[],
      SelectedCategoryId:0,
      NewTag:'',
      newTags:[]
    }
  },
  mounted(){
  this.GetAllForumCategory()
  },
  title: ' ساخت تالار جدید  - پنل ادمین بانو کلاب',
  methods:{
    RemoveTag(idx){
      this.newTags.splice(idx,1)
    },

    AddNewTag(){
      let tmptag={
        title:''
      }
      tmptag.title = this.NewTag
      const clone = {...tmptag}

      this.newTags.push(clone)
      this.NewTag = ''
      tmptag.title = ''
    },
    async CreateForum(){

      let _this = this
      let data = {
        seoTitle: this.ForumName,
        seoDescription: this.ForumDescription,
        title: this.ForumName,
        description: this.ForumDescription,
        userId: 0,
        tags: this.newTags,
        forumCategoryId: this.SelectedCategoryId,
      }
      let forumCreateRequest = new ForumCreateRequest()
           forumCreateRequest.setParams(data)
      await forumCreateRequest.fetch(()=>{
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `تالار با موفقیت ساخته شد`,
          },
        })
        this.$router.push('/apps/Forum/ForumsList')

      },()=>{})



    },
    async GetAllForumCategory() {
      let _this = this
      let forumCategoryGetAll = new ForumCategoryGetAll(_this)
      let data = {
        pageNumber:1,
        Search:''
      }
      forumCategoryGetAll.setParams(data)
      await forumCategoryGetAll.fetch(function (content) {
        _this.Categories = content.forumCategories
      }, function (error) {
        console.log(error)
      })
    },
  }


}
</script>
