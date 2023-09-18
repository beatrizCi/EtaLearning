using EtaLearning.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace EtaLearning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly List<Client> clients;

        public ClientsController()
        {
           
            clients = new List<Client>
            {
                new Client
                {
                    Id = 1,
                    Name = "Lustitia Ltd",
                    CreationDate = DateTime.UtcNow
                },
                new Client
                {
                    Id = 2,
                    Name = "Bachmann",
                    CreationDate = DateTime.UtcNow
                }
            };
        }

       

    [HttpGet("GetClients")]
    public IActionResult GetClients()
    {
     
        return Ok(clients);
    }
        [HttpPost("CreateNewClient")]
        public IActionResult CreateNewClient([FromBody] Client newClient)
        {
            // Check if a client with the same name already exists in the list
            if (clients.Any(c => c.Name == newClient.Name))
            {
                return BadRequest("A client with the same name already exists.");
            }

            // Calculate the new ID by incrementing the last ID in the collection
            int newId = clients.Count > 0 ? clients.Max(c => c.Id) + 1 : 1;

            // Set the new ID and current DateTime for the new client
            newClient.Id = newId;
            newClient.CreationDate = DateTime.UtcNow;

            // Add the new client to the list
            clients.Add(newClient);

            // Return the newly created client with a 201 Created status code
            return CreatedAtAction(nameof(GetClientById), new { id = newClient.Id }, newClient);
        }

        [HttpGet("GetClientById/{id}")]
    public IActionResult GetClientById(int id)
    {
     
        var client = clients.FirstOrDefault(c => c.Id == id);

        if (client == null)
        {
            return NotFound();
        }

        return Ok(client);
    }
}
public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
