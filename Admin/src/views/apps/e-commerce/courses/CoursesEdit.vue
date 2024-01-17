<template>
  <div v-if="course !== null">
    <b-row>
      <b-col cols="12">
        <b-card>

          <b-form>
            <b-row>

              <!-- Field:  name -->
              <b-col
                  cols="12"
                  md="6"
              >
                <b-form-group
                    label="عنوان"
                    label-for="name"
                >
                  <b-form-input
                      id="name"
                      v-model="course.name"
                  />
                </b-form-group>
              </b-col>

              <!-- Field: isOnline -->
              <b-col
                  cols="12"
                  md="6"
              >
                <b-form-group
                    label="نوع استریم"
                    label-for="isOnline"
                >
                  <v-select
                      v-model="course.isOnline"
                      :options="isOnlineOptions"
                      :reduce="val => val.value"
                      :clearable="false"
                      input-id="user-status"
                  />
                </b-form-group>
              </b-col>

              <!-- Field:  sessionsCount -->
              <b-col
                  v-if="!course.isOnline"
                  cols="12"
                  md="6"
              >
                <b-form-group
                    label="تعداد جلسات"
                    label-for="sessionsCount"
                >
                  <b-form-input
                      id="sessionsCount"
                      v-model="course.sessionsCount"
                  />
                </b-form-group>
              </b-col>

              <!-- Field:  summary -->
              <b-col
                  cols="12"
              >
                <b-form-group
                    label="خلاصه"
                    label-for="summary"
                >
                  <b-form-textarea
                      id="summary"
                      v-model="course.summary"
                      rows="2"
                  />
                </b-form-group>
              </b-col>

              <!-- Field:  description -->
              <b-col
                  cols="12"
              >
                <b-form-group
                    label="توضیحات"
                    label-for="description"
                >
                  <b-form-textarea
                      id="description"
                      v-model="course.description"
                      rows="6"
                  />
                </b-form-group>
              </b-col>

            </b-row>
          </b-form>

          <!-- Action Buttons -->
          <b-button
              v-if="course.isOnline"
              variant="warning"
              class="mt-1 mb-sm-0 mr-0 mr-sm-1 col-12 col-md-2"
              :block="$store.getters['app/currentBreakPoint'] === 'xs'"
              @click="addSchedule"
          >
            افزودن زمانبندی
          </b-button>
          <b-button
              variant="primary"
              class="mt-1 mb-sm-0 mr-0 mr-sm-1 col-12 col-md-2"
              :block="$store.getters['app/currentBreakPoint'] === 'xs'"
              @click="updateCourse"
              :disabled="course.isOnline === true && schedules.length === 0 "
          >
            ثبت
          </b-button>

        </b-card>

        <b-alert
            :show="course.isOnline === true && schedules.length === 0 "
            variant="danger"
            class=" col-12"
        >
          <div class="alert-body mt-2">
            <feather-icon
                class="mr-25"
                icon="InfoIcon"
            />
            <span class="ml-25">در صورت آنلاین بودن دوره باید زمانبندی اضافه کنید.</span>
          </div>
        </b-alert>

        <b-card v-if="course.isOnline"
            no-body
            class="mb-0 px-1"
        >
          <h4 class="my-1">زمانبندی ها</h4>

          <b-table
              ref="refUserListTable"
              class="position-relative"
              :items="schedules"
              responsive
              :fields="myTableColumns"
              primary-key="id"
              show-empty
              bordered
              striped
              empty-text="موردی موجود نیست!"
          >

            <!-- Column: startTime -->

            <template #cell(startTime)="data">

             <span>
               {{data.item.startTime.slice(11,16)}}
                  -
               {{createJalaliDate(data.item.startTime.slice(0,10))}}
             </span>

            </template>

            <!-- Column: endTime -->

            <template #cell(endTime)="data">

             <span>
               {{data.item.endTime.slice(11,16)}}
                  -
               {{createJalaliDate(data.item.endTime.slice(0,10))}}
             </span>

            </template>

            <!-- Column: delete -->

            <template #cell(delete)="data">

              <div class="cursor-pointer d-flex flex-row "
                   v-b-modal.modal-delete
                   @click="showModal(data.item.courseScheduleId)"
              >
                <feather-icon icon="TrashIcon" class="text-danger" size="20"/>
              </div>

            </template>

            <template #cell(edit)="data">

              <div v-b-modal.modal-center @click="setUpdateScheduleData(data.item)" class="cursor-pointer d-flex flex-row ">
                <feather-icon icon="EditIcon" size="20"/>
              </div>

            </template>

          </b-table>
        </b-card>

        <b-card v-if="course.isOnline" class="my-2">
          <b-row>

            <!-- Field:  startTime -->
            <b-col
                cols="12"
                md="5"
            >
              <b-form-group
                  label="تاریخ و زمان شروع"
                  label-for=""
              >
                <input
                    type="text"
                    :id="`start${1}`"
                    placeholder="شروع"
                    v-model.trim="newSchedule.startTime"
                    class="form-control form-control-lg w-11/12 2xl:h-12 h-10 mx-auto mt-1 mb-1 text-sm block w-full rounded-full text-center bg-gray-100 border-transparent focus:border-gray-500 focus:bg-white focus:ring-0 placeholder-inputPlaceHolder text-gray-700"
                />
                <custom-date-picker
                    v-model="newSchedule.startTime"
                    auto-submit
                    format="YYYY-MM-DD HH:mm"
                    display-format="jYYYY/jMM/jDD HH:mm"
                    class=""
                    :element="`start${1}`"
                    type="datetime"
                    color="#5c6bc0"
                    :min="today"
                ></custom-date-picker>
              </b-form-group>
            </b-col>

            <!-- Field:  endTime -->
            <b-col
                cols="12"
                md="5"
            >
              <b-form-group
                  label="تاریخ و زمان پایان"
                  label-for=""
              >
                <input
                    type="text"
                    :id="`end${2}`"
                    placeholder="پایان"
                    v-model.trim="newSchedule.endTime"
                    class="form-control form-control-lg w-11/12 2xl:h-12 h-10 mx-auto mt-1 mb-1 text-sm block w-full rounded-full text-center bg-gray-100 border-transparent focus:border-gray-500 focus:bg-white focus:ring-0 placeholder-inputPlaceHolder text-gray-700"
                />
                <custom-date-picker
                    v-model="newSchedule.endTime"
                    auto-submit
                    format="YYYY-MM-DD HH:mm"
                    display-format="jYYYY/jMM/jDD HH:mm"
                    class=""
                    :element="`end${2}`"
                    type="datetime"
                    color="#5c6bc0"
                    :min="today"
                ></custom-date-picker>
              </b-form-group>
            </b-col>

            <b-col
                cols="12"
                md="2"
            >
              <b-button
                  v-if="course.isOnline"
                  variant="warning"
                  class="mt-3 mb-sm-0 mr-0 mr-sm-1 py-1 col-12"
                  :block="$store.getters['app/currentBreakPoint'] === 'xs'"
                  @click="addSchedule"
              >
                افزودن زمانبندی
              </b-button>
            </b-col>

          </b-row>
        </b-card>
        <!-- modal vertical center -->
        <b-modal
            v-if="updateScheduleData !== null"
            id="modal-center"
            centered
            title="ویرایش خصوصیت"
            ok-only
            ok-title="ثبت"
            @ok="updateSchedule"

        >
          <b-form-group
              label="تاریخ و زمان شروع"
              label-for=""
          >
            <input
                type="text"
                :id="`start${10}`"
                placeholder="شروع"
                v-model.trim="updateScheduleData.startTime"
                class="form-control form-control-lg w-11/12 2xl:h-12 h-10 mx-auto mt-1 mb-1 text-sm block w-full rounded-full text-center bg-gray-100 border-transparent focus:border-gray-500 focus:bg-white focus:ring-0 placeholder-inputPlaceHolder text-gray-700"
            />
            <custom-date-picker
                v-model="updateScheduleData.startTime"
                auto-submit
                format="YYYY-MM-DD HH:mm"
                display-format="jYYYY/jMM/jDD HH:mm"
                class=""
                :element="`start${10}`"
                type="datetime"
                color="#5c6bc0"
                :min="today"
            ></custom-date-picker>
          </b-form-group>

          <b-form-group
              label="تاریخ و زمان پایان"
              label-for=""
          >
            <input
                type="text"
                :id="`end${20}`"
                placeholder="پایان"
                v-model.trim="updateScheduleData.endTime"
                class="form-control form-control-lg w-11/12 2xl:h-12 h-10 mx-auto mt-1 mb-1 text-sm block w-full rounded-full text-center bg-gray-100 border-transparent focus:border-gray-500 focus:bg-white focus:ring-0 placeholder-inputPlaceHolder text-gray-700"
            />
            <custom-date-picker
                v-model="updateScheduleData.endTime"
                auto-submit
                format="YYYY-MM-DD HH:mm"
                display-format="jYYYY/jMM/jDD HH:mm"
                class=""
                :element="`end${20}`"
                type="datetime"
                color="#5c6bc0"
                :min="today"
            ></custom-date-picker>
          </b-form-group>
        </b-modal>
        <b-modal
            id="modal-delete"
            centered
            ok-title="حذف"
            cancelTitle="انصراف"
            @ok="deleteSchedule(deleteItem)"
        >
          <span>حذف شود؟</span>
        </b-modal>
      </b-col>
    </b-row>
  </div>
