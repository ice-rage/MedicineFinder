using MedicineFinder.Server.Enums;
using MedicineFinder.Server.Enums.Extensions;
using NUnit.Framework;

namespace MedicineFinder.Server.Testing.EnumExtensionTests
{
    /// <summary>
    /// Класс, содержащий модульные тесты для класса
    /// <see cref="RequestFilterTypeExtensionTests"/>.
    /// </summary>
    [TestFixture]
    public class RequestFilterTypeExtensionTests
    {
        /// <summary>
        /// Название модульного теста для метода
        /// <see cref="RequestFilterTypeExtension.GetDescription"/>.
        /// </summary>
        private const string TestGetDescription_ReturnsValue_TestName =
            "При вызове метода GetDescription() для параметра {0} должна " +
            "возвращаться строка {1}";

        [TestCaseSource(nameof(GetDescriptionTestCases))]
        public void TestGetDescription_ReturnsValue(RequestFilterType type, 
            string expected)
        {
            // Act
            var actual = type.GetDescription();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Метод-источник тестовых случаев для тестирования метода
        /// <see cref="RequestFilterTypeExtension.GetDescription"/>.
        /// </summary>
        /// <returns> Перечисление тестовых случаев <see cref="TestCaseData"/>.
        /// </returns>
        private static IEnumerable<TestCaseData> GetDescriptionTestCases()
        {
            yield return new TestCaseData(RequestFilterType.Name, "name")
                .SetName(TestGetDescription_ReturnsValue_TestName);

            yield return new TestCaseData(RequestFilterType.Barcode, "barCode")
                .SetName(TestGetDescription_ReturnsValue_TestName);
        }
    }
}
