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

    <div 
      class="resulting-info__companies" 
      v-for="(company, companyIndex) in productCompanies" :key="companyIndex"
    >
      <section 
        class="resulting-info__owner-company" 
        v-if="company.isRegistrationCertificate"
      >
        <h2 class="resulting-info__section-title">
          Владелец регистрационного удостоверения
        </h2>

        <p class="resulting-info__paragraph">
          {{ company.company.name }}

          <span class="resulting-info__parentheses">
            ({{ company.company.country.rusName }})
          </span>
        </p>
      </section>

      <section 
        class="resulting-info__manufacturing-company" 
        v-if="company.isManufacturer"
      >
        <h2 class="resulting-info__section-title">Произведено</h2>

        <p class="resulting-info__paragraph">
          {{ company.company.name }}

          <span class="resulting-info__parentheses">
            ({{ company.company.country.rusName }})
          </span>
        </p>
      </section>
    </div>

    <section class="resulting-info__contacts" v-if="data.document.companies.length">
      <h2 class="resulting-info__section-title">Контакты для обращений</h2>
      <p
        v-for="(company, companyIndex) in documentCompanies" 
        :key="companyIndex"
        v-html="company.rusAddress"
      ></p>
    </section>
    
    <h2 class="resulting-info__section-title">Коды АТХ</h2>
    <p
      class="resulting-info__paragraph"
      v-for="atcCode in atcCodes" 
      :key="atcCode.code"
    >
      {{ atcCode.code }}
      <span class="resulting-info__parentheses">
        ({{ atcCode.rusName }})
      </span>
    </p>

    <h2 class="resulting-info__section-title">Стандарт качества</h2>
    <div
      class="resulting-info__paragraph"
      v-for="moleculeName in moleculeNames"
      :key="moleculeName.id"
    >
      <span
        v-html="moleculeName.molecule.GNParent.GNParent" 
        v-if="moleculeName.molecule.GNParent">
      </span>
      <span 
        class="resulting-info__parentheses" 
        v-if="moleculeName.molecule.GNParent">
        ({{ moleculeName.molecule.GNParent 
              ? moleculeName.molecule.GNParent.description
              : "" }})
      </span>
    </div>

    <h2 class="resulting-info__section-title">Активные вещества</h2>
    <p 
      class="resulting-info__paragraph"
      v-for="moleculeName in moleculeNames"
      :key="moleculeName.id"
      >
      {{ moleculeName.molecule.rusName }}
      <span class="resulting-info__parentheses">
        ({{moleculeName.molecule.latName }})
      </span>
    </p>

    <div class="resulting-info__content content">
      <h2>Лекарственные формы</h2>
      <ul>
        <li>{{ data.zipInfo }}</li>
        <li v-for="children in childrens" :key="children.id">
          {{ children.zipInfo }}
        </li>
      </ul>

      <h2>
        Форма выпуска, упаковка и состав препарата
        <span v-html="data.rusName"></span>
      </h2>

      <div v-html="data.composition"></div>

      <div v-for="children in childrens" :key="children.id">
        <p v-html="children.composition"></p>
      </div>
      
      <h2>Клинико-фармакологическая группа</h2>
      <div v-for="clPhGroup in clPhGroups" :key="clPhGroup.name">
        <p v-html="clPhGroup.name"></p>
      </div>

      <h2>Фармако-терапевтическая группа</h2>
      <div v-for="phThGroup in phThGroups" :key="phThGroup.code">
        <p v-html="phThGroup.code"></p>
      </div>

      <h2>Фармакологическое действие</h2>
      <div v-html="data.document.phInfluence"></div>

      <h2>
        Показания препарата
        <span v-html="data.rusName"></span>
      </h2>
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

      <h2>
        Условия хранения препарата
        <span v-html="data.rusName"></span>
      </h2>
      <div v-html="data.document.storageCondition"></div>

      <h2>
        Срок годности препарата
        <span v-html="data.rusName"></span>
      </h2>
      <div v-html="data.document.storageTime"></div>

      <h2>Условия реализации</h2>
      <div v-html="data.document.pharmDelivery"></div>
    </div>
  </section>
</template>

<script setup>
  const props = defineProps({
    data: {
      type: Object,
      required: true,
      default: () => {},
    }
  });

  const productCompanies = props.data.companies;
  const documentCompanies = props.data.document.companies;
  const atcCodes = props.data.atcCodes;
  const childrens = props.data.childrens;
  const moleculeNames = props.data.moleculeNames;
  const clPhGroups = props.data.ClPhGroups;
  const phThGroups = props.data.phthgroups;
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