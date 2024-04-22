<template>
  <main class="main-block">
    <SearchForm class="main-block__form search-form"/>
    <div class="main-block__buttons">
      <button type="button" class="main-block__btn">Выберите файл</button>
      <button type="button" class="main-block__btn">Сделайте снимок</button>
    </div>
  </main>
</template>

<script setup>
  import SearchForm from "./blocks/SearchForm.vue";
</script>

<style lang="less">
  .main-block {
    display: flex;
    flex-direction: column;
    align-items: center;
    max-width: 650px;
    margin: 0 auto;
    padding-top: 100px;

    &__buttons {
      display: flex;
      justify-content: space-between;
      width: 370px;
    }

    &__btn {
      width: 170px;
      padding: 15px;
      border-radius: 5px;
      box-shadow: rgba(0, 0, 0, 0.35) 0 5px 15px;
      background-color: @islamic_green;
      color: @white;
      font-family: @font1;
      font-weight: 400;

      &:hover {
        @media (hover: hover) {
          background-color: #56de47;
        }
      }
    }
  }
</style>