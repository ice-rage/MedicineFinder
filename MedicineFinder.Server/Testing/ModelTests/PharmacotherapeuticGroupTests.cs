using MedicineFinder.Server.Models;
using NUnit.Framework;

namespace MedicineFinder.Server.Testing.ModelTests
{
    /// <summary>
    /// Класс, содержащий модульные тесты для класса
    /// <see cref="PharmacotherapeuticGroupTests"/>.
    /// </summary>
    [TestFixture]
    public class PharmacotherapeuticGroupTests
    {
        /// <summary>
        /// Тестовый объект <see cref="PharmacotherapeuticGroup"/>.
        /// </summary>
        private readonly PharmacotherapeuticGroup 
            _testPharmacotherapeuticGroup = new()
        {
            Code = "Противомикробное средство - фторхинолон"
        };

        [TestCase(TestName = "При сравнении различных объектов " +
                             "возвращается false")]
        public void TestEquals_DifferentObjects_ReturnsFalse()
        {
            // Arrange
            var expected = _testPharmacotherapeuticGroup;

            // Act
            var actual = (PharmacotherapeuticGroup)expected.Clone();
            actual.Code = "Спазмолитическое средство";
            var isEqual = actual.Equals(expected);

            // Assert
            Assert.That(isEqual, Is.False);
        }

        [TestCase(TestName = "При сравнении объекта с null возвращается " +
                             "false")]
        public void TestEquals_NullObject_ReturnsFalse()
        {
            // Arrange
            var expected = _testPharmacotherapeuticGroup;

            // Act
            var actual = (PharmacotherapeuticGroup)expected.Clone();
            var isEqual = actual.Equals(null);

            // Assert
            Assert.That(isEqual, Is.False);
        }
    }
}
