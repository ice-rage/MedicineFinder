<template>
  <div class="image-loader">
    <div class="image-loader__container" v-show="loadedImageUrl === ''">
      <div 
        ref="dragArea" 
        class="image-loader__drag-area drag-area"
        @dragover="enterdragArea($event)"
        @dragleave="leavedragArea($event)"
        @drop="dropFile($event)"
      >
        <div class="drag-area__icon-wrapper">
          <i class="drag-area__icon fas fa-images"></i>
        </div>

        <span 
          class="drag-area__select-btn"
          @click="openFileDialog()"
        >
          Выберите изображение
        </span>

        <span 
          ref="dragText" 
          class="drag-area__drag-text"
        >
          или перетащите файл сюда
        </span>

        <input 
          ref="imageInput"
          type="file"
          class="drag-area__image-input"
          accept="image/jpeg, image/jpg, image/png"
          @change="selectFile($event)"
        />

        <span class="drag-area__supported-formats">
          Поддерживаемые форматы: JPEG, JPG, PNG
        </span>
      </div>
    </div>

    <ProcessedImage
      :imageSrc="loadedImageUrl"
      imageAlt="Загруженное изображение"
      renewBtnTitle="Выбрать другое"
      class="image-loader__processed-image"
      @renewImageEvent="selectImageFile()"
      @removeImageEvent="loadedImageUrl = ''"
      v-if="loadedImageUrl !== ''"
    />
  </div>
</template>

<script setup>
  import { ref } from "vue";
  import ProcessedImage from "@/components/ProcessedImage.vue";
  
  const dragArea = ref();
  const dragText = ref();
  const imageInput = ref();
  const loadedImageUrl = ref("");

  const enterdragArea = (event) => {
    event.preventDefault();
    dragArea.value.classList.add("drag-area--active");
    dragText.value.textContent = "Отпустите для загрузки";
    console.log("File is inside the drag area");
  }

  const leavedragArea = () => {
    dragArea.value.classList.remove("drag-area--active");
    console.log("File left the drag area");
    dragText.value.textContent = "или перетащите файл сюда";
  }

  const dropFile = (event) => {
    event.preventDefault();
    console.log("File is dropped in drag area");

    dragArea.value.classList.add("drag-area--active");

    const success = loadFile(event.dataTransfer.files);
  
    if (!success) {
      dragArea.value.classList.remove("drag-area--active");
    }
  }

  const loadFile = (selectedFiles) => {
    if (FileReader && selectedFiles && selectedFiles.length) {
      const fileReader = new FileReader();
      const selectedFile = selectedFiles[0];

      let validExtensions = ["image/jpeg", "image/jpg", "image/png"];

      let selectedFileExtension = selectedFile.type;
      console.log(`File extension: ${selectedFileExtension}`);

      if (validExtensions.includes(selectedFileExtension)) {
        fileReader.onload = () => {
          loadedImageUrl.value = fileReader.result;
        }

        fileReader.onerror = () => {
          console.log(`Произошла ошибка: ${fileReader.error}`);
        }

        fileReader.readAsDataURL(selectedFile);

        return true;
      } else {
        alert("Можно загружать только изображения");

        return false;
      }
    }
  }

  const openFileDialog = () => imageInput.value.click();

  const selectFile = (event) => {
    loadFile(event.target.files);
  }
</script>

<style lang="less">
  .image-loader {
    display: flex;
    justify-content: center;

    &__container {
      max-width: 600px;
    }

    &__drag-area {
      @media @bw768 {
        padding: 0 30px;
      }
    }
  }

  .drag-area {
    box-sizing: border-box;
    display: flex;
    flex-direction: column;
    align-items: center;
    border: 3px dashed fade(@light_gray, 30%);
    border-radius: 10px;

    &--active {
      border: 2px solid @islamic_green;
    }

    &__icon-wrapper {
      align-self: center;
      font-size: 50px;
      color: @islamic_green;
      text-align: center;

      @media @bw1170 {
        font-size: 40px;
      }
    }

    &__select-btn {
      color: @islamic_green;
      font-size: 20px;
      font-weight: 600;
      text-align: center;
      cursor: pointer;

      @media @bw1170 {
        font-size: 18px;
      }

      @media @bw768 {
        font-size: 16px;
      }
    }

    &__drag-text {
      color: @black;
      font-size: 20px;
      font-weight: 500;
      text-align: center;

      @media @bw1170 {
        font-size: 18px;
      }

      @media @bw768 {
        font-size: 16px;
      }
    }

    &__image-input {
      display: none;
    }

    &__supported-formats {
      margin: 10px 0 15px 0;
      color: @gray;
      font-size: 12px;
      text-align: center;

      @media @bw1170 {
        font-size: 10px;
      }
    }
  }
</style>