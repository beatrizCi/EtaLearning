namespace EtaLearning.API.Data.Entities
{
    public class ClientBase
    {
        public Task<Client> GetByIdAsync(int id);
    }
}