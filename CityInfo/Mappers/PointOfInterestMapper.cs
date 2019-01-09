using AutoMapper;
using CityInfo.Abstractions;
using CityInfo.Entities;
using CityInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Mappers
{
    public class PointOfInterestMapper : IMapper<PointsOfInterest, PointOfInterestDto>
    {
        public IEnumerable<PointsOfInterest> MapToEntities(IEnumerable<PointOfInterestDto> models)
        {
            return Mapper.Map<IEnumerable<PointsOfInterest>>(models);
        }

        public PointsOfInterest MapToEntity(PointOfInterestDto model)
        {
            return Mapper.Map<PointsOfInterest>(model);
        }

        public PointOfInterestDto MapToModel(PointsOfInterest entity)
        {
            return Mapper.Map<PointOfInterestDto>(entity);
        }

        public IEnumerable<PointOfInterestDto> MapToModels(IEnumerable<PointsOfInterest> entities)
        {
            return Mapper.Map<IEnumerable<PointOfInterestDto>>(entities);
        }
    }
}
