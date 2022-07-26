<template>
 <div>
   <b-card class="blog-edit-wrapper">

     <!-- form -->
     <b-form class="mt-2">
       <b-row>
         <b-col md="6">
           <b-form-group
               label="عنوان"
               label-for="blog-edit-title"
               class="mb-2"
           >
             <b-form-input
                 id="blog-edit-title"
                 v-model="blog.title"
             />
           </b-form-group>
         </b-col>
         <b-col md="6">
           <b-form-group
               label="دسته بندی"
               label-for="category"
               class="mb-2"
           >
             <v-select
                 v-if="blogCategories !== null"
                 id="category"
                 v-model="blog.blogCategoryId"
                 :options="blogCategories"
                 label="name"
                 :reduce="name => name.blogCategoryId"
                 :clearable="false"
             />
           </b-form-group>
         </b-col>
         <!-- Field:  description -->
         <b-col
             cols="12"
         >
           <b-form-group
               label="خلاصه"
               label-for="summary"
           >
             <b-form-textarea
                 id="summary"
                 v-model="blog.summary"
                 rows="2"
                 placeholder="خلاصه"
             />
           </b-form-group>
         </b-col>
         <b-col cols="12">
           <b-form-group
               label="توضیحات"
               label-for="blog-content"
               class="mb-2"
           >
             <b-form-textarea
                 id="blog-content"
                 rows="4"
                 v-model="blog.description"
             ></b-form-textarea>
           </b-form-group>
         </b-col>
         <b-col
             cols="12"
             class="mb-2"
         >
           <div class="border rounded p-2">
             <h4 class="mb-1">
               پیوست عکس
             </h4>
             <b-media
                 no-body
                 vertical-align="center"
                 class="flex-column flex-md-row"
             >
               <b-media-aside>
                 <b-img
                     ref="refPreviewEl"
                     :src="base64ImageSrc"
                     height="110"
                     width="170"
                     class="rounded mr-2 mb-1 mb-md-0"
                 />
               </b-media-aside>
               <b-media-body>
                 <div class="d-inline-block">
                   <b-form-file
                       ref="refInputEl"
                       accept=".jpg, .png, .gif"
                       placeholder="انتخاب عکس"
                       @input="makeBase64Pic"
                   />
                 </div>
               </b-media-body>
             </b-media>
           </div>
         </b-col>
         <b-col
             cols="12"
             class="mt-50"
         >
           <b-button
               v-ripple.400="'rgba(255, 255, 255, 0.15)'"
               variant="primary"
               class="mr-1"
               @click="createBlog"
           >
             ثبت
           </b-button>
         </b-col>
       </b-row>
     </b-form>
     <!--/ form -->

   </b-card>

 </div>
</template>

<script>
import {
  BCard, BMedia, BAvatar, BCardText, BMediaAside, BMediaBody, BForm, BRow, BCol, BFormGroup, BFormInput, BImg, BFormFile, BLink, BButton,BFormTextarea,BOverlay,
} from 'bootstrap-vue'
import vSelect from 'vue-select'
import { quillEditor } from 'vue-quill-editor'
import BlogCategoryGetAllRequest from "@/libs/Api/Blog/BlogCategoryGetAllRequest";
import Ripple from "vue-ripple-directive";
import BlogCreateRequest from "@/libs/Api/Blog/BlogCreateRequest";
import ToastificationContent from "@core/components/toastification/ToastificationContent";

export default {
  name: "BlogCreate",
  title:"افزودن بلاگ - پنل ادمین بانو کلاب",
  data(){
    return{
      blogCategories:null,
      base64ImageSrc:null,
      snowOption: {
        theme: 'snow',
      },
      blog:{
        isDeleted: false,
        blogId: 0,
        description: "",
        summary: "",
        title: "",
        blogCategoryId: 0,
        fileData: "",
        tagIds:[],
        createDate:this.currentDate,
      }
    }
  },
  computed:{
    currentDate(){
      let today = new Date();
      let date = today.getFullYear()+'-'+(today.getMonth()+1)+'-'+today.getDate();
      let time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
      return  date+' '+time;
    }
  },
  async created() {
    await this.getBlogCategories();
  },
  methods:{
    async getBlogCategories(){
      let _this = this;

      let blogCategoryGetAllRequest = new BlogCategoryGetAllRequest(_this);
      await blogCategoryGetAllRequest.fetch(function (content){
        _this.blogCategories = content
      } , function (content){
        console.log(content)
      });
    },
    async createBlog(){
      let _this = this;

      let blogCreateRequest = new BlogCreateRequest(_this);
      blogCreateRequest.setParams(_this.blog);
      await blogCreateRequest.fetch(function (content){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `بلاگ ساخته شد.`,
          },
        })
        window.location.reload ()
      },function (error){
        console.log(error)
      })
    },
    async readAsDataURL(file) {
      return new Promise((resolve, reject) => {
        const fr = new FileReader();
        fr.onerror = reject;
        fr.onload = () => {
          resolve(fr.result.split(",")[1]);
        }
        fr.readAsDataURL(file);
      });
    },
    async makeBase64Pic(){
      const _this = this;
      let file = _this.$refs.refInputEl.files[0]
      _this.base64ImageSrc = URL.createObjectURL(file)
      const result = await _this.readAsDataURL(file);
      _this.blog.fileData = result;
    },
  },
  components: {
    BCard,
    BMedia,
    BAvatar,
    BCardText,
    BMediaAside,
    BMediaBody,
    BForm,
    BLink,
    BImg,
    BRow,
    BCol,
    BButton,
    BFormGroup,
    BFormInput,
    BFormFile,
    BFormTextarea,
    BOverlay,
    vSelect,
    quillEditor,
  },
  directives: {
    Ripple,
  },
}
</script>

<style lang="scss">
@import '@core/scss/vue/libs/vue-select.scss';
@import '@core/scss/vue/libs/quill.scss';
@import '@core/scss/vue/pages/page-blog.scss';
</style>
