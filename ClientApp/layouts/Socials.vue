<template>
  <div class="row SectionBox d-none d-lg-block">
    <div class="col-12 text-end">
      <span class="titleSection">شبکه اجتماعی</span>
    </div>
    <div class="col-12 socialLine">
      <div class="d-flex flex-column">
        <div class="facebookLine">
          <div class="d-flex flex-row justify-content-between">
            <div class="text-white">فیسبوک</div>
            <div class="text-white">
              <i class="fab fa-facebook-f"></i>
            </div>
            <div class="text-white">25 لایک</div>
          </div>
        </div>
        <div class="twitterLine">
          <div class="d-flex flex-row justify-content-between">
            <div class="text-white">توییتر</div>
            <div class="text-white">
              <i class="fab fa-twitter"></i>
            </div>
            <div class="text-white">25 لایک</div>
          </div>
        </div>
        <div class=googleLine>
          <div class="d-flex flex-row justify-content-between">
            <div class="text-white">گوگل</div>
            <div class="text-white">
              <i class="fab fa-google"></i>
            </div>
            <div class="text-white">25 لایک</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Socials"
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

.facebookLine {
  background: #2f5b9d;
  margin-bottom: 10px;
  padding: 3px 10px;
  width: 100%;
  box-shadow: 0 2px 4px rgb(0 0 0 / 40%);
  cursor: pointer;
  font-size: 15px;
}

.twitterLine {
  background: #38bff1;
  margin-bottom: 10px;
  padding: 3px 10px;
  width: 100%;
  box-shadow: 0 2px 4px rgb(0 0 0 / 40%);
  cursor: pointer;
  font-size: 15px;
}

.googleLine {
  background: #f24756;
  margin-bottom: 10px;
  padding: 3px 10px;
  width: 100%;
  box-shadow: 0 2px 4px rgb(0 0 0 / 40%);
  cursor: pointer;
  font-size: 15px;
}

.socialLine {
  padding: 0 25px 20px;
}
</style>
