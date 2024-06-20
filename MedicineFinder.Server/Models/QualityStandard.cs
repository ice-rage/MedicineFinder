#pragma warning disable CS0659

using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

/// <summary>
/// Класс для хранения информации о стандарте качества лекарственного препарата.
/// </summary>
public class QualityStandard : ICloneable
{
    /// <summary>
    /// Наименование.
    /// </summary>
    [JsonPropertyName("GNParent")]
    public string Name { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj is not QualityStandard other)
        {
            return false;
        }

        return Name == other.Name && Description == other.Description;
    }

    /// <inheritdoc/>
    public object Clone() => MemberwiseClone();
}