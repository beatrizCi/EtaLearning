using Microsoft.AspNetCore.Mvc;
using EtaLearning.API.Data;
using EtaLearning.API.Data.Entities;

namespace EtaLearning.API.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetClientById(int id)
        {
            var client = _clientRepository.GetById(id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditClient(int id, [FromBody] string name)
        {
            var existingClient = _clientRepository.GetById(id);

            if (existingClient == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(name))
            {
                existingClient.Name = name;
                _clientRepository.Update(existingClient);
            }

            return Ok(existingClient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var clientToDelete = _clientRepository.GetById(id);

            if (clientToDelete == null)
            {
                return NotFound();
            }

            _clientRepository.Delete(id);

            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateNewClient([FromBody] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Client name is required.");
            }

            if (_clientRepository.ClientExists(name))
            {
                return BadRequest("A client with the same name already exists.");
            }

            var newClient = new Client
            {
                Name = name,
                CreationDate = DateTime.UtcNow
            };

            _clientRepository.Create(newClient);

            return CreatedAtAction(nameof(GetClientById), new { id = newClient.Id }, newClient);
        }
    }
}