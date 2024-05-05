using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class QualityStandard
{
    [JsonPropertyName("GNParent")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}