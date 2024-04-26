<template>
  <form class="main-form">
    <SearchForm class="main-form__search-form"/>

    <h3 class="main-form__option-title">Или</h3>

    <div class="main-form__option-btns">
      <button type="button" class="main-form__option-btn">
        Выберите файл
      </button>
      
      <button 
        type="button" 
        class="main-form__option-btn"
      >
        Сделайте снимок
      </button>
    </div>

    <WebCamera class="main-form__web-camera"/>
    <!-- <ProcessedImage 
      imageAlt="Загруженное изображение" 
      renewBtnTitle="Выбрать другое"
      class="main-form__processed-image"
    /> -->

    <button type="submit" class="main-form__search-btn">Искать</button>
  </form>
</template>

<script setup>
  import SearchForm from "@/components/SearchForm.vue";
  // import ProcessedImage from "@/components/ProcessedImage.vue";
  import WebCamera from "@/components/camera/WebCamera.vue";
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

    &__search-form {
      margin-bottom: 20px;
    }

    &__option-title {
      margin: 20px 0;
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
    }

    &__option-btn {
      .gradient-btn();

      margin: 0 10px;
    }

    &__web-camera,
    &__processed-image {
      margin: 30px 0;
    }

    &__search-btn {
      position: relative;
      max-width: 520px;
      border-radius: 0 0 5px 5px;
      background-color: @indian_green;
      box-shadow: rgba(@black, 0.3) 0 5px 15px;
      font-weight: 600;
      font-size: 20px;
      transition: color 0.3s;
      overflow: hidden;

      &:hover {
        color: @dandelion;

        &::before {
          content: "";
          position: absolute;
          top: 0;
          left: -100px;
          width: 100px;
          height: 100%;
          background-image: linear-gradient(
            120deg,
            rgba(@white, 0) 30%,
            rgba(@white, 0.8),
            rgba(@white, 0) 70%
          );
          animation: shine 1.5s infinite linear;
        }
      }
    }
  }
</style>