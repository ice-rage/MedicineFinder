<template>
  <div class="image-loader">
    <div class="image-loader__container" v-show="loadedImageUrl === ''">
      <DragArea class="image-loader__drag-area"/>
    </div>

    <ProcessedImage
      v-if="loadedImageUrl !== ''"
      :imageSrc="loadedImageUrl"
      imageAlt="Загруженное изображение"
      :imageName="fileName"
      renewBtnTitle="Выбрать другое"
      class="image-loader__processed-image"
      @renewImageEvent="openFileDialog"
      @removeImageEvent="resetView"
    />
  </div>
</template>

<script setup>
  const dragText = ref();
  const loadedImageUrl = ref("");
  const isImageLoaded = ref(false);
  const fileName = ref("");

  const clearDragText = () => dragText.value = "";

  const resetView = () => {
    loadedImageUrl.value = "";
    fileName.value = "";
    isImageLoaded.value = false;

    clearDragText();
  }
</script>

<style lang="less">
  .image-loader {
    display: flex;
    justify-content: center;

    &__container {
      max-width: 520px;
    }

    &__drag-area {
      @media @bw768 {
        padding: 0 30px;
      }
    }
  }
</style>