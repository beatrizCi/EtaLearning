
using EtaLearning.API.Data.Entities;
using EtaLearning.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EtaLearning.DataAccess.Data.Interfaces
{
    public interface ISmartDeviceRepository
    {
        Task<List<SmartDevice>> GetAllAsync();
        Task<SmartDevice> GetByIdAsync(Guid id);
        Task AddAsync(SmartDevice smartDevice);
        Task<SmartDevice> DeleteAsync(int id);

        Task UpdateAsync(SmartDevice smartDevice);
        Task<SmartDevice> GetByIdAsync(int id);
    }
}
