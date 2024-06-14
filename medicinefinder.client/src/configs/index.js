export default {
  initialDragText: "или перетащите файл сюда",
  webCameraStates: {
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
  errors: [
    {
      code: "404",
      message: "Не удалось найти препарат. Пожалуйста, проверьте правильность Вашего запроса",
    },
    {
      code: "500",
      message: "Сервис временно недоступен. Пожалуйста, повторите попытку позже",
    },
  ],
};