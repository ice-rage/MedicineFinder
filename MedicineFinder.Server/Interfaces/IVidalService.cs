using MedicineFinder.Server.Models;

namespace MedicineFinder.Server.Interfaces
{
    public interface IVidalService
    {
        Task<Root?> GetMedicineInfo(string medicineName);
    }
}