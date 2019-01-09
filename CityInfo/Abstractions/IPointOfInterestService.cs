using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Models;

namespace CityInfo.Abstractions
{
    public interface IPointOfInterestService : IApiService<PointOfInterestDto>
    {   
        IEnumerable<PointOfInterestDto> GetPointsOfInterestByCity(int cityId);
    }
}
