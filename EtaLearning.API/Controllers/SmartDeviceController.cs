using EtaLearning.DataAccess.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EtaLearning.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmartDeviceController : ControllerBase
    {
     
        private readonly ISmartDeviceRepository _smartdeviceRepository;

        public SmartDeviceController(ISmartDeviceRepository smartdevicesRepository)
        {
            _smartdeviceRepository = smartdevicesRepository;
        }

        [HttpGet("GetClients")]
        public IActionResult GetClients()
        {
            var clients = _smartdeviceRepository.GetAllAsync();
            return Ok(clients);
        }

        [HttpGet("ById/{id}")]
        public async Task<IActionResult> GetClientById( int id)
        {
            var client = await _smartdeviceRepository.GetByIdAsync( id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditClient(int id, [FromBody] string name)
        {
            var existingClient = await _smartdeviceRepository.GetByIdAsync(id);

            if (existingClient == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(name))
            {
                existingClient.Name = name;
                await _smartdeviceRepository.UpdateAsync(existingClient);
            }

            return Ok(existingClient);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var clientToDelete = await _smartdeviceRepository.GetByIdAsync(id);

            if (clientToDelete == null)
            {
                return NotFound();
            }

            await _smartdeviceRepository.DeleteAsync(id);

            return NoContent();
        }

    }
}
