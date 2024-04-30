<template>
  <div class="image-uploader">
    <div class="image-uploader__container">
      <div class="image-uploader__drag-area drag-area">
        <div class="drag-area__icon-wrapper">
          <i class="drag-area__icon fas fa-images"></i>
        </div>
        <span class="drag-area__select-btn">Выберите изображение</span>
        <span class="drag-area__drag-label">или перетащите файл сюда</span>
        <input type="file" class="drag-area__file-input"/>
        <span class="drag-area__supported-formats">Поддерживаемые форматы: JPG, JPEG, PNG</span>
      </div>
    </div>
  </div>
</template>

<script setup>
</script>

<style lang="less">
  .image-uploader {
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

    &:active {
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

    &__drag-label {
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

    &__file-input {
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