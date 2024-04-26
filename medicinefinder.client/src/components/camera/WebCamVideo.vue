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

    <button type="button" class="webcam-video__toggle-btn">Остановить</button>
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
      margin: 0 auto;
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

    &__toggle-btn {
      position: relative;
      width: 40px;
      height: 40px;
      margin: 20px auto 0;
      padding: 0;
      border: 3px solid @islamic_green;
      border-radius: 50%;
      font-size: 0;
      transition: 0.5s;
      animation: flicker 2s ease infinite;

      &::before {
        content: "";
        position: absolute;
        top: calc(50% - 13px);
        left: calc(50% - 13px);
        width: 26px;
        height: 26px;
        border-radius: 50%;
        background-color: @islamic_green;
      }

      &:hover {
        @media(hover: hover) {
          .gradient-btn(@gradient_direction: left);

          width: 180px;
          border: none;
          border-radius: 5px;
          font-size: 16px;
          animation: none;

          &::before {
            opacity: 0;
          }
        }
      }
    }
  }
</style>