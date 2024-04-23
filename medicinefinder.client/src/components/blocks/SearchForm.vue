<template>
  <form class="search-form">
    <label class="search-form__textbox-wrapper">
      <input
        type="text"
        class="search-form__textbox"
        placeholder=""
      />
      <span class="search-form__textbox-placeholder">
        Введите название препарата
      </span>
    </label>

    <button type="submit" class="search-form__search-btn search-btn">
      <div class="search-btn__circle"></div>
      <span class="search-btn__stick"></span>
    </button>
  </form>
</template>

<script setup></script>

<style lang="less">
  .search-form {
    display: flex;
    width: 100%;
    height: 50px;
    margin-bottom: 40px;
    outline: 0;
    border-radius: 4px;
    box-shadow: rgba(@shadow_gray, 0.2) 0 5px 30px 0;

    &__textbox-wrapper {
      position: relative;
      box-sizing: border-box;
      display: flex;
      align-items: center;
      padding: 0 15px;
      outline: 0;
      border: 2px solid @islamic_green;
      border-right: 0;
      border-radius: 3px 0 0 4px;
      background-color: @white;
      font-size: 16px;

      &:focus-within {
        border-width: 3px;
      }
    }

    &__textbox {
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
      padding: 15px 15px 20px;
      color: @black;
      transition: 0.4s;
      transition-timing-function: ease;
      transition-timing-function: cubic-bezier(0.25, 0.1, 0.25, 1);
      opacity: 0.5;
      pointer-events: none;
    }
  }

  .search-btn {
    .gradient-btn();

    position: relative;
    max-width: 50px;
    border-radius: 0 4px 4px 0;
    color: @white;

    &:hover .search-btn__circle {
      top: 1px;
      width: 25px;
      height: 5px;
      border-width: 0;
      background-color: @white;
      border-radius: 20px;
    }

    &:hover .search-btn__stick {
      top: 45%;
      left: 10px;
      width: 25px;
      margin-top: -9px;
      transform: rotateZ(0);
    }

    &:hover .search-btn__stick:before,
    &:hover .search-btn__stick:after {
      right: -5px;
      width: 15px;
      background-color: @white;
    }

    &:hover .search-btn__stick:before {
      bottom: 5px;
      transform: rotateZ(55deg);
    }

    &:hover .search-btn__stick:after {
      bottom: -5px;
      transform: rotateZ(-55deg);
    }

    &__circle {
      position: relative;
      top: -5px;
      left: 10px;
      width: 15px;
      height: 15px;
      margin-top: 0;
      border: 5px solid @white;
      border-radius: 50%;
      background-color: transparent;
      transition: 0.5s ease all;
    }

    &__stick {
      position: absolute;
      top: 25px;
      left: 38px;
      display: block;
      width: 5px;
      height: 15px;
      border-radius: 15px;
      background-color: transparent;
      transform: rotateZ(52deg);
      transition: 0.5s ease all;

      &::before,
      &::after {
        content: "";
        position: absolute;
        bottom: 0;
        right: 0;
        width: 15px;
        height: 5px;
        border-radius: 10px;
        background-color: @white;
        transform: rotateZ(0);
        transition: 0.5s ease all;
      }
    }
  }
</style>
