﻿using MedicineFinder.Server.Controllers;
using MedicineFinder.Server.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NUnit.Framework;

namespace MedicineFinder.Server.Testing.ControllerTests
{
    /// <summary>
    /// Класс, содержащий модульные тесты для класса <see cref="MedicineFinderController"/>.
    /// </summary>
    [TestFixture]
    public class MedicineFinderControllerTests
    {
        /// <summary>
        /// Токен доступа к API Видаль.
        /// </summary>
        private const string AccessTokenName = "x-token";

        /// <summary>
        /// Строковый ключ для доступа к url-адресу API Видаль.
        /// </summary>
        private const string VidalApiKey = "VidalApi";

        /// <summary>
        /// Задержка между запросами, выраженная в миллисекундах (связана с ограничением доступа к
        /// API Видаль, при котором возможно совершать не более одного запроса в секунду).
        /// </summary>
        private const int RequestDelayMilliseconds = 1000;

        /// <summary>
        /// Тестовая конфигурация серверной части приложения, используемая для извлечения токена
        /// доступа к API Видаль.
        /// </summary>
        private static readonly IConfiguration TestConfiguration = InitializeConfiguration();

        /// <summary>
        /// Словарь для получения полных путей к тестовым изображениям по их условному типу
        /// (ожидаемому результату обработки).
        /// </summary>
        private readonly Dictionary<string, string> _testEncodedImageByType = new()
            {
                { "NameSuccess", 
                    GetTestEncodedImage(GetTestImageFullPath(
                        @"Testing\TestData\Найз.png"))
                },
                { "NameNotFound",
                    GetTestEncodedImage(GetTestImageFullPath(
                        @"Testing\TestData\Печенье.jpg"))
                },
                {
                    "BarcodeSuccess", GetTestEncodedImage(
                        GetTestImageFullPath(
                            @"Testing\TestData\Кетопрофен.jpg"))
                },
                {
                    "BarcodeNotFound", GetTestEncodedImage(
                        GetTestImageFullPath(
                            @"Testing\TestData\Coca-Cola.png"))
                }
            };

        /// <summary>
        /// Представляет контроллер в его "нормальном" состоянии (т.е. в нем верно заданы настройки
        /// сервиса <see cref="VidalService"/>).
        /// </summary>
        private readonly MedicineFinderController _normalMedicineFinderController = new(
            new VidalService(new HttpClient
                {
                    BaseAddress = new Uri(TestConfiguration[VidalApiKey]!),
                    DefaultRequestHeaders =
                    {
                        {
                            AccessTokenName, 
                            TestConfiguration[AccessTokenName]
                        }
                    }
                }), InitializeLogger());

        /// <summary>
        /// Представляет контроллер в его "поврежденном" состоянии (т.е. в нем неверно заданы
        /// настройки сервиса <see cref="VidalService"/>, а именно - передан некорректный базовый
        /// URL-адрес API Видаль).
        /// </summary>
        private readonly MedicineFinderController _corruptedMedicineFinderController = new(
                new VidalService(new HttpClient
                {
                    BaseAddress = new Uri("https://wrongbaseurl.ru"),
                    DefaultRequestHeaders =
                    {
                        {
                            AccessTokenName,
                            TestConfiguration[AccessTokenName]
                        }
                    }
                }), InitializeLogger());

        [TestCase(
            "Аспирин",
            200,
            TestName = "Если препарат найден, должен возвращаться код состояния HTTP 200")]
        [TestCase(
            "Несуществующий препарат",
            404,
            "name", 
            TestName = "Если препарат не найден, должен возвращаться код состояния HTTP 404")]
        [TestCase(
            null,
            500,
            "Несуществующий фильтр", 
            TestName = "Если произошла неизвестная ошибка, должен возвращаться код состояния HTTP " +
                       "500")]
        public async Task TestGetMedicineInfo_DifferentParams_ReturnsStatusCodeCorrectly(
             string value, int expectedStatusCode, string filter = null)
        {
            // Act
            var result = await _normalMedicineFinderController.GetMedicineInfo(filter, value);
            var actual = ((IStatusCodeActionResult)result).StatusCode;

            // Assert
            Assert.That(actual, Is.EqualTo(expectedStatusCode));

            await Task.Delay(RequestDelayMilliseconds);
        }

        [TestCase(
            "NameSuccess",
            200,
            TestName = "При загрузке изображения упаковки препарата должен возвращаться код " +
                       "состояния HTTP 200")]
        [TestCase(
            "NameNotFound",
            404,
            TestName = "При загрузке изображения без упаковки препарата должен возвращаться код " +
                       "состояния HTTP 404")]
        [TestCase(
            "BarcodeSuccess",
            200,
            TestName = "При загрузке изображения штрихкода препарата должен возвращаться код " +
                       "состояния HTTP 200")]
        [TestCase(
            "BarcodeNotFound",
            404,
            TestName = "При загрузке изображения без штрихкода препарата должен возвращаться код " +
                       "состояния HTTP 404")]
        public async Task TestUploadImage_ReturnsExpectedStatusCode(string imageType, 
            int expectedStatusCode)
        {
            // Arrange
            var testEncodedImage = _testEncodedImageByType[imageType];

            // Act
            var result = await _normalMedicineFinderController.UploadImage(testEncodedImage);
            var actual = ((IStatusCodeActionResult)result).StatusCode;

            // Assert
            Assert.That(actual, Is.EqualTo(expectedStatusCode));
        }

        [TestCase(TestName = "Если произошла неизвестная ошибка, должен возвращаться код " +
                             "состояния HTTP 500")]
        public async Task TestHandleTextResults_BadApiUrl_ReturnsStatusCode500()
        {
            // Arrange
            var expected = 500;

            var testEncodedImage = _testEncodedImageByType["NameSuccess"];

            // Act
            var result = await _corruptedMedicineFinderController.UploadImage(testEncodedImage);
            var actual = ((IStatusCodeActionResult)result).StatusCode;

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Метод для создания и настройки конфигурации серверной части приложения.
        /// </summary>
        /// <returns> Созданная конфигурация.</returns>
        private static IConfiguration InitializeConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables()
                .Build();

            return configuration;
        }

        /// <summary>
        /// Метод для инициализации логгера, используемого в контроллере
        /// <see cref="MedicineFinderController"/>.
        /// </summary>
        /// <returns> Созданный объект логгера.</returns>
        private static ILogger<MedicineFinderController> InitializeLogger()
        {
            using var factory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = factory.CreateLogger<MedicineFinderController>();

            return logger;
        }

        /// <summary>
        /// Метод для преобразования тестового изображения в строку в формате Base64.
        /// </summary>
        /// <param name="imageFilePath"> Полный путь к тестовому изображению.</param>
        /// <returns> Строка в формате Base64, которая представляет исходное изображение.</returns>
        private static string GetTestEncodedImage(string imageFilePath)
        {
            var imageBytes = File.ReadAllBytes(imageFilePath);

            return Convert.ToBase64String(imageBytes);
        }

        /// <summary>
        /// Метод для получения полного пути к тестовому изображению.
        /// </summary>
        /// <param name="relativeImageFilePath"> Путь к тестовому изображению относительно папки
        /// тестирования.</param>
        /// <returns> Полный путь к данному изображению.</returns>
        private static string GetTestImageFullPath(string relativeImageFilePath) => 
            Path.GetFullPath(relativeImageFilePath, Directory.GetCurrentDirectory());
    }
}
