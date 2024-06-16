#pragma warning disable CS0659


using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models
{
    public class MedicineInfo
    {
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