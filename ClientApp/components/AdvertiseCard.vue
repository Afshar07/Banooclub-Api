<template>
  <div class="container-fluid px-0 rounded my-2" @click="goToDetailAdvertise($props.AdvertiseData.adsId)">
    <div class="row position-relative">
      <div class="col-12">
        <div class="card mb-3">
          <div class="row g-0">
            <div class="col-md-4 order-1" style="height: 200px">
              <img
                :src="`https://pplusapi.simagar.com/${ $props.AdvertiseData.photos[0].base64}`"
                class="img-fluid rounded-start" alt="..." height="100%">
            </div>
            <div class="col-md-8 order-0">
              <div class="card-body h-100 pb-0">
                <h5 class="card-title text-muted">{{ $props.AdvertiseData.title }}</h5>
                <div class="d-flex gap-2 align-items-center">
                  <i class="fas fa-dollar-sign"></i>
                  <div>قیمت :</div>
                  <div>{{ $props.AdvertiseData.price }}</div>
                </div>
                <p class="card-text my-3 d-flex gap-2 align-items-center ">
                  <i class="fas fa-history text-muted"></i>
                  <small class="text-muted">{{ $props.AdvertiseData.createDate  | moment("jYYYY/jM/jD HH:mm") }}</small></p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "AdvertiseCard",
  props: {
    AdvertiseData: {
      type: Object,
      required: false
    }
  },
  methods: {
    goToDetailAdvertise(adsId) {
      this.$router.push({
        path: '/Migration/AdvertiseDetail',
        query: {
          id: adsId
        }
      })
    }
  }
}
</script>

<style scoped>
.card {
  /*max-width: 450px;*/
  border-radius: 4px;
  min-height: 17.3374613003096vh;
  padding: 16px;
  position: relative;
}

img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.container-fluid {
  cursor: pointer;
}
</style>
