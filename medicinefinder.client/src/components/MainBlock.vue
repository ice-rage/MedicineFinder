<template>
  <main class="main-block">
    <SearchForm/>

    <h3 class="main-block__option-title">Или</h3>

    <div class="main-block__btns">
      <button type="button" class="main-block__btn">Выберите файл</button>
      <button 
        type="button" 
        class="main-block__btn"
        :isWebCameraOn="isWebCameraOn"
        @click="toggleWebCamera()"
      >
        Сделайте снимок
      </button>
    </div>

    <WebCamera v-if="isWebCameraOn === true"/>
  </main>
</template>

<script setup>
  import { ref } from "vue";

  import SearchForm from "./blocks/SearchForm.vue";
  import WebCamera from "./blocks/WebCamera.vue";

  let isWebCameraOn = ref(false);

  function toggleWebCamera() {
    isWebCameraOn.value = !isWebCameraOn.value;
  }
</script>

<style lang="less">
  .main-block {
    display: flex;
    flex-direction: column;
    align-items: center;
    max-width: 600px;
    margin: 50px auto;
    padding: 30px;
    border-radius: 5px;
    box-shadow: rgba(@shadow_gray, 0.2) 0 7px 30px 0;

    &__option-title {
      margin: 0 20px 40px;
      color: @islamic_green;
      font-family: @font3;
      font-weight: 600;
      font-size: 24px;
      line-height: 1.28;
      text-align: center;
      text-transform: uppercase;
    }

    &__btns {
      display: flex;
      justify-content: space-between;
      max-width: 400px;
    }

    &__btn {
      .gradient-btn();

      margin: 10px;
      padding: 15px 20px;
      border-radius: 5px;
    }
  }
</style>