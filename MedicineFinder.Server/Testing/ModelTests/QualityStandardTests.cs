using MedicineFinder.Server.Models;
using NUnit.Framework;

namespace MedicineFinder.Server.Testing.ModelTests
{
    /// <summary>
    /// Класс, содержащий модульные тесты для класса
    /// <see cref="QualityStandard"/>.
    /// </summary>
    [TestFixture]
    public class QualityStandardTests
    {
        /// <summary>
        /// Тестовый объект <see cref="QualityStandard"/>.
        /// </summary>
        private readonly QualityStandard _testQualityStandard = new()
        {
            Name = "Rec.INN",
            Description = "зарегистрированное ВОЗ"
        };

        [TestCase(TestName = "При сравнении различных объектов " +
                             "возвращается false")]
        public void TestEquals_DifferentObjects_ReturnsFalse()
        {
            // Arrange
            var expected = _testQualityStandard;

            // Act
            var actual = (QualityStandard)expected.Clone();
            actual.Name = "Европейская фармакопея";
            var isEqual = actual.Equals(expected);

            // Assert
            Assert.That(isEqual, Is.False);
        }

        [TestCase(TestName = "При сравнении объекта с null возвращается " +
                             "false")]
        public void TestEquals_NullObject_ReturnsFalse()
        {
            // Arrange
            var expected = _testQualityStandard;

            // Act
            var actual = (QualityStandard)expected.Clone();
            var isEqual = actual.Equals(null);

            // Assert
            Assert.That(isEqual, Is.False);
        }
    }
}
