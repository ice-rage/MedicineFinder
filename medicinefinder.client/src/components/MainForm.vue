<template>
  <form class="main-form">
    <SearchForm class="main-form__search-form"/>

    <h3 class="main-form__option-title">Или</h3>

    <div class="main-form__option-btns">
      <button 
        type="button" 
        :class="isImageLoaderVisible 
          ? 'main-form__option-btn--active' 
          : 'main-form__option-btn'"
        @click="toggleChildComponents(
          accessChildComponentRefs.isImageLoaderVisible,
          accessChildComponentRefs.isWebCameraVisible
        )"
      >
        <SvgImage 
          width="24" 
          height="24" 
          class="main-form__option-btn-icon"
        />
        Выбрать файл
      </button>
      
      <button 
        type="button" 
        :class="isWebCameraVisible 
          ? 'main-form__option-btn--active' 
          : 'main-form__option-btn'"
        @click="toggleChildComponents(
          accessChildComponentRefs.isWebCameraVisible,
          accessChildComponentRefs.isImageLoaderVisible,
        )"
      >
        <SvgCamera
          width="24"
          height="24"
          class="main-form__option-btn-icon"
        />
        Сделать снимок
      </button>
    </div>

    <ImageLoader 
      class="main-form__image-uploader"
      @toggleSearchBtnEvent="toggleSearchBtn" 
      v-if="isImageLoaderVisible"/>

    <WebCamera 
      class="main-form__web-camera"
      @toggleSearchBtnEvent="toggleSearchBtn"
      v-if="isWebCameraVisible"/>

    <button 
      type="submit" 
      class="main-form__search-btn"
      v-if="hasImageToProcess"
    >
      Искать
    </button>

    <!-- <ResultingInfo class="main-form__resulting-info"/> -->
  </form>
</template>

<script>
  import { defineComponent, ref } from "vue";

  import SearchForm from "@/components/SearchForm.vue";
  import ImageLoader from "@/components/ImageLoader.vue";
  import WebCamera from "@/components/WebCamera.vue";
  import SvgImage from "@/components/icons/SvgImage.vue";
  import SvgCamera from "@/components/icons/SvgCamera.vue";

  const toAccessChildComponentRefs = refs => ({
    ...refs,
    accessChildComponentRefs: {
      ...refs
    }
  });

  export default defineComponent({
    components: {
      SearchForm,
      ImageLoader,
      SvgImage,
      WebCamera,
      SvgCamera,
    },
    setup() {
      const isImageLoaderVisible = ref(false);
      const isWebCameraVisible = ref(false);
      const hasImageToProcess = ref(false);

      const toggleChildComponents = (firstComponentToggle, 
        secondComponentToggle) => {
        firstComponentToggle.value = !firstComponentToggle.value;

        if (secondComponentToggle.value) {
          secondComponentToggle.value = false;
        }

        hasImageToProcess.value = false;

        return firstComponentToggle.value;
      }

      const toggleSearchBtn = (toggleValue) => {
        hasImageToProcess.value = toggleValue;
      }

      return {
        hasImageToProcess,
        ...toAccessChildComponentRefs({
          isImageLoaderVisible,
          isWebCameraVisible,
        }),
        toggleChildComponents,
        toggleSearchBtn,
      };
    }
  });
</script>

<style lang="less">
  .main-form {
    box-sizing: border-box;
    display: flex;
    flex-direction: column;
    align-items: center;
    max-width: 62.5%;
    margin: 50px auto;
    padding: 30px;
    border-radius: 10px;
    box-shadow: rgba(@shadow_gray, 0.2) 0 7px 30px 0;

    @media @bw1170 {
      max-width: 73.5%;
    }

    @media @bw768 {
      max-width: 87.5%;
      margin: 30px auto;
      padding: 25px;
    }

    &__search-form {
      margin-bottom: 10px;
    }

    &__option-title {
      margin: 20px 0 30px;
      color: @islamic_green;
      font-family: @font3;
      font-weight: 600;
      font-size: 24px;
      line-height: 1.28;
      text-align: center;
      text-transform: uppercase;

      @media @bw768 {
        margin: 10px 0 20px;
        font-size: 18px;
      }
    }

    &__option-btns {
      display: flex;
      justify-content: space-between;
      max-width: 400px;

      @media @bw768 {
        max-width: 150px;
      }
    }

    &__option-btn,
    &__option-btn--active {
      @media @bw768 {
        padding: 10px;
        font-size: 0;
      }

      & + button {
        margin-left: 20px;

        @media @bw768 {
          margin-left: 25px;
        }
      }
    }

    &__option-btn {
      .gradient-btn();

      &--active {
        background-color: @indian_green;
      }
    }

    &__option-btn-icon {
      display: none;

      @media @bw768 {
        display: flex;
      }
    }

    &__web-camera,
    &__image-uploader {
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

      @media @bw1170 {
        font-size: 18px;
      }

      @media @bw768 {
        font-size: 15px;
      }

      &:hover {
        @media(hover: hover) {
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
  }
</style>