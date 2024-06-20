﻿using MedicineFinder.Server.Models;
using NUnit.Framework;

namespace MedicineFinder.Server.Testing.ModelTests
{
    /// <summary>
    /// Класс, содержащий модульные тесты для класса <see cref="Country"/>.
    /// </summary>
    [TestFixture]
    public class CountryTests
    {
        /// <summary>
        /// Тестовый объект <see cref="Country"/>.
        /// </summary>
        private readonly Country _testCountry = new()
        {
            Code = "RUS",
            Name = "Россия"
        };

        [TestCase(TestName = "При сравнении различных объектов возвращается false")]
        public void TestEquals_DifferentObjects_ReturnsFalse()
        {
            // Arrange
            var expected = _testCountry;

            // Act
            var actual = (Country)expected.Clone();
            actual.Name = "Европейская фармакопея";

            var isEqual = actual.Equals(expected);

            // Assert
            Assert.That(isEqual, Is.False);
        }

        [TestCase(TestName = "При сравнении объекта с null возвращается false")]
        public void TestEquals_NullObject_ReturnsFalse()
        {
            // Arrange
            var expected = _testCountry;

            // Act
            var actual = (Country)expected.Clone();

            var isEqual = actual.Equals(null);

            // Assert
            Assert.That(isEqual, Is.False);
        }
    }
}
