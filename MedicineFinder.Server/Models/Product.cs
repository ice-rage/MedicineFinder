#pragma warning disable CS0659

using System.Text.Json;
using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

/// <summary>
/// Класс для хранения информации о лекарственном препарате (продукте).
/// </summary>
public class Product : ICloneable
{
    /// <summary>
    /// Коды АТХ.
    /// </summary>
    [JsonPropertyName("atcCodes")]
    public List<AtcCode> AtcCodes { get; set; }

    /// <summary>
    /// Список фармакотерапевтических групп.
    /// </summary>
    [JsonPropertyName("phthgroups")]
    public List<PharmacotherapeuticGroup> PharmacotherapeuticGroups { get; set; }

    /// <summary>
    /// Список клинико-фармакологических групп.
    /// </summary>
    [JsonPropertyName("ClPhGroups")]
    public List<ClinicalPharmacologicalGroup> ClinicalPharmacologicalGroups { get; set; }

    /// <summary>
    /// Список активных веществ.
    /// </summary>
    [JsonPropertyName("moleculeNames")]
    public List<ActiveComponentName> ActiveComponentNames { get; set; }

    /// <summary>
    /// Русское наименование.
    /// </summary>
    [JsonPropertyName("rusName")]
    public string RussianName { get; set; }
    
    /// <summary>
    /// Английское наименование.
    /// </summary>
    [JsonPropertyName("engName")]
    public string EnglishName { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    [JsonPropertyName("zipInfo")]
    public string Summary { get; set; }

    /// <summary>
    /// Состав.
    /// </summary>
    [JsonPropertyName("composition")]
    public string Composition { get; set; }

    /// <summary>
    /// Список компаний-владельцев регистрационного удостоверения и компаний-производителей.
    /// </summary>
    [JsonPropertyName("companies")]
    public List<CompanyDetailed> Companies { get; set; }

    /// <summary>
    /// Инструкция по применению.
    /// </summary>
    [JsonPropertyName("document")]
    public Instruction Instruction { get; set; }

    /// <summary>
    /// Список других лекарственных форм.
    /// </summary>
    [JsonPropertyName("childrens")]
    public List<Child> Children { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj is not Product other)
        {
            return false;
        }

        return AtcCodes.SequenceEqual(other.AtcCodes) &&
               PharmacotherapeuticGroups.SequenceEqual(
                   other.PharmacotherapeuticGroups) &&
               ClinicalPharmacologicalGroups.SequenceEqual(
                   other.ClinicalPharmacologicalGroups) &&
               ActiveComponentNames.SequenceEqual(
                   other.ActiveComponentNames) &&
               RussianName == other.RussianName &&
               EnglishName == other.EnglishName &&
               Summary == other.Summary &&
               Composition == other.Composition &&
               Companies.SequenceEqual(other.Companies) &&
               Equals(Instruction, other.Instruction) &&
               Children.SequenceEqual(other.Children);
    }

    /// <inheritdoc/>
    public object Clone()
    {
        var serialized = JsonSerializer.Serialize(this);

        return JsonSerializer.Deserialize<Product>(serialized);
    }
}