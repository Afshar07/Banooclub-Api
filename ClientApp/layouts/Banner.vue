<template>
  <div class="row bgWidget  SectionBackgroundBox">
    <div class="d-flex flex-column subBackground text-end">
      <div class="">
        <div class="d-flex flex-row  justify-content-between">
          <div class="">
            <img src="~/static/book-icon.png" alt=""/>
          </div>
          <div class="">
            <font-awesome-icon icon="ellipsis-h" color="white"></font-awesome-icon>
          </div>
        </div>
      </div>
      <div class="favoritePage">صفحه مورد علاقه خود را ایجاد کنید
      </div>
      <div class="text-white">پسندیدن همه تم ها</div>
      <div class="newStart">همین حالا شروع کنید</div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Banner"
}
</script>

<style scoped>
.bgWidget {
  background-image: url("static/baner-widgetbg.jpg");
  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
  margin: 30px auto;
  width: 80%;
  border: 1px solid #ede9e9;
  border-radius: 3px;
}

.subBackground {
  background: #088dcd;
  opacity: 0.7;
  padding: 20px 30px;
}

.favoritePage {
  color: #fff;
  font-size: 23px;
  line-height: initial;
  text-transform: uppercase;
  margin-bottom: 16px;
}

.newStart {
  background: #fff none repeat scroll 0 0;
  color: #333;
  line-height: initial;
  margin-top: 30px;
  padding: 8px 0;
  text-align: center;
  text-transform: capitalize;
  width: 100%;
  opacity: 1;
}
</style>
