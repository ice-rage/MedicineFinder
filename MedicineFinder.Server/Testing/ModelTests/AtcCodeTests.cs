using MedicineFinder.Server.Models;
using NUnit.Framework;

namespace MedicineFinder.Server.Testing.ModelTests
{
    /// <summary>
    /// Класс, содержащий модульные тесты для класса <see cref="AtcCode"/>.
    /// </summary>
    [TestFixture]
    public class AtcCodeTests
    {
        /// <summary>
        /// Тестовый объект <see cref="AtcCode"/>.
        /// </summary>
        private readonly AtcCode _testAtcCode = new()
        {
            Code = "S01AE01",
            Name = "Офлоксацин"
        };

        [TestCase(TestName = "При сравнении различных объектов " +
                             "возвращается false")]
        public void TestEquals_DifferentObjects_ReturnsFalse()
        {
            // Arrange
            var expected = _testAtcCode;

            // Act
            var actual = (AtcCode)expected.Clone();
            actual.Code = "T12BD12";
            var isEqual = actual.Equals(expected);

            // Assert
            Assert.That(isEqual, Is.False);
        }

        [TestCase(TestName = "При сравнении объекта с null возвращается " +
                             "false")]
        public void TestEquals_NullObject_ReturnsFalse()
        {
            // Arrange
            var expected = _testAtcCode;

            // Act
            var actual = (AtcCode)expected.Clone();
            var isEqual = actual.Equals(null);

            // Assert
            Assert.That(isEqual, Is.False);
        }
    }
}
