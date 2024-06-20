using System.Text.Json;
using MedicineFinder.Server.Services.Interfaces;
using MedicineFinder.Server.Models;

namespace MedicineFinder.Server.Services
{
    /// <summary>
    /// Класс, реализующий интерфейс <see cref="IVidalService"/>.
    /// </summary>
    public class VidalService : IVidalService
    {
        /// <summary>
        /// Предварительно настроенный экземпляр клиента для взаимодействия с API Видаль.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Конструктор класса <see cref="VidalService"/>.
        /// </summary>
        /// <param name="httpClient"> Предварительно настроенный экземпляр клиента
        /// для взаимодействия с API Видаль.</param>
        public VidalService(HttpClient httpClient) => _httpClient = httpClient;

        /// <inheritdoc/>
        public async Task<MedicineInfo> GetMedicineInfo(string filter, string value)
        {
            var response = await _httpClient.GetAsync(
                $"/api/rest/v1/product/list?filter[{filter}]={value}");
            response.EnsureSuccessStatusCode();

            var stringResult = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<MedicineInfo>(stringResult);
        }
    }
}
