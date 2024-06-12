<template>
  <div class="main-layout">
    <SearchBox class="main-layout__search-form"/>

    <h3 class="main-layout__option-title">Или</h3>

    <div class="main-layout__option-btns">
      <OptionButton
        class="main-layout__option-btn"
        :isActive="store.isOptionVisible('imageLoader')"
        :icon="SvgImage"
        text="Выберите файл"
        @click="toggleOption('imageLoader')"
      />
      <OptionButton
        class="main-layout__option-btn"
        :isActive="store.isOptionVisible('webCamera')"
        :icon="SvgCamera"
        text="Сделайте снимок"
        @click="toggleOption('webCamera')"
      />
    </div>

    <ImageLoader 
      v-if="store.isOptionVisible('imageLoader')"
      class="main-layout__image-loader"
    />

    <WebCamera 
      v-if="store.isOptionVisible('webCamera')"
      class="main-layout__web-camera"
    />

    <button 
      v-if="store.hasImage"
      type="button" 
      class="main-layout__search-btn"
      @click="uploadImage"
    >
      Искать
    </button>

    <div v-if="store.isDataLoading" class="main-layout__loader"></div>

    <ResultingInfo 
      v-if="store.hasMedicineData"
      class="main-layout__resulting-info"
    />

    <ErrorMessage
      v-if="store.hasError"
      :message="store.getErrorMessage" 
      class="main-layout__error-message" 
    />
  </div>
</template>

<script setup>
  import SvgImage from "./icon/SvgImage.vue";
  import SvgCamera from "./icon/SvgCamera.vue";

  const store = useStore();

  const { toggleOption } = store;

  const imageToProcess = ref();

  const uploadImage = () => {
    showDataLoading();

    const uploadingData = new FormData();
    uploadingData.append("encodedImage", imageToProcess.value);
  }
</script>

<style lang="less">
  .main-layout {
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
    &__image-loader {
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