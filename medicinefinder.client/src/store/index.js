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
    loadedImage: {
      url: "",
    },
    medicineData: {
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
    isDataLoading: (state) => state.medicineData.isLoading,
    hasImage: (state) => state.loadedImage.url !== "",
    hasMedicineData: (state) => !_.isEmpty(state.medicineData.data),
    hasError: (state) => state.error.code !== "",
    getMedicineData: (state) => state.medicineData.data,
    getErrorMessage: (state) => state.error.message,
  },
  actions: {
    fetchMedicineData(medicineName) {
      this.resetMedicineData();
      this.clearError();
      this.medicineData.isLoading = true;

      axios
        .get(`medicinefinder/${medicineName}`)
        .then(response => this.medicineData.data = response.data)
        .catch(error => this.setError(error.response.status.toString()))
        .then(() => this.medicineData.isLoading = false);
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
    resetMedicineData() {
      Object.keys(this.getMedicineData).forEach(key => 
        delete this.getMedicineData[key]);
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
  },
});