using System.Text.Json;
using MedicineFinder.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicineFinder.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicineFinderController : Controller
    {
        [HttpGet("{name}")]
        public async Task<IActionResult> GetMedicineInfo(string name)
        {
            using var client = new HttpClient();

            try
            {
                client.BaseAddress = new Uri("https://www.vidal.ru");
                client.DefaultRequestHeaders.Add("x-token", "5HnGnVMkMx5e");
                var response = await client.GetAsync(
                    $"/api/rest/v1/product/list?filter[name]={name}");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Rootobject>(stringResult);

                return Ok(result);
            }
            catch (HttpRequestException httpRequestException)
            {
                return BadRequest($"Error getting info from Vidal: " +
                                  $"{httpRequestException.Message}");
            }
        }
    }
}
