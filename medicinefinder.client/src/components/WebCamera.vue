<template>
  <div class="web-camera">
    <div class="web-camera__video" v-show="!isFrameCaptured">
      <div class="web-camera__video-container">
        <video ref="video" class="web-camera__video-element"></video>
          <button
            type="button" 
            title="Сделать снимок"
            :disabled="webCamStates[currentWebCamState]['captureBtnDisabled']"
            class="web-camera__capture-btn capture-btn"
            @click="captureFrame()"
          >
          <SvgCamera class="capture-btn__icon"/>
        </button>
      </div>

      <canvas ref="canvas" class="web-camera__canvas"></canvas>

      <button 
        type="button" 
        ref="toggleBtn"
        :class="currentWebCamState === 'stop' 
          ? 'web-camera__toggle-btn web-camera__toggle-btn--stop' 
          : 'web-camera__toggle-btn'"
        :disabled="webCamStates[currentWebCamState]['toggleBtnDisabled']"
        @click="toggleWebCameraVideo()"  
      >{{ webCamStates[currentWebCamState]['toggleBtnText'] }}</button>
    </div>
    
    <ProcessedImage
      :imageSrc="snapshotUrl"
      imageAlt="Снимок"
      renewBtnTitle="Переснять"
      class="web-camera__processed-image"
      @removeImageEvent="toggleView(false)"
      @renewImageEvent="reshoot()"
      v-if="isFrameCaptured"
    />
  </div>
</template>

<script setup>
  import { onMounted, reactive, ref } from "vue";
  import SvgCamera from "@/components/icons/SvgCamera.vue";
  import ProcessedImage from "@/components/ProcessedImage.vue";

  const emit = defineEmits(["hideSiblingEvent", "toggleSearchBtnEvent"]);

  const video = ref();
  const toggleBtn = ref();
  const webCamPermissionStatus = ref();
  const canvas = ref();
  const snapshotUrl = ref();
  const isFrameCaptured = ref(false);

  const webCamStates = reactive({
    play: {
      toggleBtnText: "Остановить",
      toggleBtnDisabled: false,
      captureBtnDisabled: false,
    },
    stop: {
      toggleBtnText: "Запустить",
      toggleBtnDisabled: false,
      captureBtnDisabled: true,
    },
    unavailable: {
      toggleBtnText: "Веб-камера недоступна",
      toggleBtnDisabled: true,
      captureBtnDisabled: true,
    },
  });

  const currentWebCamState = ref("play");

  onMounted(() => {
    emit("hideSiblingEvent");

    navigator.permissions
    .query({ name: "camera" })
    .then((permissionStatus) => {
      webCamPermissionStatus.value = permissionStatus.state;

      permissionStatus.onchange = () => 
        checkWebCameraPermission(permissionStatus.state);
    });

    startWebCameraVideo();
  });

  const checkWebCameraPermission = permissionState => {
    webCamPermissionStatus.value = permissionState;

    switch(webCamPermissionStatus.value) {
      case "denied":
        currentWebCamState.value = "unavailable";
        break;
      case "granted":
        startWebCameraVideo();
        break;
      default:
        return;
    }
  }

  const startWebCameraVideo = () => {
    navigator.mediaDevices.getUserMedia({ video: true, audio: false })
    .then(function(stream) {
      video.value.srcObject = stream;
      video.value.play();
      
      currentWebCamState.value = "play";
    })
    .catch(function(error) {
      console.log(`Произошла ошибка: ${error}`);
      currentWebCamState.value = "unavailable";
    });
  }

  const stopWebCameraVideo = () => {
    video.value.srcObject.getVideoTracks()
    .forEach(videoTrack => videoTrack.stop());

    currentWebCamState.value = "stop";
  }

  const toggleWebCameraVideo = () => {
    const hasLiveVideoTracks = detectLiveVideoTracks();

    if (hasLiveVideoTracks) {
      stopWebCameraVideo();
    } else {
      startWebCameraVideo();
    }
  }

  const detectLiveVideoTracks = () => 
    video.value.srcObject.getVideoTracks().some(videoTrack => 
      videoTrack.enabled && videoTrack.readyState === "live");

  function captureFrame() {
    const context = canvas.value.getContext("2d");
    const imageWidth = video.value.offsetWidth;
    const imageHeight = video.value.offsetHeight;

    if (imageWidth && imageHeight) {
      canvas.value.width = imageWidth;
      canvas.value.height = imageHeight;
      context.drawImage(video.value, 0, 0, imageWidth, imageHeight);

      snapshotUrl.value = canvas.value.toDataURL("image/jpeg");
      
      stopWebCameraVideo();
      toggleView(true);
    }
  }

  const toggleView = (value) => {
    isFrameCaptured.value = value;
    emit("toggleSearchBtnEvent", snapshotUrl.value, isFrameCaptured.value);
  }

  const reshoot = () => {
    toggleView(false);
    startWebCameraVideo();
  }
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