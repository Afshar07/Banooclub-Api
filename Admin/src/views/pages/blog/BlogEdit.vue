<template>
  <b-overlay
      :show="blog === null"
      rounded="sm"
  >
    <b-card
    v-if="Object.keys(blogEdit).length"
    class="blog-edit-wrapper"
  >

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
              v-model="blog.description"
              rows="4"
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
            @click="updateBlog"
          >
            ذخیره تغییرات
          </b-button>
        </b-col>
      </b-row>
    </b-form>
    <!--/ form -->
  </b-card>
  </b-overlay>
</template>

<script>
import {
  BCard, BMedia, BAvatar, BCardText, BMediaAside, BMediaBody, BForm, BRow, BCol, BFormGroup, BFormInput, BImg, BFormFile, BLink, BButton,BFormTextarea,BOverlay,
} from 'bootstrap-vue'
import vSelect from 'vue-select'
import { quillEditor } from 'vue-quill-editor'
import Ripple from 'vue-ripple-directive'
import BlogGetByIdRequest from "@/libs/Api/Blog/BlogGetByIdRequest";
import BlogCategoryGetAllRequest from "@/libs/Api/Blog/BlogCategoryGetAllRequest";
import BlogUpdateBlogRequest from "@/libs/Api/Blog/BlogUpdateBlogRequest";
import ToastificationContent from "@core/components/toastification/ToastificationContent";

export default {
  title:"ویرایش بلاگ - پنل ادمین بانو کلاب",
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
  data() {
    return {
      blog:null,
      blogId:this.$route.params.id,
      blogCategories:null,
      base64ImageSrc:null,
      blogEdit: {},
      blogFile: null,
      snowOption: {
        theme: 'snow',
      },
    }
  },
  async created() {
    await this.getBlog();
    await this.getBlogCategories();
    this.$http.get('/blog/list/data/edit').then(res => { this.blogEdit = res.data })
  },
  methods:{
    async getBlog(){
      let _this = this;

      let blogGetByIdRequest = new BlogGetByIdRequest(_this);
      blogGetByIdRequest.setId(_this.blogId)
      await blogGetByIdRequest.fetch(function (content){
        _this.blog = content;
        _this.base64ImageSrc = 'https://api.halamkhoobee.com/'+_this.blog.fileData;
        _this.blog.fileData= '';
      } , function (){
        console.log('Error')
      });
    } ,
    async getBlogCategories(){
      let _this = this;

      let blogCategoryGetAllRequest = new BlogCategoryGetAllRequest(_this);
      await blogCategoryGetAllRequest.fetch(function (content){
        _this.blogCategories = content
      } , function (content){
        console.log(content)
      });
    },
    async updateBlog(){
      let _this = this;

      let blogUpdateBlogRequest = new BlogUpdateBlogRequest(_this);
      blogUpdateBlogRequest.setParams(_this.blog);
      await blogUpdateBlogRequest.fetch(function (content){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `تغییرات انجام شد.`,
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
}
</script>

<style lang="scss">
@import '@core/scss/vue/libs/vue-select.scss';
@import '@core/scss/vue/libs/quill.scss';
@import '@core/scss/vue/pages/page-blog.scss';
</style>
