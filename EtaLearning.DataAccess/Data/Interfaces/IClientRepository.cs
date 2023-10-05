using EtaLearning.API.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EtaLearning.API.Data
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client> GetById(int id);
        Task UpdateAsync(Client client);
        Task DeleteAsync(int id);
        Task CreateAsync(Client client);
        Task<bool> IsClientExists(int id);
    }
}
