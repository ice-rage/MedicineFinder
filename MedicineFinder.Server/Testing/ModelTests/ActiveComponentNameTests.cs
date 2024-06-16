using MedicineFinder.Server.Models;
using NUnit.Framework;

namespace MedicineFinder.Server.Testing.ModelTests
{
    /// <summary>
    /// Класс, содержащий модульные тесты для класса
    /// <see cref="ActiveComponentName"/>.
    /// </summary>
    [TestFixture]
    public class ActiveComponentNameTests
    {
        /// <summary>
        /// Тестовый объект <see cref="ActiveComponentName"/>.
        /// </summary>
        private readonly ActiveComponentName 
            _testActiveComponentName = new()
        {
            Id = 270,
            ActiveComponent = new ActiveComponent
            {
                LatinName = "ofloxacin",
                RussianName = "офлоксацин",
                QualityStandard = new QualityStandard
                {
                    Name = "Rec.INN",
                    Description = "зарегистрированное ВОЗ"
                }
            }
            };

        [TestCase(TestName = "При сравнении различных объектов " +
                             "возвращается false")]
        public void TestEquals_DifferentObjects_ReturnsFalse()
        {
            // Arrange
            var expected = _testActiveComponentName;

            // Act
            var actual = (ActiveComponentName)expected.Clone();
            actual.Id = 1111;
            var isEqual = actual.Equals(expected);

            // Assert
            Assert.That(isEqual, Is.False);
        }

        [TestCase(TestName = "При сравнении объекта с null возвращается " +
                             "false")]
        public void TestEquals_NullObject_ReturnsFalse()
        {
            // Arrange
            var expected = _testActiveComponentName;

            // Act
            var actual = (ActiveComponentName)expected.Clone();
            var isEqual = actual.Equals(null);

            // Assert
            Assert.That(isEqual, Is.False);
        }
    }
}
