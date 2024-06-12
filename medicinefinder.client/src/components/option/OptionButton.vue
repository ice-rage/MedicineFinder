<template>
  <button 
    type="button"
    class="option-btn"
    :class="{ 'option-btn--active' : isActive }"
  >
    <component 
      :is="icon" 
      width="24" 
      height="24" 
      class="option-btn__icon">
    </component>
    
    {{ text }}
  </button>
</template>

<script setup>
  const { isActive, icon, text } = defineProps({
    isActive: {
      type: Boolean,
      default: false,
      required: true,
    },
    icon: {
      type: Object,
      default: () => {},
      required: true,
    },
    text: {
      type: String,
      default: "",
      required: true,
    },
  });
</script>

<style lang="less">
  .option-btn {
    .gradient-btn();

    &,
    &--active {
      @media @bw768 {
        padding: 10px;
        font-size: 0;
      }

      & + button {
        margin-left: 20px;

        @media @bw768 {
          margin-left: 25px;
        }
      }
    }

    &--active {
      background-image: none;
      background-color: @indian_green;
      transition: background-image 1s ease-in-out;
    }

    &__icon {
      display: none;

      @media @bw768 {
        display: flex;
      }
    }
  }
</style>