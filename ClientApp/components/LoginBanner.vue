<template>
<div class="container-fluid LoginBanner" >
  <div class="row pt-5 " >

<!--    <div class="col-md-12 my-5 d-flex justify-content-center ">-->
<!--      <img src="../assets/images/BanooClubLogo.png" class="img-fluid"  alt="">-->
<!--    </div>-->
<!--    <div class="col-md-12" style="height: 700px;overflow-y: scroll">-->
<!--      <ul class="timeline">-->
<!--        &lt;!&ndash; Item 1 &ndash;&gt;-->
<!--        <li>-->
<!--          <div class="direction-r">-->
<!--            <div class="flag-wrapper">-->
<!--              <span class="hexa"></span>-->
<!--              <span class="time-wrapper"><span class="time">-->
<!--               <i class="fas fa-comment-medical"></i>-->
<!--              </span></span>-->
<!--              <span class="flag">آگاهی و مشاوره قبل بانو کلاب پلاس</span>-->

<!--            </div>-->
<!--&lt;!&ndash;            <div class="desc">Lorem ipsum Nisi labore aute do aute culpa magna nulla voluptate exercitation deserunt proident.</div>&ndash;&gt;-->
<!--          </div>-->
<!--        </li>-->

<!--        &lt;!&ndash; Item 2 &ndash;&gt;-->
<!--        <li>-->
<!--          <div class="direction-l">-->
<!--            <div class="flag-wrapper">-->
<!--              <span class="hexa"></span>-->
<!--              <span class="time-wrapper"><span class="time">-->
<!--            <i class="fab fa-usps"></i>-->
<!--              </span></span>-->
<!--              <span class="flag">خدمات و آگهی بانو کلاب پلاس</span>-->

<!--            </div>-->

<!--          </div>-->
<!--        </li>-->

<!--        &lt;!&ndash; Item 3 &ndash;&gt;-->
<!--        <li>-->
<!--          <div class="direction-r">-->
<!--            <div class="flag-wrapper">-->
<!--              <span class="hexa"></span>-->
<!--              <span class="time-wrapper"><span class="time">-->
<!--          <i class="fas fa-restroom"></i>-->
<!--              </span></span>-->
<!--              <span class="flag">-->
<!--هم خانه در بانو کلاب پلاس</span>-->

<!--            </div>-->

<!--          </div>-->
<!--        </li>-->
<!--        <li>-->
<!--          <div class="direction-l">-->
<!--            <div class="flag-wrapper">-->
<!--              <span class="hexa"></span>-->
<!--              <span class="time-wrapper"><span class="time">-->
<!--         <i class="fas fa-user-check"></i>-->
<!--              </span></span>-->
<!--              <span class="flag">آگاهی و مشاوره بعد از بانو کلاب پلاس</span>-->

<!--            </div>-->

<!--          </div>-->
<!--        </li>-->
<!--      </ul>-->
<!--    </div>-->



<!--    <div class="col-md-12 d-flex flex-wrap ">-->
<!--<h4 class="text-white" >آگاهی و مشاوره قبل بانو کلاب پلاس</h4><h4 class="text-white">/</h4><h4 class="text-white">خدمات و آگهی بانو کلاب پلاس</h4><h4 class="text-white">/</h4><h4 class="text-white">هم خانه در بانو کلاب پلاس</h4>-->

<!--    </div>-->
<!--    <div class="col-md-12">-->
<!--      <h4 class="text-white">آگاهی و مشاوره بعد از بانو کلاب پلاس</h4>-->
<!--    </div>-->
  </div>
</div>
</template>

<script>
export default {
  name: "LoginBanner"
}
</script>

