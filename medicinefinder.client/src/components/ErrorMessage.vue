<template>
  <div class="error-message">
    <div class="error-message__text">{{ message }}</div>
  </div>
</template>

<script setup>
  defineProps(["message"]);
</script>

<style lang="less">
  .error-message {
    box-sizing: border-box;
    max-width: 600px;
    padding: 20px;
    background-color: #fce4e4;
    border-radius: 10px;

    @media @bw1170 {
      padding: 15px 20px;
    }

    @media @bw768 {
      padding: 10px 20px;
    }

    &__text {
      color: #cc0033;
      font-family: @font1;
      font-size: 13px;
      font-weight: 600;
      text-align: center;
      text-shadow: 1px 1px rgba(250, 250, 250, 0.3);

      @media @bw1170 {
        font-size: 12px;
      }

      @media @bw768 {
        font-size: 11px;
      }
    }
  }
</style>