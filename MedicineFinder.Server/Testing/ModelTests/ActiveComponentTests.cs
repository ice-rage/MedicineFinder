using System.Text.Json;
using MedicineFinder.Server.Models;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace MedicineFinder.Server.Testing.ModelTests
{
    /// <summary>
    /// Класс, содержащий модульные тесты для класса
    /// <see cref="ActiveComponent"/>.
    /// </summary>
    public class ActiveComponentTests
    {
        private readonly ActiveComponent _testActiveComponent = 
            CreateTestActiveComponent();

        [TestCase(TestName = "Позитивный тест геттера LatinName")]
        public void TestLatinNameGet_ReturnsSameValue()
        {
            // Arrange
            var expected = "ofloxacin";

            // Act
            var actual = _testActiveComponent.LatinName;

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(TestName = "Позитивный тест сеттера LatinName")]
        public void TestLatinNameSet_SetsValueCorrectly()
        {
            // Arrange
            var expected = _testActiveComponent.LatinName;

            // Act
            _testActiveComponent.LatinName = expected;
            var actual = _testActiveComponent.LatinName;

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(TestName = "Позитивный тест геттера RussianName")]
        public void TestRussianNameGet_ReturnsSameValue()
        {
            // Arrange
            var expected = "офлоксацин";

            // Act
            var actual = _testActiveComponent.RussianName;

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(TestName = "Позитивный тест сеттера RussianName")]
        public void TestRussianNameSet_SetsValueCorrectly()
        {
            // Arrange
            var expected = _testActiveComponent.RussianName;

            // Act
            _testActiveComponent.RussianName = expected;
            var actual = _testActiveComponent.RussianName;

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(TestName = "Позитивный тест геттера QualityStandard")]
        public void TestQualityStandardGet_ReturnsSameValue()
        {
            // Arrange
            var expected = new QualityStandard
            {
                Name = "Rec.INN",
                Description = "зарегистрированное ВОЗ"
            };

            // Act
            var actual = _testActiveComponent.QualityStandard;

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(TestName = "Позитивный тест сеттера QualityStandard")]
        public void TestQualityStandardSet_SetsValueCorrectly()
        {
            // Arrange
            var expected = _testActiveComponent.QualityStandard;

            // Act
            _testActiveComponent.QualityStandard = expected;
            var actual = _testActiveComponent.QualityStandard;

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        private static ActiveComponent CreateTestActiveComponent()
        {
            var testMedicineData = File.ReadAllText(
                Environment.CurrentDirectory + 
                @"\Testing\TestData\TestMedicineData.json");
            var testMedicineInfo = JsonSerializer.Deserialize<MedicineInfo>(
                testMedicineData);

            return testMedicineInfo.Products[0].ActiveComponentNames[0]
                .ActiveComponent;
        }
    }
}
