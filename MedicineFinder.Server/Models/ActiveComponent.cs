using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class ActiveComponent
{
    [JsonPropertyName("latName")]
    public string LatinName { get; set; }

    [JsonPropertyName("rusName")]
    public string RussianName { get; set; }

    [JsonPropertyName("GNParent")]
    public QualityStandard QualityStandard { get; set; }
}