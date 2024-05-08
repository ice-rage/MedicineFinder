using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class AtcCode
{
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("rusName")]
    public string Name { get; set; }
}