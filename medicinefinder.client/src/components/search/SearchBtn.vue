<template>
  <button
    type="button"
    title="Искать" 
    class="search-btn"
    :class="{ 'search-btn--active' : active }"
    @click="fetchMedicineData(text)"
  >
    <div class="search-btn__circle"></div>
    <div class="search-btn__stick"></div>
  </button>
</template>

<script setup>
  const store = useStore();

  const { fetchMedicineData } = store;

  const { text, active } = defineProps({
    text: {
      type: String,
      default: "",
      required: true,
    },
    active: {
      type: Boolean,
      default: false,
      required: true,
    },
  });
</script>

<style lang="less">
  .search-btn {
    position: relative;
    max-width: 40px;
    padding: 0;
    border-radius: 0 4px 4px 0;
    background-color: @indian_green;
    background: fade(@indian_green, 60%);
    transition: all 0.3s ease-in-out;
    pointer-events: none;

    @media @bw768 {
      max-width: 30px;
    }
    
    &--active {
      background: rgba(@indian_green, 1);
      pointer-events: auto;

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
            left: 6px;
            width: 20px;
            height: 4px;
            border-color: transparent;
            background-color: @dandelion;
            border-radius: 16px;

            @media @bw768 {
              left: 5px;
              width: 15px;
              height: 3px;
              border-radius: 12px;
            }
          }

          .search-btn__stick {
            top: 45%;
            margin-top: -7px;
            transform: rotateZ(0);

            @media @bw768 {
              margin-top: -5px;
            }
          }

          .search-btn__stick:before,
          .search-btn__stick:after {
            background-color: @dandelion;
          }

          .search-btn__stick:before {
            bottom: 4px;
            transform: rotateZ(60deg);

            @media @bw768 {
              bottom: 3px;
            }
          }

          .search-btn__stick:after {
            bottom: -4px;
            transform: rotateZ(-60deg);

            @media @bw768 {
              bottom: -3px;
            }
          }
        }
      }
    }

    &__circle {
      position: relative;
      top: -4px;
      left: 8px;
      width: 12px;
      height: 12px;
      border: 4px solid @white;
      border-radius: 50%;
      background-color: transparent;
      background-clip: padding-box;
      transition: 0.3s ease-in-out;

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
      top: 20px;
      left: 30px;
      display: block;
      width: 4px;
      height: 12px;
      border-radius: 12px;
      background-color: transparent;
      transform: rotateZ(52deg);
      transition: 0.3s ease all;

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
        width: 12px;
        height: 4px;
        border-radius: 8px;
        background-color: @white;
        transform: rotateZ(0);
        transition: 0.5s ease all;

        @media @bw768 {
          width: 9px;
          height: 3px;
          border-radius: 6px;
        }
      }
    }
  }
</style>