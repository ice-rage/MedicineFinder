#pragma warning disable CS0659

using System.Text.Json;
using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class CompanyMain : ICloneable
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("country")]
    public Country Country { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj is not CompanyMain other)
        {
            return false;
        }

        return Name == other.Name && Equals(Country, other.Country);
    }

    /// <inheritdoc/>
    public object Clone()
    {
        var serialized = JsonSerializer.Serialize(this);

        return JsonSerializer.Deserialize<CompanyMain>(serialized);
    }
}