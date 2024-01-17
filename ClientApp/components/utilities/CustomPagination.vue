<template>
  <div
    class=" w-100 d-flex align-items-center flex-wrap justify-content-center"
    v-if="totalPages && totalPages.length > 1"
  >
    <button
      :disabled="activePage === 1"
      @click="setNewPage(activePage - 1)"
      class="bg-purple p-1 rounded text-white"
    >
      قبلی
    </button>
    <button
      v-if="totalPages.length > 3 && activePage > 3"
      @click="setNewPage(1)"
      class="bg-purple p-1 rounded text-white"
      :class="
        activePage === 1
          ? 'underline underline-offset-2 rounded-full bg-primary text-white'
          : ''
      "
    >
      {{ 1 }}
    </button>
    <span
      v-if="totalPages.length > 3 && activePage > 3"
      class="m-3  cursor-text"
    >
      ...
    </span>
    <button
      class="btn"
      v-for="i in filteredPage.filter(e=> e !==0)"
      :key="i"
      :class="
        activePage === i
          ? 'underline underline-offset-2 rounded-full bgPurple text-white'
          : ''
      "
      @click="setNewPage(i)"
    >
      {{ i }}
    </button>
    <span
      v-if="totalPages.length > 3 && activePage < totalPages.length - 3"
      class="m-3 w-9 h-9 cursor-text"
    >
      ...
    </span>
    <button
      v-if="totalPages.length > 3"
      @click="setNewPage(totalPages.length)"
      class="btn"
      :class="
        activePage === totalPages.length
          ? 'underline underline-offset-2 rounded-full bg-purple text-white'
          : ''
      "
    >
      {{ totalPages.length }}
    </button>
    <button
      :disabled="activePage === totalPages.length"
      @click="setNewPage(activePage + 1)"
      class="bg-purple  p-1 rounded text-white"
    >
      بعدی
    </button>
  </div>
</template>
<script>
export default {
  emits: ["PageChanged"],
  props: {
    totalPages: {
      type: Array,
      required: true,
    },
    activePage: {
      type: [Number, String],
      required: true,
    },
  },
  data() {
    return {

    };
  },
  mounted(){

  },
  methods: {
    GetClassName(i){
      if(i === this.activePage){
        return'ActiveClass'
      }else{
        return ''
      }
    },
    setNewPage(pageNumber) {
      this.$emit("PageChanged", pageNumber);
    },
  },
  computed: {
    filteredPage() {
      // Okay, idk what's happening here, but im proud of it
      // Half of credit goes to @amoowily
      if (this.totalPages && this.totalPages.length > 3) {
        let filteredPaginationArray = [];
        if (this.activePage < 3) {
          // Rendering first 8 pages
          for (let i = 1; i <= 3; i++) {
            filteredPaginationArray.push(i);
          }
        } else if (this.activePage < this.totalPages.length - 2) {
          // Rendering middle pages
          filteredPaginationArray = [];
          for (let i = this.activePage - 2; i <= this.activePage + 2; i++) {
            filteredPaginationArray.push(i);
          }
        } else {
          // Rendering last pages
          filteredPaginationArray = [];
          for (let i = this.activePage - 3; i <= this.totalPages.length - 1; i++) {
            filteredPaginationArray.push(i);
          }
        }
        return filteredPaginationArray;
      } else {
        return this.totalPages;
      }
    },
  },
};
</script>
<style>
.ActiveClass{
  color: white;
  background-color: #00adef;
}

.bgPurple{
  background-color: #b44aff!important;
}

</style>
