<template>
  <div v-if="comment !== null">
    <b-card>

      <b-form>
        <b-row>

          <b-col
              cols="12"
              class="mb-1"
          >
            <h3>متن نظر</h3>
          </b-col>

          <!-- Field: name -->
          <b-col
              cols="12"
          >
            <b-form-group
                label-for="name"
            >
              <b-form-textarea
                  id="name"
                  rows="2"
                  v-model="comment.message"
              />
            </b-form-group>
          </b-col>
        </b-row>
      </b-form>

      <!-- Action Buttons -->
      <b-button
          variant="primary"
          class="mt-1 mb-sm-0 mr-0 mr-sm-1"
          :block="$store.getters['app/currentBreakPoint'] === 'xs'"
          @click="updateComment"
      >
        ذخیره تغییرات
      </b-button>

    </b-card>

  </div>
</template>

<script>
import {
  BCard, BRow, BCol, BFormInput, BButton, BTable, BMedia, BAvatar, BLink,
  BBadge, BDropdown, BDropdownItem, BPagination, BFormGroup, BForm,BModal,BFormTextarea,
} from 'bootstrap-vue'
import vSelect from "vue-select";
import ToastificationContent from "@core/components/toastification/ToastificationContent";
import BlogCommentGetByIdRequest from "@/libs/Api/Blog/BlogCommentGetByIdRequest";
import BlogCommentUpdateRequest from "@/libs/Api/Blog/BlogCommentUpdateRequest";


export default {
  name: "BlogCommentEdit",
  title:"ویرایش نظر بلاگ - پنل ادمین بانو کلاب",
  data(){
    return{
      comment:null,
      commentId:this.$route.params.id,
      defaultCreateCategoryPropertyData:{
        isDeleted: false,
        categoryPropertyId: 0,
        categoryId: this.$route.params.id,
        name: ""
      },
    }
  },
  async created(){
    await this.getComment();
  },
  methods:{
    async getComment(){
      let _this = this;

      let blogCommentGetByIdRequest = new BlogCommentGetByIdRequest(_this);
      blogCommentGetByIdRequest.setId(_this.commentId);
      await blogCommentGetByIdRequest.fetch(function (content){
        _this.comment = content
      },function (error){
        console.log(error)
      })
    },
    async updateComment(){
      let _this = this;

      let blogCommentUpdateRequest = new BlogCommentUpdateRequest(_this);
      blogCommentUpdateRequest.setParams(_this.comment);
      await blogCommentUpdateRequest.fetch(function (content){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `تغییرات اعمال شد.`,
          },
        })
        _this.getComment()
      },function (error){
        console.log(error)
      })
    },
  },
  components:{
    BCard,
    BRow,
    BCol,
    BFormInput,
    BButton,
    BTable,
    BMedia,
    BAvatar,
    BLink,
    BFormGroup,
    BBadge,
    BForm,
    BDropdown,
    BDropdownItem,
    BPagination,
    BModal,
    BFormTextarea,

    vSelect,
  }
}
</script>

<style scoped>

</style>
