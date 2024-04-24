<template>
  <div class="web-camera">
    <!-- <WebCamVideo class="web-camera__video"/> -->
    <WebCamSnapshot class="web-camera__snapshot"/>

    <button type="button" class="web-camera__stop-btn">Выключить камеру</button>
  </div>
</template>

<script setup>
  import { onMounted } from "vue";
  
  // import WebCamVideo from "@/components/camera/WebCamVideo.vue";
  import WebCamSnapshot from "@/components/camera/WebCamSnapshot.vue";
  
  onMounted(() => captureStream());

  function captureStream() {
    var video = document.querySelector("#js-videoElement");

    if (navigator.mediaDevices.getUserMedia) {
      navigator.mediaDevices.getUserMedia({ 
        video: true, 
        audio: false,
      }).then(function (stream) {
          video.srcObject = stream;
      }).catch(error => console.log(`Something went wrong: ${error}`));
    }
  }
</script>

<style lang="less">
  .web-camera {
    display: flex;
    flex-direction: column;
    width: 100%;
    margin: 30px 0;

    &__stop-btn {
      position: relative;
      width: 40px;
      height: 40px;
      margin: 0 auto;
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