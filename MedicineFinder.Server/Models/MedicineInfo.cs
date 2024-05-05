using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models
{
    public class MedicineInfo
    {
        [JsonPropertyName("products")]
        public List<Product> Products { get; set; }
    }
}