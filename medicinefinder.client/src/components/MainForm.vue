<template>
  <form class="main-form">
    <SearchForm/>

    <h3 class="main-form__option-title">Или</h3>

    <div class="main-form__option-btns">
      <button type="button" class="main-form__option-btn">Выберите файл</button>
      <button 
        type="button" 
        class="main-form__option-btn"
      >
        Сделайте снимок
      </button>
    </div>

    <SelectedImage/>
    <!-- <WebCamera/> -->

    <button type="submit" class="main-form__search-btn">Искать</button>
  </form>
</template>

<script setup>
  import SearchForm from "@/components/SearchForm.vue";
  import SelectedImage from "@/components/SelectedImage.vue";
  // import WebCamera from "@/components/WebCamera.vue";
</script>

<style lang="less">
  .main-form {
    display: flex;
    flex-direction: column;
    align-items: center;
    max-width: 800px;
    margin: 50px auto;
    padding: 30px;
    border-radius: 5px;
    box-shadow: rgba(@shadow_gray, 0.2) 0 7px 30px 0;

    &__option-title {
      color: @islamic_green;
      font-family: @font3;
      font-weight: 600;
      font-size: 24px;
      line-height: 1.28;
      text-align: center;
      text-transform: uppercase;
    }

    &__option-btns {
      display: flex;
      justify-content: space-between;
      max-width: 400px;
      margin: 40px 0;
    }

    &__option-btn {
      .gradient-btn();

      margin: 0 10px;
    }

    &__search-btn {
      position: relative;
      max-width: 500px;
      border-radius: 0 0 5px 5px;
      background-color: @indian_green;
      box-shadow: rgba(@black, 0.3) 0 5px 15px;
      font-weight: 600;
      font-size: 20px;
      overflow: hidden;

      &:hover {
        &::before {
          content: "";
          position: absolute;
          top: 0;
          left: -100px;
          width: 100px;
          height: 100%;
          background-image: linear-gradient(
            120deg,
            rgba(@white, 0) 30%, /* Прозрачность начала блика */
            rgba(@white, 0.8), /* Цвет блика */
            rgba(@white, 0) 70% /* Прозрачность конца блика */
          );
          animation: shine 1.5s infinite linear; /* Анимация блика */
        }
      }
    }
  }
</style>