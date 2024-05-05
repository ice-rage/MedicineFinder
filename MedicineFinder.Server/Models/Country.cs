using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class Country
{
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("rusName")]
    public string RussianName { get; set; }
}