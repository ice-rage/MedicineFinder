export const useStore = defineStore("index", {
  state: () => ({
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
    hasMedicineData: (state) => !_.isEmpty(state.medicineData.data),
    hasError: (state) => state.error.code !== "",
    getErrorMessage: (state) => state.error.message,
  },
  actions: {
    fetchMedicineData(medicineName) {
      this.medicineData.isLoading = true;

      axios
        .get(`medicinefinder/${medicineName}`)
        .then(response => {
          console.log(response.data);
          this.medicineData.data = response.data;
        })
        .catch(error => {
          this.setError(error.response.status.toString());
        })
        .then(() => this.medicineData.isLoading = false);
    },
    setError(errorCode) {
      this.error.code = errorCode;
      this.error.message = _.find(config.errors, 
        { code: this.error.code }).message;
    },
    clearError() {
      this.error.code = "";
      this.error.message = "";
    },
  },
});