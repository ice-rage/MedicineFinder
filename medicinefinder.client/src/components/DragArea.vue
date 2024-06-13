<template>
  <div
    class="drag-area"
    :class="{ 'drag-area--active' : store.isDragAreaActive }"
    @dragover="enterDragArea($event)"
    @dragleave="clearDragArea"
    @drop="dropFile($event)"
  >
    <div class="drag-area__icon-wrapper">
      <i class="drag-area__icon fas fa-images"></i>
    </div>

    <span 
      class="drag-area__select-btn" 
      @click="openFileDialog(imageInput)"
    >
      Выберите изображение
    </span>

    <span class="drag-area__drag-text">{{ store.dragAreaText }}</span>

    <input
      ref="imageInput"
      type="file"
      class="drag-area__image-input"
      :accept="store.acceptedImageFormats"
      @change="loadImageFile($event.target.files)"
    />

    <span class="drag-area__supported-formats">
      Поддерживаемые форматы: JPEG, PNG
    </span>
  </div>
</template>

<script setup>
  const store = useStore();

  const imageInput = ref();

  defineExpose({ imageInput });

  const { 
    enterDragArea, 
    clearDragArea,
    dropFile,
    openFileDialog,
    loadImageFile } = store;
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