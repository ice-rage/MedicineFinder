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

    <button 
      type="submit" 
      title="Искать" 
      class="search-form__search-btn search-btn"
    >
      <div class="search-btn__circle"></div>
      <span class="search-btn__stick"></span>
    </button>
  </form>
</template>

<script setup></script>

<style lang="less">
  .search-form {
    display: flex;
    max-width: 600px;
    height: 50px;
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
    position: relative;
    max-width: 50px;
    padding: 0;
    border-radius: 0 4px 4px 0;
    background: rgba(@indian_green, 1);
    transition: all 0.3s ease-in-out;

    &::before {
      content: "";  
      position: absolute;
      top: 0; 
      left: 0;  
      display: block;
      height: 100%;
      width: 100%;
      border-radius: inherit;
      background: radial-gradient(ellipse at left, @light_lime, @indian_green);
      z-index: -100;
    }

    &:hover {
      background: rgba(255, 0, 0, 0);

      &::before {
        content: "";
        position: absolute; 
        top: 0; 
        left: 0;
        width: 100%;
        height: 100%;
        border-radius: inherit;
      }

      .search-btn__circle {
        top: 1px;
        height: 5px;
        width: 25px;
        border-color: transparent;
        background-color: @dandelion;
        border-radius: 20px;
      }

      .search-btn__stick {
        top: 45%;
        left: 10px;
        width: 25px;
        margin-top: -9px;
        transform: rotateZ(0);
      }

      .search-btn__stick:before,
      .search-btn__stick:after {
        right: -7px;
        width: 15px;
        background-color: @dandelion;
      }

      .search-btn__stick:before {
        bottom: 5px;
        transform: rotateZ(55deg);
      }

      .search-btn__stick:after {
        bottom: -5px;
        transform: rotateZ(-55deg);
      }
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
      background-clip: padding-box;
      transition: 0.3s ease-in-out;
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
      transition: 0.3s ease all;

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
