<template>
  <b-card
      v-if="UserIndex"
      :img-src="`https://banoclubapi.com/media/gallery/banner/${UserDetails.baseData.banner}`"
      img-alt="Profile Cover Photo"
      img-top
      class="card-profile"
  >
    <div class="profile-image-wrapper">
      <div class="profile-image p-0">
        <b-avatar
            size="114"
            variant="light"
            :src="`https://banoclubapi.com/${UserDetails.baseData.selfie}`"
        />
      </div>
    </div>
    <h3>{{ UserDetails.baseData.name }}</h3>
    <h4 class="text-muted">
  {{ UserDetails.baseData.userName }} @
    </h4>

    <hr class="mb-2">

    <!-- follower projects rank -->
    <div class="d-flex justify-content-center ">
      <div>
        <h6 class="text-muted font-weight-bolder">
          دنبال کنندگان
        </h6>
        <h3 class="mb-0">
          {{ UserDetails.baseData.followersCount }}
        </h3>
      </div>
      <div>
        <h6 class="text-muted font-weight-bolder mx-2">
        دنبال شوندگان
        </h6>
        <h3 class="mb-0">
          {{ UserDetails.baseData.followingsCount }}
        </h3>
      </div>
    </div>
    <!--/ follower projects rank -->
  </b-card>
</template>

<script>
import { BCard, BAvatar, BBadge } from 'bootstrap-vue'

export default {
  props:['UserIndex'],
  components: {
    BCard,
    BBadge,
    BAvatar,
  },
  computed:{
    UserDetails(){
      return this.UserIndex
    }
  }
}
</script>

<style>
.card-profile img{
  width: 100%;
  height: 400px!important;
  object-fit: cover;
}
</style>
