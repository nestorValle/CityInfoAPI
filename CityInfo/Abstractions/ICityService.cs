using CityInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Abstractions
{
    public interface ICityService : IApiService<CitityWithOutPointsOfInterestDto>
    {
    }
}
