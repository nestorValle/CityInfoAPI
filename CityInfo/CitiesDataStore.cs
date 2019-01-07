using CityInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo
{
    public class CitiesDataStore
    {
        public static CitiesDataStore current { get; } = new CitiesDataStore();

        public List<CityDto> cities { get; set; }

        public CitiesDataStore() {
            cities = new List<CityDto>() {
                new CityDto(){
                    Id =1,
                    Name ="New York",
                    Description ="The one with the big park",
                    PointsOfInterest = new List<PointOfInterestDto>(){
                        new PointOfInterestDto(){
                            Id =1,
                            Name ="Central park",
                            Description ="The most visit urban park visit in the USA"
                        },
                        new PointOfInterestDto(){
                            Id =2,
                            Name ="Empire state building",
                            Description ="A 102-story skyscraper"
                        }
                    }
                },
                new CityDto(){ Id=2, Name="Antwerp", Description="The one with a cathedral that was never really finished",
                    PointsOfInterest = new List<PointOfInterestDto>(){
                        new PointOfInterestDto(){
                            Id =1,
                            Name ="Central park",
                            Description ="The most visit urban park visit in the USA"
                        },
                        new PointOfInterestDto(){
                            Id =2,
                            Name ="Empire state building",
                            Description ="A 102-story skyscraper"
                        }
                    }
                },
                new CityDto(){ Id=3, Name="Paris", Description="The one with the big tower",
                    PointsOfInterest = new List<PointOfInterestDto>(){
                        new PointOfInterestDto(){
                            Id =1,
                            Name ="Central park",
                            Description ="The most visit urban park visit in the USA"
                        },
                        new PointOfInterestDto(){
                            Id =2,
                            Name ="Empire state building",
                            Description ="A 102-story skyscraper"
                        }
                    }
                },
            };
        }
    }
}
