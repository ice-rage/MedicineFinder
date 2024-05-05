using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class CompanyDetailedInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("country")]
    public Country Country { get; set; }
}