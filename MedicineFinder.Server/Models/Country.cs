#pragma warning disable CS0659

using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

/// <summary>
/// Класс для хранения данных о стране компании-владельца регистрационного удостоверения или
/// компании-производителя лекарственного препарата.
/// </summary>
public class Country : ICloneable
{
    /// <summary>
    /// Международный код Alpha-3.
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    [JsonPropertyName("rusName")]
    public string Name { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj is not Country other)
        {
            return false;
        }

        return Code == other.Code && Name == other.Name;
    }

    /// <inheritdoc/>
    public object Clone() => MemberwiseClone();
}