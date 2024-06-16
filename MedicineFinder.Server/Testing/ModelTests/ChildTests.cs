using MedicineFinder.Server.Models;
using NUnit.Framework;

namespace MedicineFinder.Server.Testing.ModelTests
{
    /// <summary>
    /// Класс, содержащий модульные тесты для класса <see cref="Child"/>.
    /// </summary>
    [TestFixture]
    public class ChildTests
    {
        /// <summary>
        /// Тестовый объект <see cref="Child"/>.
        /// </summary>
        private readonly Child _testChild = new()
        {
            Id = 2355,
            Summary = "капли глазные 3 мг/1 мл: фл. 5 мл с капельницей",
            Composition =
                "<P><I><B>Капли глазные </B></I>в виде прозрачного раствора светло-желтого цвета.</P>\n<TABLE width=\"100%\" border=\"1\">\n<TR>\n<TD></TD>\n<TD><B>1 мл</B></TD></TR>\n<TR>\n<TD>офлоксацин</TD>\n<TD>3 мг</TD></TR></TABLE>\n<P><i class=\"pring\">Вспомогательные вещества</i>: бензалкония хлорид - 0.025 мг, натрия хлорид - 9 мг, хлористоводородной кислоты 1М раствор - 4.5-5.5 мг, натрия гидроксида 1М раствор - 2.5-3.5 мг, вода д/и - до 1 мл.</P>\n<P>5 мл - флаконы полиэтиленовые (1) с капельницей - пачки картонные.</P>",
            Companies =
            [
                new CompanyDetailed
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
                },

                new CompanyDetailed
                {
                    HasRegistrationCertificate = false,
                    IsManufacturer = true,
                    CompanyMain = new CompanyMain
                    {
                        Name = "Dr. GERHARD MANN Chem.-Pharm. Fabrik",
                        Country = new Country
                        {
                            Code = "DEU",
                            Name = "Германия"
                        }
                    }
                }
            ]
        };

        [TestCase(TestName = "При сравнении различных объектов " +
                             "возвращается false")]
        public void TestEquals_DifferentObjects_ReturnsFalse()
        {
            // Arrange
            var expected = _testChild;

            // Act
            var actual = (Child)expected.Clone();
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
            var expected = _testChild;

            // Act
            var actual = (Child)expected.Clone();
            var isEqual = actual.Equals(null);

            // Assert
            Assert.That(isEqual, Is.False);
        }
    }
}
