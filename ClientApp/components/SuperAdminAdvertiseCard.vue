<template>
  <div class="container-fluid px-0 rounded my-2">
    <div class="row position-relative">
      <div class="col-12">
        <div class="card mb-3">
          <div class="row g-0">
            <div class="col-md-4 order-1">
<!--              <img src="~/static/user-avatar.jpg" class="img-fluid rounded-start" alt="...">-->
            </div>
            <div class="col-md-8 order-0">
              <div class="card-body h-100 pb-0">
                <h5 class="card-title text-muted">عنوان آگهی</h5>
                <p class="card-text my-md-2 my-3 text-muted">قیمت : 100 </p>
                <p class="card-text"><small class="text-muted">زمان آگهی </small></p>
                <div class="d-flex gap-2">
                  <div>
                    <button class="btn btn-sm btn-outline-success">
                      تایید آگهی
                    </button>
                  </div>
                  <div>
                    <button class="btn btn-sm btn-outline-primary" v-html="activeAdvertise?'غیرفعال کردن':'فعال کردن'"></button>
                  </div>
                  <div>
                    <button class="btn btn-sm btn-outline-info">
                      مشاهده آگهی
                    </button>
                  </div>
                </div>
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
  name: "SuperAdminAdvertiseCard",
  data() {
    return {
      activeAdvertise: false
    }
  }
}
</script>

<style scoped>

</style>
