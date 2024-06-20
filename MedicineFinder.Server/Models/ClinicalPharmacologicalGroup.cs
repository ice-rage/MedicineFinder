#pragma warning disable CS0659

using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

/// <summary>
/// Класс для хранения клинико-фармакологической группы лекарственного препарата.
/// </summary>
public class ClinicalPharmacologicalGroup : ICloneable
{
    /// <summary>
    /// Наименование клинико-фармакологической группы.
    /// </summary>
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

    /// <inheritdoc/>
    public object Clone() => MemberwiseClone();
}