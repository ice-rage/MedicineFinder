using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class ClinicalPharmacologicalGroup
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}