<style scoped>
@import url(https://fonts.googleapis.com/css?family=Raleway:400,900);

body{
  font-family: 'Raleway', sans-serif;
  color: #333;
}

header h1{
  text-align: center;
  font-weight: bold;
  margin-top: 0;
}

header p{
  text-align: center;
  margin-bottom: 0;
}

.hexa{
  border: 0px;
  float: left;
  text-align: center;
  height: 35px;
  width: 60px;
  font-size: 22px;
  background: #f0f0f0;
  color: #3c3c3c;
  position: relative;
  margin-top: 15px;
}

.hexa:before{
  content: "";
  position: absolute;
  left: 0;
  width: 0;
  height: 0;
  border-bottom: 15px solid #f0f0f0;
  border-left: 30px solid transparent;
  border-right: 30px solid transparent;
  top: -15px;
}

.hexa:after{
  content: "";
  position: absolute;
  left: 0;
  width: 0;
  height: 0;
  border-left: 30px solid transparent;
  border-right: 30px solid transparent;
  border-top: 15px solid #f0f0f0;
  bottom: -15px;
}

.timeline {
  position: relative;
  padding: 0;
  width: 100%;
  margin-top: 20px;
  list-style-type: none;
}

.timeline:before {
  position: absolute;
  left: 50%;
  top: 0;
  content: ' ';
  display: block;
  width: 2px;
  height: 100%;
  margin-left: -1px;
  background: rgb(213,213,213);
  background: -moz-linear-gradient(top, rgba(213,213,213,0) 0%, rgb(213,213,213) 8%, rgb(213,213,213) 92%, rgba(213,213,213,0) 100%);
  background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(30,87,153,1)), color-stop(100%,rgba(125,185,232,1)));
  background: -webkit-linear-gradient(top, rgba(213,213,213,0) 0%, rgb(213,213,213) 8%, rgb(213,213,213) 92%, rgba(213,213,213,0) 100%);
  background: -o-linear-gradient(top, rgba(213,213,213,0) 0%, rgb(213,213,213) 8%, rgb(213,213,213) 92%, rgba(213,213,213,0) 100%);
  background: -ms-linear-gradient(top, rgba(213,213,213,0) 0%, rgb(213,213,213) 8%, rgb(213,213,213) 92%, rgba(213,213,213,0) 100%);
  background: linear-gradient(to bottom, rgba(213,213,213,0) 0%, rgb(213,213,213) 8%, rgb(213,213,213) 92%, rgba(213,213,213,0) 100%);
  z-index: 5;
}

.timeline li {
  padding: 2em 0;
}

.timeline .hexa{
  width: 16px;
  height: 10px;
  position: absolute;
  background: #00c4f3;
  z-index: 5;
  left: 0;
  right: 0;
  margin-left:auto;
  margin-right:auto;
  top: -30px;
  margin-top: 0;
}

.timeline .hexa:before {
  border-bottom: 4px solid #00c4f3;
  border-left-width: 8px;
  border-right-width: 8px;
  top: -4px;
}

.timeline .hexa:after {
  border-left-width: 8px;
  border-right-width: 8px;
  border-top: 4px solid #00c4f3;
  bottom: -4px;
}

.direction-l,
.direction-r {
  float: none;
  width: 100%;
  text-align: center;
}

.flag-wrapper {
  text-align: center;
  position: relative;
}

.flag {
  position: relative;
  display: inline;
  background: rgb(255,255,255);
  font-weight: 600;
  z-index: 15;
  padding: 6px 10px;
  text-align: left;
  border-radius: 5px;
}

.direction-l .flag:after,
.direction-r .flag:after {
  content: "";
  position: absolute;
  left: 50%;
  top: -15px;
  height: 0;
  width: 0;
  margin-left: -8px;
  border: solid transparent;
  border-bottom-color: rgb(255,255,255);
  border-width: 8px;
  pointer-events: none;
}

.direction-l .flag {
  -webkit-box-shadow: -1px 1px 1px rgba(0,0,0,0.15), 0 0 1px rgba(0,0,0,0.15);
  -moz-box-shadow: -1px 1px 1px rgba(0,0,0,0.15), 0 0 1px rgba(0,0,0,0.15);
  box-shadow: -1px 1px 1px rgba(0,0,0,0.15), 0 0 1px rgba(0,0,0,0.15);
}

.direction-r .flag {
  -webkit-box-shadow: 1px 1px 1px rgba(0,0,0,0.15), 0 0 1px rgba(0,0,0,0.15);
  -moz-box-shadow: 1px 1px 1px rgba(0,0,0,0.15), 0 0 1px rgba(0,0,0,0.15);
  box-shadow: 1px 1px 1px rgba(0,0,0,0.15), 0 0 1px rgba(0,0,0,0.15);
}

