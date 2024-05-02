<template>
  <section class="resulting-info">
    <hr class="resulting-info__content-separator">

    <div class="resulting-info__title">
      <span 
        class="resulting-info__inner-title" 
        v-html="data.products[0].rusName">
      </span>
      (<span 
        class="resulting-info__inner-title" 
        v-html="data.products[0].engName">
      </span>)
    </div>

    <h2 class="resulting-info__section-title">
      Владелец регистрационного удостоверения
    </h2>
    <p class="resulting-info__paragraph">
      {{ data.products[0].companies[0].company.name }}
      <span class="resulting-info__parentheses">
        ({{ data.products[0].companies[0].company.country.rusName }})
      </span>
    </p>

    <h2 class="resulting-info__section-title">Произведено</h2>
    <p class="resulting-info__paragraph">
      {{ data.products[0].companies[1]
        ? data.products[0].companies[1].company.gddbName
        : "" }}
      <span class="resulting-info__parentheses">
        ({{ data.products[0].companies[1]
          ? data.products[0].companies[1].company.country.rusName
          : "" }})
      </span>
    </p>

    <h2 class="resulting-info__section-title">Контакты для обращений</h2>
    <div v-html="data.products[0].document.companies[0] 
      ? data.products[0].document.companies[0].shortAddress 
      : ''"></div>

    <h2 class="resulting-info__section-title">Код АТХ</h2>
    <p class="resulting-info__paragraph">
      {{ data.products[0].atcCodes[0].code }}
      <span class="resulting-info__parentheses">
        ({{ data.products[0].atcCodes[0].rusName }})
      </span>
    </p>

    <h2 class="resulting-info__section-title">Стандарт качества</h2>
    <p  class="resulting-info__paragraph">
      {{ data.products[0].moleculeNames[0].molecule.gnParent.gnParent }}
      <span class="resulting-info__parentheses">
        ({{ data.products[0].moleculeNames[0].molecule.gnParent.description }})
      </span>
    </p>

    <h2 class="resulting-info__section-title">Активные вещества</h2>
    <p class="resulting-info__paragraph">
      {{ data.products[0].moleculeNames[0].molecule.rusName }}
      <span class="resulting-info__parentheses">
        ({{ data.products[0].moleculeNames[0].molecule.latName }})
      </span>
    </p>

    <div class="resulting-info__content content">
      <h2>Лекарственные формы</h2>
      <ul>
        <li>{{ data.products[0].zipInfo }}</li>
      </ul>

      <h2>Форма выпуска, упаковка и состав препарата Аспирин</h2>

      <div v-html="data.products[0].composition"></div>

      <h2>Клинико-фармакологическая группа</h2>
      <p>{{ data.products[0].clPhGroups[0].name }}</p>

      <h2>Фармако-терапевтическая группа</h2>
      <p>{{ data.products[0].phthgroups[0].code }}</p>

      <h2>Фармакологическое действие</h2>
      <div v-html="data.products[0].document.phInfluence"></div>

      <h2>Показания препарата Аспирин</h2>
      <div v-html="data.products[0].document.indication"></div>

      <h2>Режим дозирования</h2>
      <div v-html="data.products[0].document.dosage"></div>

      <h2>Побочное действие</h2>
      <div v-html="data.products[0].document.sideEffects"></div>

      <h2>Противопоказания к применению</h2>
      <div v-html="data.products[0].document.contraIndication"></div>

      <h2>Применение при беременности и кормлении грудью</h2>
      <div v-html="data.products[0].document.lactation"></div>

      <h2>Применение при нарушениях функции печени</h2>
      <div v-html="data.products[0].document.hepatoInsuf"></div>

      <h2>Применение при нарушениях функции почек</h2>
      <div v-html="data.products[0].document.renalInsuf"></div>

      <h2>Применение у детей</h2>
      <div v-html="data.products[0].document.childInsuf"></div>

      <h2>Особые указания</h2>
      <div v-html="data.products[0].document.specialInstruction"></div>

      <h2>Передозировка</h2>
      <div v-html="data.products[0].document.overDosage"></div>

      <h2>Лекарственное взаимодействие</h2>
      <div v-html="data.products[0].document.interaction"></div>

      <h2>Условия хранения препарата Аспирин</h2>
      <div v-html="data.products[0].document.storageCondition"></div>

      <h2>Срок годности препарата Аспирин</h2>
      <div v-html="data.products[0].document.storageTime"></div>

      <h2>Условия реализации</h2>
      <div v-html="data.products[0].document.pharmDelivery"></div>
    </div>
  </section>
</template>

<script setup>
  defineProps({
    data: Object,
  });
</script>

<style lang="less">
  .resulting-info {
    &__content-separator {
      margin: 40px 0 20px;
      padding: 0;
      height: 6px;
      border: none;
      background: linear-gradient(
        to right, 
        @islamic_green 0%, 
        @light_lime 50%, 
        @white 100%);
    }

    &__title,
    &__section-title {
      margin: 1.3rem 0 0.8rem;
      color: @indian_green;
      font-weight: 700;
    }

    &__title {
      font-size: 22px;

      @media @bw1170 {
        font-size: 20px;
      }

      @media @bw768 {
        font-size: 18px;
      }
    }

    &__section-title {
      font-size: 20px;

      @media @bw1170 {
        font-size: 18px;
      }

      @media @bw768 {
        font-size: 16px;
      }
    }

    &__paragraph {
      margin-bottom: 23px;
      color: @islamic_green;
      font-weight: 500;
      text-align: justify;

      @media @bw768 {
        margin-bottom: 18px;
      }
    }

    &__parentheses {
      color: @pearl;
      font-size: 13px;

      @media @bw768 {
        font-size: 11px;
      }
    }
  }
</style>