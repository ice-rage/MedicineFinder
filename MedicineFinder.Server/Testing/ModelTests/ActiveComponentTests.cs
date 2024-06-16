using MedicineFinder.Server.Models;
using NUnit.Framework;

namespace MedicineFinder.Server.Testing.ModelTests
{
    /// <summary>
    /// Класс, содержащий модульные тесты для класса
    /// <see cref="ActiveComponent"/>.
    /// </summary>
    [TestFixture]
    public class ActiveComponentTests
    {
        /// <summary>
        /// Тестовый объект <see cref="ActiveComponent"/>.
        /// </summary>
        private readonly ActiveComponent _testActiveComponent = new()
        {
            LatinName = "ofloxacin",
            RussianName = "офлоксацин",
            QualityStandard = new QualityStandard
            {
                Name = "Rec.INN",
                Description = "зарегистрированное ВОЗ"
            }
        };

        [TestCase(TestName = "При сравнении различных объектов " +
                             "возвращается false")]
        public void TestEquals_DifferentObjects_ReturnsFalse()
        {
            // Arrange
            var expected = _testActiveComponent;

            // Act
            var actual = (ActiveComponent)expected.Clone();
            actual.LatinName = "No-Spa";
            var isEqual = actual.Equals(expected);

            // Assert
            Assert.That(isEqual, Is.False);
        }

        [TestCase(TestName = "При сравнении объекта с null возвращается " +
                             "false")]
        public void TestEquals_NullObject_ReturnsFalse()
        {
            // Arrange
            var expected = _testActiveComponent;

            // Act
            var actual = (ActiveComponent)expected.Clone();
            var isEqual = actual.Equals(null);

            // Assert
            Assert.That(isEqual, Is.False);
        }
    }
}
