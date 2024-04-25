<template>
  <div class="app">
    <PageHeader/>
    <MainForm/>
  </div>
</template>

<script setup>
  import PageHeader from "@/components/page/PageHeader.vue";
  import MainForm from "@/components/MainForm.vue";
</script>

<style lang="less">
  .app {
    flex: 1 0 auto;
  }
</style>