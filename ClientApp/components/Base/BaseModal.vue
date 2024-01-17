// You can use this fully responsive component to handle all modals, // Using three slots: title
// (For header title), content (For main content) and footer (For actions like
// buttons). Just pass a variable as isRenderingModal and handle open/close //
using that
<template>
  <div class="modal-container position-relative" v-if="isRenderingModal" style="z-index: 9999;">
    <transition name="overlay">
      <div class="overlay" @click="closeModal" v-show="isModalDone"></div>
    </transition>
    <transition name="modal" appear mode="in-out">
      <div class="modal-body" v-show="isModalDone">
        <header style="border-radius: 0.5rem;" class="d-flex justify-content-between align-items-center border-bottom p-2">
          <h4 class="mb-0" style="font-family: iranSans !important;">
            <slot name="title"></slot>
          </h4>
          <svg
            @click="closeModal"
            xmlns="http://www.w3.org/2000/svg"
            class="custom-modal-close-icon"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
            stroke-width="2"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              d="M6 18L18 6M6 6l12 12"
            />
          </svg>
        </header>
        <main class="pt-5">
          <slot name="content"></slot>
        </main>
        <footer class="mt-4">
          <slot name="actions"></slot>
        </footer>
      </div>
    </transition>
  </div>
</template>

<script>
export default {
  emits: ["closeModal"],
  props: {
    isRenderingModal: {
      type: Boolean,
      required: true,
      default: false,
    },
  },
  watch: {
    isRenderingModal(val) {
      if (val) {
        this.isModalDone = true;
      }
    },
  },
  data() {
    return {
      isModalDone: true,
    };
  },
  methods: {
    closeModal() {
      // Wait for animation to end and then emit close event
      this.isModalDone = false;
      setTimeout(() => {
        this.$emit("closeModal");
      }, 300);
    },
  },
};
</script>

<style scoped>
@media only screen and (max-width: 768px) {
  .modal-body {
    width: 95%;
    height: auto;
    position: fixed;
    bottom: 0;
    left: 50%;
    transform: translate(-50%, 0%);
    background-color: #fff;
    border-radius: 0.5rem;
    bottom: 50%;
    /*border-bottom-left-radius: 0;*/
    /*border-bottom-right-radius: 0;*/
  }
  @keyframes modal-show {
    0% {
      opacity: 0;
      transform: translate(-50%, 100%);
    }
    100% {
      opacity: 1;
      transform: translate(-50%, 0%);
    }
  }
  @keyframes modal-hide {
    0% {
      opacity: 1;
      transform: translate(-50%, 50%);
    }
    100% {
      opacity: 0;
      transform: translate(-50%, 100%);
    }
  }
}
@media only screen and (min-width: 769px) {
  .modal-body {
    min-width: 40%;
    width: auto;
    height: auto;
    position: fixed;
    bottom: 50%;
    left: 50%;
    transform: translate(-50%, 50%);
    background-color: #fff;
    border-radius: 0.5rem;
  }
  @keyframes modal-show {
    0% {
      opacity: 0;
      transform: translate(-50%, 100%);
    }
    100% {
      opacity: 1;
      transform: translate(-50%, 50%);
    }
  }
  @keyframes modal-hide {
    0% {
      opacity: 1;
      transform: translate(-50%, 50%);
    }
    100% {
      opacity: 0;
      transform: translate(-50%, 100%);
    }
  }
}
.overlay {
  position: fixed;
  top: 0;
  right: 0;
  width: 100vw;
  height: 100vh;
  background-color: #7c7c7cbd;
}
.modal-enter-active {
  animation: modal-show 0.3s ease-in-out;
}
.modal-leave-active {
  animation: modal-hide 0.3s ease-in-out;
}

.overlay-enter-active {
  animation: overlay 0.2s ease-in-out;
}
.overlay-leave-active {
  animation: overlay reverse 0.2s ease-in-out;
}
@keyframes overlay {
  0% {
    opacity: 0;
  }
  100% {
    opacity: 1;
  }
}
.custom-modal-close-icon {
  width: 1.5rem;
  height: 1.5rem;
  color: #cf0000;
  cursor: pointer;
}
</style>
