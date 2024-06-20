<template>
  <div class="image-loader">
    <div 
      v-if="!store.hasImageToProcess" 
      class="image-loader__container"
    >
      <DragArea ref="dragArea" class="image-loader__drag-area"/>
    </div>

    <ProcessedImage
      v-if="store.hasImageToProcess"
      ref="processedImage"
      :imageSrc="store.imageToProcessUrl"
      imageAlt="Загруженное изображение"
      :imageName="store.loadedImageFileName"
      updateBtnTitle="Выбрать другое"
      class="image-loader__processed-image"
      @remove="removeLoadedImage"
      @update="openFileDialog(inputElement)"
    />
  </div>
</template>

<script setup>
  const store = useStore();

  const dragArea = ref();
  const processedImage = ref();
  const inputElement = ref();

  const { removeImage, openFileDialog, clearDragArea } = store;

  //onMounted(() => inputElement.value = dragArea.value.imageInput);

  const removeLoadedImage = () => {
    removeImage(processedImage.value.image);
    clearDragArea();
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