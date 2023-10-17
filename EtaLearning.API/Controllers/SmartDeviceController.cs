using EtaLearning.DataAccess.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace EtaLearning.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmartDeviceController : ControllerBase
    {
        private readonly ISmartDeviceRepository _smartdeviceRepository;

        public SmartDeviceController(ISmartDeviceRepository smartdeviceRepository)
        {
            _smartdeviceRepository = smartdeviceRepository;
        }

        [HttpGet("GetClients")]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                var clients = await _smartdeviceRepository.GetAllAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("ById/{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            try
            {
                Guid guidId = new Guid(id.ToString());

                var client = await _smartdeviceRepository.GetByIdAsync(guidId);

                if (client == null)
                {
                    return NotFound($"Client with Id {id} not found.");
                }

                return Ok(client);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditClient(Guid id, [FromBody] string name)
        {
            try
            {
                var existingClient = await _smartdeviceRepository.GetByIdAsync(id);

                if (existingClient == null)
                {
                    return NotFound($"Client with Id {id} not found.");
                }

                if (existingClient.Name != name)
                {
                    return Conflict($"The client's name has been updated by another user. Refresh your data and try again.");
                }

                if (!string.IsNullOrEmpty(name))
                {
                    existingClient.Name = name;
                    await _smartdeviceRepository.UpdateAsync(existingClient);
                    return Ok(existingClient);
                }
                else
                {
                    return BadRequest("Name cannot be empty.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            try
            {
                var clientToDelete = await _smartdeviceRepository.GetByIdAsync(id);

                if (clientToDelete == null)
                {
                    return NotFound($"Client with Id {id} not found.");
                }

                await _smartdeviceRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
