using MedicineFinder.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicineFinder.Server.Controllers
{
    [ApiController]
    [Route("medicinefinder")]
    public class HomeController : Controller
    {
        private readonly IVidalService _vidalService;

        public HomeController(IVidalService vidalService)
        {
            _vidalService = vidalService;
        }

        [HttpGet("{medicineName}")]
        public async Task<IActionResult> GetMedicineInfo(string medicineName)
        {
            try
            {
                var result = await _vidalService.GetMedicineInfo(medicineName);

                if (result != null && result.products.Count != 0) {
                    return Ok(result.products[0]);
                }

                return NotFound();
            }
            catch (HttpRequestException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult UploadImage([FromBody] string base64Image)
        {
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            // Your code here to save the image
            return Json(new { success = true });
        }
    }
}