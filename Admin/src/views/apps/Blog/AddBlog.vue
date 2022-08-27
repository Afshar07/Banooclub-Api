<template>
  <b-form @submit.prevent="CreateBlog">
    <b-row>
      <b-col md="6">
        <b-form-group
            label="نام مقاله"
            label-for="mc-first-name"
        >
          <b-form-input
              v-model="BlogTitle"
              id="mc-first-name"
              placeholder="نام مقاله"
          />
        </b-form-group>
      </b-col>
      <b-col md="6">
        <b-form-group
            label="توضیحات کوتاه مقاله"
            label-for="mc-last-name"
        >
          <b-form-input
              v-model="BlogSummary"
              id="mc-last-name"
              placeholder="توضیحات کوتاه مقاله"
          />
        </b-form-group>
      </b-col>
      <b-col md="6">
        <b-form-group
            label="دسته بندی مقاله"
            label-for="mc-last-name"
        >
          <v-select
              v-model="SelectedCategoryId"
              :options="Categories"
              label="name"
              :reduce="name => name.blogCategoryId"
              :clearable="false"

              placeholder="دسته بندی مقاله"
              input-id="user-status"
          />
        </b-form-group>

      </b-col>
      <b-col md="6">
        <b-form-group
            label="عکس مقاله"
            label-for="mc-last-name"
        >
          <b-form-file
              ref="File"
              @input="HandleFile"
              placeholder="فایل خود را انتخاب کنید"
              drop-placeholder="Drop file here..."
          />
        </b-form-group>
        <div v-if="BaseImgUrl!==''">
          <img :src="BaseImgUrl" width="100px" height="100px" alt="">
        </div>


      </b-col>
      <b-col md="6">
        <b-form-group
            label="برچسب مقاله (پس از افزودن دکمه enter را بفشارید)"
            label-for="mc-last-name"
        >
          <b-form-input
              v-model="NewTag"
              id="mc-Tag-name"
              placeholder="نام برچسب "
              @keydown.enter.prevent="AddNewTag"
          />
        </b-form-group>
        <div class="d-flex align-items-center gap-1 my-2">
          <div v-for="(item,idx) in newTags"  :key="idx" class="d-flex align-items-center gap-1">
           <span    class="badge d-flex align-items-center mx-25 px-25 py-25  cursor-pointer pill bg-primary text-white" >
               <feather-icon icon="MinusIcon" size="20" class="text-white mr-1" @click.prevent="RemoveTag(item)"></feather-icon>
              <span> {{item.title}}</span>

            </span>
          </div>

        </div>
      </b-col>
      <b-col md="12">
        <b-form-group
            label-for="Email"
            label="توضیحات مقاله"
        >
          <div class="form-label-group">
            <b-form-textarea
                id="mc-email"
                v-model="BlogDescription"
                placeholder="توضیحات"
            />
          </div>
        </b-form-group>
      </b-col>


      <!-- submit and reset -->
      <b-col>
        <b-button
            v-ripple.400="'rgba(255, 255, 255, 0.15)'"
            type="submit"
            variant="primary"
            class="mr-1"
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
  BRow, BCol, BFormGroup, BFormInput, BFormCheckbox, BForm, BButton, BFormFile, BFormTextarea
} from 'bootstrap-vue'
import Ripple from 'vue-ripple-directive'
import vSelect from 'vue-select'
import BlogCreateRequest from '@/libs/Api/Blog/BlogCreateRequest'
import BlogCategoryGetAllRequest from '@/libs/Api/Blog/BlogCategoryGetAllRequest'
import ToastificationContent from '@core/components/toastification/ToastificationContent'

export default {
  components: {
    BRow,
    BCol,
    BFormFile,
    BFormTextarea,
    BFormGroup,
    BFormInput,
    BFormCheckbox,
    BForm,
    vSelect,
    BButton,
  },
  data() {
    return {
      // Blog Tag type === 2
      SelectedCategoryId: 0,
      Categories: [],
      BaseImgUrl: '',
      images: '',
      adminId: 0,
      BlogDescription: '',
      BlogSummary: '',
      NewTag:'',
      BlogTitle: '',
      newTags:[]
    }
  },
  title: ' ساخت مقاله جدید  - پنل ادمین بانو کلاب',
  mounted() {
    let admin = JSON.parse(localStorage.getItem('userData'))
    this.adminId = admin.data.user.userId
    this.GetAllBlogCategory()
  },
  methods: {
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
    async GetAllBlogCategory() {
      let _this = this
      let blogCategoryGetAllRequest = new BlogCategoryGetAllRequest(_this)

      await blogCategoryGetAllRequest.fetch(function (content) {
        _this.Categories = content

      }, function (error) {
        console.log(error)
      })
    },
    async CreateBlog() {
      let _this = this
      if(_this.BlogDescription===''){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات ناموفق`,
            icon: 'AlertCircleIcon',
            variant: 'danger',
            text: `توضیحات مقاله را وارد کنید`,
          },
        })
      }else if(_this.BlogTitle===''){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات ناموفق`,
            icon: 'AlertCircleIcon',
            variant: 'danger',
            text: `نام مقاله را وارد کنید`,
          },
        })
      }else if(_this.SelectedCategoryId===0){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات ناموفق`,
            icon: 'AlertCircleIcon',
            variant: 'danger',
            text: `دسته بندی مقاله را وارد کنید`,
          },
        })
      }else{
        let blogCreateRequest = new BlogCreateRequest(_this)
        let data = {
          description: this.BlogDescription,
          summary: this.BlogSummary,
          createDate: new Date(Date.now()),
          title: this.BlogTitle,
          blogCategoryId: this.SelectedCategoryId,
          // userId: this.adminId,
          fileData: this.images,
          tags: this.newTags
        }
        blogCreateRequest.setParams(data)
        await blogCreateRequest.fetch(() => {
          this.$router.push({
            path:'/apps/Blog/BlogList'
          })
          _this.$toast({
            component: ToastificationContent,
            position: 'bottom-center',
            props: {
              title: `عملیات موفق`,
              icon: 'CheckIcon',
              variant: 'success',
              text: `مقاله اضافه شد`,
            },
          })
        }, () => {

        })
      }



    },
    HandleFile() {

      const that = this
      let f = ''

      f = this.$refs.File.files[0]
      this.BaseImgUrl = URL.createObjectURL(f)
      const reader = new FileReader()
      reader.onload = (function (theFile) {
        return function () {
          const binaryData = reader.result
          that.images = window.btoa(binaryData)
        }
      })(f)
      reader.readAsBinaryString(f)

      console.log(this.images)
    }
  },
  directives: {
    Ripple,
  },
}
</script>

