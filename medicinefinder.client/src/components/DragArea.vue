<template>
  <div
    class="drag-area"
    :class="{ 'drag-area--active' : dragText }"
    @dragover="enterDragArea($event)"
    @dragleave="leaveDragArea($event)"
    @drop="dropFile($event)"
  >
    <div class="drag-area__icon-wrapper">
      <i class="drag-area__icon fas fa-images"></i>
    </div>

    <span class="drag-area__select-btn" @click="openFileDialog">
      Выберите изображение
    </span>

    <span class="drag-area__drag-text">
      {{ dragText ? dragText : "или перетащите файл сюда" }}
    </span>

    <input
      ref="imageInput"
      type="file"
      class="drag-area__image-input"
      accept="image/jpeg, image/png"
      @change="selectFile($event)"
    />

    <span class="drag-area__supported-formats">
      Поддерживаемые форматы: JPEG, PNG
    </span>
  </div>
</template>

<script setup>
  import toastr from "toastr";
  
  toastr.options = {
    "positionClass": "toast-bottom-left",
  };
  
  const dragText = ref();
  const imageInput = ref();
  const loadedImageUrl = ref("");
  const isImageLoaded = ref(false);
  const fileName = ref("");

  const enterDragArea = (event) => {
    event.preventDefault();
    dragText.value = "Отпустите для загрузки";
  }

  const leaveDragArea = () => clearDragText();

  const clearDragText = () => dragText.value = "";

  const dropFile = (event) => {
    event.preventDefault();

    if (!checkImageLoading(event.dataTransfer.files)) {
      clearDragText();
    }
  }

  const checkImageLoading = (selectedFiles) => {
    loadFile(selectedFiles)
      .then(function(data) {
        loadedImageUrl.value = data;
        isImageLoaded.value = true;

        return true;
      },  function(error) {
        console.log(error);

        return false;
    });
  }

  const loadFile = (selectedFiles) => {
    return new Promise(function(resolve, reject) {
      if (FileReader && selectedFiles && selectedFiles.length) {
        const fileReader = new FileReader();
        const selectedFile = selectedFiles[0];

        let validExtensions = ["image/jpeg", "image/png"];

        let selectedFileExtension = selectedFile.type;

        if (validExtensions.includes(selectedFileExtension)) {
          fileName.value = selectedFile.name;

          fileReader.onload = () => resolve(fileReader.result);

          fileReader.onerror = () => {
            reject(`Произошла ошибка: ${fileReader.error}`);
          }

          fileReader.readAsDataURL(selectedFile);
        } else {
          reject(toastr.error("Файлы данного формата запрещены", 
            "Ошибка загрузки"));
        }
      }
    });
  }

  const openFileDialog = () => imageInput.value.click();

  const selectFile = (event) => {
    checkImageLoading(event.target.files);
  };
</script>

<style lang="less">
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