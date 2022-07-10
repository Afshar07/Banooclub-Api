<template>
  <div class="col-md-12  d-flex justify-content-center my-5 border-top pt-2">
    <nav aria-label="Page navigation example">
      <ul  class="pagination px-0">
        <li class="page-item" @click="DecrementSelectedPageId">
          <a class="page-link" href="#" aria-label="Previous">
            <span aria-hidden="true">&laquo;</span>
          </a>
        </li>
        <li @click="SetSelectedPageId(i)" class="page-item" v-for="i in pages" :key="i"><a class="page-link" :class="GetClassName(i)" href="#">{{ i }}</a></li>

        <li class="page-item" @click="IncrementSelectedPageId">
          <a class="page-link" href="#" aria-label="Next">
            <span aria-hidden="true">&raquo;</span>
          </a>
        </li>
      </ul>
    </nav>
  </div>
</template>

<script>
    export default {
        name: "Pagination",
        props:['pages'],
        data(){
            return{
                SelectedPageId:1,
                totalPages:[]
            }
        },
        methods:{
            GetClassName(id) {
                if (id === this.SelectedPageId) {
                    return 'ActivePage'
                } else {
                    return ''
                }
            },
            SetSelectedPageId(id) {
                this.SelectedPageId = id
                this.$emit('PageChanged',id)
            },
            IncrementSelectedPageId() {

                this.SelectedPageId++
                if (this.SelectedPageId > this.pages.length) {

                    this.SelectedPageId = 1
                }
                this.$emit('PageChanged',this.SelectedPageId)
            },
            DecrementSelectedPageId() {
                this.SelectedPageId--
                if (this.SelectedPageId < 1) {

                    this.SelectedPageId = 1
                }
                this.$emit('PageChanged',this.SelectedPageId)
            },
        }
    }
</script>

<style scoped>
  .ActivePage {
    color: white;
    background-color: #00adef;
  }
</style>
