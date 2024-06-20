#pragma warning disable CS0659

using System.Text.Json;
using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

/// <summary>
/// Класс для хранения инструкции по применению лекарственного препарата.
/// </summary>
public class Instruction : ICloneable
{
    /// <summary>
    /// Условия хранения.
    /// </summary>
    [JsonPropertyName("storageCondition")]
    public string StorageCondition { get; set; }

    /// <summary>
    /// Срок годности.
    /// </summary>
    [JsonPropertyName("storageTime")]
    public string StorageLife { get; set; }

    /// <summary>
    /// Список компаний-владельцев регистрационного удостоверения и компаний-производителей.
    /// </summary>
    [JsonPropertyName("companies")]
    public List<CompanyDetailed> Companies { get; set; }

    /// <summary>
    /// Фармакологическое действие.
    /// </summary>
    [JsonPropertyName("phInfluence")]
    public string PharmacologicalEffect { get; set; }

    /// <summary>
    /// Фармакокинетика.
    /// </summary>
    [JsonPropertyName("phKinetics")]
    public string Pharmacokinetics { get; set; }

    /// <summary>
    /// Режим дозирования.
    /// </summary>
    [JsonPropertyName("dosage")]
    public string DosageRegimen { get; set; }

    /// <summary>
    /// Передозировка.
    /// </summary>
    [JsonPropertyName("overDosage")]
    public string Overdosage { get; set; }

    /// <summary>
    /// Лекарственное взаимодействие.
    /// </summary>
    [JsonPropertyName("interaction")]
    public string Interaction { get; set; }

    /// <summary>
    /// Применение при беременности и грудном вскармливании.
    /// </summary>
    [JsonPropertyName("lactation")]
    public string Lactation { get; set; }

    /// <summary>
    /// Побочные эффекты.
    /// </summary>
    [JsonPropertyName("sideEffects")]
    public string SideEffects { get; set; }

    /// <summary>
    /// Показания к применению.
    /// </summary>
    [JsonPropertyName("indication")]
    public string Indications { get; set; }

    /// <summary>
    /// Противопоказания к применению.
    /// </summary>
    [JsonPropertyName("contraIndication")]
    public string Contraindications { get; set; }

    /// <summary>
    /// Особые указания.
    /// </summary>
    [JsonPropertyName("specialInstruction")]
    public string SpecialInstructions { get; set; }

    /// <summary>
    /// Применение при нарушении функции почек.
    /// </summary>
    [JsonPropertyName("renalInsuf")]
    public string RenalImpairedUsage { get; set; }

    /// <summary>
    /// Применение при нарушении функций печени.
    /// </summary>
    [JsonPropertyName("hepatoInsuf")]
    public string HepaticImpairedUsage { get; set; }

    /// <summary>
    /// Условия реализации.
    /// </summary>
    [JsonPropertyName("pharmDelivery")]
    public string ImplementationTerms { get; set; }

    /// <summary>
    /// Применение у пожилых пациентов.
    /// </summary>
    [JsonPropertyName("elderlyInsuf")]
    public string ElderlyUsage { get; set; }

    /// <summary>
    /// Применение у детей.
    /// </summary>
    [JsonPropertyName("childInsuf")]
    public string ChildrenUsage { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj is not Instruction other)
        {
            return false;
        }

        return StorageCondition == other.StorageCondition &&
               StorageLife == other.StorageLife &&
               Companies.SequenceEqual(other.Companies) &&
               PharmacologicalEffect == other.PharmacologicalEffect &&
               Pharmacokinetics == other.Pharmacokinetics &&
               DosageRegimen == other.DosageRegimen &&
               Overdosage == other.Overdosage &&
               Interaction == other.Interaction &&
               Lactation == other.Lactation &&
               SideEffects == other.SideEffects &&
               Indications == other.Indications &&
               Contraindications == other.Contraindications &&
               SpecialInstructions == other.SpecialInstructions &&
               RenalImpairedUsage == other.RenalImpairedUsage &&
               HepaticImpairedUsage == other.HepaticImpairedUsage &&
               ImplementationTerms == other.ImplementationTerms &&
               ElderlyUsage == other.ElderlyUsage &&
               ChildrenUsage == other.ChildrenUsage;
    }

    /// <inheritdoc/>
    public object Clone()
    {
        var serialized = JsonSerializer.Serialize(this);

        return JsonSerializer.Deserialize<Instruction>(serialized);
    }
}