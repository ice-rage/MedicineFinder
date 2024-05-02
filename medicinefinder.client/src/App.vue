<template>
  <div class="app">
    <PageHeader class="app__page-header"/>
    <MainBlock class="app__main-form"/>
    <!-- <DefaultPage class="app__default-page"/> -->
  </div>
</template>

<script setup>
  import PageHeader from "@/components/page/PageHeader.vue";
  import MainBlock from "@/components/MainBlock.vue";
  // import DefaultPage from "@/DefaultPage.vue";
</script>

<style lang="less">
  .app {
    flex: 1 0 auto;
  }
</style>