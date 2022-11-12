<template>
  <b-card
      no-body
      class="card-apply-job"
  >
    <b-card-header class="pb-1">
      <b-media no-body>
        <b-media-aside class="mr-1">
          <b-avatar
              v-if="ServiceData.userInfo"
              size="42"
              :src="`https://banooclubapi.oregonserver.com/media/gallery/profile/${ServiceData.userInfo.selfieFileData}`"
          />
        </b-media-aside>
        <b-media-body>
          <h5 v-if="ServiceData.userInfo" class="mb-0">
           {{ServiceData.userInfo.userName}}
          </h5>
          <small class="text-muted">{{ new Date(ServiceData.createDate).toLocaleDateString('fa-IR') }}</small>
        </b-media-body>
      </b-media>
      <b-badge
          pill
          variant="light-primary"
      >
        {{ ServiceData.mobile }}
      </b-badge>
    </b-card-header>
    <b-card-body>
      <h5 class="apply-job-title">
        {{ServiceData.title }}
      </h5>
      <b-card-text class="mb-2">
        {{ServiceData.description
        }}
      </b-card-text>

      <!-- payment  -->
      <div class="apply-job-package bg-light-primary rounded">
        <div>
          <sup class="text-body">
            <small>تومان</small>
          </sup>
          <h2 class="d-inline mr-25">
            {{  Intl.NumberFormat('fa-IR').format(ServiceData.totalPrice) }}
          </h2>

        </div>
      </div>
      <!--/ payment  -->
        <div v-for="item in ServiceData.medias" class="d-flex align-items-center justify-content-around">
          <img :src="`https://banooclubapi.oregonserver.com/media/gallery/service/${item.base64}`" width="100px" height="100px" alt="">

        </div>


    </b-card-body>
  </b-card>
</template>

<script>
import {
  BCard, BCardHeader, BMedia, BMediaAside, BAvatar, BMediaBody, BBadge, BCardText, BCardBody, BButton,
} from 'bootstrap-vue'
import Ripple from 'vue-ripple-directive'

export default {
  props:['ServiceData'],
  components: {
    BCard,
    BButton,
    BBadge,
    BCardHeader,
    BMedia,
    BMediaAside,
    BAvatar,
    BMediaBody,
    BCardText,
    BCardBody,
  },
  directives: {
    Ripple,
  },
}
</script>
