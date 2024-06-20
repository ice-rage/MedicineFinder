using System.Text.RegularExpressions;
using MedicineFinder.Server.Enums;
using MedicineFinder.Server.Enums.Extensions;
using MedicineFinder.Server.Models;
using MedicineFinder.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OnBarcode.Barcode.BarcodeScanner;
using Tesseract;

namespace MedicineFinder.Server.Controllers
{
    /// <summary>
    /// Класс контроллера, отвечающего за взаимодействие клиентской и серверной стороны
    /// веб-приложения, обработку изображений, а также отправку запросов к API Видаль с помощью
    /// сервиса, реализующего интерфейс <see cref="IVidalService"/>.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MedicineFinderController : Controller
    {
        /// <summary>
        /// Задержка между запросами, выраженная в миллисекундах (связана с ограничением доступа
        /// к API Видаль, при котором возможно совершать не более одного запроса в секунду).
        /// </summary>
        private const int RequestDelayMilliseconds = 1000;

        /// <summary>
        /// Объект сервиса, реализующий интерфейс <see cref="IVidalService"/>.
        /// </summary>
        private readonly IVidalService _vidalService;

        /// <summary>
        /// Объект логгера, реализующий интерфейс <see cref="ILogger"/>.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Конструктор класса <see cref="MedicineFinderController"/>.
        /// </summary>
        /// <param name="vidalService"> Сервис для обращения к API Видаль.</param>
        /// <param name="logger"> Логгер для ведения журнала операций, выполняемых на сервере.
        /// </param>
        public MedicineFinderController(IVidalService vidalService, 
            ILogger<MedicineFinderController> logger)
        {
            _vidalService = vidalService;
            _logger = logger;
        }

        /// <summary>
        /// GET-метод для получения информации о лекарственном препарате на основе текста,
        /// полученного в результате обработки изображения или напрямую из текстового запроса.
        /// </summary>
        /// <param name="filter"> Фильтр запроса (возможные фильтры приведены в перечислении
        /// <see cref="RequestFilterType"/>).</param>
        /// <param name="value"> Значение фильтра, по которому производится поиск.</param>
        /// <returns> Асинхронный результат действия (код ответа от API Видаль + объект
        /// <see cref="MedicineInfo"/> в том случае, если поиск завершился успехом).</returns>
        [HttpGet("{value}")]
        public async Task<IActionResult> GetMedicineInfo(string filter, string value)
        {
            try
            {
                var currentFilter = filter ?? RequestFilterType.Name.GetDescription();

                var result = await _vidalService.GetMedicineInfo(currentFilter, value);

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

        /// <summary>
        /// POST-метод для получения растрового изображения упаковки или штрихкода лекарственного
        /// препарата от клиента, распознавания текста на нем и отправки соответствующего GET-
        /// запроса к API Видаль.
        /// </summary>
        /// <param name="encodedImage"> Изображение упаковки или штрихкода препарата в строковом
        /// формате Base64.</param>
        /// <returns> Асинхронный результат действия (код ответа от API Видаль + объект
        /// <see cref="MedicineInfo"/> в том случае, если поиск завершился успехом).</returns>
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm]string encodedImage)
        {
            var contentString = encodedImage[(encodedImage.LastIndexOf(',') + 1)..];
            var decodedImage = Convert.FromBase64String(contentString);

            _logger.LogInformation("{DT}: изображение успешно получено", 
                        DateTime.UtcNow.ToLongTimeString());

            var words = RecognizeText(decodedImage);

            var statusCodes = new List<int>();

            var response = await HandleTextResults(words, statusCodes,
                RequestFilterType.Name.GetDescription());

            var responseStatusCode = ((IStatusCodeActionResult)response).StatusCode;

            if (responseStatusCode is 200 or 500)
            {
                return response;
            }

            statusCodes.Clear();

            var barcodes = ReadBarcode(decodedImage);

            return await HandleTextResults(barcodes, statusCodes,
                RequestFilterType.Barcode.GetDescription());
        }

        /// <summary>
        /// Метод для обработки результатов распознавания текста на изображении упаковки или
        /// штрихкода лекарственного препарата.
        /// </summary>
        /// <param name="textResults"> Непосредственно результаты распознавания текста.</param>
        /// <param name="statusCodes"> Коды состояния HTTP, полученные по каждому из результатов
        /// поиска.</param>
        /// <param name="filter"> Фильтр, по которому необходимо производить поиск.</param>
        /// <returns></returns>
        private async Task<IActionResult> HandleTextResults(List<string> textResults, 
            List<int> statusCodes, string filter)
        {
            foreach (var textResult in textResults)
            {
                var response = await GetMedicineInfo(filter, textResult);
                var statusCode = ((IStatusCodeActionResult)response).StatusCode;

                if (statusCode == 200)
                {
                    _logger.LogInformation(
                        "{DT}: успешно получен результат по тексту на изображении",
                        DateTime.UtcNow.ToLongTimeString());

                    return Ok(((OkObjectResult)response).Value);
                }

                statusCodes.Add(statusCode.GetValueOrDefault());

                await Task.Delay(RequestDelayMilliseconds);
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

        /// <summary>
        /// Метод для распознавания текста на изображении упаковки лекарственного препарата.
        /// </summary>
        /// <param name="packingImage"> Растровое изображение упаковки препарата в формате JPEG или
        /// PMG.</param>
        /// <returns> Список результатов распознавания.</returns>
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

                // Заменяем слово пустой строкой, если оно содержит не алфавитно-цифровые символы
                // (подразумевается русский алфавит).
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

        /// <summary>
        /// Метод для распознавания штрихкода с изображения упаковки лекарственного препарата.
        /// </summary>
        /// <param name="barcodeImage"> Массив байтов, представляющий растровое изображение
        /// штрихкода (исходное изображение может быть в формате JPEG или PNG).</param>
        /// <returns> Список результатов распознавания.</returns>
        private static List<string> ReadBarcode(byte[] barcodeImage)
        {
            using var memoryStream = new MemoryStream(barcodeImage);

            return [.. BarcodeScanner.Scan(memoryStream, BarcodeType.All)];
        }
    }
}