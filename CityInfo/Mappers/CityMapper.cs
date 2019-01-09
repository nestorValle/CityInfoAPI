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
    public class CityMapper : IMapper<City, CitityWithOutPointsOfInterestDto>
    {
        public IEnumerable<City> MapToEntities(IEnumerable<CitityWithOutPointsOfInterestDto> models)
        {
            return Mapper.Map<IEnumerable<City>>(models);
        }

        public City MapToEntity(CitityWithOutPointsOfInterestDto model)
        {
            return Mapper.Map<City>(model);
        }

        public CitityWithOutPointsOfInterestDto MapToModel(City entity)
        {
            return Mapper.Map<CitityWithOutPointsOfInterestDto>(entity);
        }

        public IEnumerable<CitityWithOutPointsOfInterestDto> MapToModels(IEnumerable<City> entities)
        {
            return Mapper.Map<IEnumerable<CitityWithOutPointsOfInterestDto>>(entities);
        }
    }
}
