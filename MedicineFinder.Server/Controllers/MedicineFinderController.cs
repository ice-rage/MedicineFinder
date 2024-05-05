using System.Text.RegularExpressions;
using MedicineFinder.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Tesseract;

namespace MedicineFinder.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicineFinderController : Controller
    {
        private readonly IVidalService _vidalService;

        private readonly ILogger _logger;

        public MedicineFinderController(IVidalService vidalService, 
            ILogger<MedicineFinderController> logger)
        {
            _vidalService = vidalService;
            _logger = logger;
        }

        [HttpGet("{medicineName}")]
        public async Task<IActionResult> GetMedicineInfo(string medicineName)
        {
            try
            {
                var result = await _vidalService.GetMedicineInfo(medicineName);

                if (result != null && result.products.Count != 0) {
                    _logger.LogInformation("{DT}: данные по запросу успешно получены", 
                        DateTime.UtcNow.ToLongTimeString());
                    return Ok(result.products[0]);
                }

                _logger.LogInformation("{DT}: не удалось найти данные по запросу", 
                        DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }
            catch (HttpRequestException)
            {
                _logger.LogInformation("{DT}: неизвестная ошибка сервера", 
                        DateTime.UtcNow.ToLongTimeString());
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm]string encodedImage)
        {
            var contentString = encodedImage[(encodedImage.LastIndexOf(',') + 1)..];
            var decodedImage = Convert.FromBase64String(contentString);

            _logger.LogInformation("{DT}: изображение успешно получено", 
                        DateTime.UtcNow.ToLongTimeString());

            var words = RecognizeText(decodedImage);
            var statusCodes = new List<int?>();
            
            foreach (var word in words)
            {
                var response = await GetMedicineInfo(word);
                var statusCode = ((IStatusCodeActionResult)response).StatusCode;

                if (statusCode == 200)
                {
                    _logger.LogInformation(
                        "{DT}: успешно получен результат по тексту на изображении", 
                        DateTime.UtcNow.ToLongTimeString());
                    
                    return Ok(((OkObjectResult)response).Value);
                }

                statusCodes.Add(statusCode);
            }

            if (statusCodes.Any(statusCode => statusCode == 400)) 
            {
                return BadRequest();
            }

            _logger.LogInformation(
                "{DT}: не удалось получить результат по тексту на изображении", 
                DateTime.UtcNow.ToLongTimeString());
            
            return NotFound();
        }

        private static List<string> RecognizeText(byte[] imageBytes)
        {
            var engine = new TesseractEngine("./tessdata", "rus", EngineMode.Default);
            var image = Pix.LoadFromMemory(imageBytes);
            var page = engine.Process(image);
            var text = page
                .GetText()
                .Split("\n\n")
                .ToList();
            text.RemoveAll(string.IsNullOrWhiteSpace);

            for (var i = 0; i < text.Count; i++) 
            {
                text[i] = Regex.Replace(text[i], "[^а-яА-Я0-9]", string.Empty);
            }
            
            image.Dispose();
            page.Dispose();
            engine.Dispose();

            return text;
        }
    }
}