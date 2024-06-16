using System.Text.RegularExpressions;
using MedicineFinder.Server.Enums;
using MedicineFinder.Server.Enums.Extensions;
using MedicineFinder.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OnBarcode.Barcode.BarcodeScanner;
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

        [HttpGet("{value}")]
        public async Task<IActionResult> GetMedicineInfo(string value, string filter)
        {
            try
            {
                var currentFilter = filter ?? RequestFilterType.Name.GetDescription();

                var result = await _vidalService.GetMedicineInfo(value, 
                    currentFilter);

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
                return StatusCode(500);
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
            var statusCodes = new List<int>();

            var response = await HandleTextResults(statusCodes, words, 
                RequestFilterType.Name.GetDescription());

            var responseStatusCode = ((IStatusCodeActionResult)response).StatusCode;

            if (responseStatusCode is 200 or 500)
            {
                return response;
            }

            statusCodes.Clear();

            var barcodes = ReadBarcode(decodedImage);

            return await HandleTextResults(statusCodes, barcodes, 
                RequestFilterType.Barcode.GetDescription());
        }

        private async Task<IActionResult> HandleTextResults(List<int> statusCodes, 
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

                statusCodes.Add(statusCode.GetValueOrDefault());

                await Task.Delay(1000);
            }

            if (statusCodes.Any(statusCode => statusCode == 500))
            {
                return StatusCode(500);
            }

            _logger.LogInformation(
                "{DT}: не удалось получить результат по тексту на изображении",
                DateTime.UtcNow.ToLongTimeString());

            return NotFound();
        }

        private static List<string> RecognizeText(byte[] packingImage)
        {
            var engine = new TesseractEngine("./tessdata", "rus", 
                EngineMode.Default);

            var pixImage = Pix.LoadFromMemory(packingImage);

            var page = engine.Process(pixImage, PageSegMode.Auto);
            var words = page.GetText().Split('\n').ToList();

            // Обработка результатов распознавания текста.
            for (var i = 0; i < words.Count; i++)
            {
                // Заменяем слово пустой строкой, если его длина не более трех символов.
                words[i] = Regex.Replace(words[i], @"\b\w{1,3}\b",
                    string.Empty);

                // Заменяем слово пустой строкой, если оно содержит не алфавитно-
                // цифровые символы (подразумевается русский алфавит).
                words[i] = Regex.Replace(words[i], "[^а-яА-Я0-9]",
                    string.Empty);
            }
            
            // Удаляем все пустые или заполненные пробелами строки из результатов.
            words.RemoveAll(string.IsNullOrWhiteSpace);

            pixImage.Dispose();
            page.Dispose();
            engine.Dispose();

            return words;
        }

        private static List<string> ReadBarcode(byte[] barcodeImage)
        {
            using var memoryStream = new MemoryStream(barcodeImage);

            return [.. BarcodeScanner.Scan(memoryStream, BarcodeType.All)];
        }
    }
}