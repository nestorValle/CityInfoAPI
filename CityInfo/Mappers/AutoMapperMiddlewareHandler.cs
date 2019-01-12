using CityInfo.Entities;
using CityInfo.Models;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Mappers
{
    public static class AutoMapperMiddlewareHandler
    {
        public static IApplicationBuilder AutoMapperInitialize(this IApplicationBuilder builder) {
            AutoMapper.Mapper.Initialize(c => {
                c.CreateMap<CitityWithOutPointsOfInterestDto, City>();
                c.CreateMap<PointOfInterestDto, PointsOfInterest>();
            });

            return builder;
        }
    }
}
