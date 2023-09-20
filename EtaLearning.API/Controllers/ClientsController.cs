using Microsoft.AspNetCore.Mvc;
using EtaLearning.API.Models; 

namespace EtaLearning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly object clients;

        public ClientsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetClients")]
        public IActionResult GetClients()
        {
            var clients = _dbContext.Clients.ToList();
            return Ok(clients);
        }

        [HttpPut("EditClient/{id}")]
        public async Task<IActionResult> EditClient(int id, [FromBody] Clients updatedClient)
        {
            var existingClient = await _dbContext.Clients.FindAsync(id);

            if (existingClient == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(updatedClient.Name))
            {
                existingClient.Name = updatedClient.Name;
                await _dbContext.SaveChangesAsync(); 
            }

            return Ok(existingClient);
        }

        [HttpDelete("DeleteClient/{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var clientToDelete = await _dbContext.Clients.FindAsync(id);

            if (clientToDelete == null)
            {
                return NotFound();
            }

            _dbContext.Clients.Remove(clientToDelete);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("CreateNewClient")]
        public IActionResult CreateNewClient([FromBody] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Client name is required.");
            }

            if (_dbContext.Clients.Any(c => c.Name == name))
            {
                return BadRequest("A client with the same name already exists.");
            }

            var newClient = new Clients
            {
                Name = name,
                CreationDate = DateTime.UtcNow
            };

            _dbContext.Clients.Add(newClient);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetClientById), new { id = newClient.Id }, newClient);
        }

        private object GetClientById()
        {
            throw new NotImplementedException();
        }
    }
}
