<template>
  <b-form @submit.prevent="UpdateBlog">
    <b-row>
      <b-modal
          id="modal-edit"
          centered
          ok-title="بروزرسانی"
          ok-only

          @ok="UpdateTag"
      >
        <b-form-group>
          <label for="email">نام برچسب</label>
          <b-form-input
              v-if="SelectedTag!==null"
              id="email"
              type="text"
              placeholder="نام دسته بندی"
              v-model="SelectedTag.title"
          />

        </b-form-group>

      </b-modal>
      <b-col md="6">
        <b-form-group
            label="نام مقاله"
            label-for="mc-first-name"
        >
          <b-form-input
              v-model="BlogData.title"
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
              v-model="BlogData.summary"
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
              v-model="BlogData.blogCategoryId"
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
          <div>
            <img v-if="!BlogData.fileData.includes('blob')" :src="`https://banooclubapi.oregonserver.com/${BlogData.fileData}`" width="100px" height="100px" alt="">
            <img v-else :src="`${BlogData.fileData}`" width="100px" height="100px" alt="">
          </div>

      </b-col>
      <b-col md="6">
        <b-form-group
            label="برچسب مقاله"
            label-for="mc-last-name"
        >
          <b-form-input
              v-model="NewTag"
              id="mc-Tag-name"
              placeholder="نام برچسب "
              @keydown.enter.prevent="AddNewTag"
          />
        </b-form-group>
        <div class="d-flex align-items-center gap-3 ">
          <div v-for="(item,idx) in BlogData.tags"  :key="idx" class="d-flex align-items-center gap-2">

            <span    class="badge d-flex align-items-center mx-2 px-1 py-1  cursor-pointer pill bg-primary text-white" >
               <feather-icon icon="MinusIcon" size="20" class="text-white mr-1" @click.prevent="RemoveTag(item)"></feather-icon>
              <span v-b-modal.modal-edit @click.prevent="SetSelectedTag(item)" >              {{item.title}}</span>

            </span>
          </div>

        </div>
      </b-col>
      <b-col class="mt-2" md="12">
        <b-form-group
            label-for="Email"
            label="توضیحات مقاله"
        >
          <div class="form-label-group">
            <b-form-textarea
                id="mc-email"
                v-model="BlogData.description"
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
import BlogUpdateRequest from '@/libs/Api/Blog/BlogUpdateBlogRequest';
import BlogGetByIdRequest from '@/libs/Api/Blog/BlogGetByIdRequest';
import BlogCategoryGetAllRequest from '@/libs/Api/Blog/BlogCategoryGetAllRequest';
import BlogCreateTagRequest from '@/libs/Api/Blog/BlogCreateTagRequest';
import BlogUpdateTagRequest from '@/libs/Api/Blog/BlogUpdateTagRequest';
import BlogDeleteTagRequest from '@/libs/Api/Blog/BlogDeleteTagRequest';
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
      BlogData:null,
      SelectedTag:null,
      SelectedCategoryId: 0,
      Categories: [],
      BaseImgUrl: '',
      images: '',
      adminId: 0,
      BlogDescription: '',
      BlogSummary: '',
      BlogTitle: '',
      NewTag:''
    }
  },
  title: 'ویرایش مقاله  - پنل ادمین بانو کلاب',
  mounted() {
    let admin = JSON.parse(localStorage.getItem('userData'))
    this.adminId = admin.data.user.userId
    this.GetAllBlogCategory();
    this.GetBlog();
  },
  methods: {
   async AddNewTag(){
     let _this = this
     if(this.NewTag === ''){
       _this.$toast({
         component: ToastificationContent,
         position: 'bottom-center',
         props: {
           title: `عملیات نا موفق`,
           icon: 'AlertCircleIcon',
           variant: 'danger',
           text: `لطفا نام برچسب را وارد کنید`,
         },
       })
     }else{

       let blogCreateTagRequest = new BlogCreateTagRequest(_this)
       let data = {
         type: 4,
         objectId: parseInt(this.$route.params.id) ,
         title: this.NewTag,
         createDate: new Date(Date.now())

       }
       blogCreateTagRequest.setParams(data)
       await blogCreateTagRequest.fetch(()=>{
         _this.$toast({
           component: ToastificationContent,
           position: 'bottom-center',
           props: {
             title: `عملیات موفق`,
             icon: 'CheckIcon',
             variant: 'success',
             text: `برچسب جدید به مقاله اضافه شد.`,
           },
         })
         _this.GetBlog();
         _this.NewTag = ''
       },()=>{})

     }

    },
  async RemoveTag(item){
     let _this = this
     let blogDeleteTagRequest = new BlogDeleteTagRequest(_this)
     let data = {
       type: 4,
       objectId: parseInt(this.$route.params.id) ,
       title: this.NewTag,
       createDate: new Date(Date.now())

     }
     blogDeleteTagRequest.setId(item.tagId)
     await blogDeleteTagRequest.fetch(()=>{
       _this.$toast({
         component: ToastificationContent,
         position: 'bottom-center',
         props: {
           title: `عملیات موفق`,
           icon: 'CheckIcon',
           variant: 'success',
           text: `برچسب حذف  شد.`,
         },
       })
       _this.GetBlog();

     },()=>{})

   },
  async UpdateTag(){
    let _this = this
    let blogUpdateTagRequest = new BlogUpdateTagRequest(_this)
    let data = {
      tagId: this.SelectedTag.tagId,
      type: 4,
      objectId: this.SelectedTag.objectId,
      title: this.SelectedTag.title,
      createDate: new Date(Date.now())
    }
    blogUpdateTagRequest.setData(data)
    await blogUpdateTagRequest.fetch(()=>{
      _this.$toast({
        component: ToastificationContent,
        position: 'bottom-center',
        props: {
          title: `عملیات موفق`,
          icon: 'CheckIcon',
          variant: 'success',
          text: `برچسب بروزرسانی شد.`,
        },
      })
      _this.GetBlog();
      _this.SelectedTag = null
    },()=>{})




  },
    SetSelectedTag(item){
     this.SelectedTag = item
    },
   async GetBlog(){
     let _this = this
     let blogGetByIdRequest = new BlogGetByIdRequest(_this)
      blogGetByIdRequest.setId(this.$route.params.id)
     await blogGetByIdRequest.fetch(function (content) {
       _this.BlogData = content

     }, function (error) {
       console.log(error)
     })
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
    async UpdateBlog() {
      let _this = this
      let blogUpdateRequest = new BlogUpdateRequest(_this)
      let data = {
        blogId:this.BlogData.blogId,
        description: this.BlogData.description,
        summary: this.BlogData.summary,
        createDate: new Date(Date.now()),
        title: this.BlogData.title,
        blogCategoryId: this.BlogData.blogCategoryId,
        userId: this.adminId,
        fileData: this.images,
        seoTitle: this.BlogData.seoTitle,
        seoDescription: this.BlogData.seoDescription,
        seourl: this.BlogData.seourl,
      }
      blogUpdateRequest.setParams(data)
      await blogUpdateRequest.fetch(() => {
        this.$router.push({
          path:'/apps/Blog/BlogList'
        })
      }, () => {

      })

    },
    HandleFile() {

      const that = this
      let f = ''

      f = this.$refs.File.files[0]
      this.BlogData.fileData = URL.createObjectURL(f)
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

