#pragma warning disable CS0659

using System.Text.Json;
using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class ActiveComponentName : ICloneable
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("molecule")]
    public ActiveComponent ActiveComponent { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj is not ActiveComponentName other)
        {
            return false;
        }

        return Id == other.Id && Equals(ActiveComponent, 
            other.ActiveComponent);
    }

    /// <inheritdoc/>
    public object Clone()
    {
        var serialized = JsonSerializer.Serialize(this);

        return JsonSerializer.Deserialize<ActiveComponentName>(
            serialized);
    }
}