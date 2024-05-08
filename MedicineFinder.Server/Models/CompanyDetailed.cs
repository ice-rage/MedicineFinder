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
}