<template>
  <section class="resulting-info">
    <hr class="resulting-info__content-separator">

    <div class="resulting-info__title">
      <span 
        class="resulting-info__inner-title" 
        v-html="data.rusName">
      </span>
      (<span 
        class="resulting-info__inner-title" 
        v-html="data.engName">
      </span>)
    </div>

    <h2 class="resulting-info__section-title">
      Владелец регистрационного удостоверения
    </h2>
    <p class="resulting-info__paragraph">
      {{ data.companies[0].company.name }}
      <span class="resulting-info__parentheses">
        ({{ data.companies[0].company.country.rusName }})
      </span>
    </p>

    <h2 class="resulting-info__section-title">Произведено</h2>
    <p class="resulting-info__paragraph">
      {{ data.companies[1]
        ? data.companies[1].company.gddbName
        : "" }}
      <span class="resulting-info__parentheses">
        ({{ data.companies[1]
          ? data.companies[1].company.country.rusName
          : "" }})
      </span>
    </p>

    <h2 class="resulting-info__section-title">Контакты для обращений</h2>
    <div v-html="data.document.companies[0] 
      ? data.document.companies[0].rusAddress 
      : ''"></div>

    <h2 class="resulting-info__section-title">Код АТХ</h2>
    <p class="resulting-info__paragraph">
      {{ data.atcCodes[0].code }}
      <span class="resulting-info__parentheses">
        ({{ data.atcCodes[0].rusName }})
      </span>
    </p>

    <h2 class="resulting-info__section-title">Стандарт качества</h2>
    <p  class="resulting-info__paragraph">
      {{ data.moleculeNames[0].molecule.gnParent.gnParent }}
      <span class="resulting-info__parentheses">
        ({{ data.moleculeNames[0].molecule.gnParent.description }})
      </span>
    </p>

    <h2 class="resulting-info__section-title">Активные вещества</h2>
    <p class="resulting-info__paragraph">
      {{ data.moleculeNames[0].molecule.rusName }}
      <span class="resulting-info__parentheses">
        ({{ data.moleculeNames[0].molecule.latName }})
      </span>
    </p>

    <div class="resulting-info__content content">
      <h2>Лекарственные формы</h2>
      <ul>
        <li>{{ data.zipInfo }}</li>
        <li>{{ data.childrens[0].zipInfo }}</li>
      </ul>

      <h2>Форма выпуска, упаковка и состав препарата Аспирин</h2>

      <div v-html="data.composition"></div>
      <div v-html="data.childrens[0].composition"></div>

      <h2>Клинико-фармакологическая группа</h2>
      <p>{{ data.clPhGroups[0].name }}</p>

      <h2>Фармако-терапевтическая группа</h2>
      <p>{{ data.phthgroups[0].code }}</p>

      <h2>Фармакологическое действие</h2>
      <div v-html="data.document.phInfluence"></div>

      <h2>Показания препарата Аспирин</h2>
      <div v-html="data.document.indication"></div>

      <h2>Режим дозирования</h2>
      <div v-html="data.document.dosage"></div>

      <h2>Побочное действие</h2>
      <div v-html="data.document.sideEffects"></div>

      <h2>Противопоказания к применению</h2>
      <div v-html="data.document.contraIndication"></div>

      <h2>Применение при беременности и кормлении грудью</h2>
      <div v-html="data.document.lactation"></div>

      <h2>Применение при нарушениях функции печени</h2>
      <div v-html="data.document.hepatoInsuf"></div>

      <h2>Применение при нарушениях функции почек</h2>
      <div v-html="data.document.renalInsuf"></div>

      <h2>Применение у детей</h2>
      <div v-html="data.document.childInsuf"></div>

      <h2>Особые указания</h2>
      <div v-html="data.document.specialInstruction"></div>

      <h2>Передозировка</h2>
      <div v-html="data.document.overDosage"></div>

      <h2>Лекарственное взаимодействие</h2>
      <div v-html="data.document.interaction"></div>

      <h2>Условия хранения препарата Аспирин</h2>
      <div v-html="data.document.storageCondition"></div>

      <h2>Срок годности препарата Аспирин</h2>
      <div v-html="data.document.storageTime"></div>

      <h2>Условия реализации</h2>
      <div v-html="data.document.pharmDelivery"></div>
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
      margin-bottom: 20px;
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