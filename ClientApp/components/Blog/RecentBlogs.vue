<template>
  <div class="col-lg-4 tw-pt-5">
    <div>
      <h2 class="tw-text-xl tw-font-semibold tw-mb-2"> مقالات اخیر </h2>
      <ul class="tw-space-y-3">
        <li v-for="i in 5">
          <div class="tw-flex tw-flex-col tw-items-start my-2">
            <p class="m-0">Lorem ipsum dolor sit amet,um dignissimos eligendi illum in, ipsum nulla quae quia suscipit! Eius, rerum.</p>
            <div class="tw-flex tw-items-center tw-justify-end tw-gap-2">
              <div class="tw-flex tw-items-center tw-gap-1">
                <small class=" tw-text-stone-400">11</small>
                <i class="fas fa-comments tw-text-stone-400"></i>
              </div>
              <small class="text-secondary">{{new Date(Date.now()).toLocaleDateString('fa-IR')}}</small>
            </div>
          </div>
        </li>
      </ul>

    </div>
  </div>
</template>

<script>
export default {
  name: "TopCommenters"
}
</script>

<style scoped>

</style>
