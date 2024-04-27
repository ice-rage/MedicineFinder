<template>
  <article div="article">
    <div class="article__container">
      <div class="article__content content">
        <h1>Заголовок h1</h1>
        <h2>Заголовок h2</h2>
        <h3>Заголовок h3</h3>
        <h4>Заголовок h4</h4>
        <h5>Заголовок h5</h5>
        <h6>Заголовок h6</h6>

        <p>
          Абзац текста, содержащий <b>жирный</b> текст, <i>курсив</i>
          и <a href="https://yandex.ru/" target="_blank">ссылку</a>
        </p>
        
        <ul>
          <li>Элемент списка 1</li>
          <li>Элемент списка 2</li>
          <li>Элемент списка 3</li>
        </ul>

        <ol>
          <li>Элемент списка 1</li>
          <li>Элемент списка 2</li>
          <li>Элемент списка 3</li>
        </ol>

        <blockquote>Здесь содержится цитата</blockquote>

        <img alt="Подпись картинки">
      </div>
    </div>
  </article>
</template>

<style lang="less">
  .article {
    &__container {
      .container();
    }

    &__content {
      margin: 30px 0;
    }
  }
</style>