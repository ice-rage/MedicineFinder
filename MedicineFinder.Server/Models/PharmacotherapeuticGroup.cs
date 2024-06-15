#pragma warning disable CS0659

using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class PharmacotherapeuticGroup
{
    [JsonPropertyName("code")]
    public string Code { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj is not PharmacotherapeuticGroup other)
        {
            return false;
        }

        return Code == other.Code;
    }
}