#pragma warning disable CS0659

using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class CompanyDetailed
{
    [JsonPropertyName("isRegistrationCertificate")]
    public bool HasRegistrationCertificate { get; set; }

    [JsonPropertyName("isManufacturer")]
    public bool IsManufacturer { get; set; }

    [JsonPropertyName("company")]
    public CompanyMain CompanyMain { get; set; }

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
}