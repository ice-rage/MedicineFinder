<template>
  <header class="page-header">
    <div class="page-header__container">
      <div class="page-header__logo-wrap">
        <SvgHome class="page-header__logo"/>

        <h4 class="page-header__logo-text">Поиск лекарств</h4>
      </div>
    </div>
  </header>
</template>

<script setup>
  import SvgHome from "@/components/icons/SvgHome.vue";
</script>

<style lang="less">
  .page-header {
    background-color: @islamic_green;

    &__container {
      .container();
      
      height: 100px;
      display: flex;
      align-items: center;
    }

    &__logo-wrap {
      display: flex;
      align-items: flex-end;
    }

    &__logo {
      width: 36px;
      color: @white;
    }

    &__logo-text {
      margin-left: 30px;
      color: @white;
      font-family: @font3;
      font-weight: 700;
      font-size: 26px;
    }
  }
</style>