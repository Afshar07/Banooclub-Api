<template>
  <div class="grid grid-cols-4 my-3">
    <p class="col-span-2 text-gray-500 mb-1">نقشه</p>
    <div class="w-full col-span-4" id="map"></div>
  </div>
</template>
<script>
export default {
  props: {
    latitude: {
      type: [Number, String],
      required: true,
    },
    longitude: {
      type: [Number, String],
      required: true,
    },
  },
  data() {
    return {
      map: null,
    };
  },
  async mounted() {
    this.$nextTick(()=>{
      if(this.map){
        this.map.remove()
        this.map = null
      }else{
        this.map = L.map("map").setView([this.latitude, this.longitude], 14);
        L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", {
          maxZoom: 19,
          attribution: "© OpenStreetMap",
        }).addTo(this.map);
        // Leaflet has a known issue with wrong marker address
        // This section is their provided solution for fixing the issue
        // Please don't touch it or marker won't be shown on the map
        delete L.Icon.Default.prototype._getIconUrl;
        L.Icon.Default.mergeOptions({
          iconRetinaUrl: require("leaflet/dist/images/marker-icon-2x.png"),
          iconUrl: require("leaflet/dist/images/marker-icon.png"),
          shadowUrl: require("leaflet/dist/images/marker-shadow.png"),
        });
        // Solution ends here
        L.marker([this.latitude, this.longitude]).addTo(this.map);
      }

    })

  },
};
</script>
<style scoped>
#map {
  height: 300px !important;
}
</style>
