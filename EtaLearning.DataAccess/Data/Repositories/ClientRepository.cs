using EtaLearning.API.Data.Entities;
using EtaLearning.API.Data;

namespace EtaLearning.API.Data
{
    public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _dbContext;

    public ClientRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Client> GetAll()
    {
        return _dbContext.Clients.ToList();
    }

    public Client GetById(int id)
    {
        return _dbContext.Clients.FirstOrDefault(c => c.Id == id);
    }

    public void Create(Client client)
    {
        _dbContext.Clients.Add(client);
        _dbContext.SaveChanges();
    }

    public void Update(Client client)
    {
        _dbContext.Clients.Update(client);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var client = _dbContext.Clients.Find(id);
        if (client != null)
        {
            _dbContext.Clients.Remove(client);
            _dbContext.SaveChanges();
        }
    }
}
}