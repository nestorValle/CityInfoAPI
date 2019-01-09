using CityInfo.Abstractions;
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
    public class PointsOfInterestController: Controller
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailServices _mailService;
        private readonly IPointOfInterestService _pointOfInterestservice;
        private readonly ICityService _cityService;

        public PointsOfInterestController(
            ILogger<PointsOfInterestController> logger, 
            IMailServices mailService,
            ICityService cityService,
            IPointOfInterestService pointOfInterestservice)
        {
            _logger = logger;
            _mailService = mailService;
            _pointOfInterestservice = pointOfInterestservice;
            _cityService = cityService;
        }

        [HttpGet("{cityId}/pointofInterest")]
        public IActionResult getPointsOfInterest(int cityId) {
            try
            {
                var city = _cityService.GetById(cityId);
                if (city == null)
                {
                    return NotFound();
                }
                var pointsOfInterest = _pointOfInterestservice.GetPointsOfInterestByCity(cityId);
                return Ok(pointsOfInterest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("error in controller method getPointsOfInterest", ex);

                return StatusCode(500, $"Something when wron, finding the {cityId}");
            }
        }

        [HttpGet("{cityId}/pointOfInterest/{id}", Name = "getPointOfInterestById")]
        public IActionResult getPointsOfInterestById(int cityId, int id) {
            
            var pointOfInterest = _pointOfInterestservice.GetById(id);
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
            var existCity = _cityService.GetById(cityId);
            if (existCity == null)
            {
                return NotFound();
            }

            int createdId = _pointOfInterestservice.Create(new PointOfInterestDto() {
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description,
                CityId = cityId
            });

            return CreatedAtRoute("getPointOfInterestById", new { cityId= cityId, id= createdId }, pointOfInterest);
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
            var existCity = _cityService.GetById(cityId, true);
            if (existCity == null)
            {
                return NotFound();
            }

            var existPointOfInterest = _pointOfInterestservice.GetById(id, true);
            if (existPointOfInterest == null)
            {
                return NotFound();
            }

            var updatePoint = new PointOfInterestDto() {
                Id = existPointOfInterest.Id,
                CityId = cityId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };
            _pointOfInterestservice.Update(updatePoint);

            return NoContent();
        }

        [HttpDelete("{cityId}/pointOfinterest/{id}")]
        public IActionResult DeletePointOfInteres(int cityId, int id) {
            var city = _cityService.GetById(cityId, true);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = _pointOfInterestservice.GetById(id, true);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            _pointOfInterestservice.Delete(id);
            _mailService
                .Send(
                $"Delete point if interest from city {city}", 
                $"Delete point if interest from city {city} of point of interest with id {id}"
                );

            return NoContent();
        }
    }
}