.time-wrapper {
  display: block;
  position: relative;
  margin: 4px 0 0 0;
  z-index: 14;
  line-height: 1em;
  vertical-align: middle;
  color: #fff;
}

.direction-l .time-wrapper {
  float: none;
}

.direction-r .time-wrapper {
  float: none;
}

.time {
  background: #00c4f3;
  display: inline-block;
  padding: 8px;
}

.desc {
  position: relative;
  margin: 1em 0 0 0;
  padding: 1em;
  background: rgb(254,254,254);
  -webkit-box-shadow: 0 0 1px rgba(0,0,0,0.20);
  -moz-box-shadow: 0 0 1px rgba(0,0,0,0.20);
  box-shadow: 0 0 1px rgba(0,0,0,0.20);
  z-index: 15;
}

.direction-l .desc,
.direction-r .desc {
  position: relative;
  margin: 1em 1em 0 1em;
  padding: 1em;
  z-index: 15;
}

@media(min-width: 768px){
  .timeline {
    width: 660px;
    margin: 0 auto;
    margin-top: 20px;
  }

  .timeline li:after {
    content: "";
    display: block;
    height: 0;
    clear: both;
    visibility: hidden;
  }

  .timeline .hexa {
    left: -28px;
    right: auto;
    top: 8px;
  }

  .timeline .direction-l .hexa {
    left: auto;
    right: -28px;
  }

  .direction-l {
    position: relative;
    width: 310px;
    float: left;
    text-align: right;
  }

  .direction-r {
    position: relative;
    width: 310px;
    float: right;
    text-align: left;
  }

  .flag-wrapper {
    display: inline-block;
  }

  .flag {
    font-size: 18px;
  }

  .direction-l .flag:after {
    left: auto;
    right: -16px;
    top: 50%;
    margin-top: -8px;
    border: solid transparent;
    border-left-color: rgb(254,254,254);
    border-width: 8px;
  }

  .direction-r .flag:after {
    top: 50%;
    margin-top: -8px;
    border: solid transparent;
    border-right-color: rgb(254,254,254);
    border-width: 8px;
    left: -8px;
  }

  .time-wrapper {
    display: inline;
    vertical-align: middle;
    margin: 0;
  }

  .direction-l .time-wrapper {
    float: left;
  }

  .direction-r .time-wrapper {
    float: right;
  }

  .time {
    padding: 5px 10px;
  }

  .direction-r .desc {
    margin: 1em 0 0 0.75em;
  }
}

@media(min-width: 992px){
  .timeline {
    width: 800px;
    margin: 0 auto;
    margin-top: 20px;
  }

  .direction-l {
    position: relative;
    width: 380px;
    float: left;
    text-align: right;
  }

  .direction-r {
    position: relative;
    width: 380px;
    float: right;
    text-align: left;
  }
}
.land-area {
  background: #1fb6ff none repeat scroll 0 0;
  color: #fff;
  height: 100vh;
  position: relative;
  text-align: center;
  vertical-align: middle;
  width: 100%;
}

.log-reg-area > p {
  color: #878787;
  font-size: 14px;
}

.friend-logo {
  display: inline-block;
  width: 100%;
}

.friend-logo > span {
  font-size: 200px;
  position: relative;
}

.friend-logo > span::before,
.friend-logo > span::after {
  background: #0a98dc none repeat scroll 0 0;
  border: 16px solid rgba(255, 255, 255, 0.1);
  border-radius: 100%;
  content: "";
  height: 180px;
  left: -42px;
  position: absolute;
  top: 20px;
  width: 180px;
  z-index: -1;
}

.LoginBanner{
background-color: #1fb6ff;

  background-size: cover  ;
  height: 100%
}

.follow-me {
  text-decoration: none;
  cursor: pointer;
}

.follow-me:hover {
  text-shadow: 0 1px 1px #454545;
}
.land-meta-text {
  font-size: 14px;
  line-height: 26px;
}
.land-meta-title {
  font-weight: 500;
  line-height: 1.2;
}
</style>
