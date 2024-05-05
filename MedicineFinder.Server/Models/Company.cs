using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class Company
{
    [JsonPropertyName("isRegistrationCertificate")]
    public bool HasRegistrationCertificate { get; set; }

    [JsonPropertyName("isManufacturer")]
    public bool IsManufacturer { get; set; }

    [JsonPropertyName("company")]
    public CompanyDetailedInfo CompanyDetailedInfo { get; set; }

    [JsonPropertyName("rusName")]
    public string RussianName { get; set; }

    [JsonPropertyName("engName")]
    public string RussianAddress { get; set; }

    [JsonPropertyName("country")]
    public Country Country { get; set; }
}