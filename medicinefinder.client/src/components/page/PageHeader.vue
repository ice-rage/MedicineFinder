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
    .green-gradient();
    
    box-shadow: rgba(@black, 0.2) 0 5px 15px 0;

    &__container {
      .container();
      
      display: flex;
      align-items: center;
      height: 80px;

      @media @bw768 {
        height: 60px;
      }
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

      @media @bw768 {
        width: 24px;
        height: 24px;
      }
    }

    &__logo-icon {
      width: 100%;
      height: 100%;
    }

    &__logo-text {
      margin-left: 30px;
      color: @white;
      font-family: @font2;
      font-weight: 700;
      font-size: 26px;

      @media @bw1170 {
        font-size: 23px;
      }

      @media @bw768 {
        margin-left: 20px;
        font-size: 20px;
      }
    }
  }
</style>