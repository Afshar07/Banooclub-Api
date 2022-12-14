<template>
  <div class="card p-3 ">
    <div class="tw-flex tw-items-center tw-justify-between tw-mb-2">
      <div>
        <h4 class="tw-text-lg tw-font-semibold"> گروه های چت </h4>
      </div>
    </div>
    <div>
      <GroupsItem v-for="(item,idx) in Groups" :item="item" :key="idx"/>
    </div>
  </div>


</template>

<script>
import GroupsItem from "./GroupsItem";

export default {
  name: "Groups",
  components: {GroupsItem},
  data(){
    return{
      Groups:[]
    }
  },
  methods:{

  },
  async fetch(){
    try {
      const res =await this.$repositories.GetAllGroups.GetAllGroups()
          this.Groups = res.data
      console.log(this.Groups)
    }catch (e){
      console.log(e)
    }
  },
}
</script>

<style scoped>

</style>
