using Microsoft.AspNetCore.Mvc;
using EtaLearning.API.Models;

namespace EtaLearning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly List<Clients> clients;

        public ClientsController()
        {
            clients = new List<Clients>
            {
                new Clients
                {
                    Id = 1,
                    Name = "Lustitia Ltd",
                    CreationDate = DateTime.UtcNow
                },
                new Clients
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
        public IActionResult CreateNewClient([FromBody] Clients newClient)
        {
            if (clients.Any(c => c.Name == newClient.Name))
            {
                return BadRequest("A client with the same name already exists.");
            }

            int newId = clients.Count > 0 ? clients.Max(c => c.Id) + 1 : 1;
            newClient.Id = newId;
            newClient.CreationDate = DateTime.UtcNow;

            clients.Add(newClient);

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
}
