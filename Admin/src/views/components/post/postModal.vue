<template>
  <b-row class="match-height">
    <b-col
        md="6"
        lg="12"
    >
      <b-card
          v-if=" postData &&  postData.medias!==null && postData.medias.length>0"
          class="CardImg"
          :img-src="`https://banooclubapi.simagar.com/media/gallery/post/${postData.medias[0].base64}`"
          img-alt="Card image cap"
          img-top
          :title="postData.title"
      >

        <b-card-text>
          {{postData.description}}
        </b-card-text>

      </b-card>
    </b-col>

  </b-row>
</template>

<script>
import {
  BCard, BCardText, BButton, BRow, BCol, BImg, BCardBody, BCardTitle, BCardSubTitle, BLink,
} from 'bootstrap-vue'
import Ripple from 'vue-ripple-directive'

export default {
  props:['postData'],
  components: {
    BCard,
    BCardText,
    BButton,
    BCardBody,
    BCardTitle,
    BCardSubTitle,
    BRow,
    BCol,
    BLink,
    BImg,
  },
  directives: {
    Ripple,
  },
}
</script>

<style>
.CardImg img{
  width: 100%;
  height: 400px;
  object-fit: contain;
  object-position: center;
}
</style>