</template>

<script>
import {
  BAlert, BAvatar,
  BButton,
  BCard,
  BCardBody,
  BCardText,
  BCol,
  BDropdown,
  BDropdownItem, BForm, BFormGroup, BFormInput, BFormTextarea,BTable,
  BImg,
  BLink, BMedia,
  BRow, BTab, BTabs, BModal
} from "bootstrap-vue";
import vSelect from "vue-select";
import ProductCourseCreateRequest from "@/libs/Api/Product/ProductCourseCreateRequest";
import ToastificationContent from "@core/components/toastification/ToastificationContent";
import ProductCourseGetByIdRequest from "@/libs/Api/Product/ProductCourseGetByIdRequest";
import ProductCourseUpdateRequest from "@/libs/Api/Product/ProductCourseUpdateRequest";
import ProductCourseScheduleDeleteRequest from "@/libs/Api/Product/ProductCourseScheduleDeleteRequest";
import ProductCourseScheduleCreateRequest from "@/libs/Api/Product/ProductCourseScheduleCreateRequest";
import ProductCategoryPropertyUpdateRequest from "@/libs/Api/Product/ProductCategoryPropertyUpdateRequest";
import ProductCourseScheduleUpdateRequest from "@/libs/Api/Product/ProductCourseScheduleUpdateRequest";

