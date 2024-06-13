<template>
  <div class="processed-image">
    <div class="processed-image__picture-wrapper">
      <picture class="processed-image__picture">
        <img
          ref="image"
          :src="imageSrc" 
          :alt="imageAlt" 
          class="processed-image__image"
        />
      </picture>

      <span class="processed-image__image-name">{{ imageName }}</span>

      <button
        type="button"
        title="Удалить"
        class="processed-image__remove-btn"
        @click="emit('remove')"
      ></button>

      <button
        type="button"
        :title="updateBtnTitle"
        class="processed-image__update-btn"
        @click="emit('update')"
      ></button>
    </div>
  </div>
</template>

<script setup>
  const {
    imageSrc, 
    imageAlt, 
    imageName, 
    updateBtnTitle } = defineProps({
      imageSrc: {
        type: String,
        default: "",
        required: true,
      },
      imageAlt: {
        type: String,
        default: "",
        required: true,
      },
      imageName: {
        type: String,
        default: "",
      },
      updateBtnTitle: {
        type: String,
        default: "",
        required: true,
      },
    });

  const image = ref();

  defineExpose({ image });

  const emit = defineEmits(["remove", "update"]);
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
          .processed-image__remove-btn,
          .processed-image__update-btn,
          .processed-image__image-name {
            opacity: 1;
          }

          .processed-image__remove-btn,
          .processed-image__update-btn {
            top: 10px;
            
            @media @bw768 {
              top: 8px;
            }
          }

          .processed-image__image-name {
            top: 20px;

            @media @bw768 {
              top: 15px;
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

    &__image-name {
      position: absolute;
      top: -20px;
      left: 20px;
      color: @white;
      font-size: 20px;
      opacity: 0;
      transition: 0.5s;
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
        right: 35px;
        width: 25px;
        height: 25px;
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
          top: 19px;
          width: 25px;
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

    &__update-btn {
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