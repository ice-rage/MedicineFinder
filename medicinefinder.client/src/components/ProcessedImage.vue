<template>
  <div class="processed-image">
    <div class="processed-image__picture-wrapper">
      <picture class="processed-image__picture">
        <img
          :alt="imageAlt"
          class="processed-image__image"
        />
      </picture>
      
      <button 
        type="button" 
        :title="renewBtnTitle"
        class="processed-image__renew-btn">
      </button>

      <button 
        type="button" 
        title="Удалить" 
        class="processed-image__remove-btn">
      </button>
    </div>
  </div>
</template>

<script setup>
  const props = defineProps(["imageAlt", "renewBtnTitle"]);
</script>

<style lang="less">
  .processed-image {
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;

    &__picture-wrapper {
      position: relative;
      display: flex;
      max-width: 520px;

      &:hover {
        .processed-image__renew-btn,
        .processed-image__remove-btn {
          top: 10px;
          opacity: 1;
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
      transition: filter 0.3s;

      &:hover {
        filter: brightness(50%);
      }
    }

    &__renew-btn {
      position: absolute;
      top: -25px;
      right: 25px;
      height: 28px;
      width: 28px;
      background-color: transparent;
      transition: 0.5s;
      opacity: 0;

      &:hover {
        &::before {
          border-color: transparent @dandelion @dandelion;
        }

        &:after {
          border-color: transparent transparent transparent @dandelion;
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
        height: 28px;
        width: 28px;
        border: 5px solid;
        border-color: transparent @white @white;
        border-radius: 50%;
        transform: rotate(65deg);
      }

      &::after {
        position: absolute;
        height: 0;
        width: 0;
        border: 10px solid;
        border-color: transparent transparent transparent @white;
        transform: rotate(30deg) translate(0.8rem, -0.7rem);
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

      &:hover {
        &::before,
        &::after {
          background-color: @dandelion;
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