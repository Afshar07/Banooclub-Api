<template>
  <div
    class="modal fade"
    id="AddGroup"
    tabindex="-1"
    role="dialog"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">ایجاد گروه جدید</h5>
          <button
            type="button"
            class="btn"
            data-bs-dismiss="modal"
            aria-label="Close"
          >
            <i class="fas fa-times"></i>
          </button>
        </div>
        <div class="modal-body"></div>
        <div class="modal-footer">
          <button
            type="button"
            class="btn btn-secondary"
            data-bs-dismiss="modal"
          >
            بستن
          </button>
          <button type="button" class="btn btn-primary" data-bs-dismiss="modal">
            ایجاد گروه
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
