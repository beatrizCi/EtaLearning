using EtaLearning.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace EtaLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientRepository _clientRepository;

    public ClientsController(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    [HttpGet("get-clients")]
    public IActionResult GetClients()
    {
        var clients = _clientRepository.GetAllAsync();
        return Ok(clients);
    }

    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> GetClientById(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);

        if (client == null)
        {
            return NotFound();
        }

        return Ok(client);
    }

    [HttpPut("edit/{id}")]
    public async Task<IActionResult> EditClient(int id, [FromBody] string name)
    {
        var existingClient = await _clientRepository.GetByIdAsync(id);

        if (existingClient == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(name))
        {
            existingClient.Name = name;
            await _clientRepository.UpdateAsync(existingClient);
        }

        return Ok(existingClient);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var clientToDelete = await _clientRepository.GetByIdAsync(id);

        if (clientToDelete == null)
        {
            return NotFound();
        }

        await _clientRepository.DeleteAsync(id);

        return Ok();
    }

    [HttpGet("check-existence/{id}")]
    public async Task<IActionResult> CheckClientExistence(int id)
    {
        var exists = await _clientRepository.IsClientExistsAsync(id);

        return Ok(exists);
    }
}
