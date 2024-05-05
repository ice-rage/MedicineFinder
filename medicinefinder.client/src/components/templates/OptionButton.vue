<template>
  <button 
    type="button" 
    :class="isActive 
      ? 'option-btn--active' 
      : 'option-btn'"
    @click="isActive = !isActive"
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
  const isActive = defineModel();

  defineProps(["icon", "text"]);
</script>

<style lang="less">
  .option-btn,
  .option-btn--active {
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

  .option-btn {
    .gradient-btn();

    &--active {
      background-color: @indian_green;
    }

    &__icon {
      display: none;

      @media @bw768 {
        display: flex;
      }
    }
  }
</style>