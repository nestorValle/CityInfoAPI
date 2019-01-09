using CityInfo.Abstractions;
using CityInfo.Entities;
using CityInfo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Services
{
    public class PointOfInterestService : BaseApiService<PointsOfInterest, PointOfInterestDto>, IPointOfInterestService
    {
        public PointOfInterestService(CityInfoContext db, IMapper<PointsOfInterest, PointOfInterestDto> mapper) : base(db, mapper)
        {            
        }

        public IEnumerable<PointOfInterestDto> GetPointsOfInterestByCity(int cityId)
        {
            var pointsOfInterests = Db.PointsOfInterests.Where(w => w.CityId.Equals(cityId)).ToList();

            return Mapper.MapToModels(pointsOfInterests);
        }
    }
}
