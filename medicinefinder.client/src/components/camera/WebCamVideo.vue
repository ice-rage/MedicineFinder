<template>
  <div class="webcam-video">
    <div class="webcam-video__container">
      <video 
        autoplay="true" 
        id="js-videoElement" 
        class="webcam-video__video-element"
      ></video>
      <button type="button" class="webcam-video__grab-btn">
        Сделать снимок
      </button>
    </div>
  </div>
</template>

<script setup>

</script>

<style lang="less">
  .webcam-video {
    display: flex;
    flex-direction: column;
    width: 100%;

    &__container {
      .green-gradient();
      .responsive-parent(@padding-top: 75%);

      max-width: 500px;
      margin: 0 auto 20px;
      padding: 10px;
      border-radius: 10px;
      background-origin: border-box;
      background-clip: border-box;
    }

    &__video-element {
      .responsive-child(
        @top: 10px;
        @left: 10px;
        @width: calc(100% - 20px);
        @height: calc(100% - 20px)
      );
      
      border-radius: 10px;
      background-color: @gray;
    }

    &__grab-btn {
      position: absolute;
      left: 10px;
      bottom: 10px;
      width: fit-content;
      padding: 15px 20px;
      border-radius: 0 10px 0 7px;
      background-color: transparent;
      font-weight: 500;
      transition: 0.5s ease;

      &:hover {
        background-color: @dusty_gray;
        color: @dandelion;
      }
    }
  }
</style>