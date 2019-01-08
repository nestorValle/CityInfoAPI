using CityInfo.Models;
using CityInfo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterest: Controller
    {
        private ILogger<PointsOfInterest> _logger;
        private IMailServices _mailService;
        public PointsOfInterest(ILogger<PointsOfInterest> logger, IMailServices mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        [HttpGet("{cityId}/pointofInterest")]
        public IActionResult getPointsOfInterest(int cityId) {
            try
            {
                //throw new Exception("error in pointsOfInteres controller");

                var city = CitiesDataStore.current.cities.FirstOrDefault(f => f.Id == cityId);
                if (city == null)
                {
                    return NotFound();
                }
                return Ok(city.PointsOfInterest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("error in controller method getPointsOfInterest", ex);

                return StatusCode(500, $"Something when wron, finding the {cityId}");
            }
        }

        [HttpGet("{cityId}/pointOfInterest/{id}", Name = "getPointOfInterestById")]
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

        [HttpPost("{cityId}/pointOfinterest")]
        public IActionResult setPointOfInterest(int cityId, [FromBody]PointOfInterestOfCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }
            if (pointOfInterest.Name.Equals(pointOfInterest.Description))
            {
                ModelState.AddModelError("NameDesctiption", "the name and description can't be equal");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existCity = CitiesDataStore.current.cities.FirstOrDefault(e => e.Id.Equals(cityId));
            if (existCity == null)
            {
                return NotFound();
            }
            var nextIdPointOfInteres = existCity.PointsOfInterest.Max(m => m.Id) + 1;
            var dto = new PointOfInterestDto()
            {
                Id = nextIdPointOfInteres,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            existCity.PointsOfInterest.Add(dto);

            return CreatedAtRoute("getPointOfInterestById", new { cityId= cityId, id=nextIdPointOfInteres}, dto);
        }

        [HttpPut("{cityId}/pointOfinterest/{id}")]
        public IActionResult updatePointOfInterest(int cityId, int id, [FromBody]PointOfInterestOfCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }
            if (pointOfInterest.Name.Equals(pointOfInterest.Description))
            {
                ModelState.AddModelError("NameDesctiption", "the name and description can't be equal");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existCity = CitiesDataStore.current.cities.FirstOrDefault(e => e.Id.Equals(cityId));
            if (existCity == null)
            {
                return NotFound();
            }
            var dto = existCity.PointsOfInterest.FirstOrDefault(p => p.Id.Equals(id));
            if (dto == null)
            {
                return NotFound();
            }

            dto.Name = pointOfInterest.Name;
            dto.Description = pointOfInterest.Description;

            return NoContent();
        }

        [HttpDelete("{cityId}/pointOfinterest/{id}")]
        public IActionResult DeletePointOfInteres(int cityId, int id) {
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
            city.PointsOfInterest.Remove(pointOfInterest);

            _mailService
                .Send(
                $"Delete point if interest from city {city}", 
                $"Delete point if interest from city {city} of point of interest with id {id}"
                );

            return NoContent();
        }
    }
}
