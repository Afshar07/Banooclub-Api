<template>
  <div class="container-fluid px-0 rounded my-2 main-background">
    <div class="row position-relative">
      <div class="col-12">
        <div class="card m-auto">
          <!--          <img src="~/static/user-avatar2.jpg" class="card-img-top img-fluid" alt="">-->
          <div class="card-body">
            <div class="card-text">
              <p>ماهانه 2 میلیون تومن</p>
              <p>تهران - ایران</p>
              <p>
                {{ $props.UserData.name + " " + $props.UserData.familyName }}
              </p>
              <div class="d-flex flex-row">
                <div class="">24 ساله</div>
                <div class="me-auto">
                  <font-awesome-icon icon="share-alt" size="lg" />
                </div>
              </div>

              <p></p>
            </div>
          </div>
        </div>
      </div>
      <div class="col-12 py-3">
        <div class="d-flex flex-row justify-content-around">
          <div class="p-2 position-relative">
            <span class="numberStyle">35</span>
            <i class="far fa-thumbs-up"></i>
          </div>
          <div class="p-2 position-relative">
            <span class="numberStyle">35</span>
            <i class="far fa-eye"></i>
          </div>
          <div class="p-2 position-relative">
            <span class="numberStyle">35</span>
            <i class="far fa-eye"></i>
          </div>
          <div class="p-2 position-relative">
            <span class="numberStyle">35</span>
            <i class="far fa-eye"></i>
          </div>
          <div class="p-2 position-relative">
            <span class="numberStyle">35</span>
            <i class="far fa-eye"></i>
          </div>
        </div>
      </div>
      <div class="position-absolute userStatus">
        <font-awesome-icon icon="bookmark" size="2x" color="orange" />
      </div>
      <div class="position-absolute requestStatus text-white fw-bold">فوری</div>
    </div>
  </div>
</template>

<script>
export default {
  name: "RoommateCard",
  props: {
    UserData: {
      type: Object,
      required: true,
    },
  },
};
</script>

<style scoped>

.userStatus {
  top: 0;
  left: 2rem;
  height: 42px;
  text-align: center;
  width: 42px;
}

.requestStatus {
  top: 0;
  right: 0.75rem;
  background: #d78a56;
  border-radius: 0 0 0 50%;
  height: 42px;
  text-align: center;
  width: 4rem;
  padding: 0.5rem 0;
}

.numberStyle {
  background: #ccc;
  border-radius: 50%;
  position: absolute;
  top: 0;
  right: -1rem;
}
</style>
