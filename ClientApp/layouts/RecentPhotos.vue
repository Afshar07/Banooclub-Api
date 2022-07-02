<template>
  <div class="row SectionBox">
    <div class="col-12 text-end">
      <span class="titleSection">آخرین تصاویر</span>
    </div>
    <div class="row px-4">
      <div class="col-3 px-0 my-2" v-for="(item,index) in 8" :key="item">
        <img src="~/static/recent-8.jpg" alt="" class="img-fluid" @click="showSidePanel(index)"/>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "RecentPhotos",
  methods: {
    showSidePanel(imageId) {
      this.$parent.$emit('SideNavPictureVideo', 'images', imageId);
    }
  }
}
</script>

<style scoped>
.titleSection {
  border-bottom: 2px solid #088dcd;
  background: rgba(0, 0, 0, 0) none repeat scroll 0 0;
  color: #424242;
  display: inline-block;
  font-size: 15px;
  margin-bottom: 30px;
  padding: 20px 0 10px;
  position: relative;
  text-transform: uppercase;
  width: max-content;
}

.SectionBox {
  margin: 30px auto;
  background: #fdfdfd;
  border: 1px solid #ede9e9;
  border-radius: 3px;
  width: 80%;
}

img {
  cursor: pointer;
}
</style>
