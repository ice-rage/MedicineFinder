#pragma warning disable CS0659

using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

/// <summary>
/// Класс для хранения кода анатомо-терапевтически-химической (АТХ) классификации лекарственного
/// препарата.
/// </summary>
public class AtcCode : ICloneable
{
    /// <summary>
    /// Код АТХ.
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; set; }

    /// <summary>
    /// Наименование препарата по классификации АТХ.
    /// </summary>
    [JsonPropertyName("rusName")]
    public string Name { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj is not AtcCode other)
        {
            return false;
        }

        return Code == other.Code && Name == other.Name;
    }

    /// <inheritdoc/>
    public object Clone() => MemberwiseClone();
}