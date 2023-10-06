using EtaLearning.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EtaLearning.API.Data
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _dbContext;

        public ClientRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateAsync(Client client)
        {
            await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            _dbContext.Clients.Update(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _dbContext.Clients.FindAsync(id);
            if (client != null)
            {
                _dbContext.Clients.Remove(client);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<bool> IsClientExistsAsync(int id)
        {
            return await _dbContext.Clients.AnyAsync(c => c.Id == id);
        }

    }
}