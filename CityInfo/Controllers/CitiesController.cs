using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        [HttpGet()]
        public IActionResult getCities() {
            return Ok(CitiesDataStore.current);
        }
        [HttpGet("{id}")]
        public IActionResult getCity(int id) {
            var cityFound = CitiesDataStore.current.cities.FirstOrDefault(f => f.Id.Equals(id));
            if (cityFound != null)
            {
                return Ok(cityFound);
            }

            return NotFound();
        }
    }
}
