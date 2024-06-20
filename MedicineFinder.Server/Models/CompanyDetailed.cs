#pragma warning disable CS0659

using System.Text.Json;
using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

/// <summary>
/// Класс для хранения подробной информации о компаниях-владельцах регистрационного удостоверения
/// и компаниях-производителях лекарственного препарата.
/// </summary>
public class CompanyDetailed : ICloneable
{
    /// <summary>
    /// Показывает, есть у компании регистрационное удостоверение.
    /// </summary>
    [JsonPropertyName("isRegistrationCertificate")]
    public bool HasRegistrationCertificate { get; set; }

    /// <summary>
    /// Показывает, является ли компания производителем.
    /// </summary>
    [JsonPropertyName("isManufacturer")]
    public bool IsManufacturer { get; set; }

    /// <summary>
    /// Основная информация о компании.
    /// </summary>
    [JsonPropertyName("company")]
    public CompanyMain CompanyMain { get; set; }

    /// <summary>
    /// Адрес компании.
    /// </summary>
    [JsonPropertyName("rusAddress")]
    public string Address { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj is not CompanyDetailed other)
        {
            return false;
        }

        return HasRegistrationCertificate ==
               other.HasRegistrationCertificate &&
               IsManufacturer == other.IsManufacturer &&
               Equals(CompanyMain, other.CompanyMain) &&
               Address == other.Address;
    }

    /// <inheritdoc/>
    public object Clone()
    {
        var serialized = JsonSerializer.Serialize(this);

        return JsonSerializer.Deserialize<CompanyDetailed>(serialized);
    }
}