using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class ActiveComponentName
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("molecule")]
    public ActiveComponent ActiveComponent { get; set; }
}