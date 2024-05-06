<template>
  <div class="main-block">
    <SearchBox 
      class="main-block__search-form"
      @showDataLoadingEvent="showDataLoading"
      @showResultEvent="showResult"
    />

    <h3 class="main-block__option-title">Или</h3>

    <div class="main-block__option-btns">
      <OptionButton
        class="main-block__option-btn"
        v-model="isImageLoaderVisible"
        :icon="SvgImage"
        :text="'Выберите файл'"
        @update:modelValue="(toggleSearchBtn(null, modelValue))"
      />
      <OptionButton
        class="main-block__option-btn"
        v-model="isWebCameraVisible"
        :icon="SvgCamera"
        :text="'Сделайте снимок'"
        @update:modelValue="(toggleSearchBtn(null, modelValue))"
      />
    </div>

    <ImageLoader 
      class="main-block__image-uploader"
      @hideSiblingEvent="isWebCameraVisible = false"
      @toggleSearchBtnEvent="toggleSearchBtn"
      v-if="isImageLoaderVisible"
    />

    <WebCamera 
      class="main-block__web-camera"
      @hideSiblingEvent="isImageLoaderVisible = false"
      @toggleSearchBtnEvent="toggleSearchBtn"
      v-if="isWebCameraVisible"
    />

    <button 
      type="button" 
      class="main-block__search-btn"
      @click="uploadImage()"
      v-if="hasImageToProcess"
    >
      Искать
    </button>

    <div class="main-block__loader" v-if="isDataLoading"></div>

    <ResultingInfo 
      class="main-block__resulting-info"
      :data="medicineInfo"
      v-if="medicineInfo && !isDataLoading && !hasError"/>

    <ErrorMessage
      :message="errorMessage" 
      class="main-block__error-message" 
      v-if="hasError"/>
  </div>
</template>

<script setup>
  import { ref, computed } from "vue";
  import axios from "axios";
  import config from "@/configs";
  import _ from "lodash";

  import SearchBox from "@/components/SearchBox.vue";
  import ImageLoader from "@/components/ImageLoader.vue";
  import WebCamera from "@/components/WebCamera.vue";
  import OptionButton from "@/components//templates/OptionButton.vue";
  import SvgImage from "@/components/icons/SvgImage.vue";
  import SvgCamera from "@/components/icons/SvgCamera.vue";
  import ResultingInfo from "@/components/ResultingInfo.vue";
  import ErrorMessage from "@/components/ErrorMessage.vue";

  const errorCode = ref("");
  const isImageLoaderVisible = ref();
  const isWebCameraVisible = ref();
  const hasImageToProcess = ref();
  const imageToProcess = ref();
  const isDataLoading = ref();
  const medicineInfo = ref();

  const hasError = computed(() => errorCode.value !== "");

  const errorMessage = computed(() => errorCode.value !== "" 
    ? _.find(config.errors, { code: errorCode.value }).message
    : "");

  const setError = (code) => errorCode.value = code;

  const clearError = () => errorCode.value = "";

  const toggleSearchBtn = (loadedImage, toggleValue) => {
    imageToProcess.value = loadedImage;
    hasImageToProcess.value = toggleValue;
  }
      
  const showDataLoading = () => {
    isDataLoading.value = true;
    clearError();
  }

  const showResult = (promise) => {
    promise
      .then(response => {
        medicineInfo.value = response.data;
      }, error => setError(error.response.status.toString()))
      .then(() => isDataLoading.value = false);
  }

  const uploadImage = () => {
    showDataLoading();

    const uploadingData = new FormData();
    uploadingData.append("encodedImage", imageToProcess.value);

    showResult(new Promise((resolve, reject) => {
      axios
        .post("medicinefinder", uploadingData)
          .then(response => {
            resolve(response);
          }, error =>  {
            reject(error);
          });
    }));
  }
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

    &__loader {
      width: 40px;
      height: 40px;
      margin: 30px;
      border: 7px dotted @islamic_green;
      border-radius: 50%;
      display: inline-block;
      position: relative;
      box-sizing: border-box;
      animation: rotate 2s linear infinite;
    }

    &__resulting-info,
    &__error-message {
      margin-top: 40px;
    }
  }
</style>