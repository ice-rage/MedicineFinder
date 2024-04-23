<template>
  <div class="app">
    <PageHeader/>
    <MainBlock/>
  </div>
</template>

<script setup>
  import PageHeader from "@/components/page/PageHeader.vue";
  import MainBlock from "@/components/MainBlock.vue";
</script>

<style lang="less">
  .app {
    flex: 1 0 auto;
  }
</style>