<template>
  <div class="selected-image">
    <div class="selected-image__picture-wrapper">
      <picture class="selected-image__picture">
        <img
          alt="Выбранное изображение" 
          class="selected-image__image"
        />
      </picture>
      
      <button 
        type="button" 
        title="Выбрать другое" 
        class="selected-image__reshoot-btn">
      </button>
    </div>
    
  </div>
</template>

<script setup>

</script>

<style lang="less">
  .selected-image {
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;

    &__canvas {
      display: none;
    }

    &__picture-wrapper {
      position: relative;
      display: flex;
      max-width: 500px;

      &:hover {
        .selected-image__reshoot-btn {
          top: 0;
          opacity: 1;
        }
      }
    }

    &__picture {
      .green-gradient();
      .responsive-parent(@padding-top: 75%);

      margin: 0 auto 20px;
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

    &__reshoot-btn {
      position: absolute;
      top: -40px;
      right: 0;
      height: 40px;
      width: 40px;
      margin: 5px 10px;
      background-color: transparent;
      transition: 0.5s;
      opacity: 0;

      &::before,
      &::after {
        content: "";
        position: absolute;
        box-sizing: border-box;
        display: block;
        margin: 0;
        padding: 0;
      }

      &::before {
        height: 30px;
        width: 30px;
        border: 5px solid;
        border-color: transparent @dandelion @dandelion @dandelion;
        border-radius: 50%;
        transform: rotate(45deg);
      }

      &::after {
        position: absolute;
        height: 0;
        width: 0;
        border: 12px solid;
        border-color: transparent transparent transparent @dandelion;
        transform: translate(0.8rem, -0.6rem);
      }
    }
  }
</style>