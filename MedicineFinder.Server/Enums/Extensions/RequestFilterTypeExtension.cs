using System.ComponentModel;

namespace MedicineFinder.Server.Enums.Extensions
{                                                                               
    /// <summary>
    /// Статический класс, расширяющий перечисление фильтров запроса к API Видаль
    /// <see cref="RequestFilterType"/>.
    /// </summary>
    public static class RequestFilterTypeExtension
    {
        /// <summary>
        /// Статический метод для получения текстового описания фильтра запроса.                    
        /// </summary>
        /// <param name="type"> Тип фильтра запроса, выраженный значением перечисления
        /// <see cref="RequestFilterType"/>.</param>
        /// <returns> Текстовое описание фильтра запроса.</returns>
        public static string GetDescription(this RequestFilterType type)
        {
            var description = type.ToString();
            var field = type.GetType().GetField(type.ToString());

            if (field?.GetCustomAttributes(typeof(DescriptionAttribute), false) 
                    is DescriptionAttribute[] attributes && attributes.Length != 0)
            {
                description = attributes.First().Description;
            }

            return description;
        }
    }
}
