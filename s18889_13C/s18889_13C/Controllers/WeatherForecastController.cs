using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using s18889a.DAL;

namespace s18889a.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        IDataBaseService _DBS;
        public WeatherForecastController(IDataBaseService DBS)
        {
            _DBS = DBS;
        }


/*
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
*/
        [HttpGet]
        public IActionResult Get()
        {

            return Ok(_DBS.test());
        }
    }
}
