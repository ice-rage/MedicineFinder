using MedicineFinder.Server.Models;

namespace MedicineFinder.Server.Interfaces
{
    public interface IVidalService
    {
        Task<MedicineInfo?> GetMedicineInfo(string medicineName);
    }
}