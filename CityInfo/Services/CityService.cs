using CityInfo.Abstractions;
using CityInfo.Entities;
using CityInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Services
{
    public class CityService : BaseApiService<City, CitityWithOutPointsOfInterestDto>, ICityService
    {
        public CityService(CityInfoContext db, IMapper<City, CitityWithOutPointsOfInterestDto> mapper) : base(db, mapper)
        {
        }
    }
}
