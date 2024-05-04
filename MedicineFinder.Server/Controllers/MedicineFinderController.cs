using System.Text.RegularExpressions;
using MedicineFinder.Server.Interfaces;
using MedicineFinder.Server.Models;
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
        public async Task<IActionResult> UploadImage(ImageFile imageFile)
        {
            var originalString = imageFile.Image;
            var onlyContentString = originalString[(originalString.LastIndexOf(",") + 1)..];
            var result = Convert.FromBase64String(onlyContentString);

            _logger.LogInformation("{DT}: изображение успешно получено", 
                        DateTime.UtcNow.ToLongTimeString());

            var recognizedText = RecognizeText(result);
            var statusCodes = new List<int?>();
            
            foreach (var word in recognizedText)
            {
                var response = await GetMedicineInfo(word);
                var statusCode = ((IStatusCodeActionResult)response).StatusCode;

                statusCodes.Add(statusCode);

                if (statusCode == 200)
                {
                    _logger.LogInformation(
                        "{DT}: успешно получен результат по тексту на изображении", 
                        DateTime.UtcNow.ToLongTimeString());
                    return Ok(((OkObjectResult)response).Value);
                }
            }

            if (statusCodes.Any(statusCode => statusCode == 400)) {
                return BadRequest();
            }

            _logger.LogInformation(
                "{DT}: не удалось получить результат по тексту на изображении", 
                DateTime.UtcNow.ToLongTimeString());
            
            return NotFound();
        }

        private IEnumerable<string> RecognizeText(byte[] decodedImage)
        {
            var engine = new TesseractEngine("./tessdata", "rus", EngineMode.Default);
            var image = Pix.LoadFromMemory(decodedImage);
            var page = engine.Process(image);
            var text = page
                .GetText()
                .Split("\n\n")
                .ToList();
            text.RemoveAll(string.IsNullOrWhiteSpace);

            var words = text.ToArray();

            for (var i = 0; i < words.Length; i++)
            {
                words[i] = Regex.Replace(words[i], "[^а-яА-Я0-9]", string.Empty);
            }
            
            image.Dispose();
            page.Dispose();
            engine.Dispose();

            return words;
        }
    }
}