#pragma warning disable CS0659

using System.Text.Json;
using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class ActiveComponent : ICloneable
{
    [JsonPropertyName("latName")]
    public string LatinName { get; set; }

    [JsonPropertyName("rusName")]
    public string RussianName { get; set; }

    [JsonPropertyName("GNParent")]
    public QualityStandard QualityStandard { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj is not ActiveComponent other)
        {
            return false;
        }

        return LatinName == other.LatinName &&
               RussianName == other.RussianName &&
               Equals(QualityStandard, other.QualityStandard);
    }

    /// <inheritdoc/>
    public object Clone()
    {
        var serialized = JsonSerializer.Serialize(this);

        return JsonSerializer.Deserialize<ActiveComponent>(
            serialized);
    }
}