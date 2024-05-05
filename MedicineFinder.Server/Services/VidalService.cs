using System.Text.Json;
using MedicineFinder.Server.Interfaces;
using MedicineFinder.Server.Models;

namespace MedicineFinder.Server.Services
{
    public class VidalService : IVidalService
    {
        private readonly HttpClient _httpClient;

        public VidalService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<MedicineInfo?> GetMedicineInfo(string medicineName)
        {
            var response = await _httpClient.GetAsync(
                $"/api/rest/v1/product/list?filter[name]={medicineName}");
            response.EnsureSuccessStatusCode();

            var stringResult = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<MedicineInfo>(stringResult);
        }
    }
}
