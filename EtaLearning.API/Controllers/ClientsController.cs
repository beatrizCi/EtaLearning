using EtaLearning.API.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientRepository _clientRepository;

    public ClientsController(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    [HttpGet("ById/{id}")]
    public async Task<IActionResult> GetClientById(int id)
    {
        var client = await _clientRepository.GetById(id);

        if (client == null)
        {
            return NotFound();
        }

        return Ok(client);
    }

    [HttpPut("Edit/{id}")]
    public async Task<IActionResult> EditClient(int id, [FromBody] string name)
    {
        var existingClient = await _clientRepository.GetById(id);

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

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var clientToDelete = await _clientRepository.GetById(id);

        if (clientToDelete == null)
        {
            return NotFound();
        }

        await _clientRepository.DeleteAsync(id);

        return NoContent();
    }

    [HttpGet("CheckExistence/{id}")]
    public async Task<IActionResult> CheckClientExistence(int id)
    {
        var exists = await _clientRepository.IsClientExistsAsync(id);

        return Ok(exists);
    }
}
