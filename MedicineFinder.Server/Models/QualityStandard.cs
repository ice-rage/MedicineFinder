using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class QualityStandard : IEquatable<QualityStandard>
{
    [JsonPropertyName("GNParent")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    /// Метод для сравнения текущего экземпляра <see cref="QualityStandard"/>
    /// с другим объектом <see cref="QualityStandard"/>.
    /// </summary>
    /// <param name="other"> Экземпляр <see cref="QualityStandard"/>, с которым
    /// сравнивается текущий объект <see cref="QualityStandard"/></param>.
    /// <returns> <see langword="true"/>, если объекты <see cref="QualityStandard"/>
    /// равны, и <see langword="false"/> в противном случае.</returns>
    public bool Equals(QualityStandard other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Name == other.Name && Description == other.Description;
    }

    /// <summary>
    /// Обобщенный метод для сравнения текущего объекта
    /// <see cref="QualityStandard"/> с другим объектом.
    /// </summary>
    /// <param name="obj"> Объект, с которым сравнивается текущий объект
    /// <see cref="QualityStandard"/>.</param>
    /// <returns> <see langword="true"/>, если объект
    /// <see cref="QualityStandard"/> и объект <see cref="object"/> равны, и
    /// <see langword="false"/> в противном случае.</returns>
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj.GetType() == GetType() && Equals((QualityStandard)obj);
    }

    /// <summary>
    /// Метод для генерации хеш-кода, использующегося при сравнении объекта
    /// <see cref="QualityStandard"/> с другим объектом.
    /// </summary>
    /// <returns> Полученное значение хеш-кода.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Description);
    }
}