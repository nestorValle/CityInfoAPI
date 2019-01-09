using CityInfo.Abstractions;
using CityInfo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Controllers
{
    [Route("api/cities")]
    public class CitiesController : BaseApiController<CitityWithOutPointsOfInterestDto>
    {
        public CitiesController(ICityService service) : base(service)
        {

        }
    }
}
