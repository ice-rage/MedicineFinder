<template>
  <div class="image-loader">
    <div class="image-loader__container" v-if="!store.hasImage">
      <DragArea ref="dragArea" class="image-loader__drag-area"/>
    </div>

    <ProcessedImage
      v-if="store.hasImage"
      ref="processedImage"
      :imageSrc="store.loadedImageUrl"
      imageAlt="Загруженное изображение"
      :imageName="store.loadedImageFileName"
      updateBtnTitle="Выбрать другое"
      class="image-loader__processed-image"
      @remove="removeImage(processedImage.image, 'loaded')"
      @update="openFileDialog(inputElement)"
    />
  </div>
</template>

<script setup>
  const store = useStore();

  const dragArea = ref();
  const processedImage = ref();
  const inputElement = ref("");

  const { removeImage, openFileDialog } = store;

  onMounted(() => inputElement.value = dragArea.value.imageInput);
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