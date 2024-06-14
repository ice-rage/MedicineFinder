import toastr from "toastr";

export const useStore = defineStore("index", {
  state: () => ({
    options: {
      imageLoader: {
        isVisible: false,
      },
      webCamera: {
        isVisible: false,
      },
    },
    dragArea: {
      text: config.initialDragText,
      isActive: false,
    },
    loadedImage: {
      fileName: "",
      validExtensions: ["image/jpeg", "image/png"],
    },
    webCamera: {
      videoElement: {},
      canvasElement: {},
      states: config.webCameraStates,
      currentState: "play",
    },
    imageToProcessUrl: "",
    medicine: {
      isLoading: false,
      data: {},
    },
    error: {
      code: "",
      message: "",
    },
  }),
  getters: {
    isOptionVisible: (state) => (option) => 
      state.options.hasOwnProperty(option) 
        ? state.options[option].isVisible 
        : undefined,
    isDataLoading: (state) => state.medicine.isLoading,
    acceptedImageFormats: (state) => state.loadedImage.validExtensions,
    isFileDialogOpened: (state) => state.fileDialog.isOpened,
    isDragAreaActive: (state) => state.dragArea.isActive,
    dragAreaText: (state) => state.dragArea.text,
    hasImageToProcess: (state) => state.imageToProcessUrl !== "",
    loadedImageFileName: (state) => state.loadedImage.fileName,
    videoElement: (state) => state.webCamera.videoElement,
    canvasElement: (state) => state.webCamera.canvasElement,
    isFrameCaptureBtnDisabled: (state) => 
      state.webCamera.states[state.webCamera.currentState].isCaptureBtnDisabled,
    isWebCameraToggleBtnDisabled: (state) => 
      state.webCamera.states[state.webCamera.currentState].isToggleBtnDisabled,
    toggleBtnText: (state) => 
      state.webCamera.states[state.webCamera.currentState].toggleBtnText,
    currentWebCameraState: (state) => state.webCamera.currentState,
    hasMedicineData: (state) => !_.isEmpty(state.medicineData),
    medicineData: (state) => state.medicine.data,
    hasError: (state) => state.error.code !== "",
    errorMessage: (state) => state.error.message,
  },
  actions: {
    fetchMedicineData(medicineName) {
      this.resetView();
      this.resetImageToProcessUrl();

      this.options.imageLoader.isVisible = false;
      this.options.webCamera.isVisible = false;

      this.callApi("get", { parameter: medicineName });
    },
    callApi(methodType, queryDetails) {
      axios({
        method: methodType,
        url: queryDetails.parameter
          ? `medicinefinder/${queryDetails.parameter}` 
          : "medicinefinder",
        data: queryDetails.data || {},
      }).then(response => this.medicine.data = response.data)
        .catch(error => this.setError(error.response.status.toString()))
        .then(() => this.medicine.isLoading = false);
    },
    resetView() {
      this.medicine.isLoading = true;

      Object.keys(this.medicineData).forEach(key => 
        delete this.medicineData[key]);

      this.clearError();
    },
    setError(errorCode) {
      this.error.code = errorCode;
      const error = _.find(config.errors, { code: this.error.code });
      this.error.message = error ? error.message : "Неизвестная ошибка";
    },
    clearError() {
      this.error.code = "";
      this.error.message = "";
    },
    toggleOption(option) {
      if (this.options.hasOwnProperty(option)) {
        const foundOption = this.options[option];
        foundOption.isVisible = !foundOption.isVisible;

        if (option === 'imageLoader') {
          this.options.webCamera.isVisible = false;

          if (this.detectLiveVideoTracks()) {
            console.log("Has live tracks");
            this.stopWebCameraVideo();
          }
        } else {
          this.options.imageLoader.isVisible = false;
        }

        this.resetImageToProcessUrl();
        this.clearError();
      }
    },
    resetImageToProcessUrl() {
      this.imageToProcessUrl = "";
    },
    openFileDialog(inputElement) {
      inputElement.click();
    },
    enterDragArea(event) {
      event.preventDefault();

      this.dragArea.text = "Отпустите для загрузки";
      this.dragArea.isActive = true;
    },
    clearDragArea() {
      this.dragArea.text = config.initialDragText;
      this.dragArea.isActive = false;
    },
    dropFile(event) {
      event.preventDefault();
  
      if (!this.loadImageFile(event.dataTransfer.files)) {
        this.clearDragArea();
      }
    },
    loadImageFile(selectedFiles) {
      if (FileReader && selectedFiles && selectedFiles.length) {
        const fileReader = new FileReader();
        const selectedFile = selectedFiles[0];
        const selectedFileExtension = selectedFile.type;
  
        if (this.acceptedImageFormats.includes(selectedFileExtension)) {
          this.loadedImage.fileName = selectedFile.name;
  
          fileReader.onload = () => this.imageToProcessUrl = 
            fileReader.result;
  
          fileReader.onerror = () => {
            console.log(`Произошла ошибка: ${fileReader.error}`);
  
            return false;
          };
  
          fileReader.readAsDataURL(selectedFile);
  
          return true;
        } else {
          toastr.error("Файлы данного формата запрещены", "Ошибка загрузки");
  
          return false;
        }
      }
    },
    removeImage(imageElement) {
      imageElement.setAttribute("src", "");

      this.resetImageToProcessUrl();
    },
    setUpWebCamera(videoElement, canvasElement) {
      this.webCamera.videoElement = videoElement;
      this.webCamera.canvasElement = canvasElement;

      navigator.permissions
        .query({ name: "camera" })
        .then(permissionStatus => {
          permissionStatus.onchange = () => {
            const permissionState = permissionStatus.state;

            if (permissionState === "granted") {
              this.startWebCameraVideo();
            } else if (permissionState === "denied") {
              this.webCamera.currentState = "unavailable";
            }
          }
        });  
        
        this.startWebCameraVideo();
    },
    startWebCameraVideo() {
      navigator.mediaDevices
        .getUserMedia({ video: true, audio: false })
        .then(stream => {
          this.videoElement.srcObject = stream;

          return new Promise(resolve => this.videoElement.oncanplay = 
            resolve);
        })
        .then(() => {
          this.videoElement.play();
          this.webCamera.currentState = "play";
        })
        .catch(error => {
          console.log(`Произошла ошибка: ${error}`);
          this.webCamera.currentState = "unavailable";
        });
    },
    stopWebCameraVideo() {
      console.log(this.videoElement.srcObject.getVideoTracks());

      this.videoElement.srcObject.getVideoTracks()
        .forEach(videoTrack => videoTrack.stop());
  
      this.webCamera.currentState = "stop";
    },
    toggleWebCameraState() {
      if (this.detectLiveVideoTracks()) {
        this.stopWebCameraVideo();
      } else {
        this.startWebCameraVideo();
      }
    },
    detectLiveVideoTracks() {
      const srcObject = this.videoElement.srcObject;

      if (srcObject) {
        return srcObject.getVideoTracks().some(videoTrack => 
          videoTrack.enabled && videoTrack.readyState === "live");
      }
      
      return false;
    },
    captureFrame() {
      const context = this.canvasElement.getContext("2d");
      const imageWidth = this.videoElement.offsetWidth;
      const imageHeight = this.videoElement.offsetHeight;
  
      if (imageWidth && imageHeight) {
        this.canvasElement.width = imageWidth;
        this.canvasElement.height = imageHeight;
        context.drawImage(this.videoElement, 0, 0, imageWidth, 
          imageHeight);
  
        this.imageToProcessUrl = this.canvasElement.toDataURL("image/jpeg");
        
        this.stopWebCameraVideo();
      }
    },
    reshoot() {
      this.resetImageToProcessUrl();
      this.startWebCameraVideo();
    },
    uploadImage() {
      this.resetView();
      this.medicine.isLoading = true;
      const imageData = new FormData();
      imageData.append("encodedImage", this.imageToProcessUrl);

      this.callApi("post", { data: imageData });
    },
  },
});