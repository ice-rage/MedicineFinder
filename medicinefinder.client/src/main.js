import "./styles/styles.less";

import { createApp } from "vue";
import { createPinia } from "pinia";
import toastr from "toastr";
import App from "./App.vue";

toastr.options = {
  "positionClass": "toast-bottom-left",
};

const pinia = createPinia();
const app = createApp(App);

app.use(pinia).mount("#app");