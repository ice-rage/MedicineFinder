#pragma warning disable CS0659

using System.Text.Json;
using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

/// <summary>
/// Класс для хранения активного вещества лекарственного препарата.
/// </summary>
public class ActiveComponent : ICloneable
{
    /// <summary>
    /// Латинское наименование активного вещества.
    /// </summary>
    [JsonPropertyName("latName")]
    public string LatinName { get; set; }

    /// <summary>
    /// Русское наименование активного вещества.
    /// </summary>
    [JsonPropertyName("rusName")]
    public string RussianName { get; set; }

    /// <summary>
    /// Стандарт качества.
    /// </summary>
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