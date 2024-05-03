<template>
  <div class="main-block">
    <SearchBox 
      class="main-block__search-form"
      @showResultEvent="showResult"
    />

    <h3 class="main-block__option-title">Или</h3>

    <div class="main-block__option-btns">
      <button 
        type="button" 
        :class="isImageLoaderVisible 
          ? 'main-block__option-btn--active' 
          : 'main-block__option-btn'"
        @click="toggleChildComponents(
          accessChildComponentRefs.isImageLoaderVisible,
          accessChildComponentRefs.isWebCameraVisible
        )"
      >
        <SvgImage 
          width="24" 
          height="24" 
          class="main-block__option-btn-icon"
        />
        Выбрать файл
      </button>
      
      <button 
        type="button" 
        :class="isWebCameraVisible 
          ? 'main-block__option-btn--active' 
          : 'main-block__option-btn'"
        @click="toggleChildComponents(
          accessChildComponentRefs.isWebCameraVisible,
          accessChildComponentRefs.isImageLoaderVisible,
        )"
      >
        <SvgCamera
          width="24"
          height="24"
          class="main-block__option-btn-icon"
        />
        Сделать снимок
      </button>
    </div>

    <ImageLoader 
      class="main-block__image-uploader"
      @toggleSearchBtnEvent="toggleSearchBtn" 
      v-if="isImageLoaderVisible"/>

    <WebCamera 
      class="main-block__web-camera"
      @toggleSearchBtnEvent="toggleSearchBtn"
      v-if="isWebCameraVisible"/>

    <button 
      type="button" 
      class="main-block__search-btn"
      @click="searchMedicineByImage()"
      v-if="hasImageToProcess"
    >
      Искать
    </button>

    <ResultingInfo 
      class="main-block__resulting-info"
      :data="medicineInfo"
      v-if="medicineInfo"/>

    <ErrorMessage
      :errorCode="errorCode" 
      class="main-block__error-message" 
      v-if="hasError"/>
  </div>
</template>

<script>
  import { defineComponent, reactive, ref, computed } from "vue";
  import axios from "axios";

  import SearchBox from "@/components/SearchBox.vue";
  import ImageLoader from "@/components/ImageLoader.vue";
  import WebCamera from "@/components/WebCamera.vue";
  import SvgImage from "@/components/icons/SvgImage.vue";
  import SvgCamera from "@/components/icons/SvgCamera.vue";
  import ResultingInfo from "@/components/ResultingInfo.vue";
  import ErrorMessage from "@/components/ErrorMessage.vue";

  const toAccessChildComponentRefs = refs => ({
    ...refs,
    accessChildComponentRefs: {
      ...refs
    }
  });

  export default defineComponent({
    components: {
      SearchBox,
      ImageLoader,
      SvgImage,
      WebCamera,
      SvgCamera,
      ResultingInfo,
      ErrorMessage,
    },
    setup() {
      const errorCode = ref("");
      const isImageLoaderVisible = ref(false);
      const isWebCameraVisible = ref(false);
      const hasImageToProcess = ref(false);
      const imageToProcess = reactive({});
      const medicineInfo = ref();

      const hasError = computed(() => errorCode.value !== "");

      const setError = (code) => errorCode.value = code;

      const clearError = () => errorCode.value = "";

      const toggleChildComponents = (firstComponentToggle, 
        secondComponentToggle) => {
        firstComponentToggle.value = !firstComponentToggle.value;

        if (secondComponentToggle.value) {
          secondComponentToggle.value = false;
        }

        hasImageToProcess.value = false;

        return firstComponentToggle.value;
      }

      const toggleSearchBtn = (loadedImage, toggleValue) => {
        hasImageToProcess.value = toggleValue;
        imageToProcess.value = loadedImage;
        console.log(`Данные внутри MainBlock :${imageToProcess.value}`);
      }

      const showResult = (data) => {
        medicineInfo.value = data;
      }

      function searchMedicineByImage() {
        console.log(`Внутри вызова функции поиска: ${imageToProcess.value}`);
        
        axios.post('medicinefinder/uploadimage', { imageToProcess })
          .then(response => {
            console.log(`Ответ сервера: ${response.data}`);
          })
          .catch(error => {
            console.log(`Произошла ошибка: ${error.message}`);
          });
      }

      return {
        errorCode,
        hasImageToProcess,
        imageToProcess,
        medicineInfo,
        ...toAccessChildComponentRefs({
          isImageLoaderVisible,
          isWebCameraVisible,
        }),
        hasError,
        setError,
        clearError,
        toggleChildComponents,
        toggleSearchBtn,
        showResult,
        searchMedicineByImage,
      };
    }
  });
</script>

<style lang="less">
  .main-block {
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

    &__resulting-info,
    &__error-message {
      margin-top: 40px;
    }
  }
</style>