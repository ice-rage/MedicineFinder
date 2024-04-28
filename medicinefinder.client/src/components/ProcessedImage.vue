<template>
  <div class="processed-image">
    <div class="processed-image__picture-wrapper">
      <picture class="processed-image__picture">
        <img :src="imageSrc" :alt="imageAlt" class="processed-image__image"/>
      </picture>

      <button
        type="button"
        title="Удалить"
        class="processed-image__remove-btn"
      ></button>

      <button
        type="button"
        :title="renewBtnTitle"
        class="processed-image__renew-btn"
      ></button>
    </div>
  </div>
</template>

<script setup>
  const props = defineProps(["imageSrc", "imageAlt", "renewBtnTitle"]);
</script>

<style lang="less">
  .processed-image {
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;

    &__image-input {
      display: none;
    }

    &__picture-wrapper {
      position: relative;
      display: flex;
      max-width: 520px;

      &:hover {
        @media (hover: hover) {
          .processed-image__renew-btn,
          .processed-image__remove-btn {
            top: 10px;
            opacity: 1;

            @media @bw768 {
              top: 8px;
            }
          }
        }
      }
    }

    &__picture {
      .green-gradient();
      .responsive-parent(@padding-top: 75%);

      padding: 10px;
      border-radius: 10px;
      background-origin: border-box;
      background-clip: border-box;
    }

    &__image {
      .responsive-child(
          @top: 10px;
          @left: 10px;
          @width: calc(100% - 20px);
          @height: calc(100% - 20px)
        );

      border-radius: 10px;
      background-color: @gray;
      color: @white;
      transition: filter 0.3s;

      &:hover {
        @media (hover: hover) {
          filter: brightness(50%);
        }
      }
    }

    &__remove-btn {
      position: absolute;
      top: -25px;
      right: 45px;
      height: 30px;
      width: 30px;
      background-color: transparent;
      transition: 0.5s;
      opacity: 0;

      @media @bw768 {
        right: 32px;
        width: 20px;
        height: 20px;
      }

      &:hover {
        @media (hover: hover) {
          &::before,
          &::after {
            background-color: @dandelion;
          }
        }
      }

      &::before,
      &::after {
        content: "";
        position: absolute;
        top: 27px;
        left: 0;
        display: block;
        width: 30px;
        height: 5px;
        background-color: @white;

        @media @bw768 {
          top: 20px;
          width: 20px;
          height: 3px;
        }
      }

      &::before {
        transform: rotate(45deg);
      }

      &::after {
        transform: rotate(-45deg);
      }
    }

    &__renew-btn {
      position: absolute;
      top: -25px;
      right: 25px;
      width: 28px;
      height: 28px;
      background-color: transparent;
      transition: 0.5s;
      opacity: 0;

      @media @bw768 {
        right: 20px;
        width: 20px;
        height: 20px;
      }

      &:hover {
        @media (hover: hover) {
          &::before {
            border-color: transparent @dandelion @dandelion;
          }

          &:after {
            border-color: transparent transparent transparent @dandelion;
          }
        }
      }

      &::before,
      &::after {
        content: "";
        position: absolute;
        box-sizing: border-box;
        display: block;
      }

      &::before {
        width: 28px;
        height: 28px;
        border: 5px solid;
        border-color: transparent @white @white;
        border-radius: 50%;
        transform: rotate(65deg);

        @media @bw768 {
          width: 20px;
          height: 20px;
          border-width: 3px;
        }
      }

      &::after {
        position: absolute;
        width: 0;
        height: 0;
        border: 10px solid;
        border-color: transparent transparent transparent @white;
        transform: rotate(30deg) translate(0.8rem, -0.7rem);

        @media @bw768 {
          border-width: 7px;
          transform: rotate(30deg) translate(0.6rem, -0.5rem);
        }
      }
    }
  }
</style>
