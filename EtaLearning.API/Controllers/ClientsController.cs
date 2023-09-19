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
        public IActionResult CreateNewClient([FromBody] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Client name is required.");
            }

            if (clients.Any(c => c.Name == name))
            {
                return BadRequest("A client with the same name already exists.");
            }

            int newId = clients.Count > 0 ? clients.Max(c => c.Id) + 1 : 1;

            var createdClient = new Clients
            {
                Id = newId,
                Name = name,
                CreationDate = DateTime.UtcNow
            };

            clients.Add(createdClient);

            return CreatedAtAction(nameof(GetClientById), new { id = createdClient.Id }, createdClient);
        }

        [HttpPut("EditClient/{id}")]
        public IActionResult EditClient(int id, [FromBody] Clients updatedClient)
        {
            var existingClient = clients.FirstOrDefault(c => c.Id == id);

            if (existingClient == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(updatedClient.Name))
            {
                existingClient.Name = updatedClient.Name;
            }

            return Ok(existingClient);
        }

        [HttpDelete("DeleteClient/{id}")]
        public IActionResult DeleteClient(int id)
        {
            var clientToDelete = clients.FirstOrDefault(c => c.Id == id);

            if (clientToDelete == null)
            {
                return NotFound();
            }

            clients.Remove(clientToDelete);

            return NoContent();
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
