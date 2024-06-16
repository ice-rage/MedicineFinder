#pragma warning disable CS0659

using System.Text.Json;
using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class Product : ICloneable
{
    [JsonPropertyName("atcCodes")]
    public List<AtcCode> AtcCodes { get; set; }

    [JsonPropertyName("phthgroups")]
    public List<PharmacotherapeuticGroup> 
        PharmacotherapeuticGroups { get; set; }

    [JsonPropertyName("ClPhGroups")]
    public List<ClinicalPharmacologicalGroup> 
        ClinicalPharmacologicalGroups { get; set; }

    [JsonPropertyName("moleculeNames")]
    public List<ActiveComponentName> 
        ActiveComponentNames { get; set; }

    [JsonPropertyName("rusName")]
    public string RussianName { get; set; }

    [JsonPropertyName("engName")]
    public string EnglishName { get; set; }

    [JsonPropertyName("zipInfo")]
    public string Summary { get; set; }

    [JsonPropertyName("composition")]
    public string Composition { get; set; }

    [JsonPropertyName("companies")]
    public List<CompanyDetailed> Companies { get; set; }

    [JsonPropertyName("document")]
    public Instruction Instruction { get; set; }

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