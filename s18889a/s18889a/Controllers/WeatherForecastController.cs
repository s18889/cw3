using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using s18889a.DAL;
using s18889a.Models;

namespace s18889a.Controllers
{
    [ApiController]
    [Route("api/projects/3 HTTP")]
    public class WeatherForecastController : ControllerBase
    {
        IDataBaseService _DBS;
        public WeatherForecastController(IDataBaseService DBS)
        {
            _DBS = DBS;
        }




        //public IActionResult Get(int id) // http://localhost:51421/weatherforecast?id=10

        //1.1
        // http://localhost:51421/api/projects/3 HTTP/1.1?id=1
        [HttpGet]
        [Route("1.1")]
        public IActionResult zad11(int id)
        {




            try
            {
                return Ok(_DBS.z11(id));
            }catch(Exception e)
            {
                return NotFound("nie znaleziona danych");
            }
            
        }

        [HttpPut]
        [Route("2")]
        public IActionResult put(addSTH putSth)
        {  

            try
            {
                return Ok(_DBS.z2(putSth));
            }
            catch (Exception e)
            {
                return BadRequest("prawdopodobnie niepoprawne dane");
            }

        }
    }
}
