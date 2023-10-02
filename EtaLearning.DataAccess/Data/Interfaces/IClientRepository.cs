using EtaLearning.API.Data.Entities;


namespace EtaLearning.API.Data
{
    public interface IClientRepository
{
    IEnumerable<Client> GetAll();
    Client GetById(int id);
    void Create(Client client);
    void Update(Client client);
    void Delete(int id);
        bool ClientExists(string name);
    }
}
