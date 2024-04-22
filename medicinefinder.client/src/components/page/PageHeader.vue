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
    background-color: @brown;

    &__container {
      .container();
      
      height: 60px;
      display: flex;
      align-items: center;
    }

    &__logo-wrap {
      display: flex;
    }

    &__logo {
      width: 36px;
      color: @light_green;
    }

    &__logo-text {
      margin-left: 10px;
      color: @white;
      font-size: 26px;
      font-family: @font1;
    }

    &__logo-text-red {
      color: @dark_orange;
    }
  }
</style>