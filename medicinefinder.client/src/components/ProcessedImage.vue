<template>
  <div class="processed-image" v-if="isVisible">
    <div class="processed-image__picture-wrapper">
      <picture class="processed-image__picture">
        <img 
          ref="image" 
          :src="imageSrc" 
          :alt="imageAlt" 
          class="processed-image__image"/>
      </picture>

      <button
        type="button"
        title="Удалить"
        class="processed-image__remove-btn"
        @click="removeImage()"
      ></button>
    </div>
  </div>
</template>

<script setup>
  import { ref } from "vue";

  defineProps(["imageSrc", "imageAlt", "renewBtnTitle"]);
  const emit = defineEmits([
    "showParentComponentEvent",
    "reshootEvent", 
    "selectAnotherImageEvent"]);

  const isVisible = ref(true);
  const image = ref();

  const removeImage = () => {
    if (image.value) {
      image.value.setAttribute("src", "");
      isVisible.value = false;
      emit("showParentComponentEvent");
    }
  };
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
          .processed-image__remove-btn {
            top: 5px;
            opacity: 1;

            @media @bw768 {
              top: 3px;
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
      right: 10px;
      height: 30px;
      width: 30px;
      background-color: transparent;
      transition: 0.5s;
      opacity: 0;

      @media @bw768 {
        right: 5px;
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
  }
</style>
