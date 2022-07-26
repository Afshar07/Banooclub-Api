<template>
  <b-col

      v-if="propertyData.categoryPropertyId === 0"
      cols="12"
  >
    <b-row>

      <b-form-group
          class="col-3"
          label="name"
          label-for=""
      >
        <b-form-input

            v-model="propertyData.name"
        />
      </b-form-group>

      <b-form-group
          class="col-5"
          label="value"
          label-for=""
      >
        <b-form-input

            v-model="propertyData.value"
        />
      </b-form-group>

    </b-row>
  </b-col>
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
  BLink, BMedia, BOverlay,
  BRow, BTab, BTabs
} from "bootstrap-vue";
import vSelect from "vue-select";

export default {
  name: "NewCategoryProperty",
  props:['propertyData'],
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
    BOverlay,
    vSelect,
  },
}
</script>

<style scoped>

</style>