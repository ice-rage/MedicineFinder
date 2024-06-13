import toastr from "toastr";

toastr.options = {
  "positionClass": "toast-bottom-left",
};

const initialDragText = "или перетащите файл сюда";

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
      text: initialDragText,
      isActive: false,
    },
    imageTypes: {
      loaded: {
        url: "",
        fileName: "",
        validExtensions: ["image/jpeg", "image/png"],
      },
      snapshot: {
        url: "",
      },
    },
    webCamera: {
      videoElement: {},
      canvasElement: {},
      states: {
        play: {
          isCaptureBtnDisabled: false,
          isToggleBtnDisabled: false,
          toggleBtnText: "Остановить",
        },
        stop: {
          isCaptureBtnDisabled: true,
          isToggleBtnDisabled: false,
          toggleBtnText: "Запустить",
        },
        unavailable: {
          isCaptureBtnDisabled: true,
          isToggleBtnDisabled: true,
          toggleBtnText: "Веб-камера недоступна",
        },
      },
      currentState: "play",
    },
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
    acceptedImageFormats: (state) => 
      state.imageTypes.loaded.validExtensions.join(),
    isFileDialogOpened: (state) => state.fileDialog.isOpened,
    isDragAreaActive: (state) => state.dragArea.isActive,
    dragAreaText: (state) => state.dragArea.text,
    hasImage: (state) => state.imageTypes.loaded.url !== "" || 
      state.imageTypes.snapshot.url !== "",
    loadedImageUrl: (state) => state.imageTypes.loaded.url,
    loadedImageFileName: (state) => state.imageTypes.loaded.fileName,
    isFrameCaptureBtnDisabled: (state) => 
      state.webCamera.states[state.webCamera.currentState].isCaptureBtnDisabled,
    isWebCameraToggleBtnDisabled: (state) => 
      state.webCamera.states[state.webCamera.currentState].isToggleBtnDisabled,
    toggleBtnText: (state) => 
      state.webCamera.states[state.webCamera.currentState].toggleBtnText,
    currentWebCameraState: (state) => state.webCamera.currenState,
    isFrameCaptured: (state) => state.imageTypes.snapshot.url !== "",
    hasMedicineData: (state) => !_.isEmpty(state.medicineData),
    medicineData: (state) => state.medicine.data,
    hasError: (state) => state.error.code !== "",
    errorMessage: (state) => state.error.message,
  },
  actions: {
    fetchMedicineData(medicineName) {
      this.resetView();
      this.medicine.isLoading = true;

      axios
        .get(`medicinefinder/${medicineName}`)
        .then(response => this.medicine.data = response.data)
        .catch(error => this.setError(error.response.status.toString()))
        .then(() => this.medicine.isLoading = false);
    },
    resetView() {
      this.options.imageLoader.isVisible = false;
      this.options.webCamera.isVisible = false;

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
        } else {
          this.options.imageLoader.isVisible = false;
        }
      }
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
      this.dragArea.text = initialDragText;
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
  
        if (this.imageTypes.loaded.validExtensions.includes(selectedFileExtension)) {
          this.imageTypes.loaded.fileName = selectedFile.name;
  
          fileReader.onload = () => this.imageTypes.loaded.url = fileReader.result;
  
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
    removeImage(imageElement, type) {
      if (imageElement) {
        imageElement.setAttribute("src", "");

        if (this.imageTypes.hasOwnProperty(type)) {
          this.imageTypes[type].url = "";

          if (type === 'loaded') {
            this.clearDragArea();
          }
        }
      }
    },
    setUpWebCamera(videoElement, canvasElement) {
      this.webCamera.videoElement = videoElement;
      this.webCamera.canvasElement = canvasElement;

      console.log(this.webCamera.videoElement);
      console.log(this.webCamera.canvasElement);

      navigator.permissions
        .query({ name: "camera" })
        .then(permissionStatus => {
          permissionStatus.onchange = () => 
            this.onWebCameraPermissionChange(permissionStatus.state);
        });  
        
        this.startWebCameraVideo();
    },
    onWebCameraPermissionChange(permissionState) {
      if (permissionState === "granted") {
        this.startWebCameraVideo();
      } else if (permissionState === "denied") {
        this.webCamera.currenState = "unavailable";
      }
    },
    startWebCameraVideo() {
      navigator.mediaDevices
        .getUserMedia({ video: true, audio: false })
        .then(stream => {
          this.webCamera.videoElement.srcObject = stream;
          this.webCamera.videoElement.play();
          
          this.webCamera.currentState = "play";
        })
        .catch(error => {
          console.log(`Произошла ошибка: ${error}`);
          this.webCamera.currentState = "unavailable";
        });
    },
    stopWebCameraVideo() {
      this.webCamera.videoElement.srcObject.getVideoTracks()
        .forEach(videoTrack => videoTrack.stop());
  
      this.webCamera.currentState = "stop";
    },
    toggleWebCameraState() {
      const hasLiveVideoTracks = detectLiveVideoTracks();
  
      if (hasLiveVideoTracks) {
        this.stopWebCameraVideo();
      } else {
        this.startWebCameraVideo();
      }
    },
    detectLiveVideoTracks() {
      this.webCamera.videoElement.srcObject
        .getVideoTracks()
        .some(videoTrack => 
          videoTrack.enabled && videoTrack.readyState === "live");
    },
    captureFrame() {
      const context = this.webCamera.canvasElement.getContext("2d");
      const imageWidth = this.webCamera.videoElement.offsetWidth;
      const imageHeight = this.webCamera.videoElement.offsetHeight;
  
      if (imageWidth && imageHeight) {
        this.webCamera.canvasElement.width = imageWidth;
        this.webCamera.canvasElement.height = imageHeight;
        context.drawImage(this.webCamera.videoElement, 0, 0, imageWidth, 
          imageHeight);
  
        this.imageTypes.snapshot.url = this.webCamera.canvasElement.toDataURL("image/jpeg");
        
        this.stopWebCameraVideo();
      }
    },
    reshoot() {
      this.imageTypes.snapshot.url = "";

      this.startWebCameraVideo();
    },
  },
});