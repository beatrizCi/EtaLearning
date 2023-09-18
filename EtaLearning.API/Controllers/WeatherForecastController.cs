using Microsoft.AspNetCore.Mvc;

namespace EtaLearning.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            return Ok ();
        }
    }
}