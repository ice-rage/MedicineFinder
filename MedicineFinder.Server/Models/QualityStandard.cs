#pragma warning disable CS0659

using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class QualityStandard
{
    [JsonPropertyName("GNParent")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj is not QualityStandard other)
        {
            return false;
        }

        return Name == other.Name && Description == other.Description;
    }
}