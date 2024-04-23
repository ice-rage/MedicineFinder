<template>
  <header class="page-header">
    <div class="page-header__container">
      <div class="page-header__logo-wrap">
        <a href="/" class="page-header__logo" title="На главную">
          <SvgLeaf class="page-header__logo-icon"/>
        </a>

        <h4 class="page-header__logo-text">Поиск лекарств</h4>
      </div>
    </div>
  </header>
</template>

<script setup>
  import SvgLeaf from "@/components/icons/SvgLeaf.vue";
</script>

<style lang="less">

  .page-header {
    background-color: @islamic_green;
    box-shadow: rgba(@black, 0.2) 0 5px 15px 0;

    &__container {
      .container();
      
      height: 80px;
      display: flex;
      align-items: center;
    }

    &__logo-wrap {
      display: flex;
      align-items: flex-end;
    }

    &__logo {
      flex-shrink: 0;
      width: 36px;
      height: 36px;
      font-size: 0;
    }

    &__logo-icon {
      width: 100%;
      height: 100%;
    }

    &__logo-text {
      margin-left: 30px;
      font-family: @font2;
      font-weight: 700;
      font-size: 26px;
    }
  }
</style>