using System.ComponentModel;

namespace MedicineFinder.Server.Enums
{
    /// <summary>
    /// Перечисление, содержащее типы фильтров запроса к API Видаль.
    /// </summary>
    public enum RequestFilterType
    {
        /// <summary>
        /// Фильтр по названию лекарственного препарата.
        /// </summary>
        [Description("name")]
        Name,

        /// <summary>
        /// Фильтр по штрихкоду лекарственного препарата.
        /// </summary>
        [Description("barCode")]
        Barcode
    }
}
