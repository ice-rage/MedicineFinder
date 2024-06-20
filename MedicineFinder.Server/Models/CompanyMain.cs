#pragma warning disable CS0659

using System.Text.Json;
using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

/// <summary>
/// Класс для хранения основной информации о компаниях-владельцах регистрационного удостоверения
/// и компаниях-производителях лекарственного препарата.
/// </summary>
public class CompanyMain : ICloneable
{
    /// <summary>
    /// Наименование компании.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// Страна компании.
    /// </summary>
    [JsonPropertyName("country")]
    public Country Country { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj is not CompanyMain other)
        {
            return false;
        }

        return Name == other.Name && Equals(Country, other.Country);
    }

    /// <inheritdoc/>
    public object Clone()
    {
        var serialized = JsonSerializer.Serialize(this);

        return JsonSerializer.Deserialize<CompanyMain>(serialized);
    }
}