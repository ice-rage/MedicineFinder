#pragma warning disable CS0659

using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class ClinicalPharmacologicalGroup
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj is not ClinicalPharmacologicalGroup other)
        {
            return false;
        }

        return Name == other.Name;
    }
}