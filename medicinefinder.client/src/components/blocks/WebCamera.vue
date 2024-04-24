<template>
  <div class="web-camera">
    <div class="web-camera__video-container">
      <video 
        autoplay="true" 
        id="js-videoElement" 
        class="web-camera__video"
      ></video>
    </div>

    <button type="button" class="web-camera__stop-btn">Остановить</button>
  </div>
</template>

<script setup>
  import { onMounted } from "vue";
  
  onMounted(() => captureStream());

  function captureStream() {
    var video = document.querySelector("#js-videoElement");

    if (navigator.mediaDevices.getUserMedia) {
      navigator.mediaDevices.getUserMedia({ video: true })
        .then(function (stream) {
          video.srcObject = stream;
        })
        .catch(error => console.log(`Something went wrong: ${error}`));
    }
  }
</script>

<style lang="less">
  .web-camera {
    display: flex;
    flex-direction: column;

    &__video-container {
      .green-gradient();

      width: 500px;
      height: 375px;
      margin: 30px auto 15px;
      padding: 10px;
      border-radius: 10px;
      background-origin: border-box;
      background-clip: border-box;
    }

    &__video {
      width: 500px;
      height: 375px;
      background-color: @gray;
      border-radius: 10px;
    }

    &__stop-btn {
      position: relative;
      width: 40px;
      height: 40px;
      margin: 10px auto;
      border-radius: 50%;
      border-width: 0;
      background-color: @islamic_green;
      font-size: 0;
      transition: 0.5s;
      animation: flicker 2s ease infinite;

      &::before {
        content: "";
        position: absolute;
        border-radius: 50%;
        border: 3px solid @islamic_green;
        left: -20%;
        top: -20%;
        width: 125%;
        height: 125%;
      }

      &:hover {
        @media(hover: hover) {
          .gradient-btn(@gradient_direction: left);

          width: 150px;
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