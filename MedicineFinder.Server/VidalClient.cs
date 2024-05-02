using MedicineFinder.Server.Models;

namespace MedicineFinder.Server
{
    public class VidalClient
    {
        private readonly HttpClient _client;

        public VidalClient(HttpClient client) => _client = client;

        public async Task<Rootobject> GetMedicineInfo(string name)
        {
            return await _client.GetFromJsonAsync<Rootobject>(
                $"/api/rest/v1/product/list?filter[name]={name}");
        }
    }
}
