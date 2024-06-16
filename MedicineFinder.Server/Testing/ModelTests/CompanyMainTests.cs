using MedicineFinder.Server.Models;
using NUnit.Framework;

namespace MedicineFinder.Server.Testing.ModelTests
{
    /// <summary>
    /// Класс, содержащий модульные тесты для класса
    /// <see cref="CompanyMain"/>.
    /// </summary>
    [TestFixture]
    public class CompanyMainTests
    {
        /// <summary>
        /// Тестовый объект <see cref="CompanyMain"/>.
        /// </summary>
        private readonly CompanyMain _testCompanyMain = new()
        {
            Name = "БАУШ ХЕЛС",
            Country = new Country
            {
                Code = "RUS",
                Name = "Россия"
            }
        };

        [TestCase(TestName = "При сравнении различных объектов " +
                             "возвращается false")]
        public void TestEquals_DifferentObjects_ReturnsFalse()
        {
            // Arrange
            var expected = _testCompanyMain;

            // Act
            var actual = (CompanyMain)expected.Clone();
            actual.Country.Code = "DEU";
            var isEqual = actual.Equals(expected);

            // Assert
            Assert.That(isEqual, Is.False);
        }

        [TestCase(TestName = "При сравнении объекта с null возвращается " +
                             "false")]
        public void TestEquals_NullObject_ReturnsFalse()
        {
            // Arrange
            var expected = _testCompanyMain;

            // Act
            var actual = (CompanyMain)expected.Clone();
            var isEqual = actual.Equals(null);

            // Assert
            Assert.That(isEqual, Is.False);
        }
    }
}
