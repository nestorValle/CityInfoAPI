using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterest: Controller
    {
        [HttpGet("{cityId}/pointsofInterest")]
        public IActionResult getPointsOfInterest(int cityId) {
            var city = CitiesDataStore.current.cities.FirstOrDefault(f => f.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{cityId}/pointsOfInterest/{id}")]
        public IActionResult getPointsOfInterestById(int cityId, int id) {
            var city = CitiesDataStore.current.cities.FirstOrDefault(f => f.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id.Equals(id));
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointOfInterest);
        }
    }
}
