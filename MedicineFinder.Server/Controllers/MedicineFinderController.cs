using MedicineFinder.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OnBarcode.Barcode.BarcodeScanner;
using Tesseract;
using static System.Text.RegularExpressions.Regex;

namespace MedicineFinder.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class MedicineFinderController : Controller
    {
        private readonly IVidalService _vidalService;

        private readonly ILogger _logger;

        public MedicineFinderController(IVidalService vidalService, 
            ILogger<MedicineFinderController> logger)
        {
            _vidalService = vidalService;
            _logger = logger;
        }

        [HttpGet("{value}")]
        public async Task<IActionResult> GetMedicineInfo(string value, string filter = "name")
        {
            try
            {
                var result = await _vidalService.GetMedicineInfo(value, filter);

                if (result != null && result.Products.Count != 0) {
                    _logger.LogInformation("{DT}: данные по запросу успешно получены", 
                        DateTime.UtcNow.ToLongTimeString());
                    return Ok(result.Products[0]);
                }

                _logger.LogInformation("{DT}: не удалось найти данные по запросу", 
                        DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }
            catch (HttpRequestException exception)
            {
                _logger.LogInformation("{DT}: неизвестная ошибка сервера: {message}",
                    DateTime.UtcNow.ToLongTimeString(), exception.Message);
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

            var searchByNameResponse = await HandleTextResults(statusCodes, 
                words, "name");

            var responseStatusCode = ((IStatusCodeActionResult)searchByNameResponse)
                .StatusCode;

            if (responseStatusCode == 200)
            {
                return searchByNameResponse;
            }

            statusCodes.Clear();

            var barcodes = ReadBarcode(decodedImage);
            return await HandleTextResults(statusCodes, barcodes, "barCode");
        }

        private async Task<IActionResult> HandleTextResults(ICollection<int?> statusCodes, 
            IEnumerable<string> textResults, string filter)
        {
            foreach (var textResult in textResults)
            {
                var response = await GetMedicineInfo(textResult, filter);
                var statusCode = ((IStatusCodeActionResult)response).StatusCode;

                if (statusCode == 200)
                {
                    _logger.LogInformation(
                        "{DT}: успешно получен результат по тексту на изображении",
                        DateTime.UtcNow.ToLongTimeString());

                    return Ok(((OkObjectResult)response).Value);
                }

                statusCodes.Add(statusCode);

                await Task.Delay(1000);
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

        private static IEnumerable<string> RecognizeText(byte[] packingImage)
        {
            var engine = new TesseractEngine("./tessdata", "rus", 
                EngineMode.Default);
            //engine.SetVariable("tessedit_write_images", true);
            var pixImage = Pix.LoadFromMemory(packingImage);
            var page = engine.Process(pixImage, PageSegMode.Auto);
            var text = page
                .GetText()
                .Split('\n')
                .ToList();

            for (var i = 0; i < text.Count; i++)
            {
                text[i] = AtLeastThreeCharactersRegex().Replace(text[i], 
                    string.Empty);
                text[i] = NonAlphanumericCharactersRegex().Replace(text[i], string.Empty);
            }

            text.RemoveAll(string.IsNullOrWhiteSpace);

            pixImage.Dispose();
            page.Dispose();
            engine.Dispose();

            return text;
        }

        private static IEnumerable<string> ReadBarcode(byte[] barcodeImage)
        {
            using var memoryStream = new MemoryStream(barcodeImage);
            return BarcodeScanner.Scan(memoryStream, BarcodeType.All);
        }

        [System.Text.RegularExpressions.GeneratedRegex(@"\b\w{1,3}\b")]
        private static partial System.Text.RegularExpressions.Regex 
            AtLeastThreeCharactersRegex();

        [System.Text.RegularExpressions.GeneratedRegex("[^а-яА-Я0-9]{1,3}")]
        private static partial System.Text.RegularExpressions.Regex 
            NonAlphanumericCharactersRegex();
    }
}