namespace EtaLearning.API.Data.Entities
{
    public class Client : ClientBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
