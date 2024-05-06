using MedicineFinder.Server.Models;

namespace MedicineFinder.Server.Services.Interfaces
{
    public interface IVidalService
    {
        Task<MedicineInfo?> GetMedicineInfo(string value, string filter);
    }
}