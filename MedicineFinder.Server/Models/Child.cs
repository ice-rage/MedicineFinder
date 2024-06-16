#pragma warning disable CS0659

using System.Text.Json;
using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class Child : ICloneable
{
    [JsonPropertyName("id")] 
    public int Id { get; set; }

    [JsonPropertyName("zipInfo")] 
    public string Summary { get; set; }

    [JsonPropertyName("composition")] 
    public string Composition { get; set; }

    [JsonPropertyName("companies")] 
    public List<CompanyDetailed> Companies { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj is not Child other)
        {
            return false;
        }

        return Id == other.Id &&
               Summary == other.Summary &&
               Composition == other.Composition &&
               Companies.SequenceEqual(other.Companies);
    }

    /// <inheritdoc/>
    public object Clone()
    {
        var serialized = JsonSerializer.Serialize(this);

        return JsonSerializer.Deserialize<Child>(serialized);
    }
}