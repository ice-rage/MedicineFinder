<template>
  <section class="resulting-info">
    <hr class="resulting-info__content-separator">

    <div class="resulting-info__title">
      <span 
        v-if="data.rusName"
        class="resulting-info__inner-title" 
        v-html="data.rusName">
      </span>
      (<span 
        v-if="data.engName"
        class="resulting-info__inner-title" 
        v-html="data.engName">
      </span>)
    </div>

    <div 
      v-for="(company, companyIndex) in productCompanies" 
      :key="companyIndex"
      class="resulting-info__companies" 
    >
      <section 
        v-if="company.isRegistrationCertificate"
        class="resulting-info__owner-company"
      >
        <h2 
          v-if="company.company" 
          class="resulting-info__section-title"
        >
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
        v-if="company.isManufacturer"
        class="resulting-info__manufacturing-company"
      >
        <h2 
          v-if="company.company" 
          class="resulting-info__section-title"
        >
          Произведено
        </h2>
        <p class="resulting-info__paragraph">
          {{ company.company.name }}

          <span class="resulting-info__parentheses">
            ({{ company.company.country.rusName }})
          </span>
        </p>
      </section>
    </div>

    <section 
      v-if="data.document.companies.length" 
      class="resulting-info__contacts"
    >
      <h2 
        v-if="documentCompanies.length"
        class="resulting-info__section-title"
      >
        Контакты для обращений
      </h2>
      <p
        v-for="(company, companyIndex) in documentCompanies" 
        :key="companyIndex"
        v-html="company.rusAddress"
      ></p>
    </section>
    
    <h2 
      v-if="atcCodes.length"
      class="resulting-info__section-title"
    >
      Коды АТХ
    </h2>
    <p
      v-for="atcCode in atcCodes" 
      :key="atcCode.code"
      class="resulting-info__paragraph"
    >
      {{ atcCode.code }}
      <span class="resulting-info__parentheses">
        ({{ atcCode.rusName }})
      </span>
    </p>

    <h2 
      v-if="moleculeNames.length" 
      class="resulting-info__section-title"
    >
      Стандарт качества
    </h2>
    <div
      v-for="moleculeName in moleculeNames"
      :key="moleculeName.id"
      class="resulting-info__paragraph"
    >
      <span
        v-if="moleculeName.molecule.GNParent"
        v-html="moleculeName.molecule.GNParent.GNParent" 
      >
      </span>
      <span 
        v-if="moleculeName.molecule.GNParent"
        class="resulting-info__parentheses" 
      >
        ({{ moleculeName.molecule.GNParent 
              ? moleculeName.molecule.GNParent.description
              : "" }})
      </span>
    </div>

    <h2 
      v-if="moleculeNames.length" 
      class="resulting-info__section-title"
    >
      Активные вещества
    </h2>
    <p 
      v-for="moleculeName in moleculeNames"
      :key="moleculeName.id"
      class="resulting-info__paragraph"
    >
      {{ moleculeName.molecule.rusName }}
      <span class="resulting-info__parentheses">
        ({{moleculeName.molecule.latName }})
      </span>
    </p>

    <div class="resulting-info__content content">
      <h2 v-if="data.zipInfo">Лекарственные формы</h2>
      <ul>
        <li>{{ data.zipInfo }}</li>
        <li v-for="children in childrens" :key="children.id">
          {{ children.zipInfo }}
        </li>
      </ul>

      <h2>
        Форма выпуска, упаковка и состав препарата
        <span v-if="data.rusName" v-html="data.rusName"></span>
      </h2>

      <div v-html="data.composition"></div>

      <div v-for="children in childrens" :key="children.id">
        <p v-html="children.composition"></p>
      </div>
      
      <h2 v-if="clPhGroups.length">
        Клинико-фармакологическая группа
      </h2>
      <div v-for="clPhGroup in clPhGroups" :key="clPhGroup.name">
        <p v-html="clPhGroup.name"></p>
      </div>

      <h2 v-if="phThGroups.length">
        Фармако-терапевтическая группа
      </h2>
      <div v-for="phThGroup in phThGroups" :key="phThGroup.code">
        <p v-html="phThGroup.code"></p>
      </div>

      <h2 v-if="data.document.phInfluence">
        Фармакологическое действие
      </h2>
      <div v-html="data.document.phInfluence"></div>

      <h2 v-if="data.document.indication">
        Показания препарата
        <span v-html="data.rusName"></span>
      </h2>
      <div v-html="data.document.indication"></div>

      <h2 v-if="data.document.dosage">Режим дозирования</h2>
      <div v-html="data.document.dosage"></div>

      <h2 v-if="data.document.sideEffects">Побочное действие</h2>
      <div v-html="data.document.sideEffects"></div>

      <h2 v-if="data.document.contraIndication">
        Противопоказания к применению
      </h2>
      <div v-html="data.document.contraIndication"></div>

      <h2 v-if="data.document.lactation">
        Применение при беременности и кормлении грудью
      </h2>
      <div v-html="data.document.lactation"></div>

      <h2 v-if="data.document.hepatoInsuf">
        Применение при нарушениях функции печени
      </h2>
      <div v-html="data.document.hepatoInsuf"></div>

      <h2 v-if="data.document.renalInsuf">
        Применение при нарушениях функции почек
      </h2>
      <div v-html="data.document.renalInsuf"></div>

      <h2 v-if="data.document.childInsuf">Применение у детей</h2>
      <div v-html="data.document.childInsuf"></div>

      <h2 v-if="data.document.specialInstruction">
        Особые указания
      </h2>
      <div v-html="data.document.specialInstruction"></div>

      <h2 v-if="data.document.overDosage">Передозировка</h2>
      <div v-html="data.document.overDosage"></div>

      <h2 v-if="data.document.interaction">
        Лекарственное взаимодействие
      </h2>
      <div v-html="data.document.interaction"></div>

      <h2 v-if="data.document.storageCondition">
        Условия хранения препарата
        <span v-html="data.rusName"></span>
      </h2>
      <div v-html="data.document.storageCondition"></div>

      <h2 v-if="data.document.storageTime">
        Срок годности препарата
        <span v-html="data.rusName"></span>
      </h2>
      <div v-html="data.document.storageTime"></div>

      <h2 v-if="data.document.pharmDelivery">
        Условия реализации
      </h2>
      <div v-html="data.document.pharmDelivery"></div>
    </div>
  </section>
</template>

<script setup>
  const store = useStore();

  const data = store.medicineData;

  const productCompanies = data.companies;
  const documentCompanies = data.document.companies;
  const atcCodes = data.atcCodes;
  const childrens = data.childrens;
  const moleculeNames = data.moleculeNames;
  const clPhGroups = data.ClPhGroups;
  const phThGroups = data.phthgroups;
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