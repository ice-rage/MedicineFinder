#pragma warning disable CS0659

using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class Country
{
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("rusName")]
    public string Name { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj is not Country other)
        {
            return false;
        }

        return Code == other.Code && Name == other.Name;
    }
}