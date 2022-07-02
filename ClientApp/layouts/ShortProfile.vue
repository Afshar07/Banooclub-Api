<template>
  <div class="row mb-4 d-none d-lg-block m-0 col-12">
    <div class="col-12 text-end">
      <span class="titleSection">مشخصات</span>
    </div>
    <div class="col-12 ShortProfileTitle">
      درباره
    </div>
    <div class="col-12 ShortProfileText">

      {{ PageData.bio !==' '? PageData.bio : 'این کاربر بیوگرافی ثبت نکرده است'  }}
    </div>

  </div>
</template>

<script>
export default {
  name: "ShortProfile",
  data(){
    return{

    }
  },
  computed:{
    PageData(){
      return this.$store.state.HeaderData
    }
  },

  methods:{

  }
}
</script>

<style scoped>
.SectionBox {
  margin: 30px auto;
  width: 80%;
  background: #fdfdfd;
  border: 1px solid #ede9e9;
  border-radius: 3px;
}

.titleSection {
  /*border-bottom: 2px solid #088dcd;*/
  background: rgba(0, 0, 0, 0) none repeat scroll 0 0;
  color: #424242;
  display: inline-block;
  font-size: 1.25rem;
  font-weight: 600;
  margin-bottom: 30px;
  padding: 20px 0 10px;
  position: relative;
  text-transform: uppercase;
  width: max-content;
}

.ShortProfileTitle{
  color: #545454;
  font-size: 16px;
  margin-bottom: 8px;
  width: 100%;
}

.ShortProfileText {
  color: #888888;
  line-height: 23px;
  margin-top: 0;
  margin-bottom: 20px;
  font-size: 14px;
}

@media (max-width: 576px) {
  .SectionBox {
    width: 100%;
    margin-top: 7rem;
  }
}
</style>
