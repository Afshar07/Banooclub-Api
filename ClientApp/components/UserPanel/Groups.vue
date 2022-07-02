<template>
  <div class="widget card p-3 border-t">
    <div class="tw-flex tw-items-center tw-justify-between tw-mb-2">
      <div>
        <h4 class="tw-text-lg tw-font-semibold"> گروه ها </h4>
      </div>
      <nuxt-link to="#" class="tw-text-blue-600 text-decoration-none"> دیدن همه</nuxt-link>
    </div>
    <div>
      <GroupsItem v-for="i in 5"/>
    </div>
  </div>


</template>

<script>
import GroupsItem from "./GroupsItem";
export default {
  name: "Groups",
  components: {GroupsItem}
}
</script>

<style scoped>

</style>
