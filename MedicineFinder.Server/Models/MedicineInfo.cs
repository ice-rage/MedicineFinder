#pragma warning disable CS0659


using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models
{
    /// <summary>
    /// Класс для хранения информации обо всех лекарственных препаратах, удовлетворяющих условиям
    /// поиска.
    /// </summary>
    public class MedicineInfo
    {
        /// <summary>
        /// Список лекарственных препаратов (продуктов), соответствующих результату поиска.
        /// </summary>
        [JsonPropertyName("products")]
        public List<Product> Products { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is MedicineInfo other && 
                   Products.SequenceEqual(other.Products);
        }
    }
}