export default {
  title:"ویرایش دوره - پنل ادمین بانو کلاب",
  name: "CoursesEdit",
  components:{
    BCard,
    BCardBody,
    BRow,
    BCol,
    BImg,
    BCardText,
    BLink,
    BButton,
    BDropdown,
    BDropdownItem,
    BAlert,
    BTab,
    BTabs,
    BForm,
    BFormGroup,
    BFormInput,
    BFormTextarea,
    BMedia,
    BAvatar,
    BTable,
    BModal,
    vSelect,
  },
  computed:{
    today(){
      // let options = { year: 'numeric', month: '2-digit', day: '2-digit' };
      // let test = new Date().toLocaleDateString('fa-IR',options);
      // return  test.replace(/([۰-۹])/g, token => String.fromCharCode(token.charCodeAt(0) - 1728));
      return new Date()
    }
  },
  data(){
    return{
      courseId:this.$route.params.id,
      course:null,
      deleteItem:null,
      schedules:null,
      myTableColumns : [
        { key: 'courseScheduleId',label:'شناسه'},
        { key: 'startTime',label:'شروع'},
        { key: 'endTime',label:'پایان'},
        { key: 'delete',label:'حذف'},
        { key: 'edit',label:'ویرایش'},
      ],
      newSchedule:{
        isDeleted: false,
        courseScheduleId: 0,
        courseId:this.$route.params.id,
        startTime: "",
        endTime: "",
      },
      updateScheduleData:null,
      isOnlineOptions:[
        {label : 'آنلاین' , value : true},
        {label : 'آفلاین' , value : false}
      ],
    }
  },
  async created(){
    await this.getCourse();
  },
  methods:{
    async getCourse(){
      let _this = this;

      let productCourseGetByIdRequest = new ProductCourseGetByIdRequest(_this);
      productCourseGetByIdRequest.setId(_this.courseId);
      await productCourseGetByIdRequest.fetch(function (content){
        _this.course = content.data.course;
        _this.schedules = content.data.schedules;
      },function (error){
        console.log(error)
      })
    },
    async updateCourse(){
      let _this = this;

      let productCourseUpdateRequest = new ProductCourseUpdateRequest(_this);
      productCourseUpdateRequest.setParams(_this.course);
      await productCourseUpdateRequest.fetch(function (content){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `عملیات انجام شد.`,
          },
        })
        window.location.reload();
      },function (error){
        console.log(error)
      })
    },
    async deleteSchedule(param){
      let _this = this;

      let productCourseScheduleDeleteRequest = new ProductCourseScheduleDeleteRequest(_this);
      productCourseScheduleDeleteRequest.setId(param);
      await productCourseScheduleDeleteRequest.fetch(function (content){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `عملیات انجام شد.`,
          },
        })
        window.location.reload();
      },function (error){
        console.log(error)
      })
    },
    createJalaliDate(param){
      return new Date(param).toLocaleDateString("fa-IR",);
    },
    // addSchedule(){
    //   let _this = this;
    //
    //   _this.defaultData.schedules.push({
    //     startTime:'',
    //     endTime:''
    //   })
    // },
    async addSchedule(){
      let _this = this;

      let productCourseScheduleCreateRequest = new ProductCourseScheduleCreateRequest(_this);
      productCourseScheduleCreateRequest.setParams(_this.newSchedule);
      await productCourseScheduleCreateRequest.fetch(function (content){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `عملیات انجام شد.`,
          },
        })
        window.location.reload();
      },function (error){
        console.log(error)
      })
    },
    async updateSchedule(){
      let _this = this;

      let productCourseScheduleUpdateRequest = new ProductCourseScheduleUpdateRequest(_this);
      productCourseScheduleUpdateRequest.setParams(_this.updateScheduleData);
      await productCourseScheduleUpdateRequest.fetch(function (content){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `دسته بندی جدید اضافه شد.`,
          },
        })
        window.location.reload();
      },function (error){
        console.log(error)
      })
    },
    setUpdateScheduleData(param){
      this.updateScheduleData = param
    },
    showModal(param){
      let _this = this;
      _this.deleteItem = param
    },
  },
}
</script>

<style scoped>

</style>
