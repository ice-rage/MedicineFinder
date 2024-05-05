using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class PharmacotherapeuticGroup
{
    [JsonPropertyName("code")]
    public string Code { get; set; }
}