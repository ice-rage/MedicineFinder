<template>
  <div class="web-camera">
    <!-- <WebCamVideo class="web-camera__video"/> -->
    <ProcessedImage 
      imageAlt="Снимок" 
      renewBtnTitle="Переснять"
      class="web-camera__snapshot"
    />
  </div>
</template>

<script setup>
  import { onMounted } from "vue";
  
  // import WebCamVideo from "@/components/camera/WebCamVideo.vue";
  import ProcessedImage from "@/components/ProcessedImage.vue";
  
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
  }
</style>