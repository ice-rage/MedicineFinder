using MedicineFinder.Server.Enums;
using MedicineFinder.Server.Models;

namespace MedicineFinder.Server.Services.Interfaces
{
    /// <summary>
    /// Интерфейс, описывающий функции сервиса для обращения к API Видаль.
    /// </summary>
    public interface IVidalService
    {
        /// <summary>
        /// Метод для получения информации о лекарственном препарате по фильтру запроса и
        /// по соответствующему значению указанного фильтра.
        /// </summary>
        /// <param name="filter"> Фильтр запроса (используемые фильтры приведены
        /// в перечислении <see cref="RequestFilterType"/>).</param>
        /// <param name="value"> Значение фильтра запроса.</param>
        /// <returns> Асинхронная операция, которая возвращает объект <see cref="MedicineInfo"/>.
        /// </returns>
        Task<MedicineInfo> GetMedicineInfo(string filter, string value);
    }
}