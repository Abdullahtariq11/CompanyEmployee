using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployee.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        public WeatherForecastController(ILoggerManager loggerManager)
        {
            _logger= loggerManager;
        }


        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInfo($"WeatherForecast");
            _logger.LogDebug("debug log tyhi is log islsd");
            _logger.LogWarn("info dfgdfgfdgdf dfvgfdg");
            _logger.LogError("error fdgfgddf fdgfgdfgdbfd");
            return new string[] { "val1", "val2" };
        }
    }
}
