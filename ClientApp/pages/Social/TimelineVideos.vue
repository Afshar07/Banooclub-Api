<template>
  <div class="container-fluid px-0">
    <div class="offcanvas offcanvas-end"style="background: black!important;" tabindex="-1" id="Video" aria-labelledby="offcanvasExampleLabel">
    <div class="offcanvas-header">
      <h5 class="offcanvas-title" id="offcanvasExampleLabel"></h5>
      <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
    </div>
    <div class="offcanvas-body ">
      <div>
        <client-only>
          <swiper class="swiper gallery-top" :options="swiperOptionTop" ref="swiperTop">
            <swiper-slide class="slide-1" v-for="item in MyVideos" :key="item">
              <div class=" SinglePhoto"  @click="showSidePanel()">
                <a href="#offcanvasExampleeee" data-bs-toggle="offcanvas" ><img :src=" BaseUrl + item " width="100%" height="200px"  class=" rounded" alt=""></a>
              </div>
            </swiper-slide>
            <div class="swiper-button-next swiper-button-white" slot="button-next"></div>
            <div class="swiper-button-prev swiper-button-white" slot="button-prev"></div>
          </swiper>
        </client-only>
      </div>
    </div>
  </div>

    <div class="row bg-white p-4 mx-1" v-if="this.$store.state.HeaderData.status ===1 ||this.$store.state.HeaderData.status ===4">

      <h4><strong>ویدیو های من </strong></h4>
      <div v-if="Video" class="col-md-3 my-4" v-for="item in Video">
        <div class="d-flex flex-wrap SinglePhoto"  @click="showSidePanel()">
          <a href="#Video" data-bs-toggle="offcanvas" ><img :src=" BaseUrl + item " width="100%" height="200px"  class=" rounded" alt=""></a>
        </div>
      </div>
    </div>
    <div v-else-if="this.$store.state.HeaderData.status ===2" class="row boxMainContent text-warning position-relative bg-white p-3">
      <p  class="text-center">صفحه این کاربر خصوصی است برای مشاهده آن درخواست دوستی ارسال کنید</p>
    </div>
    <div v-else-if="this.$store.state.HeaderData.status ===3" class="row boxMainContent text-warning position-relative bg-white p-3">
      <p  class="text-center">برای مشاهده صفحه این کاربر ابتدا وارد اکانت خود شوید</p>
    </div>
  </div>
</template>

<script>
export default {
  name: "TimelineVideos",
  data() {
    return {
      url: ['/_nuxt/static/photo1.jpg', '/_nuxt/static/photo1.jpg', '/_nuxt/static/photo1.jpg',],
      MyVideos:''
    }
  },
  layout: 'BanooClubLayout',
  methods: {
    showSidePanel(videoId) {
      this.$parent.$emit('SideNavPictureVideo', 'video', videoId)
    },
    onFileChange(e) {
      const file = e.target.files[0];
      this.url.push(URL.createObjectURL(file));
    },
    uploadNewVideo() {
      document.querySelector('.my-file').click();
    },
    deleteVideo(id) {
      return this.url.splice(id, 1)
    },
    GetVideos(){
      this.$axios.post(`Users/GetVideosByUserId?userId=${this.$route.query.userId}`,null,{
       
      }).then((res)=>{
        console.log(res.data)
        if(res.data.status ===1){
          res.data.medias.forEach((item)=>{
            this.MyVideos.push(item)
          })
        }else if(res.data.status ===2){
          this.Msg = 'این اکانت خصوصی است.برای مشاهده ویدیو های آن ابتدا باید درخواست دوستی ارسال کنید'
        }else if(res.data.status ===3){
          this.Msg = 'ابتدا وارد اکانت خود شوید'
        }

      })
    },
  }
}
</script>

<style scoped>
.playIcon {
  position: absolute;
  top: 40%;
  left: 40%;
  cursor: pointer;
  border: 1px solid #fff;
  border-radius: 50%;
  padding: 8px;
}

.central-meta {
  background: #fdfdfd none repeat scroll 0 0;
  border: 1px solid #ede9e9;
  border-radius: 3px;
  display: inline-block;
  width: 100%;
  margin: 20px auto;
  padding: 25px;
}

.photos {
  list-style: outside none none;
  margin-bottom: 10px;
  padding-right: 0;
  position: relative;
}

.photos img:hover {
  cursor: pointer;
  transform: scale(1.01);
}

.lodmore {
  display: inline-block;
  width: 100%;
}

.btn-view.btn-load-more {
  background: #fff;
  border: medium none;
  border-radius: 100%;
  color: transparent;
  display: block;
  height: 50px;
  margin: 30px auto 0;
  position: relative;
  width: 50px;
  transition: all 0.2s linear 0s;
  z-index: 77;
}

.central-meta button.btn-view {
  background: #f1f1f1;
  color: #545454;
}

.btn-view.btn-load-more:hover {
  transform: translate(0, 0) rotate(0deg);
  z-index: 111;
  transition: all 0.2s linear 0s;
  background: #088dcd;
  color: #fff;
}

.btn-view.btn-load-more:hover:hover {
  color: #fff;
  transform: translate(0, 0) rotate(360deg);
}

.deleteIcon {
  left: 1rem;
  top: 1rem;
  cursor: pointer;
}
</style>
