<template>
  <form class="search-form">
    <label class="search-form__textbox-wrapper">
      <input
        type="text"
        class="search-form__textbox"
        placeholder=""
      />
      <span class="search-form__textbox-placeholder">Введите название...</span>
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

    @media @bw1170 {
      height: 40px;
    }

    @media @bw768 {
      height: 30px;
    }

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

      @media @bw1170 {
        padding: 10px 5px 15px 10px;
        font-size: 14px;
      }

      @media @bw768 {
        padding: 6px 10px 12px 5px;
        font-size: 12px;
      }
    }

    .search-btn {
      position: relative;
      max-width: 50px;
      padding: 0;
      border-radius: 0 4px 4px 0;
      background: rgba(@indian_green, 1);
      transition: all 0.3s ease-in-out;

      @media @bw1170 {
        max-width: 40px;
      }

      @media @bw768 {
        max-width: 30px;
      }

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
      @media(hover: hover) {
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
          width: 25px;
          height: 5px;
          border-color: transparent;
          background-color: @dandelion;
          border-radius: 20px;

          @media @bw1170 {
            width: 20px;
            height: 4px;
            border-radius: 16px;
          }

          @media @bw768 {
            width: 15px;
            height: 3px;
            border-radius: 12px;
          }
        }

        .search-btn__stick {
          top: 45%;
          left: 10px;
          width: 25px;
          margin-top: -9px;
          transform: rotateZ(0);

          @media @bw1170 {
            left: 8px;
            width: 20px;
            margin-top: -7px;
          }

          @media @bw768 {
            left: 6px;
            width: 15px;
            margin-top: -5px;
          }
        }

        .search-btn__stick:before,
        .search-btn__stick:after {
          right: -7px;
          width: 15px;
          background-color: @dandelion;

          @media @bw1170 {
            right: -6px;
            width: 12px;
          }

          @media @bw768 {
            right: -4px;
            width: 9px;
          }
        }

        .search-btn__stick:before {
          bottom: 5px;
          transform: rotateZ(55deg);

          @media @bw1170 {
            bottom: 4px;
          }

          @media @bw768 {
            bottom: 3px;
          }
        }

        .search-btn__stick:after {
          bottom: -5px;
          transform: rotateZ(-55deg);

          @media @bw1170 {
            bottom: -4px;
          }

          @media @bw768 {
            bottom: -3px;
          }
        }
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

      @media @bw1170 {
        top: -4px;
        left: 8px;
        width: 12px;
        height: 12px;
        border-width: 4px;
      }

      @media @bw768 {
        top: -3px;
        left: 6px;
        width: 9px;
        height: 9px;
        border-width: 3px;
      }
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

      @media @bw1170 {
        top: 20px;
        left: 30px;
        width: 4px;
        height: 12px;
        border-radius: 12px;
      }

      @media @bw768 {
        top: 15px;
        left: 22px;
        width: 3px;
        height: 9px;
        border-radius: 9px;
      }

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

        @media @bw1170 {
          width: 12px;
          height: 4px;
          border-radius: 8px;
        }

        @media @bw768 {
          width: 9px;
          height: 3px;
          border-radius: 6px;
        }
      }
    }
  }
}
</style>
