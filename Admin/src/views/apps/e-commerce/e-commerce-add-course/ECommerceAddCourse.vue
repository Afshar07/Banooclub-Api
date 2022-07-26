<template>
  <div>
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
                      v-model="defaultData.name"
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
                      v-model="defaultData.isOnline"
                      :options="isOnlineOptions"
                      :reduce="val => val.value"
                      :clearable="false"
                      input-id="user-status"
                  />
                </b-form-group>
              </b-col>

              <!-- Field:  sessionsCount -->
              <b-col
                  v-if="!defaultData.isOnline"
                  cols="12"
                  md="6"
              >
                <b-form-group
                    label="تعداد جلسات"
                    label-for="sessionsCount"
                >
                  <b-form-input
                      id="sessionsCount"
                      v-model="defaultData.sessionsCount"
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
                      v-model="defaultData.summary"
                      rows="2"
                  />
                </b-form-group>
              </b-col>

<!--              &lt;!&ndash; Field:  description &ndash;&gt;-->
<!--              <b-col-->
<!--                  cols="12"-->
<!--              >-->
<!--                <b-form-group-->
<!--                    label="توضیحات"-->
<!--                    label-for="description"-->
<!--                >-->
<!--                  <b-form-textarea-->
<!--                      id="description"-->
<!--                      v-model="defaultData.description"-->
<!--                      rows="6"-->
<!--                  />-->
<!--                </b-form-group>-->
<!--              </b-col>-->

            </b-row>
          </b-form>

          <!-- Action Buttons -->
          <b-button
              v-if="defaultData.isOnline"
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
              @click="createCourse"
              :disabled="defaultData.isOnline === true && defaultData.schedules.length === 0 "
          >
            ثبت
          </b-button>

        </b-card>

        <b-alert
            :show="defaultData.isOnline === true && defaultData.schedules.length === 0 "
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

        <b-card v-for="(item,index) in defaultData.schedules">
          <b-row>

            <!-- Field:  startTime -->
            <b-col
                cols="12"
                md="6"
            >
              <b-form-group
                  label="تاریخ و زمان شروع"
                  label-for=""
              >
                <input
                    type="text"
                    :id="`start${index}`"
                    placeholder="شروع"
                    v-model.trim="item.startTime"
                    class="form-control form-control-lg w-11/12 2xl:h-12 h-10 mx-auto mt-1 mb-1 text-sm block w-full rounded-full text-center bg-gray-100 border-transparent focus:border-gray-500 focus:bg-white focus:ring-0 placeholder-inputPlaceHolder text-gray-700"
                />
                <custom-date-picker
                    v-model="item.startTime"
                    auto-submit
                    format="YYYY-MM-DD HH:mm"
                    display-format="jYYYY/jMM/jDD HH:mm"
                    class=""
                    :element="`start${index}`"
                    type="datetime"
                    :min="today"
                    color="#5c6bc0"
                ></custom-date-picker>
              </b-form-group>
            </b-col>

            <!-- Field:  endTime -->
            <b-col
                cols="12"
                md="6"
            >
              <b-form-group
                  label="تاریخ و زمان پایان"
                  label-for=""
              >
                <input
                    type="text"
                    :id="`end${index}`"
                    placeholder="پایان"
                    v-model.trim="item.endTime"
                    class="form-control form-control-lg w-11/12 2xl:h-12 h-10 mx-auto mt-1 mb-1 text-sm block w-full rounded-full text-center bg-gray-100 border-transparent focus:border-gray-500 focus:bg-white focus:ring-0 placeholder-inputPlaceHolder text-gray-700"
                />
                <custom-date-picker
                    v-model="item.endTime"
                    auto-submit
                    format="YYYY-MM-DD HH:mm"
                    display-format="jYYYY/jMM/jDD HH:mm"
                    class=""
                    :min="today"
                    :element="`end${index}`"
                    type="datetime"
                    color="#5c6bc0"
                ></custom-date-picker>
              </b-form-group>
            </b-col>

          </b-row>
        </b-card>
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
  BDropdownItem, BForm, BFormGroup, BFormInput, BFormTextarea,
  BImg,
  BLink, BMedia,
  BRow, BTab, BTabs
} from "bootstrap-vue";
import vSelect from "vue-select";
import ProductCourseCreateRequest from "@/libs/Api/Product/ProductCourseCreateRequest";
import ToastificationContent from "@core/components/toastification/ToastificationContent";

export default {
  title:"افزودن دوره - پنل ادمین بانو کلاب",
  name: "ECommerceAddCourse",
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
      defaultData:{
        isDeleted: false,
        courseId: 0,
        name: "",
        summary: "",
        type: 1,
        sessionsCount:'',
        isOnline: true,
        schedules:[]
      },
      isOnlineOptions:[
        {label : 'آنلاین' , value : true},
        {label : 'آفلاین' , value : false}
      ],
    }
  },
  methods:{
    async createCourse(){
      let _this = this;

      let productCourseCreateRequest = new ProductCourseCreateRequest(_this);
      productCourseCreateRequest.setParams(_this.defaultData);
      await productCourseCreateRequest.fetch(function (content){
       console.log(content)
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
      },function (error){
        console.log(error)
      })
    },
    addSchedule(){
      let _this = this;

      _this.defaultData.schedules.push({
        startTime:'',
        endTime:''
      })
    }
  },
}
</script>

<style scoped>

</style>
