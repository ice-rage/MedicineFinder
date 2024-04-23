<template>
  <div id="webcam">
	<video autoplay="true" id="videoElement"></video>
</div>
</template>

<script setup>
  import { toRefs, watch } from "vue";

  const props = defineProps({
    isWebCameraOn: {
      type: Boolean,
      default: false,
    },
  });

  const { isWebCameraOn } = toRefs(props);

  watch(isWebCameraOn, (newVal, oldVal) => {
    console.log(`New value: ${newVal}`);
    console.log(`New value: ${oldVal}`);
  });

  // function toggleWebCamera() {
  //   var video = document.querySelector("#videoElement");

  //   if (props.isWebCameraOn) {
  //     if (navigator.mediaDevices.getUserMedia) {
  //       navigator.mediaDevices.getUserMedia({ video: true })
  //         .then(function (stream) {
  //           video.srcObject = stream;
  //         })
  //         .catch(error => console.log(`Something went wrong: ${error}`));
  //     }
  //   } else {
  //     console.log("Off");

  //     const mediaStream = video.srcObject;

  //     mediaStream.getTracks().forEach(track => track.stop());
  //   }
  // }
</script>

<style lang="less">
  #webcam {
    margin: 0 auto;
    width: 500px;
    height: 375px;
    border: 10px #333 solid;
  }

  #videoElement {
    width: 500px;
    height: 375px;
    background-color: #666;
  }
</style>