using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class Children
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("zipInfo")]
    public string Summary { get; set; }

    [JsonPropertyName("composition")]
    public string Composition { get; set; }

    [JsonPropertyName("companies")]
    public List<Company> Companies { get; set; }
}