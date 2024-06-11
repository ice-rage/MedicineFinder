<template>
  <div class="search-form">
    <label class="search-form__textbox-wrapper">
      <input
        v-model="inputText"
        type="text"
        class="search-form__textbox"
        placeholder=""
        @keyup.enter="detectPressingEnter"
      />
      <span class="search-form__textbox-placeholder">
        Введите название...
      </span>
    </label>

    <SearchBtn 
      :text="inputText" 
      :active="!isRequestEmpty" 
      class="search-form__search-btn"
    />
  </div>
</template>

<script setup>
  import { ref, computed } from "vue";
  import SearchBtn from "./SearchBtn.vue";
  // import axios from "axios";

  const emit = defineEmits(["showDataLoadingEvent", "showResultEvent"]);

  const inputText = ref("");

  const isRequestEmpty = computed(() => !/\S{3,}/.test(inputText.value));

  const detectPressingEnter = () => {
    if (!isRequestEmpty.value) {
      fetchMedicineData(inputText.value)
    }
  }

  // const fetchMedicineData = (medicineName) => {
  //   emit("showDataLoadingEvent");

  //   emit("showResultEvent", new Promise((resolve, reject) => {
  //     axios
  //       .get(`medicinefinder/${medicineName}`)
  //       .then(response => {
  //         resolve(response);
  //       }, error => {
  //         reject(error);
  //       });
  //   }));
  // }
</script>

<style lang="less">
  .search-form {
    display: flex;
    max-width: 600px;
    height: 40px;
    outline: 0;
    border-radius: 4px;
    box-shadow: rgba(@shadow_gray, 0.2) 0 5px 30px 0;

    @media @bw768 {
      height: 30px;
    }

    &__textbox-wrapper {
      position: relative;
      box-sizing: border-box;
      display: flex;
      align-items: center;
      padding: 0 10px 0 15px;
      outline: 0;
      border: 2px solid @islamic_green;
      border-right: 0;
      border-radius: 3px 0 0 4px;
      background-color: @white;

      @media @bw768 {
        padding: 0 10px;
      }

      &:focus-within {
        border-width: 3px;
      }
    }

    &__textbox {
      line-height: 1.375;

      &:focus + .search-form__textbox-placeholder,
      &:not(:placeholder-shown) + .search-form__textbox-placeholder {
        color: @islamic_green;
        opacity: 1;
        transform: scale(0.8) translateY(-100%) translateX(-15%);
      }

      /* Для браузера Internet Explorer */
      &:focus + .search-form__textbox-placeholder,
      &:not(:-ms-input-placeholder) + .search-form__textbox-placeholder {
        color: @islamic_green;
        opacity: 1;
        transform: scale(0.8) translateY(-100%) translateX(-15%);
      }
    }

    &__textbox-placeholder {
      position: absolute;
      top: 0;
      left: 0;
      padding: 8px 12px 12px;
      color: @black;
      line-height: normal;
      transition: 0.4s;
      transition-timing-function: ease;
      transition-timing-function: cubic-bezier(0.25, 0.1, 0.25, 1);
      opacity: 0.5;
      pointer-events: none;

      @media @bw1170 {
        font-size: 14px;
      }

      @media @bw768 {
        padding: 5px 8px 12px;
        font-size: 12px;
      }
    }
  }
</style>