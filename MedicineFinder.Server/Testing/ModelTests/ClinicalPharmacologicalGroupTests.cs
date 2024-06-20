using MedicineFinder.Server.Models;
using NUnit.Framework;

namespace MedicineFinder.Server.Testing.ModelTests
{
    /// <summary>
    /// Класс, содержащий модульные тесты для класса <see cref="ClinicalPharmacologicalGroup"/>.
    /// </summary>
    [TestFixture]
    public class ClinicalPharmacologicalGroupTests
    {
        /// <summary>
        /// Тестовый объект <see cref="ClinicalPharmacologicalGroup"/>.
        /// </summary>
        private readonly ClinicalPharmacologicalGroup 
            _testClinicalPharmacologicalGroup = new()
        {
            Name = "Антибактериальный препарат группы фторхинолонов для местного применения " +
                   "в офтальмологии"
        };

        [TestCase(TestName = "При сравнении различных объектов возвращается false")]
        public void TestEquals_DifferentObjects_ReturnsFalse()
        {
            // Arrange
            var expected = _testClinicalPharmacologicalGroup;

            // Act
            var actual = (ClinicalPharmacologicalGroup)expected.Clone();
            actual.Name = "Спазмолитический препарат";

            var isEqual = actual.Equals(expected);

            // Assert
            Assert.That(isEqual, Is.False);
        }

        [TestCase(TestName = "При сравнении объекта с null возвращается false")]
        public void TestEquals_NullObject_ReturnsFalse()
        {
            // Arrange
            var expected = _testClinicalPharmacologicalGroup;

            // Act
            var actual = (ClinicalPharmacologicalGroup)expected.Clone();

            var isEqual = actual.Equals(null);

            // Assert
            Assert.That(isEqual, Is.False);
        }
    }
}
