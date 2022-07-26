<template>
  <div v-if="category !== null">
    <b-card>

      <b-form>
        <b-row>

          <b-col
              cols="12"
              class="mb-1"
          >
            <h3>نام دسته بندی</h3>
          </b-col>

          <!-- Field: name -->
          <b-col
              cols="12"
              md="4"
          >
            <b-form-group
                label-for="name"
            >
              <b-form-input
                  id="name"
                  v-model="category.name"
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
          @click="updateCategory"
      >
        ذخیره تغییرات
      </b-button>

    </b-card>

  </div>
</template>

<script>
import {
  BCard, BRow, BCol, BFormInput, BButton, BTable, BMedia, BAvatar, BLink,
  BBadge, BDropdown, BDropdownItem, BPagination, BFormGroup, BForm,BModal,
} from 'bootstrap-vue'
import vSelect from "vue-select";
import ToastificationContent from "@core/components/toastification/ToastificationContent";
import BlogCategoryUpdateRequest from "@/libs/Api/Blog/BlogCategoryUpdateRequest";
import BlogCategoryGetRequest from "@/libs/Api/Blog/BlogCategoryGetRequest";


export default {
  name: "categories-edit",
  title:"ویرایش دسته بندی بلاگ - پنل ادمین بانو کلاب",
  data(){
    return{
      category:null,
      categoryProperties:null,
      categoryId:this.$route.params.id,
      addNewCategoryProperty:false,
      myTableColumns : [
        { key: 'categoryPropertyId',label:'شناسه'},
        { key: 'name',label:'نام'},
        { key: 'delete',label:'حذف'},
        { key: 'edit',label:'ویرایش'},
      ],
      defaultCreateCategoryPropertyData:{
        isDeleted: false,
        categoryPropertyId: 0,
        categoryId: this.$route.params.id,
        name: ""
      },
      updateCategoryPropertyData:null,
    }
  },
  async created(){
    await this.getCategory();
  },
  methods:{
    async getCategory(){
      let _this = this;

      let blogCategoryGetRequest = new BlogCategoryGetRequest(_this);
      blogCategoryGetRequest.setId(_this.categoryId);
      await blogCategoryGetRequest.fetch(function (content){
        _this.category = content
      },function (error){
        console.log(error)
      })
    },
    async updateCategory(){
      let _this = this;

      let blogCategoryUpdateRequest = new BlogCategoryUpdateRequest(_this);
      blogCategoryUpdateRequest.setParams(_this.category);
      await blogCategoryUpdateRequest.fetch(function (content){
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
        _this.getCategory()
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

    vSelect,
  }
}
</script>

<style scoped>

</style>
