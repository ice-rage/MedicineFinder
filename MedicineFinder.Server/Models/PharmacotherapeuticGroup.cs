#pragma warning disable CS0659

using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

/// <summary>
/// Класс для хранения фармакотерапевтической группы лекарственного препарата.
/// </summary>
public class PharmacotherapeuticGroup : ICloneable
{
    /// <summary>
    /// Код фармакотерапевтической группы.
    /// </summary>
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

    /// <inheritdoc/>
    public object Clone() => MemberwiseClone();
}