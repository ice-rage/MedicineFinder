using MedicineFinder.Server.Models;
using NUnit.Framework;

namespace MedicineFinder.Server.Testing.ModelTests
{
    /// <summary>
    /// Класс, содержащий модульные тесты для класса <see cref="CompanyDetailed"/>.
    /// </summary>
    [TestFixture]
    public class CompanyDetailedTests
    {
        /// <summary>
        /// Тестовый объект <see cref="CompanyDetailed"/>.
        /// </summary>
        private readonly CompanyDetailed _testCompanyDetailed = new()
        {
            HasRegistrationCertificate = true,
            IsManufacturer = false,
            CompanyMain = new CompanyMain
            {
                Name = "БАУШ ХЕЛС",
                Country = new Country
                {
                    Code = "RUS",
                    Name = "Россия"
                }
            }
        };

        [TestCase(TestName = "При сравнении различных объектов возвращается false")]
        public void TestEquals_DifferentObjects_ReturnsFalse()
        {
            // Arrange
            var expected = _testCompanyDetailed;

            // Act
            var actual = (CompanyDetailed)expected.Clone();
            actual.IsManufacturer = true;

            var isEqual = actual.Equals(expected);

            // Assert
            Assert.That(isEqual, Is.False);
        }

        [TestCase(TestName = "При сравнении объекта с null возвращается false")]
        public void TestEquals_NullObject_ReturnsFalse()
        {
            // Arrange
            var expected = _testCompanyDetailed;

            // Act
            var actual = (CompanyDetailed)expected.Clone();

            var isEqual = actual.Equals(null);

            // Assert
            Assert.That(isEqual, Is.False);
        }
    }
}
