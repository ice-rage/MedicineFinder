using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class Product
{
    [JsonPropertyName("atcCodes")]
    public List<AtcCode> AtcCodes { get; set; }

    [JsonPropertyName("phthgroups")]
    public List<PharmacotherapeuticGroup> PharmacotherapeuticGroups { get; set; }

    [JsonPropertyName("ClPhGroups")]
    public List<ClinicalPharmacologicalGroup> ClinicalPharmacologicalGroups { get; set; }

    [JsonPropertyName("moleculeNames")]
    public List<ActiveComponentName> ActiveComponentNames { get; set; }

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
}