<template>
  <div class="web-camera" >
    <div v-if="!store.hasImageToProcess" class="web-camera__video">
      <div class="web-camera__video-container">
        <video ref="video" class="web-camera__video-element"></video>
          <button
            type="button" 
            title="Сделать снимок"
            :disabled="store.isFrameCaptureBtnDisabled"
            class="web-camera__capture-btn capture-btn"
            @click="captureFrame"
          >
          <SvgCamera class="capture-btn__icon"/>
        </button>
      </div>

      <canvas ref="canvas" class="web-camera__canvas"></canvas>

      <button 
        type="button"
        class="web-camera__toggle-btn"
        :class="{ 
          'web-camera__toggle-btn--stop' : 
          store.currentWebCameraState === 'stop' 
        }"
        :disabled="store.isWebCameraToggleBtnDisabled"
        @click="toggleWebCameraState"  
      >
        {{ store.toggleBtnText }}
      </button>
    </div>
    
    <ProcessedImage
      v-if="store.hasImageToProcess"
      ref="processedImage"
      :imageSrc="store.imageToProcessUrl"
      imageAlt="Снимок"
      updateBtnTitle="Переснять"
      class="web-camera__processed-image"
      @remove="removeImage(processedImage.image)"
      @update="reshoot"
    />
  </div>
</template>

<script setup>
  const store = useStore();

  const video = ref();
  const toggleBtn = ref();
  const canvas = ref();
  const processedImage = ref();

  const { 
    setUpWebCamera, 
    captureFrame,
    toggleWebCameraState, 
    removeImage, 
    reshoot } = store;

  onMounted(() => setUpWebCamera(video, canvas));
</script>

<style lang="less">
  .web-camera {
    &__video {
      display: flex;
      flex-direction: column;
      align-items: center;
      width: 100%;
    }

    &__video-container {
      .green-gradient();
      .responsive-parent(@padding-top: 75%);

      max-width: 500px;
      padding: 10px;
      border-radius: 10px;
      background-origin: border-box;
      background-clip: border-box;

      @media @bw768 {
        padding: 0;
      }    
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

    &__capture-btn {
      position: absolute;
      left: 10px;
      bottom: 10px;
    }

    &__canvas {
      display: none;
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

      @media @bw768 {
        width: 30px;
        height: 30px;
      }

      &::before {
        content: "";
        position: absolute;
        top: 17.5%;
        left: 17.5%;
        width: 65%;
        height: 65%;
        border-radius: 50%;
        background-color: @islamic_green;
      }

      &--stop,
      &:hover,
      &:disabled {
        width: 220px;
        border: none;
        border-radius: 5px;
        font-size: 16px;
        animation: none;

        @media @bw1170 {
          width: 190px;
          font-size: 14px;
        }

        @media @bw768 {
          width: 160px;
          font-size: 12px;
        }

        &::before {
          opacity: 0;
        }
      }

      &:hover,
      &--stop {
        @media(hover: hover) {
          .gradient-btn(@gradient_direction: left);
        }
      }

      &:disabled {
        background-color: @gray;
        color: @light_gray;
        pointer-events: none;
      }
    }
  }

  .capture-btn {
    width: 56px;
    height: 56px;
    padding: 15px;
    border-radius: 0 10px 0 7px;
    background-color: transparent;
    color: @white;
    transition: 0.5s ease;

    @media @bw768 {
      width: 44px;
      height: 44px;
      padding: 10px;
    }

    &:hover {
      @media(hover: hover) {
        background-color: @dusty_gray;
        color: @dandelion;
      }
    }

    &:disabled {
      color: @light_gray;
      pointer-events: none;
    }

    &__icon {
      width: 100%;
      height: 100%;
    }
  }
</style>